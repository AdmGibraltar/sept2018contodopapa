using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaNegocios.StateMachine;
using CapaModelo;

namespace CapaNegocios.FlujosDeEstado.CRM
{
    public interface IProyectoState
        : IState
    {
        /// <summary>
        /// Representa el identificador de la fase en el repositorio
        /// </summary>
        int EstadoId
        {
            get;
        }

        /// <summary>
        /// Proyecto de interés
        /// </summary>
        CrmOportunidade Proyecto
        {
            get;
        }
    }
}
