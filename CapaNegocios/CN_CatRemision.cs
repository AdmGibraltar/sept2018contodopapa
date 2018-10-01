using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CapaEntidad;
using CapaDatos;

namespace CapaNegocios
{
    public class CN_CatRemision
    {
        public void ConsultarCantidadRemisionesCentroDist(ref int verificador, int Id_Cd, string Conexion)
        {
            try
            {
                new CD_CapRemision().ConsultarCantidadRemisionesCentroDist(ref verificador, Id_Cd, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Consulta un numero de referencia(Id_Rem o Id_Fac) y regresa la cantidad de resultados encontrados que 
        /// no sea estatus cancelado (can) ni capturado (cap). 1 remision, 2 factura
        /// </summary>
        /// <param name="sesion"></param>
        /// <param name="referencia"></param>
        /// <param name="verificador"></param>
        public void ConsultarReferencia(Sesion sesion, int referencia, int Id_Tm, ref string verificador, int cliente)
        {//RM
            try
            {
                new CD_CapRemision().ConsultarReferencia(sesion, referencia, Id_Tm, ref verificador, cliente);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //public void ConsultarReferenciaDet(Sesion sesion, int Id_Rem, int Id_Prd, ref int verificador)
        //{
        //    //RM
        //    try
        //    {
        //        new CD_CapRemision().ConsultarReferenciaDet(sesion, Id_Rem, Id_Prd, ref verificador);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        /// <summary>
        /// Recibe el id de un documento, el tipo de documento (1 Remision, 2 Factura) y obtiene una lista de los productos de ese documento, ordenados por agrupador
        /// </summary>
        /// <param name="Id_Rem"></param>     
        /// <param name="tipo_Documento">1 Remision, 2 Factura</param> 
        /// <param name="sesion"></param>
        /// <param name="datatable"></param>
        public void ConsultarTotalProductoDocumento(int Id_Documento, int tipo_Documento, Sesion sesion, ref System.Data.DataTable datatable)
        {
            //RM
            try
            {
                new CD_CapRemision().ConsultarTotalProductoDocumento(Id_Documento, tipo_Documento, sesion, ref datatable);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
