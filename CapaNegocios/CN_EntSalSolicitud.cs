using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaDatos;
using CapaEntidad;

namespace CapaNegocios
{
    public class CN_EntSalSolicitud
    {
        public void CapEntSalSolicitud_ConsultaLista(EntSalSolicitud es, ref List<EntSalSolicitud> List, string Conexion)
        {
            try
            {
                CD_EntSalSolicitud cd_es = new CD_EntSalSolicitud();
                cd_es.CapEntSalSolicitud_ConsultaLista(es, ref List, Conexion);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
        public void CapEntSalSolicitud_ConsultaConsecutivo(Sesion sesion, ref int Id_ESol)
        {
            try
            {
                CD_EntSalSolicitud cd_es = new CD_EntSalSolicitud();
                cd_es.CapEntSalSolicitud_ConsultaConsecutivo(sesion, ref Id_ESol);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
        public void CapEntSalSolicitud_Insertar(EntSalSolicitud es, ref int Verificador, string Conexion)
        {
            try
            {
                CD_EntSalSolicitud cd_es = new CD_EntSalSolicitud();
                cd_es.CapEntSalSolicitud_Insertar(es, ref Verificador, Conexion);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
        public void CapEntSalSolicitudDet_Insertar(int Id_ESol, EntSalSolicitud es, List<EntSalSolicitudDet> List, ref int Verificador, string Conexion)
        {
            try
            {
                CD_EntSalSolicitud cd_es = new CD_EntSalSolicitud();
                cd_es.CapEntSalSolicitudDet_Insertar(Id_ESol, es, List, ref Verificador, Conexion);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
        public void CapEntSalSolicitud_Cancelar(EntSalSolicitud es, ref int Verificador, string Conexion)
        {
            try
            {
                CD_EntSalSolicitud cd_es = new CD_EntSalSolicitud();
                cd_es.CapEntSalSolicitud_Cancelar(es, ref Verificador, Conexion);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
        public void CapEntSalSolicitud_CorreoCreo(int Id_Cd, int Id_ESol, string Url,ref int Verificador, string Conexion)
        {
            try
            {
                CD_EntSalSolicitud cd_es = new CD_EntSalSolicitud();
                cd_es.CapEntSalSolicitud_CorreoCreo(Id_Cd, Id_ESol, Url,ref Verificador, Conexion);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
        public void CapEntSalSolicitud_Consulta(string ESol_Unique, ref EntSalSolicitud es, string Conexion)
        {
            try
            {
                CD_EntSalSolicitud cd_es = new CD_EntSalSolicitud();
                cd_es.CapEntSalSolicitud_Consulta(ESol_Unique, ref es, Conexion);

            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
        public void CapEntSalSolicitud_ConsultaDet(string ESol_Unique, ref List<EntSalSolicitudDet> List, string Conexion)
        {
            try
            {
                CD_EntSalSolicitud cd_es = new CD_EntSalSolicitud();
                cd_es.CapEntSalSolicitud_ConsultaDet(ESol_Unique, ref List, Conexion);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
        public void CapEntSalSolicitud_EliminarDet(int Id_Cd, int Id_ESol, ref int Verificador, string Conexion)
        {
            try
            {
                CD_EntSalSolicitud cd_es = new CD_EntSalSolicitud();
                cd_es.CapEntSalSolicitud_EliminarDet(Id_Cd, Id_ESol, ref Verificador, Conexion);

            }
            catch (Exception ex)
            {
                
                throw ex; 
            }
        }
        public void CapEntSalSolicitud_ModificarEstatus(int Id_Cd, int Id_ESol, string ESol_Estatus, ref int Verificador, string Conexion)
        {
            try
            {
                CD_EntSalSolicitud cd_es = new CD_EntSalSolicitud();
                cd_es.CapEntSalSolicitud_ModificarEstatus(Id_Cd, Id_ESol, ESol_Estatus, ref Verificador, Conexion);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
        public void CapEntSalSolicitud_CorreoAtendio(int Id_Cd, int Id_ESol, string Url, ref int Verificador, string Conexion)
        {
            try
            {
                CD_EntSalSolicitud cd_es = new CD_EntSalSolicitud();
                cd_es.CapEntSalSolicitud_CorreoAtendio(Id_Cd, Id_ESol, Url, ref Verificador, Conexion);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void CapEntSalSolicitud_Autorizo(int Id_Cd, int Id_ESol, int Id_Es, ref int Verificador, string Conexion)
        {
            try
            {
                CD_EntSalSolicitud cd_es = new CD_EntSalSolicitud();
                cd_es.CapEntSalSolicitud_Autorizo(Id_Cd, Id_ESol, Id_Es, ref Verificador, Conexion);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
        public void GuardarEntradaSalida(ref EntradaSalida entradaSalida, List<EntradaSalidaDetalle> listaDetalle, ref string verificadorStr, int strEmp, string Conexion)
        {
            try
            {
                CD_EntSalSolicitud cd_es = new CD_EntSalSolicitud();
                cd_es.GuardarEntradaSalida(ref entradaSalida, listaDetalle, ref verificadorStr, strEmp, Conexion);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
        public void CapEntSalSolicitud_ConsultaFolio(string ESol_Unique, ref int Id_ESol, string Conexion)
        {
            try
            {
                CD_EntSalSolicitud cd_es = new CD_EntSalSolicitud();
                cd_es.CapEntSalSolicitud_ConsultaFolio(ESol_Unique, ref Id_ESol, Conexion);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
        public void CapEntSolicitud_ConsultaDatosEnvio(int Id_Cd, int Id_ESol, ref EntSalSolicitud es, string Conexion)
        {
            try
            {
                CD_EntSalSolicitud cd_es = new CD_EntSalSolicitud();
                cd_es.CapEntSolicitud_ConsultaDatosEnvio(Id_Cd, Id_ESol, ref es, Conexion);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void CapEntSalSolicitud_ValidarMonto(EntSalSolicitud es, ref int Verificador, string Conexion)
        {
            try
            {
                CD_EntSalSolicitud cd_es = new CD_EntSalSolicitud();
                cd_es.CapEntSalSolicitud_ValidarMonto(es, ref Verificador, Conexion);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
      

    


    }
}
