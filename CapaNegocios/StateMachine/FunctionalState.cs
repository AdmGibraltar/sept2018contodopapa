using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaNegocios.StateMachine
{
    public class FunctionalState
        : IState
    {
        public FunctionalState()
        {
            _transitions = new List<ITransition>();
        }

        public FunctionalState(Action onEnter, Action onExecute, Action onExit)
        {
            _transitions = new List<ITransition>();
            _onEnter = onEnter;
            _onExecute = onExecute;
            _onExit = onExit;
        }

        public Action OnEnter
        {
            get
            {
                return _onEnter;
            }
            set
            {
                _onEnter = value;
            }
        }

        public Action OnExecute
        {
            get
            {
                return _onExecute;
            }
            set
            {
                _onExecute = value;
            }
        }

        public Action OnExit
        {
            get
            {
                return _onExit;
            }
            set
            {
                _onExit = value;
            }
        }

        public void Enter()
        {
            if (_onEnter != null)
            {
                _onEnter();
            }
        }

        public void Execute()
        {
            if (_onExecute != null)
            {
                _onExecute();
            }
        }

        public void Exit()
        {
            if (_onExit != null)
            {
                _onExit();
            }
        }

        public List<ITransition> Transitions
        {
            get
            {
                return _transitions;
            }
        }

        protected Action _onEnter=null;
        protected Action _onExecute=null;
        protected Action _onExit=null;

        protected List<ITransition> _transitions=null;
    }
}
