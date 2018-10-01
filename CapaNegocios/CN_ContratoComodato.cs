using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CapaEntidad;
using CapaDatos;

namespace CapaNegocios
{
    public class CN_ContratoComodato
    {
        public void ConsultarCantidadContratoComCentroDist(ref int verificador, int Id_Cd, string Conexion)
        {
            try
            {
                new CD_ContratoComodato().ConsultarCantidadContratoComCentroDist(ref verificador, Id_Cd, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarContratoComodato_BaseInstalada(ContratoComodato contratoComodato, ref List<ContratoComodato> listaContratoCom
            , DateTime fecha1, DateTime fecha2, string Conexion)
        {
            try
            {
                new CD_ContratoComodato().ConsultarContratoComodato_BaseInstalada(contratoComodato, ref listaContratoCom, fecha1, fecha2, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarContratoComodato_FechaContrato(ref List<ContratoComodato> listaContratoCom, ref int verificador, string Conexion)
        {
            try
            {
                new CD_ContratoComodato().ModificarContratoComodato_FechaContrato(ref listaContratoCom, ref verificador, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Consultar(int Id_Emp, int Id_Cd, int folio,ref int verificador, string Conexion)
        {
            try
            {
                new CD_ContratoComodato().ModificarContratoComodato_FechaContrato(Id_Emp, Id_Cd, folio,ref verificador, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
