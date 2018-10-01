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

namespace SIANWEB.js
{
    public partial class UCAsociacionProspectoTerritorio_js : BaseServerControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

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
                return String.Join(",", TerritoriosDeRIK.Select(ct => String.Format("{{Id_Ter: {0}, Ter_Nombre: '{1}', Id_Rik: '{2}', Id_Seg: '{3}'}}", ct.Id_Ter, ct.Ter_Nombre, ct.Id_Rik, ct.Id_Seg)).ToArray());
            }
        }

        private IEnumerable<CatTerritorio> _territoriosDeRIK = null;
    }
}