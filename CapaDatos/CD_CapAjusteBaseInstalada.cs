using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.SqlClient;
using System.Data;

using CapaEntidad;

namespace CapaDatos
{
    public class CD_CapAjusteBaseInstalada
    {
        public void ConsultarAjusteBaseInstalada_PorUnique(ref AjusteBaseInstalada ajusteBaseInstalada, string Conexion, ref bool encontrado)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { 
                                          "@Id_Emp" 
                                          ,"@Id_Cd"
                                          ,"@Abi_Unique"
                                      };
                object[] Valores = { 
                                       ajusteBaseInstalada.Id_Emp
                                       ,ajusteBaseInstalada.Id_Cd
                                       ,ajusteBaseInstalada.Abi_Unique
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spAjusteBaseInstalada_ConsultarPorUnique", ref dr, Parametros, Valores);
                encontrado = false;
                if (dr.HasRows)
                {
                    dr.Read();
                    ajusteBaseInstalada.Id_Abi = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Abi")));
                    ajusteBaseInstalada.Id_U = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_U")));
                    ajusteBaseInstalada.U_Nombre = dr.GetValue(dr.GetOrdinal("U_Nombre")).ToString();
                    ajusteBaseInstalada.Cd_Nombre = dr.GetValue(dr.GetOrdinal("Cd_Nombre")).ToString();
                    ajusteBaseInstalada.Abi_Fecha = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Abi_Fecha")));
                    if (dr.IsDBNull(dr.GetOrdinal("Abi_FechaAutoriza")))
                        ajusteBaseInstalada.Abi_FechaAutoriza = null;
                    else
                        ajusteBaseInstalada.Abi_FechaAutoriza = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Abi_FechaAutoriza")));
                    encontrado = true;
                }

