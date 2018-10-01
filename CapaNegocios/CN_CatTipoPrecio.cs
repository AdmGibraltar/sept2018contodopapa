using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CapaEntidad;
using CapaDatos;

namespace CapaNegocios
{
    public class CN_CatTipoPrecio
    {
        public void ConsultaTipoPrecio(TipoPrecio tipoPrecio, string Conexion, int id_Emp, ref List<TipoPrecio> List)
        {
            try
            {
                CD_CatTipoPrecio claseCapaDatos = new CD_CatTipoPrecio();
                claseCapaDatos.ConsultaTipoPrecio(tipoPrecio, Conexion, id_Emp, ref List);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertarTipoPrecio(TipoPrecio tipoPrecio, string Conexion, ref int verificador)
        {
            try
            {
                CD_CatTipoPrecio claseCapaDatos = new CD_CatTipoPrecio();
                claseCapaDatos.InsertarTipoPrecio(tipoPrecio, Conexion, ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarTipoPrecio(TipoPrecio tipoPrecio, string Conexion, ref int verificador)
        {
            try
            {
                CD_CatTipoPrecio claseCapaDatos = new CD_CatTipoPrecio();
                claseCapaDatos.ModificarTipoPrecio(tipoPrecio, Conexion, ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
