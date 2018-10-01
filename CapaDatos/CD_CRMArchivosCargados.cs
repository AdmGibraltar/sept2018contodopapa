using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using CapaModelo;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Data;

namespace CapaDatos
{
    public class CD_CRMArchivosCargados
    {

        public int CRMArchivosCargados_Insert(            
           int Id_Emp, int Id_Cd, string NombreArchivo,string Hash, int IdDocumento, 
            int IdDocTipo, int Id_U, string conexion)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(conexion);
            SqlDataReader dr = null;
            int AfecteRows = 0;

            try
            {

                string[] Parametros = {  
                                         "@Id_Emp",
                                         "@Id_Cd",
                                         "@NombreArchivo",
                                         "@Hash",
                                         "@IdDocumento",
                                         "@IdDocTipo",
                                         "@Id_U"
                                      };


                object[] Valores = {   
                                       Id_Emp,
                                       Id_Cd,
                                       NombreArchivo,
                                       Hash,
                                       IdDocumento,
                                       IdDocTipo,
                                       Id_U
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("sp_CRMArchivosCargados_Insert", ref dr, Parametros, Valores);

                if (dr.Read())
                {
                    AfecteRows = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("AfectedRows")));
                }

                dr.Close();

            }
            catch (Exception ex)
            {
                //throw ex;
                AfecteRows = -1;
            }
            return AfecteRows;
        }


    }

}
