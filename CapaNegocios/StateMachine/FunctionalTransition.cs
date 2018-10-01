using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaNegocios.StateMachine
{
    public class FunctionalTransition
        : ITransition
    {
        public FunctionalTransition(IState source, IState destination)
        {
            _source = source;
            _destination = destination;
        }

        public FunctionalTransition(Func<bool> evaluator)
        {
            _evaluator = evaluator;
        }

        public Func<bool> Evaluator
        {
            get
            {
                return _evaluator;
            }
            set
            {
                _evaluator = value;
            }
        }

        public bool Evaluate()
        {
            return _evaluator();
        }

        public IState Source
        {
            get
            {
                return _source;
            }
            set
            {
                _source = value;
            }
        }

        public IState Destination
        {
            get
            {
                return _destination;
            }
            set
            {
                _destination = value;
            }
        }

        public static FunctionalTransition operator <=(FunctionalTransition ft, Func<bool> evaluator)
        {
            ft._evaluator = evaluator;
            return ft;
        }

        public static FunctionalTransition operator >=(FunctionalTransition ft, Func<bool> evaluator)
        {
            ft._evaluator = evaluator;
            return ft;
        }

        protected Func<bool> _evaluator;
        protected IState _source = null;
        protected IState _destination = null;
    }
}
