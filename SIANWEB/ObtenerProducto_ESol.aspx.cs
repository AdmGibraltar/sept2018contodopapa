using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaEntidad;
using CapaDatos;
using CapaNegocios;

namespace SIANWEB
{
    public partial class ObtenerProducto_ESol : System.Web.UI.Page
    {
        private List<EntSalSolicitudDet> list_Es
        {
            get { return (List<EntSalSolicitudDet>)Session["ListEs" + Session.SessionID + HF_ClvPag]; }
            set { Session["ListEs" + Session.SessionID + HF_ClvPag] = value; }
        }
        private bool actualizacionDocumento
        {
            set { Session["actualizacionDocumentoES" + Session.SessionID + HF_ClvPag] = value; }
            get { return (bool)Session["actualizacionDocumentoES" + Session.SessionID + HF_ClvPag]; }
        }
        private string HF_ClvPag;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                string valor_retorno = "";

                if (Request.Params["ini"] != null || sesion == null)
                {
                    valor_retorno = "-0";
                }
                else
                {
                    try
                    {
                        string acc_ = Request.Params["acc"].ToString();
                        string prd_ = Request.Params["prd"].ToString();

                        string gpo_ = "";
                        string ref_ = "";
                        string es_ = "";
                        string ter_ = "";
                        string nat_ = "";
                        string mov_ = "";
                        string cte_ = "";
                        string pre_ = "";
                        int can_ = 0;



                        if (acc_ == "can")
                        {
                            nat_ = Request.Params["nat"].ToString();
                            mov_ = Request.Params["mov"].ToString();
                            cte_ = Request.Params["cte"].ToString();
                            can_ = Convert.ToInt32(Request.Params["can"]);
                            es_ = Request.Params["es"].ToString();
                        }

                        if (acc_ == "can" || acc_ == "val")
                        {
                            gpo_ = Request.Params["gpo"].ToString();
                            ref_ = Request.Params["ref"].ToString();
                            ter_ = Request.Params["ter"].ToString();
                            HF_ClvPag = Request.Params["clv"].ToString();
                        }

                        if (acc_ == "cos")
                        {
                            pre_ = Request.Params["pre"].ToString();
                        }





                        CN_CatProducto cn_pro = new CN_CatProducto();
                        Producto producto = new Producto();
                        try
                        {
                            cn_pro.ConsultaProducto(ref producto, sesion.Emp_Cnx, sesion.Id_Emp, sesion.Id_Cd_Ver, sesion.Id_Cd_Ver, Convert.ToInt32(prd_),3);

                            if (acc_ == "val") //PRODUCTO VALIDO
                            {
                                valor_retorno = Producto_Valido(sesion, valor_retorno, gpo_, ref_, ter_, prd_, producto);
                            }
                            else if (acc_ == "can") //CANTIDAD
                            {
                                valor_retorno = Producto_Cantidad(sesion, valor_retorno, nat_, gpo_, Convert.ToInt32(prd_), ref_, Convert.ToInt32(es_), Convert.ToInt32(ter_), can_, mov_, cte_, producto);
                            }
                            else if (acc_ == "cos") //COSTO
                            {
                                valor_retorno = Producto_Costo(sesion, valor_retorno, producto, Convert.ToDouble(pre_));
                            }

                        }
                        catch (Exception ex)
                        {
                            valor_retorno = "-1@@" + ex.Message;
                        }
                    }
                    catch
                    {
                        valor_retorno = "-1";
                    }



                }
                Response.Write(valor_retorno);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string Producto_Costo(Sesion sesion, string valor_retorno, Producto producto, double precio)
        {
            try
            {
                valor_retorno = "1";

                if (precio == 0)
                {
                    if (producto.Prd_Colo)
                    {
                        valor_retorno = "-1@@" + "Es importante tener actualizados los costos en los productos de compras locales; favor de entrar al catalogo de productos para capturar correctamente el precio vigente AAA";
                    }
                    else
                    {
                        valor_retorno = "-1@@" + "El costo no puede ser cero";
                    }
                }
                return valor_retorno;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private string Producto_Cantidad(Sesion sesion, string valor_retorno, string nat_, string gpo_, int id_prd, string ref_, int es_, int ter_, int can_, string mov_, string cte_, Producto producto)
        {
            try
            {
                if (nat_ == "1")
                {
                    int cantidadB = 0;
                    foreach (EntSalSolicitudDet dr in list_Es)
                    {
                        if (dr.Id_Prd.ToString() == id_prd.ToString())
                        {
                            cantidadB = cantidadB + Convert.ToInt32(dr.ESol_Cantidad);
                        }
                    }
                    if (Session["estatus" + Session.SessionID + HF_ClvPag].ToString() == "1")
                    {
                        cantidadB = cantidadB - Convert.ToInt32(Session["CantidadEdicion" + Session.SessionID + HF_ClvPag]);
                    }

                    CN_CapRemision rem = new CN_CapRemision();
                    int cantidadES2 = 0;
                    if (actualizacionDocumento)
                    {
                        rem.ConsultarRemisionesCantidadRemCantidad(sesion.Id_Emp, sesion.Id_Cd_Ver, es_, id_prd, nat_, ref cantidadES2, sesion.Emp_Cnx);
                    }


                    if (producto.Prd_InvFinal - producto.Prd_Asignado + cantidadES2 < can_ + cantidadB)
                    {
                        return "-1@@" + "No hay producto suficiente";
                    }


                }
                else if (gpo_ == "0")
                {
                    int edicion = Convert.ToInt32(Session["CantidadEdicion" + Session.SessionID + HF_ClvPag]);
                    if (producto.Prd_InvFinal - producto.Prd_Asignado - (edicion - can_) < 0)
                    {
                        return "-1@@" + "Producto " + producto.Id_Prd.ToString() + " inventario disponible insuficiente, inventario final: " + producto.Prd_InvFinal.ToString() + ", asignado: " + producto.Prd_Asignado.ToString() + ", disponible:" + (producto.Prd_InvFinal - producto.Prd_Asignado).ToString();
                    }
                }

                if (gpo_ == "4" || gpo_ == "2")
                {

                    CN_CapEntradaSalida CNentrada = new CN_CapEntradaSalida();
                    int verificador = 0;
                    CNentrada.ConsultarSaldo(sesion.Id_Emp, sesion.Id_Cd_Ver, id_prd.ToString(), ter_.ToString(), cte_, sesion.Emp_Cnx, ref verificador, mov_);
                    int Prd_AgrupadoSpo = producto.Prd_AgrupadoSpo;

                    int cantidadEnDt = 0;
                    foreach (EntSalSolicitudDet dr in list_Es.Where(EntradaSalidaDetalle => EntradaSalidaDetalle.Prd_AgrupadoSpo == Prd_AgrupadoSpo && EntradaSalidaDetalle.Id_Ter == ter_ && EntradaSalidaDetalle.Id_Prd != id_prd).ToList())
                    {
                        cantidadEnDt += dr.ESol_Cantidad;
                    }

                    CN_CapRemision rem = new CN_CapRemision();
                    int cantidadES2 = 0;
                    if (actualizacionDocumento)
                    {
                        rem.ConsultarRemisionesCantidadRemCantidad(sesion.Id_Emp, sesion.Id_Cd_Ver, es_, Prd_AgrupadoSpo, nat_, ref cantidadES2, sesion.Emp_Cnx);
                        verificador += cantidadES2;
                    }


                    if (cantidadEnDt + can_ > verificador)
                    {
                        return "-1@@" + "Los artículos sobrepasan lo disponible";
                    }

                }
                else if (gpo_ == "3")
                {
                    CN_CapRemision rem = new CN_CapRemision();


                    int cantidadES = 0;

                    int cantidadEnDttemp_original = 0;
                    if (Session["estatus" + Session.SessionID + HF_ClvPag].ToString() != "1")
                    {
                        cantidadEnDttemp_original = 0;
                    }
                    else
                    {
                        cantidadEnDttemp_original = Convert.ToInt32(Session["CantidadEdicion" + Session.SessionID + HF_ClvPag]);
                    }

                    int cantidadB = 0;
                    foreach (EntSalSolicitudDet dr in list_Es)
                    {
                        if (dr.Id_Prd.ToString() == id_prd.ToString())
                        {
                            cantidadB += dr.ESol_Cantidad;

                        }
                    }


                    //rem.ConsultarRemisionesCantidad(session.Id_Emp, session.Id_Cd_Ver, refe, id_prd, ref cantidadES, session.Emp_Cnx);
                    rem.ConsultarRemisionesCantidadRem(sesion.Id_Emp, sesion.Id_Cd_Ver, ref_, id_prd, ref cantidadES, sesion.Emp_Cnx);
                    int cantidadES2 = 0;
                    if (actualizacionDocumento)
                    {
                        rem.ConsultarRemisionesCantidadRemCantidad(sesion.Id_Emp, sesion.Id_Cd_Ver, es_, id_prd, nat_, ref cantidadES2, sesion.Emp_Cnx);
                        cantidadES += cantidadES2;
                    }




                    if (cantidadES < cantidadB - cantidadEnDttemp_original + can_)
                    //if (cantidadES < can_)
                    {
                        return "-1@@" + "Los artículos sobrepasan el disponible";

                    }

                    if (producto.Prd_InvFinal - producto.Prd_Asignado - (cantidadEnDttemp_original - can_) < 0)
                    {
                        return "-1@@" + "Producto " + producto.Id_Prd.ToString() + " inventario disponible insuficiente, inventario final: " + producto.Prd_InvFinal.ToString() + ", asignado: " + producto.Prd_Asignado.ToString() + ", disponible:" + (producto.Prd_InvFinal - producto.Prd_Asignado).ToString();

                    }
                }
                else if (gpo_ == "1")
                {
                    if (actualizacionDocumento)
                    {
                        CN_CapRemision rem = new CN_CapRemision();
                        int cantidadES2 = 0;
                        rem.ConsultarRemisionesCantidadRemCantidad(sesion.Id_Emp, sesion.Id_Cd_Ver, es_, id_prd, nat_, ref cantidadES2, sesion.Emp_Cnx);

                        Producto cp = new Producto();
                        CN_CatProducto cn_catproducto = new CN_CatProducto();
                        cn_catproducto.ConsultaProducto(ref cp, sesion.Emp_Cnx, sesion.Id_Emp, sesion.Id_Cd_Ver, id_prd);

                        int cantidadB = 0;
                        foreach (EntSalSolicitudDet dr in list_Es)
                        {
                            if (dr.Id_Prd.ToString() == id_prd.ToString())
                            {
                                cantidadB += dr.ESol_Cantidad;

                            }
                        }

                        cantidadB = cantidadB - Convert.ToInt32(Session["CantidadEdicion" + Session.SessionID + HF_ClvPag]) + (int)can_;
                        if (cantidadB < cantidadES2 && (cantidadES2 - cantidadB) > (cp.Prd_InvFinal - cp.Prd_Asignado))
                        {
                            return "-1@@" + "Producto " + id_prd.ToString() + " inventario disponible insuficiente, inventario final: " + cp.Prd_InvFinal.ToString() + ", asignado: " + cp.Prd_Asignado.ToString() + " , disponible: " + (cp.Prd_InvFinal - cp.Prd_Asignado).ToString() + "";

                        }

                    }
                }
                return "1";
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private string Producto_Valido(Sesion sesion, string valor_retorno, string gpo_, string ref_, string ter_, string prd_, Producto producto)
        {
            try
            {

                if (Session["estatus" + Session.SessionID + HF_ClvPag].ToString() != "1" && list_Es.Where(EntradaSalidaDetalle => EntradaSalidaDetalle.Id_Prd.ToString() == prd_).ToList().Count > 0)
                {
                    valor_retorno = "-1@@" + "No es permitido ingresar el mismo producto";
                }

                if (gpo_ == "2")
                {
                    if (!(bool)producto.Prd_AparatoSisProp)
                    {
                        valor_retorno = "-1@@" + "El producto no es un sistema de propietarios";

                    }
                    else if (producto.Prd_Nuevo)
                    {
                        valor_retorno = "-1@@" + "El código de producto debe ser codigo usado";
                    }
                }
                else if (gpo_ == "3")
                {

                    CN_CapRemision cnremision = new CN_CapRemision();
                    Remision remision = new Remision();
                    remision.Id_Emp = sesion.Id_Emp;
                    remision.Id_Cd = sesion.Id_Cd_Ver;
                    remision.Id_Rem = Convert.ToInt32(ref_);
                    List<RemisionDet> list = new List<RemisionDet>();
                    cnremision.ConsultarRemisionesDetalle(sesion, remision, ref list);

                    int encontrados = 0;
                    foreach (RemisionDet rd in list)
                    {
                        if (producto.Id_Prd == rd.Id_Prd && ter_ == rd.Id_Ter.ToString())
                        {
                            encontrados += 1;
                        }
                    }
                    if (encontrados == 0)
                    {
                        valor_retorno = "-1@@" + "El producto no pertenece al documento de referencia";
                    }
                }

                if (valor_retorno == "")
                {
                    float precio = obtenerPrecioAAA(prd_);

                    valor_retorno = "@@" + precio.ToString() + "@@" + producto.Prd_Presentacion + "@@" + producto.Prd_Descripcion + "@@" + producto.Prd_AgrupadoSpo;

                    if (precio == 0)
                    {
                        if (gpo_ == "1" && producto.Prd_Colo == true)
                        {

                            valor_retorno = "-2" + valor_retorno + "@@" + "Es importante tener actualizado los costos en productos de " +
                                "compras locales; favor de entrar a Inventarios - Catálogo - Productos " +
                                ", para capturar el precio vigente AAA";
                        }
                        else
                        {
                            valor_retorno = "-2" + valor_retorno + "@@" + "No se ha definido el precio para este producto";
                        }
                    }
                    else
                    {
                        valor_retorno = "1" + valor_retorno;
                    }
                }
                return valor_retorno;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private float obtenerPrecioAAA(string Id_Prd)
        {
            try
            {
                float precio = 0;
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CN_ProductoPrecios cn_proprec = new CN_ProductoPrecios();
                int Id_Pre = 0;
                cn_proprec.ConsultaListaProductoPrecioAAA(ref precio, Sesion, Convert.ToInt32(Id_Prd), ref Id_Pre);
                return precio;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}