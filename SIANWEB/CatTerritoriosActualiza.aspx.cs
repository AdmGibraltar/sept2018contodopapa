using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaEntidad;
using Telerik.Web.UI;
using CapaNegocios;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Xml;
using System.ComponentModel;

namespace SIANWEB
{
    public partial class CatTerritoriosActualiza : System.Web.UI.Page
    {

        #region Variables
        public string strEmp = System.Configuration.ConfigurationManager.AppSettings["VGEmpresa"];
        private bool _PermisoGuardar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoModificar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoEliminar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoImprimir { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        //static DataTable dt;
        public bool Tipoterr = false;
        private DataTable dt { get { return (DataTable)Session["dt" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["dt" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public DataTable TablaPermisosUEN
        {
            get
            {
                return (Session["TablaPermisosUEN" + Session.SessionID] as DataTable);
            }
            set
            {
                Session["TablaPermisosUEN" + Session.SessionID] = value;
            }
        }

        //public string Valor
        //{
        //    get
        //    {
        //        return claveTerritorio();
        //    }
        //    set { }
        //}

        bool terr = false;
        bool Seg = false;
        
        bool tipoRep = false;
        bool Uen = false;
        bool Rik = false;
        bool tipoCli = false;

        //public string Valor
        //{
        //    get
        //    {
        //        return MaximoId();
        //    }
        //    set { }
        //}
        private string Emp_CnxCob
        {
            get { return ConfigurationManager.AppSettings.Get("strConnectionCobranza"); }
        }

        private string Emp_CnxCen
        {
            get { return ConfigurationManager.AppSettings.Get("strConnectionCentral"); }
        }

        #endregion
        #region Eventos

        protected void Page_Init(object sender, EventArgs e)
        {
            Sesion session = new Sesion();
            session = (Sesion)Session["Sesion" + Session.SessionID];
            if (session == null)
            {
                string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);
                Session["dir" + Session.SessionID] = pag[pag.Length - 1];
                Response.Redirect("login.aspx", false);
            }
            //else
            //{
            //    CargarClientes();
            //}
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                if (Sesion == null)
                {
                    string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);
                    Session["dir" + Session.SessionID] = pag[pag.Length - 1];
                    Response.Redirect("login.aspx", false);
                }
                else
                {
                    if (!Page.IsPostBack)
                    {
                        ValidarPermisos();
                        if (Sesion.Cu_Modif_Pass_Voluntario == false)
                        {
                            RAM1.ResponseScripts.Add("AbrirContrasenas(); return false;");
                            return;
                        }

                        CargarCentros();
                        Inicializar();
                    }
                }
            }
            catch (Exception ex)
            {
                Alerta(ex.Message);
            }
        }
        protected void CmbCentro_SelectedIndexChanged1(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                Sesion sesion = new Sesion(); sesion = (Sesion)Session["Sesion" + Session.SessionID]; if (sesion == null) { string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries); Session["dir" + Session.SessionID] = pag[pag.Length - 1]; Response.Redirect("login.aspx", false); } CN__Comun comun = new CN__Comun(); comun.CambiarCdVer(CmbCentro.SelectedItem, ref sesion);
                Inicializar();
            }
            catch (Exception ex)
            {
                Alerta(ex.Message);
            }
        }
        protected void rtb1_ButtonClick(object sender, Telerik.Web.UI.RadToolBarEventArgs e)
        {
            try
            {
                ErrorManager();
                RadToolBarButton btn = e.Item as RadToolBarButton;
                if (btn.CommandName == "save")
                {
                    if (Page.IsValid) { }
                    Guardar();
                }
                else if (btn.CommandName == "new")
                {
                    //Nuevo();
                }
                else if (btn.CommandName == "undo")
                {
                    //Regresar()
                }
            }
            catch (Exception ex)
            {
                Alerta(ex.Message);
            }
        }

