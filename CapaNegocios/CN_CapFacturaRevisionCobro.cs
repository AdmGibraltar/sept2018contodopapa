using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CapaEntidad;
using CapaDatos;

namespace CapaNegocios
{
    public class CN_CapFacturaRevisionCobro
    {
        public void ConsultaFacturaRevisionCobro_Buscar(FacturaRevisionCobro facturaRevCob, ref List<FacturaRevisionCobro> listaFacturaRevCob, string Conexion
            , int? Id_U
            , DateTime? Frc_Fecha_inicio
            , DateTime? Frc_Fecha_fin
            , string Frc_Estatus
            , int? Id_Frc_inicio
            , int? Id_Frc_fin
            , int? Id_Cte
            )
        {
            try
            {
                new CD_CapFacturaRevisionCobro().ConsultaFacturaRevisionCobro_Buscar(facturaRevCob, ref listaFacturaRevCob, Conexion
                    , Id_U
                    , Frc_Fecha_inicio
                    , Frc_Fecha_fin
                    , Frc_Estatus
                    , Id_Frc_inicio
                    , Id_Frc_fin
                    , Id_Cte);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarFacturaRevisionCobro(ref FacturaRevisionCobro facturaRevisionCobro, string Conexion)
        {
            try
            {
                new CD_CapFacturaRevisionCobro().ConsultarFacturaRevisionCobro(ref facturaRevisionCobro, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void InsertarFacturaRevisionCobro(ref FacturaRevisionCobro facturaRevisionCobro, string Conexion, ref int verificador)
        {
            try
            {
                new CD_CapFacturaRevisionCobro().InsertarFacturaRevisionCobro(ref facturaRevisionCobro, Conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarFacturaRevisionCobro(ref FacturaRevisionCobro facturaRevisionCobro, string Conexion, ref int verificador)
        {
            try
            {
                new CD_CapFacturaRevisionCobro().ModificarFacturaRevisionCobro(ref facturaRevisionCobro, Conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EliminarFacturaRevisionCobro(FacturaRevisionCobro facturaRevisionCobro, string Conexion, ref int verificador)
        {
            try
            {
                new CD_CapFacturaRevisionCobro().EliminarFacturaRevisionCobro(facturaRevisionCobro, Conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarEstatusFacturaRevisionCobro(FacturaRevisionCobro facturaRevisionCobro, string Conexion, ref int verificador)
        {
            try
            {
                new CD_CapFacturaRevisionCobro().ModificarEstatusFacturaRevisionCobro(facturaRevisionCobro, Conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarFacturaRevisionCobro_Sugerido(ref FacturaRevisionCobro facturaRevisionCobro, string Conexion)
        {
            try
            {
                new CD_CapFacturaRevisionCobro().ConsultarFacturaRevisionCobro_Sugerido(ref facturaRevisionCobro, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
