using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CapaEntidad;
using CapaDatos;

namespace CapaNegocios
{
    public class CN_CapFacturaSvtaAlmacen
    {
        public void ConsultaFacturaSvtaAlmacen_Buscar(FacturaSvtaAlmacen facturaSvtaAlmacen, ref List<FacturaSvtaAlmacen> listaFacturaSvtaAlmacen, string Conexion
            , int? Id_U
            , DateTime? Fva_Fecha_inicio
            , DateTime? Fva_Fecha_fin
            , string Fva_Estatus
            , int? Id_Fva_inicio
            , int? Id_Fva_fin
            , int? Id_Cte
            )
        {
            try
            {
                new CD_CapFacturaSvtaAlmacen().ConsultaFacturaSvtaAlmacen_Buscar(facturaSvtaAlmacen, ref listaFacturaSvtaAlmacen, Conexion
                    , Id_U
                    , Fva_Fecha_inicio
                    , Fva_Fecha_fin
                    , Fva_Estatus
                    , Id_Fva_inicio
                    , Id_Fva_fin
                    , Id_Cte);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarFacturaSvtaAlmacen(ref FacturaSvtaAlmacen facturaSvtaAlmacen, string Conexion)
        {
            try
            {
                new CD_CapFacturaSvtaAlmacen().ConsultarFacturaSvtaAlmacen(ref facturaSvtaAlmacen, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void InsertarFacturaSvtaAlmacen(ref FacturaSvtaAlmacen facturaSvtaAlmacen, string Conexion, ref int verificador)
        {
            try
            {
                new CD_CapFacturaSvtaAlmacen().InsertarFacturaSvtaAlmacen(ref facturaSvtaAlmacen, Conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarFacturaSvtaAlmacen(ref FacturaSvtaAlmacen facturaSvtaAlmacen, string Conexion, ref int verificador)
        {
            try
            {
                new CD_CapFacturaSvtaAlmacen().ModificarFacturaSvtaAlmacen(ref facturaSvtaAlmacen, Conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EliminarFacturaSvtaAlmacen(FacturaSvtaAlmacen facturaSvtaAlmacen, string Conexion, ref int verificador)
        {
            try
            {
                new CD_CapFacturaSvtaAlmacen().EliminarFacturaSvtaAlmacen(facturaSvtaAlmacen, Conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarEstatusFacturaSvtaAlmacen(FacturaSvtaAlmacen facturaSvtaAlmacen, string Conexion, ref int verificador)
        { 
            try
            {
                new CD_CapFacturaSvtaAlmacen().ModificarEstatusFacturaSvtaAlmacen(facturaSvtaAlmacen, Conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarFacturaSvtaAlmacen_Sugerido(ref FacturaSvtaAlmacen facturaSvtaAlmacen, string Conexion)
        {
            try
            {
                new CD_CapFacturaSvtaAlmacen().ConsultarFacturaSvtaAlmacen_Sugerido(ref facturaSvtaAlmacen, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Confirmar(FacturaSvtaAlmacen svta, ref int verificador, string Conexion)
        {
            try
            {
                new CD_CapFacturaSvtaAlmacen().Confirmar(svta, ref verificador, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaFacturaEncabezado(ref Factura factura, string Conexion, ref bool encontrado)
        {
            try
            {
                new CD_CapFacturaSvtaAlmacen().ConsultaFacturaEncabezado(ref factura, ref encontrado, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
