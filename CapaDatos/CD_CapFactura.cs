using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CapaEntidad;
using System.Data.SqlClient;
using System.Data;

namespace CapaDatos
{
    public class CD_CapFactura
    {
        #region Variables

        string[] Parametros = {
                                        "@Id_Emp"
                                        ,"@Id_Cd"
                                        ,"@Id_Fac"
                                        ,"@Id_Cfe"
                                        ,"@Id_FacSerie"
                                        ,"@Id_U"
                                        ,"@Id_Tm"
                                        ,"@Fac_PedNum"
                                        ,"@Fac_PedDesc"
                                        ,"@Fac_Req"
                                        ,"@Fac_Fecha"
                                        ,"@Id_Cte"
                                        ,"@Id_Ter"
                                        ,"@Id_Rik"
                                        ,"@Id_Mon"
                                        ,"@Fac_DesgIva"
                                        ,"@Fac_RetIva"
                                        ,"@Fac_CteCalle"
                                        ,"@Fac_CteNumero"
                                        ,"@Fac_CteNumeroInterior"
                                        ,"@Fac_CteCp"
                                        ,"@Fac_CteColonia"
                                        ,"@Fac_CteMunicipio"
                                        ,"@Fac_CteEstado"
                                        ,"@Fac_CteRfc"
                                        ,"@Fac_CteTel"
                                        ,"@Fac_OrdEntrega"
                                        ,"@Fac_CondEntrega"
                                        ,"@Fac_NumEntrega"
                                        ,"@Fac_Notas"
                                        ,"@Fac_DescPorcen1"
                                        ,"@Fac_Desc1"
                                        ,"@Fac_DescPorcen2"
                                        ,"@Fac_Desc2"
                                        ,"@Fac_Tipo"
                                        ,"@Fac_Conducto"
                                        ,"@Fac_NumeroGuia"
                                        ,"@Fac_CanNum"
                                        ,"@Fac_FecCan"
                                        ,"@Fac_Pagado"
                                        ,"@Fac_FecPag"
                                        ,"@Fac_Importe"
                                        ,"@Fac_SubTotal"
                                        ,"@Fac_ImporteIva"
                                        ,"@Fac_ImporteRetencion"
                                        ,"@Fac_Estatus"

                                        ,"@Id_Ped"
                                        ,"@Fac_Refactura"
                                        ,"@Fac_Contacto"
                                        //,"@Id_Tm_Rem"
                                        ,"@Fac_FPago"
                                        ,"@Fac_UDigitos"
                                        ,"@sValoresOriginales"
                                        ,"@Fac_FechaRef" // Se agregan 5 campos , RFH 02032018
                                        ,"@Fac_IdUsuRef"
                                        ,"@Fac_IdCausaRef"
                                        ,"@Fac_TipoRef"
                                        ,"@Fac_EsRefactura"
                                      };

        string[] ParametrosAparInprod = {
                                        "@Id_Emp"
                                        ,"@Id_Cd"
                                        ,"@Id_Fac"
                                        ,"@Id_Cfe"
                                        ,"@Id_FacSerie"
                                        ,"@Id_U"
                                        ,"@Id_Tm"
                                        ,"@Fac_PedNum"
                                        ,"@Fac_PedDesc"
                                        ,"@Fac_Req"
                                        ,"@Fac_Fecha"
                                        ,"@Id_Cte"
                                        ,"@Id_Ter"
                                        ,"@Id_Rik"
                                        ,"@Id_Mon"
                                        ,"@Fac_DesgIva"
                                        ,"@Fac_RetIva"
                                        ,"@Fac_CteCalle"
                                        ,"@Fac_CteNumero"
                                        ,"@Fac_CteNumeroInterior"
                                        ,"@Fac_CteCp"
                                        ,"@Fac_CteColonia"
                                        ,"@Fac_CteMunicipio"
                                        ,"@Fac_CteEstado"
                                        ,"@Fac_CteRfc"
                                        ,"@Fac_CteTel"
                                        ,"@Fac_OrdEntrega"
                                        ,"@Fac_CondEntrega"
                                        ,"@Fac_NumEntrega"
                                        ,"@Fac_Notas"
                                        ,"@Fac_DescPorcen1"
                                        ,"@Fac_Desc1"
                                        ,"@Fac_DescPorcen2"
                                        ,"@Fac_Desc2"
                                        ,"@Fac_Tipo"
                                        ,"@Fac_Conducto"
                                        ,"@Fac_NumeroGuia"
                                        ,"@Fac_CanNum"
                                        ,"@Fac_FecCan"
                                        ,"@Fac_Pagado"
                                        ,"@Fac_FecPag"
                                        ,"@Fac_Importe"
                                        ,"@Fac_SubTotal"
                                        ,"@Fac_ImporteIva"
                                        ,"@Fac_ImporteRetencion"
                                        ,"@Fac_Estatus"
                                        ,"@Id_Ped"
                                        ,"@Fac_Refactura"
                                        ,"@Fac_Contacto"
                                      };

        #endregion

        public void ConsultaFacturaNacional(ref Factura factura, string Conexion)
        {
            SqlDataReader dr = null;
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

            string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_Fac" };
            object[] Valores = { factura.Id_Emp, factura.Id_Cd, factura.Id_Fac };

            SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapFacturaNacional_Consultar", ref dr, Parametros, Valores);

            if (dr.Read())
            {
                factura.Id_Emp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Emp")));
                factura.Id_Cd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cd")));
                factura.Id_Fac = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Fac")));
                factura.Id_Cte = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cte")));
                if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_NomComercial")))) factura.Cte_NomComercial = string.Empty; else factura.Cte_NomComercial = dr.GetValue(dr.GetOrdinal("Cte_NomComercial")).ToString();

                if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Fac_CteCalle")))) factura.Fac_CteCalle = string.Empty; else factura.Fac_CteCalle = dr.GetValue(dr.GetOrdinal("Fac_CteCalle")).ToString();
                if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Fac_CteNumero")))) factura.Fac_CteNumero = string.Empty; else factura.Fac_CteNumero = dr.GetValue(dr.GetOrdinal("Fac_CteNumero")).ToString();
                if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Fac_CteColonia")))) factura.Fac_CteColonia = string.Empty; else factura.Fac_CteColonia = dr.GetValue(dr.GetOrdinal("Fac_CteColonia")).ToString();
                if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Fac_CteMunicipio")))) factura.Fac_CteMunicipio = string.Empty; else factura.Fac_CteMunicipio = dr.GetValue(dr.GetOrdinal("Fac_CteMunicipio")).ToString();
                if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Fac_CteEstado")))) factura.Fac_CteEstado = string.Empty; else factura.Fac_CteEstado = dr.GetValue(dr.GetOrdinal("Fac_CteEstado")).ToString();
                if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Fac_CteRfc")))) factura.Fac_CteRfc = string.Empty; else factura.Fac_CteRfc = dr.GetValue(dr.GetOrdinal("Fac_CteRfc")).ToString();
                if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Fac_CteCp")))) factura.Fac_CteCp = string.Empty; else factura.Fac_CteCp = dr.GetValue(dr.GetOrdinal("Fac_CteCp")).ToString();
                if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Fac_CteAdeNombre")))) factura.Fac_CteAdeNombre = string.Empty; else factura.Fac_CteAdeNombre = dr.GetValue(dr.GetOrdinal("Fac_CteAdeNombre")).ToString();
                //if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Fac_CteAdeId")))) factura.Fac_CteAdeId = 0; else factura.Fac_CteAdeId = Int32.Parse(dr.GetValue(dr.GetOrdinal("Fac_CteAdeNombre")).ToString());
            }
            else
            {
                factura = null;
            }
            dr.Close();

            CapaDatos.LimpiarSqlcommand(ref sqlcmd);
        }

        public void ConsultaFactura(ref Factura factura, ref List<FacturaDet> listaFacturaDet, string Conexion)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_Fac" };
                object[] Valores = { factura.Id_Emp, factura.Id_Cd, factura.Id_Fac };

