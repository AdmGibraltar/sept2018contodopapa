using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

using CapaEntidad;

namespace CapaDatos
{
    public class CD_CapFacturaAlmacenCobro
    {
        public void ConsultaFacturaAlmacenCobro_Buscar(FacturaAlmacenCobro FacturaAlmacenCobro, ref List<FacturaAlmacenCobro> listaFacturaAlmacenCobro, string Conexion
            , int? Id_U
            , DateTime? Fac_Fecha_inicio
            , DateTime? Fac_Fecha_fin
            , string Fac_Estatus
            , int? Id_Fac_inicio
            , int? Id_Fac_fin
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
                                          ,"@Fac_Fecha_inicio"
                                          ,"@Fac_Fecha_fin"
                                          ,"@Fac_Estatus"
                                          ,"@Id_Fac_inicio"
                                          ,"@Id_Fac_fin"
                                          ,"@Id_Cte"
                                      };
                object[] Valores = { 
                                       FacturaAlmacenCobro.Id_Emp
                                       ,FacturaAlmacenCobro.Id_Cd
                                       ,Id_U
                                       ,Fac_Fecha_inicio
                                       ,Fac_Fecha_fin
                                       ,Fac_Estatus == string.Empty ? null : Fac_Estatus
                                       ,Id_Fac_inicio
                                       ,Id_Fac_fin
                                       ,Id_Cte
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapFacAlmacenCobro_Buscar", ref dr, Parametros, Valores);
                listaFacturaAlmacenCobro = new List<FacturaAlmacenCobro>();
                while (dr.Read())
                {
                    FacturaAlmacenCobro = new FacturaAlmacenCobro();
                    FacturaAlmacenCobro.Id_Emp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Emp")));
                    FacturaAlmacenCobro.Id_Cd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cd")));
                    FacturaAlmacenCobro.Id_Fac = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Fac")));
                    FacturaAlmacenCobro.Fac_Fecha = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Fac_Fecha")));

                    FacturaAlmacenCobro.Fac_Estatus = dr.GetValue(dr.GetOrdinal("AlmCob_Estatus")).ToString();
                    FacturaAlmacenCobro.Fac_EstatusStr = dr.GetValue(dr.GetOrdinal("AlmCob_EstatusStr")).ToString();
                    FacturaAlmacenCobro.Fac_Entrego = dr.GetValue(dr.GetOrdinal("AlmCob_Entrego")).ToString();
                    FacturaAlmacenCobro.Fac_Recibio = dr.GetValue(dr.GetOrdinal("AlmCob_Recibio")).ToString();
                    listaFacturaAlmacenCobro.Add(FacturaAlmacenCobro);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarFacturaAlmacenCobro(ref FacturaAlmacenCobro FacturaAlmacenCobro, string Conexion)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { 
                                          "@Id_Emp" 
                                          ,"@Id_Cd"
                                          ,"@Id_Fac"
                                      };
                object[] Valores = { 
                                       FacturaAlmacenCobro.Id_Emp
                                       ,FacturaAlmacenCobro.Id_Cd
                                       ,FacturaAlmacenCobro.Id_Fac
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapFacAlmacenCobro_Consultar", ref dr, Parametros, Valores);
                NotaCargo fac = new NotaCargo();
                if (dr.HasRows)
                {
                    dr.Read();
                    FacturaAlmacenCobro.Id_Emp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Emp")));
                    FacturaAlmacenCobro.Id_Cd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cd")));
                    FacturaAlmacenCobro.Cd_Nombre = dr.GetValue(dr.GetOrdinal("Cd_Nombre")).ToString();
                    FacturaAlmacenCobro.Id_Fac = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_AlmCob")));
                    FacturaAlmacenCobro.Id_U = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_U")));
                    FacturaAlmacenCobro.Fac_Entrego = dr.GetValue(dr.GetOrdinal("AlmCob_Entrego")).ToString();
                    FacturaAlmacenCobro.Fac_Recibio = dr.GetValue(dr.GetOrdinal("AlmCob_Recibio")).ToString();
                    FacturaAlmacenCobro.Fac_Fecha = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("AlmCob_Fecha")));
                    FacturaAlmacenCobro.Fac_Estatus = dr.IsDBNull(dr.GetOrdinal("AlmCob_Estatus")) ? string.Empty : dr.GetValue(dr.GetOrdinal("AlmCob_Estatus")).ToString();

                    
                }

