using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using CapaEntidad;

namespace CapaDatos
{
    public class CD_GestorCobranza
    {
        public void ConsultarBitacora(CapaEntidad.Cobranza cob, ref string bitacora, string Conexion)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                SqlDataReader dr = null;

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_FacSerie", "@Id_Cte" };
                object[] Valores = { cob.Id_Emp, cob.Id_Cd, cob.Id_FacSerie, cob.Id_Cte };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spBitacora_Consultar", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    bitacora = dr.GetValue(dr.GetOrdinal("Bitacora")).ToString();
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarBitacora(CapaEntidad.Cobranza cob, ref List<CapaEntidad.Pregunta> list, string Conexion)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                SqlDataReader dr = null;

                string[] Parametros = { "@Id_Emp", "@Tipo", "@Caso", "@Id_FacSerie", "@Id_Cte" };
                object[] Valores = { cob.Id_Emp, cob.Tipo, cob.Caso, cob.Id_FacSerie, cob.Id_Cte };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spAcciones_Consultar", ref dr, Parametros, Valores);
                Pregunta pregunta;
                int contador = 1;
                while (dr.Read())
                {
                    if (dr.GetValue(dr.GetOrdinal("Resultado")).ToString() == "-1")
                    {
                        break;
                    }
                    else
                    {
                        pregunta = new Pregunta();

                        pregunta.Id_Pre = contador;
                        pregunta.pregunta = dr.GetValue(dr.GetOrdinal("Conf_Pregunta")).ToString();
                        pregunta.tpregunta = dr.GetValue(dr.GetOrdinal("Conf_Tpregunta")).ToString();
                        pregunta.respuestas = dr.GetValue(dr.GetOrdinal("Conf_Respuestas")).ToString().Split(new string[] { "\"" }, StringSplitOptions.RemoveEmptyEntries);
                        list.Add(pregunta);
                        contador++;
                    }
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarDocumentos(Cobranza cob, ref System.Data.DataSet ds, string Conexion)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);


                string[] Parametros = { "@Id_Emp", "@Id_Cd_Ver", "@Id_Cd", "@Id_U", "@Db", "@Id_TCd" };
                object[] Valores = { cob.Id_Emp, cob.Id_Cd_Ver, cob.Id_Cd, cob.Id_U, cob.DbName, cob.Id_TCd };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spMonitor_Consultar", ref ds, Parametros, Valores);

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertarBitacora(Bitacora bit, List<Pregunta> list_preg, ref int verificador, string Conexion)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {
                CapaDatos.StartTrans();

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_FacSerie", "@Bit_Fecha", "@Id_Tipo", "@Bit_Caso", "@Bit_Importe", "@Bit_Saldo", "@Id_Cte" };
                object[] Valores = { bit.Id_Emp, bit.Id_Cd, bit.Id_FacSerie, bit.Bit_Fecha, bit.Bit_Tipo, bit.Bit_Dias, bit.Bit_Importe, bit.Bit_Saldo, bit.Id_Cte };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spBitacora_Insertar", ref verificador, Parametros, Valores);

                if (verificador > 0)
                {
                    Parametros = new string[] { "@Id_Emp", "@Id_Cd", "@Id_Bit", "@Id_BitP", "@BitD_Pregunta", "@BitD_Respuesta" };
                    int verificador2 = 0;
                    int contador = 1;
                    foreach (Pregunta p in list_preg)
                    {
                        Valores = new object[] { bit.Id_Emp, bit.Id_Cd, verificador, contador, p.pregunta, p.respuesta_final };
                        sqlcmd = CapaDatos.GenerarSqlCommand("spBitacoraPreguntas_Insertar", ref verificador2, Parametros, Valores);
                        contador++;
                    }
                    CapaDatos.CommitTrans();
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                CapaDatos.RollBackTrans();
                throw ex;
            }
        }

        public void ConsultarRelaciones(Cobranza cob, ref List<RelacionGestor> list, string Conexion)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                SqlDataReader dr = null;

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_U", "@DataBase" };
                object[] Valores = { cob.Id_Emp, cob.Id_Cd, cob.Id_U, cob.DbName };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatRelacionGestor_Consultar", ref dr, Parametros, Valores);

