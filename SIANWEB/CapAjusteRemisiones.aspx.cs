using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaEntidad;
using CapaNegocios;
using CapaDatos;
using Telerik.Web.UI;
using System.Data;


namespace SIANWEB
{
    public partial class CapAjusteRemisiones : System.Web.UI.Page
    {


        private bool _PermisoGuardar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoModificar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoEliminar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoImprimir { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private List<RemisionDet> List_Saldo
        {
            get { return (List<RemisionDet>)Session["DetalleSaldo" + Session.SessionID]; }
            set { Session["DetalleSaldo" + Session.SessionID] = value; }
        }


        private List<DevolucionRemisionDet> List_Historico
        {
            get { return (List<DevolucionRemisionDet>)Session["DetalleHistorico" + Session.SessionID]; }
            set { Session["DetalleHistorico" + Session.SessionID] = value; }
        }

        private List<Remision> ListaRemisionesFactura
        {
            get { return (List<Remision>)Session["ListaDevolucionRemisionesFactura" + Session.SessionID]; }
            set { Session["ListaDevolucionRemisionesFactura" + Session.SessionID] = value; }
        }

        public DataTable dt { get; set; }




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
                        //this.ValidarPermisos();
                        if (Sesion.Cu_Modif_Pass_Voluntario == false)
                        {
                            RAM1.ResponseScripts.Add("AbrirContrasenas(); return false;");
                            return;
                        }

                        CargarEstatus();
                        CargarTipoMovimiento();

                        string Modulo = Request.QueryString["Modulo"] == null ? "1" : Request.QueryString["Modulo"];
                        hiddenId.Value = Request.QueryString["Id_Folio"] == null ? "-1" : Request.QueryString["Id_Folio"];
                        

                        if (hiddenId.Value != "-1")
                        {
                            hiddenId.Value = Request.QueryString["Id_Folio"];
                           
                            this.Inicializar(Convert.ToInt32(hiddenId.Value));
                        

                        }
                        else
                        {
                            txtSolicitud.Text = MaximoId("CapEntsalAjuste", "Id_Es");                            
                            dpFecha1.DbSelectedDate = DateTime.Now;
                        }



                        double ancho = 0;
                        foreach (GridColumn gc in rgDevParcial.Columns)
                        {
                            if (gc.Display)
                            {
                                ancho = ancho + gc.HeaderStyle.Width.Value +10;
                            }
                        }
                        rgDevParcial.Width = Unit.Pixel(Convert.ToInt32(ancho));
                        rgDevParcial.MasterTableView.Width = Unit.Pixel(Convert.ToInt32(ancho));
                        rgDevParcial.Rebind();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }

        }


