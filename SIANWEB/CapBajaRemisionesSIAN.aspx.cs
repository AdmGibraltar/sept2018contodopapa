using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Collections;
using System.Data;
using CapaEntidad;
using CapaNegocios;
using Telerik.Web.UI;

namespace SIANWEB
{
    public class PartidasBajaRemisionSian
    {
        private string[] _ocprocostosian;
        public string[] Ocprocostosian
        {
            get { return _ocprocostosian; }
            set { _ocprocostosian = value; }
        }

        private string[] ocfechasian;
        public string[] Ocfechasian
        {
            get { return ocfechasian; }
            set { ocfechasian = value; }
        }

        private string[] ocremsian;
        public string[] Ocremsian
        {
            get { return ocremsian; }
            set { ocremsian = value; }
        }

        private string[] oczonnumsian;
        public string[] Oczonnumsian
        {
            get { return oczonnumsian; }
            set { oczonnumsian = value; }
        }

        private string[] ocpronumsian;
        public string[] Ocpronumsian
        {
            get { return ocpronumsian; }
            set { ocpronumsian = value; }
        }

        private string[] ocantidadsian;
        public string[] Ocantidadsian
        {
            get { return ocantidadsian; }
            set { ocantidadsian = value; }
        }

        private string[] ocrecibida;
        public string[] Ocrecibida
        {
            get { return ocrecibida; }
            set { ocrecibida = value; }
        }

        private string[] cantidad;
        public string[] Cantidad
        {
            get { return cantidad; }
            set { cantidad = value; }
        }
    }

    public partial class CapBajaRemisionesSIAN : System.Web.UI.Page
    {
        #region Variables
        private bool _PermisoGuardar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoModificar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoEliminar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoImprimir { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }

        private string htmlMensaje = string.Concat("<div style=\"background-color: #E9F0FC; border-color: #1287DE; border-style:dashed; border-width:1px; padding: 10px 10px 10px 10px; margin: 10px 10px 10px 10px ; font-family:Arial Tahoma\">@@mensaje</div>");
        //int Id_Folio;

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];

                if (!Page.IsPostBack)
                {
                    // --------------------------------------------------------
                    // OBTENER VARIABLES ENVIADAS POR POST ---> desde MACOLA
                    // --------------------------------------------------------
                    //if (Request.Cookies["PartidasBajaRemisionSianC"] == null)
                    //{
                    //}
                    //if (Session["PartidasBajaRemisionSian"] == null)
                    //{
                    System.Text.StringBuilder displayValues = new System.Text.StringBuilder();
                    System.Collections.Specialized.NameValueCollection postedValues = Request.Form;
                    String nextKey;
                    for (int i = 0; i < postedValues.AllKeys.Length; i++)
                    {
                        nextKey = postedValues.AllKeys[i];

                        displayValues.Append(nextKey);
                        displayValues.Append(" = ");
                        displayValues.Append(postedValues[i]);
                        displayValues.Append("|");
                    }
                    //PartidasBajaRemisionSian pbr = new PartidasBajaRemisionSian();
                    //pbr.Ocprocostosian = postedValues["ocprocostosian"].Trim().Split(new char[] { ',' });
                    //pbr.Ocfechasian = postedValues["ocfechasian"].Trim().Split(new char[] { ',' });
                    //pbr.Ocremsian = postedValues["ocremsian"].Trim().Split(new char[] { ',' });
                    //pbr.Oczonnumsian = postedValues["oczonnumsian"].Trim().Split(new char[] { ',' });
                    //pbr.Ocpronumsian = postedValues["ocpronumsian"].Trim().Split(new char[] { ',' });
                    //pbr.Ocantidadsian = postedValues["ocantidadsian"].Trim().Split(new char[] { ',' });
                    //pbr.Ocrecibida = postedValues["ocrecibida"].Trim().Split(new char[] { ',' });
                    //pbr.Cantidad = postedValues["cantidad"].Trim().Split(new char[] { ',' });

                    //Session["PartidasBajaRemisionSian"] = pbr;

                    //Para guardar
                    //HttpCookie dato = new HttpCookie("PartidasBajaRemisionSianC");
                    if (!string.IsNullOrEmpty(displayValues.ToString()))
                        Session["PartidasBajaRemisionSianC" + Session.SessionID] = displayValues.ToString().Substring(0, displayValues.ToString().Length - 1);
                    //if (Request.Cookies["PartidasBajaRemisionSianC"] == null)
                    //{
                    //    Response.AppendCookie(dato);
                    //}
                    //else
                    //{
                    //    Request.Cookies.Set(dato);
                    //}
                    //}
                    // --------------------------------------------------------
                }

