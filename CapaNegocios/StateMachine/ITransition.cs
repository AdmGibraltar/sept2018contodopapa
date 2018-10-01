using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaNegocios.StateMachine
{
    public interface ITransition
    {
        bool Evaluate();

        IState Source
        {
            get;
        }

        IState Destination
        {
            get;
        }
    }
}
