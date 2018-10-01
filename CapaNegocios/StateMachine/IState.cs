using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaNegocios.StateMachine
{
    public interface IState
    {
        void Enter();
        void Execute();
        void Exit();
        List<ITransition> Transitions
        {
            get;
        }
    }
}
