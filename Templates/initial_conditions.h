
/** \brief A function for initial values of u_%COUNT%
 */
template<typename GV, typename RF>
class U%COUNT%Initial
        : public Dune::PDELab::GridFunctionBase<Dune::PDELab::
        GridFunctionTraits<GV,RF,1,Dune::FieldVector<RF,1> >, U%COUNT%Initial<GV,RF> >
{
    const GV& gv;

public:
    typedef Dune::PDELab::GridFunctionTraits<GV,RF,1,Dune::FieldVector<RF,1> > Traits;

    //! construct from grid view
    U%COUNT%Initial (const GV& gv_)
        : gv(gv_)
    {}

    //! evaluate extended function on element
    inline void evaluate (const typename Traits::ElementType& e,
                          const typename Traits::DomainType& xlocal,
                          typename Traits::RangeType& __initial) const
    {
    
%INITIALCONDITION%
        
    }

    //! get a reference to the grid view
    inline const GV& getGridView () {return gv;}
};
