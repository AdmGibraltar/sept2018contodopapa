using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.SqlClient;
using System.Data;

using CapaEntidad;

namespace CapaDatos
{
    public class CD_CapNotaCredito
    {
        public void ConsultarCantidadNotaCreditoCentroDist(ref int verificador, int Id_Cd, string Conexion)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);


                string[] Parametros = { "@Id_Cd" };
                object[] Valores = { Id_Cd };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand(
                    "spCapNotaCreditoCantidadEnCd_Consultar", ref verificador, Parametros, Valores);

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ConsultarNotaCredito(ref NotaCredito notaCredito, string Conexion)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { 
                                          "@Id_Emp" 
                                          ,"@Id_Cd"
                                          ,"@Id_Ncr"
                                          ,"@Id_NcrSerie"
                                      };
                object[] Valores = { 
                                       notaCredito.Id_Emp
                                       ,notaCredito.Id_Cd
                                       ,notaCredito.Id_Ncr
                                       , notaCredito.Id_NcrSerie
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapNotaCredito_Consultar", ref dr, Parametros, Valores);
                NotaCargo fac = new NotaCargo();
                if (dr.HasRows)
                {
                    dr.Read();
                    notaCredito.Id_Emp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Emp")));
                    notaCredito.Id_Cd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cd")));
                    notaCredito.Id_Ncr = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Ncr")));

                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Cfe")))) notaCredito.Id_Cfe = null; else notaCredito.Id_Cfe = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cfe")));
                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_NcrSerie")))) notaCredito.Id_NcrSerie = string.Empty; else notaCredito.Id_NcrSerie = dr.GetValue(dr.GetOrdinal("Id_NcrSerie")).ToString();

                    notaCredito.Id_Reg = dr.IsDBNull(dr.GetOrdinal("Id_Reg")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Reg")));
                    notaCredito.Id_Ter = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Ter")));
                    notaCredito.Ter_Nombre = dr.IsDBNull(dr.GetOrdinal("Ter_Nombre")) ? "" : dr.GetValue(dr.GetOrdinal("Ter_Nombre")).ToString();
                    if (dr.IsDBNull(dr.GetOrdinal("Id_Rik")))
                        notaCredito.Id_Rik = null;
                    else
                        notaCredito.Id_Rik = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Rik")));
                    notaCredito.Id_U = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_U")));
                    notaCredito.Id_Cte = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cte")));
                    notaCredito.Ncr_Tipo = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Ncr_Tipo")));
                    notaCredito.Ncr_Fecha = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Ncr_Fecha")));

                    if (dr.IsDBNull(dr.GetOrdinal("Ncr_FechaHr")))
                    notaCredito.Ncr_FechaHr = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Ncr_Fecha")));
                    else
                        notaCredito.Ncr_FechaHr = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Ncr_FechaHr")));
                    notaCredito.Id_Tm = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Tm")));
                    notaCredito.Tm_Nombre = Convert.ToString(dr.GetValue(dr.GetOrdinal("Tm_Nombre")));
                    if (dr.IsDBNull(dr.GetOrdinal("Ncr_Movimiento")))
                        notaCredito.Ncr_Movimiento = null;
                    else
                        notaCredito.Ncr_Movimiento = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Ncr_Movimiento")));
                    if (dr.IsDBNull(dr.GetOrdinal("Ncr_Referencia")))
                        notaCredito.Ncr_Referencia = null;
                    else
                        notaCredito.Ncr_Referencia = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Ncr_Referencia")));
                    if (dr.IsDBNull(dr.GetOrdinal("Ncr_Saldo")))
                        notaCredito.Ncr_Saldo = null;
                    else
                        notaCredito.Ncr_Saldo = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Ncr_Saldo")));

                    if (dr.IsDBNull(dr.GetOrdinal("Ncr_EmpleadoNumNomina")))
                        notaCredito.Ncr_EmpleadoNumNomina = null;
                    else
                        notaCredito.Ncr_EmpleadoNumNomina = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Ncr_EmpleadoNumNomina")));

                    if (dr.IsDBNull(dr.GetOrdinal("Ncr_EmpleadoNombre")))
                        notaCredito.Ncr_EmpleadoNombre = null;
                    else
                        notaCredito.Ncr_EmpleadoNombre = dr.GetValue(dr.GetOrdinal("Ncr_EmpleadoNombre")).ToString();
                    notaCredito.Ncr_CtaContable = dr.IsDBNull(dr.GetOrdinal("Ncr_CtaContable")) ? string.Empty : dr.GetValue(dr.GetOrdinal("Ncr_CtaContable")).ToString();

                    notaCredito.Ncr_Desgloce = dr.IsDBNull(dr.GetOrdinal("Ncr_Desgloce")) ? false : Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Ncr_Desgloce")));
                    notaCredito.Ncr_DesglocePartidas = dr.IsDBNull(dr.GetOrdinal("Ncr_DesglocePartidas")) ? false : Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Ncr_DesglocePartidas")));
                    notaCredito.Ncr_Notas = dr.IsDBNull(dr.GetOrdinal("Ncr_Notas")) ? string.Empty : dr.GetValue(dr.GetOrdinal("Ncr_Notas")).ToString();
                    notaCredito.Ncr_CteDIVA = dr.IsDBNull(dr.GetOrdinal("Ncr_CteDIVA")) ? false : Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Ncr_CteDIVA")));
                    notaCredito.Ncr_Subtotal = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Ncr_Subtotal")));
                    notaCredito.Ncr_Iva = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Ncr_Iva")));
                    notaCredito.Ncr_Total = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Ncr_Total")));
                    notaCredito.Ncr_Pagado = dr.IsDBNull(dr.GetOrdinal("Ncr_Pagado")) ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Ncr_Pagado")));
                    if (dr.IsDBNull(dr.GetOrdinal("Ncr_FecPagado")))
                        notaCredito.Ncr_FecPagado = null;
                    else
                        notaCredito.Ncr_FecPagado = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Ncr_FecPagado")));
                    notaCredito.Ncr_Estatus = dr.IsDBNull(dr.GetOrdinal("Ncr_Estatus")) ? string.Empty : dr.GetValue(dr.GetOrdinal("Ncr_Estatus")).ToString();
                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Ncr_Sello")))) notaCredito.Ncr_Sello = string.Empty; else notaCredito.Ncr_Sello = dr.GetValue(dr.GetOrdinal("Ncr_Sello")).ToString();
                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Ncr_XML"))))
                    {
                        notaCredito.Ncr_Xml = null;
                    }
                    else
                    {
                        notaCredito.Ncr_Xml = (object)dr.GetValue(dr.GetOrdinal("Ncr_XML"));
                    }
                }

                dr.Close();
                notaCredito.ListaNotaCredito = new List<NotaCreditoDet>();
                sqlcmd = CapaDatos.GenerarSqlCommand("spCapNotaCreditoDetalle_Consultar", ref dr, Parametros, Valores);
                while (dr.Read())
                {
                    NotaCreditoDet notaCreditoDet = new NotaCreditoDet();
                    notaCreditoDet.Id_Emp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Emp")));
                    notaCreditoDet.Id_Cd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cd")));
                    notaCreditoDet.Id_Ncr = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Ncr")));
                    notaCreditoDet.Id_NcrDet = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_NcrDet")));
                    notaCreditoDet.Id_Ter = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Ter")));
                    notaCreditoDet.Ter_Nombre = dr.GetValue(dr.GetOrdinal("Ter_Nombre")).ToString();
                    notaCreditoDet.Id_Rik = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Rik")));
                    notaCreditoDet.Rik_Nombre = dr.GetValue(dr.GetOrdinal("Rik_Nombre")).ToString();
                    notaCreditoDet.Id_Prd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Prd")));
                    notaCreditoDet.Prd_Nombre = dr.GetValue(dr.GetOrdinal("Prd_Descripcion")).ToString();
                    notaCreditoDet.Ncr_Importe = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Ncr_Importe")));
                    notaCreditoDet.Prd_UniNs = dr.GetValue(dr.GetOrdinal("Prd_UniNs")).ToString();
                    notaCreditoDet.Prd_Presentacion = dr.GetValue(dr.GetOrdinal("Prd_Presentacion")).ToString();

                    notaCreditoDet.Ncr_ClaveProdServ = dr.GetValue(dr.GetOrdinal("Ncr_ClaveProdServ")).ToString();
                    notaCreditoDet.Ncr_ClaveUnidad = dr.GetValue(dr.GetOrdinal("Ncr_ClaveUnidad")).ToString();


                    notaCredito.ListaNotaCredito.Add(notaCreditoDet);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ConsultaNotaCredito_Buscar(NotaCredito notaCredito, ref List<NotaCredito> listaNotaCredito, string Conexion
            , string Nombre, int? Id_Cte_inicio, int? Id_Cte_fin, DateTime? Ncr_Fecha_inicio, DateTime? Ncr_Fecha_fin, string Ncr_Estatus
            , int? Id_Ncr_inicio, int? Id_Ncr_fin, int? Id_U)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { 
                                          "@Id_Emp" 
                                          ,"@Id_Cd"
                                          ,"@Nombre"
                                          ,"@Id_Cte_inicio"
                                          ,"@Id_Cte_fin"
                                          ,"@Ncr_Fecha_inicio"
                                          ,"@Ncr_Fecha_fin"
                                          ,"@Ncr_Estatus"
                                          ,"@Id_Ncr_inicio"
                                          ,"@Id_Ncr_fin"
                                          ,"@Id_U"
                                      };
                object[] Valores = { 
                                       notaCredito.Id_Emp
                                       ,notaCredito.Id_Cd
                                       ,Nombre
                                       ,Id_Cte_inicio
                                       ,Id_Cte_fin
                                       ,Ncr_Fecha_inicio
                                       ,Ncr_Fecha_fin
                                       ,Ncr_Estatus == string.Empty ? null : Ncr_Estatus
                                       ,Id_Ncr_inicio
                                       ,Id_Ncr_fin
                                       ,Id_U
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapNotaCredito_Buscar", ref dr, Parametros, Valores);
                listaNotaCredito = new List<NotaCredito>();
                while (dr.Read())
                {
                    notaCredito = new NotaCredito();
                    notaCredito.Id_Emp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Emp")));
                    notaCredito.Id_Cd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cd")));
                    notaCredito.Id_Ncr = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Ncr")));
                    notaCredito.Id_Cte = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cte")));
                    notaCredito.Cte_NomComercial = dr.IsDBNull(dr.GetOrdinal("Cte_NomComercial")) ? string.Empty : dr.GetValue(dr.GetOrdinal("Cte_NomComercial")).ToString();
                    notaCredito.Ncr_Tipo = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Ncr_Tipo")));
                    notaCredito.Ncr_TipoStr = dr.GetValue(dr.GetOrdinal("Ncr_TipoStr")).ToString();
                    notaCredito.Ncr_Fecha = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Ncr_Fecha")));
                    notaCredito.Id_Tm = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Tm")));

                    notaCredito.Ncr_Subtotal = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Ncr_Subtotal")));
                    notaCredito.Ncr_Iva = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Ncr_ImporteIVA")));
                    notaCredito.Ncr_Total = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Ncr_Total")));
                    notaCredito.Ncr_Pagado = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Ncr_Pagado")));
                    notaCredito.Ncr_Saldo = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Ncr_Saldo")));

                    notaCredito.Ncr_Estatus = dr.IsDBNull(dr.GetOrdinal("Ncr_Estatus")) ? string.Empty : dr.GetValue(dr.GetOrdinal("Ncr_Estatus")).ToString();
                    notaCredito.Ncr_EstatusStr = dr.IsDBNull(dr.GetOrdinal("Ncr_EstatusStr")) ? string.Empty : dr.GetValue(dr.GetOrdinal("Ncr_EstatusStr")).ToString();
                    notaCredito.PDF = dr.IsDBNull(dr.GetOrdinal("PDF")) ? false : Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("PDF")).ToString());
                    notaCredito.NcrXML = dr.IsDBNull(dr.GetOrdinal("NcrXML")) ? false : Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("NcrXML")).ToString());
                    notaCredito.Id_NcrSerie = dr["Id_NcrSerie"].ToString();
                    notaCredito.Ncr_FolioFiscal = dr["Ncr_FolioFiscal"].ToString();

                    listaNotaCredito.Add(notaCredito);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ConsultarMovsNotaCredito(Movimientos mov, ref List<Movimientos> listaMovimientos, string Conexion)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { 
                                          "@Id_Emp"
                                      };
                object[] Valores = { 
                                       mov.Id_Emp
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatTipoMovimiento_MovsNotaCredito", ref dr, Parametros, Valores);
                listaMovimientos = new List<Movimientos>();
                while (dr.Read())
                {
                    mov = new Movimientos();
                    mov.Id = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Tm")));
                    mov.Nombre = dr.GetValue(dr.GetOrdinal("Tm_Nombre")).ToString();
                    mov.AfeVta = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Tm_AfcVta")));
                    listaMovimientos.Add(mov);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ConsultarEmpleados(int Id_Emp, int Id_Cd, ref List<Usuario> listaUsuarios, string Conexion)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { 
                                          "@Id1"
                                          ,"@Id2"
                                      };
                object[] Valores = { 
                                       Id_Emp
                                       ,Id_Cd
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatUsuario_NotaCredito_Combo", ref dr, Parametros, Valores);
                listaUsuarios = new List<Usuario>();
                while (dr.Read())
                {
                    Usuario usuario = new Usuario();
                    usuario.Id_U = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_U")));
                    usuario.U_Nombre = dr.GetValue(dr.GetOrdinal("U_Nombre")).ToString();
                    usuario.U_NumNomina = dr.GetValue(dr.GetOrdinal("U_NumNomina")).ToString();
                    listaUsuarios.Add(usuario);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ConsultarNotaCreditoSAT(ref NotaCredito notaCredito, string Conexion, ref object resultado)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {
                string[] Parametros = { 
                                        "@Id_Emp" 
                                        ,"@Id_Cd"
	                                    ,"@Id_Ncr"
                                        , "@Id_NcrSerie"
                                      };
                object[] Valores = { 
                                        notaCredito.Id_Emp
                                        ,notaCredito.Id_Cd
                                        ,notaCredito.Id_Ncr
                                        , notaCredito.Id_NcrSerie
                                   };
                // ------------------------------------
                // Consultar PDF de la Nota Crédito
                // ------------------------------------
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapNotaCredito_ConsultaSAT", ref resultado, Parametros, Valores);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void InsertarNotaCredito(ref NotaCredito notaCredito, string Conexion, ref int verificador, List<AdendaDet> ListCab, DataTable listDet)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {
                CapaDatos.StartTrans();
                //INSERTA CABECERAS
                #region Cabecera nota Credito
                string[] Parametros = { 
                                        "@Id_Emp"
                                        ,"@Id_Cd"
                                        ,"@Id_Ncr"
                                        ,"@Id_Cfe"
                                        ,"@Id_NcrSerie"
                                        ,"@Id_Reg"
                                        ,"@Id_Tm"
                                        ,"@Id_Cte"
                                        ,"@Id_Ter"
                                        ,"@Id_Rik"
                                        ,"@Id_U"
                                        ,"@Ncr_Tipo"
                                        ,"@Ncr_Fecha"
                                        ,"@Ncr_EmpleadoNumNomina"
                                        ,"@Ncr_EmpleadoNombre"
		                                ,"@Ncr_CtaContable"
                                        ,"@Ncr_Movimiento"
                                        ,"@Ncr_Referencia"
                                        ,"@Ncr_Saldo"
                                        ,"@Ncr_Desgloce"
                                        ,"@Ncr_DesglocePartidas"
                                        ,"@Ncr_Notas"
                                        ,"@Ncr_CteDIVA"
                                        ,"@Ncr_Subtotal"
                                        ,"@Ncr_Iva"
                                        ,"@Ncr_Total"
                                        ,"@Ncr_Pagado"
                                        ,"@Ncr_FecPagado"
                                        ,"@Ncr_Estatus",
                                        "@Ncr_ReferenciaSerie"
                                      };
                object[] Valores = { 
                                        notaCredito.Id_Emp
                                        ,notaCredito.Id_Cd
                                        ,notaCredito.Id_Ncr
                                        ,notaCredito.Id_Cfe
                                        ,notaCredito.Id_NcrSerie
                                        ,null //notaCredito.Id_Reg
                                        ,notaCredito.Id_Tm
                                        ,notaCredito.Id_Cte
                                        ,notaCredito.Id_Ter
                                        ,notaCredito.Id_Rik
                                        ,notaCredito.Id_U
                                        ,notaCredito.Ncr_Tipo
                                        ,notaCredito.Ncr_Fecha
                                        ,notaCredito.Ncr_EmpleadoNumNomina
                                        ,notaCredito.Ncr_EmpleadoNombre
                                        ,notaCredito.Ncr_CtaContable == string.Empty ? null : notaCredito.Ncr_CtaContable
                                        ,notaCredito.Ncr_Movimiento
                                        ,notaCredito.Ncr_Referencia
                                        ,notaCredito.Ncr_Saldo
                                        ,notaCredito.Ncr_Desgloce
                                        ,notaCredito.Ncr_DesglocePartidas
                                        ,notaCredito.Ncr_Notas
                                        ,notaCredito.Ncr_CteDIVA
                                        ,notaCredito.Ncr_Subtotal
                                        ,notaCredito.Ncr_Iva
                                        ,notaCredito.Ncr_Total
                                        ,notaCredito.Ncr_Pagado
                                        ,notaCredito.Ncr_FecPagado
                                        ,notaCredito.Ncr_Estatus,
                                        notaCredito.Ncr_ReferenciaSerie
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapNotaCredito_Insertar", ref verificador, Parametros, Valores);
                notaCredito.Id_Ncr = verificador; //clave de nota de cargo               
                #endregion                
                #region cabecera adenda
                Parametros = new string[] {
                                            "@Id_Emp",			
                                            "@Id_Cd",				
                                            "@Id_Nca",
                                            "@Id_Cte",
                                            "@Id_AdeDet",
                                            "@Ade_Tipo",
                                            "@Ade_Valor",
                                            "@Contador" 
                                      };

                int cont = 0;
                foreach (AdendaDet adendaF in ListCab)
                {
                    Valores = new object[] { 
                        notaCredito.Id_Emp,
                        notaCredito.Id_Cd,
                        notaCredito.Id_Ncr,
                        notaCredito.Id_Cte,
                        adendaF.Id_AdeDet,
                        adendaF.Tipo,
                        adendaF.Valor,
                        cont
                    };
                    cont++;
                    sqlcmd = CapaDatos.GenerarSqlCommand("spCapNCreditoAdenda_Insertar", ref verificador, Parametros, Valores);
                }
                #endregion 
                //INSERTAR DETALLES
                #region adenda detalle
                Parametros = new string[] {
                                            "@Id_Emp",			
                                            "@Id_Cd",				
                                            "@Id_Nca",
                                            "@Id_Cte",
                                            "@Id_AdeDet",
                                            "@Ade_Tipo",
                                            "@Ade_Valor",
                                            "@Contador",
                                            "@Id_Prd",
                                            "@Id_Ter"
                                      };
                foreach (DataRow facturaDet in listDet.Rows)
                {
                    for (int j = 11; j < listDet.Columns.Count; j++)
                    {
                        Valores = new object[] { 
                            notaCredito.Id_Emp,
                            notaCredito.Id_Cd,
                            notaCredito.Id_Ncr,
                            notaCredito.Id_Cte,
                            listDet.Columns[j].ColumnName,
                            6,
                            facturaDet[j],
                            j,
                            facturaDet["Id_Prd"],
                            facturaDet["Id_Ter"]
                        };
                        sqlcmd = CapaDatos.GenerarSqlCommand("spCapNCreditoAdenda_Insertar", ref verificador, Parametros, Valores);
                    }
                }
                #endregion              
                #region nota credito detalle
                string[] ParametrosDet = { 
                                        "@Id_Emp"				
                                        ,"@Id_Cd"				
                                        ,"@Id_Ncr"			
                                        ,"@Id_NcrDet"			
                                        ,"@Id_Ter"			
                                        ,"@Id_Rik"			
                                        ,"@Id_Prd"			
                                        ,"@Ncr_Importe"	
                                      };
                int i = 1;
                foreach (DataRow notaCreditoDet in listDet.Rows)
                {
                    notaCreditoDet["Id_NcrDet"] = i;
                    object[] ValoresDet = { 
                                        notaCreditoDet["Id_Emp"]
                                        ,notaCreditoDet["Id_Cd"]
                                        ,notaCredito.Id_Ncr //Id de nota de cargo de la tabla de encabezado
                                        ,notaCreditoDet["Id_NcrDet"]
                                        ,notaCreditoDet["Id_Ter"]
                                        ,notaCreditoDet["Id_Rik"]
                                        ,notaCreditoDet["Id_Prd"]
                                        ,notaCreditoDet["Ncr_Importe"]
                                   };

                    sqlcmd = CapaDatos.GenerarSqlCommand("spCapNotaCreditoDetalle_Insertar", ref verificador, ParametrosDet, ValoresDet);
                    i += 1;
                }
                #endregion
                CapaDatos.CommitTrans();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                CapaDatos.RollBackTrans();
                throw ex;
            }
        }
        public void ModificarNotaCredito(ref NotaCredito notaCredito, string Conexion, ref int verificador, List<AdendaDet> ListCab, DataTable listDet)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {
                CapaDatos.StartTrans();
                //MODIFICAR CABECERAS
                #region nota credito Cabecera
                string[] Parametros = { 
                                        "@Id_Emp"
                                        ,"@Id_Cd"
                                        ,"@Id_Ncr"
                                        ,"@Id_Cfe"
                                        ,"@Id_NcrSerie"
                                        ,"@Id_Reg"
                                        ,"@Id_Tm"
                                        ,"@Id_Cte"
                                        ,"@Id_Ter"
                                        ,"@Id_Rik"
                                        ,"@Id_U"
                                        ,"@Ncr_Tipo"
                                        ,"@Ncr_Fecha"
                                        ,"@Ncr_EmpleadoNumNomina"
                                        ,"@Ncr_EmpleadoNombre"
		                                ,"@Ncr_CtaContable"
                                        ,"@Ncr_Movimiento"
                                        ,"@Ncr_Referencia"
                                        ,"@Ncr_Saldo"
                                        ,"@Ncr_Desgloce"
                                        ,"@Ncr_DesglocePartidas"
                                        ,"@Ncr_Notas"
                                        ,"@Ncr_CteDIVA"
                                        ,"@Ncr_Subtotal"
                                        ,"@Ncr_Iva"
                                        ,"@Ncr_Total"
                                        ,"@Ncr_Pagado"
                                        ,"@Ncr_FecPagado"
                                        ,"@Ncr_Estatus"
                                        ,"@Ncr_ReferenciaSerie"
                                      };
                object[] Valores = { 
                                        notaCredito.Id_Emp
                                        ,notaCredito.Id_Cd
                                        ,notaCredito.Id_Ncr
                                        ,notaCredito.Id_Cfe
                                        ,notaCredito.Id_NcrSerie
                                        ,null //notaCredito.Id_Reg
                                        ,notaCredito.Id_Tm
                                        ,notaCredito.Id_Cte
                                        ,notaCredito.Id_Ter
                                        ,notaCredito.Id_Rik
                                        ,notaCredito.Id_U
                                        ,notaCredito.Ncr_Tipo
                                        ,notaCredito.Ncr_Fecha
                                        ,notaCredito.Ncr_EmpleadoNumNomina
                                        ,notaCredito.Ncr_EmpleadoNombre
                                        ,notaCredito.Ncr_CtaContable == string.Empty ? null : notaCredito.Ncr_CtaContable
                                        ,notaCredito.Ncr_Movimiento
                                        ,notaCredito.Ncr_Referencia
                                        ,notaCredito.Ncr_Saldo
                                        ,notaCredito.Ncr_Desgloce
                                        ,notaCredito.Ncr_DesglocePartidas
                                        ,notaCredito.Ncr_Notas
                                        ,notaCredito.Ncr_CteDIVA
                                        ,notaCredito.Ncr_Subtotal
                                        ,notaCredito.Ncr_Iva
                                        ,notaCredito.Ncr_Total
                                        ,notaCredito.Ncr_Pagado
                                        ,notaCredito.Ncr_FecPagado
                                        ,notaCredito.Ncr_Estatus
                                        ,notaCredito.Ncr_ReferenciaSerie
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapNotaCredito_Modificar", ref verificador, Parametros, Valores);
                #endregion
                #region adenda cabecera                
                Parametros = new string[] {
                                            "@Id_Emp",			
                                            "@Id_Cd",				
                                            "@Id_Nca",
                                            "@Id_Cte",
                                            "@Id_AdeDet",
                                            "@Ade_Tipo",
                                            "@Ade_Valor",
                                            "@Contador" 
                                      };

                int cont = 0;
                foreach (AdendaDet adendaF in ListCab)
                {
                    Valores = new object[] { 
                        notaCredito.Id_Emp,
                        notaCredito.Id_Cd,
                        notaCredito.Id_Ncr,
                        notaCredito.Id_Cte,
                        adendaF.Id_AdeDet,
                        adendaF.Tipo,
                        adendaF.Valor,
                        cont
                    };
                    cont++;
                    sqlcmd = CapaDatos.GenerarSqlCommand("spCapNCreditoAdenda_Insertar", ref verificador, Parametros, Valores);
                }
                #endregion
                //MODIFICAR DETALLES
                #region Adenda detalle
                Parametros = new string[] {
                                            "@Id_Emp",			
                                            "@Id_Cd",				
                                            "@Id_Nca",
                                            "@Id_Cte",
                                            "@Id_AdeDet",
                                            "@Ade_Tipo",
                                            "@Ade_Valor",
                                            "@Contador",
                                            "@Id_Prd",
                                            "@Id_Ter"
                                      };
                foreach (DataRow facturaDet in listDet.Rows)
                {
                    for (int j = 11; j < listDet.Columns.Count; j++)
                    {
                        Valores = new object[] { 
                            notaCredito.Id_Emp,
                            notaCredito.Id_Cd,
                            notaCredito.Id_Ncr,
                            notaCredito.Id_Cte,
                            listDet.Columns[j].ColumnName,
                            6,
                            facturaDet[j],
                            j,
                            facturaDet["Id_Prd"],
                            facturaDet["Id_Ter"]
                        };
                        sqlcmd = CapaDatos.GenerarSqlCommand("spCapNCreditoAdenda_Insertar", ref verificador, Parametros, Valores);
                    }
                }
                #endregion
                #region detalle de nota credito
                string[] ParametrosDet = { 
                                        "@Id_Emp"				
                                        ,"@Id_Cd"				
                                        ,"@Id_Ncr"			
                                        ,"@Id_NcrDet"			
                                        ,"@Id_Ter"			
                                        ,"@Id_Rik"			
                                        ,"@Id_Prd"			
                                        ,"@Ncr_Importe"	
                                      };
                int i = 1;
                foreach (DataRow notaCreditoDet in listDet.Rows)
                {
                    notaCreditoDet["Id_NcrDet"] = i;
                    object[] ValoresDet = { 
                                        notaCreditoDet["Id_Emp"]
                                        ,notaCreditoDet["Id_Cd"]
                                        ,notaCredito.Id_Ncr //Id de nota de cargo de la tabla de encabezado
                                        ,notaCreditoDet["Id_NcrDet"]
                                        ,notaCreditoDet["Id_Ter"]
                                        ,notaCreditoDet["Id_Rik"]
                                        ,notaCreditoDet["Id_Prd"]
                                        ,notaCreditoDet["Ncr_Importe"]
                                   };

                    sqlcmd = CapaDatos.GenerarSqlCommand("spCapNotaCreditoDetalle_Insertar", ref verificador, ParametrosDet, ValoresDet);
                    i += 1;
                }
                #endregion
                CapaDatos.CommitTrans();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                CapaDatos.RollBackTrans();
                throw ex;
            }
        }
        public void ModificarNotaCreditoSAT(NotaCredito notaCredito, string Conexion, ref int verificador)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {
                string[] Parametros = { 
                                        "@Id_Emp" 
                                        ,"@Id_Cd"
	                                    ,"@Id_Ncr"
                                        ,"@Ncr_Estatus"
                                        ,"@Ncr_Sello"
                                        ,"@Ncr_Xml"
                                        ,"@Ncr_Pdf"
                                        ,"@Ncr_FolioFiscal"
                                      };
                object[] Valores = { 
                                        notaCredito.Id_Emp
                                        ,notaCredito.Id_Cd
                                        ,notaCredito.Id_Ncr
                                        ,notaCredito.Ncr_Estatus
                                        ,notaCredito.Ncr_Sello
                                        ,notaCredito.Ncr_Xml
                                        ,notaCredito.Ncr_Pdf
                                        ,notaCredito.Ncr_FolioFiscal
                                   };

                // -----------------------------------------------------------------
                // Actualizar encabezado de la nota de credito
                // -----------------------------------------------------------------
                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd = CapaDatos.GenerarSqlCommand("spCapNotaCredito_ModificarSAT", ref verificador, Parametros, Valores);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void EliminarNotaCredito(NotaCredito notaCredito, string Conexion, ref int verificador)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {
                CapaDatos.StartTrans();
                string[] Parametros = { 
	                                    "@Id_Emp"
                                        ,"@Id_Cd"
                                        ,"@Id_Ncr"
                                        , "@Id_NcrSerie"
                                      };
                object[] Valores = { 
                                       notaCredito.Id_Emp
                                       ,notaCredito.Id_Cd
                                       ,notaCredito.Id_Ncr
                                       , notaCredito.Id_NcrSerie
                                   };

                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd = CapaDatos.GenerarSqlCommand("spCapNotaCredito_Eliminar", ref verificador, Parametros, Valores);
                //ordenCompra.Id_Ord = verificador; //identity de orden de compra 

                CapaDatos.CommitTrans();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                CapaDatos.RollBackTrans();
                throw ex;
            }
        }
        public void ConsultaAdendaNota(Sesion sesion, int Id_Emp, int Id_Cd, int Id_Cte, ref List<NotaCredito> listNotaCredito)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(sesion.Emp_Cnx);
                string[] Parametros = { 
                                         "@Id_Emp",        
                                         "@Id_Cd",
                                         "@Id_Cte",
                                         "@Tipo"
                                      };
                object[] Valores = {                                       
                                       Id_Emp,
                                       Id_Cd, 
                                       Id_Cte,
                                       3// tipo 3 para notas de credito
                                   };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapNotaCargo_ConsultaAdenda", ref dr, Parametros, Valores);

                NotaCredito notaCredito;
                while (dr.Read())
                {
                    notaCredito = new NotaCredito();
                    notaCredito.Campo = (string)dr.GetValue(dr.GetOrdinal("Ade_Campo"));
                    notaCredito.Ade_Longitud = (string)dr.GetValue(dr.GetOrdinal("Ade_Longitud"));
                    listNotaCredito.Add(notaCredito);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void AgregarAdenda(NotaCredito notaCredito, Sesion sesion, ref int verificador)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(sesion.Emp_Cnx);
            try
            {
                CapaDatos.StartTrans();
                string[] Parametros = { 
	                                    "@Id_Emp"
                                        ,"@Id_Cd"
                                        ,"@Id_Cte"
                                        ,"@Tipo"
                                        ,"@Ade_Campo"
                                        ,"@Ade_Longitud"
                                      };
                object[] Valores = { 
                                       notaCredito.Id_Emp
                                       ,notaCredito.Id_Cd
                                       ,notaCredito.Id_Cte
                                       ,3// tipo 3 para notas de credito
                                       ,notaCredito.Campo
                                       ,notaCredito.Ade_Longitud
                                   };

                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd = CapaDatos.GenerarSqlCommand("spCapNotaCargo_InsertarAdenda", ref verificador, Parametros, Valores);

                CapaDatos.CommitTrans();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                CapaDatos.RollBackTrans();
                throw ex;
            }
        }
        public List<NotaCredito> ConsultaProductosNotaCredito(ref NotaCredito notaCredito, string Conexion)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { 
                                          "@Id_Emp" 
                                          ,"@Id_Cd"
                                          ,"@Id_Ncr"                                          
                                      };
                object[] Valores = { 
                                       notaCredito.Id_Emp
                                       ,notaCredito.Id_Cd
                                       ,notaCredito.Id_Ncr
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapNotaCredito_ProductosEspecial", ref dr, Parametros, Valores);

                List<NotaCredito> listaNotaCredito = new List<NotaCredito>();
                while (dr.Read())
                {//Id_Clp, c.Id_Prd, c.Clp_descripcion
                    notaCredito = new NotaCredito();
                    notaCredito.Id_Prd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Prd")));
                    listaNotaCredito.Add(notaCredito);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
                return listaNotaCredito;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ConsultaNotaCreditoEspecialDetalle(ref List<NotaCreditoDet> listaFacturaProductos, string Conexion, int id_Emp, int id_Cd, int id_Ncr,string Id_NcrSerie, int id_Cte)
        {
            try
            {
                NotaCreditoDet facturaDet = null;
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_Ncr", "@Id_NcrSerie" };
                object[] Valores = { id_Emp, id_Cd, id_Ncr, Id_NcrSerie };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapNCreditoEspecialDetalle_Consultar", ref dr, Parametros, Valores);


                while (dr.Read())
                {
                    facturaDet = new NotaCreditoDet();
                    facturaDet.Id_Emp = id_Emp;
                    facturaDet.Id_Cd = id_Cd;
                    facturaDet.Id_Ncr = 0;
                    facturaDet.Id_NcrDet = 0;
                    facturaDet.Id_CteExt = id_Cte;
                    facturaDet.Id_Prd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Prd")));
                    facturaDet.Ncr_Importe = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Ncr_Importe"))) ? 0 : Convert.ToSingle(dr.GetValue(dr.GetOrdinal("Ncr_Importe")));

                    //datos del producto de la orden de compra
                    facturaDet.Producto = new Producto();
                    facturaDet.Producto.Id_Prd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Prd")));
                    facturaDet.Producto.Id_Emp = id_Emp;
                    facturaDet.Producto.Id_Cd = id_Cd;
                    facturaDet.Producto.Id_PrdEsp = dr.IsDBNull(dr.GetOrdinal("Id_PrdEsp")) ? string.Empty : dr.GetValue(dr.GetOrdinal("Id_PrdEsp")).ToString();
                    facturaDet.Producto.Prd_Descripcion = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Descripcion"))) ? string.Empty : dr.GetValue(dr.GetOrdinal("Prd_Descripcion")).ToString();
                    facturaDet.Producto.Prd_DescripcionEspecial = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("NcrEsp_Descripcion"))) ? string.Empty : dr.GetValue(dr.GetOrdinal("NcrEsp_Descripcion")).ToString();                    
                    facturaDet.Producto.Prd_Presentacion = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("NcrEsp_Presentacion"))) ? string.Empty : dr.GetValue(dr.GetOrdinal("NcrEsp_Presentacion")).ToString();
                    facturaDet.Producto.Prd_UniNe = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("NcrEsp_Unidades"))) ? string.Empty : dr.GetValue(dr.GetOrdinal("NcrEsp_Unidades")).ToString();
                    facturaDet.Producto.Prd_UniNs = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("NcrEsp_Unidades"))) ? string.Empty : dr.GetValue(dr.GetOrdinal("NcrEsp_Unidades")).ToString();
                    facturaDet.Clp_Release = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("NcrEsp_Release"))) ? string.Empty : dr.GetValue(dr.GetOrdinal("NcrEsp_Release")).ToString();

                    listaFacturaProductos.Add(facturaDet);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }                
        public void ConsultarAdenda(int Id_Emp, int Id_Cd_Ver, int Id_Ncr,string Id_NcrSerie, string Tipo1, string Tipo2, ref List<AdendaDet> listCab, ref List<AdendaDet> listDet, string Conexion)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { 
                                          "@Id_Emp" ,
                                          "@Id_Cd",
                                          "@Id_Ncr",
                                          "@Id_NcrSerie",
                                          "@Ade_Tipo1",
                                          "@Ade_Tipo2"
                                      };
                object[] Valores = { 
                                       Id_Emp,
                                       Id_Cd_Ver,
                                       Id_Ncr,
                                       Id_NcrSerie,
                                       Tipo1,
                                       Tipo2
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapNCreditoAdenda_Consultar", ref dr, Parametros, Valores);

                AdendaDet adendaDet;
                while (dr.Read())
                {
                    adendaDet = new AdendaDet();
                    adendaDet.Campo = Convert.ToString(dr.GetValue(dr.GetOrdinal("Ade_Campo")));
                    adendaDet.Nodo = Convert.ToString(dr.GetValue(dr.GetOrdinal("Ade_Nodo")));
                    adendaDet.Longitud = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Ade_Longitud")));
                    adendaDet.Valor = Convert.ToString(dr.GetValue(dr.GetOrdinal("Ade_Valor")));
                    adendaDet.Id_AdeDet = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_AdeDet")));
                    adendaDet.Id_Prd = (int?)dr.GetValue(dr.GetOrdinal("Id_Prd"));
                    adendaDet.Id_Ter = (int?)dr.GetValue(dr.GetOrdinal("Id_Ter"));
                    adendaDet.Tipo = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Ade_Tipo")));
                    adendaDet.Requerido = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Ade_Requerido")));
                    if (adendaDet.Tipo % 2 != 0)
                    {
                        listCab.Add(adendaDet);
                    }
                    else
                    {
                        listDet.Add(adendaDet);
                    }
                }
                dr.Close();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ArchivoPdf_Xml(ref NotaCredito notaCredito, string Conexion)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_Ncr" , "@Id_NcrSerie"};
                object[] Valores = { notaCredito.Id_Emp, notaCredito.Id_Cd, notaCredito.Id_Ncr , notaCredito.Id_NcrSerie};

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapNotaCredito_PDF_XML", ref dr, Parametros, Valores);
                while (dr.Read())
                {
                    notaCredito = new NotaCredito();
                    notaCredito.Ncr_Xml = Convert.ToString(dr.GetValue(dr.GetOrdinal("Ncr_Xml")));
                }
                dr.Close();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ConsultarIdNotaCredito(int Ncr_Referencia,int Id_Cd, int Id_Emp,  ref int Id_Nc,ref string Estatus, string Conexion)
        {
            try
            {
                CD_Datos cd_datos = new CD_Datos(Conexion);
                SqlDataReader dr = null;

                String[] Parametros = { 
                                          "@Id_Emp",
                                          "@Id_Cd", 
                                          "@Ncr_Referencia"
                                      };

                Object[] Valores = {
                                       Id_Emp, 
                                       Id_Cd, 
                                       Ncr_Referencia 
                                   };

                SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("SpCapNotaCredito_ObtenerId", ref dr, Parametros, Valores);

                if (dr.Read())
                {
                    Id_Nc = Convert.ToInt32(dr["Id_Ncr"]);
                    Estatus = dr["Ncr_Estatus"].ToString().Trim();
                }

                dr.Close();

                cd_datos.LimpiarSqlcommand(ref sqlcmd);



            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
        public void ValidarEstatusFactura(int Id_Emp, int Id_Cd, int Id_Ncr,string Id_NcrSerie, string Conexion, ref int Verificador)
        {
            try
            {
                CD_Datos cd_datos = new CD_Datos(Conexion);

                String[] Parametros = { "@Id_Emp",
                                        "@Id_Cd",
                                        "@Id_Ncr",
                                        "@Id_NcrSerie"};

                Object[] Valores = {    Id_Emp, 
                                        Id_Cd, 
                                        Id_Ncr,
                                   Id_NcrSerie};

                SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("SpCapNotaCredito_ValidarFuente", ref Verificador, Parametros, Valores);

                cd_datos.LimpiarSqlcommand(ref sqlcmd);


            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void ValidaMontosImpresion(NotaCredito nc, int Id_Cd, int Id_Emp, int iTipoDocumento, string conexion, ref bool bVerificador)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(conexion);
                string[] parametros = { 
                                          "@Id_Doc",
                                          "@Id_Cd",
                                          "@Id_Emp",
                                          "@iTipoDocumento"
                                      };
                object[] Valores = {
                                      nc.Id_Ncr,
                                      Id_Cd,
                                      Id_Emp,
                                      iTipoDocumento
                                   };

                int verificador = 0;
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapDocumentoValido_Impresion", ref verificador, parametros, Valores);
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

                if (verificador == 0)
                    bVerificador = false;
                else
                    bVerificador = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }     
    }
}
