using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaModelo;
using CapaDatos;
using CapaEntidad;

namespace CapaNegocios
{
    public class CN_CRMDinamo
    {
        private IBusinessTransaction _ibt=null;

        /*
        public CN_CRMDinamo(IBusinessTransaction ibt)
        {
            _ibt = ibt;
        }
        */

        public IEnumerable<CapaEntidad.EntradaCDReporteDinamo> VistaGeneral(Sesion s, int cdTipo,int iTipoRik, int Anio, int Mes, bool bTrimestral, /*IBusinessTransaction ibt,*/ string Conexion)
        {
            List<CapaEntidad.EntradaCDReporteDinamo> lst = new List<CapaEntidad.EntradaCDReporteDinamo>();
            CD_CRMDinamo CRMDinamo = new CD_CRMDinamo();
            lst = CRMDinamo.spRepCRM_Consolidado(cdTipo, Anio, Mes, iTipoRik, s.Id_U, Conexion);                        
            return lst;            
        }

        public IEnumerable<CapaEntidad.EntradaCDReporteDinamo> VistaGeneralTrimestral(Sesion s, int cdTipo, int iTipoRik, int Anio, int Mes, bool bTrimestral, /*IBusinessTransaction ibt,*/ string Conexion)
        {
            List<CapaEntidad.EntradaCDReporteDinamo> lst = new List<CapaEntidad.EntradaCDReporteDinamo>();
            CD_CRMDinamo CRMDinamo = new CD_CRMDinamo();
            lst = CRMDinamo.spRepCRM_ConsolidadoTrimestral(cdTipo, Anio, Mes, iTipoRik, s.Id_U, Conexion);
            return lst;
        }

        public IEnumerable<CapaEntidad.EntradaCDReporteDinamo> spRepCRMC_onsolidadoRik(Sesion s, int cdTipo, int iTipoRik, int Anio, int Mes, bool bTrimestral, /*IBusinessTransaction ibt,*/ string Conexion)
        {
            List<CapaEntidad.EntradaCDReporteDinamo> lst = new List<CapaEntidad.EntradaCDReporteDinamo>();
            CD_CRMDinamo CRMDinamo = new CD_CRMDinamo();
            lst = CRMDinamo.spRepCRMC_onsolidadoRik(cdTipo, Anio, Mes, iTipoRik, s.Id_U, Conexion);
            return lst;
        }

        //
    }
}
