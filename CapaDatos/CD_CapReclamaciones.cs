using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using CapaEntidad;

namespace CapaDatos
{
    public class CD_CapReclamaciones
    {
        /// <summary>
        /// Metodo para insertar los datos de la base de datos
        /// </summary>
        /// <param name="reclamaciones">Entidad de las reclamaciones</param>
        /// <param name="dt">data table donde se vaciaran los resultados obtenidos</param>
        /// <param name="conexion">Cadena de conexion a la base de datos</param>
        /// <param name="verificador">Indica si se pudo o no realizar la operacion</param>
        public void InsertarReclamaciones(Reclamaciones reclamaciones, DataTable dt, string conexion, ref int verificador)
        {
            CapaDatos.CD_Datos capaDatos = new CD_Datos(conexion);
            try
            {
                capaDatos.StartTrans();
                string[] Parametros ={
                                        "@Id_Emp",
                                        "@Id_Cd",
                                        "@Rec_Fecha",
                                        "@Id_Cte",
                                        "@Rec_Usuario",
                                        "@Rec_Telefono",
                                        "@Id_Ter",
                                        "@Id_Tipo",
                                        "@Id_NoConf",
                                        "@Rec_Descripcion",
                                        "@Rec_CausaRaiz",
                                        "@Rec_FecAccion",
                                        "@Rec_AcAccion1",
                                        "@Rec_AcAccion2",
                                        "@Rec_AcResponsable",
                                        "@Rec_FecConformidad",
                                        "@Rec_ConNombre",
                                        "@Rec_ConDepartamento",
                                        "@Rec_Comentarios",
                                        "@Rec_Estatus"
                                    };
                object[] Valores ={
                                     reclamaciones.Id_Emp,
                                     reclamaciones.Id_Cd,
                                     reclamaciones.Rec_Fecha,
                                     reclamaciones.Id_Cte,
                                     reclamaciones.Rec_Usuario,
                                     reclamaciones.Rec_Telefono,
                                     reclamaciones.Id_Ter,
                                     reclamaciones.Id_tipo,
                                     reclamaciones.Id_NoConf,
                                     reclamaciones.Rec_Descripcion,
                                     reclamaciones.Rec_CausaRaiz,
                                     reclamaciones.Rec_FecAccion,
                                     reclamaciones.Rec_AcAccion1,
                                     reclamaciones.Rec_AcAccion2,
                                     reclamaciones.Rec_AcResponsable,
                                     reclamaciones.Rec_FecConformidad,
                                     reclamaciones.Rec_ConNombre,
                                     reclamaciones.Rec_ConDepartamento,
                                     reclamaciones.Rec_Comentarios,
                                     reclamaciones.Rec_Estatus
                                  };

                SqlCommand sqlcmd = capaDatos.GenerarSqlCommand("spCapReclamaciones_Insertar", ref verificador, Parametros, Valores);
                reclamaciones.Id_Rec = verificador;
                if (verificador > -1)               
                    capaDatos.CommitTrans();               
                else                
                    capaDatos.RollBackTrans();
                
                capaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                capaDatos.RollBackTrans();
                throw ex;
            }
        }