                RelacionGestor rg;
                while (dr.Read())
                {
                    rg = new RelacionGestor();

                    rg.Id_Emp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Emp")));
                    rg.Id_Cd = dr.GetValue(dr.GetOrdinal("Id_Cd")).ToString();
                    rg.Cd_Nombre = dr.GetValue(dr.GetOrdinal("Cd_Nombre")).ToString();
                    rg.Id_Cte = dr.IsDBNull(dr.GetOrdinal("Id_Cte")) ? (double?)null : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Id_Cte")));
                    rg.Cte_Nombre = dr.GetValue(dr.GetOrdinal("Cte_Nombre")).ToString();
                    rg.Id_Ter = dr.IsDBNull(dr.GetOrdinal("Id_Ter")) ? (double?)null : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Id_Ter")));
                    rg.Ter_Nombre = dr.GetValue(dr.GetOrdinal("Ter_Nombre")).ToString();
                    rg.GUID = System.Guid.NewGuid().ToString();
                    list.Add(rg);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConfirmarRevision(FacturaRevisionCobro FacturaRevisionCobro, ref int verificador, string Conexion, string dbname)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {
                CapaDatos.StartTrans();
                SqlCommand sqlcmd = new SqlCommand();

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_Fac","@Id_Frc" , "@EnviarA" , "@Frc_Cheque", "@Frc_Efectivo", "@Db"};
              


                foreach (FacturaRevisionCobroDet revision in FacturaRevisionCobro.ListaFacturaRevisionCobroDet)
                {

                    object[] Valores = { revision.Id_Emp, revision.Id_Cd, revision.Frc_Doc, revision.Id_Frc, revision.Frc_EnviarA,
                                        revision.Frc_Cheque, revision.Frc_Efectivo,  dbname };

                     sqlcmd = CapaDatos.GenerarSqlCommand("spProSeguimientoCobranza_ConfirmarRevision", ref verificador, Parametros, Valores);
                    
                }

                CapaDatos.CommitTrans();

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
                verificador = 1;
              
            }
            catch (Exception ex)
            {
                CapaDatos.RollBackTrans();
                throw ex;
            }
        }

        public void GraficaEntrega(ref List<object> list, Usuario usu, string Conexion)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                SqlDataReader dr = null;

