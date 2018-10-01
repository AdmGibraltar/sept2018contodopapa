using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using CapaEntidad;

namespace CapaDatos
{
    public class CD_ProFactura_Embarque
    {

        /// <summary>
        /// Busca las facturas con estatus I
        /// </summary>
        /// <param name="factura">Entidad de las facturas</param>
        /// <param name="conexion">Cadena de conexion a la base de datos</param>
        /// <param name="lista">Lista donde se vaciaran los datos obtenidos</param>
        /// <param name="Id_Emp">Id de la empresa</param>
        /// <param name="Id_Cd">Id de la ciudad</param>
        /// <param name="nombreCliente">Nombre del cliente</param>
        /// <param name="Id_Cte">Id del cliente</param>
        /// <param name="Fac_Fecha_Inicio">Fecha de inicio del periodo</param>
        /// <param name="Fac_Fecha_Fin">Fecha de fin del periodo</param>
        public void BuscaFacturaEmbarque(Factura factura, string conexion, ref List<Factura> lista,
            int Id_Emp, int Id_Cd, string nombreCliente, int Id_Cte, DateTime Fac_Fecha_Inicio,
            DateTime Fac_Fecha_Fin)
        {
            try
            {
                SqlDataReader sdr = null;

                CapaDatos.CD_Datos CDDatos = new CD_Datos(conexion);

                string[] parametros = {
                                          "@Id_Emp",
		                                  "@Id_Cd",
		                                  "@nombreCliente",
		                                  "@Id_Cte",
		                                  "@Fac_Fecha_inicio",
		                                  "@Fac_Fecha_fin"
                                      };
                object[] valores = {
                                       Id_Emp,
                                       Id_Cd,
                                       nombreCliente == string.Empty? (object)null:nombreCliente,
                                       Id_Cte == -1? (object)null : Id_Cte,
                                       Fac_Fecha_Inicio == DateTime.MinValue? (object)null : Fac_Fecha_Inicio,
                                       Fac_Fecha_Fin == DateTime.MinValue? (object)null : Fac_Fecha_Fin
                                   };
                SqlCommand sqlcmd = CDDatos.GenerarSqlCommand("spCapFactura_BusquedaEmbarque", ref sdr, parametros, valores);

                while (sdr.Read())
                {
                    factura = new Factura();

                    factura.Id_Emp = Convert.ToInt32(sdr.GetValue(sdr.GetOrdinal("Id_Emp")));
                    factura.Id_Cd = Convert.ToInt32(sdr.GetValue(sdr.GetOrdinal("Id_Cd")));
                    factura.Cte_NomComercial = sdr.GetValue(sdr.GetOrdinal("Cte_NomComercial")).ToString();
                    factura.Fac_Estatus = sdr.GetValue(sdr.GetOrdinal("Fac_Estatus")).ToString();
                    factura.Fac_EstatusStr = sdr.GetValue(sdr.GetOrdinal("Fac_EstatusStr")).ToString();
                    factura.Id_Fac = Convert.ToInt32(sdr.GetValue(sdr.GetOrdinal("Id_Fac")));
                    factura.Fac_Fecha = Convert.ToDateTime(sdr.GetValue(sdr.GetOrdinal("Fac_Fecha")));
                    if (sdr.GetValue(sdr.GetOrdinal("Fac_PedNum")).ToString() != ""
                        || sdr.GetValue(sdr.GetOrdinal("Fac_PedNum")).ToString() != string.Empty)
                    {
                        factura.Fac_PedNum = Convert.ToInt32(sdr.GetValue(sdr.GetOrdinal("Fac_PedNum")));
                    }
                    else
                    {
                        factura.Fac_PedNum = null;
                    }
                    factura.Id_Cte = Convert.ToInt32(sdr.GetValue(sdr.GetOrdinal("Id_Cte")));

                    lista.Add(factura);
                }

                CDDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Metodo que actualiza el estatus de las facturas
        /// </summary>
        /// <param name="factura">Entidad de la factura</param>
        /// <param name="dt">DataTable donde se vaciaran los resultados obtenidos</param>
        /// <param name="conexion">Cadena de conexion a la base de datos</param>
        /// <param name="verificador">Indica si se pudo o no realizar la operacion</param>
        public void CambiaEstatusFacturaEmbarque(Factura factura, DataTable dt, string conexion, ref int verificador)
        {
            try
            {
                CD_Datos CDDatos = new CD_Datos(conexion);

                CDDatos.StartTrans();

                string[] parametros = {
                                          "@Id_Emp",
		                                  "@Id_Cd",
		                                  "@Id_Fac",
		                                  "@Fac_PedNum",
		                                  "@Ped_FecEmb",
		                                  "@Ped_UsrEmb"
                                      };
                object[] valores = {
                                       factura.Id_Emp,
                                       factura.Id_Cd,
                                       factura.Id_Fac,
                                       factura.Fac_PedNum,
                                       factura.Fac_Fecha,
                                       factura.Id_U
                                   };

                SqlCommand slqcmd = CDDatos.GenerarSqlCommand("spCapFacturaEmbarque_CambiaEstatus"
                    , ref verificador, parametros, valores);
                
                factura.Id_Fac = verificador;

                if (verificador > -1)
                {
                    CDDatos.CommitTrans();
                }
                else
                {
                    CDDatos.RollBackTrans();
                }

                CDDatos.LimpiarSqlcommand(ref slqcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
