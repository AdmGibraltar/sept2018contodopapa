using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class CD_CatMovimientos
    {

       
        public void InsertarMovimientos(Movimientos mv, string Conexion, ref int verificador)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = { 
                                          "@Id_Emp",
                                          "@Id_Tm",
                                          "@Tm_Nombre",
                                          "@Tm_Tipo",
                                          "@Tm_Naturaleza",
                                          "@Id_CtmInv",
                                          "@Tm_AfcVta",                                          	                                    
                                          "@Tm_AfcIva",                                                                                	                                    
                                          "@Tm_AfcOrdCom",
                                          "@Tm_Afecta",
                                          "@Tm_NatMov",
                                          "@Tm_Activo",
                                          "@Tm_ReqReferencia",
                                          "@Tm_ReqSisPropietario"
                                      };
                object[] Valores = { 
                                       mv.Id_Emp,
                                       mv.Id,
                                       mv.Nombre,
                                       mv.Tipo,
                                       mv.Naturaleza, 
                                       mv.Inverso == -1 ? (object)null : mv.Inverso,
                                       mv.AfeVta,
                                       mv.AfeIVA,
                                       mv.AfeOrdComp,
                                       mv.Afecta,
                                       mv.NatMov,
                                       mv.Estatus,
                                       mv.ReqReferencia,
                                       mv.ReqSispropietario
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatMovimiento_Insertar", ref verificador, Parametros, Valores);
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ModificarMovimientos(Movimientos mv, string Conexion, ref int verificador)
        {
            try
            {
                 
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = { 
                                          "@Id_Emp",
                                          "@Id_Tm",
                                          "@Tm_Nombre",
                                          "@Tm_Tipo",
                                          "@Tm_Naturaleza",
                                          "@Id_CtmInv",
                                          "@Tm_AfcVta",                                          	                                    
                                          "@Tm_AfcIva",                                                                                	                                    
                                          "@Tm_AfcOrdCom",
                                          "@Tm_Afecta",
                                          "@Tm_NatMov",
                                          "@Tm_Activo",
                                          "@Tm_ReqReferencia",
                                          "@Tm_ReqSisPropietario"
                                      };
                object[] Valores = { 
                                       mv.Id_Emp,
                                       mv.Id,
                                       mv.Nombre,
                                       mv.Tipo,
                                       mv.Naturaleza, 
                                       mv.Inverso == -1 ? (object)null : mv.Inverso,
                                       mv.AfeVta,
                                       mv.AfeIVA,
                                       mv.AfeOrdComp,
                                       mv.Afecta,
                                       mv.NatMov,
                                       mv.Estatus,
                                       mv.ReqReferencia,
                                       mv.ReqSispropietario
                                   };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatMovimiento_Modificar", ref verificador, Parametros, Valores);
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarTmovimientoAfecta(Sesion sesion, int Id_Tm, ref int Tm_Afecta)
        {
            //ricardo // Metodo que consulta si se afecta a un cliente(0) o a un proveedor(1)
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(sesion.Emp_Cnx);

                string[] Parametros = {
                                          "@Id1",
                                          "@Id2"                                      
                                      };
                object[] Valores = { 
                                       sesion.Id_Emp,
                                       Id_Tm
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spVerificaTm_Afecta", ref Tm_Afecta, Parametros, Valores);

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        //public void ConsultarTmovimientoReqReferencia(Sesion sesion, int Id_Tm, ref int Tm_Afecta)
        //{
        //    //ricardo
        //    try
        //    {
        //        CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(sesion.Emp_Cnx);

        //        string[] Parametros = {
        //                                  "@Id1"
        //                                  ,"@Id2"
        //                                  //,"@Id3"
        //                              };
        //        object[] Valores = { 
        //                               sesion.Id_Emp
        //                               ,Id_Tm
        //                               //,Tm_NatMov
        //                           };

        //        SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatTmovimiento_ConsultaReqRef", ref Tm_Afecta, Parametros, Valores);

        //        CapaDatos.LimpiarSqlcommand(ref sqlcmd);
        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }
        //}
        //--------------
        /// <summary>
        /// Consulta si requiere referencia y que tipo de documento es el de la referencia. (1) Remision , (2) Factura
        /// </summary>
        /// <param name="sesion"></param>
        /// <param name="Id_Tm"></param>
        /// <param name="Tm_NatMov"></param>
        /// <param name="Tm_ReqReferencia"></param>
        /// <param name="Tm_ReqTDoc"></param>
        public void ConsultarTmovimientoReqReferencia(Sesion sesion, int Id_Tm, int Tm_NatMov, ref bool Tm_ReqReferencia, ref int Tm_ReqTDoc)
        {//ric
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(sesion.Emp_Cnx);


                string[] parametros = { 
                                          "@Id1"
                                          ,"@Id2"
                                          ,"@Id3"
                                      };

                object[] Valores = {
                                       sesion.Id_Emp
                                       ,Id_Tm
                                       ,Tm_NatMov
                                       
                                   };


                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatTmovimiento_ConsultaReqRef", ref dr, parametros, Valores);

                while (dr.Read())
                {
                    Tm_ReqReferencia = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Tm_ReqReferencia"))) ? false : dr.GetBoolean(dr.GetOrdinal("Tm_ReqReferencia"));
                    Tm_ReqTDoc = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Tm_ReqTDoc"))) ? -1 : dr.GetInt32(dr.GetOrdinal("Tm_ReqTDoc"));
                    break;
                }
                dr.Close();

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
                
        public void ConsultarTmovimientoAfectaOrdCom(Sesion sesion, int Id_Tm, ref bool Tm_Afecta)
        {
            //rm //Metodo que verifica si el tipo de movimiento afecta orden de compra
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(sesion.Emp_Cnx);

                string[] Parametros = {
                                          "@Id_Emp",
                                          "@Id_Tm"                                      
                                      };
                object[] Valores = { 
                                       sesion.Id_Emp,
                                       Id_Tm
                                   };

                SqlDataReader dr=null;
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spcatTMovimiento_ConsultaAfcOrdCom", ref dr, Parametros, Valores);
                                
                while (dr.Read())
                {
                    if (dr.GetValue(dr.GetOrdinal("Tm_AfcOrdCom")) != System.DBNull.Value)
                    {
                        Tm_Afecta = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Tm_AfcOrdCom")));
                    }
                }               

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        
        public void ConsultaMovimientos(bool inventario, int Empresa, string Conexion, ref List<Movimientos> List)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);


                string[] Parametros = { "@Id_Emp", "@Tm_NatMov" };
                object[] Valores = { Empresa, inventario ? 0 : 1 };


                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatMovimiento_Consulta", ref dr, Parametros, Valores);

                Movimientos mv = default(Movimientos);
                while (dr.Read())
                {
                    mv = new Movimientos();
                    mv.Id_Emp = (int)dr.GetValue(dr.GetOrdinal("Id_Emp"));
                    mv.Id = (int)dr.GetValue(dr.GetOrdinal("Id_Tm"));
                    mv.Nombre = (string)dr.GetValue(dr.GetOrdinal("Tm_Nombre"));
                    mv.NatMov = (int)dr.GetValue(dr.GetOrdinal("Tm_NatMov"));
                    mv.Tipo = (int)dr.GetValue(dr.GetOrdinal("Tm_Tipo"));
                    mv.Naturaleza = (int)dr.GetValue(dr.GetOrdinal("Tm_Naturaleza"));
                    mv.Inverso = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_CtmInv"))) ? -1 : (int)dr.GetValue(dr.GetOrdinal("Id_CtmInv"));
                    mv.ReqReferencia = dr.IsDBNull(dr.GetOrdinal("Tm_ReqReferencia")) ? false : Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Tm_ReqReferencia")));
                    mv.ReqSispropietario = dr.IsDBNull(dr.GetOrdinal("Tm_ReqSpo")) ? false : Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Tm_ReqSpo")));
                    if (dr.GetValue(dr.GetOrdinal("Tm_AfcVta")) != System.DBNull.Value)
                    {
                        mv.AfeVta = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Tm_AfcVta")));
                    }
                    if (dr.GetValue(dr.GetOrdinal("Tm_AfcIva")) != System.DBNull.Value)
                    {
                        mv.AfeIVA = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Tm_AfcIva")));
                    }
                    if (dr.GetValue(dr.GetOrdinal("Tm_AfcOrdCom")) != System.DBNull.Value)
                    {
                        mv.AfeOrdComp = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Tm_AfcOrdCom")));
                    }
                    if (dr.GetValue(dr.GetOrdinal("Tm_Afecta")) != System.DBNull.Value)
                    {
                        mv.Afecta = (int)dr.GetValue(dr.GetOrdinal("Tm_Afecta"));
                    }
                    mv.Estatus = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Tm_Activo")));
                    if (Convert.ToBoolean(mv.Estatus))
                    {
                        mv.EstatusStr = "Activo";
                    }
                    else
                    {
                        mv.EstatusStr = "Inactivo";
                    }
                    mv.ReqReferencia = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Tm_ReqReferencia")));
                    List.Add(mv);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarTmovimientoReqSpo(Sesion sesion, int Id_Tm, ref bool Tm_ReqSpo)
        {
            //rm //Metodo que verifica si el tipo de movimiento requiere sistema de propietarios
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(sesion.Emp_Cnx);

                string[] Parametros = {
                                          "@Id_Emp",
                                          "@Id_Tm"                                      
                                      };
                object[] Valores = { 
                                       sesion.Id_Emp,
                                       Id_Tm
                                   };

                SqlDataReader dr = null;
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spcatTMovimiento_ConsultaReqSpo", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    if (dr.GetValue(dr.GetOrdinal("Tm_ReqSpo")) != System.DBNull.Value)
                    {
                        Tm_ReqSpo = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Tm_ReqSpo")));
                    }
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

    }
}
