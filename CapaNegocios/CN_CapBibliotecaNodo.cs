using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaDatos;
using CapaModelo;
using CapaEntidad;

namespace CapaNegocios
{
    public class CN_CapBibliotecaNodo
    {
        /// <summary>
        /// Devuelve el nodo que representa el nivel de la biblioteca que almacena los repositorios de los proyectos en la biblioteca por defecto del usuario en sesión.
        /// </summary>
        /// <param name="sesion">Sesión</param>
        /// <param name="ibt">Transacción de la capa de negocios</param>
        /// <returns>CapBibliotecaNodo</returns>
        public CapBibliotecaNodo ObtenerRepositorioDeProyectos(Sesion sesion, IBusinessTransaction ibt)
        {
            CN_CatBiblioteca cnCatBiblioteca = new CN_CatBiblioteca();
            CapBibliotecaUsuario bibliotecaPorDefecto = cnCatBiblioteca.ObtenerBibliotecaDefaultDeUsuario(sesion, ibt);
            var nodos = from cbn in bibliotecaPorDefecto.CatBiblioteca.CapBibliotecaNodoes
                        where cbn.BiblioNodo_Nombre.CompareTo("Proyectos") == 0 
                        && cbn.CapBibliotecaNodo2.BiblioNodo_Nombre.CompareTo("Gestión de la Promoción") == 0 
                        && cbn.CapBibliotecaNodo2.CapBibliotecaNodo2.BiblioNodo_Nombre.CompareTo("CRM") == 0 
                        && cbn.CapBibliotecaNodo2.CapBibliotecaNodo2.CapBibliotecaNodo2.BiblioNodo_Nombre.CompareTo("Raíz")==0
                        select cbn;
            if (nodos.Count() > 0)
            {
                return nodos.First();
            }
            return null;
        }
    }
}
