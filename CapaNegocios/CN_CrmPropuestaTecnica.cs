using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaModelo;
using CapaDatos;
using CapaEntidad;

namespace CapaNegocios
{
    public class CN_CrmPropuestaTecnica
    {
        /// <summary>
        /// Obtiene la fuente para la generación del reporte "Propuesta Tecnica"
        /// </summary>
        /// <param name="s">Sesión del usuario</param>
        /// <param name="idCte">Identificador del cliente de interés</param>
        /// <param name="idVal">Identificador de la valuación de interes</param>
        /// <returns>Detalle del reporte "Propuesta Tecnica"</returns>
        public IEnumerable<CrmPropuestaTecnica> ObtenerReportePropuestaTecnica(Sesion s, int idCte, int idVal)
        {
            //Falta: Validar excepciones arrojadas por la capa de acceso a datos para registrar en la bitácora; validar que el cliente exista; validar que la valuacion exista.
            IEnumerable<CrmPropuestaTecnica> resultado = null;
            CD_CrmPropuestaTecnica cdCrmPropuestaTecnica = new CD_CrmPropuestaTecnica();
            resultado = cdCrmPropuestaTecnica.ConsultarDetallePropuestaTecnica(s.Id_Emp, s.Id_Cd, idCte, s.Id_Rik, idVal, s.Emp_Cnx_EF);
            return resultado;
        }

        /// <summary>
        /// Obtiene la fuente para la generación del reporte "Propuesta Tecnica"
        /// </summary>
        /// <param name="s">Sesión del usuario</param>
        /// <param name="idCte">Identificador del cliente de interés</param>
        /// <param name="idVal">Identificador de la valuación de interes</param>
        /// <param name="ibt">Transacción de negocio</param>
        /// <returns>Detalle del reporte "Propuesta Tecnica"</returns>
        public IEnumerable<CrmPropuestaTecnica> ObtenerReportePropuestaTecnica(Sesion s, int idCte, int idVal, IBusinessTransaction ibt)
        {
            //Falta: Validar excepciones arrojadas por la capa de acceso a datos para registrar en la bitácora; validar que el cliente exista; validar que la valuacion exista.
            IEnumerable<CrmPropuestaTecnica> resultado = null;
            CD_CrmPropuestaTecnica cdCrmPropuestaTecnica = new CD_CrmPropuestaTecnica();
            resultado = cdCrmPropuestaTecnica.ConsultarDetallePropuestaTecnica(s.Id_Emp, s.Id_Cd, idCte, s.Id_Rik, idVal, ibt.DataContext);
            return resultado;
        }

        /// <summary>
        /// Persiste los cambios del detalle de una propuesta técnica
        /// </summary>
        /// <param name="s">Sesión del usuario</param>
        /// <param name="detalle">Conjunto de instancias que representan el detalle de la propuesta técnica</param>
        public void ActualizarEdicionPropuesta(Sesion s, List<CrmPropuestaTecnica> detalle)
        {
            CD_CrmPropuestaTecnica cdCrmPropuestaTecnica = new CD_CrmPropuestaTecnica();
            //Comprobar integridad referencial
            using (ICD_Contexto contexto = CD_FabricaContexto.CrearDefault(s.Emp_Cnx_EF))
            {
                try
                {
                    //agrupar por cliente y valuacion
                    var datosAgrupados = (from d in detalle
                                          group d by new { d.Id_Cte, d.Id_Val } into g
                                          select g).ToList();
                    foreach (var g in datosAgrupados)
                    {
                        cdCrmPropuestaTecnica.Actualizar(s.Id_Emp, s.Id_Cd, g.Key.Id_Cte, g.Key.Id_Val, g.ToList(), contexto);
                    }
                }
                catch (Exception ex) //capturar las excepciones conocidas de la capa de datos y arrojar una excepcion de negocio
                {
                    throw ex;
                }

                contexto.Commit();
            }
        }

