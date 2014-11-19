
/** \brief A function for initial values of the model
 */
template<typename GV, typename RF>
class UGeneralInitial
        : public Dune::PDELab::GridFunctionBase<Dune::PDELab::
        GridFunctionTraits<GV, RF, 1, Dune::FieldVector<RF, 1> >, UGeneralInitial<GV, RF> >
{
    const GV& gv;

public:
    typedef Dune::PDELab::GridFunctionTraits<GV,RF,1,Dune::FieldVector<RF,1> > Traits;

    UGeneralInitial(const GV& gv_)
      : gv(gv_)
      , param()
      , mComponentIndex(0)
    {}

    //! construct from grid view
    UGeneralInitial(const GV& gv_, const Dune::ParameterTree & param_, int componentIndex)
        : gv(gv_)
        , param(param_)
        , mComponentIndex(componentIndex)
%INITIALIZER%
    {}

    //! evaluate extended function on element
    inline void evaluate (const typename Traits::ElementType& e,
                          const typename Traits::DomainType& xlocal,
                          typename Traits::RangeType& __initial) const
    {
      const auto& x = e.geometry().global(xlocal);
%ADDITIONAL_INITIAlIZATION%

      switch(mComponentIndex)
      {
%INITIALCONDITION%
        default:
        {
          // this cannot happen
          Dune::PDELab::Exception ex;
          ex.message("Unexpected Index.");
          throw ex;
        }
      }
    }

    //! get a reference to the grid view
    inline const GV& getGridView () {return gv;}

private:

    const Dune::ParameterTree & param;
    int mComponentIndex;
%DECLARATION%
};
