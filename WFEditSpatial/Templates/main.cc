// -*- tab-width: 4; indent-tabs-mode: nil -*-
/** \file

    \brief Solve two-component diffusion-reaction system
    with finite volume scheme. This class tests the
    multi domain grid.
*/

#ifdef HAVE_CONFIG_H
#include "config.h"
#endif

#include <math.h>
#include <fstream>
#include <iostream>
#include <vector>
#include <map>
#include <string>
#include <stdlib.h>

#include <dune/common/parallel/mpihelper.hh>
#include <dune/common/exceptions.hh>
#include <dune/common/fvector.hh>
#include <dune/common/static_assert.hh>
#include <dune/common/timer.hh>
#include <dune/common/parametertreeparser.hh>
#include <dune/grid/io/file/vtk/subsamplingvtkwriter.hh>
#include <dune/grid/io/file/gmshreader.hh>
#include <dune/grid/yaspgrid.hh>
#include <dune/grid/multidomaingrid/indexsets.hh>
#include <dune/grid/multidomaingrid/subdomainset.hh>
#include <dune/grid/multidomaingrid.hh>
#include <dune/grid/common/gridinfo.hh>

#if HAVE_ALBERTA
#include <dune/grid/albertagrid.hh>
#include <dune/grid/albertagrid/dgfparser.hh>
#endif

#if HAVE_UG
#include <dune/grid/uggrid.hh>
#endif

#if HAVE_ALUGRID
#include <dune/grid/alugrid.hh>
#include <dune/grid/io/file/dgfparser/dgfalu.hh>
#include <dune/grid/io/file/dgfparser/dgfparser.hh>
#endif

#include <dune/istl/bvector.hh>
#include <dune/istl/operators.hh>
#include <dune/istl/solvers.hh>
#include <dune/istl/preconditioners.hh>
#include <dune/istl/io.hh>
#include <dune/istl/superlu.hh>

#include <dune/copasi/utilities/newton.hh>
#include <dune/copasi/utilities/newtonutilities.hh>
#include <dune/copasi/utilities/timemanager.hh>
#include <dune/copasi/utilities/datahelper.hh>
#include <dune/copasi/utilities/sbmlhelper.hh>
#include <dune/copasi/utilities/solutioncontrol.hh>

#include <dune/pdelab/common/function.hh>
#include <dune/pdelab/common/vtkexport.hh>
#include <dune/pdelab/instationary/pvdwriter.hh>

#include <dune/pdelab/backend/istl/tags.hh>
#include <dune/pdelab/backend/istl/descriptors.hh>
#include <dune/pdelab/backend/istl/bcrsmatrixbackend.hh>

#include <dune/pdelab/backend/istlvectorbackend.hh>
#include <dune/pdelab/backend/istlmatrixbackend.hh>
#include <dune/pdelab/backend/istlsolverbackend.hh>
#include <dune/pdelab/backend/seqistlsolverbackend.hh>

#include <dune/pdelab/constraints/conforming.hh>
#include <dune/pdelab/constraints/constraints.hh>
#include <dune/pdelab/ordering/orderingbase.hh>

#include <dune/pdelab/finiteelementmap/p0fem.hh>
#include <dune/pdelab/constraints/p0.hh>
#include <dune/pdelab/constraints/constraints.hh>
#include <dune/pdelab/gridoperator/gridoperator.hh>
#include <dune/pdelab/gridoperator/onestep.hh>
#include <dune/pdelab/stationary/linearproblem.hh>
#include <dune/pdelab/instationary/onestep.hh>
#include <dune/pdelab/gridfunctionspace/subspace.hh>

#include <dune/pdelab/multidomain/multidomaingridfunctionspace.hh>
#include <dune/pdelab/multidomain/coupling.hh>
#include <dune/pdelab/multidomain/subproblemlocalfunctionspace.hh>
#include <dune/pdelab/multidomain/gridoperator.hh>
#include <dune/pdelab/multidomain/subproblem.hh>
#include <dune/pdelab/constraints/conforming.hh>
#include <dune/pdelab/multidomain/constraints.hh>
#include <dune/pdelab/multidomain/interpolate.hh>
#include <dune/pdelab/multidomain/vtk.hh>

