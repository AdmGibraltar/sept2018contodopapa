using System;
using CapaModelo;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaNegocios.StateMachine;

namespace CapaNegocios.FlujosDeEstado.CRM
{
    /// <summary>
    /// Representa una transición de naturaleza funcional para una máquina de estados de un proyecto
    /// </summary>
    public class ProyectoTransition
        : FunctionalTransition, IProyectoTransition
    {
        public ProyectoTransition(IState source, IState destination)
            : base(source, destination)
        {
        }

        public ProyectoTransition(Func<bool> evaluator)
            : base(evaluator)
        {
        }

        public CrmOportunidade Proyecto
        {
            get
            {
                return _proyecto;
            }
            set
            {
                _proyecto = value;
            }
        }

        protected CrmOportunidade _proyecto = null;
    }
}
