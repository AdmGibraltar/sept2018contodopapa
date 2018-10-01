using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaEntidad;
using CapaNegocios;
using System.Text;
using System.Net.Mail;
using System.Net;
using System.IO;
using System.Configuration;
using Telerik.Web.UI;
using System.Data;
using System.Xml;

namespace SIANWEB
{
    public partial class ProAutorizacion_GastoViaje : System.Web.UI.Page
    {

        private string CryptoPassIn = "k3y9u1m1c4";
        private string CryptoPassOut = "k3y9u1m1c4";
        private string MacolaKey
        {
            get
            {
                if (Session["TimeMacolaServer"] == null || Session["TimeMacolaServer"].ToString() == "")
                {
                    wsMacola.wsMacola wsMac = new wsMacola.wsMacola();
                    Session["TimeMacolaServer"] = (DateTime.Now - Convert.ToDateTime(Convert.ToDateTime(CapaDatos.clsCrypto.BlowFish.Decrypt(wsMac.GetKey(), CryptoPassIn)))).TotalSeconds;
                }

                DateTime TimeServer = DateTime.Now.AddSeconds(Convert.ToDouble(Session["TimeMacolaServer"]));

                string key = String.Format(
                    "{0}|&|{1}|&|{2}",
                    "SIANWEB",
                    TimeServer.ToString("yyyy-MM-dd HH:mm:ss"),
                    TimeServer.AddSeconds(60).ToString("yyyy-MM-dd HH:mm:ss")
                );

                return CapaDatos.clsCrypto.BlowFish.Encrypt(key, CryptoPassOut);
            }
        }

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
                   

