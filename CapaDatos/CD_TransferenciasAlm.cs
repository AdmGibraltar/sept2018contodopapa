using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using System.Data.SqlClient;
using System.Data;

namespace CapaDatos
{
    public class CD_TransferenciasAlm
    {
        public void CapRemision_ConsultaTransferencia(TransferenciasAlm TA, ref List<Remision> List, string Conexion)
        {
            try
            {
                CD_Datos cd_datos = new CD_Datos(Conexion);
                SqlDataReader dr = null;


                string[] Parametros={
                            "@Id_Cd",
                            "@Id_CteIni",
                            "@Id_CteFIn",
                            "@Id_Rem",
                            "@Rem_FechaIni",
                            "@Rem_FechaFin",
                            "@Rem_Estatus"

                           };
                object[] Valores = {
                                       TA.Id_Cd,
                                       TA.Id_CteIni ==  null ? (object) null : TA.Id_CteIni,
                                       TA.Id_CteFin ==  null ? (object) null : TA.Id_CteFin,
                                       TA.Id_Rem == null ? (object) null:TA.Id_Rem,
                                       TA.Rem_FechaIni == null ? (object) null: TA.Rem_FechaIni,
                                       TA.Rem_FechaFin == null ? (object) null: TA.Rem_FechaFin,
                                       TA.Rem_Estatus == null ? (object) null: TA.Rem_Estatus
                                   };

                SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("spCapRemision_ConsultaTransferencia", ref dr, Parametros, Valores);

                Remision r;

                while (dr.Read())
                {
                    r = new Remision();
                    r.Id_Emp = Convert.ToInt32(dr["Id_Emp"]);
                    r.Id_Cd = Convert.ToInt32(dr["Id_Cd"]);
                    r.Id_Rem = Convert.ToInt32(dr["Id_Rem"]);
                    r.Id_Cte = Convert.ToInt32(dr["Id_Cte"]);
                    r.Cte_NomComercial = dr["Cte_NomComercial"].ToString();
                    r.Rem_Fecha = Convert.ToDateTime(dr["Rem_Fecha"]);
                    r.Rem_Total = Convert.ToDouble(dr["Rem_Total"]);
                    r.Rem_Estatus = dr["Rem_Estatus"].ToString();
                    r.Rem_EstatusStr = dr["Rem_EstatusStr"].ToString();
                    r.Rem_ManAut = Convert.ToInt32(dr["Rem_ManAut"]);

                    List.Add(r);
                }

                cd_datos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
        public void CapRemision_ConsultaTransferenciaImprimir(TransferenciasAlm TA, ref DataTable dt, string Conexion)
        {
            try
            {
                CD_Datos cd_datos = new CD_Datos(Conexion);
                DataSet ds = null;
                string[] Parametros ={
                            "@Id_Cd",
                            "@Id_CteIni",
                            "@Id_CteFIn",
                            "@Id_Rem",
                            "@Rem_FechaIni",
                            "@Rem_FechaFin",
                            "@Rem_Estatus"

                           };
                object[] Valores = {
                                       TA.Id_Cd,
                                       TA.Id_CteIni ==  null ? (object) null : TA.Id_CteIni,
                                       TA.Id_CteFin ==  null ? (object) null : TA.Id_CteFin,
                                       TA.Id_Rem == null ? (object) null:TA.Id_Rem,
                                       TA.Rem_FechaIni == null ? (object) null: TA.Rem_FechaIni,
                                       TA.Rem_FechaFin == null ? (object) null: TA.Rem_FechaFin,
                                       TA.Rem_Estatus == null ? (object) null: TA.Rem_Estatus
                                   };

                SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("spCapRemision_ConsultaTransferencia", ref ds, Parametros, Valores);

                dt = ds.Tables[0];
                cd_datos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void ProTransferenciaAlmacen_Insertar(Remision remision, ref int Verificador, string Conexion)
        {
            try
            {
                CD_Datos cd_datos = new CD_Datos(Conexion);

                string[] Parametros = {
                                          "@Id_Emp",
                                          "@Id_Cd",
                                          "@Id_Rem"

                                      };
                object[] Valores = {
                                       remision.Id_Emp,
                                       remision.Id_Cd,
                                       remision.Id_Rem
                                   };

                SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("spProTransferenciaAlmacen_Insertar", ref Verificador, Parametros, Valores);

                cd_datos.LimpiarSqlcommand(ref sqlcmd);


            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
        public void ProTransferenciaAlmacen_ConsultaLista(TransferenciasAlm TA, ref List<TransferenciasAlm> List, string Conexion)
        {
            try
            {
                CD_Datos cd_datos = new CD_Datos(Conexion);
                SqlDataReader dr = null;

                string[] Parametros ={
                            "@Id_Cd",
                            "@Id_CdOrigenIni",
                            "@Id_CdOrigenFin",
                            "@Id_RemOrigen",
                            "@Tra_FechaIni",
                            "@Tra_FechaFin",
                            "@Tra_Estatus"

                           };
                object[] Valores = {
                                       TA.Id_Cd,
                                       TA.Id_CdOrigenIni ==  null ? (object) null : TA.Id_CdOrigenIni,
                                       TA.Id_CdOrigenFin ==  null ? (object) null : TA.Id_CdOrigenFin,
                                       TA.Id_RemOrigen == null ? (object) null:TA.Id_RemOrigen,
                                       TA.Tra_FechaIni == null ? (object) null: TA.Tra_FechaIni,
                                       TA.Tra_FechaFin == null ? (object) null: TA.Tra_FechaFin,
                                       TA.Tra_Estatus == null ? (object) null: TA.Tra_Estatus
                                   };

                SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("spProTransferenciaAlmacen_ConsultaLista", ref dr, Parametros, Valores);


                TransferenciasAlm t;

                while (dr.Read())
                {
                    t = new TransferenciasAlm();
                    t.Id_Emp = Convert.ToInt32(dr["Id_Emp"]);
                    t.Id_Cd = Convert.ToInt32(dr["Id_Cd"]);
                    t.Id_Tra = Convert.ToInt32(dr["Id_Tra"]);
                    t.Id_CdOrigen = Convert.ToInt32(dr["Id_CdOrigen"]);
                    t.Cd_Nombre = dr["Cd_Nombre"].ToString();
                    t.Id_RemOrigen = Convert.ToInt32(dr["Id_RemOrigen"]);
                    t.Tra_Importe = Convert.ToDouble(dr["Tra_Importe"]);
                    t.Tra_Estatus = dr["Tra_Estatus"].ToString();
                    t.Tra_EstatusStr = dr["Tra_EstatusStr"].ToString();
                    t.Tra_RemFecha = Convert.ToDateTime(dr["Tra_RemFecha"]);
                    t.Tra_FechaEnvio = Convert.ToDateTime(dr["Tra_FechaEnvio"]);
                    t.Tra_FechaRecepcion = dr["Tra_FechaRecepcion"].ToString() == "" ? (DateTime?)null : Convert.ToDateTime(dr["Tra_FechaRecepcion"]);
                    t.Id_Es = dr["Id_Es"].ToString() == "" ? (int?)null : Convert.ToInt32(dr["Id_Es"]);
                    List.Add(t);
                
                }

                cd_datos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
        public void ProTransferenciaAlmacen_ConsultaListaImprimir(TransferenciasAlm TA, ref DataTable dt, string Conexion)
        {
            try
            {
                CD_Datos cd_datos = new CD_Datos(Conexion);
                DataSet ds = null;

                string[] Parametros ={
                            "@Id_Cd",
                            "@Id_CdOrigenIni",
                            "@Id_CdOrigenFin",
                            "@Id_RemOrigen",
                            "@Tra_FechaIni",
                            "@Tra_FechaFin",
                            "@Tra_Estatus"

                           };
                object[] Valores = {
                                       TA.Id_Cd,
                                       TA.Id_CdOrigenIni ==  null ? (object) null : TA.Id_CdOrigenIni,
                                       TA.Id_CdOrigenFin ==  null ? (object) null : TA.Id_CdOrigenFin,
                                       TA.Id_RemOrigen == null ? (object) null:TA.Id_RemOrigen,
                                       TA.Tra_FechaIni == null ? (object) null: TA.Tra_FechaIni,
                                       TA.Tra_FechaFin == null ? (object) null: TA.Tra_FechaFin,
                                       TA.Tra_Estatus == null ? (object) null: TA.Tra_Estatus
                                   };

                SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("spProTransferenciaAlmacen_ConsultaLista", ref ds, Parametros, Valores);

                dt = ds.Tables[0];



                cd_datos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void ProTransferenciaAlmacen_BajaRemitente(Remision remision, ref int Verificador, string Conexion)
        {
            try
            {
                CD_Datos cd_datos = new CD_Datos(Conexion);

                string[] Parametros = {
                                          "@Id_Emp",
                                          "@Id_Cd",
                                          "@Id_Cte",
                                          "@Id_Rem"
                                      };
                object[] Valores = {
                                       remision.Id_Emp,
                                       remision.Id_Cd,
                                       remision.Id_Cte,
                                       remision.Id_Rem
                                   };

                SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("spProTransferenciaAlmacen_BajaRemitente", ref Verificador, Parametros, Valores);


                cd_datos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
        public void ProTransferenciaAlmacen_Consulta(int Id_Emp, int Id_Cd, int Id_Tra, ref TransferenciasAlm tra, string Conexion)
        {
            try
            {
                CD_Datos cd_datos = new CD_Datos(Conexion);
                SqlDataReader dr = null;

                string[] Parametros = {
                                          "@Id_Emp",
                                          "@Id_Cd",
                                          "@Id_Tra"
                                      };
                object[] Valores = {
                                       Id_Emp, 
                                       Id_Cd,
                                       Id_Tra
                                   };

                 SqlCommand sqlcmd =  cd_datos.GenerarSqlCommand("spProTransferenciaAlmacen_Consulta", ref dr, Parametros, Valores);

                if (dr.Read())
                {
                    tra.Id_Tra = Convert.ToInt32(dr["Id_Tra"]);
                    tra.Id_CdOrigen = Convert.ToInt32(dr["Id_CdOrigen"]);
                    tra.Tra_FechaEnvio = Convert.ToDateTime(dr["Tra_FechaEnvio"]);
                    tra.Tra_FechaRecepcion = dr["Tra_FechaRecepcion"].ToString() == "" ? (DateTime?)null : Convert.ToDateTime(dr["Tra_FechaRecepcion"]);
                    tra.Id_CdOrigenStr = dr["Id_CdOrigenStr"].ToString();
                    tra.Id_RemOrigen = Convert.ToInt32(dr["Id_RemOrigen"]);
                    tra.Tra_Notas = dr["Tra_Notas"].ToString();
                    tra.CD_IVA = Convert.ToDouble(dr["CD_IVA"]);
                    tra.Tra_Estatus = dr["Tra_Estatus"].ToString();
                }

                cd_datos.LimpiarSqlcommand(ref sqlcmd);


            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
        public void ProTransferenciaAlmacen_ConsultaDet(int Id_Emp, int Id_Cd, int Id_Tra, ref List<TransferenciaAlmDet> List, string Conexion)
        {
            try
            {

                CD_Datos cd_datos = new CD_Datos(Conexion);
                SqlDataReader dr = null;

                string[] Parametros = {
                                          "@Id_Emp",
                                          "@Id_Cd",
                                          "@Id_Tra"
                                      };
                object[] Valores = {
                                       Id_Emp, 
                                       Id_Cd,
                                       Id_Tra
                                   };

                SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("spProTransferenciaAlmacen_ConsultaDet", ref dr, Parametros, Valores);

                TransferenciaAlmDet t;
                while (dr.Read())
                {
                    t = new TransferenciaAlmDet();
                    t.Id_Emp = Convert.ToInt32(dr["Id_Emp"]);
                    t.Id_Cd = Convert.ToInt32(dr["Id_Cd"]);
                    t.Id_Tra = Convert.ToInt32(dr["Id_Tra"]);
                    t.Id_Prd = Convert.ToInt32(dr["Id_Prd"]);
                    t.Prd_Descripcion = dr["Prd_Descripcion"].ToString();
                    t.Prd_Presentacion = dr["Prd_Presentacion"].ToString();
                    t.TraD_Cant = Convert.ToInt32(dr["TraD_Cant"]);
                    t.TraD_CantRec = Convert.ToInt32(dr["TraD_CantRec"]);
                    t.TraD_Costo = Convert.ToDouble(dr["TraD_Costo"]);
                    t.TraD_TotalEnv = Convert.ToDouble(dr["TraD_TotalEnv"]);
                    t.TraD_TotalRec = Convert.ToDouble(dr["TraD_TotalRec"]);
                    t.TraD_Diferencia = Convert.ToInt32(dr["TraD_Diferencia"]);
                    t.UniqueID = Guid.NewGuid().ToString();

                    List.Add(t);
                }

                cd_datos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
        public void ProTransferenciaAlmacen_Recepcion(TransferenciasAlm tra,List<TransferenciaAlmDet> List, ref int Verificador,ref int Remision, string Conexion)
        {
            try
            {

                string[] ParametrosDet = {
                                                "@Id_Emp",
                                                "@Id_Cd",
                                                "@Id_Tra",
                                                "@Id_Prd",
                                                "@TraD_CantRec"
                                             };

                foreach (TransferenciaAlmDet d in List)
                {
                    CD_Datos cd_datos2 = new CD_Datos(Conexion);

                    object[] ValoresDet = {
                                                  d.Id_Emp,
                                                  d.Id_Cd,
                                                  d.Id_Tra,
                                                  d.Id_Prd,
                                                  d.TraD_CantRec
                                              };

                    SqlCommand sqlcmd2 = cd_datos2.GenerarSqlCommand("spProTransferenciaAlmacenDet_Recepcion", ref Verificador, ParametrosDet, ValoresDet);

                    cd_datos2.LimpiarSqlcommand(ref sqlcmd2);
                }


                if (Verificador == -1)
                {

                    CD_Datos cd_datos = new CD_Datos(Conexion);

                    string[] ParametrosEnc = {
                                         "@Id_Emp",
                                         "@Id_Cd",
                                         "@Id_Tra",
                                         "@Id_Es",
                                         "@Id_U"
                                      };
                    object[] ValoresEnc = {
                                          tra.Id_Emp,
                                          tra.Id_Cd,
                                          tra.Id_Tra,
                                          tra.Id_Es,
                                          tra.Id_U

                                      };

                    SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("spProTransferenciaAlmacen_Recepcion", ref Verificador, ParametrosEnc, ValoresEnc);

                    cd_datos.LimpiarSqlcommand(ref sqlcmd);

                    if (Verificador > 0)
                    {
                        //JMM:Si me regresa un valor mayor a cero quiere decir que no se recibio completamente la mercancía hago una remisón por las diferencias

                        CD_Datos cd_datos3 = new CD_Datos(Conexion);
                        System.Data.DataSet ds = null;

                        string[] ParametrosRem = {
                                                   "@Id_Emp",
                                                   "@Id_Cd",
                                                   "@Id_Tra",
                                                   "@Id_U"
                                                 };

                        object[] ValoresRem = {
                                                tra.Id_Emp,
                                                tra.Id_Cd,
                                                tra.Id_Tra,
                                                tra.Id_U
                                              };

                        SqlCommand sqlcmd3 = cd_datos3.GenerarSqlCommand("spProTransferenciaAlmacen_GeneraRemision", ref ds, ParametrosRem, ValoresRem);

                        System.Data.DataTable dt = ds.Tables[0];
                        foreach (DataRow dr in dt.Rows)
                        {
                            Remision = Convert.ToInt32(dr["Id_Rem"]);
                        }

                        cd_datos3.LimpiarSqlcommand(ref sqlcmd3);

                    }

                }
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
    }
}
