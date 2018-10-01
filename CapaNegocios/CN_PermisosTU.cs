using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using CapaDatos;

namespace CapaNegocios
{
    public class CN_PermisosTU
    {
        public void ModificarPermisosTipoUsuario(Permiso permiso, string conexion, ref int Verificador)
        {
            try
            {
                CD_PermisosTU claseCapaDatos = new CD_PermisosTU();
                claseCapaDatos.ModificarPermisosTipoUsuario(permiso, conexion,ref Verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ConsultaPermisosTipoUsuario(Permiso permiso, string conexion, ref Telerik.Web.UI.RadGrid RadGrid)
        {
            try
            {
                System.Collections.Generic.List<Permiso> Lista = new System.Collections.Generic.List<Permiso>();
                CapaDatos.CD_PermisosTU claseCapaDatos = new CapaDatos.CD_PermisosTU();
                claseCapaDatos.ConsultaPermisosTipoUsuario(permiso, conexion,ref Lista);

                if (Lista.Count > 0)
                {
                    RadGrid.DataSource = Lista;
                    RadGrid.DataBind();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaPermisosCtrlTU(Permiso permiso, string Conexion, ref Telerik.Web.UI.RadGrid RadGrid)
        {
            try
            {
                List<Permiso> Lista = new List<Permiso>();
                CapaDatos.CD_PermisosTU claseCapaDatos = new CapaDatos.CD_PermisosTU();
                claseCapaDatos.ConsultaPermisosCtrlTU(permiso, Conexion, ref Lista);

                if (Lista.Count > 0)
                {
                    RadGrid.DataSource = Lista;
                    RadGrid.DataBind();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarPermisosTU(Permiso permiso, string conexion, ref int Verificador)
        {
            try
            {
                CD_PermisosTU claseCapaDatos = new CD_PermisosTU();
                claseCapaDatos.ModificarPermisosCtrlTU(permiso, conexion, ref Verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaPermisosCtrlTU_Pagina(Permiso permiso, string Conexion, ref List<PermisoControl> list)
        {
            try
            {
                
                CD_PermisosTU claseCapaDatos = new CD_PermisosTU();
                claseCapaDatos.ConsultaPermisosCtrlTU_Pagina(permiso, Conexion, ref list);

                

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
