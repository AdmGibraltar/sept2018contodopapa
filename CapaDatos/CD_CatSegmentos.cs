using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using System.Data.SqlClient;
using CapaModelo;

namespace CapaDatos
{
    public class CD_CatSegmentos
    {
        public void ConsultaSegmentos(int Id_Emp, string Conexion, ref List<Segmentos> List)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp" };
                object[] Valores = { Id_Emp };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatSegmentos_Consulta", ref dr, Parametros, Valores);

                Segmentos segmento;
                while (dr.Read())
                {
                    segmento = new Segmentos();
                    segmento.Id_Emp = (int)dr.GetValue(dr.GetOrdinal("Id_Emp"));
                    segmento.Id_Seg = (int)dr.GetValue(dr.GetOrdinal("Id_Seg"));
                    segmento.Descripcion = (string)dr.GetValue(dr.GetOrdinal("Seg_Descripcion"));
                    segmento.Id_UEN = (int)dr.GetValue(dr.GetOrdinal("Id_Uen"));
                    segmento.Unidades = (string)dr.GetValue(dr.GetOrdinal("Seg_Unidades"));
                    segmento.Valor = (double)dr.GetValue(dr.GetOrdinal("Seg_ValUniDim"));
                    segmento.Estatus = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Seg_Activo")));
                    segmento.Seg_IdXUen = (string)dr.GetValue(dr.GetOrdinal("Seg_IdXUen"));
                    if (Convert.ToBoolean(segmento.Estatus))
                    {
                        segmento.EstatusStr = "Activo";
                    }
                    else
                    {
                        segmento.EstatusStr = "Inactivo";
                    }
                    List.Add(segmento);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertarSegmentos(Segmentos segmentos, string Conexion, ref int verificador)
        {
            try
            {

                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = { 
	                                    "@Id_Emp",
                                        "@Id_Seg",
	                                    "@Seg_Descripcion", 
	                                    "@Id_Uen", 
	                                    "@Seg_Unidades",
                                        "@Seg_ValUniDim",
                                        "@Id_U",
                                        "@Seg_Activo",
                                        "@Seg_IdXUen"
                                      };
                object[] Valores = { 
                                        segmentos.Id_Emp,
                                        segmentos.Id_Seg,
                                        segmentos.Descripcion,
                                        segmentos.Id_UEN,
                                        segmentos.Unidades,
                                        segmentos.Valor,
                                        segmentos.Id_U,
                                        segmentos.Estatus,
                                        segmentos.Seg_IdXUen
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatSegmentos_Insertar", ref verificador, Parametros, Valores);
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarSegmentos(Segmentos segmentos, string Conexion, ref int verificador)
        {
            try
            {

                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = { 
	                                    "@Id_Emp",
                                        "@Id_Seg",
                                        "@Id_Seg_Ant",
	                                    "@Seg_Descripcion", 
	                                    "@Id_Uen", 
	                                    "@Seg_Unidades",
                                        "@Seg_ValUniDim",
                                        "@Id_U",
                                        "@Seg_Activo",
                                        "@Seg_IdXUen"
                                      };
                object[] Valores = { 
                                        segmentos.Id_Emp,
                                        segmentos.Id_Seg,
                                        segmentos.Id_Seg_Ant,
                                        segmentos.Descripcion,
                                        segmentos.Id_UEN,
                                        segmentos.Unidades,
                                        segmentos.Valor,
                                        segmentos.Id_U,
                                        segmentos.Estatus,
                                        segmentos.Seg_IdXUen
                                   };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatSegmentos_Modificar", ref verificador, Parametros, Valores);
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaSegmentos(int Id_Emp, int Id_Seg, string Conexion, ref List<Segmentos> List)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Id_seg" };
                object[] Valores = { Id_Emp, Id_Seg };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatSegmentos_Consulta", ref dr, Parametros, Valores);

                Segmentos segmento;
                while (dr.Read())
                {
                    segmento = new Segmentos();
                    segmento.Id_Emp = (int)dr.GetValue(dr.GetOrdinal("Id_Emp"));
                    segmento.Id_Seg = (int)dr.GetValue(dr.GetOrdinal("Id_Seg"));
                    segmento.Descripcion = (string)dr.GetValue(dr.GetOrdinal("Seg_Descripcion"));
                    segmento.Id_UEN = (int)dr.GetValue(dr.GetOrdinal("Id_Uen"));
                    segmento.Unidades = (string)dr.GetValue(dr.GetOrdinal("Seg_Unidades"));
                    segmento.Valor = (double)dr.GetValue(dr.GetOrdinal("Seg_ValUniDim"));
                    segmento.Estatus = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Seg_Activo")));
                    segmento.Seg_IdXUen = (string)dr.GetValue(dr.GetOrdinal("Seg_IdXUen"));
                    if (Convert.ToBoolean(segmento.Estatus))
                    {
                        segmento.EstatusStr = "Activo";
                    }
                    else
                    {
                        segmento.EstatusStr = "Inactivo";
                    }
                    List.Add(segmento);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaSegmentoTer(int Id_Emp, int Id_Cd, int Id_Ter, string Conexion, ref Segmentos segmento)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_Ter" };
                object[] Valores = { Id_Emp, Id_Cd, Id_Ter };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatSegmentoTer_Consulta", ref dr, Parametros, Valores);

                if (dr.Read())
                {
                    segmento = new Segmentos();
                    segmento.Id_Emp = (int)dr.GetValue(dr.GetOrdinal("Id_Emp"));
                    segmento.Id_Seg = (int)dr.GetValue(dr.GetOrdinal("Id_Seg"));
                    segmento.Descripcion = (string)dr.GetValue(dr.GetOrdinal("Seg_Descripcion"));
                    segmento.Id_UEN = (int)dr.GetValue(dr.GetOrdinal("Id_Uen"));
                    segmento.Unidades = (string)dr.GetValue(dr.GetOrdinal("Seg_Unidades"));
                    segmento.Valor = (double)dr.GetValue(dr.GetOrdinal("Seg_ValUniDim"));
                    segmento.Estatus = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Seg_Activo")));
                    segmento.Seg_IdXUen = (string)dr.GetValue(dr.GetOrdinal("Seg_IdXUen"));
                    if (Convert.ToBoolean(segmento.Estatus))
                    {
                        segmento.EstatusStr = "Activo";
                    }
                    else
                    {
                        segmento.EstatusStr = "Inactivo";
                    }
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ConsultaSegmento_Usuario(ref List<Segmentos> list, int Id_Emp, int? Id_U, string Conexion)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Id_U" };
                object[] Valores = { Id_Emp, Id_U };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatSegmentosUsuario_Consultar", ref dr, Parametros, Valores);

                Segmentos segmento;
                while (dr.Read())
                {
                    segmento = new Segmentos();
                    segmento.Id_Seg = (int)dr.GetValue(dr.GetOrdinal("Id_Seg"));
                    segmento.Descripcion = (string)dr.GetValue(dr.GetOrdinal("Seg_Descripcion"));
                    segmento.Id_U = dr.IsDBNull(dr.GetOrdinal("Id_U")) ? (int?)null : dr.GetInt32(dr.GetOrdinal("Id_U"));
                    segmento.Seg_IdXUen = (string)dr.GetValue(dr.GetOrdinal("Seg_IdXUen"));
                    list.Add(segmento);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<CatSegmento> ConsultarSegmentosPorUen(int idEmp, int idUen, string conexion)
        {
            IEnumerable<CatSegmento> result = null;
            using (sianwebmty_gEntities ctx = new sianwebmty_gEntities(conexion))
            {
                //var res = ctx.spCatSegmento_ConsultarPorUEN(idEmp, idUen).ToList();
                var res = from s in ctx.CatSegmentoes
                          where s.Id_Uen == idUen && s.Id_Emp == idEmp
                          select s;
                result = res;
            }
            return result;
        }

        /// <summary>
        /// Regresa el resultado de la consulta al repositorio CatSegmento, condicionado por la unidad de negocio
        /// </summary>
        /// <param name="idEmp">Identificador de la empresa</param>
        /// <param name="idUen">Identificador de la unidad de negocio</param>
        /// <param name="icdCtx">Contexto de conexión a la fuente de datos</param>
        /// <returns>IEnumerable[CatSegmento]</returns>
        public IEnumerable<CatSegmento> ConsultarSegmentosPorUen(int idEmp, int idUen, ICD_Contexto icdCtx)
        {
            IEnumerable<CatSegmento> result = null;
            var ctx = ((ICD_Contexto<sianwebmty_gEntities>)icdCtx).Contexto;
            var res = from s in ctx.CatSegmentoes
                      where s.Id_Uen == idUen && s.Id_Emp == idEmp
                      select s;
            //var res = ctx.spCatSegmento_ConsultarPorUEN(idEmp, idUen).ToList();
            result = res;
            return result;
        }
    }
}
