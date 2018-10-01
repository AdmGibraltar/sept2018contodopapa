using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using System.Data.SqlClient;
using CapaModelo;

namespace CapaDatos
{
    public class CD_CatArea
    {

        public void Lista(Area area, string Conexion, ref List<Area> List)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp" ,"@Id_Seg" };
                object[] Valores = { area.Id_Emp , area.Id_Seg };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatArea_Lista", ref dr, Parametros, Valores);


                while (dr.Read())
                {
                    area = new Area();
                    area.Id_Area = (int)dr.GetValue(dr.GetOrdinal("Id_Area"));
                    area.Area_Descripcion = (string)dr.GetValue(dr.GetOrdinal("Area_Descripcion"));
                    area.Id_Seg = (int)dr.GetValue(dr.GetOrdinal("Id_Seg"));
                    area.Id_Uen = (int)dr.GetValue(dr.GetOrdinal("Id_Uen"));
                    area.Area_Potencial = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Area_Potencial")));
                    area.Estatus = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Area_Activo")));
                    if (Convert.ToBoolean(area.Estatus))
                    {
                        area.EstatusStr = "Activo";
                    }
                    else
                    {
                        area.EstatusStr = "Inactivo";
                    }
                    List.Add(area);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Insertar(Area area, string Conexion, ref int verificador)
        {
            try
            {

                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { 
                                          "@Id_Emp", 
                                          "@Id_Area", 
                                          "@Area_Descripcion", 
                                          "@Id_Seg", 
                                          "@Area_Potencial", 
                                          "@Estatus" 
                                      };
                object[] Valores = { 
                                       area.Id_Emp, 
                                       area.Id_Area,
                                       area.Area_Descripcion,
                                       area.Id_Seg,
                                       area.Area_Potencial,
                                       area.Estatus
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatArea_Insertar", ref verificador, Parametros, Valores);

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Modificar(Area area, string Conexion, ref int verificador)
        {
            try
            {

                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { 
                                          "@Id_Emp", 
                                          "@Id_Area", 
                                          "@Area_Descripcion", 
                                          "@Id_Seg", 
                                          "@Area_Potencial", 
                                          "@Estatus" 
                                      };
                object[] Valores = { 
                                       area.Id_Emp, 
                                       area.Id_Area,
                                       area.Area_Descripcion,
                                       area.Id_Seg,
                                       area.Area_Potencial,
                                       area.Estatus
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatArea_Modificar", ref verificador, Parametros, Valores);

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Borrar(Area area, string Conexion, ref int verificador)
        {
            try
            {

                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { 
                                          "@Id_Emp", 
                                          "@Id_Area"
                                      };
                object[] Valores = { 
                                       area.Id_Emp, 
                                       area.Id_Area
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatArea_Eliminar", ref verificador, Parametros, Valores);

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Regresa el resultado de la consulta sobre el repositorio CatArea, condicionado por el identificador del segmento
        /// </summary>
        /// <param name="idEmp">Identificador de la empresa</param>
        /// <param name="idUen">Identificador del centro de negocio</param>
        /// <param name="idSegmento">Identificador del segmento</param>
        /// <param name="icdCtx">Contexto de conexión a la fuente de datos</param>
        /// <returns>IEnumerable[CatArea]</returns>
        public IEnumerable<CatArea> Consultar(int idEmp, int idUen, int idSegmento, ICD_Contexto icdCtx)
        {
            var ctx = ((ICD_Contexto<sianwebmty_gEntities>)icdCtx).Contexto;
            var resultado = from a in ctx.CatAreas
                            where a.Id_Emp==idEmp && a.Id_Seg==idSegmento
                            select a;
            return resultado;
        }
    }
}
