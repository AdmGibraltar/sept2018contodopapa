using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaDatos;
using CapaModelo;
using CapaEntidad;

namespace CapaNegocios
{
    public class CN_SysTipoUsuario
    {
        public SysTipoUsuario ObtenerPorId(Sesion s, int idTipoUsuario, IBusinessTransaction ibt)
        {
            CD_SysTipoUsuario cdSysTipoUsuario = new CD_SysTipoUsuario();
            var tipoUsuario = cdSysTipoUsuario.ConsultarPorId(s.Id_Emp, idTipoUsuario, ibt.DataContext);
            return tipoUsuario;
        }

        public SysTipoUsuario ObtenerPorId(Sesion s, int idTipoUsuario)
        {
            using (IBusinessTransaction ibt = CN_FabricaTransaccionNegocios.Default(s))
            {
                ibt.Begin();
                return ObtenerPorId(s, idTipoUsuario, ibt);
            }
        }

        public SysTipoUsuario ObtenerPorSesion(Sesion s, IBusinessTransaction ibt)
        {
            return ObtenerPorId(s, s.Id_TU, ibt);
        }

        public SysTipoUsuario ObtenerPorSesion(Sesion s)
        {
            using (IBusinessTransaction ibt = CN_FabricaTransaccionNegocios.Default(s))
            {
                ibt.Begin();
                return ObtenerPorSesion(s, ibt);
            }
        }

        public SysTipoUsuario ObtenerPorDescripcion(Sesion s, string descripcion, IBusinessTransaction ibt)
        {
            CD_SysTipoUsuario cdSysTipoUsuario = new CD_SysTipoUsuario();
            return cdSysTipoUsuario.ConsultarPorDescripcion(s.Id_Emp, descripcion, ibt.DataContext);
        }
    }
}