                if (Sesion == null)
                {
                    string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);
                    Session["dir" + Session.SessionID] = pag[pag.Length - 1];
                    Response.Redirect("login.aspx", false);
                }
                else
                    if (!Page.IsPostBack)
                    {
                        this.ValidarPermisos();
                        if (Inicializar())
                            this.BajaRemisionesSIAN();
                    }
            }
            catch (Exception ex)
            {//ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
                if (!ex.Message.ToUpper().Contains("SUBPROCESO ANULADO"))
                    lblMensaje.Text = htmlMensaje.Replace("@@mensaje", string.Concat("Error al cargar la página.<br/><br/>", ex.Message));
            }

        }

        #region Eventos

        private void BajaRemisionesSIAN()
        {
            string accionError = string.Empty;
            string mensajeError = string.Empty;
            try
            {
                this.Guardar();
                lblMensaje.Text = htmlMensaje.Replace("@@mensaje", string.Concat("Los datos se guardaron correctamente.<br/>"));
            }
            catch (Exception ex)
            {
                string mensaje = string.Concat(ex.Message, mensajeError);
                //if mensaje.Contains("")

                lblMensaje.Text = htmlMensaje.Replace("@@mensaje", string.Concat("Error al realizar la baja de remisiones a SIAN.<br/><br/>", ex.Message));
            }

        }

        #endregion

        #region "Funciones y Subs"

        private bool Inicializar()
        {
            try
            {
                bool parametrosCorrectos = true;
                string mensaje = string.Empty;
                Sesion session = new Sesion();
                session = (Sesion)Session["Sesion" + Session.SessionID];

                //string IdEmp = Request.Params["Id1"] != null ? Request.Params["Id1"].ToString() : "";
                //string IdCd = Request.Params["Id2"] != null ? Request.Params["Id2"].ToString() : "";
                //string IdOrd = Request.Params["Id3"] != null ? Request.Params["Id3"].ToString() : "";

                //if (session.Id_Emp.ToString() == IdEmp)
                //{
                //    if (session.Id_Cd_Ver.ToString() == IdCd)
                //    {
                //        if (string.IsNullOrEmpty(IdOrd))
                //        {
                //            parametrosCorrectos = false;
                //            mensaje = "No se encontró la orden de compra";
                //        }
                //    }
                //    else
                //    {
                //        parametrosCorrectos = false;
                //        mensaje = "La orden de compra no pertenece al centro de distribución en el que se encuentra";
                //    }
                //}
                //else
                //{
                //    parametrosCorrectos = false;
                //    mensaje = "La orden de compra no pertenece a la empresa en la que inicio sesión";
                //}
                if (!string.IsNullOrEmpty(mensaje))
                    Response.Write(htmlMensaje.Replace("@@mensaje", mensaje));
                return parametrosCorrectos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ValidarPermisos()
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];

                Pagina pagina = new Pagina();
                string[] pag = Page.Request.Url.ToString().Split(new string[] { "?" }, StringSplitOptions.RemoveEmptyEntries);
                //if (pag.Length > 1)
                //{
                //    pagina.Url = (new System.IO.FileInfo(Page.Request.Url.AbsolutePath)).Name + "?" + pag[1];
                //}
                //else
                //{
                pagina.Url = (new System.IO.FileInfo(Page.Request.Url.AbsolutePath)).Name;
                //}

                CN_Pagina CapaNegocio = new CN_Pagina();
                CapaNegocio.PaginaConsultar(ref pagina, Sesion.Emp_Cnx);

                Session["Head" + Session.SessionID] = pagina.Descripcion;
                this.Title = pagina.Descripcion;
                Permiso Permiso = new Permiso();
                Permiso.Id_U = Sesion.Id_U;
                Permiso.Id_Cd = Sesion.Id_Cd;
                Permiso.Sm_cve = pagina.Clave;
                //Esta clave depende de la pantalla

                CapaDatos.CD_PermisosU CN_PermisosU = new CapaDatos.CD_PermisosU();
                CN_PermisosU.ValidaPermisosUsuario(ref Permiso, Sesion.Emp_Cnx);

                if (Permiso.PAccesar == true)
                {
                    _PermisoGuardar = Permiso.PGrabar;
                    _PermisoModificar = Permiso.PModificar;
                    _PermisoEliminar = Permiso.PEliminar;
                    _PermisoImprimir = Permiso.PImprimir;

                    if (Permiso.PGrabar == false)
                    {
                        //this.rtb1.Items[1].Visible = false;
                    }
                }
                else
                {
                    //Response.Write(htmlMensaje.Replace("@@mensaje", string.Concat("No se cuenta con permisos suficientes para acceder a la página.<br/>")));
                    //Alerta("No se cuenta con permisos suficientes para acceder a la página");
                    throw new Exception(htmlMensaje.Replace("@@mensaje", string.Concat("No cuenta con permiso para realizar la operación de baja de remisiones a SIAN")));
                    //Session["dir" + Session.SessionID] = pag[pag.Length - 1];                    Response.Redirect("login.aspx" , false);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Guardar()
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];

                PartidasBajaRemisionSian pbr = null;
                if (Session["PartidasBajaRemisionSianC" + Session.SessionID] != null)
                {
                    string cookie_data = Session["PartidasBajaRemisionSianC" + Session.SessionID].ToString();
                    string[] arrayCookie = cookie_data.Split(new char[] { '|' });
                    pbr = new PartidasBajaRemisionSian();

                    foreach (string dato in arrayCookie)
                    {
                        string[] datoValores = dato.Split(new char[] { '=' });
                        switch (datoValores[0].Trim())
                        {
                            case "ocprocostosian":
                                pbr.Ocprocostosian = datoValores[1].Trim().Split(new char[] { ',' });
                                break;
                            case "ocfechasian":
                                pbr.Ocfechasian = datoValores[1].Trim().Split(new char[] { ',' });
                                break;
                            case "ocremsian":
                                pbr.Ocremsian = datoValores[1].Trim().Split(new char[] { ',' }); //<--- ESTE ES EL IDENTIFICADOR
                                break;
                            case "oczonnumsian":
                                pbr.Oczonnumsian = datoValores[1].Trim().Split(new char[] { ',' });
                                break;
                            case "ocpronumsian":
                                pbr.Ocpronumsian = datoValores[1].Trim().Split(new char[] { ',' });
                                break;
                            case "ocantidadsian":
                                pbr.Ocantidadsian = datoValores[1].Trim().Split(new char[] { ',' });
                                break;
                            case "ocrecibida":
                                pbr.Ocrecibida = datoValores[1].Trim().Split(new char[] { ',' });
                                break;
                            case "cantidad":
                                pbr.Cantidad = datoValores[1].Trim().Split(new char[] { ',' });
                                break;
                        }
                    }
                }
                //Request.Cookies.Remove("PartidasBajaRemisionSianC");
                if (pbr != null)
                {
                    //llenar objeto de entrada-salida
                    EntradaSalida entSal = new EntradaSalida();
                    entSal.Id_Emp = sesion.Id_Emp;
                    entSal.Id_Cd = sesion.Id_Cd_Ver;
                    entSal.Id_U = sesion.Id_U;
                    CentroDistribucion cd = new CentroDistribucion();
                    new CN_CatCentroDistribucion().ConsultarCentroDistribucion(ref cd, sesion.Id_Cd_Ver, sesion.Id_Emp, sesion.Emp_Cnx);
                    if (cd.Id_TipoCD == 1)
                        entSal.Id_Tm = 4;
                    else
                        entSal.Id_Tm = 2;
                    entSal.Id_Cte = 0; //se debe enviar como NULL cuando se guarde
                    entSal.Id_Pvd = 100;
                    entSal.Es_Naturaleza = 0;//entrada
                    entSal.Es_Fecha = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                    if (pbr.Ocrecibida != null && pbr.Ocremsian != null && pbr.Ocfechasian != null)
                        if ((pbr.Ocrecibida.Length > 0) && (pbr.Ocremsian.Length > 0) && (pbr.Ocfechasian.Length > 0))
                        {
                            entSal.Es_Referencia = pbr.Ocremsian[0];
                            entSal.Es_Notas = string.Concat("Entrada automática de almacén central con el folio ", pbr.Ocremsian[0], " el dia ", pbr.Ocfechasian[0]);
                        }
                    entSal.Es_SubTotal = 0;
                    entSal.Es_Iva = 0;
                    entSal.Es_Total = 0; // <--- se calcula en el SP
                    entSal.Es_Estatus = "I";

                    List<EntradaSalidaDetalle> listaEntSal = new List<EntradaSalidaDetalle>();
                    //List<OrdenCompraDet> listaOCDet = new List<OrdenCompraDet>();
                    string mensaje = string.Empty;
                    int verificador = 0;

                    //Recorre array de partidas segun los datos que llegaron por HTTP POST
                    if (pbr.Ocpronumsian != null)
                        for (int i = 0; i < pbr.Ocpronumsian.Length; i++)
                        {
                            //OrdenCompraDet OCDet = new OrdenCompraDet();
                            //OCDet.Id_Emp = entSal.Id_Emp;
                            //OCDet.Id_Cd = entSal.Id_Cd;
                            //OCDet.Id_Ord = Convert.ToInt32(pbr.Ocremsian[0]);
                            //OCDet.Ord_CantidadCump = Convert.ToSingle(pbr.Cantidad[0]);
                            //listaOCDet.Add(OCDet);

                            //Crear item de lista de entrada-salida mov. 16
                            EntradaSalidaDetalle entSalDetalle = new EntradaSalidaDetalle();
                            entSalDetalle.Id_Emp = entSal.Id_Emp;
                            entSalDetalle.Id_Cd = entSal.Id_Cd;
                            entSalDetalle.Id_Es = 0;//se reasigna al insertar la entSal de encabezado
                            entSalDetalle.Id_EsDet = 0;//se reasigna al insertar la entSalDetalle
                            entSalDetalle.Id_Ter = 0; //se debe enviar como NULL cuando se guarde
                            entSalDetalle.Id_Prd = Convert.ToInt32(pbr.Ocpronumsian[i]);
                            entSalDetalle.EsDet_Naturaleza = 0; //entrada
                            entSalDetalle.Es_Cantidad = Convert.ToInt32(pbr.Cantidad[i]);
                            entSalDetalle.Es_Costo = Convert.ToDouble(pbr.Ocprocostosian[i]);
                            entSalDetalle.Es_BuenEstado = true;
                            entSalDetalle.Afct_OrdCompra = true;
                            entSalDetalle.Es_CantidadRem = Convert.ToInt32(pbr.Ocantidadsian[i]); //OGC
                            listaEntSal.Add(entSalDetalle);
                        }
                    entSal.ListaDetalle = listaEntSal;
                    if (pbr.Ocpronumsian.Length == entSal.ListaDetalle.Count)
                    {
                        // ------------------------------------------------------------
                        // Insertar movimiento de Entrada y actualizar inventario
                        // ------------------------------------------------------------
                        if (!string.IsNullOrEmpty(entSal.Es_Referencia))
                        {
                            new CN_CapEntradaSalida().Bitacora_BajaRemisionesSIAN(ref entSal, sesion.Emp_Cnx, ref verificador);
                            new CN_CapEntradaSalida().InsertarEntradaSalida_BajaRemisionesSIAN(ref entSal, sesion.Emp_Cnx, ref verificador);
                            if (verificador == -1)
                            {
                                throw new Exception("Esta remisión ya se encuentra registrada en SIAN y no es posible ingresarla de nuevo.");
                            }
                            else if (verificador == 2)
                            {
                                throw new Exception("El sistema no pudo realizar la descarga de todas las partidas, favor de intentar de nuevo.");
                            }
                        }
                        else
                            throw new Exception("El sistema no pudo obtener la información para realizar la baja de remisiones a SIAN, favor de intentar de nuevo.");
                    }
                    else
                        throw new Exception("El sistema no pudo realizar la descarga de todas las partidas, favor de intentar de nuevo.");
                }
                else
                    throw new Exception("El sistema no pudo obtener la información para realizar la baja de remisiones a SIAN, favor de intentar de nuevo.");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region ErrorManager

        private void Alerta(string mensaje)
        {
            RAM1.ResponseScripts.Add(string.Concat(@"radalert('", mensaje, "', 330, 10, '');"));
        }

        #endregion
    }
}