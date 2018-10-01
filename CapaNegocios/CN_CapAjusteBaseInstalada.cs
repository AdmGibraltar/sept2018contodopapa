using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using CapaEntidad;
using CapaDatos;

namespace CapaNegocios
{
    public class CN_CapAjusteBaseInstalada
    {
        public void ConsultarAjusteBaseInstalada_PorUnique(ref AjusteBaseInstalada ajusteBaseInstalada, string Conexion, ref bool encontrado)
        {
            try
            {
                new CD_CapAjusteBaseInstalada().ConsultarAjusteBaseInstalada_PorUnique(ref ajusteBaseInstalada, Conexion, ref encontrado);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarEstatusAjusteBaseInstalada(ref List<AjusteBaseInstaladaDet> listaAjusteBaseInstalada, string Conexion, ref int verificador)
        {
            try
            {
                new CD_CapAjusteBaseInstalada().ModificarEstatusAjusteBaseInstalada(ref listaAjusteBaseInstalada, Conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public void Insertar(AjusteBaseInstalada cabezera, System.Data.DataTable dt, string Conexion, ref string verificador)
        {
            try
            {
                CD_CapAjusteBaseInstalada cd_ajuste = new CD_CapAjusteBaseInstalada();
                cd_ajuste.Insertar(cabezera, dt, Conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