        protected void BtnFacturar_Click(object sender, EventArgs e)
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];

                List<Remision> lRemisiones = new List<Remision>();
                if (txtNumCliente.Text.Trim() == "")
                {
                    Alerta("Es necesario seleccionar Cliente");
                    return;
                }

                if (txtTerritorio.Text.Trim() == "")
                {
                    Alerta("Es necesario seleccionar Territorio");
                    return;
                }

                if (txtTipoId.Text.Trim() == "")
                {
                    Alerta("Es necesario seleccionar Tipo de Movimiento");
                    return;
                }

                List<RemisionDet> lcompare = new List<RemisionDet>();

                lcompare = GetListSaldo();
                bool flagDif = false;
                if (rgDevParcial.MasterTableView.Items.Count != lcompare.Count())
                {
                    flagDif = true;
                }

                foreach (GridDataItem item in rgDevParcial.MasterTableView.Items)
                {
                    foreach (RemisionDet compare in lcompare)
                    {
                        if (Convert.ToInt32(item["Id_Prd"].Text) == compare.Id_Prd)
                        {
                            if (Convert.ToInt32(item["Rem_Cant"].Text) != compare.Rem_Cant)
                            {
                                flagDif = true;
                            }
                        }
                    }
                }


                if (flagDif)
                {
                    Alerta("El Saldo ha sido actualizado debido a que otro usuario esta realizando una transacción en este momento para este cliente y tipo de movimiento, es necesario volver a capturar la devolución");
                    List_Saldo = GetListSaldo();
                    rgDevParcial.Rebind();
                    return;

                }


                bool ProductosSeleccionados = false;

                foreach (GridDataItem item in rgDevParcial.MasterTableView.Items)
                {
                    if (((RadNumericTextBox)item.FindControl("NumCantDevuelta")).Text.Trim() != "" &&
                        ((RadNumericTextBox)item.FindControl("NumCantDevuelta")).Text.Trim() != "0")
                    {
                        ProductosSeleccionados = true;
                    }
                }

                if (!ProductosSeleccionados)
                {
                    Alerta("Es necesario agregar la cantidad a facturar en al menos un producto");
                    return;
                }


                foreach (GridDataItem item in rgDevParcial.MasterTableView.Items)
                {
                    if (((RadNumericTextBox)item.FindControl("NumCantDevuelta")).Text.Trim() != "" &&
                        ((RadNumericTextBox)item.FindControl("NumCantDevuelta")).Text.Trim() != "0")
                    {
                        Remision remision = new Remision();
                        remision.Id_Cte = Convert.ToInt32(txtNumCliente.Text);
                        remision.Id_Ter = Convert.ToInt32(txtTerritorio.Text);
                        remision.Cte_NomComercial = txtCliente.Text;
                        remision.Id_Prd = Convert.ToInt32(item["Id_Prd"].Text);
                        remision.Id_Tm = Convert.ToInt32(txtTipoId.Text);
                        remision.Cant = Convert.ToInt32(((RadNumericTextBox)item.FindControl("NumCantDevuelta")).Text.Trim());
                        lRemisiones.Add(remision);
                    }
                }

                ListaRemisionesFactura = lRemisiones;
                Session["DevolucionRemision" + Session.SessionID] = true;
                string PermisoGuardar = "1";
                string PermisoModificar = "1";
                string PermisoEliminar = "1";
                string PermisoImprimir = "1";

                RAM1.ResponseScripts.Add("return CloseWindowRem()");

            }
            catch (Exception ex)
            {
                ErrorManager(ex, "BtnAutorizar_Click");
            }
        }


        protected void BtnDevolucion_Click(object sender, EventArgs e)
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                DevolucionRemision dR = new DevolucionRemision();
                List<RemisionDet> lRemision = new List<RemisionDet>();
                List<RemisionDet> lRemisionFinal = new List<RemisionDet>();

                if (txtNumCliente.Text.Trim() == "")
                {
                    Alerta("Es necesario seleccionar Cliente");
                    return;
                }

                if (txtTerritorio.Text.Trim() == "")
                {
                    Alerta("Es necesario seleccionar Territorio");
                    return;
                }

                if (txtTipoId.Text.Trim() == "")
                {
                    Alerta("Es necesario seleccionar Tipo de Movimiento");
                    return;
                }
                
                bool ProductosSeleccionados = false;
                foreach (GridDataItem item in rgDevParcial.MasterTableView.Items)
                {
                    if (((RadNumericTextBox)item.FindControl("NumCantDevuelta")).Text.Trim() != "" &&
                        ((RadNumericTextBox)item.FindControl("NumCantDevuelta")).Text.Trim() != "0")
                    {
                        ProductosSeleccionados = true;
                    }
                }

                if (!ProductosSeleccionados)
                {
                    Alerta("Es necesario agregar la cantidad a devolver en al menos un producto");
                    return;
                }

                CN_CapDevolucionRemision clsCapDevolucionRemision = new CN_CapDevolucionRemision();
               
               int lastRemision  = 0;             
               string verificadorStr = "";
               List<EntradaSalidaDetalle> lEsDetalle = null;
               EntradaSalida entsal = null;
               int count = 0;
               int total = rgDevParcial.MasterTableView.Items.Count;
                foreach (GridDataItem item in rgDevParcial.MasterTableView.Items)
                {

                    if (lastRemision != Convert.ToInt32(item["Id_Rem"].Text) && lastRemision != 0 && entsal != null && lEsDetalle.Count() > 0)
                     {
                        entsal.ListaDetalle = lEsDetalle;
                        clsCapDevolucionRemision.GuardarEntradaSalidaAjuste(entsal, ref verificadorStr, Sesion.Emp_Cnx);
                     }


                    if (lastRemision == 0 ||lastRemision != Convert.ToInt32(item["Id_Rem"].Text))
                    {
                        lEsDetalle = new List<EntradaSalidaDetalle>();
                    }


                    if (((RadNumericTextBox)item.FindControl("NumCantDevuelta")).Text.Trim() != "" &&
                        ((RadNumericTextBox)item.FindControl("NumCantDevuelta")).Text.Trim() != "0")
                    {                    

                        entsal = new EntradaSalida();
                        entsal.Id_Emp = Sesion.Id_Emp;
                        entsal.Id_Cd = Sesion.Id_Cd_Ver;
                        entsal.Id_U = Sesion.Id_U;
                        entsal.Id_Es = Int32.Parse(MaximoId("CapEntsalAjuste", "Id_Es"));
                        entsal.Es_Naturaleza = 0;
                        entsal.Es_Fecha = DateTime.Now;
                        entsal.Id_Tm = clsCapDevolucionRemision.ConsultaMovInverso(Sesion.Id_Emp, Convert.ToInt32(txtTipoId.Text), Sesion.Emp_Cnx);
                        entsal.Id_Pvd = -1;
                        entsal.Id_Cte = Convert.ToInt32(txtNumCliente.Text);
                        entsal.Es_Referencia = item["Id_Rem"].Text;
                        entsal.Es_Notas = "Movimiento por proceso de devolución de remisiones, número de devolución:" + txtSolicitud.Text;
                        entsal.Es_Estatus = "C";
                        entsal.ManAut = true;
                        entsal.Id_Ter = Convert.ToInt32(txtTerritorio.Text);
                        entsal.Es_CteCuentaNacional = -1;
                        entsal.Es_CteCuentaContNacional = 0;
                        entsal.Id_Cte = Convert.ToInt32(txtNumCliente.Text);                                              
                        
                        
                            EntradaSalidaDetalle vpep = new EntradaSalidaDetalle();
                            vpep.Id_Emp = Sesion.Id_Emp;
                            vpep.Id_Cd = Sesion.Id_Cd_Ver;
                            vpep.Id_Es = entsal.Id_Es;
                            vpep.Id_Prd = Convert.ToInt32(item["Id_Prd"].Text);
                            vpep.Es_Cantidad = Convert.ToInt32(((RadNumericTextBox)item.FindControl("NumCantDevuelta")).Text.Trim());
                            vpep.Es_Costo = 0;
                            lEsDetalle.Add(vpep);                                                       
                                     
                    }

                    lastRemision = Convert.ToInt32(item["Id_Rem"].Text);

                    count++;

                    if (count == total  &&  entsal != null && lEsDetalle.Count() > 0)
                    {
                        entsal.ListaDetalle = lEsDetalle;
                        clsCapDevolucionRemision.GuardarEntradaSalidaAjuste(entsal, ref verificadorStr, Sesion.Emp_Cnx);
                    }


                }             
               
                if (verificadorStr == "0")
                {                                  
                    List_Saldo = GetListSaldo();
                    Alerta("El ajuste se ha Relizado Correctamente");
                    rgDevParcial.Rebind();                 
                   
                }
                else
                {
                    Alerta("Error al aplicar devolución");
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "BtnAutorizar_Click");
            }
        }


        private void CargarComboTerritorios()
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                int cliente = !string.IsNullOrEmpty(txtNumCliente.Text) ? Convert.ToInt32(txtNumCliente.Text.ToString()) : -1;

                List<Territorios> listaTerritorios = new List<Territorios>();
                new CN_CatCliente().ConsultaTodosTerritoriosDelCliente(cliente, sesion, ref listaTerritorios);
                cmbTerritorio.DataTextField = "Descripcion";
                cmbTerritorio.DataValueField = "Id_Ter";
                cmbTerritorio.DataSource = listaTerritorios;
                cmbTerritorio.DataBind();

                if (cmbTerritorio.Items.Count > 1)
                {
                    cmbTerritorio.SelectedIndex = 1;
                    cmbTerritorio.Text = cmbTerritorio.Items[1].Text;
                    txtTerritorio.Text = cmbTerritorio.Items[1].Value;

                    CN_CatTerritorios territorio = new CN_CatTerritorios();
                    Territorios ter = new Territorios();
                    ter.Id_Emp = sesion.Id_Emp;
                    ter.Id_Cd = sesion.Id_Cd_Ver;
                    ter.Id_Ter = Convert.ToInt32(cmbTerritorio.Items[1].Value);
                    territorio.ConsultaTerritoriosCombo(ref ter, sesion.Emp_Cnx);

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }




        protected void txtCliente_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ErrorManager();
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];



                Clientes cliente = new Clientes();
                cliente.Id_Emp = sesion.Id_Emp;
                cliente.Id_Cd = sesion.Id_Cd_Ver;
                cliente.Id_Cte = Convert.ToInt32((sender as RadNumericTextBox).Value.HasValue ? (sender as RadNumericTextBox).Value.Value : -1);
                new CN_CatCliente().ConsultaClientes(ref cliente, sesion.Emp_Cnx);
                txtCliente.Text = cliente.Cte_NomComercial;
                CargarComboTerritorios();


                List_Saldo = GetListSaldo();
                rgDevParcial.Rebind();
                txtTerritorio.Focus();

            }
            catch (Exception ex)
            {
                Alerta(ex.Message);
                txtCliente.Text = "";
                txtNumCliente.Text = "";
                txtTerritorio.Text = "";
                cmbTerritorio.SelectedIndex = -1;

            }
        }



        protected void txtTipoId_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ErrorManager();
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];

                if (txtNumCliente.Text != string.Empty && (cmbTerritorio.SelectedValue != "-1" && cmbTerritorio.SelectedValue != string.Empty))
                {


                    List_Saldo = GetListSaldo();
                    rgDevParcial.Rebind();
                    txtTerritorio.Focus();
                    this.rgDevParcial.Enabled = true;
                }

            }
            catch (Exception ex)
            {
                Alerta(ex.Message);
            }
        }


        private void CargarTipoMovimiento()
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CN_Comun.LlenaCombo(Sesion.Id_Emp, Sesion.Emp_Cnx, "spCatMovimiento_ComboParaRemisionesDevolucion", ref cmbTipoMov);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void cmbTerritorio_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                ErrorManager();
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                if ((txtCliente.Text != string.Empty) && (this.cmbTipoMov.SelectedValue != "-1" && this.cmbTipoMov.SelectedValue != string.Empty)
                        && (cmbTerritorio.SelectedValue != "-1" && cmbTerritorio.SelectedValue != string.Empty))
                {
                    this.rgDevParcial.Enabled = true;

                }
                else
                {
                    this.rgDevParcial.Enabled = false;
                }

                CN_CatTerritorios territorio = new CN_CatTerritorios();
                Territorios ter = new Territorios();
                ter.Id_Emp = sesion.Id_Emp;
                ter.Id_Cd = sesion.Id_Cd_Ver;
                ter.Id_Ter = Convert.ToInt32(cmbTerritorio.SelectedValue);
                territorio.ConsultaTerritoriosCombo(ref ter, sesion.Emp_Cnx);


                List_Saldo = GetListSaldo();
                rgDevParcial.Rebind();

            }
            catch (Exception ex)
            {
                this.Alerta(string.Concat(ex.Message));
            }
        }





        protected void NumCantDevuelta_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                ErrorManager();
                RadNumericTextBox combo = (RadNumericTextBox)sender;
                int CantidadDevolver = combo.Value.HasValue ? Int32.Parse(combo.Text) : 0;

                GridDataItem dataItem = combo.Parent.Parent as GridDataItem;

                if (Int32.Parse(dataItem["Rem_Cant"].Text) < CantidadDevolver)
                {
                    combo.Value = 0;
                    Alerta("No se devolver una cantidad Mayor al saldo del producto ");
                }

            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void cmbMov_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                ErrorManager();
                rgDevParcial.Rebind();

            }
            catch (Exception ex)
            {
                this.Alerta(ex.Message);
            }
        }


        private void Inicializar(int Id_Folio)
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                DevolucionRemision v = new DevolucionRemision();
                v.Id_Emp = Sesion.Id_Emp;
                v.Id_Cd = Sesion.Id_Cd;
                v.Id_DevRem = Id_Folio;

                new CN_CapDevolucionRemision().Consulta(ref v, Sesion.Emp_Cnx, Sesion.Id_Emp, Sesion.Id_Cd_Ver, Id_Folio);

                string Estatus = v.Estatus.Trim();

                

                dpFecha1.SelectedDate = v.DevRem_Fecha;
                cmbEstatus.SelectedValue = v.Estatus.Trim();

                txtNumCliente.Text = v.Id_Cte.ToString();
                txtCliente.Text = v.DevRem_CteNombre;
                txtTerritorio.Text = v.Id_Ter.ToString();
                txtSolicitud.Text = v.Id_DevRem.ToString();
                txtTipoId.Text = v.Id_Tm.ToString();

                CargarComboTerritorios();
                try
                {
                    cmbTipoMov.SelectedIndex = cmbTipoMov.FindItemIndexByValue(v.Id_Tm.ToString());
                    cmbTipoMov.Text = cmbTipoMov.FindItemByValue(v.Id_Tm.ToString()).Text;
                }
                catch
                {
                }

              
              

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        private List<RemisionDet> GetListSaldo()
        {
            try
            {
                List<RemisionDet> List = new List<RemisionDet>();
                CN_CapDevolucionRemision clsCapDevolucionRemision = new CN_CapDevolucionRemision();
                Sesion session2 = new Sesion();
                session2 = (Sesion)Session["Sesion" + Session.SessionID];
                DevolucionRemision rd = new DevolucionRemision();
                rd.Id_Emp = session2.Id_Emp;
                rd.Id_Cd = session2.Id_Cd_Ver;
                rd.Id_Cte = Convert.ToInt32(txtNumCliente.Text);
                rd.Id_Ter = txtTerritorio.Value.HasValue ? (int)txtTerritorio.Value.Value : -1;
                rd.Id_Tm = Convert.ToInt32(cmbTipoMov.SelectedValue);

                clsCapDevolucionRemision.ConsultaRemisionProductoSaldoDetalleTotal(rd, session2.Emp_Cnx, ref List);
                if (List.Count == 0)
                {
                    

                }
                return List;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private List<DevolucionRemisionDet> GetListDetalleHistorico()
        {
            try
            {
                List<DevolucionRemisionDet> List = new List<DevolucionRemisionDet>();
                CN_CapDevolucionRemision clsCapDevolucionRemision = new CN_CapDevolucionRemision();
                Sesion session2 = new Sesion();
                session2 = (Sesion)Session["Sesion" + Session.SessionID];
                DevolucionRemision dr = new DevolucionRemision();
                dr.Id_Emp = session2.Id_Emp;
                dr.Id_Cd = session2.Id_Cd_Ver;
                dr.Id_DevRem = (int)txtSolicitud.Value.Value;


                clsCapDevolucionRemision.ConsultaDevolucionHistorico(dr, session2.Emp_Cnx, ref List);
                if (List.Count == 0)
                {
                    // BtnDevolucion.Enabled = false;

                }
                return List;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        protected void rg1_ItemCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                Sesion session2 = new Sesion();
                session2 = (Sesion)Session["Sesion" + Session.SessionID];
                DateTime fechaPeriodoInicio = session2.CalendarioIni;
                DateTime fechaPeriodoFinal = session2.CalendarioFin;
                GridItem gi = e.Item;
                //List<string> statusPosibles;
                switch (e.CommandName.ToLower())
                {
                    case "editar":

                        editar(gi);
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
            try
            {
                string cmd = e.Argument.ToString();
                switch (cmd)
                {
                    case "RebindGrid":
                        rgDevParcial.Rebind();
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "RAM1_AjaxRequest");
            }
        }



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
                //Esta clave depende de la pantalla

                CapaDatos.CD_PermisosU CN_PermisosU = new CapaDatos.CD_PermisosU();
                CN_PermisosU.ValidaPermisosUsuario(ref Permiso, Sesion.Emp_Cnx);

                if (Permiso.PAccesar == true)
                {
                    /*  _PermisoGuardar = Permiso.PGrabar;
                      _PermisoModificar = Permiso.PModificar;
                      _PermisoEliminar = Permiso.PEliminar;
                      _PermisoImprimir = Permiso.PImprimir;*/

                    if (Permiso.PGrabar == false)
                        this.rtb1.Items[1].Visible = false;
                }
                else
                    Response.Redirect("Inicio.aspx");



            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void CargarEstatus()
        {
            cmbEstatus.Items.Add(new RadComboBoxItem("-- Todos --", ""));
            cmbEstatus.Items.Add(new RadComboBoxItem("Capturada", "C"));
            cmbEstatus.Items.Add(new RadComboBoxItem("Cancelada", "B"));
        }

        protected void rgDevParcial_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                {
                    rgDevParcial.DataSource = List_Saldo;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }


       




        private int CambiarEstatus(int Id_Env, string estatus)
        {
            try
            {
                Sesion session = new Sesion();
                session = (Sesion)Session["Sesion" + Session.SessionID];
                CN_EntradaVirtual CN_EntradaVirtual = new CN_EntradaVirtual();
                EntradaVirtual ape = new EntradaVirtual();
                ape.Id_Emp = session.Id_Emp;
                ape.Id_Cd = session.Id_Cd_Ver;
                ape.Id_Env = Id_Env;
                ape.Env_Estatus = estatus;
                int verificador = -1;
                CN_EntradaVirtual.EnviarEntradaVirtual(ape, session.Emp_Cnx, ref verificador);
                return verificador;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void editar(GridItem gi)
        {
            try
            {
                /* if (_PermisoModificar)*/
                RAM1.ResponseScripts.Add("return AbrirVentana_EntradaVirtual('" + gi.Cells[2].Text + "', 3)");

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string MaximoId(string nomTabla, string nomColumna)
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                return CN_Comun.Maximo(Sesion.Id_Emp, Sesion.Id_Cd_Ver, nomTabla, nomColumna, Sesion.Emp_Cnx, "spCatLocal_Maximo");
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

    }
}