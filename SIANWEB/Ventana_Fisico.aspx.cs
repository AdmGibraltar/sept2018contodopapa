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
using System.Collections;
using System.Data.SqlClient;

namespace SIANWEB
{
    public partial class Ventana_Fisico : System.Web.UI.Page
    {
        #region variables de excel
        public string NombreArchivo;
        public string NombreHojaExcel;
        public int Id_Pvd;
        public int Prd_Inicial;
        public int Prd_Final;
        public bool chkTransito;
        #endregion
        #region Eventos
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

            }
            catch (Exception ex)
            {
                Alerta(ex.Message);
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
                //NombreArchivo = fileName.Text;
                NombreArchivo = e.File.GetNameWithoutExtension().ToString() + DateTime.Now.ToString("ddMMyyyyHHmmss") + e.File.GetExtension();
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
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];

                string path = Server.MapPath("~/App_Data/RadUploadTemp") + "\\" + NombreArchivo;
                foreach (UploadedFile f in RadUpload1.UploadedFiles)
                {
                    if (File.Exists(path))
                    {
                        File.Delete(path);
                    }
                    f.SaveAs(path, true);
                }

                //CN_CapFisico cn_fisico = new CN_CapFisico();
                //Fisico fisico = new Fisico();
                //fisico.Id_Emp = Sesion.Id_Emp;
                //fisico.Id_Cd = Sesion.Id_Cd_Ver;
                //fisico.Auto = 1;
                //int verificador = 0;
                //cn_fisico.EliminarFisico(fisico, Sesion.Emp_Cnx, ref verificador);

                string strConn = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source='" + path + "';Extended Properties=\"Excel 12.0 xml;HDR=YES;IMEX=1;\"";
                con = new OleDbConnection(strConn);
                //lblMensaje.Text = "abriendo conexion";
                con.Close();
                con.Open();
                //lblMensaje.Text = "conexion abierta";
                DataTable dt = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                string hoja = dt.Rows[0].ItemArray[2].ToString().Replace("'", "");
                //lblMensaje.Text = "hoja" + hoja;
                OleDbCommand cmd = new OleDbCommand("select * from [" + hoja + "]", con);
                OleDbDataAdapter dad = new OleDbDataAdapter();
                dad.SelectCommand = cmd;
                DataSet ds = new DataSet();
                dad.Fill(ds);
                //lblMensaje.Text = "lleno dataset";
                con.Close();
                //lblMensaje.Text = "cerrada la conexion|";
                BulkCopy(path, hoja);
                //lblMensaje.Text = "En base de datos|";
                GuardarDeExcel();
                //lblMensaje.Text = "Finalizado";

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

                con.Close();
                Alerta(ex.Message.Replace("'", ""));
                //this.DisplayMensajeAlerta(ex.Message);
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
        #endregion
        #region Funciones
        private void GuardarDeExcel()
        {
            try
            {
                Sesion session = (Sesion)Session["Sesion" + Session.SessionID];
                CN_CapFisico clsFisico = new CN_CapFisico();
                clsFisico.Automatico(session.Id_Emp, session.Id_Cd_Ver, session.Emp_Cnx);
                //int verificador;
                //Fisico fisico;
                //List<FisicoConsignado> list = new List<FisicoConsignado>();
                //int a;
                //int b;
                //ArrayList Ceros = new ArrayList();

                //Fisico fisico1 = new Fisico();
                //fisico1.Id_Emp = session.Id_Emp;
                //fisico1.Id_Cd = session.Id_Cd_Ver;
                //fisico1.Auto = 1;
                //verificador = 0;
                //clsFisico.EliminarFisico(fisico1, session.Emp_Cnx, ref verificador);

                //foreach (DataRow dr in ds.Tables[0].Rows)
                //{
                //    if (int.TryParse(dr[0] == DBNull.Value ? "0" : dr[0].ToString(), out b) && int.TryParse(dr[5] == DBNull.Value ? "0" : dr[5].ToString(), out a))
                //    {
                //        if (b > 0 && a != 0)
                //        {
                //            fisico = new Fisico();
                //            fisico.Id_Emp = session.Id_Emp;
                //            fisico.Id_Cd = session.Id_Cd_Ver;
                //            fisico.Id_Prd = Convert.ToInt32(dr[0]);
                //            fisico.Fis_Fecha = DateTime.Now;
                //            fisico.Fis_Fisico = a;
                //            fisico.ListFisicoConsignado = list;
                //            verificador = 0;
                //            clsFisico.InsertarFisico(fisico, session.Emp_Cnx, ref verificador);
                //        }
                //    }
                //}

                RAM1.ResponseScripts.Add("CloseAndRebind('fisico');");
            }
            catch (Exception ex)
            {
                Alerta(ex.Message.Replace("'", ""));
            }
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