using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaNegocios;
using Telerik.Web.UI;
using CapaModelo_CC.CuentasCoorporativas;
using SIANWEB.CuentasCorporativas;
using SIANWEB.Utilerias;
using CapaEntidad;

namespace SIANWEB
{
    public partial class Pro_CN_Vinculacion : System.Web.UI.Page
    {
        private Sesion sesion { get { return (Sesion)Session["Sesion" + Session.SessionID]; } set { Session["Sesion" + Session.SessionID] = value; } }


        protected void Page_Load(object sender, EventArgs e)
        {

            int idmatriz = Int32.Parse(Request.QueryString["IdMatriz"]);
            int id = Int32.Parse(Request.QueryString["Id"]);

            string nombre = Request.QueryString["Nombre"];

            Boolean EsDesvinc = false;
            if (Request.QueryString["DesVinc"] != null) EsDesvinc = true;
            
     
            //IdMatriz
            var permisos = new Permisos(this.Page);
            permisos.ValidarSesion();

            if (!Page.IsPostBack && sesion != null )
            {

                txtNombreEstructura.Text = nombre;
                txtSucursalNombre.Text = sesion.Cd_Nombre;
                txtUsuario.Text = sesion.Cu_User;
                txtFechas.Text = DateTime.Now.ToShortDateString();

                CN_CatCNac_Matriz cn = new CN_CatCNac_Matriz();
                var est = cn.ConsultarEstructura(idmatriz, sesion.Id_Emp, sesion.Id_Cd).Where(x => x.Id == id).FirstOrDefault();

                txtACYS.Text = est.AcysNombre;

                this.cmbAsesorId.DataSource = cn.ComboAsesores(idmatriz);
                this.cmbAsesorId.DataBind();

                //this.cmbAsesorId.Items.Insert(0, new RadComboBoxItem(""));

                cmbRemision_Cta_Nac.Enabled = est.CatCNac_ACYS.MOV80.Value;

                if (est.CatCNac_ACYS.MOV80.Value)
                {
                    this.cmbRemision_Cta_Nac.DataSource = cn.ComboRemisionesMov80();
                    this.cmbRemision_Cta_Nac.DataBind();

                  //  this.cmbRemision_Cta_Nac.Items.Insert(0, new RadComboBoxItem(""));
                }

                //if (Request.QueryString["DesVinc"] != null)

                var solic = est.CatCNac_Solicitudes.Where(x => x.Estatus == 1 || x.Estatus == 5 || (EsDesvinc && x.Estatus == 2)).OrderByDescending(x=>x.Estatus).FirstOrDefault();
                //Pantalla de consulta
                if (solic != null )
                {

                    cmbAsesorId.Enabled = false;

                    txtClienteSIAN.Text = solic.ClienteSIAN.ToString();

                    var sol = solic;

                    object objSolicitudDir = sol.CatCNac_Solicitudes_DirFiscal;
                    AsignacionCampos.AsignaCamposForma(ref objSolicitudDir, "", this);

                    var cliente = cn.ConsultaCliente(Int32.Parse(txtClienteSIAN.Text), sesion.Id_Emp, sesion.Id_Cd);
                    txtRazonSocial.Text = cliente.Cte_NomComercial;

                    var territorios = cliente.CatClienteDets.ToList();
                    cmbTerritorio.DataSource = territorios;
                    cmbTerritorio.DataBind();


                    object objSolicitud = sol;
                    AsignacionCampos.AsignaCamposForma(ref objSolicitud, "", this);

                    if (!EsDesvinc)
                        AsignacionCampos.DesactivarControles(this, "");
                    else
                    {
                        txtComentarios.Text = "";
                        txtClienteSIAN.ReadOnly = true;
                        txtRazonSocial.ReadOnly = true;
                        cmbTerritorio.Enabled = false;
                    }


                }

                if (Request.QueryString["DesVinc"] == "1") lblTitulo.Text = "Desvinculación";
            }
        }


