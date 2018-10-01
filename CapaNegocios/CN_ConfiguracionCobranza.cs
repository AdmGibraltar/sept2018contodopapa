using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using CapaDatos;

namespace CapaNegocios
{
    public class CN_ConfiguracionCobranza
    {
        public void Guardar(List<Acciones> list_acciones, List<Alertas> list_alertas,List<ConfigCredito> list_credito, Reglas reglas, CobProceso cobProceso, int Id_Emp, string Conexion, ref int verificador)
        {
            try
            {
                CD_ConfiguracionCobranza confCobranza = new CD_ConfiguracionCobranza();
                confCobranza.Guardar(list_acciones, list_alertas, list_credito,reglas, cobProceso ,  Id_Emp, Conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Consultar(ref List<PeriodoGracia> list_gracia, ref List<Acciones> list_acciones, ref List<Alertas> list_alertas, int Id_Emp, string db, ref Reglas reglas, string Conexion)
        {
            try
            {
                CD_ConfiguracionCobranza confCobranza = new CD_ConfiguracionCobranza();
                confCobranza.Consultar(ref list_gracia, ref list_acciones, ref list_alertas, Id_Emp, db, ref  reglas, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ConsultarCobProceso(ref CobProceso cobProceso, string Conexion)
        {
            try
            {
                CD_ConfiguracionCobranza confCobranza = new CD_ConfiguracionCobranza();
                confCobranza.ConsultarCobProceso(ref  cobProceso, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ConsultarFacturasVencidasPorCliente(int Id_Emp, int Id_Cd, int Id_Cte, ref List<CapaEntidad.Factura> list, string Conexion)
         {
             try
             {
                 CD_ConfiguracionCobranza confCobranza = new CD_ConfiguracionCobranza();
                 confCobranza.ConsultarFacturasVencidasPorCliente(Id_Emp, Id_Cd, Id_Cte, ref list, Conexion);
             }
             catch (Exception ex)
             {
                 throw ex;
             }
         }
        public void ConsultarConfiguCredito(ref List<ConfigCredito> List, int Id_Emp, int Id_Cd, string Conexion)
        {
            try
            {
                CD_ConfiguracionCobranza cd_cc = new CD_ConfiguracionCobranza();
                cd_cc.ConsultarConfiguCredito(ref  List,  Id_Emp, Id_Cd,  Conexion);

            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
    }
}
