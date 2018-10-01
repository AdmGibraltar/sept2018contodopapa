using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class CD_CapGastoViaje
    {
        public void InsertarGastoViaje(GastoViaje gastoViaje, string Conexion, ref int verificador)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {
                string[] Parametros = { 
	                                    "@Id_Emp",
		                                "@Id_Cd",
                                        "@Id_GV",
                                        "@Id_GVEst",
                                        "@GV_Solicitante",
                                        "@GV_Motivo",
                                        "@GV_FechaSalida",
                                        "@GV_FechaRegreso",
                                        "@GV_Importe",
                                        "@Id_PagElec",
                                        "@GV_FechaElaboracion",
                                        "@GV_TipoTransporte",
                                        "@GV_DiasHospedaje",
                                        "@GV_CantidadDesayunos",
                                        "@GV_CantidadComidas",
                                        "@GV_CantidadCenas",
                                        "@GV_CantidadOtros",
                                        "@GV_ImporteOtros",
                                        "@UsuarioMod",
                                        "@GV_TransporteCuota",
                                        "@GV_TipoGasto"
                                      };

                object[] Valores = { 
                                       gastoViaje.Id_Emp,
                                       gastoViaje.Id_Cd,
                                       gastoViaje.Id_GV,
                                       gastoViaje.Id_GVEst,
                                       gastoViaje.GV_Solicitante,
                                       gastoViaje.GV_Motivo,
                                       gastoViaje.GV_FechaSalida,
                                       gastoViaje.GV_FechaRegreso,
                                       gastoViaje.GV_Importe,
                                       gastoViaje.Id_PagElec,
                                       gastoViaje.GV_FechaElaboracion,
		                               gastoViaje.GV_TipoTransporte,
		                               gastoViaje.GV_DiasHospedaje,
		                               gastoViaje.GV_CantidadDesayunos,
		                               gastoViaje.GV_CantidadComidas,
		                               gastoViaje.GV_CantidadCenas,
		                               gastoViaje.GV_CantidadOtros,
		                               gastoViaje.GV_ImporteOtros,
		                               gastoViaje.UsuarioMod,
                                       gastoViaje.GV_TransporteCuota,
                                       gastoViaje.GV_TipoGasto
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapGastoViaje_Insertar", ref verificador, Parametros, Valores);

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AutorizarGastoViaje(GastoViaje gastoViaje, string Conexion, ref int verificador)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {
                string[] Parametros = { 
	                                    "@Id_Emp",
		                                "@Id_Cd",
                                        "@Id_GV",
                                        "@Id_Usuario"
                                      };

                object[] Valores = { 
                                       gastoViaje.Id_Emp,
                                       gastoViaje.Id_Cd,
                                       gastoViaje.Id_GV,
                                       gastoViaje.UsuarioMod
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapGastoViaje_Autorizar", ref verificador, Parametros, Valores);

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EnviarGastoViaje(GastoViaje gastoViaje, string Conexion, ref int verificador)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {
                string[] Parametros = { 
	                                    "@Id_Emp",
		                                "@Id_Cd",
                                        "@Id_GV",
                                        "@Id_Usuario"
                                      };

                object[] Valores = { 
                                       gastoViaje.Id_Emp,
                                       gastoViaje.Id_Cd,
                                       gastoViaje.Id_GV,
                                       gastoViaje.UsuarioMod
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapGastoViaje_Enviar", ref verificador, Parametros, Valores);

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void RegistrarGastoViaje(GastoViaje gastoViaje, string Conexion, ref int verificador)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {
                string[] Parametros = { 
	                                    "@Id_Emp",
		                                "@Id_Cd",
                                        "@Id_GV"
                                      };

                object[] Valores = { 
                                       gastoViaje.Id_Emp,
                                       gastoViaje.Id_Cd,
                                       gastoViaje.Id_GV
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapGastoViaje_Registrar", ref verificador, Parametros, Valores);

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaGastoViaje(GastoViaje gastoViaje, string Conexion, ref List<GastoViaje> list)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_GV", "@Id_PagElecTipo", "@Id_PagElec" };
                object[] Valores = { gastoViaje.Id_Emp, 
                                       gastoViaje.Id_Cd,
                                       gastoViaje.Id_GV,
                                       gastoViaje.Id_PagElecTipo,
                                       gastoViaje.Id_PagElec};

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapGastoViaje_Lista", ref dr, Parametros, Valores);


                while (dr.Read())
                {
                    gastoViaje = new GastoViaje();
                    gastoViaje.Id_Emp = (int)dr.GetValue(dr.GetOrdinal("Id_Emp"));
                    gastoViaje.Id_Cd = (int)dr.GetValue(dr.GetOrdinal("Id_Cd"));
                    gastoViaje.Id_GV = (int)dr.GetValue(dr.GetOrdinal("Id_GV"));
                    gastoViaje.Id_GVEst = (int)dr.GetValue(dr.GetOrdinal("Id_GVEst"));
                    gastoViaje.GVEst_Descripcion = dr["GVEst_Descripcion"].ToString();
                    gastoViaje.GV_Solicitante = dr["GV_Solicitante"].ToString();
                    gastoViaje.GV_Motivo = dr["GV_Motivo"].ToString();
                    gastoViaje.GV_FechaSalida = DateTime.Parse(dr["GV_FechaSalida"].ToString());
                    gastoViaje.GV_FechaRegreso = DateTime.Parse(dr["GV_FechaRegreso"].ToString());
                    gastoViaje.GV_Importe = (decimal)dr.GetValue(dr.GetOrdinal("GV_Importe"));
                    gastoViaje.Id_PagElec = (int)dr.GetValue(dr.GetOrdinal("Id_PagElec"));
                    gastoViaje.Id_PagElecTipo = (int)dr.GetValue(dr.GetOrdinal("Id_PagElecTipo"));
                    gastoViaje.GV_FechaElaboracion = DateTime.Parse(dr["GV_FechaElaboracion"].ToString());
                    gastoViaje.GV_TipoTransporte = dr["GV_TipoTransporte"] == System.DBNull.Value ? 0 : (int)(dr["GV_TipoTransporte"]);
                    gastoViaje.GV_DiasHospedaje = dr["GV_DiasHospedaje"] == System.DBNull.Value ? 0 : (int)(dr["GV_DiasHospedaje"]);
                    gastoViaje.GV_CantidadDesayunos = dr["GV_CantidadDesayunos"] == System.DBNull.Value ? 0 : (int)dr.GetValue(dr.GetOrdinal("GV_CantidadDesayunos"));
                    gastoViaje.GV_CantidadComidas = dr["GV_CantidadComidas"] == System.DBNull.Value ? 0 : (int)dr.GetValue(dr.GetOrdinal("GV_CantidadComidas"));
                    gastoViaje.GV_CantidadCenas = dr["GV_CantidadCenas"] == System.DBNull.Value ? 0 : (int)dr.GetValue(dr.GetOrdinal("GV_CantidadCenas"));
                    gastoViaje.GV_CantidadOtros = dr["GV_CantidadOtros"] == System.DBNull.Value ? 0 : (int)dr.GetValue(dr.GetOrdinal("GV_CantidadOtros"));
                    gastoViaje.GV_ImporteOtros = dr["GV_ImporteOtros"] == System.DBNull.Value ? 0 : (decimal)dr.GetValue(dr.GetOrdinal("GV_ImporteOtros")); 

                    gastoViaje.FechaUltimaMod = DateTime.Parse(dr["FechaUltimaMod"].ToString());
                    gastoViaje.UsuarioMod = (int)dr.GetValue(dr.GetOrdinal("UsuarioMod"));
                    gastoViaje.GV_TransporteCuota = dr["GV_TransporteCuota"] == System.DBNull.Value ? 0 : (decimal)(dr["GV_TransporteCuota"]);

                    gastoViaje.GV_PagElec_Destino = dr["GV_PagElec_Destino"].ToString();
                    gastoViaje.GV_Saldo_Comprobar = dr["GV_Saldo_Comprobar"] == System.DBNull.Value ? 0 : (decimal)dr["GV_Saldo_Comprobar"];
                    gastoViaje.GV_MotivoRechazo = dr["GV_MotivoRechazo"].ToString();
                    gastoViaje.Acr_NumeroGenerado = dr["Acr_NumeroGenerado"].ToString();
                    gastoViaje.GV_TipoGasto = (int)dr["GV_TipoGasto"];
                    gastoViaje.PagElecTipo_Descripcion = dr["PagElecTipo_Descripcion"].ToString();

                    gastoViaje.GV_Acr_Nombre = dr["GV_Acr_Nombre"].ToString();
                    gastoViaje.GV_PagElec_Observaciones = dr["GV_PagElec_Observaciones"].ToString();
                    gastoViaje.GV_Fecha_Comprobacion = dr["GV_Fecha_Comprobacion"] == System.DBNull.Value ? "" : DateTime.Parse(dr["GV_Fecha_Comprobacion"].ToString()).ToString("dd/MM/yyyy");
                    gastoViaje.PagElec_Cc = dr["PagElec_Cc"].ToString();
                    gastoViaje.PagElec_SubCuenta = dr["PagElec_SubCuenta"].ToString();
                    gastoViaje.PagElec_SubSubCuenta = dr["PagElec_SubSubCuenta"].ToString();
 
                    list.Add(gastoViaje);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaGastoViaje(GastoViaje gastoViaje, string Conexion)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_GV" };
                object[] Valores = {    gastoViaje.Id_Emp, 
                                        gastoViaje.Id_Cd,
                                        gastoViaje.Id_GV
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapGastoViaje_Consulta", ref dr, Parametros, Valores);


                while (dr.Read())
                {
                    gastoViaje.Id_Emp = (int)dr.GetValue(dr.GetOrdinal("Id_Emp"));
                    gastoViaje.Id_Cd = (int)dr.GetValue(dr.GetOrdinal("Id_Cd"));
                    gastoViaje.Id_GV = (int)dr.GetValue(dr.GetOrdinal("Id_GV"));
                    gastoViaje.Id_GVEst = (int)dr.GetValue(dr.GetOrdinal("Id_GVEst"));
                    gastoViaje.GVEst_Descripcion = dr["GVEst_Descripcion"].ToString();
                    gastoViaje.GV_Solicitante = dr["GV_Solicitante"].ToString();
                    gastoViaje.GV_Motivo = dr["GV_Motivo"].ToString();
                    gastoViaje.GV_FechaSalida = DateTime.Parse(dr["GV_FechaSalida"].ToString());
                    gastoViaje.GV_FechaRegreso = DateTime.Parse(dr["GV_FechaRegreso"].ToString());
                    gastoViaje.GV_Importe = (decimal)dr.GetValue(dr.GetOrdinal("GV_Importe"));
                    gastoViaje.Id_PagElec = (int)dr.GetValue(dr.GetOrdinal("Id_PagElec"));
                    gastoViaje.Id_PagElecTipo = (int)dr.GetValue(dr.GetOrdinal("Id_PagElecTipo"));
                    gastoViaje.GV_FechaElaboracion = DateTime.Parse(dr["GV_FechaElaboracion"].ToString());
                    gastoViaje.GV_TipoTransporte = dr["GV_TipoTransporte"] == System.DBNull.Value ? 0 : (int)(dr["GV_TipoTransporte"]);
                    gastoViaje.GV_DiasHospedaje = dr["GV_DiasHospedaje"] == System.DBNull.Value ? 0 : (int)(dr["GV_DiasHospedaje"]);
                    gastoViaje.GV_CantidadDesayunos = dr["GV_CantidadDesayunos"] == System.DBNull.Value ? 0 : (int)dr.GetValue(dr.GetOrdinal("GV_CantidadDesayunos"));
                    gastoViaje.GV_CantidadComidas = dr["GV_CantidadComidas"] == System.DBNull.Value ? 0 : (int)dr.GetValue(dr.GetOrdinal("GV_CantidadComidas"));
                    gastoViaje.GV_CantidadCenas = dr["GV_CantidadCenas"] == System.DBNull.Value ? 0 : (int)dr.GetValue(dr.GetOrdinal("GV_CantidadCenas"));
                    gastoViaje.GV_CantidadOtros = dr["GV_CantidadOtros"] == System.DBNull.Value ? 0 : (int)dr.GetValue(dr.GetOrdinal("GV_CantidadOtros"));
                    gastoViaje.GV_ImporteOtros = dr["GV_ImporteOtros"] == System.DBNull.Value ? 0 : (decimal)dr.GetValue(dr.GetOrdinal("GV_ImporteOtros")); 
 
                    gastoViaje.FechaUltimaMod = DateTime.Parse(dr["FechaUltimaMod"].ToString());
                    gastoViaje.UsuarioMod = (int)dr.GetValue(dr.GetOrdinal("UsuarioMod"));
                    gastoViaje.GV_TransporteCuota = dr["GV_TransporteCuota"] == System.DBNull.Value ? 0 : (decimal)(dr["GV_TransporteCuota"]);

                    gastoViaje.GV_PagElec_Destino = dr["GV_PagElec_Destino"].ToString();
                   // gastoViaje.GV_Saldo_Comprobar = dr["GV_Saldo_Comprobar"] == System.DBNull.Value ? 0 : (decimal)dr["GV_Saldo_Comprobar"];
                    gastoViaje.GV_TipoGasto = (int)dr["GV_TipoGasto"];


                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarGastoViaje(GastoViaje gastoViaje, string Conexion, ref int verificador)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {
                string[] Parametros = { 
	                                    "@Id_Emp",
		                                "@Id_Cd",
                                        "@Id_GV",
                                        "@Id_GVEst",
                                        "@GV_Solicitante",
                                        "@GV_Motivo",
                                        "@GV_FechaSalida",
                                        "@GV_FechaRegreso",
                                        "@GV_Importe",
                                        "@Id_PagElec",
                                        "@GV_TipoTransporte",
                                        "@GV_DiasHospedaje",
                                        "@GV_CantidadDesayunos",
                                        "@GV_CantidadComidas",
                                        "@GV_CantidadCenas",
                                        "@GV_CantidadOtros",
                                        "@GV_ImporteOtros",
                                        "@UsuarioMod",
                                        "@GV_TransporteCuota",
                                        "@GV_TipoGasto"
                                      };

                object[] Valores = { 
                                       gastoViaje.Id_Emp,
                                       gastoViaje.Id_Cd,
                                       gastoViaje.Id_GV,
                                       gastoViaje.Id_GVEst,
                                       gastoViaje.GV_Solicitante,
                                       gastoViaje.GV_Motivo,
                                       gastoViaje.GV_FechaSalida,
                                       gastoViaje.GV_FechaRegreso,
                                       gastoViaje.GV_Importe,
                                       gastoViaje.Id_PagElec,
                                       gastoViaje.GV_TipoTransporte,
		                               gastoViaje.GV_DiasHospedaje,
		                               gastoViaje.GV_CantidadDesayunos,
		                               gastoViaje.GV_CantidadComidas,
		                               gastoViaje.GV_CantidadCenas,
		                               gastoViaje.GV_CantidadOtros,
		                               gastoViaje.GV_ImporteOtros,
		                               gastoViaje.UsuarioMod,
                                       gastoViaje.GV_TransporteCuota,
                                       gastoViaje.GV_TipoGasto
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapGastoViaje_Modificar", ref verificador, Parametros, Valores);

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //JFCV 11 ene 2016 cambiar el estatus
        public void ModificarEstatusGastoViaje(GastoViaje gastoViaje, string Conexion, ref int verificador)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {
                string[] Parametros = { 
	                                    "@Id_Emp",
		                                "@Id_Cd",
                                        "@Id_GV",
                                        "@Id_Estatus",
                                        "@Id_Usuario"
                                      };

                object[] Valores = { 
                                       gastoViaje.Id_Emp,
                                       gastoViaje.Id_Cd,
                                       gastoViaje.Id_GV,
                                       gastoViaje.Id_GVEst,
                                       gastoViaje.UsuarioMod
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapGastoViaje_CambiarEstatus", ref verificador, Parametros, Valores);

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //JFCV 12 ene 2016 cambiar el estatus
        public void RechazarGastoViaje(GastoViaje gastoViaje, string Conexion, ref int verificador)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {
                string[] Parametros = { 
	                                    "@Id_Emp",
		                                "@Id_Cd",
                                        "@Id_GV",
                                        "@MotivoRechazo",
                                        "@Id_UsuarioMod"
                                      };

                object[] Valores = { 
                                       gastoViaje.Id_Emp,
                                       gastoViaje.Id_Cd,
                                       gastoViaje.Id_GV,
                                       gastoViaje.GV_MotivoRechazo,
                                       gastoViaje.UsuarioMod
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapGastoViaje_Rechazar", ref verificador, Parametros, Valores);

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


     

    }
}
