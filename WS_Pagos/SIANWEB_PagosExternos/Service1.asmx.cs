using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using CapaNegocios;
using CapaEntidad;
using System.Xml.Serialization;
using System.IO;

namespace SIANWEB_PagosExternos
{
    /// <summary>
    /// Este WebServices guarda pagos externos
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio Web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class Service1 : System.Web.Services.WebService
    {
        [WebMethod]
        public string GuardarPagoExterno(string pago_str, string list_fichas_str, string list_pagos_str, string Emp_CnxCob, string Emp_Cnx, int cd_tipo)
        {
            try
            {
                int verificador = 0;

                CN_CapPago clsCapPago = new CN_CapPago();
                clsCapPago.InsertarPago(pago_str, list_fichas_str, list_pagos_str, Emp_CnxCob, Emp_Cnx, ref verificador, cd_tipo);

                return verificador.ToString();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        [WebMethod]
        public string CancelarPagoExterno(string pago_str, string Emp_CnxCob, string Emp_Cnx, int? Id_CdExt, int cd_tipo)
        {
            try
            {
                int verificador = 0;

                CN_CapPago clsCapPago = new CN_CapPago();
                clsCapPago.Baja(pago_str, Emp_CnxCob, Emp_Cnx, ref verificador, Id_CdExt, cd_tipo);

                return verificador.ToString();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        [WebMethod]
        public string ModificarPagoExterno(string pago_str, string list_fichas_str, string list_pagos_str, string Emp_CnxCob, string Emp_Cnx,int cd_tipo)
        {
            try
            {
                int verificador = 0;

                CN_CapPago clsCapPago = new CN_CapPago();
                clsCapPago.ModificarPago(pago_str, list_fichas_str, list_pagos_str, Emp_CnxCob, Emp_Cnx, ref verificador, cd_tipo);

                return verificador.ToString();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}