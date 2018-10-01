using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using CapaDatos;

namespace CapaNegocios
{
    public class CN_EmbarquesRemision
    {
        public void ConsultaEmbarquesRemision(int Id_Emp, int Id_Cd, string Conexion,EmbarquesRemision embarquefiltro, ref List<EmbarquesRemision> List)
        {
            try
            {
                CD_EmbarquesRemision embarquesRemision = new CD_EmbarquesRemision();
                embarquesRemision.ConsultaProEmbarquesRemision(Id_Emp, Id_Cd, Conexion, embarquefiltro, ref List);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarEmbarquesRemision(int Id_Emp, int Id_Cd, int Id_U, EmbarquesRemision embarque, string Conexion, ref int verificador)
        {
            try
            {
                CD_EmbarquesRemision embarquesRemision = new CD_EmbarquesRemision();
                embarquesRemision.ModificarProEmbarquesRemision(Id_Emp, Id_Cd,Id_U, embarque, Conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
