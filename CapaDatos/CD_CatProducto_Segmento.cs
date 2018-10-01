using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using System.Data.SqlClient;
using System.Collections;

namespace CapaDatos
{
    public class CD_CatProducto_Segmento
    {

        public void InsertarSegmentoProducto(List<SegmentoProducto> list, string Conexion, ref int verificador)
        {
            CD_Datos capadatos = default(CD_Datos);
            SqlCommand sqlcmd = default(SqlCommand);
            try
            {
                capadatos = new CapaDatos.CD_Datos(Conexion);
                capadatos.StartTrans();

                string[] Parametros = { 
                                        "@Id_Emp",
                                        "@Id_Cd",
                                        "@Id_Seg", 
                                        "@Id_Prd",
                                        "@Pds_Contribucion",
                                        "@Accion"
                                      };
                object[] Valores = null;

                for (int x = 0; x < list.Count; x++)
                {

                    Valores = new object[] { 
                                        list[x].Id_Emp, 
                                        list[x].Id_Cd,
                                        list[x].id_Seg, 
                                        list[x].Id_Prd,
                                        list[x].Pds_Contribucion,
                                        x
                                   };
                    sqlcmd = capadatos.GenerarSqlCommand("spCatProductoSegmento_Insertar", ref verificador, Parametros, Valores);
                }


                capadatos.CommitTrans();
                capadatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                capadatos.RollBackTrans();
                throw ex;
            }
        }
        //public void ModificarSegmentoProducto(List<SegmentoProducto> list, string Conexion, ref int verificador)
        //{
        //     CD_Datos capadatos =  default(CD_Datos);
        //    SqlCommand sqlcmd = default(SqlCommand);
        //    try
        //    {
        //        capadatos.StartTrans();

        //        string[] Parametros = { 
        //                                "@Id_Emp",
        //                                "@Id_Cd",
        //                                "@Id_Seg", 
        //                                "@Id_Prd",
        //                                "@Pds_Contribucion",
        //                                "@Accion"
        //                              };


        //        object[] Valores = null;
        //        for (int x = 0; x < list.Count; x++)
        //        {
        //            capadatos = new CapaDatos.CD_Datos(Conexion);
        //            Valores = new object[] { 
        //                                list[x].Id_Emp, 
        //                                list[x].Id_Cd,
        //                                list[x].id_Seg, 
        //                                list[x].Id_Prd,
        //                                list[x].Pds_Contribucion,
        //                                x
        //                           };
        //            sqlcmd = capadatos.GenerarSqlCommand("spCatProductoSegmento_Insertar", ref verificador, Parametros, Valores);
        //            capadatos.LimpiarSqlcommand(ref sqlcmd);

        //        }
        //        capadatos.CommitTrans();
        //    }
        //    catch (Exception ex)
        //    {
        //        capadatos.RollBackTrans();
        //        throw ex;
        //    }
        //}

        public void ConsultaSegmentoProducto(SegmentoProducto segmentoproducto, string Conexion, ref ArrayList list)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = {
                                        "@Id_Emp",
                                        "@Id_Cd",
                                        "@Id_Prd"
                                      };
                object[] Valores = { 
                                        segmentoproducto.Id_Emp,
                                        segmentoproducto.Id_Cd,
                                        segmentoproducto.Id_Prd
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatProductoSegmento_Consulta", ref dr, Parametros, Valores);


                while (dr.Read())
                {

                    list.Add(dr.GetValue(dr.GetOrdinal("Id_Seg")).ToString());
                    segmentoproducto.Pds_Contribucion = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Pds_Contribucion")));

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
