using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using CapaDatos;

namespace CapaNegocios
{
    public class CN_Empresa
    {
        public CN_Empresa()
        { }
         
        public void InsertarEmpresa(ref Empresa empresa, string conexion, ref int verificador)
        {
            try
            {
                CD_CatEmpresa claseCapaDatos = new CD_CatEmpresa();
                claseCapaDatos.InsertarEmpresa(ref empresa, conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarUsuario(Empresa empresa, string conexion, ref int verificador)
        {
            try
            {
                CD_CatEmpresa claseCapaDatos = new CD_CatEmpresa();
                claseCapaDatos.ModificarUsuario(ref empresa, conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaEmpresas(ref Empresa empresa, string conexion)
        {
            try
            {
                CD_CatEmpresa claseCapaDatos = new CD_CatEmpresa();
                claseCapaDatos.ConsultaEmpresas(empresa, conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

       
    }
}
