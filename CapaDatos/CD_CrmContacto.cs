using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using CapaEntidad;

namespace CapaDatos
{
    public class CD_CrmContacto
    {
        public void Insertar(CapaEntidad.Contacto contacto, ref int verificador, string Conexion)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = { 
                                        "@Id_Emp",
                                        "@Id_Cd",
                                        "@Id_Cte", 
                                        "@Id_Pos",
                                        "@Con_Nombre",
                                        "@Con_Apellido",
                                        "@Con_Correo",
                                        "@Con_Telefono1",
                                        "@Con_Celular",
                                        "@Con_Titulo",
                                        "@Con_Telefono2",
                                        "@Con_JefeInmediato",
                                        "@Con_Departamento",
                                        "@Con_Calle",
                                        "@Con_Colonia",
                                        "@Con_Municipio",
                                        "@Con_Estado",
                                        "@Con_CodigoPostal", 
                                        "@Con_FechaNac", 
                                        "@Con_Asistente", 
                                        "@Con_TelefonoAsistente", 
                                        "@Con_Comentarios", 
                                        "@Con_Extension",
                                        "@Id_Est",
                                        "@Con_OtraPosicion",
                                        "@Id_Seg" 
                                      };
                object[] Valores = { 
                                        contacto.Id_Emp,
                                        contacto.Id_Cd,
                                        contacto.Id_Cte,
                                        contacto.Id_Pos,
                                        contacto.Con_Nombre,
                                        contacto.Con_Apellido,
                                        contacto.Con_Correo,
                                        contacto.Con_Telefono1,
                                        contacto.Con_Celular,
                                        contacto.Con_Titulo,
                                        contacto.Con_Telefono2,
                                        contacto.Con_JefeInmediato,
                                        contacto.Con_Departamento,
                                        contacto.Con_Calle,
                                        contacto.Con_Colonia,
                                        contacto.Con_Municipio,
                                        contacto.Con_Estado,
                                        contacto.Con_CodigoPostal,
                                        contacto.Con_FechaNac,
                                        contacto.Con_Asistente,
                                        contacto.Con_TelefonoAsistente,
                                        contacto.Con_Comentarios,
                                        contacto.Con_Extension,
                                        contacto.Id_Est,
                                        contacto.Con_OtraPosicion,
                                        contacto.Id_Seg
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCRMContacto_Insertar", ref verificador, Parametros, Valores);

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Modificar(CapaEntidad.Contacto contacto, ref int verificador, string Conexion)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = { 
                                        "@Id_Emp",
                                        "@Id_Cd",
                                        "@Id_Cte", 
                                        "@Id_Pos",
                                        "@Con_Nombre",
                                        "@Con_Apellido",
                                        "@Con_Correo",
                                        "@Con_Telefono1",
                                        "@Con_Celular",
                                        "@Con_Titulo",
                                        "@Con_Telefono2",
                                        "@Con_JefeInmediato",
                                        "@Con_Departamento",
                                        "@Con_Calle",
                                        "@Con_Colonia",
                                        "@Con_Municipio",
                                        "@Con_Estado",
                                        "@Con_CodigoPostal", 
                                        "@Con_FechaNac", 
                                        "@Con_Asistente", 
                                        "@Con_TelefonoAsistente", 
                                        "@Con_Comentarios", 
                                        "@Con_Extension",
                                        "@Id_Est",
                                        "@Con_OtraPosicion",
                                        "@Id_Seg",
                                        "@Id_Con"
                                      };
                object[] Valores = { 
                                        contacto.Id_Emp,
                                        contacto.Id_Cd,
                                        contacto.Id_Cte,
                                        contacto.Id_Pos,
                                        contacto.Con_Nombre,
                                        contacto.Con_Apellido,
                                        contacto.Con_Correo,
                                        contacto.Con_Telefono1,
                                        contacto.Con_Celular,
                                        contacto.Con_Titulo,
                                        contacto.Con_Telefono2,
                                        contacto.Con_JefeInmediato,
                                        contacto.Con_Departamento,
                                        contacto.Con_Calle,
                                        contacto.Con_Colonia,
                                        contacto.Con_Municipio,
                                        contacto.Con_Estado,
                                        contacto.Con_CodigoPostal,
                                        contacto.Con_FechaNac,
                                        contacto.Con_Asistente,
                                        contacto.Con_TelefonoAsistente,
                                        contacto.Con_Comentarios,
                                        contacto.Con_Extension,
                                        contacto.Id_Est,
                                        contacto.Con_OtraPosicion,
                                        contacto.Id_Seg,
                                        contacto.Id_Con
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCRMContacto_Modificar", ref verificador, Parametros, Valores);

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Consulta(Contacto contacto, ref DataSet dsContacto, string Conexion)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_Con", "@Id_Cte" };
                object[] Valores = { contacto.Id_Emp, contacto.Id_Cd, contacto.Id_Con, contacto.Id_Cte };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCRMContacto_Consultar", ref dr, Parametros, Valores);

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
                    dsContacto.Tables.Add(dataTable);

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