        /// <summary>
        /// Metodo para modificar los datos de la base de datos
        /// </summary>
        /// <param name="reclamaciones">Entidad de las reclamaciones</param>
        /// <param name="dt">data table donde se vaciaran los resultados obtenidos</param>
        /// <param name="conexion">Cadena de conexion a la base de datos</param>
        /// <param name="verificador">Indica si se pudo o no realizar la operacion</param>
        public void ModificaReclamaciones(Reclamaciones reclamaciones, DataTable dt, string conexion, ref int verificador)
        {
            CapaDatos.CD_Datos capaDatos = new CD_Datos(conexion);
            try
            {
                capaDatos.StartTrans();
                string[] Parametros ={
                                        "@Id_Emp",
                                        "@Id_Cd",
                                        "@Id_Rec",
                                        "@Rec_Fecha",
                                        "@Id_Cte",
                                        "@Rec_Usuario",
                                        "@Rec_Telefono",
                                        "@Id_Ter",
                                        "@Id_Tipo",
                                        "@Id_NoConf",
                                        "@Rec_Descripcion",
                                        "@Rec_CausaRaiz",
                                        "@Rec_FecAccion",
                                        "@Rec_AcAccion1",
                                        "@Rec_AcAccion2",
                                        "@Rec_AcResponsable",
                                        "@Rec_FecConformidad",
                                        "@Rec_ConNombre",
                                        "@Rec_ConDepartamento",
                                        "@Rec_Comentarios",
                                        "@Rec_Estatus"
                                    };
                object[] Valores ={
                                     reclamaciones.Id_Emp,
                                     reclamaciones.Id_Cd,
                                     reclamaciones.Id_Rec,
                                     reclamaciones.Rec_Fecha,
                                     reclamaciones.Id_Cte,
                                     reclamaciones.Rec_Usuario,
                                     reclamaciones.Rec_Telefono,
                                     reclamaciones.Id_Ter,
                                     reclamaciones.Id_tipo,
                                     reclamaciones.Id_NoConf,
                                     reclamaciones.Rec_Descripcion,
                                     reclamaciones.Rec_CausaRaiz,
                                     reclamaciones.Rec_FecAccion,
                                     reclamaciones.Rec_AcAccion1,
                                     reclamaciones.Rec_AcAccion2,
                                     reclamaciones.Rec_AcResponsable,
                                     reclamaciones.Rec_FecConformidad,
                                     reclamaciones.Rec_ConNombre,
                                     reclamaciones.Rec_ConDepartamento,
                                     reclamaciones.Rec_Comentarios,
                                     reclamaciones.Rec_Estatus
                                  };

                SqlCommand sqlcmd = capaDatos.GenerarSqlCommand("spCapReclamaciones_Modificar", ref verificador, Parametros, Valores);
                reclamaciones.Id_Rec = verificador;
                if (verificador > -1)               
                    capaDatos.CommitTrans();                
                else               
                    capaDatos.RollBackTrans();
               
                capaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                capaDatos.RollBackTrans();
                throw ex;
            }
        }

