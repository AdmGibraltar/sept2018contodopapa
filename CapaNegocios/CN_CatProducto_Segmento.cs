using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using CapaDatos;
using System.Collections;

namespace CapaNegocios
{
    public class CN_CatProducto_Segmento
    {
         

        public void InsertarSegmentoProducto(List<SegmentoProducto> list, string Conexion, ref int verificador)
        {
            try
            {
                CD_CatProducto_Segmento claseCapaDatos = new CD_CatProducto_Segmento();
                claseCapaDatos.InsertarSegmentoProducto(list, Conexion, ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public void ModificarSegmentoProducto(List<SegmentoProducto> list, string Conexion, ref int verificador)
        //{
        //    try
        //    {
        //        CD_CatProducto_Segmento claseCapaDatos = new CD_CatProducto_Segmento();
        //        claseCapaDatos.ModificarSegmentoProducto(list, Conexion, ref verificador);

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}



        public void ConsultaSegmentoProducto(ref SegmentoProducto segmentoproducto, string Conexion, ref ArrayList list)
        {
            try
            {
                CD_CatProducto_Segmento claseCapaDatos = new CD_CatProducto_Segmento();
                claseCapaDatos.ConsultaSegmentoProducto(segmentoproducto, Conexion, ref list);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
