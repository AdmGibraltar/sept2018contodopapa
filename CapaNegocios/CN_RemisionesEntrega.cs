using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using CapaDatos;

namespace CapaNegocios
{
    public class CN_RemisionesEntrega
    {
        public void ConsultaEmbarquesRemision(int Id_Emp, int Id_Cd, string Conexion, RemisionesEntrega remisionfiltro, ref List<RemisionesEntrega> List)
        {
            try
            {
                CD_RemisionesEntrega Remision = new CD_RemisionesEntrega();
                Remision.ConsultaProRemisionEntrega(Id_Emp, Id_Cd, Conexion, remisionfiltro, ref List);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarEmbarquesRemision(int Id_Emp, int Id_Cd, int Id_U, RemisionesEntrega remisionEntrega, string Conexion, ref int verificador)
        {
            try
            {
                CD_RemisionesEntrega Remision = new CD_RemisionesEntrega();
                Remision.ModificarProRemisionEntrega(Id_Emp, Id_Cd, Id_U, remisionEntrega, Conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AutorizarRemision(ref Remision Rem, string Conexion, string Estatus)
        {
            try
            {
                CD_RemisionesEntrega Remisiones = new CD_RemisionesEntrega();
                Remisiones.AutorizarRemision(ref Rem, Conexion, Estatus);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
