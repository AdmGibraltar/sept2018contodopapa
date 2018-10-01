using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaModelo;
using CapaDatos;
using CapaEntidad;

namespace CapaNegocios
{
    public class CN_CatTipoRecurso
    {
        public CN_CatTipoRecurso()
        {
        }

        /// <summary>
        /// Constructor de la clase CN_CatTipoRecurso
        /// </summary>
        /// <param name="icdCtx">Contexto de conexión a la fuente de datos</param>
        public CN_CatTipoRecurso(IBusinessTransaction ibt)
        {
            _ibt = ibt;
        }

        /// <summary>
        /// Representa la instancia de datos para la entrada Imagen del repositorio CatTipoRecurso
        /// </summary>
        public CatTipoRecurso Imagen
        {
            get
            {
                if (_Imagen == null)
                {
                    CD_CatTipoRecurso cdCatTipoRecurso = new CD_CatTipoRecurso();
                    _Imagen = cdCatTipoRecurso.ConsultarPorIdNombre("Imagen", _ibt.DataContext);
                }
                return _Imagen;
            }
        }

        /// <summary>
        /// Representa la instancia de datos para la entrada URL del repositorio CatTipoRecurso
        /// </summary>
        public CatTipoRecurso URL
        {
            get
            {
                if (_URL == null)
                {
                    CD_CatTipoRecurso cdCatTipoRecurso = new CD_CatTipoRecurso();
                    _URL = cdCatTipoRecurso.ConsultarPorIdNombre("Enlace URL", _ibt.DataContext);
                }
                return _URL;
            }
        }

        private IBusinessTransaction _ibt = null;
        private CatTipoRecurso _Imagen = null;
        private CatTipoRecurso _URL = null;
    }
}
