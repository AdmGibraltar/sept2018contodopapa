using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaDatos;
using CapaModelo;
using CapaEntidad;

namespace CapaNegocios
{
    public class CN_CatBiblioteca
    {
        /// <summary>
        /// Regresa la biblioteca por defecto del usuario ("Biblioteca por defecto")
        /// </summary>
        /// <param name="sesion">Sesión del usuario en operación</param>
        /// <param name="ibt">Transacción de capa de negocio</param>
        /// <returns>CatBiblioteca</returns>
        public CapBibliotecaUsuario ObtenerBibliotecaDefaultDeUsuario(Sesion sesion, IBusinessTransaction ibt)
        {
            CD_CatBiblioteca cdCatBiblioteca = new CD_CatBiblioteca();
            IQueryable<CapBibliotecaUsuario> entradasBibliotecasDeUsuario = cdCatBiblioteca.ConsultarBibliotecasPorUsuario(sesion.Id_Emp, sesion.Id_Cd, sesion.Id_U, ibt.DataContext);
            var entradasBibliotecaDefault = from ebu in entradasBibliotecasDeUsuario
                                            where ebu.CatBiblioteca.Biblio_Nombre.CompareTo("Biblioteca por defecto")==0
                                            select ebu;                                      
            if (entradasBibliotecaDefault.Count() > 0)
            {
                return entradasBibliotecaDefault.First();
            }
            return null;
        }

        /// <summary>
        /// Regresa el nivel de la biblioteca por defecto que actua como repositorio para los proyectos registrados.
        /// </summary>
        /// <param name="sesion">Sesión de usuario en operación</param>
        /// <param name="ibt">Transacción de la capa de negocio</param>
        /// <returns>CapBibliotecaNodo</returns>
        public CapBibliotecaNodo ObtenerRepositorioDeProyectos(Sesion sesion, IBusinessTransaction ibt)
        {
            CapBibliotecaUsuario bibliotecaPorDefecto = ObtenerBibliotecaDefaultDeUsuario(sesion, ibt);
            CN_CapBibliotecaNodo cnCapBibliotecaNodo = new CN_CapBibliotecaNodo();
            var nodoProyectos = cnCapBibliotecaNodo.ObtenerRepositorioDeProyectos(sesion, ibt);
            return nodoProyectos;
        }
    }
}
