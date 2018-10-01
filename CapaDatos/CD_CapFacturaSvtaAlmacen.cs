using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

using CapaEntidad;

namespace CapaDatos
{
    public class CD_CapFacturaSvtaAlmacen
    {
        public void ConsultaFacturaSvtaAlmacen_Buscar(FacturaSvtaAlmacen facturaSvtaAlmacen, ref List<FacturaSvtaAlmacen> listaFacturaSvtaAlmacen, string Conexion
            , int? Id_U
            , DateTime? Fva_Fecha_inicio
            , DateTime? Fva_Fecha_fin
            , string Fva_Estatus
            , int? Id_Fva_inicio
            , int? Id_Fva_fin
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
                                          ,"@Fva_Fecha_inicio"
                                          ,"@Fva_Fecha_fin"
                                          ,"@Fva_Estatus"
                                          ,"@Id_Fva_inicio"
                                          ,"@Id_Fva_fin"
                                          ,"@Id_Cte"
                                      };
                object[] Valores = { 
                                       facturaSvtaAlmacen.Id_Emp
                                       ,facturaSvtaAlmacen.Id_Cd
                                       ,Id_U
                                       ,Fva_Fecha_inicio
                                       ,Fva_Fecha_fin
                                       ,Fva_Estatus == string.Empty ? null : Fva_Estatus
                                       ,Id_Fva_inicio
                                       ,Id_Fva_fin
                                       ,Id_Cte
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapFacSvtaAlmacen_Buscar", ref dr, Parametros, Valores);
                listaFacturaSvtaAlmacen = new List<FacturaSvtaAlmacen>();
                while (dr.Read())
                {
                    facturaSvtaAlmacen = new FacturaSvtaAlmacen();
                    facturaSvtaAlmacen.Id_Emp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Emp")));
                    facturaSvtaAlmacen.Id_Cd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cd")));
                    facturaSvtaAlmacen.Id_Fva = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Fva")));
                    facturaSvtaAlmacen.Fva_Fecha = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Fva_Fecha")));

                    facturaSvtaAlmacen.Fva_Estatus = dr.GetValue(dr.GetOrdinal("Fva_Estatus")).ToString();
                    facturaSvtaAlmacen.Fva_EstatusStr = dr.GetValue(dr.GetOrdinal("Fva_EstatusStr")).ToString();
                    facturaSvtaAlmacen.Fva_Entrego = dr.GetValue(dr.GetOrdinal("Fva_Entrego")).ToString();
                    facturaSvtaAlmacen.Fva_Recibio = dr.GetValue(dr.GetOrdinal("Fva_Recibio")).ToString();
                    listaFacturaSvtaAlmacen.Add(facturaSvtaAlmacen);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarFacturaSvtaAlmacen(ref FacturaSvtaAlmacen facturaSvtaAlmacen, string Conexion)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { 
                                          "@Id_Emp" 
                                          ,"@Id_Cd"
                                          ,"@Id_Fva"
                                      };
                object[] Valores = { 
                                       facturaSvtaAlmacen.Id_Emp
                                       ,facturaSvtaAlmacen.Id_Cd
                                       ,facturaSvtaAlmacen.Id_Fva
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapFacSvtaAlmacen_Consultar", ref dr, Parametros, Valores);
                NotaCargo fac = new NotaCargo();
                if (dr.HasRows)
                {
                    dr.Read();
                    facturaSvtaAlmacen.Id_Emp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Emp")));
                    facturaSvtaAlmacen.Id_Cd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cd")));
                    facturaSvtaAlmacen.Cd_Nombre = dr.GetValue(dr.GetOrdinal("Cd_Nombre")).ToString();
                    facturaSvtaAlmacen.Id_Fva = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Fva")));
                    facturaSvtaAlmacen.Id_Reg = dr.IsDBNull(dr.GetOrdinal("Id_Reg")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Reg")));
                    facturaSvtaAlmacen.Id_U = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_U")));
                    facturaSvtaAlmacen.Fva_Entrego = dr.GetValue(dr.GetOrdinal("Fva_Entrego")).ToString();
                    facturaSvtaAlmacen.Fva_Recibio = dr.GetValue(dr.GetOrdinal("Fva_Recibio")).ToString();
                    facturaSvtaAlmacen.Fva_Fecha = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Fva_Fecha")));
                    facturaSvtaAlmacen.Fva_FecEnvio = dr.IsDBNull(dr.GetOrdinal("Fva_FecEnvio")) ? (DateTime?)null : Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Fva_FecEnvio")));
                    facturaSvtaAlmacen.Fva_FecRecibio = dr.IsDBNull(dr.GetOrdinal("Fva_FecRecibio")) ? (DateTime?)null : Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Fva_FecRecibio")));
                    facturaSvtaAlmacen.Fva_Estatus = dr.IsDBNull(dr.GetOrdinal("Fva_Estatus")) ? string.Empty : dr.GetValue(dr.GetOrdinal("Fva_Estatus")).ToString();
                }

                dr.Close();
                facturaSvtaAlmacen.ListaFacturaSvtaAlmacenDet = new List<FacturaSvtaAlmacenDet>();

                Parametros = new string[]{ 
                                          "@Id_Emp" 
                                          ,"@Id_Cd"
                                          ,"@Id_Fva"
                                          ,"@Db"
                                      };
                Valores = new object[]{ 
                                       facturaSvtaAlmacen.Id_Emp
                                       ,facturaSvtaAlmacen.Id_Cd
                                       ,facturaSvtaAlmacen.Id_Fva
                                       ,facturaSvtaAlmacen.DbName
                                   };
                sqlcmd = CapaDatos.GenerarSqlCommand("spCapFacSvtaAlmacenDetalle_Consultar", ref dr, Parametros, Valores);
                while (dr.Read())
                {
                    FacturaSvtaAlmacenDet facturaSvtaAlmacenDet = new FacturaSvtaAlmacenDet();
                    facturaSvtaAlmacenDet.Id_Emp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Emp")));
                    facturaSvtaAlmacenDet.Id_Cd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cd")));
                    facturaSvtaAlmacenDet.Id_Fva = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Fva")));
                    facturaSvtaAlmacenDet.Id_FvaDet = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_FvaDet")));
                    facturaSvtaAlmacenDet.Id_Reg = dr.IsDBNull(dr.GetOrdinal("Id_Reg")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Reg")));
                    facturaSvtaAlmacenDet.Fva_Doc = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Fva_Doc")));
                    facturaSvtaAlmacenDet.Fva_Fecha = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Fva_Fecha")));
                    facturaSvtaAlmacenDet.Id_Cte = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cte")));
                    facturaSvtaAlmacenDet.Cte_NomComercial = dr.GetValue(dr.GetOrdinal("Cte_NomComercial")).ToString();
                    facturaSvtaAlmacenDet.Fva_Importe = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Fva_Importe")));
                    string dias_revision = dr.GetValue(dr.GetOrdinal("Fva_DiaRev")).ToString().Replace("1", "Lu,").Replace("2", "Ma,").Replace("3", "Mi,").Replace("4", "Ju,").Replace("5", "Vi,").Replace("6", "Sa,").Replace("7", "Do,");
                    facturaSvtaAlmacenDet.Fva_DiaRev = dias_revision.Length > 0 ? dias_revision.Substring(0, dias_revision.Length - 1) : "";
                    facturaSvtaAlmacenDet.Fva_Confirmado = dr.IsDBNull(dr.GetOrdinal("Confirmado")) ? false : Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Confirmado")));
                    facturaSvtaAlmacenDet.Fva_Seleccionado = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Seleccionado")));
                    facturaSvtaAlmacen.ListaFacturaSvtaAlmacenDet.Add(facturaSvtaAlmacenDet);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertarFacturaSvtaAlmacen(ref FacturaSvtaAlmacen facturaSvtaAlmacen, string Conexion, ref int verificador)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {
                CapaDatos.StartTrans();

                string[] Parametros = { 
                                        "@Id_Emp"			
                                        ,"@Id_Cd"			
                                        ,"@Id_Fva"		
                                        ,"@Id_Reg"		
                                        ,"@Id_U"			
                                        ,"@Fva_Entrego"	
                                        ,"@Fva_Recibio"	
                                        ,"@Fva_Fecha"		
                                        ,"@Fva_FecEnvio"	
                                        ,"@Fva_FecRecibio"	
                                        ,"@Fva_Estatus"	
                                      };
                object[] Valores = { 
                                        facturaSvtaAlmacen.Id_Emp
                                        ,facturaSvtaAlmacen.Id_Cd
                                        ,facturaSvtaAlmacen.Id_Fva
                                        ,null //notaCredito.Id_Reg
                                        ,facturaSvtaAlmacen.Id_U
                                        ,facturaSvtaAlmacen.Fva_Entrego
                                        ,facturaSvtaAlmacen.Fva_Recibio
                                        ,facturaSvtaAlmacen.Fva_Fecha
                                        ,facturaSvtaAlmacen.Fva_FecEnvio
                                        ,facturaSvtaAlmacen.Fva_FecRecibio
                                        ,facturaSvtaAlmacen.Fva_Estatus
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapFacSvtaAlmacen_Insertar", ref verificador, Parametros, Valores); facturaSvtaAlmacen.Id_Fva = verificador; //folio nuevo



                // -----------------------------------------------------------------
                // Insertar detalle de nota de credito
                // -----------------------------------------------------------------
                string[] ParametrosDet = { 
                                        "@Id_Emp"			
                                        ,"@Id_Cd"			
                                        ,"@Id_Fva"		
                                        ,"@Id_FvaDet"		
                                        ,"@Id_Reg"		
                                        ,"@Fva_Tipo"		
                                        ,"@Fva_Doc"		
                                        ,"@Fva_Fecha"		
                                        ,"@Id_Cte"		
                                        ,"@Fva_Importe"	
                                      };
                int i = 1;
                foreach (FacturaSvtaAlmacenDet facturaSvtaAlmacenDet in facturaSvtaAlmacen.ListaFacturaSvtaAlmacenDet)
                {
                    facturaSvtaAlmacenDet.Id_FvaDet = i;
                    object[] ValoresDet = { 
                                        facturaSvtaAlmacenDet.Id_Emp			
                                        ,facturaSvtaAlmacenDet.Id_Cd			
                                        ,facturaSvtaAlmacen.Id_Fva		
                                        ,facturaSvtaAlmacenDet.Id_FvaDet		
                                        ,null
                                        ,facturaSvtaAlmacenDet.Fva_Tipo		
                                        ,facturaSvtaAlmacenDet.Fva_Doc		
                                        ,facturaSvtaAlmacenDet.Fva_Fecha.ToString("yyyy/MM/dd")		
                                        ,facturaSvtaAlmacenDet.Id_Cte		
                                        ,facturaSvtaAlmacenDet.Fva_Importe	                                       
                                   };

                    sqlcmd = CapaDatos.GenerarSqlCommand("spCapFacSvtaAlmacenDetalle_Insertar", ref verificador, ParametrosDet, ValoresDet);
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

        public void ModificarFacturaSvtaAlmacen(ref FacturaSvtaAlmacen facturaSvtaAlmacen, string Conexion, ref int verificador)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {
                CapaDatos.StartTrans();

                string[] Parametros = { 
                                        "@Id_Emp"			
                                        ,"@Id_Cd"			
                                        ,"@Id_Fva"		
                                        ,"@Id_Reg"		
                                        ,"@Id_U"			
                                        ,"@Fva_Entrego"	
                                        ,"@Fva_Recibio"	
                                        ,"@Fva_Fecha"		
                                        ,"@Fva_FecEnvio"	
                                        ,"@Fva_FecRecibio"	
                                        ,"@Fva_Estatus"	
                                      };
                object[] Valores = { 
                                        facturaSvtaAlmacen.Id_Emp
                                        ,facturaSvtaAlmacen.Id_Cd
                                        ,facturaSvtaAlmacen.Id_Fva
                                        ,null //notaCredito.Id_Reg
                                        ,facturaSvtaAlmacen.Id_U
                                        ,facturaSvtaAlmacen.Fva_Entrego
                                        ,facturaSvtaAlmacen.Fva_Recibio
                                        ,facturaSvtaAlmacen.Fva_Fecha
                                        ,facturaSvtaAlmacen.Fva_FecEnvio
                                        ,facturaSvtaAlmacen.Fva_FecRecibio
                                        ,facturaSvtaAlmacen.Fva_Estatus
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand(
                    "spCapFacSvtaAlmacen_Modificar", ref verificador, Parametros, Valores);
                //facturaSvtaAlmacen.Id_Fva = verificador; //folio nuevo



                // -----------------------------------------------------------------
                // Insertar detalle de nota de credito
                // -----------------------------------------------------------------
                string[] ParametrosDet = { 
                                        "@Id_Emp"			
                                        ,"@Id_Cd"			
                                        ,"@Id_Fva"		
                                        ,"@Id_FvaDet"		
                                        ,"@Id_Reg"		
                                        ,"@Fva_Tipo"		
                                        ,"@Fva_Doc"		
                                        ,"@Fva_Fecha"		
                                        ,"@Id_Cte"		
                                        ,"@Fva_Importe"	                                        	
                                      };
                int i = 1;
                foreach (FacturaSvtaAlmacenDet facturaSvtaAlmacenDet in facturaSvtaAlmacen.ListaFacturaSvtaAlmacenDet)
                {
                    facturaSvtaAlmacenDet.Id_FvaDet = i;
                    object[] ValoresDet = { 
                                        facturaSvtaAlmacenDet.Id_Emp			
                                        ,facturaSvtaAlmacenDet.Id_Cd			
                                        ,facturaSvtaAlmacen.Id_Fva		
                                        ,facturaSvtaAlmacenDet.Id_FvaDet		
                                        ,null
                                        ,facturaSvtaAlmacenDet.Fva_Tipo		
                                        ,facturaSvtaAlmacenDet.Fva_Doc		
                                        ,facturaSvtaAlmacenDet.Fva_Fecha		
                                        ,facturaSvtaAlmacenDet.Id_Cte		
                                        ,facturaSvtaAlmacenDet.Fva_Importe	                                      
                                   };

                    sqlcmd = CapaDatos.GenerarSqlCommand("spCapFacSvtaAlmacenDetalle_Insertar", ref verificador, ParametrosDet, ValoresDet);
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

        public void EliminarFacturaSvtaAlmacen(FacturaSvtaAlmacen facturaSvtaAlmacen, string Conexion, ref int verificador)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {
                CapaDatos.StartTrans();
                string[] Parametros = { 
	                                    "@Id_Emp"
                                        ,"@Id_Cd"
                                        ,"@Id_Fva"
                                      };
                object[] Valores = { 
                                       facturaSvtaAlmacen.Id_Emp
                                       ,facturaSvtaAlmacen.Id_Cd
                                       ,facturaSvtaAlmacen.Id_Fva
                                   };

                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd = CapaDatos.GenerarSqlCommand("spCapFacSvtaAlmacen_Eliminar", ref verificador, Parametros, Valores);
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

        public void ModificarEstatusFacturaSvtaAlmacen(FacturaSvtaAlmacen facturaSvtaAlmacen, string Conexion, ref int verificador)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {
                CapaDatos.StartTrans();
                string[] Parametros = { 
	                                    "@Id_Emp"
                                        ,"@Id_Cd"
                                        ,"@Id_Fva"
                                        ,"@Fva_Estatus"
                                      };
                object[] Valores = { 
                                       facturaSvtaAlmacen.Id_Emp
                                       ,facturaSvtaAlmacen.Id_Cd
                                       ,facturaSvtaAlmacen.Id_Fva
                                       ,facturaSvtaAlmacen.Fva_Estatus
                                   };

                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd = CapaDatos.GenerarSqlCommand("spCapFacSvtaAlmacen_ModificarEstatus", ref verificador, Parametros, Valores);
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


        public void ConsultarFacturaSvtaAlmacen_Sugerido(ref FacturaSvtaAlmacen facturaSvtaAlmacen, string Conexion)
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
                                       facturaSvtaAlmacen.Id_Emp,
                                       facturaSvtaAlmacen.Id_Cd,
                                       facturaSvtaAlmacen.Fva_Fecha,
                                       facturaSvtaAlmacen.Fva_FechaFin
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spProSeguimientoCobranzaSvtaAlmacen_Consultar", ref dr, Parametros, Valores);



                while (dr.Read())
                {
                    FacturaSvtaAlmacenDet facturaSvtaAlmacenDet = new FacturaSvtaAlmacenDet();
                    facturaSvtaAlmacenDet.Id_Emp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Emp")));
                    facturaSvtaAlmacenDet.Id_Cd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cd")));
                    facturaSvtaAlmacenDet.Id_FvaDet = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id")));
                    facturaSvtaAlmacenDet.Fva_Doc = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Fac")));
                    facturaSvtaAlmacenDet.Fva_Fecha = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Fac_Fecha")));
                    facturaSvtaAlmacenDet.Id_Cte = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cte")));
                    facturaSvtaAlmacenDet.Cte_NomComercial = dr.GetValue(dr.GetOrdinal("Cte_NomComercial")).ToString();
                    facturaSvtaAlmacenDet.Fva_Importe = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Fac_Importe")));
                    string dias_revision = dr.GetValue(dr.GetOrdinal("DiasRevicion")).ToString().Replace("1", "Lu,").Replace("2", "Ma,").Replace("3", "Mi,").Replace("4", "Ju,").Replace("5", "Vi,").Replace("6", "Sa,").Replace("7", "Do,");
                    facturaSvtaAlmacenDet.Fva_DiaRev = dias_revision.Length > 0 ? dias_revision.Substring(0, dias_revision.Length - 1) : "";
                    facturaSvtaAlmacenDet.Fva_Confirmado = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Confirmado")));
                    facturaSvtaAlmacenDet.Fva_Seleccionado = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Seleccionado")));
                    facturaSvtaAlmacen.ListaFacturaSvtaAlmacenDet.Add(facturaSvtaAlmacenDet);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Confirmar(FacturaSvtaAlmacen svta, ref int verificador, string Conexion)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {
               
                SqlCommand sqlcmd = new SqlCommand();
                CapaDatos.StartTrans();
                string[] Parametros = { 
	                                    "@Id_Emp"
                                        ,"@Id_Cd"
                                        ,"@Id_Fva"
                                        ,"@Id_Fac"
                                      };

                foreach (FacturaSvtaAlmacenDet FacturaSvtaAlmacenDet in svta.ListaFacturaSvtaAlmacenDet)
                {

                    object[] Valores = { 
                                       FacturaSvtaAlmacenDet.Id_Emp
                                       ,FacturaSvtaAlmacenDet.Id_Cd
                                       ,FacturaSvtaAlmacenDet.Id_Fva
                                       ,FacturaSvtaAlmacenDet.Fva_Doc
                                   };

                    sqlcmd = CapaDatos.GenerarSqlCommand("spCapFacSvtaAlmacenDet_Confirmar", ref verificador, Parametros, Valores);
                  
                }

                CapaDatos.CommitTrans();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
                verificador = 1;
            }
            catch (Exception ex)
            {
                CapaDatos.RollBackTrans();
                throw ex;
            }
        }

        public void ConsultaFacturaEncabezado(ref Factura factura, ref bool encontrado, string Conexion)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {
                SqlDataReader dr = null;
                string[] Parametros = { 
	                                    "@Id_Emp"
                                        ,"@Id_Cd"
                                        ,"@Id_Fac"
                                      };
                object[] Valores = { 
                                       factura.Id_Emp
                                       ,factura.Id_Cd
                                       ,factura.Id_Fac
                                   };

                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd = CapaDatos.GenerarSqlCommand("spCapFacturaSvta_Consultar", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    factura.Fac_Fecha = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Fac_Fecha")));
                    factura.Fac_Estatus = dr.GetValue(dr.GetOrdinal("Fac_Estatus")).ToString();
                    factura.Id_Cte = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cte")));
                    factura.Cte_NomComercial = dr.GetValue(dr.GetOrdinal("Cte_NomComercial")).ToString();
                    factura.Fac_Saldo = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Saldo")));
                    string dias_revision = dr.GetValue(dr.GetOrdinal("Fva_DiaRev")).ToString().Replace("1", "Lu,").Replace("2", "Ma,").Replace("3", "Mi,").Replace("4", "Ju,").Replace("5", "Vi,").Replace("6", "Sa,").Replace("7", "Do,");
                    factura.Fac_Notas = dias_revision.Length > 0 ? dias_revision.Substring(0, dias_revision.Length - 1) : "";
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
