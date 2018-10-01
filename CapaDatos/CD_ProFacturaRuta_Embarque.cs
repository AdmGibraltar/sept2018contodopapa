using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using CapaEntidad;

namespace CapaDatos
{
    public class CD_ProFacturaRuta_Embarque
    {
        /// <summary>
        /// Metodo que busca los datos de los embarques en la base de datos
        /// </summary>
        /// <param name="embarques">Entidad de los embarques</param>
        /// <param name="conexion">Cadena de conexion a la base de datos</param>
        /// <param name="lista">Lista que se llena con el resultado de la operacion</param>
        /// <param name="Id_Emp">Id de la empresa</param>
        /// <param name="Id_Cd">Id de la ciudad</param>
        /// <param name="Id_Emb">Id del embarque</param>
        /// <param name="Fac_Fecha_Ini">Fecha de inicio para el periodo de busqueda</param>
        /// <param name="Fac_Fecha_Fin">Fecha de fin para el periodo de busqueda</param>
        public void BuscarProFacturaRutaEmbarque(Embarques embarques, string conexion, ref List<Embarques> lista,
            int Id_Emp, int Id_Cd, int Id_Emb, DateTime Fac_Fecha_Ini, DateTime Fac_Fecha_Fin)
        {
            try
            {
                SqlDataReader sdr = null;

                CapaDatos.CD_Datos CDDatos = new CapaDatos.CD_Datos(conexion);

                string[] parametros = {
                                          "@Id_Emp",
                                          "@Id_Cd",
                                          "@Id_Emb",
                                          "@Fac_Fecha_Ini",
                                          "@Fac_Fecha_Fin"
                                      };

                object[] valores = { 
                                       Id_Emp,
                                       Id_Cd,
                                       Id_Emb == -1? (object)null : Id_Emb,
                                       Fac_Fecha_Ini == DateTime.MinValue? (object)null : Fac_Fecha_Ini,
                                       Fac_Fecha_Fin == DateTime.MinValue? (object)null : Fac_Fecha_Fin
                                   };

                SqlCommand sqlcmd = CDDatos.GenerarSqlCommand("spProFactura_EmbarqueBusqueda", ref sdr, parametros, valores);

                while (sdr.Read())
                {

                    embarques = new Embarques();

                    embarques.Id_Emp = Convert.ToInt32(sdr.GetValue(sdr.GetOrdinal("Id_Emp")));
                    embarques.Id_Cd = Convert.ToInt32(sdr.GetValue(sdr.GetOrdinal("Id_Cd")));
                    embarques.Id_U = Convert.ToInt32(sdr.GetValue(sdr.GetOrdinal("Id_U")));
                    embarques.Emb_Estatus = sdr.GetValue(sdr.GetOrdinal("Emb_Estatus")).ToString();
                    embarques.Emb_EstatusStr = sdr.GetValue(sdr.GetOrdinal("Emb_EstatusStr")).ToString();
                    embarques.U_Nombre = sdr.GetValue(sdr.GetOrdinal("U_Nombre")).ToString();
                    embarques.Id_Emb = Convert.ToInt32(sdr.GetValue(sdr.GetOrdinal("Id_Emb")));
                    embarques.Emb_Fec = Convert.ToDateTime(sdr.GetValue(sdr.GetOrdinal("Emb_Fec")));
                    embarques.Emb_Dia = Convert.ToDateTime(sdr.GetValue(sdr.GetOrdinal("Emb_Dia")));
                    embarques.Emb_Chofer = sdr.GetValue(sdr.GetOrdinal("Emb_Chofer")).ToString();
                    embarques.Emb_Camioneta = sdr.GetValue(sdr.GetOrdinal("Emb_Camioneta")).ToString();

                    lista.Add(embarques);
                }

                CDDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Metodo que trae datos especificos de algun embarque
        /// </summary>
        /// <param name="embarques">Entidad de los embarques</param>
        /// <param name="conexion">Cadena de conexion a la base de datos</param>
        public void ConsultaProFacturaRutaEmbarque(ref Embarques embarques, string conexion)
        {
            try
            {
                CD_Datos CDDatos = new CD_Datos(conexion);
                SqlDataReader sdr = null;

                string[] parametros = { 
                                          "@Id_Emp",
                                          "@Id_Cd",
                                          "@Id_Emb"
                                      };
                object[] valores = { 
                                       embarques.Id_Emp,
                                       embarques.Id_Cd,
                                       embarques.Id_Emb
                                   };

                SqlCommand sqlcmd = default(SqlCommand);

                sqlcmd = CDDatos.GenerarSqlCommand("spProFactura_EmbarqueConsulta", ref sdr, parametros, valores);

                while (sdr.Read())
                {
                    embarques = new Embarques();
                    embarques.Emb_Fec = Convert.ToDateTime(sdr.GetValue(sdr.GetOrdinal("Emb_Fec")));
                    embarques.Emb_Chofer = sdr.GetValue(sdr.GetOrdinal("Emb_Chofer")).ToString();
                    embarques.Emb_Dia = Convert.ToDateTime(sdr.GetValue(sdr.GetOrdinal("Emb_Dia")));
                    embarques.Emb_Camioneta = sdr.GetValue(sdr.GetOrdinal("Emb_Camioneta")).ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
