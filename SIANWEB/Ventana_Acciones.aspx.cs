using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using CapaEntidad;
using CapaNegocios;
using CapaDatos;
using Telerik.Web.UI;
using System.Data;
namespace SIANWEB
{
    public partial class Ventana_Acciones : System.Web.UI.Page
    {
        #region Variables
        private string Emp_CnxCob
        {
            get { return ConfigurationManager.AppSettings.Get("strConnectionCobranza"); }
        }
        private bool _PermisoGuardar
        {
            get
            {
                if (Session["Sesion" + Session.SessionID] == null)
                {
                    return false;
                }
                return (bool)Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]];
            }
            set
            {
                Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value;
            }
        }
        private bool _PermisoModificar
        {
            get
            {
                if (Session["Sesion" + Session.SessionID] == null)
                {
                    return false;
                }
                return (bool)Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]];
            }
            set
            {
                Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value;
            }
        }
        private bool _PermisoEliminar
        {
            get
            {
                if (Session["Sesion" + Session.SessionID] == null)
                {
                    return false;
                }
                return (bool)Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]];
            }
            set
            {
                Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value;
            }
        }
        private bool _PermisoImprimir
        {
            get
            {
                if (Session["Sesion" + Session.SessionID] == null)
                {
                    return false;
                }
                return (bool)Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]];
            }
            set
            {
                Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value;
            }
        }
        private Sesion sesion
        {
            get
            {
                return (Sesion)Session["Sesion" + Session.SessionID];
            }
        }
        #endregion
        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            try 
            {
                //bool postback = (bool)Session["PostBackPagos" + Session.SessionID];
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                if (Sesion == null)
                    CerrarVentana();
                else
                {
                    if (!Page.IsPostBack)
                    {
                        if (Sesion.Cu_Modif_Pass_Voluntario == false)
                        {
                            RAM1.ResponseScripts.Add("AbrirContrasenas(); return false;");
                            return;
                        }

                        Inicializar();
                    }

                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        private void Inicializar()
        {
            try
            {
                lblTipo.Text = Request.QueryString["Etapa"].Replace("'", "");
                lblDias.Text = (Request.QueryString["Dias"] == "")? "" : Request.QueryString["Dias"].Replace("'", "");
                lblCliente.Text = Request.QueryString["Cliente"].Replace("'", "") + " " + Request.QueryString["Cliente"].Replace("'", "");
                lblDocumento.Text = Request.QueryString["Documento"].Replace("'", "");
                lblImporte.Text = Request.QueryString["Importe"].Replace("'", "");
                lblSaldo.Text = Request.QueryString["Saldo"].Replace("'", "");
                HiddenIdCliente.Value = Request.QueryString["Id_Cte"].Replace("'", "");


                if (Request.QueryString["Documento"].Replace("'", "") == "-1") {
                    lblDias.Visible = false;
                    lblDocumento.Visible = false;
                    lblDia.Visible = false;
                    lblDoc.Visible = false;
                }

                CN_GestorCobranza cn_cob = new CN_GestorCobranza();
                Cobranza cob = new Cobranza();
                cob.Id_Emp = Convert.ToInt32(Request.QueryString["Id_Emp"].Replace("'", ""));
                cob.Id_Cd = Convert.ToInt32(Request.QueryString["Id_Cd"].Replace("'", ""));
                cob.Id_FacSerie = Request.QueryString["Documento"].Replace("'", "").Trim();
                cob.Id_Cte = Convert.ToInt32(Request.QueryString["Id_Cte"].Replace("'", ""));

                if (cob.Id_FacSerie == "-1")
                {
                   
                    this.LlenarFacturas();
                    divFacturas.Style.Add("display", "block");
                }
                

                //CONSULTA LA BITACORA
                string bitacora = "";
                cn_cob.ConsultarBitacora(cob, ref bitacora, Emp_CnxCob);
                divBitacora.InnerHtml = bitacora == "" ? "No se encontraron acciones" : bitacora;

                //CONSULTA SI HAY ACCIONES PENDIENTES
                cob.Tipo = Request.QueryString["TipoN"].Replace("'", "");
                if ((Request.QueryString["Dias"] != "")) {
                 cob.Caso = Convert.ToInt32(Request.QueryString["Dias"].Replace("'", ""));
                }
                
                List<Pregunta> list = new List<Pregunta>();
                cn_cob.ConsultarAcciones(cob, ref list, Emp_CnxCob);

                if (list.Count == 0)
                {
                    divPreguntas.InnerHtml = "No hay acciones pendientes por realizar";
                    rtb1.Items[1].Visible = false;
                    //CerrarVentana();
                }


               


                Label lbl;
                RadioButton cb;
                RadTextBox rtb;
                int i = 0;

                foreach (Pregunta p in list)
                {
                    lbl = new Label();

                    if (i > 0)
                    {
                        lbl.Text = "<br><br>";
                    }
                    else
                    {
                        lbl.Text = "<br>";
                    }
                    i++;

                    lbl.Text += p.Id_Pre + ". " + p.pregunta + "<br>";
                    lbl.Font.Bold = true;
                    divPreguntas.Controls.Add(lbl);

                    if (p.tpregunta == "A")
                    {
                        rtb = new RadTextBox();

                        rtb.Width = 150;
                        rtb.ClientEvents.OnKeyPress = "SinComillas";
                        divPreguntas.Controls.Add(rtb);
                    }
                    else
                    {
                        foreach (string s in p.respuestas)
                        {
                            cb = new RadioButton();
                            cb.GroupName = p.Id_Pre.ToString();
                            cb.Text = s + "".PadRight(25, ' ').Replace(" ", "&nbsp;");
                            divPreguntas.Controls.Add(cb);
                        }
                    }
                }
                //listFacturas.SelectedIndexChanged += new EventHandler(ListFacturas_SelectedIndexChanged);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /*
        protected void listFacturas_SelectedIndexChanged(object sender, EventArgs e)
        {
            Alerta(listFacturas.SelectedValue);
        }*/

        protected void rtb1_ButtonClick(object sender, Telerik.Web.UI.RadToolBarEventArgs e)
        {

        }
        #endregion
        #region Funciones
        private void CerrarVentana()
        {
            try
            {
                string funcion;
                if (this.HiddenRebind.Value == "0")
                {
                    funcion = "CloseWindow()";
                }
                else
                {
                    funcion = "CloseAndRebind()";
                }
                string script = "<script>" + funcion + "</script>";
                ScriptManager.RegisterStartupScript(this, this.GetType(), funcion, script, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region ErrorManager
        private void AlertaFocus(string mensaje, string rtb)
        {
            try
            {
                RAM1.ResponseScripts.Add("AlertaFocus('" + mensaje + "','" + rtb + "');");
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "Alerta");
            }
        }
        private void Alerta(string mensaje)
        {
            try
            {
                RAM1.ResponseScripts.Add("radalert('" + mensaje + "', 340, 150);");
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "Alerta");
            }
        }
        private void Alerta2(string mensaje)
        {
            try
            {
                RAM1.ResponseScripts.Add("radalert('" + mensaje + "', 600, 150);");
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "Alerta");
            }
        }
        private void ErrorManager()
        {
            try
            {
                this.lblMensaje.Text = "";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void ErrorManager(string Message)
        {
            try
            {
                this.lblMensaje.Text = Message;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void ErrorManager(Exception eme, string NombreFuncion)
        {
            try
            {
                this.lblMensaje.Text = "Error: [" + NombreFuncion + "] " + eme.Message.ToString();

            }
            catch (Exception ex)
            {
                this.lblMensaje.Text = "Error grave: " + eme.Message.ToString() + " --> " + ex.Message.ToString();
            }
        }
        #endregion

        protected void RAM1_AjaxRequest1(object sender, AjaxRequestEventArgs e)
        {

        }

        protected void OnItemChecked(object sender, RadListBoxItemEventArgs e)
        {
            Alerta(e.Item.Text);
        }




        private void LlenarFacturas()
        {

            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            CN_ConfiguracionCobranza cn_GestorCobranza = new CN_ConfiguracionCobranza();
            List<Factura> list = new List<Factura>();
            int Id_Cte = 0;

            if ((Request.QueryString["Id_Cte"] != ""))
                {
                    Id_Cte = Convert.ToInt32(Request.QueryString["Id_Cte"].Replace("'", ""));
                }

            try
            {

                cn_GestorCobranza.ConsultarFacturasVencidasPorCliente(sesion.Id_Emp, sesion.Id_Cd_Ver, Id_Cte, ref list, Emp_CnxCob);
            }
            catch (Exception ex)
            {
                
                    throw ex;
                
            }

            listFacturas.Visible = true;
            listFacturas.Items.Clear();
            foreach (Factura f in list)
            {
                RadListBoxItem rlbi = new RadListBoxItem();
                rlbi.Value = f.Id_FacSerie;
                rlbi.Text = f.Id_FacSerie;
               // rlbi.Checked = true;
                listFacturas.Items.Add(rlbi);
            }

            listFacturas.DataBind();
        }


        protected void rtb1_ButtonClick1(object sender, RadToolBarEventArgs e)
        {
            try
            {
                List<CapaEntidad.Pregunta> list_preg = new List<CapaEntidad.Pregunta>();
                CapaEntidad.Pregunta P = default(CapaEntidad.Pregunta);
                foreach (Control c in divPreguntas.Controls)
                {
                    switch (c.GetType().Name)
                    {
                        case "Label":
                            if (P != null)
                            {
                                list_preg.Add(P);
                            }
                            P = new CapaEntidad.Pregunta();
                            Label lb = (Label)c;
                            P.pregunta = lb.Text.Substring(lb.Text.IndexOf(" ")).Replace("<br>", "").Trim();
                            break;
                        case "RadTextBox":
                            RadTextBox rtb = (RadTextBox)c;
                            P.respuesta_final = rtb.Text.Trim();
                            list_preg.Add(P);
                            P = null;
                            break;
                        case "RadioButton":
                            RadioButton cb = (RadioButton)c;
                            if (cb.Checked)
                            {
                                P.respuesta_final = cb.Text.Replace("&nbsp;", "").Trim();
                                list_preg.Add(P);
                                P = null;
                            }
                            break;
                    }
                }
                if (P != null)
                {
                    P.respuesta_final = "";
                    list_preg.Add(P);
                }


                foreach (Pregunta p in list_preg)
                {
                    if (p.respuesta_final == "" || p.respuesta_final == null)
                    {
                        Alerta("No se ha capturado respuesta para <b>" + p.pregunta + "</b>");
                        return;
                    }
                }

                
                List<Bitacora> ListBitacora = new List<Bitacora>();
                CN_GestorCobranza cn_gestor = new CN_GestorCobranza();


                Funciones funcion = new Funciones();
                List<Factura> list = new List<Factura>();
                int Id_Cte = 0;
                string Id_Doc = "-1";
                if ((Request.QueryString["Id_Cte"] != ""))
                {
                    Id_Cte = Convert.ToInt32(Request.QueryString["Id_Cte"].Replace("'", ""));
                }

                if ((Request.QueryString["Documento"] != ""))
                {
                    Id_Doc = Request.QueryString["Documento"].Replace("'", "").ToString();
                
                }

                if (Id_Doc == "-1")
                {

                    CN_ConfiguracionCobranza cn_Configuracion = new CN_ConfiguracionCobranza();
                    cn_Configuracion.ConsultarFacturasVencidasPorCliente(sesion.Id_Emp, sesion.Id_Cd_Ver, Id_Cte, ref list, Emp_CnxCob);

                   IList<RadListBoxItem> collection = listFacturas.Items;                   
                                                             

                    foreach (Factura factura in list)
                    {
                        foreach (RadListBoxItem rlbi in collection)
                        {
                            if (rlbi.Checked)
                            {
                                if (rlbi.Value == factura.Id_FacSerie)
                                {
                                    Bitacora cob = new Bitacora();    
                                    cob.Id_Emp = Convert.ToInt32(Request.QueryString["Id_Emp"].Replace("'", ""));
                                    cob.Id_Cd = Convert.ToInt32(Request.QueryString["Id_Cd"].Replace("'", ""));
                                    cob.Id_FacSerie = factura.Id_FacSerie;
                                    cob.Bit_Fecha = funcion.GetLocalDateTime(sesion.Minutos);
                                    cob.Bit_Tipo = Request.QueryString["TipoN"].Replace("'", "");
                                    cob.Bit_Dias = factura.Dias;
                                    cob.Bit_Importe = Convert.ToDouble(factura.Fac_Importe.ToString());                                     
                                    cob.Bit_Saldo = Convert.ToDouble(factura.Fac_Saldo.ToString());
                                    cob.Id_Cte = Id_Cte;
                                    ListBitacora.Add(cob);
                                }
                            }
                        }
                    }

                }
                else
                {
                    Bitacora cob = new Bitacora();                    
                    cob.Id_Emp = Convert.ToInt32(Request.QueryString["Id_Emp"].Replace("'", ""));
                    cob.Id_Cd = Convert.ToInt32(Request.QueryString["Id_Cd"].Replace("'", ""));
                    cob.Id_FacSerie = lblDocumento.Text;
                    cob.Bit_Fecha = funcion.GetLocalDateTime(sesion.Minutos);
                    cob.Bit_Tipo = Request.QueryString["TipoN"].Replace("'", "");
                    cob.Bit_Dias = Convert.ToInt32(Request.QueryString["Dias"].Replace("'", ""));
                    cob.Bit_Importe = Convert.ToDouble(lblImporte.Text.Replace("$", "").Replace(",", ""));
                    cob.Bit_Saldo = Convert.ToDouble(lblSaldo.Text.Replace("$", "").Replace(",", ""));
                    cob.Id_Cte = Id_Cte;
                    ListBitacora.Add(cob);

                }
                int verificador = 0;

                if (ListBitacora.Count() == 0 && Id_Doc == "-1")
                {
                    Alerta("Por favor seleccione al menos una factura para capturar acciones");
                    return;
                }


                foreach(Bitacora cob in  ListBitacora) {               
                    cn_gestor.InsertarBitacora(cob, list_preg, ref verificador, Emp_CnxCob);                
                }

                


                if (verificador == 0)
                {
                    Alerta("Ocurrio un error al intentar guardar las respuestas");
                }
                else
                {
                    RAM1.ResponseScripts.Add("CloseAndRebind();");
                }

            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
    }
}