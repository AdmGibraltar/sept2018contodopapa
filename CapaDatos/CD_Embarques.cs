using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.SqlClient;
using System.Data;

using CapaEntidad;

namespace CapaDatos
{
    public class CD_Embarques
    {
        public void ConsultarCantidadEmbarquesCentroDist(ref int verificador, int Id_Cd, string Conexion)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);


                string[] Parametros = { "@Id_Cd" };
                object[] Valores = { Id_Cd };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand(
                    "spEmbarquesCantidadEnCd_Consultar", ref verificador, Parametros, Valores);

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Evento que regesa a estatus de "I" una factura que haya sido asignada a una ruta
        /// </summary>
        /// <param name="factura">Entidad de la factura</param>
        /// <param name="conexion">Cadena de conexion a la base de datos</param>
        /// <param name="verificador">Variable que verificara si se realizo la operacion o no</param>
        public void RegresaEstatusFactura(Factura factura, string conexion, ref int verificador)
        {
            try
            {
                CapaDatos.CD_Datos CDDatos = new CapaDatos.CD_Datos(conexion);

                string[] parametros = { 
                                        "@Id_Emp", 
                                        "@Id_Cd",
	                                    "@Id_Fac",
                                        "@Id_Emb"
                                      };
                object[] valores = { 
                                       factura.Id_Emp,
                                       factura.Id_Cd,
                                       factura.Id_Fac,
                                       factura.Id_Emb
                                   };

                SqlCommand sqlcmd = new SqlCommand();

                sqlcmd = CDDatos.GenerarSqlCommand("spProFacturaRuta_RegresaEstatusFactura", ref verificador, parametros, valores);
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
                CapaDatos.CD_Datos CDDatos = new CapaDatos.CD_Datos(sesion.Emp_Cnx);
                int Id_Emb = 0;

                CDDatos.StartTrans();

                SqlCommand sqlcmd = new SqlCommand();

                //if (actualizarEbarque)
                //{
                //    Id_Emb = embarques.Id_Emb;
                //}
                //else
                //{
                if (embarques.Id_Emb < 0)
                {
                    string[] parametros = { 
                                              "@Id_Emp",
                                              "@Id_Cd",
                                              "@Emb_Fec",
                                              "@Emb_Chofer",
                                              "@Emb_Dia",
                                              "@Emb_Camioneta",
                                              "@Emb_Estatus",
                                              "@Id_U"
                                          };

                    object[] valores = { 
                                           embarques.Id_Emp,
                                           embarques.Id_Cd,
                                           embarques.Emb_Fec,
                                           embarques.Emb_Chofer,
                                           embarques.Emb_Dia,
                                           embarques.Emb_Camioneta,
                                           embarques.Emb_Estatus,
                                           embarques.Id_U
                                       };

                    sqlcmd = CDDatos.GenerarSqlCommand("spProFacturaRuta_InsertaEmbarques", ref verificador, parametros, valores);
                }
                else
                {

                    string[] parametros = { 
                                              "@Id_Emp",
                                              "@Id_Cd",
                                              "@Emb_Fec",
                                              "@Emb_Chofer",
                                              "@Emb_Dia",
                                              "@Emb_Camioneta",
                                              "@Emb_Estatus",
                                              "@Id_U",
                                              "@Id_Emb"
                                          };

                    object[] valores = { 
                                           embarques.Id_Emp,
                                           embarques.Id_Cd,
                                           embarques.Emb_Fec,
                                           embarques.Emb_Chofer,
                                           embarques.Emb_Dia,
                                           embarques.Emb_Camioneta,
                                           embarques.Emb_Estatus,
                                           embarques.Id_U,
                                           embarques.Id_Emb
                                       };

                    sqlcmd = CDDatos.GenerarSqlCommand("spProFacturaRuta_ActualizaEmbarques", ref verificador, parametros, valores);
                }

                Id_Emb = verificador;

                string[] parametrosEmbarquesDet = { 
                                                      "@Id_Emp",
                                                      "@Id_Cd",
                                                      "@Id_Emb",
                                                      "@Id_Fac",
                                                      "@Contador"
                                                  };
                if (Id_Emb != 0 && Id_Emb != -1)
                {
                    int contador = 0;
                    foreach (EmbarquesDet embarquesDet in listaEmbarquesDet)
                    {
                        object[] valoresEmbarquesDet = { 
                                                           embarquesDet.Id_Emp,
                                                           embarquesDet.Id_Cd,
                                                           embarquesDet.Id_Emb,
                                                           embarquesDet.Id_Fac
                                                           ,contador
                                                       };

                        sqlcmd = CDDatos.GenerarSqlCommand("spProFacturaRuta_InsertaEmbarquesDet", ref verificador,
                            parametrosEmbarquesDet, valoresEmbarquesDet);
                        contador += 1;
                    }
                }
                else
                {
                    throw new Exception("Problema al insertar en CapEntSal. Regresa ID Invalido");
                }


                string[] parametrosFactura = { 
                                                "@Id_Emp",
                                                "@Id_Cd",
                                                "@Id_Fac",
                                                "@Ped_FecEmb",
                                                "@Ped_UsrEmb"
                                             };

                if (Id_Emb != 0 && Id_Emb != -1)
                {
                    foreach (Factura factura in listaFactura)
                    {

                        object[] valoresFactura = { 
                                                      factura.Id_Emp,
                                                      factura.Id_Cd,
                                                      factura.Id_Fac,
                                                      DateTime.Now,
                                                      sesion.Id_U
                                                  };

                        sqlcmd = CDDatos.GenerarSqlCommand("spProFacturaRuta_CambiaEstatusFactura", ref verificador, parametrosFactura, valoresFactura);
                    }
                }
                else
                {
                    throw new Exception("Problema al insertar en CapEntSal. Regresa ID Invalido");
                }

                CDDatos.CommitTrans();
                CDDatos.LimpiarSqlcommand(ref sqlcmd);

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
                CD_Datos CDDatos = new CD_Datos(conexion);
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

                SqlCommand sqlcmd = CDDatos.GenerarSqlCommand("spProFacturaRuta_Baja", ref verificador, parametros, valores);
                CDDatos.LimpiarSqlcommand(ref sqlcmd);

                if (verificador > 0)
                {
                    foreach (Factura factura in listaFactura)
                    {
                        this.RegresaEstatusFactura(factura, conexion, ref verificador);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void QuitaRelacionesEmbarque(EmbarquesDet embarquesDet, string conexion)
        {
            try
            {
                CapaDatos.CD_Datos CDDatos = new CapaDatos.CD_Datos(conexion);

                string[] parametros = { 
                                          "@Id_Emp",
                                          "@Id_Cd",
                                          "@Id_Emb",
                                          "@Id_Fac"
                                      };
                object[] valores = { 
                                       embarquesDet.Id_Emp,
                                       embarquesDet.Id_Cd,
                                       embarquesDet.Id_Emp,
                                       embarquesDet.Id_Fac
                                   };
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
                SqlDataReader sdr = null;
                CapaDatos.CD_Datos CDDatos = new CapaDatos.CD_Datos(Conexion);

                string[] parametrosGrid = {
                                          "@Id_Emp",
                                          "@Id_Cd",
                                          "@Id_Emb"
                                      };
                object[] valoresGrid = { 
                                       factura.Id_Emp,
                                       factura.Id_Cd,
                                       factura.Id_Emb
                                   };

                SqlCommand sqlcmd = CDDatos.GenerarSqlCommand("spProFacturaRuta_Grid", ref sdr, parametrosGrid, valoresGrid);

                while (sdr.Read())
                {
                    factura = new Factura();

                    factura.Id_Fac = Convert.ToInt32(sdr.GetValue(sdr.GetOrdinal("Id_Fac")));
                    factura.Id_FacSerie = sdr.GetValue(sdr.GetOrdinal("Id_FacSerie")).ToString();
                    factura.Cte_NomComercial = sdr.GetValue(sdr.GetOrdinal("Cte_NomComercial")).ToString();
                    factura.Fac_Importe = float.Parse(sdr.GetValue(sdr.GetOrdinal("Fac_Importe")).ToString());
                    factura.Id_Emp = Convert.ToInt32(sdr.GetValue(sdr.GetOrdinal("Id_Emp")));
                    factura.Id_Cd = Convert.ToInt32(sdr.GetValue(sdr.GetOrdinal("Id_Cd")));
                    factura.Id_Emb = Convert.ToInt32(sdr.GetValue(sdr.GetOrdinal("Id_Emb")));

                    listaFactura.Add(factura);
                }

                CDDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
