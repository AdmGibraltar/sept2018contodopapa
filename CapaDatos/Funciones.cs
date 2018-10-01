using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Reflection;
using System.Diagnostics;

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

        public List<T> GetEntityList<T>(SqlDataReader dr) where T : new()
        {
            // Create a new type of the entity I want            
            List<T> returnObject = new List<T>();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    T curRow = new T();
                    curRow = GetEntity<T>(dr);
                    returnObject.Add(curRow);
                }
            }

            return returnObject;
        }

        public List<T> GetEntityList<T>(DataTable table) where T : new()
        {
            // Create a new type of the entity I want            
            List<T> returnObject = new List<T>();

            foreach (DataRow dRow in table.Rows)
            {
                T curRow = new T();
                curRow = GetEntity<T>(dRow);
                returnObject.Add(curRow);
            }

            return returnObject;
        }

        public T GetEntity<T>(DataRow dr) where T : new()
        {
            // Create a new type of the entity I want
            Type t = typeof(T);
            T returnObject = new T();

            foreach (DataColumn col in dr.Table.Columns)
            {
                string colName = col.ColumnName;

                // Look for the object's property with the columns name, ignore case
                PropertyInfo pInfo = t.GetProperty(colName.ToLower(),
                    BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

                // did we find the property ?
                if (pInfo != null)
                {
                    object val = dr[colName];

                    // is this a Nullable<> type
                    bool IsNullable = (Nullable.GetUnderlyingType(pInfo.PropertyType) != null);
                    if (IsNullable)
                    {
                        if (val is System.DBNull)
                        {
                            val = null;
                        }
                        else
                        {
                            // Convert the db type into the T we have in our Nullable<T> type
                            val = Convert.ChangeType
                                (val, Nullable.GetUnderlyingType(pInfo.PropertyType));
                        }
                    }
                    else
                    {
                        // Convert the db type into the type of the property in our entity
                        val = Convert.ChangeType(val, pInfo.PropertyType);
                    }
                    // Set the value of the property with the value from the db
                    pInfo.SetValue(returnObject, val, null);
                }
            }

            // return the entity object with values
            return returnObject;

        }

        public T GetEntity<T>(IDataRecord dr) where T : new()
        {
            // Create a new type of the entity I want
            Type t = typeof(T);
            T returnObject = new T();

            for (int colIndex = 0; colIndex <= dr.FieldCount - 1; colIndex++)
            {
                string colName = dr.GetName(colIndex);

                // Look for the object's property with the columns name, ignore case
                PropertyInfo pInfo = t.GetProperty(colName.ToLower(),
                    BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

                // did we find the property ?
                if (pInfo != null)
                {
                    object val = dr[colName];

                    // is this a Nullable<> type
                    bool IsNullable = (Nullable.GetUnderlyingType(pInfo.PropertyType) != null);
                    if (IsNullable)
                    {
                        if (val is System.DBNull)
                        {
                            val = null;
                        }
                        else
                        {
                            // Convert the db type into the T we have in our Nullable<T> type
                            val = Convert.ChangeType
                                (val, Nullable.GetUnderlyingType(pInfo.PropertyType));
                        }
                    }
                    else
                    {
                        // Convert the db type into the type of the property in our entity
                        val = Convert.ChangeType(val, pInfo.PropertyType);
                    }
                    // Set the value of the property with the value from the db
                    pInfo.SetValue(returnObject, val, null);
                }
            }

            // return the entity object with values
            return returnObject;

        }

        //public void WriteToEventLog(string node, string method, Exception ex)
        //{
        //    string template = "Ocurrió un error.\n\nMétodo: {0}\n\nMensaje: {1}";
        //    string errMensaje = GetMessageFromException(ex);

        //    string mensaje = string.Format(template, method, errMensaje);

        //    if (!EventLog.SourceExists(node))
        //    {
        //        EventLog.CreateEventSource(node, "SIANWeb");
        //    }

        //    EventLog.WriteEntry(node, mensaje, EventLogEntryType.Error);
                            
        //}

       public string GetMessageFromException(Exception ex)
       {
           string mensaje = ex.Message;

           if (ex.InnerException != null)
           {
               mensaje += "\n" + GetMessageFromException(ex.InnerException);
           }           

           return mensaje;
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
