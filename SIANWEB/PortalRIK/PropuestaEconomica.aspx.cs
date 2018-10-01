using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaNegocios;
using CapaModelo;
using CapaEntidad;
using SIANWEB.MasterPage;
using Telerik.Web.UI;

namespace SIANWEB.PortalRIK
{
    public partial class PropuestaEconomica : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if(Id_Cte!=null && Id_Val!= null)
                {
                    CargarListadoProductos();
                    rgPropuesta.DataBind();
                }
            }
        }

        protected void CargarListadoProductos()
        {
            CN_CrmPropuestaEconomica cnCrmPropuestaEconomica = new CN_CrmPropuestaEconomica();
            var productosPropuesta = cnCrmPropuestaEconomica.ObtenerPorValuacion(Sesion, Id_Cte.Value, Id_Val.Value);
            RgProductosPropuestaDataSource = productosPropuesta;
            rgPropuesta.DataSource = RgProductosPropuestaDataSource;
        }

        public Sesion Sesion
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

        protected void rgPropuesta_UpdateCommand(object sender, GridCommandEventArgs e)
        {
            GridEditableItem item = (GridEditableItem)e.Item;
            Hashtable newValues=new Hashtable();
            e.Item.OwnerTableView.ExtractValuesFromItem(newValues, item);
            int idPrd = (int)item.OwnerTableView.DataKeyValues[item.ItemIndex]["Id_Prd"];
            var elementos = (from cpe in RgProductosPropuestaDataSource
                             where cpe.Id_Prd == idPrd
                             select cpe).ToList();
            if (elementos.Count > 0)
            {
                CrmPropuestaEconomica elemento = elementos[0];
                elemento.PropEc_CostoUso = newValues["PropEc_CostoUso"].ToString();
                elemento.PropEc_Dilucion = newValues["PropEc_Dilucion"].ToString();
            }
        }

        protected void rgPropuesta_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                rgPropuesta.DataSource = RgProductosPropuestaDataSource;
            }
            catch (Exception ex)
            {
                
            }
        }

        public int? Id_Cte
        {
            get
            {
                if (_idCte == null)
                {
                    string idCteStr = Request["Id_Cte"];
                    if (idCteStr != null)
                    {
                        try
                        {
                            _idCte = int.Parse(idCteStr);
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                }
                return _idCte;
            }
        }

        public int? Id_Val
        {
            get
            {
                if (_idVal == null)
                {
                    string idValStr = Request["Id_Val"];
                    if (idValStr != null)
                    {
                        try
                        {
                            _idVal = int.Parse(idValStr);
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                }
                return _idVal;
            }
        }

        public IEnumerable<CrmPropuestaEconomica> RgProductosPropuestaDataSource
        {
            get
            {
                return (IEnumerable<CrmPropuestaEconomica>)ViewState["_rgProductosPropuestaDataSource"];
            }
            set
            {
                ViewState["_rgProductosPropuestaDataSource"] = value;
            }
        }

        private int? _idCte=null;
        private int? _idVal = null;
    }
}