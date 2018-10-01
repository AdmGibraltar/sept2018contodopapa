using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using System.Data.SqlClient;
using System.IO;
using System.Collections;

namespace CapaDatos
{
    public class CD_ProGenPoliza_Compaq
    {
        public void FiltrarProGenPoliza(Sesion sesion, ProGenPoliza_Compaq proGenPoliza, ref ArrayList verificadorArr)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(sesion.Emp_Cnx);
                string Cuenta1 = proGenPoliza.FiltroCuenta1;
                string Cuenta2 = proGenPoliza.FiltroCuenta2;
                string Cuenta3 = proGenPoliza.FiltroCuenta3;
                int id_CD = proGenPoliza.FiltroCmbCentro;
                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Cal_Año", "@Cal_Mes" };
                object[] Valores = { sesion.Id_Emp, id_CD, proGenPoliza.FiltroAnhio, proGenPoliza.FiltroMes };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spProGenPolizaFecha_Consulta", ref dr, Parametros, Valores);
                List<ProGenPoliza_Compaq> List = new List<ProGenPoliza_Compaq>();

                ProGenPoliza_Compaq polizaCompaq;
                while (dr.Read())
                {
                    polizaCompaq = new ProGenPoliza_Compaq();
                    polizaCompaq.Id_Cal1 = (int)dr.GetValue(dr.GetOrdinal("Id_Cal"));
                    polizaCompaq.Cal_Actual1 = (bool)dr.GetValue(dr.GetOrdinal("Cal_Actual"));//aun no se usa
                    polizaCompaq.Cal_FechaIni1 = (DateTime)dr.GetValue(dr.GetOrdinal("Cal_FechaIni"));
                    polizaCompaq.Cal_FechaFin1 = (DateTime)dr.GetValue(dr.GetOrdinal("Cal_FechaFin"));                   
                    List.Add(polizaCompaq);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

                verificadorArr = this.FiltrarPorFechaPeriodo(sesion, List, Cuenta1, Cuenta2, Cuenta3, id_CD);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ArrayList FiltrarPorFechaPeriodo(Sesion sesion, List<ProGenPoliza_Compaq> List, string Cuenta1, string Cuenta2, string Cuenta3, int id_CD)
        {
             ArrayList arrText = new ArrayList();
            try
            {                              
                for (int i = 0; i < List.Count; i++)
                {
                    DateTime fechaIni = List[i].Cal_FechaIni1;
                    DateTime fechaFin = List[i].Cal_FechaFin1;
                    List<ProGenPoliza_Compaq> listFechas = FiltrarPorFecha(sesion, fechaIni, fechaFin, id_CD);
                    if (listFechas.Count > 0)
                        for (int j = 0; j < listFechas.Count; j++)
                        {
                            DateTime FechaPeriodo = listFechas[j].FechaFactura1;
                            string strFechaPeriodo = listFechas[j].FechaFactura1.ToString("yyyyMMdd");
                            if (!string.IsNullOrEmpty(strFechaPeriodo))
                            {
                                List<ProGenPoliza_Compaq> listFacturas = FiltrarPorFactura(sesion, FechaPeriodo, id_CD);
                                if (listFacturas.Count > 0)
                                {
                                    arrText.Add(encabezadoFormato(strFechaPeriodo, Cuenta1, Cuenta2, Cuenta3));
                                    for (int k = 0; k < listFacturas.Count; k++)
                                    {
                                        int idFac = listFacturas[k].Id_Fac1;
                                        string strFecha = listFacturas[k].Fac_Fecha1.ToString("yyyyMMdd");
                                        string strIdFac = listFacturas[k].Id_Fac1.ToString("00000");
                                        string strIdCte = listFacturas[k].Id_Cte1.ToString("0000");
                                        string strImporte = listFacturas[k].Fac_Importe1.ToString("00000000000000000.00");//
                                        string strSubtotal = listFacturas[k].Fac_SubTotal1.ToString("00000000000000000.00");
                                        string strCosto = listFacturas[k].Costo1.ToString("00000000000000000.00");//
                                        string strIva = listFacturas[k].Fac_ImporteIva1.ToString("00000000000000000.00");/*
                                        string strImporte = listFacturas[k].Fac_Importe1.ToString("##0.00");//
                                        string strSubtotal = listFacturas[k].Fac_SubTotal1.ToString("##0.00");
                                        string strCosto = listFacturas[k].Costo1.ToString("##0.00");//
                                        string strIva = listFacturas[k].Fac_ImporteIva1.ToString("##0.00");*/
                                        detalleFormato(idFac, strIdFac, strIdCte, strImporte, strSubtotal, strCosto, strIva, ref arrText);
                                    }
                                }
                            }
                        }                  
                }             
            }
            catch (Exception ex)
            {
                throw ex;
            }           
            return arrText;
        }

        public string encabezadoFormato(string strFechaPeriodo, string Cuenta1, string Cuenta2, string Cuenta3)
        {          
            string encabezado = string.Empty;
            int intCuenta1 = Cuenta1.Length + 1;
            int intCuenta2 = Cuenta2.Length + 1;            
            int intCuenta3 = Cuenta3.Length + 10;
            encabezado = String.Format("{0,-3}", "P");//fijo
            encabezado += String.Format("{0,-12}", strFechaPeriodo);
            encabezado += String.Format("{0,-8}", "3");//fijo
            encabezado += String.Format("{0,-" + intCuenta1 + "}", Cuenta1);
            encabezado += String.Format("{0,-" + intCuenta2 + "}", Cuenta2);
            encabezado += String.Format("{0,-" + intCuenta3 + "}", Cuenta3);
            encabezado += String.Format("{0,-80}", "VENTAS");//fijo
            encabezado += String.Format("{0,-2}", "1");//fijo
            encabezado += String.Format("{0,-2}", "1");//fijo
            encabezado += String.Format("{0,-2}", "0");//fijo
           /* encabezado += String.Format("{0,-2}", "1");//fijo
            encabezado += String.Format("{0,-2}", "1");//fijo
            encabezado += String.Format("{0,-2}", "0");//fijo*/
            return encabezado;
        }

        public void detalleFormato(int idFac, string strIdFac, string strIdCte, string strImporte, string strSubtotal, string strCosto, string strIva, ref ArrayList arrText)
        {    
            /*detalle*/
            string detalle = string.Empty;    
            detalle = String.Format("{0,-3}", "M");//fijo
            detalle += String.Format("{0,-4}", "1103200");//fijo  
            //detalle += String.Format("{0,-4}", "1103200");//fijo  
            detalle += String.Format("{0,-24}", strIdCte);
            detalle += String.Format("{0,-11}", "FACT " + strIdFac);
            detalle += String.Format("{0,-2}", "0");//fijo
            detalle += String.Format("{0,-21}", strImporte);
            /*detalle += String.Format("{0,-11}", "0");//fijo
            detalle += String.Format("{0,-21}", "0.0");//fijo*/
            detalle += String.Format("{0,46}", "VENTAS DEL DIA");//fijo
            detalle += String.Format("{0,80}", "2");//fijo
            arrText.Add(detalle);
            /*detalle1*/
            string detalle1 = string.Empty;
            detalle1 = String.Format("{0,-3}", "M");//fijo            
            detalle1 += String.Format("{0,-31}", "40000010001");//fijo            
            detalle1 += String.Format("{0,-11}", "FACT " + strIdFac);
            detalle1 += String.Format("{0,-2}", "1");//fijo
            detalle1 += String.Format("{0,-21}", strSubtotal);
            /*detalle1 += String.Format("{0,-11}", "0");//fijo
            detalle1 += String.Format("{0,-21}", "0.0");//fijo*/
            detalle1 += String.Format("{0,46}", "VENTAS DEL DIA");//fijo
            //detalle1 += String.Format("{0,-104}", "VENTAS DEL DIA");//fijo
            detalle1 += String.Format("{0,80}", "2");//fijo
            arrText.Add(detalle1);
            /*detalle2*/
            string detalle2 = string.Empty;
            detalle2 = String.Format("{0,-3}", "M");//fijo            
            detalle2 += String.Format("{0,-31}", "21040000001");//fijo            
            detalle2 += String.Format("{0,-11}", "FACT " + strIdFac);
            detalle2 += String.Format("{0,-2}", "1");//fijo
            detalle2 += String.Format("{0,-21}", strIva);
            /*detalle2 += String.Format("{0,-11}", "0");//fijo
            detalle2 += String.Format("{0,-21}", "0.0");//fijo*/
            detalle2 += String.Format("{0,46}", "VENTAS DEL DIA");//fijo
            detalle2 += String.Format("{0,80}", "2");//fijo
            arrText.Add(detalle2);
            /*detalle3*/
            string detalle3 = string.Empty;
            detalle3 = String.Format("{0,-3}", "M");//fijo            
            detalle3 += String.Format("{0,-31}", "11040000002");//fijo            
            detalle3 += String.Format("{0,-11}", "FACT " + strIdFac);
            detalle3 += String.Format("{0,-2}", "1");//fijo
            detalle3 += String.Format("{0,-21}", strCosto);
            /*detalle3 += String.Format("{0,-11}", "0");//fijo
            detalle3 += String.Format("{0,-21}", "0.0");//fijo*/
            detalle3 += String.Format("{0,46}", "VENTAS DEL DIA");//fijo
            detalle3 += String.Format("{0,80}", "2");//fijo
            arrText.Add(detalle3);
            /*detalle4*/
            string detalle4 = string.Empty;
            detalle4 = String.Format("{0,-3}", "M");//fijo            
            detalle4 += String.Format("{0,-31}", "50000000001");//fijo            
            detalle4 += String.Format("{0,-11}", "FACT " + strIdFac);
            detalle4 += String.Format("{0,-2}", "0");//fijo
            detalle4 += String.Format("{0,-21}", strCosto);            /*
            detalle4 += String.Format("{0,-11}", "0");//fijo
            detalle4 += String.Format("{0,-21}", "0.0");//fijo*/
            detalle4 += String.Format("{0,46}", "VENTAS DEL DIA");//fijo
            detalle4 += String.Format("{0,80}", "2");//fijo
            arrText.Add(detalle4);           
        }

        public List<ProGenPoliza_Compaq> FiltrarPorFecha(Sesion sesion, DateTime fechaIni, DateTime fechaFin, int id_CD)
        {
            List<ProGenPoliza_Compaq> list = new List<ProGenPoliza_Compaq>();
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(sesion.Emp_Cnx);

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@FechaIni", "@FechaFin" };
                object[] Valores = { sesion.Id_Emp, id_CD /*sesion.Id_Cd*/, fechaIni, fechaFin };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spProGenPolizaFechaFiltro_Consulta", ref dr, Parametros, Valores);
               
                ProGenPoliza_Compaq polizaCompaq;
                while (dr.Read())
                {
                    polizaCompaq = new ProGenPoliza_Compaq();
                    polizaCompaq.FechaFactura1 = (DateTime)dr.GetValue(dr.GetOrdinal("Fac_Fecha"));                    
                    list.Add(polizaCompaq);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);                
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return list;
        }

        public List<ProGenPoliza_Compaq> FiltrarPorFactura(Sesion sesion, DateTime Fecha, int id_CD)
        {
            List<ProGenPoliza_Compaq> list = new List<ProGenPoliza_Compaq>();
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(sesion.Emp_Cnx);

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Fecha" };
                object[] Valores = { sesion.Id_Emp, id_CD /*sesion.Id_Cd*/, Fecha };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spProGenPolizaFiltroFacturas_Consulta", ref dr, Parametros, Valores);

                ProGenPoliza_Compaq polizaCompaq;
                while (dr.Read())
                {
                    polizaCompaq = new ProGenPoliza_Compaq();
                    polizaCompaq.Fac_Fecha1 = (DateTime)dr.GetValue(dr.GetOrdinal("Fac_Fecha"));
                    polizaCompaq.Id_Fac1 = (int)dr.GetValue(dr.GetOrdinal("Id_Fac"));
                    polizaCompaq.Id_Cte1 = (int)dr.GetValue(dr.GetOrdinal("Id_Cte"));
                    polizaCompaq.Fac_Importe1 = (double)dr.GetValue(dr.GetOrdinal("Fac_Importe"));
                    polizaCompaq.Fac_SubTotal1 = (double)dr.GetValue(dr.GetOrdinal("Fac_SubTotal"));
                    polizaCompaq.Costo1 = (double)dr.GetValue(dr.GetOrdinal("Costo"));
                    polizaCompaq.Fac_ImporteIva1 = (double)dr.GetValue(dr.GetOrdinal("Fac_ImporteIva")); 
                    list.Add(polizaCompaq);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return list;
        }
        
        private void guardarTexto(ArrayList arrText)
        {// true- Agregar a txt -- false- sobreEscribir en txt
            try
            {
                const string fic = @"~\polizavta.txt";         
                StreamWriter sw = new StreamWriter(fic, false, Encoding.UTF8);
                if (arrText.Count > 0)
                    for (int i = 0; i < arrText.Count; i++)
                        sw.WriteLine(arrText[i].ToString());
                sw.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }     
    }
}
