using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using CapaEntidad;
using CapaModelo;
using System.Data;

namespace CapaDatos
{
    public class CD__Comun
    {
        /*prueba*/
        public void LlenaCombo(string SP, string Conexion, ref System.Collections.Generic.List<Comun> list)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand(SP, ref dr);

                Comun Comun = default(Comun);
                while (dr.Read())
                {
                    Comun = new Comun();
                    Comun.Id = dr.GetInt32(dr.GetOrdinal("Id"));
                    Comun.Descripcion = dr.GetString(dr.GetOrdinal("Descripcion"));
                    try
                    {
                        Comun.Relacion = dr.IsDBNull(dr.GetOrdinal("Relacion")) ? "" : dr.GetValue(dr.GetOrdinal("Relacion")).ToString();
                    }
                    catch
                    {
                        Comun.Relacion = "sin_relacion";

                    }
                    list.Add(Comun);
                }
                dr.Close();

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void LlenaCombo(Int32 Id1, string SP, string Conexion, ref System.Collections.Generic.List<Comun> list)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id1" };

                object[] Valores = { Id1 };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand(SP, ref dr, Parametros, Valores);

                Comun Comun = default(Comun);
                while (dr.Read())
                {
                    Comun = new Comun();
                    Comun.Id = dr.GetInt32(dr.GetOrdinal("Id"));
                    Comun.Descripcion = dr.GetString(dr.GetOrdinal("Descripcion"));
                    try
                    {
                        Comun.Relacion = dr.IsDBNull(dr.GetOrdinal("Relacion")) ? "" : dr.GetValue(dr.GetOrdinal("Relacion")).ToString();
                    }
                    catch
                    {
                        Comun.Relacion = "sin_relacion";

                    }
                    list.Add(Comun);
                }
                dr.Close();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void LlenaCombo(Int32 Id1, Int32 Id2, string SP, string Conexion, ref System.Collections.Generic.List<Comun> list, params bool[] claveString)
        {

            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id1", "@Id2" };

                object[] Valores = { Id1, Id2 };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand(SP, ref dr, Parametros, Valores);

                Comun Comun = default(Comun);
                while (dr.Read())
                {
                    Comun = new Comun();

                    if (claveString.Length > 0)
                    {
                        if (claveString[0])
                            Comun.IdStr = dr.GetValue(dr.GetOrdinal("Id")).ToString();
                        else
                            Comun.Id = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id")));
                    }
                    else
                    {
                        Comun.Id = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id")));
                    }
                    Comun.Descripcion = dr.GetString(dr.GetOrdinal("Descripcion"));
                    try
                    {
                        Comun.Relacion = dr.IsDBNull(dr.GetOrdinal("Relacion")) ? "" : dr.GetValue(dr.GetOrdinal("Relacion")).ToString();
                    }
                    catch
                    {
                        Comun.Relacion = "sin_relacion";

                    }
                    list.Add(Comun);
                }
                dr.Close();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void LlenaComboContr(Int32 Id1, Int32 Id2, Int32 Id3, string SP, string Conexion, ref System.Collections.Generic.List<Comun> list, params bool[] claveString)
        {

            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id1", "@Id2", "@Id3" };

                object[] Valores = { Id1, Id2, Id3  };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand(SP, ref dr, Parametros, Valores);

                Comun Comun = default(Comun);
                while (dr.Read())
                {
                    Comun = new Comun();

                    if (claveString.Length > 0)
                    {
                        if (claveString[0])
                            Comun.IdStr = dr.GetValue(dr.GetOrdinal("Id")).ToString();
                        else
                            Comun.Id = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id")));
                    }
                    else
                    {
                        Comun.Id = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id")));
                    }
                    Comun.Descripcion = dr.GetString(dr.GetOrdinal("Descripcion"));
                    try
                    {
                        Comun.Relacion = dr.IsDBNull(dr.GetOrdinal("Relacion")) ? "" : dr.GetValue(dr.GetOrdinal("Relacion")).ToString();
                    }
                    catch
                    {
                        Comun.Relacion = "sin_relacion";

                    }
                    list.Add(Comun);
                }
                dr.Close();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void LlenaCombo<T>(Dictionary<string, object> iPars, string SP, string Conexion, ref System.Collections.Generic.List<T> list) where T : class, new()
        {
            try
            {                
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                Funciones funcs = new Funciones();
                
                string[] Parametros = iPars.Select(x => x.Key).ToArray();
                object[] Valores = iPars.Select(x => x.Value).ToArray();                               

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand(SP, ref dr, Parametros, Valores);

                T Comun = default(T);
                while (dr.Read())
                {
                    Comun = new T();                    
                    Comun = funcs.GetEntity<T>(dr);

                    list.Add(Comun);
                }
                dr.Close();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void LlenaCombo(Int32 Id1, Int32 Id2, int? Id3, string SP, string Conexion, ref System.Collections.Generic.List<Comun> list)
        {

            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id1", "@Id2", "@Id3" };
                object[] Valores = { Id1, Id2, Id3 };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand(SP, ref dr, Parametros, Valores);

                Comun Comun = default(Comun);
                while (dr.Read())
                {
                    Comun = new Comun();
                    Comun.Id = dr.GetInt32(dr.GetOrdinal("Id"));
                    Comun.Descripcion = dr.GetString(dr.GetOrdinal("Descripcion"));
                    try
                    {
                        Comun.Relacion = dr.IsDBNull(dr.GetOrdinal("Relacion")) ? "" : dr.GetValue(dr.GetOrdinal("Relacion")).ToString();
                    }
                    catch
                    {
                        Comun.Relacion = "sin_relacion";

                    }


                    list.Add(Comun);
                }
                dr.Close();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void LlenaCombo(Int32 Id1, Int32 Id2, Int32 Id3, int? Id4, string SP, string Conexion, ref System.Collections.Generic.List<Comun> list)
        {

            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id1", "@Id2", "@Id3", "@Id4" };
                object[] Valores = { Id1, Id2, Id3, Id4 };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand(SP, ref dr, Parametros, Valores);

                Comun Comun = default(Comun);
                while (dr.Read())
                {
                    Comun = new Comun();
                    Comun.Id = dr.GetInt32(dr.GetOrdinal("Id"));
                    Comun.Descripcion = dr.GetString(dr.GetOrdinal("Descripcion"));
                    try
                    {
                        Comun.Relacion = dr.IsDBNull(dr.GetOrdinal("Relacion")) ? "" : dr.GetValue(dr.GetOrdinal("Relacion")).ToString();
                    }
                    catch
                    {
                        Comun.Relacion = "sin_relacion";

                    }
                    try {
                        Comun.ValorBool = dr.IsDBNull(dr.GetOrdinal("isDefault")) ? false : dr.GetValue(dr.GetOrdinal("isDefault")).ToString() == "0" || dr.GetValue(dr.GetOrdinal("isDefault")).ToString().ToUpper() == "FALSE"? false : true;
                    }
                    catch { }
                    list.Add(Comun);
                }
                dr.Close();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void LlenaComboCRM(Int32 Id1, Int32 Id2, Int32 Id3, int? Id4, string SP, string Conexion, ref System.Collections.Generic.List<Comun> list, Int32 Id_Cd_Ver)
        {

            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id1", "@Id2", "@Id3", "@Id4", "@Id_Cd_Ver" };
                object[] Valores = { Id1, Id2, Id3, Id4, Id_Cd_Ver };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand(SP, ref dr, Parametros, Valores);

                Comun Comun = default(Comun);
                while (dr.Read())
                {
                    Comun = new Comun();
                    Comun.Id = dr.GetInt32(dr.GetOrdinal("Id"));
                    Comun.Descripcion = dr.GetString(dr.GetOrdinal("Descripcion"));
                    try
                    {
                        Comun.Relacion = dr.IsDBNull(dr.GetOrdinal("Relacion")) ? "" : dr.GetValue(dr.GetOrdinal("Relacion")).ToString();
                    }
                    catch
                    {
                        Comun.Relacion = "sin_relacion";

                    }
                    list.Add(Comun);
                }
                dr.Close();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void LlenaComboUEN(Int32 Id1, Int32 Id2, Int32 Id3, int? Id4, string SP, string Conexion, ref System.Collections.Generic.List<Comun> list, int Id_Cd_Ver)
        {

            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id1", "@Id2", "@Id3", "@Id4", "@Id_Cd_Ver" };
                object[] Valores = { Id1, Id2, Id3, Id4, Id_Cd_Ver };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand(SP, ref dr, Parametros, Valores);

                Comun Comun = default(Comun);
                while (dr.Read())
                {
                    Comun = new Comun();
                    Comun.Id = dr.GetInt32(dr.GetOrdinal("Id"));
                    Comun.Descripcion = dr.GetString(dr.GetOrdinal("Descripcion"));
                    try
                    {
                        Comun.Relacion = dr.IsDBNull(dr.GetOrdinal("Relacion")) ? "" : dr.GetValue(dr.GetOrdinal("Relacion")).ToString();
                    }
                    catch
                    {
                        Comun.Relacion = "sin_relacion";

                    }
                    list.Add(Comun);
                }
                dr.Close();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void LlenaCombo(object Id1, object Id2, object Id3, object Id4, object Id5, string SP, string Conexion, ref System.Collections.Generic.List<Comun> list)
        {

            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id1", "@Id2", "@Id3", "@Id4", "@Id5" };
                object[] Valores = { Id1, Id2, Id3, Id4, Id5 };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand(SP, ref dr, Parametros, Valores);

                Comun Comun = default(Comun);
                while (dr.Read())
                {
                    Comun = new Comun();
                    Comun.Id = dr.GetInt32(dr.GetOrdinal("Id"));
                    Comun.Descripcion = dr.GetString(dr.GetOrdinal("Descripcion"));
                    try
                    {
                        Comun.Relacion = dr.IsDBNull(dr.GetOrdinal("Relacion")) ? "" : dr.GetValue(dr.GetOrdinal("Relacion")).ToString();
                    }
                    catch
                    {
                        Comun.Relacion = "sin_relacion";

                    }
                    list.Add(Comun);
                }
                dr.Close();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void LlenaCombo(object Id1, object Id2, object Id3, object Id4, object Id5, object Id6, string SP, string Conexion, ref System.Collections.Generic.List<Comun> list)
        {

            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id1", "@Id2", "@Id3", "@Id4", "@Id5", "@IdTipoRep" };
                object[] Valores = { Id1, Id2, Id3, Id4, Id5, Id6};

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand(SP, ref dr, Parametros, Valores);

                Comun Comun = default(Comun);
                while (dr.Read())
                {
                    Comun = new Comun();
                    Comun.Id = dr.GetInt32(dr.GetOrdinal("Id"));
                    Comun.Descripcion = dr.GetString(dr.GetOrdinal("Descripcion"));
                    try
                    {
                        Comun.Relacion = dr.IsDBNull(dr.GetOrdinal("Relacion")) ? "" : dr.GetValue(dr.GetOrdinal("Relacion")).ToString();
                    }
                    catch
                    {
                        Comun.Relacion = "sin_relacion";

                    }
                    list.Add(Comun);
                }
                dr.Close();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
       
        public void LlenaComboTabla(Int32 Id1, Int32 Id2, Int32 Id3, string SP, string Conexion, ref System.Collections.Generic.List<Comun> list)
        {

            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id1", "@Id2", "@Id3" };
                object[] Valores = { Id1, Id2, Id3 };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand(SP, ref dr, Parametros, Valores);

                Comun Comun = default(Comun);
                while (dr.Read())
                {
                    Comun = new Comun();
                    Comun.Id = dr.GetInt32(dr.GetOrdinal("Id"));
                    Comun.Descripcion = dr.GetString(dr.GetOrdinal("Descripcion"));
                    try
                    {
                        Comun.Relacion = dr.IsDBNull(dr.GetOrdinal("Relacion")) ? "" : dr.GetValue(dr.GetOrdinal("Relacion")).ToString();
                    }
                    catch
                    {
                        Comun.Relacion = "sin_relacion";

                    }
                    list.Add(Comun);
                }
                dr.Close();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void LlenaComboStr(Int32 Id1, string SP, string Conexion, ref System.Collections.Generic.List<Comun> list)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id1" };
                object[] Valores = { Id1 };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand(SP, ref dr, Parametros, Valores);

                Comun Comun = default(Comun);
                while (dr.Read())
                {
                    Comun = new Comun();
                    Comun.IdStr = dr.GetString(dr.GetOrdinal("IdStr"));
                    Comun.Descripcion = dr.GetString(dr.GetOrdinal("Descripcion"));

                    list.Add(Comun);
                }
                dr.Close();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CorreoRecuperaDatos(ref CentroDistribucion Cdis, ref ConfiguracionGlobal Configuarcion, string Conexion)
        {
            try
            {
                SqlDataReader SqlDr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Ofi" };
                object[] Valores = { Cdis.Id_Cd };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCorreoRecuperaDatos", ref SqlDr, Parametros, Valores);

                SqlDr.Read();

                //Oficina.Ofi_Logo = SqlDr.Item("Ofi_Logo").ToString;
                Cdis.Cd_Descripcion = SqlDr.GetString(SqlDr.GetOrdinal("Ofi_Descripcion"));
                Cdis.Cd_Tel = SqlDr.GetString(SqlDr.GetOrdinal("Ofi_Tel"));
                //Oficina.Ofi_Pagina = SqlDr.Item("Ofi_Pagina").ToString;
                Cdis.Cd_CalleNo = SqlDr.GetString(SqlDr.GetOrdinal("Ofi_DireccionUbicacion"));
                Configuarcion.Mail_Servidor = SqlDr.GetString(SqlDr.GetOrdinal("Mail_Servidor"));
                Configuarcion.Mail_Usuario = SqlDr.GetString(SqlDr.GetOrdinal("Mail_Usuario"));
                Configuarcion.Mail_Contraseña = SqlDr.GetString(SqlDr.GetOrdinal("Mail_Contraseña"));
                Configuarcion.Mail_Puerto = SqlDr.GetString(SqlDr.GetOrdinal("Mail_Puerto"));
                Configuarcion.Mail_Remitente = SqlDr.GetString(SqlDr.GetOrdinal("Mail_Remitente"));

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Maximo(int Id_Emp, int Id_Cd, string Tabla, string Columna, string SP, string Conexion, ref string maximo)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Tabla", "@Columna" };

                object[] Valores = { Id_Emp, Id_Cd, Tabla, Columna };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand(SP, ref dr, Parametros, Valores);


                while (dr.Read())
                {
                    maximo = dr.GetValue(dr.GetOrdinal("Id")).ToString();
                }
                dr.Close();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Maximo(int Id_Emp, int Id_Cd, string Tabla, string Columna, string SP, ICD_Contexto icdCtx, ref string maximo)
        {
            try
            {
                SqlDataReader dr = null;
                //CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                ICD_Contexto<sianwebmty_gEntities> ctx = ((ICD_Contexto<sianwebmty_gEntities>)icdCtx);
                IDbTransaction transaction = ctx.Contexto.Database.CurrentTransaction.UnderlyingTransaction;
                SqlCommand sqlcmd = transaction.Connection.CreateCommand() as SqlCommand;
                sqlcmd.Transaction = (System.Data.SqlClient.SqlTransaction)transaction;

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Tabla", "@Columna" };

                object[] Valores = { Id_Emp, Id_Cd, Tabla, Columna };


                sqlcmd = CD_Datos.GenerarSqlCommand(SP, ref dr, Parametros, Valores, sqlcmd);
                

                while (dr.Read())
                {
                    maximo = dr.GetValue(dr.GetOrdinal("Id")).ToString();
                }
                dr.Close();
                sqlcmd.Dispose();
                //CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Maximo(int Id_Emp, int Id_Cd, string Tabla, string Columna,int Tipo, string SP, string Conexion, ref string maximo)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Tabla", "@Columna", "@Tipo" };

                object[] Valores = { Id_Emp, Id_Cd, Tabla, Columna, Tipo };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand(SP, ref dr, Parametros, Valores);


                while (dr.Read())
                {
                    maximo = dr.GetValue(dr.GetOrdinal("Id")).ToString();
                }
                dr.Close();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void MaximoTerritorio
        (int Id_Emp, int Id_Cd, int Id_TipoRepresentante,int Id_TipoCliente,int Id_Uen,int Id_Seg,string SP,string Conexion,ref string maximo)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = 
                {
                    "@Id_Emp",
                    "@Id_Cd",
                    "@Id_TipoRepresentante",
                    "@Id_TipoCliente",
                    "@Id_Uen",
                    "@Id_Seg"
                };

                    
                object[] Valores = 
                { 
                    Id_Emp,
                    Id_Cd,
                    Id_TipoRepresentante,
                    Id_TipoCliente,
                    Id_Uen,
                    Id_Seg
                };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand(SP, ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    maximo = dr.GetValue(dr.GetOrdinal("Id")).ToString();
                }
                dr.Close();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void MaximoTerritorio
        (int Id_Emp, int Id_Cd, int Id_TipoRepresentante, int Id_TipoCliente, int Id_Uen, int Id_Seg, string prefix, string SP, string Conexion, ref string maximo)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = 
                {
                    "@Id_Emp",
                    "@Id_Cd",
                    "@Id_TipoRepresentante",
                    "@Id_TipoCliente",
                    "@Id_Uen",
                    "@Id_Seg",
                    "@Prefix"
                };


                object[] Valores = 
                { 
                    Id_Emp,
                    Id_Cd,
                    Id_TipoRepresentante,
                    Id_TipoCliente,
                    Id_Uen,
                    Id_Seg,
                    prefix
                };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand(SP, ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    maximo = dr.GetValue(dr.GetOrdinal("Id")).ToString();
                }
                dr.Close();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void Maximo(int Id_Emp, string Tabla, string Columna, string SP, string Conexion, ref string maximo)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Tabla", "@Columna" };

                object[] Valores = { Id_Emp, Tabla, Columna };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand(SP, ref dr, Parametros, Valores);


                while (dr.Read())
                {
                    maximo = dr.GetValue(dr.GetOrdinal("Id")).ToString();
                }
                dr.Close();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Deshabilitar(Catalogo ct, string Conexion, ref bool verificador)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = { "@Tabla", "@Columna", "@Id_Emp", "@Id_Cd", "@Id" };
                object[] Valores = { ct.Tabla, ct.Columna, ct.Id_Emp, ct.Id_Cd, ct.IsStr == true ? ct.IdStr : ct.Id.ToString() };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatalogo_Deshabilitar", ref dr, Parametros, Valores);

                if (dr.HasRows)
                {
                    dr.Read();
                    verificador = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("verificador")));
                }
                dr.Close();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Maximo(int Id_Emp, string Tabla, int naturaleza, string Columna, string SP, string Conexion, ref string maximo)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = { "@Id_Emp", "@Tabla", "@Columna", "@Naturaleza" };
                object[] Valores = { Id_Emp, Tabla, Columna, naturaleza };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand(SP, ref dr, Parametros, Valores);
                while (dr.Read())
                {
                    maximo = dr.GetValue(dr.GetOrdinal("Id")).ToString();
                }
                dr.Close();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Actual(int Id_Emp, int Id_Cd, string Emp_Cnx, int? Cte, int? Prd, int? Terr, string sp, DateTime Fecha, ref double actual)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Emp_Cnx);

                string[] Parametros = { 
                                          "@Id_Emp", 
                                          "@Id_Cd", 
                                          "@Id_Cte", 
                                          "@Id_Prd", 
                                          "@Fecha", 
                                          "@Id_Ter",
                                          "@Ignora_Agrup",
                                          "@Ultimo_Cierre"
                                      };
                object[] Valores = { 
                                       Id_Emp, 
                                       Id_Cd, 
                                       Cte, 
                                       Prd, 
                                       Fecha, 
                                       Terr,
                                       1,
                                       1
                                   };

                SqlCommand sqlcmd;

                sqlcmd = CapaDatos.GenerarSqlCommand(sp, ref dr, Parametros, Valores);

                if (dr.HasRows)
                {
                    dr.Read();
                    actual = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Actual")));
                }
                dr.Close();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaFactura_ConsecutivoFacElectronica(int Id_Emp, int Id_Cd, int Id_Cfe, int Cfe_TMov, ref int verificador, string Conexion)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_Cfe", "@Cfe_TMov" };
                object[] Valores = { Id_Emp, Id_Cd, Id_Cfe == -1 ? (int?)null : Id_Cfe, Cfe_TMov };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapFactura_ConsultarConsFacElectronica", ref verificador, Parametros, Valores);

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }




    }
}
