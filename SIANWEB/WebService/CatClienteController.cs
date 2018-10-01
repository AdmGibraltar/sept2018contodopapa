using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using CapaEntidad;
using CapaNegocios;
using System.Net.Http;

namespace SIANWEB.WebService
{
    public class CatClienteController : ApiController
    {
        /// <summary>
        /// Operación de cosulta de clientes mediante un término de búsqueda para los elementos de listado con opción de búsqueda
        /// </summary>
        /// <param name="terminoDeBusqueda">String. Término de búsqueda.</param>
        /// <returns>List<Clientes> - Clientes coincidentes con el término de búsqueda.</returns>
        [HttpGet]
        public HttpResponseMessage Get(string terminoDeBusqueda, int idTer)
        {
            try
            {
                CN_CatCliente cnCatCte = new CN_CatCliente();
                Clientes pars = new Clientes();
                List<Clientes> result = new List<Clientes>();

                pars.Id_Emp = Sesion.Id_Emp;
                pars.Id_Cd = Sesion.Id_Cd;
                pars.Id_Rik = Sesion.Id_Rik;
                pars.Cte_NomComercial = terminoDeBusqueda;
                pars.Id_Terr = idTer;
                //¿Pasar la UEN como parámetro?

                cnCatCte.Lista(pars, Sesion.Emp_Cnx, ref result);
                return Request.CreateResponse(System.Net.HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                //Manejar los casos adecuadamente
                return Request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, ex);
            }
        }

        /// <summary>
        /// Obtiene el conjunto de clientes en los cuales el valor rfc coincide con el valor del campo Cte_Rfc
        /// </summary>
        /// <param name="rfc">Valor del RFC a buscar</param>
        /// <returns>Colección de clientes coincidenes con el valor del campo Cte_Rfc y el valor rfc</returns>
        [HttpGet]
        public HttpResponseMessage Get(string rfc)
        {
            try
            {
                CN_CatCliente cnCatCliente = new CN_CatCliente();
                //var clientes=cnCatCliente.ObtenerPorRFC(Sesion, rfc);
                var clientes = cnCatCliente.Obtener_PorRFC(Sesion, rfc);
                return Request.CreateResponse(System.Net.HttpStatusCode.OK, clientes);
            }
            catch (Exception ex) //manejo de error genérico
            {
                return Request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, ex);
            }
        }


        // Identifica si el IDCte esta en los prospectos 

        [HttpGet]
        public HttpResponseMessage Get(int IdCte)
        {
            try            
            {                
                int Id_Emp = Sesion.Id_Emp;
                int Id_Cd = Sesion.Id_Cd;
                int Id_Rik = Sesion.Id_Rik;
                int Id_Cte = IdCte;

                CN_Prospecto CNp = new CN_Prospecto();
                int IdCrmProspecto  = CNp.Consultar(Id_Emp, Id_Cd, Id_Rik, Id_Cte, Sesion.Emp_Cnx);

                return Request.CreateResponse(System.Net.HttpStatusCode.OK, IdCrmProspecto);

            }
            catch (Exception ex) //manejo de error genérico
            {
                return Request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, ex);
            }
        }


        [HttpGet]
        public HttpResponseMessage Get(string nombreEmpresa, string sinUsar)
        {
            try
            {
                CN_CatCliente cnCatCliente = new CN_CatCliente();
                var clientes = cnCatCliente.ObtenerPorNombreComercial(Sesion, nombreEmpresa);
                return Request.CreateResponse(System.Net.HttpStatusCode.OK, clientes);
            }
            catch (Exception ex) //manejo de error genérico
            {
                return Request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, ex);
            }
        }
        
        // Busqueda de Cliente
        // Regresa un listado 
        // 6 Sep 2018

        [HttpGet]
        public eResponse<List<eClienteBuscar>> Buscar(string TextoBuscar)
        {
            eResponse<List<eClienteBuscar>> result = new eResponse<List<eClienteBuscar>>();
            result.Estado = 0;

            List<eClienteBuscar> lst = new List<eClienteBuscar>();
            try
            {
                
                CN_CatCliente CC = new CN_CatCliente();
                lst = CC.ListarBusqueda(Sesion.Id_Emp, Sesion.Id_Cd, 0, 0, 0, Sesion.Id_Rik, TextoBuscar, Sesion.Emp_Cnx); 

                result.Estado = 1;
                result.Datos = lst;

            }
            catch (Exception ex)
            {
                result.Estado = 0;
                result.Datos = null;
            }

            return result;
        }

        // Buscar Cliente por Id
        // 8 Sep 2018 RFH
        [HttpGet]
        public eResponse<Clientes> Buscar_CteById(int Id_Cte)
        {
            Clientes C = new Clientes();
            eResponse<Clientes> result = new eResponse<Clientes>();
            result.Estado = 0;
            try
            {
                CN_CatCliente CN = new CN_CatCliente();
                C = CN.Consultar_PorId_Cte(Sesion.Id_Emp, Sesion.Id_Cd, Id_Cte, Sesion.Emp_Cnx);
                result.Estado = 1;
                result.Datos = C;
            }
            catch (Exception ex)
            {
                result.Estado = 0;
                result.Datos = null;
            }

            return result;
        }

        protected Sesion Sesion
        {
            get
            {
                if (HttpContext.Current.Session != null)
                {
                    return (Sesion)HttpContext.Current.Session["Sesion" + HttpContext.Current.Session.SessionID];
                }
                return null;
            }
        }
    }
}