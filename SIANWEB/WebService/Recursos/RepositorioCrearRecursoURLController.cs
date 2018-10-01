using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using CapaModelo;
using CapaNegocios;
using CapaEntidad;
using System.Net.Http;
using SIANWEB.Core.Web.API;
using System.Threading.Tasks;
using SIANWEB.Core.Web;
using CapaModelo;

namespace SIANWEB.WebService.Recursos
{
    public class RepositorioCrearRecursoURLController
        : BaseWebAPIController
    {
        /// <summary>
        /// Operación Post del controlador RepositorioCrearRecursoURL: crea una entrada de recurso de tipo URL, y crea una entrada para asociar dicho recurso en el repositorio especificado (idbiblioNodoPadre)
        /// </summary>
        /// <returns>Task<HttpResponseMessage></returns>
        [HttpPost]
        public Task<HttpResponseMessage> Post([FromBody]RepositorioCrearRecursoURLController_PostModel model)
        {
            try
            {
                var hctx = HttpContext.Current;
                var t = Task<HttpResponseMessage>.Factory.StartNew(() =>
                {
                    HttpContext.Current = hctx;
                    try
                    {
                        using (IBusinessTransaction ibt = CN_FabricaTransaccionNegocios.Default(Sesion))
                        {
                            ibt.Begin();
                            CN_RepositorioRecursos cnRepositorioRecursos = new CN_RepositorioRecursos();
                            var resultado = cnRepositorioRecursos.CrearRecursoURLRepositorio(Sesion, model.Url, model.IdBibliotecaNodoPadre, ibt);
                            ibt.Commit();
                            return Request.CreateResponse(System.Net.HttpStatusCode.OK, new
                            {
                                url = string.Format("{0}", resultado.RecURL_URL),
                                idRecurso = resultado.Id_Recurso
                            });
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.Error("RepositorioCrearRecursoURLController::Post", ex);
                        return Request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, ex);
                    }
                    
                });
                return t;
            }
            catch (Exception ex)
            {
                Logger.Error("RepositorioCrearRecursoURLController::Post", ex);
                return Task<HttpResponseMessage>.Factory.StartNew(() => Request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, ex));
            }
            
        }

        /// <summary>
        /// Modelo para la operación POST del controlador RepositorioCrearRecursoURLController 
        /// </summary>
        public class RepositorioCrearRecursoURLController_PostModel
        {
            /// <summary>
            /// Ubicación del recurso
            /// </summary>
            public string Url
            {
                get;
                set;
            }

            /// <summary>
            /// Identificador del nodo repositorio del recurso
            /// </summary>
            public int IdBibliotecaNodoPadre
            {
                get;
                set;
            }
        }
    }
}