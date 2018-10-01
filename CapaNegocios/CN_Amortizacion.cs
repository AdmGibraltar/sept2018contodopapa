using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using CapaEntidad;
using CapaDatos;

namespace CapaNegocios
{
    public class CN_Amortizacion
    {
        public void ConsultaInversionComodato(int Id_Emp, int Id_Cd, int Id_Ter, int Id_Cte, string Conexion, ref Double InversionComodatos)
        {
            try
            {
                CD_Amortizacion claseCapaDatos = new CD_Amortizacion();
                claseCapaDatos.ConsultaInversionComodato(Id_Emp, Id_Cd, Id_Ter, Id_Cte, Conexion, ref InversionComodatos);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ConsultaAmortizacionCliente(Amortizacion amorizacion, string Conexion, ref List<Amortizacion> List)
        {
            try
            {
                CD_Amortizacion claseCapaDatos = new CD_Amortizacion();
                claseCapaDatos.ConsultaAmortizacionCliente(amorizacion, Conexion, ref List);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
