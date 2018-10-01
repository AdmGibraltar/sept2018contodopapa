using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using CapaEntidad;
using CapaDatos;

namespace CapaNegocios
{
    public class CN_ProCompraLocal
    {
        public void InsertarSolicitud(CompraLocal compralocal, DataTable dt, string Conexion, ref int verificador)
        {
            try
            {
                CD_ProCompraLocal claseCapaDatos = new CD_ProCompraLocal();
                claseCapaDatos.InsertarSolicitud(compralocal, dt, Conexion, ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarSolicitud(CompraLocal compralocal, DataTable dt, string Conexion, ref int verificador)
        {
            try
            {
                CD_ProCompraLocal claseCapaDatos = new CD_ProCompraLocal();
                claseCapaDatos.ModificarSolicitud(compralocal, dt, Conexion, ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarSolicitud(CompraLocal compralocal, ref DataTable dt, string Conexion)
        {
            try
            {
                CD_ProCompraLocal claseCapaDatos = new CD_ProCompraLocal();
                claseCapaDatos.ConsultarSolicitud(compralocal, dt, Conexion);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaCompraLocalList(int Id_Emp, int Id_Cd, int Id_Sol, string Conexion, ref List<ProductoLocal> List)
        {
            try
            {
                CD_ProCompraLocal claseCapaDatos = new CD_ProCompraLocal();
                claseCapaDatos.ConsultaCompraLocalList(Id_Emp, Id_Cd, Id_Sol, Conexion, ref List);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarSolicitud(ref CompraLocal cl, string Conexion, ref int verificador)
        {
            try
            {
                CD_ProCompraLocal claseCapaDatos = new CD_ProCompraLocal();
                claseCapaDatos.ConsultarSolicitud(ref cl, Conexion, ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarCompraLocal(CompraLocal cl, List<ProductoLocal> list, string Conexion, ref int verificador)
        {
            try
            {
                CD_ProCompraLocal claseCapaDatos = new CD_ProCompraLocal();
                claseCapaDatos.ModificarCompraLocal(cl, list, Conexion, ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarPrdCompraLocal(Sesion sesion, int prd, ref List<ProductoLocal> List)
        {
            try
            {
                CD_ProCompraLocal claseCapaDatos = new CD_ProCompraLocal();
                claseCapaDatos.ConsultarPrdCompraLocal(sesion, prd, ref List);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
