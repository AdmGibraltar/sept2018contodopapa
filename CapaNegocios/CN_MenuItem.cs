using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using CapaEntidad;
using CapaDatos;
using Telerik.Web.UI;

namespace CapaNegocios
{
    public class CN_MenuItem
    {
        public void LlenarTreeMenu2(string conexion, ref DataTable dt)
        {
            try
            {
                CD_MenuItem claseCapaDatos = new CD_MenuItem(conexion);
                claseCapaDatos.LlenarTreeMenu2(ref dt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void LlenarTreeMenu(string conexion, ref DataTable dt)
        {
            try
            {
                CD_MenuItem claseCapaDatos = new CD_MenuItem(conexion);
                claseCapaDatos.LlenarTreeMenu(ref dt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void LimpiarMenu(string conexion)
        {
            try
            {
                CapaDatos.CD_MenuItem claseCapaDatos = new CapaDatos.CD_MenuItem(conexion);
                claseCapaDatos.LimpiarMenu();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CatModulosInsertar(List<MenuItem> list, string Conexion, ref int verificador)
        {
            try
            {
                CD_MenuItem claseCapaDatos = new CD_MenuItem();
                claseCapaDatos.CatModulosInsertar(list, Conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void CatModulosInsertar(CapaEntidad.MenuItem menuitem, string conexion, ref int verficador)
        {
            try
            {
                CapaDatos.CD_MenuItem claseCapaDatos = new CapaDatos.CD_MenuItem();
                claseCapaDatos.CatModulosInsertar(menuitem, conexion, ref verficador, 1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CatModulosModificar(CapaEntidad.MenuItem menuitem, string conexion, ref int verficador)
        {
            try
            {
                CapaDatos.CD_MenuItem claseCapaDatos = new CapaDatos.CD_MenuItem();
                claseCapaDatos.CatModulosModificar(menuitem, conexion, ref verficador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CatModulosEliminar(string cve, string conexion, ref int verificador)
        {
            try
            {
                CapaDatos.CD_MenuItem claseCapaDatos = new CapaDatos.CD_MenuItem();
                claseCapaDatos.CatModulosEliminar(cve, conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
