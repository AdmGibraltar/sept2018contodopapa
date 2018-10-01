using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using CapaDatos;
using CapaModelo;

namespace CapaNegocios
{
    public class CN_UsuarioRik
    {
        public List<eUsuarioRik> Lista(int Id_Emp, int Id_Cd, string Conexion)
        {
            CD_UsuarioRik cdURik = new CD_UsuarioRik();
            return cdURik.Lista(Id_Emp, Id_Cd,Conexion);
        }
        //
    }
}
