using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using System.Data;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class CD_CapDevolucionRemision
    {
        public void Consulta_Lista(DevolucionRemision devolRemision, string Conexion, ref List<DevolucionRemision> List)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = {
                                        "@Id_Emp", 
                                        "@Id_Cd",
                                        "@Folio1",
                                        "@Folio2",
                                        "@Fecha1",
                                        "@Fecha2",
                                        "@Estatus",
                                        "@Id_Cte1",
                                        "@Id_Cte2",
                                        "@Id_Tm"
                                     
                                      };
                object[] Valores = { 
                                       devolRemision.Id_Emp, 
                                       devolRemision.Id_Cd,
                                       devolRemision.Folio1,
                                       devolRemision.Folio2,
                                       devolRemision.Fecha1,
                                       devolRemision.Fecha2,
                                       devolRemision.Estatus == "" ? (object)null: devolRemision.Estatus,
                                       devolRemision.Id_CteFiltro1,
                                       devolRemision.Id_CteFiltro2,
                                       devolRemision.Id_Tm
                                                                           
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapDevolucionRemision_Lista", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    devolRemision = new DevolucionRemision();
                    devolRemision.Id_Emp = (int)dr.GetValue(dr.GetOrdinal("Id_Emp"));
                    devolRemision.Id_Cd = (int)dr.GetValue(dr.GetOrdinal("Id_Cd"));
                    devolRemision.Id_DevRem = (int)dr.GetValue(dr.GetOrdinal("Id_DevRem"));
                    devolRemision.Id_U = (int)dr.GetValue(dr.GetOrdinal("Id_U"));
                    devolRemision.DevRem_UNombre = (string)dr.GetValue(dr.GetOrdinal("U_Nombre"));
                    devolRemision.Id_Cte = (int)dr.GetValue(dr.GetOrdinal("Id_Cte"));
                    devolRemision.DevRem_CteNombre = (string)dr.GetValue(dr.GetOrdinal("DevRem_CteNombre"));
                    if (!dr.IsDBNull(dr.GetOrdinal("Id_Fac")))
                    {
                        devolRemision.Id_Fac = (int)dr.GetValue(dr.GetOrdinal("Id_Fac"));
                    }
                    devolRemision.DevRem_Fecha = (DateTime)dr.GetValue(dr.GetOrdinal("DevRem_Fecha"));
                    devolRemision.Id_Tm = (int)dr.GetValue(dr.GetOrdinal("Id_Tm"));
                    devolRemision.DevRem_TmNombre = (string)dr.GetValue(dr.GetOrdinal("Tm_nombre"));
                    devolRemision.Estatus = (string)dr.GetValue(dr.GetOrdinal("DevRem_Estatus"));
                    devolRemision.EstatusStr = (string)dr.GetValue(dr.GetOrdinal("DevRem_EstatusStr"));
                    devolRemision.NumEntradas = dr["NumEntradas"].ToString();
                    devolRemision.DevRem_Tipo = (string)dr.GetValue(dr.GetOrdinal("DevRem_Tipo"));

                    List.Add(devolRemision);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void ConsultaRemisionSaldo(DevolucionRemision rd, string Conexion, ref List<RemisionDet> List)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = {
                                        "@Id_Emp",
                                        "@Id_Cd",                                                                               
                                        "@Id_Cte",
                                        "@TipoRemision",
                                        "@Id_TG"
                                       
                                      };
                object[] Valores = { 
                                        rd.Id_Emp,
                                        rd.Id_Cd,                                                                                
                                        rd.Id_Cte,
                                        rd.Id_Tm,
                                        rd.Id_TG
                                                                              
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spRep_RemisionesSaldo", ref dr, Parametros, Valores);


                int a = 0;
                while (dr.Read())
                {
                    RemisionDet rd_prd = new RemisionDet();
                    rd_prd.Id_Ter = (int)dr.GetValue(dr.GetOrdinal("Id_Ter"));
                    rd_prd.Ter_Nombre = (string)dr.GetValue(dr.GetOrdinal("Ter_Nombre"));
                    rd_prd.Id_Cte = (int)dr.GetValue(dr.GetOrdinal("Id_Cte"));
                    rd_prd.Cte_NomComercial = (string)dr.GetValue(dr.GetOrdinal("Cte_NomComercial"));
                    rd_prd.Id_Prd = (int)dr.GetValue(dr.GetOrdinal("Id_Prd"));
                    rd_prd.Prd_Descripcion = (string)dr.GetValue(dr.GetOrdinal("Prd_Descripcion"));
                    rd_prd.Rem_Cant = (int)dr.GetValue(dr.GetOrdinal("SaldoUnidades"));
                    rd_prd.Rem_Precio = (double)dr.GetValue(dr.GetOrdinal("Prd_Pesos"));
                    rd_prd.Rem_CantE = (int)dr.GetValue(dr.GetOrdinal("CatDevuelta"));

                    List.Add(rd_prd);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public int ConsultaMovInverso(int id_emp, int id_tm, string Conexion)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            int verificador = 0;
            object[] Valores1 = {    
                                id_emp
                                ,id_tm
                                };
            string[] Parametros1 = new string[] {
                                    "@Id_Emp",			
                                    "@Id_Tm_Rem"
                                };
            SqlCommand sqlcmd1 = CapaDatos.GenerarSqlCommand("spCatTmovimientoInverso_Consulta", ref verificador, Parametros1, Valores1);
            return verificador;

        }


        public void GuardarEntradaSalida(DevolucionRemision devRem, ref string verificadorStr, int strEmp, string Conexion, ref int Id_DevRem)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            verificadorStr = "0";
            try
            {
                CapaDatos.StartTrans();
                SqlCommand sqlcmd = new SqlCommand();

                int Id_Es = 0;


                string[] Parametros = { 
                                              "@Id_Emp",
	                                          "@Id_Cd",	
                                              "@Id_DevRem",
	                                          "@Id_U", 
	                                          "@Id_Cte",
	                                          "@DevRem_CteNombre",
	                                          "@Id_Fac", 
	                                          "@DevRem_Fecha",
	                                          "@Id_Tm",
	                                          "@DevRem_Estatus",
	                                          "@Id_Ter",
                                              "@DevRem_Tipo"
	                                          
                                          };
                object[] Valores = { 
                                            devRem.Id_Emp,
                                            devRem.Id_Cd,
                                            devRem.Id_DevRem,
                                            devRem.Id_U,
                                            devRem.Id_Cte,
                                            devRem.DevRem_CteNombre,
                                            devRem.Id_Fac,
                                            devRem.DevRem_Fecha,
                                            devRem.Id_Tm,
                                            devRem.Estatus,
                                            devRem.Id_Ter,
                                            devRem.DevRem_Tipo
                                       };

                sqlcmd = CapaDatos.GenerarSqlCommand("spCapDevolucionRemision_Insertar", ref Id_DevRem, Parametros, Valores);

                devRem.Id_DevRem = Id_DevRem;


                foreach (EntradaSalida entradaSalida in devRem.ListEntradaSalida)
                {

                    string[] ParametrosEs = { 
                                              "@Id_Emp",
	                                          "@Id_Cd",	
                                              "@Id_U",
	                                          "@Es_Naturaleza", 
	                                          "@Es_Fecha",
	                                          "@Id_Tm",
	                                          "@Id_Cte", 
	                                          "@Id_Pvd",
	                                          "@Es_Referencia",
	                                          "@Es_Notas",
	                                          "@Es_Subtotal",
	                                          "@Es_Iva",
	                                          "@Es_Total",
	                                          "@Es_Estatus",
                                              "@ManAut",
                                              "@Id_Ter"
                                              ,"@Es_CteCuentaNacional"
                                              ,"@Es_CteCuentaContNacional"
                                              ,"@Es_FechaReferencia"
                                          };
                    object[] ValoresEs = { 
                                            entradaSalida.Id_Emp,
                                            entradaSalida.Id_Cd,
                                            entradaSalida.Id_U,
                                            entradaSalida.Es_Naturaleza,
                                            entradaSalida.Es_Fecha,
                                            entradaSalida.Id_Tm,
                                            ((entradaSalida.Id_Cte==-1) ? (object)null: entradaSalida.Id_Cte),
                                            ((entradaSalida.Id_Pvd==-1) ? (object)null: entradaSalida.Id_Pvd), 
                                            entradaSalida.Es_Referencia,
                                            entradaSalida.Es_Notas,
                                            entradaSalida.Es_SubTotal,
                                            entradaSalida.Es_Iva,
                                            entradaSalida.Es_Total,
                                            entradaSalida.Es_Estatus
                                            ,0 //MANUAL
                                            ,entradaSalida.Id_Ter == -1 ? (object)null: entradaSalida.Id_Ter
                                            ,entradaSalida.Es_CteCuentaNacional
                                            ,entradaSalida.Es_CteCuentaContNacional
                                            ,entradaSalida.Es_FechaReferencia
                                       };
                    //inserta una entradaSalida
                    sqlcmd = CapaDatos.GenerarSqlCommand("spCapEntSal_Insertar", ref Id_Es, ParametrosEs, ValoresEs);

                    entradaSalida.Id_Es = Id_Es;

                    string[] ParametrosEsdet = new string[]{
                                                    "@Id_Emp",  
                                                    "@Id_Cd",  
                                                    "@Id_Es",  
                                                    "@Id_EsDet",  
                                                    "@EsDet_Naturaleza",  
                                                    "@Id_Tm",
                                                    "@Id_Ter",  
                                                    "@Es_BuenEstado",  
                                                    "@Id_Prd",  
                                                    "@Es_Cantidad",  
                                                    "@Es_Costo",  
                                                    "@Afct_OrdCompra", 
                                                    "@Afct_Precios", 
                                                    "@Prd_AgrupadoSpo",  
                                                    "@Id_Ref", 
                                                    "@Id_Pvd", 
                                                    "@Id_Cte", 
                                                    "@Fecha"
		                                        };
                    int Id_EsDet = 0;
                    foreach (EntradaSalidaDetalle detalle in entradaSalida.ListEntradaSalidaDetalle)
                    {
                        object[] ValoresDet = new object[]{
                                                        detalle.Id_Emp,
                                                        detalle.Id_Cd,
                                                        Id_Es,
                                                        Id_EsDet++,
                                                        entradaSalida.Es_Naturaleza,//<-- naturaleza del movimiento EntradaSalida
                                                        entradaSalida.Id_Tm,
                                                        detalle.Id_Ter,
                                                        detalle.Es_BuenEstado,
                                                        detalle.Id_Prd,
                                                        detalle.Es_Cantidad,
                                                        detalle.Es_Costo,
                                                        detalle.Afecta,
                                                        strEmp != detalle.Id_Emp,
                                                        detalle.Prd_AgrupadoSpo,
                                                        entradaSalida.Es_Referencia
                                                        ,entradaSalida.Id_Pvd
                                                        ,entradaSalida.Id_Cte
                                                        ,entradaSalida.Es_Fecha
		                                            };
                        sqlcmd = CapaDatos.GenerarSqlCommand("spCapEntSalDet_Insertar", ref verificadorStr, ParametrosEsdet, ValoresDet);
                    }



                    Parametros = new string[]{
                                                    "@Id_Emp",  
                                                    "@Id_Cd",  
                                                    "@Id_DevRem",  
                                                    "@Id_Cte",  
                                                    "@Id_Rem",  
                                                    "@Id_Prd",
                                                    "@Cant",  
                                                    "@Id_Ter",  
                                                    "@Id_Es"  
                                           
		                                        };

                    foreach (EntradaSalidaDetalle detalle in entradaSalida.ListEntradaSalidaDetalle)
                    {
                        Valores = new object[]{
                                                        detalle.Id_Emp,
                                                        detalle.Id_Cd,
                                                        devRem.Id_DevRem,
                                                        devRem.Id_Cte,                                                      
                                                        entradaSalida.Es_Referencia,
                                                        detalle.Id_Prd,                                                
                                                        detalle.Es_Cantidad,
                                                        detalle.Id_Ter,
                                                        Id_Es   
		                                            };

                        sqlcmd = CapaDatos.GenerarSqlCommand("spCapDevolucionRemisionDet_Insertar", ref verificadorStr, Parametros, Valores);
                    }

                    verificadorStr = "0";

                }
                CapaDatos.CommitTrans();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                CapaDatos.RollBackTrans();
                verificadorStr = "1";
                throw ex;
            }
        }



        public void GuardarEntradaSalidaAjuste(EntradaSalida entradaSalida, ref string verificadorStr, string Conexion)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            verificadorStr = "0";
            try
            {
                CapaDatos.StartTrans();
                SqlCommand sqlcmd = new SqlCommand();

                int Id_Es = 0;

                string[] ParametrosEs = { 
                                              "@Id_Emp",
	                                          "@Id_Cd",	
                                              "@Id_U",
	                                          "@Es_Naturaleza", 
	                                          "@Es_Fecha",
	                                          "@Id_Tm",
	                                          "@Id_Cte", 
	                                          "@Id_Pvd",
	                                          "@Es_Referencia",
	                                          "@Es_Notas",
	                                          "@Es_Subtotal",
	                                          "@Es_Iva",
	                                          "@Es_Total",
	                                          "@Es_Estatus",
                                              "@ManAut",
                                              "@Id_Ter"
                                              ,"@Es_CteCuentaNacional"
                                              ,"@Es_CteCuentaContNacional"
                                              ,"@Es_FechaReferencia"
                                          };
                object[] ValoresEs = { 
                                            entradaSalida.Id_Emp,
                                            entradaSalida.Id_Cd,
                                            entradaSalida.Id_U,
                                            entradaSalida.Es_Naturaleza,
                                            entradaSalida.Es_Fecha,
                                            entradaSalida.Id_Tm,
                                            ((entradaSalida.Id_Cte==-1) ? (object)null: entradaSalida.Id_Cte),
                                            ((entradaSalida.Id_Pvd==-1) ? (object)null: entradaSalida.Id_Pvd), 
                                            entradaSalida.Es_Referencia,
                                            entradaSalida.Es_Notas,
                                            entradaSalida.Es_SubTotal,
                                            entradaSalida.Es_Iva,
                                            entradaSalida.Es_Total,
                                            entradaSalida.Es_Estatus
                                            ,0 //MANUAL
                                            ,entradaSalida.Id_Ter == -1 ? (object)null: entradaSalida.Id_Ter
                                            ,entradaSalida.Es_CteCuentaNacional
                                            ,entradaSalida.Es_CteCuentaContNacional
                                            ,entradaSalida.Es_FechaReferencia
                                       };
                //inserta una entradaSalida
                sqlcmd = CapaDatos.GenerarSqlCommand("spCapEntSalAjuste_Insertar", ref Id_Es, ParametrosEs, ValoresEs);

                entradaSalida.Id_Es = Id_Es;


                string[] ParametrosEsdet = new string[]{
                                                    "@Id_Emp",  
                                                    "@Id_Cd",  
                                                    "@Id_Es",  
                                                    "@Id_EsDet",  
                                                    "@EsDet_Naturaleza",  
                                                    "@Id_Tm",
                                                    "@Id_Ter",  
                                                    "@Es_BuenEstado",  
                                                    "@Id_Prd",  
                                                    "@Es_Cantidad",  
                                                    "@Es_Costo",  
                                                    "@Afct_OrdCompra", 
                                                    "@Afct_Precios", 
                                                    "@Prd_AgrupadoSpo",  
                                                    "@Id_Ref", 
                                                    "@Id_Pvd", 
                                                    "@Id_Cte", 
                                                    "@Fecha"
		                                        };
                int Id_EsDet = 0;
                foreach (EntradaSalidaDetalle detalle in entradaSalida.ListaDetalle)
                {
                    if (detalle.Es_Cantidad != 0)
                    {
                        object[] ValoresDet = new object[]{
                                                        detalle.Id_Emp,
                                                        detalle.Id_Cd,
                                                        Id_Es,
                                                        Id_EsDet++,
                                                        entradaSalida.Es_Naturaleza,//<-- naturaleza del movimiento EntradaSalida
                                                        entradaSalida.Id_Tm,
                                                        entradaSalida.Id_Ter,
                                                        detalle.Es_BuenEstado,
                                                        detalle.Id_Prd,
                                                        detalle.Es_Cantidad,
                                                        detalle.Es_Costo,
                                                        detalle.Afecta,
                                                        detalle.Id_Emp,
                                                        detalle.Prd_AgrupadoSpo,
                                                        entradaSalida.Es_Referencia
                                                        ,entradaSalida.Id_Pvd
                                                        ,entradaSalida.Id_Cte
                                                        ,entradaSalida.Es_Fecha
		                                            };
                        sqlcmd = CapaDatos.GenerarSqlCommand("spCapEntSalDetAjuste_Insertar", ref verificadorStr, ParametrosEsdet, ValoresDet);
                    }
                }


                verificadorStr = "0";


                CapaDatos.CommitTrans();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                CapaDatos.RollBackTrans();
                verificadorStr = "1";
                throw ex;
            }
        }




        public void ConsultaRemisionProductoSaldoDetalle(DevolucionRemision rd, string Conexion, ref List<RemisionDet> List)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = {
                                        "@Id_Emp",
                                        "@Id_Cd",
                                        "@Id_Cte",
                                        "@TipoRemision",
                                        "@Id_Prd",
                                        "@Id_TG"
                                       
                                      };
                object[] Valores = { 
                                        rd.Id_Emp,
                                        rd.Id_Cd,                                                                                
                                        rd.Id_Cte,
                                        rd.Id_Tm,
                                        rd.Id_Prd,
                                        rd.Id_TG
                                                                              
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spRep_RemisionesVencidasProducto", ref dr, Parametros, Valores);

                int a = 0;
                while (dr.Read())
                {
                    RemisionDet rd_prd = new RemisionDet();
                    rd_prd.Id_Ter = (int)dr.GetValue(dr.GetOrdinal("Id_Ter"));
                    rd_prd.Ter_Nombre = (string)dr.GetValue(dr.GetOrdinal("Ter_Nombre"));
                    rd_prd.Id_Cte = (int)dr.GetValue(dr.GetOrdinal("Id_Cte"));
                    rd_prd.Id_Rem = (int)dr.GetValue(dr.GetOrdinal("Id_Rem"));
                    rd_prd.Cte_NomComercial = (string)dr.GetValue(dr.GetOrdinal("Cte_NomComercial"));
                    rd_prd.Id_Prd = (int)dr.GetValue(dr.GetOrdinal("Id_Prd"));
                    rd_prd.Prd_Descripcion = (string)dr.GetValue(dr.GetOrdinal("Prd_Descripcion"));
                    rd_prd.Rem_Cant = (int)dr.GetValue(dr.GetOrdinal("SaldoUnidades"));
                    rd_prd.Rem_Precio = (double)dr.GetValue(dr.GetOrdinal("Rem_Prec"));
                    rd_prd.Prd_Pesos = (double)dr.GetValue(dr.GetOrdinal("Prd_Pesos"));
                    rd_prd.Rem_CantE = (int)dr.GetValue(dr.GetOrdinal("SaldoUnidades"))/*(int)dr.GetValue(dr.GetOrdinal("CantDevuelta"))*/;
                    rd_prd.Rem_Fecha = (DateTime)dr.GetValue(dr.GetOrdinal("rem_fecha"));
                    rd_prd.Rem_Estatus = (string)dr.GetValue(dr.GetOrdinal("Rem_Estatus"));

                    List.Add(rd_prd);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void ConsultaRemisionProductoSaldoDetalleTotal(DevolucionRemision rd, string Conexion, ref List<RemisionDet> List)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = {
                                        "@Id_Emp",
                                        "@Id_Cd",                                       
                                        "@Id_Ter",
                                        "@Id_Cte",
                                        "@TipoRemision",
                                        "@Id_Prd"
                                       
                                      };
                object[] Valores = { 
                                        rd.Id_Emp,
                                        rd.Id_Cd,                                        
                                        rd.Id_Ter,
                                        rd.Id_Cte,
                                        rd.Id_Tm,
                                        rd.Id_Prd
                                                                              
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spRep_RemisionesVencidasProductoTotal", ref dr, Parametros, Valores);

                int a = 0;
                while (dr.Read())
                {
                    RemisionDet rd_prd = new RemisionDet();
                    rd_prd.Id_Ter = (int)dr.GetValue(dr.GetOrdinal("Id_Ter"));
                    rd_prd.Ter_Nombre = (string)dr.GetValue(dr.GetOrdinal("Ter_Nombre"));
                    rd_prd.Id_Cte = (int)dr.GetValue(dr.GetOrdinal("Id_Cte"));
                    rd_prd.Id_Rem = (int)dr.GetValue(dr.GetOrdinal("Id_Rem"));
                    rd_prd.Cte_NomComercial = (string)dr.GetValue(dr.GetOrdinal("Cte_NomComercial"));
                    rd_prd.Id_Prd = (int)dr.GetValue(dr.GetOrdinal("Id_Prd"));
                    rd_prd.Prd_Descripcion = (string)dr.GetValue(dr.GetOrdinal("Prd_Descripcion"));
                    rd_prd.Rem_Cant = (int)dr.GetValue(dr.GetOrdinal("SaldoUnidades"));
                    rd_prd.Rem_Precio = (double)dr.GetValue(dr.GetOrdinal("Prd_Pesos"));
                    rd_prd.Rem_CantE = (int)dr.GetValue(dr.GetOrdinal("SaldoUnidades")) < 0 ? (int)dr.GetValue(dr.GetOrdinal("CantDevuelta")) : 0;
                    rd_prd.Rem_Fecha = (DateTime)dr.GetValue(dr.GetOrdinal("rem_fecha"));

                    List.Add(rd_prd);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public void ConsultaDevolucionHistorico(DevolucionRemision devolRemisionDet, string Conexion, ref List<DevolucionRemisionDet> List)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = {
                                        "@Id_Emp",
                                        "@Id_Cd",                                       
                                        "@Id_DevRem"
                                      
                                       
                                      };
                object[] Valores = { 
                                        devolRemisionDet.Id_Emp,
                                        devolRemisionDet.Id_Cd,                                        
                                        devolRemisionDet.Id_DevRem
                                                                              
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("sp_DevolucionRemisionDet_Consulta", ref dr, Parametros, Valores);

                int a = 0;
                while (dr.Read())
                {
                    DevolucionRemisionDet rd_prd = new DevolucionRemisionDet();
                    rd_prd.Id_Emp = (int)dr.GetValue(dr.GetOrdinal("Id_Emp"));
                    rd_prd.Id_Cd = (int)dr.GetValue(dr.GetOrdinal("Id_Cd"));
                    rd_prd.Id_DevRem = (int)dr.GetValue(dr.GetOrdinal("Id_DevRem"));
                    rd_prd.Id_Cte = (int)dr.GetValue(dr.GetOrdinal("Id_Cte"));
                    rd_prd.Id_Es = (int)dr.GetValue(dr.GetOrdinal("Id_Es"));
                    rd_prd.Prd_Descripcion = dr.GetValue(dr.GetOrdinal("Prd_Descripcion")).ToString();
                    rd_prd.Id_Rem = (int)dr.GetValue(dr.GetOrdinal("Id_Rem"));
                    rd_prd.Id_Prd = (int)dr.GetValue(dr.GetOrdinal("Id_Prd"));
                    rd_prd.Cant = (int)dr.GetValue(dr.GetOrdinal("Cant"));
                    rd_prd.Id_Ter = (int)dr.GetValue(dr.GetOrdinal("Id_Ter"));

                    List.Add(rd_prd);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Consulta(ref DevolucionRemision devolRemision, string Conexion, int Id_Emp, int Id_Cd, int folio)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", 
                                        "@Id_Cd",
                                        "@Folio"
                                      };
                object[] Valores = { 
                                       Id_Emp, 
                                       Id_Cd,
                                       folio
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spDevolucionRemision_Consulta", ref dr, Parametros, Valores);

                if (dr.HasRows)
                {
                    dr.Read();
                    devolRemision = new DevolucionRemision();
                    devolRemision.Id_Emp = (int)dr.GetValue(dr.GetOrdinal("Id_Emp"));
                    devolRemision.Id_Cd = (int)dr.GetValue(dr.GetOrdinal("Id_Cd"));
                    devolRemision.Id_DevRem = (int)dr.GetValue(dr.GetOrdinal("Id_DevRem"));
                    devolRemision.Id_U = (int)dr.GetValue(dr.GetOrdinal("Id_U"));
                    devolRemision.DevRem_UNombre = (string)dr.GetValue(dr.GetOrdinal("U_Nombre"));
                    devolRemision.Id_Cte = (int)dr.GetValue(dr.GetOrdinal("Id_Cte"));
                    devolRemision.Id_Ter = (int)dr.GetValue(dr.GetOrdinal("Id_Ter"));
                    devolRemision.DevRem_CteNombre = (string)dr.GetValue(dr.GetOrdinal("DevRem_CteNombre"));

                    if (!dr.IsDBNull(dr.GetOrdinal("Id_Fac")))
                    {
                        devolRemision.Id_Fac = (int)dr.GetValue(dr.GetOrdinal("Id_Fac"));
                    }
                    devolRemision.DevRem_Fecha = (DateTime)dr.GetValue(dr.GetOrdinal("DevRem_Fecha"));
                    devolRemision.Id_Tm = (int)dr.GetValue(dr.GetOrdinal("Id_Tm"));
                    devolRemision.DevRem_TmNombre = (string)dr.GetValue(dr.GetOrdinal("Tm_nombre"));
                    devolRemision.Estatus = (string)dr.GetValue(dr.GetOrdinal("DevRem_Estatus"));
                    devolRemision.DevRem_Tipo = (string)dr.GetValue(dr.GetOrdinal("DevRem_Tipo"));

                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaPorFactura(ref DevolucionRemision devolRemision, ref bool pHasRows, string Conexion, int Id_Emp, int Id_Cd, int Id_Fac)
        {
            try
            {
                pHasRows = false;
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", 
                                        "@Id_Cd",
                                        "@Id_Fac"
                                      };
                object[] Valores = { 
                                       Id_Emp, 
                                       Id_Cd,
                                       Id_Fac
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("SpCapDevolucionRemisionFactura_Consulta", ref dr, Parametros, Valores);

                if (dr.HasRows)
                {
                    dr.Read();
                    pHasRows = true;
                    devolRemision = new DevolucionRemision();
                    devolRemision.Id_Emp = (int)dr.GetValue(dr.GetOrdinal("Id_Emp"));
                    devolRemision.Id_Cd = (int)dr.GetValue(dr.GetOrdinal("Id_Cd"));
                    devolRemision.Id_DevRem = (int)dr.GetValue(dr.GetOrdinal("Id_DevRem"));
                    devolRemision.Id_U = (int)dr.GetValue(dr.GetOrdinal("Id_U"));
                    devolRemision.DevRem_UNombre = (string)dr.GetValue(dr.GetOrdinal("U_Nombre"));
                    devolRemision.Id_Cte = (int)dr.GetValue(dr.GetOrdinal("Id_Cte"));
                    devolRemision.Id_Ter = (int)dr.GetValue(dr.GetOrdinal("Id_Ter"));
                    devolRemision.DevRem_CteNombre = (string)dr.GetValue(dr.GetOrdinal("DevRem_CteNombre"));

                    if (!dr.IsDBNull(dr.GetOrdinal("Id_Fac")))
                    {
                        devolRemision.Id_Fac = (int)dr.GetValue(dr.GetOrdinal("Id_Fac"));
                    }
                    devolRemision.DevRem_Fecha = (DateTime)dr.GetValue(dr.GetOrdinal("DevRem_Fecha"));
                    devolRemision.Id_Tm = (int)dr.GetValue(dr.GetOrdinal("Id_Tm"));
                    devolRemision.DevRem_TmNombre = (string)dr.GetValue(dr.GetOrdinal("Tm_nombre"));
                    devolRemision.Estatus = (string)dr.GetValue(dr.GetOrdinal("DevRem_Estatus"));
                    devolRemision.DevRem_Tipo = (string)dr.GetValue(dr.GetOrdinal("DevRem_Tipo"));
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CancelaEntradas(string Conexion, int Id_Emp, int Id_Cd, int folio)
        {
            try
            {
                int verificador = 0;
                CapaDatos.CD_Datos CapaDatos = new CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", 
                                        "@Id_Cd",
                                        "@Folio"
                                      };
                object[] Valores = { 
                                       Id_Emp, 
                                       Id_Cd,
                                       folio
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spDevolucionRemision_CancelaEntradas", ref verificador, Parametros, Valores);

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaDetalle(ref DevolucionRemision devolRemision, string Conexion, int Id_Emp, int Id_Cd, int folio)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                DevolucionRemisionDet devRemDet = new DevolucionRemisionDet();

                string[] Parametros = { "@Id_Emp", 
                                        "@Id_Cd",
                                        "@Folio"
                                      };
                object[] Valores = { 
                                       Id_Emp, 
                                       Id_Cd,
                                       folio
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spDevolucionRemisionDet_Consulta", ref dr, Parametros, Valores);

                if (dr.HasRows)
                {
                    dr.Read();
                    devolRemision = new DevolucionRemision();
                    devolRemision.Id_Emp = (int)dr.GetValue(dr.GetOrdinal("Id_Emp"));
                    devolRemision.Id_Cd = (int)dr.GetValue(dr.GetOrdinal("Id_Cd"));
                    devolRemision.Id_DevRem = (int)dr.GetValue(dr.GetOrdinal("Id_DevRem"));
                    devolRemision.Id_U = (int)dr.GetValue(dr.GetOrdinal("Id_U"));
                    devolRemision.DevRem_UNombre = (string)dr.GetValue(dr.GetOrdinal("U_Nombre"));
                    devolRemision.Id_Cte = (int)dr.GetValue(dr.GetOrdinal("Id_Cte"));
                    devolRemision.Id_Ter = (int)dr.GetValue(dr.GetOrdinal("Id_Ter"));
                    devolRemision.DevRem_CteNombre = (string)dr.GetValue(dr.GetOrdinal("DevRem_CteNombre"));

                    if (!dr.IsDBNull(dr.GetOrdinal("Id_Fac")))
                    {
                        devolRemision.Id_Fac = (int)dr.GetValue(dr.GetOrdinal("Id_Fac"));
                    }
                    devolRemision.DevRem_Fecha = (DateTime)dr.GetValue(dr.GetOrdinal("DevRem_Fecha"));
                    devolRemision.Id_Tm = (int)dr.GetValue(dr.GetOrdinal("Id_Tm"));
                    devolRemision.DevRem_TmNombre = (string)dr.GetValue(dr.GetOrdinal("Tm_nombre"));
                    devolRemision.Estatus = (string)dr.GetValue(dr.GetOrdinal("DevRem_Estatus"));
                    devolRemision.Id_TmInv = (int)dr.GetValue(dr.GetOrdinal("Id_TmInv"));
                    devolRemision.Es_NatInv = (int)dr.GetValue(dr.GetOrdinal("Tm_NaturalezaInv"));
                    devolRemision.ListDevolucionRemisionDet = new List<DevolucionRemisionDet>();
                }

                if (dr.NextResult())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            devRemDet = new DevolucionRemisionDet();
                            devRemDet.Id_Emp = Convert.ToInt32(dr["Id_Emp"]);
                            devRemDet.Id_Cd = Convert.ToInt32(dr["Id_Cd"]);
                            devRemDet.Id_Cte = Convert.ToInt32(dr["Id_Cte"]);
                            devRemDet.Id_DevRem = Convert.ToInt32(dr["Id_DevRem"]);
                            devRemDet.Id_Prd = Convert.ToInt32(dr["Id_Prd"]);
                            devRemDet.Id_Rem = Convert.ToInt32(dr["Id_Rem"]);
                            devRemDet.Id_Es = Convert.ToInt32(dr["Id_Es"]);
                            devRemDet.Id_Ter = Convert.ToInt32(dr["Id_Ter"]);
                            devRemDet.Cant = Convert.ToInt32(dr["Cant"]);
                            devolRemision.ListDevolucionRemisionDet.Add(devRemDet);
                        }
                    }
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaDetallePorRemision(ref List<DevolucionRemisionDet> devolRemisionDet, string Conexion, int Id_Emp, int Id_Cd, int Id_Rem)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                DevolucionRemisionDet devRemDet = new DevolucionRemisionDet();

                string[] Parametros = { "@Id_Emp", 
                                        "@Id_Cd",
                                        "@Id_Rem"
                                      };
                object[] Valores = { 
                                       Id_Emp, 
                                       Id_Cd,
                                       Id_Rem
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spDevolucionRemisionDet_Remision_Consulta", ref dr, Parametros, Valores);

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        devRemDet = new DevolucionRemisionDet();
                        devRemDet.Id_Emp = Convert.ToInt32(dr["Id_Emp"]);
                        devRemDet.Id_Cd = Convert.ToInt32(dr["Id_Cd"]);
                        devRemDet.Id_Cte = Convert.ToInt32(dr["Id_Cte"]);
                        devRemDet.Id_DevRem = Convert.ToInt32(dr["Id_DevRem"]);
                        devRemDet.Id_Prd = Convert.ToInt32(dr["Id_Prd"]);
                        devRemDet.Id_Rem = Convert.ToInt32(dr["Id_Rem"]);
                        devRemDet.Id_Es = Convert.ToInt32(dr["Id_Es"]);
                        devRemDet.Id_Ter = Convert.ToInt32(dr["Id_Ter"]);
                        devRemDet.Cant = Convert.ToInt32(dr["Cant"]);
                        devolRemisionDet.Add(devRemDet);
                    }
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
