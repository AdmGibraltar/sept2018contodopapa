using System;
using System.Collections.Generic;
using System.Text;
using CapaDatos;
using CapaEntidad;
using System.Data;

namespace CapaNegocios
{
    public class CN_TransferenciasAlm
    {
        public void CapRemision_ConsultaTransferencia(TransferenciasAlm TA, ref List<Remision> List, string Conexion)
        {
            try
            {
                CD_TransferenciasAlm cd_ta = new CD_TransferenciasAlm();
                cd_ta.CapRemision_ConsultaTransferencia(TA, ref List, Conexion);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void CapRemision_ConsultaTransferenciaImprimir(TransferenciasAlm TA, ref DataTable dt, string Conexion)
        {
            try
            {
                CD_TransferenciasAlm cd_ta = new CD_TransferenciasAlm();
                cd_ta.CapRemision_ConsultaTransferenciaImprimir(TA, ref dt, Conexion);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void ProTransferenciaAlmacen_Insertar(Remision remision, ref int Verificador, string Conexion)
        {
            try
            {
                CD_TransferenciasAlm cd_ta = new CD_TransferenciasAlm();
                cd_ta.ProTransferenciaAlmacen_Insertar(remision, ref Verificador, Conexion);

            }
            catch (Exception ex)
            {
    
                throw ex;
            }
        }
        public void ProTransferenciaAlmacen_ConsultaLista(TransferenciasAlm TA, ref List<TransferenciasAlm> List, string Conexion)
        {
            try
            {
                CD_TransferenciasAlm cd_ta = new CD_TransferenciasAlm();
                cd_ta.ProTransferenciaAlmacen_ConsultaLista(TA, ref List, Conexion);
              
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
        public void ProTransferenciaAlmacen_ConsultaListaImprimir(TransferenciasAlm TA, ref DataTable dt, string Conexion)
        {
            try
            {
                CD_TransferenciasAlm cd_ta = new CD_TransferenciasAlm();
                cd_ta.ProTransferenciaAlmacen_ConsultaListaImprimir(TA, ref dt, Conexion);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void ProTransferenciaAlmacen_BajaRemitente(Remision remision, ref int Verificador, string Conexion)
        {
            try
            {
                CD_TransferenciasAlm  cd_ta = new CD_TransferenciasAlm();
                cd_ta.ProTransferenciaAlmacen_BajaRemitente(remision, ref Verificador, Conexion);

            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
        public void ProTransferenciaAlmacen_Consulta(int Id_Emp, int Id_Cd, int Id_Tra, ref TransferenciasAlm tra, string Conexion)
        {
            try
            {
                CD_TransferenciasAlm cd_tra = new CD_TransferenciasAlm();
                cd_tra.ProTransferenciaAlmacen_Consulta(Id_Emp, Id_Cd, Id_Tra, ref tra, Conexion);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
        public void ProTransferenciaAlmacen_ConsultaDet(int Id_Emp, int Id_Cd, int Id_Tra, ref List<TransferenciaAlmDet> List, string Conexion)
        {
            try
            {
                CD_TransferenciasAlm cd_tra = new CD_TransferenciasAlm();
                cd_tra.ProTransferenciaAlmacen_ConsultaDet(Id_Emp, Id_Cd, Id_Tra, ref List, Conexion);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
        public void ProTransferenciaAlmacen_Recepcion(TransferenciasAlm tra, List<TransferenciaAlmDet> List, ref int Verificador, ref int Remision, string Conexion)
        {
            try
            {
                CD_TransferenciasAlm cd_tra = new CD_TransferenciasAlm();
                cd_tra.ProTransferenciaAlmacen_Recepcion(tra, List, ref Verificador, ref Remision, Conexion);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
    
    }
}
