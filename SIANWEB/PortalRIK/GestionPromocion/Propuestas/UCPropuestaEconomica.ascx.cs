using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaNegocios;
using CapaModelo;
using CapaEntidad;
using Newtonsoft.Json;
using SIANWEB.Core.UI;

namespace SIANWEB.PortalRIK.GestionPromocion.Propuestas
{
    public partial class UCPropuestaEconomica : BaseServerControl
    {
        public int? IdCte
        {
            get
            {
                if (_idCte == null)
                {
                    string strIdCte = Page.Request["idCte"];
                    try
                    {
                        _idCte = int.Parse(strIdCte);
                    }
                    catch (Exception ex)
                    {
                    }
                }
                return _idCte;
            }
        }

        public int? IdVal
        {
            get
            {
                if (_idVal == null)
                {
                    string strIdVal = Page.Request["idVal"];
                    try
                    {
                        _idVal = int.Parse(strIdVal);
                    }
                    catch (Exception ex)
                    {
                    }
                }
                return _idVal;
            }
        }

        public IEnumerable<CrmPropuestaTecnica> DetallePropuesta
        {
            get
            {
                if (_DetallePropuesta == null)
                {
                    if (_parametrosConsultaDetalleValidos)
                    {
                        CN_CrmPropuestaTecnica cnCrmPropuestaTecnica = new CN_CrmPropuestaTecnica();
                        _DetallePropuesta = cnCrmPropuestaTecnica.ObtenerReportePropuestaTecnica(EntidadSesion, IdCte.Value, IdVal.Value);
                    }
                }
                return _DetallePropuesta;
            }
        }

        public IEnumerable<CrmOportunidadesProducto> DetallePropuestaEconomica
        {
            get
            {
                if (_DetallePropuestaEconomica == null)
                {
                    if (_parametrosConsultaDetalleValidos)
                    {
                        CN_CrmOportunidadesProductos cnCrmOportunidadesProductos = new CN_CrmOportunidadesProductos();
                        _DetallePropuestaEconomica = cnCrmOportunidadesProductos.ObtenerPropuestaEconomica(EntidadSesion, IdVal.Value, IdCte.Value);
                    }
                }
                return _DetallePropuestaEconomica;
            }
        }

        public string DetallePropuestaSerializado
        {
            get
            {
                return JsonConvert.SerializeObject(DetallePropuesta);
            }
        }

        public string DetallePropuestaEconomicaSerializado
        {
            get
            {
                return JsonConvert.SerializeObject(DetallePropuestaEconomica);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //validar si la valuación se encuentra en un estado válido para la generación de las propuestas
            _parametrosConsultaDetalleValidos = IdCte != null && IdVal != null;
            if (!_parametrosConsultaDetalleValidos)
            {
                //falta: arrojar excepción
            }
        }

        protected IEnumerable<CrmPropuestaTecnica> _DetallePropuesta = null;
        protected IEnumerable<CrmOportunidadesProducto> _DetallePropuestaEconomica = null;
        protected int? _idCte = null;
        protected int? _idVal = null;
        protected bool _parametrosConsultaDetalleValidos = false;
    }
}