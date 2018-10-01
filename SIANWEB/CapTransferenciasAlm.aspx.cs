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
using Telerik.Reporting.Processing;
using System.Xml;

namespace SIANWEB
{
    public partial class CapTransferenciasAlm : System.Web.UI.Page
    {
        #region Variables

        public bool _PermisoImprimir { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
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
        //private Sesion sesion
        //{
        //    get
        //    {
        //        return (Sesion)Session["Sesion" + Session.SessionID];
        //    }
        //    set
        //    {
        //        Session["Sesion" + Session.SessionID] = value;
        //    }
        //}

        #endregion Variables
        protected void Page_Load(object sender, EventArgs e)
        {
            try
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
                    if (!Page.IsPostBack)
                    {
                        Random randObj = new Random(DateTime.Now.Millisecond);
                        HF_Cve.Value = randObj.Next().ToString();

                        this.ValidarPermisos();
                        if (Sesion.Cu_Modif_Pass_Voluntario == false)
                        {
                            RAM1.ResponseScripts.Add("AbrirContrasenas(); return false;");
                            return;
                        }
                        this.CargarCentros();
                        //CargarCategoria();
                        //CargarPermisosEspeciales();
                        //rgUtilizados.Rebind();
                        //rgNoUtilizados.Rebind();

                        Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                        this.txtFecha1.SelectedDate = sesion.CalendarioIni;
                        this.txtFecha2.SelectedDate = sesion.CalendarioFin;


                        this.rgEnviados.Rebind();
                        this.rgRecibidos.Rebind();



                    }
                }
            }
            catch (Exception ex)
            {
                this.DisplayMensajeAlerta(string.Concat(ex.Message, "Page_Load_error"));
            }
        }
        #region Eventos
        protected void CmbCentro_SelectedIndexChanged1(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                Sesion sesion = new Sesion();
                sesion = (Sesion)Session["Sesion" + Session.SessionID];
                if (sesion == null)
                {
                    string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);

                    Session["dir" + Session.SessionID] = pag[pag.Length - 1]; Response.Redirect("login.aspx", false);
                }
                CN__Comun comun = new CN__Comun();
                comun.CambiarCdVer(CmbCentro.SelectedItem, ref sesion);

