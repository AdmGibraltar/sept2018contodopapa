using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using CapaDatos;

namespace CapaNegocios
{
    public class CN_CapRutaServicio
    {
        public void ConsultaCapRutaServicio(int Id_Emp, int Id_Cd, string Conexion, ref List<RutaServicio> List)
        {
            try
            {
                CD_CapRutaServicio claseCapaDatos = new CD_CapRutaServicio();
                claseCapaDatos.ConsultaCapRutaServicio(Id_Emp, Id_Cd, Conexion, ref List);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertarCapRutaServicio(int Id_Emp, int Id_Cd, int Id_U, RutaServicio ruta, string Conexion, ref int verificador)
        {
            try
            {
                CD_CapRutaServicio claseCapRutaServicio = new CD_CapRutaServicio();
                claseCapRutaServicio.InsertarCapRutaServicio(Id_Emp, Id_Cd, Id_U, ruta, Conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarCapRutaServicio(int Id_Emp, int Id_Cd, int Id_U, RutaServicio ruta, string Conexion, ref int verificador)
        {
            try
            {
                CD_CapRutaServicio claseCapRutaServicio = new CD_CapRutaServicio();
                claseCapRutaServicio.ModificarCapRutaServicio(Id_Emp, Id_Cd, Id_U, ruta, Conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EliminarCapRutaServicio(int Id_Emp, int Id_Cd, RutaServicio ruta, string Conexion, ref int verificador)
        {
            try
            {
                CD_CapRutaServicio claseCapRutaServicio = new CD_CapRutaServicio();
                claseCapRutaServicio.EliminarCapRutaServicio(Id_Emp, Id_Cd, ruta, Conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
