using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using CapaDatos;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Telerik.Web.UI;
using System.Web.UI;

namespace CapaNegocios
{
    public class CN_Ctrl
    {
        public CN_Ctrl()
        { }

        public void InsertarCtrl(PermisoControl ctrl, string conexion, ref int verificador)
        {
            try
            {
                CD_Ctrl claseCapaDatos = new CD_Ctrl();
                claseCapaDatos.InsertarCtrl(ctrl, conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ListaCtrls(string Conexion, int sm_cve, System.Web.UI.ControlCollection controles_contenidos)
        {
            try
            {
                PermisoControl pc = new PermisoControl();
                int verificador = 0;

                pc.Sm_Cve = sm_cve;
                for (int x = 0; x < controles_contenidos.Count; x++)
                {
                    string Type = controles_contenidos[x].GetType().FullName;
                    if (Type.Contains("RadMultiPage") || Type.Contains("RadPageView") || Type.Contains("Panel"))
                    {
                        ListaCtrls(Conexion, sm_cve, controles_contenidos[x].Controls);
                    }
                    if (Type.Contains("RadNumericTextBox") || Type.Contains("RadTextBox") || Type.Contains("RadComboBox") || Type.Contains("RadDatePicker") || (Type.Contains("CheckBox") && ((CheckBox)controles_contenidos[x]).Text == "") )
                    {
                        pc.Id_Ctrl = controles_contenidos[x].ID;
                        pc.Tipo = Type;
                        if (x - 2 >= 0)
                        {
                            if (controles_contenidos[x - 2].GetType().FullName.Contains("Label"))
                            {
                                pc.Ctrl_Label = controles_contenidos[x - 2].ID;
                                pc.Descripcion = ((Label)controles_contenidos[x - 2]).Text;
                            }
                        }
                        else
                        {
                            pc.Descripcion = controles_contenidos[x].ID;
                        }
                        InsertarCtrl(pc, Conexion, ref verificador);
                    }
                    else if (Type.Contains("CheckBox"))
                    {
                        pc.Id_Ctrl = controles_contenidos[x].ID;
                        pc.Tipo = Type;
                        pc.Descripcion = ((CheckBox)controles_contenidos[x]).Text;
                        pc.Ctrl_Label = "";
                        InsertarCtrl(pc, Conexion, ref verificador);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ValidarCtrl(Sesion Sesion, int sm_cve, Control divPrincipal)
        {
            List<PermisoControl> list = new List<PermisoControl>();
            Permiso permiso = new Permiso();
            permiso.Id_Emp = Sesion.Id_Emp;
            permiso.Id_Cd = Sesion.Id_Cd_Ver;
            permiso.Id_TU = Sesion.Id_TU;
            permiso.Sm_cve = sm_cve;
            CN_PermisosTU clsPermisosTU = new CN_PermisosTU();
            clsPermisosTU.ConsultaPermisosCtrlTU_Pagina(permiso, Sesion.Emp_Cnx, ref list);
            
            foreach (PermisoControl p in list)
            {
                switch (p.Tipo)
                {
                    case "System.Web.UI.WebControls.CheckBox":
                        CheckBox ch = (CheckBox)FindControlRecursive(divPrincipal, p.Id_Ctrl);
                        if (p.Ctrl_Deshabilitado) { ch.Enabled = false; }
                        if (p.Ctrl_Oculto) { ch.Visible = false; }
                        break;
                    case "Telerik.Web.UI.RadTextBox":
                        RadTextBox rtb = (RadTextBox)FindControlRecursive(divPrincipal, p.Id_Ctrl);
                        if (p.Ctrl_Deshabilitado) { rtb.Enabled = false; }
                        if (p.Ctrl_Oculto) { rtb.Visible = false; }
                        break;
                    case "Telerik.Web.UI.RadNumericTextBox":
                        RadNumericTextBox rntb = (RadNumericTextBox)FindControlRecursive(divPrincipal, p.Id_Ctrl);
                        if (p.Ctrl_Deshabilitado) { rntb.Enabled = false; }
                        if (p.Ctrl_Oculto) { rntb.Visible = false; }
                        break;
                    case "Telerik.Web.UI.RadComboBox":
                        RadComboBox rcb = (RadComboBox)FindControlRecursive(divPrincipal, p.Id_Ctrl);
                        if (p.Ctrl_Deshabilitado) { rcb.Enabled = false; }
                        if (p.Ctrl_Oculto) { rcb.Visible = false; }
                        break;
                    case "Telerik.Web.UI.RadDatePicker":
                        RadDatePicker rdp = (RadDatePicker)FindControlRecursive(divPrincipal, p.Id_Ctrl);
                        if (p.Ctrl_Deshabilitado) { rdp.Enabled = false; }
                        if (p.Ctrl_Oculto) { rdp.Visible = false; }
                        break;
                }
                if (p.Ctrl_Oculto && p.Ctrl_Label != "")
                {
                    Label lb = (Label)FindControlRecursive(divPrincipal, p.Ctrl_Label);
                    lb.Visible = false;
                }
            }
        }

        private object FindControlRecursive(Control control, string id)
        {
            Control returnControl = control.FindControl(id);
            if (returnControl == null)
            {
                foreach (Control child in control.Controls)
                {
                    returnControl = child.FindControl(id);
                    if (returnControl != null && returnControl.ID == id)
                    {
                        return returnControl;
                    }
                }
            }
            return returnControl;
        }
    }
}
