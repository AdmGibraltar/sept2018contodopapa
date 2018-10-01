using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using CapaEntidad;

using System.Data;

namespace CapaDatos
{
    public class CD_CapValuacionProyectoCtasMarg
    {

        public void ConsultaValuacionProyecto_Buscar(ValuacionProyecto valuacionProyecto, ref List<ValuacionProyecto> listaValuacionProyecto, string Conexion,
            int? Id_U, string Nombre, int? Id_Cte_inicio, int? Id_Cte_fin, DateTime? Vap_Fecha_inicio, DateTime? Vap_Fecha_fin)
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

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapValProyectoCtasMarg_Buscar", ref dr, Parametros, Valores);
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

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapValProyectoCtasMarg_Consultar", ref dr, Parametros, Valores);
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
                }

                dr.Close();
                valuacionProyecto.ListaProductosValuacionProyecto = new List<ValuacionProyectoDetalle>();
                sqlcmd = CapaDatos.GenerarSqlCommand("spCapValProyectoCtasMargDetalle_Consultar", ref dr, Parametros, Valores);
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

        public void InsertarValuacionProyecto(ref ValuacionProyecto valuacionProyecto, ValuacionParametrosCtasMarg vp, string Conexion, ref int verificador)
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

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapValProyectoCtasMarg_Insertar", ref verificador, Parametros, Valores);
                valuacionProyecto.Id_Vap = verificador; //clave de val. proyecto

                // INSERTAR PARAMETROS
                Parametros = new string[] { 
                                            "@Id_Emp", 
                                            "@Id_Cd", 
                                            "@Id_Vap" , 
                                            //"@Vap_Vigencia" ,  
                                            //"@Vap_Participacion" , 
                                            "@Vap_Mano_Obra" , 
                                            //"@Vap_Amortizacion" , 
                                            //"@Vap_Numero_Entregas" , 
                                            //"@Vap_Costo_Entregas" , 
                                            //"@Vap_Comision_Factoraje" , 
                                            //"@Vap_Comision_Anden" , 
                                            "@Vap_Gasto_Flete_Locales" , 
                                            "@Vap_IVA" , 
                                            "@Vap_Dias_Cuentas_por_Cobrar" , 
                                            "@Vap_Inventario_Key" , 
                                            //"@Vap_Inventario_Consignacion" , 
                                            //"@Vap_Inventario_Papel" , 
                                            //"@Vap_Consignacion_Papel" , 
                                            "@Vap_Credito_Key" , 
                                            //"@Vap_Credito_Papel" , 
                                            "@Vap_ISR" , 
                                            //"@Vap_Ucs" , 
                                            "@Vap_Cetes" , 
                                            "@Vap_Adicional_Cetes" , 
                                            "@Vap_Contribucion_Costos_Fijos" , 
                                            //"@Vap_Costos_Fijos_Papel" , 
                                            "@Vap_Gastos_Admin" , 
                                            "@Vap_Inversion_Activos" ,
                                            "@Vap_Otros_Gastos_Variable"  
                                        };

                Valores = new object[] { 
                                            vp.Id_Emp,
                                            vp.Id_Cd,
                                            valuacionProyecto.Id_Vap,
                                            //vp.Vap_Vigencia ,
                                            //vp.Vap_Participacion ,
                                            vp.Vap_Mano_Obra ,
                                           // vp.Vap_Amortizacion ,
                                            //vp.Vap_Numero_Entregas ,
                                            //vp.Vap_Costo_Entregas ,
                                            //vp.Vap_Comision_Factoraje ,
                                            //vp.Vap_Comision_Anden ,
                                            vp.Vap_Gasto_Flete_Locales ,
                                            vp.Vap_IVA ,
                                            vp.Vap_Dias_Cuentas_por_Cobrar ,
                                            vp.Vap_Inventario_Key ,
                                            //vp.Vap_Inventario_Consignacion ,
                                            //vp.Vap_Inventario_Papel ,
                                            //vp.Vap_Consignacion_Papel ,
                                            vp.Vap_Credito_Key ,
                                            //vp.Vap_Credito_Papel ,
                                            vp.Vap_ISR ,
                                            //vp.Vap_Ucs ,
                                            vp.Vap_Cetes ,
                                            vp.Vap_Adicional_Cetes ,
                                            vp.Vap_Contribucion_Costos_Fijos ,
                                            //vp.Vap_Costos_Fijos_Papel ,
                                            vp.Vap_Gastos_Admin ,
                                            vp.Vap_Inversion_Activos,
                                            vp.Vap_Otros_Gastos_Variable
                                          };
                verificador = 0;
                sqlcmd = CapaDatos.GenerarSqlCommand("CapValProyectoParamsCtasMarg_Insertar", ref verificador, Parametros, Valores);


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
                    sqlcmd = CapaDatos.GenerarSqlCommand("spCapValProyectoCtasMargDetalle_Insertar", ref verificador, ParametrosDet, ValoresDet);
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