                string[] Parametros = { "@Db", "@Id_Emp", "@Id_Cd", "@Id_U", "@Id_Cd_Ver", "@Id_TCd" };
                object[] Valores = { usu.DbName, usu.Id_Emp, usu.Id_Cd, usu.Id_U, usu.Id_Cd_Ver, usu.Id_TCd};

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spGraficaEntregaAlmacen", ref dr, Parametros, Valores);
                if (dr.HasRows)
                {
                    dr.Read();
                    list.Add(Convert.ToInt32(dr.GetValue(dr.GetOrdinal("CIENXCIENTO"))));
                    list.Add(Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ENTREGADAS"))));
                    list.Add(Convert.ToInt32(dr.GetValue(dr.GetOrdinal("PENDIENTES"))));
                    list.Add(Convert.ToDouble(dr.GetValue(dr.GetOrdinal("PORCENTAJE"))));
                    list.Add(Convert.ToDouble(dr.GetValue(dr.GetOrdinal("EFECTIVIDAD"))));
                }


                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void GraficaEntrega_Saldos(ref List<double> list, Usuario usu, string Conexion)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                SqlDataReader dr = null;

                string[] Parametros = { "@Db", "@Id_Emp", "@Id_Cd", "@Id_U", "@Id_Cd_Ver", "@Id_TCd" };
                object[] Valores = { usu.DbName, usu.Id_Emp, usu.Id_Cd, usu.Id_U, usu.Id_Cd_Ver, usu.Id_TCd };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spGraficaEntregaAlmacen_Saldos", ref dr, Parametros, Valores);
                if (dr.HasRows)
                {
                    dr.Read();
                    list.Add(Convert.ToDouble(dr.GetValue(dr.GetOrdinal("CIENXCIENTO"))));
                    list.Add(Convert.ToDouble(dr.GetValue(dr.GetOrdinal("ENTREGADAS"))));
                    list.Add(Convert.ToDouble(dr.GetValue(dr.GetOrdinal("PENDIENTES"))));
                    list.Add(Convert.ToDouble(dr.GetValue(dr.GetOrdinal("PORCENTAJE"))));
                    list.Add(Convert.ToDouble(dr.GetValue(dr.GetOrdinal("EFECTIVIDAD"))));
                }


                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public void GraficaCobranza(ref List<object> list, Usuario usu, string Conexion)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                SqlDataReader dr = null;

                string[] Parametros = { "@Db", "@Id_Emp", "@Id_Cd", "@Id_U", "@Id_Cd_Ver", "@Id_TCd" };
                object[] Valores = { usu.DbName, usu.Id_Emp, usu.Id_Cd, usu.Id_U, usu.Id_Cd_Ver, usu.Id_TCd };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spGraficaEntregaCobranza", ref dr, Parametros, Valores);
                if (dr.HasRows)
                {
                    dr.Read();
                    list.Add(Convert.ToInt32(dr.GetValue(dr.GetOrdinal("CIENXCIENTO"))));
                    list.Add(Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ENTREGADAS"))));
                    list.Add(Convert.ToInt32(dr.GetValue(dr.GetOrdinal("PENDIENTES"))));
                    list.Add(Convert.ToDouble(dr.GetValue(dr.GetOrdinal("PORCENTAJE"))));
                    list.Add(Convert.ToDouble(dr.GetValue(dr.GetOrdinal("EFECTIVIDAD"))));
                }


                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void GraficaCobranza_Saldos(ref List<double> list, Usuario usu, string Conexion)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                SqlDataReader dr = null;

                string[] Parametros = { "@Db", "@Id_Emp", "@Id_Cd", "@Id_U", "@Id_Cd_Ver", "@Id_TCd" };
                object[] Valores = { usu.DbName, usu.Id_Emp, usu.Id_Cd, usu.Id_U, usu.Id_Cd_Ver, usu.Id_TCd };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spGraficaEntregaCobranza_Saldos", ref dr, Parametros, Valores);
                if (dr.HasRows)
                {
                    dr.Read();
                    list.Add(Convert.ToDouble(dr.GetValue(dr.GetOrdinal("CIENXCIENTO"))));
                    list.Add(Convert.ToDouble(dr.GetValue(dr.GetOrdinal("ENTREGADAS"))));
                    list.Add(Convert.ToDouble(dr.GetValue(dr.GetOrdinal("PENDIENTES"))));
                    list.Add(Convert.ToDouble(dr.GetValue(dr.GetOrdinal("PORCENTAJE"))));
                    list.Add(Convert.ToDouble(dr.GetValue(dr.GetOrdinal("EFECTIVIDAD"))));
                }


                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
       
        public void GraficaRevision(ref List<object> list, Usuario usu, string Conexion)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                SqlDataReader dr = null;

                string[] Parametros = { "@Db", "@Id_Emp", "@Id_Cd", "@Id_U", "@Id_Cd_Ver", "@Id_TCd" };
                object[] Valores = { usu.DbName, usu.Id_Emp, usu.Id_Cd, usu.Id_U, usu.Id_Cd_Ver, usu.Id_TCd };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spGraficaRevision", ref dr, Parametros, Valores);
                if (dr.HasRows)
                {
                    dr.Read();
                    list.Add(Convert.ToInt32(dr.GetValue(dr.GetOrdinal("CIENXCIENTO"))));
                    list.Add(Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ENTREGADAS"))));
                    list.Add(Convert.ToInt32(dr.GetValue(dr.GetOrdinal("PENDIENTES"))));
                    list.Add(Convert.ToDouble(dr.GetValue(dr.GetOrdinal("PORCENTAJE"))));
                    list.Add(Convert.ToDouble(dr.GetValue(dr.GetOrdinal("EFECTIVIDAD"))));
                }


                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void GraficaRevision_Saldos(ref List<double> list, Usuario usu, string Conexion)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                SqlDataReader dr = null;

                string[] Parametros = { "@Db", "@Id_Emp", "@Id_Cd", "@Id_U", "@Id_Cd_Ver", "@Id_TCd" };
                object[] Valores = { usu.DbName, usu.Id_Emp, usu.Id_Cd, usu.Id_U, usu.Id_Cd_Ver, usu.Id_TCd };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spGraficaRevision_Saldos", ref dr, Parametros, Valores);
                if (dr.HasRows)
                {
                    dr.Read();
                    list.Add(Convert.ToDouble(dr.GetValue(dr.GetOrdinal("CIENXCIENTO"))));
                    list.Add(Convert.ToDouble(dr.GetValue(dr.GetOrdinal("ENTREGADAS"))));
                    list.Add(Convert.ToDouble(dr.GetValue(dr.GetOrdinal("PENDIENTES"))));
                    list.Add(Convert.ToDouble(dr.GetValue(dr.GetOrdinal("PORCENTAJE"))));
                    list.Add(Convert.ToDouble(dr.GetValue(dr.GetOrdinal("EFECTIVIDAD"))));
                }


                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
      
        public void GraficaVencidas(ref List<object> list, Usuario usu, string Conexion)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                SqlDataReader dr = null;

                string[] Parametros = { "@Db", "@Id_Emp", "@Id_Cd", "@Id_U", "@Id_Cd_Ver", "@Id_TCd" };
                object[] Valores = { usu.DbName, usu.Id_Emp, usu.Id_Cd, usu.Id_U, usu.Id_Cd_Ver, usu.Id_TCd };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spGraficaVencidos", ref dr, Parametros, Valores);
                if (dr.HasRows)
                {
                    dr.Read();
                    list.Add(Convert.ToInt32(dr.GetValue(dr.GetOrdinal("CIENXCIENTO"))));
                    list.Add(Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ENTREGADAS"))));
                    list.Add(Convert.ToInt32(dr.GetValue(dr.GetOrdinal("PENDIENTES"))));
                    list.Add(Convert.ToDouble(dr.GetValue(dr.GetOrdinal("PORCENTAJE"))));
                    list.Add(Convert.ToDouble(dr.GetValue(dr.GetOrdinal("EFECTIVIDAD"))));
                }


                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void GraficaVencidas_Saldos(ref List<double> list, Usuario usu, string Conexion)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                SqlDataReader dr = null;

                string[] Parametros = { "@Db", "@Id_Emp", "@Id_Cd", "@Id_U", "@Id_Cd_Ver", "@Id_TCd" };
                object[] Valores = { usu.DbName, usu.Id_Emp, usu.Id_Cd, usu.Id_U, usu.Id_Cd_Ver, usu.Id_TCd };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spGraficaVencidos_Saldos", ref dr, Parametros, Valores);
                if (dr.HasRows)
                {
                    dr.Read();
                    list.Add(Convert.ToDouble(dr.GetValue(dr.GetOrdinal("CIENXCIENTO"))));
                    list.Add(Convert.ToDouble(dr.GetValue(dr.GetOrdinal("ENTREGADAS"))));
                    list.Add(Convert.ToDouble(dr.GetValue(dr.GetOrdinal("PENDIENTES"))));
                    list.Add(Convert.ToDouble(dr.GetValue(dr.GetOrdinal("PORCENTAJE"))));
                    list.Add(Convert.ToDouble(dr.GetValue(dr.GetOrdinal("EFECTIVIDAD"))));
                }


                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GraficaDiasVencidos(ref List<Comun> list, Usuario usu, string Conexion)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                SqlDataReader dr = null;

                string[] Parametros = { "@Db", "@Id_Emp", "@Id_Cd", "@Id_U", "@Id_Cd_Ver", "@Id_TCd" };
                object[] Valores = { usu.DbName, usu.Id_Emp, usu.Id_Cd, usu.Id_U, usu.Id_Cd_Ver, usu.Id_TCd };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spGraficaDiasVencidos", ref dr, Parametros, Valores);
                Comun comun;
                while(dr.Read())
                {
                    comun = new Comun();
                    comun.ValorDateTime = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Fecha")));
                    comun.ValorDoble = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Valor")));
                    list.Add(comun);
                }


                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GraficaCosto(ref List<Comun> list, Usuario usu, string Conexion)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                SqlDataReader dr = null;

                string[] Parametros = { "@Db", "@Id_Emp", "@Id_Cd", "@Id_U", "@Id_Cd_Ver", "@Id_TCd" };
                object[] Valores = { usu.DbName, usu.Id_Emp, usu.Id_Cd, usu.Id_U, usu.Id_Cd_Ver, usu.Id_TCd };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spGraficaCosto", ref dr, Parametros, Valores);
                Comun comun;
                while (dr.Read())
                {
                    comun = new Comun();
                    comun.ValorDateTime = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Fecha")));
                    comun.ValorDoble = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Valor")));
                    list.Add(comun);
                }


                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GraficaRotacion(ref List<Comun> list, Usuario usu, string Conexion)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                SqlDataReader dr = null;

                string[] Parametros = { "@Db", "@Id_Emp", "@Id_Cd", "@Id_U", "@Id_Cd_Ver", "@Id_TCd" };
                object[] Valores = { usu.DbName, usu.Id_Emp, usu.Id_Cd, usu.Id_U, usu.Id_Cd_Ver, usu.Id_TCd };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spGraficaRotacion", ref dr, Parametros, Valores);
                Comun comun;
                while (dr.Read())
                {
                    comun = new Comun();
                    comun.ValorDateTime = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Fecha")));
                    comun.ValorDoble = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Valor")));
                    list.Add(comun);
                }


                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConfirmarRecibidoSvtas(Cobranza cob, ref int verificador, string Conexion)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);


                string[] Parametros = { "@Id_Emp", "@Id_Cd", "Id_Fac", "@Db" };
                object[] Valores = { cob.Id_Emp, cob.Id_Cd, cob.Id_Fac, cob.DbName };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spProSeguimientoCobranza_ConfirmarEntregaSvta", ref verificador, Parametros, Valores);

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
