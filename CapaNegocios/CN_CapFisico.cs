using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaDatos;using CapaEntidad;

namespace CapaNegocios
{
    public class CN_CapFisico
    {
        public void EliminarFisico(Fisico Fisico, string Conexion, ref int verificador)
        {
            try
            {
                CD_CapFisico claseCapaDatos = new CD_CapFisico();
                claseCapaDatos.EliminarFisico(Fisico, Conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertarFisico(Fisico Fisico, string Conexion, ref int verificador)
        {
            try
            {
                CD_CapFisico claseCapaDatos = new CD_CapFisico();
                claseCapaDatos.InsertarFisico(Fisico, Conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaFisicoConsignado(FisicoConsignado FisicoConsignado, int Id_Prd, string Conexion, ref List<FisicoConsignado> List)
        {
            try
            {
                CD_CapFisico claseCapaDatos = new CD_CapFisico();
                claseCapaDatos.ConsultaFisicoConsignado(FisicoConsignado, Id_Prd, Conexion, ref List);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaFisico(Producto fisico, string Conexion, ref List<Producto> List)
        {
            try
            {
                CD_CapFisico claseCapaDatos = new CD_CapFisico();
                claseCapaDatos.ConsultaFisico(fisico,  Conexion, ref List);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Automatico(int Id_Emp, int Id_Cd, string Conexion)
        {
            try
            {
                CD_CapFisico claseCapaDatos = new CD_CapFisico();
                claseCapaDatos.Automatico(Id_Emp, Id_Cd, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
