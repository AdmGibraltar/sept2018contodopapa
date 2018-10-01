using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CapaEntidad;
using CapaDatos;

namespace CapaNegocios
{
    public class CN_CapFacturaAlmacenCobro 
    {
        public void ConsultaFacturaAlmacenCobro_Buscar(FacturaAlmacenCobro FacturaAlmacenCobro, ref List<FacturaAlmacenCobro> listaFacturaAlmacenCobro, string Conexion
            , int? Id_U
            , DateTime? Fac_Fecha_inicio
            , DateTime? Fac_Fecha_fin
            , string Fac_Estatus
            , int? Id_Fac_inicio
            , int? Id_Fac_fin
            , int? Id_Cte
            )
        {
            try
            {
                new CD_CapFacturaAlmacenCobro().ConsultaFacturaAlmacenCobro_Buscar(FacturaAlmacenCobro, ref listaFacturaAlmacenCobro, Conexion
                    , Id_U
                    , Fac_Fecha_inicio
                    , Fac_Fecha_fin
                    , Fac_Estatus
                    , Id_Fac_inicio
                    , Id_Fac_fin
                    , Id_Cte);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarFacturaAlmacenCobro(ref FacturaAlmacenCobro FacturaAlmacenCobro, string Conexion)
        {
            try
            {
                new CD_CapFacturaAlmacenCobro().ConsultarFacturaAlmacenCobro(ref FacturaAlmacenCobro, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void InsertarFacturaAlmacenCobro(ref FacturaAlmacenCobro FacturaAlmacenCobro, string Conexion, ref int verificador)
        {
            try
            {
                new CD_CapFacturaAlmacenCobro().InsertarFacturaAlmacenCobro(ref FacturaAlmacenCobro, Conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarFacturaAlmacenCobro(ref FacturaAlmacenCobro FacturaAlmacenCobro, string Conexion, ref int verificador)
        {
            try
            {
                new CD_CapFacturaAlmacenCobro().ModificarFacturaAlmacenCobro(ref FacturaAlmacenCobro, Conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EliminarFacturaAlmacenCobro(FacturaAlmacenCobro FacturaAlmacenCobro, string Conexion, ref int verificador)
        {
            try
            {
                new CD_CapFacturaAlmacenCobro().EliminarFacturaAlmacenCobro(FacturaAlmacenCobro, Conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarEstatusFacturaAlmacenCobro(FacturaAlmacenCobro FacturaAlmacenCobro, string Conexion, ref int verificador)
        {
            try
            {
                new CD_CapFacturaAlmacenCobro().ModificarEstatusFacturaAlmacenCobro(FacturaAlmacenCobro, Conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarFacturaAlmacenCobro_Sugerido(ref FacturaAlmacenCobro FacturaAlmacenCobro, string Conexion)
        {
            try
            {
                new CD_CapFacturaAlmacenCobro().ConsultarFacturaAlmacenCobro_Sugerido(ref FacturaAlmacenCobro, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Confirmar(FacturaAlmacenCobro lAlmcob, string Conexion)
        {
            try
            {
                new CD_CapFacturaAlmacenCobro().Confirmar(lAlmcob, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ValidaProcesoFacturaAlmacenCobro(Factura factura, ref int Verificador, string Conexion)
        {
            try
            {
                CD_CapFacturaAlmacenCobro cd_Fac = new CD_CapFacturaAlmacenCobro();
                cd_Fac.ValidaProcesoFacturaAlmacenCobro(factura, ref Verificador, Conexion);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
    }
}
