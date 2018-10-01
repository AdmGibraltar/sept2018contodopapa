using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaDatos;
using CapaEntidad;
using System.Data;

namespace CapaNegocios
{
    public class CN_ProCierreMes
    {
        public void Cierre(int Id_Emp, int Id_Cd, string Conexion, ref  int verificador)
        {
            try
            {
                CD_CierreMes cd_cierremes = new CD_CierreMes();
                cd_cierremes.Cierre(Id_Emp, Id_Cd, Conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void CierreGrid(Sesion sesion, ref List<PronCierre> listProCierre)
        {
            try
            {
                CD_CierreMes cd_cierremes = new CD_CierreMes();
                cd_cierremes.CierreGrid(sesion, ref listProCierre);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ModificarPronosticoCierre(PronCierre pronCierre, Sesion sesion, ref int validador)
        {
            try
            {
                CD_CierreMes cd_cierremes = new CD_CierreMes();
                cd_cierremes.ModificarPronosticoCierre(pronCierre, sesion, ref validador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Boolean RemisionesPendientes(int Id_Emp, int Id_Cd, string Conexion, ref string RemisionesPend)
        {

            CD_CierreMes cd_cierremes = new CD_CierreMes();
            return cd_cierremes.RemisionesPendientes(Id_Emp, Id_Cd, Conexion, ref RemisionesPend);
        }


    }
}
