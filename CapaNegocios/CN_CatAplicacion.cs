using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using CapaDatos;
using CapaModelo;

namespace CapaNegocios
{
    public class CN_CatAplicacion
    {
        public void Lista(Aplicacion aplicacion, string Conexion, ref List<Aplicacion> List)
        {
            try
            {
                CD_CatAplicacion claseCapaDatos = new CD_CatAplicacion();
                claseCapaDatos.Lista(aplicacion, Conexion, ref List);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AplicacionesSegmento_Consultar(int Id_Emp, int Id_Seg, string Conexion, ref List<Aplicacion> List)
        {
            try
            {
                CD_CatAplicacion claseCapaDatos = new CD_CatAplicacion();
                claseCapaDatos.AplicacionesSegmento_Consultar(Id_Emp, Id_Seg, Conexion, ref List);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Insertar(Aplicacion aplicacion, string Conexion, ref int verificador)
        {
            try
            {
                CD_CatAplicacion claseCapaDatos = new CD_CatAplicacion();
                claseCapaDatos.Insertar(aplicacion, Conexion, ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Modificar(Aplicacion aplicacion, string Conexion, ref int verificador)
        {
            try
            {
                CD_CatAplicacion claseCapaDatos = new CD_CatAplicacion();
                claseCapaDatos.Modificar(aplicacion, Conexion, ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<CatAplicacion> ObtenerPorEmpresaYSolucion(int idEmp, int idSol, Sesion sesion)
        {
            CD_CatAplicacion cdCatAplicacion = new CD_CatAplicacion();
            var result = cdCatAplicacion.ConsultarPorEmpresaYSolucion(idEmp, idSol, sesion.Emp_Cnx_EF);
            return result;
        }

        public IEnumerable<CatAplicacion> ObtenerPorEmpresaSolucionSegmento(int idEmp, int idCd, int idSol, int idSeg, int idOp, Sesion sesion)
        {
            CD_CatAplicacion cdCatAplicacion = new CD_CatAplicacion();
            var result = cdCatAplicacion.ConsultarPorEmpresaYSolucion(idEmp, idSol, sesion.Emp_Cnx_EF);
            return result;
        }

        /// <summary>
        /// Obtiene las aplicaciones válidas disponibles para el cliente dada la oferta especificada.
        /// </summary>
        /// <param name="sesion"></param>
        /// <param name="idCte"></param>
        /// <param name="idUen"></param>
        /// <param name="idSeg"></param>
        /// <param name="idArea"></param>
        /// <param name="idSol"></param>
        /// <returns></returns>
        public IEnumerable<CatAplicacion> ObtenerAplicacionesEnProyectos(Sesion sesion, int idCte, int idUen, int idSeg, int idArea, int idSol)
        {
            CD_CatAplicacion cdCatAplicacion = new CD_CatAplicacion();
            var result = cdCatAplicacion.ConsultarAplicacionesEnProyectos(sesion.Id_Emp, sesion.Id_Cd, idCte, idUen, idSeg, idArea, idSol, sesion.Emp_Cnx_EF);
            return result;
        }

        public IEnumerable<CatAplicacion> ObtenerAplicacionesEnProyecto(Sesion sesion, int idCte, int idOp, IBusinessTransaction ibt)
        {
            CD_CatAplicacion cdCatAplicacion = new CD_CatAplicacion();
            var result = cdCatAplicacion.ConsultarAplicacionesEnProyecto(sesion.Id_Emp, sesion.Id_Cd, sesion.Id_Rik, idCte, idOp, ibt.DataContext);
            return result;
        }

        public IEnumerable<CatAplicacion> ObtenerAplicacionesEnProyecto(Sesion sesion, int idCte, int idOp)
        {
            CD_CatAplicacion cdCatAplicacion = new CD_CatAplicacion();
            var result = cdCatAplicacion.ConsultarAplicacionesEnProyecto(sesion.Id_Emp, sesion.Id_Cd, sesion.Id_Rik, idCte, idOp, sesion.Emp_Cnx_EF);
            return result;
        }

        /// <summary>
        /// Regresa todas las aplicaciones disponibles para la creación de un nuevo proyecto
        /// </summary>
        /// <param name="sesion"></param>
        /// <param name="idUen"></param>
        /// <param name="idSeg"></param>
        /// <param name="idArea"></param>
        /// <param name="idSol"></param>
        /// <param name="idCte"></param>
        /// <returns></returns>
        public IEnumerable<CatAplicacion> ObtenerTodasLasAplicacionesDisponibles(Sesion sesion, int idUen, int idSeg, int idArea, int idSol, int idCte)
        {
            var result = ObtenerPorEmpresaSolucionSegmento(sesion.Id_Emp, sesion.Id_Cd, idSol, idSeg, 0, sesion);
            var aplicacionesEnUso = ObtenerAplicacionesEnProyectos(sesion, idCte, idUen, idSeg, idArea, idSol).Select(ca => ca.Id_Apl).ToList();
            var aplicacionesDisponibles = (from r in result
                                           where !aplicacionesEnUso.Contains(r.Id_Apl)
                                           select r).ToList();

            // Recorre las aplicadas para desactivar lo no disponibles            
            foreach (var a in result)
            {   
                var bEnUso = aplicacionesEnUso.Contains(a.Id_Apl)  ? 1 : 0;    
                if (bEnUso==1) {
                    a.Apl_Activo =  false;
                }
            }
            return result; // aplicacionesDisponibles;
        }

        /// <summary>
        /// Regresa todas las aplicaciones elejidas y disponibles para un proyecto existente.
        /// </summary>
        /// <param name="sesion"></param>
        /// <param name="idUen"></param>
        /// <param name="idSeg"></param>
        /// <param name="idArea"></param>
        /// <param name="idSol"></param>
        /// <param name="idOp"></param>
        /// <param name="idCte"></param>
        /// <returns></returns>
        public IEnumerable<CatAplicacion> ObtenerTodasLasAplicacionesDisponibles(Sesion sesion, int idUen, int idSeg, int idArea, int idSol, int idOp, int idCte)
        {
            var result = ObtenerPorEmpresaSolucionSegmento(sesion.Id_Emp, sesion.Id_Cd, idSol, idSeg, idOp, sesion);
            var aplicacionesEnProyecto = ObtenerAplicacionesEnProyecto(sesion, idCte, idOp).Select(ca => ca.Id_Apl);
            var aplicacionesEnUso = ObtenerAplicacionesEnProyectos(sesion, idCte, idUen, idSeg, idArea, idSol).Select(ca => ca.Id_Apl).Where(idApl => !aplicacionesEnProyecto.Contains(idApl));
            var aplicacionesDisponibles = (from r in result
                                           where !aplicacionesEnUso.Contains(r.Id_Apl)
                                           select r).ToList();
            return aplicacionesDisponibles;
        }

        public IEnumerable<CatAplicacion> Obtener(Sesion s, IBusinessTransaction ibt)
        {
            CD_CatAplicacion cdCatAplicacion = new CD_CatAplicacion();
            return cdCatAplicacion.ConsultarTodas(s.Id_Emp, ibt.DataContext);
        }

        /// <summary>
        /// Regresa una instancia de datos de la entidad CatAplicacion
        /// </summary>
        /// <param name="s">Sesión del usuario en operación</param>
        /// <param name="idApl">Identificador de la aplicación</param>
        /// <returns>CatAplicacion</returns>
        public CatAplicacion Consultar(Sesion s, int idApl)
        {
            CD_CatAplicacion cdCatAplicacion = new CD_CatAplicacion();
            return cdCatAplicacion.Consultar(s.Id_Emp, idApl, s.Emp_Cnx_EF);
        }

        /// <summary>
        /// Regresa una instancia de datos de la entidad CatAplicacion
        /// </summary>
        /// <param name="s">Sesión del usuario en operación</param>
        /// <param name="idApl">Identificador de la aplicación</param>
        /// <param name="ibt">Transacción de la capa de negocio</param>
        /// <returns>CatAplicacion</returns>
        public CatAplicacion Consultar(Sesion s, int idApl, IBusinessTransaction ibt)
        {
            CD_CatAplicacion cdCatAplicacion = new CD_CatAplicacion();
            return cdCatAplicacion.Consultar(s.Id_Emp, idApl, ibt.DataContext);
        }
    }
}
