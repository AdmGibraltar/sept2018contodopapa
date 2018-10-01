using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaDatos;
using CapaEntidad;

namespace CapaNegocios
{
    public class CN_GestorCobranza
    {


        public void ConsultarBitacora(CapaEntidad.Cobranza cob, ref string bitacora, string Conexion)
        {
            try
            {
                CD_GestorCobranza cd_cob = new CD_GestorCobranza();
                cd_cob.ConsultarBitacora(cob, ref bitacora, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarAcciones(CapaEntidad.Cobranza cob, ref List<CapaEntidad.Pregunta> list, string Conexion)
        {
            try
            {
                CD_GestorCobranza cd_cob = new CD_GestorCobranza();
                cd_cob.ConsultarBitacora(cob, ref list, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarDocumentos(CapaEntidad.Cobranza cob, ref System.Data.DataSet ds, string Conexion)
        {
            try
            {
                CD_GestorCobranza cd_cob = new CD_GestorCobranza();
                cd_cob.ConsultarDocumentos(cob, ref ds, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertarBitacora(CapaEntidad.Bitacora cob, List<CapaEntidad.Pregunta> list_preg, ref int verificador, string Conexion)
        {
            try
            {
                CD_GestorCobranza cd_cob = new CD_GestorCobranza();
                cd_cob.InsertarBitacora(cob, list_preg, ref verificador, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarRelaciones(CapaEntidad.Cobranza cob, ref List<CapaEntidad.RelacionGestor> list, string Conexion)
        {
            try
            {
                CD_GestorCobranza cd_cob = new CD_GestorCobranza();
                cd_cob.ConsultarRelaciones(cob, ref list, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConfirmarRevision(FacturaRevisionCobro FacturaRevisionCobro, ref int verificador, string Conexion, string dbname)
        {
            try
            {
                CD_GestorCobranza cd_cob = new CD_GestorCobranza();
                cd_cob.ConfirmarRevision(FacturaRevisionCobro, ref verificador, Conexion, dbname);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConfirmarRecibidoSvtas(CapaEntidad.Cobranza cob, ref int verificador, string Conexion)
        {
            try
            {
                CD_GestorCobranza cd_cob = new CD_GestorCobranza();
                cd_cob.ConfirmarRecibidoSvtas(cob, ref verificador, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GraficaEntrega(ref List<object> list, CapaEntidad.Usuario usu, string Conexion)
        {
            try
            {
                CD_GestorCobranza cd_cob = new CD_GestorCobranza();
                cd_cob.GraficaEntrega(ref list, usu, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void GraficaEntrega_Saldos(ref List<double> list, CapaEntidad.Usuario usu, string Conexion)
        {
            try
            {
                CD_GestorCobranza cd_cob = new CD_GestorCobranza();
                cd_cob.GraficaEntrega_Saldos(ref list, usu, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public void GraficaCobranza(ref List<object> list, CapaEntidad.Usuario usu, string Conexion)
        {
            try
            {
                CD_GestorCobranza cd_cob = new CD_GestorCobranza();
                cd_cob.GraficaCobranza(ref list, usu, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void GraficaCobranza_Saldos(ref List<double> list, CapaEntidad.Usuario usu, string Conexion)
        {
            try
            {
                CD_GestorCobranza cd_cob = new CD_GestorCobranza();
                cd_cob.GraficaCobranza_Saldos(ref list, usu, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public void GraficaRevision(ref List<object> list, CapaEntidad.Usuario usu, string Conexion)
        {
            try
            {
                CD_GestorCobranza cd_cob = new CD_GestorCobranza();
                cd_cob.GraficaRevision(ref list, usu, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void GraficaRevision_Saldos(ref List<double> list, CapaEntidad.Usuario usu, string Conexion)
        {
            try
            {
                CD_GestorCobranza cd_cob = new CD_GestorCobranza();
                cd_cob.GraficaRevision_Saldos(ref list, usu, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
       
        public void GraficaVencidas(ref List<object> list, CapaEntidad.Usuario usu, string Conexion)
        {
            try
            {
                CD_GestorCobranza cd_cob = new CD_GestorCobranza();
                cd_cob.GraficaVencidas(ref list, usu, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void GraficaVencidas_Saldos(ref List<double> list, CapaEntidad.Usuario usu, string Conexion)
        {
            try
            {
                CD_GestorCobranza cd_cob = new CD_GestorCobranza();
                cd_cob.GraficaVencidas_Saldos(ref list, usu, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
       
        public void GraficaDiasVencidos(ref List<Comun> list, CapaEntidad.Usuario usu, string Conexion)
        {
            try
            {
                CD_GestorCobranza cd_cob = new CD_GestorCobranza();
                cd_cob.GraficaDiasVencidos(ref list, usu, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void GraficaCosto(ref List<Comun> list, Usuario usu, string Conexion)
        {
            try
            {
                CD_GestorCobranza cd_cob = new CD_GestorCobranza();
                cd_cob.GraficaCosto(ref list, usu, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void GraficaRotacion(ref List<Comun> list, CapaEntidad.Usuario usu, string Conexion)
        {
            try
            {
                CD_GestorCobranza cd_cob = new CD_GestorCobranza();
                cd_cob.GraficaRotacion(ref list, usu, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