                // ------------------------------------
                // Consultar encabezado de la factura
                // ------------------------------------
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapFactura_Consultar", ref dr, Parametros, Valores);
                if (dr.Read())
                {
                    factura.Id_Emp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Emp")));
                    factura.Id_Cd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cd")));
                    factura.Id_Fac = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Fac")));
                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Cfe")))) factura.Id_Cfe = null; else factura.Id_Cfe = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cfe")));
                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_FacSerie")))) factura.Id_FacSerie = string.Empty; else factura.Id_FacSerie = dr.GetValue(dr.GetOrdinal("Id_FacSerie")).ToString();
                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Serie")))) factura.Serie = string.Empty; else factura.Serie = dr.GetValue(dr.GetOrdinal("Serie")).ToString();
                    factura.Id_U = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_U")));
                    factura.Id_Tm = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Tm")));

                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Fac_PedNum")))) factura.Fac_PedNum = null; else factura.Fac_PedNum = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Fac_PedNum")));
                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Fac_PedDesc")))) factura.Fac_PedDesc = null; else factura.Fac_PedDesc = dr.GetValue(dr.GetOrdinal("Fac_PedDesc")).ToString();

                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Fac_Req")))) factura.Fac_Req = null; else factura.Fac_Req = dr.GetValue(dr.GetOrdinal("Fac_Req")).ToString();
                    factura.Fac_Fecha = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Fac_Fecha")));
                    factura.Id_Cte = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cte")));
                    if (dr.IsDBNull(dr.GetOrdinal("Fac_FechaHr")))
                        factura.Fac_FechaHr = dr.GetDateTime(dr.GetOrdinal("Fac_Fecha"));
                    else
                        factura.Fac_FechaHr = dr.GetDateTime(dr.GetOrdinal("Fac_FechaHr"));
                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_NomComercial")))) factura.Cte_NomComercial = string.Empty; else factura.Cte_NomComercial = dr.GetValue(dr.GetOrdinal("Cte_NomComercial")).ToString();
                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_Email")))) factura.Cte_Email = string.Empty; else factura.Cte_Email = dr.GetValue(dr.GetOrdinal("Cte_Email")).ToString();
                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Ter")))) factura.Id_Ter = null; else factura.Id_Ter = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Ter")));
                    factura.Ter_Nombre = dr.IsDBNull(dr.GetOrdinal("Ter_Nombre")) ? "" : dr.GetValue(dr.GetOrdinal("Ter_Nombre")).ToString();
                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Rik")))) factura.Id_Rik = null; else factura.Id_Rik = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Rik")));
                    factura.Rik_Nombre = dr.GetValue(dr.GetOrdinal("Rik_Nombre")).ToString();
                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Emb")))) factura.Id_Emb = null; else factura.Id_Emb = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Emb")));
                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Mon")))) factura.Id_Mon = null; else factura.Id_Mon = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Mon")));
                    factura.Mon_Unidad = dr.IsDBNull(dr.GetOrdinal("Mon_Unidad")) ? "" : dr.GetValue(dr.GetOrdinal("Mon_Unidad")).ToString();
                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Mon_Descripcion")))) factura.Mon_Descripcion = null; else factura.Mon_Descripcion = dr.GetValue(dr.GetOrdinal("Mon_Descripcion")).ToString();
                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Mon_TipCambio")))) factura.Mon_TipCambio = 0; else factura.Mon_TipCambio = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Mon_TipCambio")));
                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Fac_DesgIva")))) factura.Fac_DesgIva = null; else factura.Fac_DesgIva = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Fac_DesgIva")));
                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Fac_RetIva")))) factura.Fac_RetIva = null; else factura.Fac_RetIva = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Fac_RetIva")));

                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Fac_CteCalle")))) factura.Fac_CteCalle = string.Empty; else factura.Fac_CteCalle = dr.GetValue(dr.GetOrdinal("Fac_CteCalle")).ToString();
                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Fac_CteNumero")))) factura.Fac_CteNumero = string.Empty; else factura.Fac_CteNumero = dr.GetValue(dr.GetOrdinal("Fac_CteNumero")).ToString();
                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Fac_CteNumeroInterior")))) factura.Fac_CteNumeroInterior = string.Empty; else factura.Fac_CteNumeroInterior = dr.GetValue(dr.GetOrdinal("Fac_CteNumeroInterior")).ToString();
                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Fac_CteCp")))) factura.Fac_CteCp = string.Empty; else factura.Fac_CteCp = dr.GetValue(dr.GetOrdinal("Fac_CteCp")).ToString();
                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Fac_CteColonia")))) factura.Fac_CteColonia = string.Empty; else factura.Fac_CteColonia = dr.GetValue(dr.GetOrdinal("Fac_CteColonia")).ToString();
                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Fac_CteMunicipio")))) factura.Fac_CteMunicipio = string.Empty; else factura.Fac_CteMunicipio = dr.GetValue(dr.GetOrdinal("Fac_CteMunicipio")).ToString();
                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Fac_CteEstado")))) factura.Fac_CteEstado = string.Empty; else factura.Fac_CteEstado = dr.GetValue(dr.GetOrdinal("Fac_CteEstado")).ToString();
                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Fac_CteRfc")))) factura.Fac_CteRfc = string.Empty; else factura.Fac_CteRfc = dr.GetValue(dr.GetOrdinal("Fac_CteRfc")).ToString();
                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Fac_CteTel")))) factura.Fac_CteTel = string.Empty; else factura.Fac_CteTel = dr.GetValue(dr.GetOrdinal("Fac_CteTel")).ToString();

                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Fac_OrdEntrega")))) factura.Fac_OrdEntrega = null; else factura.Fac_OrdEntrega = dr.GetValue(dr.GetOrdinal("Fac_OrdEntrega")).ToString();
                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Fac_NumeroGuia")))) factura.Fac_NumeroGuia = string.Empty; else factura.Fac_NumeroGuia = dr.GetValue(dr.GetOrdinal("Fac_NumeroGuia")).ToString();
                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Fac_CondEntrega")))) factura.Fac_CondEntrega = string.Empty; else factura.Fac_CondEntrega = dr.GetValue(dr.GetOrdinal("Fac_CondEntrega")).ToString();
                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Fac_NumEntrega")))) factura.Fac_NumEntrega = null; else factura.Fac_NumEntrega = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Fac_NumEntrega")));
                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Fac_Notas")))) factura.Fac_Notas = string.Empty; else factura.Fac_Notas = dr.GetValue(dr.GetOrdinal("Fac_Notas")).ToString();
                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Fac_DescPorcen1")))) factura.Fac_DescPorcen1 = null; else factura.Fac_DescPorcen1 = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Fac_DescPorcen1")));
                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Fac_Desc1")))) factura.Fac_Desc1 = string.Empty; else factura.Fac_Desc1 = dr.GetValue(dr.GetOrdinal("Fac_Desc1")).ToString();
                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Fac_DescPorcen2")))) factura.Fac_DescPorcen2 = null; else factura.Fac_DescPorcen2 = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Fac_DescPorcen2")));
                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Fac_Desc2")))) factura.Fac_Desc2 = string.Empty; else factura.Fac_Desc2 = dr.GetValue(dr.GetOrdinal("Fac_Desc2")).ToString();

                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Fac_Tipo")))) factura.Fac_Tipo = string.Empty; else factura.Fac_Tipo = dr.GetValue(dr.GetOrdinal("Fac_Tipo")).ToString();
                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Fac_Conducto")))) factura.Fac_Conducto = string.Empty; else factura.Fac_Conducto = dr.GetValue(dr.GetOrdinal("Fac_Conducto")).ToString();
                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Fac_NumeroGuia")))) factura.Fac_NumeroGuia = string.Empty; else factura.Fac_NumeroGuia = dr.GetValue(dr.GetOrdinal("Fac_NumeroGuia")).ToString();
                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Fac_CanNum")))) factura.Fac_CanNum = null; else factura.Fac_CanNum = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Fac_CanNum")));
                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Fac_FecCan")))) factura.Fac_FecCan = null; else factura.Fac_FecCan = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Fac_FecCan")));
                    double pagado = 0;
                    double subtotal = 0;
                    double iva = 0;
                    double Retencion = 0;
                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Fac_Pagado")))) factura.Fac_Pagado = null;
                    else factura.Fac_Pagado = pagado = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Fac_Pagado")));

                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Fac_FecPag")))) factura.Fac_FecPag = null; else factura.Fac_FecPag = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Fac_FecPag")));
                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Fac_Importe")))) factura.Fac_Importe = null; else factura.Fac_Importe = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Fac_Importe")));
                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Fac_SubTotal"))))
                        factura.Fac_SubTotal = null;
                    else
                        factura.Fac_SubTotal = subtotal = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Fac_SubTotal")));
                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Fac_ImporteIva"))))
                        factura.Fac_ImporteIva = null;
                    else
                        factura.Fac_ImporteIva = iva = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Fac_ImporteIva")));

                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Fac_ImporteRetencion")))) factura.Fac_ImporteRetencion = null; else factura.Fac_ImporteRetencion = Retencion = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Fac_ImporteRetencion")));

                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Fac_Sello")))) factura.Fac_Sello = string.Empty; else factura.Fac_Sello = dr.GetValue(dr.GetOrdinal("Fac_Sello")).ToString();
                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Fac_Estatus")))) factura.Fac_Estatus = string.Empty; else factura.Fac_Estatus = dr.GetValue(dr.GetOrdinal("Fac_Estatus")).ToString();
                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Fac_Estatus")))) factura.Fac_Estatus = string.Empty; else factura.Fac_EstatusStr = Estatus(dr.GetValue(dr.GetOrdinal("Fac_Estatus")).ToString());
                    factura.Fac_Saldo = (subtotal + iva) - pagado;

                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("TienePagos")))) factura.TienePagos = false; else factura.TienePagos = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("TienePagos")));
                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Es")))) factura.Id_Es = null; else factura.Id_Es = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Es")));
                    factura.Fac_Contacto = dr.GetValue(dr.GetOrdinal("Fac_Contacto")).ToString();
                    factura.Folio_cancelacion = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Folio_cancelacion")));

                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_Calle")))) factura.Consig_Dir = string.Empty; else factura.Consig_Dir = dr.GetValue(dr.GetOrdinal("Cte_Calle")).ToString();
                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_Numero")))) factura.Consig_Num = string.Empty; else factura.Consig_Num = dr.GetValue(dr.GetOrdinal("Cte_Numero")).ToString();
                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_Colonia")))) factura.Consig_colonia = string.Empty; else factura.Consig_colonia = dr.GetValue(dr.GetOrdinal("Cte_Colonia")).ToString();
                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_Municipio")))) factura.Consig_mun = string.Empty; else factura.Consig_mun = dr.GetValue(dr.GetOrdinal("Cte_Municipio")).ToString();
                    factura.Fac_Refactura = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Fac_Refactura"))) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("Fac_Refactura")));
                    factura.Fac_FPago = dr.GetValue(dr.GetOrdinal("Fac_FPago")).ToString();
                    factura.Fac_UDigitos = dr.GetValue(dr.GetOrdinal("Fac_UDigitos")).ToString();
                    factura.U_Nombre = dr.GetValue(dr.GetOrdinal("U_Nombre")).ToString();

                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Fac_XML"))))
                    {
                        factura.Fac_Xml = null;
                    }
                    else
                    {
                        factura.Fac_Xml = (object)dr.GetValue(dr.GetOrdinal("Fac_XML"));
                    }
                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Fac_FolioFiscalCN")))) factura.Fac_FolioFiscalCN = string.Empty; else factura.Fac_FolioFiscalCN = dr.GetValue(dr.GetOrdinal("Fac_FolioFiscalCN")).ToString();
                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Fac_FolioCN")))) { factura.Fac_FolioCN = null; } else { factura.Fac_FolioCN = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Fac_FolioCN"))); }

                }


                // ------------------------------------
                // Consultar detalle de la factura
                // ------------------------------------
                dr.Close();
                sqlcmd = CapaDatos.GenerarSqlCommand("spCapFacturaDetalle_Consultar", ref dr, Parametros, Valores);
                while (dr.Read())
                {
                    FacturaDet facturaDet = new FacturaDet();
                    facturaDet.Id_Emp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Emp")));
                    facturaDet.Id_Cd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cd")));
                    facturaDet.Id_Fac = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Fac")));
                    facturaDet.Id_FacDet = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_FacDet")));
                    facturaDet.Id_Ter = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Ter")));
                    facturaDet.Id_TerStr = dr.GetValue(dr.GetOrdinal("Id_TerStr")).ToString();
                    facturaDet.Id_Prd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Prd")));

                    if (!Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Rem"))))
                        facturaDet.Id_Rem = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Rem")));

                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Cte"))))
                        facturaDet.Id_CteExt = null;
                    else
                        facturaDet.Id_CteExt = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cte")));

                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_CteStr"))))
                        facturaDet.Id_CteExtStr = string.Empty;
                    else
                        facturaDet.Id_CteExtStr = dr.GetValue(dr.GetOrdinal("Id_CteStr")).ToString();

                    facturaDet.Fac_Cant = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Fac_Cant")));
                    facturaDet.Fac_Precio = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Fac_Precio")));

                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Fac_Asignar"))))
                        facturaDet.Fac_Asignar = null;
                    else
                        facturaDet.Fac_Asignar = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Fac_Asignar")));

                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Fac_Devolucion"))))
                        facturaDet.Fac_Devolucion = false;
                    else
                        facturaDet.Fac_Devolucion = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Fac_Devolucion")));

                    facturaDet.Producto = new Producto();
                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Descripcion"))))
                        facturaDet.Producto.Prd_Descripcion = string.Empty;
                    else
                        facturaDet.Producto.Prd_Descripcion = dr.GetValue(dr.GetOrdinal("Prd_Descripcion")).ToString();

                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Presentacion"))))
                        facturaDet.Producto.Prd_Presentacion = string.Empty;
                    else
                        facturaDet.Producto.Prd_Presentacion = dr.GetValue(dr.GetOrdinal("Prd_Presentacion")).ToString();
                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_UniNe"))))
                        facturaDet.Producto.Prd_UniNe = string.Empty;
                    else
                        facturaDet.Producto.Prd_UniNe = dr.GetValue(dr.GetOrdinal("Prd_UniNs")).ToString();
                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_UniNs"))))
                        facturaDet.Producto.Prd_UniNs = string.Empty;
                    else
                        facturaDet.Producto.Prd_UniNs = dr.GetValue(dr.GetOrdinal("Prd_UniNs")).ToString();
                    facturaDet.Prd_Unis = Convert.ToString(dr.GetValue(dr.GetOrdinal("Prd_UniNs")));
                    facturaDet.Prd_Agrupador = dr.GetInt32(dr.GetOrdinal("Prd_AgrupadoSpo"));
                    facturaDet.Producto.U_Descripcion = dr.GetValue(dr.GetOrdinal("Uni_Descripcion")).ToString();

                    if (!Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Fac_MultiplicadorGarantia"))))
                        facturaDet.Multiplicador = Convert.ToSingle(dr.GetValue(dr.GetOrdinal("Fac_MultiplicadorGarantia")));

                    if (!Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Fac_VentaFacturadaGarantia"))))
                        facturaDet.Precio_Venta = Convert.ToSingle(dr.GetValue(dr.GetOrdinal("Fac_VentaFacturadaGarantia")));

                    if (!Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Fac_TotalesGarantia"))))
                        facturaDet.Totales = Convert.ToSingle(dr.GetValue(dr.GetOrdinal("Fac_TotalesGarantia")));


                    facturaDet.Fac_ClaveProdServ = dr.GetValue(dr.GetOrdinal("Fac_ClaveProdServ")).ToString();
                    facturaDet.Fac_ClaveUnidad = dr.GetValue(dr.GetOrdinal("Fac_ClaveUnidad")).ToString();

                    listaFacturaDet.Add(facturaDet);
                }


                // ----------------------------------------------------
                // Consultar remisiones de la factura, si las tiene
                // ----------------------------------------------------
                dr.Close();
                sqlcmd = CapaDatos.GenerarSqlCommand("spCapFacturaRemisiones_Consultar", ref dr, Parametros, Valores);
                while (dr.Read())
                {
                    int Id_Rem = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Rem")));
                    int Id_Prd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Prd")));
                    int Fac_Cant = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Fac_Cant")));
                    int Rem_Cant = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Rem_Cant")));

                    //actualizar remision en el detalle de la factura
                    foreach (FacturaDet fd in listaFacturaDet)
                    {
                        if (fd.Id_Prd == Id_Prd && fd.Fac_Cant == Fac_Cant && fd.Id_Rem == Id_Rem)
                            fd.Rem_Cant = Rem_Cant;
                    }
                }
                //if (!dr.Read())
                //{
                //    foreach (FacturaDet fd in listaFacturaDet)
                //    {
                //           fd.Rem_Cant = 0;
                //    }
                //}

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ConsultaFacturaOtraBD(ref Factura factura, string serie, string Conexion)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_Fac", "@Serie" };
                object[] Valores = { factura.Id_Emp, factura.Id_Cd, factura.Id_Fac, serie };

                // ------------------------------------
                // Consultar encabezado de la factura
                // ------------------------------------
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapFactura_ConsultarOtraBD", ref dr, Parametros, Valores);
                if (dr.Read())
                {
                    factura.Id_Emp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Emp")));
                    factura.Id_Cd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cd")));
                    factura.Id_Fac = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Fac")));
                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Cfe")))) factura.Id_Cfe = null; else factura.Id_Cfe = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cfe")));
                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_FacSerie")))) factura.Id_FacSerie = string.Empty; else factura.Id_FacSerie = dr.GetValue(dr.GetOrdinal("Id_FacSerie")).ToString();
                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Serie")))) factura.Serie = string.Empty; else factura.Serie = dr.GetValue(dr.GetOrdinal("Serie")).ToString();
                    factura.Id_U = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_U")));
                    factura.Id_Tm = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Tm")));

                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Fac_PedNum")))) factura.Fac_PedNum = null; else factura.Fac_PedNum = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Fac_PedNum")));
                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Fac_PedDesc")))) factura.Fac_PedDesc = null; else factura.Fac_PedDesc = dr.GetValue(dr.GetOrdinal("Fac_PedDesc")).ToString();

                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Fac_Req")))) factura.Fac_Req = null; else factura.Fac_Req = dr.GetValue(dr.GetOrdinal("Fac_Req")).ToString();
                    factura.Fac_Fecha = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Fac_Fecha")));
                    factura.Id_Cte = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cte")));
                    if (dr.IsDBNull(dr.GetOrdinal("Fac_FechaHr")))
                        factura.Fac_FechaHr = dr.GetDateTime(dr.GetOrdinal("Fac_Fecha"));
                    else
                        factura.Fac_FechaHr = dr.GetDateTime(dr.GetOrdinal("Fac_FechaHr"));
                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_NomComercial")))) factura.Cte_NomComercial = string.Empty; else factura.Cte_NomComercial = dr.GetValue(dr.GetOrdinal("Cte_NomComercial")).ToString();
                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_Email")))) factura.Cte_Email = string.Empty; else factura.Cte_Email = dr.GetValue(dr.GetOrdinal("Cte_Email")).ToString();
                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Ter")))) factura.Id_Ter = null; else factura.Id_Ter = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Ter")));
                    factura.Ter_Nombre = dr.IsDBNull(dr.GetOrdinal("Ter_Nombre")) ? "" : dr.GetValue(dr.GetOrdinal("Ter_Nombre")).ToString();
                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Rik")))) factura.Id_Rik = null; else factura.Id_Rik = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Rik")));
                    factura.Rik_Nombre = dr.GetValue(dr.GetOrdinal("Rik_Nombre")).ToString();
                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Emb")))) factura.Id_Emb = null; else factura.Id_Emb = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Emb")));
                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Mon")))) factura.Id_Mon = null; else factura.Id_Mon = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Mon")));
                    factura.Mon_Unidad = dr.IsDBNull(dr.GetOrdinal("Mon_Unidad")) ? "" : dr.GetValue(dr.GetOrdinal("Mon_Unidad")).ToString();
                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Mon_Descripcion")))) factura.Mon_Descripcion = null; else factura.Mon_Descripcion = dr.GetValue(dr.GetOrdinal("Mon_Descripcion")).ToString();
                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Mon_TipCambio")))) factura.Mon_TipCambio = 0; else factura.Mon_TipCambio = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Mon_TipCambio")));
                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Fac_DesgIva")))) factura.Fac_DesgIva = null; else factura.Fac_DesgIva = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Fac_DesgIva")));
                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Fac_RetIva")))) factura.Fac_RetIva = null; else factura.Fac_RetIva = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Fac_RetIva")));

                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Fac_CteCalle")))) factura.Fac_CteCalle = string.Empty; else factura.Fac_CteCalle = dr.GetValue(dr.GetOrdinal("Fac_CteCalle")).ToString();
                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Fac_CteNumero")))) factura.Fac_CteNumero = string.Empty; else factura.Fac_CteNumero = dr.GetValue(dr.GetOrdinal("Fac_CteNumero")).ToString();
                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Fac_CteNumeroInterior")))) factura.Fac_CteNumeroInterior = string.Empty; else factura.Fac_CteNumeroInterior = dr.GetValue(dr.GetOrdinal("Fac_CteNumeroInterior")).ToString();
                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Fac_CteCp")))) factura.Fac_CteCp = string.Empty; else factura.Fac_CteCp = dr.GetValue(dr.GetOrdinal("Fac_CteCp")).ToString();
                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Fac_CteColonia")))) factura.Fac_CteColonia = string.Empty; else factura.Fac_CteColonia = dr.GetValue(dr.GetOrdinal("Fac_CteColonia")).ToString();
                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Fac_CteMunicipio")))) factura.Fac_CteMunicipio = string.Empty; else factura.Fac_CteMunicipio = dr.GetValue(dr.GetOrdinal("Fac_CteMunicipio")).ToString();
                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Fac_CteEstado")))) factura.Fac_CteEstado = string.Empty; else factura.Fac_CteEstado = dr.GetValue(dr.GetOrdinal("Fac_CteEstado")).ToString();
                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Fac_CteRfc")))) factura.Fac_CteRfc = string.Empty; else factura.Fac_CteRfc = dr.GetValue(dr.GetOrdinal("Fac_CteRfc")).ToString();
                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Fac_CteTel")))) factura.Fac_CteTel = string.Empty; else factura.Fac_CteTel = dr.GetValue(dr.GetOrdinal("Fac_CteTel")).ToString();

                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Fac_OrdEntrega")))) factura.Fac_OrdEntrega = null; else factura.Fac_OrdEntrega = dr.GetValue(dr.GetOrdinal("Fac_OrdEntrega")).ToString();
                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Fac_NumeroGuia")))) factura.Fac_NumeroGuia = string.Empty; else factura.Fac_NumeroGuia = dr.GetValue(dr.GetOrdinal("Fac_NumeroGuia")).ToString();
                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Fac_CondEntrega")))) factura.Fac_CondEntrega = string.Empty; else factura.Fac_CondEntrega = dr.GetValue(dr.GetOrdinal("Fac_CondEntrega")).ToString();
                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Fac_NumEntrega")))) factura.Fac_NumEntrega = null; else factura.Fac_NumEntrega = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Fac_NumEntrega")));
                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Fac_Notas")))) factura.Fac_Notas = string.Empty; else factura.Fac_Notas = dr.GetValue(dr.GetOrdinal("Fac_Notas")).ToString();
                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Fac_DescPorcen1")))) factura.Fac_DescPorcen1 = null; else factura.Fac_DescPorcen1 = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Fac_DescPorcen1")));
                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Fac_Desc1")))) factura.Fac_Desc1 = string.Empty; else factura.Fac_Desc1 = dr.GetValue(dr.GetOrdinal("Fac_Desc1")).ToString();
                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Fac_DescPorcen2")))) factura.Fac_DescPorcen2 = null; else factura.Fac_DescPorcen2 = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Fac_DescPorcen2")));
                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Fac_Desc2")))) factura.Fac_Desc2 = string.Empty; else factura.Fac_Desc2 = dr.GetValue(dr.GetOrdinal("Fac_Desc2")).ToString();

                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Fac_Tipo")))) factura.Fac_Tipo = string.Empty; else factura.Fac_Tipo = dr.GetValue(dr.GetOrdinal("Fac_Tipo")).ToString();
                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Fac_Conducto")))) factura.Fac_Conducto = string.Empty; else factura.Fac_Conducto = dr.GetValue(dr.GetOrdinal("Fac_Conducto")).ToString();
                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Fac_NumeroGuia")))) factura.Fac_NumeroGuia = string.Empty; else factura.Fac_NumeroGuia = dr.GetValue(dr.GetOrdinal("Fac_NumeroGuia")).ToString();
                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Fac_CanNum")))) factura.Fac_CanNum = null; else factura.Fac_CanNum = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Fac_CanNum")));
                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Fac_FecCan")))) factura.Fac_FecCan = null; else factura.Fac_FecCan = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Fac_FecCan")));
                    double pagado = 0;
                    double subtotal = 0;
                    double iva = 0;
                    double Retencion = 0;
                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Fac_Pagado")))) factura.Fac_Pagado = null;
                    else factura.Fac_Pagado = pagado = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Fac_Pagado")));

                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Fac_FecPag")))) factura.Fac_FecPag = null; else factura.Fac_FecPag = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Fac_FecPag")));
                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Fac_Importe")))) factura.Fac_Importe = null; else factura.Fac_Importe = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Fac_Importe")));
                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Fac_SubTotal"))))
                        factura.Fac_SubTotal = null;
                    else
                        factura.Fac_SubTotal = subtotal = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Fac_SubTotal")));
                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Fac_ImporteIva"))))
                        factura.Fac_ImporteIva = null;
                    else
                        factura.Fac_ImporteIva = iva = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Fac_ImporteIva")));

                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Fac_ImporteRetencion")))) factura.Fac_ImporteRetencion = null; else factura.Fac_ImporteRetencion = Retencion = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Fac_ImporteRetencion")));

                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Fac_Sello")))) factura.Fac_Sello = string.Empty; else factura.Fac_Sello = dr.GetValue(dr.GetOrdinal("Fac_Sello")).ToString();
                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Fac_Estatus")))) factura.Fac_Estatus = string.Empty; else factura.Fac_Estatus = dr.GetValue(dr.GetOrdinal("Fac_Estatus")).ToString();
                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Fac_Estatus")))) factura.Fac_Estatus = string.Empty; else factura.Fac_EstatusStr = Estatus(dr.GetValue(dr.GetOrdinal("Fac_Estatus")).ToString());
                    factura.Fac_Saldo = (subtotal + iva) - pagado;

                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("TienePagos")))) factura.TienePagos = false; else factura.TienePagos = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("TienePagos")));
                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Es")))) factura.Id_Es = null; else factura.Id_Es = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Es")));
                    factura.Fac_Contacto = dr.GetValue(dr.GetOrdinal("Fac_Contacto")).ToString();
                    factura.Folio_cancelacion = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Folio_cancelacion")));

                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_Calle")))) factura.Consig_Dir = string.Empty; else factura.Consig_Dir = dr.GetValue(dr.GetOrdinal("Cte_Calle")).ToString();
                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_Numero")))) factura.Consig_Num = string.Empty; else factura.Consig_Num = dr.GetValue(dr.GetOrdinal("Cte_Numero")).ToString();
                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_Colonia")))) factura.Consig_colonia = string.Empty; else factura.Consig_colonia = dr.GetValue(dr.GetOrdinal("Cte_Colonia")).ToString();
                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_Municipio")))) factura.Consig_mun = string.Empty; else factura.Consig_mun = dr.GetValue(dr.GetOrdinal("Cte_Municipio")).ToString();
                    factura.Fac_Refactura = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Fac_Refactura"))) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("Fac_Refactura")));
                    factura.Fac_FPago = dr.GetValue(dr.GetOrdinal("Fac_FPago")).ToString();
                    factura.Fac_UDigitos = dr.GetValue(dr.GetOrdinal("Fac_UDigitos")).ToString();
                    factura.U_Nombre = dr.GetValue(dr.GetOrdinal("U_Nombre")).ToString();

                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Fac_XML"))))
                    {
                        factura.Fac_Xml = null;
                    }
                    else
                    {
                        factura.Fac_Xml = (object)dr.GetValue(dr.GetOrdinal("Fac_XML"));
                    }
                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Fac_FolioFiscalCN")))) factura.Fac_FolioFiscalCN = string.Empty; else factura.Fac_FolioFiscalCN = dr.GetValue(dr.GetOrdinal("Fac_FolioFiscalCN")).ToString();
                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Fac_FolioCN")))) { factura.Fac_FolioCN = null; } else { factura.Fac_FolioCN = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Fac_FolioCN"))); }

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
                    case "O": return "Confirmado"; //break;
                    case "C": return "Capturado"; //break;
                    case "I": return "Impreso";// break;
                    case "U": return "Autorizado";// break;
                    case "A": return "Asignado";// break;
                    case "F": return "Facturado"; //break;
                    case "R": return "Remisionado";// break;
                    case "X": return "Facturado/Remisionado";// break;
                    case "E": return "Embarque";// break;
                    case "N": return "Entregado"; //break;
                    case "D": return "Baja por administración"; //break;
                    case "B": return "Baja por cliente"; //break;
                    default: return "";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Consulta solo el envcabezado de la factura
        /// </summary>
        /// <param name="factura">objeto factura</param>
        /// <param name="Conexion">cadena de conexion</param>
        public void ConsultaFacturaEncabezado(ref Factura factura, string Conexion, ref bool encontrado)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_Fac" };
                object[] Valores = { factura.Id_Emp, factura.Id_Cd, factura.Id_Fac };

                // ------------------------------------
                // Consultar encabezado de la factura
                // ------------------------------------
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapFactura_Consultar", ref dr, Parametros, Valores);
                encontrado = false;
                if (dr.Read())
                {
                    encontrado = true;
                    factura.Id_Emp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Emp")));
                    factura.Id_Cd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cd")));
                    factura.Id_Fac = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Fac")));
                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Cfe")))) factura.Id_Cfe = null; else factura.Id_Cfe = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cfe")));
                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_FacSerie")))) factura.Id_FacSerie = string.Empty; else factura.Id_FacSerie = dr.GetValue(dr.GetOrdinal("Id_FacSerie")).ToString();
                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Serie")))) factura.Serie = string.Empty; else factura.Serie = dr.GetValue(dr.GetOrdinal("Serie")).ToString();
                    factura.Id_U = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_U")));
                    factura.Id_Tm = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Tm")));

                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Fac_PedNum")))) factura.Fac_PedNum = null; else factura.Fac_PedNum = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Fac_PedNum")));
                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Fac_PedDesc")))) factura.Fac_PedDesc = null; else factura.Fac_PedDesc = dr.GetValue(dr.GetOrdinal("Fac_PedDesc")).ToString();

                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Fac_Req")))) factura.Fac_Req = null; else factura.Fac_Req = dr.GetValue(dr.GetOrdinal("Fac_Req")).ToString();
                    factura.Fac_Fecha = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Fac_Fecha")));
                    factura.Id_Cte = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cte")));
                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_NomComercial")))) factura.Cte_NomComercial = string.Empty; else factura.Cte_NomComercial = dr.GetValue(dr.GetOrdinal("Cte_NomComercial")).ToString();
                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_Email")))) factura.Cte_Email = string.Empty; else factura.Cte_Email = dr.GetValue(dr.GetOrdinal("Cte_Email")).ToString();
                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Ter")))) factura.Id_Ter = null; else factura.Id_Ter = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Ter")));
                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Rik")))) factura.Id_Rik = null; else factura.Id_Rik = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Rik")));
                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Emb")))) factura.Id_Emb = null; else factura.Id_Emb = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Emb")));
                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Mon")))) factura.Id_Mon = null; else factura.Id_Mon = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Mon")));
                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Mon_Descripcion")))) factura.Mon_Descripcion = null; else factura.Mon_Descripcion = dr.GetValue(dr.GetOrdinal("Mon_Descripcion")).ToString();
                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Mon_TipCambio")))) factura.Mon_TipCambio = 0; else factura.Mon_TipCambio = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Mon_TipCambio")));
                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Fac_DesgIva")))) factura.Fac_DesgIva = null; else factura.Fac_DesgIva = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Fac_DesgIva")));
                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Fac_RetIva")))) factura.Fac_RetIva = null; else factura.Fac_RetIva = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Fac_RetIva")));

                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Fac_CteCalle")))) factura.Fac_CteCalle = string.Empty; else factura.Fac_CteCalle = dr.GetValue(dr.GetOrdinal("Fac_CteCalle")).ToString();
                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Fac_CteNumero")))) factura.Fac_CteNumero = string.Empty; else factura.Fac_CteNumero = dr.GetValue(dr.GetOrdinal("Fac_CteNumero")).ToString();
                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Fac_CteNumeroInterior")))) factura.Fac_CteNumeroInterior = string.Empty; else factura.Fac_CteNumeroInterior = dr.GetValue(dr.GetOrdinal("Fac_CteNumeroInterior")).ToString();
                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Fac_CteCp")))) factura.Fac_CteCp = string.Empty; else factura.Fac_CteCp = dr.GetValue(dr.GetOrdinal("Fac_CteCp")).ToString();
                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Fac_CteColonia")))) factura.Fac_CteColonia = string.Empty; else factura.Fac_CteColonia = dr.GetValue(dr.GetOrdinal("Fac_CteColonia")).ToString();
                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Fac_CteMunicipio")))) factura.Fac_CteMunicipio = string.Empty; else factura.Fac_CteMunicipio = dr.GetValue(dr.GetOrdinal("Fac_CteMunicipio")).ToString();
                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Fac_CteEstado")))) factura.Fac_CteEstado = string.Empty; else factura.Fac_CteEstado = dr.GetValue(dr.GetOrdinal("Fac_CteEstado")).ToString();
                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Fac_CteRfc")))) factura.Fac_CteRfc = string.Empty; else factura.Fac_CteRfc = dr.GetValue(dr.GetOrdinal("Fac_CteRfc")).ToString();
                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Fac_CteTel")))) factura.Fac_CteTel = string.Empty; else factura.Fac_CteTel = dr.GetValue(dr.GetOrdinal("Fac_CteTel")).ToString();

                    // if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Fac_OrdEntrega")))) factura.Fac_OrdEntrega = null; else factura.Fac_OrdEntrega = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Fac_OrdEntrega")));
                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Fac_OrdEntrega")))) factura.Fac_OrdEntrega = null; else factura.Fac_OrdEntrega = dr.GetValue(dr.GetOrdinal("Fac_OrdEntrega")).ToString();
                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Fac_NumeroGuia")))) factura.Fac_NumeroGuia = string.Empty; else factura.Fac_NumeroGuia = dr.GetValue(dr.GetOrdinal("Fac_NumeroGuia")).ToString();
                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Fac_CondEntrega")))) factura.Fac_CondEntrega = string.Empty; else factura.Fac_CondEntrega = dr.GetValue(dr.GetOrdinal("Fac_CondEntrega")).ToString();
                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Fac_NumEntrega")))) factura.Fac_NumEntrega = null; else factura.Fac_NumEntrega = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Fac_NumEntrega")));
                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Fac_Notas")))) factura.Fac_Notas = string.Empty; else factura.Fac_Notas = dr.GetValue(dr.GetOrdinal("Fac_Notas")).ToString();
                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Fac_DescPorcen1")))) factura.Fac_DescPorcen1 = null; else factura.Fac_DescPorcen1 = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Fac_DescPorcen1")));
                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Fac_Desc1")))) factura.Fac_Desc1 = string.Empty; else factura.Fac_Desc1 = dr.GetValue(dr.GetOrdinal("Fac_Desc1")).ToString();
                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Fac_DescPorcen2")))) factura.Fac_DescPorcen2 = null; else factura.Fac_DescPorcen2 = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Fac_DescPorcen2")));
                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Fac_Desc2")))) factura.Fac_Desc2 = string.Empty; else factura.Fac_Desc2 = dr.GetValue(dr.GetOrdinal("Fac_Desc2")).ToString();


                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Fac_Tipo")))) factura.Fac_Tipo = string.Empty; else factura.Fac_Tipo = dr.GetValue(dr.GetOrdinal("Fac_Tipo")).ToString();
                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Fac_Conducto")))) factura.Fac_Conducto = string.Empty; else factura.Fac_Conducto = dr.GetValue(dr.GetOrdinal("Fac_Conducto")).ToString();
                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Fac_NumeroGuia")))) factura.Fac_NumeroGuia = string.Empty; else factura.Fac_NumeroGuia = dr.GetValue(dr.GetOrdinal("Fac_NumeroGuia")).ToString();
                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Fac_CanNum")))) factura.Fac_CanNum = null; else factura.Fac_CanNum = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Fac_CanNum")));
                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Fac_FecCan")))) factura.Fac_FecCan = null; else factura.Fac_FecCan = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Fac_FecCan")));
                    double pagado = 0;
                    double subtotal = 0;
                    double iva = 0;
                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Fac_Pagado")))) factura.Fac_Pagado = null;
                    else factura.Fac_Pagado = pagado = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Fac_Pagado")));

                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Fac_FecPag")))) factura.Fac_FecPag = null; else factura.Fac_FecPag = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Fac_FecPag")));
                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Fac_Importe")))) factura.Fac_Importe = null; else factura.Fac_Importe = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Fac_Importe")));
                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Fac_SubTotal"))))
                        factura.Fac_SubTotal = null;
                    else
                        factura.Fac_SubTotal = subtotal = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Fac_SubTotal")));
                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Fac_ImporteIva"))))
                        factura.Fac_ImporteIva = null;
                    else
                        factura.Fac_ImporteIva = iva = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Fac_ImporteIva")));

                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Fac_Sello")))) factura.Fac_Sello = string.Empty; else factura.Fac_Sello = dr.GetValue(dr.GetOrdinal("Fac_Sello")).ToString();

                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Fac_Estatus")))) factura.Fac_Estatus = string.Empty; else factura.Fac_Estatus = dr.GetValue(dr.GetOrdinal("Fac_Estatus")).ToString();

                    factura.Fac_Saldo = (subtotal + iva) - pagado;

                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("TienePagos")))) factura.TienePagos = false; else factura.TienePagos = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("TienePagos")));
                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Es")))) factura.Id_Es = null; else factura.Id_Es = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Es")));
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string[] ConsultaFacturacion_DatosGeneralesFacturacion(string Conexion, int id_Emp, int id_Cd)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Id_Cd" };
                object[] Valores = { id_Emp, id_Cd };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapFacturaDatosGeneralesFacturacion_Consultar", ref dr, Parametros, Valores);

                string[] datosGen = new string[4];
                if (dr.Read())
                {
                    datosGen[0] = dr.GetValue(dr.GetOrdinal("Emp_Nombre")).ToString();
                    datosGen[1] = dr.GetValue(dr.GetOrdinal("Cd_Nombre")).ToString();
                    datosGen[2] = dr.GetValue(dr.GetOrdinal("Reg_Descripcion")).ToString();
                    datosGen[3] = dr.GetValue(dr.GetOrdinal("Id_Reg")).ToString();
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

                return datosGen;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaFacturaEspecialDetalle(ref List<FacturaDet> listaFacturaProductos, string Conexion, int id_Emp, int id_Cd, int id_Fac, int id_Cte)
        {
            try
            {
                FacturaDet facturaDet = null;
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_Fac" };
                object[] Valores = { id_Emp, id_Cd, id_Fac };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapFacturaEspecialDetalle_Consultar", ref dr, Parametros, Valores);


                while (dr.Read())
                {
                    facturaDet = new FacturaDet();

                    facturaDet.Id_Emp = id_Emp;
                    facturaDet.Id_Cd = id_Cd;
                    facturaDet.Id_Fac = 0;
                    facturaDet.Id_FacDet = 0;
                    facturaDet.Id_CteExt = id_Cte;
                    facturaDet.Id_Prd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Prd")));
                    facturaDet.Fac_CantE = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Fac_Cant"))) ? 0 : float.Parse(dr.GetValue(dr.GetOrdinal("Fac_Cant")).ToString());
                    facturaDet.Fac_Precio = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Fac_Precio"))) ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Fac_Precio")));
                    facturaDet.Fac_ClaveProdServ = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_ClaveProdServ"))) ? string.Empty : dr.GetValue(dr.GetOrdinal("Prd_ClaveProdServ")).ToString();
                    facturaDet.Fac_ClaveUnidad = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_ClaveUnidad"))) ? string.Empty : dr.GetValue(dr.GetOrdinal("Prd_ClaveUnidad")).ToString();


                    //datos del producto de la orden de compra
                    facturaDet.Producto = new Producto();
                    facturaDet.Producto.Id_Prd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Prd")));
                    facturaDet.Producto.Id_Emp = id_Emp;
                    facturaDet.Producto.Id_Cd = id_Cd;
                    facturaDet.Producto.Id_PrdEsp = dr.IsDBNull(dr.GetOrdinal("Id_PrdEsp")) ? string.Empty : dr.GetValue(dr.GetOrdinal("Id_PrdEsp")).ToString();
                    facturaDet.Producto.Prd_Descripcion = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("FacEsp_Descripcion"))) ? string.Empty : dr.GetValue(dr.GetOrdinal("FacEsp_Descripcion")).ToString();
                    facturaDet.Producto.Prd_DescripcionEspecial = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("FacEsp_DescripcionEspecial"))) ? string.Empty : dr.GetValue(dr.GetOrdinal("FacEsp_DescripcionEspecial")).ToString();
                    facturaDet.Producto.Prd_Presentacion = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("FacEsp_Presentacion"))) ? string.Empty : dr.GetValue(dr.GetOrdinal("FacEsp_Presentacion")).ToString();
                    facturaDet.Producto.Prd_UniNe = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("FacEsp_Unidades"))) ? string.Empty : dr.GetValue(dr.GetOrdinal("FacEsp_Unidades")).ToString();
                    facturaDet.Producto.Prd_UniNs = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("FacEsp_Unidades"))) ? string.Empty : dr.GetValue(dr.GetOrdinal("FacEsp_Unidades")).ToString();
                    facturaDet.Clp_Release = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("FacEsp_Release"))) ? string.Empty : dr.GetValue(dr.GetOrdinal("FacEsp_Release")).ToString();
                    facturaDet.Producto.U_Descripcion = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Uni_Descripcion"))) ? string.Empty : dr.GetValue(dr.GetOrdinal("Uni_Descripcion")).ToString();

                    facturaDet.Producto.Prd_ClaveProdServ = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_ClaveProdServ"))) ? string.Empty : dr.GetValue(dr.GetOrdinal("Prd_ClaveProdServ")).ToString();
                    facturaDet.Producto.Prd_ClaveUnidad = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_ClaveUnidad"))) ? string.Empty : dr.GetValue(dr.GetOrdinal("Prd_ClaveUnidad")).ToString();



                    listaFacturaProductos.Add(facturaDet);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public bool ConsultaFactura_Pagos(ref Factura factura, string Conexion)
        //{
        //    try
        //    {
        //        SqlDataReader dr = null;
        //        CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
        //        bool tienePagos = false;

        //        string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_fac" };
        //        object[] Valores = { factura.Id_Emp, factura.Id_Cd, factura.Id_Fac };

        //        SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapFacturaTienePagos_Consultar", ref dr, Parametros, Valores);
        //        if (dr.Read())
        //        {
        //            tienePagos = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("TienePagos")));
        //        }
        //        CapaDatos.LimpiarSqlcommand(ref sqlcmd);

        //        return tienePagos;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        /// <summary>
        /// Metodo que retorna los datos requeridos para el grid de  FacturaXRuta
        /// </summary>
        /// <param name="factura">Entidad de las facturas</param>
        /// <param name="conexion">Cadena de conexion a la base de datos</param>
        /// <param name="lista">Lista donde se almacenara el resultado de la operacion</param>
        /// <param name="Id_Emp">Id de la empresa</param>
        /// <param name="Id_Cd">Id de la ciudad</param>
        /// <param name="Id_Fac">Id de la factura</param>
        //public void BuscaFacturaRuta(Factura factura, string conexion, ref List<Factura> list,
        //    int Id_Emp, int Id_Cd, int Id_Fac)
        //{
        //    try
        //    {
        //        SqlDataReader dr = null;
        //        CapaDatos.CD_Datos CDDatos = new CapaDatos.CD_Datos(conexion);

        //        string[] parametros = { 
        //                                  "@Id_Emp",
        //                                  "@Id_Cd",
        //                                  "@Id_Fac"
        //                              };

        //        object[] valores = { 
        //                               Id_Emp,
        //                               Id_Cd,
        //                               Id_Fac
        //                           };

        //        SqlCommand sqlcmd = CDDatos.GenerarSqlCommand("spProFacturaRuta_Busca", ref dr, parametros, valores);

        //        while (dr.Read())
        //        {
        //            factura = new Factura();

        //            factura.Id_Emp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Emp")));
        //            factura.Id_Cd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cd")));
        //            factura.Id_Fac = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Fac")));
        //            factura.Id_FacSerie = dr.GetValue(dr.GetOrdinal("Id_FacSerie")).ToString(); ;
        //            factura.Id_Cte = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cte")));
        //            factura.Cte_NomComercial = dr.GetValue(dr.GetOrdinal("Cte_NomComercial")).ToString();
        //            factura.Fac_Importe = Convert.ToSingle(dr.GetValue(dr.GetOrdinal("Fac_Importe")));

        //            list.Add(factura);
        //        }
        //        CDDatos.LimpiarSqlcommand(ref sqlcmd);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        public void BuscaFacturaRuta(ref Factura factura, string conexion)
        {
            try
            {
                CD_Datos CDDatos = new CD_Datos(conexion);
                SqlDataReader sdr = null;

                string[] parametros = {
                                          "@Id_Emp",
                                          "@Id_Cd",
                                          "@Id_Fac"
                                      };
                object[] valores = { 
                                       factura.Id_Emp,
                                       factura.Id_Cd,
                                       factura.Id_Fac
                                   };

                SqlCommand sqlcmd = default(SqlCommand);

                sqlcmd = CDDatos.GenerarSqlCommand("spProFacturaRuta_Busca", ref sdr, parametros, valores);

                while (sdr.Read())
                {
                    factura = new Factura();

                    factura.Id_Emp = Convert.ToInt32(sdr.GetValue(sdr.GetOrdinal("Id_Emp")));
                    factura.Id_Cd = Convert.ToInt32(sdr.GetValue(sdr.GetOrdinal("Id_Cd")));
                    factura.Id_Fac = Convert.ToInt32(sdr.GetValue(sdr.GetOrdinal("Id_Fac")));
                    factura.Id_FacSerie = sdr.GetValue(sdr.GetOrdinal("Id_FacSerie")).ToString(); ;
                    factura.Id_Cte = Convert.ToInt32(sdr.GetValue(sdr.GetOrdinal("Id_Cte")));
                    factura.Cte_NomComercial = sdr.GetValue(sdr.GetOrdinal("Cte_NomComercial")).ToString();
                    factura.Fac_Importe = Convert.ToDouble(sdr.GetValue(sdr.GetOrdinal("Fac_Importe")));
                }
                CDDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ValidarDisponibilidadInventario(Sesion sesion, int cantidad, int producto, int Id_Ped, ref int validador)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(sesion.Emp_Cnx);

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_Prd", "@Cantidad", "@Id_Ped" };
                object[] Valores = { sesion.Id_Emp, sesion.Id_Cd_Ver, producto == -1 ? (object)null : producto, cantidad, Id_Ped };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spDisponibilidadInventario_Consulta", ref validador, Parametros, Valores);

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaFactura_Buscar(Factura factura, string Conexion, ref List<Factura> List
            , int id_Emp, int id_Cd, string nombreCliente, int id_Cte_inicio, int id_Cte_fin
            , string fac_Tipo, string fac_Estatus, DateTime fac_Fecha_inicio
            , DateTime Fac_Fecha_fin, int id_Fac_inicio, int id_Fac_fin, int id_Ped_inicio
            , int id_Ped_fin, bool? acuse, bool? depuracion, int id_U, bool? Complementaria, string OrdenCompra)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { 
                                        "@Id_Emp"
                                        , "@Id_Cd"
                                        , "@nombreCliente"
                                        , "@Id_Cte_inicio"
                                        , "@Id_Cte_fin"
                                        , "@Fac_Tipo"
                                        , "@Fac_Fecha_inicio"
                                        , "@Fac_Fecha_fin"
                                        , "@Fac_Estatus"
                                        , "@Id_Fac_inicio"
                                        , "@Id_Fac_fin"
                                        , "@Id_Ped_inicio"
                                        , "@Id_Ped_fin"
                                        , "@acuse"
                                        , "@Fac_Depuracion"
                                        , "@Id_U"
                                        ,"@Fac_Complementaria"
                                        ,"@OrdenCompra"
                                      };
                object[] Valores = { 
                                        id_Emp
                                        , id_Cd
                                        , nombreCliente == string.Empty ? (object)null : nombreCliente
                                        , id_Cte_inicio == -1 ? (object)null : id_Cte_inicio
                                        , id_Cte_fin == -1 ? (object)null : id_Cte_fin
                                        , fac_Tipo == "-1" ? (object)null : fac_Tipo
                                        , fac_Fecha_inicio == DateTime.MinValue ? (object)null : fac_Fecha_inicio
                                        , Fac_Fecha_fin == DateTime.MinValue ? (object)null : Fac_Fecha_fin
                                        , fac_Estatus == "-1" ? (object)null : fac_Estatus
                                        , id_Fac_inicio == -1 ? (object)null : id_Fac_inicio
                                        , id_Fac_fin == -1 ? (object)null : id_Fac_fin
                                        , id_Ped_inicio == -1 ? (object)null : id_Ped_inicio
                                        , id_Ped_fin == -1 ? (object)null : id_Ped_fin
                                        , acuse
                                        , depuracion
                                        , id_U == -1 ? (object)null : id_U
                                        ,Complementaria
                                        ,OrdenCompra
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapFactura_Buscar", ref dr, Parametros, Valores);


                while (dr.Read())
                {
                    factura = new Factura();
                    factura.Id_Emp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Emp")));
                    factura.Id_Cd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cd")));
                    factura.Id_Fac = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Fac")));
                    factura.Id_Ped = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Ped"))) ? -1 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Ped")));
                    factura.Fac_Tipo = dr.GetValue(dr.GetOrdinal("Fac_Tipo")).ToString();
                    factura.Fac_TipoStr = dr.GetValue(dr.GetOrdinal("Fac_TipoStr")).ToString();
                    factura.Fac_Fecha = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Fac_Fecha")));
                    factura.Id_U = dr.IsDBNull(dr.GetOrdinal("Id_U")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_U")));
                    factura.U_Nombre = dr.IsDBNull(dr.GetOrdinal("U_Nombre")) ? "" : dr.GetValue(dr.GetOrdinal("U_Nombre")).ToString();
                    factura.Id_Cte = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cte")));
                    factura.Cte_NomComercial = dr.GetValue(dr.GetOrdinal("Cte_NomComercial")).ToString();
                    factura.Fac_Estatus = dr.GetValue(dr.GetOrdinal("Fac_Estatus")).ToString();
                    factura.Fac_EstatusStr = dr.GetValue(dr.GetOrdinal("Fac_EstatusStr")).ToString();
                    factura.Fac_DepuracionStr = dr.GetValue(dr.GetOrdinal("Fac_DepuracionStr")).ToString();
                    factura.Fac_DescPorcen1 = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Fac_DescPorcen1")));
                    factura.Fac_DescPorcen2 = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Fac_DescPorcen2")));
                    factura.Cd_IvaPedidosFacturacion = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Cd_IvaPedidosFacturacion")));
                    factura.Fac_SubTotal = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Fac_SubTotal")));
                    factura.Fac_ImporteIva = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Fac_ImporteIva")));
                    factura.Fac_Importe = factura.Fac_SubTotal + factura.Fac_ImporteIva;
                    factura.Fac_Saldo = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Fac_Saldo")));
                    factura.Fac_FolioFiscal = dr.GetValue(dr.GetOrdinal("Fac_FolioFiscal")).ToString();
                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("TienePagos")))) factura.TienePagos = null; else factura.TienePagos = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("TienePagos")));
                    try
                    {
                        if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("PDF"))))
                            factura.PDF = false;
                        else
                            factura.PDF = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("PDF")));
                    }
                    catch (Exception)
                    {
                        factura.PDF = false;
                    }
                    try
                    {
                        if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("FacXML"))))
                            factura.FXML = false;
                        else
                            factura.FXML = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("FacXML")));
                    }
                    catch (Exception)
                    {
                        factura.FXML = false;
                    }
                    try
                    {
                        if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("FacXMLCN"))))
                            factura.FXMLCN = false;
                        else
                            factura.FXMLCN = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("FacXMLCN")));
                    }
                    catch (Exception)
                    {
                        factura.FXMLCN = false;
                    }
                    List.Add(factura);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertarFactura(Sesion sesion, ref Factura factura, ref DataTable listaFacturaDet, ref DataTable listaFacturaDetAdenda, int CantidadR, string Conexion,
            ref int verificador, ref int? Id_Ped, ref List<EntradaSalida> listaEntSalRemisiones, List<AdendaDet> listAdendaCabecera, string IdRF, string arrayRemisiones, ref EntradaSalida entSal, ref List<EntradaSalidaDetalle> listaEntSalDetalle, ref FacturaEspecial facturaEsp, ref   List<FacturaDet> listaProductosFacturaEspecial, bool actualizar)
        {
            int verificador2 = 0;
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {
                #region mov. inverso
                if (string.IsNullOrEmpty(IdRF))
                {//sino es refactura entra aquí
                    if (factura.Id_Tm_Rem != null)
                    {
                        object[] Valores1 = {    
                                        factura.Id_Emp
                                        ,factura.Id_Tm_Rem
                                        };
                        string[] Parametros1 = new string[] {
                                            "@Id_Emp",			
                                            "@Id_Tm_Rem"
                                        };
                        SqlCommand sqlcmd1 = CapaDatos.GenerarSqlCommand("spCatTmovimientoInverso_Consulta", ref verificador, Parametros1, Valores1);
                        if (verificador == -2)
                        {
                            return;
                        }
                    }
                }
                #endregion
                CapaDatos.StartTrans();
                #region insertar Factura
                object[] Valores = {    
                                factura.Id_Emp
                                ,factura.Id_Cd
                                ,factura.Id_Fac
                                ,factura.Id_Cfe
                                ,factura.Id_FacSerie
                                ,factura.Id_U
                                ,factura.Id_Tm
                                ,factura.Fac_PedNum
                                ,factura.Fac_PedDesc
                                ,factura.Fac_Req                                        
                                ,factura.Fac_Fecha
                                ,factura.Id_Cte
                                ,factura.Id_Ter
                                ,factura.Id_Rik
                                ,factura.Id_Mon
                                ,factura.Fac_DesgIva
                                ,factura.Fac_RetIva
                                ,factura.Fac_CteCalle
                                ,factura.Fac_CteNumero
                                ,factura.Fac_CteNumeroInterior
                                ,factura.Fac_CteCp
                                ,factura.Fac_CteColonia                                        
                                ,factura.Fac_CteMunicipio
                                ,factura.Fac_CteEstado
                                ,factura.Fac_CteRfc
                                ,factura.Fac_CteTel
                                ,factura.Fac_OrdEntrega
                                ,factura.Fac_CondEntrega
                                ,factura.Fac_NumEntrega
                                ,factura.Fac_Notas
                                ,factura.Fac_DescPorcen1
                                ,factura.Fac_Desc1
                                ,factura.Fac_DescPorcen2                                        
                                ,factura.Fac_Desc2
                                ,factura.Fac_Tipo
                                ,factura.Fac_Conducto
                                ,factura.Fac_NumeroGuia
                                ,factura.Fac_CanNum
                                ,factura.Fac_FecCan
                                ,factura.Fac_Pagado
                                ,factura.Fac_FecPag
                                ,factura.Fac_Importe
                                 ,factura.Fac_SubTotal       
                                 ,factura.Fac_ImporteIva
                                ,factura.Fac_ImporteRetencion        
                                ,factura.Fac_Estatus
                                ,Id_Ped //si es facturacion con pedido previo, el pedido (segun esta clave) guarda la referencia de la factura
                                ,IdRF
                                ,factura.Fac_Contacto 
                                , factura.Fac_FPago
                                ,factura.Fac_UDigitos                                
                                ,string.Empty
                                ,factura.Fac_FechaRef
                                ,factura.Fac_IdUsuRef
                                ,factura.Fac_IdCausaRef
                                ,factura.Fac_TipoRef
                                ,factura.Fac_EsRefactura
                                   };
                // Insertar factura
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapFactura_Insertar", ref verificador, Parametros, Valores);
                factura.Id_Fac = verificador; //clave (folio) de factura generada



                int Id_DevRem = 0;
                if (listaEntSalRemisiones.Count() > 0)
                {

                    string[] ParametrosDevRemision = { 
                                              "@Id_Emp",
	                                          "@Id_Cd",	
                                              "@Id_DevRem",
	                                          "@Id_U", 
	                                          "@Id_Cte",
	                                          "@DevRem_CteNombre",
	                                          "@Id_Fac", 
	                                          "@DevRem_Fecha",
	                                          "@Id_Tm",
	                                          "@DevRem_Estatus",
	                                          "@Id_Ter",
                                              "@DevRem_Tipo"
	                                          
                                          };
                    object[] ValoresDevRemision = { 
                                            factura.Id_Emp,
                                            factura.Id_Cd,
                                            Id_DevRem,
                                            factura.Id_U,
                                            factura.Id_Cte,
                                            factura.Fac_CteNombre ,
                                            factura.Id_Fac,
                                            factura.Fac_Fecha,
                                            factura.Id_Tm_Rem,
                                            "C",
                                            factura.Id_Ter,
                                            "F"
                                       };

                    sqlcmd = CapaDatos.GenerarSqlCommand("spCapDevolucionRemision_Insertar", ref Id_DevRem, ParametrosDevRemision, ValoresDevRemision);
                }

                #endregion
                #region EntSalRemisiones
                foreach (EntradaSalida entSalRem in listaEntSalRemisiones)
                {
                    int id_remision = Convert.ToInt32(entSalRem.Es_Referencia);//referencia a remision
                    entSalRem.Es_Referencia = factura.Id_Fac.ToString(); //referencia a remision se sustituye por referencia a la factura
                    //if (entSalRem.Es_SubTotal > 0)
                    //{
                    string[] ParametrosEntSalRem = {
                                            "@Id_Emp"
                                            ,"@Id_Cd"
                                            ,"@Id_U"
                                            ,"@Es_Naturaleza"
                                            ,"@Es_Fecha"
                                            ,"@Id_Tm"
                                            ,"@Id_Cte"
                                            ,"@Id_Pvd"
                                            ,"@Es_Referencia"
                                            ,"@Es_ReferenciaRem"
                                            ,"@Es_Notas"
                                            ,"@Es_Subtotal"
                                            ,"@Es_Iva"
                                            ,"@Es_Total"
                                            ,"@Es_Estatus"
                                            ,"@ManAut"
                                            ,"@Id_Rem"
                                            ,"@Id_Ter"
                                            ,"@Id_Fac"
                                      };
                    object[] ValoresEntSalRem = {    
                                            entSalRem.Id_Emp
                                            ,entSalRem.Id_Cd
                                            ,entSalRem.Id_U
                                            ,entSalRem.Es_Naturaleza
                                            ,factura.Fac_Fecha
                                            ,entSalRem.Id_Tm
                                            ,entSalRem.Id_Cte
                                            ,entSalRem.Id_Pvd == -1 ? (object)null : entSalRem.Id_Pvd
                                            ,entSalRem.Es_Referencia //referencia factura
                                            ,id_remision //referencia remision
                                            ,entSalRem.Es_Notas
                                            ,entSalRem.Es_SubTotal
                                            ,entSalRem.Es_Iva
                                            ,entSalRem.Es_Total
                                            ,entSalRem.Es_Estatus
                                            ,0
                                            ,entSalRem.Id_Rem == -1 ? (object)null : entSalRem.Id_Rem
                                            ,entSalRem.Id_Ter
                                            ,factura.Id_Fac
                                          };
                    sqlcmd = CapaDatos.GenerarSqlCommand("spCapEntSal_FacturacionRemision_Insertar", ref verificador2, ParametrosEntSalRem, ValoresEntSalRem);
                    //entSalRem.Id_Es = verificador2; //clave (folio) de entrada-salida generado    
                    //}
                }
                #endregion
                //INSERTA ADENDA CABECERA
                #region Adenda
                Parametros = new string[] {
                                            "@Id_Emp",			
                                            "@Id_Cd",				
                                            "@Id_Fac",
                                            "@Id_Cte",
                                            "@Id_AdeDet",
                                            "@Ade_Tipo",
                                            "@Ade_Valor",
                                            "@Contador" 
                                      };

                int cont1 = 0;
                foreach (AdendaDet adendaF in listAdendaCabecera)
                {
                    Valores = new object[] { 
                        factura.Id_Emp,
                        factura.Id_Cd,
                        factura.Id_Fac,
                        factura.Id_Cte,
                        adendaF.Id_AdeDet,
                        adendaF.Tipo,
                        adendaF.Valor,
                        cont1
                    };
                    cont1++;
                    sqlcmd = CapaDatos.GenerarSqlCommand("spCapFacturaAdenda_Insertar", ref verificador, Parametros, Valores);
                }
                #endregion
                //INSERTAR ADENDA DETALLE
                #region adenda detalle
                Parametros = new string[] {
                                            "@Id_Emp",			
                                            "@Id_Cd",				
                                            "@Id_Fac",
                                            "@Id_Cte",
                                            "@Id_AdeDet",
                                            "@Ade_Tipo",
                                            "@Ade_Valor",
                                            "@Contador",
                                            "@Id_Prd",
                                            //"@Id_Ter"
                                      };


                foreach (DataRow facturaDet in listaFacturaDetAdenda.Rows)
                {
                    for (int i = 3; i < listaFacturaDetAdenda.Columns.Count; i++)
                    {
                        Valores = new object[] { 
                            factura.Id_Emp,
                            factura.Id_Cd,
                            factura.Id_Fac,
                            factura.Id_Cte,
                            listaFacturaDetAdenda.Columns[i].ColumnName,
                            (i + CantidadR) >= listaFacturaDetAdenda.Columns.Count ? 8 : 2,
                            facturaDet[i],
                            i,
                            facturaDet["Id_Prd"],
                            //facturaDet["Id_Ter"]
                        };
                        sqlcmd = CapaDatos.GenerarSqlCommand("spCapFacturaAdenda_Insertar", ref verificador, Parametros, Valores);
                    }
                }
                //foreach (DataRow facturaDet in listaFacturaDet.Rows)
                //{
                //    for (int i = 22; i < listaFacturaDet.Columns.Count; i++)
                //    {


                //        Valores = new object[] { 
                //            factura.Id_Emp,
                //            factura.Id_Cd,
                //            factura.Id_Fac,
                //            factura.Id_Cte,
                //            listaFacturaDet.Columns[i].ColumnName,
                //            (i + CantidadR) >= listaFacturaDet.Columns.Count ? 8 : 2,
                //            facturaDet[i],
                //            i,
                //            facturaDet["Id_Prd"],
                //            facturaDet["Id_Ter"]
                //        };
                //        sqlcmd = CapaDatos.GenerarSqlCommand("spCapFacturaAdenda_Insertar", ref verificador, Parametros, Valores);
                //    }
                //}

                #endregion
                // Insertar detalle de factura
                #region detalle factura
                string[] ParametrosDetalle = {
                                            "@Id_Emp"			
                                            ,"@Id_Cd"				
                                            ,"@Id_Fac"
                                            ,"@Id_FacDet"		
                                            ,"@Id_Ter"			
                                            ,"@Id_Prd"			
                                            ,"@Id_Cte"
			                                ,"@Id_Ped"
                                            ,"@Id_Rem"
                                            ,"@Fac_Cant"		
                                            ,"@Fac_Precio"		
                                            ,"@Fac_Asignar"
                                            ,"@Fac_Amortizacion"
                                            ,"@Rem_Cantidad"   
                                            ,"@Fac_Refactura"
                                            ,"@Id_DevRem"
                                            ,"@Rem_CantidadList"
                                            ,"@Fac_MultiplicadorGarantia"
                                            ,"@Fac_VentaFacturadaGarantia"
                                            ,"@Fac_TotalesGarantia"
                                            ,"@Fac_Precio_Original"
                                      };
                cont1 = 1;
                foreach (DataRow facturaDet in listaFacturaDet.Rows)
                {
                    object[] ValoresDetalle = {    
                                            facturaDet["Id_Emp"]				
                                            ,facturaDet["Id_Cd"]				
                                            ,factura.Id_Fac	//clave del encabezado de la factura		
                                            ,cont1
                                            ,facturaDet["Id_Ter"]			
                                            ,facturaDet["Id_Prd"]			
                                            ,facturaDet["Id_CteExt"]			
                                            ,Id_Ped//factura.Fac_PedNum //si es facturacion con pedido previo, actualiza cantidades del pedido
                                            ,facturaDet["Remisiones"] //si es facturacion de remision, se actualiza la cantidad remisionada del producto
                                            ,facturaDet["Fac_Cant"]		
                                            ,facturaDet["Fac_Precio"]		
                                            ,0
                                            ,facturaDet["AmortizacionProducto"]
                                            ,facturaDet["Rem_Cant"]
                                            ,IdRF//,verificador2//id_Es
                                            ,Id_DevRem
                                            ,facturaDet["RemisionesXML"]
                                            ,facturaDet["Multiplicador"]
                                            ,facturaDet["Precio_Venta"]
                                            ,facturaDet["Totales"]
                                            ,facturaDet["Fac_Precio_Original"]
                                          };
                    sqlcmd = CapaDatos.GenerarSqlCommand("spCapFacturaDetalle_Insertar", ref verificador, ParametrosDetalle, ValoresDetalle);
                    cont1++;
                }
                #endregion

                // Insertar detalle de factura especial
                #region detalle factura especial
                if (facturaEsp != null)
                {
                    string[] ParametrosFacturaEspecial = { 
                                        "@Id_Emp"
                                        ,"@Id_Cd"
                                        ,"@Id_Fac"
                                        ,"@Id_Ter"
                                        ,"@FacEsp_Fecha"
                                        ,"@FacEsp_Importe"
                                        ,"@FacEsp_SubTotal"
                                        ,"@FacEsp_ImporteIva"
                                        ,"@FacEsp_Total"
                                        ,"@actualizar"
                                      };
                    object[] ValoresFacturaEspecial = { 
                                        facturaEsp.Id_Emp
                                        ,facturaEsp.Id_Cd
                                        ,factura.Id_Fac
                                        ,facturaEsp.Id_Ter
                                        ,facturaEsp.FacEsp_Fecha
                                        ,facturaEsp.FacEsp_Importe
                                        ,facturaEsp.FacEsp_SubTotal
                                        ,facturaEsp.FacEsp_ImporteIva
                                        ,facturaEsp.FacEsp_Total
                                        ,actualizar
                                   };
                    sqlcmd = CapaDatos.GenerarSqlCommand("spCapFacturaEspecial_Insertar", ref verificador, ParametrosFacturaEspecial, ValoresFacturaEspecial);



                }
                // -----------------------------------------------------------------
                // Parametros de  detalle de factura epsecial y Cliente-Producto
                // -----------------------------------------------------------------
                string[] ParametrosFacturaEspecialDetalle = { 
                                        "@Id_Emp"
                                        ,"@Id_Cd"
                                        ,"@Id_Fac"
                                        ,"@Id_FacDet"
                                        ,"@Id_Prd"
                                        ,"@Id_PrdEsp"
                                        ,"@FacEsp_Descripcion"
                                        ,"@FacEsp_Presentacion"
                                        ,"@FacEsp_Unidades"
                                        ,"@FacEsp_Release"
                                        ,"@Fac_Cant"
                                        ,"@Fac_Precio"
                                        ,"@Prd_ClaveProdServ"
                                        ,"@Prd_ClaveUnidad"
                                      };
                int cont = 0;
                foreach (FacturaDet facturaDet in listaProductosFacturaEspecial)
                {
                    // --------------------------------------
                    // Insertar detalle de factura especial
                    // --------------------------------------
                    object[] ValoresFacturaEspecialDetalle = { 
                                                        facturaDet.Id_Emp
                                                        ,facturaDet.Id_Cd
                                                        , factura.Id_Fac
                                                        ,cont
                                                        ,facturaDet.Id_Prd
                                                        ,facturaDet.Producto.Id_PrdEsp
                                                        ,facturaDet.Producto.Prd_DescripcionEspecial.Split(new string[]{"|"}, StringSplitOptions.RemoveEmptyEntries).Length > 0 ? facturaDet.Producto.Prd_DescripcionEspecial.Split(new string[]{"|"}, StringSplitOptions.RemoveEmptyEntries)[0] : ""
                                                        ,facturaDet.Producto.Prd_Presentacion
                                                        ,facturaDet.Producto.Prd_UniNe
                                                        ,facturaDet.Clp_Release
                                                        ,facturaDet.Fac_CantE //cantidad facturada
                                                        ,facturaDet.Fac_Precio
                                                        ,facturaDet.Fac_ClaveProdServ //cantidad facturada
                                                        ,facturaDet.Fac_ClaveUnidad

                                   };
                    sqlcmd = CapaDatos.GenerarSqlCommand("spCapFacturaEspecialDetalle_Insertar"
                        , ref verificador, ParametrosFacturaEspecialDetalle, ValoresFacturaEspecialDetalle);
                    cont++; //Aumenta contador de partida
                }
                #endregion


                // Insertar momivimentos de Entrada inversos de remisiones, si es que es una facturacion de remisiones

                #region remisiones1
                string[] ParametrosDetalleEntSalRem = {
                                        "@Id_Emp",
                                        "@Id_Cd",                                      
                                        "@Id_Rem",                                        
                                        "@Id_Fac",
                                        "@Fac_Refactura"
                                      };
                object[] ValoresDetalleEntSalRem = {    
                                        factura.Id_Emp
                                        ,factura.Id_Cd                         
                                        ,arrayRemisiones
                                        ,factura.Id_Fac
                                        ,IdRF
                                      };
                sqlcmd = CapaDatos.GenerarSqlCommand("spCapEntSalDet_FactRemision_Insertar", ref verificador, ParametrosDetalleEntSalRem, ValoresDetalleEntSalRem);
                #endregion
                #region facturas tipo 70
                if (factura.Id_Tm == 70)
                {
                    // ----------------------------------------
                    // Insertar mov. entrada-salida mov. 16
                    // ----------------------------------------
                    entSal.Es_Referencia = factura.Id_Fac.ToString(); //referencia a la factura
                    entSal.Es_Notas = entSal.Es_Notas.Replace("#Id_Fac#", factura.Id_Fac.ToString());
                    string[] ParametrosEntSal = {
                                            "@Id_Emp"
                                            ,"@Id_Cd"
                                            ,"@Id_U"
                                            ,"@Es_Naturaleza"
                                            ,"@Es_Fecha"
                                            ,"@Id_Tm"
                                            ,"@Id_Cte"
                                            ,"@Id_Pvd"
                                            ,"@Es_Referencia"
                                            ,"@Es_Notas"
                                            ,"@Es_Subtotal"
                                            ,"@Es_Iva"
                                            ,"@Es_Total"
                                            ,"@Es_Estatus"
                                            ,"@ManAut",
                                            "@Id_Ter"
                                      };
                    object[] ValoresEntSal = {    
                                            entSal.Id_Emp
                                            ,entSal.Id_Cd
                                            ,entSal.Id_U
                                            ,entSal.Es_Naturaleza
                                            ,entSal.Es_Fecha
                                            ,entSal.Id_Tm
                                            ,entSal.Id_Cte
                                            ,entSal.Id_Pvd == -1 ? (object)null : entSal.Id_Pvd
                                            ,entSal.Es_Referencia
                                            ,entSal.Es_Notas
                                            ,entSal.Es_SubTotal
                                            ,entSal.Es_Iva
                                            ,entSal.Es_Total
                                            ,entSal.Es_Estatus
                                            ,0,
                                            entSal.Id_Ter
                                          };
                    sqlcmd = CapaDatos.GenerarSqlCommand("spCapEntSal_Insertar", ref verificador, ParametrosEntSal, ValoresEntSal);
                    entSal.Id_Es = verificador; //clave (folio) de entrada-salida generado

                    // ----------------------------------------
                    // Insertar detalle de Entrada-Salida
                    // ----------------------------------------
                    string[] ParametrosDetalleEntSal = {
                                        "@Id_Emp",
                                        "@Id_Cd",
                                        "@Id_Es",
                                        "@Id_EsDet",
                                        "@EsDet_Naturaleza",
                                        "@Id_Ter",
                                        "@Id_Prd",
                                        "@Es_Cantidad",
                                        "@Es_Costo",
                                        "@Es_BuenEstado",
                                        "@EsDet_AfcOrdCom"
                                      };
                    int contEntsal = 0;
                    foreach (EntradaSalidaDetalle entSalDetalle in listaEntSalDetalle)
                    {
                        object[] ValoresDetalleEntSal = {    
                                            entSalDetalle.Id_Emp
                                            ,entSalDetalle.Id_Cd
                                            ,entSal.Id_Es
                                            ,contEntsal
                                            ,entSalDetalle.EsDet_Naturaleza
                                            ,entSalDetalle.Id_Ter
                                            ,entSalDetalle.Id_Prd
                                            ,entSalDetalle.Es_Cantidad
                                            ,entSalDetalle.Es_Costo
                                            ,entSalDetalle.Es_BuenEstado
                                            ,entSalDetalle.Afct_OrdCompra
                                          };
                        sqlcmd = CapaDatos.GenerarSqlCommand("spCapEntSalDet_FactAparatosInproductivos_Insertar", ref verificador, ParametrosDetalleEntSal, ValoresDetalleEntSal);
                        contEntsal++;
                    }
                }
                #endregion
                CapaDatos.CommitTrans();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
                #region Nota de Crédito
                int verificado = 0;
                int generarVerificador = 0;
                int Id_Ref = 0;
                double total = 0.00;
                double porciento = 0.00;
                string referencia = string.Empty;
                List<AdendaDet> ListCab = new List<AdendaDet>();
                CapaDatos.CD_Datos CapaDatos1 = new CapaDatos.CD_Datos(Conexion);
                SqlDataReader dr = null;
                string[] ParametrosCliente = {
                                                "@Id_Emp"
                                                ,"@Id_Cd"
                                                ,"@Id_Cte"
                                             };
                object[] ValoresCliente = {    
                                            factura.Id_Emp
                                            ,factura.Id_Cd
                                            ,factura.Id_Cte
                                          };

                sqlcmd = CapaDatos1.GenerarSqlCommand("spCatCliente_validarNotaCredito", ref dr, ParametrosCliente, ValoresCliente);
                while (dr.Read())
                {
                    generarVerificador = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Validador")));
                    porciento = dr.IsDBNull(dr.GetOrdinal("Porciento")) ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Porciento")));
                    Id_Ref = dr.IsDBNull(dr.GetOrdinal("Id_Ref")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Ref")));
                    referencia = dr.IsDBNull(dr.GetOrdinal("Referencia")) ? "" : dr.GetValue(dr.GetOrdinal("Referencia")).ToString();
                }


                if (generarVerificador == 1 && porciento > 0)
                {

                    NotaCredito notaCredito = new NotaCredito();



                    notaCredito.Id_Emp = factura.Id_Emp;
                    notaCredito.Id_Cd = factura.Id_Cd;
                    notaCredito.Id_Ncr = 0;
                    notaCredito.Id_Cfe = Id_Ref;
                    notaCredito.Id_NcrSerie = referencia;
                    notaCredito.Id_Tm = 4; // Descuento aplicado a la factura
                    notaCredito.Id_Cte = factura.Id_Cte;
                    notaCredito.Id_Ter = factura.Id_Ter;
                    notaCredito.Id_Rik = factura.Id_Rik;
                    notaCredito.Id_U = factura.Id_U;
                    notaCredito.Ncr_Tipo = 1; // 1-- tipo factura
                    notaCredito.Ncr_Fecha = factura.Fac_Fecha;
                    notaCredito.Ncr_EmpleadoNumNomina = null;
                    notaCredito.Ncr_EmpleadoNombre = null;
                    notaCredito.Ncr_CtaContable = null;
                    notaCredito.Ncr_Movimiento = 1;
                    notaCredito.Ncr_Referencia = factura.Id_Fac;
                    notaCredito.Ncr_Saldo = factura.Fac_Importe;
                    notaCredito.Ncr_Desgloce = true;
                    notaCredito.Ncr_DesglocePartidas = false;
                    notaCredito.Ncr_Notas = "Descuento aplicado a la factura";
                    notaCredito.Ncr_CteDIVA = false;
                    total = (porciento / 100);
                    notaCredito.Ncr_Subtotal = factura.Fac_SubTotal * total;
                    notaCredito.Ncr_Iva = factura.Fac_ImporteIva * total;

                    notaCredito.Ncr_Total = notaCredito.Ncr_Subtotal + notaCredito.Ncr_Iva;
                    notaCredito.Ncr_Pagado = notaCredito.Ncr_Subtotal + notaCredito.Ncr_Iva;
                    notaCredito.Ncr_FecPagado = DateTime.Now;
                    notaCredito.Ncr_Estatus = "I";
                    notaCredito.Ncr_ReferenciaSerie = referencia;

                    DataTable ListaProductosNotaCredito = new DataTable();
                    ListaProductosNotaCredito.Columns.Add("Id_Emp");
                    ListaProductosNotaCredito.Columns.Add("Id_Cd");
                    ListaProductosNotaCredito.Columns.Add("Id_Ncr");
                    ListaProductosNotaCredito.Columns.Add("Id_NcrDet");
                    ListaProductosNotaCredito.Columns.Add("Id_Ter");
                    ListaProductosNotaCredito.Columns.Add("Ter_Nombre");
                    ListaProductosNotaCredito.Columns.Add("Id_Rik");
                    ListaProductosNotaCredito.Columns.Add("Rik_Nombre");
                    ListaProductosNotaCredito.Columns.Add("Id_Prd");
                    ListaProductosNotaCredito.Columns.Add("Prd_Nombre");
                    ListaProductosNotaCredito.Columns.Add("Ncr_Importe", typeof(System.Double));

                    cont1 = 1;
                    foreach (DataRow facturaDet in listaFacturaDet.Rows)
                    {
                        double precio = facturaDet["Fac_Precio"] != null ? Convert.ToDouble(facturaDet["Fac_Precio"]) : 0.00;
                        double cantidad = facturaDet["Fac_Cant"] != null ? Convert.ToDouble(facturaDet["Fac_Cant"]) : 0.00;

                        ListaProductosNotaCredito.Rows.Add(
                                           facturaDet["Id_Emp"]
                                           , facturaDet["Id_Cd"]
                                           , 0
                                           , cont1
                                           , facturaDet["Id_Ter"]
                                           , ""
                                           , factura.Id_Rik
                                           , ""
                                           , facturaDet["Id_Prd"]
                                           , ""
                                           , ((precio * cantidad) * total)
                                    );
                        cont1++;
                    }

                    CD_CapNotaCredito cdCredito = new CD_CapNotaCredito();
                    cdCredito.InsertarNotaCredito(ref notaCredito, sesion.Emp_Cnx, ref verificado, ListCab, ListaProductosNotaCredito);
                    CapaDatos1.LimpiarSqlcommand(ref sqlcmd);
                }
                #endregion
            }
            catch (Exception ex)
            {
                CapaDatos.RollBackTrans();
                throw ex;
            }
        }

        public void InsertarFactura(Sesion sesion, ref Factura factura, ref Factura facturaNacional, ref DataTable listaFacturaDet, ref DataTable listaFacturaDetAdenda, ref DataTable listaFacturaDetAdendaNacional,
            int CantidadR, string Conexion,
            ref int verificador, ref int? Id_Ped, ref List<EntradaSalida> listaEntSalRemisiones, List<AdendaDet> listAdendaCabecera, List<AdendaDet> listAdendaNacionalCabecera, string IdRF, string arrayRemisiones, ref EntradaSalida entSal, ref List<EntradaSalidaDetalle> listaEntSalDetalle, ref FacturaEspecial facturaEsp, ref   List<FacturaDet> listaProductosFacturaEspecial, bool actualizar)
        {
            int verificador2 = 0;
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {
                #region mov. inverso
                if (string.IsNullOrEmpty(IdRF))
                {//sino es refactura entra aquí
                    if (factura.Id_Tm_Rem != null)
                    {
                        object[] Valores1 = {    
                                        factura.Id_Emp
                                        ,factura.Id_Tm_Rem
                                        };
                        string[] Parametros1 = new string[] {
                                            "@Id_Emp",			
                                            "@Id_Tm_Rem"
                                        };
                        SqlCommand sqlcmd1 = CapaDatos.GenerarSqlCommand("spCatTmovimientoInverso_Consulta", ref verificador, Parametros1, Valores1);
                        if (verificador == -2)
                        {
                            return;
                        }
                    }
                }
                #endregion
                CapaDatos.StartTrans();
                #region insertar Factura
                object[] Valores = {    
                                factura.Id_Emp
                                ,factura.Id_Cd
                                ,factura.Id_Fac
                                ,factura.Id_Cfe
                                ,factura.Id_FacSerie
                                ,factura.Id_U
                                ,factura.Id_Tm
                                ,factura.Fac_PedNum
                                ,factura.Fac_PedDesc
                                ,factura.Fac_Req                                        
                                ,factura.Fac_Fecha
                                ,factura.Id_Cte
                                ,factura.Id_Ter
                                ,factura.Id_Rik
                                ,factura.Id_Mon
                                ,factura.Fac_DesgIva
                                ,factura.Fac_RetIva
                                ,factura.Fac_CteCalle
                                ,factura.Fac_CteNumero
                                ,factura.Fac_CteNumeroInterior
                                ,factura.Fac_CteCp

                                ,factura.Fac_CteColonia                                        
                                ,factura.Fac_CteMunicipio
                                ,factura.Fac_CteEstado
                                ,factura.Fac_CteRfc
                                ,factura.Fac_CteTel
                                ,factura.Fac_OrdEntrega
                                ,factura.Fac_CondEntrega
                                ,factura.Fac_NumEntrega
                                ,factura.Fac_Notas
                                ,factura.Fac_DescPorcen1
                                ,factura.Fac_Desc1
                                ,factura.Fac_DescPorcen2                                        
                                ,factura.Fac_Desc2
                                ,factura.Fac_Tipo
                                ,factura.Fac_Conducto
                                ,factura.Fac_NumeroGuia
                                ,factura.Fac_CanNum
                                ,factura.Fac_FecCan
                                ,factura.Fac_Pagado
                                ,factura.Fac_FecPag
                                ,factura.Fac_Importe
                                 ,factura.Fac_SubTotal       
                                 ,factura.Fac_ImporteIva
                                ,factura.Fac_ImporteRetencion        
                                ,factura.Fac_Estatus
                                ,Id_Ped //si es facturacion con pedido previo, el pedido (segun esta clave) guarda la referencia de la factura
                                ,IdRF
                                ,factura.Fac_Contacto 
                                , factura.Fac_FPago
                                ,factura.Fac_UDigitos    
                                ,string.Empty
                                ,factura.Fac_FechaRef // Se agregan 5 campos para refactura , RFH 02032018
                                ,factura.Fac_IdUsuRef
                                ,factura.Fac_IdCausaRef
                                ,factura.Fac_TipoRef
                                ,factura.Fac_EsRefactura
                                   };
                // Insertar factura
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapFactura_Insertar", ref verificador, Parametros, Valores);
                factura.Id_Fac = verificador; //clave (folio) de factura generada



                int Id_DevRem = 0;
                if (listaEntSalRemisiones.Count() > 0)
                {

                    string[] ParametrosDevRemision = { 
                                              "@Id_Emp",
	                                          "@Id_Cd",	
                                              "@Id_DevRem",
	                                          "@Id_U", 
	                                          "@Id_Cte",
	                                          "@DevRem_CteNombre",
	                                          "@Id_Fac", 
	                                          "@DevRem_Fecha",
	                                          "@Id_Tm",
	                                          "@DevRem_Estatus",
	                                          "@Id_Ter",
                                              "@DevRem_Tipo"
	                                          
                                          };
                    object[] ValoresDevRemision = { 
                                            factura.Id_Emp,
                                            factura.Id_Cd,
                                            Id_DevRem,
                                            factura.Id_U,
                                            factura.Id_Cte,
                                            factura.Fac_CteNombre ,
                                            factura.Id_Fac,
                                            factura.Fac_Fecha,
                                            factura.Id_Tm_Rem,
                                            "C",
                                            factura.Id_Ter,
                                            "F"
                                       };

                    sqlcmd = CapaDatos.GenerarSqlCommand("spCapDevolucionRemision_Insertar", ref Id_DevRem, ParametrosDevRemision, ValoresDevRemision);
                }

                #endregion
                #region EntSalRemisiones
                foreach (EntradaSalida entSalRem in listaEntSalRemisiones)
                {
                    int id_remision = Convert.ToInt32(entSalRem.Es_Referencia);//referencia a remision
                    entSalRem.Es_Referencia = factura.Id_Fac.ToString(); //referencia a remision se sustituye por referencia a la factura
                    //if (entSalRem.Es_SubTotal > 0)
                    //{
                    string[] ParametrosEntSalRem = {
                                            "@Id_Emp"
                                            ,"@Id_Cd"
                                            ,"@Id_U"
                                            ,"@Es_Naturaleza"
                                            ,"@Es_Fecha"
                                            ,"@Id_Tm"
                                            ,"@Id_Cte"
                                            ,"@Id_Pvd"
                                            ,"@Es_Referencia"
                                            ,"@Es_ReferenciaRem"
                                            ,"@Es_Notas"
                                            ,"@Es_Subtotal"
                                            ,"@Es_Iva"
                                            ,"@Es_Total"
                                            ,"@Es_Estatus"
                                            ,"@ManAut"
                                            ,"@Id_Rem"
                                            ,"@Id_Ter"
                                            ,"@Id_Fac"
                                      };
                    object[] ValoresEntSalRem = {    
                                            entSalRem.Id_Emp
                                            ,entSalRem.Id_Cd
                                            ,entSalRem.Id_U
                                            ,entSalRem.Es_Naturaleza
                                            ,factura.Fac_Fecha
                                            ,entSalRem.Id_Tm
                                            ,entSalRem.Id_Cte
                                            ,entSalRem.Id_Pvd == -1 ? (object)null : entSalRem.Id_Pvd
                                            ,entSalRem.Es_Referencia //referencia factura
                                            ,id_remision //referencia remision
                                            ,entSalRem.Es_Notas
                                            ,entSalRem.Es_SubTotal
                                            ,entSalRem.Es_Iva
                                            ,entSalRem.Es_Total
                                            ,entSalRem.Es_Estatus
                                            ,0
                                            ,entSalRem.Id_Rem == -1 ? (object)null : entSalRem.Id_Rem
                                            ,entSalRem.Id_Ter
                                            ,factura.Id_Fac
                                          };
                    sqlcmd = CapaDatos.GenerarSqlCommand("spCapEntSal_FacturacionRemision_Insertar", ref verificador2, ParametrosEntSalRem, ValoresEntSalRem);
                    //entSalRem.Id_Es = verificador2; //clave (folio) de entrada-salida generado    
                    //}
                }
                #endregion
                #region Factura Nacional
                //Factura Nacional
                Parametros = new string[]{
                    "@Id_Emp",
                    "@Id_Cd",
                    "@Id_Fac",
                    "@Id_Cte",
                    "@Cte_NomComercial",
                    "@Fac_CteCalle",
                    "@Fac_CteNumero",
                    "@Fac_CteColonia",
                    "@Fac_CteMunicipio",
                    "@Fac_CteEstado",
                    "@Fac_CteRfc",
                    "@Fac_CteCp",
                    "@Fac_CteAdeNombre"
                };
                Valores = new object[]{
                    facturaNacional.Id_Emp,
                    facturaNacional.Id_Cd,
                    factura.Id_Fac,
                    facturaNacional.Id_Cte,
                    facturaNacional.Cte_NomComercial,
                    facturaNacional.Fac_CteCalle,
                    facturaNacional.Fac_CteNumero,
                    facturaNacional.Fac_CteColonia,
                    facturaNacional.Fac_CteMunicipio,
                    facturaNacional.Fac_CteEstado,
                    facturaNacional.Fac_CteRfc,
                    facturaNacional.Fac_CteCp,
                    facturaNacional.Fac_CteAdeNombre
                };
                sqlcmd = CapaDatos.GenerarSqlCommand("spCapFacturaNacional_Insertar", ref verificador, Parametros, Valores);
                #endregion
                //INSERTA ADENDA CABECERA
                #region Adenda
                Parametros = new string[] {
                                            "@Id_Emp",			
                                            "@Id_Cd",				
                                            "@Id_Fac",
                                            "@Id_Cte",
                                            "@Id_AdeDet",
                                            "@Ade_Tipo",
                                            "@Ade_Valor",
                                            "@Contador" 
                                      };

                int cont1 = 0;
                foreach (AdendaDet adendaF in listAdendaCabecera)
                {
                    Valores = new object[] { 
                        factura.Id_Emp,
                        factura.Id_Cd,
                        factura.Id_Fac,
                        factura.Id_Cte,
                        adendaF.Id_AdeDet,
                        adendaF.Tipo,
                        adendaF.Valor,
                        cont1
                    };
                    cont1++;
                    sqlcmd = CapaDatos.GenerarSqlCommand("spCapFacturaAdenda_Insertar", ref verificador, Parametros, Valores);
                }
                Parametros = new string[] {
                                            "@Id_Emp",			
                                            "@Id_Cd",				
                                            "@Id_Fac",
                                            "@Id_Cte",
                                            "@Id_AdeDet",
                                            "@Ade_Tipo",
                                            "@Ade_Valor",
                                            "@Contador" 
                                      };

                cont1 = 0;
                foreach (AdendaDet adendaF in listAdendaNacionalCabecera)
                {
                    Valores = new object[] { 
                        factura.Id_Emp,
                        factura.Id_Cd,
                        factura.Id_Fac,
                        factura.Id_Cte,
                        adendaF.Id_AdeDet,
                        adendaF.Tipo,
                        adendaF.Valor,
                        cont1
                    };
                    cont1++;
                    sqlcmd = CapaDatos.GenerarSqlCommand("spCapFacturaAdendaNacional_Insertar", ref verificador, Parametros, Valores);
                }

                #endregion

                //Factura Nacional
                //INSERTAR ADENDA DETALLE
                #region adenda detalle
                Parametros = new string[] {
                                            "@Id_Emp",			
                                            "@Id_Cd",				
                                            "@Id_Fac",
                                            "@Id_Cte",
                                            "@Id_AdeDet",
                                            "@Ade_Tipo",
                                            "@Ade_Valor",
                                            "@Contador",
                                            "@Id_Prd",
                                            //"@Id_Ter"
                                      };


                foreach (DataRow facturaDet in listaFacturaDetAdenda.Rows)
                {
                    for (int i = 3; i < listaFacturaDetAdenda.Columns.Count; i++)
                    {
                        Valores = new object[] { 
                            factura.Id_Emp,
                            factura.Id_Cd,
                            factura.Id_Fac,
                            factura.Id_Cte,
                            listaFacturaDetAdenda.Columns[i].ColumnName,
                            (i + CantidadR) >= listaFacturaDetAdenda.Columns.Count ? 8 : 2,
                            facturaDet[i],
                            i,
                            facturaDet["Id_Prd"],
                            //facturaDet["Id_Ter"]
                        };
                        sqlcmd = CapaDatos.GenerarSqlCommand("spCapFacturaAdenda_Insertar", ref verificador, Parametros, Valores);
                    }
                }

                //Adenda Nacional
                foreach (DataRow facturaDet in listaFacturaDetAdendaNacional.Rows)
                {
                    for (int i = 3; i < listaFacturaDetAdendaNacional.Columns.Count; i++)
                    {
                        Valores = new object[] { 
                            factura.Id_Emp,
                            factura.Id_Cd,
                            factura.Id_Fac,
                            factura.Id_Cte,
                            listaFacturaDetAdendaNacional.Columns[i].ColumnName,
                            (i + CantidadR) >= listaFacturaDetAdendaNacional.Columns.Count ? 8 : 2,
                            facturaDet[i],
                            i,
                            facturaDet["Id_Prd"],
                            //facturaDet["Id_Ter"]
                        };
                        sqlcmd = CapaDatos.GenerarSqlCommand("spCapFacturaAdendaNacional_Insertar", ref verificador, Parametros, Valores);
                    }
                }
                //foreach (DataRow facturaDet in listaFacturaDet.Rows)
                //{
                //    for (int i = 22; i < listaFacturaDet.Columns.Count; i++)
                //    {


                //        Valores = new object[] { 
                //            factura.Id_Emp,
                //            factura.Id_Cd,
                //            factura.Id_Fac,
                //            factura.Id_Cte,
                //            listaFacturaDet.Columns[i].ColumnName,
                //            (i + CantidadR) >= listaFacturaDet.Columns.Count ? 8 : 2,
                //            facturaDet[i],
                //            i,
                //            facturaDet["Id_Prd"],
                //            facturaDet["Id_Ter"]
                //        };
                //        sqlcmd = CapaDatos.GenerarSqlCommand("spCapFacturaAdenda_Insertar", ref verificador, Parametros, Valores);
                //    }
                //}

                #endregion
                // Insertar detalle de factura
                #region detalle factura
                string[] ParametrosDetalle = {
                                            "@Id_Emp"			
                                            ,"@Id_Cd"				
                                            ,"@Id_Fac"
                                            ,"@Id_FacDet"		
                                            ,"@Id_Ter"			
                                            ,"@Id_Prd"			
                                            ,"@Id_Cte"
			                                ,"@Id_Ped"
                                            ,"@Id_Rem"
                                            ,"@Fac_Cant"		
                                            ,"@Fac_Precio"		
                                            ,"@Fac_Asignar"
                                            ,"@Fac_Amortizacion"
                                            ,"@Rem_Cantidad"   
                                            ,"@Fac_Refactura"
                                            ,"@Id_DevRem"
                                            ,"@Rem_CantidadList"
                                      };
                cont1 = 1;
                foreach (DataRow facturaDet in listaFacturaDet.Rows)
                {
                    object[] ValoresDetalle = {    
                                            facturaDet["Id_Emp"]				
                                            ,facturaDet["Id_Cd"]				
                                            ,factura.Id_Fac	//clave del encabezado de la factura		
                                            ,cont1
                                            ,facturaDet["Id_Ter"]			
                                            ,facturaDet["Id_Prd"]			
                                            ,facturaDet["Id_CteExt"]			
                                            ,Id_Ped//factura.Fac_PedNum //si es facturacion con pedido previo, actualiza cantidades del pedido
                                            ,facturaDet["Remisiones"] //si es facturacion de remision, se actualiza la cantidad remisionada del producto
                                            ,facturaDet["Fac_Cant"]		
                                            ,facturaDet["Fac_Precio"]		
                                            ,0
                                            ,facturaDet["AmortizacionProducto"]
                                            ,facturaDet["Rem_Cant"]
                                            ,IdRF//,verificador2//id_Es
                                            ,Id_DevRem
                                            ,facturaDet["RemisionesXML"]
                                          };
                    sqlcmd = CapaDatos.GenerarSqlCommand("spCapFacturaDetalle_Insertar", ref verificador, ParametrosDetalle, ValoresDetalle);
                    cont1++;
                }
                #endregion

                // Insertar detalle de factura especial
                #region detalle factura especial
                if (facturaEsp != null)
                {
                    string[] ParametrosFacturaEspecial = { 
                                        "@Id_Emp"
                                        ,"@Id_Cd"
                                        ,"@Id_Fac"
                                        ,"@Id_Ter"
                                        ,"@FacEsp_Fecha"
                                        ,"@FacEsp_Importe"
                                        ,"@FacEsp_SubTotal"
                                        ,"@FacEsp_ImporteIva"
                                        ,"@FacEsp_Total"
                                        ,"@actualizar"
                                      };
                    object[] ValoresFacturaEspecial = { 
                                        facturaEsp.Id_Emp
                                        ,facturaEsp.Id_Cd
                                        ,factura.Id_Fac
                                        ,facturaEsp.Id_Ter
                                        ,facturaEsp.FacEsp_Fecha
                                        ,facturaEsp.FacEsp_Importe
                                        ,facturaEsp.FacEsp_SubTotal
                                        ,facturaEsp.FacEsp_ImporteIva
                                        ,facturaEsp.FacEsp_Total
                                        ,actualizar
                                   };
                    sqlcmd = CapaDatos.GenerarSqlCommand("spCapFacturaEspecial_Insertar", ref verificador, ParametrosFacturaEspecial, ValoresFacturaEspecial);



                }
                // -----------------------------------------------------------------
                // Parametros de  detalle de factura epsecial y Cliente-Producto
                // -----------------------------------------------------------------
                string[] ParametrosFacturaEspecialDetalle = { 
                                        "@Id_Emp"
                                        ,"@Id_Cd"
                                        ,"@Id_Fac"
                                        ,"@Id_FacDet"
                                        ,"@Id_Prd"
                                        ,"@Id_PrdEsp"
                                        ,"@FacEsp_Descripcion"
                                        ,"@FacEsp_Presentacion"
                                        ,"@FacEsp_Unidades"
                                        ,"@FacEsp_Release"
                                        ,"@Fac_Cant"
                                        ,"@Fac_Precio"
                                        ,"@Prd_ClaveProdServ"
                                        ,"@Prd_ClaveUnidad"

                                      };
                int cont = 0;
                foreach (FacturaDet facturaDet in listaProductosFacturaEspecial)
                {
                    // --------------------------------------
                    // Insertar detalle de factura especial
                    // --------------------------------------
                    object[] ValoresFacturaEspecialDetalle = { 
                                                        facturaDet.Id_Emp
                                                        ,facturaDet.Id_Cd
                                                        , factura.Id_Fac
                                                        ,cont
                                                        ,facturaDet.Id_Prd
                                                        ,facturaDet.Producto.Id_PrdEsp
                                                        ,facturaDet.Producto.Prd_DescripcionEspecial.Split(new string[]{"|"}, StringSplitOptions.RemoveEmptyEntries).Length > 0 ? facturaDet.Producto.Prd_DescripcionEspecial.Split(new string[]{"|"}, StringSplitOptions.RemoveEmptyEntries)[0] : ""
                                                        ,facturaDet.Producto.Prd_Presentacion
                                                        ,facturaDet.Producto.Prd_UniNe
                                                        ,facturaDet.Clp_Release
                                                        ,facturaDet.Fac_CantE //cantidad facturada
                                                        ,facturaDet.Fac_Precio
                                                        ,facturaDet.Fac_ClaveProdServ //cantidad facturada
                                                        ,facturaDet.Fac_ClaveUnidad

                                   };
                    sqlcmd = CapaDatos.GenerarSqlCommand("spCapFacturaEspecialDetalle_Insertar"
                        , ref verificador, ParametrosFacturaEspecialDetalle, ValoresFacturaEspecialDetalle);
                    cont++; //Aumenta contador de partida
                }
                #endregion


                // Insertar momivimentos de Entrada inversos de remisiones, si es que es una facturacion de remisiones

                #region remisiones1
                string[] ParametrosDetalleEntSalRem = {
                                        "@Id_Emp",
                                        "@Id_Cd",                                      
                                        "@Id_Rem",                                        
                                        "@Id_Fac",
                                        "@Fac_Refactura"
                                      };
                object[] ValoresDetalleEntSalRem = {    
                                        factura.Id_Emp
                                        ,factura.Id_Cd                         
                                        ,arrayRemisiones
                                        ,factura.Id_Fac
                                        ,IdRF
                                      };
                sqlcmd = CapaDatos.GenerarSqlCommand("spCapEntSalDet_FactRemision_Insertar", ref verificador, ParametrosDetalleEntSalRem, ValoresDetalleEntSalRem);
                #endregion
                #region facturas tipo 70
                if (factura.Id_Tm == 70)
                {
                    // ----------------------------------------
                    // Insertar mov. entrada-salida mov. 16
                    // ----------------------------------------
                    entSal.Es_Referencia = factura.Id_Fac.ToString(); //referencia a la factura
                    entSal.Es_Notas = entSal.Es_Notas.Replace("#Id_Fac#", factura.Id_Fac.ToString());
                    string[] ParametrosEntSal = {
                                            "@Id_Emp"
                                            ,"@Id_Cd"
                                            ,"@Id_U"
                                            ,"@Es_Naturaleza"
                                            ,"@Es_Fecha"
                                            ,"@Id_Tm"
                                            ,"@Id_Cte"
                                            ,"@Id_Pvd"
                                            ,"@Es_Referencia"
                                            ,"@Es_Notas"
                                            ,"@Es_Subtotal"
                                            ,"@Es_Iva"
                                            ,"@Es_Total"
                                            ,"@Es_Estatus"
                                            ,"@ManAut",
                                            "@Id_Ter"
                                      };
                    object[] ValoresEntSal = {    
                                            entSal.Id_Emp
                                            ,entSal.Id_Cd
                                            ,entSal.Id_U
                                            ,entSal.Es_Naturaleza
                                            ,entSal.Es_Fecha
                                            ,entSal.Id_Tm
                                            ,entSal.Id_Cte
                                            ,entSal.Id_Pvd == -1 ? (object)null : entSal.Id_Pvd
                                            ,entSal.Es_Referencia
                                            ,entSal.Es_Notas
                                            ,entSal.Es_SubTotal
                                            ,entSal.Es_Iva
                                            ,entSal.Es_Total
                                            ,entSal.Es_Estatus
                                            ,0,
                                            entSal.Id_Ter
                                          };
                    sqlcmd = CapaDatos.GenerarSqlCommand("spCapEntSal_Insertar", ref verificador, ParametrosEntSal, ValoresEntSal);
                    entSal.Id_Es = verificador; //clave (folio) de entrada-salida generado

                    // ----------------------------------------
                    // Insertar detalle de Entrada-Salida
                    // ----------------------------------------
                    string[] ParametrosDetalleEntSal = {
                                        "@Id_Emp",
                                        "@Id_Cd",
                                        "@Id_Es",
                                        "@Id_EsDet",
                                        "@EsDet_Naturaleza",
                                        "@Id_Ter",
                                        "@Id_Prd",
                                        "@Es_Cantidad",
                                        "@Es_Costo",
                                        "@Es_BuenEstado",
                                        "@EsDet_AfcOrdCom"
                                      };
                    int contEntsal = 0;
                    foreach (EntradaSalidaDetalle entSalDetalle in listaEntSalDetalle)
                    {
                        object[] ValoresDetalleEntSal = {    
                                            entSalDetalle.Id_Emp
                                            ,entSalDetalle.Id_Cd
                                            ,entSal.Id_Es
                                            ,contEntsal
                                            ,entSalDetalle.EsDet_Naturaleza
                                            ,entSalDetalle.Id_Ter
                                            ,entSalDetalle.Id_Prd
                                            ,entSalDetalle.Es_Cantidad
                                            ,entSalDetalle.Es_Costo
                                            ,entSalDetalle.Es_BuenEstado
                                            ,entSalDetalle.Afct_OrdCompra
                                          };
                        sqlcmd = CapaDatos.GenerarSqlCommand("spCapEntSalDet_FactAparatosInproductivos_Insertar", ref verificador, ParametrosDetalleEntSal, ValoresDetalleEntSal);
                        contEntsal++;
                    }
                }
                #endregion
                CapaDatos.CommitTrans();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
                #region Nota de Crédito
                int verificado = 0;
                int generarVerificador = 0;
                int Id_Ref = 0;
                double total = 0.00;
                double porciento = 0.00;
                string referencia = string.Empty;
                List<AdendaDet> ListCab = new List<AdendaDet>();
                CapaDatos.CD_Datos CapaDatos1 = new CapaDatos.CD_Datos(Conexion);
                SqlDataReader dr = null;
                string[] ParametrosCliente = {
                                                "@Id_Emp"
                                                ,"@Id_Cd"
                                                ,"@Id_Cte"
                                             };
                object[] ValoresCliente = {    
                                            factura.Id_Emp
                                            ,factura.Id_Cd
                                            ,factura.Id_Cte
                                          };

                sqlcmd = CapaDatos1.GenerarSqlCommand("spCatCliente_validarNotaCredito", ref dr, ParametrosCliente, ValoresCliente);
                while (dr.Read())
                {
                    generarVerificador = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Validador")));
                    porciento = dr.IsDBNull(dr.GetOrdinal("Porciento")) ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Porciento")));
                    Id_Ref = dr.IsDBNull(dr.GetOrdinal("Id_Ref")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Ref")));
                    referencia = dr.IsDBNull(dr.GetOrdinal("Referencia")) ? "" : dr.GetValue(dr.GetOrdinal("Referencia")).ToString();
                }


                if (generarVerificador == 1 && porciento > 0)
                {

                    NotaCredito notaCredito = new NotaCredito();



                    notaCredito.Id_Emp = factura.Id_Emp;
                    notaCredito.Id_Cd = factura.Id_Cd;
                    notaCredito.Id_Ncr = 0;
                    notaCredito.Id_Cfe = Id_Ref;
                    notaCredito.Id_NcrSerie = referencia;
                    notaCredito.Id_Tm = 4; // Descuento aplicado a la factura
                    notaCredito.Id_Cte = factura.Id_Cte;
                    notaCredito.Id_Ter = factura.Id_Ter;
                    notaCredito.Id_Rik = factura.Id_Rik;
                    notaCredito.Id_U = factura.Id_U;
                    notaCredito.Ncr_Tipo = 1; // 1-- tipo factura
                    notaCredito.Ncr_Fecha = factura.Fac_Fecha;
                    notaCredito.Ncr_EmpleadoNumNomina = null;
                    notaCredito.Ncr_EmpleadoNombre = null;
                    notaCredito.Ncr_CtaContable = null;
                    notaCredito.Ncr_Movimiento = 1;
                    notaCredito.Ncr_Referencia = factura.Id_Fac;
                    notaCredito.Ncr_Saldo = factura.Fac_Importe;
                    notaCredito.Ncr_Desgloce = true;
                    notaCredito.Ncr_DesglocePartidas = false;
                    notaCredito.Ncr_Notas = "Descuento aplicado a la factura";
                    notaCredito.Ncr_CteDIVA = false;
                    total = (porciento / 100);
                    notaCredito.Ncr_Subtotal = factura.Fac_SubTotal * total;
                    notaCredito.Ncr_Iva = factura.Fac_ImporteIva * total;

                    notaCredito.Ncr_Total = notaCredito.Ncr_Subtotal + notaCredito.Ncr_Iva;
                    notaCredito.Ncr_Pagado = notaCredito.Ncr_Subtotal + notaCredito.Ncr_Iva;
                    notaCredito.Ncr_FecPagado = DateTime.Now;
                    notaCredito.Ncr_Estatus = "I";
                    notaCredito.Ncr_ReferenciaSerie = referencia;

                    DataTable ListaProductosNotaCredito = new DataTable();
                    ListaProductosNotaCredito.Columns.Add("Id_Emp");
                    ListaProductosNotaCredito.Columns.Add("Id_Cd");
                    ListaProductosNotaCredito.Columns.Add("Id_Ncr");
                    ListaProductosNotaCredito.Columns.Add("Id_NcrDet");
                    ListaProductosNotaCredito.Columns.Add("Id_Ter");
                    ListaProductosNotaCredito.Columns.Add("Ter_Nombre");
                    ListaProductosNotaCredito.Columns.Add("Id_Rik");
                    ListaProductosNotaCredito.Columns.Add("Rik_Nombre");
                    ListaProductosNotaCredito.Columns.Add("Id_Prd");
                    ListaProductosNotaCredito.Columns.Add("Prd_Nombre");
                    ListaProductosNotaCredito.Columns.Add("Ncr_Importe", typeof(System.Double));

                    cont1 = 1;
                    foreach (DataRow facturaDet in listaFacturaDet.Rows)
                    {
                        double precio = facturaDet["Fac_Precio"] != null ? Convert.ToDouble(facturaDet["Fac_Precio"]) : 0.00;
                        double cantidad = facturaDet["Fac_Cant"] != null ? Convert.ToDouble(facturaDet["Fac_Cant"]) : 0.00;

                        ListaProductosNotaCredito.Rows.Add(
                                           facturaDet["Id_Emp"]
                                           , facturaDet["Id_Cd"]
                                           , 0
                                           , cont1
                                           , facturaDet["Id_Ter"]
                                           , ""
                                           , factura.Id_Rik
                                           , ""
                                           , facturaDet["Id_Prd"]
                                           , ""
                                           , ((precio * cantidad) * total)
                                    );
                        cont1++;
                    }

                    CD_CapNotaCredito cdCredito = new CD_CapNotaCredito();
                    cdCredito.InsertarNotaCredito(ref notaCredito, sesion.Emp_Cnx, ref verificado, ListCab, ListaProductosNotaCredito);
                    CapaDatos1.LimpiarSqlcommand(ref sqlcmd);
                }
                #endregion
            }
            catch (Exception ex)
            {
                CapaDatos.RollBackTrans();
                throw ex;
            }
        }



        public void DepuraPartidasEliminadas(Sesion sesion, int Id_Fact, DataTable listaFacturaDet, int Id_Ped)
        {
            try
            {
                SqlDataReader dr = null;


                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(sesion.Emp_Cnx);


                string[] parametros = { 
                                          "@Id_Emp",
                                              "@Id_Cd",
                                          "@Id_Fact", 
                                      };

                string[] Valores = {
                                       sesion.Id_Emp.ToString(),
                                       sesion.Id_Cd_Ver.ToString(),
                                       Id_Fact.ToString()
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapFactura_Consultar_Con", ref dr, parametros, Valores);

                string Existe = "N";
                int verificador = 0;

                FacturaDet detalleCarga = new FacturaDet();
                List<FacturaDet> listaFactura = new List<FacturaDet>();
                while (dr.Read())
                {
                    detalleCarga = new FacturaDet();
                    detalleCarga.Id_Prd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Prd")));
                    detalleCarga.Id_Ter = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Ter")));
                    detalleCarga.Fac_Cant = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Fac_Cant")));
                    listaFactura.Add(detalleCarga);
                }

                dr.Close();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

                foreach (FacturaDet lista in listaFactura)
                {
                    Existe = "N";
                    foreach (DataRow detalle in listaFacturaDet.Rows)
                    {
                        if (detalle["Id_Ter"].ToString() == lista.Id_Ter.ToString() && detalle["Id_Prd"].ToString() == lista.Id_Prd.ToString())
                        {
                            Existe = "S";
                        }
                    }

                    if (Existe == "N")
                    {
                        string[] parametros1 = { 
                                    "@Id_Emp",
                                    "@Id_Cd",
                                    "@Id_Fact", 
                                    "@Id_Ter", 
                                    "@Id_Prd", 
                                    "@Id_Ped", 
                                    "@Fac_Cant"
                                };

                        string[] Valores1 = {
                                       sesion.Id_Emp.ToString(),
                                       sesion.Id_Cd_Ver.ToString(),
                                       Id_Fact.ToString(),
                                       lista.Id_Ter.ToString(),
                                       lista.Id_Prd.ToString(),
                                       Id_Ped.ToString(),
                                       lista.Fac_Cant.ToString()
                                   };
                        CapaDatos.CD_Datos CapaDatos1 = new CapaDatos.CD_Datos(sesion.Emp_Cnx);

                        SqlCommand sqlcmd1 = CapaDatos1.GenerarSqlCommand("spCapFactura_Eliminar_Prd", ref verificador, parametros1, Valores1);
                        CapaDatos1.LimpiarSqlcommand(ref sqlcmd1);

                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void ModificarFactura(Sesion sesion, ref Factura factura, ref DataTable listaFacturaDet, ref DataTable listaFacturaDetAdenda, int CantidadR, string Conexion, ref int verificador, ref int? Id_Ped,
    ref List<EntradaSalida> listaEntSalRemisiones, List<AdendaDet> listAdendaCabecera, string arrayRemisiones, ref EntradaSalida entSal, ref List<EntradaSalidaDetalle> listaEntSalDetalle, ref FacturaEspecial facturaEsp, ref   List<FacturaDet> listaProductosFacturaEspecial, bool actualizar)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {
                CapaDatos.StartTrans();

                #region Validacion
                if (factura.Id_Tm == 51)
                {
                    int CantT = 0;
                    int CantT1 = 0;

                    Factura fac = new Factura();
                    fac.Id_Emp = factura.Id_Emp;
                    fac.Id_Cd = factura.Id_Cd;
                    fac.Id_Fac = factura.Id_Fac;
                    List<FacturaDet> list = new List<FacturaDet>();
                    ConsultaFactura(ref fac, ref list, Conexion);
                    Producto prod = new Producto();
                    CD_CatProducto cn_producto = new CD_CatProducto();
                    CD_CapEntradaSalida CNentrada = new CD_CapEntradaSalida();

                    List<Producto> lp = new List<Producto>();
                    Producto p;

                    foreach (DataRow dr in listaFacturaDet.Rows)
                    {
                        p = new Producto();
                        List<FacturaDet> a = list.Where(FacturaDet => FacturaDet.Id_Prd == Convert.ToInt32(dr["Id_Prd"])).ToList();
                        if (a.Count > 0)
                        {

                            p.Prd_AgrupadoSpo = a[0].Prd_Agrupador;

                        }
                        else
                        {
                            cn_producto.ConsultaProducto(ref prod, Conexion, factura.Id_Emp, factura.Id_Cd, Convert.ToInt32(dr["Id_Prd"]));
                            p.Prd_AgrupadoSpo = prod.Prd_AgrupadoSpo;
                        }
                        p.CantFact = Convert.ToDouble(dr["Fac_Cant"]);
                        lp.Add(p);
                    }


                    foreach (FacturaDet fd in list)
                    {
                        CantT = 0;
                        CantT1 = 0;
                        if (fd.Prd_Agrupador != -1)
                        {
                            List<Producto> TProducto = lp.Where(Producto => Producto.Prd_AgrupadoSpo == fd.Prd_Agrupador).ToList();
                            CantT = Convert.ToInt32(TProducto.Sum(Producto => Producto.CantFact));

                            foreach (FacturaDet fd1 in list)
                            {
                                if (fd.Prd_Agrupador == fd1.Prd_Agrupador)
                                {
                                    CantT1 += fd1.Fac_Cant;
                                }
                            }

                            int saldo = 0;
                            CNentrada.ConsultarSaldo(factura.Id_Emp, factura.Id_Cd, fd.Id_Prd.ToString(), fd.Id_Ter.ToString(), factura.Id_Cte.ToString(), Conexion, ref saldo, "14");

                            if (saldo - (CantT1 - CantT) < 0)
                            {
                                throw new System.ArgumentException("El producto " + fd.Id_Prd.ToString() + " no cuenta con saldo suficiente|producto_insuficiente");
                            }
                        }
                    }
                }
                #endregion

                #region facturas
                // Se consultan y concatenan las cantidades que originalmente traian las partidas de la factura.
                string sValoresOriginales = "";


                DepuraPartidasEliminadas(sesion, factura.Id_Fac, listaFacturaDet, Convert.ToInt32(factura.Fac_PedNum));

                foreach (DataRow dr in listaFacturaDet.Rows)
                    sValoresOriginales += dr["Id_Prd"].ToString() + "," + dr["Fac_Cant"].ToString() + "|";

                sValoresOriginales = sValoresOriginales.Substring(0, sValoresOriginales.Length - 1);

                object[] Valores = {    
                                        factura.Id_Emp
                                        ,factura.Id_Cd
                                        ,factura.Id_Fac
                                        ,factura.Id_Cfe
                                        ,factura.Id_FacSerie
                                        ,factura.Id_U
                                        ,factura.Id_Tm
                                        ,factura.Fac_PedNum
                                        ,factura.Fac_PedDesc
                                        ,factura.Fac_Req
                                        ,factura.Fac_Fecha
                                        ,factura.Id_Cte
                                        ,factura.Id_Ter
                                        ,factura.Id_Rik
                                        ,factura.Id_Mon
                                        ,factura.Fac_DesgIva
                                        ,factura.Fac_RetIva
                                        ,factura.Fac_CteCalle
                                        ,factura.Fac_CteNumero
                                        ,factura.Fac_CteNumeroInterior
                                        ,factura.Fac_CteCp
                                        ,factura.Fac_CteColonia
                                        ,factura.Fac_CteMunicipio
                                        ,factura.Fac_CteEstado
                                        ,factura.Fac_CteRfc
                                        ,factura.Fac_CteTel
                                        ,factura.Fac_OrdEntrega
                                        ,factura.Fac_CondEntrega
                                        ,factura.Fac_NumEntrega
                                        ,factura.Fac_Notas
                                        ,factura.Fac_DescPorcen1
                                        ,factura.Fac_Desc1
                                        ,factura.Fac_DescPorcen2
                                        ,factura.Fac_Desc2
                                        ,factura.Fac_Tipo
                                        ,factura.Fac_Conducto
                                        ,factura.Fac_NumeroGuia
                                        ,factura.Fac_CanNum
                                        ,factura.Fac_FecCan
                                        ,factura.Fac_Pagado
                                        ,factura.Fac_FecPag
                                        ,factura.Fac_Importe
                                        ,factura.Fac_SubTotal
                                        ,factura.Fac_ImporteIva
                                        ,factura.Fac_ImporteRetencion
                                        ,factura.Fac_Estatus
                                        ,Id_Ped //si es facturacion con pedido previo, el pedido (segun esta clave) guarda la referencia de la factura
                                        //,factura.Id_Rem_Lista //si es actualacion de factura de remisiones actualiza cantidades de producto remisionadas
                                        ,(object)null
                                        ,factura.Fac_Contacto
                                        ,factura.Fac_FPago
                                        ,factura.Fac_UDigitos
                                        ,sValoresOriginales
                                        ,factura.Fac_FechaRef
                                        ,factura.Fac_IdUsuRef
                                        ,factura.Fac_IdCausaRef
                                        ,factura.Fac_TipoRef
                                        ,factura.Fac_EsRefactura
                                   };
                // ---------------------------
                // Insertar factura
                // ---------------------------
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapFactura_Modificar", ref verificador, Parametros, Valores);
                #endregion

                #region Ent/SalRemisiones
                int verificador2 = 0;
                foreach (EntradaSalida entSalRem in listaEntSalRemisiones)
                {
                    int id_remision = Convert.ToInt32(entSalRem.Es_Referencia);//referencia a remision
                    entSalRem.Es_Referencia = factura.Id_Fac.ToString(); //referencia a remision se sustituye por referencia a la factura
                    string[] ParametrosEntSalRem = {
                                            "@Id_Emp"
                                            ,"@Id_Cd"
                                            ,"@Id_U"
                                            ,"@Es_Naturaleza"
                                            ,"@Es_Fecha"
                                            ,"@Id_Tm"
                                            ,"@Id_Cte"
                                            ,"@Id_Pvd"
                                            ,"@Es_Referencia"
                                            ,"@Es_ReferenciaRem"
                                            ,"@Es_Notas"
                                            ,"@Es_Subtotal"
                                            ,"@Es_Iva"
                                            ,"@Es_Total"
                                            ,"@Es_Estatus"
                                            ,"@ManAut"
                                            ,"@Id_Rem"
                                            ,"@Id_Ter"
                                            ,"@Id_Fac"
                                      };
                    object[] ValoresEntSalRem = {    
                                            entSalRem.Id_Emp
                                            ,entSalRem.Id_Cd
                                            ,entSalRem.Id_U
                                            ,entSalRem.Es_Naturaleza
                                            ,factura.Fac_Fecha
                                            ,entSalRem.Id_Tm
                                            ,entSalRem.Id_Cte
                                            ,entSalRem.Id_Pvd == -1 ? (object)null : entSalRem.Id_Pvd
                                            ,entSalRem.Es_Referencia //referencia factura
                                            ,entSalRem.Id_Rem// == -1 ? (object)null : entSalRem.Id_Rem // id_remision //referencia remision
                                            ,entSalRem.Es_Notas
                                            ,entSalRem.Es_SubTotal
                                            ,entSalRem.Es_Iva
                                            ,entSalRem.Es_Total
                                            ,entSalRem.Es_Estatus
                                            ,0
                                            ,entSalRem.Id_Rem == -1 ? (object)null : entSalRem.Id_Rem
                                            ,entSalRem.Id_Ter
                                            ,factura.Id_Fac
                                          };

                    sqlcmd = CapaDatos.GenerarSqlCommand("spCapEntSal_FacturacionRemision_Insertar", ref verificador2, ParametrosEntSalRem, ValoresEntSalRem);
                    //entSalRem.Id_Es = verificador2; //clave (folio) de entrada-salida generado
                }
                #endregion
                //INSERTA ADENDA CABECERA
                #region adenda
                Parametros = new string[] {
                                            "@Id_Emp",			
                                            "@Id_Cd",				
                                            "@Id_Fac",
                                            "@Id_Cte",
                                            "@Id_AdeDet",
                                            "@Ade_Tipo",
                                            "@Ade_Valor",
                                            "@Contador" 
                                      };

                int cont = 0;
                foreach (AdendaDet adendaF in listAdendaCabecera)
                {
                    Valores = new object[] { 
                        factura.Id_Emp,
                        factura.Id_Cd,
                        factura.Id_Fac,
                        factura.Id_Cte,
                        adendaF.Id_AdeDet,
                        adendaF.Tipo,
                        adendaF.Valor,
                        cont
                    };
                    cont++;
                    sqlcmd = CapaDatos.GenerarSqlCommand("spCapFacturaAdenda_Insertar", ref verificador, Parametros, Valores);
                }
                #endregion
                //INSERTAR ADENDA DETALLE
                #region adenda detalle
                Parametros = new string[] {
                                            "@Id_Emp",			
                                            "@Id_Cd",				
                                            "@Id_Fac",
                                            "@Id_Cte",
                                            "@Id_AdeDet",
                                            "@Ade_Tipo",
                                            "@Ade_Valor",
                                            "@Contador",
                                            "@Id_Prd"
                                            //"@Id_Ter"
                                      };

                foreach (DataRow facturaDet in listaFacturaDetAdenda.Rows)
                {
                    for (int i = 3; i < listaFacturaDetAdenda.Columns.Count; i++)
                    {
                        Valores = new object[] { 
                            factura.Id_Emp,
                            factura.Id_Cd,
                            factura.Id_Fac,
                            factura.Id_Cte,
                            listaFacturaDetAdenda.Columns[i].ColumnName,
                            (i + CantidadR) >= listaFacturaDetAdenda.Columns.Count ? 8 : 2,
                            facturaDet[i],
                            i,
                            facturaDet["Id_Prd"],
                            //facturaDet["Id_Ter"]
                        };
                        sqlcmd = CapaDatos.GenerarSqlCommand("spCapFacturaAdenda_Insertar", ref verificador, Parametros, Valores);
                    }
                }
                #endregion
                // ------------------------------------
                // Insertar nuevo detalle de factura
                // ------------------------------------


                #region factura detalle
                string[] ParametrosDetalle = {
                                            "@Id_Emp"			
                                            ,"@Id_Cd"				
                                            ,"@Id_Fac"
                                            ,"@Id_FacDet"		
                                            ,"@Id_Ter"			
                                            ,"@Id_Prd"			
                                            ,"@Id_Cte"
			                                ,"@Id_Ped"
                                            ,"@Id_Rem"
                                            ,"@Fac_Cant"		
                                            ,"@Fac_Precio"		
                                            ,"@Fac_Asignar"
                                            ,"@Fac_Amortizacion"
                                            ,"@Rem_Cantidad"
                                            ,"@Rem_CantidadList"
                                      };
                cont = 0;
                foreach (DataRow facturaDet in listaFacturaDet.Rows)
                {
                    object[] ValoresDetalle = {    
                                            facturaDet["Id_Emp"]				
                                            ,facturaDet["Id_Cd"]				
                                            ,factura.Id_Fac //clave del encabezado de la factura
                                            ,cont
                                            ,facturaDet["Id_Ter"]			
                                            ,facturaDet["Id_Prd"]			
                                            ,facturaDet["Id_CteExt"]			
                                            ,Id_Ped //si es facturacion con pedido previo, actualiza cantidades del pedido
                                            ,facturaDet["Remisiones"]//facturaDet["Id_Rem"] //si es facturacion de remision, se actualiza la cantidad remisionada del producto
                                            ,facturaDet["Fac_Cant"]		
                                            ,facturaDet["Fac_Precio"]		
                                            ,0
                                            ,facturaDet["AmortizacionProducto"]
                                            ,facturaDet["Rem_Cant"]
                                            ,facturaDet["RemisionesXML"]
                                          };
                    sqlcmd = CapaDatos.GenerarSqlCommand("spCapFacturaDetalle_Insertar", ref verificador, ParametrosDetalle, ValoresDetalle);
                    cont++;
                }
                #endregion


                if (facturaEsp != null)
                {
                    // Insertar detalle de factura especial
                    #region detalle factura especial
                    string[] ParametrosFacturaEspecial = { 
                                        "@Id_Emp"
                                        ,"@Id_Cd"
                                        ,"@Id_Fac"
                                        ,"@Id_Ter"
                                        ,"@FacEsp_Fecha"
                                        ,"@FacEsp_Importe"
                                        ,"@FacEsp_SubTotal"
                                        ,"@FacEsp_ImporteIva"
                                        ,"@FacEsp_Total"
                                        ,"@actualizar"
                                      };
                    object[] ValoresFacturaEspecial = { 
                                        facturaEsp.Id_Emp
                                        ,facturaEsp.Id_Cd
                                        ,facturaEsp.Id_Fac
                                        ,facturaEsp.Id_Ter
                                        ,facturaEsp.FacEsp_Fecha
                                        ,facturaEsp.FacEsp_Importe
                                        ,facturaEsp.FacEsp_SubTotal
                                        ,facturaEsp.FacEsp_ImporteIva
                                        ,facturaEsp.FacEsp_Total
                                        ,actualizar
                                   };
                    sqlcmd = CapaDatos.GenerarSqlCommand("spCapFacturaEspecial_Insertar", ref verificador, ParametrosFacturaEspecial, ValoresFacturaEspecial);
                }
                // -----------------------------------------------------------------
                // Parametros de  detalle de factura epsecial y Cliente-Producto
                // -----------------------------------------------------------------
                string[] ParametrosFacturaEspecialDetalle = { 
                                        "@Id_Emp"
                                        ,"@Id_Cd"
                                        ,"@Id_Fac"
                                        ,"@Id_FacDet"
                                        ,"@Id_Prd"
                                        ,"@Id_PrdEsp"
                                        ,"@FacEsp_Descripcion"
                                        ,"@FacEsp_Presentacion"
                                        ,"@FacEsp_Unidades"
                                        ,"@FacEsp_Release"
                                        ,"@Fac_Cant"
                                        ,"@Fac_Precio"
                                        ,"@Prd_ClaveProdServ"
                                        ,"@Prd_ClaveUnidad"

                                      };
                int contEspecial = 0;
                foreach (FacturaDet facturaDet in listaProductosFacturaEspecial)
                {
                    // --------------------------------------
                    // Insertar detalle de factura epsecial
                    // --------------------------------------
                    object[] ValoresFacturaEspecialDetalle = { 
                                                        facturaDet.Id_Emp
                                                        ,facturaDet.Id_Cd
                                                        ,facturaEsp.Id_Fac
                                                        ,contEspecial
                                                        ,facturaDet.Id_Prd
                                                        ,facturaDet.Producto.Id_PrdEsp
                                                        ,facturaDet.Producto.Prd_DescripcionEspecial.Split(new string[]{"|"}, StringSplitOptions.RemoveEmptyEntries).Length > 0 ? facturaDet.Producto.Prd_DescripcionEspecial.Split(new string[]{"|"}, StringSplitOptions.RemoveEmptyEntries)[0] : ""
                                                        ,facturaDet.Producto.Prd_Presentacion
                                                        ,facturaDet.Producto.Prd_UniNe
                                                        ,facturaDet.Clp_Release
                                                        ,facturaDet.Fac_CantE //cantidad facturada
                                                        ,facturaDet.Fac_Precio
                                                        ,facturaDet.Fac_ClaveProdServ //cantidad facturada
                                                        ,facturaDet.Fac_ClaveUnidad

                                   };
                    sqlcmd = CapaDatos.GenerarSqlCommand("spCapFacturaEspecialDetalle_Insertar"
                        , ref verificador, ParametrosFacturaEspecialDetalle, ValoresFacturaEspecialDetalle);
                    contEspecial++; //Aumenta contador de partida
                }
                    #endregion



                // ---------------------------------------------------------------------------------------------------
                // Insertar movimientos de Entrada inversos de remisiones, si es que s una facturacion de remisiones
                // ---------------------------------------------------------------------------------------------------
                #region entradas / salidas
                // ---------------------------
                // Insertar detalle
                // ---------------------------

                string[] ParametrosDetalleEntSalRem = {
                                        "@Id_Emp",
                                        "@Id_Cd",                                    
                                        "@Id_Rem",                                        
                                        "@Id_Fac"
                                      };
                // ----------------------------------------
                // Insertar detalle de Entrada-Salida
                // ----------------------------------------
                object[] ValoresDetalleEntSalRem = {    
                                            factura.Id_Emp
                                            ,factura.Id_Cd                                     
                                            ,string.IsNullOrEmpty(arrayRemisiones) ? (object)null : arrayRemisiones                                         
                                            ,factura.Id_Fac
                                          };
                sqlcmd = CapaDatos.GenerarSqlCommand("spCapEntSalDet_FactRemision_Insertar", ref verificador, ParametrosDetalleEntSalRem, ValoresDetalleEntSalRem);
                #endregion

                #region Aparatos Improductivos
                if (factura.Id_Tm == 70)
                {
                    string[] ParametrosEntSal = {
                                            "@Id_Emp"
                                            ,"@Id_Cd"
                                            ,"@Id_Es"
                                            ,"@Id_U"
                                            ,"@Es_Naturaleza"
                                            ,"@Es_Fecha"
                                            ,"@Id_Tm"
                                            ,"@Id_Cte"
                                            ,"@Id_Pvd"
                                            ,"@Es_Referencia"
                                            ,"@Es_Notas"
                                            ,"@Es_Subtotal"
                                            ,"@Es_Iva"
                                            ,"@Es_Total"
                                            ,"@Es_Estatus"
                                            ,"@ManAut"
                                      };
                    object[] ValoresEntSal = {    
                                            entSal.Id_Emp
                                            ,entSal.Id_Cd
                                            ,entSal.Id_Es
                                            ,entSal.Id_U
                                            ,entSal.Es_Naturaleza
                                            ,entSal.Es_Fecha
                                            ,entSal.Id_Tm
                                            ,entSal.Id_Cte
                                            ,entSal.Id_Pvd == -1 ? (object)null : entSal.Id_Pvd
                                            ,entSal.Es_Referencia
                                            ,entSal.Es_Notas
                                            ,entSal.Es_SubTotal
                                            ,entSal.Es_Iva
                                            ,entSal.Es_Total
                                            ,entSal.Es_Estatus
                                            ,0
                                          };
                    sqlcmd = CapaDatos.GenerarSqlCommand("spCapEntSal_FactAparatosInproductivos_Modificar", ref verificador, ParametrosEntSal, ValoresEntSal);


                    string[] ParametrosDetalleEntSal = {
                                        "@Id_Emp",
                                        "@Id_Cd",
                                        "@Id_Es",
                                        "@Id_EsDet",
                                        "@EsDet_Naturaleza",
                                        "@Id_Ter",
                                        "@Id_Prd",
                                        "@Es_Cantidad",
                                        "@Es_Costo",
                                        "@Es_BuenEstado",
                                        "@EsDet_AfcOrdCom"
                                      };
                    cont = 0;
                    foreach (EntradaSalidaDetalle entSalDetalle in listaEntSalDetalle)
                    {
                        object[] ValoresDetalleEntSal = {    
                                            entSalDetalle.Id_Emp
                                            ,entSalDetalle.Id_Cd
                                            ,entSal.Id_Es
                                            ,cont
                                            ,entSalDetalle.EsDet_Naturaleza
                                            ,entSalDetalle.Id_Ter
                                            ,entSalDetalle.Id_Prd
                                            ,entSalDetalle.Es_Cantidad
                                            ,entSalDetalle.Es_Costo
                                            ,entSalDetalle.Es_BuenEstado
                                            ,entSalDetalle.Afct_OrdCompra
                                          };
                        sqlcmd = CapaDatos.GenerarSqlCommand("spCapEntSalDet_FactAparatosInproductivos_Insertar", ref verificador, ParametrosDetalleEntSal, ValoresDetalleEntSal);
                        cont++;
                    }
                }
                #endregion

                CapaDatos.CommitTrans();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);


                #region NotaCredito
                int verificado = 0;
                int generarVerificador = 0;
                int Id_Ref = 0;
                double total = 0.00;
                double porciento = 0.00;
                string referencia = string.Empty;
                List<AdendaDet> ListCab = new List<AdendaDet>();
                CapaDatos.CD_Datos CapaDatos1 = new CapaDatos.CD_Datos(Conexion);
                CD_CapNotaCredito cd_nc = new CD_CapNotaCredito();
                SqlDataReader dr1 = null;
                string[] ParametrosCliente = {
                                                "@Id_Emp"
                                                ,"@Id_Cd"
                                                ,"@Id_Cte"
                                             };
                object[] ValoresCliente = {    
                                            factura.Id_Emp
                                            ,factura.Id_Cd
                                            ,factura.Id_Cte
                                          };

                sqlcmd = CapaDatos1.GenerarSqlCommand("spCatCliente_validarNotaCredito", ref dr1, ParametrosCliente, ValoresCliente);

                if (dr1.Read())
                {
                    generarVerificador = Convert.ToInt32(dr1.GetValue(dr1.GetOrdinal("Validador")));
                    porciento = dr1.IsDBNull(dr1.GetOrdinal("Porciento")) ? 0 : Convert.ToDouble(dr1.GetValue(dr1.GetOrdinal("Porciento")));
                    Id_Ref = dr1.IsDBNull(dr1.GetOrdinal("Id_Ref")) ? 0 : Convert.ToInt32(dr1.GetValue(dr1.GetOrdinal("Id_Ref")));
                    referencia = dr1.IsDBNull(dr1.GetOrdinal("Referencia")) ? "" : dr1.GetValue(dr1.GetOrdinal("Referencia")).ToString();
                }

                if (generarVerificador == 1 && porciento > 0)
                {

                    NotaCredito notaCredito = new NotaCredito();
                    //JMM:Obtengo el Id de nota de credito
                    int Id_Ncr = 0;
                    string Estatus = string.Empty;

                    cd_nc.ConsultarIdNotaCredito(factura.Id_Fac, factura.Id_Cd, factura.Id_Emp, ref Id_Ncr, ref Estatus, sesion.Emp_Cnx);
                    if (Estatus != "B")
                    {
                        notaCredito.Id_Emp = factura.Id_Emp;
                        notaCredito.Id_Cd = factura.Id_Cd;
                        notaCredito.Id_Ncr = Id_Ncr;
                        notaCredito.Id_Cfe = Id_Ref;
                        notaCredito.Id_NcrSerie = referencia;
                        notaCredito.Id_Tm = 4; // Descuento aplicado a la factura
                        notaCredito.Id_Cte = factura.Id_Cte;
                        notaCredito.Id_Ter = factura.Id_Ter;
                        notaCredito.Id_Rik = factura.Id_Rik;
                        notaCredito.Id_U = factura.Id_U;
                        notaCredito.Ncr_Tipo = 1; // 1-- tipo factura
                        notaCredito.Ncr_Fecha = factura.Fac_Fecha;
                        notaCredito.Ncr_EmpleadoNumNomina = null;
                        notaCredito.Ncr_EmpleadoNombre = null;
                        notaCredito.Ncr_CtaContable = null;
                        notaCredito.Ncr_Movimiento = 1;
                        notaCredito.Ncr_Referencia = factura.Id_Fac;
                        notaCredito.Ncr_Saldo = factura.Fac_Importe;
                        notaCredito.Ncr_Desgloce = true;
                        notaCredito.Ncr_DesglocePartidas = false;
                        notaCredito.Ncr_Notas = "Descuento aplicado a la factura";
                        notaCredito.Ncr_CteDIVA = false;
                        total = (porciento / 100);
                        notaCredito.Ncr_Subtotal = factura.Fac_SubTotal * total;
                        notaCredito.Ncr_Iva = factura.Fac_ImporteIva * total;

                        notaCredito.Ncr_Total = notaCredito.Ncr_Subtotal + notaCredito.Ncr_Iva;
                        notaCredito.Ncr_Pagado = notaCredito.Ncr_Subtotal + notaCredito.Ncr_Iva;
                        notaCredito.Ncr_FecPagado = DateTime.Now;
                        notaCredito.Ncr_Estatus = "I";
                        notaCredito.Ncr_ReferenciaSerie = referencia;

                        DataTable ListaProductosNotaCredito = new DataTable();
                        ListaProductosNotaCredito.Columns.Add("Id_Emp");
                        ListaProductosNotaCredito.Columns.Add("Id_Cd");
                        ListaProductosNotaCredito.Columns.Add("Id_Ncr");
                        ListaProductosNotaCredito.Columns.Add("Id_NcrDet");
                        ListaProductosNotaCredito.Columns.Add("Id_Ter");
                        ListaProductosNotaCredito.Columns.Add("Ter_Nombre");
                        ListaProductosNotaCredito.Columns.Add("Id_Rik");
                        ListaProductosNotaCredito.Columns.Add("Rik_Nombre");
                        ListaProductosNotaCredito.Columns.Add("Id_Prd");
                        ListaProductosNotaCredito.Columns.Add("Prd_Nombre");
                        ListaProductosNotaCredito.Columns.Add("Ncr_Importe", typeof(System.Double));
                        int cont1 = 0;
                        cont1 = 1;
                        foreach (DataRow facturaDet in listaFacturaDet.Rows)
                        {
                            double precio = facturaDet["Fac_Precio"] != null ? Convert.ToDouble(facturaDet["Fac_Precio"]) : 0.00;
                            double cantidad = facturaDet["Fac_Cant"] != null ? Convert.ToDouble(facturaDet["Fac_Cant"]) : 0.00;

                            ListaProductosNotaCredito.Rows.Add(
                                               facturaDet["Id_Emp"]
                                               , facturaDet["Id_Cd"]
                                               , 0
                                               , cont1
                                               , facturaDet["Id_Ter"]
                                               , ""
                                               , factura.Id_Rik
                                               , ""
                                               , facturaDet["Id_Prd"]
                                               , ""
                                               , ((precio * cantidad) * total)
                                        );
                            cont1++;
                        }

                        CD_CapNotaCredito cdCredito = new CD_CapNotaCredito();
                        cdCredito.ModificarNotaCredito(ref notaCredito, sesion.Emp_Cnx, ref verificado, ListCab, ListaProductosNotaCredito);
                        CapaDatos1.LimpiarSqlcommand(ref sqlcmd);
                    }
                }

                #endregion

            }
            catch (Exception ex)
            {
                CapaDatos.RollBackTrans();
                throw ex;
            }
        }

        public void ModificarFactura(Sesion sesion, ref Factura factura, ref Factura facturaNacional, ref DataTable listaFacturaDet, ref DataTable listaFacturaDetAdenda, ref DataTable listaFacturaDetAdendaNacional, int CantidadR, string Conexion, ref int verificador, ref int? Id_Ped,
    ref List<EntradaSalida> listaEntSalRemisiones, List<AdendaDet> listAdendaCabecera, List<AdendaDet> listAdendaNacionalCabecera, string arrayRemisiones, ref EntradaSalida entSal, ref List<EntradaSalidaDetalle> listaEntSalDetalle, ref FacturaEspecial facturaEsp, ref   List<FacturaDet> listaProductosFacturaEspecial, bool actualizar)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {
                CapaDatos.StartTrans();

                #region Validacion
                if (factura.Id_Tm == 51)
                {
                    int CantT = 0;
                    int CantT1 = 0;

                    Factura fac = new Factura();
                    fac.Id_Emp = factura.Id_Emp;
                    fac.Id_Cd = factura.Id_Cd;
                    fac.Id_Fac = factura.Id_Fac;
                    List<FacturaDet> list = new List<FacturaDet>();
                    ConsultaFactura(ref fac, ref list, Conexion);
                    Producto prod = new Producto();
                    CD_CatProducto cn_producto = new CD_CatProducto();
                    CD_CapEntradaSalida CNentrada = new CD_CapEntradaSalida();

                    List<Producto> lp = new List<Producto>();
                    Producto p;

                    foreach (DataRow dr in listaFacturaDet.Rows)
                    {
                        p = new Producto();
                        List<FacturaDet> a = list.Where(FacturaDet => FacturaDet.Id_Prd == Convert.ToInt32(dr["Id_Prd"])).ToList();
                        if (a.Count > 0)
                        {

                            p.Prd_AgrupadoSpo = a[0].Prd_Agrupador;

                        }
                        else
                        {
                            cn_producto.ConsultaProducto(ref prod, Conexion, factura.Id_Emp, factura.Id_Cd, Convert.ToInt32(dr["Id_Prd"]));
                            p.Prd_AgrupadoSpo = prod.Prd_AgrupadoSpo;
                        }
                        p.CantFact = Convert.ToDouble(dr["Fac_Cant"]);
                        lp.Add(p);
                    }


                    foreach (FacturaDet fd in list)
                    {
                        CantT = 0;
                        CantT1 = 0;
                        if (fd.Prd_Agrupador != -1)
                        {
                            List<Producto> TProducto = lp.Where(Producto => Producto.Prd_AgrupadoSpo == fd.Prd_Agrupador).ToList();
                            CantT = Convert.ToInt32(TProducto.Sum(Producto => Producto.CantFact));

                            foreach (FacturaDet fd1 in list)
                            {
                                if (fd.Prd_Agrupador == fd1.Prd_Agrupador)
                                {
                                    CantT1 += fd1.Fac_Cant;
                                }
                            }

                            int saldo = 0;
                            CNentrada.ConsultarSaldo(factura.Id_Emp, factura.Id_Cd, fd.Id_Prd.ToString(), fd.Id_Ter.ToString(), factura.Id_Cte.ToString(), Conexion, ref saldo, "14");

                            if (saldo - (CantT1 - CantT) < 0)
                            {
                                throw new System.ArgumentException("El producto " + fd.Id_Prd.ToString() + " no cuenta con saldo suficiente|producto_insuficiente");
                            }
                        }
                    }
                }
                #endregion

                #region facturas
                object[] Valores = {    
                                        factura.Id_Emp
                                        ,factura.Id_Cd
                                        ,factura.Id_Fac
                                        ,factura.Id_Cfe
                                        ,factura.Id_FacSerie
                                        ,factura.Id_U
                                        ,factura.Id_Tm
                                        ,factura.Fac_PedNum
                                        ,factura.Fac_PedDesc
                                        ,factura.Fac_Req
                                        ,factura.Fac_Fecha
                                        ,factura.Id_Cte
                                        ,factura.Id_Ter
                                        ,factura.Id_Rik
                                        ,factura.Id_Mon
                                        ,factura.Fac_DesgIva
                                        ,factura.Fac_RetIva
                                        ,factura.Fac_CteCalle
                                        ,factura.Fac_CteNumero
                                        ,factura.Fac_CteNumeroInterior
                                        ,factura.Fac_CteCp
                                        ,factura.Fac_CteColonia
                                        ,factura.Fac_CteMunicipio
                                        ,factura.Fac_CteEstado
                                        ,factura.Fac_CteRfc
                                        ,factura.Fac_CteTel
                                        ,factura.Fac_OrdEntrega
                                        ,factura.Fac_CondEntrega
                                        ,factura.Fac_NumEntrega
                                        ,factura.Fac_Notas
                                        ,factura.Fac_DescPorcen1
                                        ,factura.Fac_Desc1
                                        ,factura.Fac_DescPorcen2
                                        ,factura.Fac_Desc2
                                        ,factura.Fac_Tipo
                                        ,factura.Fac_Conducto
                                        ,factura.Fac_NumeroGuia
                                        ,factura.Fac_CanNum
                                        ,factura.Fac_FecCan
                                        ,factura.Fac_Pagado
                                        ,factura.Fac_FecPag
                                        ,factura.Fac_Importe
                                        ,factura.Fac_SubTotal
                                        ,factura.Fac_ImporteIva
                                        ,factura.Fac_ImporteRetencion
                                        ,factura.Fac_Estatus
                                        ,Id_Ped //si es facturacion con pedido previo, el pedido (segun esta clave) guarda la referencia de la factura
                                        //,factura.Id_Rem_Lista //si es actualacion de factura de remisiones actualiza cantidades de producto remisionadas
                                        ,(object)null
                                        ,factura.Fac_Contacto
                                        ,factura.Fac_FPago
                                        ,factura.Fac_UDigitos
                                        ,(object)null
                                   };
                // ---------------------------
                // Insertar factura
                // ---------------------------
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapFactura_Modificar", ref verificador, Parametros, Valores);
                #endregion



                #region Factura Nacional
                //Factura Nacional
                Parametros = new string[] {
                    "@Id_Emp",
                    "@Id_Cd",
                    "@Id_Fac",
                    "@Id_Cte",
                    "@Cte_NomComercial",
                    "@Fac_CteCalle",
                    "@Fac_CteNumero",
                    "@Fac_CteColonia",
                    "@Fac_CteMunicipio",
                    "@Fac_CteEstado",
                    "@Fac_CteRfc",
                    "@Fac_CteCp",
                    "@Fac_CteAdeNombre"
                };
                Valores = new object[]{
                    facturaNacional.Id_Emp,
                    facturaNacional.Id_Cd,
                    factura.Id_Fac,
                    facturaNacional.Id_Cte,
                    facturaNacional.Cte_NomComercial,
                    facturaNacional.Fac_CteCalle,
                    facturaNacional.Fac_CteNumero,
                    facturaNacional.Fac_CteColonia,
                    facturaNacional.Fac_CteMunicipio,
                    facturaNacional.Fac_CteEstado,
                    facturaNacional.Fac_CteRfc,
                    facturaNacional.Fac_CteCp,
                    facturaNacional.Fac_CteAdeNombre
                };
                sqlcmd = CapaDatos.GenerarSqlCommand("spCapFacturaNacional_Modificar", ref verificador, Parametros, Valores);
                #endregion





                #region Ent/SalRemisiones
                int verificador2 = 0;
                foreach (EntradaSalida entSalRem in listaEntSalRemisiones)
                {
                    int id_remision = Convert.ToInt32(entSalRem.Es_Referencia);//referencia a remision
                    entSalRem.Es_Referencia = factura.Id_Fac.ToString(); //referencia a remision se sustituye por referencia a la factura
                    string[] ParametrosEntSalRem = {
                                            "@Id_Emp"
                                            ,"@Id_Cd"
                                            ,"@Id_U"
                                            ,"@Es_Naturaleza"
                                            ,"@Es_Fecha"
                                            ,"@Id_Tm"
                                            ,"@Id_Cte"
                                            ,"@Id_Pvd"
                                            ,"@Es_Referencia"
                                            ,"@Es_ReferenciaRem"
                                            ,"@Es_Notas"
                                            ,"@Es_Subtotal"
                                            ,"@Es_Iva"
                                            ,"@Es_Total"
                                            ,"@Es_Estatus"
                                            ,"@ManAut"
                                            ,"@Id_Rem"
                                            ,"@Id_Ter"
                                            ,"@Id_Fac"
                                      };
                    object[] ValoresEntSalRem = {    
                                            entSalRem.Id_Emp
                                            ,entSalRem.Id_Cd
                                            ,entSalRem.Id_U
                                            ,entSalRem.Es_Naturaleza
                                            ,factura.Fac_Fecha
                                            ,entSalRem.Id_Tm
                                            ,entSalRem.Id_Cte
                                            ,entSalRem.Id_Pvd == -1 ? (object)null : entSalRem.Id_Pvd
                                            ,entSalRem.Es_Referencia //referencia factura
                                            ,entSalRem.Id_Rem// == -1 ? (object)null : entSalRem.Id_Rem // id_remision //referencia remision
                                            ,entSalRem.Es_Notas
                                            ,entSalRem.Es_SubTotal
                                            ,entSalRem.Es_Iva
                                            ,entSalRem.Es_Total
                                            ,entSalRem.Es_Estatus
                                            ,0
                                            ,entSalRem.Id_Rem == -1 ? (object)null : entSalRem.Id_Rem
                                            ,entSalRem.Id_Ter
                                            ,factura.Id_Fac
                                          };

                    sqlcmd = CapaDatos.GenerarSqlCommand("spCapEntSal_FacturacionRemision_Insertar", ref verificador2, ParametrosEntSalRem, ValoresEntSalRem);
                    //entSalRem.Id_Es = verificador2; //clave (folio) de entrada-salida generado
                }
                #endregion
                //INSERTA ADENDA CABECERA
                #region adenda
                Parametros = new string[] {
                                            "@Id_Emp",			
                                            "@Id_Cd",				
                                            "@Id_Fac",
                                            "@Id_Cte",
                                            "@Id_AdeDet",
                                            "@Ade_Tipo",
                                            "@Ade_Valor",
                                            "@Contador" 
                                      };

                int cont = 0;
                foreach (AdendaDet adendaF in listAdendaCabecera)
                {
                    Valores = new object[] { 
                        factura.Id_Emp,
                        factura.Id_Cd,
                        factura.Id_Fac,
                        factura.Id_Cte,
                        adendaF.Id_AdeDet,
                        adendaF.Tipo,
                        adendaF.Valor,
                        cont
                    };
                    cont++;
                    sqlcmd = CapaDatos.GenerarSqlCommand("spCapFacturaAdenda_Insertar", ref verificador, Parametros, Valores);
                }
                #endregion
                //INSERTAR ADENDA DETALLE
                #region adenda detalle
                Parametros = new string[] {
                                            "@Id_Emp",			
                                            "@Id_Cd",				
                                            "@Id_Fac",
                                            "@Id_Cte",
                                            "@Id_AdeDet",
                                            "@Ade_Tipo",
                                            "@Ade_Valor",
                                            "@Contador",
                                            "@Id_Prd",
                                            //"@Id_Ter"
                                      };

                foreach (DataRow facturaDet in listaFacturaDetAdenda.Rows)
                {
                    for (int i = 3; i < listaFacturaDetAdenda.Columns.Count; i++)
                    {
                        Valores = new object[] { 
                            factura.Id_Emp,
                            factura.Id_Cd,
                            factura.Id_Fac,
                            factura.Id_Cte,
                            listaFacturaDetAdenda.Columns[i].ColumnName,
                            (i + CantidadR) >= listaFacturaDetAdenda.Columns.Count ? 8 : 2,
                            facturaDet[i],
                            i,
                            facturaDet["Id_Prd"],
                            //facturaDet["Id_Ter"]
                        };
                        sqlcmd = CapaDatos.GenerarSqlCommand("spCapFacturaAdenda_Insertar", ref verificador, Parametros, Valores);
                    }
                }


                //Adenda Nacional

                Parametros = new string[] {
                                            "@Id_Emp",			
                                            "@Id_Cd",				
                                            "@Id_Fac",
                                            "@Id_Cte",
                                            "@Id_AdeDet",
                                            "@Ade_Tipo",
                                            "@Ade_Valor",
                                            "@Contador" ,
                                            "@Id_Prd"
                                      };

                int cont1 = 0;
                foreach (AdendaDet adendaF in listAdendaNacionalCabecera)
                {
                    Valores = new object[] { 
                        factura.Id_Emp,
                        factura.Id_Cd,
                        factura.Id_Fac,
                        factura.Id_Cte,
                        adendaF.Id_AdeDet,
                        adendaF.Tipo,
                        adendaF.Valor,
                        cont1,
                        null
                    };
                    cont1++;
                    sqlcmd = CapaDatos.GenerarSqlCommand("spCapFacturaAdendaNacional_Insertar", ref verificador, Parametros, Valores);
                }



                foreach (DataRow facturaDet in listaFacturaDetAdendaNacional.Rows)
                {
                    for (int i = 3; i < listaFacturaDetAdendaNacional.Columns.Count; i++)
                    {
                        Valores = new object[] { 
                            factura.Id_Emp,
                            factura.Id_Cd,
                            factura.Id_Fac,
                            factura.Id_Cte,
                            listaFacturaDetAdendaNacional.Columns[i].ColumnName,
                            (i + CantidadR) >= listaFacturaDetAdendaNacional.Columns.Count ? 8 : 2,
                            facturaDet[i],
                            i,
                            facturaDet["Id_Prd"],
                            //facturaDet["Id_Ter"]
                        };
                        sqlcmd = CapaDatos.GenerarSqlCommand("spCapFacturaAdendaNacional_Insertar", ref verificador, Parametros, Valores);
                    }
                }







                #endregion
                // ------------------------------------
                // Insertar nuevo detalle de factura
                // ------------------------------------


                #region factura detalle
                string[] ParametrosDetalle = {
                                            "@Id_Emp"			
                                            ,"@Id_Cd"				
                                            ,"@Id_Fac"
                                            ,"@Id_FacDet"		
                                            ,"@Id_Ter"			
                                            ,"@Id_Prd"			
                                            ,"@Id_Cte"
			                                ,"@Id_Ped"
                                            ,"@Id_Rem"
                                            ,"@Fac_Cant"		
                                            ,"@Fac_Precio"		
                                            ,"@Fac_Asignar"
                                            ,"@Fac_Amortizacion"
                                            ,"@Rem_Cantidad"
                                            ,"@Rem_CantidadList"
                                      };
                cont = 0;
                foreach (DataRow facturaDet in listaFacturaDet.Rows)
                {
                    object[] ValoresDetalle = {    
                                            facturaDet["Id_Emp"]				
                                            ,facturaDet["Id_Cd"]				
                                            ,factura.Id_Fac //clave del encabezado de la factura
                                            ,cont
                                            ,facturaDet["Id_Ter"]			
                                            ,facturaDet["Id_Prd"]			
                                            ,facturaDet["Id_CteExt"]			
                                            ,Id_Ped //si es facturacion con pedido previo, actualiza cantidades del pedido
                                            ,facturaDet["Remisiones"]//facturaDet["Id_Rem"] //si es facturacion de remision, se actualiza la cantidad remisionada del producto
                                            ,facturaDet["Fac_Cant"]		
                                            ,facturaDet["Fac_Precio"]		
                                            ,0
                                            ,facturaDet["AmortizacionProducto"]
                                            ,facturaDet["Rem_Cant"]
                                            ,facturaDet["RemisionesXML"]
                                          };
                    sqlcmd = CapaDatos.GenerarSqlCommand("spCapFacturaDetalle_Insertar", ref verificador, ParametrosDetalle, ValoresDetalle);
                    cont++;
                }
                #endregion


                if (facturaEsp != null)
                {
                    // Insertar detalle de factura especial
                    #region detalle factura especial
                    string[] ParametrosFacturaEspecial = { 
                                        "@Id_Emp"
                                        ,"@Id_Cd"
                                        ,"@Id_Fac"
                                        ,"@Id_Ter"
                                        ,"@FacEsp_Fecha"
                                        ,"@FacEsp_Importe"
                                        ,"@FacEsp_SubTotal"
                                        ,"@FacEsp_ImporteIva"
                                        ,"@FacEsp_Total"
                                        ,"@actualizar"
                                      };
                    object[] ValoresFacturaEspecial = { 
                                        facturaEsp.Id_Emp
                                        ,facturaEsp.Id_Cd
                                        ,facturaEsp.Id_Fac
                                        ,facturaEsp.Id_Ter
                                        ,facturaEsp.FacEsp_Fecha
                                        ,facturaEsp.FacEsp_Importe
                                        ,facturaEsp.FacEsp_SubTotal
                                        ,facturaEsp.FacEsp_ImporteIva
                                        ,facturaEsp.FacEsp_Total
                                        ,actualizar
                                   };
                    sqlcmd = CapaDatos.GenerarSqlCommand("spCapFacturaEspecial_Insertar", ref verificador, ParametrosFacturaEspecial, ValoresFacturaEspecial);
                }
                // -----------------------------------------------------------------
                // Parametros de  detalle de factura epsecial y Cliente-Producto
                // -----------------------------------------------------------------
                string[] ParametrosFacturaEspecialDetalle = { 
                                        "@Id_Emp"
                                        ,"@Id_Cd"
                                        ,"@Id_Fac"
                                        ,"@Id_FacDet"
                                        ,"@Id_Prd"
                                        ,"@Id_PrdEsp"
                                        ,"@FacEsp_Descripcion"
                                        ,"@FacEsp_Presentacion"
                                        ,"@FacEsp_Unidades"
                                        ,"@FacEsp_Release"
                                        ,"@Fac_Cant"
                                        ,"@Fac_Precio"
                                        ,"@Prd_ClaveProdServ"
                                        ,"@Prd_ClaveUnidad"

                                      };
                int contEspecial = 0;
                foreach (FacturaDet facturaDet in listaProductosFacturaEspecial)
                {
                    // --------------------------------------
                    // Insertar detalle de factura epsecial
                    // --------------------------------------
                    object[] ValoresFacturaEspecialDetalle = { 
                                                        facturaDet.Id_Emp
                                                        ,facturaDet.Id_Cd
                                                        ,facturaEsp.Id_Fac
                                                        ,contEspecial
                                                        ,facturaDet.Id_Prd
                                                        ,facturaDet.Producto.Id_PrdEsp
                                                        ,facturaDet.Producto.Prd_DescripcionEspecial.Split(new string[]{"|"}, StringSplitOptions.RemoveEmptyEntries).Length > 0 ? facturaDet.Producto.Prd_DescripcionEspecial.Split(new string[]{"|"}, StringSplitOptions.RemoveEmptyEntries)[0] : ""
                                                        ,facturaDet.Producto.Prd_Presentacion
                                                        ,facturaDet.Producto.Prd_UniNe
                                                        ,facturaDet.Clp_Release
                                                        ,facturaDet.Fac_CantE //cantidad facturada
                                                        ,facturaDet.Fac_Precio
                                                        ,facturaDet.Fac_ClaveProdServ //cantidad facturada
                                                        ,facturaDet.Fac_ClaveUnidad

                                   };
                    sqlcmd = CapaDatos.GenerarSqlCommand("spCapFacturaEspecialDetalle_Insertar"
                        , ref verificador, ParametrosFacturaEspecialDetalle, ValoresFacturaEspecialDetalle);
                    contEspecial++; //Aumenta contador de partida
                }
                    #endregion



                // ---------------------------------------------------------------------------------------------------
                // Insertar movimientos de Entrada inversos de remisiones, si es que s una facturacion de remisiones
                // ---------------------------------------------------------------------------------------------------
                #region entradas / salidas
                // ---------------------------
                // Insertar detalle
                // ---------------------------

                string[] ParametrosDetalleEntSalRem = {
                                        "@Id_Emp",
                                        "@Id_Cd",                                    
                                        "@Id_Rem",                                        
                                        "@Id_Fac"
                                      };
                // ----------------------------------------
                // Insertar detalle de Entrada-Salida
                // ----------------------------------------
                object[] ValoresDetalleEntSalRem = {    
                                            factura.Id_Emp
                                            ,factura.Id_Cd                                     
                                            ,string.IsNullOrEmpty(arrayRemisiones) ? (object)null : arrayRemisiones                                         
                                            ,factura.Id_Fac
                                          };
                sqlcmd = CapaDatos.GenerarSqlCommand("spCapEntSalDet_FactRemision_Insertar", ref verificador, ParametrosDetalleEntSalRem, ValoresDetalleEntSalRem);
                #endregion

                #region Aparatos Improductivos
                if (factura.Id_Tm == 70)
                {
                    string[] ParametrosEntSal = {
                                            "@Id_Emp"
                                            ,"@Id_Cd"
                                            ,"@Id_Es"
                                            ,"@Id_U"
                                            ,"@Es_Naturaleza"
                                            ,"@Es_Fecha"
                                            ,"@Id_Tm"
                                            ,"@Id_Cte"
                                            ,"@Id_Pvd"
                                            ,"@Es_Referencia"
                                            ,"@Es_Notas"
                                            ,"@Es_Subtotal"
                                            ,"@Es_Iva"
                                            ,"@Es_Total"
                                            ,"@Es_Estatus"
                                            ,"@ManAut"
                                      };
                    object[] ValoresEntSal = {    
                                            entSal.Id_Emp
                                            ,entSal.Id_Cd
                                            ,entSal.Id_Es
                                            ,entSal.Id_U
                                            ,entSal.Es_Naturaleza
                                            ,entSal.Es_Fecha
                                            ,entSal.Id_Tm
                                            ,entSal.Id_Cte
                                            ,entSal.Id_Pvd == -1 ? (object)null : entSal.Id_Pvd
                                            ,entSal.Es_Referencia
                                            ,entSal.Es_Notas
                                            ,entSal.Es_SubTotal
                                            ,entSal.Es_Iva
                                            ,entSal.Es_Total
                                            ,entSal.Es_Estatus
                                            ,0
                                          };
                    sqlcmd = CapaDatos.GenerarSqlCommand("spCapEntSal_FactAparatosInproductivos_Modificar", ref verificador, ParametrosEntSal, ValoresEntSal);


                    string[] ParametrosDetalleEntSal = {
                                        "@Id_Emp",
                                        "@Id_Cd",
                                        "@Id_Es",
                                        "@Id_EsDet",
                                        "@EsDet_Naturaleza",
                                        "@Id_Ter",
                                        "@Id_Prd",
                                        "@Es_Cantidad",
                                        "@Es_Costo",
                                        "@Es_BuenEstado",
                                        "@EsDet_AfcOrdCom"
                                      };
                    cont = 0;
                    foreach (EntradaSalidaDetalle entSalDetalle in listaEntSalDetalle)
                    {
                        object[] ValoresDetalleEntSal = {    
                                            entSalDetalle.Id_Emp
                                            ,entSalDetalle.Id_Cd
                                            ,entSal.Id_Es
                                            ,cont
                                            ,entSalDetalle.EsDet_Naturaleza
                                            ,entSalDetalle.Id_Ter
                                            ,entSalDetalle.Id_Prd
                                            ,entSalDetalle.Es_Cantidad
                                            ,entSalDetalle.Es_Costo
                                            ,entSalDetalle.Es_BuenEstado
                                            ,entSalDetalle.Afct_OrdCompra
                                          };
                        sqlcmd = CapaDatos.GenerarSqlCommand("spCapEntSalDet_FactAparatosInproductivos_Insertar", ref verificador, ParametrosDetalleEntSal, ValoresDetalleEntSal);
                        cont++;
                    }
                }
                #endregion

                CapaDatos.CommitTrans();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);


                #region NotaCredito
                int verificado = 0;
                int generarVerificador = 0;
                int Id_Ref = 0;
                double total = 0.00;
                double porciento = 0.00;
                string referencia = string.Empty;
                List<AdendaDet> ListCab = new List<AdendaDet>();
                CapaDatos.CD_Datos CapaDatos1 = new CapaDatos.CD_Datos(Conexion);
                CD_CapNotaCredito cd_nc = new CD_CapNotaCredito();
                SqlDataReader dr1 = null;
                string[] ParametrosCliente = {
                                                "@Id_Emp"
                                                ,"@Id_Cd"
                                                ,"@Id_Cte"
                                             };
                object[] ValoresCliente = {    
                                            factura.Id_Emp
                                            ,factura.Id_Cd
                                            ,factura.Id_Cte
                                          };

                sqlcmd = CapaDatos1.GenerarSqlCommand("spCatCliente_validarNotaCredito", ref dr1, ParametrosCliente, ValoresCliente);

                if (dr1.Read())
                {
                    generarVerificador = Convert.ToInt32(dr1.GetValue(dr1.GetOrdinal("Validador")));
                    porciento = dr1.IsDBNull(dr1.GetOrdinal("Porciento")) ? 0 : Convert.ToDouble(dr1.GetValue(dr1.GetOrdinal("Porciento")));
                    Id_Ref = dr1.IsDBNull(dr1.GetOrdinal("Id_Ref")) ? 0 : Convert.ToInt32(dr1.GetValue(dr1.GetOrdinal("Id_Ref")));
                    referencia = dr1.IsDBNull(dr1.GetOrdinal("Referencia")) ? "" : dr1.GetValue(dr1.GetOrdinal("Referencia")).ToString();
                }

                if (generarVerificador == 1 && porciento > 0)
                {

                    NotaCredito notaCredito = new NotaCredito();
                    //JMM:Obtengo el Id de nota de credito
                    int Id_Ncr = 0;
                    string Estatus = string.Empty;

                    cd_nc.ConsultarIdNotaCredito(factura.Id_Fac, factura.Id_Cd, factura.Id_Emp, ref Id_Ncr, ref Estatus, sesion.Emp_Cnx);
                    if (Estatus != "B")
                    {
                        notaCredito.Id_Emp = factura.Id_Emp;
                        notaCredito.Id_Cd = factura.Id_Cd;
                        notaCredito.Id_Ncr = Id_Ncr;
                        notaCredito.Id_Cfe = Id_Ref;
                        notaCredito.Id_NcrSerie = referencia;
                        notaCredito.Id_Tm = 4; // Descuento aplicado a la factura
                        notaCredito.Id_Cte = factura.Id_Cte;
                        notaCredito.Id_Ter = factura.Id_Ter;
                        notaCredito.Id_Rik = factura.Id_Rik;
                        notaCredito.Id_U = factura.Id_U;
                        notaCredito.Ncr_Tipo = 1; // 1-- tipo factura
                        notaCredito.Ncr_Fecha = factura.Fac_Fecha;
                        notaCredito.Ncr_EmpleadoNumNomina = null;
                        notaCredito.Ncr_EmpleadoNombre = null;
                        notaCredito.Ncr_CtaContable = null;
                        notaCredito.Ncr_Movimiento = 1;
                        notaCredito.Ncr_Referencia = factura.Id_Fac;
                        notaCredito.Ncr_Saldo = factura.Fac_Importe;
                        notaCredito.Ncr_Desgloce = true;
                        notaCredito.Ncr_DesglocePartidas = false;
                        notaCredito.Ncr_Notas = "Descuento aplicado a la factura";
                        notaCredito.Ncr_CteDIVA = false;
                        total = (porciento / 100);
                        notaCredito.Ncr_Subtotal = factura.Fac_SubTotal * total;
                        notaCredito.Ncr_Iva = factura.Fac_ImporteIva * total;

                        notaCredito.Ncr_Total = notaCredito.Ncr_Subtotal + notaCredito.Ncr_Iva;
                        notaCredito.Ncr_Pagado = notaCredito.Ncr_Subtotal + notaCredito.Ncr_Iva;
                        notaCredito.Ncr_FecPagado = DateTime.Now;
                        notaCredito.Ncr_Estatus = "I";
                        notaCredito.Ncr_ReferenciaSerie = referencia;

                        DataTable ListaProductosNotaCredito = new DataTable();
                        ListaProductosNotaCredito.Columns.Add("Id_Emp");
                        ListaProductosNotaCredito.Columns.Add("Id_Cd");
                        ListaProductosNotaCredito.Columns.Add("Id_Ncr");
                        ListaProductosNotaCredito.Columns.Add("Id_NcrDet");
                        ListaProductosNotaCredito.Columns.Add("Id_Ter");
                        ListaProductosNotaCredito.Columns.Add("Ter_Nombre");
                        ListaProductosNotaCredito.Columns.Add("Id_Rik");
                        ListaProductosNotaCredito.Columns.Add("Rik_Nombre");
                        ListaProductosNotaCredito.Columns.Add("Id_Prd");
                        ListaProductosNotaCredito.Columns.Add("Prd_Nombre");
                        ListaProductosNotaCredito.Columns.Add("Ncr_Importe", typeof(System.Double));
                        cont1 = 0;
                        cont1 = 1;
                        foreach (DataRow facturaDet in listaFacturaDet.Rows)
                        {
                            double precio = facturaDet["Fac_Precio"] != null ? Convert.ToDouble(facturaDet["Fac_Precio"]) : 0.00;
                            double cantidad = facturaDet["Fac_Cant"] != null ? Convert.ToDouble(facturaDet["Fac_Cant"]) : 0.00;

                            ListaProductosNotaCredito.Rows.Add(
                                               facturaDet["Id_Emp"]
                                               , facturaDet["Id_Cd"]
                                               , 0
                                               , cont1
                                               , facturaDet["Id_Ter"]
                                               , ""
                                               , factura.Id_Rik
                                               , ""
                                               , facturaDet["Id_Prd"]
                                               , ""
                                               , ((precio * cantidad) * total)
                                        );
                            cont1++;
                        }

                        CD_CapNotaCredito cdCredito = new CD_CapNotaCredito();
                        cdCredito.ModificarNotaCredito(ref notaCredito, sesion.Emp_Cnx, ref verificado, ListCab, ListaProductosNotaCredito);
                        CapaDatos1.LimpiarSqlcommand(ref sqlcmd);
                    }
                }

                #endregion

            }
            catch (Exception ex)
            {
                CapaDatos.RollBackTrans();
                throw ex;
            }
        }


        public void ModificarFacturaEspecial(ref FacturaEspecial facturaEsp, ref List<FacturaDet> listaFacturaProductos, string Conexion, ref int verificador, bool actualizar)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            SqlCommand sqlcmd = null;
            try
            {
                CapaDatos.StartTrans();

                // --------------------------
                // Insertar factura epsecial
                // --------------------------
                string[] ParametrosFacturaEspecial = { 
                                        "@Id_Emp"
                                        ,"@Id_Cd"
                                        ,"@Id_Fac"
                                        ,"@Id_Ter"
                                        ,"@FacEsp_Fecha"
                                        ,"@FacEsp_Importe"
                                        ,"@FacEsp_SubTotal"
                                        ,"@FacEsp_ImporteIva"
                                        ,"@FacEsp_Total"
                                        ,"@actualizar"
                                      };
                object[] ValoresFacturaEspecial = { 
                                        facturaEsp.Id_Emp
                                        ,facturaEsp.Id_Cd
                                        ,facturaEsp.Id_Fac
                                        ,facturaEsp.Id_Ter
                                        ,facturaEsp.FacEsp_Fecha
                                        ,facturaEsp.FacEsp_Importe
                                        ,facturaEsp.FacEsp_SubTotal
                                        ,facturaEsp.FacEsp_ImporteIva
                                        ,facturaEsp.FacEsp_Total
                                        ,actualizar
                                   };
                sqlcmd = CapaDatos.GenerarSqlCommand("spCapFacturaEspecial_Insertar", ref verificador, ParametrosFacturaEspecial, ValoresFacturaEspecial);

                // -----------------------------------------------------------------
                // Parametros de  detalle de factura epsecial y Cliente-Producto
                // -----------------------------------------------------------------
                string[] ParametrosFacturaEspecialDetalle = { 
                                        "@Id_Emp"
                                        ,"@Id_Cd"
                                        ,"@Id_Fac"
                                        ,"@Id_FacDet"
                                        ,"@Id_Prd"
                                        ,"@Id_PrdEsp"
                                        ,"@FacEsp_Descripcion"
                                        ,"@FacEsp_Presentacion"
                                        ,"@FacEsp_Unidades"
                                        ,"@FacEsp_Release"
                                        ,"@Fac_Cant"
                                        ,"@Fac_Precio"
                                        ,"@Prd_ClaveProdServ"
                                        ,"@Prd_ClaveUnidad"

                                      };
                int cont = 0;
                foreach (FacturaDet facturaDet in listaFacturaProductos)
                {
                    // --------------------------------------
                    // Insertar detalle de factura epsecial
                    // --------------------------------------
                    object[] ValoresFacturaEspecialDetalle = { 
                                                        facturaDet.Id_Emp
                                                        ,facturaDet.Id_Cd
                                                        ,facturaEsp.Id_Fac
                                                        ,cont
                                                        ,facturaDet.Id_Prd
                                                        ,facturaDet.Producto.Id_PrdEsp
                                                        ,facturaDet.Producto.Prd_DescripcionEspecial.Split(new string[]{"|"}, StringSplitOptions.RemoveEmptyEntries).Length > 0 ? facturaDet.Producto.Prd_DescripcionEspecial.Split(new string[]{"|"}, StringSplitOptions.RemoveEmptyEntries)[0] : ""
                                                        ,facturaDet.Producto.Prd_Presentacion
                                                        ,facturaDet.Producto.Prd_UniNe
                                                        ,facturaDet.Clp_Release
                                                        ,facturaDet.Fac_Cant //cantidad facturada
                                                        ,facturaDet.Fac_Precio
                                                        ,facturaDet.Fac_ClaveProdServ //cantidad facturada
                                                        ,facturaDet.Fac_ClaveUnidad
                                   };
                    sqlcmd = CapaDatos.GenerarSqlCommand("spCapFacturaEspecialDetalle_Insertar"
                        , ref verificador, ParametrosFacturaEspecialDetalle, ValoresFacturaEspecialDetalle);
                    cont++; //Aumenta contador de partida
                }
                CapaDatos.CommitTrans();
                if (sqlcmd != null)
                {
                    CapaDatos.LimpiarSqlcommand(ref sqlcmd);
                }
            }
            catch (Exception ex)
            {
                CapaDatos.RollBackTrans();
                throw ex;
            }
        }

        public void Factura_DepuracionConsulta(ref Factura factura, string conexion)
        {
            try
            {
                CD_Datos CDDatos = new CD_Datos(conexion);
                SqlDataReader sdr = null;

                string[] parametros = {
                                          "@Id_Emp",
                                          "@Id_Cd",
                                          "@Id_Fac"
                                      };
                object[] valores = { 
                                       factura.Id_Emp,
                                       factura.Id_Cd,
                                       factura.Id_Fac
                                   };

                SqlCommand sqlcmd = default(SqlCommand);

                sqlcmd = CDDatos.GenerarSqlCommand("spCapFacturaDepuracion_Consultar", ref sdr, parametros, valores);

                while (sdr.Read())
                {
                    factura = new Factura();
                    factura.Id_Cte = Convert.ToInt32(sdr.GetValue(sdr.GetOrdinal("Id_Cte")));
                    factura.Cte_NomComercial = sdr.GetValue(sdr.GetOrdinal("Cte_NomComercial")).ToString();
                    factura.Fac_Depuracion = Convert.ToBoolean(sdr.GetValue(sdr.GetOrdinal("Fac_Depuracion")));
                    factura.Fac_DepuracionMotivo = sdr.GetValue(sdr.GetOrdinal("Fac_DepuracionMotivo")).ToString();
                    factura.Fac_DepuracionAutorizo = sdr.GetValue(sdr.GetOrdinal("Fac_DepuracionAutorizo")).ToString();
                    factura.Fac_DepuracionFecha = Convert.ToDateTime(sdr.GetValue(sdr.GetOrdinal("Fac_DepuracionFecha")));


                }
                CDDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void Factura_DepuracionActualiza(Factura factura, string Conexion, ref int verificador)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {
                string[] Parametros = { 
                                        "@Id_Emp" 
                                        ,"@Id_Cd"
	                                    ,"@Id_Fac"
                                        ,"@Fac_Depuracion"
                                        ,"@Fac_DepuracionMotivo"
                                        ,"@Fac_DepuracionAutorizo"
                                      };
                object[] Valores = { 
                                        factura.Id_Emp
                                        ,factura.Id_Cd
                                        ,factura.Id_Fac
                                        ,factura.Fac_Depuracion 
                                        ,factura.Fac_DepuracionMotivo
                                        ,factura.Fac_DepuracionAutorizo 
                                   };


                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd = CapaDatos.GenerarSqlCommand("spCapFacturaDepuracion_Actualiza", ref verificador, Parametros, Valores);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarCantidadProdFactura(Sesion sesion, int prd, int fac, int territorio, ref int cantidadFac)
        {
            try
            {
                CD_Datos CDDatos = new CD_Datos(sesion.Emp_Cnx);
                SqlDataReader sdr = null;

                string[] parametros = {
                                          "@Id_Emp",
                                          "@Id_Cd",
                                          "@Id_Fac",
                                          "@Id_Prd",
                                          "@Id_Ter"
                                      };
                object[] valores = { 
                                       sesion.Id_Emp,
                                       sesion.Id_Cd_Ver,
                                       fac,
                                       prd,
                                       territorio == -1 ? (object)null : territorio
                                   };
                SqlCommand sqlcmd = default(SqlCommand);
                sqlcmd = CDDatos.GenerarSqlCommand("spProFacturaProducto_Cantidad", ref sdr, parametros, valores);
                while (sdr.Read())
                {
                    cantidadFac = Convert.ToInt32(sdr.GetValue(sdr.GetOrdinal("Cantidad")));
                }
                CDDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void InsertarFactura_AparatosInproductivos(ref Factura factura, ref DataTable listaFacturaDet, ref DataTable listaFacturaDetAdenda, int CantidadR, string Conexion, ref int verificador,
            ref EntradaSalida entSal, ref List<EntradaSalidaDetalle> listaEntSalDetalle, ref List<EntradaSalida> listaEntSalRemisiones, List<AdendaDet> listAdendaCabecera, string Fac_Refactura, string arrRemisiones)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {
                CapaDatos.StartTrans();

                object[] Valores = {    
                                        factura.Id_Emp
                                        ,factura.Id_Cd
                                        ,factura.Id_Fac
                                        ,factura.Id_Cfe
                                        ,factura.Id_FacSerie
                                        ,factura.Id_U
                                        ,factura.Id_Tm
                                        ,factura.Fac_PedNum
                                        ,factura.Fac_PedDesc
                                        ,factura.Fac_Req
                                        ,factura.Fac_Fecha
                                        ,factura.Id_Cte
                                        ,factura.Id_Ter
                                        ,factura.Id_Rik
                                        ,factura.Id_Mon
                                        ,factura.Fac_DesgIva
                                        ,factura.Fac_RetIva
                                        ,factura.Fac_CteCalle
                                        ,factura.Fac_CteNumero
                                        ,factura.Fac_CteNumeroInterior
                                        ,factura.Fac_CteCp
                                        ,factura.Fac_CteColonia
                                        ,factura.Fac_CteMunicipio
                                        ,factura.Fac_CteEstado
                                        ,factura.Fac_CteRfc
                                        ,factura.Fac_CteTel
                                        ,factura.Fac_OrdEntrega
                                        ,factura.Fac_CondEntrega
                                        ,factura.Fac_NumEntrega
                                        ,factura.Fac_Notas
                                        ,factura.Fac_DescPorcen1
                                        ,factura.Fac_Desc1
                                        ,factura.Fac_DescPorcen2
                                        ,factura.Fac_Desc2
                                        ,factura.Fac_Tipo
                                        ,factura.Fac_Conducto
                                        ,factura.Fac_NumeroGuia
                                        ,factura.Fac_CanNum
                                        ,factura.Fac_FecCan
                                        ,factura.Fac_Pagado
                                        ,factura.Fac_FecPag
                                        ,factura.Fac_Importe
                                        ,factura.Fac_SubTotal
                                        ,factura.Fac_ImporteIva
                                        ,factura.Fac_ImporteRetencion
                                        ,factura.Fac_Estatus
                                        ,factura.Id_Ped
                                        ,Fac_Refactura
                                        ,factura.Fac_Contacto                         
                                , factura.Fac_FPago
                                ,factura.Fac_UDigitos    
                                ,string.Empty
                                ,factura.Fac_FechaRef // Se agregan 5 campos para refactura , RFH 02032018
                                ,factura.Fac_IdUsuRef
                                ,factura.Fac_IdCausaRef
                                ,factura.Fac_TipoRef
                                ,factura.Fac_EsRefactura               
                                   };
                // --------------------------------
                // Insertar factura
                // --------------------------------
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapFactura_Insertar", ref verificador, ParametrosAparInprod, Valores);
                factura.Id_Fac = verificador; //clave (folio) de factura generada


                //INSERTAR ADENDA CABECERA
                Parametros = new string[] {
                                            "@Id_Emp",			
                                            "@Id_Cd",				
                                            "@Id_Fac",
                                            "@Id_Cte",
                                            "@Id_AdeDet",
                                            "@Ade_Tipo",
                                            "@Ade_Valor",
                                            "@Contador" 
                                      };
                int cont = 0;
                foreach (AdendaDet adendaF in listAdendaCabecera)
                {
                    Valores = new object[] { 
                        factura.Id_Emp,
                        factura.Id_Cd,
                        factura.Id_Fac,
                        factura.Id_Cte,
                        adendaF.Id_AdeDet,
                        adendaF.Tipo,
                        adendaF.Valor,
                        cont
                    };
                    cont++;
                    sqlcmd = CapaDatos.GenerarSqlCommand("spCapFacturaAdenda_Insertar", ref verificador, Parametros, Valores);
                }

                //INSERTAR ADENDA DETALLE
                Parametros = new string[] {
                                            "@Id_Emp",			
                                            "@Id_Cd",				
                                            "@Id_Fac",
                                            "@Id_Cte",
                                            "@Id_AdeDet",
                                            "@Ade_Tipo",
                                            "@Ade_Valor",
                                            "@Contador",
                                            "@Id_Prd",
                                            "@Id_Ter"
                                      };

                foreach (DataRow facturaDet in listaFacturaDetAdenda.Rows)
                {
                    for (int i = 3; i < listaFacturaDetAdenda.Columns.Count; i++)
                    {
                        Valores = new object[] { 
                            factura.Id_Emp,
                            factura.Id_Cd,
                            factura.Id_Fac,
                            factura.Id_Cte,
                            listaFacturaDetAdenda.Columns[i].ColumnName,
                            (i + CantidadR) >= listaFacturaDetAdenda.Columns.Count ? 8 : 2,
                            facturaDet[i],
                            i,
                            facturaDet["Id_Prd"],
                            //facturaDet["Id_Ter"]
                        };
                        sqlcmd = CapaDatos.GenerarSqlCommand("spCapFacturaAdenda_Insertar", ref verificador, Parametros, Valores);
                    }
                }

                //foreach (DataRow facturaDet in listaFacturaDet.Rows)
                //{
                //    for (int i = 22; i < listaFacturaDet.Columns.Count; i++)
                //    {
                //        Valores = new object[] { 
                //            factura.Id_Emp,
                //            factura.Id_Cd,
                //            factura.Id_Fac,
                //            factura.Id_Cte,
                //            listaFacturaDet.Columns[i].ColumnName,
                //             (i + CantidadR) >= listaFacturaDet.Columns.Count ? 8 : 2,
                //            facturaDet[i],
                //            i,
                //            facturaDet["Id_Prd"],
                //            facturaDet["Id_Ter"]
                //        };
                //        sqlcmd = CapaDatos.GenerarSqlCommand("spCapFacturaAdenda_Insertar", ref verificador, Parametros, Valores);
                //    }
                //}

                // ----------------------------------------
                // Insertar mov. entrada-salida mov. 16
                // ----------------------------------------
                entSal.Es_Referencia = factura.Id_Fac.ToString(); //referencia a la factura
                entSal.Es_Notas = entSal.Es_Notas.Replace("#Id_Fac#", factura.Id_Fac.ToString());
                string[] ParametrosEntSal = {
                                            "@Id_Emp"
                                            ,"@Id_Cd"
                                            ,"@Id_U"
                                            ,"@Es_Naturaleza"
                                            ,"@Es_Fecha"
                                            ,"@Id_Tm"
                                            ,"@Id_Cte"
                                            ,"@Id_Pvd"
                                            ,"@Es_Referencia"
                                            ,"@Es_Notas"
                                            ,"@Es_Subtotal"
                                            ,"@Es_Iva"
                                            ,"@Es_Total"
                                            ,"@Es_Estatus"
                                            ,"@ManAut"
                                      };
                object[] ValoresEntSal = {    
                                            entSal.Id_Emp
                                            ,entSal.Id_Cd
                                            ,entSal.Id_U
                                            ,entSal.Es_Naturaleza
                                            ,entSal.Es_Fecha
                                            ,entSal.Id_Tm
                                            ,entSal.Id_Cte
                                            ,entSal.Id_Pvd == -1 ? (object)null : entSal.Id_Pvd
                                            ,entSal.Es_Referencia
                                            ,entSal.Es_Notas
                                            ,entSal.Es_SubTotal
                                            ,entSal.Es_Iva
                                            ,entSal.Es_Total
                                            ,entSal.Es_Estatus
                                            ,0
                                          };
                sqlcmd = CapaDatos.GenerarSqlCommand("spCapEntSal_Insertar", ref verificador, ParametrosEntSal, ValoresEntSal);
                entSal.Id_Es = verificador; //clave (folio) de entrada-salida generado


                // ----------------------------------------
                // Insertar detalle de factura
                // ----------------------------------------
                string[] ParametrosDetalle = {
                                            "@Id_Emp"			
                                            ,"@Id_Cd"				
                                            ,"@Id_Fac"
                                            ,"@Id_FacDet"		
                                            ,"@Id_Ter"			
                                            ,"@Id_Prd"			
                                            ,"@Id_Cte"			
                                            ,"@Id_Ped"
                                            ,"@Id_Rem"
                                            ,"@Fac_Cant"		
                                            ,"@Fac_Precio"		
                                            ,"@Fac_Asignar"
                                            ,"@Fac_Amortizacion"
                                            ,"@Id_Es"                                            
                                      };
                cont = 0;
                foreach (DataRow facturaDet in listaFacturaDet.Rows)
                {
                    object[] ValoresDetalle = {    
                                            facturaDet["Id_Emp"]				
                                            ,facturaDet["Id_Cd"]				
                                            ,factura.Id_Fac		
                                            ,cont
                                            ,facturaDet["Id_Ter"]			
                                            ,facturaDet["Id_Prd"]			
                                            ,facturaDet["Id_CteExt"]
                                            ,(object)null //cuando es facturacion de aparatos inproductovos, no aplica la fact. de pedido previo			
                                            ,facturaDet["Id_Rem"] //si es facturacion de remision, se actualiza la cantidad remisionada del producto
                                            ,facturaDet["Fac_Cant"]		
                                            ,facturaDet["Fac_Precio"]		
                                            ,0
                                            ,facturaDet["AmortizacionProducto"]
                                            ,entSal.Id_Es
                                            //,listaEntSalRemisiones.Count > 0 ? (object)null : 1 // Mov. para que no reduzca el inv. final, si no tiene remisiones
                                          };
                    sqlcmd = CapaDatos.GenerarSqlCommand("spCapFacturaDetalle_Insertar", ref verificador, ParametrosDetalle, ValoresDetalle);
                    cont++;
                }

                // ----------------------------------------
                // Insertar detalle de Entrada-Salida
                // ----------------------------------------
                string[] ParametrosDetalleEntSal = {
                                        "@Id_Emp",
                                        "@Id_Cd",
                                        "@Id_Es",
                                        "@Id_EsDet",
                                        "@EsDet_Naturaleza",
                                        "@Id_Ter",
                                        "@Id_Prd",
                                        "@Es_Cantidad",
                                        "@Es_Costo",
                                        "@Es_BuenEstado",
                                        "@EsDet_AfcOrdCom"
                                      };
                cont = 0;
                foreach (EntradaSalidaDetalle entSalDetalle in listaEntSalDetalle)
                {
                    object[] ValoresDetalleEntSal = {    
                                            entSalDetalle.Id_Emp
                                            ,entSalDetalle.Id_Cd
                                            ,entSal.Id_Es
                                            ,cont
                                            ,entSalDetalle.EsDet_Naturaleza
                                            ,entSalDetalle.Id_Ter
                                            ,entSalDetalle.Id_Prd
                                            ,entSalDetalle.Es_Cantidad
                                            ,entSalDetalle.Es_Costo
                                            ,entSalDetalle.Es_BuenEstado
                                            ,entSalDetalle.Afct_OrdCompra
                                          };
                    sqlcmd = CapaDatos.GenerarSqlCommand("spCapEntSalDet_FactAparatosInproductivos_Insertar", ref verificador, ParametrosDetalleEntSal, ValoresDetalleEntSal);
                    cont++;
                }


                // ---------------------------------------------------------------------------------------------------
                // Insertar momivimentos de Entrada inversos de remisiones, si es que s una facturacion de remisiones
                // ---------------------------------------------------------------------------------------------------
                foreach (EntradaSalida entSalRem in listaEntSalRemisiones)
                {
                    int id_remision = Convert.ToInt32(entSalRem.Es_Referencia);//referencia a remision
                    entSalRem.Es_Referencia = factura.Id_Fac.ToString(); //referencia a remision se sustituye por referencia a la factura
                    string[] ParametrosEntSalRem = {
                                            "@Id_Emp"
                                            ,"@Id_Cd"
                                            ,"@Id_U"
                                            ,"@Es_Naturaleza"
                                            ,"@Es_Fecha"
                                            ,"@Id_Tm"
                                            ,"@Id_Cte"
                                            ,"@Id_Pvd"
                                            ,"@Es_Referencia"
                                            ,"@Es_ReferenciaRem"
                                            ,"@Es_Notas"
                                            ,"@Es_Subtotal"
                                            ,"@Es_Iva"
                                            ,"@Es_Total"
                                            ,"@Es_Estatus"
                                            ,"@ManAut"
                                            ,"@Id_Rem"
                                            ,"@Id_Ter"
                                            ,"@Id_Fac"
                                      };
                    object[] ValoresEntSalRem = {    
                                            entSalRem.Id_Emp
                                            ,entSalRem.Id_Cd
                                            ,entSalRem.Id_U
                                            ,entSalRem.Es_Naturaleza
                                            ,entSalRem.Es_Fecha
                                            ,entSalRem.Id_Tm
                                            ,entSalRem.Id_Cte
                                            ,entSalRem.Id_Pvd == -1 ? (object)null : entSalRem.Id_Pvd
                                            ,entSalRem.Es_Referencia //referencia factura
                                            ,id_remision //referencia remision
                                            ,entSalRem.Es_Notas
                                            ,entSalRem.Es_SubTotal
                                            ,entSalRem.Es_Iva
                                            ,entSalRem.Es_Total
                                            ,entSalRem.Es_Estatus
                                            ,0
                                            ,entSalRem.Id_Rem
                                            ,entSalRem.Id_Ter
                                            ,factura.Id_Fac
                                          };
                    sqlcmd = CapaDatos.GenerarSqlCommand("spCapEntSal_FacturacionRemision_Insertar", ref verificador, ParametrosEntSalRem, ValoresEntSalRem);
                    entSalRem.Id_Es = verificador; //clave (folio) de entrada-salida generado

                    // ---------------------------
                    // Insertar detalle
                    // ---------------------------
                    string[] ParametrosDetalleEntSalRem = {
                                        "@Id_Emp",
                                        "@Id_Cd",
                                        "@Id_Es",                                      
                                        "@Id_Rem",
                                        "@Id_Fac"
                                      };
                    cont = 0;
                    foreach (EntradaSalidaDetalle entSalRemDetalle in entSalRem.ListaDetalle)
                    {
                        // ----------------------------------------
                        // Insertar detalle de Entrada-Salida
                        // ----------------------------------------
                        object[] ValoresDetalleEntSalRem = {    
                                            entSalRemDetalle.Id_Emp
                                            ,entSalRemDetalle.Id_Cd
                                            ,entSalRem.Id_Es
                                            //,cont
                                            //,entSalRemDetalle.EsDet_Naturaleza
                                            //,entSalRemDetalle.Id_Ter
                                            //,entSalRemDetalle.Id_Prd
                                            //,entSalRemDetalle.Es_Cantidad
                                            //,entSalRemDetalle.Es_Costo
                                            //,entSalRemDetalle.Es_BuenEstado
                                            //,entSalRemDetalle.Afct_OrdCompra
                                            ,arrRemisiones//entSalRemDetalle.Id_Rem
                                            ,factura.Id_Fac
                                            //,entSalRemDetalle.Es_CantidadRem
                                          };
                        sqlcmd = CapaDatos.GenerarSqlCommand("spCapEntSalDet_FactRemision_Insertar", ref verificador, ParametrosDetalleEntSalRem, ValoresDetalleEntSalRem);
                        cont++;
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

        public void ModificarFactura_AparatosInproductivos(ref Factura factura, ref DataTable listaFacturaDet, ref DataTable listaFacturaDetAdenda, int CantidadR, string Conexion, ref int verificador,
            ref EntradaSalida entSal, ref List<EntradaSalidaDetalle> listaEntSalDetalle, ref List<EntradaSalida> listaEntSalRemisiones, List<AdendaDet> listAdendaCabecera, string IdRF, string arrayRemisiones)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {
                CapaDatos.StartTrans();

                #region FACTURA
                object[] Valores = {    
                                        factura.Id_Emp
                                        ,factura.Id_Cd
                                        ,factura.Id_Fac
                                        ,factura.Id_Cfe
                                        ,factura.Id_FacSerie
                                        ,factura.Id_U
                                        ,factura.Id_Tm
                                        ,factura.Fac_PedNum
                                        ,factura.Fac_PedDesc
                                        ,factura.Fac_Req
                                        ,factura.Fac_Fecha
                                        ,factura.Id_Cte
                                        ,factura.Id_Ter
                                        ,factura.Id_Rik
                                        ,factura.Id_Mon
                                        ,factura.Fac_DesgIva
                                        ,factura.Fac_RetIva
                                        ,factura.Fac_CteCalle
                                        ,factura.Fac_CteNumero
                                        ,factura.Fac_CteNumeroInterior
                                        ,factura.Fac_CteCp
                                        ,factura.Fac_CteColonia
                                        ,factura.Fac_CteMunicipio
                                        ,factura.Fac_CteEstado
                                        ,factura.Fac_CteRfc
                                        ,factura.Fac_CteTel
                                        ,factura.Fac_OrdEntrega
                                        ,factura.Fac_CondEntrega
                                        ,factura.Fac_NumEntrega
                                        ,factura.Fac_Notas
                                        ,factura.Fac_DescPorcen1
                                        ,factura.Fac_Desc1
                                        ,factura.Fac_DescPorcen2
                                        ,factura.Fac_Desc2
                                        ,factura.Fac_Tipo
                                        ,factura.Fac_Conducto
                                        ,factura.Fac_NumeroGuia
                                        ,factura.Fac_CanNum
                                        ,factura.Fac_FecCan
                                        ,factura.Fac_Pagado
                                        ,factura.Fac_FecPag
                                        ,factura.Fac_Importe
                                        ,factura.Fac_SubTotal
                                        ,factura.Fac_ImporteIva
                                        ,factura.Fac_ImporteRetencion
                                        ,factura.Fac_Estatus
                                        ,factura.Id_Ped
                                        ,IdRF
                                        ,factura.Fac_Contacto                                        
                                   };
                // --------------------------------
                // Insertar factura
                // --------------------------------
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapFactura_Modificar", ref verificador, ParametrosAparInprod, Valores);
                #endregion

                #region INSERTAR ADENDA CABECERA
                Parametros = new string[] {
                                            "@Id_Emp",			
                                            "@Id_Cd",				
                                            "@Id_Fac",
                                            "@Id_Cte",
                                            "@Id_AdeDet",
                                            "@Ade_Tipo",
                                            "@Ade_Valor",
                                            "@Contador" 
                                      };
                int cont = 0;
                foreach (AdendaDet adendaF in listAdendaCabecera)
                {
                    Valores = new object[] { 
                        factura.Id_Emp,
                        factura.Id_Cd,
                        factura.Id_Fac,
                        factura.Id_Cte,
                        adendaF.Id_AdeDet,
                        adendaF.Tipo,
                        adendaF.Valor,
                        cont
                    };
                    cont++;
                    sqlcmd = CapaDatos.GenerarSqlCommand("spCapFacturaAdenda_Insertar", ref verificador, Parametros, Valores);
                }
                #endregion

                #region INSERTAR ADENDA DETALLE
                Parametros = new string[] {
                                            "@Id_Emp",			
                                            "@Id_Cd",				
                                            "@Id_Fac",
                                            "@Id_Cte",
                                            "@Id_AdeDet",
                                            "@Ade_Tipo",
                                            "@Ade_Valor",
                                            "@Contador",
                                            "@Id_Prd",
                                            //"@Id_Ter"
                                      };

                foreach (DataRow facturaDet in listaFacturaDetAdenda.Rows)
                {
                    for (int i = 3; i < listaFacturaDetAdenda.Columns.Count; i++)
                    {
                        Valores = new object[] { 
                            factura.Id_Emp,
                            factura.Id_Cd,
                            factura.Id_Fac,
                            factura.Id_Cte,
                            listaFacturaDetAdenda.Columns[i].ColumnName,
                            (i + CantidadR) >= listaFacturaDetAdenda.Columns.Count ? 8 : 2,
                            facturaDet[i],
                            i,
                            facturaDet["Id_Prd"],
                            //facturaDet["Id_Ter"]
                        };
                        sqlcmd = CapaDatos.GenerarSqlCommand("spCapFacturaAdenda_Insertar", ref verificador, Parametros, Valores);
                    }
                }

                //foreach (DataRow facturaDet in listaFacturaDet.Rows)
                //{
                //    for (int i = 22; i < listaFacturaDet.Columns.Count; i++)
                //    {
                //        Valores = new object[] { 
                //            factura.Id_Emp,
                //            factura.Id_Cd,
                //            factura.Id_Fac,
                //            factura.Id_Cte,
                //            listaFacturaDet.Columns[i].ColumnName,
                //            (i + CantidadR) >= listaFacturaDet.Columns.Count ? 8 : 2,
                //            facturaDet[i],
                //            i,
                //            facturaDet["Id_Prd"],
                //            //facturaDet["Id_Ter"]
                //        };
                //        sqlcmd = CapaDatos.GenerarSqlCommand("spCapFacturaAdenda_Insertar", ref verificador, Parametros, Valores);
                //    }
                //}
                #endregion
                // ----------------------------------------
                // Actualiza mov. entrada-salida mov. 16
                // ----------------------------------------
                #region ENT/SAL
                string[] ParametrosEntSal = {
                                            "@Id_Emp"
                                            ,"@Id_Cd"
                                            ,"@Id_Es"
                                            ,"@Id_U"
                                            ,"@Es_Naturaleza"
                                            ,"@Es_Fecha"
                                            ,"@Id_Tm"
                                            ,"@Id_Cte"
                                            ,"@Id_Pvd"
                                            ,"@Es_Referencia"
                                            ,"@Es_Notas"
                                            ,"@Es_Subtotal"
                                            ,"@Es_Iva"
                                            ,"@Es_Total"
                                            ,"@Es_Estatus"
                                            ,"@ManAut"
                                      };
                object[] ValoresEntSal = {    
                                            entSal.Id_Emp
                                            ,entSal.Id_Cd
                                            ,entSal.Id_Es
                                            ,entSal.Id_U
                                            ,entSal.Es_Naturaleza
                                            ,entSal.Es_Fecha
                                            ,entSal.Id_Tm
                                            ,entSal.Id_Cte
                                            ,entSal.Id_Pvd == -1 ? (object)null : entSal.Id_Pvd
                                            ,entSal.Es_Referencia
                                            ,entSal.Es_Notas
                                            ,entSal.Es_SubTotal
                                            ,entSal.Es_Iva
                                            ,entSal.Es_Total
                                            ,entSal.Es_Estatus
                                            ,0
                                          };
                sqlcmd = CapaDatos.GenerarSqlCommand("spCapEntSal_FactAparatosInproductivos_Modificar", ref verificador, ParametrosEntSal, ValoresEntSal);

                #endregion
                // ----------------------------------------
                // Insertar detalle de factura
                // ----------------------------------------
                #region DETALLE FACTURA
                string[] ParametrosDetalle = {
                                            "@Id_Emp"			
                                            ,"@Id_Cd"				
                                            ,"@Id_Fac"
                                            ,"@Id_FacDet"		
                                            ,"@Id_Ter"			
                                            ,"@Id_Prd"			
                                            ,"@Id_Cte"			
                                            ,"@Id_Ped"
                                            ,"@Id_Rem"
                                            ,"@Fac_Cant"		
                                            ,"@Fac_Precio"		
                                            ,"@Fac_Asignar"
                                            ,"@Fac_Amortizacion"
                                            ,"@Id_Es"                                            
                                      };
                cont = 0;
                foreach (DataRow facturaDet in listaFacturaDet.Rows)
                {
                    object[] ValoresDetalle = {    
                                            facturaDet["Id_Emp"]				
                                            ,facturaDet["Id_Cd"]				
                                            ,factura.Id_Fac
                                            ,cont
                                            ,facturaDet["Id_Ter"]			
                                            ,facturaDet["Id_Prd"]			
                                            ,facturaDet["Id_CteExt"]	
		                                    ,(object)null //cuando es facturacion de aparatos inproductovos, no aplica la fact. de pedido previo			
                                            ,arrayRemisiones//facturaDet["Id_Rem"] //si es facturacion de remision, se actualiza la cantidad remisionada del producto
                                            ,facturaDet["Fac_Cant"]		
                                            ,facturaDet["Fac_Precio"]	
                                            ,0
                                            ,facturaDet["AmortizacionProducto"]                                          
                                            ,entSal.Id_Es                                           
                                          };
                    sqlcmd = CapaDatos.GenerarSqlCommand("spCapFacturaDetalle_Insertar", ref verificador, ParametrosDetalle, ValoresDetalle);
                    cont++;
                }
                #endregion
                // ----------------------------------------
                // Insertar detalle de Entrada-Salida
                // ----------------------------------------
                #region DETALLE ENT/SAL
                string[] ParametrosDetalleEntSal = {
                                        "@Id_Emp",
                                        "@Id_Cd",
                                        "@Id_Es",
                                        "@Id_EsDet",
                                        "@EsDet_Naturaleza",
                                        "@Id_Ter",
                                        "@Id_Prd",
                                        "@Es_Cantidad",
                                        "@Es_Costo",
                                        "@Es_BuenEstado",
                                        "@EsDet_AfcOrdCom"
                                      };
                cont = 0;
                foreach (EntradaSalidaDetalle entSalDetalle in listaEntSalDetalle)
                {
                    object[] ValoresDetalleEntSal = {    
                                            entSalDetalle.Id_Emp
                                            ,entSalDetalle.Id_Cd
                                            ,entSal.Id_Es
                                            ,cont
                                            ,entSalDetalle.EsDet_Naturaleza
                                            ,entSalDetalle.Id_Ter
                                            ,entSalDetalle.Id_Prd
                                            ,entSalDetalle.Es_Cantidad
                                            ,entSalDetalle.Es_Costo
                                            ,entSalDetalle.Es_BuenEstado
                                            ,entSalDetalle.Afct_OrdCompra
                                          };
                    sqlcmd = CapaDatos.GenerarSqlCommand("spCapEntSalDet_FactAparatosInproductivos_Insertar", ref verificador, ParametrosDetalleEntSal, ValoresDetalleEntSal);
                    cont++;
                }

                #endregion
                // ---------------------------------------------------------------------------------------------------
                // Insertar momivimentos de Entrada inversos de remisiones, si es que s una facturacion de remisiones
                // ---------------------------------------------------------------------------------------------------
                #region ENTSAL/REMISIONES
                foreach (EntradaSalida entSalRem in listaEntSalRemisiones)
                {
                    int id_remision = Convert.ToInt32(entSalRem.Es_Referencia);//referencia a remision
                    entSalRem.Es_Referencia = factura.Id_Fac.ToString(); //referencia a remision se sustituye por referencia a la factura
                    string[] ParametrosEntSalRem = {
                                            "@Id_Emp"
                                            ,"@Id_Cd"
                                            ,"@Id_U"
                                            ,"@Es_Naturaleza"
                                            ,"@Es_Fecha"
                                            ,"@Id_Tm"
                                            ,"@Id_Cte"
                                            ,"@Id_Pvd"
                                            ,"@Es_Referencia"
                                            ,"@Es_ReferenciaRem"
                                            ,"@Es_Notas"
                                            ,"@Es_Subtotal"
                                            ,"@Es_Iva"
                                            ,"@Es_Total"
                                            ,"@Es_Estatus"
                                            ,"@ManAut"
                                            ,"@Id_Rem"
                                            ,"@Id_Ter"
                                            ,"@Id_Fac"
                                      };
                    object[] ValoresEntSalRem = {    
                                            entSalRem.Id_Emp
                                            ,entSalRem.Id_Cd
                                            ,entSalRem.Id_U
                                            ,entSalRem.Es_Naturaleza
                                            ,entSalRem.Es_Fecha
                                            ,entSalRem.Id_Tm
                                            ,entSalRem.Id_Cte
                                            ,entSalRem.Id_Pvd == -1 ? (object)null : entSalRem.Id_Pvd
                                            ,entSalRem.Es_Referencia //referencia factura
                                            ,id_remision //referencia remision
                                            ,entSalRem.Es_Notas
                                            ,entSalRem.Es_SubTotal
                                            ,entSalRem.Es_Iva
                                            ,entSalRem.Es_Total
                                            ,entSalRem.Es_Estatus
                                            ,0
                                            ,entSalRem.Id_Rem
                                            ,entSalRem.Id_Ter
                                            ,factura.Id_Fac
                                          };
                    sqlcmd = CapaDatos.GenerarSqlCommand("spCapEntSal_FacturacionRemision_Insertar", ref verificador, ParametrosEntSalRem, ValoresEntSalRem);
                    entSalRem.Id_Es = verificador; //clave (folio) de entrada-salida generado

                    // ---------------------------
                    // Insertar detalle
                    // ---------------------------
                    string[] ParametrosDetalleEntSalRem = {
                                        "@Id_Emp",
                                        "@Id_Cd",
                                        "@Id_Es",
                                        "@Id_EsDet",
                                        "@EsDet_Naturaleza",
                                        "@Id_Ter",
                                        "@Id_Prd",
                                        "@Es_Cantidad",
                                        "@Es_Costo",
                                        "@Es_BuenEstado",
                                        "@EsDet_AfcOrdCom",
                                        "@Id_Rem",
                                        "@Rem_Cant"
                                      };
                    cont = 0;
                    foreach (EntradaSalidaDetalle entSalRemDetalle in entSalRem.ListaDetalle)
                    {
                        // ----------------------------------------
                        // Insertar detalle de Entrada-Salida
                        // ----------------------------------------
                        object[] ValoresDetalleEntSalRem = {    
                                            entSalRemDetalle.Id_Emp
                                            ,entSalRemDetalle.Id_Cd
                                            ,entSalRem.Id_Es
                                            ,cont
                                            ,entSalRemDetalle.EsDet_Naturaleza
                                            ,entSalRemDetalle.Id_Ter
                                            ,entSalRemDetalle.Id_Prd
                                            ,entSalRemDetalle.Es_Cantidad
                                            ,entSalRemDetalle.Es_Costo
                                            ,entSalRemDetalle.Es_BuenEstado
                                            ,entSalRemDetalle.Afct_OrdCompra
                                            ,entSalRemDetalle.Id_Rem
                                            ,entSalRemDetalle.Es_CantidadRem
                                          };
                        sqlcmd = CapaDatos.GenerarSqlCommand("spCapEntSalDet_FactRemision_Insertar", ref verificador, ParametrosDetalleEntSalRem, ValoresDetalleEntSalRem);
                        cont++;
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

        public void ModificarFactura_Estatus(Factura factura, string Conexion, ref int verificador)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {
                string[] Parametros = { 
                                        "@Id_Emp" 
                                        ,"@Id_Cd"
	                                    ,"@Id_Fac"
                                        ,"@Fac_Estatus"
                                      };
                object[] Valores = { 
                                        factura.Id_Emp
                                        ,factura.Id_Cd
                                        ,factura.Id_Fac
                                        ,factura.Fac_Estatus
                                   };

                // -----------------------------------------------------------------
                // Actualizar encabezado de la orden de compra
                // -----------------------------------------------------------------
                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd = CapaDatos.GenerarSqlCommand("spCapFactura_ModificarEstatus", ref verificador, Parametros, Valores);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarFacturaSAT(Factura factura, string Conexion, ref int verificador)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {
                string[] Parametros = { 
                                        "@Id_Emp" 
                                        ,"@Id_Cd"
	                                    ,"@Id_Fac"
                                        ,"@Fac_Estatus"
                                        ,"@Fac_Sello"
                                        ,"@Fac_Xml"
                                        ,"@Fac_Pdf"
                                        ,"@Fac_FolioFiscal"
                                        ,"@Fac_SelloCN"
                                        ,"@Fac_PdfCN"
                                        ,"@Fac_FolioFiscalCN"
                                        ,"@Fac_XmlCN"
                                        ,"@Fac_FolioCN"
                                      };
                object[] Valores = { 
                                        factura.Id_Emp
                                        ,factura.Id_Cd
                                        ,factura.Id_Fac
                                        ,factura.Fac_Estatus
                                        ,factura.Fac_Sello
                                        ,factura.Fac_Xml
                                        ,factura.Fac_Pdf
                                        ,factura.Fac_FolioFiscal
                                        ,factura.Fac_SelloCN
                                        ,factura.Fac_PdfCN
                                        ,factura.Fac_FolioFiscalCN
                                        ,factura.Fac_XmlCN
                                        ,factura.Fac_FolioCN
                                   };

                // -----------------------------------------------------------------
                // Actualizar encabezado de la orden de compra
                // -----------------------------------------------------------------
                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd = CapaDatos.GenerarSqlCommand("spCapFactura_ModificarSAT", ref verificador, Parametros, Valores);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarFacturaSAT(ref Factura factura, string Conexion, ref object resultado)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {
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

                // ------------------------------------
                // Consultar PDF de la factura
                // ------------------------------------
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapFactura_ConsultaSAT", ref resultado, Parametros, Valores);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarFacturaSAT(ref Factura factura, string Conexion, ref object resultado, ref object resultadoCN)
        {
            SqlDataReader dr = null;
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {
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

                // ------------------------------------
                // Consultar PDF de la factura
                // ------------------------------------
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapFactura_ConsultaSAT", ref dr, Parametros, Valores);

                if (dr.Read())
                {
                    resultado = dr["Fac_Pdf"];
                    resultadoCN = dr["Fac_PdfCN"];
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Eliminar Factura, baja lógica
        /// </summary>
        public void EliminarFactura(ref Factura factura, string Conexion, ref int verificador, ref EntradaSalida entSal, ref List<EntradaSalidaDetalle> listaEntSalDetalle)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {
                CapaDatos.StartTrans();
                string[] ParametrosEliminar = {
                                        "@Id_Emp"
                                        ,"@Id_Cd"
                                        ,"@Id_Fac"
                                        ,"@Id_U_Cancelo"
                                      };
                object[] Valores = {    
                                        factura.Id_Emp
                                        ,factura.Id_Cd
                                        ,factura.Id_Fac
                                        ,factura.Id_U
                                   };
                // --------------------------------
                // Eliminar factura
                // --------------------------------
                SqlCommand sqlcmd;
                sqlcmd = CapaDatos.GenerarSqlCommand("spCapFactura_Eliminar", ref verificador, ParametrosEliminar, Valores);
                CapaDatos.CommitTrans();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                CapaDatos.RollBackTrans();
                throw ex;
            }
        }

        public void LogError_Insertar(string clave, string error, string Conexion)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {
                string[] Parametros = { 
                                        "@clave" 
                                        ,"@error"
	                                    ,"@fecha"
                                      };
                object[] Valores = { 
                                        clave
                                        ,error
                                        ,new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second)
                                   };

                // -----------------------------------------------------------------
                // Actualizar encabezado de la orden de compra
                // -----------------------------------------------------------------
                int verificador = 0;
                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd = CapaDatos.GenerarSqlCommand("spLogError_Insertar", ref verificador, Parametros, Valores);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Rastreo(ref Factura fac, string Conexion, int tipoBusqueda)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_FacSerie", "@Fac_FolioFiscal", "@tipoBusqueda" };
                object[] Valores = { fac.Id_Emp, fac.Id_Cd, fac.Id_FacSerie, fac.Fac_FolioFiscal, tipoBusqueda };

                // ------------------------------------
                // Consultar encabezado de la factura
                // ------------------------------------
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spProRastreo_Factura", ref dr, Parametros, Valores);
                if (dr.Read())
                {
                    fac.Id_Cte = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cte")));
                    fac.Cte_NomComercial = dr.GetValue(dr.GetOrdinal("Cte_NomComercial")).ToString();
                    fac.Fac_ImporteIva = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Fac_ImporteIva")));
                    fac.Fac_SubTotal = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Fac_SubTotal")));
                    fac.Fac_Pagado = (double?)dr.GetValue(dr.GetOrdinal("Fac_Pagado"));
                    fac.Fac_Estatus = dr.GetValue(dr.GetOrdinal("Fac_Estatus")).ToString();
                    fac.Fac_EstatusStr = Estatus(dr.GetValue(dr.GetOrdinal("Fac_Estatus")).ToString());
                    fac.Fac_Fecha = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Fac_Fecha")));
                    fac.Fac_FolioFiscal = dr.GetValue(dr.GetOrdinal("Fac_FolioFiscal")).ToString();
                    fac.Id_Fac = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Fac")));
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarAdenda(int Id_Emp, int Id_Cd_Ver, int Id_Fac, string Tipo1, string Tipo2, ref List<AdendaDet> listCab, ref List<AdendaDet> listDet, string Conexion)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = {   
                                          "@Id_Emp",
                                          "@Id_Cd",
                                          "@Id_Fac",
                                          "@Ade_Tipo1",
                                          "@Ade_Tipo2"
                                      };
                object[] Valores = { 
                                       Id_Emp,
                                       Id_Cd_Ver,
                                       Id_Fac,
                                       Tipo1,
                                       Tipo2
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapFacturaAdenda_Consultar", ref dr, Parametros, Valores);
                int tipo = 0;
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
                    // adendaDet.Id_AdeCons = (int?)dr.GetValue(dr.GetOrdinal("Id_AdeCons"));
                    adendaDet.Requerido = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Ade_Requerido")));
                    tipo = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Ade_Tipo")));
                    if (tipo % 2 != 0)
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

        public void ConsultarAdendaNacional(int Id_Emp, int Id_Cd_Ver, int Id_Fac, string Tipo1, string Tipo2, ref List<AdendaDet> listCab, ref List<AdendaDet> listDet, string Conexion)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = {   
                                          "@Id_Emp",
                                          "@Id_Cd",
                                          "@Id_Fac",
                                          "@Ade_Tipo1",
                                          "@Ade_Tipo2"
                                      };
                object[] Valores = { 
                                       Id_Emp,
                                       Id_Cd_Ver,
                                       Id_Fac,
                                       Tipo1,
                                       Tipo2
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapFacturaAdendaNacional_Consultar", ref dr, Parametros, Valores);
                int tipo = 0;
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
                    // adendaDet.Id_AdeCons = (int?)dr.GetValue(dr.GetOrdinal("Id_AdeCons"));
                    adendaDet.Requerido = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Ade_Requerido")));
                    tipo = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Ade_Tipo")));
                    if (tipo % 2 != 0)
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

        public void DisponibleFacturar(Sesion sesion, Factura fac2, int prd, ref int disponibleFacturar)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(sesion.Emp_Cnx);

                string[] Parametros = {   
                                          "@Id_Emp" ,
                                          "@Id_Cd",
                                          "@Id_Fac",
                                          "@Id_Rem",
                                          "@Id_Prd"
                                      };
                object[] Valores = { 
                                       sesion.Id_Emp,
                                       sesion.Id_Cd_Ver,
                                       fac2.Id_Fac,
                                       fac2.Id_Rem,
                                       prd
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapFacRem_Cantidad", ref disponibleFacturar, Parametros, Valores);

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void FacturaRemision_Nota(Factura factura_remision, string Conexion, ref string agregado_nota)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = {   
                                          "@Id_Emp",
                                          "@Id_Cd",
                                          "@Id_Fac"
                                      };
                object[] Valores = { 
                                       factura_remision.Id_Emp,
                                       factura_remision.Id_Cd,
                                       factura_remision.Id_Fac
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spFacturaRemision_Nota", ref dr, Parametros, Valores);
                if (dr.HasRows)
                {
                    dr.Read();
                    agregado_nota = dr.GetValue(dr.GetOrdinal("NotaRemisiones")).ToString();
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void ArchivoPdf_Xml(ref Factura fac, string Conexion)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_Fac" };
                object[] Valores = { fac.Id_Emp, fac.Id_Cd, fac.Id_Fac };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapFactura_PDF_XML", ref dr, Parametros, Valores);
                //byte[] filebytes = null;
                while (dr.Read())
                {
                    fac = new Factura();
                    fac.Fac_Xml = Convert.ToString(dr.GetValue(dr.GetOrdinal("Fac_Xml")));
                    fac.Fac_XmlCN = Convert.ToString(dr.GetValue(dr.GetOrdinal("Fac_XmlCN")));
                    //fac.Fac_Pdf =  Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Fac_Pdf"))) ? filebytes : Convert.FromBase64String(dr.GetValue(dr.GetOrdinal("Fac_Pdf")).ToString());                                                          
                }
                dr.Close();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private byte[] Base64ToByte(string data)
        {
            byte[] filebytes = null;
            try
            {
                if (!string.IsNullOrEmpty(data))
                {
                    filebytes = Convert.FromBase64String(data);
                }
                return filebytes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void FacturaVI_ValidadorRequisicion(Sesion session, Factura fac, ref bool requisicion)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(session.Emp_Cnx);

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_Cte", "@ID_Ter" };
                object[] Valores = { session.Id_Emp, session.Id_Cd, fac.Id_Cte, fac.Id_Ter };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spFacturaVentaInatalada_ValidarRequisicion", ref dr, Parametros, Valores);
                while (dr.Read())
                {
                    requisicion = dr.IsDBNull(dr.GetOrdinal("Acs_ReqOrden")) ? false : Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Acs_ReqOrden")));
                }
                dr.Close();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void revisionEspeciales(Sesion sesion, int Id_Prd, int Id_Ter, int Id_Fac, ref int verificador)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(sesion.Emp_Cnx);
            try
            {
                string[] Parametros = { 
                                        "@Id_Emp",
                                        "@Id_Cd",
                                        "@Id_Fac",
                                        "@Id_Prd",
                                        "@Id_Ter"
                                      };
                object[] Valores = { 
                                        sesion.Id_Emp,
                                        sesion.Id_Cd_Ver,
                                        Id_Fac,
                                        Id_Prd,
                                        Id_Ter
                                   };

                // -----------------------------------------------------------------
                // Actualizar encabezado de la orden de compra
                // -----------------------------------------------------------------                
                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd = CapaDatos.GenerarSqlCommand("spCapFacturaEspecial_Validacion", ref verificador, Parametros, Valores);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ValidaMontosImpresion(Factura factura, int Id_Cd, int Id_Emp, int iTipoDocumento, string conexion, ref bool bVerificador)
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
                                      factura.Id_Fac,
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

        public void ConsultaCorreoUsuarioAutoriza(Factura factura, string conexion, ref string correo)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(conexion);

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_Fac" };

                object[] Valores = { factura.Id_Emp, factura.Id_Cd, factura.Id_Fac };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatUsuario_ConsultaCorreoAutorizacion", ref dr, Parametros, Valores);

                //Dim VarUsuario As Usuario
                while (dr.Read())
                {
                    correo = dr.GetString(dr.GetOrdinal("U_Correo"));
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public void ConsultaEstatus(int Id_Fac, Sesion sesion, ref string Fac_Estatus)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(sesion.Emp_Cnx);

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_Fac" };

                object[] Valores = { sesion.Id_Emp, sesion.Id_Cd, Id_Fac };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spConsultaEstatus", ref dr, Parametros, Valores);

                //Dim VarUsuario As Usuario
                while (dr.Read())
                {
                    Fac_Estatus = dr.GetString(dr.GetOrdinal("Fac_Estatus"));
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
