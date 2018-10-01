using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using CapaEntidad;

namespace CapaDatos
{
    public class CD_DevParcial_Detalle
    {
        public void InsertarDevParcial(Sesion Sesion, DevParcial_Detalle devparcial, List<DevParcial_Detalle> devparcialList, ref int verificador)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Sesion.Emp_Cnx);
            try
            {
                CapaDatos.StartTrans();
                SqlCommand sqlcmd = new SqlCommand();
                #region insert, update notaCredito Cabecera
                string[] Parametros = {

                                        "@Id_Emp",
                                        "@Id_Cd",
                                        "@Id_Ncr",
                                        "@Id_Cfe",
                                        "@Id_NcrSerie",
                                        "@Id_Reg",
                                        "@Id_Tm",

                                        "@Id_Cte",
                                        "@Id_Ter",
                                        "@Id_Rik",
                                        "@Id_U",
                                        "@Ncr_Tipo",

                                        "@Ncr_Fecha",
                                        "@Ncr_EmpleadoNumNomina",
                                        "@Ncr_EmpleadoNombre",
                                        "@Ncr_CtaContable",
                                        "@Ncr_Movimiento",

                                        "@Ncr_Referencia",
                                        "@Ncr_Saldo",
                                        "@Ncr_Desgloce",
                                        "@Ncr_DesglocePartidas",
                                        "@Ncr_Notas",
                                        "@Ncr_CteDIVA",

                                        "@Ncr_Subtotal",
                                        "@Ncr_Iva",
                                        "@Ncr_Total",
                                        "@Ncr_Pagado",
                                        "@Ncr_FecPagado",
                                        "@Ncr_Estatus",
                                        "@Ncr_ReferenciaSerie"
                                      };
                object[] Valores = { 
                                        Sesion.Id_Emp, 
                                        Sesion.Id_Cd_Ver,
                                        devparcial.Nota,
                                        null,
                                        null,
                                        0,                                        
                                        5,//devparcial.TipoMovimiento,

                                        devparcial.Cliente1,
                                        devparcial.Territorio,
                                        devparcial.Representante,
                                        Sesion.Id_U,
                                        4,

                                        devparcial.Fecha_dev,
                                        null,
                                        null,
                                        null,
                                        1,

                                        devparcial.Factura,//ref
                                        float.Parse("0"),//saldo
                                        0,//desgloce
                                        0,//desg-partidas
                                        devparcial.Notas,   
                                        0,//cteIVA      
 
                                        devparcial.Subtotal,
                                        devparcial.Iva,
                                        devparcial.Total,
                                        float.Parse("0"),//Ncr_Pagado
                                        devparcial.Fecha_dev,
                                        "C",
                                        null
                                   };
                //revisar el store falta lo de crear nota de credito
                if (devparcial.Nota != 0 && devparcial.Nota != null)
                    sqlcmd = CapaDatos.GenerarSqlCommand("spCapNotaCredito_Modificar", ref verificador, Parametros, Valores);
                else
                {
                    sqlcmd = CapaDatos.GenerarSqlCommand("spCapNotaCredito_InsertarDevolucion", ref verificador, Parametros, Valores);
                    if (verificador == 0)
                    {
                        sqlcmd = CapaDatos.GenerarSqlCommand("spCapNotaCredito_Modificar", ref verificador, Parametros, Valores);
                        devparcial.Nota = verificador;
                    }
                    else
                        devparcial.Nota = verificador;
                }
                #endregion
                #region insert en DevParcial Cabecera
                if (verificador != 0)
                {//insertar movimientos de nota..                      
                    string[] Parametros3 = {                                        
                                          "@Id_Emp"
                                          ,"@Id_Cd"
                                          ,"@Id_Num"
                                          ,"@Id_U"
                                          ,"@Id_Region"
                                          ,"@TipoDev"
                                          ,"@FechaDev"
                                          ,"@Factura"
                                          ,"@FechaFac"
                                          ,"@TipoMov"

                                          ,"@Id_Cliente"
                                          ,"@Territorio"
                                          ,"@Id_Rik"
                                          ,"@Nota"
                                          ,"@Descuento"
                                          ,"@Desc"
                                          ,"@Descuento2"  
                                          ,"@Desc2"

                                          ,"@Notas"
                                          ,"@Importe"
                                          ,"@Subtotal"
                                          ,"@IVA"
                                          ,"@Total" 
                                      };
                    object[] Valores3 = { 
                                        Sesion.Id_Emp, 
                                        Sesion.Id_Cd_Ver,
                                        devparcial.Numero,
                                        Sesion.Id_U,
                                        null,
                                        devparcial.TipoDev,
                                        devparcial.Fecha_dev,       
                                        devparcial.Factura,
                                        devparcial.Fecha_Fac,
                                        5,// devparcial.TipoMovimiento,

                                        devparcial.Cliente1,
                                        devparcial.Territorio,
                                        devparcial.Representante,
                                        devparcial.Nota,
                                        devparcial.Descuento,
                                        devparcial.Desc,
                                        devparcial.Descuento2,
                                        devparcial.Desc2,  
                                      
                                        devparcial.Notas,
                                        devparcial.Importe,
                                        devparcial.Subtotal,
                                        devparcial.Iva,
                                        devparcial.Total
                                   };
                    //revisar el store falta lo de crear nota de credito
                    int verificador3 = 0;
                     sqlcmd = CapaDatos.GenerarSqlCommand("spCapDevParcial_Insertar", ref verificador3, Parametros3, Valores3);                  
                }
                #endregion      
                #region insert, update notaCreditoDet
                foreach (DevParcial_Detalle devparcialDet in devparcialList)
                {
                    string[] Parametros2 = {
                                            "@Id_Emp",
                                            "@Id_Cd",
                                            "@Id_Ncr",
                                            "@Id_Ter",

                                            "@Id_Prd",
                                            "@Id_Rik",
                                            "@Ncr_Importe",
                                            "@Devuelto"
                                      };
                    object[] Valores2 = { 
                                        Sesion.Id_Emp, 
                                        Sesion.Id_Cd_Ver,
                                        devparcial.Nota, //Nota,//devparcial.Nota,                                        
                                        devparcialDet.Territorio,

                                        devparcialDet.IdProd,
                                        devparcialDet.Representante,
                                        float.Parse(devparcialDet.TotalImporte.ToString()),
                                        devparcialDet.Devuelto
                                   };
                    //revisar el store falta lo de crear nota de credito
                    sqlcmd = CapaDatos.GenerarSqlCommand("spCapNotaCreditoyDetalle", ref verificador, Parametros2, Valores2);               
                }
                #endregion
                #region insert en DevParcialDetalle
                if (verificador != 0)
                {//insertar movimientos de nota..   
                    //***************************
                    foreach (DevParcial_Detalle devparcialD in devparcialList)
                    {
                        string[] Parametros3 = {
                                                  "@Id_Emp",
                                                 "@Id_Cd",
                                                 "@Id_Dev",
                                                 "@Id_Ter",
                                                 "@Id_Prd",

                                                 "@Dev_Cant",
                                                 "@Factura" ,
                                                 "@Dev_Precio",
                                                 "@Devuelto"                                             
                                      };
                        object[] Valores3 = { 
                                        Sesion.Id_Emp, 
                                        Sesion.Id_Cd_Ver,
                                        devparcialD.Numero,
                                        devparcialD.Territorio,
                                        devparcialD.IdProd,

                                        devparcialD.Dev_Cant,
                                        devparcialD.Factura,
                                        devparcialD.Dev_Precio,
                                        devparcialD.Devuelto                                                                
                                   };
                        //revisar el store falta lo de crear nota de credito                        
                        sqlcmd = CapaDatos.GenerarSqlCommand("spCapDevParcialDet_Insertar", ref verificador, Parametros3, Valores3);                      
                    }
                }
                #endregion
                CapaDatos.CommitTrans();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                CapaDatos.RollBackTrans();
                throw ex;
            }
        }

        public void ConsultaDevParcialDetalleFactura(Sesion Sesion, int factura, int devolucion, ref List<DevParcial_Detalle> List)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Sesion.Emp_Cnx);
                string[] Parametros = { 
                                         "@Id_Emp",        
                                         "@Id_Cd",
                                         "@Id_Fac",
                                         "@Id_Dev"
                                      };
                object[] Valores = {                                       
                                       Sesion.Id_Emp,
                                       Sesion.Id_Cd_Ver,   
                                       factura,
                                       devolucion                                                                  
                                   };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatDevParcialDetalle_Consulta", ref dr, Parametros, Valores);

                DevParcial_Detalle devParcialDetalle;
                while (dr.Read())
                {
                    devParcialDetalle = new DevParcial_Detalle();
                    devParcialDetalle.Numero = (int)dr.GetValue(dr.GetOrdinal("Id_Dev"));
                    devParcialDetalle.TipoDev = (int)dr.GetValue(dr.GetOrdinal("Dev_Tipo"));
                    devParcialDetalle.Factura = (int)dr.GetValue(dr.GetOrdinal("Id_Fac"));
                    devParcialDetalle.TipoMovimiento = (int)dr.GetValue(dr.GetOrdinal("Id_Tm"));
                    devParcialDetalle.Cliente1 = (int)dr.GetValue(dr.GetOrdinal("Id_Cte"));
                    devParcialDetalle.Territorio = (int)dr.GetValue(dr.GetOrdinal("Id_Ter"));
                    devParcialDetalle.Representante = (int)dr.GetValue(dr.GetOrdinal("Id_Rik"));
                    devParcialDetalle.Descuento = (double)dr.GetValue(dr.GetOrdinal("Fac_DescPorcen1"));
                    devParcialDetalle.Descuento2 = (double)dr.GetValue(dr.GetOrdinal("Fac_DescPorcen2"));
                    devParcialDetalle.Desc = (string)dr.GetValue(dr.GetOrdinal("Fac_Desc1"));
                    devParcialDetalle.Desc2 = (string)dr.GetValue(dr.GetOrdinal("Fac_Desc2"));
                    devParcialDetalle.Notas = (string)dr.GetValue(dr.GetOrdinal("Fac_Notas"));
                    devParcialDetalle.Nota = (int)dr.GetValue(dr.GetOrdinal("Id_Ncr"));
                    devParcialDetalle.Fecha_Fac = (DateTime)dr.GetValue(dr.GetOrdinal("Fac_Fecha"));
                    devParcialDetalle.Fecha_dev = (DateTime)dr.GetValue(dr.GetOrdinal("Dev_Fecha"));
                    devParcialDetalle.Fecha_devHr =  dr.GetDateTime(dr.GetOrdinal("Dev_FechaHr"));  
                    devParcialDetalle.Importe = (double)dr.GetValue(dr.GetOrdinal("Fac_Importe"));//Fac_Importe
                    devParcialDetalle.Subtotal = (double)dr.GetValue(dr.GetOrdinal("Fac_Subtotal"));
                    devParcialDetalle.Iva = (double)dr.GetValue(dr.GetOrdinal("Fac_Iva"));
                    devParcialDetalle.Total = (double)dr.GetValue(dr.GetOrdinal("Fac_Total"));
                    devParcialDetalle.Fac_Total2 = (string)dr.GetValue(dr.GetOrdinal("Fac_Total2"));
                    List.Add(devParcialDetalle);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaDetalleFactura(Sesion Sesion, int factura, int id, ref List<DevParcial_DetalleFactura> List)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Sesion.Emp_Cnx);
                string[] Parametros = { 
                                         "@Id_Emp",        
                                         "@Id_Cd",
                                         "@Id_Fac",
                                         "@Id_Dev"
                                      };
                object[] Valores = {                                       
                                       Sesion.Id_Emp,
                                       Sesion.Id_Cd_Ver,   
                                       factura      ,
                                       id                                                           
                                   };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapFacturaDetalleDev", ref dr, Parametros, Valores);

                DevParcial_DetalleFactura devParcial;
                while (dr.Read())
                {
                    devParcial = new DevParcial_DetalleFactura();
                    devParcial.Id_Fac = (int)dr.GetValue(dr.GetOrdinal("Id_Fac"));
                    devParcial.Id_FacDet = (int)dr.GetValue(dr.GetOrdinal("Id_FacDet"));
                    devParcial.Id_Ter = (int)dr.GetValue(dr.GetOrdinal("Id_Ter"));
                    devParcial.Territorio1 = (string)dr.GetValue(dr.GetOrdinal("Ter_Nombre"));
                    devParcial.Id_Prod = (int)dr.GetValue(dr.GetOrdinal("Id_Prd"));
                    devParcial.Descripcion1 = (string)dr.GetValue(dr.GetOrdinal("Prd_Descripcion"));
                    devParcial.Present1 = (string)dr.GetValue(dr.GetOrdinal("Prd_Presentacion"));
                    devParcial.Cantidad1 = (int)dr.GetValue(dr.GetOrdinal("Fac_Cant"));
                    devParcial.Precio1 = (double)dr.GetValue(dr.GetOrdinal("Fac_Precio"));
                    devParcial.Importe1 = (double)dr.GetValue(dr.GetOrdinal("Fac_Importe"));
                    devParcial.Devuelto = (int)dr.GetValue(dr.GetOrdinal("Devuelto")) == 1 ? true : false;
                    devParcial.CantDevuelta = (int)dr.GetValue(dr.GetOrdinal("Dev_Cant"));
                    devParcial.Prd_Agrupador = (int)dr.GetValue(dr.GetOrdinal("Prd_AgrupadoSpo"));
                    List.Add(devParcial);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public void ConsultaFacturas(Sesion Sesion, int factura, ref DevParcial_DetalleFactura devParcialDetalle)
        {//combo_Facturas
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Sesion.Emp_Cnx);
                string[] Parametros = { 
                                         "@Id1",        
                                         "@Id2",
                                         "@Id3"
                                      };
                object[] Valores = {                                       
                                       Sesion.Id_Emp,
                                       Sesion.Id_Cd_Ver,   
                                       factura == -1 ? (object)null : factura                                                                                                            
                                   };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatFactura_Combo", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    devParcialDetalle = new DevParcial_DetalleFactura();                   
                    devParcialDetalle.Id_Fac = (int)dr.GetValue(dr.GetOrdinal("id"));
                    devParcialDetalle.Descripcion1 = (string)dr.GetValue(dr.GetOrdinal("Descripcion"));  
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
