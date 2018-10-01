using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using CapaNegocios;
using CapaEntidad;
using System.Xml.Serialization;
using System.IO;

namespace WS_TransferenciasElectronicas
{
    /// <summary>
    /// Summary description for Service1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class Service1 : System.Web.Services.WebService
    {

        [WebMethod]
        public string GuardarTransferenciaExterna(string transferencia_str,  string list_transferencias_str, string Emp_CnxCen, string Emp_Cnx)
        {
            try
            {
                int verificador = 0;

                CN_CapTransferencia clsTransferencia = new CN_CapTransferencia();
                clsTransferencia.InsertarTransferencia(transferencia_str, list_transferencias_str, Emp_CnxCen, Emp_Cnx, ref verificador);

                return verificador.ToString();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
