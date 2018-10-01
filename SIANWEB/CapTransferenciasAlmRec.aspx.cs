using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.IO;
using System.Net;
using Telerik.Web.UI;
using CapaEntidad;
using CapaNegocios;
using System.Collections;
using System.Text;
using System.Xml;
using CapaDatos;
using System.Globalization;
using System.Threading;

namespace SIANWEB
{
    public partial class CapTransferenciasAlmRec : System.Web.UI.Page
    {
        #region Variables
   
        private Sesion sesion
        {
            get
            {
                return (Sesion)Session["Sesion" + Session.SessionID];
            }
        }
        public int strEmp
        {
            get
            {
                int VGEmpresa = 0;
                Int32.TryParse(System.Configuration.ConfigurationManager.AppSettings["VGEmpresa"], out VGEmpresa);
                return VGEmpresa;
            }
        }
        private bool _PermisoGuardar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoModificar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoEliminar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoImprimir { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public List<TransferenciaAlmDet> ListDet
        {
            get
            {
                return (Session["ListTraDet" + Session.SessionID] as List<TransferenciaAlmDet>);
            }
            set
            {
                Session["ListTraDet" + Session.SessionID] = value;
            }
 
        }

        #endregion

        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (sesion == null)
                {
                    CerrarVentana();

                }
                else
                {
                    if (!Page.IsPostBack)
                    {
                        Inicializar();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }

        }
        protected void RAM1_AjaxRequest1(object sender, AjaxRequestEventArgs e)
        {
            try
            {
                switch (e.Argument.ToString())
                {
                    case "panel":
                        //Unit altura = (Unit)(Convert.ToDouble(HiddenHeight.Value) - 180);
                        //RadPageViewDetalles.Height = altura;
                        //RadPane1.Height = altura;
                        //RadPane1.Width = RadPageViewDGenerales.Width;
                        //RadSplitter1.Height = altura;
                        //RadPageViewDGenerales.Height = altura;
                        //RadSplitter2.Height = altura;
                        //RadPane2.Height = altura;
                        //RadPane2.Width = RadPageViewDGenerales.Width;
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }

        }
        protected void RadToolBar1_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            try
            {
                RadToolBarButton btn = e.Item as RadToolBarButton;
                switch (btn.CommandName)
                {
                    case "save":
                        Guardar();
                        break;
                }

            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rgTransferencia_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                ErrorManager();
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                {
                    this.rgTransferenciaDet.DataSource = ListDet;
                }
             
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rgTransferencia_UpdateCommand(object source, GridCommandEventArgs e)
        {
            try
            {


                ErrorManager();
                GridEditableItem Item = (GridEditableItem)e.Item;
                string UniqueID = Item.OwnerTableView.DataKeyValues[Item.ItemIndex]["UniqueID"].ToString();
                List<TransferenciaAlmDet> List = new List<TransferenciaAlmDet>();

                List = ListDet;

                double TotalRec = Convert.ToInt32((Item["TraD_CantRec"].FindControl("TxtTraD_CantRec") as RadNumericTextBox).Text) * Convert.ToDouble((Item["TraD_Costo"].FindControl("TxtTraD_Costo") as RadNumericTextBox).Text);
                int Diferencia = Convert.ToInt32((Item["TraD_Cant"].FindControl("TxtTraD_Cant") as RadNumericTextBox).Text) - Convert.ToInt32((Item["TraD_CantRec"].FindControl("TxtTraD_CantRec") as RadNumericTextBox).Text);
               
                List.Where(o => o.UniqueID == UniqueID).First().TraD_CantRec = Convert.ToInt32((Item["TraD_CantRec"].FindControl("TxtTraD_CantRec") as RadNumericTextBox).Text);
                List.Where(o => o.UniqueID == UniqueID).First().TraD_TotalRec = TotalRec;
                List.Where(o => o.UniqueID == UniqueID).First().TraD_Diferencia = Diferencia;



                ListDet = List;

                rgTransferenciaDet.Rebind();
                CalcularTotales();
     
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void TxtTraD_CantRec_TextChanged(object sender, EventArgs e)
        {
            try
                {
                RadNumericTextBox txtCantRec = (sender) as RadNumericTextBox;
                RadNumericTextBox txtCant =(RadNumericTextBox) txtCantRec.Parent.FindControl("TxtTraD_Cant");
                RadNumericTextBox txtCosto = (RadNumericTextBox)txtCantRec.Parent.FindControl("TxtTraD_Costo");
                RadNumericTextBox txtTotalRec = (RadNumericTextBox)txtCantRec.Parent.FindControl("TxtTraD_TotalRec");
                RadNumericTextBox txtDif = (RadNumericTextBox)txtCantRec.Parent.FindControl("TxtTraD_Diferencia");


                if (txtCantRec.Text == "")
                {
                    txtCantRec.Text = "0";
                    txtCantRec.Value = 0;
                }

                if (txtCantRec.Value > txtCant.Value)
                {
                    Alerta("No se puede recibir mayor cantidad a la enviada");
                    txtCantRec.Value = txtCant.Value;
                    txtCantRec.Focus();
                    return;
                }


                txtTotalRec.Value = txtCantRec.Value * txtCosto.Value;
                txtDif.Value = txtCant.Value - txtCantRec.Value;


            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        #endregion

        #region Funciones
        private void Inicializar()
        {
            try
            {
                Random randObj = new Random(DateTime.Now.Millisecond);
                HF_ClvPag.Value = randObj.Next().ToString();
                int Modificar = int.Parse(Request.QueryString["Mod"]);
                if (Modificar != 1)
                {
         
                    this.rgTransferenciaDet.Columns[13].Visible= false;
                    this.rtb1.Items[1].Visible= false;

                }


       
                ValidarPermisos();
                CargarEncabezado();
                CargarDetalles();
                this.rgTransferenciaDet.Rebind();
                CalcularTotales();


            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        private void CargarEncabezado()
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                TransferenciasAlm tra = new TransferenciasAlm();
                CN_TransferenciasAlm cn_tra = new CN_TransferenciasAlm();

                int Id_Emp = int.Parse(Request.QueryString["Id_Emp"]);
                int Id_Cd = int.Parse(Request.QueryString["Id_Cd"]);
                int Id_Tra = int.Parse(Request.QueryString["Id_Tra"]);

                cn_tra.ProTransferenciaAlmacen_Consulta(Id_Emp, Id_Cd, Id_Tra, ref tra, sesion.Emp_Cnx);

                this.TxtId_Tra.Text = tra.Id_Tra.ToString();
                this.HFId_CdOrigen.Value = tra.Id_CdOrigen.ToString();
                this.TxtId_CdOrigenStr.Text = tra.Id_CdOrigenStr;
                this.TxtId_RemOrigen.Text = tra.Id_RemOrigen.ToString();
                this.TxtTra_Notas.Text = tra.Tra_Notas;
                this.TxtTra_FechaEnvio.Text = tra.Tra_FechaEnvio.ToShortDateString();
                this.TxtTra_FechaRecepcion.Text = tra.Tra_FechaRecepcion.ToString()== "" ? "" :  DateTime.Parse(tra.Tra_FechaRecepcion.ToString()).ToShortDateString();
                this.HFCD_IVA.Value = tra.CD_IVA.ToString();
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
        private void CargarDetalles()
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                List<TransferenciaAlmDet> List = new List<TransferenciaAlmDet>();
                CN_TransferenciasAlm cn_tra = new CN_TransferenciasAlm();

                int Id_Emp = int.Parse(Request.QueryString["Id_Emp"]);
                int Id_Cd = int.Parse(Request.QueryString["Id_Cd"]);
                int Id_Tra = int.Parse(Request.QueryString["Id_Tra"]);

                cn_tra.ProTransferenciaAlmacen_ConsultaDet(Id_Emp, Id_Cd, Id_Tra, ref List, sesion.Emp_Cnx);

                this.ListDet = List;

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

      //JMM: Le pongo la url fija para que tome los permisos de la lista
                pagina.Url = "CapTransferenciasAlm.aspx";
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

                    if (_PermisoGuardar == false)
                    {
                        this.rtb1.Items[1].Visible = false;
 
                    }

                }
                else
                {
                    Response.Redirect("Inicio.aspx");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CerrarVentana()
        {
            try
            {
                string funcion;
                funcion = "CloseAndRebind()";
                string script = "<script>" + funcion + "</script>";
                ScriptManager.RegisterStartupScript(this, this.GetType(), funcion, script, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void Guardar()
        {
            try
            {

                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                int Verificador = 0;
                string VerificadorStr = string.Empty;
                EntradaSalida es = new EntradaSalida();
                List<EntradaSalidaDetalle> List = new List<EntradaSalidaDetalle>();
                CN_EntSalSolicitud  cn_es = new CN_EntSalSolicitud();
               

                //JMM:Se genera una entrada por TM=4, la hago por todo lo enviado para despues poder realizar una remision si hay diferencias
                LlenaObjetosEntrada(ref es, ref List, sesion);


                cn_es.GuardarEntradaSalida(ref es, List, ref VerificadorStr, strEmp, sesion.Emp_Cnx);

                //JMM:Se creó la entrada, ahora actualizo el estatus de la remisión y el de la transferencia
                if (es.Id_Es != 0)
                {
                    TransferenciasAlm tra = new TransferenciasAlm();
                    List<TransferenciaAlmDet> ListTra = new List<TransferenciaAlmDet>();
                    int Remision = 0;
                    CN_TransferenciasAlm cn_tra = new CN_TransferenciasAlm();

                    LlenaObjetosTransferencia(es.Id_Es, ref tra, ref ListTra, sesion);

                    cn_tra.ProTransferenciaAlmacen_Recepcion(tra, ListTra, ref Verificador, ref Remision, sesion.Emp_Cnx);

                    if (Remision == 0)
                    {
                        AlertaCerrar("Se realizó de manera exitosa el proceso de recepción, se creo automáticamente la entrada <b>#" + es.Id_Es.ToString()+"</b>");
                    }
                    else
                    {
                        AlertaCerrar("Se realizó de manera exitosa el proceso de recepción, se creo automáticamente la entrada <b>#" + es.Id_Es.ToString() + "</b><br><br>" + 
                               "Adicionalmente se creo la remisión  <b>#" +  Remision.ToString() + "</b> por los productos que no se recibieron de manera completa" );
                    }

                }
                else
                {
                    Alerta("Error realizar el proceso de recepción de transferencia");
                }



            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
        private void CalcularTotales()
        {
            try
            {
                double TotEnv = 0;
                double TotRec = 0;

                List<TransferenciaAlmDet> List = new List<TransferenciaAlmDet>();
                List = ListDet;

                foreach (TransferenciaAlmDet t in List)
                {
                    TotEnv += t.TraD_TotalEnv;
                    TotRec += t.TraD_TotalRec;
                }

                this.TxtTra_TotalEnv.Text = TotEnv.ToString("N2");
                this.TxtTra_TotalRec.Text = TotRec.ToString("N2");

            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
        private void LlenaObjetosEntrada(ref EntradaSalida es, ref List<EntradaSalidaDetalle> List, Sesion sesion)
        {
            try
            {

                double iva =  (double.Parse(HFCD_IVA.Value) / 100);

                //Encabezado
                es.Id_Emp = sesion.Id_Emp;
                es.Id_Cd = sesion.Id_Cd_Ver;
                es.Id_U = sesion.Id_U;
                es.Es_Naturaleza = 0;
                es.Es_Fecha = DateTime.Now;
                es.Id_Tm = 4;
                es.Id_Cte = -1;
                es.Id_Pvd = int.Parse(this.HFId_CdOrigen.Value);
                es.Es_Referencia = this.TxtId_RemOrigen.Text;
                es.Es_SubTotal = double.Parse(this.TxtTra_TotalEnv.Text);
                es.Es_Iva = double.Parse(this.TxtTra_TotalEnv.Text) * iva;
                es.Es_Total = es.Es_SubTotal + es.Es_Iva;
                es.Es_Estatus = "I";/*Lo mando impreso para que ya no lo puedan modificar */
                es.Id_Ter = -1;
                es.Es_CteCuentaNacional = -1;
                es.Es_CteCuentaContNacional = -1;
                es.Es_FechaReferencia = Convert.ToDateTime(this.TxtTra_FechaEnvio.Text);
                es.Es_Notas = "Movimiento automático creado por recepción de transferencia de almacén #"+ this.TxtId_Tra.Text +  
                              "; remisión #"+ this.TxtId_RemOrigen.Text +  " enviada por la sucursal " +  this.TxtId_CdOrigenStr.Text; 


                //Detalle
                List<TransferenciaAlmDet> ListTra = new List<TransferenciaAlmDet>();
                EntradaSalidaDetalle e;

                ListTra = ListDet;

                foreach (TransferenciaAlmDet t in ListTra)
                {
                    e = new EntradaSalidaDetalle();
                    e.Id_Emp = sesion.Id_Emp;
                    e.Id_Cd = sesion.Id_Cd_Ver;
                    e.Id_Ter = -1;
                    e.Es_BuenEstado = true;
                    e.Id_Prd = t.Id_Prd;
                    e.Es_Cantidad = t.TraD_Cant;
                    e.Es_Costo = t.TraD_Costo;
                    e.Afecta = false;
                    e.Prd_AgrupadoSpo = 0;

                    List.Add(e);
                }

                

            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
        private void LlenaObjetosTransferencia(int Id_Es,ref TransferenciasAlm tra, ref List<TransferenciaAlmDet> List, Sesion sesion)
        {
            try
            {

                //Encabezado
                tra.Id_Emp = sesion.Id_Emp;
                tra.Id_Cd = sesion.Id_Cd_Ver;
                tra.Id_Tra = int.Parse(this.TxtId_Tra.Text);
                tra.Id_Es = Id_Es;
                tra.Id_U = sesion.Id_U;

                //Detalle

                List = ListDet;

            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
        #endregion
        #region ErrorManager
        private void AlertaFocus(string mensaje, string rtb)
        {
            try
            {
                RadAjaxManager1.ResponseScripts.Add("AlertaFocus('" + mensaje + "','" + rtb + "');");
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "Alerta");
            }
        }
        private void AlertaCerrar(string mensaje)
        {
            try
            {
                mensaje = mensaje.Replace(Convert.ToChar(10).ToString(), string.Empty);
                mensaje = mensaje.Replace(Convert.ToChar(13).ToString(), string.Empty);
                RadAjaxManager1.ResponseScripts.Add("CloseWindowA('" + mensaje + "');");
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
                mensaje = mensaje.Replace(Convert.ToChar(10).ToString(), string.Empty);
                mensaje = mensaje.Replace(Convert.ToChar(13).ToString(), string.Empty);
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