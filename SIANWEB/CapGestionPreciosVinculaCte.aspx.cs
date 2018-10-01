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
using Telerik.Reporting.Processing;
using System.Data.OleDb;
using Telerik.Web.UI.GridExcelBuilder;
using System.Collections;
using System.Data.SqlClient;

namespace SIANWEB
{
    public partial class CapGestionPreciosVinculaCte : System.Web.UI.Page
    {
        #region Variables
        public string NombreArchivo;
        public string NombreHojaExcel;
        private List<ConvenioDet> ListDet
        {
            get { return (List<ConvenioDet>)Session["ListDet" + Session.SessionID + HF_Cve.Value]; }
            set { Session["ListDet" + Session.SessionID + HF_Cve.Value] = value; }
        }
        public bool _PermisoGuardar 
        {
            get { if (Session["Sesion" + Session.SessionID] == null) 
                { return false; } 
                return (bool)Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } 
            set 
            { Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } 
        }
        public bool _PermisoModificar
        {
            get
            {
                if (Session["Sesion" + Session.SessionID] == null)
                { return false; }
                return (bool)Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]];
            }
            set
            { Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; }
        }
        private Sesion sesion
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
                        this.ValidarPermisos();
                        if (Sesion.Cu_Modif_Pass_Voluntario == false)
                        {
                            RAM1.ResponseScripts.Add("AbrirContrasenas(); return false;");
                            return;
                        }

                        HFId_PC.Value = Page.Request.QueryString["Id_PC"].ToString();
                        HFTipoOp.Value = Page.Request.QueryString["TipoOp"].ToString();
                        ValidarPermisos();
                        this.CargarCentros();
                        CargarCategoria();
                        CargarUsuarios();
                        Random randObj = new Random(DateTime.Now.Millisecond);
                        HF_Cve.Value = randObj.Next().ToString();
                        this.TblEncabezado.Visible = false;
                        List<ConvenioDet> List = new List<ConvenioDet>();
                        this.rgConvenioDet.DataSource = List;
                        rgConvenioDet.DataBind();

                        if (this.HFId_PC.Value != "0")
                        {
                            ConsultaConvenio();
                        }
                       
   

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
                switch (e.Argument.ToString())
                {
                    case "RebindGrid":
                        //this.rgFacturaRuta.Rebind();
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "RAM1_AjaxRequest");
            }
        }
        protected void rtb1_ButtonClick(object sender, Telerik.Web.UI.RadToolBarEventArgs e)
        {
            try
            {
                ErrorManager();
                RadToolBarButton btn = e.Item as RadToolBarButton;

                if (Page.IsValid)
                {
                    if (btn.CommandName == "guardar")
                    {

                        if (int.Parse(HFId_PC.Value) == 0 && int.Parse(HFTipoOp.Value) == 0)
                        {
                            if (!_PermisoGuardar)
                            {
                                Alerta("No tiene permiso para guardar");
                                return;
                            }

                            Guardar();
                        }
                        else if (int.Parse(HFId_PC.Value) != 0 && int.Parse(HFTipoOp.Value) == 0)
                        {
                            if (!_PermisoModificar)
                            {
                                Alerta("No tiene permiso para modificar");
                                return;
                            }

                            Modificar();
                        }
                        else if (int.Parse(HFId_PC.Value) != 0 && int.Parse(HFTipoOp.Value) == 1)
                        {
                            if (!_PermisoGuardar)
                            {
                                Alerta("No tiene permiso para guardar");
                                return;
                            }

                            Guardar();
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
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
        protected void CmbId_Cat_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                if (CmbId_Cat.SelectedValue != "-1")
                {
                    CargaConsecutivo();
                }
                else
                {
                    this.TxtPC_NoConvenio.Text = string.Empty;
                    TxtPC_NoConvenio.Enabled = true;
                }

            }
            catch (Exception ex)
            {
                
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }

        }
        protected void Customvalidator1_ServerValidate(object source, ServerValidateEventArgs args)
        {
            try
            {
                //args.IsValid = (RadUpload1.InvalidFiles.Count == 0);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void RadAsyncUpload1_FileUploaded(object sender, FileUploadedEventArgs e)
        {
            try
            {

                Label fileName = new Label();
                fileName.Text = e.File.FileName;
                NombreArchivo = e.File.GetNameWithoutExtension().ToString() + DateTime.Now.ToString("ddMMyyyyHHmmss") + e.File.GetExtension();
                NombreHojaExcel = e.File.GetNameWithoutExtension().ToString();

                if (e.IsValid)
                {
                    //ValidFiles.Visible = true;
                    //ValidFiles.Controls.Add(fileName);
                 
                }
            }
            catch (Exception ex)
            {
                Alerta(ex.Message);
            }
        }
        private void BulkCopy(string path, string hoja)
        {
            try
            {
                //'Declare Variables - Edit these based on your particular situation
                String sSQLTable = "TempTableForExcelImport";



                //'Create our connection strings
                string strConn = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties=\"Excel 12.0 xml;HDR=YES\";";
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                String sSqlConnectionString = Sesion.Emp_Cnx;


                //'Series of commands to bulk copy data from the excel file into our SQL table
                OleDbConnection OleDbConn = new OleDbConnection(strConn);
                OleDbCommand OleDbCmd = new OleDbCommand(("SELECT * FROM [" + hoja + "]"), OleDbConn);
                OleDbConn.Open();
                OleDbDataReader dr = OleDbCmd.ExecuteReader();
                SqlBulkCopy bulkCopy = new SqlBulkCopy(sSqlConnectionString);
                bulkCopy.DestinationTableName = sSQLTable;
                bulkCopy.WriteToServer(dr);
                OleDbConn.Close();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        protected void btnImportar_Click(object sender, EventArgs e)
        {
            try
            {
                List<ConvenioDet> List = new List<ConvenioDet>();

                CargarGrid(ref List);                     
                this.rgConvenioDet.DataSource = List;
                rgConvenioDet.DataBind();

            }
            catch (Exception ex)
            {
                
               Alerta(ex.Message);
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
                pagina.Url = "CapGestionPreciosP.aspx";

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

                 _PermisoGuardar = Permiso.PGrabar;
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
                    CN_Comun.LlenaCombo(1, Sesion.Id_Emp, Sesion.Id_U, sesion.Id_Cd_Ver, sesion.Id_Cd, Sesion.Emp_Cnx, "spCatCentroDistribucion_Combo", ref CmbCentro);
                    this.CmbCentro.SelectedValue = Sesion.Id_Cd_Ver.ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargarCategoria()
        {
            try
            {
               
                CN__Comun cn_comun = new CN__Comun();
                string Conexion = System.Configuration.ConfigurationManager.AppSettings["strConnectionCentral"];

                cn_comun.LlenaCombo(Conexion, "spCatConvCategoria_Combo", ref  CmbId_Cat);
          
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
        private void CargarUsuarios()
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CN__Comun cn_comun = new CN__Comun();
                cn_comun.LlenaCombo(sesion.Id_Cd_Ver , 1, 1, sesion.Emp_Cnx, "spCatUsuario_Combo", ref CmbId_ULider);
                cn_comun.LlenaCombo(sesion.Id_Cd_Ver, 1, 1, sesion.Emp_Cnx, "spCatUsuario_Combo", ref CmbId_UEjecutivo);

            }
            catch (Exception ex)
            {
                
                throw  ex;
            }
        }
        private void CargaConsecutivo()
        {
            try
            {
                CN_Convenio cn_conv = new CN_Convenio();
                Convenio conv = new Convenio();
                string Conexion = System.Configuration.ConfigurationManager.AppSettings["strConnectionCentral"];

                cn_conv.ConsultaConsecutivo(int.Parse(CmbId_Cat.SelectedValue), ref conv, Conexion);

                this.HFCat_Consecutivo.Value = conv.Cat_Consecutivo.ToString();
                if (conv.Cat_Consecutivo == 1)
                {
                    TxtPC_NoConvenio.Text = conv.PC_NoConvenio;
                    TxtPC_NoConvenio.Enabled = false;
                }
                else
                {
                    this.TxtPC_NoConvenio.Text = string.Empty;
                    TxtPC_NoConvenio.Enabled = true;
 
                }

                
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
        private void CargarGrid( ref List<ConvenioDet> List)
        {
            try
            {
                OleDbConnection con = default(OleDbConnection);
                string path = Server.MapPath("~/App_Data/RadUploadTemp") + "\\" + NombreArchivo;
                foreach (UploadedFile f in RadUpload1.UploadedFiles)
                {
                    if (File.Exists(path))
                    {
                        File.Delete(path);
                    }
                    f.SaveAs(path, true);
                }

                string strConn = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source='" + path + "';Extended Properties=\"Excel 12.0 xml;HDR=YES;IMEX=1;\"";
                con = new OleDbConnection(strConn);

                con.Close();
                con.Open();

                DataTable dt = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                string hoja = dt.Rows[0].ItemArray[2].ToString().Replace("'", "");
                //lblMensaje.Text = "hoja" + hoja;
                OleDbCommand cmd = new OleDbCommand("select * from [" + hoja + "]", con);
                OleDbDataAdapter dad = new OleDbDataAdapter();
                dad.SelectCommand = cmd;
                DataSet ds = new DataSet();
                dad.Fill(ds);
         
                if (ListDet != null)
                {
                    List = ListDet;
                }

                ConvenioDet c;
                foreach (DataRow dr in ds.Tables[0].Rows)
                {

                    c = new ConvenioDet();
                    if (dr[0].ToString() != "")
                    {
                        //Entidad ent = entidades.FirstOrDefault(o => o.Nombre == label5.Text);
                         c = List.FirstOrDefault(o => o.Id_Prd == Convert.ToInt32(dr[0]));

                         if (c != null)
                         {
                             c.PCD_ClaveProv = dr[1].ToString();
                             c.PCD_PrecioVtaMin = Convert.ToDouble(dr[2]);
                             c.PCD_PrecioVtaMax = Convert.ToDouble(dr[3]);
                             c.PCD_CantidadMax = Convert.ToInt32(dr[4]);
                             c.Id_Moneda = Convert.ToInt32(dr[5]);
                             c.Id_MonedaStr = Convert.ToInt32(dr[5]) == 1 ? "Dlls" : "Mx";
                             //c.PCD_PrecioAAAEsp = Convert.ToDouble(dr[6]);
                             //c.PCD_FechaInicio = Convert.ToDateTime(dr[7]);
                             //Anterior:la fecha fin es menor a la fecha actual
                             c.PCD_PrecioAAAEspA = Convert.ToDateTime(dr[8]) < DateTime.Now ? Convert.ToDouble(dr[6]) : c.PCD_PrecioAAAEspA;
                             c.PCD_FechaInicioA = Convert.ToDateTime(dr[8]) < DateTime.Now ? Convert.ToDateTime(dr[7]) : c.PCD_FechaInicioA;
                             c.PCD_FechaFinA = Convert.ToDateTime(dr[8]) < DateTime.Now ? Convert.ToDateTime(dr[8]) : c.PCD_FechaFinA;
                             //Actual:La fecha inicio es menor a la fecha actual y la fecha fin es mayor a la fecha actual
                             c.PCD_PrecioAAAEsp = (Convert.ToDateTime(dr[7]) <= DateTime.Now && Convert.ToDateTime(dr[8]) >= DateTime.Now) ? Convert.ToDouble(dr[6]) : c.PCD_PrecioAAAEsp;
                             c.PCD_FechaInicio = (Convert.ToDateTime(dr[7]) <= DateTime.Now && Convert.ToDateTime(dr[8]) >= DateTime.Now) ? Convert.ToDateTime(dr[7]) : c.PCD_FechaInicio;
                             c.PCD_FechaFin = (Convert.ToDateTime(dr[7]) <= DateTime.Now && Convert.ToDateTime(dr[8]) >= DateTime.Now) ? Convert.ToDateTime(dr[8]) : c.PCD_FechaFin;
                             //Futuro: La fecha inicio es mayor a la fecha actual 
                             c.PCD_PrecioAAAEspC = Convert.ToDateTime(dr[7]) > DateTime.Now ? Convert.ToDouble(dr[6]) : c.PCD_PrecioAAAEspC;
                             c.PCD_FechaInicioC = Convert.ToDateTime(dr[7]) > DateTime.Now ? Convert.ToDateTime(dr[7]) : c.PCD_FechaInicioC;
                             c.PCD_FechaFinC = Convert.ToDateTime(dr[7]) > DateTime.Now ? Convert.ToDateTime(dr[8]) : c.PCD_FechaFinC;
                             c.PCD_CatDesp = dr[9].ToString();
                         }
                         else
                         {
                             c = new ConvenioDet();
                             c.Id_Prd = Convert.ToInt32(dr[0]);
                             c.PCD_ClaveProv = dr[1].ToString();
                             c.Prd_Descripcion = DescripcionProducto(Convert.ToInt32(dr[0]));
                             c.PCD_PrecioVtaMin = Convert.ToDouble(dr[2]);
                             c.PCD_PrecioVtaMax = Convert.ToDouble(dr[3]);
                             c.PCD_CantidadMax = Convert.ToInt32(dr[4]);
                             c.Id_Moneda = Convert.ToInt32(dr[5]);
                             c.Id_MonedaStr = Convert.ToInt32(dr[5]) == 1 ? "Dlls" : "Mx";
                             //c.PCD_PrecioAAAEsp = Convert.ToDouble(dr[6]);
                             //c.PCD_FechaInicio = Convert.ToDateTime(dr[7]);
                             //Anterior:la fecha fin es menor a la fecha actual
                             c.PCD_PrecioAAAEspA = Convert.ToDateTime(dr[8]) < DateTime.Now ? Convert.ToDouble(dr[6]) : (Double?)null;
                             c.PCD_FechaInicioA = Convert.ToDateTime(dr[8]) < DateTime.Now ? Convert.ToDateTime(dr[7]) : (DateTime?)null;
                             c.PCD_FechaFinA = Convert.ToDateTime(dr[8]) < DateTime.Now ? Convert.ToDateTime(dr[8]) : (DateTime?)null;
                             //Actual:La fecha inicio es menor a la fecha actual y la fecha fin es mayor a la fecha actual
                             c.PCD_PrecioAAAEsp = (Convert.ToDateTime(dr[7]) <= DateTime.Now && Convert.ToDateTime(dr[8]) >= DateTime.Now) ? Convert.ToDouble(dr[6]) : (Double?)null;
                             c.PCD_FechaInicio = (Convert.ToDateTime(dr[7]) <= DateTime.Now && Convert.ToDateTime(dr[8]) >= DateTime.Now) ? Convert.ToDateTime(dr[7]) : (DateTime?)null;
                             c.PCD_FechaFin = (Convert.ToDateTime(dr[7]) <= DateTime.Now && Convert.ToDateTime(dr[8]) >= DateTime.Now) ? Convert.ToDateTime(dr[8]) : (DateTime?)null;
                             //Futuro: La fecha inicio es mayor a la fecha actual 
                             c.PCD_PrecioAAAEspC = Convert.ToDateTime(dr[7]) > DateTime.Now ? Convert.ToDouble(dr[6]) : (Double?)null;
                             c.PCD_FechaInicioC = Convert.ToDateTime(dr[7]) > DateTime.Now ? Convert.ToDateTime(dr[7]) : (DateTime?)null;
                             c.PCD_FechaFinC = Convert.ToDateTime(dr[7]) > DateTime.Now ? Convert.ToDateTime(dr[8]) : (DateTime?)null; ;
                             c.PCD_CatDesp = dr[9].ToString();
                             List.Add(c);
                         }
                    }

                }


                ListDet = List;

                //lblMensaje.Text = "cerrada la conexion|";
                //BulkCopy(path, hoja);
                ////lblMensaje.Text = "En base de datos|";
                //GuardarDeExcel();
                ////lblMensaje.Text = "Finalizado";

                try
                {
                    File.Delete(path);
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
        private string DescripcionProducto(int Id_Prd)
        {
            try
            {
                string Prd_Descripcion = string.Empty;

                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CN_CatProducto cn_prd = new CN_CatProducto();
                cn_prd.ConsultaProducto_Descripcion(Id_Prd, ref Prd_Descripcion, sesion.Emp_Cnx);

          

                return Prd_Descripcion;

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
                List<ConvenioDet> List = new List<ConvenioDet>();
                List = ListDet;

                if (List == null)
                {
                    Alerta("La lista de productos esta vacía");
                    return;
                }
                if (List.Count == 0)
                {
                    Alerta("La lista de productos esta vacía");
                    return;
 
                }
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                string Conexion = System.Configuration.ConfigurationManager.AppSettings["strConnectionCentral"];
                Convenio conv = new Convenio();
           
                CN_Convenio cn_conv = new CN_Convenio();
                int Verificador = 0;

                LlenarEncabezado(ref conv, sesion.Id_U);
               
                cn_conv.InsertarConvenio(conv, List, ref Verificador, Conexion);

                if (Verificador == -1)
                {

                    if (int.Parse(HFTipoOp.Value) == 0)
                    {
                        AlertaCerrar("Los datos se guardaron correctamente");
                    }
                    else
                    {
                        AlertaCerrar("Se realizó la sustición correctamente");
 
                    }

                }
                else
                {
                    if (int.Parse(HFTipoOp.Value) == 0)
                    {
                        Alerta("Error al tratar de guardar convenio");
                    }
                    else
                    {
                        Alerta("Error al tratar de realizar la sustitución");
                    }
                }

            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
        private void LlenarEncabezado(ref Convenio conv, int Id_U)
        {
            try
            {
                conv.PC_NoConvenio = this.TxtPC_NoConvenio.Text;
                conv.PC_Nombre = this.TxtPC_Nombre.Text.Trim();
                conv.Id_Cat  = int.Parse(this.CmbId_Cat.SelectedValue);
                conv.Id_ULider = int.Parse(this.CmbId_ULider.SelectedValue);
                conv.Id_UEjecutivo = int.Parse(this.CmbId_UEjecutivo.SelectedValue);
                conv.PC_Margen = double.Parse(TxtPC_Margen.Text);
                conv.PC_Notas = this.TxtPC_Notas.Text.Trim();
                conv.Cat_Consecutivo = int.Parse(HFCat_Consecutivo.Value);
                conv.Id_PCAnterior = this.HFTipoOp.Value == "1" ? int.Parse(this.HFId_PC.Value) : 0;
                conv.Id_U = Id_U;
                 


            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
        private void ConsultaConvenio()
        {
            try
            {
                string Conexion = System.Configuration.ConfigurationManager.AppSettings["strConnectionCentral"];
                CN_Convenio cn_conv = new CN_Convenio();
                Convenio conv = new Convenio();
                List<ConvenioDet> List = new List<ConvenioDet>();
                int Id_PC = int.Parse(HFId_PC.Value);

                cn_conv.ConsultaConvenio(Id_PC, ref conv, Conexion);
                VaciaEncabezado(conv);
                cn_conv.ConsultaConvenioDet(Id_PC, ref List, Conexion);
                ListDet = List;
                rgConvenioDet.DataSource = List;
                rgConvenioDet.DataBind();

                if (this.HFTipoOp.Value == "1")
                {
                    this.TxtConvAnt.Visible = true;
                    this.LblConvAnt.Visible = true;
                    CargaConsecutivo();
                }
                else
                {
                    this.CmbId_Cat.Enabled = false;
                    this.TxtPC_NoConvenio.Enabled = false;
                }
                

            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
        private void VaciaEncabezado(Convenio conv)
        {
            try
            {
                this.CmbId_Cat.SelectedValue = conv.Id_Cat.ToString();
                this.CmbId_ULider.SelectedValue = conv.Id_ULider.ToString();
                this.CmbId_UEjecutivo.SelectedValue = conv.Id_UEjecutivo.ToString();
                this.TxtPC_FechaCreo.Text = conv.PC_FechaMod.ToString();
                this.TxtPC_Notas.Text = conv.PC_Notas;

                if (this.HFTipoOp.Value == "1")
                {
                    this.TxtConvAnt.Text = conv.PC_NoConvenio;
                }
                else
                {
                    this.TxtPC_NoConvenio.Text = conv.PC_NoConvenio;
                    this.TxtPC_Nombre.Text = conv.PC_Nombre;
                }

                TxtPC_Margen.Text = conv.PC_Margen.ToString();


            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
        private void Modificar()
        {
            try
            {
                string Conexion = System.Configuration.ConfigurationManager.AppSettings["strConnectionCentral"];
                List<ConvenioDet> List = new List<ConvenioDet>();
                List = ListDet;

                if (List == null)
                {
                    Alerta("La lista de productos esta vacía");
                    return;
                }
                if (List.Count == 0)
                {
                    Alerta("La lista de productos esta vacía");
                    return;

                }

                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                Convenio conv = new Convenio();
                CN_Convenio cn_conv = new CN_Convenio();
                int Verificador = 0;

                LlenarEncabezadoMod(ref conv, sesion.Id_U);
                cn_conv.ModificaConvenio(conv, ref Verificador, Conexion);

                if (Verificador == -1)
                {
                    cn_conv.InsertarConvenioDet(int.Parse(HFId_PC.Value), List, ref Verificador, Conexion);

                    if (Verificador == -1)
                    {
                        AlertaCerrar("Los datos se modificaron correctamente");
                    }
                    else
                    {
                        Alerta("Error al insertar los detalles del convenio");
                    }

                }
                else
                {
                    Alerta("Error al tratar de modificar el convenio");
                }

            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
        private void LlenarEncabezadoMod(ref Convenio conv, int Id_U)
        {
            try
            {
                conv.Id_PC = int.Parse(HFId_PC.Value);
                conv.PC_Nombre = this.TxtPC_Nombre.Text.Trim();
                conv.Id_ULider = int.Parse(this.CmbId_ULider.SelectedValue);
                conv.Id_UEjecutivo = int.Parse(this.CmbId_UEjecutivo.SelectedValue);
                conv.PC_Margen = double.Parse(TxtPC_Margen.Text);
                conv.PC_Notas = this.TxtPC_Notas.Text.Trim();
                conv.Id_U = Id_U;
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
        private void AlertaCerrar(string mensaje)
        {
            try
            {
                mensaje = mensaje.Replace(Convert.ToChar(10).ToString(), string.Empty);
                mensaje = mensaje.Replace(Convert.ToChar(13).ToString(), string.Empty);
                RAM1.ResponseScripts.Add("CloseWindowA('" + mensaje + "');");
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