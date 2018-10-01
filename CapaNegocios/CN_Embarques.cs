using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CapaEntidad;
using CapaDatos;

namespace CapaNegocios
{
    public class CN_Embarques
    {
        public void ConsultarCantidadEmbarquesCentroDist(ref int verificador, int Id_Cd, string Conexion)
        {
            try
            {
                new CD_Embarques().ConsultarCantidadEmbarquesCentroDist(ref verificador, Id_Cd, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Metodo que guarda el embarque en la base de datos, asi tabien guarda en la tabla EmbarquesDet los detalles
        /// de los embarques, y actualiza el estuatus de las facturas en la tabla de CapFactura
        /// </summary>
        /// <param name="embarques">Entidad de los embarques</param>
        /// <param name="listaEmbarquesDet">Lista de la entidad de los detalles del embarque</param>
        /// <param name="sesion">Variable de sesion del sistema</param>
        /// <param name="listaFactura">Lista de la entindad de las facturas</param>
        /// <param name="verificador">Variable para verificar el resultado de las operaciones</param>
        public void GuardaEmbarques(Embarques embarques, List<EmbarquesDet> listaEmbarquesDet, Sesion sesion,
           List<Factura> listaFactura, ref int verificador)
        {
            try
            {
                CD_Embarques CDEmbarqes = new CD_Embarques();

                CDEmbarqes.GuardaEmbarques(embarques, listaEmbarquesDet, sesion, listaFactura, 
                    ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Metodo que trae los valores de la factura, necesarios para llenar el grid de ProFacturaRuta
        /// </summary>
        /// <param name="factura">Entidad de la factura</param>
        /// <param name="listaFactura">Lista donde se vaciaran los datos</param>
        /// <param name="Conexion">Cadena de conexion a la base de datos</param>
        public void LlenaGridProFacturaRuta(ref Factura factura, ref List<Factura> listaFactura, string Conexion)
        {
            try
            {
                CD_Embarques CDEmbarques = new CD_Embarques();

                CDEmbarques.LlenaGridProFacturaRuta(ref factura, ref listaFactura, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Evento que regesaa estatus de "I" una factura que haya sido asignada a una ruta
        /// </summary>
        /// <param name="factura">Entidad de la factura</param>
        /// <param name="conexion">Cadena de conexion a la base de datos</param>
        /// <param name="verificador">Variable que verificara si se realizo la operacion o no</param>
        public void RegresaEstatusFactura(Factura factura, string conexion, ref int verificador)
        {
            try
            {
                CD_Embarques CDEmbarques = new CD_Embarques();

                CDEmbarques.RegresaEstatusFactura(factura,conexion,ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Metodo que da de baja un embarque en la base de datos y regresa a su estatus original las facturas y
        /// lo relacionado a ellas
        /// </summary>
        /// <param name="embarques">Entidad de los embarqes</param>
        /// <param name="conexion">Cadena de conexion a la base de datos</param>
        /// <param name="verificador">Variable para determinar si se ejecuto correctamente o no al operacion</param>
        /// <param name="listaFactura">Lista que trae los datos necesarios para procesar el regreso de estatus
        /// de las facturas y todo lo relacionado a esta operacion</param>
        public void BajaEmbarque(Embarques embarques, string conexion, ref int verificador, List<Factura> listaFactura)
        {
            try
            {
                CD_Embarques CDEmbarques = new CD_Embarques();

                CDEmbarques.BajaEmbarque(embarques, conexion, ref verificador, listaFactura);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
