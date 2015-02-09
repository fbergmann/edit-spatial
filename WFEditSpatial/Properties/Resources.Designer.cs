﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34014
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace EditSpatial.Properties {
  /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [DebuggerNonUserCode()]
    [CompilerGenerated()]
    internal class Resources {
        
        private static ResourceManager resourceMan;
        
        private static CultureInfo resourceCulture;
        
        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        internal static ResourceManager ResourceManager {
            get {
                if (ReferenceEquals(resourceMan, null)) {
                    ResourceManager temp = new ResourceManager("EditSpatial.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        internal static CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 
        ///set (HEADERS 
        ///
        ///local_operator.hh componentparameters.hh
        ///
        ///)
        ///set (SOURCES 
        ///main.cc
        ///)
        ///
        ///add_executable(main ${SOURCES})
        ///target_link_dune_default_libraries(&quot;main&quot;)
        ///
        ///# copy config file if we don&apos;t have it
        ///if (NOT EXISTS &quot;${CMAKE_CURRENT_BINARY_DIR}/main.conf&quot;)
        ///configure_file(${CMAKE_CURRENT_SOURCE_DIR}/main.conf ${CMAKE_CURRENT_BINARY_DIR}/main.conf COPYONLY)
        ///endif()
        ///
        ///# create vtk dir, as otherwise the executable just falls over
        ///if (NOT EXISTS &quot;${CMAKE_CURRENT_BINARY_DIR}/vtk&quot;)
        ///file(MAKE_DIRE [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string CMakeLists {
            get {
                return ResourceManager.GetString("CMakeLists", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to // -*- tab-width: 4; indent-tabs-mode: nil; c-basic-offset: 2 -*-
        ///// vi: set ts=4 sw=2 et sts=2:
        ///#ifndef DUNE_DYCAP_MULTITRANSPORTPARAMETERS_HH
        ///#define DUNE_DYCAP_MULTITRANSPORTPARAMETERS_HH
        ///
        ///#include&quot;ccfv_local_operator.hh&quot;
        ///
        /////! Transport in water phase
        ///template&lt;typename GV, typename RF&gt;
        ///class DiffusionParameter :
        ///        public Dune::PDELab::DiffusionMulticomponentInterface&lt;Dune::PDELab::DiffusionParameterTraits&lt;GV,RF&gt;,
        ///        DiffusionParameter&lt;GV,RF&gt; &gt;
        ///{
        ///    enum {dim=GV::Grid::dimension}; [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string componentparameters {
            get {
                return ResourceManager.GetString("componentparameters", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to writeVTK = yes
        ///VTKname =  fish
        ///overlap = 1 #overlap for parallel computation
        ///integrationorder = 2 #for Q1 it should be enough
        ///subsampling = 2 #subsampling level
        ///
        ///timesolver = Alexander2
        ///
        ///# defaults are verbosity  
        ///[Verbosity] 
        ///verbosity = 0 
        ///
        ///[Newton]
        ///LinearVerbosity = 0
        ///ReassembleThreshold = 0.
        ///LineSearchMaxIterations = 5
        ///MaxIterations = 30
        ///AbsoluteLimit = 1.e-8
        ///Reduction = 1.e-8
        ///LinearReduction = 1e-3
        ///LineSearchDampingFactor = 0.5
        ///Verbosity = 0
        ///
        ///
        ///[Timeloop]
        ///time = 500 #time for si [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string config {
            get {
                return ResourceManager.GetString("config", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Drawing.Icon similar to (Icon).
        /// </summary>
        internal static Icon ICON_EditSpatial {
            get {
                object obj = ResourceManager.GetObject("ICON_EditSpatial", resourceCulture);
                return ((Icon)(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Drawing.Bitmap.
        /// </summary>
        internal static Bitmap IMAGE_NoGeomery {
            get {
                object obj = ResourceManager.GetObject("IMAGE_NoGeomery", resourceCulture);
                return ((Bitmap)(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to #ifndef INITIAL_CONDITIONS_H
        ///#define INITIAL_CONDITIONS_H
        ///
        ///
        ////** \brief A function for initial values of u_0
        /// */
        ///template&lt;typename GV, typename RF&gt;
        ///class U0Initial
        ///        : public Dune::PDELab::GridFunctionBase&lt;Dune::PDELab::
        ///        GridFunctionTraits&lt;GV,RF,1,Dune::FieldVector&lt;RF,1&gt; &gt;, U0Initial&lt;GV,RF&gt; &gt;
        ///{
        ///    const GV&amp; gv;
        ///
        ///public:
        ///    typedef Dune::PDELab::GridFunctionTraits&lt;GV,RF,1,Dune::FieldVector&lt;RF,1&gt; &gt; Traits;
        ///
        ///    //! construct from grid view
        ///    U0Initial (const GV&amp; gv_)
        ///        [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string initial_conditions {
            get {
                return ResourceManager.GetString("initial_conditions", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to // -*- tab-width: 4; indent-tabs-mode: nil; c-basic-offset: 2 -*-
        ///// vi: set ts=4 sw=2 et sts=2:
        ///#ifndef DUNE_PDELAB_MULTICOMPONENTTRANSPORTOP_HH
        ///#define DUNE_PDELAB_MULTICOMPONENTTRANSPORTOP_HH
        ///
        ///#include&lt;dune/common/exceptions.hh&gt;
        ///#include&lt;dune/common/fvector.hh&gt;
        ///#include&lt;dune/common/static_assert.hh&gt;
        ///
        ///#include &lt;dune/geometry/referenceelements.hh&gt;
        ///
        ///#include&lt;dune/pdelab/common/geometrywrapper.hh&gt;
        ///#include&lt;dune/pdelab/common/function.hh&gt;
        ///#include&lt;dune/pdelab/localoperator/defaultimp.hh&gt;
        ///#includ [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string local_operator {
            get {
                return ResourceManager.GetString("local_operator", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to // -*- tab-width: 4; indent-tabs-mode: nil -*-
        ////** \file
        ///
        ///    \brief Solve two-component diffusion-reaction system
        ///    with finite volume scheme. This class tests the
        ///    multi domain grid.
        ///*/
        ///#ifdef HAVE_CONFIG_H
        ///#include &quot;config.h&quot;
        ///#endif
        ///#include &lt;math.h&gt;
        ///#include &lt;iostream&gt;
        ///#include &lt;vector&gt;
        ///#include &lt;map&gt;
        ///#include &lt;string&gt;
        ///#include &lt;stdlib.h&gt;
        ///
        ///#include &lt;dune/common/parallel/mpihelper.hh&gt;
        ///#include &lt;dune/common/exceptions.hh&gt;
        ///#include &lt;dune/common/fvector.hh&gt;
        ///#include &lt;dune/common/sta [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string main {
            get {
                return ResourceManager.GetString("main", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to #ifndef REACTIONADAPTER_H
        ///#define REACTIONADAPTER_H
        ///
        /////! Adapter for system of equations
        ////*!
        ///  \tparam M model type
        ///  This class is used for adapting system of ODE&apos;s (right side)
        ///  to system of PDE&apos;s (as source term)
        ///  What need to be implemented is only the evaluate function
        ///*/
        ///template&lt;typename RF, int N&gt;
        ///class ReactionAdapter
        ///{
        ///public:
        ///
        ///    //! constructor stores reference to the model
        ///    ReactionAdapter(const Dune::ParameterTree &amp; param_)
        ///        : param(param_),
        ///          v1(param.su [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string reactionadapter {
            get {
                return ResourceManager.GetString("reactionadapter", resourceCulture);
            }
        }
    }
}
