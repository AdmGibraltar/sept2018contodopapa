using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaModelo;
using CapaDatos;
using CapaEntidad;

namespace CapaNegocios
{
    public class CN_CrmPropuestaEconomica
    {
        public void Generar(Sesion sesion, IEnumerable<CrmPropuestaEconomica> detalle)
        {
            CD_CrmPropuestaEconomica cdCrmPropuestaEconomica = new CD_CrmPropuestaEconomica();
            cdCrmPropuestaEconomica.Insertar(detalle, sesion.Emp_Cnx_EF);
        }

        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        public List<CapaEntidad.ePropuestaTecnoEconomicaDetalle>
            CRM_ObtenerPropuestaEconomica(int Id_Emp, int Id_Cd, int Id_Cte, int Id_Rik, int Id_Val, Sesion sesion)
        {
            List<CapaEntidad.ePropuestaTecnoEconomicaDetalle> Lst = new List<CapaEntidad.ePropuestaTecnoEconomicaDetalle>();
            CD_CrmPropuestaEconomica cdCrmPE = new CD_CrmPropuestaEconomica();
            Lst = cdCrmPE.CRM_ObtenerPropuestaEconomica(Id_Emp, Id_Cd, Id_Cte, Id_Rik, Id_Val, sesion.Emp_Cnx);
            return Lst;
        }

        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        public CapaEntidad.eCapValProyecto spCRMCapValProyecto(int Id_Emp, int Id_Cd, int Id_Cte, int Id_Rik, int Id_Val, Sesion sesion)
        {
            CapaEntidad.eCapValProyecto Obj = new CapaEntidad.eCapValProyecto();
            CD_CrmPropuestaEconomica cdCrmPE = new CD_CrmPropuestaEconomica();
            Obj = cdCrmPE.spCRMCapValProyecto(Id_Emp, Id_Cd, Id_Cte, Id_Rik, Id_Val, sesion.Emp_Cnx);
            return Obj;
        }


        public IEnumerable<CrmPropuestaEconomica> ObtenerPorValuacion(Sesion sesion, int idCte, int idVal)
        {
            CD_CrmPropuestaEconomica cdCrmPropuestaEconomica = new CD_CrmPropuestaEconomica();
            var result = cdCrmPropuestaEconomica.ConsultarPorValuacion(sesion.Id_Emp, sesion.Id_Cd, idCte, sesion.Id_Rik, idVal, sesion.Emp_Cnx_EF);
            CD_ProductoPrecios cdProductoPrecios = new CD_ProductoPrecios();
            result = result.Select(cpe => 
            {
                float precio = 0.0f;
                cdProductoPrecios.ConsultaListaProductoPrecioAAA(ref precio, sesion, cpe.Id_Prd);
                cpe.Prd_Precio = precio;
                return cpe; 
            }).ToList();
            return result;
        }

        /// <summary>
        /// Versión para clientes que no cuentan con una manera de especificar la sesión
        /// </summary>
        /// <param name="sesion"></param>
        /// <param name="idCte"></param>
        /// <param name="idVal"></param>
        /// <returns></returns>
        public IEnumerable<CrmPropuestaEconomica> ObtenerPorValuacion(int idEmp, int idCd, int idRik, int idCte, int idVal, string cadenaConexionEF, string cadenaConexion)
        {
            CD_CrmPropuestaEconomica cdCrmPropuestaEconomica = new CD_CrmPropuestaEconomica();
            var result = cdCrmPropuestaEconomica.ConsultarPorValuacion(idEmp, idCd, idCte, idRik, idVal, cadenaConexionEF);
            CD_ProductoPrecios cdProductoPrecios = new CD_ProductoPrecios();
            result = result.Select(cpe =>
            {
                float precio = 0.0f;
                cdProductoPrecios.ConsultaListaProductoPrecioAAA(ref precio, idEmp, idCd, cpe.Id_Prd, cadenaConexion);
                cpe.Prd_Precio = precio;
                return cpe;
            }).ToList();
            return result;
        }

    }
}
