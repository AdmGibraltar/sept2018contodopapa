using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaNegocios.StateMachine
{
    public abstract class BaseStateMachine<S, T>
        : IStateMachine where S: IState where T : ITransition
    {
        public BaseStateMachine()
        {
            _states = new List<IState>();
            _transitions = new List<ITransition>();
        }

        public S InitialState
        {
            get
            {
                return (S)_initialState;
            }
            set
            {
                if (!_started)
                {
                    _initialState = value;
                    _currentState = _initialState;
                }
            }
        }

        public S CreateState()
        {
            S state = _CreateState();
            _states.Add(state);
            return state;
        }

        public T LinkStates(S source, S destination)
        {
            //revisar la lista de transiciones para validar que no existe una transición que tenga a [source] como fuente y a [destination] como destino
            foreach (var t in _transitions)
            {
                if ( t.Source.Equals(source) && t.Destination.Equals(destination))
                {
                    throw new StateMachineTransitionAlreadyExistsException("A transition already exists for the source and destination.");
                }
            }
            T transition = CreateTransition(source, destination);
            _transitions.Add(transition);
            return transition;
        }

        public void Update()
        {
            _started = true;
            _currentState.Execute();
            foreach (var transition in _currentState.Transitions)
            {
                if (transition.Evaluate())
                {
                    _currentState.Exit();
                    _currentState = transition.Destination;
                    _currentState.Enter();
                    break;
                }
            }
        }

        protected abstract T CreateTransition(S source, S destination);
        protected abstract S _CreateState();

        protected IState _initialState = null;
        protected IState _currentState = null;
        protected List<IState> _states=null;
        protected List<ITransition> _transitions = null;

        private bool _started = false;

        public class StateMachineTransitionAlreadyExistsException
            : Exception
        {
            public StateMachineTransitionAlreadyExistsException(string message)
                : base(message)
            {
            }
        }
    }
}
