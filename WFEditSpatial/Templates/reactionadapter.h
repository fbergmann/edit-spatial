#ifndef REACTIONADAPTER_H
#define REACTIONADAPTER_H

#include <dune/copasi/utilities/sbmlhelper.hh>

//! Adapter for system of equations
/*!
  \tparam M model type
  This class is used for adapting system of ODE's (right side)
  to system of PDE's (as source term)
  What need to be implemented is only the evaluate function
*/
template<typename RF>
class ReactionAdapter
{
public:

    //! constructor stores reference to the model
    ReactionAdapter(const Dune::ParameterTree & param_)
        : param(param_)
%INITIALIZER%
    {

    }

    void preStep(RF time,RF dt,int stages)
    {

    }

    //! evaluate model in entity eg with local values x
    template<typename EG, typename LFSU, typename X, typename LFSV, typename R, typename DomainType>
    void evaluate(const EG& eg, const LFSU& lfsu, const X& x, const LFSV& lfsv, R& r, const DomainType& x_)
    {
      const auto& pos = eg.geometry().global(x_);

%ODES%
    }

private:
    const Dune::ParameterTree & param;
%DECLARATION%
};

#endif // REACTIONADAPTER_H
