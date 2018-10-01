using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using CapaEntidad;
using CapaModelo;

namespace CapaDatos
{
    public class CD_Datos
    {
        StringBuilder qry;
        #region Variable de clase
        /// <summary>
        /// Conexión usando el proveedor sqlClient
        /// </summary>
        private SqlConnection sqlcnx;
        /// <summary>
        /// Transacción de la conexión
        /// </summary>
        private SqlTransaction trans;
        #endregion
        #region Constructor
        /// <summary>
        /// Este constructor inicia la conexión con la base de datos
        /// </summary>
        /// <param name="Conexion">Cadena de conexión</param>
        public CD_Datos(string Conexion)
        {
            this.sqlcnx = new SqlConnection(Conexion);
        }
        #endregion
        #region Métodos para el manejo de la transacción
        /// <summary>
        /// Inicia transacción
        /// </summary>
        public void StartTrans()
        {
            if (sqlcnx.State == ConnectionState.Closed) sqlcnx.Open();
            if (trans == null) trans = sqlcnx.BeginTransaction();
        }

        /// <summary>
        /// Confirma transacción.
        /// </summary>
        public void CommitTrans()
        {
            trans.Commit();
            trans.Dispose();
            sqlcnx.Close();
        }

        /// <summary>
        /// Cancela la transacción.
        /// </summary>
        public void RollBackTrans()
        {
            trans.Rollback();
            trans.Dispose();
            sqlcnx.Close();
        }

