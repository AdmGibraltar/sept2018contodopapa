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

using System.IO;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Activation;
using System.Web.Script.Serialization;
using System.Configuration;

using System.Net;
using System.Web.Http;



namespace SIANWEB.WebService.Recursos
{
    public class RepositorioCrearRecursoArchivoController
        : BaseWebAPIController
    {

        /// <summary>
        /// 
        /// 
        /// 
        /// </summary>
        /// <returns>Task<HttpResponseMessage></returns>
        /// 
        
        [HttpPost]        
        public Task<HttpResponseMessage> Post()
        {
            int IdDocumento = 0;
            int IdDocTipo = 0;
            string CadenaHash = "";
            string NewName = "";
            string Extencion = "";

            try
            {
                // Check if the request contains multipart/form-data.
                if (!Request.Content.IsMimeMultipartContent())
                {
                    throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
                }
                string root = HttpContext.Current.Server.MapPath("/imgupload");
                var provider = new MultipartFormDataStreamProvider(root);

                var httpRequest = HttpContext.Current.Request;
                if (httpRequest.Files.Count > 0)
                {
                    var docfiles = new List<string>();
                    foreach (string file in httpRequest.Files)
                    {                        
                        var postedFile = httpRequest.Files[file];

                        if (postedFile.ContentType == "image/jpg" || postedFile.ContentType == "image/jpeg")
                        {
                            Extencion = ".jpg";
                        }
                        if (postedFile.ContentType == "image/png") {
                            Extencion = ".png";
                        }

                        if (postedFile.ContentType != "image/png" && postedFile.ContentType != "image/jpeg")
                        {
                            return Task<HttpResponseMessage>.Factory.StartNew(
                                () => Request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, new HttpError("El formato del archivo no es soportado.")));       
                        }

                        var filePath = HttpContext.Current.Server.MapPath("~/imgupload/" + postedFile.FileName);
                        postedFile.SaveAs(filePath);
                        docfiles.Add(filePath);

                        // Crear el archivo en CRMArchivosCargados
                        CN_CRMArchivosCargados cnCRM = new CN_CRMArchivosCargados();
                        eCRMArchivosCargados eCRMAC = new eCRMArchivosCargados();
                        int iEstatus = 0;
                        CadenaHash = cnCRM.CrearCadenaHash() + Extencion;

                        NewName = HttpContext.Current.Server.MapPath("~/imgupload/" + CadenaHash );
                        //var filePath = HttpContext.Current.Server.MapPath("~/imgupload/" + CadenaHash);
                        iEstatus = cnCRM.Crear(Sesion, IdDocumento, IdDocTipo, filePath, NewName, Sesion.Id_U);

                        // exito
                        if (iEstatus == 1)
                        {
                            // renombra el archivo con el hash.                            
                            System.IO.File.Move(filePath, NewName);
                        }
                    }

                   /* return Task<HttpResponseMessage>.Factory.StartNew(
                        () => Request.CreateErrorResponse(System.Net.HttpStatusCode.Created, new HttpError(NewName))
                    );
                    */
                    var t = Request.Content.ReadAsMultipartAsync(provider).ContinueWith(o =>
                    {
                        return Request.CreateResponse(System.Net.HttpStatusCode.OK, new
                        {
                            Estatus = 1,
                            Url = "",
                            idRecurso = 0,
                            nombreArchivo = NewName,
                            Hash= CadenaHash
                        });
                    });
                   
                    return t;
                }
                else
                {                
                    return Task<HttpResponseMessage>.Factory.StartNew(
                        () => Request.CreateErrorResponse(System.Net.HttpStatusCode.NoContent, new HttpError("Ok")));       
                }
                //return Task<HttpResponseMessage>.Factory.StartNew(()=> Request.CreateErrorResponse(System.Net.HttpStatusCode.BadRequest, new HttpError("Ok")));

            }
            catch (Exception e)
            {
                return Task<HttpResponseMessage>.Factory.StartNew(
                        () => Request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, new HttpError("Error")));       
            }
        }
          

        /*
        [HttpPost]
        public Task<HttpResponseMessage> Post()
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                return Task<HttpResponseMessage>.Factory.StartNew(()=> Request.CreateErrorResponse(System.Net.HttpStatusCode.BadRequest, new HttpError("Tipo de contenido no soportado para la operación")));
            }
            var valorIdBiblioNodoPadre = Request.GetQueryNameValuePairs().Where(kvp => kvp.Key.CompareTo("idBiblioNodoPadre") == 0);
            if (valorIdBiblioNodoPadre.Count() == 0)
            {
                return Task<HttpResponseMessage>.Factory.StartNew(()=> Request.CreateErrorResponse(System.Net.HttpStatusCode.BadRequest, new HttpError("El nivel del repositorio del recurso no ha sido especificado (idBiblioNodoPadre)")));
            }
            string idBiblioNodoPadreValue = valorIdBiblioNodoPadre.First().Value;
            int idBiblioNodoPadre = 0;
            try
            {
                idBiblioNodoPadre = int.Parse(idBiblioNodoPadreValue);
            }
            catch (Exception ex)
            {
                return Task<HttpResponseMessage>.Factory.StartNew(()=> Request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, new HttpError("El nivel del repositorio del recurso no tiene un formato valido (idBiblioNodoPadre debe ser entero)")));
            }
            
            try
            {
                //string root = "c:\\temp\\img";
                //string root = Server.MapPath("/imgupload");
                string root = HttpContext.Current.Server.MapPath("/imgupload");
                var currentContext = HttpContext.Current;
                var provider = new MultipartFormDataStreamProvider(root);
                var t = Request.Content.ReadAsMultipartAsync(provider).ContinueWith(o =>
                    {
                        HttpContext.Current = currentContext;
                        using (IBusinessTransaction ibt = CN_FabricaTransaccionNegocios.Default(Sesion))
                        {
                            ibt.Begin();
                            var extension = o.Result.Contents.First().Headers.ContentDisposition.FileName.Split('.').Last();
                            extension = extension.Substring(0, extension.Length - 1);
                            CN_RepositorioRecursos cnRepositorioRecursos = new CN_RepositorioRecursos();
                            string nombreArchivo = provider.FileData.First().LocalFileName;
                            var catRecurso = cnRepositorioRecursos.CrearRecursoImagenRepositorio(Sesion, nombreArchivo, extension, string.Empty/*HttpContext.Current.Request.ContentType*, idBiblioNodoPadre, ibt);
                            ibt.Save();
                            ibt.Commit();
                            var nombreArchivoConExtension = catRecurso.RecArc_Nombre.Split('\\').Last();
                            if (catRecurso.RecArc_Extension != null)
                            {
                                if (catRecurso.RecArc_Extension.Length > 0)
                                {
                                    nombreArchivoConExtension = string.Format("{0}.{1}", catRecurso.RecArc_Nombre.Split('\\').Last(), catRecurso.RecArc_Extension);
                                }
                            }
                            return Request.CreateResponse(System.Net.HttpStatusCode.OK, new
                            {
                                url = string.Format("{0}/api/RepositorioObtenerRecursoArchivo?idRecurso={1}&idU={2}&idEmp={3}&idCd={4}", ApplicationUrl, catRecurso.Id_Recurso, Sesion.Id_U, Sesion.Id_Emp, Sesion.Id_Cd),
                                idRecurso = catRecurso.Id_Recurso,
                                nombreArchivo = nombreArchivoConExtension
                            });
                        }
                    });
                return t;
            }
            catch (Exception ex)
            {
                Logger.Error("RepositorioCrearRecursoArchivoController::Post", ex);
                return Task<HttpResponseMessage>.Factory.StartNew(()=> Request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, ex));
            }
        }
        
        */
        //

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