        /// <summary>
        /// Metodo que busca los datos en la base de datos
        /// </summary>
        /// <param name="reclamaciones">Entidad de las reclamaciones</param>
        /// <param name="conexion">Cadena de conexion a la base de datos</param>
        /// <param name="lista">lista donde se vaciaran los resultados obtenidos</param>
        /// <param name="Id_Emp">Id de la empresa</param>
        /// <param name="Id_Cd">Id de la ciudad</param>
        /// <param name="Id_Rec_Ini">Id de la reclamacion, tomado como un rango inicial</param>
        /// <param name="Id_Rec_Fin">Id de la reclamacion, tomado como un rango final</param>
        /// <param name="Id_Cte_Ini">Id del cliente, tomado como un rango inicial</param>
        /// <param name="Id_Cte_Fin">Id del cliente, tomado como un rango final</param>
        /// <param name="Rec_Estatus">Estatus de las reclamaciones</param>
        /// <param name="Rec_Fecha_Ini">Fecha para buscar desde un rango inicial</param>
        /// <param name="Rec_Fecha_Fin">Fecha para buscar hasta un rango final</param>
        /// <param name="NomCte">Nombre del cliente</param>
        /// <param name="Id_Tipo">Id del tipo de la reclamacion</param>
        public void BuscaReclamaciones(Reclamaciones reclamaciones, string conexion, ref List<Reclamaciones> lista,
            int Id_Emp, int Id_Cd, int Id_Rec_Ini, int Id_Rec_Fin, int Id_Cte_Ini, int Id_Cte_Fin, string Rec_Estatus,
            DateTime Rec_Fecha_Ini, DateTime Rec_Fecha_Fin, string NomCte, int Id_Tipo)
        {
            try
            {
                SqlDataReader sdr = null;

                CapaDatos.CD_Datos capaDatos = new CD_Datos(conexion);

                string[] parametros = {
                                          "@Id_Emp", 
                                          "@Id_Cd",
                                          "@Id_Rec_Ini",
                                          "@Id_Rec_Fin", 
                                          "@Id_Cte_Ini", 
                                          "@Id_Cte_Fin", 
                                          "@Rec_Estatus",
                                          "@Rec_Fecha_Ini", 
                                          "@Rec_Fecha_Fin", 
                                          "@NomCte", 
                                          "@Id_Tipo"
                                      };
                object[] valores = { 
                                       Id_Emp,
                                       Id_Cd,
                                       Id_Rec_Ini == -1? (object)null : Id_Rec_Ini,
                                       Id_Rec_Fin == -1? (object)null : Id_Rec_Fin,
                                       Id_Cte_Ini == -1? (object)null : Id_Cte_Ini,
                                       Id_Cte_Fin == -1? (object)null : Id_Cte_Fin,
                                       Rec_Estatus == string.Empty? (object)null : Rec_Estatus,
                                       Rec_Fecha_Ini == DateTime.MinValue? (object)null : Rec_Fecha_Ini,
                                       Rec_Fecha_Fin == DateTime.MinValue? (object)null : Rec_Fecha_Fin,
                                       NomCte== string.Empty? (object)null : NomCte,
                                       Id_Tipo == -1? (object)null : Id_Tipo
                                   };

                SqlCommand sqlcmd = capaDatos.GenerarSqlCommand("spCapReclamaciones_buscar", ref sdr, parametros, valores);

                while (sdr.Read())
                {
                    reclamaciones = new Reclamaciones();
                    reclamaciones.Id_Emp = Convert.ToInt32(sdr.GetValue(sdr.GetOrdinal("Id_Emp")));
                    reclamaciones.Id_Cd = Convert.ToInt32(sdr.GetValue(sdr.GetOrdinal("Id_Cd")));
                    reclamaciones.Id_Rec = Convert.ToInt32(sdr.GetValue(sdr.GetOrdinal("Id_Rec")));
                    reclamaciones.Id_Cte = Convert.ToInt32(sdr.GetValue(sdr.GetOrdinal("Id_Cte")));
                    reclamaciones.Rec_Fecha = Convert.ToDateTime(sdr.GetValue(sdr.GetOrdinal("Rec_Fecha")));
                    reclamaciones.Rec_Usuario = sdr.GetValue(sdr.GetOrdinal("Rec_Usuario")).ToString();
                    reclamaciones.Rec_Telefono = sdr.GetValue(sdr.GetOrdinal("Rec_Telefono")).ToString();
                    reclamaciones.Id_Ter = Convert.ToInt32(sdr.GetValue(sdr.GetOrdinal("Id_Ter")));
                    reclamaciones.Id_NoConf = Convert.ToInt32(sdr.GetValue(sdr.GetOrdinal("Id_NoConf")));
                    reclamaciones.Rec_Descripcion = sdr.GetValue(sdr.GetOrdinal("Rec_Descripcion")).ToString();
                    reclamaciones.Rec_CausaRaiz = sdr.GetValue(sdr.GetOrdinal("Rec_CausaRaiz")).ToString();
                    if (sdr.GetValue(sdr.GetOrdinal("Rec_Estatus")).ToString() == "A")                    
                        reclamaciones.Rec_FecAccion = Convert.ToDateTime(sdr.GetValue(sdr.GetOrdinal("Rec_FecAccion")));                   
                    else                   
                        reclamaciones.Rec_FecAccion = null;                   
                    reclamaciones.Rec_AcAccion1 = sdr.GetValue(sdr.GetOrdinal("Rec_AcAccion1")).ToString();
                    reclamaciones.Rec_AcAccion2 = sdr.GetValue(sdr.GetOrdinal("Rec_AcAccion2")).ToString();
                    reclamaciones.Rec_AcResponsable = sdr.GetValue(sdr.GetOrdinal("Rec_AcResponsable")).ToString();
                    if (sdr.GetValue(sdr.GetOrdinal("Rec_Estatus")).ToString() == "F")
                    {
                        reclamaciones.Rec_FecAccion = Convert.ToDateTime(sdr.GetValue(sdr.GetOrdinal("Rec_FecAccion")));
                        reclamaciones.Rec_FecConformidad = Convert.ToDateTime(sdr.GetValue(sdr.GetOrdinal("Rec_FecConformidad")));
                    }
                    else                   
                        reclamaciones.Rec_FecConformidad = null;                   
                    reclamaciones.Rec_ConNombre = sdr.GetValue(sdr.GetOrdinal("Rec_ConNombre")).ToString();
                    reclamaciones.Rec_ConDepartamento = sdr.GetValue(sdr.GetOrdinal("Rec_ConDepartamento")).ToString();
                    reclamaciones.Rec_Comentarios = sdr.GetValue(sdr.GetOrdinal("Rec_Comentarios")).ToString();
                    reclamaciones.Cte_NomComercial = sdr.GetValue(sdr.GetOrdinal("Cte_NomComercial")).ToString();
                    reclamaciones.Accion = sdr.GetValue(sdr.GetOrdinal("Accion")).ToString();
                    reclamaciones.Estatus = sdr.GetValue(sdr.GetOrdinal("Estatus")).ToString();
                    reclamaciones.Nco_Descripcion = sdr.GetValue(sdr.GetOrdinal("Nco_Descripcion")).ToString();
                    reclamaciones.Rec_Estatus = sdr.GetValue(sdr.GetOrdinal("Rec_Estatus")).ToString();
                    lista.Add(reclamaciones);
                }
                capaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {                
                throw ex;
            }
        }
                
        /// <summary>
        /// Hace una consulta y trae un resultado de la base de datos
        /// </summary>
        /// <param name="reclamaciones">Entidad de las reclamaciones</param>
        /// <param name="conexion">Cadena de conexion a la base de datos</param>
        public void ConsultaReclamaciones(ref Reclamaciones reclamaciones, string conexion)
        {
            try
            {
                CD_Datos capaDatos = new CD_Datos(conexion);
                SqlDataReader sdr = null;

                string[] parametros = {
                                          "@Id_Emp",
                                          "@Id_Cd",
                                          "@Id_Rec"
                                      };
                object[] valores = { 
                                       reclamaciones.Id_Emp, 
                                       reclamaciones.Id_Cd, 
                                       reclamaciones.Id_Rec
                                   };

                SqlCommand sqlcmd = default(SqlCommand);

                sqlcmd = capaDatos.GenerarSqlCommand("spCapReclamaciones_Consulta", ref sdr, parametros, valores);

                while (sdr.Read())
                {
                    reclamaciones = new Reclamaciones();
                    reclamaciones.Id_Emp = Convert.ToInt32(sdr.GetValue(sdr.GetOrdinal("Id_Emp")));
                    reclamaciones.Id_Cd = Convert.ToInt32(sdr.GetValue(sdr.GetOrdinal("Id_Cd")));
                    reclamaciones.Id_Rec = Convert.ToInt32(sdr.GetValue(sdr.GetOrdinal("Id_Rec")));
                    reclamaciones.Id_Cte = Convert.ToInt32(sdr.GetValue(sdr.GetOrdinal("Id_Cte")));
                    reclamaciones.Rec_Fecha = Convert.ToDateTime(sdr.GetValue(sdr.GetOrdinal("Rec_Fecha")));
                    reclamaciones.Rec_Usuario = sdr.GetValue(sdr.GetOrdinal("Rec_Usuario")).ToString();
                    reclamaciones.Rec_Telefono = sdr.GetValue(sdr.GetOrdinal("Rec_Telefono")).ToString();
                    reclamaciones.Id_Ter = Convert.ToInt32(sdr.GetValue(sdr.GetOrdinal("Id_Ter")));
                    reclamaciones.Id_NoConf = Convert.ToInt32(sdr.GetValue(sdr.GetOrdinal("Id_NoConf")));
                    reclamaciones.Rec_Descripcion = sdr.GetValue(sdr.GetOrdinal("Rec_Descripcion")).ToString();
                    reclamaciones.Rec_CausaRaiz = sdr.GetValue(sdr.GetOrdinal("Rec_CausaRaiz")).ToString();

                    if (sdr.GetValue(sdr.GetOrdinal("Rec_Estatus")).ToString() == "A")                   
                        reclamaciones.Rec_FecAccion = Convert.ToDateTime(sdr.GetValue(sdr.GetOrdinal("Rec_FecAccion")));                   
                    else                   
                        reclamaciones.Rec_FecAccion = null;                   

                    reclamaciones.Rec_AcAccion1 = sdr.GetValue(sdr.GetOrdinal("Rec_AcAccion1")).ToString();
                    reclamaciones.Rec_AcAccion2 = sdr.GetValue(sdr.GetOrdinal("Rec_AcAccion2")).ToString();
                    reclamaciones.Rec_AcResponsable = sdr.GetValue(sdr.GetOrdinal("Rec_AcResponsable")).ToString();

                    if (sdr.GetValue(sdr.GetOrdinal("Rec_Estatus")).ToString() == "F")
                    {
                        reclamaciones.Rec_FecAccion = Convert.ToDateTime(sdr.GetValue(sdr.GetOrdinal("Rec_FecAccion")));
                        reclamaciones.Rec_FecConformidad = Convert.ToDateTime(sdr.GetValue(sdr.GetOrdinal("Rec_FecConformidad")));
                    }
                    else                    
                        reclamaciones.Rec_FecConformidad = null;
                   
                    reclamaciones.Rec_ConNombre = sdr.GetValue(sdr.GetOrdinal("Rec_ConNombre")).ToString();
                    reclamaciones.Rec_ConDepartamento = sdr.GetValue(sdr.GetOrdinal("Rec_ConDepartamento")).ToString();
                    reclamaciones.Rec_Comentarios = sdr.GetValue(sdr.GetOrdinal("Rec_Comentarios")).ToString();
                    reclamaciones.Id_tipo = Convert.ToInt32(sdr.GetValue(sdr.GetOrdinal("Id_Tipo")));
                    reclamaciones.Cte_NomComercial = sdr.GetValue(sdr.GetOrdinal("Cte_NomComercial")).ToString();
                    reclamaciones.Accion = sdr.GetValue(sdr.GetOrdinal("Accion")).ToString();
                    reclamaciones.Estatus = sdr.GetValue(sdr.GetOrdinal("Estatus")).ToString();
                    reclamaciones.Nco_Descripcion = sdr.GetValue(sdr.GetOrdinal("Nco_Descripcion")).ToString();
                    reclamaciones.Rec_Estatus = sdr.GetValue(sdr.GetOrdinal("Rec_Estatus")).ToString();
                }
                capaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Metodo que pone en estatus de Baja una reclamacion
        /// </summary>
        /// <param name="reclamaciones">Entidad de las reclamaciones</param>
        /// <param name="conexion">Cadena de conexion a la base de datos</param>
        /// <param name="verificador">Dijito que indica si se efectuo la operacion correctamente</param>
        public void BajaReclamaciones(Reclamaciones reclamaciones, string conexion, ref int verificador)
        {
            CapaDatos.CD_Datos CDDatos = new CapaDatos.CD_Datos(conexion);

            try
            {
                string[] parametros = {
                                            "@Id_Emp",
                                            "@Id_Cd",
                                            "@Id_Rec"
                                      };
                object[] valores = { 
                                        reclamaciones.Id_Emp,
                                        reclamaciones.Id_Cd,
                                        reclamaciones.Id_Rec
                                   };

                SqlCommand sqlcmd = CDDatos.GenerarSqlCommand("spCapReclamaciones_Baja", ref verificador, parametros, valores);
                CDDatos.LimpiarSqlcommand(ref sqlcmd);
            } 
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ImprimirReclamaciones(Reclamaciones reclamaciones, string conexion, ref int verificador)
        {
            CapaDatos.CD_Datos CDDatos = default(CD_Datos);
            SqlCommand sqlcmd = default(SqlCommand);    
            try
            {
                CDDatos = new CapaDatos.CD_Datos(conexion);
                string[] parametros = {
	                                        "@Id_Emp", 
	                                        "@Id_Cd", 
	                                        "@Id_Rec"
                                      };
                object[] valores = { 
                                       reclamaciones.Id_Emp,
                                       reclamaciones.Id_Cd,
                                       reclamaciones.Id_Rec
                                   };

                sqlcmd = CDDatos.GenerarSqlCommand("spCapReclamaciones_Imprimir", ref verificador, parametros, valores);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
