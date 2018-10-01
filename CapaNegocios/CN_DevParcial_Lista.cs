using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using CapaDatos;

namespace CapaNegocios
{
    public class CN_DevParcial_Lista
    {
        public void ConsultaDevParcial(int Id_Emp, int Id_Cd, string Conexion, DevParcial devParcial, ref List<DevParcial> List)
        {
            try
            {
                CD_DevParcial_Lista cddevParcial = new CD_DevParcial_Lista();
                cddevParcial.ConsultaDevParcialLista(Id_Emp, Id_Cd, Conexion, devParcial, ref List);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarDevParcial(int Id_Emp, int Id_Cd, int Id_U, DevParcial devParcial, string Conexion, ref int verificador)
        {
            try
            {
                CD_DevParcial_Lista cddevParcial = new CD_DevParcial_Lista();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
