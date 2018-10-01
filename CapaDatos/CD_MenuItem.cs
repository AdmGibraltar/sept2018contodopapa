using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using CapaEntidad;

namespace CapaDatos
{
    public class CD_MenuItem
    {
        private string _StrCnx;
        public CD_MenuItem()
        {

        }
        public CD_MenuItem(string Conexion)
        {
            _StrCnx = Conexion;
        }
        public void LlenarTreeMenu2(ref DataTable Dt)
        {
            try
            {
                SqlConnection sqlcnx = default(SqlConnection);
                SqlCommand sqlcmd = default(SqlCommand);
                SqlDataAdapter sqlda = default(SqlDataAdapter);
                Funciones DesEncr = new Funciones();

                sqlcnx = new SqlConnection(_StrCnx);
                sqlcmd = new SqlCommand("SpSysTreeMenu2", sqlcnx);
                sqlcmd.CommandTimeout = 0;
                sqlcmd.CommandType = CommandType.StoredProcedure;
                sqlda = new SqlDataAdapter(sqlcmd);
                Dt = new DataTable();
                sqlda.Fill(Dt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void LlenarTreeMenu(ref DataTable Dt)
        {
            try
            {
                SqlConnection sqlcnx = default(SqlConnection);
                SqlCommand sqlcmd = default(SqlCommand);
                SqlDataAdapter sqlda = default(SqlDataAdapter);
                Funciones DesEncr = new Funciones();

                sqlcnx = new SqlConnection(_StrCnx);
                sqlcmd = new SqlCommand("SpSysTreeMenu", sqlcnx);
                sqlcmd.CommandTimeout = 0;
                sqlcmd.CommandType = CommandType.StoredProcedure;
                sqlda = new SqlDataAdapter(sqlcmd);
                Dt = new DataTable();
                sqlda.Fill(Dt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void LimpiarMenu()
        {
            try
            {
                SqlConnection sqlcnx = default(SqlConnection);
                SqlCommand sqlcmd = default(SqlCommand);
                SqlDataAdapter sqlda = default(SqlDataAdapter);
                Funciones DesEncr = new Funciones();
                sqlcnx = new SqlConnection(_StrCnx);
                sqlcmd = new SqlCommand("truncate table SysMenu", sqlcnx);
                sqlcmd.CommandTimeout = 0;
                sqlcmd.CommandType = CommandType.Text;
                sqlda = new SqlDataAdapter(sqlcmd);
                sqlcnx.Open();
                sqlcmd.ExecuteNonQuery();
                sqlcnx.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public object SiguienteItem()
        //{
        //    return Escalar("select isnull(max(sm_cve),0) + 1 from SysMenu");
        //}
        //public object ObtenerDescripcion(string href)
        //{
        //    return Escalar("select Sm_Desc from SysMenu where Sm_Href='" + href + ".aspx'");
        //}



        public void CatModulosInsertar(List<MenuItem> list, string Conexion, ref int verificador)
        {
            if (list.Count == 0) return;

            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {

                CapaDatos.StartTrans();
                string[] Parametros = { 
	                                    "@Sm_Cve", 
	                                    "@Sm_Sm_Cve", 
	                                    "@Sm_Ord", 
                                        "@Sm_Desc",
	                                    "@Sm_Href", 
	                                    "@Sm_Nivel", 
                                        "@Sm_Tipo",
                                        "@Sm_Img",
                                        
                                        "@Accion"
                                      };

                object[] Valores = null;
                SqlCommand sqlcmd = null; ;

                for (int x = 0; x < list.Count; x++)
                {
                    Valores = new object[] { 
                                        list[x].cve,
                                        list[x].cve_p == "NULL" ? (object)null : list[x].cve_p,
                                        list[x].ord,
                                        list[x].desc,
                                        list[x].href == "NULL" ? (object)null : list[x].href,
                                        list[x].nivel,
                                        list[x].Tipo,
                                        list[x].Img == ""? (object)null: list[x].Img,
                                         
                                        x
                                   };
                    sqlcmd = CapaDatos.GenerarSqlCommand("spSysMenu_Insertar", ref verificador, Parametros, Valores);

                }
                CapaDatos.CommitTrans();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                CapaDatos.RollBackTrans();
                throw ex;
            }
        }
        public void CatModulosInsertar(CapaEntidad.MenuItem menuitem, string Conexion, ref int verificador, int accion)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                try
                {
                    string[] Parametros = { 
	                                    "@Sm_Cve", 
	                                    "@Sm_Sm_Cve", 
	                                    "@Sm_Ord", 
                                        "@Sm_Desc",
	                                    "@Sm_Href", 
	                                    "@Sm_Nivel", 
                                        "@Sm_Tipo",
                                        "@Sm_Img",
                                        "@Accion"
                                      };
                    object[] Valores = new object[] { 
                                        menuitem.cve== "NULL" ? (object)null : menuitem.cve,
                                        menuitem.cve_p== "NULL" ? (object)null : menuitem.cve_p,
                                        menuitem.ord,
                                        menuitem.desc,
                                        menuitem.href== "NULL" ? (object)null :menuitem.href,
                                        menuitem.nivel,
                                        menuitem.Tipo,
                                        menuitem.Img== ""? (object)null:menuitem.Img,
                                        accion
                                   };
                    SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spSysMenu_Insertar", ref verificador, Parametros, Valores);
                    CapaDatos.LimpiarSqlcommand(ref sqlcmd);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CatModulosModificar(CapaEntidad.MenuItem menuitem, string Conexion, ref int verificador)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                try
                {
                    string[] Parametros = { 
	                                    "@Sm_Cve", 
                                        "@Sm_Desc",
	                                    "@Sm_Href", 
                                        "@Sm_Tipo",
                                        "@Sm_Img"
                                      
                                      };
                    object[] Valores = new object[] { 
                                        menuitem.cve,
                                        menuitem.desc,
                                        menuitem.href== "NULL" ? (object)null :menuitem.href,
                                        menuitem.Tipo,
                                        menuitem.Img== ""? (object)null:menuitem.Img
                                       
                                   };
                    SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spSysMenu_Modificar", ref verificador, Parametros, Valores);
                    CapaDatos.LimpiarSqlcommand(ref sqlcmd);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
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
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(conexion);
                try
                {
                    string[] Parametros = { "@Sm_Cve" };
                    object[] Valores = new object[] { cve };
                    SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spSysMenu_Eliminar", ref verificador, Parametros, Valores);
                    CapaDatos.LimpiarSqlcommand(ref sqlcmd);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
