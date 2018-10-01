using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using CapaEntidad;
using CapaNegocios;

namespace SIANWEB
{
    public partial class VentanaVersion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                if (!IsPostBack)
                {

                    RadGrid1.Rebind();
                }

            }
            catch (Exception)
            {

                throw;
            }

        }

        protected void RadGrid1_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                    RadGrid1.DataSource = GetList();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "RadGrid1_NeedDataSource");
            }
        }

        private List<VersionDll> GetList()
        {
            try
            {
                List<VersionDll> List = new List<VersionDll>();

                Sesion session2 = new Sesion();
                session2 = (Sesion)Session["Sesion" + Session.SessionID];
                VersionDll version;

                try
                {
                    string[] fibarray = new string[] { "CapaDatos.dll", "CapaEntidad.dll", "CapaModelo.dll", "CapaNegocios.dll", "LibreriaReportes.dll", "SIANWEB.dll" };

                    for (int i = 0; i < fibarray.Length; i++)
                    {
                        version = new VersionDll();

                        System.Reflection.AssemblyName[] names = System.Reflection.Assembly.GetExecutingAssembly().GetReferencedAssemblies();

                        String strAppDir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);

                        strAppDir = strAppDir + "\\" + fibarray[i];
                        //quitarleel file:
                        strAppDir = strAppDir.Replace(@"fil", "");
                        strAppDir = strAppDir.Replace(@"e:\", "");
                        System.Reflection.Assembly assem2 = System.Reflection.Assembly.ReflectionOnlyLoadFrom(strAppDir);
                        System.Reflection.AssemblyName assemName2 = assem2.GetName();
                        Version ver2 = assemName2.Version;

                        version.Num_Version = ver2.ToString();
                        version.Dll_Nombre = fibarray[i];


                        DateTime fileCreatedDate = System.IO.File.GetLastWriteTime(strAppDir);
                        version.Dll_Fecha = fileCreatedDate;

                        List.Add(version);
                    }
                }

                catch (Exception ex)
                {

                    ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);

                }


                return List;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
       
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
                Alerta(Message);
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
                Alerta("Error: [" + NombreFuncion + "] " + eme.Message.ToString());
                //this.lblMensaje.Text = "Error: [" + NombreFuncion + "] " + eme.Message.ToString();

            }
            catch (Exception ex)
            {
                Alerta("Error grave: " + eme.Message.ToString() + " --> " + ex.Message.ToString());
                //this.lblMensaje.Text = "Error grave: " + eme.Message.ToString() + " --> " + ex.Message.ToString();
            }
        }
        #endregion

       
        private void CerrarVentana(string Id_Acs)
        {
            try
            {
                string funcion = "CloseAndRebind('" + Id_Acs + "')";
                string script = "<script>" + funcion + "</script>";
                ScriptManager.RegisterStartupScript(this, this.GetType(), funcion, script, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        

        public class VersionDll
        {
           
            private string _Version;
            private string _Dll_Nombre;
            private DateTime _Dll_Fecha;

            public string Num_Version
            {
                get { return _Version; }
                set { _Version = value; }
            }
            public string Dll_Nombre
            {
                get { return _Dll_Nombre; }
                set { _Dll_Nombre = value; }
            }
            public DateTime Dll_Fecha
            {
                get { return _Dll_Fecha; }
                set { _Dll_Fecha = value; }
            }
          
        }

    }
}
