using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class CD_PermisosTU
    {
        public void ModificarPermisosTipoUsuario(Permiso permiso, string Conexion, ref int Verificador)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = {
			"@Id_TU",
			"@Sm_cve",
			"@Sptu_PAccesar",
			"@Sptu_PGrabar",
			"@Sptu_PModificar",
			"@Sptu_PEliminar",
			"@Sptu_PImprimir",
			"@PAccesar",
			"@PGrabar",
			"@PModificar",
			"@PEliminar",
			"@PImprimir",
		 
		};

                object[] Valores = {
			permiso.Id_TU,
			permiso.Sm_cve,
			permiso.Sp_PAccesar,
			permiso.Sp_PGrabar,
			permiso.Sp_PModificar,
			permiso.Sp_PEliminar,
			permiso.Sp_PImprimir,
			permiso.PAccesar,
			permiso.PGrabar,
			permiso.PModificar,
			permiso.PEliminar,
			permiso.PImprimir,
			 
		};

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("SpSysPermisosTU_Modificar", ref Verificador, Parametros, Valores);

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ConsultaPermisosTipoUsuario(Permiso permiso, string conexion, ref System.Collections.Generic.List<Permiso> list)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(conexion);

                string[] Parametros = { "@Id_TU", "@Id_Emp" };

                object[] Valores = { permiso.Id_TU, permiso.Id_Emp };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("SpSysPermisosTU_Consulta", ref dr, Parametros, Valores);

                Permiso VarPermiso = default(Permiso);
                while (dr.Read())
                {
                    VarPermiso = new Permiso();
                    VarPermiso.Id_TU = (int)dr.GetValue(dr.GetOrdinal("Id_Tu"));
                    VarPermiso.Sm_cve = (int)dr.GetValue(dr.GetOrdinal("Sm_Cve"));

                    VarPermiso.PAccesar = (bool)dr.GetValue(dr.GetOrdinal("acceso"));
                    VarPermiso.PGrabar = (bool)dr.GetValue(dr.GetOrdinal("grabar"));
                    VarPermiso.PModificar = (bool)dr.GetValue(dr.GetOrdinal("modificacion"));
                    VarPermiso.PEliminar = (bool)dr.GetValue(dr.GetOrdinal("eliminar"));
                    VarPermiso.PImprimir = (bool)dr.GetValue(dr.GetOrdinal("imprimir"));

                    VarPermiso.Sp_PAccesar = (bool)dr.GetValue(dr.GetOrdinal("SpTu_PAccesar"));
                    VarPermiso.Sp_PGrabar = (bool)dr.GetValue(dr.GetOrdinal("SpTu_PGrabar"));
                    VarPermiso.Sp_PModificar = (bool)dr.GetValue(dr.GetOrdinal("SpTu_PModificar"));
                    VarPermiso.Sp_PEliminar = (bool)dr.GetValue(dr.GetOrdinal("SpTu_PEliminar"));
                    VarPermiso.Sp_PImprimir = (bool)dr.GetValue(dr.GetOrdinal("SpTu_PImprimir"));

                    VarPermiso.Menu = (string)dr.GetValue(dr.GetOrdinal("Menu"));
                    list.Add(VarPermiso);
                }
                dr.Close();

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void ConsultaPermisosCtrlTU(Permiso permiso, string Conexion, ref List<Permiso> Lista)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_TU", "@Id_Emp", "@Id_Cd", "@sm_cve" };

                object[] Valores = { permiso.Id_TU, permiso.Id_Emp, permiso.Id_Cd, permiso.Sm_cve };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("SpSysPermisosCtrlTU_Consulta", ref dr, Parametros, Valores);

                Permiso VarPermiso = default(Permiso);
                while (dr.Read())
                {
                    VarPermiso = new Permiso();
                    VarPermiso.Id_Ctrl = (string)dr.GetValue(dr.GetOrdinal("Id_Ctrl"));
                    VarPermiso.PDeshabilitar = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Deshabilitar")));
                    VarPermiso.POcultar = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Ocultar")));
                    //VarPermiso.Sp_PAccesar = (bool)dr.GetValue(dr.GetOrdinal("SpTu_PAccesar"));
                    VarPermiso.Menu = (string)dr.GetValue(dr.GetOrdinal("Ctrl_Descripcion"));
                    Lista.Add(VarPermiso);
                }
                dr.Close();

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarPermisosCtrlTU(Permiso permiso, string conexion, ref int Verificador)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(conexion);

                string[] Parametros = {
                                          "@Id_Emp",
                                          "@Id_Cd",
			                              "@Id_TU",
			                              "@Sm_cve",
                                          "@Id_Ctrl",
			                              "@PDeshabilitar",
                                          "@POcultar"
		};

                object[] Valores = {
			                            permiso.Id_Emp,
                                        permiso.Id_Cd,
                                        permiso.Id_TU,
                                        permiso.Sm_cve,
                                        permiso.Id_Ctrl,
                                        permiso.PDeshabilitar,
                                        permiso.POcultar
		};

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("SpSysPermisosCtrlTU_Modificar", ref Verificador, Parametros, Valores);

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
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
                SqlDataReader dr = null;
                CD_Datos CapaDatos = new CD_Datos(Conexion);

                string[] Parametros = { "@Id_TU", "@Id_Emp", "@Id_Cd", "@sm_cve" };

                object[] Valores = { permiso.Id_TU, permiso.Id_Emp, permiso.Id_Cd, permiso.Sm_cve };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("SpSysPermisosCtrlTU_ConsultaP", ref dr, Parametros, Valores);

                PermisoControl VarPermiso = default(PermisoControl);
                while (dr.Read())
                {
                    VarPermiso = new PermisoControl();
                    VarPermiso.Id_Ctrl = (string)dr.GetValue(dr.GetOrdinal("Id_Ctrl"));
                    VarPermiso.Tipo = (string)dr.GetValue(dr.GetOrdinal("Ctrl_Tipo"));
                    VarPermiso.Ctrl_Deshabilitado = (bool)dr.GetValue(dr.GetOrdinal("Deshabilitar"));
                    VarPermiso.Ctrl_Oculto = (bool)dr.GetValue(dr.GetOrdinal("Ocultar"));
                    VarPermiso.Ctrl_Label = (string)dr.GetValue(dr.GetOrdinal("Ctrl_Label"));
                    //VarPermiso
                    list.Add(VarPermiso);
                }
                dr.Close();

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
