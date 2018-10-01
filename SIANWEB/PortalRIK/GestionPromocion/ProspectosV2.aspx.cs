using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaEntidad;
using CapaModelo;
using CapaNegocios;
using SIANWEB.Core.UI;

namespace SIANWEB.PortalRIK.GestionPromocion
{
    public partial class ProspectosV2 : BaseServerPage
    {        
        public int Parametro_IdTU; // Tipo Usaurio 3.- Gerente         
        public int Parametro_IdRik; // Representante Institucional Key RIK , para recibir parametro 
        public string Parametro_Nombre; // Nombre Gerente       
        // Gerente
        public int CRM_Gerente_Id;
        public int CRM_Gerente_Rik;
        public string CRM_Gerente_Nombre;
        // Usuario 
        public int CRM_Usuario_Id;
        public int CRM_Usuario_Rik;
        public string CRM_Usuario_Nombre;
                
        protected void Page_Load(object sender, EventArgs e)
        {
            SIANWEB.MasterPage.PortalRIK mp = Master as SIANWEB.MasterPage.PortalRIK;
            mp.CurrentPath = new List<string>() { "Gestion de la Promoción", "Prospectos" }.ToArray();

            // Si el URL incluye parametros.
            try
            {
                Int32.TryParse(Request.QueryString["Id_Rik"].ToString(), out Parametro_IdRik);
            }
            catch (Exception ex)
            {
                Parametro_IdRik = 0;
            }
            // Si viene el RIK en los parametos 
            if (Parametro_IdRik > 0)
            {
                // Si viene Rik debe venir el nombre 
                try
                {
                    Parametro_Nombre = Request.QueryString["Rik_Nombre"].ToString();
                }
                catch (Exception ex)
                {
                    Parametro_Nombre = "";
                }
            }            
            
            if (!IsPostBack)
            {
                Parametro_IdTU = session.Id_TU;
                // Es perfil gerente ?
                if (Parametro_IdTU == 3)
                {
                    // Si es Gerente
                    // Llena valores de gerente
                    CRM_Gerente_Id = session.Id_U;
                    CRM_Gerente_Rik = session.Id_Rik;
                    CRM_Gerente_Nombre = session.U_Nombre;
                    // Llena valores de usuario
                    CRM_Usuario_Id = session.Id_U;
                    CRM_Usuario_Rik = session.Id_Rik;
                    CRM_Usuario_Nombre = session.U_Nombre;
                    // Si viene el RIK en los parametos 
                    if (Parametro_IdRik > 0)
                    {
                        CRM_Usuario_Rik = Parametro_IdRik;
                        CRM_Usuario_Nombre = Parametro_Nombre;
                    }
                }
                else
                {
                    // No es Gerente
                    // 
                    CRM_Gerente_Id = 0;
                    CRM_Gerente_Rik = 0;
                    CRM_Gerente_Nombre = "";
                    //
                    CRM_Usuario_Id = session.Id_U;
                    CRM_Usuario_Rik = session.Id_Rik;
                    CRM_Usuario_Nombre = session.U_Nombre;
                }
                
                Session["activeMenu"] = 1;
                try
                {
                    rptTerritoriosProspecto.DataSource = TerritoriosDeRIK;
                    rptTerritoriosProspecto.DataBind();
                    rptTerritoriosProspectoEditar.DataSource = TerritoriosDeRIK;
                    rptTerritoriosProspectoEditar.DataBind();
                }
                catch (Exception ex)
                {
                }
            }
        }

        protected IEnumerable<CatTerritorio> TerritoriosDeRIK
        {
            get
            {
                if (_territoriosDeRIK == null)
                {
                    CN_CatTerritorios cnCatTerritorios = new CN_CatTerritorios();
                    _territoriosDeRIK = cnCatTerritorios.ObtenerTerritoriosPorRik(EntidadSesion.Id_Emp, EntidadSesion.Id_Cd, EntidadSesion.Id_Rik, EntidadSesion);
                }
                return _territoriosDeRIK;
            }
        }

        protected string TerritoriosDeRIKComoJson
        {
            get
            {
                return String.Join(",", TerritoriosDeRIK.Select(ct => String.Format("{{Id_Ter: {0}, Ter_Nombre: '{1}'}}", ct.Id_Ter, ct.Ter_Nombre)).ToArray());
            }
        }

        private IEnumerable<CatTerritorio> _territoriosDeRIK=null;

        public Sesion session
        {
            get
            {
                return (Sesion)Session["Sesion" + Session.SessionID];
            }
            set
            {
                Session["Sesion" + Session.SessionID] = value;

            }
        }
        //
    }
}