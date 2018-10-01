using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.SqlClient;
using System.Data;

using CapaEntidad;

namespace CapaDatos
{
    public class CD_CapPago
    {
        public void InsertarPago(Pago pago, List<Banco_Ficha> list_fichas, List<PagoDet> list_pagos, string Conexion, ref int verificador)
        {
            CapaDatos.CD_Datos CapaDatos = default(CD_Datos);
            SqlCommand sqlcmd = default(SqlCommand);
            try
            {
                CapaDatos = new CapaDatos.CD_Datos(Conexion);
                CapaDatos.StartTrans();

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Tipo", "@Pag_Fecha", "@Id_Tmov", "@Pag_Importe", "@Pag_Total", "@Id_U", "@Pag_Estatus", "@Pag_Extemporaneo", "@Id_CdOrigen", "@Id_CdOrigenStr", "@Id_PagOrigen", "@Id_UOrigen", "@U_NombreOrigen" };
                object[] Valores = { pago.Id_Emp, pago.Id_Cd, pago.Tipo, pago.Pag_Fecha, pago.Id_Tmov, pago.Pag_Importe, pago.Pag_Total, pago.Id_U, pago.Pag_Estatus, pago.Pag_Extemporaneo, pago.Id_CdOrigen, pago.Id_CdOrigenStr, pago.Id_PagOrigen, pago.Id_UOrigen, pago.U_Nombre };

                sqlcmd = CapaDatos.GenerarSqlCommand("spCapPagoExterno_Insertar", ref verificador, Parametros, Valores);

                if (verificador > 0)
                {
                    pago.Id_Pag = verificador;
                    Parametros = new string[] { "@Id_Emp", "@Id_Cd", "@Id_Pag", "@Num", "@Id_Ban", "@Fecha", "@Importe", };
                    for (int x = 0; x < list_fichas.Count; x++)
                    {
                        Valores = new object[] { pago.Id_Emp, pago.Id_Cd, pago.Id_Pag, list_fichas[x].Pag_Ficha, list_fichas[x].Id_Ban, list_fichas[x].Pag_Fecha, list_fichas[x].Pag_Importe };
                        sqlcmd = CapaDatos.GenerarSqlCommand("spCapPagoFicha_Insertar", ref verificador, Parametros, Valores);
                    }


                    if (verificador == 1)
                    {
                        Parametros = new string[] { 
                            "@Id_Emp", "@Id_Cd", "@Serie", "@Id_Pag", "@Id_PagDet", "@Mov", "@Ref", "@Ficha", "@Cheque", 
                            "@Importe", "@Fecha", "@Pag_Id_Cd", "@Pag_Id_Ter", "@Pag_Doc_Fecha", "@Pag_Id_Cte", "@Pag_Cte_Nombre" 
                        };
                        for (int x = 0; x < list_pagos.Count; x++)
                        {
                            Valores = new object[] { 
                                pago.Id_Emp, pago.Id_Cd, list_pagos[x].Serie.ToString().ToUpper(), pago.Id_Pag, x, list_pagos[x].Mov, 
                                list_pagos[x].Ref, list_pagos[x].Ficha, list_pagos[x].Cheque, list_pagos[x].Importe, pago.Pag_Fecha, list_pagos[x].Pag_Id_cd, 
                                list_pagos[x].Pag_Id_Ter, list_pagos[x].Pag_Fac_Fecha, list_pagos[x].Pag_Id_cte, list_pagos[x].Pag_Cte_Nombre, 
                            };
                            sqlcmd = CapaDatos.GenerarSqlCommand("spCapPagoDet_Insertar", ref verificador, Parametros, Valores);
                        }
                    }
                    CapaDatos.CommitTrans();
                    verificador = pago.Id_Pag;
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

        public void ModificarPago(Pago pago, List<Banco_Ficha> list_fichas, List<PagoDet> list_pagos, string Conexion, ref int verificador)
        {
            CapaDatos.CD_Datos CapaDatos = default(CD_Datos);
            try
            {
                CapaDatos = new CapaDatos.CD_Datos(Conexion);
                CapaDatos.StartTrans();

                string[] Parametros = { 
                                          "@Id_Emp", 
                                          "@Id_Cd",
                                          "@Id_Pag",
                                          "@Tipo",
                                          "@Pag_Fecha",
                                          "@Id_Tmov",
                                          "@Pag_Importe",
                                          "@Pag_Total",
                                          "@Id_U",
                                          "@Pag_Estatus",
                                          "@Id_CdOrigen",
                                          "@Id_PagOrigen"
                                      };
                object[] Valores = { 
                                       pago.Id_Emp, 
                                       pago.Id_Cd, 
                                       pago.Id_Pag,
                                       pago.Tipo, 
                                       pago.Pag_Fecha, 
                                       pago.Id_Tmov, 
                                       pago.Pag_Importe,
                                       pago.Pag_Total,
                                       pago.Id_U, 
                                       pago.Pag_Estatus, 
                                       pago.Id_CdOrigen,
                                       pago.Id_PagOrigen
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapPagoExterno_Modificar", ref verificador, Parametros, Valores);

                if (verificador > 0)
                {
                    pago.Id_Pag = verificador;

                    Parametros = new string[] { 
                            "@Id_Emp", 
                            "@Id_Cd",
                            "@Id_Pag",
                            "@Num",
                            "@Id_Ban",
                            "@Fecha",
                            "@Importe",
                    };
                    for (int x = 0; x < list_fichas.Count; x++)
                    {
                        Valores = new object[] {
                            pago.Id_Emp,
                            pago.Id_Cd,
                            pago.Id_Pag,
                            x+1, 
                            list_fichas[x].Id_Ban, 
                            list_fichas[x].Pag_Fecha, 
                            list_fichas[x].Pag_Importe 
                        };
                        sqlcmd = CapaDatos.GenerarSqlCommand("spCapPagoFicha_Insertar", ref verificador, Parametros, Valores);
                    }


                    if (verificador == 1)
                    {
                        Parametros = new string[] { 
                            "@Id_Emp", 
                            "@Id_Cd",
                            "@Serie",
                            "@Id_Pag",
                            "@Id_PagDet",
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
                        for (int x = 0; x < list_pagos.Count; x++)
                        {
                            Valores = new object[] {
                            pago.Id_Emp,
                            pago.Id_Cd,
                            list_pagos[x].Serie.ToString().ToUpper(), 
                            pago.Id_Pag,
                            x,
                            list_pagos[x].Mov, 
                            list_pagos[x].Ref, 
                            list_pagos[x].Ficha, 
                            list_pagos[x].Cheque,
                            list_pagos[x].Importe,
                            pago.Pag_Fecha,
                            list_pagos[x].Pag_Id_cd,
                            list_pagos[x].Pag_Id_Ter,
                            list_pagos[x].Pag_Fac_Fecha,
                            list_pagos[x].Pag_Id_cte,
                            list_pagos[x].Pag_Cte_Nombre,
                        };
                            sqlcmd = CapaDatos.GenerarSqlCommand("spCapPagoDet_Insertar", ref verificador, Parametros, Valores);
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
        }

        public void ConsultarCentro(int Id_Emp, int Id_Cd, int cd_tipo, ref DbCentro centro, string ConexionCob)
        {
            try
            {
                SqlCommand sqlcmd = default(SqlCommand);
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(ConexionCob);
                SqlDataReader dr = null;

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Db_Serie", "@Cd_Tipo" };
                object[] Valores = { Id_Emp, Id_Cd, null,  cd_tipo };

                sqlcmd = CapaDatos.GenerarSqlCommand("spCatDb_Consultar", ref dr, Parametros, Valores);

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

        public void ConsultarCentro(int Id_Emp, int Id_Cd, int Id_Pag, ref List<int> List, string Conexion)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                SqlCommand sqlcmd = default(SqlCommand);
                SqlDataReader dr = null;

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_Pag" };
                object[] Valores = { Id_Emp, Id_Cd, Id_Pag };

                sqlcmd = CapaDatos.GenerarSqlCommand("spCapPagoDet_Consultar", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    int cen = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Pag_Id_Cd")));
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

        public void Baja(Pago pago, int Id_Cd, string Conexion, ref int verificador)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Id_CdOrigen", "@Id_PagOrigen", "@Id_Cd" };
                object[] Valores = { pago.Id_Emp, pago.Id_CdOrigen, pago.Id_Pag, Id_Cd };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapPagoExterno_Baja", ref verificador, Parametros, Valores);

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void EnviarCorreo_Insertar(Pago pago, string Conexion, ref int verificador)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = new string[] { "@Id_Emp", "@Id_Cd", "@Id_Pag" };
                object[] Valores = new object[] { pago.Id_Emp, pago.Id_Cd, pago.Id_Pag };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spEnvioCorreoExterno_Insertar", ref verificador, Parametros, Valores);

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void EnviarCorreo_Baja(Pago pago, string Conexion, ref int verificador)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = new string[] { "@Id_Emp", "@Id_Cd", "@Id_Pag" };
                object[] Valores = new object[] { pago.Id_Emp, pago.Id_Cd, pago.Id_Pag };
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
