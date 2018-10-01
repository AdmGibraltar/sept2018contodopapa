using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using CapaEntidad;
using CapaDatos;

namespace CapaNegocios
{
    public class CN_CatTipoProducto
    {
        public void ConsultaTipoProducto(TipoProducto tipoProducto, string Conexion, int id_Emp, ref List<TipoProducto> List)
        {
            try
            {
                CD_CatTipoProducto claseCapaDatos = new CD_CatTipoProducto();
                claseCapaDatos.ConsultaTipoProducto(tipoProducto, Conexion, id_Emp, ref List);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertarTipoProducto(TipoProducto tipoProducto, string Conexion, ref int verificador)
        {
            try
            {
                CD_CatTipoProducto claseCapaDatos = new CD_CatTipoProducto();
                claseCapaDatos.InsertarTipoProducto(tipoProducto, Conexion, ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarTipoProducto(TipoProducto tipoProducto, string Conexion, ref int verificador)
        {
            try
            {
                CD_CatTipoProducto claseCapaDatos = new CD_CatTipoProducto();
                claseCapaDatos.ModificarTipoProducto(tipoProducto, Conexion, ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
