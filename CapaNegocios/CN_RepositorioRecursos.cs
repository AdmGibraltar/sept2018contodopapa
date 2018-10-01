using  System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaDatos;
using CapaModelo;
using CapaEntidad;

namespace CapaNegocios
{
    public class CN_RepositorioRecursos
    {
        /// <summary>
        /// Obtiene la entrada del repositorio de recursos que representa el nivel de la propuesta tecnoeconomica.
        /// </summary>
        /// <param name="sesion">Sesión del usuario en operación</param>
        /// <param name="ibt">Transacción de la capa de negocio</param>
        /// <returns>CapBibliotecaNodo</returns>
        public CapBibliotecaNodo ObtenerRepositorioPropuestasCRM(Sesion sesion, IBusinessTransaction ibt)
        {
            CD_CapBibliotecaNodo cdCapBibliotecaNodo = new CD_CapBibliotecaNodo();
            var entradas = cdCapBibliotecaNodo.ConsultarPorUsuario(sesion.Id_Emp, sesion.Id_Cd, sesion.Id_U, ibt.DataContext);
            var coincidencias = from c in entradas
                                where c.CapBibliotecaNodo2.BiblioNodo_Nombre == "CRM" 
                                && c.CapBibliotecaNodo2.CapBibliotecaNodo2.BiblioNodo_Nombre=="Raíz" 
                                && c.BiblioNodo_Nombre=="Propuestas"
                                select c;
            if (coincidencias.Count() > 0)
            {
                return coincidencias.First();
            }
            return null;
        }

        // Metodo utiliza SP
        public List<eCapBibliotecaNodo> ObtenerRepositorioPropuestasCRM_(Sesion sesion, IBusinessTransaction ibt)
        {
            List<eCapBibliotecaNodo> Lst = new List<eCapBibliotecaNodo>();

            CD_CapBibliotecaNodo cdBN = new CD_CapBibliotecaNodo();
            Lst = cdBN.ConsultaNodos(sesion.Id_Emp, sesion.Id_Cd, sesion.Id_U, sesion.Emp_Cnx);
            
            return Lst;
        }
        
        /// <summary>
        /// Crea una entrada en el repositorio "Raíz/CRM/Propuestas" de tipo imagen. El archivo debe de existir en la unidad de almacenamiento para estos momentos.
        /// </summary>
        /// <param name="sesion">Sesión del usuario en operación</param>
        /// <param name="nombreArchivo">Nombre del archivo de imágen que se asociará al repositorio</param>
        /// <param name="ibt">Transacción de capa de negocio</param>
        /// <returns>CatRecurso</returns>
        public CatRecurso CrearRecursoImagenRepositorioCRM(Sesion sesion, string nombreArchivo, IBusinessTransaction ibt)
        {
            CD_CatRecurso cdCatRecurso = new CD_CatRecurso();
            CN_CatTipoRecurso cnCatTipoRecurso = new CN_CatTipoRecurso(ibt);
            CatRecurso catRecurso = new CatRecurso() { Id_Emp = sesion.Id_Emp, Id_Cd = sesion.Id_Cd, Id_TipoRecurso = cnCatTipoRecurso.Imagen.Id_TipoRecurso };
            catRecurso = cdCatRecurso.Insertar(catRecurso, ibt.DataContext);

            CD_CatRecursoArchivo cdCatRecursoArchivo = new CD_CatRecursoArchivo();
            CatRecursoArchivo catRecursoArchivo = new CatRecursoArchivo() 
            { 
                Id_Emp=sesion.Id_Emp, 
                Id_Cd=sesion.Id_Cd,
                Id_Recurso=catRecurso.Id_Recurso,
                RecArc_Nombre=nombreArchivo,
                RecArc_Extension=string.Empty
            };

            catRecursoArchivo = cdCatRecursoArchivo.Insertar(catRecursoArchivo, ibt.DataContext);

            CD_CapBibliotecaNodo cdCapBibliotecaNodo = new CD_CapBibliotecaNodo();
            CN_CatBiblioteca cnCatBiblioteca = new CN_CatBiblioteca();
            var catBiblioteca = cnCatBiblioteca.ObtenerBibliotecaDefaultDeUsuario(sesion, ibt);
            CapBibliotecaNodo capBibliotecaNodoCRMPropuestas = ObtenerRepositorioPropuestasCRM(sesion, ibt);
            CapBibliotecaNodo capBibliotecaNodo = new CapBibliotecaNodo() 
            { 
                Id_Emp=sesion.Id_Emp,
                Id_Cd=sesion.Id_Cd,
                Id_Recurso=catRecurso.Id_Recurso,
                Id_BiblioNodo_Padre = capBibliotecaNodoCRMPropuestas.Id_BiblioNodo,
                Id_Biblioteca = catBiblioteca.Id_Biblioteca
            };
            return catRecurso;
        }

