using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaNegocios.StateMachine;
using CapaModelo;

namespace CapaNegocios.FlujosDeEstado.CRM
{
    public interface IProyectoTransition
        : ITransition
    {
        CrmOportunidade Proyecto
        {
            get;
        }
    }
}
