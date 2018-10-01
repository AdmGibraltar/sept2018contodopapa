using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.IO;
using System.Text;
using System.Configuration;
using CapaEntidad;
using Telerik.Web.UI;
using CapaNegocios;

namespace SIANWEB
{
    public partial class RepPagoElectronico : System.Web.UI.Page
    {
        public bool _PermisoGuardar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public bool _PermisoModificar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public bool _PermisoEliminar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public bool _PermisoImprimir { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }

        protected void Page_Load(object sender, EventArgs e)
        {
            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];
            if (Sesion == null)
            {
                string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);

                Session["dir" + Session.SessionID] = pag[pag.Length - 1]; Response.Redirect("login.aspx", false);
            }
            else
            {
                if (!IsPostBack)
                {
                    Inicializar();
                    ValidarPermisos();
                    
                    CargarCentros();
                    CargarAcreedores();

                }
            }
        }

        protected void CmbCentro_SelectedIndexChanged1(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                Sesion sesion = new Sesion();
                sesion = (Sesion)Session["Sesion" + Session.SessionID];

                if (sesion == null)
                {
                    string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);
                    Session["dir" + Session.SessionID] = pag[pag.Length - 1];
                    Response.Redirect("login.aspx", false);
                }

                CN__Comun comun = new CN__Comun();
                comun.CambiarCdVer(CmbCentro.SelectedItem, ref sesion);
                rgPagoElectronico.Rebind();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "CmbCentro_SelectedIndexChanged1");
            }
        }

        protected void CmbTipo_SelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {


        }

        protected void BtnBuscar_Click(object sender, EventArgs e)
        {
            rgPagoElectronico.Rebind();
        }

        protected void rtb1_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            try
            {
                if (Page.IsValid)
                {
                    this.lblMensaje.Text = string.Empty;
                    RadToolBarButton btn = e.Item as RadToolBarButton;

                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "rtb1_ButtonClick");
            }
        }

        protected void rgPagoElectronico_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                {
                    int? tipo;
                    int? acreedor;
                    //jfcv 20Oct2016 agregue el id pago ellecrónico en los filtros mejoras 2 punto 11
                    int? id_pagoElectronico;

                    if (CmbTipo.SelectedValue == "")
                    {
                        tipo = null;
                    }
                    else
                    {
                        tipo = Int32.Parse(CmbTipo.SelectedValue);
                    }

                    if (CmbAcreedor.SelectedValue == "")
                    {
                        acreedor = null;
                    }
                    else
                    {
                        acreedor = Int32.Parse(CmbAcreedor.SelectedValue);
                    }
                    //jfcv 20Oct2016 agregue el id pago ellecrónico en los filtros mejoras 2 punto 11
                    if (txtidPagoElectronico.Text == "")
                    {
                        id_pagoElectronico = -1;
                    }
                    else
                    {
                        id_pagoElectronico = Int32.Parse(txtidPagoElectronico.Text);
                    }

                    rgPagoElectronico.DataSource = GetList(tipo, acreedor, id_pagoElectronico);
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        //JFCV 19 feb 2016  paginación paginacion para que pagine correctamente
        protected void rgPagoElectronico_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            try
            {

                rgPagoElectronico.Rebind();

            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void rgPagoElectronico_ItemCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                Int32 item = default(Int32);
                item = e.Item.ItemIndex;
                if (item == -1)
                {
                    item = 0;
                }


                int Id_PagElec = Convert.ToInt32(rgPagoElectronico.Items[item]["Id_PagElec"].Text);

                switch (e.CommandName.ToString())
                {

                    case "Comprobantes":
                        RAM1.ResponseScripts.Add(string.Concat(@"AbrirVentana_LstComprobantes('", Id_PagElec, "')"));
                        break;

                    case "Consultar":
                            RAM1.ResponseScripts.Add("return AbrirVentana_GastosConsultar(" + Id_PagElec.ToString() + ")");
                            break;
                }

            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void RAM1_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            rgPagoElectronico.Rebind();
        }

        private void Inicializar()
        {
            rgPagoElectronico.Rebind();
        }

        protected void CargarTipos()
        {
            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];

            CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
            CN_Comun.LlenaCombo(1, Sesion.Id_Emp, Sesion.Id_Cd_Ver, Sesion.Emp_Cnx, "spCatPagoElectronicoTipo_Combo", ref CmbTipo);
        }


        private void CargarCentros()
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();


                if (Sesion.U_MultiOfi == false)
                {
                    CN_Comun.LlenaCombo(2, Sesion.Id_Emp, Sesion.Id_U, Sesion.Emp_Cnx, "spCatCentroDistribucion_Combo", ref CmbCentro);
                    this.CmbCentro.Visible = false;
                    this.TblEncabezado.Rows[0].Cells[2].InnerText = " " + CmbCentro.FindItemByValue(Sesion.Id_Cd_Ver.ToString()).Text;

                }
                else
                {
                    CN_Comun.LlenaCombo(1, Sesion.Id_Emp, Sesion.Id_U, Sesion.Id_Cd_Ver, Sesion.Id_Cd, Sesion.Emp_Cnx, "spCatCentroDistribucion_Combo", ref CmbCentro);
                    this.CmbCentro.SelectedValue = Sesion.Id_Cd_Ver.ToString();
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
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
                if (pag.Length > 1)
                {
                    pagina.Url = (new System.IO.FileInfo(Page.Request.Url.AbsolutePath)).Name + "?" + pag[1];
                }
                else
                {
                    pagina.Url = (new System.IO.FileInfo(Page.Request.Url.AbsolutePath)).Name;
                }

                CN_Pagina CapaNegocio = new CN_Pagina();
                CapaNegocio.PaginaConsultar(ref pagina, Sesion.Emp_Cnx);

                Session["Head" + Session.SessionID] = pagina.Path;
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
                        this.rtb1.Items[6].Visible = false;
                    }
                    if (Permiso.PGrabar == false && Permiso.PModificar == false)
                    {
                        this.rtb1.Items[5].Visible = false;
                    }
                    this.rtb1.Items[4].Visible = false;
                    //Eliminar
                    this.rtb1.Items[3].Visible = false;
                    //Imprimir
                    this.rtb1.Items[2].Visible = false;
                    //Correo
                    this.rtb1.Items[1].Visible = false;

                    rtb1.Visible = false;
                }
                else
                {
                    Response.Redirect("Inicio.aspx");
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }


        private List<PagoElectronico> GetList()
        {
            try
            {
                CN_CapPagoElectronico clsPagoElectronico = new CN_CapPagoElectronico();
                List<PagoElectronico> list = new List<PagoElectronico>();

                Sesion session = new Sesion();
                session = (Sesion)Session["Sesion" + Session.SessionID];

                PagoElectronico pagoElectronico = new PagoElectronico();
                pagoElectronico.Id_Emp = session.Id_Emp;
                pagoElectronico.Id_Cd = session.Id_Cd_Ver;

                clsPagoElectronico.ConsultaPagoElectronico(pagoElectronico, session.Emp_Cnx, ref list);

                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private List<PagoElectronico> GetList(int? tipo, int? acreedor, int? id_pagoelectronico)
        {
            try
            {
                //jfcv 20Oct2016 agregue el id pago ellecrónico en los filtros mejoras 2 punto 11
                CN_CapPagoElectronico clsPagoElectronico = new CN_CapPagoElectronico();
                List<PagoElectronico> list = new List<PagoElectronico>();

                Sesion session = new Sesion();
                session = (Sesion)Session["Sesion" + Session.SessionID];

                PagoElectronico pagoElectronico = new PagoElectronico();
                pagoElectronico.Id_Emp = session.Id_Emp;
                pagoElectronico.Id_Cd = session.Id_Cd_Ver;
                pagoElectronico.Id_Acr_Filtro = acreedor;
                pagoElectronico.Id_PagElecTipo_Filtro = tipo;
                pagoElectronico.Id_PagElecCuenta_Filtro = -1;
                //jfcv 20Oct2016 agregue el id pago ellecrónico en los filtros mejoras 2 punto 11
                pagoElectronico.Id_PagElec = Convert.ToInt32(id_pagoelectronico);
                pagoElectronico.Id_PagElecEstatus_Filtro = (CmbEstatus.SelectedValue == "" ? -1 : Int32.Parse(CmbEstatus.SelectedValue));


                clsPagoElectronico.ConsultaPagoElectronicoAdmin(pagoElectronico, session.Emp_Cnx, ref list);


                return list;
                 

                //    int idGVEst = Int32.Parse(CmbEstatus.SelectedValue);

                //    if (idGVEst != 0)
                //    {
                //        //Si el gasto de viaje tiene estatus 3 es que esta ya comprobado
                //        if (idGVEst == 6)
                //            idGVEst = 3;
                //        else
                //        {
                //            if (idGVEst == 3)
                //            { idGVEst = 6; }
                //        }
                //        return list.Where(x => x.Id_PagElecEstatus == idGVEst).ToList();
                //    }
                
                 


                //if (CmbEstatus.SelectedValue == "")
                //{
                //    return list;
                //}
                //else
                //{
                //    bool autorizado = Boolean.Parse(CmbEstatus.SelectedValue);
                //    return list.Where(x => x.PagElec_Autorizado == autorizado).ToList();
                //}
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void CargarAcreedores()
        {
            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];

            CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
            CN_Comun.LlenaCombo(1, Sesion.Id_Emp, Sesion.Id_Cd_Ver, Sesion.Emp_Cnx, "spCatAcreedor_Combo", ref CmbAcreedor);
        }

        private void Alerta(string mensaje)
        {
            try
            {
                RAM1.ResponseScripts.Add("radalert('" + mensaje + "', 330, 150);");
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "Alerta");
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
    }
}