        public void ModificarValuacionProyecto(ref ValuacionProyecto valuacionProyecto, ValuacionParametrosCtasMarg vp, string Conexion, ref int verificador)
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

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapValProyectoCtasMarg_Modificar", ref verificador, Parametros, Valores);
                //valuacionProyecto.Id_Vap = verificador; //clave de val. proyecto

                // INSERTAR PARAMETROS
                Parametros = new string[] { 
                                            "@Id_Emp", 
                                            "@Id_Cd", 
                                            "@Id_Vap" , 
                                            //"@Vap_Vigencia" ,  
                                            //"@Vap_Participacion" , 
                                            "@Vap_Mano_Obra" , 
                                            //"@Vap_Amortizacion" , 
                                            //"@Vap_Numero_Entregas" , 
                                            //"@Vap_Costo_Entregas" , 
                                            //"@Vap_Comision_Factoraje" , 
                                            //"@Vap_Comision_Anden" , 
                                            "@Vap_Gasto_Flete_Locales" , 
                                            "@Vap_IVA" , 
                                            "@Vap_Dias_Cuentas_por_Cobrar" , 
                                            "@Vap_Inventario_Key" , 
                                            //"@Vap_Inventario_Consignacion" , 
                                            //"@Vap_Inventario_Papel" , 
                                            //"@Vap_Consignacion_Papel" , 
                                            "@Vap_Credito_Key" , 
                                            //"@Vap_Credito_Papel" , 
                                            "@Vap_ISR" , 
                                            //"@Vap_Ucs" , 
                                            "@Vap_Cetes" , 
                                            "@Vap_Adicional_Cetes" , 
                                            "@Vap_Contribucion_Costos_Fijos" , 
                                            //"@Vap_Costos_Fijos_Papel" , 
                                            "@Vap_Gastos_Admin" , 
                                            "@Vap_Inversion_Activos" ,
                                            "@Vap_Otros_Gastos_Variable" 
                                        };

                Valores = new object[] { 
                                            vp.Id_Emp,
                                            vp.Id_Cd,
                                            valuacionProyecto.Id_Vap,
                                            //vp.Vap_Vigencia ,
                                            //vp.Vap_Participacion ,
                                            vp.Vap_Mano_Obra ,
                                            //vp.Vap_Amortizacion ,
                                            //vp.Vap_Numero_Entregas ,
                                            //vp.Vap_Costo_Entregas ,
                                            //vp.Vap_Comision_Factoraje ,
                                            //vp.Vap_Comision_Anden ,
                                            vp.Vap_Gasto_Flete_Locales ,
                                            vp.Vap_IVA ,
                                            vp.Vap_Dias_Cuentas_por_Cobrar,
                                            vp.Vap_Inventario_Key ,
                                            //vp.Vap_Inventario_Consignacion ,
                                            //vp.Vap_Inventario_Papel ,
                                            //vp.Vap_Consignacion_Papel ,
                                            vp.Vap_Credito_Key ,
                                            //vp.Vap_Credito_Papel ,
                                            vp.Vap_ISR ,
                                            //vp.Vap_Ucs ,
                                            vp.Vap_Cetes ,
                                            vp.Vap_Adicional_Cetes ,
                                            vp.Vap_Contribucion_Costos_Fijos,
                                            //vp.Vap_Costos_Fijos_Papel ,
                                            vp.Vap_Gastos_Admin ,
                                            vp.Vap_Inversion_Activos,
                                            vp.Vap_Otros_Gastos_Variable
                                          };
                verificador = 0;
                sqlcmd = CapaDatos.GenerarSqlCommand("CapValProyectoParamsCtasMarg_Modificar", ref verificador, Parametros, Valores);

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
                    sqlcmd = CapaDatos.GenerarSqlCommand("spCapValProyectoCtasMargDetalle_Insertar", ref verificador, ParametrosDet, ValoresDet);
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

