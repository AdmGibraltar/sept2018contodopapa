using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using CapaEntidad;
using CapaModelo;

using System.Data;

namespace CapaDatos
{
    public class CD_CapValuacionProyecto
    {
        public void ConsultarUltimaValuacionProyectoCentroDist(ref int verificador, int Id_Cd, string Conexion)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);


                string[] Parametros = { "@Id_Cd" };
                object[] Valores = { Id_Cd };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand(
                    "spCapValuacionProyectosCantidadEnCd_Consultar", ref verificador, Parametros, Valores);

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaValuacionProyecto_Buscar(ValuacionProyecto valuacionProyecto, ref List<ValuacionProyecto> listaValuacionProyecto, string Conexion
            , int? Id_U
            , string Nombre
            , int? Id_Cte_inicio
            , int? Id_Cte_fin
            , DateTime? Vap_Fecha_inicio
            , DateTime? Vap_Fecha_fin)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { 
                                          "@Id_Emp" 
                                          ,"@Id_Cd"
                                          ,"@Id_U"
                                          ,"@Nombre"
                                          ,"@Id_Cte_inicio"
                                          ,"@Id_Cte_fin"
                                          ,"@Vap_Fecha_inicio"
                                          ,"@Vap_Fecha_fin" 
                                          ,"@Vap_Estatus"
                                      };
                object[] Valores = { 
                                       valuacionProyecto.Id_Emp
                                       ,valuacionProyecto.Id_Cd
                                       ,Id_U
                                       ,Nombre
                                       ,Id_Cte_inicio
                                       ,Id_Cte_fin
                                       ,Vap_Fecha_inicio
                                       ,Vap_Fecha_fin
                                       ,valuacionProyecto.Vap_Estatus
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapValProyecto_Buscar", ref dr, Parametros, Valores);
                listaValuacionProyecto = new List<ValuacionProyecto>();
                while (dr.Read())
                {
                    valuacionProyecto = new ValuacionProyecto();
                    valuacionProyecto.Id_Emp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Emp")));
                    valuacionProyecto.Id_Cd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cd")));
                    valuacionProyecto.Id_Vap = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Vap")));
                    valuacionProyecto.Id_Cte = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cte")));
                    valuacionProyecto.Vap_Fecha = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Vap_Fecha")));
                    valuacionProyecto.Cte_NomComercial = dr.IsDBNull(dr.GetOrdinal("Cte_NomComercial")) ? string.Empty : dr.GetValue(dr.GetOrdinal("Cte_NomComercial")).ToString();
                    valuacionProyecto.Vap_Nota = dr.IsDBNull(dr.GetOrdinal("Vap_Nota")) ? string.Empty : dr.GetValue(dr.GetOrdinal("Vap_Nota")).ToString();
                    valuacionProyecto.Vap_Estatus = dr.GetValue(dr.GetOrdinal("Vap_Estatus")).ToString();
                    valuacionProyecto.Vap_Usuario = dr.GetValue(dr.GetOrdinal("U_Nombre")).ToString();
                    listaValuacionProyecto.Add(valuacionProyecto);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarValuacionProyecto(ref ValuacionProyecto valuacionProyecto, string Conexion)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { 
                                          "@Id_Emp" 
                                          ,"@Id_Cd"
                                          ,"@Id_Vap"
                                      };
                object[] Valores = { 
                                       valuacionProyecto.Id_Emp
                                       ,valuacionProyecto.Id_Cd
                                       ,valuacionProyecto.Id_Vap
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapValProyecto_Consultar", ref dr, Parametros, Valores);
                NotaCargo fac = new NotaCargo();
                if (dr.HasRows)
                {
                    dr.Read();
                    valuacionProyecto.Id_Emp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Emp")));
                    valuacionProyecto.Id_Cd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cd")));
                    valuacionProyecto.Id_Vap = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Vap")));
                    valuacionProyecto.Vap_Fecha = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Vap_Fecha")));
                    valuacionProyecto.Id_U = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_U")));
                    valuacionProyecto.Id_Cte = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cte")));
                    valuacionProyecto.Cte_NomComercial = dr.GetValue(dr.GetOrdinal("Cte_NomComercial")).ToString();
                    valuacionProyecto.Vap_Nota = dr.IsDBNull(dr.GetOrdinal("Vap_Nota")) ? string.Empty : dr.GetValue(dr.GetOrdinal("Vap_Nota")).ToString();
                    valuacionProyecto.Vap_Estatus = dr.GetValue(dr.GetOrdinal("Vap_Estatus")).ToString();
                    valuacionProyecto.MotivoParaAutorizacion = dr.GetValue(dr.GetOrdinal("MotivoParaAutorizacion")).ToString();
                }

                dr.Close();
                valuacionProyecto.ListaProductosValuacionProyecto = new List<ValuacionProyectoDetalle>();
                sqlcmd = CapaDatos.GenerarSqlCommand("spCapValProyectoDetalle_Consultar", ref dr, Parametros, Valores);
                while (dr.Read())
                {
                    ValuacionProyectoDetalle valuacionProyectoDetalle = new ValuacionProyectoDetalle();
                    valuacionProyectoDetalle.Id_Emp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Emp")));
                    valuacionProyectoDetalle.Id_Cd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cd")));
                    valuacionProyectoDetalle.Id_Vap = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Vap")));
                    valuacionProyectoDetalle.Id_VapDet = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_VapDet")));
                    valuacionProyectoDetalle.Vap_Tipo = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Vap_Tipo")));
                    valuacionProyectoDetalle.Vap_TipoStr = dr.GetValue(dr.GetOrdinal("Vap_TipoStr")).ToString();

                    valuacionProyectoDetalle.Id_Prd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Prd")));
                    valuacionProyectoDetalle.Producto = new Producto();
                    valuacionProyectoDetalle.Producto.Id_Prd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Prd")));
                    valuacionProyectoDetalle.Producto.Prd_Descripcion = dr.GetValue(dr.GetOrdinal("Prd_Descripcion")).ToString();
                    valuacionProyectoDetalle.Producto.Prd_Presentacion = dr.GetValue(dr.GetOrdinal("Prd_Presentacion")).ToString();
                    if (dr.IsDBNull(dr.GetOrdinal("Prd_UniNs")))
                        valuacionProyectoDetalle.Producto.Prd_UniNs = null;
                    else
                        valuacionProyectoDetalle.Producto.Prd_UniNs = dr.GetValue(dr.GetOrdinal("Prd_UniNs")).ToString();

                    valuacionProyectoDetalle.Vap_Cantidad = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Vap_Cantidad")));
                    valuacionProyectoDetalle.Vap_Costo = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Vap_Costo")));
                    valuacionProyectoDetalle.Vap_Precio = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Vap_Precio")));
                    valuacionProyectoDetalle.Estatus = dr.GetValue(dr.GetOrdinal("Det_Estatus")).ToString();
                    valuacionProyectoDetalle.Vap_PrecioEspecial = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Vap_PrecioEspecial")));
                    valuacionProyecto.ListaProductosValuacionProyecto.Add(valuacionProyectoDetalle);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarValuacionProyecto_ReporteTotales(ref ValuacionProyecto valuacionProyecto, ref DataTable dt, string Conexion)
        {
            try
            {
                //SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { 
                                          "@Id_Emp" 
                                          ,"@Id_Cd"
                                          ,"@Id_Vap"
                                      };
                object[] Valores = { 
                                       valuacionProyecto.Id_Emp
                                       ,valuacionProyecto.Id_Cd
                                       ,valuacionProyecto.Id_Vap
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapValProyectoDetalle_ConsultarReporteValuacionProy_Totales", "tabla", ref dt, Parametros, Valores);

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertarValuacionProyecto(ref ValuacionProyecto valuacionProyecto, ValuacionParametros vp, string Conexion, ref int verificador, ValuacionParametrosActual vpactual)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {
                CapaDatos.StartTrans();

                string[] Parametros = { 
                                        "@Id_Emp"			
                                        ,"@Id_Cd"			
                                        ,"@Id_Vap"		
                                        ,"@Vap_Fecha"		
                                        ,"@Id_U"			
                                        ,"@Id_Cte"		
                                        ,"@Vap_Nota"		
                                        ,"@Vap_Estatus"
                                      };
                object[] Valores = { 
                                        valuacionProyecto.Id_Emp			
                                        ,valuacionProyecto.Id_Cd			
                                        ,valuacionProyecto.Id_Vap		
                                        ,valuacionProyecto.Vap_Fecha		
                                        ,valuacionProyecto.Id_U			
                                        ,valuacionProyecto.Id_Cte		
                                        ,valuacionProyecto.Vap_Nota		
                                        ,valuacionProyecto.Vap_Estatus	
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapValProyecto_Insertar", ref verificador, Parametros, Valores);
                valuacionProyecto.Id_Vap = verificador; //clave de val. proyecto

                // INSERTAR PARAMETROS
                Parametros = new string[] { 
                                            "@Id_Emp", 
                                            "@Id_Cd", 
                                            "@Id_Vap" , 
                                            "@Vap_Vigencia" ,  
                                            "@Vap_Participacion" , 
                                            "@Vap_Mano_Obra" , 
                                            "@Vap_Amortizacion" , 
                                            "@Vap_Numero_Entregas" , 
                                            "@Vap_Costo_Entregas" , 
                                            "@Vap_Comision_Factoraje" , 
                                            "@Vap_Comision_Anden" , 
                                            "@Vap_Gasto_Flete_Locales" , 
                                            "@Vap_IVA" , 
                                            "@Vap_Plazo_Pago_Cliente" , 
                                            "@Vap_Inventario_Key" , 
                                            "@Vap_Inventario_Consignacion" , 
                                            "@Vap_Inventario_Papel" , 
                                            "@Vap_Consignacion_Papel" , 
                                            "@Vap_Credito_Key" , 
                                            "@Vap_Credito_Papel" , 
                                            "@Vap_ISR" , 
                                            "@Vap_Ucs" , 
                                            "@Vap_Cetes" , 
                                            "@Vap_Adicional_Cetes" , 
                                            "@Vap_Costos_Fijos_No_Papel" , 
                                            "@Vap_Costos_Fijos_Papel" , 
                                            "@Vap_Gastos_Admin" , 
                                            "@Vap_Inversion_Activos" ,
                                            "@ptxtGastosVarAplTerr" ,
                                            "@ptxtFletesPagadosalCliente",
                                            "@txtCuentasPorCobrar" , 
                                            "@txtInventario" , 
                                            "@txtGastosServirCliente" , 
                                            "@txtVigencia" , 
                                            "@txtFleteLocales" , 
                                            "@txtIsr" , 
                                            "@txtCetes" , 
                                            "@txtFinanciamientoproveedores" , 
                                            "@txtInversionactivosfijos" , 
                                            "@txtCostodecapital" , 
                                            "@txtManoObra"  ,
                                            "@txtGastosVarAplTerr" ,
                                            "@txtFletesPagadosalCliente"
                                        };

                Valores = new object[] { 
                                            vp.Id_Emp,
                                            vp.Id_Cd,
                                            valuacionProyecto.Id_Vap,
                                            vp.Vap_Vigencia ,
                                            vp.Vap_Participacion ,
                                            vp.Vap_Mano_Obra ,
                                            vp.Vap_Amortizacion ,
                                            vp.Vap_Numero_Entregas ,
                                            vp.Vap_Costo_Entregas ,
                                            vp.Vap_Comision_Factoraje ,
                                            vp.Vap_Comision_Anden ,
                                            vp.Vap_Gasto_Flete_Locales ,
                                            vp.Vap_IVA ,
                                            vp.Vap_Plazo_Pago_Cliente ,
                                            vp.Vap_Inventario_Key ,
                                            vp.Vap_Inventario_Consignacion ,
                                            vp.Vap_Inventario_Papel ,
                                            vp.Vap_Consignacion_Papel ,
                                            vp.Vap_Credito_Key ,
                                            vp.Vap_Credito_Papel ,
                                            vp.Vap_ISR ,
                                            vp.Vap_Ucs ,
                                            vp.Vap_Cetes ,
                                            vp.Vap_Adicional_Cetes ,
                                            vp.Vap_Costos_Fijos_No_Papel ,
                                            vp.Vap_Costos_Fijos_Papel ,
                                            vp.Vap_Gastos_Admin ,
                                            vp.Vap_Inversion_Activos,
                                            vp.txtGastosVarAplTerr,
                                            vp.txtFletesPagadosalCliente,
                                            vpactual.txtCuentasPorCobrar,
                                            vpactual.txtInventario,
                                            vpactual.txtGastosServirCliente, 
                                            vpactual.txtVigencia, 
                                            vpactual.txtFleteLocales, 
                                            vpactual.txtIsr, 
                                            vpactual.txtCetes, 
                                            vpactual.txtFinanciamientoproveedores, 
                                            vpactual.txtInversionactivosfijos, 
                                            vpactual.txtCostodecapital, 
                                            vpactual.txtManoObra,
                                            vpactual.txtGastosVarAplTerr,
                                            vpactual.txtFletesPagadosalCliente
                                          };
                verificador = 0;
                sqlcmd = CapaDatos.GenerarSqlCommand("CapValProyectoParams_Insertar", ref verificador, Parametros, Valores);


                // -----------------------------------------------------------------
                // Insertar detalle 
                // -----------------------------------------------------------------
                string[] ParametrosDet = { 
                                        "@Id_Emp"
                                        ,"@Id_Cd"
                                        ,"@Id_Vap"
                                        ,"@Id_VapDet"
                                        ,"@Vap_Tipo"
                                        ,"@Id_Prd"
                                        ,"@Vap_Cantidad"
                                        ,"@Vap_Costo"
                                        ,"@Vap_Precio"	
                                        ,"@Vap_PrecioLista"
                                      };
                int i = 1;
                foreach (ValuacionProyectoDetalle ValuacionProyectoDetalle in valuacionProyecto.ListaProductosValuacionProyecto)
                {
                    ValuacionProyectoDetalle.Id_VapDet = i;
                    object[] ValoresDet = { 
                                        ValuacionProyectoDetalle.Id_Emp
                                        ,ValuacionProyectoDetalle.Id_Cd
                                        ,ValuacionProyectoDetalle.Id_Vap
                                        ,ValuacionProyectoDetalle.Id_VapDet
                                        ,ValuacionProyectoDetalle.Vap_Tipo
                                        ,ValuacionProyectoDetalle.Id_Prd
                                        ,ValuacionProyectoDetalle.Vap_Cantidad
                                        ,ValuacionProyectoDetalle.Vap_Costo
                                        ,ValuacionProyectoDetalle.Vap_Precio
                                        ,ValuacionProyectoDetalle.Vap_PrecioEspecial
                                   };
                    sqlcmd = CapaDatos.GenerarSqlCommand("spCapValProyectoDetalle_Insertar", ref verificador, ParametrosDet, ValoresDet);
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

        public void InsertarValuacionProyecto(ref ValuacionProyecto valuacionProyecto, ValuacionParametros vp, string Conexion, ref int verificador, int[] idOps, string conexionEF, int idRik)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {
                CapaDatos.StartTrans();

                string[] Parametros = { 
                                        "@Id_Emp"			
                                        ,"@Id_Cd"			
                                        ,"@Id_Vap"		
                                        ,"@Vap_Fecha"		
                                        ,"@Id_U"			
                                        ,"@Id_Cte"		
                                        ,"@Vap_Nota"		
                                        ,"@Vap_Estatus"
                                        ,"@Vap_UtilidadRemanente"
                                        ,"@Vap_ValorPresenteNeto"
                                        ,"@Vap_Estatus2"
                                        ,"@Id_Rik"
                                        ,"@Id_Ter"
                                      };
                object[] Valores = { 
                                        valuacionProyecto.Id_Emp			
                                        ,valuacionProyecto.Id_Cd			
                                        ,valuacionProyecto.Id_Vap		
                                        ,valuacionProyecto.Vap_Fecha		
                                        ,valuacionProyecto.Id_U			
                                        ,valuacionProyecto.Id_Cte		
                                        ,valuacionProyecto.Vap_Nota		
                                        ,valuacionProyecto.Vap_Estatus
                                        ,valuacionProyecto.Vap_UtilidadRemanente
                                        ,valuacionProyecto.Vap_ValorPresenteNeto
                                        ,valuacionProyecto.Estatus2
                                        ,valuacionProyecto.Id_Rik
                                        ,valuacionProyecto.Id_Ter
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapValProyecto_Insertar", ref verificador, Parametros, Valores);
                valuacionProyecto.Id_Vap = verificador; //clave de val. proyecto

                // INSERTAR PARAMETROS
                Parametros = new string[] { 
                                            "@Id_Emp", 
                                            "@Id_Cd", 
                                            "@Id_Vap" , 
                                            "@Vap_Vigencia" ,  
                                            "@Vap_Participacion" , 
                                            "@Vap_Mano_Obra" , 
                                            "@Vap_Amortizacion" , 
                                            "@Vap_Numero_Entregas" , 
                                            "@Vap_Costo_Entregas" , 
                                            "@Vap_Comision_Factoraje" , 
                                            "@Vap_Comision_Anden" , 
                                            "@Vap_Gasto_Flete_Locales" , 
                                            "@Vap_IVA" , 
                                            "@Vap_Plazo_Pago_Cliente" , 
                                            "@Vap_Inventario_Key" , 
                                            "@Vap_Inventario_Consignacion" , 
                                            "@Vap_Inventario_Papel" , 
                                            "@Vap_Consignacion_Papel" , 
                                            "@Vap_Credito_Key" , 
                                            "@Vap_Credito_Papel" , 
                                            "@Vap_ISR" , 
                                            "@Vap_Ucs" , 
                                            "@Vap_Cetes" , 
                                            "@Vap_Adicional_Cetes" , 
                                            "@Vap_Costos_Fijos_No_Papel" , 
                                            "@Vap_Costos_Fijos_Papel" , 
                                            "@Vap_Gastos_Admin" , 
                                            "@Vap_Inversion_Activos" 
                                        };

                Valores = new object[] { 
                                            vp.Id_Emp,
                                            vp.Id_Cd,
                                            valuacionProyecto.Id_Vap,
                                            vp.Vap_Vigencia ,
                                            vp.Vap_Participacion ,
                                            vp.Vap_Mano_Obra ,
                                            vp.Vap_Amortizacion ,
                                            vp.Vap_Numero_Entregas ,
                                            vp.Vap_Costo_Entregas ,
                                            vp.Vap_Comision_Factoraje ,
                                            vp.Vap_Comision_Anden ,
                                            vp.Vap_Gasto_Flete_Locales ,
                                            vp.Vap_IVA ,
                                            vp.Vap_Plazo_Pago_Cliente ,
                                            vp.Vap_Inventario_Key ,
                                            vp.Vap_Inventario_Consignacion ,
                                            vp.Vap_Inventario_Papel ,
                                            vp.Vap_Consignacion_Papel ,
                                            vp.Vap_Credito_Key ,
                                            vp.Vap_Credito_Papel ,
                                            vp.Vap_ISR ,
                                            vp.Vap_Ucs ,
                                            vp.Vap_Cetes ,
                                            vp.Vap_Adicional_Cetes ,
                                            vp.Vap_Costos_Fijos_No_Papel ,
                                            vp.Vap_Costos_Fijos_Papel ,
                                            vp.Vap_Gastos_Admin ,
                                            vp.Vap_Inversion_Activos
                                          };
                verificador = 0;
                sqlcmd = CapaDatos.GenerarSqlCommand("CapValProyectoParams_Insertar", ref verificador, Parametros, Valores);


                // -----------------------------------------------------------------
                // Insertar detalle 
                // -----------------------------------------------------------------
                string[] ParametrosDet = { 
                                        "@Id_Emp"
                                        ,"@Id_Cd"
                                        ,"@Id_Vap"
                                        ,"@Id_VapDet"
                                        ,"@Vap_Tipo"
                                        ,"@Id_Prd"
                                        ,"@Vap_Cantidad"
                                        ,"@Vap_Costo"
                                        ,"@Vap_Precio"	
                                        ,"@Vap_PrecioLista"
                                      };
                int i = 1;
                foreach (ValuacionProyectoDetalle ValuacionProyectoDetalle in valuacionProyecto.ListaProductosValuacionProyecto)
                {
                    ValuacionProyectoDetalle.Id_VapDet = i;
                    object[] ValoresDet = { 
                                        ValuacionProyectoDetalle.Id_Emp
                                        ,ValuacionProyectoDetalle.Id_Cd
                                        ,ValuacionProyectoDetalle.Id_Vap
                                        ,ValuacionProyectoDetalle.Id_VapDet
                                        ,ValuacionProyectoDetalle.Vap_Tipo
                                        ,ValuacionProyectoDetalle.Id_Prd
                                        ,ValuacionProyectoDetalle.Vap_Cantidad
                                        ,ValuacionProyectoDetalle.Vap_Costo
                                        ,ValuacionProyectoDetalle.Vap_Precio
                                        ,ValuacionProyectoDetalle.Vap_PrecioEspecial
                                   };
                    sqlcmd = CapaDatos.GenerarSqlCommand("spCapValProyectoDetalle_Insertar", ref verificador, ParametrosDet, ValoresDet);
                    i += 1;
                }

                sianwebmty_gEntities ctx = new sianwebmty_gEntities(conexionEF);
                CD_CrmValuacionOportunidades cdCrmValuacionOportunidades = new CD_CrmValuacionOportunidades();
                cdCrmValuacionOportunidades.Insertar(valuacionProyecto.Id_Emp, valuacionProyecto.Id_Cd, valuacionProyecto.Id_Cte, valuacionProyecto.Id_Vap, idRik, idOps, ctx);

                CapaDatos.CommitTrans();
                ctx.SaveChanges();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                CapaDatos.RollBackTrans();
                throw ex;
            }
        }

        public void InsertarValuacionProyecto(ref ValuacionProyecto valuacionProyecto, ValuacionParametros vp, string Conexion, ref int verificador, int[] idOps, ICD_Contexto icdCtx, int idRik)
        {
            ICD_Contexto<sianwebmty_gEntities> ctx = ((ICD_Contexto<sianwebmty_gEntities>)icdCtx);
            IDbTransaction transaction = ctx.Contexto.Database.CurrentTransaction.UnderlyingTransaction;
            SqlCommand sqlcmd = null;

            try
            {

                string[] Parametros = { 
                                        "@Id_Emp"			
                                        ,"@Id_Cd"			
                                        ,"@Id_Vap"		
                                        ,"@Vap_Fecha"		
                                        ,"@Id_U"			
                                        ,"@Id_Cte"		
                                        ,"@Vap_Nota"		
                                        ,"@Vap_Estatus"
                                        ,"@Vap_UtilidadRemanente"
                                        ,"@Vap_ValorPresenteNeto"
                                        ,"@Vap_Estatus2"
                                        ,"@Id_Rik"
                                        ,"@Id_Ter"
                                        ,"@motivoParaAutorizacion"
                                      };
                object[] Valores = { 
                                        valuacionProyecto.Id_Emp			
                                        ,valuacionProyecto.Id_Cd			
                                        ,valuacionProyecto.Id_Vap		
                                        ,valuacionProyecto.Vap_Fecha		
                                        ,valuacionProyecto.Id_U			
                                        ,valuacionProyecto.Id_Cte		
                                        ,valuacionProyecto.Vap_Nota		
                                        ,valuacionProyecto.Vap_Estatus
                                        ,valuacionProyecto.Vap_UtilidadRemanente
                                        ,valuacionProyecto.Vap_ValorPresenteNeto
                                        ,valuacionProyecto.Estatus2
                                        ,valuacionProyecto.Id_Rik
                                        ,valuacionProyecto.Id_Ter
                                        ,valuacionProyecto.MotivoParaAutorizacion
                                   };

                sqlcmd = transaction.Connection.CreateCommand() as SqlCommand;
                sqlcmd.Transaction = (System.Data.SqlClient.SqlTransaction)transaction;
                sqlcmd = CD_Datos.GenerarSqlCommand("spCapValProyecto_Insertar", ref verificador, Parametros, Valores, transaction.Connection, sqlcmd);
                valuacionProyecto.Id_Vap = verificador; //clave de val. proyecto
                sqlcmd.Dispose();

                // INSERTAR PARAMETROS
                Parametros = new string[] { 
                                            "@Id_Emp", 
                                            "@Id_Cd", 
                                            "@Id_Vap" , 
                                            "@Vap_Vigencia" ,  
                                            "@Vap_Participacion" , 
                                            "@Vap_Mano_Obra" , 
                                            "@Vap_Amortizacion" , 
                                            "@Vap_Numero_Entregas" , 
                                            "@Vap_Costo_Entregas" , 
                                            "@Vap_Comision_Factoraje" , 
                                            "@Vap_Comision_Anden" , 
                                            "@Vap_Gasto_Flete_Locales" , 
                                            "@Vap_IVA" , 
                                            "@Vap_Plazo_Pago_Cliente" , 
                                            "@Vap_Inventario_Key" , 
                                            "@Vap_Inventario_Consignacion" , 
                                            "@Vap_Inventario_Papel" , 
                                            "@Vap_Consignacion_Papel" , 
                                            "@Vap_Credito_Key" , 
                                            "@Vap_Credito_Papel" , 
                                            "@Vap_ISR" , 
                                            "@Vap_Ucs" , 
                                            "@Vap_Cetes" , 
                                            "@Vap_Adicional_Cetes" , 
                                            "@Vap_Costos_Fijos_No_Papel" , 
                                            "@Vap_Costos_Fijos_Papel" , 
                                            "@Vap_Gastos_Admin" , 
                                            "@Vap_Inversion_Activos" 
                                        };

                Valores = new object[] { 
                                            vp.Id_Emp,
                                            vp.Id_Cd,
                                            valuacionProyecto.Id_Vap,
                                            vp.Vap_Vigencia ,
                                            vp.Vap_Participacion ,
                                            vp.Vap_Mano_Obra ,
                                            vp.Vap_Amortizacion ,
                                            vp.Vap_Numero_Entregas ,
                                            vp.Vap_Costo_Entregas ,
                                            vp.Vap_Comision_Factoraje ,
                                            vp.Vap_Comision_Anden ,
                                            vp.Vap_Gasto_Flete_Locales ,
                                            vp.Vap_IVA ,
                                            vp.Vap_Plazo_Pago_Cliente ,
                                            vp.Vap_Inventario_Key ,
                                            vp.Vap_Inventario_Consignacion ,
                                            vp.Vap_Inventario_Papel ,
                                            vp.Vap_Consignacion_Papel ,
                                            vp.Vap_Credito_Key ,
                                            vp.Vap_Credito_Papel ,
                                            vp.Vap_ISR ,
                                            vp.Vap_Ucs ,
                                            vp.Vap_Cetes ,
                                            vp.Vap_Adicional_Cetes ,
                                            vp.Vap_Costos_Fijos_No_Papel ,
                                            vp.Vap_Costos_Fijos_Papel ,
                                            vp.Vap_Gastos_Admin ,
                                            vp.Vap_Inversion_Activos
                                          };
                verificador = 0;
                sqlcmd = transaction.Connection.CreateCommand() as SqlCommand;
                sqlcmd.Transaction = (System.Data.SqlClient.SqlTransaction)transaction;
                sqlcmd = CD_Datos.GenerarSqlCommand("CapValProyectoParams_Insertar", ref verificador, Parametros, Valores, transaction.Connection, sqlcmd);
                sqlcmd.Dispose();

                // -----------------------------------------------------------------
                // Insertar detalle 
                // -----------------------------------------------------------------
                string[] ParametrosDet = { 
                                        "@Id_Emp"
                                        ,"@Id_Cd"
                                        ,"@Id_Vap"
                                        ,"@Id_VapDet"
                                        ,"@Vap_Tipo"
                                        ,"@Id_Prd"
                                        ,"@Vap_Cantidad"
                                        ,"@Vap_Costo"
                                        ,"@Vap_Precio"	
                                        ,"@Vap_PrecioLista"
                                      };
                int i = 1;
                foreach (ValuacionProyectoDetalle ValuacionProyectoDetalle in valuacionProyecto.ListaProductosValuacionProyecto)
                {
                    ValuacionProyectoDetalle.Id_VapDet = i;
                    object[] ValoresDet = { 
                                        ValuacionProyectoDetalle.Id_Emp
                                        ,ValuacionProyectoDetalle.Id_Cd
                                        ,ValuacionProyectoDetalle.Id_Vap
                                        ,ValuacionProyectoDetalle.Id_VapDet
                                        ,ValuacionProyectoDetalle.Vap_Tipo
                                        ,ValuacionProyectoDetalle.Id_Prd
                                        ,ValuacionProyectoDetalle.Vap_Cantidad
                                        ,ValuacionProyectoDetalle.Vap_Costo
                                        ,ValuacionProyectoDetalle.Vap_Precio
                                        ,ValuacionProyectoDetalle.Vap_PrecioEspecial
                                   };
                    sqlcmd = transaction.Connection.CreateCommand() as SqlCommand;
                    sqlcmd.Transaction = (System.Data.SqlClient.SqlTransaction)transaction;
                    sqlcmd = CD_Datos.GenerarSqlCommand("spCapValProyectoDetalle_Insertar", ref verificador, ParametrosDet, ValoresDet, transaction.Connection, sqlcmd);
                    sqlcmd.Dispose();
                    i += 1;
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarValuacionProyecto(ref ValuacionProyecto valuacionProyecto, ValuacionParametros vp, string Conexion, ref int verificador, ValuacionParametrosActual vpactual)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {
                CapaDatos.StartTrans();

                string[] Parametros = { 
                                        "@Id_Emp"			
                                        ,"@Id_Cd"			
                                        ,"@Id_Vap"		
                                        ,"@Vap_Fecha"		
                                        ,"@Id_U"			
                                        ,"@Id_Cte"		
                                        ,"@Vap_Nota"		
                                        ,"@Vap_Estatus"
                                        ,"@txtCuentasPorCobrar"
                                        ,"@txtInventario"
                                        ,"@txtGastosServirCliente"
                                        ,"@txtVigencia"
                                        ,"@txtFleteLocales"
                                        ,"@txtIsr"
                                        ,"@txtCetes"
                                        ,"@txtFinanciamientoproveedores"
                                        ,"@txtInversionactivosfijos"
                                        ,"@txtCostodecapital"
                                        ,"@txtManoObra"
                                        ,"@ptxtGastosVarAplTerr"
                                        ,"@ptxtFletesPagadosalCliente"

                                      };
                object[] Valores = { 
                                        valuacionProyecto.Id_Emp			
                                        ,valuacionProyecto.Id_Cd			
                                        ,valuacionProyecto.Id_Vap		
                                        ,valuacionProyecto.Vap_Fecha		
                                        ,valuacionProyecto.Id_U			
                                        ,valuacionProyecto.Id_Cte		
                                        ,valuacionProyecto.Vap_Nota		
                                        ,valuacionProyecto.Vap_Estatus	
                                        ,vpactual.txtCuentasPorCobrar
                                        ,vpactual.txtInventario
                                        ,vpactual.txtGastosServirCliente
                                        ,vpactual.txtVigencia
                                        ,vpactual.txtFleteLocales
                                        ,vpactual.txtIsr
                                        ,vpactual.txtCetes
                                        ,vpactual.txtFinanciamientoproveedores
                                        ,vpactual.txtInversionactivosfijos
                                        ,vpactual.txtCostodecapital
                                        ,vpactual.txtManoObra
                                        ,vpactual.txtGastosVarAplTerr
                                        ,vpactual.txtFletesPagadosalCliente

                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapValProyecto_Modificar", ref verificador, Parametros, Valores);
                //valuacionProyecto.Id_Vap = verificador; //clave de val. proyecto

                // INSERTAR PARAMETROS
                Parametros = new string[] { 
                                            "@Id_Emp", 
                                            "@Id_Cd", 
                                            "@Id_Vap" , 
                                            "@Vap_Vigencia" ,  
                                            "@Vap_Participacion" , 
                                            "@Vap_Mano_Obra" , 
                                            "@Vap_Amortizacion" , 
                                            "@Vap_Numero_Entregas" , 
                                            "@Vap_Costo_Entregas" , 
                                            "@Vap_Comision_Factoraje" , 
                                            "@Vap_Comision_Anden" , 
                                            "@Vap_Gasto_Flete_Locales" , 
                                            "@Vap_IVA" , 
                                            "@Vap_Plazo_Pago_Cliente" , 
                                            "@Vap_Inventario_Key" , 
                                            "@Vap_Inventario_Consignacion" , 
                                            "@Vap_Inventario_Papel" , 
                                            "@Vap_Consignacion_Papel" , 
                                            "@Vap_Credito_Key" , 
                                            "@Vap_Credito_Papel" , 
                                            "@Vap_ISR" , 
                                            "@Vap_Ucs" , 
                                            "@Vap_Cetes" , 
                                            "@Vap_Adicional_Cetes" , 
                                            "@Vap_Costos_Fijos_No_Papel" , 
                                            "@Vap_Costos_Fijos_Papel" , 
                                            "@Vap_Gastos_Admin" , 
                                            "@Vap_Inversion_Activos",
                                            "@txtGastosVarAplTerr" ,
                                            "@txtFletesPagadosalCliente"
                                        };

                Valores = new object[] { 
                                            vp.Id_Emp,
                                            vp.Id_Cd,
                                            valuacionProyecto.Id_Vap,
                                            vp.Vap_Vigencia ,
                                            vp.Vap_Participacion ,
                                            vp.Vap_Mano_Obra ,
                                            vp.Vap_Amortizacion ,
                                            vp.Vap_Numero_Entregas ,
                                            vp.Vap_Costo_Entregas ,
                                            vp.Vap_Comision_Factoraje ,
                                            vp.Vap_Comision_Anden ,
                                            vp.Vap_Gasto_Flete_Locales ,
                                            vp.Vap_IVA ,
                                            vp.Vap_Plazo_Pago_Cliente ,
                                            vp.Vap_Inventario_Key ,
                                            vp.Vap_Inventario_Consignacion ,
                                            vp.Vap_Inventario_Papel ,
                                            vp.Vap_Consignacion_Papel ,
                                            vp.Vap_Credito_Key ,
                                            vp.Vap_Credito_Papel ,
                                            vp.Vap_ISR ,
                                            vp.Vap_Ucs ,
                                            vp.Vap_Cetes ,
                                            vp.Vap_Adicional_Cetes ,
                                            vp.Vap_Costos_Fijos_No_Papel ,
                                            vp.Vap_Costos_Fijos_Papel ,
                                            vp.Vap_Gastos_Admin ,
                                            vp.Vap_Inversion_Activos,
                                            vp.txtGastosVarAplTerr,
                                            vp.txtFletesPagadosalCliente
                                          };
                verificador = 0;
                sqlcmd = CapaDatos.GenerarSqlCommand("CapValProyectoParams_Modificar", ref verificador, Parametros, Valores);

                // -----------------------------------------------------------------
                // Insertar detalle 
                // -----------------------------------------------------------------
                string[] ParametrosDet = { 
                                        "@Id_Emp"
                                        ,"@Id_Cd"
                                        ,"@Id_Vap"
                                        ,"@Id_VapDet"
                                        ,"@Vap_Tipo"
                                        ,"@Id_Prd"
                                        ,"@Vap_Cantidad"
                                        ,"@Vap_Costo"
                                        ,"@Vap_Precio"	
                                        ,"@Vap_PrecioLista"
                                      };
                int i = 1;
                foreach (ValuacionProyectoDetalle ValuacionProyectoDetalle in valuacionProyecto.ListaProductosValuacionProyecto)
                {
                    ValuacionProyectoDetalle.Id_VapDet = i;
                    object[] ValoresDet = { 
                                        ValuacionProyectoDetalle.Id_Emp
                                        ,ValuacionProyectoDetalle.Id_Cd
                                        ,ValuacionProyectoDetalle.Id_Vap
                                        ,ValuacionProyectoDetalle.Id_VapDet
                                        ,ValuacionProyectoDetalle.Vap_Tipo
                                        ,ValuacionProyectoDetalle.Id_Prd
                                        ,ValuacionProyectoDetalle.Vap_Cantidad
                                        ,ValuacionProyectoDetalle.Vap_Costo
                                        ,ValuacionProyectoDetalle.Vap_Precio
                                        ,ValuacionProyectoDetalle.Vap_PrecioEspecial//Vap_PrecioEspecial
                                   };
                    sqlcmd = CapaDatos.GenerarSqlCommand("spCapValProyectoDetalle_Insertar", ref verificador, ParametrosDet, ValoresDet);
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

        public void ModificarValuacionProyecto(ref ValuacionProyecto valuacionProyecto, ValuacionParametros vp, string Conexion, ref int verificador, int[] idOps, string conexionEF, int idRik)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {
                CapaDatos.StartTrans();

                string[] Parametros = { 
                                        "@Id_Emp"			
                                        ,"@Id_Cd"			
                                        ,"@Id_Vap"		
                                        ,"@Vap_Fecha"		
                                        ,"@Id_U"			
                                        ,"@Id_Cte"		
                                        ,"@Vap_Nota"		
                                        ,"@Vap_Estatus"
                                        ,"@Vap_UtilidadRemanente"
                                        ,"@Vap_ValorPresenteNeto"
                                        ,"@Vap_Estatus2"
                                        ,"@Id_Ter"
                                      };
                object[] Valores = { 
                                        valuacionProyecto.Id_Emp			
                                        ,valuacionProyecto.Id_Cd			
                                        ,valuacionProyecto.Id_Vap		
                                        ,valuacionProyecto.Vap_Fecha		
                                        ,valuacionProyecto.Id_U			
                                        ,valuacionProyecto.Id_Cte		
                                        ,valuacionProyecto.Vap_Nota		
                                        ,valuacionProyecto.Vap_Estatus
                                        ,valuacionProyecto.Vap_UtilidadRemanente
                                        ,valuacionProyecto.Vap_ValorPresenteNeto
                                        ,valuacionProyecto.Estatus2
                                        ,valuacionProyecto.Id_Ter
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapValProyecto_Modificar", ref verificador, Parametros, Valores);
                //valuacionProyecto.Id_Vap = verificador; //clave de val. proyecto

                // INSERTAR PARAMETROS
                Parametros = new string[] { 
                                            "@Id_Emp", 
                                            "@Id_Cd", 
                                            "@Id_Vap" , 
                                            "@Vap_Vigencia" ,  
                                            "@Vap_Participacion" , 
                                            "@Vap_Mano_Obra" , 
                                            "@Vap_Amortizacion" , 
                                            "@Vap_Numero_Entregas" , 
                                            "@Vap_Costo_Entregas" , 
                                            "@Vap_Comision_Factoraje" , 
                                            "@Vap_Comision_Anden" , 
                                            "@Vap_Gasto_Flete_Locales" , 
                                            "@Vap_IVA" , 
                                            "@Vap_Plazo_Pago_Cliente" , 
                                            "@Vap_Inventario_Key" , 
                                            "@Vap_Inventario_Consignacion" , 
                                            "@Vap_Inventario_Papel" , 
                                            "@Vap_Consignacion_Papel" , 
                                            "@Vap_Credito_Key" , 
                                            "@Vap_Credito_Papel" , 
                                            "@Vap_ISR" , 
                                            "@Vap_Ucs" , 
                                            "@Vap_Cetes" , 
                                            "@Vap_Adicional_Cetes" , 
                                            "@Vap_Costos_Fijos_No_Papel" , 
                                            "@Vap_Costos_Fijos_Papel" , 
                                            "@Vap_Gastos_Admin" , 
                                            "@Vap_Inversion_Activos" 
                                        };

                Valores = new object[] { 
                                            vp.Id_Emp,
                                            vp.Id_Cd,
                                            valuacionProyecto.Id_Vap,
                                            vp.Vap_Vigencia ,
                                            vp.Vap_Participacion ,
                                            vp.Vap_Mano_Obra ,
                                            vp.Vap_Amortizacion ,
                                            vp.Vap_Numero_Entregas ,
                                            vp.Vap_Costo_Entregas ,
                                            vp.Vap_Comision_Factoraje ,
                                            vp.Vap_Comision_Anden ,
                                            vp.Vap_Gasto_Flete_Locales ,
                                            vp.Vap_IVA ,
                                            vp.Vap_Plazo_Pago_Cliente ,
                                            vp.Vap_Inventario_Key ,
                                            vp.Vap_Inventario_Consignacion ,
                                            vp.Vap_Inventario_Papel ,
                                            vp.Vap_Consignacion_Papel ,
                                            vp.Vap_Credito_Key ,
                                            vp.Vap_Credito_Papel ,
                                            vp.Vap_ISR ,
                                            vp.Vap_Ucs ,
                                            vp.Vap_Cetes ,
                                            vp.Vap_Adicional_Cetes ,
                                            vp.Vap_Costos_Fijos_No_Papel ,
                                            vp.Vap_Costos_Fijos_Papel ,
                                            vp.Vap_Gastos_Admin ,
                                            vp.Vap_Inversion_Activos
                                          };
                verificador = 0;
                sqlcmd = CapaDatos.GenerarSqlCommand("CapValProyectoParams_Modificar", ref verificador, Parametros, Valores);

                // -----------------------------------------------------------------
                // Insertar detalle 
                // -----------------------------------------------------------------
                string[] ParametrosDet = { 
                                        "@Id_Emp"
                                        ,"@Id_Cd"
                                        ,"@Id_Vap"
                                        ,"@Id_VapDet"
                                        ,"@Vap_Tipo"
                                        ,"@Id_Prd"
                                        ,"@Vap_Cantidad"
                                        ,"@Vap_Costo"
                                        ,"@Vap_Precio"	
                                        ,"@Vap_PrecioLista"
                                      };
                int i = 1;
                foreach (ValuacionProyectoDetalle ValuacionProyectoDetalle in valuacionProyecto.ListaProductosValuacionProyecto)
                {
                    ValuacionProyectoDetalle.Id_VapDet = i;
                    object[] ValoresDet = { 
                                        ValuacionProyectoDetalle.Id_Emp
                                        ,ValuacionProyectoDetalle.Id_Cd
                                        ,ValuacionProyectoDetalle.Id_Vap
                                        ,ValuacionProyectoDetalle.Id_VapDet
                                        ,ValuacionProyectoDetalle.Vap_Tipo
                                        ,ValuacionProyectoDetalle.Id_Prd
                                        ,ValuacionProyectoDetalle.Vap_Cantidad
                                        ,ValuacionProyectoDetalle.Vap_Costo
                                        ,ValuacionProyectoDetalle.Vap_Precio
                                        ,ValuacionProyectoDetalle.Vap_PrecioEspecial//Vap_PrecioEspecial
                                   };
                    sqlcmd = CapaDatos.GenerarSqlCommand("spCapValProyectoDetalle_Insertar", ref verificador, ParametrosDet, ValoresDet);
                    i += 1;
                }

                sianwebmty_gEntities ctx = new sianwebmty_gEntities(conexionEF);
                CD_CrmValuacionOportunidades cdCrmValuacionOportunidades = new CD_CrmValuacionOportunidades();
                cdCrmValuacionOportunidades.Eliminar(valuacionProyecto.Id_Emp, valuacionProyecto.Id_Cd, valuacionProyecto.Id_Cte, valuacionProyecto.Id_Vap, idRik, ctx);
                cdCrmValuacionOportunidades.Insertar(valuacionProyecto.Id_Emp, valuacionProyecto.Id_Cd, valuacionProyecto.Id_Cte, valuacionProyecto.Id_Vap, idRik, idOps, ctx);

                CapaDatos.CommitTrans();
                ctx.SaveChanges();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                CapaDatos.RollBackTrans();
                throw ex;
            }
        }

        public void ModificarValuacionProyecto(ref ValuacionProyecto valuacionProyecto, ValuacionParametros vp, string Conexion, ref int verificador, int[] idOps, string conexionEF, int idRik, ICD_Contexto icdCtx)
        {
            ICD_Contexto<sianwebmty_gEntities> ctx = ((ICD_Contexto<sianwebmty_gEntities>)icdCtx);
            IDbTransaction transaction = ctx.Contexto.Database.CurrentTransaction.UnderlyingTransaction;
            SqlCommand sqlcmd = null;

            try
            {

                string[] Parametros = { 
                                        "@Id_Emp"			
                                        ,"@Id_Cd"			
                                        ,"@Id_Vap"		
                                        ,"@Vap_Fecha"		
                                        ,"@Id_U"			
                                        ,"@Id_Cte"		
                                        ,"@Vap_Nota"		
                                        ,"@Vap_Estatus"
                                        ,"@Vap_UtilidadRemanente"
                                        ,"@Vap_ValorPresenteNeto"
                                        ,"@Vap_Estatus2"
                                        ,"@motivoParaAutorizacion"
                                      };
                object[] Valores = { 
                                        valuacionProyecto.Id_Emp			
                                        ,valuacionProyecto.Id_Cd			
                                        ,valuacionProyecto.Id_Vap		
                                        ,valuacionProyecto.Vap_Fecha		
                                        ,valuacionProyecto.Id_U			
                                        ,valuacionProyecto.Id_Cte		
                                        ,valuacionProyecto.Vap_Nota		
                                        ,valuacionProyecto.Vap_Estatus
                                        ,valuacionProyecto.Vap_UtilidadRemanente
                                        ,valuacionProyecto.Vap_ValorPresenteNeto
                                        ,valuacionProyecto.Estatus2
                                        ,valuacionProyecto.MotivoParaAutorizacion
                                   };

                sqlcmd = transaction.Connection.CreateCommand() as SqlCommand;
                sqlcmd.Transaction = (System.Data.SqlClient.SqlTransaction)transaction;
                sqlcmd = CD_Datos.GenerarSqlCommand("spCapValProyecto_Modificar", ref verificador, Parametros, Valores, transaction.Connection, sqlcmd);
                sqlcmd.Dispose();
                //valuacionProyecto.Id_Vap = verificador; //clave de val. proyecto

                // INSERTAR PARAMETROS
                Parametros = new string[] { 
                                            "@Id_Emp", 
                                            "@Id_Cd", 
                                            "@Id_Vap" , 
                                            "@Vap_Vigencia" ,  
                                            "@Vap_Participacion" , 
                                            "@Vap_Mano_Obra" , 
                                            "@Vap_Amortizacion" , 
                                            "@Vap_Numero_Entregas" , 
                                            "@Vap_Costo_Entregas" , 
                                            "@Vap_Comision_Factoraje" , 
                                            "@Vap_Comision_Anden" , 
                                            "@Vap_Gasto_Flete_Locales" , 
                                            "@Vap_IVA" , 
                                            "@Vap_Plazo_Pago_Cliente" , 
                                            "@Vap_Inventario_Key" , 
                                            "@Vap_Inventario_Consignacion" , 
                                            "@Vap_Inventario_Papel" , 
                                            "@Vap_Consignacion_Papel" , 
                                            "@Vap_Credito_Key" , 
                                            "@Vap_Credito_Papel" , 
                                            "@Vap_ISR" , 
                                            "@Vap_Ucs" , 
                                            "@Vap_Cetes" , 
                                            "@Vap_Adicional_Cetes" , 
                                            "@Vap_Costos_Fijos_No_Papel" , 
                                            "@Vap_Costos_Fijos_Papel" , 
                                            "@Vap_Gastos_Admin" , 
                                            "@Vap_Inversion_Activos" 
                                        };

                Valores = new object[] { 
                                            vp.Id_Emp,
                                            vp.Id_Cd,
                                            valuacionProyecto.Id_Vap,
                                            vp.Vap_Vigencia ,
                                            vp.Vap_Participacion ,
                                            vp.Vap_Mano_Obra ,
                                            vp.Vap_Amortizacion ,
                                            vp.Vap_Numero_Entregas ,
                                            vp.Vap_Costo_Entregas ,
                                            vp.Vap_Comision_Factoraje ,
                                            vp.Vap_Comision_Anden ,
                                            vp.Vap_Gasto_Flete_Locales ,
                                            vp.Vap_IVA ,
                                            vp.Vap_Plazo_Pago_Cliente ,
                                            vp.Vap_Inventario_Key ,
                                            vp.Vap_Inventario_Consignacion ,
                                            vp.Vap_Inventario_Papel ,
                                            vp.Vap_Consignacion_Papel ,
                                            vp.Vap_Credito_Key ,
                                            vp.Vap_Credito_Papel ,
                                            vp.Vap_ISR ,
                                            vp.Vap_Ucs ,
                                            vp.Vap_Cetes ,
                                            vp.Vap_Adicional_Cetes ,
                                            vp.Vap_Costos_Fijos_No_Papel ,
                                            vp.Vap_Costos_Fijos_Papel ,
                                            vp.Vap_Gastos_Admin ,
                                            vp.Vap_Inversion_Activos
                                          };
                verificador = 0;
                sqlcmd = transaction.Connection.CreateCommand() as SqlCommand;
                sqlcmd.Transaction = (System.Data.SqlClient.SqlTransaction)transaction;
                sqlcmd = CD_Datos.GenerarSqlCommand("CapValProyectoParams_Modificar", ref verificador, Parametros, Valores, transaction.Connection, sqlcmd);
                sqlcmd.Dispose();

                // -----------------------------------------------------------------
                // Insertar detalle 
                // -----------------------------------------------------------------
                string[] ParametrosDet = { 
                                        "@Id_Emp"
                                        ,"@Id_Cd"
                                        ,"@Id_Vap"
                                        ,"@Id_VapDet"
                                        ,"@Vap_Tipo"
                                        ,"@Id_Prd"
                                        ,"@Vap_Cantidad"
                                        ,"@Vap_Costo"
                                        ,"@Vap_Precio"	
                                        ,"@Vap_PrecioLista"
                                      };
                int i = 1;
                foreach (ValuacionProyectoDetalle ValuacionProyectoDetalle in valuacionProyecto.ListaProductosValuacionProyecto)
                {
                    ValuacionProyectoDetalle.Id_VapDet = i;
                    object[] ValoresDet = { 
                                        ValuacionProyectoDetalle.Id_Emp
                                        ,ValuacionProyectoDetalle.Id_Cd
                                        ,ValuacionProyectoDetalle.Id_Vap
                                        ,ValuacionProyectoDetalle.Id_VapDet
                                        ,ValuacionProyectoDetalle.Vap_Tipo
                                        ,ValuacionProyectoDetalle.Id_Prd
                                        ,ValuacionProyectoDetalle.Vap_Cantidad
                                        ,ValuacionProyectoDetalle.Vap_Costo
                                        ,ValuacionProyectoDetalle.Vap_Precio
                                        ,ValuacionProyectoDetalle.Vap_PrecioEspecial//Vap_PrecioEspecial
                                   };
                    sqlcmd = transaction.Connection.CreateCommand() as SqlCommand;
                    sqlcmd.Transaction = (System.Data.SqlClient.SqlTransaction)transaction;
                    sqlcmd = CD_Datos.GenerarSqlCommand("spCapValProyectoDetalle_Insertar", ref verificador, ParametrosDet, ValoresDet, transaction.Connection, sqlcmd);
                    sqlcmd.Dispose();
                    i += 1;
                }

                CD_CrmValuacionOportunidades cdCrmValuacionOportunidades = new CD_CrmValuacionOportunidades();
                cdCrmValuacionOportunidades.Eliminar(valuacionProyecto.Id_Emp, valuacionProyecto.Id_Cd, valuacionProyecto.Id_Cte, valuacionProyecto.Id_Vap, idRik, icdCtx);
                cdCrmValuacionOportunidades.Insertar(valuacionProyecto.Id_Emp, valuacionProyecto.Id_Cd, valuacionProyecto.Id_Cte, valuacionProyecto.Id_Vap, idRik, idOps, icdCtx);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EliminarValuacionProyecto(ValuacionProyecto valuacionProyecto, string Conexion, ref int verificador)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {
                CapaDatos.StartTrans();
                string[] Parametros = { 
	                                    "@Id_Emp"
                                        ,"@Id_Cd"
                                        ,"@Id_Vap"
                                      };
                object[] Valores = { 
                                       valuacionProyecto.Id_Emp
                                       ,valuacionProyecto.Id_Cd
                                       ,valuacionProyecto.Id_Vap
                                   };

                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd = CapaDatos.GenerarSqlCommand("spCapValProyecto_Eliminar", ref verificador, Parametros, Valores);
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

        public void ConsultaValuacionProyecto_Autorizacion(ref ValuacionProyecto VP, string Conexion, ref int verificador)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", 
                                        "@Id_Cd",
                                        "@Id_Vap"
                                      };
                object[] Valores = { 
                                       VP.Id_Emp, 
                                       VP.Id_Cd,
                                       VP.Id_Vap == 0?(object)null: VP.Id_Vap,
                                       VP.Vap_Estatus == ""? (object)null : VP.Vap_Estatus
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spValProyectoAutorizacion_Consulta", ref dr, Parametros, Valores);

                if (dr.HasRows)
                {//agregue procedure.. revisar si se necesita agregar a la tabla el campo fecha autorizacion.. y en que tabla?
                    //Id_Cd  Cd_Nombre	Id_U	U_Nombre	Id_Vap	Vap_Fecha	           Vap_Nota	Id_Emp	Vap_Estatus	Vap_Sustituida
                    dr.Read();
                    VP.Id_Emp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Emp")).ToString());
                    VP.Id_Cd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cd")).ToString());
                    VP.Cd_Nombre = dr.GetValue(dr.GetOrdinal("Cd_Nombre")).ToString();
                    VP.Id_U = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_U")));
                    VP.Cte_NomComercial = dr.GetValue(dr.GetOrdinal("U_Nombre")).ToString();
                    VP.Id_Vap = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Vap")).ToString());
                    VP.Vap_Fecha = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Vap_Fecha")).ToString());
                    VP.Vap_FechaStr = dr.IsDBNull(dr.GetOrdinal("Vap_Fecha")) ? "" : Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Vap_Fecha"))).ToString("dd/MM/yyyy hh:mm:ss tt");
                    VP.Vap_Nota = dr.GetValue(dr.GetOrdinal("Vap_Nota")).ToString();
                    VP.Vap_Estatus = (dr.GetValue(dr.GetOrdinal("Vap_Estatus")).ToString());
                    VP.Vap_Sustituida = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Vap_Sustituida")).ToString());
                    VP.Vap_FechaAutStr = dr.IsDBNull(dr.GetOrdinal("Vap_FechaAut")) ? "" : Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Vap_FechaAut"))).ToString("dd/MM/yyyy hh:mm:ss tt");
                    verificador = 1;
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaValuacionProyectoList(int Id_Emp, int Id_Cd, int Id_Val, string Conexion, ref List<ValuacionProyectoDetalle> List)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                SqlDataReader dr = null;
                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_Val" };
                object[] Valores = { Id_Emp, Id_Cd, Id_Val };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spValuacionProyectoDet_Consultar", ref dr, Parametros, Valores);
                ValuacionProyectoDetalle vpd = default(ValuacionProyectoDetalle);
                while (dr.Read())
                {
                    vpd = new ValuacionProyectoDetalle();
                    vpd.Id_VapDet = dr.GetInt32(dr.GetOrdinal("Id_VapDet"));
                    vpd.Vap_Tipo = dr.GetInt32(dr.GetOrdinal("Vap_Tipo"));
                    vpd.Vap_TipoStr = dr.IsDBNull(dr.GetOrdinal("Tipo")) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("Tipo")));
                    vpd.Prd_Descripcion = dr.IsDBNull(dr.GetOrdinal("Prd_Descripcion")) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("Prd_Descripcion")));
                    vpd.Prd_Presentacion = dr.IsDBNull(dr.GetOrdinal("Prd_Presentacion")) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("Prd_Presentacion")));
                    vpd.Prd_UniNe = dr.IsDBNull(dr.GetOrdinal("Prd_UniNe")) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("Prd_UniNe")));
                    vpd.Vap_Cantidad = dr.IsDBNull(dr.GetOrdinal("Vap_Cantidad")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Vap_Cantidad")));
                    vpd.Vap_Costo = dr.IsDBNull(dr.GetOrdinal("Vap_Costo")) ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Vap_Costo")));
                    vpd.Vap_Precio = dr.IsDBNull(dr.GetOrdinal("Vap_Precio")) ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Vap_Precio")));
                    vpd.Autorizado = dr.GetValue(dr.GetOrdinal("Det_Estatus")).ToString().ToUpper() == "A" ? true : false;
                    vpd.Rechazado = dr.GetValue(dr.GetOrdinal("Det_Estatus")).ToString().ToUpper() == "R" ? true : false;
                    vpd.Det_FecAut = dr.IsDBNull(dr.GetOrdinal("Det_FecAut")) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("Det_FecAut")));
                    vpd.Id_Prd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Prd")));
                    vpd.Estatus = dr.IsDBNull(dr.GetOrdinal("Det_Estatus")) ? "" : dr.GetValue(dr.GetOrdinal("Det_Estatus")).ToString();
                    List.Add(vpd);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarValuacionProyectoDetalle(ValuacionProyectoDetalle cl, List<ValuacionProyectoDetalle> list, string Conexion, ref int verificador)
        {
            CD_Datos CapaDatos = default(CD_Datos);
            try
            {
                SqlCommand sqlcmd = default(SqlCommand);
                CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros;
                object[] Valores;
                int valor = 0;
                int idEmp = 0;
                int idCd = 0;
                int idVal = 0;
                int idUsu = 0;
                int cantidad = list.Count;
                string estatus = string.Empty;
                CapaDatos.StartTrans();
                //Guardar los Detalles
                Parametros = new string[] { "@Id_Emp", "@Id_Cd", "@Id_Vap", "@Id_VapDet", "@Det_Estatus", "@Det_FecAut", "@Det_Autorizo" };
                foreach (ValuacionProyectoDetalle ValuacionProyecto in list)
                {
                    Valores = new object[] {
                                            cl.Id_Emp,
                                            cl.Id_Cd,
                                            cl.Id_Vap,
                                            ValuacionProyecto.Id_VapDet,
                                            ValuacionProyecto.Estatus,
                                            ValuacionProyecto.Det_FecAut==null ? (object)null: Convert.ToDateTime(ValuacionProyecto.Det_FecAut),                                           
                                            ValuacionProyecto.Id_U
                    };
                    sqlcmd = CapaDatos.GenerarSqlCommand("spValProyectoDet_Modificar", ref verificador, Parametros, Valores);

                    if (ValuacionProyecto.Estatus == "A")
                        valor++;
                    idEmp = cl.Id_Emp;
                    idCd = cl.Id_Cd;
                    idVal = cl.Id_Vap;
                    idUsu = cl.Id_U;
                }
                CapaDatos.CommitTrans();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
                if (valor == cantidad)
                    estatus = "A";
                else
                    if (valor == 0)
                        estatus = "R";
                    else
                        estatus = "P";

                ModificarValuacionProyecto_Aut(idEmp, idCd, idVal, idUsu, estatus, Conexion, ref verificador);
            }
            catch (Exception ex)
            {
                CapaDatos.RollBackTrans();
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cl"></param>
        /// <param name="list"></param>
        /// <param name="Conexion"></param>
        /// <param name="verificador"></param>
        /// <param name="icdCtx"></param>
        public void ModificarValuacionProyectoDetalle(ValuacionProyectoDetalle cl, List<ValuacionProyectoDetalle> list, string Conexion, ref int verificador, ICD_Contexto icdCtx)
        {
            ICD_Contexto<sianwebmty_gEntities> ctx = ((ICD_Contexto<sianwebmty_gEntities>)icdCtx);
            IDbTransaction transaction = ctx.Contexto.Database.CurrentTransaction.UnderlyingTransaction;
            SqlCommand sqlcmd = null;

            try
            {
                string[] Parametros;
                object[] Valores;
                int valor = 0;
                int idEmp = 0;
                int idCd = 0;
                int idVal = 0;
                int idUsu = 0;
                int cantidad = list.Count;
                string estatus = string.Empty;
                //Guardar los Detalles
                Parametros = new string[] { "@Id_Emp", "@Id_Cd", "@Id_Vap", "@Id_VapDet", "@Det_Estatus", "@Det_FecAut", "@Det_Autorizo" };
                foreach (ValuacionProyectoDetalle ValuacionProyecto in list)
                {
                    Valores = new object[] {
                                            cl.Id_Emp,
                                            cl.Id_Cd,
                                            cl.Id_Vap,
                                            ValuacionProyecto.Id_VapDet,
                                            ValuacionProyecto.Estatus,
                                            ValuacionProyecto.Det_FecAut==null ? (object)null: Convert.ToDateTime(ValuacionProyecto.Det_FecAut),                                           
                                            ValuacionProyecto.Id_U
                    };
                    sqlcmd = transaction.Connection.CreateCommand() as SqlCommand;
                    sqlcmd.Transaction = (System.Data.SqlClient.SqlTransaction)transaction;
                    sqlcmd = CD_Datos.GenerarSqlCommand("spValProyectoDet_Modificar", ref verificador, Parametros, Valores, transaction.Connection, sqlcmd);
                    sqlcmd.Dispose();

                    if (ValuacionProyecto.Estatus == "A")
                        valor++;
                    idEmp = cl.Id_Emp;
                    idCd = cl.Id_Cd;
                    idVal = cl.Id_Vap;
                    idUsu = cl.Id_U;
                }
                //CapaDatos.CommitTrans();
                //CapaDatos.LimpiarSqlcommand(ref sqlcmd);
                if (valor == cantidad)
                    estatus = "A";
                else
                    if (valor == 0)
                        estatus = "R";
                    else
                        estatus = "P";

                ModificarValuacionProyecto_Aut(idEmp, idCd, idVal, idUsu, estatus, Conexion, ref verificador, transaction);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarValuacionProyecto_Aut(int emp, int cd, int val, int usu, string estatus, string Conexion, ref int verificador)
        {
            CD_Datos CapaDatos = default(CD_Datos);
            try
            {
                SqlCommand sqlcmd = default(SqlCommand);
                CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros;
                object[] Valores;
                CapaDatos.StartTrans();
                //Guardar los Detalles
                Parametros = new string[] { "@Id_Emp", "@Id_Cd", "@Id_Vap", "@Det_Estatus", "@Det_Autorizo" };

                Valores = new object[] {
                                            emp,
                                            cd,
                                            val,                                           
                                            estatus,                                         
                                            usu
                    };
                sqlcmd = CapaDatos.GenerarSqlCommand("spValProyecto_Modificar", ref verificador, Parametros, Valores);


                CapaDatos.CommitTrans();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                CapaDatos.RollBackTrans();
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="emp"></param>
        /// <param name="cd"></param>
        /// <param name="val"></param>
        /// <param name="usu"></param>
        /// <param name="estatus"></param>
        /// <param name="Conexion"></param>
        /// <param name="verificador"></param>
        /// <param name="transaction"></param>
        public void ModificarValuacionProyecto_Aut(int emp, int cd, int val, int usu, string estatus, string Conexion, ref int verificador, IDbTransaction transaction)
        {
            SqlCommand sqlcmd = null;
            //CD_Datos CapaDatos = default(CD_Datos);
            try
            {
                //SqlCommand sqlcmd = default(SqlCommand);
                //CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros;
                object[] Valores;
                //CapaDatos.StartTrans();
                //Guardar los Detalles
                Parametros = new string[] { "@Id_Emp", "@Id_Cd", "@Id_Vap", "@Det_Estatus", "@Det_Autorizo" };

                Valores = new object[] {
                                            emp,
                                            cd,
                                            val,                                           
                                            estatus,                                         
                                            usu
                    };
                sqlcmd = transaction.Connection.CreateCommand() as SqlCommand;
                sqlcmd.Transaction = (System.Data.SqlClient.SqlTransaction)transaction;
                sqlcmd = CD_Datos.GenerarSqlCommand("spValProyecto_Modificar", ref verificador, Parametros, Valores, transaction.Connection, sqlcmd);
                sqlcmd.Dispose();


            }
            catch (Exception ex)
            {
                //CapaDatos.RollBackTrans();
                throw ex;
            }
        }

        public void consultarParametrosActuales(ref ValuacionParametrosActual vp, string Conexion, ref int verificador)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                verificador = 0;
                SqlDataReader dr = null;
                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_Vap" };
                object[] Valores = { vp.Id_Emp, vp.Id_Cd, vp.Id_Vap };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapValProyecto_ConsultaParametros", ref dr, Parametros, Valores);
                //ValuacionProyectoDetalle vpd = default(ValuacionProyectoDetalle);
                if (dr.HasRows)
                {
                    dr.Read();
                    vp.txtCuentasPorCobrar = (double)dr.GetValue(dr.GetOrdinal("txtCuentasPorCobrar"));
                    vp.txtInventario = (double)dr.GetValue(dr.GetOrdinal("txtInventario"));
                    vp.txtGastosServirCliente = (double)dr.GetValue(dr.GetOrdinal("txtGastosServirCliente"));
                    vp.txtVigencia = (double)dr.GetValue(dr.GetOrdinal("txtVigencia"));
                    vp.txtFleteLocales = (double)dr.GetValue(dr.GetOrdinal("txtFleteLocales"));
                    vp.txtIsr = (double)dr.GetValue(dr.GetOrdinal("txtIsr"));
                    vp.txtCetes = (double)dr.GetValue(dr.GetOrdinal("txtCetes"));
                    vp.txtFinanciamientoproveedores = (double)dr.GetValue(dr.GetOrdinal("txtFinanciamientoproveedores"));
                    vp.txtInversionactivosfijos = (double)dr.GetValue(dr.GetOrdinal("txtInversionactivosfijos"));
                    vp.txtCostodecapital = (double)dr.GetValue(dr.GetOrdinal("txtCostodecapital"));
                    vp.txtManoObra = (double)dr.GetValue(dr.GetOrdinal("txtManoObra"));
                    vp.txtGastosVarAplTerr = (double)dr.GetValue(dr.GetOrdinal("txtGastosVarAplTerr"));
                    vp.txtFletesPagadosalCliente = (double)dr.GetValue(dr.GetOrdinal("txtFletesPagadosalCliente"));


                    verificador = 1;


                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public void consultarParametros(ref ValuacionParametros vp, string Conexion, ref int verificador)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                SqlDataReader dr = null;
                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_Vap" };
                object[] Valores = { vp.Id_Emp, vp.Id_Cd, vp.Id_Vap };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spValuacionProyectoParametros_Consultar", ref dr, Parametros, Valores);
                //ValuacionProyectoDetalle vpd = default(ValuacionProyectoDetalle);
                if (dr.HasRows)
                {
                    dr.Read();
                    vp.Vap_Vigencia = (int)dr.GetValue(dr.GetOrdinal("Vap_Vigencia"));
                    vp.Vap_Participacion = (double)dr.GetValue(dr.GetOrdinal("Vap_Participacion"));
                    vp.Vap_Mano_Obra = (double)dr.GetValue(dr.GetOrdinal("Vap_Mano_Obra"));
                    vp.Vap_Amortizacion = (double)dr.GetValue(dr.GetOrdinal("Vap_Amortizacion"));
                    vp.Vap_Numero_Entregas = (int)dr.GetValue(dr.GetOrdinal("Vap_Numero_Entregas"));
                    vp.Vap_Costo_Entregas = (double)dr.GetValue(dr.GetOrdinal("Vap_Costo_Entregas"));
                    vp.Vap_Comision_Factoraje = (double)dr.GetValue(dr.GetOrdinal("Vap_Comision_Factoraje"));
                    vp.Vap_Comision_Anden = (double)dr.GetValue(dr.GetOrdinal("Vap_Comision_Anden"));
                    vp.Vap_Gasto_Flete_Locales = (double)dr.GetValue(dr.GetOrdinal("Vap_Gasto_Flete_Locales"));
                    vp.Vap_IVA = (double)dr.GetValue(dr.GetOrdinal("Vap_IVA"));
                    vp.Vap_Plazo_Pago_Cliente = (int)dr.GetValue(dr.GetOrdinal("Vap_Plazo_Pago_Cliente"));
                    vp.Vap_Inventario_Key = (int)dr.GetValue(dr.GetOrdinal("Vap_Inventario_Key"));
                    vp.Vap_Inventario_Consignacion = (int)dr.GetValue(dr.GetOrdinal("Vap_Inventario_Consignacion"));
                    vp.Vap_Inventario_Papel = (int)dr.GetValue(dr.GetOrdinal("Vap_Inventario_Papel"));
                    vp.Vap_Consignacion_Papel = (int)dr.GetValue(dr.GetOrdinal("Vap_Consignacion_Papel"));
                    vp.Vap_Credito_Key = (int)dr.GetValue(dr.GetOrdinal("Vap_Credito_Key"));
                    vp.Vap_Credito_Papel = (int)dr.GetValue(dr.GetOrdinal("Vap_Credito_Papel"));
                    vp.Vap_ISR = (double)dr.GetValue(dr.GetOrdinal("Vap_ISR"));
                    vp.Vap_Ucs = (double)dr.GetValue(dr.GetOrdinal("Vap_Ucs"));
                    vp.Vap_Cetes = (double)dr.GetValue(dr.GetOrdinal("Vap_Cetes"));
                    vp.Vap_Adicional_Cetes = (double)dr.GetValue(dr.GetOrdinal("Vap_Adicional_Cetes"));
                    vp.Vap_Costos_Fijos_No_Papel = (double)dr.GetValue(dr.GetOrdinal("Vap_Costos_Fijos_No_Papel"));
                    vp.Vap_Costos_Fijos_Papel = (double)dr.GetValue(dr.GetOrdinal("Vap_Costos_Fijos_Papel"));
                    vp.Vap_Gastos_Admin = (double)dr.GetValue(dr.GetOrdinal("Vap_Gastos_Admin"));
                    vp.Vap_Inversion_Activos = (int)dr.GetValue(dr.GetOrdinal("Vap_Inversion_Activos"));
                    verificador = 1;


                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void consultarCondicionesCentro(ref ValuacionParametros vp, string Conexion, ref int verificador)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                SqlDataReader dr = null;
                string[] Parametros = { "@Id_Emp", "@Id_Cd" };
                object[] Valores = { vp.Id_Emp, vp.Id_Cd };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spValCondicionesCentro_Consultar", ref dr, Parametros, Valores);
                //ValuacionProyectoDetalle vpd = default(ValuacionProyectoDetalle);
                if (dr.HasRows)
                {
                    dr.Read();
                    vp.Vap_Vigencia = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Vap_Vigencia")));
                    vp.Vap_Participacion = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Vap_Participacion")));
                    vp.Vap_Mano_Obra = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Vap_Mano_Obra")));
                    vp.Vap_Amortizacion = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Vap_Amortizacion")));
                    vp.Vap_Numero_Entregas = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Vap_Numero_Entregas")));
                    vp.Vap_Costo_Entregas = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Vap_Costo_Entregas")));
                    vp.Vap_Comision_Factoraje = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Vap_Comision_Factoraje")));
                    vp.Vap_Comision_Anden = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Vap_Comision_Anden")));
                    vp.Vap_Gasto_Flete_Locales = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Vap_Gasto_Flete_Locales")));
                    vp.Vap_IVA = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Vap_IVA")));
                    vp.Vap_Plazo_Pago_Cliente = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Vap_Plazo_Pago_Cliente")));
                    vp.Vap_Inventario_Key = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Vap_Inventario_Key")));
                    vp.Vap_Inventario_Consignacion = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Vap_Inventario_Consignacion")));
                    vp.Vap_Inventario_Papel = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Vap_Inventario_Key")));
                    vp.Vap_Consignacion_Papel = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Vap_Inventario_Consignacion")));
                    vp.Vap_Credito_Key = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Vap_Credito_Key")));
                    vp.Vap_Credito_Papel = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Vap_Credito_Papel")));
                    vp.Vap_ISR = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Vap_ISR")));
                    vp.Vap_Ucs = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Vap_Ucs")));
                    vp.Vap_Cetes = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Vap_Cetes")));
                    vp.Vap_Adicional_Cetes = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Vap_Adicional_Cetes")));
                    vp.Vap_Costos_Fijos_No_Papel = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Vap_Costos_Fijos_No_Papel")));
                    vp.Vap_Costos_Fijos_Papel = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Vap_Costos_Fijos_Papel")));
                    vp.Vap_Gastos_Admin = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Vap_Gastos_Admin")));
                    vp.Vap_Inversion_Activos = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Vap_Inversion_Activos")));
                    vp.Cd_ComisionRik = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Cd_ComisionRik")));
                    vp.Cd_FactorConvActFijo = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Cd_FactorConvActFijo")));
                    vp.Cd_DiasFinanciaProv = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Cd_DiasFinanciaProv")));
                    vp.Cd_TasaIncCostoCapital = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Cd_TasaIncCostoCapital")));
                    verificador = 1;
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Crea una entrada de persistencia en el repositorio para la entidad CapValProyecto
        /// </summary>
        /// <param name="capValProyecto">Instancia de datos desconectada de la entidad CapValProyecto</param>
        /// <param name="cadenaConexionEF">Cadena de conexión con formato de Entity Framework</param>
        /// <returns>Instancia de datos de la entidad CapValProyecto</returns>
        public CapValProyecto Insertar(CapValProyecto capValProyecto, string cadenaConexionEF)
        {
            CapValProyecto resultado = null;
            using (sianwebmty_gEntities ctx = new sianwebmty_gEntities(cadenaConexionEF))
            {
                resultado = ctx.CapValProyectoes.Add(capValProyecto);
                ctx.SaveChanges();
            }
            return resultado;
        }

        /// <summary>
        /// Crea una entrada de persistencia en el repositorio para la entidad CapValProyecto
        /// </summary>
        /// <param name="capValProyecto">Instancia de datos desconectada de la entidad CapValProyecto</param>
        /// <param name="icdCtx">Contexto de conexión a la fuente de datos </param>
        /// <returns>Instancia de datos de la entidad CapValProyecto</returns>
        public CapValProyecto Insertar(CapValProyecto capValProyecto, ICD_Contexto icdCtx)
        {
            CapValProyecto resultado = null;
            sianwebmty_gEntities ctx = ((ICD_Contexto<sianwebmty_gEntities>)icdCtx).Contexto;
            resultado = ctx.CapValProyectoes.Add(capValProyecto);
            return resultado;
        }

        /// <summary>
        /// Regresa la instancia de datos del repositorio de la valuación correspondiente al identificador proporcionado
        /// </summary>
        /// <param name="idEmp">Identificador de la empresa</param>
        /// <param name="idCd">Identificador del centro de distribución</param>
        /// <param name="idVal">Identificador de la valuación</param>
        /// <param name="cadenaConexionEF">Cadena de conexión con formato de Entity Framework</param>
        /// <returns>Instancia de datos de la valuación</returns>
        public CapValProyecto ConsultarPorId(int idEmp, int idCd, int idVal, string cadenaConexionEF)
        {
            CapValProyecto resultado = null;
            using (sianwebmty_gEntities ctx = new sianwebmty_gEntities(cadenaConexionEF))
            {
                var vals = (from v in ctx.CapValProyectoes
                            where v.Id_Emp == idEmp && v.Id_Cd == idCd && v.Id_Vap == idVal
                            select v).ToList();
                if (vals.Count > 0)
                {
                    resultado = vals[0];
                }
            }
            return resultado;
        }

        /// <summary>
        /// Regresa la instancia de datos del repositorio de la valuación correspondiente al identificador proporcionado
        /// </summary>
        /// <param name="idEmp">Identificador de la empresa</param>
        /// <param name="idCd">Identificador del centro de distribución</param>
        /// <param name="idVal">Identificador de la valuación</param>
        /// <param name="icdCtx">Contexto de conexión a la fuente de datos</param>
        /// <returns>Instancia de datos de la valuación</returns>
        public CapValProyecto ConsultarPorId(int idEmp, int idCd, int idVal, ICD_Contexto icdCtx)
        {
            CapValProyecto resultado = null;
            sianwebmty_gEntities ctx = ((ICD_Contexto<sianwebmty_gEntities>)icdCtx).Contexto;
            var vals = (from v in ctx.CapValProyectoes
                        where v.Id_Emp == idEmp && v.Id_Cd == idCd && v.Id_Vap == idVal
                        select v).ToList();
            if (vals.Count > 0)
            {
                resultado = vals[0];
            }
            return resultado;
        }

        /// <summary>
        /// Regresa el conjunto de valuaciones creadas por el RIK especificado.
        /// </summary>
        /// <param name="idEmp">Identificador de la empresa</param>
        /// <param name="idCd">Identificador del centro de distribución</param>
        /// <param name="idRik">Identificador del RIK</param>
        /// <param name="cadenaConexionEF">Cadena de conexión con formato de Entity Framework</param>
        /// <returns>Conjunto de valuaciones creadas por el RIK especificado</returns>
        public IEnumerable<CapValProyecto> ConsultarValuacionesPorRik(int idEmp, int idCd, int idRik, string cadenaConexionEF)
        {
            IEnumerable<CapValProyecto> resultado = null;
            using (sianwebmty_gEntities ctx = new sianwebmty_gEntities(cadenaConexionEF))
            {
                resultado = (from cvp in ctx.CapValProyectoes
                             where cvp.Id_Emp == idEmp && cvp.Id_Cd == idCd && cvp.Id_Rik == idRik
                             select cvp).ToList().Select(cvp =>
                             {
                                 cvp.CatClienteSerializable = cvp.CatCliente;
                                 return cvp;
                             }).ToList();
            }
            return resultado;
        }

        /// <summary>
        /// Actualiza el campo [Vap_Estatus2] de la entidad [CapValProyecto]
        /// </summary>
        /// <param name="idEmp">Identificador de la empresa a la que pertenece el centro de distribución idCd</param>
        /// <param name="idCd">Identificador del centro de distribución donde se encuentra registrada la valuación idVal</param>
        /// <param name="idVal">Identificador de la valuación a la que desea actualizar el valor del campo [Vap_Estatus2]</param>
        /// <param name="val">Valor al que desea actualizar el campo [vap_Estatus2]</param>
        /// <param name="cadenaConexionEF">Cadena de conexión a la fuente de datos con formato compatible con Entity Framework</param>
        public void ActualizarAtributoCap_Estatus2(int idEmp, int idCd, int idVal, int val, string cadenaConexionEF)
        {
            using (sianwebmty_gEntities ctx = new sianwebmty_gEntities(cadenaConexionEF))
            {
                var valuaciones = (from v in ctx.CapValProyectoes
                                   where v.Id_Emp == idEmp && v.Id_Cd == idCd && v.Id_Vap == idVal
                                   select v).ToList();
                if (valuaciones.Count > 0)
                {
                    var valuacion = valuaciones[0];
                    valuacion.Vap_Estatus2 = val;
                    ctx.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Actualiza el campo [Vap_Estatus2] de la entidad [CapValProyecto]
        /// </summary>
        /// <param name="idEmp">Identificador de la empresa a la que pertenece el centro de distribución idCd</param>
        /// <param name="idCd">Identificador del centro de distribución donde se encuentra registrada la valuación idVal</param>
        /// <param name="idVal">Identificador de la valuación a la que desea actualizar el valor del campo [Vap_Estatus2]</param>
        /// <param name="val">Valor al que desea actualizar el campo [vap_Estatus2]</param>
        /// <param name="ctx">Contexto de datos</param>
        public void ActualizarAtributoCap_Estatus2(int idEmp, int idCd, int idVal, int val, ICD_Contexto icdCtx)
        {
            sianwebmty_gEntities ctx = ((ICD_Contexto<sianwebmty_gEntities>)icdCtx).Contexto;
            var valuaciones = (from v in ctx.CapValProyectoes
                               where v.Id_Emp == idEmp && v.Id_Cd == idCd && v.Id_Vap == idVal
                               select v).ToList();
            if (valuaciones.Count > 0)
            {
                var valuacion = valuaciones[0];
                valuacion.Vap_Estatus2 = val;
            }
        }

        public IEnumerable<CapValProyecto> ConsultarValuacionesAAutorizar(int idEmp, int idCd, ICD_Contexto icdCtx)
        {
            sianwebmty_gEntities ctx = ((ICD_Contexto<sianwebmty_gEntities>)icdCtx).Contexto;
            var valuaciones = (from v in ctx.CapValProyectoes
                               join vop in ctx.CrmValuacionOportunidades
                               on new { idEmp = v.Id_Emp, idCd = v.Id_Cd, idVap = v.Id_Vap } equals new { idEmp = vop.Id_Emp, idCd = vop.Id_Cd, idVap = vop.Id_Val }
                               where v.Vap_Estatus2 == 2 && vop.CrmOportunidade.Estatus.Value == 2
                               select v).ToList();
            valuaciones.Distinct(new GenericEqualityComparer<CapValProyecto>((cvp1, cvp2) =>
            {
                return cvp1.Id_Vap == cvp2.Id_Vap && cvp1.Id_Emp == cvp2.Id_Emp && cvp1.Id_Cd == cvp2.Id_Cd && cvp1.Id_Cte == cvp2.Id_Cte;
            }, cvp => cvp.Id_Emp + cvp.Id_Cd + cvp.Id_Vap)).ToList();
            return valuaciones;
        }

        //
        // 11 Sep 2018 RFH 
        // Consulta Valuaciones por Autorizar reemplaza EF metodo.

        public List<eCapValProyecto> CRM2_ConsultarValuacionesAAutorizar(int Id_Emp, int Id_Cd, string Conexion)
        {
            List<eCapValProyecto> lst = new List<eCapValProyecto>();

            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", 
                                        "@Id_Cd"                                        
                                      };
                object[] Valores = { 
                                       Id_Emp, 
                                       Id_Cd                                       
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCRM_ValuacionesAAutorizar", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    eCapValProyecto obj = new eCapValProyecto();

                    obj.Id_Emp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Emp")).ToString());
                    obj.Id_Cd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cd")).ToString());
                    obj.Id_Op = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Op")).ToString());
                    obj.Id_Vap = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_vap")).ToString());
                    obj.Vap_Fecha = dr.GetValue(dr.GetOrdinal("Vap_Fecha")).ToString();
                    obj.Id_U = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_U")).ToString());
                    obj.Id_Cte = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cte")).ToString());
                    obj.Vap_Nota = dr.GetValue(dr.GetOrdinal("Vap_Nota")).ToString();
                    obj.Vap_Estatus = dr.GetValue(dr.GetOrdinal("Vap_Estatus")).ToString();
                    obj.Vap_UtilidadRemanente = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("Vap_UtilidadRemanente")));
                    obj.Vap_ValorPresenteNeto = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("Vap_ValorPresenteNeto")));
                    obj.Vap_Estatus2 = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Vap_Estatus2")).ToString());
                    obj.Id_Rik = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Rik")).ToString());
                    obj.Id_Ter = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Ter")).ToString());
                    obj.MotivoParaAutorizacion = dr.GetValue(dr.GetOrdinal("MotivoParaAutorizacion")).ToString();

                    obj.RikNombre = dr.GetValue(dr.GetOrdinal("RikNombre")).ToString();
                    obj.CteNombre = dr.GetValue(dr.GetOrdinal("CteNombre")).ToString();
                    obj.AplNombre = dr.GetValue(dr.GetOrdinal("AplicacionNombre")).ToString();

                    lst.Add(obj);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                lst = null;
            }
            return lst;
        }

        //

    }

    public class GenericEqualityComparer<T>
        : IEqualityComparer<T>
    {
        public GenericEqualityComparer(Func<T, T, bool> equal, Func<T, int> hashCode)
        {
            _equal = equal;
            _hashCode = hashCode;
        }

        public bool Equals(T x, T y)
        {
            return _equal(x, y);
        }

        public int GetHashCode(T obj)
        {
            return _hashCode(obj);
        }

        private Func<T, T, bool> _equal = null;
        private Func<T, int> _hashCode = null;
    }
}