        protected void cmbCliente_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
        }
        protected void rgTerritorios_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                {
                    double ancho = 0;
                    foreach (GridColumn gc in rgTerritorios.Columns)
                    {
                        if (gc.Display)
                        {
                            ancho = ancho + gc.HeaderStyle.Width.Value;
                        }
                    }
                    int extra = 0;
                    if (dt.Rows.Count > 11)
                    {
                        extra = 20;
                    }
                    rgTerritorios.Width = Unit.Pixel(Convert.ToInt32(ancho) + extra);
                    rgTerritorios.MasterTableView.Width = Unit.Pixel(Convert.ToInt32(ancho));
                    rgTerritorios.DataSource = dt;

                }
            }
            catch (Exception ex)
            {
                Alerta(ex.Message);
            }
        }
        public DataTable ConvertToDataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection properties =
               TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;

        }
        protected void rgTerritorios_ItemCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                switch (e.CommandName)
                {
                    case "Edit":
                            GridItem gi = e.Item;
                            string idTerAnterior = (gi.FindControl("LblIdTerAnt") as Label).Text;
                            string idTipoRepresentante = (gi.FindControl("LTipoRep") as Label).Text;

                            if (idTerAnterior != "0")
                            {
                                e.Canceled = true;
                                Alerta("El registro ya no puede ser modificado");
                            }
                            else
                            {
                                Edit(e);
                            }

                        break;

                    case "Update":
                        Update(e);
                        break;
                }
            }
            catch (Exception ex)
            {
                Alerta(ex.Message);
            }
        }


       //// 1.-Llenamos la lista de territorios.
       //// 2.-Seleccionamos el tipo de Representante que tendra el territorio.
       
       //// 3.- Si es Rik 
       
       //// - Cargamos UEN.
       //// a) Seleccionamos el UEN.
       //// - Cargamos Segmento.
       //// b) Seleccionamos el Segmento.
       //// c) Seleccionamos el Representante.
       //// e) Seleccionamos el Tipo Cliente.
       //// f) Seleccionamos el Id Local.

       //// 4.- Si es Asesor o RSC
       //// a) Seleccionamos el Representante.
       //// b) Seleccionamos el Tipo Cliente.
       //// c) Seleccionamos el Id Local.

       //--------------------------------------------------------------------------------
       // Tipo Representante
       //--------------------------------------------------------------------------------

        protected void TipoRep_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            RadComboBox RadCombo = (sender as RadComboBox);
            RadCombo.SelectedIndex = RadCombo.FindItemIndexByValue((RadCombo.Parent.FindControl("txtId_TipoRep") as RadNumericTextBox).Text);
        }
        private void CargarTipoRepDet(RadComboBox comboBox)
        {
            try
            {
                if (Tipoterr)
                    return;
                Tipoterr = true;

                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CN_Comun.LlenaCombo(1, Sesion.Id_Emp, Sesion.Emp_Cnx, "spCatTipoRepresentante_Combo", ref comboBox);

            }
            catch (Exception ex)
            {
                Alerta(ex.Message);
            }
        }
        protected void cmbTipoRep_DataBinding(object sender, EventArgs e)
        {
            try
            {
                RadComboBox cmb = sender as RadComboBox;
                CargarTipoRepDet(cmb);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void cmbTipoRep_TextChanged(object sender, EventArgs e)
        {
            RadComboBox RadCombo = (sender as RadComboBox);
            (RadCombo.Parent.Parent.FindControl("txtId_TipoRep") as RadNumericTextBox).Value = Convert.ToInt16((((sender as RadComboBox).Parent.Parent.FindControl("cmbTipoRep") as RadComboBox).SelectedValue));

            (RadCombo.Parent.Parent.FindControl("cmbUen") as RadComboBox).SelectedIndex = 0;
            (RadCombo.Parent.Parent.FindControl("cmbUen") as RadComboBox).Text = (RadCombo.Parent.Parent.FindControl("cmbUen") as RadComboBox).Items[0].Text;
            (RadCombo.Parent.Parent.FindControl("cmbUen") as RadComboBox).Text = "";

            (RadCombo.Parent.Parent.FindControl("cmbSeg") as RadComboBox).SelectedIndex = 0;
            (RadCombo.Parent.Parent.FindControl("cmbSeg") as RadComboBox).Text = (RadCombo.Parent.Parent.FindControl("cmbSeg") as RadComboBox).Items[0].Text;
            (RadCombo.Parent.Parent.FindControl("cmbSeg") as RadComboBox).Text = "";

            (RadCombo.Parent.Parent.FindControl("cmbRik") as RadComboBox).SelectedIndex = 0;
            (RadCombo.Parent.Parent.FindControl("cmbRik") as RadComboBox).Text = (RadCombo.Parent.Parent.FindControl("cmbRik") as RadComboBox).Items[0].Text;
            (RadCombo.Parent.Parent.FindControl("cmbRik") as RadComboBox).Text = "";

            (RadCombo.Parent.Parent.FindControl("cmbTipoCli") as RadComboBox).SelectedIndex = 0;
            (RadCombo.Parent.Parent.FindControl("cmbTipoCli") as RadComboBox).Text = (RadCombo.Parent.Parent.FindControl("cmbTipoCli") as RadComboBox).Items[0].Text;
            (RadCombo.Parent.Parent.FindControl("cmbTipoCli") as RadComboBox).Text = "";

            (RadCombo.Parent.Parent.FindControl("cmbUen") as RadComboBox).Visible = false;
            (RadCombo.Parent.Parent.FindControl("cmbSeg") as RadComboBox).Visible = false;

            (RadCombo.Parent.Parent.FindControl("txtId_Id_Uen") as RadNumericTextBox).Value = null;
            (RadCombo.Parent.Parent.FindControl("txtId_Id_Seg") as RadNumericTextBox).Value = null;
            (RadCombo.Parent.Parent.FindControl("txtId_Id_Rik") as RadNumericTextBox).Value = null;
            (RadCombo.Parent.Parent.FindControl("txtId_Id_TipoCliente") as RadNumericTextBox).Value = null;



            if (Convert.ToInt16((((sender as RadComboBox).Parent.Parent.FindControl("cmbTipoRep") as RadComboBox).SelectedValue))==3)
            {
                (RadCombo.Parent.Parent.FindControl("cmbUen") as RadComboBox).Visible = true;
                (RadCombo.Parent.Parent.FindControl("cmbSeg") as RadComboBox).Visible = true;
                (RadCombo.Parent.Parent.FindControl("cmbUen") as RadComboBox).Enabled = true;
                (RadCombo.Parent.Parent.FindControl("cmbSeg") as RadComboBox).Enabled = true;

                CargarUenDet((RadCombo.Parent.Parent.FindControl("cmbUen") as RadComboBox));
                CargarSegmentoDet((RadCombo.Parent.Parent.FindControl("cmbSeg") as RadComboBox));
                

                (RadCombo.Parent.Parent.FindControl("cmbUen") as RadComboBox).SelectedIndex = 0;
                (RadCombo.Parent.Parent.FindControl("cmbUen") as RadComboBox).Text = (RadCombo.Parent.Parent.FindControl("cmbUen") as RadComboBox).Items[0].Text;
                (RadCombo.Parent.Parent.FindControl("cmbUen") as RadComboBox).Text = "";

                (RadCombo.Parent.Parent.FindControl("cmbSeg") as RadComboBox).SelectedIndex = 0;
                (RadCombo.Parent.Parent.FindControl("cmbSeg") as RadComboBox).Text = (RadCombo.Parent.Parent.FindControl("cmbSeg") as RadComboBox).Items[0].Text;
                (RadCombo.Parent.Parent.FindControl("cmbSeg") as RadComboBox).Text = "";

                (RadCombo.Parent.Parent.FindControl("txtId_Id_Uen") as RadNumericTextBox).Visible = true;
                (RadCombo.Parent.Parent.FindControl("txtId_Id_Seg") as RadNumericTextBox).Visible = true;

                CargarRikDet((RadCombo.Parent.Parent.FindControl("cmbRik") as RadComboBox));
            }
            else 
            {
                (RadCombo.Parent.Parent.FindControl("cmbUen") as RadComboBox).Enabled = false;
                (RadCombo.Parent.Parent.FindControl("cmbSeg") as RadComboBox).Enabled = false;
                (RadCombo.Parent.Parent.FindControl("txtId_Id_Uen") as RadNumericTextBox).Visible = false;
                (RadCombo.Parent.Parent.FindControl("txtId_Id_Seg") as RadNumericTextBox).Visible = false;


                CargarRikDet((RadCombo.Parent.Parent.FindControl("cmbRik") as RadComboBox));

                (RadCombo.Parent.Parent.FindControl("cmbRik") as RadComboBox).SelectedIndex = 0;
                (RadCombo.Parent.Parent.FindControl("cmbRik") as RadComboBox).Text = (RadCombo.Parent.Parent.FindControl("cmbRik") as RadComboBox).Items[0].Text;
                (RadCombo.Parent.Parent.FindControl("cmbRik") as RadComboBox).Text = "";
            }

            claveTerritorio(RadCombo);
        }

        //--------------------------------------------------------------------------------
        // UEN
        //--------------------------------------------------------------------------------
        protected void Uen_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            RadComboBox RadCombo = (sender as RadComboBox);
            RadCombo.SelectedIndex = RadCombo.FindItemIndexByValue((RadCombo.Parent.FindControl("lblUen") as Label).Text);
        }
        private void CargarUenDet(RadComboBox comboBox)
        {
            try
            {
                if (Uen)
                {
                    return;
                }

                Uen = true;

                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CN_Comun.LlenaCombo(1, Sesion.Id_Emp, Sesion.Emp_Cnx, "spCatUen_Combo", ref comboBox);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void cmbUen_DataBinding(object sender, EventArgs e)
        {
            try
            {
                RadComboBox cmb = sender as RadComboBox;
                CargarUenDet(cmb);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void cmbUen_TextChanged(object sender, EventArgs e)
        {
            RadComboBox RadCombo = (sender as RadComboBox);
            
            (RadCombo.Parent.Parent.FindControl("txtId_Id_Uen") as RadNumericTextBox).Value = Convert.ToInt16((((sender as RadComboBox).Parent.Parent.FindControl("cmbUen") as RadComboBox).SelectedValue));

            (RadCombo.Parent.Parent.FindControl("cmbSeg") as RadComboBox).SelectedIndex = 0;
            (RadCombo.Parent.Parent.FindControl("cmbSeg") as RadComboBox).Text = (RadCombo.Parent.Parent.FindControl("cmbSeg") as RadComboBox).Items[0].Text;
            (RadCombo.Parent.Parent.FindControl("cmbSeg") as RadComboBox).Text = "";
            CargarSegmentoDet((RadCombo.Parent.Parent.FindControl("cmbSeg") as RadComboBox));
            (RadCombo.Parent.Parent.FindControl("cmbSeg") as RadComboBox).SelectedIndex = 0;
            (RadCombo.Parent.Parent.FindControl("cmbSeg") as RadComboBox).Text = (RadCombo.Parent.Parent.FindControl("cmbSeg") as RadComboBox).Items[0].Text;
            (RadCombo.Parent.Parent.FindControl("cmbSeg") as RadComboBox).Text = "";

            CargarRikDet((RadCombo.Parent.Parent.FindControl("cmbRik") as RadComboBox));
            (RadCombo.Parent.Parent.FindControl("cmbRik") as RadComboBox).SelectedIndex = 0;
            (RadCombo.Parent.Parent.FindControl("cmbRik") as RadComboBox).Text = (RadCombo.Parent.Parent.FindControl("cmbRik") as RadComboBox).Items[0].Text;
            (RadCombo.Parent.Parent.FindControl("cmbRik") as RadComboBox).Text = "";

            (RadCombo.Parent.Parent.FindControl("txtId_Id_Seg") as RadNumericTextBox).Value = null;
            (RadCombo.Parent.Parent.FindControl("txtId_Id_Rik") as RadNumericTextBox).Value = null;

            claveTerritorio(RadCombo);
        }
        //--------------------------------------------------------------------------------
        // SEGMENTO
        //--------------------------------------------------------------------------------
        protected void Segmento_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            RadComboBox RadCombo = (sender as RadComboBox);
            RadCombo.SelectedIndex = RadCombo.FindItemIndexByValue((RadCombo.Parent.FindControl("lblSeg") as Label).Text);
        }
        protected void cmbSegmento_DataBinding(object sender, EventArgs e)
        {
            try
            {
                RadComboBox cmb = sender as RadComboBox;
                CargarSegmentoDet(cmb);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void cmbSegmento_TextChanged(object sender, EventArgs e)
        {
            RadComboBox RadCombo = (sender as RadComboBox);
            (RadCombo.Parent.Parent.FindControl("txtId_Id_Seg") as RadNumericTextBox).Value = Convert.ToInt16((((sender as RadComboBox).Parent.Parent.FindControl("cmbSeg") as RadComboBox).SelectedValue));

            claveTerritorio(RadCombo);
        }
        private void CargarSegmentoDet(RadComboBox comboBox)
        {
            try
            {
                if (Seg)
                {
                    return;
                }

                Seg = true;
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();

                CN_Comun.LlenaCombo(1, Sesion.Id_Emp, Convert.ToInt32((comboBox.Parent.Parent.FindControl("cmbUen") as RadComboBox).SelectedValue), Sesion.Emp_Cnx, "spCatSegmentos_ComboTerr", ref comboBox);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //--------------------------------------------------------------------------------
        // REPRESENTANTE
        //--------------------------------------------------------------------------------        
        protected void Rik_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            RadComboBox RadCombo = (sender as RadComboBox);
            RadCombo.SelectedIndex = RadCombo.FindItemIndexByValue((RadCombo.Parent.FindControl("lblRik") as Label).Text);
        }
        private void CargarRikDet(RadComboBox comboBox)
        {
            try
            {
                if (Rik)
                {
                    return;
                }

                Rik = true;
                int tipoRep = Convert.ToInt32((comboBox.Parent.Parent.FindControl("cmbTipoRep") as RadComboBox).SelectedValue);
                int UenSel=Convert.ToInt32((comboBox.Parent.Parent.FindControl("cmbUen") as RadComboBox).SelectedValue);

                if (tipoRep != 0)
                {
                    Sesion Sesion = new Sesion();
                    Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                    CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                    CN_Comun.LlenaCombo
                    (1,
                    Sesion.Id_Emp,
                    Sesion.Id_Cd_Ver,
                    Sesion.Id_TU == 2 ? Sesion.Id_U : (int?)null,
                    UenSel,
                    tipoRep,
                    Sesion.Emp_Cnx, "spCatRik_Combo", ref comboBox);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void cmbRik_DataBinding(object sender, EventArgs e)
        {
            try
            {
                RadComboBox cmb = sender as RadComboBox;
                CargarRikDet(cmb);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void cmbRik_TextChanged(object sender, EventArgs e)
        {
            RadComboBox RadCombo = (sender as RadComboBox);
            (RadCombo.Parent.Parent.FindControl("txtId_Id_Rik") as RadNumericTextBox).Value = Convert.ToInt32((((sender as RadComboBox).Parent.Parent.FindControl("cmbRik") as RadComboBox).SelectedValue));
        }
        //--------------------------------------------------------------------------------
        // TIPO DE CLIENTE
        //--------------------------------------------------------------------------------
        protected void TipoCli_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            RadComboBox RadCombo = (sender as RadComboBox);
            RadCombo.SelectedIndex = RadCombo.FindItemIndexByValue((RadCombo.Parent.FindControl("txtId_Id_TipoCliente") as RadNumericTextBox).Text);
        }
        private void CargarTipoCliDet(RadComboBox comboBox)
        {
            try
            {
                if (tipoCli)
                    return;
                tipoCli = true;

                //(comboBox.Parent.Parent.FindControl("txtId_Id_TipoCliente") as RadNumericTextBox).Value = null;

                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CN_Comun.LlenaCombo(1, Sesion.Id_Emp, Sesion.Emp_Cnx, "spCatTipoCliente_Combo", ref comboBox);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void cmbTipoCli_DataBinding(object sender, EventArgs e)
        {
            try
            {
                RadComboBox cmb = sender as RadComboBox;
                CargarTipoCliDet(cmb);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void RDId_Local(object sender, EventArgs e)
        {

            RadTextBox RadCombo = (sender as RadTextBox);

            claveTerritorioTxt(RadCombo);
        
        }

        protected void cmbTipoCli_TextChanged(object sender, EventArgs e)
        {
            RadComboBox RadCombo = (sender as RadComboBox);
            (RadCombo.Parent.Parent.FindControl("txtId_Id_TipoCliente") as RadNumericTextBox).Value = Convert.ToInt32((((sender as RadComboBox).Parent.Parent.FindControl("cmbTipoCli") as RadComboBox).SelectedValue));

            claveTerritorio(RadCombo);
        }


        private void claveTerritorioTxt(RadTextBox RadCombo)
        {

            string claveTerritorio = string.Empty;


            int Id_TipoRepresentante = 0;
            int Id_Uen = 0;
            int Id_Seg = 0;
            int Id_TipoCliente = 0;            
            
            
            (RadCombo.Parent.Parent.FindControl("txtId_TerNue") as RadNumericTextBox).Value = null;


            if ((RadCombo.Parent.Parent.FindControl("cmbTipoRep") as RadComboBox).SelectedValue != null)
            {
                claveTerritorio += (RadCombo.Parent.Parent.FindControl("cmbTipoRep") as RadComboBox).SelectedValue.ToString();
                Id_TipoRepresentante = Convert.ToInt32((RadCombo.Parent.Parent.FindControl("cmbTipoRep") as RadComboBox).SelectedValue.ToString());
            }

            if (
                (RadCombo.Parent.Parent.FindControl("cmbUen") as RadComboBox).SelectedValue != null
                &&
                (RadCombo.Parent.Parent.FindControl("cmbUen") as RadComboBox).SelectedValue.ToString() != "-1"
            )
            {
                claveTerritorio += (RadCombo.Parent.Parent.FindControl("cmbUen") as RadComboBox).SelectedValue.ToString().PadLeft(2, '0');
                Id_Uen = Convert.ToInt32((RadCombo.Parent.Parent.FindControl("cmbUen") as RadComboBox).SelectedValue.ToString());
            }

            if ((RadCombo.Parent.Parent.FindControl("cmbSeg") as RadComboBox).SelectedValue != null
                  &&
                (RadCombo.Parent.Parent.FindControl("cmbSeg") as RadComboBox).SelectedValue.ToString() != "-1")
            {
                claveTerritorio += (RadCombo.Parent.Parent.FindControl("cmbSeg") as RadComboBox).SelectedValue.ToString().PadLeft(2, '0');
                Id_Seg = Convert.ToInt32((RadCombo.Parent.Parent.FindControl("cmbSeg") as RadComboBox).SelectedValue.ToString());
            }

            if ((RadCombo.Parent.Parent.FindControl("cmbTipoCli") as RadComboBox).SelectedValue != null
                    &&
               (RadCombo.Parent.Parent.FindControl("cmbTipoCli") as RadComboBox).SelectedValue.ToString() != "-1")
            {
                claveTerritorio += (RadCombo.Parent.Parent.FindControl("cmbTipoCli") as RadComboBox).SelectedValue.ToString().PadLeft(2, '0');
                Id_TipoCliente = Convert.ToInt32((RadCombo.Parent.Parent.FindControl("cmbTipoCli") as RadComboBox).SelectedValue.ToString());
            }






            //aqui calcular consecutivo


            DataRow[] Ar_dr;

            int x = dt.Rows.Count;

            if (Id_TipoRepresentante == 3)
            {
                Ar_dr = dt.Select("Id_TipoRepresentante='" + Id_TipoRepresentante + "' and Id_Uen = '" + Id_Uen + "' and Id_Seg = '" + Id_Seg + "' and Id_TipoCliente = '" + Id_TipoCliente + "'", "Consecutivo Desc");
            }
            else
            {
                Ar_dr = dt.Select("Id_TipoRepresentante='" + Id_TipoRepresentante + "' and Id_TipoCliente = '" + Id_TipoCliente + "'", "Consecutivo Desc");
            }


            int Consecutivo = 0;

            for (int i = 0; i < Ar_dr.Length; i++)
                if (i == 0)
                {
                    Consecutivo = Convert.ToInt32(Ar_dr[i]["Consecutivo"].ToString());
                }

            //aqui calcular consecutivo


            if (Id_TipoCliente > 0)
            {
                Consecutivo++;
            }

            (RadCombo.Parent.Parent.FindControl("Consecutivo") as RadTextBox).Text = Convert.ToString(Consecutivo);


            claveTerritorio += Convert.ToString(Consecutivo).Trim().PadLeft(2, '0');


            (RadCombo.Parent.Parent.FindControl("txtId_TerNue") as RadNumericTextBox).Value = Convert.ToInt32(claveTerritorio);


            if (Id_TipoRepresentante == 4)
            {
                (RadCombo.Parent.Parent.FindControl("cmbTipoCli") as RadComboBox).SelectedValue = "4";
            }


            //return claveTerritorio;
        }
        
        
        
        
        private void claveTerritorio(RadComboBox RadCombo)
        {

            string claveTerritorio = string.Empty;


            int Id_TipoRepresentante = 0;
            int Id_Uen = 0;
            int Id_Seg = 0;
            int Id_TipoCliente = 0;



            (RadCombo.Parent.Parent.FindControl("txtId_TerNue") as RadNumericTextBox).Value = null;


            if ((RadCombo.Parent.Parent.FindControl("cmbTipoRep") as RadComboBox).SelectedValue != null)
            {
                claveTerritorio +=(RadCombo.Parent.Parent.FindControl("cmbTipoRep") as RadComboBox).SelectedValue.ToString();
                Id_TipoRepresentante = Convert.ToInt32((RadCombo.Parent.Parent.FindControl("cmbTipoRep") as RadComboBox).SelectedValue.ToString());
            }

            if (
                (RadCombo.Parent.Parent.FindControl("cmbUen") as RadComboBox).SelectedValue != null 
                && 
                (RadCombo.Parent.Parent.FindControl("cmbUen") as RadComboBox).SelectedValue.ToString() !="-1"
            )
            {
                claveTerritorio += (RadCombo.Parent.Parent.FindControl("cmbUen") as RadComboBox).SelectedValue.ToString().PadLeft(2, '0');
                Id_Uen = Convert.ToInt32((RadCombo.Parent.Parent.FindControl("cmbUen") as RadComboBox).SelectedValue.ToString());
            }

            if ((RadCombo.Parent.Parent.FindControl("cmbSeg") as RadComboBox).SelectedValue != null
                  &&
                (RadCombo.Parent.Parent.FindControl("cmbSeg") as RadComboBox).SelectedValue.ToString() != "-1")
            {
                claveTerritorio += (RadCombo.Parent.Parent.FindControl("cmbSeg") as RadComboBox).SelectedValue.ToString().PadLeft(2, '0');
                Id_Seg = Convert.ToInt32((RadCombo.Parent.Parent.FindControl("cmbSeg") as RadComboBox).SelectedValue.ToString());
            }

            if ((RadCombo.Parent.Parent.FindControl("cmbTipoCli") as RadComboBox).SelectedValue != null
                    &&
               (RadCombo.Parent.Parent.FindControl("cmbTipoCli") as RadComboBox).SelectedValue.ToString() != "-1")
            {
                claveTerritorio += (RadCombo.Parent.Parent.FindControl("cmbTipoCli") as RadComboBox).SelectedValue.ToString().PadLeft(2, '0');
                Id_TipoCliente = Convert.ToInt32((RadCombo.Parent.Parent.FindControl("cmbTipoCli") as RadComboBox).SelectedValue.ToString());
            }

            //aqui calcular consecutivo


            DataRow[] Ar_dr;

            int x = dt.Rows.Count;

            if (Id_TipoRepresentante == 3)
            {
                Ar_dr = dt.Select("Id_TipoRepresentante='" + Id_TipoRepresentante + "' and Id_Uen = '" + Id_Uen + "' and Id_Seg = '" + Id_Seg + "' and Id_TipoCliente = '" + Id_TipoCliente + "'" , "Consecutivo Desc");
            }
            else
            {
                Ar_dr = dt.Select("Id_TipoRepresentante='" + Id_TipoRepresentante + "' and Id_TipoCliente = '" + Id_TipoCliente + "'", "Consecutivo Desc");
            }


            int Consecutivo = 0;

            for (int i = 0; i < Ar_dr.Length; i++)
                if (i == 0)
                {
                    Consecutivo = Convert.ToInt32(Ar_dr[i]["Consecutivo"].ToString());
                }

            //aqui calcular consecutivo


            if (Id_TipoCliente > 0)
            {
                Consecutivo++;
            }

            (RadCombo.Parent.Parent.FindControl("Consecutivo") as RadTextBox).Text = Convert.ToString(Consecutivo);


            claveTerritorio += Convert.ToString(Consecutivo).Trim().PadLeft(2, '0');


            (RadCombo.Parent.Parent.FindControl("txtId_TerNue") as RadNumericTextBox).Value = Convert.ToInt32(claveTerritorio);


            if (Id_TipoRepresentante == 4)
            {
                (RadCombo.Parent.Parent.FindControl("cmbTipoCli") as RadComboBox).SelectedValue = "4";
            }

            
            //return claveTerritorio;
        }

        #endregion
        #region Funciones
        
        private void Inicializar()
        {
            try
            {
                GetList();
                rgTerritorios.Rebind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
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
                    CmbCentro.Visible = false;
                    this.TblEncabezado.Rows[0].Cells[2].InnerText = " " + CmbCentro.FindItemByValue(Sesion.Id_Cd_Ver.ToString()).Text;
                }
                else
                {
                    CN_Comun.LlenaCombo(1, Sesion.Id_Emp, Sesion.Id_U, Sesion.Id_Cd_Ver, Sesion.Id_Cd, Sesion.Emp_Cnx, "spCatCentroDistribucion_Combo", ref CmbCentro);
                    CmbCentro.SelectedValue = Sesion.Id_Cd_Ver.ToString();
                }
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
                if (pag.Length > 1)
                    pagina.Url = (new System.IO.FileInfo(Page.Request.Url.AbsolutePath)).Name + "?" + pag[1];
                else
                    pagina.Url = (new System.IO.FileInfo(Page.Request.Url.AbsolutePath)).Name;
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

                    //this.rtb1.Items[1].Visible = false;

                    if (Permiso.PGrabar == false)
                        this.rtb1.Items[1].Visible = false;
                    if (Permiso.PGrabar == false && Permiso.PModificar == false)
                        this.rtb1.Items[1].Visible = false;
                    ////Regresar
                    //this.rtb1.Items[4].Visible = false;
                    ////Eliminar
                    //this.rtb1.Items[3].Visible = false;
                    ////Imprimir
                    //this.rtb1.Items[2].Visible = false;
                    ////Correo
                    //this.rtb1.Items[1].Visible = false;
                }
                else
                    Response.Redirect("Inicio.aspx");
                CN_Ctrl ctrl = new CN_Ctrl();
                ctrl.ValidarCtrl(Sesion, pagina.Clave, divPrincipal);
                ctrl.ListaCtrls(Sesion.Emp_Cnx, pagina.Clave, divPrincipal.Controls);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        private void GetList()
        {
            try
            {
                if(ObtenerRepPendientesAct().Rows.Count>0)
                {
                    Alerta("No puede utilizar esta opción debido a que existen representantes que no tienen definido su tipo");
                    return;
                }

                List<Territorios> List = new List<Territorios>();
                CN_CatTerritorios clsCatTerritorios = new CN_CatTerritorios();
                Sesion session2 = new Sesion();
                session2 = (Sesion)Session["Sesion" + Session.SessionID];
                Territorios territorio = new Territorios();
                territorio.Id_Emp = session2.Id_Emp;
                territorio.Id_Cd = session2.Id_Cd_Ver;
                clsCatTerritorios.ConsultaTerritorios(territorio, session2.Emp_Cnx, ref List);

                dt = ConvertToDataTable(List);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private DataTable ObtenerRepPendientesAct()
        {
            try
            {
                DataTable DTRep = new DataTable();
                CN_CatTerritorios clsCatTerritorios = new CN_CatTerritorios();   
                Sesion session2 = new Sesion();
                session2 = (Sesion)Session["Sesion" + Session.SessionID];
                DTRep=clsCatTerritorios.ObtenerRepPendientesAct(session2.Emp_Cnx, session2.Id_Emp, session2.Id_Cd_Ver);
                return DTRep;
            }
            catch (Exception ex)
            {throw ex;}
        }

        private void Guardar()
        {
            try
            {
                //bool puedeguardar = false;
                //int contadorRegistros = 0;
                //int NumRegistros = dt.Rows.Count;

                //if (1 == 2)
                //{
                int RegistrosSinLlaveNueva = 0;// dt.Select("Id_TerNuevo='" + "0" + "'").Count();
                    int RegistrosYaActualizados = dt.Select("Id_TerAnt > 0").Count();

                    if (RegistrosYaActualizados > 0)
                    {
                        Alerta("Ya existen registros actualizados solo puede guardar una vez");
                        return;
                    }

                    if (RegistrosSinLlaveNueva > 0)
                    {
                        Alerta("Para poder guardar, Debe capturar todos los datos para generar la nueva clave del territorio");
                        return;
                    }
                //}

                Sesion session = new Sesion();
                session = (Sesion)Session["Sesion" + Session.SessionID];

                CN_CatTerritorios clsTerr= new CN_CatTerritorios();
                Territorios territorio = new Territorios();
                
                int verificador = 0;
                clsTerr.ModificarTerritoriosActID(territorio, dt, session.Emp_Cnx, ref verificador);

                if (verificador == 1)
                {
                    Alerta("Los datos se modificaron correctamente");
                    GetList();
                }
                else
                {
                    Alerta("Ocurrió un error al intentar modificar los datos");
                }

                rgTerritorios.Rebind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void Update(GridCommandEventArgs e)
        {
            Sesion sesion = new Sesion();
            sesion = (Sesion)Session["Sesion" + Session.SessionID];

            int Id_Emp=0;
            int Id_Cd=0;
            int Id_Ter = 0;
            int Id_TerNuevo = 0;
            string Descripcion=string.Empty;
            
            int Id_TipoRepresentante = 0;
            string TipoRepresentante = string.Empty;   

            int Id_Uen = 0;
            string Uen_Descripcion = string.Empty;
            
            int Id_Rik = 0;
            string Rik_Nombre = string.Empty;

            int Id_Seg = 0;
            string Seg_Nombre = string.Empty;

            int Id_TipoCliente = 0;
            string TipoCliente_Nombre = string.Empty;

            string Id_Local = string.Empty;
            int Consecutivo = 0;                    
            
            DataRow[] Ar_dr;
            GridItem gi = e.Item;


            Id_Ter = Convert.ToInt32(((RadNumericTextBox)gi.FindControl("txtId_Ter")).Text);
            Id_TerNuevo = Convert.ToInt32(((RadNumericTextBox)gi.FindControl("txtId_TerNue")).Text);
            Descripcion = ((RadTextBox)gi.FindControl("DescripcionTerr")).Text;


            if (int.TryParse(((RadComboBox)gi.FindControl("cmbTipoRep")).SelectedValue, out Id_TipoRepresentante))
            {
                TipoRepresentante = ((RadComboBox)gi.FindControl("cmbTipoRep")).Text;
            }

            if (int.TryParse(((RadComboBox)gi.FindControl("cmbUen")).SelectedValue, out Id_Uen))
            {
                Uen_Descripcion = ((RadComboBox)gi.FindControl("cmbUen")).Text;
            }

            if (int.TryParse(((RadComboBox)gi.FindControl("cmbRik")).SelectedValue, out Id_Rik))
            {
                Rik_Nombre = ((RadComboBox)gi.FindControl("cmbRik")).Text;
            }

            if (int.TryParse(((RadComboBox)gi.FindControl("cmbSeg")).SelectedValue, out Id_Seg))
            {
                Seg_Nombre = ((RadComboBox)gi.FindControl("cmbSeg")).Text;
            }

            if (int.TryParse(((RadComboBox)gi.FindControl("cmbTipoCli")).SelectedValue, out Id_TipoCliente))
            {
                TipoCliente_Nombre = ((RadComboBox)gi.FindControl("cmbTipoCli")).Text;
            }

            Id_Local = ((RadTextBox)gi.FindControl("Id_Local")).Text;
            Id_Local = Id_Local.Trim();

            Consecutivo = Convert.ToInt32(((RadTextBox)gi.FindControl("Consecutivo")).Text);

            if (Id_TipoRepresentante > 0 )
            {
                if (Id_TipoRepresentante == 3)
                {
                    if (Id_Uen > 0 && Id_Seg > 0 && Id_Rik > 0 && Id_TipoCliente > 0 && Id_Local.Length > 0 && Descripcion.Length>0)
                    {
                    }
                    else
                    {
                        e.Canceled = true;
                        Alerta("todos los campos son requeridos");
                        return;
                    }
                }
                else
                {
                    if (Id_Rik > 0 && Id_TipoCliente > 0 && Id_Local.Length > 0)
                    {
                    }
                    else
                    {
                        e.Canceled = true;
                        Alerta("todos los campos son requeridos");
                        return;
                    }
                }
            }
            else
            {
                e.Canceled = true;
                Alerta("Debe seleccionar un tipo de representante valido");
                return;
            }


         
            int x = dt.Rows.Count;

            Ar_dr = dt.Select("Id_Ter='" + Id_Ter + "'");
            Ar_dr[0].BeginEdit();
            Ar_dr[0]["Id_TerNuevo"] = Id_TerNuevo;
            Ar_dr[0]["Descripcion"] = Descripcion;
            Ar_dr[0]["Id_Uen"] = Id_Uen;
            Ar_dr[0]["Uen_Descripcion"] = Uen_Descripcion;
            Ar_dr[0]["Id_Seg"] = Id_Seg;
            Ar_dr[0]["Seg_Nombre"] = Seg_Nombre;
            Ar_dr[0]["Id_Rik"] = Id_Rik;
            Ar_dr[0]["Rik_Nombre"] = Rik_Nombre;
            Ar_dr[0]["Id_TipoCliente"] = Id_TipoCliente;
            Ar_dr[0]["TipoCliente_Nombre"] = TipoCliente_Nombre;
            Ar_dr[0]["Id_Local"] = Id_Local;
            Ar_dr[0]["Id_TipoRepresentante"] = Id_TipoRepresentante;
            Ar_dr[0]["TipoRepresentante_Nombre"] = TipoRepresentante;
            Ar_dr[0]["Consecutivo"] = Consecutivo;
            Ar_dr[0].AcceptChanges();

        }
        private void Edit(GridCommandEventArgs e)
        {
           
        }

        private bool Deshabilitar()
        {
            //try
            //{
                bool verificador = false;
            //    if (HF_ID.Value != "")
            //    {
            //        Sesion Sesion = new Sesion();
            //        Sesion = (Sesion)Session["Sesion" + Session.SessionID];
            //        CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
            //        Catalogo ct = new Catalogo();
            //        ct.Id_Emp = Sesion.Id_Emp;
            //        ct.Id_Cd = Sesion.Id_Cd_Ver;
            //        ct.Id = Convert.ToInt32(HF_ID.Value);
            //        ct.Tabla = "CatCliente";
            //        ct.Columna = "Id_Cte";
            //        CN_Comun.Deshabilitar(ct, Sesion.Emp_Cnx, ref verificador);
            //    }
                return verificador;
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
        }
        private void deshabilitarcontroles(System.Web.UI.ControlCollection controles_contenidos,bool valor )
        {
            for (int x = 0; x < controles_contenidos.Count; x++)
            {
                string Type = controles_contenidos[x].GetType().FullName;

                if (Type.Contains("RadMultiPage") || Type.Contains("RadPageView") || Type.Contains("Panel"))
                {
                    deshabilitarcontroles(controles_contenidos[x].Controls,valor);
                }

                switch (Type.Replace("Telerik.Web.UI.", ""))
                {
                    case "RadNumericTextBox":
                        (controles_contenidos[x] as RadNumericTextBox).Enabled = valor;
                        break;
                    case "RadTextBox":
                        (controles_contenidos[x] as RadTextBox).Enabled = valor; 
                        break;
                    case "RadComboBox":
                        (controles_contenidos[x] as RadComboBox).Enabled = valor;
                        break;
                    case "RadDatePicker":
                        (controles_contenidos[x] as RadDatePicker).Enabled = valor;
                        break;
                    case "RadDateTimePicker":
                        (controles_contenidos[x] as RadDateTimePicker).Enabled = valor;
                        break;
                    case "RadTimePicker":
                        (controles_contenidos[x] as RadTimePicker).Enabled = valor;
                        break;
                    case "RadListBox":
                        (controles_contenidos[x] as RadListBox).Enabled = valor;
                        break;

                }
                if (Type.Contains("System.Web.UI.WebControls.CheckBox"))
                {
                    (controles_contenidos[x] as System.Web.UI.WebControls.CheckBox).Enabled = valor;
                }

                
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

        protected void RAM1_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            string cmd = e.Argument.ToString();
        }

        protected void rgTerritorios_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if ((e.Item is GridEditableItem) && (e.Item.IsInEditMode))
            {
                GridEditableItem editItem = (GridEditableItem)e.Item;

                if (((RadComboBox)editItem.FindControl("cmbTipoRep")).SelectedValue != "3")
                {
                    ((RadComboBox)editItem.FindControl("cmbUen")).Visible = false;
                    ((RadComboBox)editItem.FindControl("cmbSeg")).Visible = false;
                    ((RadComboBox)editItem.FindControl("cmbUen")).Enabled = false;
                    ((RadComboBox)editItem.FindControl("cmbSeg")).Enabled = false;
                    ((RadNumericTextBox)editItem.FindControl("txtId_Id_Uen")).Visible = false;
                    ((RadNumericTextBox)editItem.FindControl("txtId_Id_Seg")).Visible = false;
                }
                else
                {
                    ((RadComboBox)editItem.FindControl("cmbUen")).Visible = true;
                    ((RadComboBox)editItem.FindControl("cmbSeg")).Visible = true;
                    ((RadComboBox)editItem.FindControl("cmbUen")).Enabled = true;
                    ((RadComboBox)editItem.FindControl("cmbSeg")).Enabled = true;
                    ((RadNumericTextBox)editItem.FindControl("txtId_Id_Uen")).Visible = true;
                    ((RadNumericTextBox)editItem.FindControl("txtId_Id_Seg")).Visible = true;
                }

            }
        }
    }
}
