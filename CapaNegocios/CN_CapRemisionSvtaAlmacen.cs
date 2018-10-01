using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CapaEntidad;
using CapaDatos;

namespace CapaNegocios
{
    public class CN_CapRemisionSvtaAlmacen
    {
        public void ConsultaRemisionSvtaAlmacen_Buscar(RemisionSvtaAlmacen RemisionSvtaAlmacen, ref List<RemisionSvtaAlmacen> listaRemisionSvtaAlmacen, string Conexion
            , int? Id_U
            , DateTime? Rva_Fecha_inicio
            , DateTime? Rva_Fecha_fin
            , string Rva_Estatus
            , int? Id_Rva_inicio
            , int? Id_Rva_fin
            , int? Id_Cte
            )
        {
            try
            {
                new CD_CapRemisionSvtaAlmacen().ConsultaRemisionSvtaAlmacen_Buscar(RemisionSvtaAlmacen, ref listaRemisionSvtaAlmacen, Conexion
                    , Id_U
                    , Rva_Fecha_inicio
                    , Rva_Fecha_fin
                    , Rva_Estatus
                    , Id_Rva_inicio
                    , Id_Rva_fin
                    , Id_Cte);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarRemisionSvtaAlmacen(ref RemisionSvtaAlmacen RemisionSvtaAlmacen, string Conexion)
        {
            try
            {
                new CD_CapRemisionSvtaAlmacen().ConsultarRemisionSvtaAlmacen(ref RemisionSvtaAlmacen, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void InsertarRemisionSvtaAlmacen(ref RemisionSvtaAlmacen RemisionSvtaAlmacen, string Conexion, ref int verificador)
        {
            try
            {
                new CD_CapRemisionSvtaAlmacen().InsertarRemisionSvtaAlmacen(ref RemisionSvtaAlmacen, Conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarRemisionSvtaAlmacen(ref RemisionSvtaAlmacen RemisionSvtaAlmacen, string Conexion, ref int verificador)
        {
            try
            {
                new CD_CapRemisionSvtaAlmacen().ModificarRemisionSvtaAlmacen(ref RemisionSvtaAlmacen, Conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EliminarRemisionSvtaAlmacen(RemisionSvtaAlmacen RemisionSvtaAlmacen, string Conexion, ref int verificador)
        {
            try
            {
                new CD_CapRemisionSvtaAlmacen().EliminarRemisionSvtaAlmacen(RemisionSvtaAlmacen, Conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarEstatusRemisionSvtaAlmacen(RemisionSvtaAlmacen RemisionSvtaAlmacen, string Conexion, ref int verificador)
        { 
            try
            {
                new CD_CapRemisionSvtaAlmacen().ModificarEstatusRemisionSvtaAlmacen(RemisionSvtaAlmacen, Conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarRemisionSvtaAlmacen_Sugerido(ref RemisionSvtaAlmacen RemisionSvtaAlmacen, string Conexion)
        {
            try
            {
                new CD_CapRemisionSvtaAlmacen().ConsultarRemisionSvtaAlmacen_Sugerido(ref RemisionSvtaAlmacen, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Confirmar(RemisionSvtaAlmacenDet svta, ref int verificador, string Conexion)
        {
            try
            {
                new CD_CapRemisionSvtaAlmacen().Confirmar(svta, ref verificador, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaRemisionEncabezado(ref Remision Remision, string Conexion, ref bool encontrado)
        {
            try
            {
                new CD_CapRemisionSvtaAlmacen().ConsultaRemisionEncabezado(ref Remision, ref encontrado, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
