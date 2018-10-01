using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace CapaDatos
{
   public class Funciones
    {
        public string Encripta(string Pass)
        { 
            string functionReturnValue = null;
            try
            {
                string Clave = null;
                int i = 0;
                string Pass2 = null;
                string CAR = null;
                string Codigo = null;
                string comodin = "";
                Clave = "120302";
                Pass2 = "";
               
                for ( i = 1 ; i<= Pass.Length;i++)
                {
                    CAR = Pass.Substring(i, 1);
                    Codigo = Clave.Substring((i - 1) % Clave.Length, 1);
                    comodin = "0" + (Convert.ToInt32(Codigo) ^ Convert.ToInt32(CAR)).ToString("X");
                    Pass2 = Pass2 + comodin.Substring(comodin.Length - 2);
                }
                functionReturnValue = Pass2;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return functionReturnValue;
        }
        public string DesEncripta(string Pass)
        {
            string functionReturnValue = null;
            try
            {
                string Clave = null;
                int i = 0;
                string Pass2 = null;
                string CAR = null;
                string Codigo = null;
                int j = 0;

                Clave = "120302";
                Pass2 = "";
                j = 1;
                for (i = 1; i <= Pass.Length; i += 2)
                {
                    CAR = Pass.Substring( i, 2);
                    Codigo = Clave.Substring(((j - 1) % Clave.Length) + 1, 1);
                    Pass2 = Pass2 + Convert.ToChar(Convert.ToInt32(Codigo) ^ Convert.ToInt32("&h" + CAR));
                    j = j + 1;
                }
                functionReturnValue = Pass2;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return functionReturnValue;
        }

        public string GetLocalTime(int MinutesToAdd)
        {
            try
            {
                DateTime CurrentTime = DateTime.UtcNow;
                return CurrentTime.AddMinutes(MinutesToAdd).ToString("MM/dd/yyyy HH:mm:ss");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DateTime GetLocalDateTime(int MinutesToAdd)
        {
            try
            {
                DateTime CurrentTime = DateTime.UtcNow;
                return CurrentTime.AddMinutes(MinutesToAdd);
                //.ToString("MM/dd/yyyy HH:mm:ss")
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //public void ConfiguracionMail(ref CapaEntidad.ConfiguracionMail MailConfig, string conexion)
        //{
        //    try
        //    {
        //        SqlDataReader dr = null;
        //        CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(conexion);

        //        string[] Parametros = { "@Id_Ofi" };
        //        object[] Valores = { MailConfig.Id_Ofi };
        //        SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spSysConfiguracionMail", dr, Parametros, Valores);
                
        //        //Dim VarSolicitud As ExpClienteSolicitud
        //        while (dr.Read())
        //        {
        //            MailConfig.Mail_Contraseña = dr.GetString(dr.GetOrdinal("Mail_Contraseña"));
        //            MailConfig.Mail_Puerto = dr.GetString(dr.GetOrdinal("Mail_Puerto"));
        //            MailConfig.Mail_Remitente = dr.GetString(dr.GetOrdinal("Mail_Remitente"));
        //            MailConfig.Mail_Servidor = dr.GetString(dr.GetOrdinal("Mail_Servidor"));
        //            MailConfig.Mail_Usuario = dr.GetString(dr.GetOrdinal("Mail_Usuario"));
        //            MailConfig.Ofi_Logo = dr.GetString(dr.GetOrdinal("Ofi_Logo"));
        //        }
        //        dr.Close();
        //        CapaDatos.LimpiarSqlcommand(sqlcmd);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

    }
}
