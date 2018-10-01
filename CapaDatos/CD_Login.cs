using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using CapaEntidad;

namespace CapaDatos
{
    public class CD_Login
    {
        public CD_Login()
        { }
        private SqlCommand GenerarSqlCommand(string SP, string Conexion)
        {
            SqlCommand functionReturnValue = default(SqlCommand);
            SqlConnection sqlcnx = default(SqlConnection);
            sqlcnx = new SqlConnection(Conexion);
            functionReturnValue = new SqlCommand(SP, sqlcnx);
            functionReturnValue.CommandType = CommandType.StoredProcedure;
            functionReturnValue.CommandTimeout = 0;
            return functionReturnValue;
        }
        private void LimpiarSqlCommand(ref SqlCommand SqlCmd)
        {
            try
            {
                SqlCmd.Connection.Close();
                SqlCmd.Connection.Dispose();
                SqlCmd.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void RecuperarContraseña(ref Usuario Usuario, ref CentroDistribucion Cdis, ref ConfiguracionGlobal Configuarcion, out Int32 Id, string Conexion)
        {
            try
            {
                SqlDataReader SqlDr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                Id = 0;
                string[] Parametros = { "@Cu_User" };
                string[] Valores = { Usuario.Cu_User };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spLoginRecuperarContraseña", ref SqlDr, Parametros, Valores);

                if (SqlDr.HasRows == true)
                {
                    SqlDr.Read();
                    Id = SqlDr.GetInt32(0);
                    if (Id == 1)
                    {
                        Usuario.U_Correo = SqlDr.GetString(SqlDr.GetOrdinal("U_Correo"));
                        Usuario.Cu_pass = SqlDr.GetString(SqlDr.GetOrdinal("Cu_Pass"));
                        Cdis.Cd_Descripcion = SqlDr.GetString(SqlDr.GetOrdinal("Ofi_Descripcion"));
                        Cdis.Cd_Tel = SqlDr.GetString(SqlDr.GetOrdinal("Ofi_Tel"));
                        Cdis.Cd_CalleNo = SqlDr.GetString(SqlDr.GetOrdinal("Ofi_DireccionUbicacion"));
                        Configuarcion.Mail_Servidor = SqlDr.GetString(SqlDr.GetOrdinal("Mail_Servidor"));
                        Configuarcion.Mail_Usuario = SqlDr.GetString(SqlDr.GetOrdinal("Mail_Usuario"));
                        Configuarcion.Mail_Contraseña = SqlDr.GetString(SqlDr.GetOrdinal("Mail_Contraseña"));
                        Configuarcion.Mail_Puerto = SqlDr.GetString(SqlDr.GetOrdinal("Mail_Puerto"));
                        Configuarcion.Mail_Remitente = SqlDr.GetString(SqlDr.GetOrdinal("Mail_Remitente"));
                    }
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void Login(ref Usuario Usuario, out int Id, out int Minutos, out bool Dependientes, string conexion)
        {
            try
            {
                SqlDataReader SqlDr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(conexion);

                Id = 0;
                Minutos = 0;
                Dependientes = false;
                string[] Parametros = { "@Cu_User", "@Cu_Pass" };
                string[] Valores = { Usuario.Cu_User, Usuario.Cu_pass };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spLogin", ref SqlDr, Parametros, Valores);

                if (SqlDr.HasRows == true)
                {
                    SqlDr.Read();
                    Id = SqlDr.GetInt32(0);
                    if (Id == 1)
                    {
                        Usuario.Id_Cd = SqlDr.GetInt32(SqlDr.GetOrdinal("Id_Cd"));
                        Usuario.Id_U = SqlDr.GetInt32(SqlDr.GetOrdinal("Id_U"));
                        Usuario.U_Nombre = SqlDr.GetString(SqlDr.GetOrdinal("U_Nombre"));
                        Usuario.U_Correo = SqlDr.GetString(SqlDr.GetOrdinal("U_Correo"));
                        Usuario.Id_TU = SqlDr.GetInt32(SqlDr.GetOrdinal("Id_TU"));
                        Usuario.U_VerTodo = SqlDr.GetBoolean(SqlDr.GetOrdinal("U_VerTodo"));
                        Usuario.U_MultiCentro = SqlDr.GetBoolean(SqlDr.GetOrdinal("U_MultiOfi"));
                        Usuario.Cu_Estatus = SqlDr.GetBoolean(SqlDr.GetOrdinal("Cu_Estatus"));
                        Usuario.Cu_Caducada = SqlDr.GetBoolean(SqlDr.GetOrdinal("Cu_Caducada"));
                        Minutos = SqlDr.GetInt32(SqlDr.GetOrdinal("Minutos"));
                        Dependientes = Convert.ToBoolean(SqlDr.GetValue(SqlDr.GetOrdinal("Dependientes")));
                        Usuario.Id_Emp = SqlDr.GetInt32(SqlDr.GetOrdinal("Id_Emp"));
                        Usuario.CalendarioIni = SqlDr.GetDateTime(SqlDr.GetOrdinal("CalendarioIni"));
                        Usuario.CalendarioFin = SqlDr.GetDateTime(SqlDr.GetOrdinal("CalendarioFin"));
                        Usuario.Cu_Activo = Convert.ToBoolean(SqlDr.GetValue(SqlDr.GetOrdinal("Activo")));
                        Usuario.cc_Propia = Convert.ToBoolean(SqlDr.GetValue(SqlDr.GetOrdinal("Propia")));
                        Usuario.Id_Rik = SqlDr.GetInt32(SqlDr.GetOrdinal("Id_Rik"));
                        Usuario.ProcSvtasAlm = Convert.ToBoolean(SqlDr["ProcSvtasAlm"]);
                        Usuario.ProcEmbAlm = Convert.ToBoolean(SqlDr["ProcEmbAlm"]);
                        Usuario.ProcEntAlm = Convert.ToBoolean(SqlDr["ProcEntAlm"]);
                        Usuario.ProcAlmCob = Convert.ToBoolean(SqlDr["ProcAlmCob"]);
                        Usuario.ProcRevCob = Convert.ToBoolean(SqlDr["ProcRevCob"]);
                    }
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
