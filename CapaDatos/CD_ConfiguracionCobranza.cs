using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using CapaEntidad;
using System.Data;

namespace CapaDatos
{
    public class CD_ConfiguracionCobranza
    {
        public void Guardar(List<Acciones> list_acciones, List<Alertas> list_alertas,List<ConfigCredito> list_credito, Reglas reglas, CobProceso CobProceso, int Id_Emp, string Conexion, ref int verificador)
        {
            CD_Datos CapaDatos = new CD_Datos(Conexion);
            try
            {
                CapaDatos.StartTrans();

                string[] Parametros;
                object[] Valores;
                SqlCommand sqlcmd = default(SqlCommand);

                //ACCIONES
                Parametros = new string[] { "@Id_Emp" };
                Valores = new object[] { Id_Emp };
                sqlcmd = CapaDatos.GenerarSqlCommand("spCatConfCobranzaAccion_Eliminar", ref verificador, Parametros, Valores);
                int Id_Accion = 0;

                foreach (Acciones a in list_acciones)
                {
                    Parametros = new string[] { "@Id_Emp", "@Id_Tipo", "@Conf_Caso", "@Conf_Pregunta", "@Conf_Tpregunta" };
                    Valores = new object[] { Id_Emp, a.Etapa, a.Dias, a.Pregunta, a.Tipo_Respuesta };
                    sqlcmd = CapaDatos.GenerarSqlCommand("spCatConfCobranzaAccion_Guardar", ref verificador, Parametros, Valores);
                    Id_Accion = verificador;
                    int contador = 0;
                    foreach (string s in a.Respuestas)
                    {
                        contador++;
                        Parametros = new string[] { "@Id_Emp", "@Id_conf", "@Id_confD", "@Conf_Respuesta" };
                        Valores = new object[] { Id_Emp, Id_Accion, contador, s };
                        sqlcmd = CapaDatos.GenerarSqlCommand("spCatConfCobranzaRespuestas_Guardar", ref verificador, Parametros, Valores);
                    }
                }

                //ALERTAS
                Parametros = new string[] { "@Id_Emp" };
                Valores = new object[] { Id_Emp };
                sqlcmd = CapaDatos.GenerarSqlCommand("spCatConfCobranzaAlerta_Eliminar", ref verificador, Parametros, Valores);

                foreach (Alertas a in list_alertas)
                {
                    Parametros = new string[] { "@Id_Emp", "@Id_Tipo", "@Conf_Caso", "@Id_Tu", "@Conf_SuspCredito" };
                    Valores = new object[] { Id_Emp, a.Etapa, a.Dias, a.EnviarA, a.SuspenderCredito };
                    sqlcmd = CapaDatos.GenerarSqlCommand("spCatConfCobranzaAlerta_Guardar", ref verificador, Parametros, Valores);
                }


                //REGLAS
                Parametros = new string[] { "@Id_Emp", "@Plazo", "@Id_Tu1", "@Id_Tu2", "@Id_Tu3", "@Val1", "@Val2", "@Val3", "@Val4", "@Val5", "@Val6" };
                Valores = new object[] { Id_Emp, reglas.Plazo == null ? 0 : reglas.Plazo, reglas.Id_Tu1, reglas.Id_Tu2, reglas.Id_Tu3, reglas.Val1, reglas.Val2, reglas.Val3, reglas.Val4, reglas.Val5, reglas.Val6 };
                sqlcmd = CapaDatos.GenerarSqlCommand("spCatConfCobranzaRegla_Guardar", ref verificador, Parametros, Valores);

                int consecutivo  = 1;
                foreach (PeriodoGracia a in reglas.List_gracia)
                {
                    Parametros = new string[] { "@Id_Emp", "@Id_Reg", "@Reg_Condicion", "@Reg_Periodo"};
                    Valores = new object[] { Id_Emp, consecutivo++, a.Reg_Condicion, a.Reg_Periodo };
                    sqlcmd = CapaDatos.GenerarSqlCommand("spCatConfCobranzaPeriodoGracia_Insertar", ref verificador, Parametros, Valores);
                }


                //PROCESO
                Parametros = new string[] { "@Id_Emp", "@Id_Cd", "@SvtasAlm", "@EmbAlm", "@EntAlm", "@AlmCob", "@RevCob" };
                Valores = new object[] { CobProceso.Id_Emp, CobProceso.Id_Cd, CobProceso.SvtasAlm, CobProceso.EmbAlm, CobProceso.EntAlm, CobProceso.AlmCob, CobProceso.RevCob };
                sqlcmd = CapaDatos.GenerarSqlCommand("spCatConfCobranzaProceso_Guardar", ref verificador, Parametros, Valores);


                //Configuracion de credito
               
                foreach (ConfigCredito C in list_credito)
                {
                    Parametros = new string[] { "@Id_Tu", "@MaxDias" };
                    Valores = new object[] { C.Id_Tu, C.MaxDias };
                    sqlcmd = CapaDatos.GenerarSqlCommand("spCatConfCobranzaAutCredito_Insertar", ref verificador, Parametros, Valores);
                }


                CapaDatos.CommitTrans();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                CapaDatos.RollBackTrans();
                throw ex;
            }
        }
        public void Consultar(ref List<PeriodoGracia> list_gracia, ref List<Acciones> list_acciones, ref List<Alertas> list_alertas, int Id_Emp, string db, ref Reglas reglas, string Conexion)
        {
            try
            {
                CD_Datos CapaDatos = new CD_Datos(Conexion);
                string[] Parametros = { "@Id_Emp", "@db" };
                object[] Valores = { Id_Emp, db };
                DataSet ds = null;
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatConfCobranza_Consultar", ref ds, Parametros, Valores);

                Acciones acciones;
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    acciones = new Acciones();

                    acciones.GUID = Guid.NewGuid().ToString();
                    acciones.Id_Conf = Convert.ToInt32(dr["Id_Conf"]);
                    acciones.Etapa = dr["Etapa"].ToString();
                    acciones.EtapaStr = dr["EtapaStr"].ToString();
                    acciones.Dias = Convert.ToDouble(dr["Dias"]);
                    acciones.Tipo_Respuesta = dr["Tipo_Respuesta"].ToString();
                    acciones.Tipo_RespuestaStr = dr["Tipo_RespuestaStr"].ToString();
                    acciones.Pregunta = dr["Pregunta"].ToString();
                    acciones.Respuestas = new System.Collections.ArrayList();
                    acciones.RespuestasStr = "";
                    list_acciones.Add(acciones);
                }

                foreach (DataRow dr in ds.Tables[1].Rows)
                {
                    acciones = list_acciones.Where(Acciones => Acciones.Id_Conf == Convert.ToInt32(dr["Id_Conf"])).ToList()[0];
                    acciones.Respuestas.Add(dr["Conf_Respuesta"].ToString());

                    if (acciones.RespuestasStr != "")
                    {
                        acciones.RespuestasStr += ", ";
                    }

                    acciones.RespuestasStr += dr["Conf_Respuesta"].ToString();
                }

                Alertas alertas;
                foreach (DataRow dr in ds.Tables[2].Rows)
                {
                    alertas = new Alertas();

                    alertas.GUID = Guid.NewGuid().ToString();
                    alertas.Etapa = dr["Etapa"].ToString();
                    alertas.EtapaStr = dr["EtapaStr"].ToString();
                    alertas.Dias = Convert.IsDBNull(dr["Dias"]) ? (Double?)null : Convert.ToDouble(dr["Dias"]);
                    alertas.EnviarA = Convert.ToInt32(dr["EnviarA"]);
                    alertas.EnviarAStr = dr["EnviarAStr"].ToString();
                    alertas.SuspenderCredito = Convert.ToBoolean(dr["SuspenderCredito"]);
                    alertas.SuspenderCreditoStr = dr["SuspenderCreditoStr"].ToString();
                    list_alertas.Add(alertas);
                }

                reglas.Plazo = ds.Tables[3].Select("Id_Regla = 1").Length == 0 ? null : ds.Tables[3].Select("Id_Regla = 1")[0]["Reg_Valor"];

                reglas.Id_Tu1 = ds.Tables[3].Select("Id_Regla = 2").Length == 0 ? null : ds.Tables[3].Select("Id_Regla = 2")[0]["Reg_Valor"];
                reglas.Id_Tu2 = ds.Tables[3].Select("Id_Regla = 3").Length == 0 ? null : ds.Tables[3].Select("Id_Regla = 3")[0]["Reg_Valor"];
                reglas.Id_Tu3 = ds.Tables[3].Select("Id_Regla = 4").Length == 0 ? null : ds.Tables[3].Select("Id_Regla = 4")[0]["Reg_Valor"];

                reglas.Val1 = ds.Tables[3].Select("Id_Regla = 5").Length == 0 ? null : ds.Tables[3].Select("Id_Regla = 5")[0]["Reg_Valor"];
                reglas.Val2 = ds.Tables[3].Select("Id_Regla = 6").Length == 0 ? null : ds.Tables[3].Select("Id_Regla = 6")[0]["Reg_Valor"];
                reglas.Val3 = ds.Tables[3].Select("Id_Regla = 7").Length == 0 ? null : ds.Tables[3].Select("Id_Regla = 7")[0]["Reg_Valor"];
                reglas.Val4 = ds.Tables[3].Select("Id_Regla = 8").Length == 0 ? null : ds.Tables[3].Select("Id_Regla = 8")[0]["Reg_Valor"];
                reglas.Val5 = ds.Tables[3].Select("Id_Regla = 9").Length == 0 ? null : ds.Tables[3].Select("Id_Regla = 9")[0]["Reg_Valor"];
                reglas.Val6 = ds.Tables[3].Select("Id_Regla = 10").Length == 0 ? null : ds.Tables[3].Select("Id_Regla = 10")[0]["Reg_Valor"];

                PeriodoGracia periodogracia;
                foreach (DataRow dr in ds.Tables[4].Rows)
                {
                    periodogracia = new PeriodoGracia();

                    periodogracia.GUID = dr["Reg_Condicion"].ToString();
                    periodogracia.Reg_Condicion = Convert.ToInt32(dr["Reg_Condicion"]);
                    periodogracia.Reg_Periodo = Convert.ToInt32(dr["Reg_Periodo"]);
                    list_gracia.Add(periodogracia);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ConsultarCobProceso(ref CobProceso CobProceso, string Conexion)
        {
            try
            {
                CD_Datos CapaDatos = new CD_Datos(Conexion);
                string[] Parametros = { "@Id_Emp", "@Id_Cd" };
                object[] Valores = { CobProceso.Id_Emp, CobProceso.Id_Cd };
                SqlDataReader dr = null;
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatConfCobranzaProceso_Consultar", ref dr, Parametros, Valores);

                if (dr.HasRows)
                {
                    dr.Read();
                    CobProceso.Id_Emp = dr.IsDBNull(dr.GetOrdinal("Id_Emp")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Emp")));
                    CobProceso.Id_Cd = dr.IsDBNull(dr.GetOrdinal("Id_Cd")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cd")));
                    CobProceso.SvtasAlm = dr.IsDBNull(dr.GetOrdinal("SvtasAlm")) ? true : Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("SvtasAlm")));
                    CobProceso.EmbAlm = dr.IsDBNull(dr.GetOrdinal("EmbAlm")) ? true : Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("EmbAlm")));
                    CobProceso.EntAlm = dr.IsDBNull(dr.GetOrdinal("EntAlm")) ? true : Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("EntAlm")));
                    CobProceso.AlmCob = dr.IsDBNull(dr.GetOrdinal("AlmCob")) ? true : Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("AlmCob")));
                    CobProceso.RevCob = dr.IsDBNull(dr.GetOrdinal("RevCob")) ? true : Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("RevCob")));
                    
                   
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
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
                 SqlDataReader dr = null;
                 CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                 string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_Cte" };
                 object[] Valores = { Id_Emp, Id_Cd, Id_Cte };

                 SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spFacturasVencidas", ref dr, Parametros, Valores);
                 Factura factura;

                 while (dr.Read())
                 {
                     factura = new Factura();                    
                     factura.Id_FacSerie = dr.GetValue(dr.GetOrdinal("Id_FacSerie")).ToString();
                     factura.Fac_Importe = double.Parse(dr.GetValue(dr.GetOrdinal("Fac_Importe")).ToString());
                     factura.Fac_Saldo = double.Parse(dr.GetValue(dr.GetOrdinal("Saldo")).ToString());
                     factura.Dias = (int)dr.GetValue(dr.GetOrdinal("Dias"));
                     list.Add(factura);
                 }

                 CapaDatos.LimpiarSqlcommand(ref sqlcmd);
             }
             catch (Exception ex)
             {
                 throw ex;
             }
         }
        public void ConsultarConfiguCredito (ref List<ConfigCredito> List,int Id_Emp, int Id_Cd, string Conexion)
        {
            try
            {
                CD_Datos cd_datos = new CD_Datos(Conexion);
                SqlDataReader dr = null;

                string[] Parametros = {"@Id_Emp",
                                        "@Id_Cd"};

                object[] Valores = {Id_Emp, 
                                    Id_Cd };

                SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("spCatConfCobranzaAutCredito_Lista", ref dr, Parametros, Valores);

                ConfigCredito C;

                while (dr.Read())
                {
                    C = new ConfigCredito();
                    C.Id_Tu = Convert.ToInt32(dr["Id_Tu"]);
                    C.Tu_Descripcion = dr["Tu_Descripcion"].ToString();
                    C.MaxDias = Convert.ToInt32(dr["MaxDias"]);
                    List.Add(C);
                }

                dr.Close();

                cd_datos.LimpiarSqlcommand(ref sqlcmd);


            }
            catch (Exception ex)
            {
                
                throw ex;
            }

        }

    }
}
