using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaNegocios.StateMachine;
using CapaModelo;

namespace CapaNegocios.FlujosDeEstado.CRM
{
    /// <summary>
    /// Representa una fase de un proyecto
    /// </summary>
    public class ProyectoState
        : FunctionalState, IProyectoState
    {
        public ProyectoState()
            : base()
        {

        }

        public ProyectoState(Action onEnter, Action onExecute, Action onExit)
            : base(onEnter, onExecute, onExit)
        {

        }

        /// <summary>
        /// Representa el identificador de la fase en el repositorio
        /// </summary>
        public int EstadoId
        {
            get;
            set;
        }

        /// <summary>
        /// Proyecto de interés
        /// </summary>
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
