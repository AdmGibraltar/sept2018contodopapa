using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SIANWEB.Core.UI;

namespace SIANWEB.Controles.Cliente.DialogoRecurso
{
    public partial class UCDialogoRecurso : BaseServerControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Devuelve la cadena del identificador del elemento que representa el área de arrastre.
        /// </summary>
        public string AreaArrastreClientID
        {
            get
            {
                return string.Format("{0}_{1}", ClientID, "areaArrastre");
            }
        }

        /// <summary>
        /// Devuelve la cadena del identificador del elemento que representa el comando de aceptación.
        /// </summary>
        public string ComandoAceptarId
        {
            get
            {
                return string.Format("{0}_{1}", ClientID, "comandoAceptar");
            }
        }

        /// <summary>
        /// Devuelve la cadena del identificador del elemento que representa el comando de búsqueda y selección de archivos.
        /// </summary>
        public string ComandoArchivosId
        {
            get
            {
                return string.Format("{0}_{1}", ClientID, "archivo");
            }
        }

        /// <summary>
        /// Devuelve la cadena del identificador del elemento que representa el campo URL.
        /// </summary>
        public string CampoURLId
        {
            get
            {
                return string.Format("{0}_{1}", ClientID, "campoUrl");
            }
        }

        /// <summary>
        /// Devuelve la cadena del identificador del elemento que representa el comando de búsqueda y selección de archivos.
        /// </summary>
        public string BarraProgresoTransferenciaArchivoId
        {
            get
            {
                return string.Format("{0}_{1}", ClientID, "barraProgresoTransferenciaArchivo");
            }
        }

        /// <summary>
        /// Devuelve la cadena del identificador del elemento que representa el menú "Nuevo Recurso" de navegación del diálogo
        /// </summary>
        public string MenuNavNuevoRecursoId
        {
            get
            {
                return string.Format("{0}_{1}", ClientID, "menuNavNuevoRecurso");
            }
        }

        /// <summary>
        /// Devuelve la cadena del identificador del elemento que representa el menú "Repositorio" de navegación del diálogo
        /// </summary>
        public string MenuNavRepositorioId
        {
            get
            {
                return string.Format("{0}_{1}", ClientID, "menuNavRepositorio");
            }
        }

        /// <summary>
        /// Devuelve la cadena del identificador del elemento que representa el elemento de acordeon "URL".
        /// </summary>
        public string AcordeonElementoURL
        {
            get
            {
                return string.Format("{0}_{1}", ClientID, "acordeonElementoURL");
            }
        }

        /// <summary>
        /// Devuelve la cadena del identificador del elemento que representa el elemento de acordeon "Archivo"
        /// </summary>
        public string AcordeonElementoArchivo
        {
            get
            {
                return string.Format("{0}_{1}", ClientID, "acordeonElementoArchivo");
            }
        }
    }
}