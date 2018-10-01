using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using System.Data.SqlClient;
using System.Data;

using CapaEntidad;

namespace CapaDatos
{
    public class CD_CapFacturaRevisionCobro
    {
        public void ConsultaFacturaRevisionCobro_Buscar(FacturaRevisionCobro facturaRevCob, ref List<FacturaRevisionCobro> listaFacturaRevCob, string Conexion
            , int? Id_U
            , DateTime? Frc_Fecha_inicio
            , DateTime? Frc_Fecha_fin
            , string Frc_Estatus
            , int? Id_Frc_inicio
            , int? Id_Frc_fin
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
                                          ,"@Frc_Fecha_inicio"
                                          ,"@Frc_Fecha_fin"
                                          ,"@Frc_Estatus"
                                          ,"@Id_Frc_inicio"
                                          ,"@Id_Frc_fin"
                                          ,"@Id_Cte"
                                      };
                object[] Valores = { 
                                       facturaRevCob.Id_Emp
                                       ,facturaRevCob.Id_Cd
                                       ,Id_U
                                       ,Frc_Fecha_inicio
                                       ,Frc_Fecha_fin
                                       ,Frc_Estatus == string.Empty ? null : Frc_Estatus
                                       ,Id_Frc_inicio
                                       ,Id_Frc_fin
                                       ,Id_Cte
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapFacRevCob_Buscar", ref dr, Parametros, Valores);
                listaFacturaRevCob = new List<FacturaRevisionCobro>();
                while (dr.Read())
                {
                    facturaRevCob = new FacturaRevisionCobro();
                    facturaRevCob.Id_Emp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Emp")));
                    facturaRevCob.Id_Cd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cd")));
                    facturaRevCob.Id_Frc = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Frc")));
                    facturaRevCob.Frc_Fecha = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Frc_Fecha")));

                    facturaRevCob.Frc_Estatus = dr.GetValue(dr.GetOrdinal("Frc_Estatus")).ToString();
                    facturaRevCob.Frc_EstatusStr = dr.GetValue(dr.GetOrdinal("Frc_EstatusStr")).ToString();
                    facturaRevCob.Frc_Entrego = dr.GetValue(dr.GetOrdinal("Frc_Entrego")).ToString();
                    facturaRevCob.Frc_Recibio = dr.GetValue(dr.GetOrdinal("Frc_Recibio")).ToString();
                    listaFacturaRevCob.Add(facturaRevCob);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarFacturaRevisionCobro(ref FacturaRevisionCobro facturaRevisionCobro, string Conexion)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { 
                                          "@Id_Emp" 
                                          ,"@Id_Cd"
                                          ,"@Id_Frc"
                                      };
                object[] Valores = { 
                                       facturaRevisionCobro.Id_Emp
                                       ,facturaRevisionCobro.Id_Cd
                                       ,facturaRevisionCobro.Id_Frc
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapFacRevCob_Consultar", ref dr, Parametros, Valores);
                NotaCargo fac = new NotaCargo();
                if (dr.HasRows)
                {
                    dr.Read();
                    facturaRevisionCobro.Id_Emp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Emp")));
                    facturaRevisionCobro.Id_Cd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cd")));
                    facturaRevisionCobro.Cd_Nombre = dr.GetValue(dr.GetOrdinal("Cd_Nombre")).ToString();
                    facturaRevisionCobro.Id_Frc = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Frc")));
                    facturaRevisionCobro.Id_Reg = dr.IsDBNull(dr.GetOrdinal("Id_Reg")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Reg")));
                    facturaRevisionCobro.Id_U = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_U")));
                    facturaRevisionCobro.Frc_Entrego = dr.GetValue(dr.GetOrdinal("Frc_Entrego")).ToString();
                    facturaRevisionCobro.Frc_Recibio = dr.GetValue(dr.GetOrdinal("Frc_Recibio")).ToString();
                    facturaRevisionCobro.Frc_Fecha = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Frc_Fecha")));
                    if (dr.IsDBNull(dr.GetOrdinal("Frc_FecEnvio")))
                        facturaRevisionCobro.Frc_FecEnvio = null;
                    else
                        facturaRevisionCobro.Frc_FecEnvio = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Frc_FecEnvio")));
                    if (dr.IsDBNull(dr.GetOrdinal("Frc_FecRecibio")))
                        facturaRevisionCobro.Frc_FecRecibio = null;
                    else
                        facturaRevisionCobro.Frc_FecRecibio = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Frc_FecRecibio")));
                    facturaRevisionCobro.Frc_Estatus = dr.IsDBNull(dr.GetOrdinal("Frc_Estatus")) ? string.Empty : dr.GetValue(dr.GetOrdinal("Frc_Estatus")).ToString();
                }

                dr.Close();
                facturaRevisionCobro.ListaFacturaRevisionCobroDet = new List<FacturaRevisionCobroDet>();

                Parametros = new string[]{ 
                                          "@Id_Emp" 
                                          ,"@Id_Cd"
                                          ,"@Id_Frc"
                                          ,"@Db"
                                      };
                Valores = new object[]{ 
                                       facturaRevisionCobro.Id_Emp
                                       ,facturaRevisionCobro.Id_Cd
                                       ,facturaRevisionCobro.Id_Frc
                                       ,facturaRevisionCobro.DbName
                                   };
                sqlcmd = CapaDatos.GenerarSqlCommand("spCapFacRevCobDetalle_Consultar", ref dr, Parametros, Valores);
                while (dr.Read())
                {
                    FacturaRevisionCobroDet facturaRevisionCobroDet = new FacturaRevisionCobroDet();
                    facturaRevisionCobroDet.Id_Emp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Emp")));
                    facturaRevisionCobroDet.Id_Cd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cd")));
                    facturaRevisionCobroDet.Id_Frc = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Frc")));
                    facturaRevisionCobroDet.Id_FrcDet = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_FrcDet")));
                    facturaRevisionCobroDet.Id_Reg = dr.IsDBNull(dr.GetOrdinal("Id_Reg")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Reg")));
                    facturaRevisionCobroDet.Frc_Tipo = dr.GetValue(dr.GetOrdinal("Frc_Tipo")).ToString();
                    facturaRevisionCobroDet.Frc_TipoStr = dr.GetValue(dr.GetOrdinal("Frc_TipoStr")).ToString();
                    facturaRevisionCobroDet.Frc_Doc = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Frc_Doc")));
                    facturaRevisionCobroDet.Frc_Fecha = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Frc_Fecha")));
                    facturaRevisionCobroDet.Id_Cte = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cte")));
                    facturaRevisionCobroDet.Cte_NomComercial = dr.GetValue(dr.GetOrdinal("Cte_NomComercial")).ToString();
                    facturaRevisionCobroDet.Frc_Importe = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Frc_Importe")));
                    facturaRevisionCobroDet.Frc_EnviarA = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Frc_EnviarA")));
                    facturaRevisionCobroDet.Frc_EnviarAStr = dr.GetValue(dr.GetOrdinal("Frc_EnviarAStr")).ToString();
                    facturaRevisionCobroDet.Frc_Confirmado = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Confirmado")));
                    facturaRevisionCobroDet.Frc_Seleccionado = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Seleccionado")));
                    facturaRevisionCobroDet.Frc_Cheque = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Frc_Cheque")));
                    facturaRevisionCobroDet.Frc_Efectivo = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Frc_Efectivo")));
                    facturaRevisionCobro.ListaFacturaRevisionCobroDet.Add(facturaRevisionCobroDet);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertarFacturaRevisionCobro(ref FacturaRevisionCobro facturaRevisionCobro, string Conexion, ref int verificador)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {
                CapaDatos.StartTrans();

                string[] Parametros = { 
                                        "@Id_Emp"			
                                        ,"@Id_Cd"			
                                        ,"@Id_Frc"		
                                        ,"@Id_Reg"		
                                        ,"@Id_U"			
                                        ,"@Frc_Entrego"	
                                        ,"@Frc_Recibio"	
                                        ,"@Frc_Fecha"		
                                        ,"@Frc_FecEnvio"	
                                        ,"@Frc_FecRecibio"	
                                        ,"@Frc_Estatus"	
                                      };
                object[] Valores = { 
                                        facturaRevisionCobro.Id_Emp
                                        ,facturaRevisionCobro.Id_Cd
                                        ,facturaRevisionCobro.Id_Frc
                                        ,null //notaCredito.Id_Reg
                                        ,facturaRevisionCobro.Id_U
                                        ,facturaRevisionCobro.Frc_Entrego
                                        ,facturaRevisionCobro.Frc_Recibio
                                        ,facturaRevisionCobro.Frc_Fecha
                                        ,facturaRevisionCobro.Frc_FecEnvio
                                        ,facturaRevisionCobro.Frc_FecRecibio
                                        ,facturaRevisionCobro.Frc_Estatus
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapFacRevCob_Insertar", ref verificador, Parametros, Valores); facturaRevisionCobro.Id_Frc = verificador; //folio nuevo



                // -----------------------------------------------------------------
                // Insertar detalle de nota de credito
                // -----------------------------------------------------------------
                string[] ParametrosDet = { 
                                        "@Id_Emp"			
                                        ,"@Id_Cd"			
                                        ,"@Id_Frc"		
                                        ,"@Id_FrcDet"		
                                        ,"@Id_Reg"		
                                        ,"@Frc_Tipo"		
                                        ,"@Frc_Doc"		
                                        ,"@Frc_Fecha"		
                                        ,"@Id_Cte"		
                                        ,"@Frc_Importe"	
                                        ,"@Frc_EnviarA"	
                                      };
                int i = 1;
                foreach (FacturaRevisionCobroDet facturaRevisionCobroDet in facturaRevisionCobro.ListaFacturaRevisionCobroDet)
                {
                    facturaRevisionCobroDet.Id_FrcDet = i;
                    object[] ValoresDet = { 
                                        facturaRevisionCobroDet.Id_Emp			
                                        ,facturaRevisionCobroDet.Id_Cd			
                                        ,facturaRevisionCobro.Id_Frc		
                                        ,facturaRevisionCobroDet.Id_FrcDet		
                                        ,null
                                        ,facturaRevisionCobroDet.Frc_Tipo		
                                        ,facturaRevisionCobroDet.Frc_Doc		
                                        ,facturaRevisionCobroDet.Frc_Fecha		
                                        ,facturaRevisionCobroDet.Id_Cte		
                                        ,facturaRevisionCobroDet.Frc_Importe	
                                        ,facturaRevisionCobroDet.Frc_EnviarA
                                   };

                    sqlcmd = CapaDatos.GenerarSqlCommand("spCapFacRevCobDetalle_Insertar", ref verificador, ParametrosDet, ValoresDet);
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

        public void ModificarFacturaRevisionCobro(ref FacturaRevisionCobro facturaRevisionCobro, string Conexion, ref int verificador)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {
                CapaDatos.StartTrans();

                string[] Parametros = { 
                                        "@Id_Emp"			
                                        ,"@Id_Cd"			
                                        ,"@Id_Frc"		
                                        ,"@Id_Reg"		
                                        ,"@Id_U"			
                                        ,"@Frc_Entrego"	
                                        ,"@Frc_Recibio"	
                                        ,"@Frc_Fecha"		
                                        ,"@Frc_FecEnvio"	
                                        ,"@Frc_FecRecibio"	
                                        ,"@Frc_Estatus"	
                                      };
                object[] Valores = { 
                                        facturaRevisionCobro.Id_Emp
                                        ,facturaRevisionCobro.Id_Cd
                                        ,facturaRevisionCobro.Id_Frc
                                        ,null //notaCredito.Id_Reg
                                        ,facturaRevisionCobro.Id_U
                                        ,facturaRevisionCobro.Frc_Entrego
                                        ,facturaRevisionCobro.Frc_Recibio
                                        ,facturaRevisionCobro.Frc_Fecha
                                        ,facturaRevisionCobro.Frc_FecEnvio
                                        ,facturaRevisionCobro.Frc_FecRecibio
                                        ,facturaRevisionCobro.Frc_Estatus
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand(
                    "spCapFacRevCob_Modificar", ref verificador, Parametros, Valores);
                //facturaRevisionCobro.Id_Frc = verificador; //folio nuevo



                // -----------------------------------------------------------------
                // Insertar detalle de nota de credito
                // -----------------------------------------------------------------
                string[] ParametrosDet = { 
                                        "@Id_Emp"			
                                        ,"@Id_Cd"			
                                        ,"@Id_Frc"		
                                        ,"@Id_FrcDet"		
                                        ,"@Id_Reg"		
                                        ,"@Frc_Tipo"		
                                        ,"@Frc_Doc"		
                                        ,"@Frc_Fecha"		
                                        ,"@Id_Cte"		
                                        ,"@Frc_Importe"	
                                        ,"@Frc_EnviarA"	
                                      };
                int i = 1;
                foreach (FacturaRevisionCobroDet facturaRevisionCobroDet in facturaRevisionCobro.ListaFacturaRevisionCobroDet)
                {
                    facturaRevisionCobroDet.Id_FrcDet = i;
                    object[] ValoresDet = { 
                                        facturaRevisionCobroDet.Id_Emp			
                                        ,facturaRevisionCobroDet.Id_Cd			
                                        ,facturaRevisionCobro.Id_Frc		
                                        ,facturaRevisionCobroDet.Id_FrcDet		
                                        ,null
                                        ,facturaRevisionCobroDet.Frc_Tipo		
                                        ,facturaRevisionCobroDet.Frc_Doc		
                                        ,facturaRevisionCobroDet.Frc_Fecha		
                                        ,facturaRevisionCobroDet.Id_Cte		
                                        ,facturaRevisionCobroDet.Frc_Importe	
                                        ,facturaRevisionCobroDet.Frc_EnviarA
                                   };

                    sqlcmd = CapaDatos.GenerarSqlCommand("spCapFacRevCobDetalle_Insertar", ref verificador, ParametrosDet, ValoresDet);
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

        public void EliminarFacturaRevisionCobro(FacturaRevisionCobro facturaRevisionCobro, string Conexion, ref int verificador)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {
                CapaDatos.StartTrans();
                string[] Parametros = { 
	                                    "@Id_Emp"
                                        ,"@Id_Cd"
                                        ,"@Id_Frc"
                                      };
                object[] Valores = { 
                                       facturaRevisionCobro.Id_Emp
                                       ,facturaRevisionCobro.Id_Cd
                                       ,facturaRevisionCobro.Id_Frc
                                   };

                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd = CapaDatos.GenerarSqlCommand("spCapFacRevCob_Eliminar", ref verificador, Parametros, Valores);
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

        public void ModificarEstatusFacturaRevisionCobro(FacturaRevisionCobro facturaRevisionCobro, string Conexion, ref int verificador)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {
                CapaDatos.StartTrans();
                string[] Parametros = { 
	                                    "@Id_Emp"
                                        ,"@Id_Cd"
                                        ,"@Id_Frc"
                                        ,"@Frc_Estatus"
                                      };
                object[] Valores = { 
                                       facturaRevisionCobro.Id_Emp
                                       ,facturaRevisionCobro.Id_Cd
                                       ,facturaRevisionCobro.Id_Frc
                                       ,facturaRevisionCobro.Frc_Estatus
                                   };

                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd = CapaDatos.GenerarSqlCommand("spCapFacRevCob_ModificarEstatus", ref verificador, Parametros, Valores);
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


        public void ConsultarFacturaRevisionCobro_Sugerido(ref FacturaRevisionCobro facturaRevisionCobro, string Conexion)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { 
                                          "@Id_Emp", 
                                          "@Id_Cd",
                                          "@Fecha",
                                          "@Db"
                                      };
                object[] Valores = { 
                                       facturaRevisionCobro.Id_Emp,
                                       facturaRevisionCobro.Id_Cd,
                                       facturaRevisionCobro.Frc_Fecha,
                                       facturaRevisionCobro.DbName
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spProSeguimientoCobranzaRevision_Consultar", ref dr, Parametros, Valores);



                while (dr.Read())
                {
                    FacturaRevisionCobroDet facturaRevisionCobroDet = new FacturaRevisionCobroDet();
                    facturaRevisionCobroDet.Id_Emp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Emp")));
                    facturaRevisionCobroDet.Id_Cd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cd")));
                    facturaRevisionCobroDet.Id_Frc = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Frc")));
                    facturaRevisionCobroDet.Id_FrcDet = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_FrcDet")));
                    facturaRevisionCobroDet.Id_Reg = dr.IsDBNull(dr.GetOrdinal("Id_Reg")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Reg")));
                    facturaRevisionCobroDet.Frc_Tipo = dr.GetValue(dr.GetOrdinal("Frc_Tipo")).ToString();
                    facturaRevisionCobroDet.Frc_TipoStr = dr.GetValue(dr.GetOrdinal("Frc_TipoStr")).ToString();
                    facturaRevisionCobroDet.Frc_Doc = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Frc_Doc")));
                    facturaRevisionCobroDet.Frc_Fecha = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Frc_Fecha")));
                    facturaRevisionCobroDet.Id_Cte = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cte")));
                    facturaRevisionCobroDet.Cte_NomComercial = dr.GetValue(dr.GetOrdinal("Cte_NomComercial")).ToString();
                    facturaRevisionCobroDet.Frc_Importe = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Frc_Importe")));
                    facturaRevisionCobroDet.Frc_EnviarA = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Frc_EnviarA")));
                    facturaRevisionCobroDet.Frc_EnviarAStr = dr.GetValue(dr.GetOrdinal("Frc_EnviarAStr")).ToString();
                    facturaRevisionCobroDet.Frc_Confirmado = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Confirmado")));
                    facturaRevisionCobroDet.Frc_Seleccionado = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Seleccionado")));
                    facturaRevisionCobro.ListaFacturaRevisionCobroDet.Add(facturaRevisionCobroDet);
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
