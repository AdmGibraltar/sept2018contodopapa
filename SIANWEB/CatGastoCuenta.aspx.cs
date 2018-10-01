using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaEntidad;
using Telerik.Web.UI;
using CapaNegocios;

namespace SIANWEB
{
    public partial class CatGastoCuenta : System.Web.UI.Page
    {
        public bool _PermisoGuardar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public bool _PermisoModificar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public bool _PermisoEliminar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public bool _PermisoImprimir { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Inicializar();
                ValidarPermisos();
                CargarCentros();
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
                //rgAsignacion.Rebind();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "CmbCentro_SelectedIndexChanged1");
            }
        }

        protected void rtb1_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            try
            {
                if (Page.IsValid)
                {
                    ErrorManager();
                    RadToolBarButton btn = e.Item as RadToolBarButton;
                    if (btn.CommandName == "save")
                    {
                        Guardar();
                    }
                    else if (btn.CommandName == "new")
                    {
                        Nuevo();
                    }
                    else if (btn.CommandName == "undo")
                    {
                        //Regresar()
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "rtb1_ButtonClick");
            }
        }

        protected void rgCuenta_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                {
                    rgCuenta.DataSource = GetList();
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void rgCuenta_ItemCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                if (e.CommandName.ToString() == "Modificar")
                {
                    int item = e.Item.ItemIndex;

                    HF_ID.Value = "1";

                    TxtClave.Text = rgCuenta.Items[item]["Id_PagElecCuenta"].Text;
                    TxtDescripcion.Text = rgCuenta.Items[item]["PagElecCuenta_Descripcion"].Text;
                    TxtCC.Text = rgCuenta.Items[item]["PagElecCuenta_CC"].Text;
                    TxtNumero.Text = rgCuenta.Items[item]["PagElecCuenta_Numero"].Text;
                    TxtSubCuenta.Text = rgCuenta.Items[item]["PagElecCuenta_SubCuenta"].Text;
                    TxtSubSubCuenta.Text = rgCuenta.Items[item]["PagElecCuenta_SubSubCuenta"].Text;
                    txtCuentaPago.Text = rgCuenta.Items[item]["PagElecCuenta_CuentaPago"].Text;

                     GridDataItem dataitem = rgCuenta.Items[item];
                     TableCell cell = dataitem["Flete"];
                     CheckBox checkBox = (CheckBox)cell.Controls[0];
                     chkFlete.Checked = checkBox.Checked;

                  
                     TableCell cell3 = dataitem["NoInventariable"];
                     CheckBox checkBox3 = (CheckBox)cell3.Controls[0];
                     chkNoInventariable.Checked = checkBox3.Checked;


                     TableCell cell4 = dataitem["CompraLocal"];
                     CheckBox checkBox4 = (CheckBox)cell4.Controls[0];
                     chkCompraLocal.Checked = checkBox4.Checked;

                     TableCell cellOtros = dataitem["Otros"];
                     CheckBox checkBoxOtros = (CheckBox)cellOtros.Controls[0];
                     chkOtros.Checked = checkBoxOtros.Checked;

                     TableCell cellServicios = dataitem["Servicios"];
                     CheckBox checkBoxServicios = (CheckBox)cellServicios.Controls[0];
                     chkServicios.Checked = checkBoxServicios.Checked;


                     TableCell cellHonorarios = dataitem["Honorarios"];
                     CheckBox checkBoxHonorarios = (CheckBox)cellHonorarios.Controls[0];
                     chkHonorarios.Checked = checkBoxHonorarios.Checked;

                     TableCell cellArrendamientos = dataitem["Arrendamientos"];
                     CheckBox checkBoxArrendamientos = (CheckBox)cellArrendamientos.Controls[0];
                     chkArrendamientos.Checked = checkBoxArrendamientos.Checked;
  
                    TxtClave.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        //JFCV 05 oct 2015 inicio
        //protected void rgCuenta_PageIndexChanged(object source, GridPageChangedEventArgs e)
        //{
        //    try
        //    {
        //        ErrorManager();
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorManager(ex, "rg1_PageIndexChanged");
        //    }
        //}

        //JFCV 05 oct 2015
        protected void rgCuenta_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            try
            {

                rgCuenta.Rebind();

            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        //JFCV 05 oct 2015 Fin


        protected void TxtClave_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (TxtClave.Text.Trim() != string.Empty)
                {
                    CN_CatPagoElectronicoCuenta clsCuenta = new CN_CatPagoElectronicoCuenta();

                    Sesion session = new Sesion();
                    session = (Sesion)Session["Sesion" + Session.SessionID];

                    PagoElectronicoCuenta cuenta = new PagoElectronicoCuenta();
                    cuenta.Id_Emp = session.Id_Emp;
                    cuenta.Id_Cd = session.Id_Cd_Ver;
                    cuenta.Id_PagElecCuenta = Int32.Parse(TxtClave.Text.Trim());

                    clsCuenta.ConsultaCuenta(cuenta, session.Emp_Cnx);

                    TxtDescripcion.Text = cuenta.PagElecCuenta_Descripcion;
                    TxtNumero.Text = cuenta.PagElecCuenta_Numero;
                    TxtCC.Text = cuenta.PagElecCuenta_CC;
                    TxtSubCuenta.Text = cuenta.PagElecCuenta_SubCuenta;
                    TxtSubSubCuenta.Text = cuenta.PagElecCuenta_SubSubCuenta;
                    txtCuentaPago.Text = cuenta.PagElecCuenta_CuentaPago;
                    chkFlete.Checked = cuenta.Flete;
                    chkNoInventariable.Checked = cuenta.NoInventariable;
                    chkCompraLocal.Checked = cuenta.CompraLocal;
                    chkServicios.Checked = cuenta.Servicios;
                    chkOtros.Checked = cuenta.Otros;
                    chkHonorarios.Checked = cuenta.Honorarios;
                    chkArrendamientos.Checked = cuenta.Arrendamientos;

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void RAM1_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
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
                //throw ex;
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
                }
                else
                {
                    Response.Redirect("Inicio.aspx");
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
                //throw ex;
            }
        }

        private void Inicializar()
        {
            rgCuenta.Rebind();
        }

        protected void Guardar()
        {
            Sesion session = new Sesion();
            session = (Sesion)Session["Sesion" + Session.SessionID];

            PagoElectronicoCuenta cuenta = new PagoElectronicoCuenta();

            cuenta.Id_Emp = session.Id_Emp;
            cuenta.Id_Cd = session.Id_Cd_Ver;
            cuenta.Id_PagElecCuenta = Int32.Parse(TxtClave.Text.Trim());
            cuenta.PagElecCuenta_Descripcion = TxtDescripcion.Text.Trim();
            cuenta.PagElecCuenta_CC = TxtCC.Text.Trim();
            cuenta.PagElecCuenta_Numero = TxtNumero.Text.Trim();
            cuenta.PagElecCuenta_SubCuenta = TxtSubCuenta.Text.Trim();
            cuenta.PagElecCuenta_SubSubCuenta = TxtSubSubCuenta.Text.Trim();
            cuenta.PagElecCuenta_CuentaPago = txtCuentaPago.Text.Trim();
            cuenta.Flete = chkFlete.Checked   ;
            cuenta.NoInventariable = chkNoInventariable.Checked ;
            cuenta.CompraLocal = chkCompraLocal.Checked ;
            cuenta.Servicios = chkServicios.Checked ;
            cuenta.Otros = chkOtros.Checked ;
            cuenta.Honorarios = chkHonorarios.Checked ;
            cuenta.Arrendamientos = chkArrendamientos.Checked ;

            int verificador = -1;

            CN_CatPagoElectronicoCuenta clsCuenta = new CN_CatPagoElectronicoCuenta();

            if (HF_ID.Value == "1")
            {
                clsCuenta.ModificarCuenta(cuenta, session.Emp_Cnx, ref verificador);

                if (verificador == 1)
                {
                    Alerta("La cuenta se modifico correctamente.");
                }
            }
            else
            {
                clsCuenta.InsertarCuenta(cuenta, session.Emp_Cnx, ref verificador);

                if (verificador == 1)
                {
                    Alerta("La cuenta se guardo correctamente.");
                }
            }


            rgCuenta.Rebind();

            Nuevo();
        }

        private List<PagoElectronicoCuenta> GetList()
        {
            try
            {
                CN_CatPagoElectronicoCuenta clsCuenta = new CN_CatPagoElectronicoCuenta();
                List<PagoElectronicoCuenta> list = new List<PagoElectronicoCuenta>();

                Sesion session = new Sesion();
                session = (Sesion)Session["Sesion" + Session.SessionID];

                PagoElectronicoCuenta cuenta = new PagoElectronicoCuenta();
                cuenta.Id_Emp = session.Id_Emp;
                cuenta.Id_Cd = session.Id_Cd_Ver;

                clsCuenta.ConsultaCuenta(cuenta, session.Emp_Cnx, ref list);

                return list;
            }
            catch (Exception ex)
            {
                throw ex;
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

        protected void Nuevo()
        {
            HF_ID.Value = "0";
            TxtClave.Enabled = true;

            TxtClave.Text = MaximoId();
            TxtDescripcion.Text = string.Empty;
            TxtCC.Text = string.Empty;
            TxtNumero.Text = string.Empty;
            TxtSubCuenta.Text = string.Empty;
            TxtSubSubCuenta.Text = string.Empty;
            txtCuentaPago.Text = string.Empty;
            chkFlete.Checked = false;
            chkNoInventariable.Checked = false;
            chkCompraLocal.Checked = false;
            chkServicios.Checked = false;
            chkOtros.Checked = false;
            chkHonorarios.Checked = false;
            chkArrendamientos.Checked = false;
          
            
        }

        private string MaximoId()
        {
            try
            {
                Sesion Sesion = new Sesion();

                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                if (Sesion == null)
                    return "";
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                return CN_Comun.Maximo(Sesion.Id_Emp, Sesion.Id_Cd_Ver, "CatPagoElectronicoCuenta", "Id_PagElecCuenta", Sesion.Emp_Cnx, "spCatLocal_Maximo");
            }
            catch (Exception ex)
            {
                throw ex;
            }
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