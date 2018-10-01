using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.SqlClient;
using System.Data;

using CapaEntidad;

namespace CapaDatos
{
    public class CD_CapTransferencia
    {
        public void InsertarTransferencia(Transferencia transferencia, List<TransferenciaDet> list_TransferenciaDet,  string Conexion, ref int verificador)
        {
            CapaDatos.CD_Datos CapaDatos = default(CD_Datos);
            SqlCommand sqlcmd = default(SqlCommand);
            try
            {
                CapaDatos = new CapaDatos.CD_Datos(Conexion);
                CapaDatos.StartTrans();

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_Trans", "@Id_U", "@Trans_Fecha", "@Trans_Nota", "@Id_CdOrigen", "@Id_CdOrigenStr", "@Id_RemOrigen", "@Trans_Subtotal", "@Trans_Iva", "@Trans_Total", "@Trans_Estatus", "Trans_FechaHr",  "@Id_UOrigen", "@U_NombreOrigen" };
                object[] Valores = { transferencia.Id_Emp, transferencia.Id_Cd, transferencia.Id_Trans, transferencia.Id_U, transferencia.Trans_Fecha, transferencia.TransNota, transferencia.Id_CdOrigen, transferencia.Id_CdOrigenStr, transferencia.Id_RemOrigen, 
                                       transferencia.Trans_SubTotal, transferencia.Trans_Iva, transferencia.Trans_Total, transferencia.Trans_Estatus, transferencia.Trans_FechaHr, transferencia.Id_UOrigen, transferencia.U_NombreOrigen };

                sqlcmd = CapaDatos.GenerarSqlCommand("spTransferenciaExterna_Insertar", ref verificador, Parametros, Valores);

                if (verificador > 0)
                {                   

                   
                        Parametros = new string[] { 
                            "@Id_Emp", "@Id_Cd", "@Id_Trans", "@Id_TransDet", "@Id_Prd", "@Trans_Cant"
                        };
                        for (int x = 0; x < list_TransferenciaDet.Count; x++)
                        {
                            Valores = new object[] { 
                                transferencia.Id_Emp, transferencia.Id_Cd, list_TransferenciaDet[x].Id_Trans,  x, list_TransferenciaDet[x].Id_Prd, 
                                list_TransferenciaDet[x].Id_Trans
                            };
                            sqlcmd = CapaDatos.GenerarSqlCommand("spTransferenciaExternaDet_Insertar", ref verificador, Parametros, Valores);
                        }
                    
                    CapaDatos.CommitTrans();
                    verificador = transferencia.Id_Trans;
                }
                else
                {
                    CapaDatos.RollBackTrans();
                }

            }
            catch (Exception ex)
            {
                CapaDatos.RollBackTrans();
                throw ex;
            }
            finally
            {
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
        }

        /*
        public void Modificartransferencia(Transferencia transferencia, List<Banco_Ficha> list_fichas, List<TransferenciaDet> list_transferencias, string Conexion, ref int verificador)
        {
            CapaDatos.CD_Datos CapaDatos = default(CD_Datos);
            try
            {
                CapaDatos = new CapaDatos.CD_Datos(Conexion);
                CapaDatos.StartTrans();

                string[] Parametros = { 
                                          "@Id_Emp", 
                                          "@Id_Cd",
                                          "@Id_Trans",
                                          "@Tipo",
                                          "@Pag_Fecha",
                                          "@Id_Tmov",
                                          "@Pag_Importe",
                                          "@Pag_Total",
                                          "@Id_U",
                                          "@Pag_Estatus",
                                          "@Id_CdOrigen",
                                          "@Id_transferenciarigen"
                                      };
                object[] Valores = { 
                                       transferencia.Id_Emp, 
                                       transferencia.Id_Cd, 
                                       transferencia.Id_Trans,
                                       transferencia.Tipo, 
                                       transferencia.Pag_Fecha, 
                                       transferencia.Id_Tmov, 
                                       transferencia.Pag_Importe,
                                       transferencia.Pag_Total,
                                       transferencia.Id_U, 
                                       transferencia.Pag_Estatus, 
                                       transferencia.Id_CdOrigen,
                                       transferencia.Id_transferenciarigen
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCaptransferenciaExterno_Modificar", ref verificador, Parametros, Valores);

                if (verificador > 0)
                {
                    transferencia.Id_Trans = verificador;

                    Parametros = new string[] { 
                            "@Id_Emp", 
                            "@Id_Cd",
                            "@Id_Trans",
                            "@Num",
                            "@Id_Ban",
                            "@Fecha",
                            "@Importe",
                    };
                    for (int x = 0; x < list_fichas.Count; x++)
                    {
                        Valores = new object[] {
                            transferencia.Id_Emp,
                            transferencia.Id_Cd,
                            transferencia.Id_Trans,
                            x+1, 
                            list_fichas[x].Id_Ban, 
                            list_fichas[x].Pag_Fecha, 
                            list_fichas[x].Pag_Importe 
                        };
                        sqlcmd = CapaDatos.GenerarSqlCommand("spCaptransferenciaFicha_Insertar", ref verificador, Parametros, Valores);
                    }


                    if (verificador == 1)
                    {
                        Parametros = new string[] { 
                            "@Id_Emp", 
                            "@Id_Cd",
                            "@Serie",
                            "@Id_Trans",
                            "@Id_TransDet",
                            "@Mov",
                            "@Ref",
                            "@Ficha",
                            "@Cheque",
                            "@Importe",
                            "@Fecha",
                            "@Pag_Id_Cd",
                            "@Pag_Id_Ter",
                            "@Pag_Doc_Fecha",
                            "@Pag_Id_Cte",
                            "@Pag_Cte_Nombre"
                    };
                        for (int x = 0; x < list_transferencias.Count; x++)
                        {
                            Valores = new object[] {
                            transferencia.Id_Emp,
                            transferencia.Id_Cd,
                            list_transferencias[x].Serie.ToString().ToUpper(), 
                            transferencia.Id_Trans,
                            x,
                            list_transferencias[x].Mov, 
                            list_transferencias[x].Ref, 
                            list_transferencias[x].Ficha, 
                            list_transferencias[x].Cheque,
                            list_transferencias[x].Importe,
                            transferencia.Pag_Fecha,
                            list_transferencias[x].Pag_Id_cd,
                            list_transferencias[x].Pag_Id_Ter,
                            list_transferencias[x].Pag_Fac_Fecha,
                            list_transferencias[x].Pag_Id_cte,
                            list_transferencias[x].Pag_Cte_Nombre,
                        };
                            sqlcmd = CapaDatos.GenerarSqlCommand("spCaptransferenciaDet_Insertar", ref verificador, Parametros, Valores);
                        }

                    }
                }
                CapaDatos.CommitTrans();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                CapaDatos.RollBackTrans();
                throw ex;
            }
        }*/

        public void ConsultarCentro(int Id_Emp, int Id_Cd,  ref DbCentro centro, string ConexionCob)
        {
            try
            {
                SqlCommand sqlcmd = default(SqlCommand);
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(ConexionCob);
                SqlDataReader dr = null;

                string[] Parametros = { "@Id_Emp", "@Id_Cd" };
                object[] Valores = { Id_Emp, Id_Cd };

                sqlcmd = CapaDatos.GenerarSqlCommand("spCatCentros_Consultar", ref dr, Parametros, Valores);

                if (dr.HasRows)
                {
                    dr.Read();
                    centro.Id_Emp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Emp")));
                    centro.Id_Cd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cd")));
                    centro.Db_Nombre = dr.GetValue(dr.GetOrdinal("Db_Nombre")).ToString();
                    centro.Db_CdNombre = dr.GetValue(dr.GetOrdinal("Db_CdNombre")).ToString();
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarCentro(int Id_Emp, int Id_Cd, int Id_Trans, ref List<int> List, string Conexion)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                SqlCommand sqlcmd = default(SqlCommand);
                SqlDataReader dr = null;

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_Trans" };
                object[] Valores = { Id_Emp, Id_Cd, Id_Trans };

                sqlcmd = CapaDatos.GenerarSqlCommand("spCaptransferenciaDet_Consultar", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    int cen = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Trans_Id_Cd")));
                    if (!List.Contains(cen) && cen != Id_Cd)
                    {
                        List.Add(cen);
                    }
                }


                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Baja(Transferencia transferencia, int Id_Cd, string Conexion, ref int verificador)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Id_CdOrigen", "@Id_transferenciarigen", "@Id_Cd" };
                object[] Valores = { transferencia.Id_Emp, transferencia.Id_CdOrigen, transferencia.Id_Trans, Id_Cd };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCaptransferenciaExterno_Baja", ref verificador, Parametros, Valores);

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void EnviarCorreo_Insertar(Transferencia transferencia, string Conexion, ref int verificador)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = new string[] { "@Id_Emp", "@Id_Cd", "@Id_Trans" };
                object[] Valores = new object[] { transferencia.Id_Emp, transferencia.Id_Cd, transferencia.Id_Trans };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spTransferenciaEnvioCorreoExterno_Insertar", ref verificador, Parametros, Valores);

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void EnviarCorreo_Baja(Transferencia transferencia, string Conexion, ref int verificador)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = new string[] { "@Id_Emp", "@Id_Cd", "@Id_Trans" };
                object[] Valores = new object[] { transferencia.Id_Emp, transferencia.Id_Cd, transferencia.Id_Trans };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spEnvioCorreoExterno_Baja", ref verificador, Parametros, Valores);

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
