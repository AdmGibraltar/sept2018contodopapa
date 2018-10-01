using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using CapaEntidad;
using CapaModelo;

namespace CapaDatos
{
   public class CD_CatSolucion
    {
       public void Lista(Solucion solucion, string Conexion, ref List<Solucion> List)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp"};
                object[] Valores = { solucion.Id_Emp };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatSolucion_Lista", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    solucion = new Solucion();
                    solucion.Id_Sol = (int)dr.GetValue(dr.GetOrdinal("Id_Sol"));
                    solucion.Id_Area = (int)dr.GetValue(dr.GetOrdinal("Id_Area"));
                    solucion.Sol_Descripcion = (string)dr.GetValue(dr.GetOrdinal("Sol_Descripcion"));
                    solucion.Id_Seg = (int)dr.GetValue(dr.GetOrdinal("Id_Seg"));
                    solucion.Id_Uen = (int)dr.GetValue(dr.GetOrdinal("Id_Uen"));
                    solucion.Sol_Potencial = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Sol_Potencial")));
                    solucion.Estatus = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Sol_Activo")));
                    if (Convert.ToBoolean(solucion.Estatus))
                    {
                        solucion.EstatusStr = "Activo";
                    }
                    else
                    {
                        solucion.EstatusStr = "Inactivo";
                    }
                    List.Add(solucion);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

       public void Insertar(Solucion solucion, string Conexion, ref int verificador)
        {
            try
            {

                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { 
                                          "@Id_Emp", 
                                          "@Id_Sol",
                                          "@Id_Area", 
                                          "@Sol_Descripcion", 
                                          "@Sol_Potencial", 
                                          "@Estatus" 
                                      };
                object[] Valores = { 
                                       solucion.Id_Emp,
                                       solucion.Id_Sol,
                                       solucion.Id_Area,
                                       solucion.Sol_Descripcion,
                                       solucion.Sol_Potencial,
                                       solucion.Estatus
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatSolucion_Insertar", ref verificador, Parametros, Valores);

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

       public void Modificar(Solucion solucion, string Conexion, ref int verificador)
        {
            try
            {

                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { 
                                          "@Id_Emp", 
                                          "@Id_Sol",
                                          "@Id_Area", 
                                          "@Sol_Descripcion", 
                                          "@Sol_Potencial", 
                                          "@Estatus" 
                                      };
                object[] Valores = { 
                                       solucion.Id_Emp, 
                                       solucion.Id_Sol,
                                       solucion.Id_Area,
                                       solucion.Sol_Descripcion,
                                       solucion.Sol_Potencial,
                                       solucion.Estatus
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatSolucion_Modificar", ref verificador, Parametros, Valores);

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

       /// <summary>
       /// Regresa el resultado de la consulta al repositorio CatSolucion, condicionado por el área
       /// </summary>
       /// <param name="idEmp">Identificador de empresa</param>
       /// <param name="idArea">Identificador de área</param>
       /// <param name="conexion">Cadena de conexión a la fuente de datos</param>
       /// <returns>IEnumerable[CatSolucion]</returns>
       public IEnumerable<CatSolucion> ConsultarPorEmpresaYArea(int idEmp, int idArea, string conexion)
       {
           IEnumerable<CatSolucion> resultado = null;
           using (sianwebmty_gEntities ctx = new sianwebmty_gEntities(conexion))
           {
               var res = (from s in ctx.CatSolucions
                          where s.Id_Emp == idEmp && s.Id_Area == idArea && s.Sol_Activo == true
                          select s).ToList();
               resultado = res;
           }
           return resultado;
       }

       /// <summary>
       /// Regresa el resultado de la consulta al repositorio CatSolucion, condicionado por el área
       /// </summary>
       /// <param name="idEmp">Identificador de empresa</param>
       /// <param name="idArea">Identificador de área</param>
       /// <param name="icdCtx">Contexto de conexión a la fuente de datos</param>
       /// <returns>IEnumerable[CatSolucion]</returns>
       public IEnumerable<CatSolucion> ConsultarPorEmpresaYArea(int idEmp, int idArea, ICD_Contexto icdCtx)
       {
           IEnumerable<CatSolucion> resultado = null;
           var ctx = ((ICD_Contexto<sianwebmty_gEntities>)icdCtx).Contexto;
           var res = (from s in ctx.CatSolucions
                      where s.Id_Emp == idEmp && s.Id_Area == idArea && s.Sol_Activo == true
                      select s).ToList();
           resultado = res;
           return resultado;
       }
    }
}
