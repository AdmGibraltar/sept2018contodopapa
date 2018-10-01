using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;

namespace CapaNegocios
{
    public class CN_Login
    {
        public CN_Login()
        { }
        public void Login(ref Usuario Usuario, out int Id, out int Minutos, out bool Dependientes, string conexion)
        {
            try
            {
                CapaDatos.CD_Login CD_Login = new CapaDatos.CD_Login();
                CD_Login.Login(ref Usuario, out Id, out Minutos, out Dependientes, conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void RecuperarContraseña(ref Usuario Usuario, ref CentroDistribucion Cdis, ref ConfiguracionGlobal Configuracion, out int Id, string conexion)
        {
            try
            {
                CapaDatos.CD_Login CD_Login = new CapaDatos.CD_Login();
                CD_Login.RecuperarContraseña(ref Usuario, ref Cdis, ref Configuracion, out Id, conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
