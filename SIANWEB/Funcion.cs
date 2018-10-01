using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;


namespace SIANWEB
{
    public class Funcion
    {
        
        /// <summary>
        /// Clase genérica para convertir una Lista Genérica de elementos en
        /// un objeto DataTable
        /// </summary>
        /// <typeparam name="T">Tipo de datos de los elementos de la Lista. 
        /// Debe ser una clase con un constructor sin parámetros. ver referencia de clases genericas</typeparam>

        public static class Convertidor<T> where T : new()
        {

            /// <summary>
            /// 
            /// </summary>
            /// <param name="items"></param>
            /// <returns></returns>

            public static DataTable ListaToDatatable(List<T> items)
            {

                // Instancia del objeto a devolver

                DataTable dataTable = new DataTable();

                // Información del tipo de datos de los elementos del List

                Type itemsType = typeof(T);

                // Recorremos las propiedades para crear las columnas del datatable

                foreach (PropertyInfo prop in itemsType.GetProperties())
                {

                    // Crearmos y agregamos una columna por cada propiedad de la entidad

                    DataColumn column = new DataColumn(prop.Name);

                    if (prop.PropertyType.FullName == "System.Nullable`1[[System.Double, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]")
                    {
                        column.DataType = System.Type.GetType("System.Double");
                    }
                    else if (prop.PropertyType.FullName == "System.Nullable`1[[System.DateTime, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]")
                    {
                        column.DataType = System.Type.GetType("System.DateTime");
                    }
                    else if (prop.PropertyType.FullName == "System.Nullable`1[[System.Int32, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]")
                    {
                        column.DataType = System.Type.GetType("System.Int32");

                    }
                    else if (prop.PropertyType.FullName == "System.Nullable`1[[System.Boolean, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]")
                    {
                        column.DataType = System.Type.GetType("System.Boolean");

                    }
                    else
                    {
                        column.DataType = prop.PropertyType;
                    }


                    dataTable.Columns.Add(column);

                }



                int j;

                // ahora recorremos la colección para guardar los datos
                // en el DataTable

                foreach (T item in items)
                {

                    j = 0;

                    object[] newRow = new object[dataTable.Columns.Count];

                    // Volvemos a recorrer las propiedades de cada item para
                    // obtener su valor guardarlo en la fila de la tabla

                    foreach (PropertyInfo prop in itemsType.GetProperties())
                    {

                        newRow[j] = prop.GetValue(item, null);

                        j++;

                    }

                    dataTable.Rows.Add(newRow);

                }

                // Devolver el objeto creado
                return dataTable;

            }



            /// <summary>
            /// Métod encargado de recorrer el DataTable y asignar propiedades al objeto
            /// </summary>
            /// <returns>Una lista de objetos T</returns>

            public static List<T> DataTableToLista(DataTable tabla)
            {

                List<T> lista = new List<T>();

                T elemento;

                for (int i = 0; i < tabla.Rows.Count; i++)
                {

                    // Información del tipo de datos de los elementos del List

                    Type itemsType = typeof(T);

                    elemento = new T();



                    foreach (PropertyInfo prop in itemsType.GetProperties())
                    {

                        //Establecemos cada una de las propiedades

                        prop.SetValue(elemento, ValorDefault(tabla.Rows[i][prop.Name]), null);

                    }

                    lista.Add(elemento);



                }

                return lista;

            }



            //Esta parte hay que buscar hacerla de una mejor modo
            /// <summary>
            /// Método que se encarga de validar los DBNull y convertirlos en una cadena vacia
            /// </summary>
            /// <returns>El mismo objeto de entrada validado</returns>

            private static object ValorDefault(object objeto)
            {

                if (objeto == System.DBNull.Value)

                    return "";

                else

                    return objeto;

            }

        }

        public void ExportarExcel(String nombreArchivo, String tabla)
        {
            try
            {
                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.Buffer = true;
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + nombreArchivo + ".xls");
                HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;
                HttpContext.Current.Response.ContentType = "application/vnd.ms-excel"; //Excel
                System.IO.StringWriter sw = new System.IO.StringWriter();
                sw.WriteLine("<html xmlns='http://www.w3.org/1999/xhtml'>");
                sw.WriteLine("<head>");
                sw.WriteLine("<meta http-equiv='content-type' content='text/html; charset=UTF-8' />");
                sw.WriteLine("<title>");
                sw.WriteLine("Page-");
                sw.WriteLine(Guid.NewGuid().ToString());
                sw.WriteLine("</title>");
                sw.WriteLine("</head>");
                sw.WriteLine("<body>");
                sw.Write(tabla);
                sw.WriteLine("</body>");
                sw.WriteLine("</html>");
                HttpContext.Current.Response.Output.Write(sw.ToString());
                HttpContext.Current.Response.Flush();
                HttpContext.Current.Response.End();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



    }
}