        /// <summary>
        /// Genera una propuesta técnica a partir de la valuación especificada en idVal.
        /// </summary>
        /// <param name="s">Sesión del usuario</param>
        /// <param name="idVal">Identificador de la valuación en la cual se basa la propuesta</param>
        public void GenerarAPartirDeValuacion(Sesion s, int idVal)
        {
            CN_CapValuacionProyecto cnCapValuacionProyecto = new CN_CapValuacionProyecto();
            var valuacion = cnCapValuacionProyecto.Obtener(s, idVal);

            CN_Configuracion cnConfiguracion = new CN_Configuracion();
            string urlImagenNoDisponible = string.Empty;
            try
            {
                var config = cnConfiguracion.Obtener(s, 2300);
                urlImagenNoDisponible = config.Conf_Valor;
            }
            catch (Exception ex)
            {
                //log, establecer una url fija
            }

            CD_CrmPropuestaTecnica cdCrmPropuestaTecnica = new CD_CrmPropuestaTecnica();
            //Ejecutar el siguiente bloque en una transacción
            List<CrmPropuestaTecnica> detalle = new List<CrmPropuestaTecnica>();
            CD_CapValProyectoDet cdCapValProyectoDet = new CD_CapValProyectoDet();
            var capValProyectoDets=cdCapValProyectoDet.ConsultarPorCapValProyectoId(valuacion.Id_Emp, valuacion.Id_Cd, valuacion.Id_Vap, s.Emp_Cnx_EF);
            foreach (var cvpd in capValProyectoDets)
            {
                CrmPropuestaTecnica crmPropuestaTecnica = new CrmPropuestaTecnica();
                crmPropuestaTecnica.Id_Emp = s.Id_Emp;
                crmPropuestaTecnica.Id_Cd = s.Id_Cd;
                crmPropuestaTecnica.Id_Cte = valuacion.Id_Cte;
                crmPropuestaTecnica.Id_Val = idVal;
                crmPropuestaTecnica.Id_Prd = cvpd.Id_Prd;
                crmPropuestaTecnica.CPT_RecursoImagenProductoActual = urlImagenNoDisponible;
                crmPropuestaTecnica.CPT_RecursoImagenSolucionKey = urlImagenNoDisponible;
                detalle.Add(crmPropuestaTecnica);
            }
            cdCrmPropuestaTecnica.Insertar(detalle, s.Emp_Cnx_EF);
        }

        /// <summary>
        /// Genera una propuesta técnica a partir de la valuación especificada en idVal. Versión transaccional.
        /// </summary>
        /// <param name="s">Sesión del usuario</param>
        /// <param name="idVal">Identificador de la valuación en la cual se basa la propuesta</param>
        /// <param name="icdCtx">Context de conexión al repositorio de datos</param>
        public void GenerarAPartirDeValuacion(Sesion s, int idVal, IBusinessTransaction ibt)
        {
            CN_CapValuacionProyecto cnCapValuacionProyecto = new CN_CapValuacionProyecto();
            var valuacion = cnCapValuacionProyecto.Obtener(s, idVal, ibt);

            CN_Configuracion cnConfiguracion = new CN_Configuracion();
            string urlImagenNoDisponible = string.Empty;
            try
            {
                var config = cnConfiguracion.Obtener(s, 2300, ibt);
                urlImagenNoDisponible = config.Conf_Valor;
            }
            catch (Exception ex)
            {
                //log, establecer una url fija
            }

            CD_CrmPropuestaTecnica cdCrmPropuestaTecnica = new CD_CrmPropuestaTecnica();
            //Ejecutar el siguiente bloque en una transacción
            List<CrmPropuestaTecnica> detalle = new List<CrmPropuestaTecnica>();
            CD_CapValProyectoDet cdCapValProyectoDet = new CD_CapValProyectoDet();
            var capValProyectoDets = cdCapValProyectoDet.ConsultarPorCapValProyectoId(valuacion.Id_Emp, valuacion.Id_Cd, valuacion.Id_Vap, ibt.DataContext);
            foreach (var cvpd in capValProyectoDets)
            {
                CrmPropuestaTecnica crmPropuestaTecnica = new CrmPropuestaTecnica();
                crmPropuestaTecnica.Id_Emp = s.Id_Emp;
                crmPropuestaTecnica.Id_Cd = s.Id_Cd;
                crmPropuestaTecnica.Id_Cte = valuacion.Id_Cte;
                crmPropuestaTecnica.Id_Val = idVal;
                crmPropuestaTecnica.Id_Prd = cvpd.Id_Prd;
                crmPropuestaTecnica.CPT_RecursoImagenProductoActual = urlImagenNoDisponible;
                crmPropuestaTecnica.CPT_RecursoImagenSolucionKey = urlImagenNoDisponible;
                detalle.Add(crmPropuestaTecnica);
            }
            cdCrmPropuestaTecnica.Insertar(detalle, ibt.DataContext);
        }
    }
}