        public void consultarParametros(ref ValuacionParametrosCtasMarg vp, string Conexion, ref int verificador)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                SqlDataReader dr = null;
                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_Vap" };
                object[] Valores = { vp.Id_Emp, vp.Id_Cd, vp.Id_Vap };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spValuacionProyectoParametrosCtasMarg_Consultar", ref dr, Parametros, Valores);
                //ValuacionProyectoDetalle vpd = default(ValuacionProyectoDetalle);
                if (dr.HasRows)
                {
                    dr.Read();
                    //vp.Vap_Vigencia = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Vap_Vigencia")));
                    //vp.Vap_Participacion = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Vap_Participacion")));
                    vp.Vap_Mano_Obra = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Vap_Mano_Obra")));
                    //vp.Vap_Amortizacion = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Vap_Amortizacion")));
                    //vp.Vap_Numero_Entregas = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Vap_Numero_Entregas")));
                    //vp.Vap_Costo_Entregas = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Vap_Costo_Entregas")));
                    //vp.Vap_Comision_Factoraje = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Vap_Comision_Factoraje")));
                    //vp.Vap_Comision_Anden = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Vap_Comision_Anden")));
                    vp.Vap_Gasto_Flete_Locales = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Vap_Gasto_Flete_Locales")));
                    vp.Vap_IVA = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Vap_IVA")));
                    vp.Vap_Dias_Cuentas_por_Cobrar = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Vap_Dias_Cuentas_por_Cobrar")));
                    vp.Vap_Inventario_Key = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Vap_Inventario_Key")));
                    // vp.Vap_Inventario_Consignacion = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Vap_Inventario_Consignacion")));
                    //  vp.Vap_Inventario_Papel = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Vap_Inventario_Key")));
                    //vp.Vap_Consignacion_Papel = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Vap_Inventario_Consignacion")));
                    vp.Vap_Credito_Key = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Vap_Credito_Key")));
                    //vp.Vap_Credito_Papel = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Vap_Credito_Papel")));
                    vp.Vap_ISR = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Vap_ISR")));
                    //vp.Vap_Ucs = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Vap_Ucs")));
                    vp.Vap_Cetes = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Vap_Cetes")));
                    vp.Vap_Adicional_Cetes = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Vap_Adicional_Cetes")));
                    vp.Vap_Contribucion_Costos_Fijos = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Vap_Contribucion_Costos_Fijos")));
                    // vp.Vap_Costos_Fijos_Papel = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Vap_Costos_Fijos_Papel")));
                    vp.Vap_Gastos_Admin = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Vap_Gastos_Admin")));
                    vp.Vap_Inversion_Activos = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Vap_Inversion_Activos")));
                    vp.Vap_Otros_Gastos_Variable = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Vap_Otros_Gastos_Variable")));
                    verificador = 1;


                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void consultarCondicionesCentro(ref ValuacionParametrosCtasMarg vp, string Conexion, ref int verificador)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                SqlDataReader dr = null;
                string[] Parametros = { "@Id_Emp", "@Id_Cd" };
                object[] Valores = { vp.Id_Emp, vp.Id_Cd };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spValCondicionesCentroCtasMarg_Consultar", ref dr, Parametros, Valores);
                //ValuacionProyectoDetalle vpd = default(ValuacionProyectoDetalle);
                if (dr.HasRows)
                {
                    dr.Read();
                    //vp.Vap_Vigencia = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Vap_Vigencia")));
                    //vp.Vap_Participacion = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Vap_Participacion")));
                    vp.Vap_Mano_Obra = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Vap_Mano_Obra")));
                    //vp.Vap_Amortizacion = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Vap_Amortizacion")));
                    //vp.Vap_Numero_Entregas = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Vap_Numero_Entregas")));
                    //vp.Vap_Costo_Entregas = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Vap_Costo_Entregas")));
                    //vp.Vap_Comision_Factoraje = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Vap_Comision_Factoraje")));
                    //vp.Vap_Comision_Anden = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Vap_Comision_Anden")));
                    vp.Vap_Gasto_Flete_Locales = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Vap_Gasto_Flete_Locales")));
                    vp.Vap_IVA = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Vap_IVA")));
                    vp.Vap_Dias_Cuentas_por_Cobrar = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Vap_Dias_Cuentas_por_Cobrar")));
                    vp.Vap_Inventario_Key = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Vap_Inventario_Key")));
                   // vp.Vap_Inventario_Consignacion = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Vap_Inventario_Consignacion")));
                  //  vp.Vap_Inventario_Papel = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Vap_Inventario_Key")));
                    //vp.Vap_Consignacion_Papel = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Vap_Inventario_Consignacion")));
                    vp.Vap_Credito_Key = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Vap_Credito_Key")));
                    //vp.Vap_Credito_Papel = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Vap_Credito_Papel")));
                    vp.Vap_ISR = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Vap_ISR")));
                    //vp.Vap_Ucs = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Vap_Ucs")));
                    vp.Vap_Cetes = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Vap_Cetes")));
                    vp.Vap_Adicional_Cetes = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Vap_Adicional_Cetes")));
                    vp.Vap_Contribucion_Costos_Fijos = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Vap_Contribucion_Costos_Fijos")));
                   // vp.Vap_Costos_Fijos_Papel = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Vap_Costos_Fijos_Papel")));
                    vp.Vap_Gastos_Admin = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Vap_Gastos_Admin")));
                    vp.Vap_Inversion_Activos = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Vap_Inversion_Activos")));
                    vp.Vap_Otros_Gastos_Variable = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Vap_Otros_Gastos_Variable")));
                    verificador = 1;
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