#include "componentparameters.hh"
#include "local_operator.hh"
#include "initial_conditions.hh"
#include "reactionadapter.hh"

#include <dune/copasi/utilities/solutiontimeoutput.hh>

/**
 * global data helper variable, that can be used to exchange the geometry
 * via config file
 */

DataHelper* dh_geometry = NULL;
double geometry_min = 10;
double geometry_max = 10;


EVENT_SUPPORT_GLOBALS(mEvents, skipOutputUntilEvent, appliedEvent);

/** \brief Control time step after reaction.

    If some concentration is negative, then it returns false.
    Otherwise it returns true

    \tparam V           Vector backend (containing DOF)
*/
template<typename V>
bool controlReactionTimeStep (V &v)
{
    for (auto it=v.begin(); it!=v.end();++it)
        if (*it<0) return false;

    return true;
}

template<class GV>
void run (const GV& gv, Dune::ParameterTree & param)
{
    const bool verbosity = param.sub("Verbosity").get<bool>("verbosity", false);
    if (verbosity)
    Dune::gridinfo(gv.grid());
    typedef typename GV::Grid::ctype DF;
    typedef double RF;
    const int dim = GV::dimension;
    Dune::Timer watch;

    std::string timestepping = param.get<std::string>("timestepping");


    // output to vtk file (true/false)
    const bool graphics = param.get<bool>("writeVTK", false);

    ConvergenceAdaptiveTimeStepper<RF> timemanager(param);

    // for each component parameters different classes
    typedef DiffusionParameter<GV,RF> DP;
%DPCREATION%

    typedef Dune::PDELab::MulticomponentDiffusion<GV,RF,DP,%NUMCOMPONENTS%> VCT;
    VCT vct(%DPINITIALIZATION%);

    // <<<2>>> Make grid function space
    typedef Dune::PDELab::P0LocalFiniteElementMap<DF,RF,dim> FEM;
    FEM fem(Dune::GeometryType(Dune::GeometryType::cube,dim));
    typedef Dune::PDELab::P0ParallelConstraints CON;
    typedef Dune::PDELab::ISTLVectorBackend<> VBE0;
    typedef Dune::PDELab::GridFunctionSpace<GV,FEM,CON,VBE0> GFS;

    typedef Dune::PDELab::ISTLVectorBackend<> VBE;
            //<Dune::PDELab::ISTLParameters::static_blocking,VCT::COMPONENTS> VBE;
    typedef Dune::PDELab::PowerGridFunctionSpace<GFS,VCT::COMPONENTS,VBE,
            Dune::PDELab::EntityBlockedOrderingTag> TPGFS;
    watch.reset();
    CON con;
    GFS gfs(gv,fem,con);
    TPGFS tpgfs(gfs);
    if (verbosity) std::cout << "=== function space setup " <<  watch.elapsed() << " s" << std::endl;

    // some informations
    tpgfs.update();
    if (verbosity) std::cout << "number of DOF =" << tpgfs.globalSize() << std::endl;

    watch.reset();


    // <<<2b>>> make subspaces for visualization
%SUBCREATION%


   if (verbosity) std::cout << "=== grid function subspaces " << watch.elapsed() << " s" << std::endl;
   watch.reset();

    // make constraints map and initialize it from a function (boundary conditions)
    typedef typename TPGFS::template ConstraintsContainer<RF>::Type CC;
    CC cg;
    cg.clear();
    Dune::PDELab::constraints(tpgfs,cg,false);

    typedef ReactionAdapter<RF> RA;
    RA ra(param);

    typedef Dune::PDELab::MulticomponentCCFVSpatialDiffusionOperator<VCT,RA> LOP;
    LOP lop(vct,ra); //local operator including reaction adapter
    typedef Dune::PDELab::MulticomponentCCFVTemporalOperator<VCT> TLOP;
    TLOP tlop(vct);
    typedef Dune::PDELab::istl::BCRSMatrixBackend<> MBE;
    MBE mbe(27); // Maximal number of nonzeroes per row can be cross-checked by patternStatistics().
    // grid operators
    typedef Dune::PDELab::GridOperator<TPGFS,TPGFS,LOP,MBE,RF,RF,RF,CC,CC> CGO0;
    CGO0 cgo0(tpgfs,cg,tpgfs,cg,lop,mbe);
    typedef Dune::PDELab::GridOperator<TPGFS,TPGFS,TLOP,MBE,RF,RF,RF,CC,CC> CGO1;
    CGO1 cgo1(tpgfs,cg,tpgfs,cg,tlop,mbe);
    // one step grid operator for instationary problems


    typedef Dune::PDELab::OneStepGridOperator<CGO0, CGO1, false> EIGO;
    typedef Dune::PDELab::OneStepGridOperator<CGO0, CGO1, true> DIGO;

    DIGO digo(cgo0, cgo1);
    EIGO eigo(cgo0, cgo1);


    // <<<7>>> make vector for old time step and initialize
    typedef typename Dune::PDELab::BackendVectorSelector<TPGFS, RF>::Type V;
    V uold(tpgfs,0.0);
    V unew(tpgfs,0.0);

    // initial conditions
    typedef UGeneralInitial<GV, RF> UGeneralInitialType;
%INITIALTYPECREATION%


    typedef Dune::PDELab::PowerGridFunction<UGeneralInitialType, %NUMCOMPONENTS%> UInitialType;
    UInitialType uinitial(%INITIALARGS%); //new

    //typedef Dune::PDELab::CompositeGridFunction<%INITIALTYPES%> UInitialType; //new
    //UInitialType uinitial(%INITIALARGS%); //new
    Dune::PDELab::interpolate(uinitial,tpgfs,uold);
    unew = uold;
    
    if (verbosity) std::cout << "=== initial conditions " << watch.elapsed() << " s" << std::endl;
  // <<<5>>> Select a linear solver backend
#if HAVE_MPI
#if HAVE_SUPERLU
    typedef Dune::PDELab::ISTLBackend_BCGS_AMG_SSOR<DIGO> DLS;
    DLS dls(tpgfs, param.sub("Newton").get<int>("LSMaxIterations", 100), param.sub("Newton").get<int>("LinearVerbosity", 0));
#else
    typedef Dune::PDELab::ISTLBackend_OVLP_BCGS_SSORk<GFS, CC> DLS;
    DLS dls(tpgfs, cc, 5000, 5, param.sub("Newton").get<int>("LinearVerbosity", 0));
#endif
#else //!parallel
#if HAVE_SUPERLU
    typedef Dune::PDELab::ISTLBackend_SEQ_SuperLU DLS;
    DLS dls(param.sub("Newton").get<int>("LinearVerbosity", 0));
#else
    typedef Dune::PDELab::ISTLBackend_SEQ_BCGS_SSOR DLS;
    DLS dls(5000, param.sub("Newton").get<int>("LinearVerbosity", 0));
#endif
#endif

    typedef Dune::PDELab::ISTLBackend_OVLP_ExplicitDiagonal<TPGFS> ELS;
    ELS els(tpgfs);

    if (verbosity) std::cout << "=== linear solvers " << watch.elapsed() << " s" << std::endl;
    // <<<6>>> Solver for non-linear problem per stage
    typedef Dune::PDELab::Newton<DIGO, DLS, V> CPDESOLVER;
    CPDESOLVER pdesolver(digo, dls);
    pdesolver.setLineSearchStrategy(CPDESOLVER::hackbuschReuskenAcceptBest); //strategy for linesearch
    typedef Dune::PDELab::NewtonParameters NewtonParameters;
    NewtonParameters newtonparameters(param.sub("Newton"));
    newtonparameters.set(pdesolver);
    typedef Dune::PDELab::OneStepMethod<RF, DIGO, CPDESOLVER, V, V> OSMI;
    Dune::PDELab::Alexander2Parameter<RF> imethod;
    OSMI osmi(imethod, digo, pdesolver);

    // <<<7>>> time-stepper
    typedef Dune::PDELab::SimpleTimeController<RF> TC;
    TC tc;

    typedef Dune::PDELab::ExplicitOneStepMethod<RF, EIGO, ELS, V, V, TC> OSME;
    Dune::PDELab::ExplicitEulerParameter<RF> emethod;
    OSME osme(emethod, eigo, els, tc);

    if (verbosity) std::cout << "=== osm " << watch.elapsed() << " s" << std::endl;

    Dune::PDELab::TimeSteppingMethods<RF> timesteppingmethods;
    timesteppingmethods.setTimestepMethod(osme, param.get<std::string>("explicitsolver"));
    timesteppingmethods.setTimestepMethod(osmi, param.get<std::string>("implicitsolver"));

    osme.setVerbosityLevel(param.sub("Verbosity").get<int>("Instationary", 0));
    osmi.setVerbosityLevel(param.sub("Verbosity").get<int>("Instationary", 0));

    char basename[255];
    sprintf(basename,"%s-%01d",param.get<std::string>("VTKname","").c_str(),param.sub("Domain").get<int>("refine"));

    typedef Dune::PVDWriter<GV> PVDWriter;
    PVDWriter pvdwriter(gv,basename,Dune::VTK::conforming);
    // discrete grid functions
%CREATEGRIDFUNCTIONS%

    if (verbosity) std::cout << "=== output " << watch.elapsed() << " s" << std::endl;

    // <<<9>>> time loop

    if (graphics)
        pvdwriter.write(0);

    if (verbosity) std::cout << "=== output0 " << watch.elapsed() << " s" << std::endl;

    EVENT_SUPPORT_INITIALIZE(gv);

    double dt = timemanager.getTimeStepSize();
    while (!timemanager.finalize())
    {

        dt = timemanager.getTimeStepSize();
        if (gv.comm().rank() == 0)
        {
            if (verbosity)
            {
                std::cout << "======= solve reaction problem ======= from " << timemanager.getTime() << "\n";
                std::cout << "time " << timemanager.getTime() << " dt " << timemanager.getTimeStepSize() << std::endl;
            }           
        }

        watch.reset();
        try
        {
            // do time step
            if (timestepping == "explicit")
              osme.apply(timemanager.getTime(), dt, uold, unew);
            else
              osmi.apply(timemanager.getTime(), dt, uold, unew);

            if (!controlReactionTimeStep(unew))
            {
                timemanager.notifyFailure();
                unew = uold;
                continue;

            }

            uold = unew;

            if (gv.comm().rank() == 0 && verbosity)
                std::cout << "... done\n";
        }
        // newton linear search error
        catch (Dune::PDELab::NewtonLineSearchError) {
            if (gv.comm().rank() == 0 && verbosity)
                std::cout << "Newton Linesearch Error" << std::endl;
            timemanager.notifyFailure();
            unew = uold;
            if (!verbosity)
            {
              std::cout << "x" << std::flush;
            }
            continue;
        }
        catch (Dune::PDELab::NewtonNotConverged) {
            if (gv.comm().rank() == 0 && verbosity)
                std::cout << "Newton Convergence Error" << std::endl;
            timemanager.notifyFailure();
            unew = uold;
            if (!verbosity)
            {
              std::cout << "x" << std::flush;
            }
            continue;
        }
        catch (Dune::PDELab::NewtonLinearSolverError) {
            if (gv.comm().rank() == 0 && verbosity )
                std::cout << "Newton Linear Solver Error" << std::endl;
            timemanager.notifyFailure();
            unew = uold;
            if (!verbosity)
            {
              std::cout << "x" << std::flush;
            }
            continue;
        }
        catch (Dune::ISTLError) {
            if (gv.comm().rank() == 0 && verbosity )
                std::cout << "ISTL Error" << std::endl;
            timemanager.notifyFailure();
            unew = uold;
            if (!verbosity)
            {
              std::cout << "x" << std::flush;
            }
            continue;
        }

        if (gv.comm().rank() == 0 && verbosity)
            std::cout << "... took : " << watch.elapsed() << " s" << std::endl;

        // notify success in this timestep
        timemanager.notifySuccess(5);

        EVENT_SUPPORT_HANDLE_EVENTS(timemanager.getTime(), %NUMCOMPONENTS%, mEvents, uold, unew);

        if (graphics && timemanager.isTimeForOutput())
        {
            if ((skipOutputUntilEvent && appliedEvent) || !skipOutputUntilEvent)  
            {
                pvdwriter.write(timemanager.getTime());
                if (!verbosity)
                    std::cout << "o" << std::flush;
            }
            else if (!verbosity)
            {
              std::cout << "n" << std::flush;
            }
        }
        else if (!verbosity)
        {
          std::cout << "." << std::flush;
        }
    }


    std::cout << std::endl;

}