        protected void txtClienteSIAN_TextChanged(object sender, EventArgs e)
        {
            CN_CatCNac_Matriz cn = new CN_CatCNac_Matriz();

            try
            {
                var objSession = ((CapaEntidad.Sesion)(Session["Sesion" + Session.SessionID]));

                 var cliente = cn.ConsultaCliente(Int32.Parse(txtClienteSIAN.Text), objSession.Id_Emp, objSession.Id_Cd);
                txtRazonSocial.Text = cliente.Cte_NomComercial;


                txtCalle.Text = cliente.Cte_FacCalle;
                txtNumInterior.Text = cliente.Cte_FacNumeroInterior;
                txtNumExterior.Text = cliente.Cte_FacNumero;
                txtColonia.Text = cliente.Cte_FacColonia;
                txtMunicipio.Text = cliente.Cte_FacMunicipio;
                txtEstado.Text = cliente.Cte_FacEstado;
                txtTelefonos.Text = cliente.Cte_FacTel;
                txtFAX.Text = cliente.Cte_Fax;
                txtRFC.Text = cliente.Cte_FacRfc;


                var territorios = cliente.CatClienteDets.ToList();
                cmbTerritorio.DataSource = territorios;
                cmbTerritorio.DataBind();
            }
            catch(Exception)
            {

            }

        }


        protected void ImgBuscarDireccionEntrega_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                int idmatriz = Int32.Parse(Request.QueryString["IdMatriz"]);

                RAM1.ResponseScripts.Add("AbrirBuscarDireccionEntrega(" + idmatriz.ToString() + ");");
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
                //ErrorManager();
                switch (e.Argument.ToString())
                {

                    case "direccion_CC":
                        string dirFiscal = "";
                        if (Session["Descripcion_Buscar" + Session.SessionID] != null)
                        {
                            dirFiscal = Session["Descripcion_Buscar" + Session.SessionID].ToString();

                            var dirArray = dirFiscal.Split(',');


                            txtCalle.Text = dirArray[1];
                            txtNumInterior.Text = dirArray[2];
                            txtNumExterior.Text = "";
                            txtColonia.Text = dirArray[3];
                            txtCP.Text = dirArray[4];
                            txtMunicipio.Text = dirArray[3];
                            txtEstado.Text = dirArray[5];
                            txtTelefonos.Text = "";
                            txtFAX.Text = "";
                            txtRFC.Text = dirArray[6];

                            Session.Remove("DirFisc");
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        private void ErrorManager(Exception eme, string NombreFuncion)
        {
            //try
            //{
            //    this.lblMensaje.Text = "Error: [" + NombreFuncion + "] " + eme.Message.ToString();

            //}
            //catch (Exception ex)
            //{
            //    this.lblMensaje.Text = "Error grave: " + eme.Message.ToString() + " --> " + ex.Message.ToString();
            //}
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            Boolean EsDesvinc = false;
            if (Request.QueryString["DesVinc"] != null) EsDesvinc = true;

            int idmatriz = Int32.Parse(Request.QueryString["IdMatriz"]);
            int id = Int32.Parse(Request.QueryString["Id"]);
            var objSession = ((CapaEntidad.Sesion)(Session["Sesion" + Session.SessionID]));
            CN_CatCNac_Matriz cn = new CN_CatCNac_Matriz();

            var solicitud = new CatCNac_Solicitudes();
            solicitud.CatCNac_Solicitudes_DirFiscal = new CatCNac_Solicitudes_DirFiscal();

            object objSolicitud = solicitud;
            AsignacionCampos.AsignaCamposEntidad(ref objSolicitud, "", this);

            object objSolicitudDir = solicitud.CatCNac_Solicitudes_DirFiscal;
            AsignacionCampos.AsignaCamposEntidad(ref objSolicitudDir, "", this);

            solicitud.Fecha = DateTime.Now;
           
            solicitud.Id_Matriz = idmatriz;
            solicitud.Id_Estructura = id;
            solicitud.Sucursal = objSession.Id_Cd;

            if (EsDesvinc) solicitud.Estatus = 5;
            else solicitud.Estatus = 1;

            solicitud.Id_Cd = objSession.Id_Cd;
            solicitud.Id_Emp = objSession.Id_Emp;

            if (solicitud.Comentarios == null) solicitud.Comentarios = "";

            cn.GuardarSolicitud(solicitud);

            if (EsDesvinc) RAM1.ResponseScripts.Add("CloseAlert('Su solicitud de desvinculación ha sido procesada');");
            else RAM1.ResponseScripts.Add("CloseAlert('Su solicitud de vinculación ha sido procesada');");

           

        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            RAM1.ResponseScripts.Add("GetRadWindow().Close();");
        }

        

    }
}