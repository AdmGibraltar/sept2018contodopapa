using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SIANWEB.Core.UI;
using CapaNegocios;
using CapaModelo;

namespace SIANWEB.PortalRIK.Administracion.MapaDeOferta.Aplicaciones
{
    public partial class Listado 
        : BaseServerPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InicializarListadoUens();
            }
        }

        /// <summary>
        /// Inicializa el control de listado de unidades de negocio
        /// </summary>
        protected void InicializarListadoUens()
        {
            rptUen.DataSource = UEns;
            rptUen.DataBind();
        }

        /// <summary>
        /// Listado de unidades de negocio de la empresa
        /// </summary>
        protected IEnumerable<CatUEN> UEns
        {
            get
            {
                if (_UEns == null)
                {
                    _UEns = CargarUEns();
                }
                return _UEns;
            }
        }

        /// <summary>
        /// Regresa el listado de unidades de negocio de la empresa
        /// </summary>
        /// <returns></returns>
        protected IEnumerable<CatUEN> CargarUEns()
        {
            CN_CatUen cnCatUen = new CN_CatUen();
            return cnCatUen.ObtenerUEnsDeEmpresa(EntidadSesion, BusinessTransaction);
        }

        private IEnumerable<CatUEN> _UEns = null;
    }
}