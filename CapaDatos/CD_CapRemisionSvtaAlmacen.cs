using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

using CapaEntidad;

namespace CapaDatos
{
    public class CD_CapRemisionSvtaAlmacen
    {
        public void ConsultaRemisionSvtaAlmacen_Buscar(RemisionSvtaAlmacen RemisionSvtaAlmacen, ref List<RemisionSvtaAlmacen> listaRemisionSvtaAlmacen, string Conexion
            , int? Id_U
            , DateTime? Rva_Fecha_inicio
            , DateTime? Rva_Fecha_fin
            , string Rva_Estatus
            , int? Id_Rva_inicio
            , int? Id_Rva_fin
            , int? Id_Cte
            )
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { 
                                          "@Id_Emp" 
                                          ,"@Id_Cd"
                                          ,"@Id_U"
                                          ,"@Rva_Fecha_inicio"
                                          ,"@Rva_Fecha_fin"
                                          ,"@Rva_Estatus"
                                          ,"@Id_Rva_inicio"
                                          ,"@Id_Rva_fin"
                                          ,"@Id_Cte"
                                      };
                object[] Valores = { 
                                       RemisionSvtaAlmacen.Id_Emp
                                       ,RemisionSvtaAlmacen.Id_Cd
                                       ,Id_U
                                       ,Rva_Fecha_inicio
                                       ,Rva_Fecha_fin
                                       ,Rva_Estatus == string.Empty ? null : Rva_Estatus
                                       ,Id_Rva_inicio
                                       ,Id_Rva_fin
                                       ,Id_Cte
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapRemSvtaAlmacen_Buscar", ref dr, Parametros, Valores);
                listaRemisionSvtaAlmacen = new List<RemisionSvtaAlmacen>();
                while (dr.Read())
                {
                    RemisionSvtaAlmacen = new RemisionSvtaAlmacen();
                    RemisionSvtaAlmacen.Id_Emp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Emp")));
                    RemisionSvtaAlmacen.Id_Cd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cd")));
                    RemisionSvtaAlmacen.Id_Rva = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Rva")));
                    RemisionSvtaAlmacen.Rva_Fecha = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Rva_Fecha")));

                    RemisionSvtaAlmacen.Rva_Estatus = dr.GetValue(dr.GetOrdinal("Rva_Estatus")).ToString();
                    RemisionSvtaAlmacen.Rva_EstatusStr = dr.GetValue(dr.GetOrdinal("Rva_EstatusStr")).ToString();
                    RemisionSvtaAlmacen.Rva_Entrego = dr.GetValue(dr.GetOrdinal("Rva_Entrego")).ToString();
                    RemisionSvtaAlmacen.Rva_Recibio = dr.GetValue(dr.GetOrdinal("Rva_Recibio")).ToString();
                    listaRemisionSvtaAlmacen.Add(RemisionSvtaAlmacen);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarRemisionSvtaAlmacen(ref RemisionSvtaAlmacen RemisionSvtaAlmacen, string Conexion)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { 
                                          "@Id_Emp" 
                                          ,"@Id_Cd"
                                          ,"@Id_Rva"
                                      };
                object[] Valores = { 
                                       RemisionSvtaAlmacen.Id_Emp
                                       ,RemisionSvtaAlmacen.Id_Cd
                                       ,RemisionSvtaAlmacen.Id_Rva
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapRemSvtaAlmacen_Consultar", ref dr, Parametros, Valores);
                NotaCargo Rem = new NotaCargo();
                if (dr.HasRows)
                {
                    dr.Read();
                    RemisionSvtaAlmacen.Id_Emp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Emp")));
                    RemisionSvtaAlmacen.Id_Cd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cd")));
                    RemisionSvtaAlmacen.Cd_Nombre = dr.GetValue(dr.GetOrdinal("Cd_Nombre")).ToString();
                    RemisionSvtaAlmacen.Id_Rva = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Rva")));
                    RemisionSvtaAlmacen.Id_Reg = dr.IsDBNull(dr.GetOrdinal("Id_Reg")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Reg")));
                    RemisionSvtaAlmacen.Id_U = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_U")));
                    RemisionSvtaAlmacen.Rva_Entrego = dr.GetValue(dr.GetOrdinal("Rva_Entrego")).ToString();
                    RemisionSvtaAlmacen.Rva_Recibio = dr.GetValue(dr.GetOrdinal("Rva_Recibio")).ToString();
                    RemisionSvtaAlmacen.Rva_Fecha = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Rva_Fecha")));
                    RemisionSvtaAlmacen.Rva_FecEnvio = dr.IsDBNull(dr.GetOrdinal("Rva_FecEnvio")) ? (DateTime?)null : Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Rva_FecEnvio")));
                    RemisionSvtaAlmacen.Rva_FecRecibio = dr.IsDBNull(dr.GetOrdinal("Rva_FecRecibio")) ? (DateTime?)null : Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Rva_FecRecibio")));
                    RemisionSvtaAlmacen.Rva_Estatus = dr.IsDBNull(dr.GetOrdinal("Rva_Estatus")) ? string.Empty : dr.GetValue(dr.GetOrdinal("Rva_Estatus")).ToString();
                }

                dr.Close();
                RemisionSvtaAlmacen.ListaRemisionSvtaAlmacenDet = new List<RemisionSvtaAlmacenDet>();

                Parametros = new string[]{ 
                                          "@Id_Emp" 
                                          ,"@Id_Cd"
                                          ,"@Id_Rva"
                                          ,"@Db"
                                      };
                Valores = new object[]{ 
                                       RemisionSvtaAlmacen.Id_Emp
                                       ,RemisionSvtaAlmacen.Id_Cd
                                       ,RemisionSvtaAlmacen.Id_Rva
                                       ,RemisionSvtaAlmacen.DbName
                                   };
                sqlcmd = CapaDatos.GenerarSqlCommand("spCapRemSvtaAlmacenDetalle_Consultar", ref dr, Parametros, Valores);
                while (dr.Read())
                {
                    RemisionSvtaAlmacenDet RemisionSvtaAlmacenDet = new RemisionSvtaAlmacenDet();
                    RemisionSvtaAlmacenDet.Id_Emp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Emp")));
                    RemisionSvtaAlmacenDet.Id_Cd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cd")));
                    RemisionSvtaAlmacenDet.Id_Rva = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Rva")));
                    RemisionSvtaAlmacenDet.Id_RvaDet = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_RvaDet")));
                    RemisionSvtaAlmacenDet.Id_Reg = dr.IsDBNull(dr.GetOrdinal("Id_Reg")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Reg")));
                    RemisionSvtaAlmacenDet.Rva_Doc = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Rva_Doc")));
                    RemisionSvtaAlmacenDet.Rva_Fecha = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Rva_Fecha")));
                    RemisionSvtaAlmacenDet.Id_Cte = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cte")));
                    RemisionSvtaAlmacenDet.Cte_NomComercial = dr.GetValue(dr.GetOrdinal("Cte_NomComercial")).ToString();
                    RemisionSvtaAlmacenDet.Rva_Importe = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Rva_Importe")));
                    string dias_revision = dr.GetValue(dr.GetOrdinal("Rva_DiaRev")).ToString().Replace("1", "Lu,").Replace("2", "Ma,").Replace("3", "Mi,").Replace("4", "Ju,").Replace("5", "Vi,").Replace("6", "Sa,").Replace("7", "Do,");
                    RemisionSvtaAlmacenDet.Rva_DiaRev = dias_revision.Length > 0 ? dias_revision.Substring(0, dias_revision.Length - 1) : "";
                    RemisionSvtaAlmacenDet.Rva_Confirmado = dr.IsDBNull(dr.GetOrdinal("Confirmado")) ? false : Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Confirmado")));
                    RemisionSvtaAlmacenDet.Rva_Seleccionado = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Seleccionado")));
                    RemisionSvtaAlmacen.ListaRemisionSvtaAlmacenDet.Add(RemisionSvtaAlmacenDet);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertarRemisionSvtaAlmacen(ref RemisionSvtaAlmacen RemisionSvtaAlmacen, string Conexion, ref int verificador)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {
                CapaDatos.StartTrans();

                string[] Parametros = { 
                                        "@Id_Emp"			
                                        ,"@Id_Cd"			
                                        ,"@Id_Rva"		
                                        ,"@Id_Reg"		
                                        ,"@Id_U"			
                                        ,"@Rva_Entrego"	
                                        ,"@Rva_Recibio"	
                                        ,"@Rva_Fecha"		
                                        ,"@Rva_FecEnvio"	
                                        ,"@Rva_FecRecibio"	
                                        ,"@Rva_Estatus"	
                                      };
                object[] Valores = { 
                                        RemisionSvtaAlmacen.Id_Emp
                                        ,RemisionSvtaAlmacen.Id_Cd
                                        ,RemisionSvtaAlmacen.Id_Rva
                                        ,null //notaCredito.Id_Reg
                                        ,RemisionSvtaAlmacen.Id_U
                                        ,RemisionSvtaAlmacen.Rva_Entrego
                                        ,RemisionSvtaAlmacen.Rva_Recibio
                                        ,RemisionSvtaAlmacen.Rva_Fecha
                                        ,RemisionSvtaAlmacen.Rva_FecEnvio
                                        ,RemisionSvtaAlmacen.Rva_FecRecibio
                                        ,RemisionSvtaAlmacen.Rva_Estatus
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapRemSvtaAlmacen_Insertar", ref verificador, Parametros, Valores); RemisionSvtaAlmacen.Id_Rva = verificador; //folio nuevo



                // -----------------------------------------------------------------
                // Insertar detalle de nota de credito
                // -----------------------------------------------------------------
                string[] ParametrosDet = { 
                                        "@Id_Emp"			
                                        ,"@Id_Cd"			
                                        ,"@Id_Rva"		
                                        ,"@Id_RvaDet"		
                                        ,"@Id_Reg"		
                                        ,"@Rva_Tipo"		
                                        ,"@Rva_Doc"		
                                        ,"@Rva_Fecha"		
                                        ,"@Id_Cte"		
                                        ,"@Rva_Importe"	
                                      };
                int i = 1;
                foreach (RemisionSvtaAlmacenDet RemisionSvtaAlmacenDet in RemisionSvtaAlmacen.ListaRemisionSvtaAlmacenDet)
                {
                    RemisionSvtaAlmacenDet.Id_RvaDet = i;
                    object[] ValoresDet = { 
                                        RemisionSvtaAlmacenDet.Id_Emp			
                                        ,RemisionSvtaAlmacenDet.Id_Cd			
                                        ,RemisionSvtaAlmacen.Id_Rva		
                                        ,RemisionSvtaAlmacenDet.Id_RvaDet		
                                        ,null
                                        ,RemisionSvtaAlmacenDet.Rva_Tipo		
                                        ,RemisionSvtaAlmacenDet.Rva_Doc		
                                        ,RemisionSvtaAlmacenDet.Rva_Fecha.ToString("yyyy/MM/dd")		
                                        ,RemisionSvtaAlmacenDet.Id_Cte		
                                        ,RemisionSvtaAlmacenDet.Rva_Importe	                                       
                                   };

                    sqlcmd = CapaDatos.GenerarSqlCommand("spCapRemSvtaAlmacenDetalle_Insertar", ref verificador, ParametrosDet, ValoresDet);
                    i += 1;
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

        public void ModificarRemisionSvtaAlmacen(ref RemisionSvtaAlmacen RemisionSvtaAlmacen, string Conexion, ref int verificador)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {
                CapaDatos.StartTrans();

                string[] Parametros = { 
                                        "@Id_Emp"			
                                        ,"@Id_Cd"			
                                        ,"@Id_Rva"		
                                        ,"@Id_Reg"		
                                        ,"@Id_U"			
                                        ,"@Rva_Entrego"	
                                        ,"@Rva_Recibio"	
                                        ,"@Rva_Fecha"		
                                        ,"@Rva_FecEnvio"	
                                        ,"@Rva_FecRecibio"	
                                        ,"@Rva_Estatus"	
                                      };
                object[] Valores = { 
                                        RemisionSvtaAlmacen.Id_Emp
                                        ,RemisionSvtaAlmacen.Id_Cd
                                        ,RemisionSvtaAlmacen.Id_Rva
                                        ,null //notaCredito.Id_Reg
                                        ,RemisionSvtaAlmacen.Id_U
                                        ,RemisionSvtaAlmacen.Rva_Entrego
                                        ,RemisionSvtaAlmacen.Rva_Recibio
                                        ,RemisionSvtaAlmacen.Rva_Fecha
                                        ,RemisionSvtaAlmacen.Rva_FecEnvio
                                        ,RemisionSvtaAlmacen.Rva_FecRecibio
                                        ,RemisionSvtaAlmacen.Rva_Estatus
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand(
                    "spCapRemSvtaAlmacen_Modificar", ref verificador, Parametros, Valores);
                //RemisionSvtaAlmacen.Id_Rva = verificador; //folio nuevo



                // -----------------------------------------------------------------
                // Insertar detalle de nota de credito
                // -----------------------------------------------------------------
                string[] ParametrosDet = { 
                                        "@Id_Emp"			
                                        ,"@Id_Cd"			
                                        ,"@Id_Rva"		
                                        ,"@Id_RvaDet"		
                                        ,"@Id_Reg"		
                                        ,"@Rva_Tipo"		
                                        ,"@Rva_Doc"		
                                        ,"@Rva_Fecha"		
                                        ,"@Id_Cte"		
                                        ,"@Rva_Importe"	                                        	
                                      };
                int i = 1;
                foreach (RemisionSvtaAlmacenDet RemisionSvtaAlmacenDet in RemisionSvtaAlmacen.ListaRemisionSvtaAlmacenDet)
                {
                    RemisionSvtaAlmacenDet.Id_RvaDet = i;
                    object[] ValoresDet = { 
                                        RemisionSvtaAlmacenDet.Id_Emp			
                                        ,RemisionSvtaAlmacenDet.Id_Cd			
                                        ,RemisionSvtaAlmacen.Id_Rva		
                                        ,RemisionSvtaAlmacenDet.Id_RvaDet		
                                        ,null
                                        ,RemisionSvtaAlmacenDet.Rva_Tipo		
                                        ,RemisionSvtaAlmacenDet.Rva_Doc		
                                        ,RemisionSvtaAlmacenDet.Rva_Fecha		
                                        ,RemisionSvtaAlmacenDet.Id_Cte		
                                        ,RemisionSvtaAlmacenDet.Rva_Importe	                                      
                                   };

                    sqlcmd = CapaDatos.GenerarSqlCommand("spCapRemSvtaAlmacenDetalle_Insertar", ref verificador, ParametrosDet, ValoresDet);
                    i += 1;
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

        public void EliminarRemisionSvtaAlmacen(RemisionSvtaAlmacen RemisionSvtaAlmacen, string Conexion, ref int verificador)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {
                CapaDatos.StartTrans();
                string[] Parametros = { 
	                                    "@Id_Emp"
                                        ,"@Id_Cd"
                                        ,"@Id_Rva"
                                      };
                object[] Valores = { 
                                       RemisionSvtaAlmacen.Id_Emp
                                       ,RemisionSvtaAlmacen.Id_Cd
                                       ,RemisionSvtaAlmacen.Id_Rva
                                   };

                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd = CapaDatos.GenerarSqlCommand("spCapRemSvtaAlmacen_Eliminar", ref verificador, Parametros, Valores);
                //ordenCompra.Id_Ord = verificador; //identity de orden de compra 

                CapaDatos.CommitTrans();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                CapaDatos.RollBackTrans();
                throw ex;
            }
        }

        public void ModificarEstatusRemisionSvtaAlmacen(RemisionSvtaAlmacen RemisionSvtaAlmacen, string Conexion, ref int verificador)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {
                CapaDatos.StartTrans();
                string[] Parametros = { 
	                                    "@Id_Emp"
                                        ,"@Id_Cd"
                                        ,"@Id_Rva"
                                        ,"@Rva_Estatus"
                                      };
                object[] Valores = { 
                                       RemisionSvtaAlmacen.Id_Emp
                                       ,RemisionSvtaAlmacen.Id_Cd
                                       ,RemisionSvtaAlmacen.Id_Rva
                                       ,RemisionSvtaAlmacen.Rva_Estatus
                                   };

                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd = CapaDatos.GenerarSqlCommand("spCapRemSvtaAlmacen_ModificarEstatus", ref verificador, Parametros, Valores);
                //ordenCompra.Id_Ord = verificador; //identity de orden de compra 

                CapaDatos.CommitTrans();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                CapaDatos.RollBackTrans();
                throw ex;
            }
        }


        public void ConsultarRemisionSvtaAlmacen_Sugerido(ref RemisionSvtaAlmacen RemisionSvtaAlmacen, string Conexion)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { 
                                          "@Id_Emp", 
                                          "@Id_Cd",
                                          "@Fecha",
                                          "@FechaFin"
                                      };
                object[] Valores = { 
                                       RemisionSvtaAlmacen.Id_Emp,
                                       RemisionSvtaAlmacen.Id_Cd,
                                       RemisionSvtaAlmacen.Rva_Fecha,
                                       RemisionSvtaAlmacen.Rva_FechaFin
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spProSeguimientoCobranzaRemAlmacen_Consultar", ref dr, Parametros, Valores);



                while (dr.Read())
                {
                    RemisionSvtaAlmacenDet RemisionSvtaAlmacenDet = new RemisionSvtaAlmacenDet();
                    RemisionSvtaAlmacenDet.Id_Emp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Emp")));
                    RemisionSvtaAlmacenDet.Id_Cd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cd")));
                    RemisionSvtaAlmacenDet.Id_RvaDet = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id")));
                    RemisionSvtaAlmacenDet.Rva_Doc = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Rem")));
                    RemisionSvtaAlmacenDet.Rva_Fecha = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Rem_Fecha")));
                    RemisionSvtaAlmacenDet.Id_Cte = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cte")));
                    RemisionSvtaAlmacenDet.Cte_NomComercial = dr.GetValue(dr.GetOrdinal("Cte_NomComercial")).ToString();
                    RemisionSvtaAlmacenDet.Rva_Importe = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Rem_Importe")));
                    string dias_revision = dr.GetValue(dr.GetOrdinal("DiasRevicion")).ToString().Replace("1", "Lu,").Replace("2", "Ma,").Replace("3", "Mi,").Replace("4", "Ju,").Replace("5", "Vi,").Replace("6", "Sa,").Replace("7", "Do,");
                    RemisionSvtaAlmacenDet.Rva_DiaRev = dias_revision.Length > 0 ? dias_revision.Substring(0, dias_revision.Length - 1) : "";
                    RemisionSvtaAlmacenDet.Rva_Confirmado = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Confirmado")));
                    RemisionSvtaAlmacenDet.Rva_Seleccionado = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Seleccionado")));
                    RemisionSvtaAlmacen.ListaRemisionSvtaAlmacenDet.Add(RemisionSvtaAlmacenDet);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Confirmar(RemisionSvtaAlmacenDet svta, ref int verificador, string Conexion)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {
                CapaDatos.StartTrans();
                string[] Parametros = { 
	                                    "@Id_Emp"
                                        ,"@Id_Cd"
                                        ,"@Id_Rva"
                                        ,"@Id_Rem"
                                      };
                object[] Valores = { 
                                       svta.Id_Emp
                                       ,svta.Id_Cd
                                       ,svta.Id_Rva
                                       ,svta.Rva_Doc
                                   };

                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd = CapaDatos.GenerarSqlCommand("spCapRemSvtaAlmacenDet_Confirmar", ref verificador, Parametros, Valores);


                CapaDatos.CommitTrans();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                CapaDatos.RollBackTrans();
                throw ex;
            }
        }

        public void ConsultaRemisionEncabezado(ref Remision Remision, ref bool encontrado, string Conexion)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {
                SqlDataReader dr = null;
                string[] Parametros = { 
	                                    "@Id_Emp"
                                        ,"@Id_Cd"
                                        ,"@Id_Rem"
                                      };
                object[] Valores = { 
                                       Remision.Id_Emp
                                       ,Remision.Id_Cd
                                       ,Remision.Id_Rem
                                   };

                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd = CapaDatos.GenerarSqlCommand("spCapRemisionSvta_Consultar", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    Remision.Rem_Fecha = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Rem_Fecha")));
                    Remision.Rem_Estatus = dr.GetValue(dr.GetOrdinal("Rem_Estatus")).ToString();
                    Remision.Id_Cte = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cte")));
                    Remision.Cte_NomComercial = dr.GetValue(dr.GetOrdinal("Cte_NomComercial")).ToString();
                   // Remision.Rem_Saldo = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Saldo")));
                    string dias_revision = dr.GetValue(dr.GetOrdinal("Rva_DiaRev")).ToString().Replace("1", "Lu,").Replace("2", "Ma,").Replace("3", "Mi,").Replace("4", "Ju,").Replace("5", "Vi,").Replace("6", "Sa,").Replace("7", "Do,");
                    //Remision.Rem_Notas = dias_revision.Length > 0 ? dias_revision.Substring(0, dias_revision.Length - 1) : "";
                    encontrado = true;
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