                dr.Close();
                FacturaAlmacenCobro.ListaFacturaAlmacenCobroDet = new List<FacturaAlmacenCobroDet>();

                Parametros = new string[]{ 
                                          "@Id_Emp" 
                                          ,"@Id_Cd"
                                          ,"@Id_Fac"
                                          ,"@Db"
                                      };
                Valores = new object[]{ 
                                       FacturaAlmacenCobro.Id_Emp
                                       ,FacturaAlmacenCobro.Id_Cd
                                       ,FacturaAlmacenCobro.Id_Fac
                                       ,FacturaAlmacenCobro.DbName
                                   };
                sqlcmd = CapaDatos.GenerarSqlCommand("spCapFacAlmacenCobroDetalle_Consultar", ref dr, Parametros, Valores);
                while (dr.Read())
                {
                    FacturaAlmacenCobroDet FacturaAlmacenCobroDet = new FacturaAlmacenCobroDet();
                    FacturaAlmacenCobroDet.Id_Emp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Emp")));
                    FacturaAlmacenCobroDet.Id_Cd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cd")));
                    FacturaAlmacenCobroDet.Id_Fac = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_AlmCob")));
                    FacturaAlmacenCobroDet.Id_FacDet = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_AlmCobDet")));
                    FacturaAlmacenCobroDet.Fac_Doc = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Fac")));
                    FacturaAlmacenCobroDet.Fac_Fecha = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Fac_Fecha")));
                    FacturaAlmacenCobroDet.Id_Cte = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cte")));
                    FacturaAlmacenCobroDet.Cte_NomComercial = dr.GetValue(dr.GetOrdinal("Cte_NomComercial")).ToString();
                    FacturaAlmacenCobroDet.Fac_Importe = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Fac_Importe")));
                    FacturaAlmacenCobroDet.Fac_Confirmado = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Confirmado")));
                    FacturaAlmacenCobroDet.Fac_Seleccionado = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Seleccionado")));
                    FacturaAlmacenCobroDet.Cte_DiasRevision = dr.GetValue(dr.GetOrdinal("DiasRevicion")).ToString();
                    if (FacturaAlmacenCobroDet.Cte_DiasRevision.Length > 0)
                        FacturaAlmacenCobroDet.Cte_DiasRevision = FacturaAlmacenCobroDet.Cte_DiasRevision.Substring(0, FacturaAlmacenCobroDet.Cte_DiasRevision.Length - 1);

                    FacturaAlmacenCobro.ListaFacturaAlmacenCobroDet.Add(FacturaAlmacenCobroDet);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertarFacturaAlmacenCobro(ref FacturaAlmacenCobro FacturaAlmacenCobro, string Conexion, ref int verificador)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {
                CapaDatos.StartTrans();

                string[] Parametros = { 
                                        "@Id_Emp"			
                                        ,"@Id_Cd"			
                                        ,"@Id_Fac"		
                                        ,"@Id_U"			
                                        ,"@Fac_Entrego"	
                                        ,"@Fac_Recibio"	
                                        ,"@Fac_Fecha"		
                                        ,"@Fac_Estatus"	
                                      };
                object[] Valores = { 
                                        FacturaAlmacenCobro.Id_Emp
                                        ,FacturaAlmacenCobro.Id_Cd
                                        ,FacturaAlmacenCobro.Id_Fac
                                        ,FacturaAlmacenCobro.Id_U
                                        ,FacturaAlmacenCobro.Fac_Entrego
                                        ,FacturaAlmacenCobro.Fac_Recibio
                                        ,FacturaAlmacenCobro.Fac_Fecha
                                        ,FacturaAlmacenCobro.Fac_Estatus
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapFacAlmacenCobro_Insertar", ref verificador, Parametros, Valores); FacturaAlmacenCobro.Id_Fac = verificador; //folio nuevo



                // -----------------------------------------------------------------
                // Insertar detalle de nota de credito
                // -----------------------------------------------------------------
                string[] ParametrosDet = { 
                                        "@Id_Emp"			
                                        ,"@Id_Cd"			
                                        ,"@Id_Fac"		
                                        ,"@Id_FacDet"		
                                        ,"@Fac_Doc"		
                                        ,"@Fac_Fecha"		
                                        ,"@Id_Cte"		
                                        ,"@Fac_Importe"	
                                      };
                int i = 1;
                foreach (FacturaAlmacenCobroDet FacturaAlmacenCobroDet in FacturaAlmacenCobro.ListaFacturaAlmacenCobroDet)
                {
                    FacturaAlmacenCobroDet.Id_FacDet = i;
                    object[] ValoresDet = { 
                                        FacturaAlmacenCobroDet.Id_Emp			
                                        ,FacturaAlmacenCobroDet.Id_Cd			
                                        ,FacturaAlmacenCobro.Id_Fac		
                                        ,FacturaAlmacenCobroDet.Id_FacDet		
                                        ,FacturaAlmacenCobroDet.Fac_Doc		
                                        ,FacturaAlmacenCobroDet.Fac_Fecha		
                                        ,FacturaAlmacenCobroDet.Id_Cte		
                                        ,FacturaAlmacenCobroDet.Fac_Importe	
                                   };

                    sqlcmd = CapaDatos.GenerarSqlCommand("spCapFacAlmacenCobroDetalle_Insertar", ref verificador, ParametrosDet, ValoresDet);
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

        public void ModificarFacturaAlmacenCobro(ref FacturaAlmacenCobro FacturaAlmacenCobro, string Conexion, ref int verificador)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {
                CapaDatos.StartTrans();

                string[] Parametros = { 
                                        "@Id_Emp"			
                                        ,"@Id_Cd"			
                                        ,"@Id_Fac"		
                                        ,"@Id_Reg"		
                                        ,"@Id_U"			
                                        ,"@Fac_Entrego"	
                                        ,"@Fac_Recibio"	
                                        ,"@Fac_Fecha"		
                                        ,"@Fac_FecEnvio"	
                                        ,"@Fac_FecRecibio"	
                                        ,"@Fac_Estatus"	
                                      };
                object[] Valores = { 
                                        FacturaAlmacenCobro.Id_Emp
                                        ,FacturaAlmacenCobro.Id_Cd
                                        ,FacturaAlmacenCobro.Id_Fac
                                        ,null //notaCredito.Id_Reg
                                        ,FacturaAlmacenCobro.Id_U
                                        ,FacturaAlmacenCobro.Fac_Entrego
                                        ,FacturaAlmacenCobro.Fac_Recibio
                                        ,FacturaAlmacenCobro.Fac_Fecha
                                        ,FacturaAlmacenCobro.Fac_FecEnvio
                                        ,FacturaAlmacenCobro.Fac_FecRecibio
                                        ,FacturaAlmacenCobro.Fac_Estatus
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand(
                    "spCapFacAlmacenCobro_Modificar", ref verificador, Parametros, Valores);
                //FacturaAlmacenCobro.Id_Fac = verificador; //folio nuevo



                // -----------------------------------------------------------------
                // Insertar detalle de nota de credito
                // -----------------------------------------------------------------
                string[] ParametrosDet = { 
                                        "@Id_Emp"			
                                        ,"@Id_Cd"			
                                        ,"@Id_Fac"		
                                        ,"@Id_FacDet"		
                                        ,"@Id_Reg"		
                                        ,"@Fac_Tipo"		
                                        ,"@Fac_Doc"		
                                        ,"@Fac_Fecha"		
                                        ,"@Id_Cte"		
                                        ,"@Fac_Importe"	
                                        ,"@Fac_EnviarA"	
                                      };
                int i = 1;
                foreach (FacturaAlmacenCobroDet FacturaAlmacenCobroDet in FacturaAlmacenCobro.ListaFacturaAlmacenCobroDet)
                {
                    FacturaAlmacenCobroDet.Id_FacDet = i;
                    object[] ValoresDet = { 
                                        FacturaAlmacenCobroDet.Id_Emp			
                                        ,FacturaAlmacenCobroDet.Id_Cd			
                                        ,FacturaAlmacenCobro.Id_Fac		
                                        ,FacturaAlmacenCobroDet.Id_FacDet		
                                        ,null
                                        ,FacturaAlmacenCobroDet.Fac_Tipo		
                                        ,FacturaAlmacenCobroDet.Fac_Doc		
                                        ,FacturaAlmacenCobroDet.Fac_Fecha		
                                        ,FacturaAlmacenCobroDet.Id_Cte		
                                        ,FacturaAlmacenCobroDet.Fac_Importe	
                                        ,FacturaAlmacenCobroDet.Fac_EnviarA
                                   };

                    sqlcmd = CapaDatos.GenerarSqlCommand("spCapFacAlmacenCobroDetalle_Insertar", ref verificador, ParametrosDet, ValoresDet);
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

        public void EliminarFacturaAlmacenCobro(FacturaAlmacenCobro FacturaAlmacenCobro, string Conexion, ref int verificador)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {
                CapaDatos.StartTrans();
                string[] Parametros = { 
	                                    "@Id_Emp"
                                        ,"@Id_Cd"
                                        ,"@Id_Fac"
                                      };
                object[] Valores = { 
                                       FacturaAlmacenCobro.Id_Emp
                                       ,FacturaAlmacenCobro.Id_Cd
                                       ,FacturaAlmacenCobro.Id_Fac
                                   };

                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd = CapaDatos.GenerarSqlCommand("spCapFacAlmacenCobro_Eliminar", ref verificador, Parametros, Valores);
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

        public void ModificarEstatusFacturaAlmacenCobro(FacturaAlmacenCobro FacturaAlmacenCobro, string Conexion, ref int verificador)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {
                CapaDatos.StartTrans();
                string[] Parametros = { 
	                                    "@Id_Emp"
                                        ,"@Id_Cd"
                                        ,"@Id_Fac"
                                        ,"@Fac_Estatus"
                                      };
                object[] Valores = { 
                                       FacturaAlmacenCobro.Id_Emp
                                       ,FacturaAlmacenCobro.Id_Cd
                                       ,FacturaAlmacenCobro.Id_Fac
                                       ,FacturaAlmacenCobro.Fac_Estatus
                                   };

                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd = CapaDatos.GenerarSqlCommand("spCapFacAlmacenCobro_ModificarEstatus", ref verificador, Parametros, Valores);
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


        public void ConsultarFacturaAlmacenCobro_Sugerido(ref FacturaAlmacenCobro facturaAlmacenCobro, string Conexion)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { 
                                          "@Id_Emp", 
                                          "@Id_Cd",
                                          "@Fecha",
                                          "@FechaFin",
                                          "@Db"
                                      };
                object[] Valores = { 
                                       facturaAlmacenCobro.Id_Emp,
                                       facturaAlmacenCobro.Id_Cd,
                                       facturaAlmacenCobro.Fac_Fecha,
                                       facturaAlmacenCobro.Fac_FechaFin,
                                       facturaAlmacenCobro.DbName
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spProSeguimientoAlmacenCobranza_Consultar", ref dr, Parametros, Valores);



                while (dr.Read())
                {
                    FacturaAlmacenCobroDet FacturaAlmacenCobroDet = new FacturaAlmacenCobroDet();
                    FacturaAlmacenCobroDet.Id_Emp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Emp")));
                    FacturaAlmacenCobroDet.Id_Cd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cd")));
                    FacturaAlmacenCobroDet.Id_Fac = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Fac")));
                    FacturaAlmacenCobroDet.Id_FacDet = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_FacDet")));
                    FacturaAlmacenCobroDet.Id_Reg = dr.IsDBNull(dr.GetOrdinal("Id_Reg")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Reg")));
                    FacturaAlmacenCobroDet.Fac_Tipo = dr.GetValue(dr.GetOrdinal("Fac_Tipo")).ToString();
                    FacturaAlmacenCobroDet.Fac_TipoStr = dr.GetValue(dr.GetOrdinal("Fac_TipoStr")).ToString();
                    FacturaAlmacenCobroDet.Fac_Doc = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Fac_Doc")));
                    FacturaAlmacenCobroDet.Fac_Fecha = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Fac_Fecha")));
                    FacturaAlmacenCobroDet.Id_Cte = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cte")));
                    FacturaAlmacenCobroDet.Cte_NomComercial = dr.GetValue(dr.GetOrdinal("Cte_NomComercial")).ToString();
                    FacturaAlmacenCobroDet.Fac_Importe = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Fac_Importe")));
                    FacturaAlmacenCobroDet.Cte_DiasRevision = dr.GetValue(dr.GetOrdinal("DiasRevicion")).ToString();
                    if (FacturaAlmacenCobroDet.Cte_DiasRevision.Length > 0)
                        FacturaAlmacenCobroDet.Cte_DiasRevision = FacturaAlmacenCobroDet.Cte_DiasRevision.Substring(0, FacturaAlmacenCobroDet.Cte_DiasRevision.Length - 1);

                    FacturaAlmacenCobroDet.Fac_Confirmado = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Confirmado")));
                    FacturaAlmacenCobroDet.Fac_Seleccionado = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Seleccionado")));
                    facturaAlmacenCobro.ListaFacturaAlmacenCobroDet.Add(FacturaAlmacenCobroDet);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Confirmar(FacturaAlmacenCobro lAlmcob, string Conexion)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {
                CapaDatos.StartTrans();
                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_AlmCob", "@Id_Fac" };             
                int verificador = 0;
                SqlCommand sqlcmd = new SqlCommand();
                
                //ordenCompra.Id_Ord = verificador; //identity de orden de compra 


                foreach (FacturaAlmacenCobroDet FacturaAlmacenCobroDet in lAlmcob.ListaFacturaAlmacenCobroDet)
                {
                    
                    object[] ValoresDet = { 
                                        FacturaAlmacenCobroDet.Id_Emp			
                                        ,FacturaAlmacenCobroDet.Id_Cd
			                            ,FacturaAlmacenCobroDet.Id_Fac
                                        ,FacturaAlmacenCobroDet.Fac_Doc		
                                       
                                   };

                    sqlcmd = CapaDatos.GenerarSqlCommand("spCapFacAlmacenCobro_Confirmar", ref verificador, Parametros, ValoresDet);
                  
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

        public void ValidaProcesoFacturaAlmacenCobro(Factura factura,  ref int Verificador, string Conexion)
        {
            try
            {
                CD_Datos cd_datos = new CD_Datos(Conexion);

                string[] Parametros ={"@Id_Emp",
                                      "@Id_Cd",
                                      "@Id_Fac"};
                object[] Valores = {factura.Id_Emp,
                                    factura.Id_Cd, 
                                    factura.Id_Fac
                                   };
                SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("spCapFacturaAlmCobro_ValidaProc", ref Verificador, Parametros, Valores);

                cd_datos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
    }
}
