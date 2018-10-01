using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using CapaDatos;

namespace CapaNegocios
{
    public class CN_CatRegion
    {
        public void ConsultaRegion(ref Region region, int id_region,string reg_descripcion, CapaEntidad.Sesion sesion, ref List<Region> list)
        {
            try
            {
                CD_CatRegion cd_catRegion = new CD_CatRegion();
                cd_catRegion.ConsultaRegion(ref region,id_region,reg_descripcion, sesion, ref list);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GuardarRegion(ref Region region_nueva, ref Region region_vieja, CapaEntidad.Sesion sesion, ref int verificador, bool actualizar)
        {
            try
            {
                CD_CatRegion cd_catRegion = new CD_CatRegion();
                cd_catRegion.GuardarRegion(ref region_nueva, ref region_vieja, sesion, ref verificador, actualizar);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaRegionConsecutivo( ref Region region, CapaEntidad.Sesion sesion)
        {
            try
            {
                CD_CatRegion cd_catRegion = new CD_CatRegion();
                cd_catRegion.ConsultaRegionConsecutivo(ref region, sesion);
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }

    }
}
