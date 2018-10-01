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
using System.Data.OleDb;
using Telerik.Web.UI.GridExcelBuilder;
using System.IO;

namespace SIANWEB
{
    public partial class ProPrecioEspecial_Autorizacion_GenExcel : System.Web.UI.Page
    {
        #region variables de excel
        public string NombreArchivo;
        public string NombreHojaExcel;
       
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                if (Sesion == null)
                {
                    string mensaje = "";
                    RAM1.ResponseScripts.Add(string.Concat(@"CloseWindow_Excel('", mensaje, "')"));
                }
                else
                {
                    Inicializar();
                }
            }
            catch (Exception ex)
            {
                this.DisplayMensajeAlerta(string.Concat(ex.Message, "Page_Load_error"));
            }
        }

        #region Eventos
        private void Inicializar()
        {
            try
            { //Id_Emp, Id_Cd
                /*
                Id_Pvd = Convert.ToInt32(Page.Request.QueryString["proveedor"]);
                chkTransito = Convert.ToBoolean(Page.Request.QueryString["aplicaTransito"]);
                Prd_Inicial = Convert.ToInt32(Page.Request.QueryString["productoInicial"]);
                Prd_Final = Convert.ToInt32(Page.Request.QueryString["productoFinal"]);*/
            }
            catch (Exception ex)
            {
                this.DisplayMensajeAlerta(string.Concat(ex.Message, "Page_Load_error"));
            }
        }
        protected void Customvalidator1_ServerValidate(object source, ServerValidateEventArgs args)
        {
            try
            {
                args.IsValid = (RadUpload1.InvalidFiles.Count == 0);
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
                NombreArchivo = fileName.Text;
                NombreHojaExcel = e.File.GetNameWithoutExtension().ToString();
                if (e.IsValid)
                {
                    ValidFiles.Visible = true;
                    ValidFiles.Controls.Add(fileName);
                }
            }
            catch (Exception ex)
            {
                Alerta(ex.Message);
            }
        }
        protected void btnImportar_Click(object sender, EventArgs e)
        {
            OleDbConnection con = default(OleDbConnection);
            try
            {
                string path = Server.MapPath("~/App_Data/RadUploadTemp") + "\\" + NombreArchivo;
                //if (File.Exists(path))
                //{
                //    File.Delete(path);
                //}

                foreach (UploadedFile f in RadUpload1.UploadedFiles)
                {
                    f.SaveAs(path, true);
                }

                string strConn = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties=\"Excel 12.0 xml;HDR=YES;IMEX=1;\"";
                con = new OleDbConnection(strConn);
                con.Open();
                DataTable dt = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                string hoja = dt.Rows[0].ItemArray[2].ToString().Replace("'", "");
                OleDbCommand cmd = new OleDbCommand("select * from [" + hoja + "]", con);
                OleDbDataAdapter dad = new OleDbDataAdapter();
                dad.SelectCommand = cmd;
                DataSet ds = new DataSet();
                try
                {
                    dad.Fill(ds);
                }
                catch (Exception)
                {
                    cmd = new OleDbCommand("select * from [OrdenesDeCompra 1 $]", con);
                    dad = new OleDbDataAdapter();
                    dad.SelectCommand = cmd;
                    ds = new DataSet();
                    dad.Fill(ds);
                }
                con.Close();

            //    GuardarDeExcel(ds);
            }
            catch (Exception ex)
            {
                con.Close();
                Alerta(ex.Message.Replace("'", ""));
                //this.DisplayMensajeAlerta(ex.Message);
            }
        }

        /*
        private void GuardarDeExcel(DataSet ds)
        {
            try
            {
                if (ds.Tables != null)
                {
                    Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                    OrdenCompra ordCompra = new OrdenCompra();
                    ordCompra.Id_Emp = sesion.Id_Emp;
                    ordCompra.Id_Cd = sesion.Id_Cd_Ver;
                    ordCompra.Id_Ord = 0;
                    ordCompra.Id_Pvd = Id_Pvd;
                    ordCompra.Id_U = sesion.Id_U;
                    //cuando se da de alta por el usuario el estatus siempre es M = Manual
                    ordCompra.Ord_Estatus = "C";

                    CapaDatos.Funciones funciones = new CapaDatos.Funciones();
                    DateTime fechaServidor = funciones.GetLocalDateTime(sesion.Minutos);
                    ordCompra.Ord_Fecha = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, fechaServidor.Hour, fechaServidor.Minute, fechaServidor.Second);

                    ordCompra.Ord_Tipo = 2; //1 = Manual, 2 = Automática
                    ordCompra.Ord_Notas = "Orden generada automáticamente";

                    List<OrdenCompraDet> listaPartidaTemp = new List<OrdenCompraDet>();
                    OrdenCompraDet ordenCompraDet;

                    int productoInicial = Prd_Inicial;
                    int productoFinal = Prd_Final;
                    if (!Page.IsPostBack)
                    {
                        productoInicial = -1;
                        productoFinal = -1;
                    }
                    bool aplicaTransito = chkTransito;
                    DataTable dtPartidas = null;
                    new CN_CapOrdenCompra().GeneraOrdenCompraAutomatica(sesion.Emp_Cnx, ref dtPartidas, "tabla", sesion.Id_Emp, sesion.Id_Cd_Ver, Id_Pvd, productoInicial, productoFinal, aplicaTransito, 1);

                    //checar    
                    int ordenado = 0;

                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        if (row[10] != DBNull.Value && int.TryParse(string.IsNullOrEmpty(row[10].ToString()) ? "0" : row[10].ToString(), out ordenado))
                        {
                            int prd = 0;//!string.IsNullOrEmpty(row[0].ToString()) ? int.TryParse(row[0].ToString(), out prd) : 0;
                            int.TryParse(row[0].ToString(), out prd);
                            if (prd > 0)
                            {
                                if (ordenado > 0)
                                {
                                    DataRow[] drArray;
                                    ordenCompraDet = new OrdenCompraDet();
                                    drArray = dtPartidas.Select("Id_Prd='" + prd + "'");
                                    if (drArray.Length > 0)
                                    {
                                        double Prd_UniEmp = !string.IsNullOrEmpty(drArray[0].ItemArray[1].ToString()) ? Convert.ToDouble(drArray[0].ItemArray[1].ToString()) : 1;
                                        if (ordenado % Prd_UniEmp != 0)
                                        {
                                            while (ordenado % Prd_UniEmp != 0)
                                            {
                                                ordenado += 1;
                                            }
                                        }
                                        ordenCompraDet.Id_Emp = sesion.Id_Emp;
                                        ordenCompraDet.Id_Cd = sesion.Id_Cd_Ver;
                                        ordenCompraDet.Id_Ord = 0; //se debe volver asignar cuando se guarda la orden de compra, cuando actualiza queda igual
                                        ordenCompraDet.Id_OrdDet = 0; //identity
                                        ordenCompraDet.Id_Prd = Convert.ToInt32(row[0]);
                                        ordenCompraDet.Ord_Cantidad = ordenado;// Convert.ToInt32(row[10]);
                                        ordenCompraDet.Ord_CantidadGen = 0;
                                        listaPartidaTemp.Add(ordenCompraDet);
                                    }
                                }
                            }
                        }
                    }
                    // Create the query.
                    IEnumerable<OrdenCompraDet> sortedStudents =
                        from Partida in listaPartidaTemp
                        orderby Partida.Id_Prd ascending
                        select Partida;

                    List<OrdenCompraDet> listaPartida = new List<OrdenCompraDet>();
                    foreach (OrdenCompraDet Partida2 in sortedStudents)
                    {
                        listaPartida.Add(Partida2);
                    }
                    //Genera la orden de compra solo si la lista trae 1 o mas partidas
                    if (listaPartida.Count > 0)
                    {
                        ordCompra.ListOrdenCompra = listaPartida;
                        int verificador = 0;
                        new CN_CapOrdenCompra().InsertarOrdenCompra(ref ordCompra, sesion.Emp_Cnx, ref verificador);
                        string mensaje = "Se genero la orden de compra # <b>" + verificador.ToString() + "</b>";
                        RAM1.ResponseScripts.Add(string.Concat(@"CloseWindow_Excel('", mensaje, "')"));
                    }
                    else
                        this.DisplayMensajeAlerta("ProOrdenCompraAuto_insert_NoPartidas");
                }
                else
                    this.DisplayMensajeAlerta("ErrorPartidas");

            }
            catch (Exception ex)
            {
                Alerta(ex.Message.Replace("'", ""));
            }
        }*/
        #endregion

        #region Funciones

        private void DisplayMensajeAlerta(string mensaje)
        {
            if (mensaje.Contains("Page_Load_error"))
                Alerta("Error al cargar la página");
            else
                if (mensaje.Contains("ProOrdenCompraAuto_insert_NoPartidas"))
                    Alerta("Los datos no se guardaron, no hay partidas que guardar o la cantidad de cada una de ellas es 0");
                else
                    if (mensaje.Contains("ErrorPartidas"))
                        Alerta("No se pudo subir el archivo, favor de revisarlo");
                    else
                        if (mensaje.Contains("Page_guardar_error"))
                            Alerta("Error al guardar el archivo en la base de datos");
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
    }
}