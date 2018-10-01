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

namespace SIANWEB
{
    public partial class CatCentroDisParamsRentabilidad : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            { 
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                if (!Page.IsPostBack)
                {
                    // ------------------------------------------------------------------------
                    // Consulta datos Gnerales, Nombre de Epmresa, C. Dist y Región
                    // ------------------------------------------------------------------------
                    CN_CapFactura fac = new CN_CapFactura();
                    
                    string[] datosGen = fac.ConsultaFacturacion_DatosGeneralesFacturacion(sesion.Emp_Cnx, sesion.Id_Emp, sesion.Id_Cd_Ver);
                    lblEmpresaNombre.Text = string.Concat(Page.Request.QueryString["Id_Emp"].ToString(), " - ", datosGen[0]);
                    lblSucursalNombre.Text = string.Concat(Page.Request.QueryString["Id_Cd"].ToString(), " - ", datosGen[1]);
                    lblRegionNombre.Text = string.Concat(datosGen[3], " - ", datosGen[2]);
                    lblClienteNombre.Text = string.Concat(Page.Request.QueryString["Id_Cte"].ToString(), " - ", Page.Request.QueryString["Cte_NomComercial"].ToString().Replace("�", "ñ"));

                    //Define variable de sesion con datos de impresion de reporte de valuacion de proyectos
                    this.HD_Folio.Value = string.Concat(
                        Page.Request.QueryString["Id_Emp"].ToString()
                        , ",", Page.Request.QueryString["Id_Cd"].ToString()
                        , ",", Page.Request.QueryString["Id_Vap"].ToString());
                    Session["ReporteValuacionProyecto" + Session.SessionID] = this.HD_Folio.Value;
                    //Define variable de sesion como indicativo de impreion de reporte de rentabilidad
                    Session["ReporteRentabilidadClientes" + Session.SessionID] = "SI";

                    //Datos del centro de distribución
                    CentroDistribucion cd = new CentroDistribucion();
                    new CN_CatCentroDistribucion().ConsultarCentroDistribucion(ref cd
                        , Convert.ToInt32(Page.Request.QueryString["Id_Cd"])
                        , Convert.ToInt32(Page.Request.QueryString["Id_Emp"])
                        , sesion.Emp_Cnx);

                    //Datos de valuación de proyectos del C. de Dist.
                    CentroDistribucion cdValProy = new CentroDistribucion();
                    new CN_CatCentroDistribucion().ConsultaCentroDistribucion_DatosValProyecto(ref cdValProy, sesion.Emp_Cnx);

                    //Llenar Datos de valuación de proyecto del centro de distribucion
                    #region Llenar Datos de valuación de proyecto del centro de distribucion

                    txtCetesCd.Text = cd.Cd_TasaCetes == 0 ? cdValProy.Cd_TasaCetes.ToString() : cd.Cd_TasaCetes.ToString();
                    txtCetesEstandar.Text = cdValProy.Cd_TasaCetes.ToString();

                    txtIvaCd.Text = cd.Cd_Iva == 0 ? cdValProy.Cd_Iva.ToString() : cd.Cd_Iva.ToString();
                    txtIvaEstandar.Text = cdValProy.Cd_Iva.ToString();

                    txtCuentasCd.Text = cd.Cd_DiasCuentasPorCobrar == 0 ? cdValProy.Cd_DiasCuentasPorCobrar.ToString() : cd.Cd_DiasCuentasPorCobrar.ToString();
                    txtCuentasEstandar.Text = cdValProy.Cd_DiasCuentasPorCobrar.ToString();

                    txtFleteCd.Text = cd.Cd_Flete == 0 ? cdValProy.Cd_Flete.ToString() : cd.Cd_Flete.ToString();
                    txtFleteEstandar.Text = cdValProy.Cd_Flete.ToString();

                    txtDiasCd.Text = cd.Cd_Dias == 0 ? cdValProy.Cd_Dias.ToString() : cd.Cd_Dias.ToString();
                    txtDiasEstandar.Text = cdValProy.Cd_Dias.ToString();

                    txtComisionCd.Text = cd.Cd_ComisionRik == 0 ? cdValProy.Cd_ComisionRik.ToString() : cd.Cd_ComisionRik.ToString();
                    txtComisionEstandar.Text = cdValProy.Cd_ComisionRik.ToString();

                    txtInventarioCd.Text = cd.Cd_DiasInv == 0 ? cdValProy.Cd_DiasInv.ToString() : cd.Cd_DiasInv.ToString();
                    txtInventarioEstandar.Text = cdValProy.Cd_DiasInv.ToString();

                    txtOtrosCd.Text = cd.Cd_OtrosGastosVar == 0 ? cdValProy.Cd_OtrosGastosVar.ToString() : cd.Cd_OtrosGastosVar.ToString();
                    txtOtrosEstandar.Text = cdValProy.Cd_OtrosGastosVar.ToString();

                    txtFactorInvCd.Text = cd.Cd_FactorInvComodato == 0 ? cdValProy.Cd_FactorInvComodato.ToString() : cd.Cd_FactorInvComodato.ToString();
                    txtFactorInvEstandar.Text = cdValProy.Cd_FactorInvComodato.ToString();

                    txtGastofijoCd.Text = cd.Cd_ContribucionGastosFijosOtros == 0 ? cdValProy.Cd_ContribucionGastosFijosOtros.ToString() : cd.Cd_ContribucionGastosFijosOtros.ToString();
                    txtGastofijoEstandar.Text = cdValProy.Cd_ContribucionGastosFijosOtros.ToString();

                    txtFactorConCd.Text = cd.Cd_FactorConvActFijo == 0 ? cdValProy.Cd_FactorConvActFijo.ToString() : cd.Cd_FactorConvActFijo.ToString();
                    txtFactorConEstandar.Text = cdValProy.Cd_FactorConvActFijo.ToString();

                    txtGastofijopapelCd.Text = cd.Cd_ContribucionGastosFijosPapel == 0 ? cdValProy.Cd_ContribucionGastosFijosPapel.ToString() : cd.Cd_ContribucionGastosFijosPapel.ToString();
                    txtGastofijopapelEstandar.Text = cdValProy.Cd_ContribucionGastosFijosPapel.ToString();

                    txtFinanciamientoCd.Text = cd.Cd_DiasFinanciaProv == 0 ? cdValProy.Cd_DiasFinanciaProv.ToString() : cd.Cd_DiasFinanciaProv.ToString();
                    txtfinanciamientoEstandar.Text = cdValProy.Cd_DiasFinanciaProv.ToString();

                    txtIsrCd.Text = cd.Cd_ISRyPTU == 0 ? cdValProy.Cd_ISRyPTU.ToString() : cd.Cd_ISRyPTU.ToString();
                    txtIsrEstandar.Text = cdValProy.Cd_ISRyPTU.ToString();

                    txtTasaCd.Text = cd.Cd_TasaIncCostoCapital == 0 ? cdValProy.Cd_TasaIncCostoCapital.ToString() : cd.Cd_TasaIncCostoCapital.ToString();
                    txtTasaEstandar.Text = cdValProy.Cd_TasaIncCostoCapital.ToString();

                    txtCargoCd.Text = cd.Cd_CargoUCS == 0 ? cdValProy.Cd_CargoUCS.ToString() : cd.Cd_CargoUCS.ToString();
                    txtCargoEstandar.Text = cdValProy.Cd_CargoUCS.ToString();

                    #endregion
                }
            }
            catch (Exception ex)
            {
                this.DisplayMensajeAlerta(string.Concat(ex.Message, "Page_Load_error"));
            }
        }

        protected void RadToolBar1_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            string accionError = string.Empty;
            string mensajeError = string.Empty;
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];

            try
            {
                RadToolBarButton btn = e.Item as RadToolBarButton;

                switch (btn.CommandName)
                {
                    case "save":
                        mensajeError = "CapValProyecto_ParamsRentabilidad_update_error";
                        this.Guardar();
                        break;
                }
            }
            catch (Exception ex)
            {
                string mensaje = string.Concat(ex.Message, mensajeError);
                this.DisplayMensajeAlerta(mensaje);
            }
        }

        #region Funciones

        private void Guardar()
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];

                CentroDistribucion cd = new CentroDistribucion();
                this.LlenarObjetoCentroDistribucion(ref cd);
                string mensaje = "Los datos se modificaron correctamente";

                int verificador = 0;
                new CN_CatCentroDistribucion().ModificarCentroDistribucion_DatosValuacionProyectos(ref cd, sesion.Emp_Cnx, ref verificador);

                RAM1.ResponseScripts.Add(string.Concat(@"CloseWindow('", mensaje, "')")); //cerrar ventana radWindow de factura
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void LlenarObjetoCentroDistribucion(ref CentroDistribucion cd)
        {
            cd = new CentroDistribucion();

            cd.Id_Emp = Convert.ToInt32(Page.Request.QueryString["Id_Emp"]);
            cd.Id_Cd = Convert.ToInt32(Page.Request.QueryString["Id_Cd"]);

            cd.Cd_TasaCetes = txtCetesCd.Text == string.Empty ? Convert.ToInt32(txtCetesEstandar.Text) : Convert.ToInt32(txtCetesCd.Text);
            cd.Cd_DiasCuentasPorCobrar = txtCuentasCd.Text == string.Empty ? Convert.ToInt32(txtCuentasEstandar.Text) : Convert.ToInt32(txtCuentasCd.Text);
            cd.Cd_Dias = txtDiasCd.Text == string.Empty ? Convert.ToInt32(txtDiasEstandar.Text) : Convert.ToInt32(txtDiasCd.Text);
            cd.Cd_DiasInv = txtInventarioCd.Text == string.Empty ? Convert.ToInt32(txtInventarioEstandar.Text) : Convert.ToInt32(txtInventarioCd.Text);
            cd.Cd_FactorInvComodato = txtFactorInvCd.Text == string.Empty ? Convert.ToDouble(txtFactorInvEstandar.Text) : Convert.ToDouble(txtFactorInvCd.Text);
            cd.Cd_FactorConvActFijo = txtFactorConCd.Text == string.Empty ? Convert.ToInt32(txtFactorConEstandar.Text) : Convert.ToInt32(txtFactorConCd.Text);
            cd.Cd_DiasFinanciaProv = txtFinanciamientoCd.Text == string.Empty ? Convert.ToInt32(txtfinanciamientoEstandar.Text) : Convert.ToInt32(txtFinanciamientoCd.Text);
            cd.Cd_TasaIncCostoCapital = txtTasaCd.Text == string.Empty ? Convert.ToInt32(txtTasaEstandar.Text) : Convert.ToInt32(txtTasaCd.Text);
            cd.Cd_Iva = txtIvaCd.Text == string.Empty ? Convert.ToInt32(txtIvaEstandar.Text) : Convert.ToInt32(txtIvaCd.Text);
            cd.Cd_Flete = txtFleteCd.Text == string.Empty ? Convert.ToInt32(txtFleteEstandar.Text) : Convert.ToInt32(txtFleteCd.Text);
            cd.Cd_ComisionRik = txtComisionCd.Text == string.Empty ? Convert.ToInt32(txtComisionEstandar.Text) : Convert.ToInt32(txtComisionCd.Text);
            cd.Cd_OtrosGastosVar = txtOtrosCd.Text == string.Empty ? Convert.ToInt32(txtOtrosEstandar.Text) : Convert.ToInt32(txtOtrosCd.Text);
            cd.Cd_ContribucionGastosFijosOtros = txtGastofijoCd.Text == string.Empty ? Convert.ToInt32(txtGastofijoEstandar.Text) : Convert.ToInt32(txtGastofijoCd.Text);
            cd.Cd_ContribucionGastosFijosPapel = txtGastofijopapelCd.Text == string.Empty ? Convert.ToDouble(txtGastofijopapelEstandar.Text) : Convert.ToDouble(txtGastofijopapelCd.Text);
            cd.Cd_ISRyPTU = txtIsrCd.Text == string.Empty ? Convert.ToInt32(txtIsrEstandar.Text) : Convert.ToInt32(txtIsrCd.Text);
            cd.Cd_CargoUCS = txtCargoCd.Text == string.Empty ? Convert.ToInt32(txtCargoEstandar.Text) : Convert.ToInt32(txtCargoCd.Text);
        }

        private void DisplayMensajeAlerta(string mensaje)
        {
            if (mensaje.Contains("Page_Load_error"))
                Alerta("Error al cargar la página");
            else
            if (mensaje.Contains("CapValProyecto_ParamsRentabilidad_update_error"))
                Alerta("Error al momento de actualizar los datos de valuación de proyectos");
            else
                Alerta(string.Concat("No se pudo realizar la operación solicitada.<br/>", mensaje.Replace("'", "\"")));
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