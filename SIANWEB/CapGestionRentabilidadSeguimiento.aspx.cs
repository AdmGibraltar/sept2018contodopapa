using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using Telerik.Web.UI;
using CapaEntidad;
using CapaNegocios;
using System.Text;
using System.Net;
using System.IO;
using System.Collections;
using System.Xml;
using System.Diagnostics;
using System.Configuration;

namespace SIANWEB
{
    public partial class CapGestionRentabilidadSeguimiento : System.Web.UI.Page
    {

        #region Variables

        public bool _PermisoGuardar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public bool _PermisoModificar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public bool _PermisoEliminar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public bool _PermisoImprimir { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }

        public int PermisoGuardar { get { return _PermisoGuardar == true ? 1 : 0; } }
        public int PermisoModificar { get { return _PermisoModificar == true ? 1 : 0; } }
        public int PermisoEliminar { get { return _PermisoEliminar == true ? 1 : 0; } }
        public int PermisoImprimir { get { return _PermisoImprimir == true ? 1 : 0; } }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            this.Inicializar();

            CN_CatCalendario cn_calenda = new CN_CatCalendario();
            Calendario c = new Calendario();
            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];



            cn_calenda.ConsultaCalendarioActual(ref c, Sesion);
            TxtAnioInicial.Text = c.Cal_Año.ToString();
            TxtAnioFinal.Text = c.Cal_Año.ToString();
            if (Convert.ToInt32(c.Cal_Mes.ToString()) <= 9)
            {
                txtMesInicial.SelectedValue = "0" + c.Cal_Mes.ToString();
                txtMesFinal.SelectedValue = "0" + c.Cal_Mes.ToString();
            }
            else
            {
                txtMesInicial.SelectedValue = c.Cal_Mes.ToString();
                txtMesFinal.SelectedValue = c.Cal_Mes.ToString();
            }

        }
        protected void cmbCentrosDist_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                Sesion sesion = new Sesion();
                sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CN__Comun comun = new CN__Comun();

                comun.CambiarCdVer(CmbCentro.SelectedItem, ref sesion);


                Session["Sesion" + Session.SessionID] = sesion;


                rgGestionRentabilidadSeguimiento.Rebind();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void rgGestionRentabilidadSeguimiento_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                {  //Llenar Grid
                    rgGestionRentabilidadSeguimiento.DataSource = this.GetList();

                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rgGestionRentabilidadSeguimiento_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            try
            {
                this.rgGestionRentabilidadSeguimiento.Rebind();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rgGestionRentabilidadSeguimiento_ItemCommand(object source, GridCommandEventArgs e)
        {
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            string mensajeError = string.Empty;
            try
            {
                ErrorManager();
                Int32 item = default(Int32);
                if (e.Item == null) return;
                item = e.Item.ItemIndex;
                if (item >= 0)
                {
                    int Id_Emp = Convert.ToInt32(rgGestionRentabilidadSeguimiento.Items[item]["Id_Emp"].Text);
                    int Id_Cd = Convert.ToInt32(rgGestionRentabilidadSeguimiento.Items[item]["Id_Cd"].Text);
                    int Id_Cte = Convert.ToInt32(rgGestionRentabilidadSeguimiento.Items[item]["Id_Cte"].Text);
                    int Id_Ter = Convert.ToInt32(rgGestionRentabilidadSeguimiento.Items[item]["Id_Ter"].Text);
                    string Cte_NomComercial = rgGestionRentabilidadSeguimiento.Items[item]["Cte_NomComercial"].Text;

                    switch (e.CommandName.ToString())
                    {
                        case "Crear Proyecto":
                            RAM1.ResponseScripts.Add(string.Concat(@"AbrirProyecto('", Id_Emp, "','", Id_Cd, "','", Id_Ter, "','", Id_Cte, "','", Cte_NomComercial, "')"));
                            break;
                    }

                }
                if (e.CommandName.ToString().ToUpper().Contains("SORT"))
                    this.rgGestionRentabilidadSeguimiento.Rebind();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
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
                throw ex;
            }
        }

        private void Inicializar()
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                this.CargarCentros();
                rgGestionRentabilidadSeguimiento.Rebind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private List<GestionRentabilidadSeguimiento> GetList()
        {

            try
            {

                           
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];

                CN_CatCalendario cn_calenda = new CN_CatCalendario();
                Calendario c = new Calendario();
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];



                cn_calenda.ConsultaCalendarioActual(ref c, Sesion);


                List<GestionRentabilidadSeguimiento> listGestionRentabilidadSeguimiento = new List<GestionRentabilidadSeguimiento>();
                GestionRentabilidadSeguimiento gestionRentabilidadseguimiento = new GestionRentabilidadSeguimiento();




                new CN_GestionRentabilidadSeguimiento().ConsultaGestionRentabilidadSeguimiento_Buscar(gestionRentabilidadseguimiento
                                    , sesion.Emp_Cnx
                                    , ref listGestionRentabilidadSeguimiento
                                    , sesion.Id_Emp
                                    , sesion.Id_Cd_Ver
                                    , this.txtTerritorio.Text == string.Empty ? 0 : Convert.ToInt32(this.txtTerritorio.Text)
                                    , this.TxtNumeroCliente.Text == string.Empty ? 0 : Convert.ToInt32(this.TxtNumeroCliente.Text)
                                    );



                return listGestionRentabilidadSeguimiento;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        protected void RAM1_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            try
            {
                switch (e.Argument.ToString())
                {
                    case "RebindGrid":
                        Session["ListaRemisionesFactura"] = new List<Remision>();// null;


                        break;

                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }

        }

    }
}