                    //JFCV 02 feb 2016 agregar columnas 
                    if (Request.QueryString["ref"] != null)
                    {
                        if (Convert.ToInt32(Request.QueryString["ref"]) == 1)
                        {
                            rgPago.Columns[6].Visible = true;  // Motivo
                            rgPago.Columns[8].Visible = false;  // Nombre acreedor
                            rgPago.Columns[9].Visible = false;  // Observaciones
                            rgPago.Columns[10].Visible = false;  // fecha elaboracion
                            rgPago.Columns[11].Visible = true;  // fecha entrada
                            rgPago.Columns[12].Visible = true;  // fecha salida
                            rgPago.Columns[13].Visible = true;  //Destino


                        }
                        else
                        {
                            rgPago.Columns[6].Visible = false;      // Motivo
                            rgPago.Columns[8].Visible = true;  // Nombre acreedor
                            rgPago.Columns[9].Visible = true;  // Observaciones
                            rgPago.Columns[10].Visible = true;  // fecha elaboracion
                            rgPago.Columns[11].Visible = false;  // fecha entrada
                            rgPago.Columns[12].Visible = false;  // fecha salida
                            rgPago.Columns[13].Visible = false;     //Destino

                        }
                    }
                     Inicializar();
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
                //rgAsignacion.Rebind();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "CmbCentro_SelectedIndexChanged1");
            }
        }

        protected void rgPago_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                {
                    rgPago.DataSource = GetList();
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void rgPago_ItemCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                GastoViaje gastoViaje = new GastoViaje();

                int item = e.Item.ItemIndex;

                gastoViaje.Id_GV = Int32.Parse(rgPago.Items[item]["Id_GV"].Text);
                gastoViaje.Id_Emp = Int32.Parse(rgPago.Items[item]["Id_Emp"].Text);
                gastoViaje.Id_Cd = Int32.Parse(rgPago.Items[item]["Id_Cd"].Text);

                if (((RadTextBox)(rgPago.Items[item]["Acr_MotivoRechazo"].FindControl("TxtMotivoRechazo"))).Text != string.Empty)
                {
                    gastoViaje.GV_MotivoRechazo = Convert.ToString(((RadTextBox)(rgPago.Items[item]["Acr_MotivoRechazo"].FindControl("TxtMotivoRechazo"))).Text);
                }
                //diferencia 
                decimal diferencia = Decimal.Parse(rgPago.Items[item]["GV_Saldo_Comprobar"].Text);

                switch (e.CommandName.ToString())
                {
                    case "XML":
                        //descargarXML(pago.Id_PagElec);
                        break;
                    case "PDF":
                        //descargarPDF(pago.Id_PagElec);
                        break;
                    //JFCV 24 feb 2016 agregue la funcionalidad de abrir comprobantes 
                    case "Comprobantes":
                        RAM1.ResponseScripts.Add(string.Concat(@"AbrirVentana_LstComprobantes('", gastoViaje.Id_GV, "')"));
                        break;
                    //JFCV 18 dic 2015 agregue la funcionalidad de rechazar 
                    case "Delete":
                        #region Rechazar
                        Sesion sessionr = new Sesion();
                        sessionr = (Sesion)Session["Sesion" + Session.SessionID];
                        gastoViaje.UsuarioMod = sessionr.Id_U;

                        if (gastoViaje.GV_MotivoRechazo != "" && gastoViaje.GV_MotivoRechazo != null)
                        {
                            int verificador = -1;

                            CapaDatos.dbAccess oDB = new CapaDatos.dbAccess(sessionr.Emp_Cnx);
                            //using ())
                            //{

                            try
                            {

                                CN_CapGastoViaje clsGastoViaje = new CN_CapGastoViaje();
                                clsGastoViaje.RechazarGastoViaje(gastoViaje, sessionr.Emp_Cnx, ref verificador);

                                if (verificador == 1)
                                {

                                    try
                                    {
                                        PagoElectronico pagoElec = new PagoElectronico();
                                        pagoElec.Id_PagElec = Int32.Parse(rgPago.Items[item]["Id_PagElec"].Text);
                                        pagoElec.PagElec_MotivoRechazo = gastoViaje.GV_MotivoRechazo;
                                        EnviarCorreo(pagoElec, 0);
                                    }
                                    catch (Exception ex)
                                    {
                                        this.lblMensaje.Text = "El Gasto de Viaje fue rechazado pero el correo no pudo ser enviado, error: " + ex.Message;
                                    }
                                }
                                else //no paso la validación
                                {
                                    this.lblMensaje.Text = "No se pudo rechazar el Gasto de Viaje ";
                                }


                            }
                            catch (Exception ex)
                            {
                                throw ex;
                            }
                            finally
                            {

                            }

                        }
                        else
                        {
                            this.lblMensaje.Text = "Capture el Motivo de Rechazo.";
                        }
                        #endregion Rechazar
                        break;
                    case "Autorizar":

                        #region Autorizar COmprobante
                        Sesion session = new Sesion();
                        session = (Sesion)Session["Sesion" + Session.SessionID];
                        gastoViaje.UsuarioMod = session.Id_U;


                        int verificadorins = -1;

                        ///JFCV TODO Definir rango de tolerancia 
                        if (diferencia >= -1)  // si la diferencia no es 0 o a favor no hace nada 
                        {


                            CN_CapGastoViaje clsGastoViajeAut = new CN_CapGastoViaje();

                            CapaDatos.dbAccess oDBaut = new CapaDatos.dbAccess(session.Emp_Cnx);

                            try
                            {

                                #region Autorizar

                                CN_CapPagoElectronico clsPago = new CN_CapPagoElectronico();

                                CN_CapGastoViajeComprobante clsGastoViajeComprobante = new CN_CapGastoViajeComprobante();
                                List<GastoViajeComprobante> list = new List<GastoViajeComprobante>();

                                GastoViajeComprobante gastoViajeComprobante = new GastoViajeComprobante();
                                gastoViajeComprobante.Id_Emp = Int32.Parse(rgPago.Items[item]["Id_Emp"].Text);
                                gastoViajeComprobante.Id_Cd = Int32.Parse(rgPago.Items[item]["Id_Cd"].Text);
                                gastoViajeComprobante.Id_GV = Int32.Parse(rgPago.Items[item]["Id_GV"].Text);

                                clsGastoViajeComprobante.ConsultaGastoViajeComprobante(gastoViajeComprobante, session.Emp_Cnx, ref list);

                                PagoElectronico pagoelectronico = new PagoElectronico();
                                pagoelectronico.Id_Emp = Int32.Parse(rgPago.Items[item]["Id_Emp"].Text);
                                pagoelectronico.Id_Cd = Int32.Parse(rgPago.Items[item]["Id_Cd"].Text);
                                pagoelectronico.Id_GV = Int32.Parse(rgPago.Items[item]["Id_GV"].Text);
                                pagoelectronico.Id_PagElec = Int32.Parse(rgPago.Items[item]["Id_PagElec"].Text);
                                CN_CapPagoElectronico clsPagoElectronico = new CN_CapPagoElectronico();
                                clsPagoElectronico.ConsultaPagoElectronicoAutorizacion(pagoelectronico, session.Emp_Cnx);

                                wsMacola.wsMacola wsMac = new wsMacola.wsMacola();


                                List<wsMacola.Gasto> lstGasto = new List<wsMacola.Gasto>();
                                List<wsMacola.Gasto> lstGastoDiferencia = new List<wsMacola.Gasto>();

                                #region foreach de comprobantes
                                foreach (GastoViajeComprobante comprobante in list)
                                {
                                    wsMacola.Gasto gasto = new wsMacola.Gasto();


                                    if (comprobante.GVComprobante_ConComprobante)  //Inserto los que tienen comprobante 
                                    {
                                        #region Con Comprobante
                                        gasto.id_trx = Int32.Parse(rgPago.Items[item]["Id_PagElec"].Text);
                                        gasto.cus_type_cd = rgPago.Items[item]["Id_Cd"].Text;
                                        //JFCV 29 sep 2015 el valor de vend_no truncarlo a 12 caracteres porque esa es la longitud en la tabla 
                                        if (rgPago.Items[item]["Acr_NumeroGenerado"].Text.Length > 12)
                                            gasto.vend_no = rgPago.Items[item]["Acr_NumeroGenerado"].Text.Substring(0, 12);
                                        else
                                            gasto.vend_no = rgPago.Items[item]["Acr_NumeroGenerado"].Text;
                                        //JFCV agregar el numero de referencia en el web service
                                        gasto.no_ref = Convert.ToString(((RadTextBox)(rgPago.Items[item]["Acr_NumeroGenerado"].FindControl("TxtNumeroAcreedor"))).Text);

                                        DataTable DT = ObtenerDatos(comprobante.GVComprobante_XmlStream.Length > 0 ? comprobante.GVComprobante_XmlStream : null);

                                        gasto.inv_no = string.Concat((string)DT.Rows[0]["serie"], (string)DT.Rows[0]["folio"]);
                                        gasto.amt = Convert.ToDecimal("0" + (string)DT.Rows[0]["importe"]);
                                        gasto.trx_dt = (DateTime)DT.Rows[0]["fecha"];

                                        //JFCV agregar retenciones
                                        gasto.sub_amt = Convert.ToDecimal("0" + (string)DT.Rows[0]["subtotal"]);
                                        gasto.tax_amt = Convert.ToDecimal("0" + (string)DT.Rows[0]["iva"]);
                                        gasto.ret_amt = Convert.ToDecimal("0" + (string)DT.Rows[0]["impuestoretenido"]);
                                        gasto.iva_ret = Convert.ToDecimal("0" + (string)DT.Rows[0]["ivaretenido"]);

                                        gasto.dwn = null; //null
                                        gasto.dwn_dt = null; //null
                                        gasto.vchr_no = null; //null
                                        gasto.mov_type = null; //null
                                        //jfcv 0909 2016 agregar columan de subtipo para enviarselo a macola 
                                        gasto.mov_type = pagoelectronico.Id_PagElecSubTipo.ToString();   //rgPago.Items[item]["Id_PagElecSubTipo"].Text;
                                        if (Convert.ToInt32(gasto.mov_type) == -1)
                                            gasto.mov_type = null;
                                        //fin jfcv 09092016

                                        gasto.account_no = String.Concat(
                                            CerosDerecha(comprobante.GVComprobante_GV_Cc, 8),
                                            CerosDerecha(comprobante.GVComprobante_GV_SubCuenta, 8),
                                            CerosDerecha(comprobante.GVComprobante_GV_SubSubCuenta, 8)
                                        );
                                        gasto.SAT_no = (string)DT.Rows[0]["UUID"];
                                        try
                                        {
                                            gasto.flg_test = Convert.ToBoolean(ConfigurationManager.AppSettings["insGastoMacola"].ToString());
                                        }
                                        catch
                                        {
                                            gasto.flg_test = false;
                                        }
                                        //JFCV 22 ago 2016 agregar el mail del usuario 
                                        gasto.mail_usu = session.U_Correo;
                                        lstGasto.Add(gasto);
                                        #endregion con comprobante
                                    }
                                    else
                                    {
                                        #region Enviar si tiene archivo de soporte

                                        gasto.id_trx = Int32.Parse(rgPago.Items[item]["Id_PagElec"].Text);
                                        gasto.cus_type_cd = rgPago.Items[item]["Id_Cd"].Text;
                                        //JFCV 29 sep 2015 el valor de vend_no truncarlo a 12 caracteres porque esa es la longitud en la tabla 
                                        if (rgPago.Items[item]["Acr_NumeroGenerado"].Text.Length > 12)
                                            gasto.vend_no = rgPago.Items[item]["Acr_NumeroGenerado"].Text.Substring(0, 12);
                                        else
                                            gasto.vend_no = rgPago.Items[item]["Acr_NumeroGenerado"].Text;
                                        //JFCV agregar el numero de referencia en el web service
                                        gasto.no_ref = Convert.ToString(((RadTextBox)(rgPago.Items[item]["Acr_NumeroGenerado"].FindControl("TxtNumeroAcreedor"))).Text);


                                        gasto.inv_no = "";
                                        gasto.amt = comprobante.GVComprobante_Importe;
                                        gasto.trx_dt = comprobante.GVComprobante_Fecha;

                                        //JFCV agregar retenciones si es una factura de soporte el iva y el impuesto retenido es cero
                                        gasto.sub_amt = comprobante.GVComprobante_Importe;
                                        gasto.tax_amt = 0;
                                        gasto.ret_amt = 0;
                                        gasto.iva_ret = 0;

                                        gasto.dwn = null; //null
                                        gasto.dwn_dt = null; //null
                                        gasto.vchr_no = null; //null
                                        gasto.mov_type = null; //null
                                        //jfcv 0909 2016 agregar columan de subtipo para enviarselo a macola 
                                        gasto.mov_type = pagoelectronico.Id_PagElecSubTipo.ToString();
                                        if (Convert.ToInt32(gasto.mov_type) == -1)
                                            gasto.mov_type = null;
                                        //fin jfcv 09092016

                                        gasto.bank_name = null;
                                        gasto.bank_account = null;
                                        gasto.account_no = String.Concat(
                                           CerosDerecha(comprobante.GVComprobante_GV_Cc, 8),
                                           CerosDerecha(comprobante.GVComprobante_GV_SubCuenta, 8),
                                           CerosDerecha(comprobante.GVComprobante_GV_SubSubCuenta, 8)
                                        );

                                        gasto.SAT_no = "";
                                        try
                                        {
                                            gasto.flg_test = Convert.ToBoolean(ConfigurationManager.AppSettings["insGastoMacola"].ToString());
                                        }
                                        catch
                                        {
                                            gasto.flg_test = false;
                                        }

                                        //JFCV 22 ago 2016 agregar el mail del usuario 
                                        gasto.mail_usu = session.U_Correo;

                                        lstGasto.Add(gasto);


                                        #endregion Enviar si tiene archivo de soporte
                                    }

                                }

                                #endregion foreach de comprobantes

                                #region Enviar a GL el monto original en saldo negativo

                                wsMacola.Gasto gastoGL = new wsMacola.Gasto();
                                gastoGL.id_trx = Int32.Parse(rgPago.Items[item]["Id_PagElec"].Text);
                                gastoGL.cus_type_cd = rgPago.Items[item]["Id_Cd"].Text;
                                if (rgPago.Items[item]["Acr_NumeroGenerado"].Text.Length > 12)
                                    gastoGL.vend_no = rgPago.Items[item]["Acr_NumeroGenerado"].Text.Substring(0, 12);
                                else
                                    gastoGL.vend_no = rgPago.Items[item]["Acr_NumeroGenerado"].Text;
                                gastoGL.no_ref = Convert.ToString(((RadTextBox)(rgPago.Items[item]["Acr_NumeroGenerado"].FindControl("TxtNumeroAcreedor"))).Text);
                                gastoGL.inv_no = "";
                                gastoGL.amt = Convert.ToDecimal(rgPago.Items[item]["GV_importe"].Text)*-1;
                                gastoGL.trx_dt = Convert.ToDateTime(DateTime.Now); 
                                gastoGL.sub_amt = gastoGL.amt;
                                gastoGL.tax_amt = 0;
                                gastoGL.ret_amt = 0;
                                gastoGL.iva_ret = 0;
                                gastoGL.dwn = null; //null
                                gastoGL.dwn_dt = null; //null
                                gastoGL.vchr_no = null; //null
                                gastoGL.mov_type = null; //null
                                //jfcv 0909 2016 agregar columan de subtipo para enviarselo a macola 
                                gastoGL.mov_type = pagoelectronico.Id_PagElecSubTipo.ToString(); 
                                if (Convert.ToInt32(gastoGL.mov_type) == -1)
                                    gastoGL.mov_type = null;
                                //fin jfcv 09092016

                                gastoGL.bank_name = null;
                                gastoGL.bank_account = null;
                                gastoGL.SAT_no = "";
                                gastoGL.flg_test = false;
                                gastoGL.account_no = String.Concat(
                                 CerosDerecha(pagoelectronico.PagElec_Cc, 8),
                                 CerosDerecha(pagoelectronico.PagElec_SubCuenta, 8),
                                 CerosDerecha(pagoelectronico.PagElec_SubSubCuenta, 8)
                                );
                                //JFCV 22 ago 2016 agregar el mail del usuario 
                                gastoGL.mail_usu = session.U_Correo;
                                lstGasto.Add(gastoGL);
                                #endregion Enviar a GL el monto original en saldo negativo
                                // si la diferencia es  en contra  no inserto nada y mando mensaje 
                                // si la diferencia es a favor inserto y aparte inserto en insGasto la diferencia 
                                decimal montominimocheque = Convert.ToDecimal(ConfigurationManager.AppSettings["montominimocheque"].ToString());
                                if (diferencia > 0 && diferencia >= montominimocheque)  //Si la diferencia es mayor a cero inserto otro movimiento en macola 
                                {
                                    #region Enviar a GL el monto de la diferencia pero en negativo ( contrario a lo que traigo ) 
                                    wsMacola.Gasto gastoGLDiferencia = new wsMacola.Gasto();
                                    gastoGLDiferencia.id_trx = Int32.Parse(rgPago.Items[item]["Id_PagElec"].Text);
                                    gastoGLDiferencia.cus_type_cd = rgPago.Items[item]["Id_Cd"].Text;
                                    if (rgPago.Items[item]["Acr_NumeroGenerado"].Text.Length > 12)
                                        gastoGLDiferencia.vend_no = rgPago.Items[item]["Acr_NumeroGenerado"].Text.Substring(0, 12);
                                    else
                                        gastoGLDiferencia.vend_no = rgPago.Items[item]["Acr_NumeroGenerado"].Text;
                                // JFCV TODO ver de cuanto es el margen para insertar diferencias 
                                    gastoGLDiferencia.no_ref = Convert.ToString(((RadTextBox)(rgPago.Items[item]["Acr_NumeroGenerado"].FindControl("TxtNumeroAcreedor"))).Text);

                                    gastoGLDiferencia.inv_no = "";
                                    gastoGLDiferencia.amt = diferencia * -1;
                                    gastoGLDiferencia.trx_dt = Convert.ToDateTime(DateTime.Now);

                                    gastoGLDiferencia.sub_amt = gastoGLDiferencia.amt;
                                    gastoGLDiferencia.tax_amt = 0;
                                    gastoGLDiferencia.ret_amt = 0;
                                    gastoGLDiferencia.iva_ret = 0;
                                    gastoGLDiferencia.dwn = null; //null
                                    gastoGLDiferencia.dwn_dt = null; //null
                                    gastoGLDiferencia.vchr_no = null; //null
                                    gastoGLDiferencia.mov_type = null; //null
                                    //jfcv 0909 2016 agregar columan de subtipo para enviarselo a macola 
                                    gastoGLDiferencia.mov_type = pagoelectronico.Id_PagElecSubTipo.ToString();
                                    if (Convert.ToInt32(gastoGLDiferencia.mov_type) == -1)
                                        gastoGLDiferencia.mov_type = null;
                                    //fin jfcv 09092016

                                    gastoGLDiferencia.bank_name = null;
                                    gastoGLDiferencia.bank_account = null;
                                    gastoGLDiferencia.SAT_no = "";
                                    gastoGLDiferencia.flg_test = false;
                                    gastoGLDiferencia.account_no = String.Concat(
                                     CerosDerecha(pagoelectronico.PagElec_Cc, 8),
                                     CerosDerecha(pagoelectronico.PagElec_SubCuenta, 8),
                                     CerosDerecha(pagoelectronico.PagElec_SubSubCuenta, 8)
                                    );
                                    //JFCV 22 ago 2016 agregar el mail del usuario 
                                    gastoGLDiferencia.mail_usu = session.U_Correo;
                                    lstGasto.Add(gastoGLDiferencia);
                                    #endregion Enviar a GL el monto de la diferencia pero en negativo ( contrario a lo que traigo )
                                }
                                oDBaut.BeginTransaction();


                                //jfcv 29 mzo 2017 adecuaciones para Gastos OCK que no envíe a macola si la configuración esta en false
                                bool EnviaAMacola = true;
                                if (ConfigurationManager.AppSettings["EnviaAMacola"] != null)
                                {
                                    EnviaAMacola = Convert.ToBoolean(ConfigurationManager.AppSettings["EnviaAMacola"].ToString());
                                }
                                string resultado = "";
                                if (EnviaAMacola == true)
                                    resultado = wsMac.insGastoViaje(MacolaKey, lstGasto.ToArray());
                                else
                                    resultado = "ok";

                  
                              //jfcv 29 mzo 2017 adecuaciones para Gastos OCK que no envíe a macola si la configuración esta en false 
                              //  string resultado = wsMac.insGastoViaje(MacolaKey, lstGasto.ToArray());
                                 

                                if (resultado == "ok")
                                {

                                    //JFCV obtengo el monto minimo por el que puedo generar un cheque , 
                                    // si la diferencia es menor a ese monto , entonces no genero el segundo movimiento 
                                    // a la tabla de CXP
                                 


                                    if (diferencia > 0 && diferencia >= montominimocheque)  //Si la diferencia es mayor a cero inserto otro movimiento en macola 
                                    {
                                        wsMacola.Gasto gastodferencia = new wsMacola.Gasto();

                                        #region Enviar el gasto en caso de que tenga una diferencia a favor


                                        gastodferencia.id_trx = Int32.Parse(rgPago.Items[item]["Id_PagElec"].Text);
                                        gastodferencia.cus_type_cd = rgPago.Items[item]["Id_Cd"].Text;
                                        //JFCV 29 sep 2015 el valor de vend_no truncarlo a 12 caracteres porque esa es la longitud en la tabla 
                                        if (rgPago.Items[item]["Acr_NumeroGenerado"].Text.Length > 12)
                                            gastodferencia.vend_no = rgPago.Items[item]["Acr_NumeroGenerado"].Text.Substring(0, 12);
                                        else
                                            gastodferencia.vend_no = rgPago.Items[item]["Acr_NumeroGenerado"].Text;
                                        //JFCV agregar el numero de referencia en el web service
                                        gastodferencia.no_ref = Convert.ToString(((RadTextBox)(rgPago.Items[item]["Acr_NumeroGenerado"].FindControl("TxtNumeroAcreedor"))).Text);


                                        gastodferencia.inv_no = "";
                                        gastodferencia.amt = diferencia;
                                       // gastodferencia.trx_dt = Convert.ToDateTime(rgPago.Items[item]["GV_FechaSalida"].Text);
                                        gastodferencia.trx_dt = Convert.ToDateTime(DateTime.Now);

                                        //JFCV agregar retenciones si es una factura de soporte el iva y el impuesto retenido es cero
                                        gastodferencia.sub_amt = diferencia;
                                        gastodferencia.tax_amt = 0;
                                        gastodferencia.ret_amt = 0;
                                        gastodferencia.iva_ret = 0;

                                        gastodferencia.dwn = null; //null
                                        gastodferencia.dwn_dt = null; //null
                                        gastodferencia.vchr_no = null; //null
                                        gastodferencia.mov_type = null; //null
                                        //jfcv 0909 2016 agregar columan de subtipo para enviarselo a macola 
                                        gastodferencia.mov_type = pagoelectronico.Id_PagElecSubTipo.ToString();
                                        if (Convert.ToInt32(gastodferencia.mov_type) == -1)
                                            gastodferencia.mov_type = null;
                                        //fin jfcv 09092016
                                        gastodferencia.bank_name = null;
                                        gastodferencia.bank_account = null;
                                        gastodferencia.SAT_no = "";
                                        gastodferencia.flg_test = false;
                                       
                                        //JFCV para el movimiento de diferencia en CXP debo insertar la cuenta del pago electronico 
                                        // y no la cuenta de los comprobantes 
                                        //gastodferencia.account_no = String.Concat(
                                        // CerosDerecha(rgPago.Items[item]["GV_Cc"].Text, 8),
                                        // CerosDerecha(rgPago.Items[item]["GV_SubCuenta"].Text, 8),
                                        // CerosDerecha(rgPago.Items[item]["GV_SubSubCuenta"].Text, 8) 
                                        //);

                                        gastodferencia.account_no = String.Concat(
                                         CerosDerecha(pagoelectronico.PagElec_Cc, 8),
                                         CerosDerecha(pagoelectronico.PagElec_SubCuenta, 8),
                                         CerosDerecha(pagoelectronico.PagElec_SubSubCuenta, 8) 
                                        );

                                        //JFCV 22 ago 2016 agregar el mail del usuario 
                                        gastodferencia.mail_usu = session.U_Correo;
                                        lstGastoDiferencia.Add(gastodferencia);

                                        #endregion Enviar si tiene archivo de soporte


                                        //jfcv 29 mzo 2017 adecuaciones para Gastos OCK que no envíe a macola si la configuración esta en false
                                        
                                       
                                        string resultadodiferencia  = "";
                                        if (EnviaAMacola == true)
                                            resultadodiferencia = wsMac.insGasto(MacolaKey, lstGastoDiferencia.ToArray());
                                        else
                                            resultadodiferencia = "ok";

                                        //jfcv 29 mzo 2017 adecuaciones para Gastos OCK que no envíe a macola si la configuración esta en false 


                                        //string resultadodiferencia = wsMac.insGasto(MacolaKey, lstGastoDiferencia.ToArray());
                                        if (resultadodiferencia != "ok")
                                        {
                                            oDBaut.RollBack();
                                            Alerta("No se pudo realizar la autorización." + resultadodiferencia);
                                            return;
                                        }


                                    }

                                    clsGastoViajeAut.AutorizarGastoViaje(gastoViaje, session.Emp_Cnx, ref verificadorins);
                                    if (verificadorins == 1)
                                    {
                                        oDBaut.Commit();
                                    }
                                    else
                                    {
                                        oDBaut.RollBack();
                                        Alerta("Ocurrio un error al realizar la autorización.");
                                    }


                                    try
                                    {
                                        PagoElectronico pagoElec2 = new PagoElectronico();
                                        pagoElec2.Id_PagElec = Int32.Parse(rgPago.Items[item]["Id_PagElec"].Text);

                                        EnviarCorreo(pagoElec2, 1);
                                    }
                                    catch (Exception ex)
                                    {
                                        Alerta(ex.Message);
                                    }

                                }
                                else
                                {
                                    oDBaut.RollBack();
                                    Alerta("No se pudo realizar la autorización.");
                                }


                                #endregion Autorizar


                            }

                            catch (Exception ex)
                            {
                                oDBaut.RollBack();
                                throw ex;
                            }
                            finally
                            {
                                oDBaut.Dispose();
                            }
                        }
                        else  // si la diferencia no es 0 o a favor no hace nada 
                        {
                            Alerta("Se cuenta con Saldo en contra para enviar a autorización debe comprobar el saldo solicitado.");
                        }

                        rgPago.Rebind();

                        break;

                        #endregion Autorizar COmprobante
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }



        private DataTable ObtenerDatos(byte[] xmlObject)
        {
            DataTable dtable = new DataTable();
            dtable.Columns.Add("rfc", typeof(string));
            dtable.Columns.Add("fecha", typeof(DateTime));
            dtable.Columns.Add("serie", typeof(string));
            dtable.Columns.Add("folio", typeof(string));
            dtable.Columns.Add("importe", typeof(string));
            dtable.Columns.Add("UUID", typeof(string));
            dtable.Columns.Add("subtotal", typeof(string));
            dtable.Columns.Add("iva", typeof(string));
            dtable.Columns.Add("impuestoretenido", typeof(string));
            dtable.Columns.Add("ivaretenido", typeof(string));

            if (xmlObject != null && xmlObject.Length > 0)
            {
                try
                {
                    MemoryStream xmlStream = new MemoryStream(xmlObject);
                    string rfc = null;
                    string serie = null;
                    string folio = null;
                    DateTime? fecha = null;
                    decimal? importe = null;
                    string UUID = null;
                    decimal? subtotal = null;
                    decimal? ivaretenido = null;
                    decimal? impuestoretenido = null;
                    decimal? iva = null;


                    XmlDocument xmldoc = new XmlDocument();
                    XmlNodeList xmlnode;

                    try
                    {
                        xmldoc.Load(xmlStream);
                        Session["xml"] = xmlStream;
                    }
                    catch (Exception ex)
                    {
                        dtable = null;
                        //Alerta("Problemas al leer el XML de la factura" + ex.Message);
                        return dtable;
                    }
                    xmlnode = xmldoc.GetElementsByTagName("cfdi:Comprobante");
                    try
                    {
                        serie = xmlnode[0].Attributes["serie"].Value;
                    }
                    catch
                    {
                        try
                        {
                            serie = xmlnode[0].Attributes["Serie"].Value;
                        }
                        catch
                        {
                            serie = "";
                        }
                    }
                    //JFCV 04 ene 2016 algunas facturas pueden no traer serie ni folio
                    try
                    {
                        folio = xmlnode[0].Attributes["folio"].Value;
                    }
                    catch
                    {

                        try
                        {
                            folio = xmlnode[0].Attributes["Folio"].Value;
                        }
                        catch
                        {
                            folio = "";
                        }

                    }

                    //JFCV 11 AGO2017 TUVE QUE PONER UN TRY CATCH PORQUE EN LA VERSION DE CFDI 3.3 CAMBIaron nombre por ejemplo fecha por Fecha
                    try
                    {
                        fecha = Convert.ToDateTime(xmlnode[0].Attributes["fecha"].Value);
                        importe = decimal.Parse("0" + xmlnode[0].Attributes["total"].Value);

                        //jfcv 24nov2015 Agregar 3 valores de impuestos
                        subtotal = decimal.Parse("0" + xmlnode[0].Attributes["subTotal"].Value);
                        //jfcv fin
                    }
                    catch
                    {
                        fecha = Convert.ToDateTime(xmlnode[0].Attributes["Fecha"].Value);
                        importe = decimal.Parse("0" + xmlnode[0].Attributes["Total"].Value);

                        //jfcv 24nov2015 Agregar 3 valores de impuestos
                        subtotal = decimal.Parse("0" + xmlnode[0].Attributes["SubTotal"].Value);
                        //jfcv fin
                    }
                    xmlnode = xmldoc.GetElementsByTagName("cfdi:Emisor");
                    //JFCV 11 AGO2017 TUVE QUE PONER UN TRY CATCH PORQUE EN LA VERSION DE CFDI 3.3 CAMBIaron nombre por ejemplo fecha por Fecha
                    try
                    {
                        rfc = xmlnode[0].Attributes["rfc"].Value;
                    }
                    catch
                    {
                        rfc = xmlnode[0].Attributes["Rfc"].Value;
                    }

                    xmlnode = xmldoc.GetElementsByTagName("tfd:TimbreFiscalDigital");
                    UUID = xmlnode[0].Attributes["UUID"].Value;
                    if (folio == "")
                    {
                        folio = UUID.Substring(0, 10);
                    }


                    iva = 0;

                    try
                    {

                        xmlnode = xmldoc.GetElementsByTagName("cfdi:Traslados");
                        if (xmlnode[0].ChildNodes.Count > 0)
                        {
                            if (xmlnode[0].ChildNodes.Count == 1)
                            {
                                //JFCV 11 AGO2017 TUVE QUE PONER UN TRY CATCH PORQUE EN LA VERSION DE CFDI 3.3 CAMBIaron nombre por ejemplo fecha por Fecha
                                try
                                {
                                    if (xmlnode[0].ChildNodes[0].Attributes["impuesto"].Value == "IVA")
                                        iva = decimal.Parse("0" + xmlnode[0].ChildNodes[0].Attributes["importe"].Value);
                                }
                                catch
                                {
                                    if (xmlnode[0].ChildNodes[0].Attributes["Impuesto"].Value == "002")
                                        iva = decimal.Parse("0" + xmlnode[0].ChildNodes[0].Attributes["Importe"].Value);
                                }
                            }
                            else
                            {
                                //JFCV 11 AGO2017 TUVE QUE PONER UN TRY CATCH PORQUE EN LA VERSION DE CFDI 3.3 CAMBIaron nombre por ejemplo fecha por Fecha
                                try
                                {
                                    if (xmlnode[0].ChildNodes[0].Attributes["impuesto"].Value == "IVA")
                                        iva = decimal.Parse("0" + xmlnode[0].ChildNodes[0].Attributes["importe"].Value);

                                    if (xmlnode[0].ChildNodes[1].Attributes["impuesto"].Value == "IVA")
                                        iva = iva + decimal.Parse("0" + xmlnode[0].ChildNodes[1].Attributes["importe"].Value);
                                }
                                catch
                                {
                                    if (xmlnode[0].ChildNodes[0].Attributes["Impuesto"].Value == "002")
                                        iva = decimal.Parse("0" + xmlnode[0].ChildNodes[0].Attributes["Importe"].Value);

                                    if (xmlnode[0].ChildNodes[1].Attributes["Impuesto"].Value == "002")
                                        iva = iva + decimal.Parse("0" + xmlnode[0].ChildNodes[1].Attributes["Importe"].Value);
                                }
                            }
                        }

                    }
                    catch
                    {

                    }

                    ivaretenido = 0;
                    impuestoretenido = 0;

                    try
                    {

                        xmlnode = xmldoc.GetElementsByTagName("cfdi:Retenciones");
                        if (xmlnode[0].ChildNodes.Count > 0)
                        {

                            if (xmlnode[0].ChildNodes.Count == 1)
                            {
                                //JFCV 11 AGO2017 TUVE QUE PONER UN TRY CATCH PORQUE EN LA VERSION DE CFDI 3.3 CAMBIaron nombre por ejemplo fecha por Fecha
                                try
                                {
                                    if (xmlnode[0].ChildNodes[0].Attributes["impuesto"].Value == "IVA")
                                        ivaretenido = decimal.Parse("0" + xmlnode[0].ChildNodes[0].Attributes["importe"].Value);
                                    else
                                        impuestoretenido = decimal.Parse("0" + xmlnode[0].ChildNodes[0].Attributes["importe"].Value);
                                }
                                catch
                                {
                                    if (xmlnode[0].ChildNodes[0].Attributes["Impuesto"].Value == "002")
                                        ivaretenido = decimal.Parse("0" + xmlnode[0].ChildNodes[0].Attributes["Importe"].Value);
                                    else
                                        impuestoretenido = decimal.Parse("0" + xmlnode[0].ChildNodes[0].Attributes["Importe"].Value);
                                }
                            }
                            else
                            {
                                //JFCV 11 AGO2017 TUVE QUE PONER UN TRY CATCH PORQUE EN LA VERSION DE CFDI 3.3 CAMBIaron nombre por ejemplo fecha por Fecha
                                try
                                {
                                    if (xmlnode[0].ChildNodes[0].Attributes["impuesto"].Value == "IVA")
                                        ivaretenido = decimal.Parse("0" + xmlnode[0].ChildNodes[0].Attributes["importe"].Value);
                                    else
                                        impuestoretenido = decimal.Parse("0" + xmlnode[0].ChildNodes[0].Attributes["importe"].Value);

                                    if (xmlnode[0].ChildNodes[1].Attributes["impuesto"].Value == "IVA")
                                        ivaretenido = decimal.Parse("0" + xmlnode[0].ChildNodes[1].Attributes["importe"].Value);
                                    else
                                        impuestoretenido = decimal.Parse("0" + xmlnode[0].ChildNodes[1].Attributes["importe"].Value);
                                }
                                catch
                                {
                                    if (xmlnode[0].ChildNodes[0].Attributes["Impuesto"].Value == "002")
                                        ivaretenido = decimal.Parse("0" + xmlnode[0].ChildNodes[0].Attributes["Importe"].Value);
                                    else
                                        impuestoretenido = decimal.Parse("0" + xmlnode[0].ChildNodes[0].Attributes["Importe"].Value);

                                    if (xmlnode[0].ChildNodes[1].Attributes["Impuesto"].Value == "002")
                                        ivaretenido = decimal.Parse("0" + xmlnode[0].ChildNodes[1].Attributes["Importe"].Value);
                                    else
                                        impuestoretenido = decimal.Parse("0" + xmlnode[0].ChildNodes[1].Attributes["Importe"].Value);
                                }
                            }
                        }

                    }
                    catch
                    {

                    }

                    dtable.Rows.Add(rfc, fecha, serie, folio, importe, UUID, subtotal, iva, impuestoretenido, ivaretenido);

                }
                catch (Exception ex)
                {
                    dtable = null;
                }
            }
            else
            {
                dtable = null;
            }


            return dtable != null && dtable.Rows.Count > 0 ? dtable : null;
        }

        private string CerosDerecha(string Valor, int Len)
        {
            string Result = "";

            Result = string.Concat(Valor.Trim(), String.Join("", Enumerable.Repeat("0", Len - Valor.Trim().Length)));

            return Result;
        }

        private void Alerta(string mensaje)
        {
            try
            {
                //jfcv 23 ago 2016 
                RAM1.ResponseScripts.Add("radalert('" + mensaje + "</br></br>', 330, 150);");
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "Alerta");
            }
        }

        protected void rgPago_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            try
            {
                ErrorManager();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "rg1_PageIndexChanged");
            }
        }

        protected void RAM1_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            rgPago.Rebind();
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
                    //_PermisoGuardar = Permiso.PGrabar;
                    //_PermisoModificar = Permiso.PModificar;
                    //_PermisoEliminar = Permiso.PEliminar;
                    //_PermisoImprimir = Permiso.PImprimir;

                    //if (Permiso.PGrabar == false)
                    //{
                    //    this.rtb1.Items[6].Visible = false;
                    //}
                    //if (Permiso.PGrabar == false && Permiso.PModificar == false)
                    //{
                    //    this.rtb1.Items[5].Visible = false;
                    //}
                    //this.rtb1.Items[4].Visible = false;
                    ////Eliminar
                    //this.rtb1.Items[3].Visible = false;
                    ////Imprimir
                    //this.rtb1.Items[2].Visible = false;
                    ////Correo
                    //this.rtb1.Items[1].Visible = false;
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
            rgPago.Rebind();
        }

        private List<GastoViaje> GetList()
        {
            try
            {
                CN_CapGastoViaje clsGastoViaje = new CN_CapGastoViaje();
                List<GastoViaje> list = new List<GastoViaje>();

                Sesion session = new Sesion();
                session = (Sesion)Session["Sesion" + Session.SessionID];

                GastoViaje gastoViaje = new GastoViaje();
                gastoViaje.Id_Emp = session.Id_Emp;
                gastoViaje.Id_Cd = session.Id_Cd_Ver;

                clsGastoViaje.ConsultaGastoViaje(gastoViaje, session.Emp_Cnx, ref list);
                //JFCV cambio de estatus ahora el que debe usar es el 2 
                //return list.Where(x => x.Id_GVEst == 1).ToList();
                //JFCV 02 feb regresa solo los reg que coincidan con la referencia 1 Gtos de viaje 2 es aut comp acreedores 
     
                int reference = Convert.ToInt32(Request.QueryString["ref"]);
                if (reference == 1)
                {
                    return list.Where(x => (x.Id_GVEst == 2) && (Convert.ToInt32(Request.QueryString["ref"]) == x.GV_TipoGasto)).ToList();

                }
                else
                {
                    return list.Where(x => (x.Id_GVEst == 2 ) && (x.GV_TipoGasto == 2 || x.GV_TipoGasto == 4)).ToList();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void descargarXML(int id_PagElec)
        {
            PagoElectronico pagoElectronico = new PagoElectronico();

            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];

            pagoElectronico.Id_Emp = Sesion.Id_Emp;
            pagoElectronico.Id_Cd = Sesion.Id_Cd_Ver;
            pagoElectronico.Id_PagElec = id_PagElec;

            CN_CapPagoElectronico clsPagoElectronico = new CN_CapPagoElectronico();
            clsPagoElectronico.ConsultaPagoElectronico(pagoElectronico, Sesion.Emp_Cnx);

            string ruta = null;
            System.IO.StreamWriter sw = null;
            //ruta = Server.MapPath("Reportes") + "\\archivoXml" + Sesion.Id_U.ToString() + "PagElec" + id_PagElec.ToString() + ".txt";

            //if (File.Exists(ruta))
            //    File.Delete(ruta);
            //if (File.Exists(Server.MapPath("Reportes") + "\\archivoXml" + Sesion.Id_U.ToString() + "PagElec" + id_PagElec.ToString() + ".xml"))
            //    File.Delete(Server.MapPath("Reportes") + "\\archivoXml" + Sesion.Id_U.ToString() + "PagElec" + id_PagElec.ToString() + ".xml");
            //sw = new System.IO.StreamWriter(ruta, false, Encoding.UTF8);
            //sw.WriteLine(pagoElectronico.PagElec_Xml.ToString());
            //sw.Close();
            //File.Move(ruta, Server.MapPath("Reportes") + "\\archivoXml" + Sesion.Id_U.ToString() + "PagElec" + id_PagElec.ToString() + ".xml");
            //RAM1.ResponseScripts.Add(string.Concat(@"abrirArchivo('Reportes\\archivoXml" + Sesion.Id_U.ToString() + "PagElec", id_PagElec.ToString(), ".xml')"));

            //
            string rutadestino = ConfigurationManager.AppSettings["URLtempPDF"].ToString();
            ruta = Server.MapPath(string.Concat(rutadestino, Sesion.Id_U.ToString() + "GV" + id_PagElec.ToString() + ".txt"));

            if (File.Exists(ruta))
                File.Delete(ruta);
            if (File.Exists(rutadestino + "\\archivoXml" + Sesion.Id_U.ToString() + "GV" + id_PagElec.ToString() + ".xml"))
                File.Delete(rutadestino + "\\archivoXml" + Sesion.Id_U.ToString() + "GV" + id_PagElec.ToString() + ".xml");
            sw = new System.IO.StreamWriter(ruta, false, Encoding.UTF8);
            sw.WriteLine(pagoElectronico.PagElec_Xml.ToString());
            sw.Close();
            File.Move(ruta, rutadestino + "\\archivoXml" + Sesion.Id_U.ToString() + "GV" + id_PagElec.ToString() + ".xml");
            RAM1.ResponseScripts.Add(string.Concat(@"abrirArchivo('" + rutadestino + "\\archivoXml" + Sesion.Id_U.ToString() + "GV", id_PagElec.ToString(), ".xml')"));

        }



        private void descargarPDF(int id_PagElec)
        {
            PagoElectronico pagoElectronico = new PagoElectronico();

            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];

            pagoElectronico.Id_Emp = Sesion.Id_Emp;
            pagoElectronico.Id_Cd = Sesion.Id_Cd_Ver;
            pagoElectronico.Id_PagElec = id_PagElec;

            CN_CapPagoElectronico clsPagoElectronico = new CN_CapPagoElectronico();
            clsPagoElectronico.ConsultaPagoElectronico(pagoElectronico, Sesion.Emp_Cnx);

            byte[] archivoPdf = pagoElectronico.PagElec_Pdf;

            if (archivoPdf != null)
            {
                if (archivoPdf.Length > 0)
                {
                    string tempPDFname = string.Concat("GASTO_"
                             , Sesion.Id_Emp.ToString()
                             , "_", Sesion.Id_Cd.ToString()
                             , "_", id_PagElec.ToString()
                             , ".pdf");
                    string URLtempPDF = Server.MapPath(string.Concat(ConfigurationManager.AppSettings["URLtempPDF"].ToString(), tempPDFname));
                    string WebURLtempPDFGastos = string.Concat(ConfigurationManager.AppSettings["WebURLtempPDFGastos"].ToString(), tempPDFname);
                    this.ByteToTempPDF(URLtempPDF, archivoPdf);
                    // ------------------------------------------------------------------------------------------------
                    // Ejecuta funciós JS para abrir una nueva ventada de Explorador y visualizar el archivo PDF
                    // ------------------------------------------------------------------------------------------------
                    RAM1.ResponseScripts.Add(string.Concat(@"AbrirFacturaPDF('", WebURLtempPDFGastos, "')"));
                }
            }
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

        private void EnviarCorreo(PagoElectronico pagoElectronico, int tipotransaccion)
        {
            Sesion session = new Sesion();
            session = (Sesion)Session["Sesion" + Session.SessionID];

            try
            {

                ConfiguracionGlobal configuracion = new ConfiguracionGlobal();
                configuracion.Id_Cd = session.Id_Cd_Ver;
                configuracion.Id_Emp = session.Id_Emp;
                CN_Configuracion cn_configuracion = new CN_Configuracion();
                cn_configuracion.Consulta(ref configuracion, session.Emp_Cnx);

                Usuario usu = new Usuario();
                CN_CatUsuario cn_catusuario = new CN_CatUsuario();
                usu.Id_Emp = session.Id_Emp;
                usu.Id_Cd = session.Id_Cd_Ver;
                usu.Id_U = session.Id_U;
                string Correo_Usuario = "";
                cn_catusuario.ConsultaCorreoUsuario(usu, session.Emp_Cnx, ref Correo_Usuario);

                StringBuilder cuerpo_correo = new StringBuilder();
                cuerpo_correo.Append("<table>");
                cuerpo_correo.Append("<tr>");
                //cuerpo_correo.Append("<td>Le informamos que su solicitud de gastos ha sido autorizada, pago al acreedor {Acr_Nombre} por el monto de $" + pagoElectronico.PagElec_Importe + "</td>");
                if (tipotransaccion == 1) //es una autorización 
                {
                    cuerpo_correo.Append("<td>Le informamos que su comprobación de gastos de Viaje ha sido autorizada. </td>");
                    cuerpo_correo.Append("</tr>");
                    cuerpo_correo.Append("<tr>");
                    cuerpo_correo.Append("<td>Número de Solicitud : {pagElec}</td>");
                    cuerpo_correo.Append("</tr>");
                }
                else
                {
                    cuerpo_correo.Append("<td>Le informamos que la comprobación de gastos de Viaje  ha sido rechazada. </td>");
                    cuerpo_correo.Append("</tr>");
                    cuerpo_correo.Append("<tr>");
                    cuerpo_correo.Append("<td>Número de Solicitud : {pagElec}</td>");
                    cuerpo_correo.Append("</tr>");
                    cuerpo_correo.Append("<tr>");
                    cuerpo_correo.Append("<td>Motivo Rechazo : {pagElecMotivoRechazo}</td>");
                    cuerpo_correo.Append("</tr>");
                }
                cuerpo_correo.Append("</tr>");
                //cuerpo_correo.Append("<tr>");
                //cuerpo_correo.Append("<td>Cuenta : {pagElecCuenta_Descripcion}</td>");
                //cuerpo_correo.Append("</tr>");
                //cuerpo_correo.Append("<tr>");
                //cuerpo_correo.Append("<td>Concepto : " + pagoElectronico.PagElecConcepto_Descripcion + "</td>");
                //cuerpo_correo.Append("</tr>");
                cuerpo_correo.Append("<tr>");
                cuerpo_correo.Append("<td><br></td>");
                cuerpo_correo.Append("</tr>");
                cuerpo_correo.Append("<tr>");
                cuerpo_correo.Append("<td>Accesar a : <a href='{href}'>Rastreo de Solicitud</a></td>");
                cuerpo_correo.Append("</tr>");
                cuerpo_correo.Append("<tr>");
                cuerpo_correo.Append("<td>&nbsp;</td>");
                cuerpo_correo.Append("</tr>");
                cuerpo_correo.Append("</table>");

                string strUrl = (HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + HttpContext.Current.Request.ApplicationPath + "/").Replace("//", "/").Replace("http:/", "http://");
                string txtCuerpoMail = cuerpo_correo.ToString();
                //txtCuerpoMail = txtCuerpoMail.Replace("{Acr_Nombre}", pagoElectronico.Acr_Nombre);
                // txtCuerpoMail = txtCuerpoMail.Replace("{PagElec_Solicitante}", pagoElectronico.PagElec_Solicitante);
                // txtCuerpoMail = txtCuerpoMail.Replace("{pagElecCuenta_Descripcion}", pagoElectronico.pagElecCuenta_Descripcion);

                if (tipotransaccion == 1) //es una autorización 
                {
                    txtCuerpoMail = txtCuerpoMail.Replace("{href}", strUrl + "RepPagoElectronico.aspx");
                }
                else
                {   // cuando es un rechazo
                    txtCuerpoMail = txtCuerpoMail.Replace("{href}", strUrl + "CapGastoViajePendiente.aspx");
                    txtCuerpoMail = txtCuerpoMail.Replace("{pagElecMotivoRechazo}", pagoElectronico.PagElec_MotivoRechazo);

                }

                txtCuerpoMail = txtCuerpoMail.Replace("{pagElec}", pagoElectronico.Id_PagElec.ToString());


                SmtpClient smtp = new SmtpClient();
                //smtp.EnableSsl = true;
                smtp.Host = configuracion.Mail_Servidor;
                smtp.Port = Convert.ToInt32(configuracion.Mail_Puerto);
                smtp.Credentials = new NetworkCredential(configuracion.Mail_Usuario, configuracion.Mail_Contraseña);
                MailAddress from = new MailAddress(configuracion.Mail_Remitente);
                MailMessage mail = new MailMessage();
                mail.From = from;
                mail.IsBodyHtml = true;
                mail.BodyEncoding = System.Text.Encoding.UTF8;

                if (tipotransaccion == 1) //es una autorización 
                {
                    mail.Subject = "(SIANWEB) Notificación de  Comprobación de Gasto de Viaje Autorizado.";
                }
                else
                {   // cuando es un rechazo
                    mail.Subject = "(SIANWEB) Notificación de Comprobación de Gasto de Viaje Rechazado.";
                }

                mail.To.Add(new MailAddress(Correo_Usuario));
                //JFCV envia mail al encargado de la comprobación y al gerente de la sucursal 
                mail.To.Add(new MailAddress(configuracion.Mail_GastosAvisoGerente));

                mail.Body = txtCuerpoMail;
                smtp.Send(mail);


            }
            catch (Exception ex)
            {
                Alerta("El correo no pudo ser enviado. Error: " + ex.Message);
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
    }
}