                // ----------------------
                // consultar detalle
                // ----------------------
                dr.Close();
                string[] ParametrosDetalle = { 
                                          "@Id_Emp" 
                                          ,"@Id_Cd"
                                          ,"@Id_Abi"
                                      };
                object[] ValoresDetalle = { 
                                       ajusteBaseInstalada.Id_Emp
                                       ,ajusteBaseInstalada.Id_Cd
                                       ,ajusteBaseInstalada.Id_Abi
                                   };
                ajusteBaseInstalada.ListaAjusteBaseInstalada = new List<AjusteBaseInstaladaDet>();
                sqlcmd = CapaDatos.GenerarSqlCommand("spAjusteBaseInstaladaDetalle_Consultar", ref dr, ParametrosDetalle, ValoresDetalle);
                while (dr.Read())
                {
                    AjusteBaseInstaladaDet ajusteBaseInstaladaDet = new AjusteBaseInstaladaDet();
                    ajusteBaseInstaladaDet.Id_Emp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Emp")));
                    ajusteBaseInstaladaDet.Id_Cd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cd")));
                    ajusteBaseInstaladaDet.Id_Abi = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Abi")));
                    ajusteBaseInstaladaDet.Id_AbiDet = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_AbiDet")));
                    ajusteBaseInstaladaDet.Abi_Tipo = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Abi_Tipo")));
                    ajusteBaseInstaladaDet.Abi_TipoStr = dr.GetValue(dr.GetOrdinal("Abi_TipoStr")).ToString();

                    ajusteBaseInstaladaDet.Id_Ter_Origen = dr.IsDBNull(dr.GetOrdinal("Id_Ter_Origen")) ? (int?)null : (int?)dr.GetValue(dr.GetOrdinal("Id_Ter_Origen"));
                    ajusteBaseInstaladaDet.Id_Cte_Origen = dr.IsDBNull(dr.GetOrdinal("Id_Cte_Origen")) ? (int?)null : (int?)dr.GetValue(dr.GetOrdinal("Id_Cte_Origen"));
                    ajusteBaseInstaladaDet.Id_Prd_Origen = dr.IsDBNull(dr.GetOrdinal("Id_Prd_Origen")) ? (int?)null : (int?)dr.GetValue(dr.GetOrdinal("Id_Prd_Origen"));

                    if (dr.IsDBNull(dr.GetOrdinal("Abi_CantActual_Origen")))
                        ajusteBaseInstaladaDet.Abi_CantActual_Origen = null;
                    else
                        ajusteBaseInstaladaDet.Abi_CantActual_Origen = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Abi_CantActual_Origen")));
                    if (dr.IsDBNull(dr.GetOrdinal("Abi_CantQuitar_Origen")))
                        ajusteBaseInstaladaDet.Abi_CantQuitar_Origen = null;
                    else
                        ajusteBaseInstaladaDet.Abi_CantQuitar_Origen = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Abi_CantQuitar_Origen")));

                    ajusteBaseInstaladaDet.Id_Ter_Destino = dr.IsDBNull(dr.GetOrdinal("Id_Ter_Destino")) ? (int?)null : (int?)dr.GetValue(dr.GetOrdinal("Id_Ter_Destino"));
                    ajusteBaseInstaladaDet.Id_Cte_Destino = dr.IsDBNull(dr.GetOrdinal("Id_Cte_Destino")) ? (int?)null : (int?)dr.GetValue(dr.GetOrdinal("Id_Cte_Destino"));
                    ajusteBaseInstaladaDet.Id_Prd_Destino = dr.IsDBNull(dr.GetOrdinal("Id_Prd_Destino")) ? (int?)null : (int?)dr.GetValue(dr.GetOrdinal("Id_Prd_Destino"));

                    if (dr.IsDBNull(dr.GetOrdinal("Abi_CantActual_Destino")))
                        ajusteBaseInstaladaDet.Abi_CantActual_Destino = null;
                    else
                        ajusteBaseInstaladaDet.Abi_CantActual_Destino = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Abi_CantActual_Destino")));
                    if (dr.IsDBNull(dr.GetOrdinal("Abi_CantQuitar_Destino")))
                        ajusteBaseInstaladaDet.Abi_CantQuitar_Destino = null;
                    else
                        ajusteBaseInstaladaDet.Abi_CantQuitar_Destino = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Abi_CantQuitar_Destino")));

                    ajusteBaseInstaladaDet.Abi_ExplicacionCaso = dr.GetValue(dr.GetOrdinal("Abi_ExplicacionCaso")).ToString();
                    ajusteBaseInstaladaDet.Abi_Estatus = dr.GetValue(dr.GetOrdinal("Abi_Estatus")).ToString();
                    ajusteBaseInstaladaDet.Abi_EstatusStr = Nombre(dr.GetValue(dr.GetOrdinal("Abi_Estatus")).ToString());
                    ajusteBaseInstalada.ListaAjusteBaseInstalada.Add(ajusteBaseInstaladaDet);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string Nombre(string p)
        {
            string ret="";
            switch (p)
            {
                case "C": ret="Capturado"; break;
                case "A": ret="Autorizado"; break;
                case "R": ret="Rechazado"; break;
                default: ret=""; break;

            }
            return ret;
        }

        public void ModificarEstatusAjusteBaseInstalada(ref List<AjusteBaseInstaladaDet> listaAjusteBaseInstalada, string Conexion, ref int verificador)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {
                CapaDatos.StartTrans();

                string[] Parametros = { 
	                                    "@Id_Emp"
                                        ,"@Id_Cd"
                                        ,"@Id_Abi"
                                        ,"@Id_AbiDet"
                                        ,"@Abi_Estatus"
                                      };

                SqlCommand sqlcmd = new SqlCommand();
                object[] Valores = new object[5];
                foreach (AjusteBaseInstaladaDet ajusteBaseInstaladaDet in listaAjusteBaseInstalada)
                {
                    Valores[0] = ajusteBaseInstaladaDet.Id_Emp;
                    Valores[1] = ajusteBaseInstaladaDet.Id_Cd;
                    Valores[2] = ajusteBaseInstaladaDet.Id_Abi;
                    Valores[3] = ajusteBaseInstaladaDet.Id_AbiDet;
                    Valores[4] = ajusteBaseInstaladaDet.Abi_Estatus;

                    sqlcmd = CapaDatos.GenerarSqlCommand("spAjusteBaseInstaladaDetalle_ModificarEstatus", ref verificador, Parametros, Valores);
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



        public void Insertar(AjusteBaseInstalada cabezera, DataTable dt, string Conexion, ref string verificador)
        {
            string GUID = "";
            CapaDatos.CD_Datos CapaDatos = default(CD_Datos);
            SqlCommand sqlcmd = default(SqlCommand);
            try
            {
                CapaDatos = new CapaDatos.CD_Datos(Conexion);
                CapaDatos.StartTrans();
               
                    string[] Parametros = { 
                                          "@Id_Emp", 
                                          "@Id_Cd",
                                          "@Id_U",
                                          "@Abi_Fecha"
                                      };
                    object[] Valores = { 
                                        cabezera.Id_Emp,
                                        cabezera.Id_Cd,
                                        cabezera.Id_U,
                                        cabezera.Abi_Fecha
                                   };

 if (cabezera.Abi_Unique == "-1")
                {

                    sqlcmd = CapaDatos.GenerarSqlCommand("spCapAjusteBi_Insertar", ref verificador, Parametros, Valores);


                    GUID = verificador;
                }
                else
                {
                    GUID = cabezera.Abi_Unique;
                }
                Parametros = new string[]{ 
                                        "@Id_Emp",
                                        "@Id_Cd",
                                        "@Id_AbiDet",
                                        "@Abi_Tipo",
                                        "@Id_Ter_Origen",
                                        "@Id_Cte_Origen",
                                        "@Id_Prd_Origen",
                                        "@Abi_CantActual_Origen",
                                        "@Abi_CantQuitar_Origen",
                                        "@Id_Ter_Destino",
                                        "@Id_Cte_Destino",
                                        "@Id_Prd_Destino",
                                        "@Abi_CantActual_Destino",
                                        "@Abi_CantQuitar_Destino",
                                        "@Abi_ExplicacionCaso",
                                        "@Abi_Estatus",
                                        "@Abi_Unique"
                                      };
                int det = 1;
                foreach (DataRow dr in dt.Rows)
                {

                    Valores = new object[]{
                        cabezera.Id_Emp,
                        cabezera.Id_Cd,
                        det,
                        dr["Abi_Tipo"],
                        dr["Id_Ter_Origen"],
                        dr["Id_Cte_Origen"],
                        dr["Id_Prd_Origen"],
                        dr["Abi_CantActual_Origen"],
                        dr["Abi_CantQuitar_Origen"],
                        dr["Id_Ter_Destino"],
                        dr["Id_Cte_Destino"],
                        dr["Id_Prd_Destino"],
                        dr["Abi_CantActual_Destino"],
                        dr["Abi_CantQuitar_Destino"],
                        dr["Abi_ExplicacionCaso"],
                        dr["Abi_Estatus"],
                        GUID
                    };
                    sqlcmd = CapaDatos.GenerarSqlCommand("spCapAjusteBiDet_Insertar", ref verificador, Parametros, Valores);
                    det += 1;
                }

                verificador = GUID;
                CapaDatos.CommitTrans();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                CapaDatos.RollBackTrans();
                throw ex;
            }
        }
    }
}
