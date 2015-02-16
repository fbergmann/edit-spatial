// -*- tab-width: 4; indent-tabs-mode: nil; c-basic-offset: 2 -*-
// vi: set ts=4 sw=2 et sts=2:
#ifndef DUNE_COPASI_DIFFUSIONPARAMETERS_HH
#define DUNE_COPASI_DIFFUSIONPARAMETERS_HH

#include <dune/copasi/utilities/componentparameters.hh>
#include <dune/copasi/utilities/datahelper.hh>

#include "local_operator.hh"

/** 
 * Generic DiffusionParameter for all variables. 
 *
 * It is to be initialized from a config file, with the following parameters: 
 * 
 *  - D    -> double, the diffusion coefficient
 *  - Xmin -> double, boundary condition at left boundary
 *  - Xmax -> double, boundary condition at right boundary
 *  - Ymin -> double, boundary condition at top boundary
 *  - Ymax -> double, boundary condition at bottom boundary
 *
 *  - BCType: int, the type of boundary condition
 *            Dirichlet=1, Neumann=-1, Outflow=-2, None=-3
 * 
 * Additionally the values returned can be overwritten by files. For that set the 
 * parameters below to dmp files. 
 *
 *  - file_bcType, a file containing bctypes for all coordinates
 *  - file_dc, a file containing the diffusion coefficients for the coordinates
 *  - file_neumann, a file containing Neumann boundary values for the coordinates
 *  - file_dirichlet, a file containing Dirichlet boundary values for the coordinates
 *  - file_compartment, a file 1 wherever inside compartment and 0 outside for the coordinates
 */
template<typename GV, typename RF>
class DiffusionParameter :
  public Dune::PDELab::DiffusionMulticomponentInterface<Dune::PDELab::DiffusionParameterTraits<GV, RF>,
  DiffusionParameter<GV, RF> >
{
  enum { dim = GV::Grid::dimension };

public:
  typedef Dune::PDELab::DiffusionParameterTraits<GV, RF> Traits;
  typedef typename Traits::BCType BCType;

  DiffusionParameter(const Dune::ParameterTree & param, const std::string cname)
    : time(0.)
    , Dt(param.sub(cname).template get<RF>("D"))
    , Xmin(param.sub(cname).template get<RF>("Xmin", 0))
    , Xmax(param.sub(cname).template get<RF>("Xmax", 0))
    , Ymin(param.sub(cname).template get<RF>("Ymin", 0))
    , Ymax(param.sub(cname).template get<RF>("Ymax", 0))
    , width(param.sub("Domain").template get<int>("width", 0))
    , height(param.sub("Domain").template get<int>("height", 0))
    , boundarytype(BCType::Neumann)
    , dh_bcType(DataHelper::forFile(param.sub(cname).template get<std::string>("file_bcType", "")))
    , dh_neumann(DataHelper::forFile(param.sub(cname).template get<std::string>("file_neumann", "")))
    , dh_dirichlet(DataHelper::forFile(param.sub(cname).template get<std::string>("file_dirichlet", ""), NearestNeighbor))
    , dh_dc(DataHelper::forFile(param.sub(cname).template get<std::string>("file_dc", "")))
    , dh_compartment(DataHelper::forFile(param.sub(cname).template get<std::string>("file_compartment", ""), NearestNeighbor))

  {
    int bc = param.sub(cname).template get<int>("BCType");
    switch (bc) {
    case 1:
      boundarytype = BCType::Dirichlet;
      break;
    case -1:
      boundarytype = BCType::Neumann;
      break;
    case -2:
      boundarytype = BCType::Outflow;
      break;
    case -3:
      boundarytype = BCType::None;
      break;
    }
  }


  //! tensor permeability
  typename Traits::RangeFieldType
    D(const typename Traits::ElementType& e, const typename Traits::DomainType& x_) const
  {
    typename Traits::DomainType x = e.geometry().global(x_);
    
    if (dh_dc == NULL)
    {
      if (dh_compartment == NULL) return Dt;
      return Dt * dh_compartment->get(double(x[0]), double(x[1]));
    }

    return (typename Traits::RangeFieldType)dh_dc->get(double(x[0]), double(x[1]));
  }

  //! source/reaction term

  typename Traits::RangeFieldType    q(const typename Traits::ElementType& e, const typename Traits::DomainType&) const
  {
    return 0.0;
  }

  //! boundary condition type function
  BCType
    bctype(const typename Traits::IntersectionType& is, const typename Traits::IntersectionDomainType& x_) const
  {
    if (dh_bcType == NULL) return boundarytype;

    typename Traits::DomainType x = is.geometry().global(x_);
    return (BCType)dh_bcType->get(double(x[0]), double(x[1]));
  }

  bool
    isDirichlet(const typename Traits::IntersectionType& is, const typename Traits::IntersectionDomainType& x) const
  {
    return bctype(is, x) == BCType::Dirichlet;
  }

  //! Dirichlet boundary condition value
  typename Traits::RangeFieldType
    g(const typename Traits::IntersectionType& is, const typename Traits::IntersectionDomainType& x_) const
  {
    if (!is.boundary() || bctype(is, x_) != BCType::Dirichlet)
      return 0; // Dirichlet is zero

    typename Traits::DomainType x = is.geometry().global(x_);

    if (dh_dirichlet != NULL)
    {
      return (typename Traits::RangeFieldType)dh_dirichlet->get(double(x[0]), double(x[1]));
    }

    if (x[0] < 1e-6)
      return Xmin;
    if (x[0] > width - 1e-6)
      return Xmax;
    if (x[1] < 1e-6)
      return Ymin;
    if (x[1] > height - 1e-6)
      return Ymax;

    return 0.0;
  }

  //! Neumann boundary condition
  typename Traits::RangeFieldType
    j(const typename Traits::IntersectionType& is, const typename Traits::IntersectionDomainType& x_) const
  {
    if (!is.boundary() || bctype(is, x_) != BCType::Neumann)
      return 0.0;

    typename Traits::DomainType x = is.geometry().global(x_);
    if (dh_neumann != NULL)
    {
      return (typename Traits::RangeFieldType)dh_neumann->get(double(x[0]), double(x[1]));
    }

    if (x[0] < 1e-6)
      return Xmin;
    if (x[0] > width - 1e-6)
      return Xmax;
    if (x[1] < 1e-6)
      return Ymin;
    if (x[1] > height - 1e-6)
      return Ymax;

    return 0.0;
  }

  //! set time for subsequent evaluation
  void setTime(RF t)
  {
    time = t;
  }

  void setTimeTarget(RF time_, RF dt_)
  {
    tend = time_;
  }

  //! to be called once before each time step
  void preStep(RF time_, RF dt_, int stages)
  {

  }


private:
  RF time, tend, dt;
  const RF Dt;
  const RF Xmin;
  const RF Xmax;
  const RF Ymin;
  const RF Ymax;
  const RF width;
  const RF height;
  BCType boundarytype;
  const DataHelper* dh_bcType;
  const DataHelper* dh_neumann;
  const DataHelper* dh_dirichlet;
  const DataHelper* dh_dc;
  const DataHelper* dh_compartment;
};


#endif
