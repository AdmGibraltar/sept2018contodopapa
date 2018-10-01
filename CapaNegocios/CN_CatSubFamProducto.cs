using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CapaEntidad;
using CapaDatos;

namespace CapaNegocios
{
    public class CN_CatSubFamProducto
    {
        public void ConsultaSubFamProducto(SubFamProducto subFamProducto, string Conexion, int id_Emp, ref List<SubFamProducto> List)
        {
            try
            {
                CD_CatSubFamProducto claseCapaDatos = new CD_CatSubFamProducto();
                claseCapaDatos.ConsultaSubFamProducto(subFamProducto, Conexion, id_Emp, ref List);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertarSubFamProducto(SubFamProducto subFamProducto, string Conexion, ref int verificador)
        {
            try
            {
                CD_CatSubFamProducto claseCapaDatos = new CD_CatSubFamProducto();
                claseCapaDatos.InsertarSubFamProducto(subFamProducto, Conexion, ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarSubFamProducto(SubFamProducto subFamProducto, string Conexion, ref int verificador)
        {
            try
            {
                CD_CatSubFamProducto claseCapaDatos = new CD_CatSubFamProducto();
                claseCapaDatos.ModificarSubFamProducto(subFamProducto, Conexion, ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