bool isInside(const Dune::FieldVector<double, 2>& point, const Dune::FieldVector<double, 2>& dimension)
{
  const auto& x = point[0];
  const auto& y = point[1];
  const auto& width = dimension[0];
  const auto& height = dimension[1];

  if (dh_geometry != NULL)
  {
    double val = (*dh_geometry).get((double)x, (double)y);
    bool inside = val >= (geometry_min - 1e-5) && val <= (geometry_max + 1e-5);
    return inside;
  }

%GEOMETRY%

}

//===============================================================
// Main program with grid setup
//===============================================================

int main(int argc, char** argv)
{
    try{
        //Maybe initialize Mpi
        Dune::MPIHelper& helper = Dune::MPIHelper::instance(argc, argv);

        std::string configfile = argv[0]; configfile += ".conf";

        // parse cmd line parameters
        if (argc > 1 && argv[1][0] != '-')
            configfile = argv[1];
        for (int i = 1; i < argc; i++)
        {
            if (std::string(argv[i]) == "--help" || std::string(argv[i]) == "-h")
            {
                if(helper.rank()==0)
                    std::cout << "usage: ./%NAME% <configfile> [OPTIONS]" << std::endl;
                return 0;
            }
        }

        // read parameters from file
        Dune::ParameterTree param;
        Dune::ParameterTreeParser parser;
        parser.readINITree(configfile, param);

        EVENT_SUPPORT_READ_DATA(mEvents, parser, "eventinfo.ini", skipOutputUntilEvent);

        int dim=param.sub("Domain").get<int>("dim", 2);

        dh_geometry = DataHelper::forFile(param.sub("Domain").template get<std::string>("geometry", ""));
        geometry_min = param.sub("Domain").template get<double>("geometry_min", 0);
        geometry_max = param.sub("Domain").template get<double>("geometry_max", 0);

        if (dim==2)
        {
            // make grid
            Dune::FieldVector<double,2> L;
            L[0] = param.get<double>("Domain.height");
            L[1] = param.get<double>("Domain.width");

            std::array<int,2> N;
            N[0] = param.get<int>("Domain.nx");
            N[1] = param.get<int>("Domain.ny");

            std::bitset<2> periodic(false);
            int overlap = param.get<int>("overlap", 0);

            typedef Dune::YaspGrid<2> HostGrid;
            HostGrid hostgrid(helper.getCommunicator(),L,N,periodic,overlap);
            hostgrid.globalRefine(param.get<int>("Domain.refine",0));

            typedef Dune::MultiDomainGrid<HostGrid,Dune::mdgrid::ArrayBasedTraits<2,8,8> > MDGrid;
            MDGrid mdgrid(hostgrid,true);
            typedef typename MDGrid::LeafGridView MDGV;
            typedef typename MDGV::template Codim<0>::Iterator Iterator;
            typedef typename MDGrid::SubDomainIndexType SubDomainIndexType;

            MDGV mdgv = mdgrid.leafGridView();
            mdgrid.startSubDomainMarking();
            for(Iterator it = mdgv.template begin<0>(); it != mdgv.template end<0>(); ++it)
            {
                SubDomainIndexType subdomain = 0;
                mdgrid.removeFromAllSubDomains(*it);

                // figure out, whether it is part of the
                // region, and if so add it like so:

                const auto& center = it->geometry().center();
                if (isInside(center, L))
                {
                    mdgrid.addToSubDomain(subdomain,*it);
                }
            }

            mdgrid.preUpdateSubDomains();
            mdgrid.updateSubDomains();
            mdgrid.postUpdateSubDomains();

            typedef MDGrid::SubDomainGrid SDGrid;
            const SDGrid& sdgrid = mdgrid.subDomain(0);
            SDGrid::LeafGridView sdgv = sdgrid.leafGridView();

            // solve problem
            run(sdgv,param);
        }
    }
    catch (Dune::Exception &e){
        std::cerr << "Dune reported error: " << e << std::endl;
        return 1;
    }
    //catch (...){
    //  std::cerr << "Unknown exception thrown!" << std::endl;
    //  return 1;
    //}
}

