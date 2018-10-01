using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CapaEntidad;
using CapaDatos;

namespace CapaNegocios
{
    public class CN_CatFamProducto
    {
        public void ConsultaFamProducto(FamProducto famProducto, string Conexion, int id_Emp, ref List<FamProducto> List)
        {
            try
            {
                CD_CatFamProducto claseCapaDatos = new CD_CatFamProducto();
                claseCapaDatos.ConsultaFamProducto(famProducto, Conexion, id_Emp, ref List);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertarFamProducto(FamProducto famProducto, string Conexion, ref int verificador)
        {
            try
            {
                CD_CatFamProducto claseCapaDatos = new CD_CatFamProducto();
                claseCapaDatos.InsertarFamProducto(famProducto, Conexion, ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarFamProducto(FamProducto famProducto, string Conexion, ref int verificador)
        {
            try
            {
                CD_CatFamProducto claseCapaDatos = new CD_CatFamProducto();
                claseCapaDatos.ModificarFamProducto(famProducto, Conexion, ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
