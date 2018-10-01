using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.SqlClient;
using System.Data;

using CapaEntidad;

namespace CapaDatos
{
    public class CD_CapNotaCargo
    {
        public void ConsultaNotaCargo(ref NotaCargo notaCargo, string Conexion)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { 
                                          "@Id_Emp" 
                                          ,"@Id_Cd"
                                          ,"@Id_Nca"
                                          , "@Id_NcaSerie"
                                      };
                object[] Valores = { 
                                       notaCargo.Id_Emp
                                       ,notaCargo.Id_Cd
                                       ,notaCargo.Id_Nca
                                       , notaCargo.Id_NcaSerie
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapNotaCargo_Consultar", ref dr, Parametros, Valores);
                NotaCargo fac = new NotaCargo();
                if (dr.HasRows)
                {
                    dr.Read();
                    notaCargo.Id_Emp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Emp")));
                    notaCargo.Id_Cd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cd")));
                    notaCargo.Id_Nca = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Nca")));

                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Cfe")))) notaCargo.Id_Cfe = null; else notaCargo.Id_Cfe = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cfe")));
                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_NcaSerie")))) notaCargo.Id_NcaSerie = string.Empty; else notaCargo.Id_NcaSerie = dr.GetValue(dr.GetOrdinal("Id_NcaSerie")).ToString();

