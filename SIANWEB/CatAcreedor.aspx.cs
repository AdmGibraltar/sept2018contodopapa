using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaEntidad;
using Telerik.Web.UI;
using CapaNegocios;
using CapaDatos;
using System.Collections;
using System.Text;
using System.Net.Mail;
using System.Net;
using System.Configuration;

namespace SIANWEB
{
    public partial class CatAcredor : System.Web.UI.Page
    {
        public bool _PermisoGuardar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public bool _PermisoModificar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public bool _PermisoEliminar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public bool _PermisoImprimir { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private string rfcGenerico = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];
            if (Sesion == null)
            {
                string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);

                Session["dir" + Session.SessionID] = pag[pag.Length - 1]; Response.Redirect("login.aspx", false);

                //string str = Context.Items["href"].ToString();
                //string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);Context.Items.Add("href", pag[pag.Length-1]);Session["dir" + Session.SessionID] = pag[pag.Length - 1];                    Response.Redirect("login.aspx" , false);
            }
            else
            {
                if (!IsPostBack)
                {
                    ValidarPermisos();
                    CargarCentros();
                    Inicializar();

                }
            }
            //JFCV se obtiene el rfc generico ( para las validaciones de rfc ) 
            rfcGenerico = ConfigurationManager.AppSettings["gastosrfcGenerico"].ToString();
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
                // que no valide la pagina al darle botón de nuevo , solo al guardar
                //if (Page.IsValid)
                //{
                    ErrorManager();
                    RadToolBarButton btn = e.Item as RadToolBarButton;
                    if (btn.CommandName == "save")
                    {
                        if (Page.IsValid)
                        {
                            Guardar();
                        }
                        else
                        {
                            Alerta("Algunos campos no están correctos.");
                        }
                    }
                    else if (btn.CommandName == "new")
                    {
                        Nuevo();
                    }
                    else if (btn.CommandName == "undo")
                    {
                        //Regresar()
                    }
               // }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "rtb1_ButtonClick");
            }
        }

        protected void txtClave_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtClave.Text.Trim() != string.Empty)
                {
                    CN_CatAcreedor clsAcreedor = new CN_CatAcreedor();

                    Sesion session = new Sesion();
                    session = (Sesion)Session["Sesion" + Session.SessionID];

                    Acreedor acreedor = new Acreedor();
                    acreedor.Id_Emp = session.Id_Emp;
                    acreedor.Id_Cd = session.Id_Cd_Ver;
                    acreedor.Id_Acr = Int32.Parse(txtClave.Text.Trim());

                    clsAcreedor.ConsultaAcreedor(acreedor, session.Emp_Cnx);

                    //txtClave.Text = acreedor.Id_Acr.to;
                    TxtNombre.Text = acreedor.Acr_Nombre;
                    TxtCalle.Text = acreedor.Acr_Calle;
                    TxtNumero.Text = acreedor.Acr_Numero;
                    TxtNumeroInterior.Text = acreedor.Acr_NumInterior;
                    TxtCp.Text = acreedor.Acr_CP;
                    TxtColonia.Text = acreedor.Acr_Colonia;
                    TxtMunicipio.Text = acreedor.Acr_Municipio;
                    TxtEstado.Text = acreedor.Acr_Estado;
                    TxtRfc.Text = acreedor.Acr_RFC;
                    TxtCorreo.Text = acreedor.Acr_Correo;
                    TxtTelefono.Text = acreedor.Acr_Telefono;
                    TxtColonia.Text = acreedor.Acr_Colonia;
                   
                    TxtBanco.Text = acreedor.Acr_Banco;
                    TxtCuentaClabe.Text = acreedor.Acr_Cuenta;
                    TxtCondPago.Text = acreedor.Acr_CondPago.ToString();
                    //TxtContacto.Text = acreedor.Acr_Contacto;
                     
                    rdActivo.Checked = acreedor.Acr_Estatus;
                    rdActivo.Checked = acreedor.Acr_Autorizado;

                    this.cmbTipo.Text = (this.cmbTipo.FindItemByValue(acreedor.Acr_Tipo.ToString())).Text;
                    this.cmbTipo.SelectedValue = acreedor.Acr_Tipo.ToString();
                    HF_ID.Value = "1";


                }
                 
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void rgAcreedor_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                {
                    rgAcreedor.DataSource = GetList();
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void rgAcreedor_ItemCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                if (e.CommandName.ToString() == "Modificar")
                {
                    int item = e.Item.ItemIndex;

                    HF_ID.Value = "1";

                    txtClave.Text = rgAcreedor.Items[item]["Id_Acr"].Text;

                    if (rgAcreedor.Items[item]["Acr_Nombre"].Text == "&nbsp;")
                        TxtNombre.Text = "";
                    else
                        TxtNombre.Text = rgAcreedor.Items[item]["Acr_Nombre"].Text;

                    if (rgAcreedor.Items[item]["Acr_Calle"].Text == "&nbsp;")
                        TxtCalle.Text = "";
                    else
                        TxtCalle.Text = rgAcreedor.Items[item]["Acr_Calle"].Text;

                    if (rgAcreedor.Items[item]["Acr_Numero"].Text == "&nbsp;")
                        TxtNumero.Text = "";
                    else
                        TxtNumero.Text = rgAcreedor.Items[item]["Acr_Numero"].Text;

                    if (rgAcreedor.Items[item]["Acr_NumInterior"].Text == "&nbsp;")
                        TxtNumeroInterior.Text = "";
                    else
                        TxtNumeroInterior.Text = rgAcreedor.Items[item]["Acr_NumInterior"].Text;

                    if (rgAcreedor.Items[item]["Acr_CP"].Text == "&nbsp;")
                        TxtCp.Text = "";
                    else
                        TxtCp.Text = rgAcreedor.Items[item]["Acr_CP"].Text;


                    if (rgAcreedor.Items[item]["Acr_Colonia"].Text == "&nbsp;")
                        TxtColonia.Text = "";
                    else
                        TxtColonia.Text = rgAcreedor.Items[item]["Acr_Colonia"].Text;

                    if (rgAcreedor.Items[item]["Acr_Municipio"].Text == "&nbsp;")
                        TxtMunicipio.Text = "";
                    else
                        TxtMunicipio.Text = rgAcreedor.Items[item]["Acr_Municipio"].Text;

                    if (rgAcreedor.Items[item]["Acr_Estado"].Text == "&nbsp;")
                        TxtEstado.Text = "";
                    else
                        TxtEstado.Text = rgAcreedor.Items[item]["Acr_Estado"].Text;

                    if (rgAcreedor.Items[item]["Acr_RFC"].Text == "&nbsp;")
                        TxtRfc.Text = "";
                    else
                        TxtRfc.Text = rgAcreedor.Items[item]["Acr_RFC"].Text;

                    if (rgAcreedor.Items[item]["Acr_Telefono"].Text == "&nbsp;")
                        TxtTelefono.Text = "";
                    else
                        TxtTelefono.Text = rgAcreedor.Items[item]["Acr_Telefono"].Text;

                    if (rgAcreedor.Items[item]["Acr_Correo"].Text == "&nbsp;" || rgAcreedor.Items[item]["Acr_Correo"].Text == "NULL")
                        TxtCorreo.Text = "";
                    else
                        TxtCorreo.Text = rgAcreedor.Items[item]["Acr_Correo"].Text;


                    GridDataItem dataitem = rgAcreedor.Items[item];
                    TableCell cell = dataitem["Acr_Estatus"];
                    CheckBox checkBox = (CheckBox)cell.Controls[0];
                     rdActivo.Checked = checkBox.Checked;

                     TableCell cellautorizado = dataitem["Acr_Autorizado"];
                     CheckBox checkBoxAutorizado = (CheckBox)cellautorizado.Controls[0];
   
                     lblAutorizado.Text = rgAcreedor.Items[item]["Acr_EstatusDescripcion"].Text;

                     if (rdActivo.Checked == true)
                     {
                         rdActivo.Text = "Presione para Cancelar Acreedor/proveedor";
                         rdActivo.Text = "Desactive la casilla si desea Cancelar al Acreedor/proveedor";
                     }
                     else
                     {
                         rdActivo.Text = "Presione para Activar";
                         rdActivo.ToolTip = "Active la casilla si desea reactivarlo.";
                     }
                     //if (Convert.ToBoolean(rgAcreedor.Items[item]["Acr_Autorizado"].Text) == true )
                     //if (checkBoxAutorizado.Checked == true)
                     //{
                     //    lblAutorizado.Text = "Autorizado";
                     //}
                     //else
                     //{
                     //    lblAutorizado.Text = "";
                     //}
 

                    //if (rgAcreedor.Items[item]["Acr_Contacto"].Text == "&nbsp;")
                    //    TxtContacto.Text = "";
                    //else
                    //    TxtContacto.Text = rgAcreedor.Items[item]["Acr_Contacto"].Text;

                    if (rgAcreedor.Items[item]["Acr_CondPago"].Text == "&nbsp;")
                        TxtCondPago.Text = "";
                    else
                        TxtCondPago.Text = rgAcreedor.Items[item]["Acr_CondPago"].Text;

                     //Agregar Banco y Cuenta Bancaria 
                    if (rgAcreedor.Items[item]["Acr_Banco"].Text == "&nbsp;")
                        TxtBanco.Text = "";
                    else
                        TxtBanco.Text = rgAcreedor.Items[item]["Acr_Banco"].Text;

                    if (rgAcreedor.Items[item]["Acr_Cuenta"].Text == "&nbsp;")
                        TxtCuentaClabe.Text = "";
                    else
                        TxtCuentaClabe.Text = rgAcreedor.Items[item]["Acr_Cuenta"].Text;


                    cmbTipo.SelectedValue = rgAcreedor.Items[item]["Acr_Tipo"].Text;

                    txtClave.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        ////JFCV 06 oct 2015 Inicio PageIndex
        ////protected void rgAcreedor_PageIndexChanged(object source, GridPageChangedEventArgs e)
        ////{
        ////    try
        ////    {
        ////        ErrorManager();
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        ErrorManager(ex, "rg1_PageIndexChanged");
        ////    }
        ////}

        protected void rgAcreedor_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            try
            {

                rgAcreedor.Rebind();

            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        //JFCV 06 oct 2015 Fin

        protected void TxtRfc_TextChanged(object sender, System.EventArgs e)
        {

            Sesion session = (Sesion)Session["Sesion" + Session.SessionID];

            if ((new CN_CatAcreedor().ValidaRFC(Convert.ToInt32(txtClave.Text), (sender as RadTextBox).Text.Trim(), session.Emp_Cnx)))
            {
                //JFCV se obtiene el rfc generico ( para las validaciones de rfc ) 
                if (rfcGenerico != TxtRfc.Text)
                {
                    Alerta("El RFC ya existe en otro registro");
                    (sender as RadTextBox).Focus();
                }
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
                    //JFCV 2 oct 2015 el botón de nuevo que se active para cuando no quieran editar le pueden dar nuevo para agregar uno mas 
                    this.rtb1.Items[6].Visible = true;

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
            txtClave.Text = MaximoId();
            rgAcreedor.Rebind();
        }

        protected void Guardar()
        {
            Sesion session = new Sesion();
            session = (Sesion)Session["Sesion" + Session.SessionID];

            if ((new CN_CatAcreedor().ValidaRFC(Convert.ToInt32(txtClave.Text), TxtRfc.Text.Trim(), session.Emp_Cnx)))
            {
                 //JFCV se obtiene el rfc generico ( para las validaciones de rfc ) 
                if (rfcGenerico != TxtRfc.Text)
                {
                    Alerta("El RFC ya existe en otro registro");
                    TxtRfc.Focus();
                    return;
                }
            }


            if (TxtCuentaClabe.Text.Trim().Length < 18 )
            {
              
                    Alerta("La cuenta contable debe tener 18 caracteres Númericos");
                    TxtRfc.Focus();
                    return;
               
            }

            Acreedor acreedor = new Acreedor();

            acreedor.Id_Emp = session.Id_Emp;
            acreedor.Id_Cd = session.Id_Cd_Ver;
            acreedor.Id_Acr = Int32.Parse(txtClave.Text.Trim());
            acreedor.Acr_Tipo = Int32.Parse(cmbTipo.SelectedValue);
            acreedor.Acr_Nombre = TxtNombre.Text.Trim();
            acreedor.Acr_Calle = TxtCalle.Text.Trim();
            acreedor.Acr_Numero = TxtNumero.Text.Trim();
            acreedor.Acr_NumInterior = TxtNumeroInterior.Text.Trim();
            acreedor.Acr_CP = TxtCp.Text.Trim();
            acreedor.Acr_Colonia = TxtColonia.Text.Trim();
            acreedor.Acr_Municipio = TxtMunicipio.Text.Trim();
            acreedor.Acr_Estado = TxtEstado.Text.Trim();
            acreedor.Acr_RFC = TxtRfc.Text.Trim();
            acreedor.Acr_Telefono = TxtTelefono.Text.Trim();
            acreedor.Acr_Correo = TxtCorreo.Text.Trim();
            //acreedor.Acr_Contacto = TxtContacto.Text.Trim();
            acreedor.Acr_Estatus = rdActivo.Checked;
            
            acreedor.Acr_CondPago = Int32.Parse(TxtCondPago.Text.Trim());
            acreedor.Acr_Banco =  TxtBanco.Text.Trim();
            acreedor.Acr_Cuenta = TxtCuentaClabe.Text.Trim();

            int verificador = -1;

            CN_CatAcreedor clsAcreedor = new CN_CatAcreedor();

            if (HF_ID.Value == "1")
            {
                clsAcreedor.ModificarAcreedor(acreedor, session.Emp_Cnx, ref verificador);
                //JFCV 02 oct 2015 cuando grabe Que se ponga en cero para la edición 
                HF_ID.Value = "0";
            }
            else
            {
                clsAcreedor.InsertarAcreedor(acreedor, session.Emp_Cnx, ref verificador);
                try
                {
                    EnviarCorreo();
                }
                catch (Exception ex)
                {
                    Alerta("El proveedor se creo, pero fue imposible enviar el correo al autorizador.");
                }
            }


            Nuevo();

            rgAcreedor.Rebind();

           
        }

        protected void Nuevo()
        {
            txtClave.Enabled = true;

            txtClave.Text = MaximoId();
            cmbTipo.SelectedValue = "0";
            TxtNombre.Text = string.Empty;
            TxtCalle.Text = string.Empty;
            TxtNumero.Text = string.Empty;
            TxtNumeroInterior.Text = string.Empty;
            TxtCp.Text = string.Empty;
            TxtColonia.Text = string.Empty;
            TxtMunicipio.Text = string.Empty;
            TxtEstado.Text = string.Empty;
            TxtRfc.Text = string.Empty;
            TxtTelefono.Text = string.Empty;
            TxtCorreo.Text = string.Empty;
            //TxtContacto.Text = string.Empty;
            rdActivo.Checked = true;
            rdActivo.Text = "";
            rdActivo.ToolTip = "";
            lblAutorizado.Text = "";
            TxtCondPago.Text = string.Empty;
            TxtBanco.Text = string.Empty;
            TxtCuentaClabe.Text = string.Empty;
            //JFCV 02 oct 2015 cuando le de nuevo esta variable toma valor cero 
            HF_ID.Value = "0";
        }

        private List<Acreedor> GetList()
        {
            try
            {
                CN_CatAcreedor clsAcreedor = new CN_CatAcreedor();
                List<Acreedor> list = new List<Acreedor>();

                Sesion session = new Sesion();
                session = (Sesion)Session["Sesion" + Session.SessionID];

                Acreedor acreedor = new Acreedor();
                acreedor.Id_Emp = session.Id_Emp;
                acreedor.Id_Cd = session.Id_Cd_Ver;
                acreedor.Acr_Nombre = TxtNombre.Text;

                clsAcreedor.ConsultaAcreedor(acreedor, session.Emp_Cnx, ref list);

                return list;
            }
            catch(Exception ex)
            {
                throw ex;
            }
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
                return CN_Comun.Maximo(Sesion.Id_Emp, Sesion.Id_Cd_Ver, "CatAcreedor", "Id_Acr", Sesion.Emp_Cnx, "spCatLocal_Maximo");
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

        private void EnviarCorreo()
        {
            Sesion session = new Sesion();
            session = (Sesion)Session["Sesion" + Session.SessionID];

            ConfiguracionGlobal configuracion = new ConfiguracionGlobal();
            configuracion.Id_Cd = session.Id_Cd_Ver;
            configuracion.Id_Emp = session.Id_Emp;
            CN_Configuracion cn_configuracion = new CN_Configuracion();
            cn_configuracion.Consulta(ref configuracion, session.Emp_Cnx);

            StringBuilder cuerpo_correo = new StringBuilder();
            cuerpo_correo.Append("<table>");
            cuerpo_correo.Append("<tr>");
            cuerpo_correo.Append("<td> La sucursal {Cd_Nombre} solicita la autorización para el alta del {cmbTipo}: {TxtNombre}</td>");
            cuerpo_correo.Append("</tr>");
            cuerpo_correo.Append("<tr>");
            cuerpo_correo.Append("<td><br></td>");
            cuerpo_correo.Append("</tr>");
            cuerpo_correo.Append("<tr>");
            cuerpo_correo.Append("<td>Accesar a : <a href='{href}'>Autorización de {cmbTipo}</a></td>");
            cuerpo_correo.Append("</tr>");
            cuerpo_correo.Append("<tr>");
            cuerpo_correo.Append("<td>&nbsp;</td>");
            cuerpo_correo.Append("</tr>");
            cuerpo_correo.Append("</table>");

            string strUrl = (HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + HttpContext.Current.Request.ApplicationPath + "/").Replace("//", "/").Replace("http:/", "http://");
            string txtCuerpoMail = cuerpo_correo.ToString();
            txtCuerpoMail = txtCuerpoMail.Replace("{Cd_Nombre}", session.Cd_Nombre);
            txtCuerpoMail = txtCuerpoMail.Replace("{cmbTipo}", (cmbTipo.SelectedValue.Trim() == "1" ? "Proveedor" : "Acreedor"));
            txtCuerpoMail = txtCuerpoMail.Replace("{TxtNombre}", TxtNombre.Text);
            txtCuerpoMail = txtCuerpoMail.Replace("{href}", strUrl + "ProAcreedor_Autorizacion.aspx");


            SmtpClient smtp = new SmtpClient();
            smtp.Host = configuracion.Mail_Servidor;
            smtp.Port = Convert.ToInt32(configuracion.Mail_Puerto);
            smtp.Credentials = new NetworkCredential(configuracion.Mail_Usuario, configuracion.Mail_Contraseña);

            MailAddress from = new MailAddress(configuracion.Mail_Remitente);
            MailMessage mail = new MailMessage();

            mail.From = from;
            mail.IsBodyHtml = true;
            mail.BodyEncoding = System.Text.Encoding.UTF8;
            mail.Subject = "Autorización de " + (cmbTipo.SelectedValue.Trim() == "1" ? "Proveedor" : "Acreedor");
            mail.To.Add(
                    (cmbTipo.SelectedValue.Trim() == "1") ? 
                        new MailAddress(configuracion.Mail_GastosProveedores) : 
                        new MailAddress(configuracion.Mail_GastosAcreedores)
            );

            mail.Body = txtCuerpoMail;
            smtp.Send(mail);

            /*
            SmtpClient sm = new SmtpClient(configuracion.Mail_Servidor, Convert.ToInt32(configuracion.Mail_Puerto));
            sm.Credentials = new NetworkCredential(configuracion.Mail_Usuario, configuracion.Mail_Contraseña);
            //sm.EnableSsl = true;
            MailMessage m = new MailMessage();
            m.From = new MailAddress(configuracion.Mail_Remitente);
            if (cmbTipo.SelectedValue.Trim() == "1")
            {
                m.To.Add(new MailAddress(configuracion.Mail_GastosProveedores));
            }
            else
            {
                m.To.Add(new MailAddress(configuracion.Mail_GastosAcreedores));
            }

            m.Subject = "Autorización de " + (cmbTipo.SelectedValue.Trim() == "1"?"Proveedor":"Acreedor");
            m.IsBodyHtml = true;
            m.Body = txtCuerpoMail;
            sm.Send(m);
            */
        }


        protected void imgBuscar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                CN_CatAcreedor clsAcreedor = new CN_CatAcreedor();
                List<Acreedor> list = new List<Acreedor>();

                Sesion session = new Sesion();
                session = (Sesion)Session["Sesion" + Session.SessionID];

                Acreedor acreedor = new Acreedor();
                acreedor.Id_Emp = session.Id_Emp;
                acreedor.Id_Cd = session.Id_Cd_Ver;
                acreedor.Acr_Nombre = TxtNombre.Text;

                clsAcreedor.ConsultaAcreedor(acreedor, session.Emp_Cnx, ref list);
                rgAcreedor.DataSource = list;
                rgAcreedor.DataBind();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

    }
}