        /// <summary>
        /// Crea una entrada en el repositorio "Raíz/CRM/Propuestas" de tipo imagen. El archivo debe de existir en la unidad de almacenamiento para estos momentos.
        /// </summary>
        /// <param name="sesion">Sesión del usuario en operación</param>
        /// <param name="nombreArchivo">Nombre del archivo de imágen que se asociará al repositorio</param>
        /// <param name="idBiblioNodoPadre">Identificador del nivel del repositorio en donde se asociará el recurso</param>
        /// <param name="ibt">Transacción de capa de negocio</param>
        /// <returns>CatRecurso</returns>
        public CatRecursoArchivo CrearRecursoImagenRepositorio(Sesion sesion, string nombreArchivo, string extension, string contentType, int idBiblioNodoPadre, IBusinessTransaction ibt)
        {
            CN_CatBiblioteca cnCatBiblioteca = new CN_CatBiblioteca();
            var catBiblioteca = cnCatBiblioteca.ObtenerBibliotecaDefaultDeUsuario(sesion, ibt);

            CD_CapBibliotecaNodo cdCapBibliotecaNodo = new CD_CapBibliotecaNodo();
            var capBibliotecaNodoPadre = cdCapBibliotecaNodo.ConsultarPorIdentificador(sesion.Id_Emp, sesion.Id_Cd, sesion.Id_U, catBiblioteca.Id_Biblioteca, idBiblioNodoPadre, ibt.DataContext);
            if (capBibliotecaNodoPadre == null)
            {
                throw new InexistentCapBibliotecaNodoException(idBiblioNodoPadre);
            }

            CD_CatRecurso cdCatRecurso = new CD_CatRecurso();
            CN_CatTipoRecurso cnCatTipoRecurso = new CN_CatTipoRecurso(ibt);
            CatRecurso catRecurso = new CatRecurso() { Id_Emp = sesion.Id_Emp, Id_Cd = sesion.Id_Cd, Id_TipoRecurso = cnCatTipoRecurso.Imagen.Id_TipoRecurso };
            catRecurso = cdCatRecurso.Insertar(catRecurso, ibt.DataContext);

            CD_CatRecursoArchivo cdCatRecursoArchivo = new CD_CatRecursoArchivo();
            CatRecursoArchivo catRecursoArchivo = new CatRecursoArchivo()
            {
                Id_Emp = sesion.Id_Emp,
                Id_Cd = sesion.Id_Cd,
                Id_Recurso = catRecurso.Id_Recurso,
                RecArc_Nombre = nombreArchivo,
                RecArc_Extension = extension,
                RecArc_ContentType=contentType
            };

            catRecursoArchivo = cdCatRecursoArchivo.Insertar(catRecursoArchivo, ibt.DataContext);

            CapBibliotecaNodo capBibliotecaNodo = new CapBibliotecaNodo()
            {
                Id_Emp = sesion.Id_Emp,
                Id_Cd = sesion.Id_Cd,
                Id_Recurso = catRecurso.Id_Recurso,
                Id_BiblioNodo_Padre = idBiblioNodoPadre,
                Id_Biblioteca = catBiblioteca.Id_Biblioteca,
                BiblioNodo_Nombre=catRecursoArchivo.RecArc_Nombre.Split('\\').Last()
            };

            cdCapBibliotecaNodo.Insertar(capBibliotecaNodo, ibt.DataContext);

            return catRecursoArchivo;
        }

        public CatRecursoURL CrearRecursoURLRepositorio(Sesion sesion, string url, int idBiblioNodoPadre, IBusinessTransaction ibt)
        {
            CD_CatRecurso cdCatRecurso = new CD_CatRecurso();
            CN_CatTipoRecurso cnCatTipoRecurso=new CN_CatTipoRecurso(ibt);

            CatRecurso catRecurso=new CatRecurso()
            {
                Id_Emp=sesion.Id_Emp,
                Id_Cd=sesion.Id_Cd,
                Id_TipoRecurso=cnCatTipoRecurso.URL.Id_TipoRecurso
            };
            catRecurso = cdCatRecurso.Insertar(catRecurso, ibt.DataContext);

            CD_CatRecursoURL cdCatRecursoURL = new CD_CatRecursoURL();
            ibt.Save();
            CatRecursoURL catRecursoURL=new CatRecursoURL()
            {
                Id_Emp=sesion.Id_Emp,
                Id_Cd=sesion.Id_Cd,
                Id_Recurso=catRecurso.Id_Recurso,
                Id_U=sesion.Id_U,
                RecURL_URL=url
            };
            catRecursoURL = cdCatRecursoURL.Insertar(catRecursoURL, ibt.DataContext);

            CN_CatBiblioteca cnCatBiblioteca = new CN_CatBiblioteca();
            var catBiblioteca = cnCatBiblioteca.ObtenerBibliotecaDefaultDeUsuario(sesion, ibt);

            CD_CapBibliotecaNodo cdCapBibliotecaNodo = new CD_CapBibliotecaNodo();
            var capBibliotecaNodoPadre = cdCapBibliotecaNodo.ConsultarPorIdentificador(sesion.Id_Emp, sesion.Id_Cd, sesion.Id_U, catBiblioteca.Id_Biblioteca, idBiblioNodoPadre, ibt.DataContext);
            if (capBibliotecaNodoPadre == null)
            {
                throw new InexistentCapBibliotecaNodoException(idBiblioNodoPadre);
            }

            CapBibliotecaNodo capBibliotecaNodo = new CapBibliotecaNodo()
            {
                Id_Emp = sesion.Id_Emp,
                Id_Cd = sesion.Id_Cd,
                Id_Recurso = catRecurso.Id_Recurso,
                Id_BiblioNodo_Padre = idBiblioNodoPadre,
                Id_Biblioteca = catBiblioteca.Id_Biblioteca,
                BiblioNodo_Nombre=catRecursoURL.RecURL_URL
            };

            cdCapBibliotecaNodo.Insertar(capBibliotecaNodo, ibt.DataContext);

            return catRecursoURL;
        }
    }

    /// <summary>
    /// Excepción que representa la inexistencia de la entrada para CapBibliotecaNodo con identificador id
    /// </summary>
    public class InexistentCapBibliotecaNodoException
        : Exception
    {
        /// <summary>
        /// Excepción que representa la inexistencia de la entrada para CapBibliotecaNodo con identificador id
        /// </summary>
        /// <param name="id">Identificador del nivel del repositorio</param>
        public InexistentCapBibliotecaNodoException(int id)
            : base(string.Format("El identificador {0} del repositorio no existe", id))
        { 
        }
    }
}
