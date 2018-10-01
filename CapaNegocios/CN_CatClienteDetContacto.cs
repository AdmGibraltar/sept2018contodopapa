using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using CapaDatos;
using CapaModelo;

namespace CapaNegocios
{
    public class CN_CatClienteDetContacto
    {
        private Sesion _s;

        public CN_CatClienteDetContacto(Sesion s)
        {
            _s = s;
        }

        public CatClienteDet CrearNuevo(Sesion s, int idCte, int idRik, int idTer, int idSeg, double vpo)
        {
            CD_CatClienteDet cdCatClienteDet = new CD_CatClienteDet(s.Emp_Cnx);
            return cdCatClienteDet.InsertarBasico(s.Id_Emp, s.Id_Cd, idCte, idRik, idTer, idSeg, vpo, s.Emp_Cnx_EF);
        }

        public void InsertarCR(CapaEntidad.ClienteDetContacto contacto, string Conexion, ref int verificador)
        {
            try
            {
                CD_CatClienteDetContacto classCapaDatos = new CD_CatClienteDetContacto();                
                classCapaDatos.InsertarUpdate(contacto, Conexion , ref verificador);
            }
            catch (Exception ex)
            {                
                throw ex;
            }
        }

        public void Consulta(CapaEntidad.ClienteDetContacto contacto, string Conexion, ref List<CapaEntidad.ClienteDetContacto> List)
        {
            try
            {                
                CD_CatClienteDetContacto classCapaDatos = new CD_CatClienteDetContacto();
                classCapaDatos.Consulta(contacto, Conexion, ref List);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaPorId(CapaEntidad.ClienteDetContacto contacto, string Conexion, ref CapaEntidad.ClienteDetContacto List)
        {
            try
            {
                CD_CatClienteDetContacto classCapaDatos = new CD_CatClienteDetContacto();
                classCapaDatos.ConsultaPorId(contacto, Conexion, ref List);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
