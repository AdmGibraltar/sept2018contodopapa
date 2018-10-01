using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CapaEntidad;
using CapaDatos;

namespace CapaNegocios
{
    public class CN_CatTipoCosto
    {
        public void ConsultaTipoCosto(TipoCosto tipoCosto, string Conexion, int id_Emp, ref List<TipoCosto> List)
        {
            try
            {
                CD_CatTipoCosto claseCapaDatos = new CD_CatTipoCosto();
                claseCapaDatos.ConsultaTipoCosto(tipoCosto, Conexion, id_Emp, ref List);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertarTipoCosto(TipoCosto tipoCosto, string Conexion, ref int verificador)
        {
            try
            {
                CD_CatTipoCosto claseCapaDatos = new CD_CatTipoCosto();
                claseCapaDatos.InsertarTipoCosto(tipoCosto, Conexion, ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarTipoCosto(TipoCosto tipoCosto, string Conexion, ref int verificador)
        {
            try
            {
                CD_CatTipoCosto claseCapaDatos = new CD_CatTipoCosto();
                claseCapaDatos.ModificarTipoCosto(tipoCosto, Conexion, ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
