using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace CapaDatos
{
    public class CD_CRMGraficas
    {
        public void GraficaActividad(ref System.Data.DataSet dsGraficaActividad, string Id_Cd, int Id_Emp, int? Id_U, int? GerSeg_Id, int? GerUen_Id, string Conexion, int intDdl)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_U", "@GerSeg_Id", "@GerUen_Ud", "@tipo" };
                object[] Valores = { Id_Emp, Id_Cd, Id_U, GerSeg_Id, GerUen_Id, intDdl };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCRMGraficaActividad", ref dr, Parametros, Valores);

                //creamos esquema
                DataTable estructura; //= dr.GetSchemaTable();

                //creamos tabla para guardar los datos
                DataTable dataTable; //= new DataTable();


                for (int x = 0; x < 10; x++)
                {
                    estructura = dr.GetSchemaTable();
                    dataTable = new DataTable();

                    //generemos la estructura de columnas
                    for (int i = 0; i <= estructura.Rows.Count - 1; i++)
                    {
                        DataRow dataRow = estructura.Rows[i];
                        string columnName = dataRow["ColumnName"].ToString();
                        DataColumn column = new DataColumn(columnName, (Type)dataRow["DataType"]);
                        dataTable.Columns.Add(column);
                    }
                    dsGraficaActividad.Tables.Add(dataTable);

                    while (dr.Read())
                    {
                        DataRow dataRow = dataTable.NewRow();
                        for (int i = 0; i <= dr.FieldCount - 1; i++)
                        {
                            dataRow[i] = dr.GetValue(i);
                        }
                        dataTable.Rows.Add(dataRow);
                    }
                    if (!dr.NextResult())
                    {
                        break;
                    }
                }




                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GraficaDistribucion(int Id_Emp, int Id_Cd, string Estatus, int? Id_U, int? GerSeg_Id, int? GerUen_Id, int intDdl, DataSet dsGraficaDistribucion, string Conexion)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_U", "@EstatusStr", "GerSeg_Id", "GerUen_Id", "@tipo" };
                object[] Valores = { Id_Emp, Id_Cd, Id_U, Estatus, GerSeg_Id, GerUen_Id, intDdl };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCRMGraficaDistribucion", ref dr, Parametros, Valores);

                //creamos esquema
                DataTable estructura; //= dr.GetSchemaTable();

                //creamos tabla para guardar los datos
                DataTable dataTable; //= new DataTable();


                for (int x = 0; x < 10; x++)
                {
                    estructura = dr.GetSchemaTable();
                    dataTable = new DataTable();

                    //generemos la estructura de columnas
                    for (int i = 0; i <= estructura.Rows.Count - 1; i++)
                    {
                        DataRow dataRow = estructura.Rows[i];
                        string columnName = dataRow["ColumnName"].ToString();
                        DataColumn column = new DataColumn(columnName, (Type)dataRow["DataType"]);
                        dataTable.Columns.Add(column);
                    }
                    dsGraficaDistribucion.Tables.Add(dataTable);

                    while (dr.Read())
                    {
                        DataRow dataRow = dataTable.NewRow();
                        for (int i = 0; i <= dr.FieldCount - 1; i++)
                        {
                            dataRow[i] = dr.GetValue(i);
                        }
                        dataTable.Rows.Add(dataRow);
                    }
                    if (!dr.NextResult())
                    {
                        break;
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
