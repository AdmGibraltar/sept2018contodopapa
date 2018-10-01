using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class CD_CatTiposUsuario
    {
        public void ConsultaTiposDeUsuario(TipoUsuario TipoUsuario, string Conexion, ref System.Collections.Generic.List<TipoUsuario> list)
        {

            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = new string[] { "@Id_Emp" };

                object[] Valores = new object[] { TipoUsuario.Id_Emp };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("SpSysTipoUsuario_Consulta", ref dr, Parametros, Valores);

                TipoUsuario VarTipoUsuario = default(TipoUsuario);
                while (dr.Read())
                {
                    VarTipoUsuario = new TipoUsuario();
                    VarTipoUsuario.Id_TU = dr.GetInt32(dr.GetOrdinal("PerfilID"));
                    VarTipoUsuario.TU_Descripcion = dr.GetString(dr.GetOrdinal("Perfil"));
                    VarTipoUsuario.TU_Activo = dr.GetBoolean(dr.GetOrdinal("TU_Activo"));
                    VarTipoUsuario.TU_ActivoStr = dr.GetString(dr.GetOrdinal("TU_ActivoStr"));
                    VarTipoUsuario.Tu_Propia = dr.GetBoolean(dr.GetOrdinal("TU_Propia"));
                    list.Add(VarTipoUsuario);
                }
                dr.Close();

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaTiposDeUsuarioPorNombre(TipoUsuario TipoUsuario, string Conexion, ref System.Collections.Generic.List<TipoUsuario> list) {
            try {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = new string[] { "@Id_Emp", "@Tu_Descripcion" };

                object[] Valores = new object[] { TipoUsuario.Id_Emp, TipoUsuario.TU_Descripcion };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("SpSysTipoUsuario_ConsultaPorNombre", ref dr, Parametros, Valores);

                list = new List<CapaEntidad.TipoUsuario>();
                TipoUsuario VarTipoUsuario = default(TipoUsuario);
                while (dr.Read()) {
                    VarTipoUsuario = new TipoUsuario();
                    VarTipoUsuario.Id_TU = dr.GetInt32(dr.GetOrdinal("PerfilID"));
                    VarTipoUsuario.TU_Descripcion = dr.GetString(dr.GetOrdinal("Perfil"));
                    VarTipoUsuario.TU_Activo = dr.GetBoolean(dr.GetOrdinal("TU_Activo"));
                    VarTipoUsuario.TU_ActivoStr = dr.GetString(dr.GetOrdinal("TU_ActivoStr"));
                    VarTipoUsuario.Tu_Propia = dr.GetBoolean(dr.GetOrdinal("TU_Propia"));
                    list.Add(VarTipoUsuario);
                }
                dr.Close();

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public void InsertarTipoUsuario(ref TipoUsuario TipoUsuario, string conexion, ref List<Permiso> list)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(conexion);

                string[] Parametros = {
			"@TU_Descripcion",
			"@TU_Id_TU",
			"@TU_Activo",
			"@Id_Cd",
            "@Id_Emp",
            "@TU_Propia"
            
		};

                object[] Valores = {
			TipoUsuario.TU_Descripcion,
			TipoUsuario.TU_Id_TU,
			TipoUsuario.TU_Activo,
			TipoUsuario.Id_Cd,
            TipoUsuario.Id_Emp,
            TipoUsuario.Tu_Propia
             
		};

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("SpSysTipoUsuario_Insertar", ref dr, Parametros, Valores);
                Permiso VarPermiso = default(Permiso);
                while (dr.Read())
                {
                    if (dr.FieldCount > 1)
                    {
                        VarPermiso = new Permiso();
                        VarPermiso.Id_TU = dr.GetInt32(dr.GetOrdinal("Id_Tu"));
                        VarPermiso.Sm_cve = dr.GetInt32(dr.GetOrdinal("Sm_Cve"));
                        VarPermiso.Sp_PAccesar = dr.GetBoolean(dr.GetOrdinal("SpTu_PAccesar"));
                        VarPermiso.Sp_PGrabar = dr.GetBoolean(dr.GetOrdinal("SpTu_PGrabar"));
                        VarPermiso.Sp_PModificar = dr.GetBoolean(dr.GetOrdinal("SpTu_PModificar"));
                        VarPermiso.Sp_PEliminar = dr.GetBoolean(dr.GetOrdinal("SpTu_PEliminar"));
                        VarPermiso.Sp_PImprimir = dr.GetBoolean(dr.GetOrdinal("SpTu_PImprimir"));
                        VarPermiso.Menu = dr.GetString(dr.GetOrdinal("Menu"));
                        list.Add(VarPermiso);
                    }
                    else
                    {
                        VarPermiso = new Permiso();
                        VarPermiso.Id_U = dr.GetInt32(0);
                    }
                }
                dr.Close();

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ModificarTipoUsuario(TipoUsuario TipoUsuario, string Conexion, ref Int32 Verificador)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = {
			"@Id_TU",
			"@TU_Descripcion",
			"@TU_Activo",
            "@Id_Emp",
            "@TU_Propia"
		};

                object[] Valores = {
			TipoUsuario.Id_TU,
			TipoUsuario.TU_Descripcion,
			TipoUsuario.TU_Activo,
            TipoUsuario.Id_Emp,
            TipoUsuario.Tu_Propia
		};

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("SPSysTipoUsuario_Modificar", ref Verificador, Parametros, Valores);

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