        #endregion
        #region Métodos
        #region ExecuteReader
        public SqlCommand GenerarSqlCommand(string SP, ref SqlDataReader dr)
        {
            SqlCommand functionReturnValue = default(SqlCommand);
            try
            {
                //SqlConnection sqlcnx = default(SqlConnection);
                Funciones DesEncr = new Funciones();
                //sqlcnx = new SqlConnection(_StrCnx);
                functionReturnValue = new SqlCommand(SP, sqlcnx);
                functionReturnValue.CommandType = CommandType.StoredProcedure;

                if (trans != null) functionReturnValue.Transaction = trans;
                if (trans == null) sqlcnx.Open();

                functionReturnValue.CommandTimeout = 0;
                //functionReturnValue.Connection.Open();
                dr = functionReturnValue.ExecuteReader(CommandBehavior.CloseConnection);

                //if (trans == null) sqlcnx.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return functionReturnValue;
        }
        public SqlCommand GenerarSqlCommand(string SP, string tableName, ref DataTable dt)
        {
            SqlCommand functionReturnValue = default(SqlCommand);
            try
            {
                functionReturnValue = new SqlCommand(SP, sqlcnx);
                functionReturnValue.CommandType = CommandType.StoredProcedure;

                if (trans != null) functionReturnValue.Transaction = trans;
                if (trans == null) sqlcnx.Open();

                functionReturnValue.CommandTimeout = 0;
                //functionReturnValue.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(functionReturnValue);
                dt = new DataTable(tableName);
                da.Fill(dt);

                da.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return functionReturnValue;
        }
        public SqlCommand GenerarSqlCommand(string SP, string nombreTabla, ref DataTable dt, string[] parametros, object[] resultados)
        {
            SqlCommand functionReturnValue = default(SqlCommand);
            try
            {
                functionReturnValue = new SqlCommand(SP, sqlcnx);
                functionReturnValue.CommandType = CommandType.StoredProcedure;

                if (trans != null) functionReturnValue.Transaction = trans;
                if (trans == null) sqlcnx.Open();

                resultados = Eliminarcomilla(resultados);

                functionReturnValue.CommandTimeout = 0;
                if ((parametros != null))
                {
                    for (int l = 0; l <= parametros.Length - 1; l++)
                        functionReturnValue.Parameters.AddWithValue(parametros[l], resultados[l]);
                }
                //functionReturnValue.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(functionReturnValue);
                dt = new DataTable(nombreTabla);
                da.Fill(dt);

                da.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return functionReturnValue;
        }
        public SqlCommand GenerarSqlCommand(string SP, ref DataSet ds, string[] parametros, object[] resultados)
        {
            SqlCommand functionReturnValue = default(SqlCommand);
            try
            {
                functionReturnValue = new SqlCommand(SP, sqlcnx);
                functionReturnValue.CommandType = CommandType.StoredProcedure;

                if (trans != null) functionReturnValue.Transaction = trans;
                if (trans == null) sqlcnx.Open();

                resultados = Eliminarcomilla(resultados);

                functionReturnValue.CommandTimeout = 0;
                if ((parametros != null))
                {
                    for (int l = 0; l <= parametros.Length - 1; l++)
                        functionReturnValue.Parameters.AddWithValue(parametros[l], resultados[l]);
                }

                //functionReturnValue.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(functionReturnValue);
                ds = new DataSet();
                da.Fill(ds);

                da.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return functionReturnValue;
        }
        public SqlCommand GenerarSqlCommand(string SP, ref  SqlDataReader dr, string[] parametros, object[] resultados)
        {
            SqlCommand functionReturnValue = default(SqlCommand);
            try
            {
                //SqlConnection sqlcnx = default(SqlConnection);
                Funciones DesEncr = new Funciones();
                //sqlcnx = new SqlConnection(_StrCnx);
                functionReturnValue = new SqlCommand(SP, sqlcnx);
                functionReturnValue.CommandType = CommandType.StoredProcedure;

                if (trans != null) functionReturnValue.Transaction = trans;
                if (trans == null) sqlcnx.Open();

                resultados = Eliminarcomilla(resultados);

                functionReturnValue.CommandTimeout = 0;
                if ((parametros != null))
                {
                    for (int l = 0; l <= parametros.Length - 1; l++)
                    {
                        functionReturnValue.Parameters.AddWithValue(parametros[l], resultados[l]);
                    }
                }
                //functionReturnValue.Connection.Open();
                dr = functionReturnValue.ExecuteReader(CommandBehavior.CloseConnection);

                //if (trans == null) sqlcnx.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return functionReturnValue;
        }

        /// <summary>
        /// Versión que acepta un contexto de conexión a la fuente de datos.
        /// </summary>
        /// <param name="SP"></param>
        /// <param name="dr"></param>
        /// <param name="parametros"></param>
        /// <param name="resultados"></param>
        /// <param name="icdCtx">Contexto de conexión a la fuente de datos</param>
        /// <returns></returns>
        public static SqlCommand GenerarSqlCommand(string SP, ref  SqlDataReader dr, string[] parametros, object[] resultados, ICD_Contexto icdCtx)
        {
            try
            {
                ICD_Contexto<sianwebmty_gEntities> ctx = ((ICD_Contexto<sianwebmty_gEntities>)icdCtx);
                IDbTransaction transaction = ctx.Contexto.Database.CurrentTransaction.UnderlyingTransaction;
                SqlCommand functionReturnValue = null;

                functionReturnValue = transaction.Connection.CreateCommand() as SqlCommand;
                functionReturnValue.Transaction = (System.Data.SqlClient.SqlTransaction)transaction;
                functionReturnValue.CommandText = SP;

                Funciones DesEncr = new Funciones();

                functionReturnValue.CommandType = CommandType.StoredProcedure;

                

                resultados = CD_Datos.EliminarcomillaStat(resultados);

                functionReturnValue.CommandTimeout = 0;
                if ((parametros != null))
                {
                    for (int l = 0; l <= parametros.Length - 1; l++)
                    {
                        functionReturnValue.Parameters.AddWithValue(parametros[l], resultados[l]);
                    }
                }

                dr = functionReturnValue.ExecuteReader();

                return functionReturnValue;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        public static SqlCommand GenerarSqlCommand(string SP, ref  SqlDataReader dr, string[] parametros, object[] resultados, SqlCommand cmd)
        {
            //SqlCommand functionReturnValue = default(SqlCommand);
            try
            {
                //SqlConnection sqlcnx = default(SqlConnection);
                Funciones DesEncr = new Funciones();
                //sqlcnx = new SqlConnection(_StrCnx);
                //functionReturnValue = new SqlCommand(SP, sqlcnx);
                cmd.CommandText = SP;
                cmd.CommandType = CommandType.StoredProcedure;

                resultados = EliminarcomillaStat(resultados);

                cmd.CommandTimeout = 0;
                if ((parametros != null))
                {
                    for (int l = 0; l <= parametros.Length - 1; l++)
                    {
                        cmd.Parameters.AddWithValue(parametros[l], resultados[l]);
                    }
                }
                //functionReturnValue.Connection.Open();
                dr = cmd.ExecuteReader();

                //if (trans == null) sqlcnx.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return cmd;
        }

        public static SqlCommand GenerarSqlCommand(string SP, ref int verificador, string[] parametros, object[] resultados, IDbConnection connection, SqlCommand dbCommand)
        {
            try
            {
                //SqlConnection sqlcnx = default(SqlConnection);
                Funciones DesEncr = new Funciones();
                //sqlcnx = new SqlConnection(_StrCnx);
                dbCommand.CommandText = SP;
                dbCommand.CommandType = CommandType.StoredProcedure;

                resultados = EliminarcomillaStat(resultados);

                dbCommand.CommandTimeout = 0;
                StringBuilder _qry = new StringBuilder("EXEC " + SP + " ");
                if ((parametros != null))
                {
                    for (int l = 0; l <= parametros.Length - 1; l++)
                    {
                        dbCommand.Parameters.AddWithValue(parametros[l], resultados[l]);
                        _qry.Append(parametros[l] + "=" + resultados[l] + ", ");
                    }
                }
                //functionReturnValue.Connection.Open();
                object res = dbCommand.ExecuteScalar();
                if (res == DBNull.Value)
                {
                    res = 0;
                }
                verificador = Convert.ToInt32(res);

                //if (trans == null) sqlcnx.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dbCommand;
        }
        #endregion
        #region ExecuteScalar
        public SqlCommand GenerarSqlCommand(string SP, ref int verificador)
        {
            SqlCommand functionReturnValue = default(SqlCommand);
            try
            {
                //SqlConnection sqlcnx = default(SqlConnection);
                Funciones DesEncr = new Funciones();
                //sqlcnx = new SqlConnection(_StrCnx);
                functionReturnValue = new SqlCommand(SP, sqlcnx);
                functionReturnValue.CommandType = CommandType.StoredProcedure;

                if (trans != null) functionReturnValue.Transaction = trans;
                if (trans == null) sqlcnx.Open();

                functionReturnValue.CommandTimeout = 0;
                //functionReturnValue.Connection.Open();
                verificador = functionReturnValue.ExecuteNonQuery();

                //if (trans == null) sqlcnx.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return functionReturnValue;
        }
        public SqlCommand GenerarSqlCommand(string SP, ref int verificador, string[] parametros, object[] resultados)
        {
            SqlCommand functionReturnValue = default(SqlCommand);
            try
            {
                //SqlConnection sqlcnx = default(SqlConnection);
                Funciones DesEncr = new Funciones();
                //sqlcnx = new SqlConnection(_StrCnx);
                functionReturnValue = new SqlCommand(SP, sqlcnx);
                functionReturnValue.CommandType = CommandType.StoredProcedure;

                if (trans != null) functionReturnValue.Transaction = trans;
                if (trans == null) sqlcnx.Open();

                resultados = Eliminarcomilla(resultados);

                functionReturnValue.CommandTimeout = 0;
                qry = new StringBuilder("EXEC " + SP + " ");
                if ((parametros != null))
                {
                    for (int l = 0; l <= parametros.Length - 1; l++)
                    {
                        functionReturnValue.Parameters.AddWithValue(parametros[l], resultados[l]);
                        qry.Append(parametros[l] + "=" + resultados[l] + ", ");
                    }
                }
                //functionReturnValue.Connection.Open();
                object res = functionReturnValue.ExecuteScalar();
                if (res == DBNull.Value)
                {
                    res = 0;
                }
                verificador = Convert.ToInt32(res);

                //if (trans == null) sqlcnx.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return functionReturnValue;
        }
        public SqlCommand GenerarSqlCommand(string SP, ref object resultado, string[] parametros, object[] resultados)
        {
            SqlCommand functionReturnValue = default(SqlCommand);
            try
            {
                //SqlConnection sqlcnx = default(SqlConnection);
                Funciones DesEncr = new Funciones();
                //sqlcnx = new SqlConnection(_StrCnx);
                functionReturnValue = new SqlCommand(SP, sqlcnx);
                functionReturnValue.CommandType = CommandType.StoredProcedure;

                if (trans != null) functionReturnValue.Transaction = trans;
                if (trans == null) sqlcnx.Open();

                resultados = Eliminarcomilla(resultados);

                functionReturnValue.CommandTimeout = 0;
                qry = new StringBuilder("EXEC " + SP + " ");
                if ((parametros != null))
                {
                    for (int l = 0; l <= parametros.Length - 1; l++)
                    {
                        functionReturnValue.Parameters.AddWithValue(parametros[l], resultados[l]);
                        qry.Append(parametros[l] + "=" + resultados[l] + ", ");
                    }
                }
                //functionReturnValue.Connection.Open();
                resultado = functionReturnValue.ExecuteScalar();

                //if (trans == null) sqlcnx.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return functionReturnValue;
        }
        public SqlCommand GenerarSqlCommand(string SP, ref string verificador, string[] parametros, object[] resultados)
        {
            SqlCommand functionReturnValue = default(SqlCommand);
            try
            {
                //SqlConnection sqlcnx = default(SqlConnection);
                Funciones DesEncr = new Funciones();
                //sqlcnx = new SqlConnection(_StrCnx);
                functionReturnValue = new SqlCommand(SP, sqlcnx);
                functionReturnValue.CommandType = CommandType.StoredProcedure;

                if (trans != null) functionReturnValue.Transaction = trans;
                if (trans == null) sqlcnx.Open();

                resultados = Eliminarcomilla(resultados);

                functionReturnValue.CommandTimeout = 0;
                qry = new StringBuilder("EXEC " + SP + " ");
                if ((parametros != null))
                {
                    for (int l = 0; l <= parametros.Length - 1; l++)
                    {
                        functionReturnValue.Parameters.AddWithValue(parametros[l], resultados[l]);
                        qry.Append(parametros[l] + "=" + resultados[l] + ", ");
                    }
                }
                //functionReturnValue.Connection.Open();
                object resp = functionReturnValue.ExecuteScalar();
                verificador = Convert.ToString(resp);

                //if (trans == null) sqlcnx.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return functionReturnValue;
        }
        public SqlCommand GenerarSqlCommand(string SP, ref int verificador, string[] parametros, object[] resultados, string BitesParametro, byte[] Bites)
        {
            SqlCommand functionReturnValue = default(SqlCommand);
            try
            {
                //SqlConnection sqlcnx = default(SqlConnection);
                Funciones DesEncr = new Funciones();
                //sqlcnx = new SqlConnection(_StrCnx);
                functionReturnValue = new SqlCommand(SP, sqlcnx);
                functionReturnValue.CommandType = CommandType.StoredProcedure;

                if (trans != null) functionReturnValue.Transaction = trans;
                if (trans == null) sqlcnx.Open();

                resultados = Eliminarcomilla(resultados);

                functionReturnValue.CommandTimeout = 0;
                qry = new StringBuilder("EXEC " + SP + " ");
                if ((parametros != null))
                {
                    for (int l = 0; l <= parametros.Length - 1; l++)
                    {
                        functionReturnValue.Parameters.AddWithValue(parametros[l], resultados[l]);
                        qry.Append(parametros[l] + "=" + resultados[l] + ", ");
                    }
                }
                functionReturnValue.Parameters.AddWithValue(BitesParametro, Bites);
                //functionReturnValue.Connection.Open();
                verificador = Convert.ToInt32(functionReturnValue.ExecuteScalar());

                //if (trans == null) sqlcnx.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return functionReturnValue;
        }
        #endregion
        #region ExecuteNonQuery
        public SqlCommand GenerarSqlCommand_Nonquery(string SP, int verificador)
        {
            SqlCommand functionReturnValue = default(SqlCommand);
            try
            {
                //SqlConnection sqlcnx = default(SqlConnection);
                Funciones DesEncr = new Funciones();
                //sqlcnx = new SqlConnection(_StrCnx);
                functionReturnValue = new SqlCommand(SP, sqlcnx);
                functionReturnValue.CommandType = CommandType.StoredProcedure;

                if (trans != null) functionReturnValue.Transaction = trans;
                if (trans == null) sqlcnx.Open();

                functionReturnValue.CommandTimeout = 0;
                //functionReturnValue.Connection.Open();
                verificador = functionReturnValue.ExecuteNonQuery();

                //if (trans == null) sqlcnx.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return functionReturnValue;
        }
        public SqlCommand GenerarSqlCommand_Nonquery(string SP, string[] parametros, object[] resultados)
        {
            SqlCommand functionReturnValue = default(SqlCommand);
            try
            {
                //SqlConnection sqlcnx = default(SqlConnection);
                Funciones DesEncr = new Funciones();
                //sqlcnx = new SqlConnection(_StrCnx);
                functionReturnValue = new SqlCommand(SP, sqlcnx);
                functionReturnValue.CommandType = CommandType.StoredProcedure;

                if (trans != null) functionReturnValue.Transaction = trans;
                if (trans == null) sqlcnx.Open();

                resultados = Eliminarcomilla(resultados);

                functionReturnValue.CommandTimeout = 0;
                if ((parametros != null))
                {
                    for (int l = 0; l <= parametros.Length - 1; l++)
                    {
                        functionReturnValue.Parameters.AddWithValue(parametros[l], resultados[l]);
                    }
                }
                //functionReturnValue.Connection.Open();
                functionReturnValue.ExecuteNonQuery();

                //if (trans == null) sqlcnx.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return functionReturnValue;
        }
        public SqlCommand GenerarSqlCommand_Nonquery(string SP, int verificador, string[] parametros, object[] resultados)
        {
            SqlCommand functionReturnValue = default(SqlCommand);
            try
            {
                //SqlConnection sqlcnx = default(SqlConnection);
                Funciones DesEncr = new Funciones();
                //sqlcnx = new SqlConnection(_StrCnx);
                functionReturnValue = new SqlCommand(SP, sqlcnx);
                functionReturnValue.CommandType = CommandType.StoredProcedure;

                if (trans != null) functionReturnValue.Transaction = trans;
                if (trans == null) sqlcnx.Open();

                resultados = Eliminarcomilla(resultados);

                functionReturnValue.CommandTimeout = 0;
                if ((parametros != null))
                {
                    for (int l = 0; l <= parametros.Length - 1; l++)
                    {
                        functionReturnValue.Parameters.AddWithValue(parametros[l], resultados[l]);
                    }
                }
                //functionReturnValue.Connection.Open();
                verificador = functionReturnValue.ExecuteNonQuery();

                //if (trans == null) sqlcnx.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return functionReturnValue;
        }
        #endregion
        public void Bulk(DataTable SourceTable, string tabla)
        {
            //Open a connection with destination database;
            using (sqlcnx)
            {
                sqlcnx.Open();

                //Open bulkcopy connection.
                using (SqlBulkCopy bulkcopy = new SqlBulkCopy(sqlcnx))
                {
                    //Set destination table name
                    //to table previously created.
                    bulkcopy.DestinationTableName = tabla;

                    try
                    {
                        bulkcopy.WriteToServer(SourceTable);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                    sqlcnx.Close();
                }
            }
        }
        public void LimpiarSqlcommand(ref SqlCommand SqlCmd)
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
        #endregion

        private object[] Eliminarcomilla(object[] resultados)
        {

            for (int i = 0; i < resultados.Length; i++)
            {
                if (resultados[i] != null && resultados[i].ToString().Contains("'"))
                {
                    resultados[i] = resultados[i].ToString().Replace("'", "");
                }
            }
            return resultados;
        }

        private static object[] EliminarcomillaStat(object[] resultados)
        {

            for (int i = 0; i < resultados.Length; i++)
            {
                if (resultados[i] != null && resultados[i].ToString().Contains("'"))
                {
                    resultados[i] = resultados[i].ToString().Replace("'", "");
                }
            }
            return resultados;
        }

        public SqlTransaction CurrentTransaction
        {
            get
            {
                return trans;
            }
        }

        public SqlConnection CurrentConnection
        {
            get
            {
                return sqlcnx;
            }
        }
    }
}
