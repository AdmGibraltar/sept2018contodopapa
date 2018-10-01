using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class CD_PermisosU
    {
        public void ConsultaPermisosUsuario(Permiso permiso, string Conexion, ref List<Permiso> list)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = {
			                            "@Id_U",
			                            "@Id_Cd",
                                        "@Id_Emp"
		                            };
                object[] Valores = {
			                            permiso.Id_U,
			                            permiso.Id_Cd,
                                        permiso.Id_Emp
		                            };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("SPSysPermisosU_Consulta", ref dr, Parametros, Valores);
                Permiso VarPermiso = default(Permiso);
                while (dr.Read())
                {
                    VarPermiso = new Permiso();
                    VarPermiso.Id_U = dr.GetInt32(dr.GetOrdinal("Id_u"));
                    VarPermiso.Sm_cve = dr.GetInt32(dr.GetOrdinal("Sm_Cve"));
                    VarPermiso.PAccesar = dr.GetBoolean(dr.GetOrdinal("acceso"));
                    VarPermiso.PGrabar = dr.GetBoolean(dr.GetOrdinal("grabar"));
                    VarPermiso.PModificar = dr.GetBoolean(dr.GetOrdinal("modificacion"));
                    VarPermiso.PEliminar = dr.GetBoolean(dr.GetOrdinal("eliminar"));
                    VarPermiso.PImprimir = dr.GetBoolean(dr.GetOrdinal("imprimir"));
                    VarPermiso.Sp_PAccesar = dr.GetBoolean(dr.GetOrdinal("SpTu_PAccesar"));
                    VarPermiso.Sp_PGrabar = dr.GetBoolean(dr.GetOrdinal("SpTu_PGrabar"));
                    VarPermiso.Sp_PModificar = dr.GetBoolean(dr.GetOrdinal("SpTu_PModificar"));
                    VarPermiso.Sp_PEliminar = dr.GetBoolean(dr.GetOrdinal("SpTu_PEliminar"));
                    VarPermiso.Sp_PImprimir = dr.GetBoolean(dr.GetOrdinal("SpTu_PImprimir"));
                    VarPermiso.Menu = Convert.ToString(dr.GetValue(dr.GetOrdinal("Menu")));
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
        public void ModificarPermisosUsuario(Permiso permiso, string Conexion, ref int Verificador)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = {
			                            "@Id_Cd",
			                            "@Id_U",
			                            "@Sm_cve",
			                            "@Spu_PAccesar",
			                            "@Spu_PGrabar",
			                            "@Spu_PModificar",
			                            "@Spu_PEliminar",
			                            "@Spu_PImprimir",
			                            "@Spu_PAccesarS",
			                            "@Spu_PGrabarS",
			                            "@Spu_PModificarS",
			                            "@Spu_PEliminarS",
			                            "@Spu_PImprimirS"
		                            };

                object[] Valores = {
			                            permiso.Id_Cd,
			                            permiso.Id_U,
			                            permiso.Sm_cve,
			                            permiso.Sp_PAccesar,
			                            permiso.Sp_PGrabar,
			                            permiso.Sp_PModificar,
			                            permiso.Sp_PEliminar,
			                            permiso.Sp_PImprimir,
			                            permiso.Sp_PAccesar,
			                            permiso.Sp_PGrabar,
			                            permiso.Sp_PModificar,
			                            permiso.Sp_PEliminar,
			                            permiso.Sp_PImprimir
		                            };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("SpSysPermisosU_Modificar", ref Verificador, Parametros, Valores);
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ValidaPermisosUsuario(ref Permiso Permiso, string Conexion)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                        string[] Parametros = {
			                                    "@Id_U",
			                                    "@Id_Cd",
			                                    "@Sm_Cve"
		                                    };

                        object[] Valores = {
			                                    Permiso.Id_U,
			                                    Permiso.Id_Cd,
			                                    Permiso.Sm_cve
		                                    };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spSysPermisosU_Verificar", ref dr, Parametros, Valores);
                if (dr.HasRows == true)
                {
                    dr.Read();
                    Permiso.PAccesar = dr.GetBoolean(dr.GetOrdinal("Spu_PAccesar"));
                    Permiso.PGrabar = dr.GetBoolean(dr.GetOrdinal("Spu_PGrabar"));
                    Permiso.PModificar = dr.GetBoolean(dr.GetOrdinal("Spu_PModificar"));
                    Permiso.PEliminar = dr.GetBoolean(dr.GetOrdinal("Spu_PEliminar"));
                    Permiso.PImprimir = dr.GetBoolean(dr.GetOrdinal("Spu_PImprimir"));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
