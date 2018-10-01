using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using CapaDatos;
using System.Data;

namespace CapaNegocios
{
    public class CN_ProSeguimientoPrd
    {
        /// <summary>
        /// Metodo que busca los datos necesarios en la base de datos referentes al 
        /// seguimiento de productos en entrega a sucursal
        /// </summary>
        /// <param name="producto">Entidad del producto</param>
        /// <param name="conexion">Cadena de concexion a la base de datos</param>
        /// <param name="lista">Lista que contendra los resultados</param>
        /// <param name="Id_Emp">Id de la empresa</param>
        /// <param name="Id_Prd">Id del producto a buscar</param>
        public void BuscaProSeguimientoPrd(ref Producto producto, string conexion, ref List<Producto> lista, ref int validador)
        {
            try
            {
                CD_ProSeguimientoPrd CDProSeguimientoPrd = new CD_ProSeguimientoPrd();
                CDProSeguimientoPrd.BuscaProSeguimientoPrd(ref producto, conexion, ref lista, ref validador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Metodo utilizado para llenar el grid del formulario de observaciones de seguimieto
        /// </summary>
        /// <param name="segPrd">Entidad del seguimiento de productos</param>
        /// <param name="listaPrd">Lista que contendra los resultados de la operacion</param>
        /// <param name="conexion">Cadena de conexion a la base de datos</param>
        public void LlenaGridSeguimiento(SeguimientoProductos segPrd, ref List<SeguimientoProductos> listaPrd, string conexion)
        {
            try
            {
                CD_ProSeguimientoPrd CDProSeguimientoPrd = new CD_ProSeguimientoPrd();

                CDProSeguimientoPrd.LlenaGridSeguimiento(segPrd, ref listaPrd, conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Metodo que inserta observaciones en la tabla de CapSegProd
        /// </summary>
        /// <param name="SegPrd">Entidad de los seguimeintos a productos</param>
        /// <param name="conexion">Cadena de conexion a la base de datos</param>
        /// <param name="verificador">Variable que confirma la operacon si regresa con valor mayor a 0</param>
        public void GuardaObservaciones(SeguimientoProductos SegPrd, string conexion, ref int verificador)
        {
            try
            {
                CD_ProSeguimientoPrd CDProSeguimientoPrd = new CD_ProSeguimientoPrd();
                CDProSeguimientoPrd.GuardaObservaciones(SegPrd, conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// Metodo que modifica las observaciones en la tabla de CapSegProd
        /// </summary>
        /// <param name="SegPrd">Entidad de los seguimeintos a productos</param>
        /// <param name="conexion">Cadena de conexion a la base de datos</param>
        /// <param name="verificador">Variable que confirma la operacon si regresa con valor mayor a 0</param>
        public void ModificaObservaciones(SeguimientoProductos SegPrd, string conexion, ref int verificador)
        {
            try
            {
                CD_ProSeguimientoPrd CDProSeguimientoPrd = new CD_ProSeguimientoPrd();
                CDProSeguimientoPrd.ModificaObservaciones(SegPrd, conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        /// <summary>
        /// Metodo que borra registros de la tabla CapSegProd
        /// </summary>
        /// <param name="SegPrd">Entidad de los seguimeintos a productos</param>
        /// <param name="conexion">Cadena de conexion a la base de datos</param>
        /// <param name="verificador">Variable que confirma la operacon si regresa con valor mayor a 0</param>
        public void EliminaObservaciones(SeguimientoProductos SegPrd, string conexion, ref int verificador)
        {
            try
            {
                CD_ProSeguimientoPrd CDProSeguimientoPrd = new CD_ProSeguimientoPrd();
                CDProSeguimientoPrd.EliminaObservaciones(SegPrd, conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