                //rgUtilizados.Rebind();
                //rgNoUtilizados.Rebind();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "CmbCentro_SelectedIndexChanged1");
            }
        }
        protected void RAM1_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            try
            {
                string cmd = e.Argument.ToString();
                switch (cmd)
                {
                    case "RebindGrid":
                        rgEnviados.Rebind();
                        rgRecibidos.Rebind();
                        break;

                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "RAM1_AjaxRequest");
            }
        }
        protected void CmbCentro_SelectedIndexChanged(object sender, EventArgs e)
        {
            Sesion sesion = new Sesion();
            sesion = (Sesion)Session["Sesion" + Session.SessionID];
            if (sesion == null)
            {
                string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);

                Session["dir" + Session.SessionID] = pag[pag.Length - 1]; Response.Redirect("login.aspx", false);
            }
            CN__Comun comun = new CN__Comun();
            comun.CambiarCdVer(CmbCentro.SelectedItem, ref sesion);
        }
        protected void RadToolBar1_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            try
            {
                RadToolBarButton btn = e.Item as RadToolBarButton;
                switch (btn.CommandName)
                {
                    case "Excel":
                        if (RadMultiPage1.SelectedIndex == 0)
                        {
                            ImprimirEnviados();
                        }
                        else
                        {
                            ImprimirRecibidos();
                        }
                        break;
                }

            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void BtnBuscar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                this.rgEnviados.Rebind();
                this.rgRecibidos.Rebind();
            }
            catch (Exception ex)
            {

                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }

        }
        protected void rgEnviados_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                {
                    this.rgEnviados.DataSource = getListEnviado();
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rgEnviados_PageIndexChanged(object source, Telerik.Web.UI.GridPageChangedEventArgs e)
        {
            try
            {
                rgEnviados.Rebind();

            }
            catch (Exception ex)
            {

                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }

        }
        protected void rgEnviados_ItemCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                GridItem item = e.Item;
                List<string> statusPosibles = new List<string>();
                switch (e.CommandName)
                {
                    case "Baja":
                        baja(ref e);
                        break;
                    case "Imprimir":
                        if (rgEnviados.MasterTableView.Items[e.Item.ItemIndex]["Rem_Estatus"].Text.ToUpper() == "B")
                        {
                            Alerta("El documento se encuentra en estatus no válido para imprimir");
                            e.Canceled = true;
                            return;

                        }
                        Imprimir(ref e);
                        //el estatus impreso lo asigna el reporte                        
                        break;

                    case "Editar":
                        Sesion Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                        DateTime fecha = DateTime.Parse(rgEnviados.MasterTableView.Items[e.Item.ItemIndex]["Rem_Fecha"].Text);
                        bool permitirModificar = _PermisoModificar;
                        string mensaje = "";
                        if (!(fecha >= Sesion.CalendarioIni && fecha <= Sesion.CalendarioFin))//validar fecha dentro del periodo
                        {
                            mensaje = "La fecha se encuentra fuera del periodo";
                            permitirModificar = false;
                        }

                        statusPosibles = new List<string>() { "C" };
                        if (!statusPosibles.Contains(rgEnviados.MasterTableView.Items[e.Item.ItemIndex]["Rem_Estatus"].Text.ToUpper()))
                        {
                            mensaje = "El documento se encuentra en estatus no válido para realizar la modificación";
                            permitirModificar = false;
                        }

                        if (rgEnviados.MasterTableView.Items[e.Item.ItemIndex]["Rem_ManAut"].Text.ToLower() != "1")
                        {
                            mensaje = "El documento fue creado de manera automática y no puede ser modificado";
                            permitirModificar = false;
                        }

                        string Id_Rem = rgEnviados.MasterTableView.Items[e.Item.ItemIndex]["Id_Rem"].Text;
                        CN_CapRemision cn_capremision = new CN_CapRemision();
                        int verificador = 0;
                        cn_capremision.ConsultarPermitirModificar(Convert.ToInt32(Id_Rem), Sesion.Id_Emp, Sesion.Id_Cd_Ver, Sesion.Emp_Cnx, ref verificador);
                        if (verificador > 0)
                        {
                            mensaje = "La remisión ya tiene movimientos aplicados, no es posible modificarla";
                            permitirModificar = false;
                        }
                        RAM1.ResponseScripts.Add("return OpenWindow('" + 2 + "','" + Id_Rem + "','" + permitirModificar + "','" + mensaje + "')");
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rgEnviados_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem item = (GridDataItem)e.Item;
                WebControl Button = default(WebControl);
                string clickHandler = "";

                Button = (WebControl)item["Baja"].Controls[0];
                clickHandler = Button.Attributes["onclick"];
                Button.Attributes["onclick"] = clickHandler.Replace("[[ID]]", item.GetDataKeyValue("Id_Rem").ToString());


            }
        }
        protected void rgRecibidos_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                {
                    this.rgRecibidos.DataSource = getListRecibido();
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rgRecibidos_PageIndexChanged(object source, Telerik.Web.UI.GridPageChangedEventArgs e)
        {
            try
            {
                rgRecibidos.Rebind();

            }
            catch (Exception ex)
            {

                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }

        }
        protected void rgRecibidos_ItemCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                GridItem item = e.Item;
                List<string> statusPosibles = new List<string>();
                switch (e.CommandName)
                {
                    case "Recibir":



                        int Id_Emp = int.Parse(rgRecibidos.MasterTableView.Items[e.Item.ItemIndex]["Id_Emp"].Text);
                        int Id_Cd = int.Parse(rgRecibidos.MasterTableView.Items[e.Item.ItemIndex]["Id_Cd"].Text);
                        int Id_Tra = int.Parse(rgRecibidos.MasterTableView.Items[e.Item.ItemIndex]["Id_Tra"].Text);
                        int Modificar = rgRecibidos.MasterTableView.Items[e.Item.ItemIndex]["Tra_Estatus"].Text == "N" ? 1 : 0;

                        Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                        CN_TransferenciasAlm cn_tra = new CN_TransferenciasAlm();
                        TransferenciasAlm tra = new TransferenciasAlm();

                        cn_tra.ProTransferenciaAlmacen_Consulta(Id_Emp, Id_Cd, Id_Tra, ref tra, sesion.Emp_Cnx);

                        if (tra.Tra_Estatus == "X" || tra.Tra_Estatus == "B")
                        {

                            Alerta("La transferencia se encuentra en un estatus inválido para realizar la recepción");
                            rgEnviados.Rebind();
                            rgRecibidos.Rebind();
                            return;
                        }

                        RAM1.ResponseScripts.Add("return AbrirVentana_Recepcion('" + Id_Emp + "','" + Id_Cd + "','" + Id_Tra + "','" + Modificar + "')");
                        break;
                    case "Imprimir":
                        ImprimirTransfer(ref e);
                        //el estatus impreso lo asigna el reporte                        
                        break;

                    //case "Editar":
                    //    Sesion Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                    //    DateTime fecha = DateTime.Parse(rgEnviados.MasterTableView.Items[e.Item.ItemIndex]["Rem_Fecha"].Text);
                    //    bool permitirModificar = _PermisoModificar;
                    //    string mensaje = "";
                    //    if (!(fecha >= Sesion.CalendarioIni && fecha <= Sesion.CalendarioFin))//validar fecha dentro del periodo
                    //    {
                    //        mensaje = "La fecha se encuentra fuera del periodo";
                    //        permitirModificar = false;
                    //    }

                    //    statusPosibles = new List<string>() { "C" };
                    //    if (!statusPosibles.Contains(rgEnviados.MasterTableView.Items[e.Item.ItemIndex]["Rem_Estatus"].Text.ToUpper()))
                    //    {
                    //        mensaje = "El documento se encuentra en estatus no válido para realizar la modificación";
                    //        permitirModificar = false;
                    //    }

                    //    if (rgEnviados.MasterTableView.Items[e.Item.ItemIndex]["Rem_ManAut"].Text.ToLower() != "1")
                    //    {
                    //        mensaje = "El documento fue creado de manera automática y no puede ser modificado";
                    //        permitirModificar = false;
                    //    }

                    //    string Id_Rem = rgEnviados.MasterTableView.Items[e.Item.ItemIndex]["Id_Rem"].Text;
                    //    CN_CapRemision cn_capremision = new CN_CapRemision();
                    //    int verificador = 0;
                    //    cn_capremision.ConsultarPermitirModificar(Convert.ToInt32(Id_Rem), Sesion.Id_Emp, Sesion.Id_Cd_Ver, Sesion.Emp_Cnx, ref verificador);
                    //    if (verificador > 0)
                    //    {
                    //        mensaje = "La remisión ya tiene movimientos aplicados, no es posible modificarla";
                    //        permitirModificar = false;
                    //    }
                    //    RAM1.ResponseScripts.Add("return OpenWindow('" + 2 + "','" + Id_Rem + "','" + permitirModificar + "','" + mensaje + "')");
                    //    break;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        public void Timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                rgEnviados.Rebind();
                rgRecibidos.Rebind();
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }


        #endregion Eventos
        #region Funciones
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

                CapaDatos.CD_PermisosU CN_PermisosU = new CapaDatos.CD_PermisosU();
                CN_PermisosU.ValidaPermisosUsuario(ref Permiso, Sesion.Emp_Cnx);

                if (Permiso.PAccesar == true)
                    _PermisoImprimir = Permiso.PImprimir;
                _PermisoModificar = Permiso.PModificar;
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
        private List<Remision> getListEnviado()
        {
            try
            {
                List<Remision> List = new List<Remision>();
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                TransferenciasAlm TA = new TransferenciasAlm();
                CN_TransferenciasAlm cn_ta = new CN_TransferenciasAlm();
                string Conexion = sesion.Emp_Cnx;

                TA.Id_Cd = sesion.Id_Cd_Ver;
                TA.Id_CteIni = this.TxtSucIni.Text == "" ? (int?)null : int.Parse(this.TxtSucIni.Text);
                TA.Id_CteFin = this.TxtSucFin.Text == "" ? (int?)null : int.Parse(this.TxtSucFin.Text);
                TA.Id_Rem = this.TxtId_Rem.Text == "" ? (int?)null : int.Parse(this.TxtId_Rem.Text);
                TA.Rem_FechaIni = this.txtFecha1.SelectedDate.ToString() == "" ? (DateTime?)null : DateTime.Parse(this.txtFecha1.SelectedDate.ToString());
                TA.Rem_FechaFin = this.txtFecha2.SelectedDate.ToString() == "" ? (DateTime?)null : DateTime.Parse(this.txtFecha2.SelectedDate.ToString());
                TA.Rem_Estatus = this.CmbEstatus.SelectedValue == "T" ? null : this.CmbEstatus.SelectedValue;

                cn_ta.CapRemision_ConsultaTransferencia(TA, ref List, Conexion);

                return List;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private List<TransferenciasAlm> getListRecibido()
        {
            try
            {
                List<TransferenciasAlm> List = new List<TransferenciasAlm>();
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                TransferenciasAlm TA = new TransferenciasAlm();
                CN_TransferenciasAlm cn_ta = new CN_TransferenciasAlm();

                TA.Id_Cd = sesion.Id_Cd_Ver;
                TA.Id_CdOrigenIni = this.TxtSucIni.Text == "" ? (int?)null : int.Parse(this.TxtSucIni.Text);
                TA.Id_CdOrigenFin = this.TxtSucFin.Text == "" ? (int?)null : int.Parse(this.TxtSucFin.Text);
                TA.Id_RemOrigen = this.TxtId_Rem.Text == "" ? (int?)null : int.Parse(this.TxtId_Rem.Text);
                TA.Tra_FechaIni = this.txtFecha1.SelectedDate.ToString() == "" ? (DateTime?)null : DateTime.Parse(this.txtFecha1.SelectedDate.ToString());
                TA.Tra_FechaFin = this.txtFecha2.SelectedDate.ToString() == "" ? (DateTime?)null : DateTime.Parse(this.txtFecha2.SelectedDate.ToString());
                TA.Tra_Estatus = this.CmbEstatus.SelectedValue == "T" ? null : this.CmbEstatus.SelectedValue;

                cn_ta.ProTransferenciaAlmacen_ConsultaLista(TA, ref List, sesion.Emp_Cnx);

                return List;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private DataTable getdtEnviado()
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                TransferenciasAlm TA = new TransferenciasAlm();
                CN_TransferenciasAlm cn_ta = new CN_TransferenciasAlm();

                DataTable dt = null;
                string Conexion = sesion.Emp_Cnx;

                TA.Id_Cd = sesion.Id_Cd_Ver;
                TA.Id_CteIni = this.TxtSucIni.Text == "" ? (int?)null : int.Parse(this.TxtSucIni.Text);
                TA.Id_CteFin = this.TxtSucFin.Text == "" ? (int?)null : int.Parse(this.TxtSucFin.Text);
                TA.Id_Rem = this.TxtId_Rem.Text == "" ? (int?)null : int.Parse(this.TxtId_Rem.Text);
                TA.Rem_FechaIni = this.txtFecha1.SelectedDate.ToString() == "" ? (DateTime?)null : DateTime.Parse(this.txtFecha1.SelectedDate.ToString());
                TA.Rem_FechaFin = this.txtFecha2.SelectedDate.ToString() == "" ? (DateTime?)null : DateTime.Parse(this.txtFecha2.SelectedDate.ToString());
                TA.Rem_Estatus = this.CmbEstatus.SelectedValue == "T" ? null : this.CmbEstatus.SelectedValue;

                cn_ta.CapRemision_ConsultaTransferenciaImprimir(TA, ref dt, Conexion);

                return dt;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private DataTable getdtRecibido()
        {
            try
            {
                DataTable dt = null;
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                TransferenciasAlm TA = new TransferenciasAlm();
                CN_TransferenciasAlm cn_ta = new CN_TransferenciasAlm();

                TA.Id_Cd = sesion.Id_Cd_Ver;
                TA.Id_CdOrigenIni = this.TxtSucIni.Text == "" ? (int?)null : int.Parse(this.TxtSucIni.Text);
                TA.Id_CdOrigenFin = this.TxtSucFin.Text == "" ? (int?)null : int.Parse(this.TxtSucFin.Text);
                TA.Id_RemOrigen = this.TxtId_Rem.Text == "" ? (int?)null : int.Parse(this.TxtId_Rem.Text);
                TA.Tra_FechaIni = this.txtFecha1.SelectedDate.ToString() == "" ? (DateTime?)null : DateTime.Parse(this.txtFecha1.SelectedDate.ToString());
                TA.Tra_FechaFin = this.txtFecha2.SelectedDate.ToString() == "" ? (DateTime?)null : DateTime.Parse(this.txtFecha2.SelectedDate.ToString());
                TA.Tra_Estatus = this.CmbEstatus.SelectedValue == "T" ? null : this.CmbEstatus.SelectedValue;

                cn_ta.ProTransferenciaAlmacen_ConsultaListaImprimir(TA, ref dt, sesion.Emp_Cnx);

                return dt;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private void baja(ref GridCommandEventArgs e)
        {
            List<string> statusPosibles;
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            DateTime fecha = DateTime.Parse(rgEnviados.MasterTableView.Items[e.Item.ItemIndex]["Rem_Fecha"].Text);


            if (!(fecha >= sesion.CalendarioIni && fecha <= sesion.CalendarioFin))////validar fecha dentro del periodo
            {
                Alerta("La fecha se encuentra fuera del periodo");
                e.Canceled = true;
                return;
            }

            //validar que no sea tipo impreso
            statusPosibles = new List<string>() { "B", "X", "R" };
            if ((statusPosibles.Contains(rgEnviados.MasterTableView.Items[e.Item.ItemIndex]["Rem_Estatus"].Text.ToUpper())))
            {
                Alerta("El documento se encuentra en estatus no válido para realizar la baja");
                e.Canceled = true;
                return;
            }

            // REMTIPO debe ser diferente a A para poderse eliminar. ?????
            // posiblemente validar que sea manual
            if (rgEnviados.MasterTableView.Items[e.Item.ItemIndex]["Rem_ManAut"].Text.ToLower() != "1")
            {
                Alerta("El documento fue creado de manera automática y no puede ser modificado");
                e.Canceled = true;
                return;
            }

            // afectar inventario final <== se afecta en el sp

            Remision remision = new Remision();
            remision.Id_Emp = int.Parse(rgEnviados.MasterTableView.Items[e.Item.ItemIndex]["Id_Emp"].Text);
            remision.Id_Cd = int.Parse(rgEnviados.MasterTableView.Items[e.Item.ItemIndex]["Id_Cd"].Text);
            remision.Id_Rem = int.Parse(rgEnviados.MasterTableView.Items[e.Item.ItemIndex]["Id_Rem"].Text);
            remision.Rem_Fecha = fecha;
            remision.Rem_Estatus = "B";
            remision.Id_Tm = 54;
            remision.Id_Cte = Convert.ToInt32(rgEnviados.MasterTableView.Items[e.Item.ItemIndex]["Id_Cte"].Text);
            remision.Id_Ped = -1;




            new CN_CapRemision().ConsultarEncabezadoImprimir(sesion, remision.Id_Rem, ref remision, 0);

            if (remision.Rem_EstatusStr == "Cancelado por destinatario" || remision.Rem_EstatusStr == "Recibido")
            {
                Alerta("El documento se encuentra en estatus no válido para realizar la baja");
                e.Canceled = true;
                rgEnviados.Rebind();
                rgRecibidos.Rebind();
                return;
            }

            List<RemisionDet> detalles = new List<RemisionDet>();
            new CN_CapRemision().ConsultarRemisionesDetalle(sesion, remision, ref detalles);

            int verificador12 = -1;
            try
            {
                if (remision.Rem_CteCuentaNacional > 0)
                {
                    ImprimirRemisionElectronica(remision);
                }


                CN_TransferenciasAlm cn_ta = new CN_TransferenciasAlm();

                new CN_CapRemision().BajaRemision(ref remision, ref detalles, sesion, ref verificador12);
                cn_ta.ProTransferenciaAlmacen_BajaRemitente(remision, ref verificador12, sesion.Emp_Cnx);
            }
            catch (Exception ex)
            {
                Alerta(ex.Message);
                return;
            }

            rgEnviados.Rebind();

            Alerta("El movimiento se dio de baja correctamente");
        }
        private void Imprimir(ref GridCommandEventArgs e)
        {
            try
            {
                ///Valida que el estatus esté en capturado, impreso, embarcado o entregado.
                ///Que sea diferente a baja
                ///Lo manda a imprimir, y se cambia el estatus a impreso.
                //List<string> statusPosibles = new List<string>() { "C", "I", "E", "N" };
                //if (!statusPosibles.Contains(rgPedido.MasterTableView.Items[e.Item.ItemIndex]["Rem_Estatus"].Text.ToUpper()))
                //{
                //    Alerta("El documento se encuentra en estatus no válido");
                //    e.Canceled = true;
                //    return;
                //}



                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                int Id_Emp = sesion.Id_Emp;
                int Id_Cd_Ver = sesion.Id_Cd_Ver;
                int Id_Rem = int.Parse(rgEnviados.MasterTableView.Items[e.Item.ItemIndex]["Id_Rem"].Text);

                GridItem gi = Session["Pedido" + Session.SessionID] as GridItem;

                Remision remision = new Remision();
                new CN_CapRemision().ConsultarEncabezadoImprimir(sesion, Id_Rem, ref remision, 0);
                ArrayList ALValorParametrosInternos = new ArrayList();



                if (remision.Id_Tm == 54 || remision.Id_Tm == 80)
                {
                    CN_CatCliente cn_catcliente = new CN_CatCliente();
                    Clientes cliente = new Clientes();
                    cliente.Id_Emp = sesion.Id_Emp;
                    cliente.Id_Cd = sesion.Id_Cd_Ver;
                    cliente.Id_Cte = remision.Id_Cte;
                    cn_catcliente.ConsultaClientes(ref cliente, sesion.Emp_Cnx);





                    if (remision.Rem_CteCuentaNacional != 0 || cliente.Cte_RemisionElectronica > 0)
                    {
                        if (cliente.Cte_RemisionElectronica > 0 && remision.Rem_CteCuentaNacional == null || remision.Rem_CteCuentaNacional == 1)
                        {
                            ImprimirRemisionElectronicaPIPES(remision);
                        }
                        else
                        {
                            ImprimirRemisionElectronica(remision);
                        }

                        return;
                    }



                }
                int Verificador = 0;
                CN_TransferenciasAlm cn_ta = new CN_TransferenciasAlm();
                cn_ta.ProTransferenciaAlmacen_Insertar(remision, ref Verificador, sesion.Emp_Cnx);

                ////Consulta centro de distribución               
                ALValorParametrosInternos.Add(remision.Id_Emp);
                ALValorParametrosInternos.Add(remision.Rem_Calle);
                if (!string.IsNullOrEmpty(remision.Rem_Numero))
                    remision.Rem_Numero = "# " + remision.Rem_Numero;
                ALValorParametrosInternos.Add(remision.Rem_Numero);
                ALValorParametrosInternos.Add(remision.Rem_Colonia);
                ALValorParametrosInternos.Add(remision.Rem_Municipio);
                ALValorParametrosInternos.Add(remision.Rem_Estado);
                if (!string.IsNullOrEmpty(remision.Rem_Cp))
                    if (!remision.Rem_Cp.Contains("C.P."))
                        remision.Rem_Cp = "C.P. " + remision.Rem_Cp;
                ALValorParametrosInternos.Add(remision.Rem_Cp);
                ALValorParametrosInternos.Add(remision.Id_Rem);
                ALValorParametrosInternos.Add(remision.Rem_Fecha.ToShortDateString());
                ALValorParametrosInternos.Add((remision.Rem_FechaHr == null) ? "00:00" : remision.Rem_FechaHr.Value.ToShortTimeString());
                ALValorParametrosInternos.Add(remision.Cte_NomComercial);
                ALValorParametrosInternos.Add((remision.Id_Ped == 0 || remision.Id_Ped == -1) ? string.Empty : remision.Id_Ped.ToString());
                ALValorParametrosInternos.Add(remision.Rem_Conducto);
                ALValorParametrosInternos.Add(remision.Cte_CondPago);

                ALValorParametrosInternos.Add(remision.Cte_Calle);
                ALValorParametrosInternos.Add(remision.Cte_Numero);
                ALValorParametrosInternos.Add(remision.Cte_Colonia);

                ALValorParametrosInternos.Add(remision.Id_Cte);
                ALValorParametrosInternos.Add(remision.Id_Tm);
                ALValorParametrosInternos.Add(remision.Tm_Nombre);
                ALValorParametrosInternos.Add(remision.Id_Cd);
                ALValorParametrosInternos.Add((remision.Id_Ter == -1) ? string.Empty : remision.Id_Ter.ToString());
                ALValorParametrosInternos.Add(remision.Id_Rik == -1 ? string.Empty : remision.Id_Rik.ToString());

                ALValorParametrosInternos.Add(remision.Rik_Calle);
                ALValorParametrosInternos.Add(remision.Rik_Numero);
                ALValorParametrosInternos.Add(remision.Rik_Colonia);

                ALValorParametrosInternos.Add(remision.Rem_Subtotal);
                ALValorParametrosInternos.Add(remision.Rem_Iva);
                ALValorParametrosInternos.Add(remision.Rem_Total);
                ALValorParametrosInternos.Add(remision.Rem_OrdenCompra);
                ALValorParametrosInternos.Add(sesion.Emp_Cnx);
                StringBuilder NotaProductosOriginales = new StringBuilder();

                if (remision.Rem_Especial > 0)
                {
                    List<RemisionDet> detalles = new List<RemisionDet>();
                    new CN_CapRemision().ConsultarRemisionesDetalle(sesion, remision, ref detalles);

                    foreach (RemisionDet detalle in detalles)
                    {
                        NotaProductosOriginales.Append("/");
                        NotaProductosOriginales.Append(detalle.Id_Prd.ToString());
                        NotaProductosOriginales.Append("/");
                        NotaProductosOriginales.Append(Math.Round(detalle.Rem_Precio, 2).ToString());
                        NotaProductosOriginales.Append("/");
                        NotaProductosOriginales.Append(detalle.Rem_Cant.ToString());
                    }
                }
                ALValorParametrosInternos.Add(remision.Rem_Nota + NotaProductosOriginales.ToString());

                ALValorParametrosInternos.Add("pp");
                ALValorParametrosInternos.Add("jj");
                ALValorParametrosInternos.Add("jj");
                ALValorParametrosInternos.Add("jj");
                ALValorParametrosInternos.Add("ks");
                ALValorParametrosInternos.Add("sd");
                ALValorParametrosInternos.Add("sd");
                ALValorParametrosInternos.Add("sd");
                ALValorParametrosInternos.Add("sds");
                ALValorParametrosInternos.Add("sd");

                Type instance = null;
                instance = typeof(LibreriaReportes.RemisionImpresion);


                Session["InternParameter_Values" + Session.SessionID + HF_Cve.Value] = ALValorParametrosInternos;
                Session["assembly" + Session.SessionID + HF_Cve.Value] = instance.AssemblyQualifiedName;

                //NOTA: El estatus de impresión, lo pone cuando el reporte se carga
                RAM1.ResponseScripts.Add("AbrirReporteCve('" + HF_Cve.Value + "');");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void ImprimirRemisionElectronica(Remision remision)
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                List<RemisionDet> listaProdFacturaEspecialFinal = new List<RemisionDet>();

                if (remision.Rem_Estatus != "B")
                {
                    remision.Rem_Estatus = "I";
                }
                int verificador = 0;
                new CN_CapRemision().ModificarRemision_Estatus(remision, sesion.Emp_Cnx, ref verificador);

                new CN_CapRemision().ConsultaRemisionEspecialDetalle(ref listaProdFacturaEspecialFinal, sesion.Emp_Cnx, sesion.Id_Emp, sesion.Id_Cd_Ver, remision.Id_Rem, remision.Id_Cte);

                CN_CatCliente cn_catcliente = new CN_CatCliente();
                Clientes clientes = new Clientes();
                clientes.Id_Emp = sesion.Id_Emp;
                clientes.Id_Cd = sesion.Id_Cd_Ver;
                clientes.Id_Cte = remision.Id_Cte;
                cn_catcliente.ConsultaClientes(ref clientes, sesion.Emp_Cnx);

                int Id_Rem = remision.Id_Rem;
                Remision remision2 = new Remision();
                new CN_CapRemision().ConsultarEncabezadoImprimir(sesion, Id_Rem, ref remision2, 0);

                CN_CatCentroDistribucion cn_cd = new CN_CatCentroDistribucion();
                CentroDistribucion cd = new CentroDistribucion();
                double iva = 0;
                cn_cd.ConsultarIva(sesion.Id_Emp, sesion.Id_Cd_Ver, ref iva, sesion.Emp_Cnx);

                StringBuilder XML_Enviar = new StringBuilder();
                XML_Enviar.Append("<?xml version=\"1.0\" encoding=\"iso-8859-1\"?>");
                XML_Enviar.Append("<RemisionCuentaNacional");
                XML_Enviar.Append(" TipoDocumento=\"\">");

                XML_Enviar.Append(" <Sucursal");
                XML_Enviar.Append(" CDINum=\"\"");
                XML_Enviar.Append(" CDINom=\"\"");
                XML_Enviar.Append(" CDIIVA=\"\"/>");

                XML_Enviar.Append(" <Documento");
                XML_Enviar.Append(" Folio=\"\"");
                XML_Enviar.Append(" Status=\"\"");
                XML_Enviar.Append(" Fecha=\"\"");
                XML_Enviar.Append(" Conducto=\"\"");
                XML_Enviar.Append(" Total=\"\"/>");

                XML_Enviar.Append(" <Cliente");
                XML_Enviar.Append(" NoCliente=\"\"");
                XML_Enviar.Append(" Nombre=\"\"");
                XML_Enviar.Append(" CuentaNacional=\"\">");

                XML_Enviar.Append(" <DatosFiscales");
                XML_Enviar.Append(" Calle=\"\"");
                XML_Enviar.Append(" Numero=\"\"");
                XML_Enviar.Append(" Colonia=\"\"");
                XML_Enviar.Append(" Municipio=\"\"");
                XML_Enviar.Append(" Estado=\"\"");
                XML_Enviar.Append(" C.P.=\"\"/>");

                XML_Enviar.Append(" <DatosConsignacion");
                XML_Enviar.Append(" Calle=\"\"");
                XML_Enviar.Append(" Numero=\"\"");
                XML_Enviar.Append(" Colonia=\"\"");
                XML_Enviar.Append(" Municipio=\"\"");
                XML_Enviar.Append(" Estado=\"\"");
                XML_Enviar.Append(" C.P.=\"\"/>");
                XML_Enviar.Append(" </Cliente>");

                XML_Enviar.Append(" <DetalleKey>");
                if (listaProdFacturaEspecialFinal.Count() > 0)
                {
                    foreach (RemisionDet d in listaProdFacturaEspecialFinal)
                    {
                        XML_Enviar.Append(" <Producto");
                        XML_Enviar.Append(" Codigo=\"" + d.Producto.Id_Prd + "\"");
                        XML_Enviar.Append(" Descipcion=\"" + d.Producto.Prd_Descripcion.ToString().Replace("\"", "").Replace("'", "").Replace("&", "") +
                                          "\"");
                        XML_Enviar.Append(" Cantidad=\"" + d.Rem_Cant + "\"");
                        XML_Enviar.Append(" Unidad=\"" + d.Producto.Prd_UniNe + "\"");
                        XML_Enviar.Append(" Presentacion=\"" + d.Producto.Prd_Presentacion + "\"");
                        XML_Enviar.Append(" Precio=\"" + d.Rem_Precio + "\"");
                        XML_Enviar.Append(" Territorio=\"" + d.Producto.Id_Ter + "\"");
                        XML_Enviar.Append(" Nombre=\"" + ObtenerNombreTerritorio(sesion.Id_Emp, sesion.Id_Cd_Ver, d.Producto.Id_Ter, sesion.Emp_Cnx) + "\"");

                        XML_Enviar.Append(" />");
                    }
                }

                XML_Enviar.Append(" </DetalleKey>");

                if (remision.Rem_CteCuentaNacional == 6)
                {
                    XML_Enviar.Append(" <DetalleEspecial>");
                    if (listaProdFacturaEspecialFinal.Count() > 0)
                    {
                        foreach (RemisionDet d in listaProdFacturaEspecialFinal)
                        {
                            XML_Enviar.Append(" <ProductoEspecial");
                            XML_Enviar.Append(" Codigo=\"" + d.Producto.Id_PrdEsp + "\"");
                            XML_Enviar.Append(" Descipcion=\"" + d.Producto.Prd_DescripcionEspecial.ToString().Replace("\"", "").Replace("'", "").Replace("&", "") +
                                              "\"");
                            XML_Enviar.Append(" Cantidad=\"" + d.Rem_Cant + "\"");
                            XML_Enviar.Append(" Unidad=\"" + d.Producto.Prd_UniNe + "\"");
                            XML_Enviar.Append(" Presentacion=\"" + d.Producto.Prd_Presentacion + "\"");
                            XML_Enviar.Append(" Precio=\"" + d.Rem_Precio + "\"");
                            XML_Enviar.Append(" Territorio=\"" + d.Producto.Id_Ter + "\"");
                            XML_Enviar.Append(" Nombre=\"" + ObtenerNombreTerritorio(sesion.Id_Emp, sesion.Id_Cd_Ver, d.Producto.Id_Ter, sesion.Emp_Cnx) + "\"");
                            XML_Enviar.Append(" CodigoOriginal=\"" + d.Producto.Id_Prd + "\"");
                            XML_Enviar.Append(" Release=\"" + d.Clp_Release + "\"");

                            XML_Enviar.Append(" />");
                        }
                    }
                    XML_Enviar.Append(" </DetalleEspecial>");
                }
                XML_Enviar.Append(" </RemisionCuentaNacional>");

                XmlDocument xml = new XmlDocument();

                xml.LoadXml(XML_Enviar.ToString());

                XmlNode RemisionCuentaNacional = xml.SelectSingleNode("RemisionCuentaNacional");
                RemisionCuentaNacional.Attributes["TipoDocumento"].Value = "Remision";

                XmlNode Sucursal = RemisionCuentaNacional.SelectSingleNode("Sucursal");
                Sucursal.Attributes["CDINum"].Value = remision.Id_Cd.ToString();
                Sucursal.Attributes["CDINom"].Value = "Remision";
                Sucursal.Attributes["CDIIVA"].Value = iva.ToString();

                XmlNode Documento = RemisionCuentaNacional.SelectSingleNode("Documento");
                Documento.Attributes["Folio"].Value = remision.Id_Rem.ToString();
                Documento.Attributes["Status"].Value = remision.Rem_Estatus.ToString();
                Documento.Attributes["Fecha"].Value = remision.Rem_Fecha.ToShortDateString();
                Documento.Attributes["Conducto"].Value = remision.Rem_Conducto.ToString();
                Documento.Attributes["Total"].Value = remision.Rem_Total.ToString();

                XmlNode Cliente = RemisionCuentaNacional.SelectSingleNode("Cliente");
                Cliente.Attributes["NoCliente"].Value = remision.Id_Cte.ToString();
                Cliente.Attributes["Nombre"].Value = remision.Cte_NomComercial.ToString();
                Cliente.Attributes["CuentaNacional"].Value = remision.Rem_CteCuentaNacional.ToString();

                XmlNode DatosFiscales = Cliente.SelectSingleNode("DatosFiscales");
                DatosFiscales.Attributes["Calle"].Value = clientes.Cte_FacCalle.ToString();
                DatosFiscales.Attributes["Numero"].Value = clientes.Cte_FacNumero.ToString();
                DatosFiscales.Attributes["Colonia"].Value = clientes.Cte_FacColonia.ToString();
                DatosFiscales.Attributes["Municipio"].Value = clientes.Cte_FacMunicipio.ToString();
                DatosFiscales.Attributes["Estado"].Value = clientes.Cte_FacEstado.ToString();
                DatosFiscales.Attributes["C.P."].Value = clientes.Cte_FacCp.ToString();

                XmlNode DatosConsignacion = Cliente.SelectSingleNode("DatosConsignacion");
                DatosConsignacion.Attributes["Calle"].Value = remision2.Rem_Calle.ToString();
                DatosConsignacion.Attributes["Numero"].Value = remision2.Rem_Numero.ToString();
                DatosConsignacion.Attributes["Colonia"].Value = remision2.Rem_Colonia.ToString();
                DatosConsignacion.Attributes["Municipio"].Value = remision.Rem_Municipio.ToString();
                DatosConsignacion.Attributes["Estado"].Value = remision.Rem_Estado.ToString();
                DatosConsignacion.Attributes["C.P."].Value = remision.Rem_Cp.ToString();

                /*
                StringBuilder cadena = new StringBuilder();
                int contador = 0;
                foreach (RemisionDet d in listaProdFacturaEspecialFinal)
                {
                if (contador > 0)
                cadena.Append("|");
                cadena.Append(remision.Id_Cd);
                cadena.Append("|");
                cadena.Append(d.Clp_Release.Replace("|",""));
                cadena.Append("|");
                cadena.Append(remision.Id_Rem);
                cadena.Append("|");
                cadena.Append(remision.Rem_EstatusStr.Replace("|", ""));
                cadena.Append("|");
                cadena.Append(d.Producto.Id_Ter);
                cadena.Append("|");
                cadena.Append(ObtenerNombreTerritorio(sesion.Id_Emp, sesion.Id_Cd_Ver, d.Producto.Id_Ter, sesion.Emp_Cnx));
                cadena.Append("|");
                cadena.Append(remision.Id_Cte);
                cadena.Append("|");
                cadena.Append(remision.Cte_NomComercial.Replace("|", ""));
                cadena.Append("|");
                //DATOS FISCALES
                cadena.Append(clientes.Cte_FacCalle.Replace("|", ""));
                cadena.Append("|");
                cadena.Append(clientes.Cte_FacNumero.Replace("|", ""));
                cadena.Append("|");
                cadena.Append(clientes.Cte_FacColonia.Replace("|", ""));
                cadena.Append("|");
                cadena.Append(clientes.Cte_FacMunicipio.Replace("|", ""));
                cadena.Append("|");
                cadena.Append(clientes.Cte_FacEstado.Replace("|", ""));
                cadena.Append("|");
                cadena.Append(clientes.Cte_FacCp.Replace("|", ""));
                cadena.Append("|");
                //DATOS DE CONSIGNACION
                cadena.Append(remision2.Rem_Calle.Replace("|", ""));
                cadena.Append("|");
                cadena.Append(remision2.Rem_Numero.Replace("|", ""));
                cadena.Append("|");
                cadena.Append(remision2.Rem_Colonia.Replace("|", ""));
                cadena.Append("|");
                cadena.Append(remision2.Rem_Municipio.Replace("|", ""));
                cadena.Append("|");
                cadena.Append(remision2.Rem_Estado.Replace("|", ""));
                cadena.Append("|");
                cadena.Append(remision2.Rem_Cp.Replace("|", ""));
                cadena.Append("|");
                //PRODUCTOS
                cadena.Append(d.Producto.Id_PrdEsp);
                cadena.Append("|");
                cadena.Append(d.Producto.Id_Prd);
                cadena.Append("|");
                cadena.Append(d.Producto.Prd_DescripcionEspecial.Replace("|", ""));
                cadena.Append("|");
                cadena.Append(d.Producto.Prd_Presentacion.Replace("|", ""));
                cadena.Append("|");
                cadena.Append(d.Producto.Prd_UniNs.Replace("|", ""));
                cadena.Append("|");
                cadena.Append(d.Rem_Cant);
                cadena.Append("|");
                cadena.Append(d.Rem_Precio);
                cadena.Append("|");
                cadena.Append(iva);
                cadena.Append("|");
                cadena.Append(remision.Rem_Total);
                if (remision.Rem_FechaHr == null)
                {
                cadena.Append("|");
                cadena.Append(remision.Rem_Fecha);
                }
                else
                {
                cadena.Append("|");
                cadena.Append(remision.Rem_FechaHr);
                }
                contador = 1;
                }
                string cadena_Final = cadena.ToString();*/

                StringWriter sw = new StringWriter();
                XmlTextWriter tx = new XmlTextWriter(sw);
                xml.WriteTo(tx);
                string xmlString = sw.ToString();

                WS_RemElectronicaCtaNacional.Service1 remelectronica = new WS_RemElectronicaCtaNacional.Service1();

                string sianRemisionElectronicaResult = remelectronica.Imprime(xmlString).ToString();

                byte[] PDFRemision = this.Base64ToByte(sianRemisionElectronicaResult);
                remision.PDF = PDFRemision;
                verificador = 0;
                new CN_CapRemision().ModificarRemision_PDF(remision, sesion.Emp_Cnx, ref verificador);

                string tempPDFname = string.Concat("REMISION_", remision.Id_Emp.ToString(), "_", remision.Id_Cd.ToString(), "_", remision.Id_U.ToString(), ".pdf");
                string URLtempPDF = Server.MapPath(string.Concat(System.Configuration.ConfigurationManager.AppSettings["URLtempPDF"].ToString(), tempPDFname));
                string WebURLtempPDF = string.Concat(System.Configuration.ConfigurationManager.AppSettings["WebURLtempPDF"].ToString(), tempPDFname);
                this.ByteToTempPDF(URLtempPDF, PDFRemision);
                RAM1.ResponseScripts.Add(string.Concat(@"AbrirFacturaPDF('", WebURLtempPDF, "')"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void ImprimirRemisionElectronicaPIPES(Remision remision)
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                List<RemisionDet> listaProdFacturaEspecialFinal = new List<RemisionDet>();

                remision.Rem_Estatus = "I";
                int verificador = 0;
                new CN_CapRemision().ModificarRemision_Estatus(remision, sesion.Emp_Cnx, ref verificador);

                new CN_CapRemision().ConsultaRemisionEspecialDetalle(ref listaProdFacturaEspecialFinal, sesion.Emp_Cnx, sesion.Id_Emp, sesion.Id_Cd_Ver, remision.Id_Rem, remision.Id_Cte);

                CN_CatCliente cn_catcliente = new CN_CatCliente();
                Clientes clientes = new Clientes();
                clientes.Id_Emp = sesion.Id_Emp;
                clientes.Id_Cd = sesion.Id_Cd_Ver;
                clientes.Id_Cte = remision.Id_Cte;
                cn_catcliente.ConsultaClientes(ref clientes, sesion.Emp_Cnx);

                int Id_Rem = remision.Id_Rem;
                Remision remision2 = new Remision();
                new CN_CapRemision().ConsultarEncabezadoImprimir(sesion, Id_Rem, ref remision2, 0);

                CN_CatCentroDistribucion cn_cd = new CN_CatCentroDistribucion();
                CentroDistribucion cd = new CentroDistribucion();
                double iva = 0;
                cn_cd.ConsultarIva(sesion.Id_Emp, sesion.Id_Cd_Ver, ref iva, sesion.Emp_Cnx);

                StringBuilder cadena = new StringBuilder();
                int contador = 0;
                foreach (RemisionDet d in listaProdFacturaEspecialFinal)
                {
                    if (contador > 0)
                        cadena.Append("|");
                    cadena.Append(remision.Id_Cd);
                    cadena.Append("|");
                    cadena.Append(d.Clp_Release.Replace("|", ""));
                    cadena.Append("|");
                    cadena.Append(remision.Id_Rem);
                    cadena.Append("|");
                    cadena.Append(remision.Rem_EstatusStr.Replace("|", ""));
                    cadena.Append("|");
                    cadena.Append(d.Producto.Id_Ter);
                    cadena.Append("|");
                    cadena.Append(ObtenerNombreTerritorio(sesion.Id_Emp, sesion.Id_Cd_Ver, d.Producto.Id_Ter, sesion.Emp_Cnx));
                    cadena.Append("|");
                    cadena.Append(remision.Id_Cte);
                    cadena.Append("|");
                    cadena.Append(remision.Cte_NomComercial.Replace("|", ""));
                    cadena.Append("|");
                    //DATOS FISCALES
                    cadena.Append(clientes.Cte_FacCalle.Replace("|", ""));
                    cadena.Append("|");
                    cadena.Append(clientes.Cte_FacNumero.Replace("|", ""));
                    cadena.Append("|");
                    cadena.Append(clientes.Cte_FacColonia.Replace("|", ""));
                    cadena.Append("|");
                    cadena.Append(clientes.Cte_FacMunicipio.Replace("|", ""));
                    cadena.Append("|");
                    cadena.Append(clientes.Cte_FacEstado.Replace("|", ""));
                    cadena.Append("|");
                    cadena.Append(clientes.Cte_FacCp.Replace("|", ""));
                    cadena.Append("|");
                    //DATOS DE CONSIGNACION
                    cadena.Append(remision2.Rem_Calle.Replace("|", ""));
                    cadena.Append("|");
                    cadena.Append(remision2.Rem_Numero.Replace("|", ""));
                    cadena.Append("|");
                    cadena.Append(remision2.Rem_Colonia.Replace("|", ""));
                    cadena.Append("|");
                    cadena.Append(remision2.Rem_Municipio.Replace("|", ""));
                    cadena.Append("|");
                    cadena.Append(remision2.Rem_Estado.Replace("|", ""));
                    cadena.Append("|");
                    cadena.Append(remision2.Rem_Cp.Replace("|", ""));
                    cadena.Append("|");
                    //PRODUCTOS
                    cadena.Append(d.Producto.Id_PrdEsp);
                    cadena.Append("|");
                    cadena.Append(d.Producto.Id_Prd);
                    cadena.Append("|");
                    cadena.Append(d.Producto.Prd_DescripcionEspecial.Replace("|", ""));
                    cadena.Append("|");
                    cadena.Append(d.Producto.Prd_Presentacion.Replace("|", ""));
                    cadena.Append("|");
                    cadena.Append(d.Producto.Prd_UniNs.Replace("|", ""));
                    cadena.Append("|");
                    cadena.Append(d.Rem_Cant);
                    cadena.Append("|");
                    cadena.Append(d.Rem_Precio);
                    cadena.Append("|");
                    cadena.Append(iva);
                    cadena.Append("|");
                    cadena.Append(remision.Rem_Total);
                    if (remision.Rem_FechaHr == null)
                    {
                        cadena.Append("|");
                        cadena.Append(remision.Rem_Fecha);
                    }
                    else
                    {
                        cadena.Append("|");
                        cadena.Append(remision.Rem_FechaHr);
                    }
                    contador = 1;
                }
                string cadena_Final = cadena.ToString();

                RemElectronica.Service1 remelectronica = new RemElectronica.Service1();

                string sianRemisionElectronicaResult = remelectronica.Imprime(cadena_Final).ToString();

                byte[] PDFRemision = this.Base64ToByte(sianRemisionElectronicaResult);
                remision.PDF = PDFRemision;
                verificador = 0;
                new CN_CapRemision().ModificarRemision_PDF(remision, sesion.Emp_Cnx, ref verificador);

                string tempPDFname = string.Concat("REMISION_", remision.Id_Emp.ToString(), "_", remision.Id_Cd.ToString(), "_", remision.Id_U.ToString(), ".pdf");
                string URLtempPDF = Server.MapPath(string.Concat(System.Configuration.ConfigurationManager.AppSettings["URLtempPDF"].ToString(), tempPDFname));
                string WebURLtempPDF = string.Concat(System.Configuration.ConfigurationManager.AppSettings["WebURLtempPDF"].ToString(), tempPDFname);
                this.ByteToTempPDF(URLtempPDF, PDFRemision);
                RAM1.ResponseScripts.Add(string.Concat(@"AbrirFacturaPDF('", WebURLtempPDF, "')"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private string ObtenerNombreTerritorio(int Id_Emp, int Id_Cd, int? Id_ter, string Conexion)
        {
            CN_CatTerritorios cn_catterritorio = new CN_CatTerritorios();
            Territorios terr = new Territorios();
            terr.Id_Emp = Id_Emp;
            terr.Id_Cd = Id_Cd;
            terr.Id_Ter = (int)Id_ter;
            cn_catterritorio.ConsultaTerritorio(ref terr, Conexion);
            return terr.Descripcion;
        }
        private void ByteToTempPDF(string tempPath, byte[] filebytes)
        {
            if (File.Exists(tempPath))
            {
                File.Delete(tempPath);
            }
            FileStream fs = new FileStream(tempPath,
                FileMode.CreateNew,
                FileAccess.Write,
                FileShare.None);
            fs.Write(filebytes, 0, filebytes.Length);
            fs.Close();
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
        private void ImprimirTransfer(ref GridCommandEventArgs e)
        {
            try
            {

                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                ArrayList ALValorParametrosInternos = new ArrayList();
                int Id_Tra = int.Parse(rgRecibidos.MasterTableView.Items[e.Item.ItemIndex]["Id_Tra"].Text);

                ////Consulta centro de distribución               
                ALValorParametrosInternos.Add(sesion.Emp_Cnx);
                ALValorParametrosInternos.Add(sesion.Id_Emp);
                ALValorParametrosInternos.Add(sesion.Id_Cd_Ver);
                ALValorParametrosInternos.Add(Id_Tra);
                ALValorParametrosInternos.Add(sesion.U_Nombre);

                Type instance = null;
                instance = typeof(LibreriaReportes.TransferenciaImp);
                Session["InternParameter_Values" + Session.SessionID + HF_Cve.Value] = ALValorParametrosInternos;
                Session["assembly" + Session.SessionID + HF_Cve.Value] = instance.AssemblyQualifiedName;


                RAM1.ResponseScripts.Add("AbrirReporteCve('" + HF_Cve.Value + "');");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void ImprimirEnviados()
        {
            try
            {
                StringBuilder tabla = new StringBuilder();
                tabla.Append("<html><head><meta http-equiv='Content-Type' content='text/html; charset=ISO-8859-1'></head><body><table style='width:700px'>");

                EscribeDetalleEnviados(ref tabla);
                tabla.Append("</table></body></html>");
                CN__Comun cn_comun = new CN__Comun();
                cn_comun.ExportarExcel("Transferencias_Enviadas", tabla.ToString());

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private void EscribeDetalleEnviados(ref StringBuilder Tabla)
        {
            try
            {
                String width;
                DataTable dt = getdtEnviado();

                Tabla.Append("<tr>");

                //lectura y escritura de columnas
                for (int i = 0; i < dt.Columns.Count; i++)
                {

                    if (dt.Columns[i].ColumnName == "Id_Rem")
                    {
                        width = (i == 0) ? "50px" : "70px";
                        Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:" + width + "'>");
                        Tabla.Append("Remisión");
                        Tabla.Append("</th>");
                    }
                    else if (dt.Columns[i].ColumnName == "Cte_NomComercial")
                    {
                        width = (i == 0) ? "300px" : "340px";
                        Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:" + width + "'>");
                        Tabla.Append("Sucursal");
                        Tabla.Append("</th>");
                    }
                    else if (dt.Columns[i].ColumnName == "Rem_Fecha")
                    {
                        width = (i == 0) ? "70px" : "90px";
                        Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:" + width + "'>");
                        Tabla.Append("Fecha");
                        Tabla.Append("</th>");
                    }
                    else if (dt.Columns[i].ColumnName == "Rem_Total")
                    {
                        width = (i == 0) ? "50px" : "70px";
                        Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:" + width + "'>");
                        Tabla.Append("Total");
                        Tabla.Append("</th>");
                    }
                    else if (dt.Columns[i].ColumnName == "Rem_EstatusStr")
                    {
                        width = (i == 0) ? "150px" : "190px";
                        Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:" + width + "'>");
                        Tabla.Append("Estatus");
                        Tabla.Append("</th>");
                    }



                }
                Tabla.Append("</tr>");
                // lectura y escritura de filas
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    Tabla.Append("<tr>");
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {

                        if (dt.Columns[i].ColumnName == "Id_Rem")
                        {
                            Tabla.Append("<td   style='text-align:center'>");
                            Tabla.Append(dt.Rows[j][i].ToString());
                            Tabla.Append("</td>");
                        }
                        else if (dt.Columns[i].ColumnName == "Cte_NomComercial")
                        {
                            Tabla.Append("<td   style='text-align:left '>");
                            Tabla.Append(dt.Rows[j][i].ToString());
                            Tabla.Append("</td>");
                        }
                        else if (dt.Columns[i].ColumnName == "Rem_Fecha")
                        {
                            DateTime d = Convert.ToDateTime(dt.Rows[j][i].ToString());

                            Tabla.Append("<td   style='text-align:center '>");
                            Tabla.Append(d.ToShortDateString());
                            Tabla.Append("</td>");
                        }
                        else if (dt.Columns[i].ColumnName == "Rem_Total")
                        {
                            Double valor = Convert.ToDouble(dt.Rows[j][i].ToString());

                            Tabla.Append("<td   style='text-align:right '>");
                            Tabla.Append(valor.ToString("N2"));
                            Tabla.Append("</td>");
                        }
                        else if (dt.Columns[i].ColumnName == "Rem_EstatusStr")
                        {


                            Tabla.Append("<td   style='text-align:left'>");
                            Tabla.Append(dt.Rows[j][i].ToString());
                            Tabla.Append("</td>");
                        }



                    }

                }

                Tabla.Append("</tr>");
                Tabla.Append("<tr>");
                Tabla.Append("<td>");
                Tabla.Append("&nbsp; &nbsp;</td>");
                Tabla.Append("</tr>");
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        private void ImprimirRecibidos()
        {
            try
            {
                StringBuilder tabla = new StringBuilder();
                tabla.Append("<html><head><meta http-equiv='Content-Type' content='text/html; charset=ISO-8859-1'></head><body><table style='width:700px'>");

                EscribeDetalleRecibidos(ref tabla);
                tabla.Append("</table></body></html>");
                CN__Comun cn_comun = new CN__Comun();
                cn_comun.ExportarExcel("Transferencias_Recibidas", tabla.ToString());
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private void EscribeDetalleRecibidos(ref StringBuilder Tabla)
        {
            try
            {
                String width;
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                List<TransferenciasAlm> List = new List<TransferenciasAlm>();
                DataTable dt = null;

                dt = getdtRecibido();

                Tabla.Append("<tr>");

                //lectura y escritura de columnas
                for (int i = 0; i < dt.Columns.Count; i++)
                {

                    if (dt.Columns[i].ColumnName == "Id_RemOrigen")
                    {
                        width = (i == 0) ? "50px" : "70px";
                        Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:" + width + "'>");
                        Tabla.Append("Remisión");
                        Tabla.Append("</th>");
                    }
                    else if (dt.Columns[i].ColumnName == "Cd_Nombre")
                    {
                        width = (i == 0) ? "200px" : "240px";
                        Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:" + width + "'>");
                        Tabla.Append("Sucursal");
                        Tabla.Append("</th>");
                    }
                    else if (dt.Columns[i].ColumnName == "Tra_RemFecha")
                    {
                        width = (i == 0) ? "70px" : "90px";
                        Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:" + width + "'>");
                        Tabla.Append("Fecha remisión");
                        Tabla.Append("</th>");
                    }
                    else if (dt.Columns[i].ColumnName == "Tra_FechaEnvio")
                    {
                        width = (i == 0) ? "70px" : "90px";
                        Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:" + width + "'>");
                        Tabla.Append("Fecha envió");
                        Tabla.Append("</th>");
                    }
                    else if (dt.Columns[i].ColumnName == "Tra_FechaRecepcion")
                    {
                        width = (i == 0) ? "70px" : "90px";
                        Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:" + width + "'>");
                        Tabla.Append("Fecha recepción");
                        Tabla.Append("</th>");
                    }
                    else if (dt.Columns[i].ColumnName == "Id_Es")
                    {
                        width = (i == 0) ? "50px" : "70px";
                        Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:" + width + "'>");
                        Tabla.Append("Entrada");
                        Tabla.Append("</th>");
                    }
                    else if (dt.Columns[i].ColumnName == "Tra_EstatusStr")
                    {
                        width = (i == 0) ? "150px" : "190px";
                        Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:" + width + "'>");
                        Tabla.Append("Estatus");
                        Tabla.Append("</th>");
                    }

                }
                Tabla.Append("</tr>");
                // lectura y escritura de filas
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    Tabla.Append("<tr>");
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {

                        if (dt.Columns[i].ColumnName == "Id_RemOrigen")
                        {
                            Tabla.Append("<td   style='text-align:center'>");
                            Tabla.Append(dt.Rows[j][i].ToString());
                            Tabla.Append("</td>");
                        }
                        else if (dt.Columns[i].ColumnName == "Cd_Nombre")
                        {
                            Tabla.Append("<td   style='text-align:left '>");
                            Tabla.Append(dt.Rows[j][i].ToString());
                            Tabla.Append("</td>");
                        }
                        else if (dt.Columns[i].ColumnName == "Tra_RemFecha")
                        {
                            DateTime d = Convert.ToDateTime(dt.Rows[j][i].ToString());

                            Tabla.Append("<td   style='text-align:center '>");
                            Tabla.Append(d.ToShortDateString());
                            Tabla.Append("</td>");
                        }
                        else if (dt.Columns[i].ColumnName == "Tra_FechaEnvio")
                        {
                            DateTime d = Convert.ToDateTime(dt.Rows[j][i].ToString());

                            Tabla.Append("<td   style='text-align:center '>");
                            Tabla.Append(d.ToShortDateString());
                            Tabla.Append("</td>");
                        }
                        else if (dt.Columns[i].ColumnName == "Tra_FechaRecepcion")
                        {
                            DateTime? d = dt.Rows[j][i].ToString() == "" ? (DateTime?)null : Convert.ToDateTime(dt.Rows[j][i].ToString());

                            Tabla.Append("<td   style='text-align:center '>");
                            Tabla.Append(d.ToString() == "" ? "" : DateTime.Parse(d.ToString()).ToShortDateString());
                            Tabla.Append("</td>");
                        }

                        else if (dt.Columns[i].ColumnName == "Id_Es")
                        {
                            Tabla.Append("<td   style='text-align:Center '>");
                            Tabla.Append(dt.Rows[j][i].ToString());
                            Tabla.Append("</td>");
                        }
                        else if (dt.Columns[i].ColumnName == "Tra_EstatusStr")
                        {

                            Tabla.Append("<td   style='text-align:left'>");
                            Tabla.Append(dt.Rows[j][i].ToString());
                            Tabla.Append("</td>");
                        }



                    }

                }

                Tabla.Append("</tr>");
                Tabla.Append("<tr>");
                Tabla.Append("<td>");
                Tabla.Append("&nbsp; &nbsp;</td>");
                Tabla.Append("</tr>");
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        #endregion Funciones
        #region ErrorManager

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

        private void DisplayMensajeAlerta(string mensaje)
        {
            if (mensaje.Contains("Page_Load_error"))
                Alerta("Error al cargar la página");
            else
                if (mensaje.Contains("Impresion_error"))
                    Alerta("Error al momento de imprimir");
                else
                    Alerta(string.Concat("No se pudo realizar la operación solicitada.<br/>", mensaje.Replace("'", "\"")));
        }
        #endregion


    }
}