                    notaCargo.Id_Reg = dr.IsDBNull(dr.GetOrdinal("Id_Reg")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Reg")));
                    notaCargo.Id_Ter = dr.IsDBNull(dr.GetOrdinal("Id_Ter")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Ter")));
                    notaCargo.Ter_Nombre = dr.IsDBNull(dr.GetOrdinal("Ter_Nombre")) ? "" : dr.GetValue(dr.GetOrdinal("Ter_Nombre")).ToString();
                    notaCargo.Id_Rik = dr.IsDBNull(dr.GetOrdinal("Id_Rik")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Rik")));
                    notaCargo.Id_U = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_U")));
                    notaCargo.Id_Cte = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cte")));
                    notaCargo.Cte_NomComercial = dr.IsDBNull(dr.GetOrdinal("Cte_NomComercial")) ? string.Empty : dr.GetValue(dr.GetOrdinal("Cte_NomComercial")).ToString();
                    notaCargo.Nca_Tipo = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Nca_Tipo")));
                    notaCargo.Nca_Fecha = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Nca_Fecha")));
                    if (dr.IsDBNull(dr.GetOrdinal("Nca_FechaHr")))
                        notaCargo.Nca_FechaHr = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Nca_Fecha")));
                    else
                        notaCargo.Nca_FechaHr = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Nca_FechaHr")));
                    notaCargo.Id_Tm = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Tm")));
                    notaCargo.Tm_Nombre = Convert.ToString(dr.GetValue(dr.GetOrdinal("Tm_Nombre")));
                    if (dr.IsDBNull(dr.GetOrdinal("Id_Ban")))
                    {
                        notaCargo.Id_Ban = null;
                    }
                    else
                    {
                        notaCargo.Id_Ban = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Ban")));
                    }
                    notaCargo.Nca_CtaContable = dr.IsDBNull(dr.GetOrdinal("Nca_CtaContable")) ? string.Empty : dr.GetValue(dr.GetOrdinal("Nca_CtaContable")).ToString();
                    notaCargo.Nca_Desgloce = dr.IsDBNull(dr.GetOrdinal("Nca_Desgloce")) ? false : Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Nca_Desgloce")));
                    notaCargo.Nca_Referencia = dr.IsDBNull(dr.GetOrdinal("Nca_Referencia")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Nca_Referencia")));
                    notaCargo.Nca_Notas = dr.IsDBNull(dr.GetOrdinal("Nca_Notas")) ? string.Empty : dr.GetValue(dr.GetOrdinal("Nca_Notas")).ToString();

                    notaCargo.Nca_RSubtotal = dr.IsDBNull(dr.GetOrdinal("Nca_RSubtotal")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Nca_RSubtotal")));
                    notaCargo.Nca_RIva = dr.IsDBNull(dr.GetOrdinal("Nca_RIva")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Nca_RIva")));
                    notaCargo.Nca_RTotal = dr.IsDBNull(dr.GetOrdinal("Nca_RTotal")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Nca_RTotal")));
                    notaCargo.Nca_Subtotal = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Nca_Subtotal")));
                    notaCargo.Nca_Iva = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Nca_Iva")));
                    notaCargo.Nca_Total = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Nca_Total")));

                    notaCargo.Nca_Estatus = dr.IsDBNull(dr.GetOrdinal("Nca_Estatus")) ? string.Empty : dr.GetValue(dr.GetOrdinal("Nca_Estatus")).ToString();
                    notaCargo.Nca_EstatusStr = dr.IsDBNull(dr.GetOrdinal("Nca_Estatus")) ? string.Empty : Estatus(dr.GetValue(dr.GetOrdinal("Nca_Estatus")).ToString());
                    if (dr.IsDBNull(dr.GetOrdinal("Nca_FecPag"))) notaCargo.Nca_FecPag = null; else notaCargo.Nca_FecPag = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Nca_FecPag")));
                    //notaCargo.Importe = dr.IsDBNull(dr.GetOrdinal("Nca_Importe")) ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Nca_Importe")));
                    notaCargo.Nca_Pagado = dr.IsDBNull(dr.GetOrdinal("Nca_Pagado")) ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Nca_Pagado")));

                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Nca_Sello")))) notaCargo.Nca_Sello = string.Empty; else notaCargo.Nca_Sello = dr.GetValue(dr.GetOrdinal("Nca_Sello")).ToString();
                    notaCargo.Nca_FPago = dr.IsDBNull(dr.GetOrdinal("Nca_FPago")) ? "" : dr.GetValue(dr.GetOrdinal("Nca_FPago")).ToString();
                    notaCargo.Nca_UDigitos = dr.IsDBNull(dr.GetOrdinal("nca_udigitos")) ? string.Empty : dr.GetValue(dr.GetOrdinal("nca_udigitos")).ToString();

                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Nca_XML"))))
                    {
                        notaCargo.Nca_Xml = null;
                    }
                    else
                    {
                        notaCargo.Nca_Xml = (object)dr.GetValue(dr.GetOrdinal("Nca_XML"));
                    }
                }
                dr.Close();

                notaCargo.ListaNotaCargo = new List<NotaCargoDet>();
                sqlcmd = CapaDatos.GenerarSqlCommand("spCapNotaCargoDetalle_Consultar", ref dr, Parametros, Valores);
                while (dr.Read())
                {
                    NotaCargoDet notaCargoDet = new NotaCargoDet();
                    notaCargoDet.Id_Emp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Emp")));
                    notaCargoDet.Id_Cd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cd")));
                    notaCargoDet.Id_Nca = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Nca")));
                    notaCargoDet.Id_NcaDet = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_NcaDet")));
                    notaCargoDet.Id_Ter = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Ter")));
                    notaCargoDet.Ter_Nombre = dr.GetValue(dr.GetOrdinal("Ter_Nombre")).ToString();
                    notaCargoDet.Id_Rik = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Rik")));
                    notaCargoDet.Rik_Nombre = dr.GetValue(dr.GetOrdinal("Rik_Nombre")).ToString();
                    notaCargoDet.Id_Prd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Prd")));
                    notaCargoDet.Prd_Nombre = dr.GetValue(dr.GetOrdinal("Prd_Descripcion")).ToString();
                    notaCargoDet.Nca_Importe = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Nca_Importe")));
                    notaCargoDet.Prd_Unis = Convert.ToString(dr.GetValue(dr.GetOrdinal("Nca_Unis")));
                    notaCargoDet.Prd_Presentacion = Convert.ToString(dr.GetValue(dr.GetOrdinal("Prd_Presentacion")));
                    notaCargo.ListaNotaCargo.Add(notaCargoDet);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string Estatus(string p)
        {
            try
            {
                switch (p.ToUpper())
                {
                    case "C": return "Capturado";
                    case "I": return "Impreso";
                    case "B": return "Baja";
                    default: return "";
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaNotaCargo_Encabezado(ref NotaCargo notaCargo, string Conexion, ref bool encontrado)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { 
                                          "@Id_Emp" 
                                          ,"@Id_Cd"
                                          ,"@Id_Nca"
                                          , "@Id_NcaSerie"
                                      };
                object[] Valores = { 
                                       notaCargo.Id_Emp
                                       ,notaCargo.Id_Cd
                                       ,notaCargo.Id_Nca
                                       ,notaCargo.Id_NcaSerie 
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapNotaCargo_Consultar", ref dr, Parametros, Valores);
                NotaCargo fac = new NotaCargo();
                encontrado = false;
                if (dr.HasRows)
                {
                    dr.Read();
                    encontrado = true;
                    notaCargo.Id_Emp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Emp")));
                    notaCargo.Id_Cd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cd")));
                    notaCargo.Id_Nca = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Nca")));

                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Cfe")))) notaCargo.Id_Cfe = null; else notaCargo.Id_Cfe = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cfe")));
                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_NcaSerie")))) notaCargo.Id_NcaSerie = string.Empty; else notaCargo.Id_NcaSerie = dr.GetValue(dr.GetOrdinal("Id_NcaSerie")).ToString();

                    notaCargo.Id_Reg = dr.IsDBNull(dr.GetOrdinal("Id_Reg")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Reg")));
                    notaCargo.Id_Ter = dr.IsDBNull(dr.GetOrdinal("Id_Ter")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Ter")));
                    notaCargo.Id_Rik = dr.IsDBNull(dr.GetOrdinal("Id_Rik")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Rik")));
                    notaCargo.Id_U = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_U")));
                    notaCargo.Id_Cte = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cte")));
                    notaCargo.Cte_NomComercial = dr.IsDBNull(dr.GetOrdinal("Cte_NomComercial")) ? string.Empty : dr.GetValue(dr.GetOrdinal("Cte_NomComercial")).ToString();
                    notaCargo.Nca_Tipo = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Nca_Tipo")));
                    notaCargo.Nca_Fecha = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Nca_Fecha")));
                    notaCargo.Id_Tm = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Tm")));
                    if (dr.IsDBNull(dr.GetOrdinal("Id_Ban")))
                    {
                        notaCargo.Id_Ban = null;
                    }
                    else
                    {
                        notaCargo.Id_Ban = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Ban")));
                    }
                    notaCargo.Nca_CtaContable = dr.IsDBNull(dr.GetOrdinal("Nca_CtaContable")) ? string.Empty : dr.GetValue(dr.GetOrdinal("Nca_CtaContable")).ToString();
                    notaCargo.Nca_Desgloce = dr.IsDBNull(dr.GetOrdinal("Nca_Desgloce")) ? false : Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Nca_Desgloce")));
                    notaCargo.Nca_Referencia = dr.IsDBNull(dr.GetOrdinal("Nca_Referencia")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Nca_Referencia")));
                    notaCargo.Nca_Notas = dr.IsDBNull(dr.GetOrdinal("Nca_Notas")) ? string.Empty : dr.GetValue(dr.GetOrdinal("Nca_Notas")).ToString();

                    notaCargo.Nca_RSubtotal = dr.IsDBNull(dr.GetOrdinal("Nca_RSubtotal")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Nca_RSubtotal")));
                    notaCargo.Nca_RIva = dr.IsDBNull(dr.GetOrdinal("Nca_RIva")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Nca_RIva")));
                    notaCargo.Nca_RTotal = dr.IsDBNull(dr.GetOrdinal("Nca_RTotal")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Nca_RTotal")));
                    notaCargo.Nca_Subtotal = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Nca_Subtotal")));
                    notaCargo.Nca_Iva = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Nca_Iva")));
                    notaCargo.Nca_Total = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Nca_Total")));

                    notaCargo.Nca_Estatus = dr.IsDBNull(dr.GetOrdinal("Nca_Estatus")) ? string.Empty : dr.GetValue(dr.GetOrdinal("Nca_Estatus")).ToString();
                    if (dr.IsDBNull(dr.GetOrdinal("Nca_FecPag"))) notaCargo.Nca_FecPag = null; else notaCargo.Nca_FecPag = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Nca_FecPag")));
                    //notaCargo.Importe = dr.IsDBNull(dr.GetOrdinal("Nca_Importe")) ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Nca_Importe")));
                    notaCargo.Nca_Pagado = dr.IsDBNull(dr.GetOrdinal("Nca_Pagado")) ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Nca_Pagado")));

                    notaCargo.Nca_Saldo = notaCargo.Nca_Total - notaCargo.Nca_Pagado; //SALDO
                }
                dr.Close();

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaNotaCargo_Buscar(NotaCargo notaCargo, ref List<NotaCargo> listaNotaCargo, string Conexion
            , int? Id_Cte_inicio
            , int? Id_Cte_fin
            , DateTime? Nca_Fecha_inicio
            , DateTime? Nca_Fecha_fin
            , string Nca_Estatus
            , int? Id_Nca_inicio
            , int? Id_Nca_fin
            , int? Id_U)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { 
                                          "@Id_Emp" 
                                          ,"@Id_Cd"
                                          ,"@Id_Cte_inicio"
                                          ,"@Id_Cte_fin"
                                          ,"@Nca_Fecha_inicio"
                                          ,"@Nca_Fecha_fin"
                                          ,"@Nca_Estatus"
                                          ,"@Id_Nca_inicio"
                                          ,"@Id_Nca_fin"
                                          ,"@Id_U"
                                      };
                object[] Valores = { 
                                       notaCargo.Id_Emp
                                       ,notaCargo.Id_Cd
                                       ,Id_Cte_inicio
                                       ,Id_Cte_fin
                                       ,Nca_Fecha_inicio
                                       ,Nca_Fecha_fin
                                       ,Nca_Estatus == string.Empty ? null : Nca_Estatus
                                       ,Id_Nca_inicio
                                       ,Id_Nca_fin
                                       ,Id_U
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapNotaCargo_Buscar", ref dr, Parametros, Valores);
                listaNotaCargo = new List<NotaCargo>();
                while (dr.Read())
                {
                    notaCargo = new NotaCargo();
                    notaCargo.Id_Emp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Emp")));
                    notaCargo.Id_Cd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cd")));
                    notaCargo.Id_Nca = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Nca")));
                    notaCargo.Id_Cte = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cte")));
                    notaCargo.Cte_NomComercial = dr.IsDBNull(dr.GetOrdinal("Cte_NomComercial")) ? string.Empty : dr.GetValue(dr.GetOrdinal("Cte_NomComercial")).ToString();
                    notaCargo.Nca_Tipo = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Nca_Tipo")));
                    notaCargo.Nca_TipoStr = dr.GetValue(dr.GetOrdinal("Nca_TipoStr")).ToString();
                    notaCargo.U_Nombre = dr.GetValue(dr.GetOrdinal("U_Nombre")).ToString();
                    notaCargo.Nca_Fecha = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Nca_Fecha")));
                    notaCargo.Nca_Subtotal = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Nca_Subtotal")));
                    notaCargo.Nca_Iva = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Nca_ImporteIVA")));
                    notaCargo.Nca_Total = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Nca_Total")));
                    notaCargo.Nca_Pagado = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Nca_Pagado")));
                    notaCargo.Nca_Saldo = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Nca_Saldo")));
                    notaCargo.Nca_Estatus = dr.IsDBNull(dr.GetOrdinal("Nca_Estatus")) ? string.Empty : dr.GetValue(dr.GetOrdinal("Nca_Estatus")).ToString();
                    notaCargo.Nca_EstatusStr = dr.IsDBNull(dr.GetOrdinal("Nca_EstatusStr")) ? string.Empty : dr.GetValue(dr.GetOrdinal("Nca_EstatusStr")).ToString();
                    notaCargo.PDF = dr.IsDBNull(dr.GetOrdinal("PDF")) ? false : Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("PDF")).ToString());
                    notaCargo.NcaXML = dr.IsDBNull(dr.GetOrdinal("NcaXML")) ? false : Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("NcaXML")).ToString());
                    notaCargo.Id_NcaSerie = dr["Id_NcaSerie"].ToString();
                    notaCargo.Nca_FolioFiscal = dr["Nca_FolioFiscal"].ToString();
                    listaNotaCargo.Add(notaCargo);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarMovsNotaCargo(Movimientos mov, ref List<Movimientos> listaMovimientos, string Conexion)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { 
                                          "@Id_Emp"
                                      };
                object[] Valores = { 
                                       mov.Id_Emp
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatTipoMovimiento_MovsNotaCargo", ref dr, Parametros, Valores);
                listaMovimientos = new List<Movimientos>();
                while (dr.Read())
                {
                    mov = new Movimientos();
                    mov.Id = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Tm")));
                    mov.Nombre = dr.GetValue(dr.GetOrdinal("Tm_Nombre")).ToString();
                    mov.AfeVta = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Tm_AfcVta")));
                    listaMovimientos.Add(mov);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarNotaCargoSAT(ref NotaCargo notaCargo, string Conexion, ref object resultado)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {
                string[] Parametros = { 
                                        "@Id_Emp" 
                                        ,"@Id_Cd"
	                                    ,"@Id_Nca"
                                        , "@Id_NcaSerie"
                                      };
                object[] Valores = { 
                                        notaCargo.Id_Emp
                                        ,notaCargo.Id_Cd
                                        ,notaCargo.Id_Nca
                                        , notaCargo.Id_NcaSerie
                                   };

                // ------------------------------------
                // Consultar PDF de la factura
                // ------------------------------------
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapNotaCargo_ConsultaSAT", ref resultado, Parametros, Valores);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarCantidadNotaCargoCentroDist(ref int verificador, int Id_Cd, string Conexion)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);


                string[] Parametros = { "@Id_Cd" };
                object[] Valores = { Id_Cd };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand(
                    "spCapNotaCargoCantidadEnCd_Consultar", ref verificador, Parametros, Valores);

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaPagoFicha(ref NotaCargo ficha, string Conexion, ref int verificador)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { 
                                          "@Id_Emp", 
                                          "@Serie",
                                          "@Id_Ref"
                                      };
                object[] Valores = { 
                                       ficha.Id_Emp, 
                                       ficha.Serie,
                                       ficha.Id_Nca
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapNCargoFicha_Consultar", ref dr, Parametros, Valores);
                NotaCargo fac = new NotaCargo();
                if (dr.HasRows)
                {
                    dr.Read();
                    ficha.Id_Ter = dr.IsDBNull(dr.GetOrdinal("Id_Ter")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Ter")));
                    ficha.Id_Cte = dr.IsDBNull(dr.GetOrdinal("Id_Cte")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cte")));
                    ficha.Cte_NomComercial = dr.IsDBNull(dr.GetOrdinal("Cte_NomComercial")) ? "" : dr.GetValue(dr.GetOrdinal("Cte_NomComercial")).ToString();
                    ficha.Nca_Fecha = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Nca_Fecha")));
                    ficha.Nca_Estatus = dr.IsDBNull(dr.GetOrdinal("Nca_Estatus")) ? "" : dr.GetValue(dr.GetOrdinal("Nca_Estatus")).ToString();
                    ficha.Importe = dr.IsDBNull(dr.GetOrdinal("Nca_Importe")) ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Nca_Importe")));
                    ficha.Nca_Pagado = dr.IsDBNull(dr.GetOrdinal("Nca_Pagado")) ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Nca_Pagado")));
                    ficha.Id_Cd = dr.IsDBNull(dr.GetOrdinal("Id_Cd")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cd")));
                    verificador = 1;
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertarNotaCargo(ref NotaCargo notaCargo, string Conexion, ref int verificador, List<AdendaDet> ListCab, DataTable listaFacturaDet)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {
                CapaDatos.StartTrans();
                // ------------------------
                // Insertar nota de cargo
                // ------------------------
                string[] Parametros = { 
                                        "@Id_Emp"				
                                        ,"@Id_Cd"				
                                        ,"@Id_Nca"	
		                                ,"@Id_Cfe"
                                        ,"@Id_NcaSerie"
                                        ,"@Id_Reg"			
                                        ,"@Id_Ter"			
                                        ,"@Id_Rik"	
		                                ,"@Id_U"
                                        ,"@Id_Cte"			
                                        ,"@Nca_Tipo"			
                                        ,"@Nca_Fecha"		
                                        ,"@Id_Tm"			
                                        ,"@Id_Ban"
                                        ,"@Nca_CtaContable"
                                        ,"@Nca_Desgloce"		
                                        ,"@Nca_Referencia"	
                                        ,"@Nca_Notas"
                                        ,"@Nca_RSubtotal"	
                                        ,"@Nca_RIva"			
                                        ,"@Nca_RTotal"		
                                        ,"@Nca_Subtotal"		
                                        ,"@Nca_Iva"			
                                        ,"@Nca_Total"		
                                        ,"@Nca_Pagado"		
                                        ,"@Nca_FecPag"		
                                        ,"@Nca_Estatus"	
	                                    ,"@Nca_FPago"
                                        ,"@Nca_UDigitos"
                                      };
                object[] Valores = { 
                                        notaCargo.Id_Emp
                                        ,notaCargo.Id_Cd
                                        ,notaCargo.Id_Nca
                                        ,notaCargo.Id_Cfe
                                        ,notaCargo.Id_NcaSerie
                                        ,null
                                        ,notaCargo.Id_Ter
                                        ,notaCargo.Id_Rik
                                        ,notaCargo.Id_U
                                        ,notaCargo.Id_Cte
                                        ,notaCargo.Nca_Tipo
                                        ,notaCargo.Nca_Fecha
                                        ,notaCargo.Id_Tm
                                        ,notaCargo.Id_Ban
                                        ,notaCargo.Nca_CtaContable == string.Empty ? null : notaCargo.Nca_CtaContable
                                        ,notaCargo.Nca_Desgloce
                                        ,notaCargo.Nca_Referencia
                                        ,notaCargo.Nca_Notas
                                        ,0 //Nca_RSubtotal
                                        ,0 //Nca_RIva
                                        ,0 //Nca_RTotal
                                        ,notaCargo.Nca_Subtotal
                                        ,notaCargo.Nca_Iva
                                        ,notaCargo.Nca_Total
                                        ,null
                                        ,null
                                        ,notaCargo.Nca_Estatus
                                        ,notaCargo.Nca_FPago
                                        ,notaCargo.Nca_UDigitos
                                   };

                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd = CapaDatos.GenerarSqlCommand("spCapNotaCargo_Insertar", ref verificador, Parametros, Valores);
                notaCargo.Id_Nca = verificador; //clave de nota de cargo

                //INSERTA ADENDA CABECERA
                Parametros = new string[] {
                                            "@Id_Emp",			
                                            "@Id_Cd",				
                                            "@Id_Nca",
                                            "@Id_Cte",
                                            "@Id_AdeDet",
                                            "@Ade_Tipo",
                                            "@Ade_Valor",
                                            "@Contador" 
                                      };

                int cont = 0;
                foreach (AdendaDet adendaF in ListCab)
                {
                    Valores = new object[] { 
                        notaCargo.Id_Emp,
                        notaCargo.Id_Cd,
                        notaCargo.Id_Nca,
                        notaCargo.Id_Cte,
                        adendaF.Id_AdeDet,
                        adendaF.Tipo,
                        adendaF.Valor,
                        cont
                    };
                    cont++;
                    sqlcmd = CapaDatos.GenerarSqlCommand("spCapNCargoAdenda_Insertar", ref verificador, Parametros, Valores);
                }

                //INSERTAR ADENDA DETALLE
                Parametros = new string[] {
                                            "@Id_Emp",			
                                            "@Id_Cd",				
                                            "@Id_Nca",
                                            "@Id_Cte",
                                            "@Id_AdeDet",
                                            "@Ade_Tipo",
                                            "@Ade_Valor",
                                            "@Contador",
                                            "@Id_Prd",
                                            "@Id_Ter"
                                      };
                foreach (DataRow facturaDet in listaFacturaDet.Rows)
                {
                    for (int j = 11; j < listaFacturaDet.Columns.Count; j++)
                    {
                        Valores = new object[] { 
                            notaCargo.Id_Emp,
                            notaCargo.Id_Cd,
                            notaCargo.Id_Nca,
                            notaCargo.Id_Cte,
                            listaFacturaDet.Columns[j].ColumnName,
                            4,
                            facturaDet[j],
                            j,
                            facturaDet["Id_Prd"],
                            facturaDet["Id_Ter"]
                        };
                        sqlcmd = CapaDatos.GenerarSqlCommand("spCapNCargoAdenda_Insertar", ref verificador, Parametros, Valores);
                    }
                }

                // -----------------------------------------------------------------
                // Insertar detalle de nota de cargo
                // -----------------------------------------------------------------
                string[] ParametrosDet = { 
                                        "@Id_Emp"				
                                        ,"@Id_Cd"				
                                        ,"@Id_Nca"			
                                        ,"@Id_NcaDet"			
                                        ,"@Id_Ter"			
                                        ,"@Id_Rik"			
                                        ,"@Id_Prd"			
                                        ,"@Nca_Importe"	
                                      };
                int i = 1;
                foreach (DataRow notaCargoDet in listaFacturaDet.Rows)
                {
                    notaCargoDet["Id_NcaDet"] = i;
                    object[] ValoresDet = { 
                                        notaCargoDet["Id_Emp"]
                                        ,notaCargoDet["Id_Cd"]
                                        ,notaCargo.Id_Nca //Id de nota de cargo de la tabla de encabezado
                                        ,notaCargoDet["Id_NcaDet"]
                                        ,notaCargo.Id_Ter
                                        ,notaCargo.Id_Rik
                                        ,notaCargoDet["Id_Prd"]
                                        ,notaCargoDet["Nca_Importe"]
                                   };

                    sqlcmd = CapaDatos.GenerarSqlCommand("spCapNotaCargoDetalle_Insertar", ref verificador, ParametrosDet, ValoresDet);
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

        public void ModificarNotaCargo(ref NotaCargo notaCargo, string Conexion, ref int verificador, List<AdendaDet> ListCab, DataTable listaFacturaDet)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {
                CapaDatos.StartTrans();
                // ---------------------------
                // Modificar nota de cargo
                // ---------------------------
                string[] Parametros = { 
                                        "@Id_Emp"				
                                        ,"@Id_Cd"				
                                        ,"@Id_Nca"	
		                                ,"@Id_Cfe"
                                        ,"@Id_NcaSerie"
                                        ,"@Id_Reg"			
                                        ,"@Id_Ter"			
                                        ,"@Id_Rik"		
	                                    ,"@Id_U"
                                        ,"@Id_Cte"			
                                        ,"@Nca_Tipo"			
                                        ,"@Nca_Fecha"		
                                        ,"@Id_Tm"		
	                                    ,"@Id_Ban"
                                        ,"@Nca_CtaContable"
                                        ,"@Nca_Desgloce"		
                                        ,"@Nca_Referencia"	
                                        ,"@Nca_Notas"	
                                        ,"@Nca_Subtotal"		
                                        ,"@Nca_Iva"			
                                        ,"@Nca_Total"		
                                        ,"@Nca_Pagado"		
                                        ,"@Nca_FecPag"		
                                        ,"@Nca_Estatus"		
                                        ,"@Nca_FPago"
                                        ,"@Nca_UDigitos"
                                      };
                object[] Valores = { 
                                        notaCargo.Id_Emp
                                        ,notaCargo.Id_Cd
                                        ,notaCargo.Id_Nca
                                        ,notaCargo.Id_Cfe
                                        ,notaCargo.Id_NcaSerie
                                        ,null
                                        ,notaCargo.Id_Ter
                                        ,notaCargo.Id_Rik
                                        ,notaCargo.Id_U
                                        ,notaCargo.Id_Cte
                                        ,notaCargo.Nca_Tipo
                                        ,notaCargo.Nca_Fecha
                                        ,notaCargo.Id_Tm
                                        ,notaCargo.Id_Ban
                                        ,notaCargo.Nca_CtaContable == string.Empty ? null : notaCargo.Nca_CtaContable
                                        ,notaCargo.Nca_Desgloce
                                        ,notaCargo.Nca_Referencia
                                        ,notaCargo.Nca_Notas
                                        ,notaCargo.Nca_Subtotal
                                        ,notaCargo.Nca_Iva
                                        ,notaCargo.Nca_Total
                                        ,null
                                        ,null
                                        ,notaCargo.Nca_Estatus
                                        ,notaCargo.Nca_FPago
                                        ,notaCargo.Nca_UDigitos
                                   };

                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd = CapaDatos.GenerarSqlCommand("spCapNotaCargo_Modificar", ref verificador, Parametros, Valores);

                //MODIFICAR ADENDA CABECERA
                Parametros = new string[] {
                                            "@Id_Emp",			
                                            "@Id_Cd",				
                                            "@Id_Nca",
                                            "@Id_Cte",
                                            "@Id_AdeDet",
                                            "@Ade_Tipo",
                                            "@Ade_Valor",
                                            "@Contador" 
                                      };

                int cont = 0;
                foreach (AdendaDet adendaF in ListCab)
                {
                    Valores = new object[] { 
                        notaCargo.Id_Emp,
                        notaCargo.Id_Cd,
                        notaCargo.Id_Nca,
                        notaCargo.Id_Cte,
                        adendaF.Id_AdeDet,
                        adendaF.Tipo,
                        adendaF.Valor,
                        cont
                    };
                    cont++;
                    sqlcmd = CapaDatos.GenerarSqlCommand("spCapNCargoAdenda_Insertar", ref verificador, Parametros, Valores);
                }


                //MODIFICAR ADENDA DETALLE
                Parametros = new string[] {
                                            "@Id_Emp",			
                                            "@Id_Cd",				
                                            "@Id_Nca",
                                            "@Id_Cte",
                                            "@Id_AdeDet",
                                            "@Ade_Tipo",
                                            "@Ade_Valor",
                                            "@Contador",
                                            "@Id_Prd",
                                            "@Id_Ter"
                                      };
                foreach (DataRow facturaDet in listaFacturaDet.Rows)
                {
                    for (int j = 11; j < listaFacturaDet.Columns.Count; j++)
                    {
                        Valores = new object[] { 
                            notaCargo.Id_Emp,
                            notaCargo.Id_Cd,
                            notaCargo.Id_Nca,
                            notaCargo.Id_Cte,
                            listaFacturaDet.Columns[j].ColumnName,
                            4,
                            facturaDet[j],
                            j,
                            facturaDet["Id_Prd"],
                            facturaDet["Id_Ter"]
                        };
                        sqlcmd = CapaDatos.GenerarSqlCommand("spCapNCargoAdenda_Insertar", ref verificador, Parametros, Valores);
                    }
                }

                // -----------------------------------------------------------------
                // Insertar detalle de nota de cargo
                // -----------------------------------------------------------------
                string[] ParametrosDet = { 
                                        "@Id_Emp"				
                                        ,"@Id_Cd"				
                                        ,"@Id_Nca"			
                                        ,"@Id_NcaDet"			
                                        ,"@Id_Ter"			
                                        ,"@Id_Rik"			
                                        ,"@Id_Prd"			
                                        ,"@Nca_Importe"	
                                      };
                int i = 1;
                foreach (DataRow notaCargoDet in listaFacturaDet.Rows)
                {
                    notaCargoDet["Id_NcaDet"] = i;
                    object[] ValoresDet = { 
                                        notaCargoDet["Id_Emp"]
                                        ,notaCargoDet["Id_Cd"]
                                        ,notaCargo.Id_Nca //Id de nota de cargo de la tabla de encabezado
                                        ,notaCargoDet["Id_NcaDet"]
                                        ,notaCargoDet["Id_Ter"]
                                        ,notaCargoDet["Id_Rik"]
                                        ,notaCargoDet["Id_Prd"]
                                        ,notaCargoDet["Nca_Importe"]
                                   };

                    sqlcmd = CapaDatos.GenerarSqlCommand("spCapNotaCargoDetalle_Insertar", ref verificador, ParametrosDet, ValoresDet);
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

        public void EliminarNotaCargo(NotaCargo notaCargo, string Conexion, ref int verificador)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {
                CapaDatos.StartTrans();
                string[] Parametros = { 
	                                    "@Id_Emp"
                                        ,"@Id_Cd"
                                        ,"@Id_Nca"
                                        , "@Id_NcaSerie"
                                      };
                object[] Valores = { 
                                       notaCargo.Id_Emp
                                       ,notaCargo.Id_Cd
                                       ,notaCargo.Id_Nca
                                       ,notaCargo.Id_NcaSerie
                                   };

                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd = CapaDatos.GenerarSqlCommand("spCapNotaCargo_Eliminar", ref verificador, Parametros, Valores);
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

        public void ModificarNotaCargoSAT(NotaCargo notaCargo, string Conexion, ref int verificador)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {
                string[] Parametros = { 
                                        "@Id_Emp" 
                                        ,"@Id_Cd"
	                                    ,"@Id_Nca"
                                        ,"@Nca_Estatus"
                                        ,"@Nca_Sello"
                                        ,"@Nca_Xml"
                                        ,"@Nca_Pdf"
                                        ,"@Nca_FolioFiscal"
                                      };
                object[] Valores = { 
                                        notaCargo.Id_Emp
                                        ,notaCargo.Id_Cd
                                        ,notaCargo.Id_Nca
                                        ,notaCargo.Nca_Estatus
                                        ,notaCargo.Nca_Sello
                                        ,notaCargo.Nca_Xml
                                        ,notaCargo.Nca_Pdf
                                        ,notaCargo.Nca_FolioFiscal
                                   };

                // -----------------------------------------------------------------
                // Actualizar encabezado de la nota de credito
                // -----------------------------------------------------------------
                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd = CapaDatos.GenerarSqlCommand("spCapNotaCargo_ModificarSAT", ref verificador, Parametros, Valores);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarNotaCargo_Estatus(NotaCargo notaCargo, string Conexion, ref int verificador)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {
                CapaDatos.StartTrans();
                string[] Parametros = { 
	                                    "@Id_Emp"
                                        ,"@Id_Cd"
                                        ,"@Id_Nca"
                                        ,"@Nca_Estatus"
                                      };
                object[] Valores = { 
                                       notaCargo.Id_Emp
                                       ,notaCargo.Id_Cd
                                       ,notaCargo.Id_Nca
                                       ,notaCargo.Nca_Estatus
                                   };

                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd = CapaDatos.GenerarSqlCommand("spCapNotaCargo_ModificarEstatus", ref verificador, Parametros, Valores);
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

        public void Rastreo(NotaCargo nca, string Conexion, int tipoBusqueda)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_NcaSerie", "@Nca_FolioFiscal", "@tipoBusqueda" };
                object[] Valores = { nca.Id_Emp, nca.Id_Cd, nca.Id_NcaSerie, nca.Nca_FolioFiscal, tipoBusqueda };

                // ------------------------------------
                // Consultar encabezado de la factura
                // ------------------------------------
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spProRastreo_NCargo", ref dr, Parametros, Valores);
                if (dr.Read())
                {
                    nca.Id_Cte = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cte")));
                    nca.Cte_NomComercial = dr.GetValue(dr.GetOrdinal("Cte_NomComercial")).ToString();
                    nca.Nca_Iva = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Nca_Iva")));
                    nca.Nca_Subtotal = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Nca_Subtotal")));
                    nca.Nca_Pagado = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Nca_Pagado")));
                    nca.Nca_Estatus = dr.GetValue(dr.GetOrdinal("Nca_Estatus")).ToString();
                    nca.Nca_EstatusStr = Estatus(dr.GetValue(dr.GetOrdinal("Nca_Estatus")).ToString());
                    nca.Nca_Fecha = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Nca_Fecha")));
                    nca.Nca_FolioFiscal = dr.GetValue(dr.GetOrdinal("Nca_FolioFiscal")).ToString();
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaAdendaNota(Sesion sesion, int Id_Emp, int Id_Cd, int Id_Cte, ref List<NotaCargo> listNotaCargo)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(sesion.Emp_Cnx);
                string[] Parametros = { 
                                         "@Id_Emp",        
                                         "@Id_Cd",
                                         "@Id_Cte",
                                         "@Tipo"
                                      };
                object[] Valores = {                                       
                                       Id_Emp,
                                       Id_Cd, 
                                       Id_Cte,
                                       2// tipo 2 para notas de cargo
                                   };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapNotaCargo_ConsultaAdenda", ref dr, Parametros, Valores);

                NotaCargo notaCargo;
                while (dr.Read())
                {
                    notaCargo = new NotaCargo();
                    notaCargo.Ade_Campo = (string)dr.GetValue(dr.GetOrdinal("Ade_Campo"));
                    notaCargo.Ade_Longitud = (string)dr.GetValue(dr.GetOrdinal("Ade_Longitud"));
                    listNotaCargo.Add(notaCargo);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AgregarAdenda(NotaCargo notaCargo, Sesion sesion, ref int verificador)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(sesion.Emp_Cnx);
            try
            {
                CapaDatos.StartTrans();
                string[] Parametros = { 
	                                    "@Id_Emp"
                                        ,"@Id_Cd"
                                        ,"@Id_Cte"
                                        ,"@Tipo"
                                        //,"@Ade_Campo"
                                        ,"@Ade_Longitud"
                                      };
                object[] Valores = { 
                                       notaCargo.Id_Emp
                                       ,notaCargo.Id_Cd
                                       ,notaCargo.Id_Cte
                                       ,2// tipo 2 para notas de cargo
                                       //,notaCargo.Ade_Campo
                                       ,notaCargo.Ade_Longitud
                                   };

                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd = CapaDatos.GenerarSqlCommand("spCapNotaCargo_InsertarAdenda", ref verificador, Parametros, Valores);

                CapaDatos.CommitTrans();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                CapaDatos.RollBackTrans();
                throw ex;
            }
        }

        public List<NotaCargo> ConsultaProductosNotaCargo(ref NotaCargo notaCargo, string Conexion)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { 
                                          "@Id_Emp" 
                                          ,"@Id_Cd"
                                          ,"@Id_Nca"                                          
                                      };
                object[] Valores = { 
                                       notaCargo.Id_Emp
                                       ,notaCargo.Id_Cd
                                       ,notaCargo.Id_Nca
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapNotaCargo_ProductosEspecial", ref dr, Parametros, Valores);

                List<NotaCargo> listaNotaCargo = new List<NotaCargo>();
                while (dr.Read())
                {//Id_Clp, c.Id_Prd, c.Clp_descripcion
                    notaCargo = new NotaCargo();
                    notaCargo.Id_Prd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Prd")));
                    listaNotaCargo.Add(notaCargo);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
                return listaNotaCargo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaNotaCargoEspecialDetalle(ref List<NotaCargoDet> listaFacturaProductos, string Conexion, int id_Emp, int id_Cd, int id_Nca,string Id_NcaSerie, int id_Cte)
        {
            try
            {
                NotaCargoDet facturaDet = null;
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_Nca", "@Id_NcaSerie" };
                object[] Valores = { id_Emp, id_Cd, id_Nca, Id_NcaSerie };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapNCargoEspecialDetalle_Consultar", ref dr, Parametros, Valores);
                
                while (dr.Read())
                {
                    facturaDet = new NotaCargoDet();

                    facturaDet.Id_Emp = id_Emp;
                    facturaDet.Id_Cd = id_Cd;
                    facturaDet.Id_Nca = 0;
                    facturaDet.Id_NcaDet = 0;
                    facturaDet.Id_CteExt = id_Cte;
                    facturaDet.Id_Prd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Prd")));

                    facturaDet.Nca_Importe = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Nca_Importe"))) ? 0 : Convert.ToSingle(dr.GetValue(dr.GetOrdinal("Nca_Importe")));
                    
                    //datos del producto de la orden de compra
                    facturaDet.Producto = new Producto();
                    facturaDet.Producto.Id_Prd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Prd")));
                    facturaDet.Producto.Id_Emp = id_Emp;
                    facturaDet.Producto.Id_Cd = id_Cd;
                    facturaDet.Producto.Id_PrdEsp = dr.IsDBNull(dr.GetOrdinal("Id_PrdEsp")) ? string.Empty : dr.GetValue(dr.GetOrdinal("Id_PrdEsp")).ToString();
                    facturaDet.Producto.Prd_Descripcion = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("NcaEsp_Descripcion"))) ? string.Empty : dr.GetValue(dr.GetOrdinal("NcaEsp_Descripcion")).ToString();
                    facturaDet.Producto.Prd_DescripcionEspecial = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("NcaEsp_DescripcionEspecial"))) ? string.Empty : dr.GetValue(dr.GetOrdinal("NcaEsp_DescripcionEspecial")).ToString();
                    facturaDet.Producto.Prd_Presentacion = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("NcaEsp_Presentacion"))) ? string.Empty : dr.GetValue(dr.GetOrdinal("NcaEsp_Presentacion")).ToString();
                    facturaDet.Producto.Prd_UniNe = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("NcaEsp_Unidades"))) ? string.Empty : dr.GetValue(dr.GetOrdinal("NcaEsp_Unidades")).ToString();
                    facturaDet.Producto.Prd_UniNs = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("NcaEsp_Unidades"))) ? string.Empty : dr.GetValue(dr.GetOrdinal("NcaEsp_Unidades")).ToString();
                    facturaDet.Clp_Release = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("NcaEsp_Release"))) ? string.Empty : dr.GetValue(dr.GetOrdinal("NcaEsp_Release")).ToString();

                    listaFacturaProductos.Add(facturaDet);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarAdenda(int Id_Emp, int Id_Cd_Ver, int Id_Nca, string Id_NcaSerie,string Tipo1, string Tipo2, ref List<AdendaDet> listCab, ref List<AdendaDet> listDet, string Conexion)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { 
                                          "@Id_Emp" ,
                                          "@Id_Cd",
                                          "@Id_Nca",
                                          "@Id_NcaSerie",
                                          "@Ade_Tipo1",
                                          "@Ade_Tipo2"

                                      };
                object[] Valores = { 
                                       Id_Emp,
                                       Id_Cd_Ver,
                                       Id_Nca,
                                       Id_NcaSerie,
                                       Tipo1,
                                       Tipo2
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapNCargoAdenda_Consultar", ref dr, Parametros, Valores);

                AdendaDet adendaDet;
                while (dr.Read())
                {
                    adendaDet = new AdendaDet();
                    adendaDet.Campo = Convert.ToString(dr.GetValue(dr.GetOrdinal("Ade_Campo")));
                    adendaDet.Nodo = Convert.ToString(dr.GetValue(dr.GetOrdinal("Ade_Nodo")));
                    adendaDet.Longitud = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Ade_Longitud")));
                    adendaDet.Valor = Convert.ToString(dr.GetValue(dr.GetOrdinal("Ade_Valor")));
                    adendaDet.Id_AdeDet = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_AdeDet")));
                    adendaDet.Id_Prd = (int?)dr.GetValue(dr.GetOrdinal("Id_Prd"));
                    adendaDet.Id_Ter = (int?)dr.GetValue(dr.GetOrdinal("Id_Ter"));
                    adendaDet.Tipo = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Ade_Tipo")));
                    adendaDet.Requerido = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Ade_Requerido")));
                    if (adendaDet.Tipo % 2 != 0)
                    {
                        listCab.Add(adendaDet);
                    }
                    else
                    {
                        listDet.Add(adendaDet);
                    }
                }
                dr.Close();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ArchivoPdf_Xml(ref NotaCargo notaCargo, string Conexion)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_Nca" , "@Id_NcaSerie"};
                object[] Valores = { notaCargo.Id_Emp, notaCargo.Id_Cd, notaCargo.Id_Nca, notaCargo.Id_NcaSerie };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapNotaCargo_PDF_XML", ref dr, Parametros, Valores);
                //byte[] filebytes = null;
                while (dr.Read())
                {
                    notaCargo = new NotaCargo();
                    notaCargo.Nca_Xml = Convert.ToString(dr.GetValue(dr.GetOrdinal("Nca_Xml")));
                }
                dr.Close();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ValidaMontosImpresion(NotaCargo nc, int Id_Cd, int Id_Emp, int iTipoDocumento, string conexion, ref bool bVerificador)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(conexion);
                string[] parametros = { 
                                          "@Id_Doc",
                                          "@Id_Cd",
                                          "@Id_Emp",
                                          "@iTipoDocumento"
                                      };
                object[] Valores = {
                                      nc.Id_Nca,
                                      Id_Cd,
                                      Id_Emp,
                                      iTipoDocumento
                                   };

                int verificador = 0;
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapDocumentoValido_Impresion", ref verificador, parametros, Valores);
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

                if (verificador == 0)
                    bVerificador = false;
                else
                    bVerificador = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }     
    }
}
