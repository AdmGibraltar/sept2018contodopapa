using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CapaEntidad;
using CapaDatos;
using System.Data;
namespace CapaNegocios
{
    public class CN_CapValuacionProyectoCtasMarg
    {

        public void ConsultaValuacionProyecto_Buscar(ValuacionProyecto valuacionProyecto, ref List<ValuacionProyecto> listaValuacionProyecto, string Conexion
            , int? Id_U, string Nombre, int? Id_Cte_inicio, int? Id_Cte_fin, DateTime? Vap_Fecha_inicio, DateTime? Vap_Fecha_fin)
        {
            try
            {
                new CD_CapValuacionProyectoCtasMarg().ConsultaValuacionProyecto_Buscar(valuacionProyecto, ref listaValuacionProyecto, Conexion
                    , Id_U
                    , Nombre
                    , Id_Cte_inicio
                    , Id_Cte_fin
                    , Vap_Fecha_inicio
                    , Vap_Fecha_fin);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarValuacionProyecto(ref ValuacionProyecto valuacionProyecto, string Conexion)
        {
            try
            {
                new CD_CapValuacionProyectoCtasMarg().ConsultarValuacionProyecto(ref valuacionProyecto, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertarValuacionProyecto(ref ValuacionProyecto valuacionProyecto, ValuacionParametrosCtasMarg vp, string Conexion, ref int verificador)
        {
            try
            {
                new CD_CapValuacionProyectoCtasMarg().InsertarValuacionProyecto(ref valuacionProyecto, vp, Conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarValuacionProyecto(ref ValuacionProyecto valuacionProyecto, ValuacionParametrosCtasMarg vp, string Conexion, ref int verificador)
        {
            try
            {
                new CD_CapValuacionProyectoCtasMarg().ModificarValuacionProyecto(ref valuacionProyecto, vp, Conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void consultarCondiciones(ref ValuacionParametrosCtasMarg vp, string Conexion, ref int verificador)
        {
            try
            {
                CD_CapValuacionProyectoCtasMarg claseCapaDatos = new CD_CapValuacionProyectoCtasMarg();
                claseCapaDatos.consultarParametros(ref vp, Conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void consultarCondicionesCentro(ref ValuacionParametrosCtasMarg vp, string Conexion, ref int verificador)
        {
            try
            {
                CD_CapValuacionProyectoCtasMarg claseCapaDatos = new CD_CapValuacionProyectoCtasMarg();
                claseCapaDatos.consultarCondicionesCentro(ref vp, Conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
