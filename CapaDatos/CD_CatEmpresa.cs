using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using System.Data.SqlClient;
using CapaModelo;

namespace CapaDatos
{
    public class CD_CatEmpresa
    {
        public void InsertarEmpresa(ref Empresa empresa, string conexion, ref int verificador)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(conexion);

                string[] Parametros = { 
                                        "@Emp_Nombre",
                                        "@Activo",
                                        "@Emp_GastoLocal", 
                                        "@Emp_GastoAdmin", 
                                        "@Emp_ContribucionPapel", 
                                        "@Emp_ContribucionNoPapel", 
                                        "@Emp_Ucs", 
                                        "@Emp_Isr", 
                                        "@Emp_Inversion", 
                                        "@Emp_Cetes", 
                                        "@Emp_AdicionalCetes"
                                      };
                object[] Valores = {
                                        empresa.Emp_Nombre,
                                        empresa.Emp_Activo,
                                        empresa.Emp_GastoLocal, 
                                        empresa.Emp_GastoAdmin, 
                                        empresa.Emp_ContribucionPapel, 
                                        empresa.Emp_ContribucionNoPapel, 
                                        empresa.Emp_Ucs, 
                                        empresa.Emp_Isr, 
                                        empresa.Emp_Inversion, 
                                        empresa.Emp_Cetes, 
                                        empresa.Emp_AdicionalCetes
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatEmpresa_Insertar", ref verificador, Parametros, Valores);

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarUsuario(ref Empresa empresa, string conexion, ref int verificador)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(conexion);

                string[] Parametros = { 
                                        "@Id_Emp", 
                                        "@Emp_Nombre",
                                        "@Activo",
                                        "@Emp_GastoLocal", 
                                        "@Emp_GastoAdmin", 
                                        "@Emp_ContribucionPapel", 
                                        "@Emp_ContribucionNoPapel", 
                                        "@Emp_Ucs", 
                                        "@Emp_Isr", 
                                        "@Emp_Inversion", 
                                        "@Emp_Cetes", 
                                        "@Emp_AdicionalCetes"
                                      };
                object[] Valores = {
                                        empresa.Id_Emp, 
                                        empresa.Emp_Nombre,
                                        empresa.Emp_Activo,
                                        empresa.Emp_GastoLocal, 
                                        empresa.Emp_GastoAdmin, 
                                        empresa.Emp_ContribucionPapel, 
                                        empresa.Emp_ContribucionNoPapel, 
                                        empresa.Emp_Ucs, 
                                        empresa.Emp_Isr, 
                                        empresa.Emp_Inversion, 
                                        empresa.Emp_Cetes, 
                                        empresa.Emp_AdicionalCetes
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatEmpresa_Modificar", ref verificador, Parametros, Valores);

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaEmpresas(Empresa empresa, string conexion)
        {
            try
            {
                SqlDataReader dr = null;
                CD_Datos CapaDatos = new CD_Datos(conexion);


                string[] Parametros = { "@Id_Emp" };
                object[] Valores = { empresa.Id_Emp };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatEmpresa_Consulta", ref dr, Parametros, Valores);

                while (dr.Read())
                {

                    empresa.Id_Emp = (int)dr.GetValue(dr.GetOrdinal("Id_Emp"));
                    empresa.Emp_Nombre = (string)dr.GetValue(dr.GetOrdinal("Emp_Nombre"));
                    empresa.Emp_Activo = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Emp_Activo")));
                    empresa.Emp_GastoLocal = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Emp_GastoLocal")));
                    empresa.Emp_GastoAdmin = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Emp_GastoAdmin")));
                    empresa.Emp_ContribucionPapel = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Emp_ContribucionPapel")));
                    empresa.Emp_ContribucionNoPapel = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Emp_ContribucionNoPapel")));
                    empresa.Emp_Ucs = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Emp_Ucs")));
                    empresa.Emp_Isr = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Emp_Isr")));
                    empresa.Emp_Inversion = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Emp_Inversion")));
                    empresa.Emp_Cetes = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Emp_Cetes")));
                    empresa.Emp_AdicionalCetes = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Emp_AdicionalCetes")));
                    if (Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Emp_Activo"))))
                    {
                        empresa.Emp_ActivoStr = "Activo";
                    }
                    else
                    {
                        empresa.Emp_ActivoStr = "Inactivo";
                    }
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Devuelve el resultado de consultar el repositorio CatEmpresa por identificador.
        /// </summary>
        /// <param name="idEmp">Identificador de la empresa</param>
        /// <param name="icdCtx">Contexto de conexión a la fuente de datos</param>
        /// <returns>CatEmpresa</returns>
        public CatEmpresa Consultar(int idEmp, ICD_Contexto icdCtx)
        {
            sianwebmty_gEntities ctx = ((ICD_Contexto<sianwebmty_gEntities>)icdCtx).Contexto;
            var resultados = from e in ctx.CatEmpresas
                             where e.Id_Emp==idEmp
                             select e;
            if (resultados.Count() > 0)
                return resultados.First();
            return null;
        }
    }
}
