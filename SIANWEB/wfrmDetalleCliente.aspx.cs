using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaEntidad;
using CapaNegocios;
using System.Text;
using System.Data;
using CapaDatos;
using Telerik.Web.UI;

namespace SIANWEB
{
    public partial class wfrmDetalleCliente : System.Web.UI.Page
    {
        #region Variables
        private bool _PermisoGuardar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoModificar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoEliminar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoImprimir { get { return (bool)Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }

        public Sesion session
        {
            get
            {
                return (Sesion)Session["Sesion" + Session.SessionID];
            }
            set
            {
                Session["Sesion" + Session.SessionID] = value;

            }
        }
        #endregion
        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (ValidarSesion())
                {
                    if (!Page.IsPostBack)
                    {
                        ValidarPermisos();

                        if (session.Cu_Modif_Pass_Voluntario == false)
                        { //RAM1.ResponseScripts.Add("AbrirContrasenas(); return false;");
                            return;
                        }
                        //CargarCentros();
                        Inicializar();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void txt_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (_PermisoModificar)
                {
                    Guardar(0);
                    RadAjaxManager1.ResponseScripts.Add("setFocus();");
                }
                else
                {
                    Alerta("No tiene permisos para modificar");
                    Inicializar();
                    return;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void ibtnRegresar_Click(object sender, EventArgs e)
        {
            try
            {
                ErrorManager();
                Response.Redirect("wfrmPrincipalClientes.aspx");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void ibtnGuardaPotencial_Click(object sender, EventArgs e)
        {
            try
            {
                ErrorManager();
                if (_PermisoModificar)
                {
                    Guardar(1);
                    ibtnRegresar_Click(null, null);
                }
                else
                {
                    if (!_PermisoModificar)
                    {
                        Alerta("No tiene permisos para modificar");
                    }
                    
                    return;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        private void Guardar(int validador)
        {
            ActualizarValorPotencialCliente();
            int result = 0;
            if (Int32.TryParse(txtValor.Text, out result))
            {
                if (txtValor.Text != lblDim.Text)
                {
                    CN_CatCliente cn_catacliente = new CN_CatCliente();
                    Clientes cte = new Clientes();
                    cte.Id_Emp = session.Id_Emp;
                    cte.Id_Cd = session.Id_Cd_Ver;
                    cte.Id_Terr = Convert.ToInt32(lblTer.Text);
                    cte.Id_Cte = Convert.ToInt32(lblCte.Text);
                    int verificador = 0;
                    Funciones funcion = new Funciones();
                    if (_PermisoModificar)
                    {
                        cn_catacliente.ActualizaDimension(cte, (int)txtValor.Value, txtFactor.Value, funcion.GetLocalDateTime(session.Minutos), ref verificador, session.Emp_Cnx);
                        txtValorPT.Text = (Convert.ToDouble(txtFactor.Text) * Convert.ToDouble(txtValor.Text)).ToString("$ #,##0.00");
                        //if (validador == 0)
                        //Alerta("Registro actualizado correctamente");
                    }
                    else
                    {
                        Alerta("No tiene permiso para modificar");
                    }
                }
                switch (lblSeg.Text)
                {
                    case "11":
                    case "14":
                    case "21":
                    case "22":
                        LeerEstructuraSegmento(Convert.ToInt32(lblCte.Text), Convert.ToInt32(lblSeg.Text));
                        break;
                    default:
                        LeerEstructura(Convert.ToInt32(lblCte.Text), Convert.ToInt32(lblSeg.Text));
                        break;
                }
            }
        }
        #endregion
        #region Funciones
        private void Inicializar()
        {
            try
            { //txtClave.Text = Valor;
                Clientes cte = new Clientes();
                CN_CatCliente cn_catcliente = new CN_CatCliente();
                cte.Id_Emp = session.Id_Emp;
                cte.Id_Cd = session.Id_Cd_Ver;
                cte.Id_Cte = Convert.ToInt32(Request.QueryString["ID"]);
                cte.Id_Terr = Convert.ToInt32(Request.QueryString["Ter"]);
                cn_catcliente.ConsultaClienteTerritorio(ref cte, session.Emp_Cnx);
                txtCliente.Text = cte.Cte_NomComercial;

                txtUEN.Text = cte.Uen_Descripcion;
                txtSegmento.Text = cte.Seg_Descripcion;
                txtTerritorio.Text = cte.Ter_Nombre;

                txtUnidadDimension.Text = cte.Seg_Unidades;
                txtFactor.Text = cte.Cte_Dimension.ToString();
                txtValor.Text = cte.Seg_ValUniDim.ToString();

                txtValorPO.Text = "0";
                txtValorPT.Text = cte.VPTeorico.ToString("$ #,##0.00");

                lblCte.Text = cte.Id_Cte.ToString();
                lblSeg.Text = cte.Id_Seg.ToString();
                lblTer.Text = cte.Id_Terr.ToString();

                imgContactos.PostBackUrl = "wfrmDetalleClientesContactos.aspx?ID=" + cte.Id_Cte.ToString() + "&Seg=" + cte.Id_Seg.ToString() + "&Ter=" + cte.Id_Terr.ToString();

                switch (cte.Id_Seg)
                {
                    case 11:
                    case 14:
                    case 21:
                    case 22: 
                LeerEstructuraSegmento(cte.Id_Cte, cte.Id_Seg);
                break;
                    default:
                LeerEstructura(cte.Id_Cte, cte.Id_Seg);
                break;
                }

                if (session.Id_TU != 2)
                {
                    txtCliente.Enabled = false;
                    txtFactor.Enabled = false;
                    txtSegmento.Enabled = false;
                    txtTerritorio.Enabled = false;
                    txtUEN.Enabled = false;
                    txtUnidadDimension.Enabled = false;
                    txtValor.Enabled = false;
                    txtValorPO.Enabled = false;
                    txtValorPT.Enabled = false;
                    ibtnGuardaPotencial.Visible = false;

                    for (int i = 0; i < DataGrid1.Items.Count; i++)
                    {
                        if (i == 0)
                            (DataGrid1.Items[i].Cells[6].FindControl("txt") as RadNumericTextBox).Enabled = false;
                        else
                            (DataGrid1.Items[i].Cells[1].FindControl("txt") as RadNumericTextBox).Enabled = false;
                    }
                }
                else
                {
                    txtCliente.Enabled = true;
                    txtFactor.Enabled = true;
                    txtSegmento.Enabled = true;
                    txtTerritorio.Enabled = true;
                    txtUEN.Enabled = true;
                    txtUnidadDimension.Enabled = true;
                    txtValor.Enabled = true;
                    txtValorPO.Enabled = true;
                    txtValorPT.Enabled = true;
                    ibtnGuardaPotencial.Visible = true;

                    for (int i = 0; i < DataGrid1.Items.Count; i++)
                    {
                        if (i == 0)
                            (DataGrid1.Items[i].Cells[6].FindControl("txt") as RadNumericTextBox).Enabled = true;
                        else
                            (DataGrid1.Items[i].Cells[1].FindControl("txt") as RadNumericTextBox).Enabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void LeerEstructura(int? Id_Cte, int? Id_Seg)
        {
            try
            {
                int vTotalGralAreas = 0;
                DataSet dsEstructuraSegmento = new DataSet();
                CN_CatCliente cn_catcliente = new CN_CatCliente();
                Clientes cte = new Clientes();
                cte.Id_Emp = session.Id_Emp;
                cte.Id_Cd = session.Id_Cd_Ver;
                cte.Id_Cte = Id_Cte;
                cte.Id_Seg = Id_Seg;
                cn_catcliente.EstructuraSegmento(ref dsEstructuraSegmento, cte, session.Emp_Cnx);
                DataTable dtTotalAreas = new DataTable();
                DataTable dtTotalSoluciones = new DataTable();

                DataGrid1.DataSource = dsEstructuraSegmento.Tables[0];
                DataGrid1.DataBind();
                dg2.DataSource = dsEstructuraSegmento.Tables[0];
                dg2.DataBind();

                dtTotalAreas = dsEstructuraSegmento.Tables[1];
                dtTotalSoluciones = dsEstructuraSegmento.Tables[2];

                if (DataGrid1.Items.Count == 0)
                { return; }


                this.DataGrid1.Items[0].Cells[0].RowSpan = this.DataGrid1.Items.Count;
                this.DataGrid1.Items[0].Cells[1].RowSpan = this.DataGrid1.Items.Count;

                vTotalGralAreas = dtTotalAreas.Rows.Count;

                for (int i = 1; i <= this.DataGrid1.Items.Count - 1; i++)
                {
                    if (i == 1)
                        this.DataGrid1.Items[0].Cells[5].Text = (Convert.ToDouble(this.DataGrid1.Items[0].Cells[5].Text) * Convert.ToDouble(this.txtValor.Text) * Convert.ToDouble(this.txtFactor.Text)).ToString("$ #,##0.00");

                    this.DataGrid1.Items[i].Cells[5].Text = (Convert.ToDouble(this.DataGrid1.Items[i].Cells[5].Text) * Convert.ToDouble(this.txtValor.Text) * Convert.ToDouble(this.txtFactor.Text)).ToString("$ #,##0.00");
                    this.DataGrid1.Items[i].Cells.RemoveAt(0);
                    this.DataGrid1.Items[i].Cells.RemoveAt(0);
                }

                int totSoluciones = 0;
                int vSolucionAnterior = 0;
                int totAnterior = 0;
                int totAreas = 0;
                int vAreaAnterior = 0;
                int totAnteriorA = 0;

                //AREAS
                for (int i = 0; i <= dtTotalAreas.Rows.Count - 1; i++)
                {
                    if (i == 0)
                    {
                        if (vAreaAnterior != (int)dtTotalAreas.Rows[i]["AreaID"])
                        {
                            vAreaAnterior = (int)dtTotalAreas.Rows[i]["AreaID"];
                            totAreas = (int)dtTotalAreas.Rows[i]["TotalArea"];
                            this.DataGrid1.Items[totAnteriorA].Cells[2].RowSpan = totAreas;
                            for (int j = totAnteriorA + 1; j <= (totAnteriorA + totAreas) - 1; j++)
                            {
                                this.DataGrid1.Items[j].Cells.RemoveAt(0);
                            }

                            totAnteriorA = totAreas;
                        }

                    }
                    else if (i >= 1)
                    {
                        vAreaAnterior = (int)dtTotalAreas.Rows[i]["AreaID"];
                        totAreas = (int)dtTotalAreas.Rows[i]["TotalArea"];
                        this.DataGrid1.Items[totAnteriorA].Cells[0].RowSpan = totAreas;
                        for (int j = totAnteriorA + 1; j <= (totAnteriorA + totAreas) - 1; j++)
                        {
                            this.DataGrid1.Items[j].Cells.RemoveAt(0);
                        }

                        totAnteriorA = totAnteriorA + totAreas;
                    }
                }

                //SOLUCIONES
                for (int i = 0; i <= dtTotalSoluciones.Rows.Count - 1; i++)
                {
                    if (i == 0)
                    {
                        if (vSolucionAnterior != (int)dtTotalSoluciones.Rows[i]["SolucionID"])
                        {
                            vSolucionAnterior = (int)dtTotalSoluciones.Rows[i]["SolucionID"];
                            totSoluciones = (int)dtTotalSoluciones.Rows[i]["TotalSolucion"];
                            this.DataGrid1.Items[totAnterior].Cells[3].RowSpan = totSoluciones;
                            for (int j = totAnterior + 1; j <= (totAnterior + totSoluciones) - 1; j++)
                            {
                                this.DataGrid1.Items[j].Cells.RemoveAt(0);
                            }

                            totAnterior = totSoluciones;
                        }

                    }
                    else if (i >= 1)
                    {
                        vSolucionAnterior = (int)dtTotalSoluciones.Rows[i]["SolucionID"];
                        totSoluciones = (int)dtTotalSoluciones.Rows[i]["TotalSolucion"];
                        this.DataGrid1.Items[totAnterior].Cells[1].RowSpan = totSoluciones;
                        for (int j = totAnterior + 1; j <= (totAnterior + totSoluciones) - 1; j++)
                        {
                            this.DataGrid1.Items[j].Cells.RemoveAt(0);
                        }

                        totAnterior = totAnterior + totSoluciones;
                    }
                }

                //APLICACIONES
                for (int i = 0; i <= dtTotalSoluciones.Rows.Count - 1; i++)
                {
                    int vAplicacionID = 0;
                    //    'Escribiendo el VPTeorico de la Aplicacion
                    if (dsEstructuraSegmento.Tables[3].Rows.Count != 0)
                    {
                        // 'Escribiendo valores potenciales observados
                        for (int p = 0; p <= this.dg2.Items.Count - 1; p++)
                        {
                            RadNumericTextBox txt = new RadNumericTextBox();
                            txt = (RadNumericTextBox)this.DataGrid1.Items[p].FindControl("txt");
                            try
                            {
                                txt.Value = (Convert.ToDouble(this.dg2.Items[p].Cells[5].Text) * Convert.ToDouble(this.txtValor.Text) * Convert.ToDouble(this.txtFactor.Text));
                            }
                            catch (Exception ex)
                            {
                                throw ex;
                            }
                        }
                        //'''''''''''''''''''''''''''''''''''''''''''
                        for (int k = 0; k <= this.dg2.Items.Count - 1; k++)
                        {
                            vAplicacionID = (int)this.dg2.DataKeys[k];
                            for (int j = 0; j <= dsEstructuraSegmento.Tables[3].Rows.Count - 1; j++)
                            {
                                RadNumericTextBox txt = new RadNumericTextBox();
                                txt = (RadNumericTextBox)this.DataGrid1.Items[k].FindControl("txt");
                                if ((int)dsEstructuraSegmento.Tables[3].Rows[j]["AplicacionID"] == vAplicacionID)
                                    txt.Value = Convert.ToDouble(dsEstructuraSegmento.Tables[3].Rows[j]["VPTeorico"]);
                            }
                        }
                    }
                    else
                    {
                        for (int j = 0; j <= this.dg2.Items.Count - 1; j++)
                        {
                            RadNumericTextBox txt = new RadNumericTextBox();
                            txt = (RadNumericTextBox)this.DataGrid1.Items[j].FindControl("txt");
                            txt.Value = (Convert.ToDouble(this.dg2.Items[j].Cells[5].Text) * Convert.ToDouble(this.txtValor.Text) * Convert.ToDouble(this.txtFactor.Text));
                        }
                    }
                }
                CopiaValoresPotenciales();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CopiaValoresPotenciales()
        {
            try
            {
                double tot = 0;
                for (int i = 0; i <= this.dg2.Items.Count - 1; i++)
                {
                    RadNumericTextBox txt = new RadNumericTextBox();
                    txt = (RadNumericTextBox)this.DataGrid1.Items[i].FindControl("txt");
                    RadNumericTextBox txt2 = new RadNumericTextBox();
                    txt2 = (RadNumericTextBox)this.dg2.Items[i].FindControl("txt");
                    txt2.Text = txt.Text;
                    tot += CastDouble(txt2.Text);
                }
                this.txtValorPO.Text = tot.ToString("$ #,##0.00");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void LeerEstructuraSegmento(int? Id_Cte, int? Id_Seg)
        {
            try
            {
                DataSet dsEstructuraSegmento = new DataSet();
                StringBuilder tablahtml = new StringBuilder();
                DataTable dtTotalAreas = new DataTable();
                DataTable dtTotalSoluciones = new DataTable();
                string vSegmento = "";
                string vArea = "";
                string vSolucion = "";
                string vNuevaArea = "";
                string vTotArea = "";
                string vTotSolucion = "";
                string vAreaID = "";
                string vSolucionID = "";
                string vNuevaSolucion = "";
                Double vpTotal = 0;
                Double vTotSegmento = 0;
                DataRow drEstructura;
                CN_CatCliente cn_catcliente = new CN_CatCliente();
                Clientes cte = new Clientes();
                cte.Id_Emp = session.Id_Emp;
                cte.Id_Cd = session.Id_Cd_Ver;
                cte.Id_Cte = Id_Cte;
                cte.Id_Seg = Id_Seg;

                cn_catcliente.EstructuraSegmento(ref dsEstructuraSegmento, cte, session.Emp_Cnx);
                if (dsEstructuraSegmento != null)
                {
                    if (dsEstructuraSegmento.Tables[0].Rows.Count != 0)
                    {
                        dtTotalAreas = dsEstructuraSegmento.Tables[1];
                        dtTotalSoluciones = dsEstructuraSegmento.Tables[2];

                        //Leer estructura del Segmento
                        tablahtml.Append("<table cellspacing=\"1\" rules=\"all\" border=\"1\" id=\"DataGrid1\" style=\"background-color:White;width:900px;\">");
                        tablahtml.Append("<tr class=\"tr_tit\">");
                        tablahtml.Append("<th scope=\"col\">UEN</th>");
                        tablahtml.Append("<th scope=\"col\">Segmento</th>");
                        tablahtml.Append("<th scope=\"col\">Área</th>");
                        tablahtml.Append("<th scope=\"col\">Solución</th>");
                        tablahtml.Append("<th scope=\"col\">Aplicación</th>");
                        tablahtml.Append("<th scope=\"col\">Potencial teórico</th>");
                        tablahtml.Append("<th scope=\"col\">Potencial observado</th>");
                        tablahtml.Append("</tr>");

                        vTotArea = dsEstructuraSegmento.Tables[1].Rows.Count.ToString();
                        if (vTotArea == "1")
                            vArea = dsEstructuraSegmento.Tables[1].Rows[0]["TotalArea"].ToString();

                        for (int i = 0; i <= dsEstructuraSegmento.Tables[0].Rows.Count - 1; i++)
                        {
                            drEstructura = dsEstructuraSegmento.Tables[0].Rows[i];
                            vSegmento = dsEstructuraSegmento.Tables[0].Rows.Count.ToString();
                            //        'vArea = dsEstructuraSegmento.Tables(0).Rows(i)("AreaID")
                            if (i == 0)
                            {
                                tablahtml.Append("<tr>");
                                tablahtml.Append("<td rowspan=" + vSegmento + "> " + txtUEN.Text + "</td>");
                                tablahtml.Append("<td rowspan=" + vSegmento + "> " + txtSegmento.Text + "</td>");
                                vAreaID = dsEstructuraSegmento.Tables[1].Rows[i]["AreaID"].ToString();
                                if (vTotArea == "1")
                                    tablahtml.Append("<td rowspan=" + vArea + "> " + drEstructura["Area"] + "</td>");
                                else
                                {
                                    for (int j = 0; j <= dtTotalAreas.Rows.Count - 1; j++)
                                    {
                                        if (vAreaID == dtTotalAreas.Rows[j]["AreaID"].ToString())
                                        {
                                            vTotArea = dtTotalAreas.Rows[j]["TotalArea"].ToString();
                                            vArea = dtTotalAreas.Rows[j]["Area"].ToString();
                                            break;
                                        }
                                    }
                                    tablahtml.Append("<td rowspan=" + vTotArea + "> " + vArea + "</td>");
                                }
                                vSolucionID = dsEstructuraSegmento.Tables[0].Rows[i]["SolucionID"].ToString();
                                for (int j = 0; j <= dtTotalSoluciones.Rows.Count - 1; j++)
                                {
                                    if (vSolucionID == dtTotalSoluciones.Rows[j]["SolucionID"].ToString())
                                    {
                                        vTotSolucion = dtTotalSoluciones.Rows[j]["TotalSolucion"].ToString();
                                        vSolucion = dtTotalSoluciones.Rows[j]["Solucion"].ToString();
                                        break;
                                    }
                                }
                                tablahtml.Append("<td rowspan=" + vTotSolucion + "> " + vSolucion + "</td>");
                            }
                            else
                            {
                                //''''''''''''''''''''
                                tablahtml.Append("<tr>");
                                //''''''''''''''''''''
                                vNuevaArea = dsEstructuraSegmento.Tables[0].Rows[i]["AreaID"].ToString();
                                if (vNuevaArea != vAreaID)
                                {
                                    vAreaID = vNuevaArea;
                                    for (int j = 0; j <= dtTotalSoluciones.Rows.Count - 1; j++)
                                    {
                                        if (vAreaID == dtTotalAreas.Rows[j]["AreaID"].ToString())
                                        {
                                            vTotArea = dtTotalAreas.Rows[j]["TotalArea"].ToString();
                                            vArea = dtTotalAreas.Rows[j]["Area"].ToString();
                                            break;
                                        }
                                    }
                                    tablahtml.Append("<td rowspan=" + vTotArea + "> " + vArea + "</td>");
                                }
                                //            '''''''''''''''''''''''''
                                vNuevaSolucion = dsEstructuraSegmento.Tables[0].Rows[i]["SolucionID"].ToString();
                                if (vNuevaSolucion != vSolucionID)
                                {
                                    vSolucionID = vNuevaSolucion;
                                    for (int j = 0; j <= dtTotalSoluciones.Rows.Count - 1; j++)
                                    {
                                        if (vSolucionID == dtTotalSoluciones.Rows[j]["SolucionID"].ToString())
                                        {
                                            vTotSolucion = dtTotalSoluciones.Rows[j]["TotalSolucion"].ToString();
                                            vSolucion = dtTotalSoluciones.Rows[j]["Solucion"].ToString();
                                            break;
                                        }
                                    }
                                    tablahtml.Append("<td rowspan=" + vTotSolucion + "> " + vSolucion + "</td>");
                                }
                            }
                            vpTotal += Convert.ToDouble(drEstructura["Porcentaje"]) * (Convert.ToDouble(txtValor.Text) * Convert.ToDouble(txtFactor.Text));
                            tablahtml.Append("<td> " + drEstructura["Aplicacion"].ToString() + "</td>");
                            tablahtml.Append("<td> " + (Convert.ToDouble(drEstructura["Porcentaje"]) * Convert.ToDouble(txtValor.Text) * Convert.ToDouble(txtFactor.Text)).ToString("$ #,##0.00") + "</td>");
                            //        ' tablahtml.Append("<td> " + for (matCurrency((drEstructura("Porcentaje") * (Me.txtValor.Text * Me.txtFactor.Text)), TriState.True, TriState.True) + "</td>")
                            //        'Verif (icando si hay valor potencial teorico cambiado
                            double vTeorico = 0;

                            if (dsEstructuraSegmento.Tables[3].Rows.Count != 0)
                            {
                                for (int m = 0; m <= dsEstructuraSegmento.Tables[3].Rows.Count - 1; m++)
                                {
                                    if (drEstructura["AplicacionID"] == dsEstructuraSegmento.Tables[3].Rows[m]["AplicacionID"])
                                    {
                                        vTeorico = Convert.ToDouble(dsEstructuraSegmento.Tables[3].Rows[m]["VPTeorico"]);
                                        break;
                                    }
                                    else
                                        vTeorico = 0;
                                }
                            }
                            else
                                vTeorico = 0;

                            if (vTeorico == 0)
                                vTeorico = Convert.ToDouble(drEstructura["Porcentaje"]) * Convert.ToDouble(txtValor.Text) * Convert.ToDouble(txtFactor.Text);

                            vTotSegmento += vTeorico;
                            switch (i)
                            {
                                case 1:
                                    tablahtml.Append("<td><input id=\"txtApp1\" runat=\"server\" type=\"text\" size=\"10\" value=" + vTeorico.ToString("$ #,##0.00") + ">" + "</td>");
                                    break;
                                case 2:
                                    tablahtml.Append("<td><input id=\"txtApp2\" runat=\"server\" type=\"text\" size=\"10\" value=" + vTeorico.ToString("$ #,##0.00") + ">" + "</td>");
                                    break;
                                case 3:
                                    tablahtml.Append("<td><input id=\"txtApp3\" runat=\"server\" type=\"text\" size=\"10\" value=" + vTeorico.ToString("$ #,##0.00") + ">" + "</td>");
                                    break;
                                case 4:
                                    tablahtml.Append("<td><input id=\"txtApp4\" runat=\"server\" type=\"text\" size=\"10\" value=" + vTeorico.ToString("$ #,##0.00") + ">" + "</td>");
                                    break;
                                default:
                                    tablahtml.Append("<td><input id=\"txtApp\" runat=\"server\" type=\"text\" size=\"10\" value=" + vTeorico.ToString("$ #,##0.00") + ">" + "</td>");
                                    break;
                            }
                            tablahtml.Append("</tr>");
                        }
                    }
                }
                txtValorPO.Text = vTotSegmento.ToString("$ #,##0.00");
                tablahtml.Append("</table>");
                lblEstruct.Text = tablahtml.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private bool ValidarSesion()
        {
            try
            {
                if (session == null)
                {
                    string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);
                    Session["dir" + Session.SessionID] = pag[pag.Length - 1];
                    Response.Redirect("login.aspx", false);
                    return false;
                }
                return true;
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
                Pagina pagina = new Pagina();
                string[] pag = Page.Request.Url.ToString().Split(new string[] { "?" }, StringSplitOptions.RemoveEmptyEntries);
                pagina.Url = (new System.IO.FileInfo(Page.Request.Url.AbsolutePath)).Name;

                CN_Pagina CapaNegocio = new CN_Pagina();
                CapaNegocio.PaginaConsultar(ref pagina, session.Emp_Cnx);

                Session["Head" + Session.SessionID] = pagina.Path;
                this.Title = pagina.Descripcion;
                Permiso Permiso = new Permiso();
                Permiso.Id_U = session.Id_U;
                Permiso.Id_Cd = session.Id_Cd;
                Permiso.Sm_cve = pagina.Clave; //Esta clave depende de la pantalla

                CapaDatos.CD_PermisosU CN_PermisosU = new CapaDatos.CD_PermisosU();
                CN_PermisosU.ValidaPermisosUsuario(ref Permiso, session.Emp_Cnx);

                if (Permiso.PAccesar == true)
                {
                    _PermisoGuardar = Permiso.PGrabar;
                    _PermisoModificar = Permiso.PModificar;
                    _PermisoEliminar = Permiso.PEliminar;
                    _PermisoImprimir = Permiso.PImprimir;
                }
                else
                    Response.Redirect("Inicio.aspx");

                if (_PermisoGuardar || _PermisoModificar)
                    this.ibtnGuardaPotencial.Visible = true;
                else
                    this.ibtnGuardaPotencial.Visible = false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void ActualizarValorPotencialCliente()
        {
            try
            {
                for (int i = 0; i <= dg2.Items.Count - 1; i++)
                {
                    RadNumericTextBox txt1 = new RadNumericTextBox();
                    txt1 = (RadNumericTextBox)DataGrid1.Items[i].FindControl("txt");
                    RadNumericTextBox txt2 = new RadNumericTextBox();
                    txt2 = (RadNumericTextBox)dg2.Items[i].FindControl("txt");

                    double VPONuevo = 0;
                    double VPDiff = 0;
                    if (CastDouble(txt1.Text) != CastDouble(txt2.Text))
                    {
                        if (CastDouble(txt1.Text) < CastDouble(txt2.Text))
                        {
                            VPDiff = CastDouble(txt2.Text) - CastDouble(txt1.Text);
                            VPONuevo = CastDouble(txtValorPO.Text) - VPDiff;
                        }
                        else
                        {
                            VPDiff = CastDouble(txt1.Text) - CastDouble(txt2.Text);
                            VPONuevo = CastDouble(txtValorPO.Text) + VPDiff;
                        }

                        CN_CatCliente cn_catacliente = new CN_CatCliente();
                        Clientes cte = new Clientes();
                        cte.Id_Emp = session.Id_Emp;
                        cte.Id_Cd = session.Id_Cd_Ver;
                        cte.Id_Seg = Convert.ToInt32(lblSeg.Text);
                        cte.Id_Terr = Convert.ToInt32(lblTer.Text);
                        cte.Id_Cte = Convert.ToInt32(lblCte.Text);
                        cte.Id_Apl = Convert.ToInt32(DataGrid1.DataKeys[i]);

                        int verificador = 0;
                        cn_catacliente.ActualizaPotencial(cte, VPONuevo, CastDouble(txt1.Text).ToString(), ref verificador, session.Emp_Cnx);
                        txtValorPO.Text = VPONuevo.ToString("$ #,##0.00");
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private double CastDouble(string value)
        {
            try
            {
                if (!string.IsNullOrEmpty(value))
                    return Convert.ToDouble(value.Replace("$", "").Replace(",", "").Replace(" ", ""));
                else
                    return 0;
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
                RadAjaxManager1.ResponseScripts.Add("radalert('" + mensaje + "', 330, 150);");
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
                //this.lblMensaje.Text = "";
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
                //this.lblMensaje.Text = Message;
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
                //this.lblMensaje.Text = "Error: [" + NombreFuncion + "] " + eme.Message.ToString();

            }
            catch (Exception)
            {
                //this.lblMensaje.Text = "Error grave: " + eme.Message.ToString() + " --> " + ex.Message.ToString();
            }
        }
        #endregion

        protected void RadAjaxManager1_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {

        }
    }
}