using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using CapaEntidad;
using CapaDatos;

namespace CapaNegocios
{
    public class CN_PermisosU
    {
        public void ConsultaPermisosUsuario(Permiso permiso, string conexion, ref Telerik.Web.UI.RadGrid RadGrid)
        {
            try
            {
                System.Collections.Generic.List<Permiso> Lista = new System.Collections.Generic.List<Permiso>();
                CapaDatos.CD_PermisosU claseCapaDatos = new CapaDatos.CD_PermisosU();
                claseCapaDatos.ConsultaPermisosUsuario(permiso, conexion, ref Lista);
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
        public void ModificarPermisosUsuario(Permiso permiso, string conexion, ref int Verificador)
        {
            try
            {
                CapaDatos.CD_PermisosU claseCapaDatos = new CapaDatos.CD_PermisosU();
                claseCapaDatos.ModificarPermisosUsuario(permiso, conexion, ref Verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ValidaPermisosUsuario(ref Permiso Permiso, string conexion)
        {
            try
            {
                CapaDatos.CD_PermisosU claseCapaDatos = new CapaDatos.CD_PermisosU();
                claseCapaDatos.ValidaPermisosUsuario(ref Permiso, conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }







        //public void ConsultaPermisosCtrlU(Permiso permiso, string Conexion, ref Telerik.Web.UI.RadGrid RadGrid)
        //{
        //    try
        //    {
        //        List<Permiso> Lista = new List<Permiso>();
        //        CD_PermisosU claseCapaDatos = new CD_PermisosU();
        //        claseCapaDatos.ConsultaPermisosCtrlU(permiso, Conexion, ref Lista);

        //        if (Lista.Count > 0)
        //        {
        //            RadGrid.DataSource = Lista;
        //            RadGrid.DataBind();
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public void ModificarPermisosU(Permiso permiso, string conexion, ref int Verificador)
        //{
        //    try
        //    {
        //        CD_PermisosU claseCapaDatos = new CD_PermisosU();
        //        claseCapaDatos.ModificarPermisosU(permiso, conexion, ref Verificador);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public void ConsultaPermisosCtrlU_Pagina(Permiso permiso, string Conexion, ref List<PermisoControl> list)
        {
        //    try
        //    {
        //        CD_PermisosU claseCapaDatos = new CD_PermisosU();
        //        claseCapaDatos.ConsultaPermisosCtrlU_Pagina(permiso, Conexion, ref list);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        }
    }
}
