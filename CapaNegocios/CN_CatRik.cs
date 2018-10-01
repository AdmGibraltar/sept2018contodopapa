using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaModelo;
using CapaDatos;
using CapaEntidad;

namespace CapaNegocios
{
    public class CN_CatRik
    {
        public string ObtenerCorreo(Sesion s, int idRik)
        {
            CD_CatUsuario cdCatUsuario=new CD_CatUsuario();
            var usuario = cdCatUsuario.ConsultarPorRik(s.Id_Emp, s.Id_Cd, idRik, s.Emp_Cnx_EF);

            return usuario.U_Correo;
        }
    }
}
