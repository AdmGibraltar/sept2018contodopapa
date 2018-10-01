using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using CapaEntidad;
using CapaModelo;

namespace CapaDatos
{
   public class CD_CatUen
    {
       public void ConsultaUen(int Id_Emp, string Conexion, ref List<Uen> List)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp" };
                object[] Valores = { Id_Emp };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatUen_Consulta", ref dr, Parametros, Valores);

                Uen uen;
                while (dr.Read())
                {
                    uen = new Uen();
                    uen.Id = (int)dr.GetValue(dr.GetOrdinal("Id_Uen"));
                    uen.Descripcion = (string)dr.GetValue(dr.GetOrdinal("Uen_Descripcion"));
                    uen.Estatus = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Uen_Activo")));
                    if (Convert.ToBoolean(uen.Estatus))
                    {
                        uen.EstatusStr = "Activo";
                    }
                    else
                    {
                        uen.EstatusStr = "Inactivo";
                    }
                    List.Add(uen);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

       public void InsertarUen(Uen uen, string Conexion, ref int verificador)
        {
            try
            {
                
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = { 
	                                    "@Id_Uen",
                                        "@Id_Emp",
	                                    "@Uen_Descripcion", 
	                                    "@Uen_Activo"
                                      };
                object[] Valores = { 
                                        uen.Id,
                                        uen.Id_Emp,
                                        uen.Descripcion,
                                        uen.Estatus
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatUen_Insertar", ref verificador, Parametros, Valores);
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

       public void ModificarUen(Uen uen, string Conexion, ref int verificador)
        {
            try
            {

                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = { 
	                                    "@Id_Uen",
                                        "@Id_Emp",
	                                    "@Uen_Descripcion", 
	                                    "@Uen_Activo"
                                      };
                object[] Valores = { 
                                        uen.Id,
                                        uen.Id_Emp,
                                        uen.Descripcion,
                                        uen.Estatus
                                   };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatUen_Modificar", ref verificador, Parametros, Valores);
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



       public void ConsultaUen_Usuario(ref List<Uen> list, int Id_Emp, int? Id_U, string Conexion)
       {
           try
           {
               SqlDataReader dr = null;
               CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

               string[] Parametros = { "@Id_Emp","@Id_U" };
               object[] Valores = { Id_Emp, Id_U};

               SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatUenUsuario_Consultar", ref dr, Parametros, Valores);

               Uen uen;
               while (dr.Read())
               {
                   uen = new Uen();
                   uen.Id = (int)dr.GetValue(dr.GetOrdinal("Id_Uen"));
                   uen.Descripcion = (string)dr.GetValue(dr.GetOrdinal("Uen_Descripcion"));
                   uen.Id_U = dr.IsDBNull(dr.GetOrdinal("Id_U")) ? (int?)null: dr.GetInt32(dr.GetOrdinal("Id_U"));
                   
                   list.Add(uen);
               }

               CapaDatos.LimpiarSqlcommand(ref sqlcmd);
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }

       public IEnumerable<CatUEN> ConsultarUENsDeRepresentante(int idEmp, int idCd, int idRik, string Conexion)
       {
           IEnumerable<CatUEN> result = null;
           using (sianwebmty_gEntities ctx = new sianwebmty_gEntities(Conexion))
           {
               var res = ctx.spCatUEN_ConsultarPorRIK(idEmp, idCd, idRik).ToList();
               result = res;
           }
           return result;
       }

       /// <summary>
       /// Regresa la consulta al repositorio CatUEN condicionado por la empresa
       /// </summary>
       /// <param name="idEmp">Identificador de la empresa</param>
       /// <param name="cadenaConexionEf">Cadena de conexión a la fuente de datos</param>
       /// <returns>IEnumerable[CatUEN]</returns>
       public IEnumerable<CatUEN> ConsultarPorEmpresa(int idEmp, string cadenaConexionEf)
       {
           IEnumerable<CatUEN> result = null;
           using (sianwebmty_gEntities ctx = new sianwebmty_gEntities(cadenaConexionEf))
           {
               result = (from u in ctx.CatUENs
                         where u.Id_Emp == idEmp
                         select u).ToList().Select(cuen => { cuen.CatSegmentos.Count(); return cuen; }).ToList();
           }
           return result;
       }

       /// <summary>
       /// Regresa la consulta al repositorio CatUEN condicionado por la empresa
       /// </summary>
       /// <param name="idEmp">Identificador de la empresa</param>
       /// <param name="icdCtx">Contexto de conexión a la fuente de datos</param>
       /// <returns>IEnumerable[CatUEN]</returns>
       public IEnumerable<CatUEN> ConsultarPorEmpresa(int idEmp, ICD_Contexto icdCtx)
       {
           IEnumerable<CatUEN> result = null;
           var ctx = ((ICD_Contexto<sianwebmty_gEntities>)icdCtx).Contexto;
           result = (from u in ctx.CatUENs
                     where u.Id_Emp == idEmp
                     select u).ToList().Select(cuen => { cuen.CatSegmentos.Count(); return cuen; }).ToList();
           return result;
       }
    }
}
