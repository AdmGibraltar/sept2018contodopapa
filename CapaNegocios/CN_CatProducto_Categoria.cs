using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using CapaDatos;

namespace CapaNegocios
{
    public class CN_CatProducto_Categoria
    {
        public void ConsultaCategoriaProducto(CategoriaProducto CategoriaProducto, string Conexion, ref List<CategoriaProducto> List)
        {
            try
            {
                CD_CatProducto_Categoria claseCapaDatos = new CD_CatProducto_Categoria();
                claseCapaDatos.ConsultaCategoriaProducto(CategoriaProducto, Conexion, ref List);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertarCategoriaProducto(CategoriaProducto CategoriaProducto, string Conexion, ref int verificador)
        {
            try
            {
                CD_CatProducto_Categoria claseCapaDatos = new CD_CatProducto_Categoria();
                claseCapaDatos.InsertarCategoriaProducto(CategoriaProducto, Conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarCategoriaProducto(CategoriaProducto CategoriaProducto, string Conexion, ref int verificador)
        {
            try
            {
                CD_CatProducto_Categoria claseCapaDatos = new CD_CatProducto_Categoria();
                claseCapaDatos.ModificarCategoriaProducto(CategoriaProducto, Conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
