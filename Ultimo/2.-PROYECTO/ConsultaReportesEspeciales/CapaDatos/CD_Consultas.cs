using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.SqlClient;
using System.Data;

using CapaEntidad;
using System.Xml.Serialization;
using System.Xml;
using System.IO;

namespace CapaDatos
{
    public class CD_Consultas
    {

        public void EstadisticaVentasEnUnidades(string Conexion,int Id_Emp,int Id_Cd,int Anio,string Territorio,string Cliente,string Producto,int Tipo,int NivelCliente,int NivelProducto,int Reporte,string Id_U,ref string stringXML)
        {

            DataSet ds = new DataSet();

            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = { 
	                                    "@Id_Emp",
                                        "@Id_Cd",
	                                    "@Anio",
                                        "@Territorio",
                                        "@Cliente",
                                        "@Producto",
                                        "@Tipo",
                                        "@NivelCliente",
                                        "@NivelProducto",                                        
                                        "@Reporte",
                                        "@Id_U"
                                      };
                object[] Valores = { 
                                            Id_Emp,
                                            Id_Cd,
	                                        Anio,
                                            Territorio.ToString()==string.Empty ? (object)null : Territorio,
                                            Cliente.ToString()==string.Empty ? (object)null : Cliente,
                                            Producto.ToString()==string.Empty ? (object)null : Producto,
                                            Tipo,
                                            NivelCliente,
                                            NivelProducto,
                                            Reporte,
                                            Id_U.ToString()==string.Empty ? (object)null : Id_U
                                       };


                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spVentasAnuales", ref ds,Parametros, Valores);
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

                ds.DataSetName = "EstadisticaVentasEnUnidades";
                DataTable D = new DataTable();
                D = ds.Tables[0];
                D.TableName = "EstadisticaVentas";

                string xmlString = ConvertDatatableToXML(D);
                stringXML = xmlString;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EstadisticaVentasEnPesos(string Conexion, int Id_Emp, int Id_Cd, int Anio, string Territorio, string Cliente, string Producto, int Tipo, int NivelCliente, int NivelProducto, int Reporte, string Id_U, ref string stringXML)
        {

            DataSet ds = new DataSet();
            int verificador = 0;

            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = { 
	                                    "@Id_Emp",
                                        "@Id_Cd",
	                                    "@Anio",
                                        "@Territorio",
                                        "@Cliente",
                                        "@Producto",
                                        "@Tipo",
                                        "@NivelCliente",
                                        "@NivelProducto",                                        
                                        "@Reporte",
                                        "@Id_U"
                                      };
                    object[] Valores = { 
                                            Id_Emp,
                                            Id_Cd,
	                                        Anio,
                                            Territorio.ToString()==string.Empty ? (object)null : Territorio,
                                            Cliente.ToString()==string.Empty ? (object)null : Cliente,
                                            Producto.ToString()==string.Empty ? (object)null : Producto,
                                            Tipo,
                                            NivelCliente,
                                            NivelProducto,
                                            Reporte,
                                            Id_U.ToString()==string.Empty ? (object)null : Id_U
                                       };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spVentasAnuales", ref ds, Parametros, Valores);
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

                ds.DataSetName = "EstadisticaVentasEnPesos";
                DataTable D = new DataTable();
                D = ds.Tables[0];
                D.TableName = "EstadisticaVentas";

                string xmlString = ConvertDatatableToXML(D);
                stringXML = xmlString;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region Listo

        public void Cientes(string Conexion, ref string stringXML)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                SqlCommand sqlcmd = default(SqlCommand);

                DataSet ds = new DataSet();
                sqlcmd = CapaDatos.GenerarSqlCommand("SELECT Id_Cte, Cte_NomComercial FROM CatCliente", ref ds);

                DataTable D = new DataTable();
                ds.DataSetName = "Clientes";
                D = ds.Tables[0];
                D.TableName = "Cliente";

                string xmlString = ConvertDatatableToXML(D);
                stringXML = xmlString;

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void DetalleCientes(string Conexion, ref string stringXML)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                SqlCommand sqlcmd = default(SqlCommand);

                DataSet ds = new DataSet();
                sqlcmd = CapaDatos.GenerarSqlCommand("SELECT cc.Id_Cte, cc.Cte_NomComercial, cc.Cte_Telefono, cc.Cte_Email, cc.Cte_FacCalle, cc.Cte_FacNumero, cc.Cte_FacCp, cc.Cte_FacColonia, cc.Cte_FacMunicipio, cc.Cte_FacEstado, cc.Cte_FacRfc, cc.Cte_CondPago, cc.Cte_Calle, cc.Cte_Numero, cc.Cte_Cp, cc.Cte_Colonia, cc.Cte_Municipio, cc.Cte_Estado, cc.Cte_Telefono, cc.Cte_Rfc, cc.Cte_Activo, cc.Cte_CPLunes, cc.Cte_CPMartes, cc.Cte_CPMiercoles, cc.Cte_CPJueves, cc.Cte_CPViernes, cc.Cte_CPSabado, cc.Cte_CPDomingo, cc.Cte_RecLunes, cc.Cte_RecMartes, cc.Cte_RecMiercoles, cc.Cte_RecJueves, cc.Cte_RecViernes, cc.Cte_RecSabado, cc.Cte_RecDomingo, cfp.Fpa_Descripcion FROM CatCliente AS cc, CatClienteFpago AS ccp, CatFormaPago AS cfp WHERE cc.Id_Cte=ccp.Id_Cte AND ccp.Id_Fpa=cfp.Id_Fpa", ref ds);

                DataTable D = new DataTable();
                ds.DataSetName = "Clientes";
                D = ds.Tables[0];
                D.TableName = "DetalleCliente";

                string xmlString = ConvertDatatableToXML(D);
                stringXML = xmlString;

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void CientesBloqueados(string Conexion, ref string stringXML)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                SqlCommand sqlcmd = default(SqlCommand);

                DataSet ds = new DataSet();
                sqlcmd = CapaDatos.GenerarSqlCommand("SELECT Id_Cte, Cte_NomComercial, Cte_Facturacion FROM CatCliente WHERE Cte_Facturacion = '0'", ref ds);

                DataTable D=new DataTable();
                ds.DataSetName = "Clientes";
                D = ds.Tables[0];
                D.TableName = "ClienteBloqueado";

                string xmlString = ConvertDatatableToXML(D);
                stringXML = xmlString;

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void CientesProducto(string Conexion, ref string stringXML)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                SqlCommand sqlcmd = default(SqlCommand);

                DataSet ds = new DataSet();
                string sql = "SELECT" +
                          " cp.Id_Cte, cp.Id_Prd," +
                          " cp.Clp_Pesos," +
                          " ISNULL(c.Cte_NomComercial,'N/A') as Cte_NomComercial" +
                          " FROM CatClienteProdDet AS cp" +
                          " Left Join CatCliente AS c" +
                          " on cp.Id_Cte=c.Id_Cte";

                sqlcmd = CapaDatos.GenerarSqlCommand(sql, ref ds);

                DataTable D=new DataTable();
                ds.DataSetName = "Clientes";
                D = ds.Tables[0];
                D.TableName = "ClienteProducto";

                string xmlString = ConvertDatatableToXML(D);
                stringXML = xmlString;

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void FacturasEnRevision(string Conexion,string d_ini, string d_fin, ref string stringXML)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                SqlCommand sqlcmd = default(SqlCommand);

                DataSet ds = new DataSet();
                string sql="SELECT CONVERT (varchar, cb.Fac_Fecha,103) Fecha, cb.Id_Fac, cb.Id_Cte, cb.Cte_Nombre, CONVERT (varchar, cb.Fac_FechaRevision,103) FechaRevision"+
			    " FROM sianwebcobranza.dbo.ProSeguimientoCobranza AS cb"+
			    " WHERE cb.Fac_FechaRevision is not NULL"+
			    " AND cb.Fac_Fecha BETWEEN '"+d_ini+"' AND '"+d_fin+"'";

                sqlcmd = CapaDatos.GenerarSqlCommand(sql, ref ds);

                DataTable D = new DataTable();
                ds.DataSetName = "FacturasEnRevision";
                D = ds.Tables[0];
                D.TableName = "Factura";

                string xmlString = ConvertDatatableToXML(D);
                stringXML = xmlString;

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void CostosVigentes(string Conexion,string Prd_FechaFin, ref string stringXML)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                SqlCommand sqlcmd = default(SqlCommand);

                DataSet ds = new DataSet();
                string sql =
                    "SELECT pp.Id_Prd, cp.Prd_Descripcion, pp.Id_Pre, pp.Prd_Pesos, CONVERT (varchar, pp.Prd_FechaFin,103) Fecha" +
                    " FROM ProductoPrecio AS pp, CatProducto AS cp" +
                    " WHERE cp.Id_Prd = pp.Id_Prd" +
                    " AND pp.Prd_FechaFin > '" + Prd_FechaFin + "'";


                sqlcmd = CapaDatos.GenerarSqlCommand(sql, ref ds);

                DataTable D = new DataTable();
                ds.DataSetName = "CostosVigente";
                D = ds.Tables[0];
                D.TableName = "Costo";

                string xmlString = ConvertDatatableToXML(D);
                stringXML = xmlString;

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        //-----------EstadisticaVentasEnPesos
        public void DetallePedidos(string Conexion, string d_ini, string d_fin, ref string stringXML)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                SqlCommand sqlcmd = default(SqlCommand);

                DataSet ds = new DataSet();
                string sql = "SELECT CONVERT (varchar, p.Ped_Fecha,103) fpedido, CONVERT (varchar, p.Ped_Fecha,108) hpedido, p.Ped_Solicito, c.Id_Cte, c.Cte_NomComercial, f.Fac_PedNum, p.Ped_Total, CONVERT (varchar, f.Fac_Fecha,103) ffactura, f.Id_Fac, (f.Fac_SubTotal + Fac_ImporteIva) AS total, f.Fac_Estatus, f.Fac_Notas, p.Ped_Estatus " +
                           " FROM CapPedido AS p, CatCliente AS c, CapFactura AS f" +
                           " WHERE f.Id_Cte=c.Id_Cte" +
                           " AND p.Id_Ped=f.Fac_PedNum" +
                           " AND p.Id_Ter=f.Id_Ter" +
                           " AND p.Ped_Fecha BETWEEN '" + d_ini + "' AND '" + d_fin + "'" +
                           " ORDER BY p.Ped_Fecha";

                sqlcmd = CapaDatos.GenerarSqlCommand(sql, ref ds);

                DataTable D = new DataTable();
                ds.DataSetName = "DetallePedidos";
                D = ds.Tables[0];
                D.TableName = "DetallePedido";

                string xmlString = ConvertDatatableToXML(D);
                stringXML = xmlString;

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void FacturasMenores(string Conexion, string d_ini, string d_fin, ref string stringXML)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                SqlCommand sqlcmd = default(SqlCommand);

                DataSet ds = new DataSet();
                string sql = "SELECT CONVERT (varchar, f.Fac_Fecha,103) Fecha, f.Id_Fac, f.Id_Cte, f.Id_Ter, c.Cte_NomComercial, f.Fac_Importe, f.Fac_Estatus, f.Fac_PedNum, p.Ped_Total" +
                        " FROM CatCliente AS c, CapFactura as f, CapPedido AS p" +
                        " WHERE c.Id_Cte=f.Id_Cte" +
                        " AND f.Id_U=p.Id_U" +
                        " AND c.Id_Cte=p.Id_Cte" +
                        " AND f.Id_Ter=p.Id_Ter" +
                        " AND p.Id_Ped=f.Fac_PedNum" +
                        " AND p.Ped_Total < 500 " +
                        " AND f.Fac_Fecha BETWEEN '" + d_ini + "' AND '" + d_fin + "'" +
                        " ORDER BY f.Fac_Fecha";

                sqlcmd = CapaDatos.GenerarSqlCommand(sql, ref ds);

                DataTable D = new DataTable();
                ds.DataSetName = "FacturasMenores";
                D = ds.Tables[0];
                D.TableName = "Factura";

                string xmlString = ConvertDatatableToXML(D);
                stringXML = xmlString;

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void Facturas(string Conexion, string d_ini, string d_fin, ref string stringXML)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                SqlCommand sqlcmd = default(SqlCommand);

                DataSet ds = new DataSet();
                string sql = "SELECT CONVERT (varchar, fac.Fac_Fecha,103) Fecha, fac.Id_Fac, fac.Id_Cte, cc.Cte_NomComercial, fac.Fac_Importe, fac.Fac_SubTotal, (fac.Fac_SubTotal + fac.Fac_ImporteIva) AS total, fac.Fac_PedNum, fac.Fac_Estatus" +
                  " FROM CapFactura AS fac, CatCliente AS cc" +
                  " WHERE cc.Id_Cte=fac.Id_Cte AND fac.Fac_Fecha BETWEEN '" + d_ini + "' AND '" + d_fin + "'" +
                  " ORDER BY fac.Fac_Fecha";

                sqlcmd = CapaDatos.GenerarSqlCommand(sql, ref ds);

                DataTable D = new DataTable();
                ds.DataSetName = "Facturas";
                D = ds.Tables[0];
                D.TableName = "Factura";

                string xmlString = ConvertDatatableToXML(D);
                stringXML = xmlString;

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void FoliosFiscalesFacturas(string Conexion,string d_ini, string d_fin,ref string stringXML)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                SqlCommand sqlcmd = default(SqlCommand);

                DataSet ds = new DataSet();
                string sql = "SELECT f.Id_Fac, CONVERT (varchar, f.Fac_Fecha,103) Fecha, f.Id_Cte, c.Cte_NomComercial, substring(cast(fac_xml as varchar(max)),charindex('UUID=',cast(fac_xml as varchar(max)))+6,36) AS Fac_Xml" +
                " FROM CapFactura AS f, CatCliente AS c" +
                " WHERE c.Id_Cte = f.Id_Cte" +
                " AND Fac_Xml IS NOT NULL" +
                " AND f.Fac_Fecha BETWEEN '" + d_ini + "' AND '" + d_fin + "'"+
				" ORDER BY f.Fac_Fecha";

                sqlcmd = CapaDatos.GenerarSqlCommand(sql, ref ds);

                DataTable D = new DataTable();
                ds.DataSetName = "FoliosFiscales";
                D = ds.Tables[0];
                D.TableName = "FolioFiscal";

                string xmlString = ConvertDatatableToXML(D);
                stringXML = xmlString;

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void FacturasSinFechaRevision(string Conexion, string d_fin, ref string stringXML)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                SqlCommand sqlcmd = default(SqlCommand);

                DataSet ds = new DataSet();
                string sql = "SELECT CONVERT (varchar, c.Fac_Fecha,103) Fecha, c.Id_Fac, c.Id_Cte, c.Cte_Nombre," +
                " DATEDIFF(day, c.fac_fecha,'" + d_fin + "') as diferencia" +
                " FROM sianwebcobranza.dbo.ProSeguimientoCobranza AS c" +
                " WHERE c.Fac_FechaRevision is NULL AND c.Fac_Estatus <> 'b'" +
                " ORDER BY c.Fac_Fecha";

                sqlcmd = CapaDatos.GenerarSqlCommand(sql, ref ds);

                DataTable D = new DataTable();
                ds.DataSetName = "FacturasSinFechaRevision";
                D = ds.Tables[0];
                D.TableName = "Facturas";

                string xmlString = ConvertDatatableToXML(D);
                stringXML = xmlString;

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void InventarioProductosAsignados(string Conexion, ref string stringXML)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                SqlCommand sqlcmd = default(SqlCommand);

                DataSet ds = new DataSet();
                string sql = "SELECT cp.Id_Prd, cp.Prd_Descripcion, pi.Prd_InvFinal, pi.Prd_Asignado" +
                           " FROM CatProducto AS cp, CatProductoInventario AS pi" +
                           " WHERE cp.Id_Prd=pi.Id_Prd";

                sqlcmd = CapaDatos.GenerarSqlCommand(sql, ref ds);

                DataTable D = new DataTable();
                ds.DataSetName = "ProductosAsignados";
                D = ds.Tables[0];
                D.TableName = "Producto";

                string xmlString = ConvertDatatableToXML(D);
                stringXML = xmlString;

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void IgualaPorTerritorio(string Conexion,string Id_Terr, string Prd_FechaFin, string Id_Tm, string d_ini, string d_fin, ref string stringXML)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                SqlCommand sqlcmd = default(SqlCommand);

                DataSet ds = new DataSet();

                string sql = "SELECT p.Id_Prd, p.Prd_Descripcion,  pr.Prd_Pesos, SUM(rd.Rem_Cant) AS total" +
                " FROM ProductoPrecio AS pr, CatProducto AS p, CapRemisionDet as rd, CapRemision as r" +
                " WHERE p.id_prd=pr.Id_Prd" +
                " AND pr.id_prd=rd.id_prd" +
                " AND r.id_rem=rd.Id_Rem" +
                " AND r.Id_Ter =" + Id_Terr +
                " AND pr.Prd_FechaFin > '" + Prd_FechaFin + "'" +
                " AND r.Id_Tm=" + Id_Tm +
                " AND r.Rem_Estatus = 'I'" +
                " AND r.Rem_Fecha BETWEEN '" + d_ini + "' AND '" + d_fin + "'" +
                " GROUP BY p.id_prd, p.Prd_Descripcion, pr.Prd_Pesos" +
                " ORDER BY p.Id_Prd";

                sqlcmd = CapaDatos.GenerarSqlCommand(sql, ref ds);

                DataTable D = new DataTable();
                ds.DataSetName = "IgualaPorTerritorios";
                D = ds.Tables[0];
                D.TableName = "IgualaPorTerritorio";

                string xmlString = ConvertDatatableToXML(D);
                stringXML = xmlString;

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void Territorios(string Conexion, ref string stringXML)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                SqlCommand sqlcmd = default(SqlCommand);

                DataSet ds = new DataSet();
                string sql = "SELECT Id_Ter, Ter_Nombre FROM CatTerritorio";

                sqlcmd = CapaDatos.GenerarSqlCommand(sql, ref ds);

                DataTable D = new DataTable();
                ds.DataSetName = "Territorios";
                D = ds.Tables[0];
                D.TableName = "Territorio";

                string xmlString = ConvertDatatableToXML(D);
                stringXML = xmlString;

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void NotasDeCredito(string Conexion, string d_ini, string d_fin, ref string stringXML)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                SqlCommand sqlcmd = default(SqlCommand);

                DataSet ds = new DataSet();
                string sql = "SELECT  CONVERT (varchar,  Ncr_Fecha,103) Fecha, Ncr_Referencia, Ncr_Notas, Id_Ncr, Id_Cte, Id_Ter, Ncr_Subtotal, Ncr_Estatus" +
                           " FROM CapNotaCredito" +
                           " WHERE Ncr_Fecha BETWEEN '" + d_ini + "' AND '" + d_fin + "'" +
                           " ORDER BY Ncr_Fecha";
                sqlcmd = CapaDatos.GenerarSqlCommand(sql, ref ds);

                DataTable D = new DataTable();
                ds.DataSetName = "NotasCredito";
                D = ds.Tables[0];
                D.TableName = "NotaCredito";

                string xmlString = ConvertDatatableToXML(D);
                stringXML = xmlString;

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void IgualaPorClientes(string Conexion, string idCliente, string Id_Tm, string Prd_FechaFin, string d_ini, string d_fin, ref string stringXML)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                SqlCommand sqlcmd = default(SqlCommand);

                DataSet ds = new DataSet();
                string sql = " SELECT p.Id_Prd, p.Prd_Descripcion,  pr.Prd_Pesos, SUM(rd.Rem_Cant) AS total " +
                           " FROM ProductoPrecio AS pr, CatProducto AS p, CapRemisionDet as rd, CapRemision as r " +
                           " WHERE p.id_prd=pr.Id_Prd" +
                           " AND pr.id_prd=rd.id_prd" +
                           " AND r.id_rem=rd.Id_Rem" +
                           " AND r.Id_Cte =" + idCliente +
                           " AND pr.Prd_FechaFin >'" + Prd_FechaFin + "'" +
                           " AND r.Id_Tm=" + Id_Tm +
                           " AND r.Rem_Estatus='I'" +
                           " AND r.Rem_Fecha BETWEEN '" + d_ini + "' AND '" + d_fin + "' " +
                           " GROUP BY p.id_prd, p.Prd_Descripcion, pr.Prd_Pesos" +
                           " ORDER BY p.Id_Prd";

                sqlcmd = CapaDatos.GenerarSqlCommand(sql, ref ds);

                DataTable D = new DataTable();
                ds.DataSetName = "IgualaPorClientes";
                D = ds.Tables[0];
                D.TableName = "IgualaPorCliente";

                string xmlString = ConvertDatatableToXML(D);
                stringXML = xmlString;

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void Productos(string Conexion, string d_fecha, ref string stringXML)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                SqlCommand sqlcmd = default(SqlCommand);

                DataSet ds = new DataSet();
                string sql = "SELECT cp.Id_Prd, cp.Prd_Descripcion, cp.Prd_Presentacion, cp.Prd_UniNe, cp.Id_Ptp, pp.Prd_Pesos" +
                " FROM  CatProducto AS cp, ProductoPrecio AS pp " +
                " WHERE cp.Id_Prd = pp.Id_Prd " +
                " AND pp.Prd_FechaFin > " + d_fecha;

                sqlcmd = CapaDatos.GenerarSqlCommand(sql, ref ds);

                DataTable D = new DataTable();
                ds.DataSetName = "Productos";
                D = ds.Tables[0];
                D.TableName = "Producto";

                string xmlString = ConvertDatatableToXML(D);
                stringXML = xmlString;

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void Pedidos(string Conexion, string d_ini, string d_fin, ref string stringXML)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                SqlCommand sqlcmd = default(SqlCommand);

                DataSet ds = new DataSet();
                string sql = "SELECT CONVERT (varchar, p.Ped_Fecha,103) fpedido, CONVERT (varchar, p.Ped_Fecha,108) hpedido, p.Ped_Solicito, c.Id_Cte, c.Cte_NomComercial, f.Fac_PedNum, p.Ped_Total, CONVERT (varchar, f.Fac_Fecha,103) ffactura, f.Id_Fac, (f.Fac_SubTotal + Fac_ImporteIva) AS total, f.Fac_Estatus, f.Fac_Notas, p.Ped_Estatus" +
                            " FROM CapPedido AS p, CatCliente AS c, CapFactura AS f" +
                            " WHERE f.Id_Cte=c.Id_Cte" +
                            " AND p.Id_Ped=f.Fac_PedNum" +
                            " AND p.Id_Ter=f.Id_Ter" +
                            " AND p.Ped_Fecha BETWEEN '" + d_ini + "' AND '" + d_fin + "'" +
                            " ORDER BY p.Ped_Fecha";

                sqlcmd = CapaDatos.GenerarSqlCommand(sql, ref ds);

                DataTable D = new DataTable();
                ds.DataSetName = "Pedidos";
                D = ds.Tables[0];
                D.TableName = "Pedido";

                string xmlString = ConvertDatatableToXML(D);
                stringXML = xmlString;

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void Proveedores(string Conexion, ref string stringXML)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                SqlCommand sqlcmd = default(SqlCommand);

                DataSet ds = new DataSet();
                string sql = "SELECT Id_Pvd, Pvd_Descripcion FROM CatProveedor";
                sqlcmd = CapaDatos.GenerarSqlCommand(sql, ref ds);

                DataTable D = new DataTable();
                ds.DataSetName = "Proveedores";
                D = ds.Tables[0];
                D.TableName = "Proveedor";

                string xmlString = ConvertDatatableToXML(D);
                stringXML = xmlString;

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void Remisiones(string Conexion, string d_ini, string d_fin, ref string stringXML)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                SqlCommand sqlcmd = default(SqlCommand);

                DataSet ds = new DataSet();
                string sql = "SELECT r.Id_Rem, CONVERT (varchar, r.Rem_Fecha,103) Fecha, r.Id_Ped, r.Id_Cte, c.Cte_NomComercial, r.Rem_Conducto, r.Id_Ter, r.Id_U, u.U_Nombre, r.Id_Tm, m.Tm_Nombre" +
                  " FROM CapRemision AS r, CatUsuario AS u, CatCliente AS c, CatTMovimiento AS m" +
                  " WHERE r.Id_U = u.Id_U" +
                  " AND r.Id_Tm = m.Id_Tm" +
                  " AND r.Id_Cte = c.Id_Cte" +
                  " AND m.Tm_NatMov=1" +
                  " AND r.Rem_Fecha BETWEEN '" + d_ini + "' AND '" + d_fin + "' ORDER BY r.Rem_Fecha";

                sqlcmd = CapaDatos.GenerarSqlCommand(sql, ref ds);

                DataTable D = new DataTable();
                ds.DataSetName = "Remisiones";
                D = ds.Tables[0];
                D.TableName = "Remision";

                string xmlString = ConvertDatatableToXML(D);
                stringXML = xmlString;

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void Rutas(string Conexion, string d_ini, string d_fin, ref string stringXML)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                SqlCommand sqlcmd = default(SqlCommand);

                DataSet ds = new DataSet();
                string sql = "SELECT CONVERT (varchar, f.Fac_Fecha,103) Fecha, e.Id_Emb, ed.Id_Fac, c.Id_Cte, e.Emb_Chofer, CONVERT (varchar, e.Emb_Fec,103) Fecha2, e.Emb_Camioneta, e.Emb_Estatus" +
                            " FROM CapFactura AS f, Embarques AS e, CatCliente AS c, EmbarquesDet AS ed" +
                            " WHERE c.Id_Cte=f.Id_Cte" +
                            " AND f.Id_Fac=ed.Id_Fac" +
                            " AND ed.Id_Emb=e.Id_Emb" +
                            " AND f.Fac_Fecha BETWEEN '" + d_ini + "' AND '" + d_fin + "' ORDER BY f.Fac_Fecha";

                sqlcmd = CapaDatos.GenerarSqlCommand(sql, ref ds);

                DataTable D = new DataTable();
                ds.DataSetName = "Rutas";
                D = ds.Tables[0];
                D.TableName = "Ruta";

                string xmlString = ConvertDatatableToXML(D);
                stringXML = xmlString;

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion

        #region Bitacora

        public string InsertBitacoraWS(string Conexion, string Metodo, string idBD)
        {
            string BaseDeDatos = string.Empty;

            try
            {
                DataTable D = new DataTable();
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                SqlCommand sqlcmd = default(SqlCommand);

                DataSet ds = new DataSet();
                string sql = "InsertBitacoraWS " + "'" + Metodo + "'," + idBD;
                            
                sqlcmd = CapaDatos.GenerarSqlCommand(sql, ref ds);
                D = ds.Tables[0];
                BaseDeDatos = D.Rows[0][0].ToString() + "," + D.Rows[0][1].ToString();

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return BaseDeDatos;

        }
        public string UpdateBitacoraWS(string Conexion, string id, string idBD)
        {
            string BaseDeDatos = string.Empty;

            try
            {
                DataTable D = new DataTable();
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                SqlCommand sqlcmd = default(SqlCommand);

                DataSet ds = new DataSet();
                string sql = "UpdateBitacoraWS "+ id + "," + idBD;

                sqlcmd = CapaDatos.GenerarSqlCommand(sql, ref ds);
                //D = ds.Tables[0];
                //BaseDeDatos = D.Rows[0][0].ToString();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return BaseDeDatos;

        }
        public string ConvertDatatableToXML(DataTable dt)
        {
            MemoryStream str = new MemoryStream();
            dt.WriteXml(str, true);
            str.Seek(0, SeekOrigin.Begin);
            StreamReader sr = new StreamReader(str);
            string xmlstr;
            xmlstr = sr.ReadToEnd();
            return (xmlstr);
        }
        
        #endregion

    }
}
