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
using System.IO;
using SIANWEB.Core.Web;

namespace SIANWEB.WebService.Recursos
{
    public class RepositorioObtenerRecursoArchivoController
        : BaseWebAPIController
    {
        /// <summary>
        /// Sirve un recurso de imágen del repositorio dado el identificador del recurso
        /// </summary>
        /// <param name="idRecurso">Identificador del recurso</param>
        /// <param name="idU">Identificador del usuario dueño del recurso</param>
        /// <param name="idEmp">Identificador de la empresa</param>
        /// <param name="idCd">Identificador del centro de distribución</param>
        /// <returns>Task<HttpResponseMessage></returns>
        [HttpGet]
        public HttpResponseMessage Get(int idRecurso, int idU, int idEmp, int idCd)
        {
            try
            {
                using (IBusinessTransaction ibt = CN_FabricaTransaccionNegocios.Default(Sesion))
                {
                    ibt.Begin();
                    CN_CatRecursoArchivo cnCatRecursoArchivo = new CN_CatRecursoArchivo();
                    var fs = cnCatRecursoArchivo.AbrirArchivo(Sesion, idRecurso, ibt);

                    StreamContent sc = new StreamContent(fs.Fs);
                    var dummyName = string.Format("1.{0}", fs.CatRecursoArchivo.RecArc_Extension);

                    HttpResponseMessage response = Request.CreateResponse(System.Net.HttpStatusCode.OK);
                    sc.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(MimeExtensionHelper.GetMimeType(dummyName));
                    response.Content = sc;
                    return response;
                }
            }
            catch (Exception ex)
            {
                Logger.Error("RepositorioObtenerRecursoArchivoController::Get", ex);
                return Request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, ex);//Task<HttpResponseMessage>.Factory.StartNew(() => Request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, ex));
            }
        }
    }
}