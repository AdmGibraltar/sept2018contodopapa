using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using System.Data.SqlClient;
using CapaNegocios;
using System.Configuration;

namespace ConsultaReportesEspeciales
{
    /// <summary>
    /// Descripción breve de Reportes
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio Web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class Reportes : System.Web.Services.WebService
    {
        
        #region Ready

        [WebMethod]
        public string ConsultaClientes(string idBD)
        {
            string XML = string.Empty;
            string BD = string.Empty;
            string Metodo = "ConsultaClientes";
            string Id = string.Empty;

            string Conexion = ConfigurationManager.AppSettings.Get("strConnection");
            string ConexionWS = ConfigurationManager.AppSettings.Get("strConnectionWS");

            try
            {
                CN_Consulta clsConsulta = new CN_Consulta();
                string Resultado = clsConsulta.InsertBitacoraWS(ConexionWS, Metodo, idBD);

                string[] Result = Resultado.Split(',');
                BD = Result[0].ToString();
                Id = Result[1].ToString();

                Conexion=Conexion.Replace("BD", BD);

                XML = clsConsulta.Clientes(Conexion);

                clsConsulta.UpdateBitacoraWS(ConexionWS, Id, idBD);

            }
            catch (Exception ex)
            {
                
            }
            return XML;
        }

        [WebMethod]
        public string ConsultaClienteBloqueado(string idBD)
        {
            string XML = string.Empty;
            string BD = string.Empty;
            string Metodo = "ConsultaClienteBloqueado";
            string Id = string.Empty;

            string Conexion = ConfigurationManager.AppSettings.Get("strConnection");
            string ConexionWS = ConfigurationManager.AppSettings.Get("strConnectionWS");

            try
            {
                CN_Consulta clsConsulta = new CN_Consulta();
                string Resultado = clsConsulta.InsertBitacoraWS(ConexionWS, Metodo, idBD);

                string[] Result = Resultado.Split(',');
                BD = Result[0].ToString();
                Id = Result[1].ToString();

                Conexion = Conexion.Replace("BD", BD);
                XML = clsConsulta.ClientesBloqueados(Conexion);

                clsConsulta.UpdateBitacoraWS(ConexionWS, Id, idBD);
            }
            catch (Exception ex)
            {
            }
            return XML;

        }

        [WebMethod]
        public string ConsultaClienteProducto(string idBD)
        {
            string XML = string.Empty;
            string BD = string.Empty;
            string Metodo = "ClientesProducto";
            string Id = string.Empty;

            string Conexion = ConfigurationManager.AppSettings.Get("strConnection");
            string ConexionWS = ConfigurationManager.AppSettings.Get("strConnectionWS");

            try
            {
                CN_Consulta clsConsulta = new CN_Consulta();
                string Resultado = clsConsulta.InsertBitacoraWS(ConexionWS, Metodo, idBD);

                string[] Result = Resultado.Split(',');
                BD = Result[0].ToString();
                Id = Result[1].ToString();

                Conexion = Conexion.Replace("BD", BD);
                XML = clsConsulta.ClientesProducto(Conexion);

                clsConsulta.UpdateBitacoraWS(ConexionWS, Id, idBD);
            }
            catch (Exception ex)
            {
            }
            return XML;
        }

        [WebMethod]
        public string ConsultaClienteDetalle(string idBD)
        {
            string XML = string.Empty;
            string BD = string.Empty;
            string Metodo = "DetalleCientes";
            string Id = string.Empty;

            string Conexion = ConfigurationManager.AppSettings.Get("strConnection");
            string ConexionWS = ConfigurationManager.AppSettings.Get("strConnectionWS");

            try
            {
                CN_Consulta clsConsulta = new CN_Consulta();
                string Resultado = clsConsulta.InsertBitacoraWS(ConexionWS, Metodo, idBD);

                string[] Result = Resultado.Split(',');
                BD = Result[0].ToString();
                Id = Result[1].ToString();

                Conexion = Conexion.Replace("BD", BD);
                XML = clsConsulta.DetalleCientes(Conexion);

                clsConsulta.UpdateBitacoraWS(ConexionWS, Id, idBD);
            }
            catch (Exception ex)
            {
            }
            return XML;
        }

        [WebMethod]
        public string EstadisticaVentasEnUnidades(string idBD, int Id_Emp, int Id_Cd, int Anio, string Territorio, string Cliente, string Producto, int NivelCliente, int NivelProducto, int Reporte, string Id_U)
        {
            string XML = string.Empty;
            string BD = string.Empty;
            string Metodo = "EstadisticaVentasEnUnidades";
            string Id = string.Empty;

            string Conexion = ConfigurationManager.AppSettings.Get("strConnection");
            string ConexionWS = ConfigurationManager.AppSettings.Get("strConnectionWS");

            try
            {
                CN_Consulta clsConsulta = new CN_Consulta();
                string Resultado = clsConsulta.InsertBitacoraWS(ConexionWS, Metodo, idBD);

                string[] Result = Resultado.Split(',');
                BD = Result[0].ToString();
                Id = Result[1].ToString();

                Conexion = Conexion.Replace("BD", BD);
                XML = clsConsulta.EstadisticaVentasEnUnidades(Conexion, Id_Emp, Id_Cd, Anio, Territorio, Cliente, Producto, 2, NivelCliente, NivelProducto, Reporte, Id_U);

                clsConsulta.UpdateBitacoraWS(ConexionWS, Id, idBD);
            }
            catch (Exception ex)
            {
            }
            return XML;

        }

        [WebMethod]
        public string EstadisticaVentasEnPesos(string idBD, int Id_Emp, int Id_Cd, int Anio, string Territorio, string Cliente, string Producto, int NivelCliente, int NivelProducto, int Reporte,string Id_U)
        {
            string XML = string.Empty;
            string BD = string.Empty;
            string Metodo = "EstadisticaVentasEnPesos";
            string Id = string.Empty;

            string Conexion = ConfigurationManager.AppSettings.Get("strConnection");
            string ConexionWS = ConfigurationManager.AppSettings.Get("strConnectionWS");

            try
            {
                CN_Consulta clsConsulta = new CN_Consulta();
                string Resultado = clsConsulta.InsertBitacoraWS(ConexionWS, Metodo, idBD);

                string[] Result = Resultado.Split(',');
                BD = Result[0].ToString();
                Id = Result[1].ToString();

                Conexion = Conexion.Replace("BD", BD);
                XML = clsConsulta.EstadisticaVentasEnPesos(Conexion, Id_Emp, Id_Cd, Anio, Territorio, Cliente, Producto, 1, NivelCliente, NivelProducto, Reporte, Id_U);

                clsConsulta.UpdateBitacoraWS(ConexionWS, Id, idBD);
            }
            catch (Exception ex)
            {
            }
            return XML;

        }

        [WebMethod]
        public string FacturasEnRevision(string idBD, string d_ini, string d_fin)
        {
            string XML = string.Empty;
            string BD = string.Empty;
            string Metodo = "FacturasEnRevision";
            string Id = string.Empty;

            string Conexion = ConfigurationManager.AppSettings.Get("strConnection");
            string ConexionWS = ConfigurationManager.AppSettings.Get("strConnectionWS");

            try
            {
                CN_Consulta clsConsulta = new CN_Consulta();
                string Resultado = clsConsulta.InsertBitacoraWS(ConexionWS, Metodo, idBD);

                string[] Result = Resultado.Split(',');
                BD = Result[0].ToString();
                Id = Result[1].ToString();

                Conexion = Conexion.Replace("BD", BD);
                XML = clsConsulta.FacturasEnRevision(Conexion, d_ini, d_fin);

                clsConsulta.UpdateBitacoraWS(ConexionWS, Id, idBD);
            }
            catch (Exception ex)
            {
            }
            return XML;
        }

        [WebMethod]
        public string CostosVigentes(string idBD, string Prd_FechaFin)
        {

            string XML = string.Empty;
            string BD = string.Empty;
            string Metodo = "CostosVigentes";
            string Id = string.Empty;

            string Conexion = ConfigurationManager.AppSettings.Get("strConnection");
            string ConexionWS = ConfigurationManager.AppSettings.Get("strConnectionWS");

            try
            {
                CN_Consulta clsConsulta = new CN_Consulta();
                string Resultado = clsConsulta.InsertBitacoraWS(ConexionWS, Metodo, idBD);

                string[] Result = Resultado.Split(',');
                BD = Result[0].ToString();
                Id = Result[1].ToString();

                Conexion = Conexion.Replace("BD", BD);
                XML = clsConsulta.CostosVigentes(Conexion, Prd_FechaFin);

                clsConsulta.UpdateBitacoraWS(ConexionWS, Id, idBD);
            }
            catch (Exception ex)
            {
            }
            return XML;

        }

        [WebMethod]
        public string DetallePedidos(string idBD, string d_ini, string d_fin)
        {
            string XML = string.Empty;
            string BD = string.Empty;
            string Metodo = "DetallePedidos";
            string Id = string.Empty;

            string Conexion = ConfigurationManager.AppSettings.Get("strConnection");
            string ConexionWS = ConfigurationManager.AppSettings.Get("strConnectionWS");

            try
            {
                CN_Consulta clsConsulta = new CN_Consulta();
                string Resultado = clsConsulta.InsertBitacoraWS(ConexionWS, Metodo, idBD);

                string[] Result = Resultado.Split(',');
                BD = Result[0].ToString();
                Id = Result[1].ToString();

                Conexion = Conexion.Replace("BD", BD);
                XML = clsConsulta.DetallePedidos(Conexion, d_ini, d_fin);

                clsConsulta.UpdateBitacoraWS(ConexionWS, Id, idBD);
            }
            catch (Exception ex)
            {
            }
            return XML;
        }

        [WebMethod]
        public string FacturasMenores(string idBD, string d_ini, string d_fin)
        {
            string XML = string.Empty;
            string BD = string.Empty;
            string Metodo = "FacturasMenores";
            string Id = string.Empty;

            string Conexion = ConfigurationManager.AppSettings.Get("strConnection");
            string ConexionWS = ConfigurationManager.AppSettings.Get("strConnectionWS");

            try
            {
                CN_Consulta clsConsulta = new CN_Consulta();
                string Resultado = clsConsulta.InsertBitacoraWS(ConexionWS, Metodo, idBD);

                string[] Result = Resultado.Split(',');
                BD = Result[0].ToString();
                Id = Result[1].ToString();

                Conexion = Conexion.Replace("BD", BD);
                XML = clsConsulta.FacturasMenores(Conexion, d_ini, d_fin);

                clsConsulta.UpdateBitacoraWS(ConexionWS, Id, idBD);
            }
            catch (Exception ex)
            {
            }
            return XML;
        }

        [WebMethod]
        public string Facturas(string idBD, string d_ini, string d_fin)
        {

            string XML = string.Empty;
            string BD = string.Empty;
            string Metodo = "Facturas";
            string Id = string.Empty;

            string Conexion = ConfigurationManager.AppSettings.Get("strConnection");
            string ConexionWS = ConfigurationManager.AppSettings.Get("strConnectionWS");

            try
            {
                CN_Consulta clsConsulta = new CN_Consulta();
                string Resultado = clsConsulta.InsertBitacoraWS(ConexionWS, Metodo, idBD);

                string[] Result = Resultado.Split(',');
                BD = Result[0].ToString();
                Id = Result[1].ToString();

                Conexion = Conexion.Replace("BD", BD);
                XML = clsConsulta.Facturas(Conexion, d_ini, d_fin);

                clsConsulta.UpdateBitacoraWS(ConexionWS, Id, idBD);
            }
            catch (Exception ex)
            {
            }
            return XML;
        }

        [WebMethod]
        public string FoliosFiscalesFacturas(string idBD, string d_ini, string d_fin)
        {
            string XML = string.Empty;
            string BD = string.Empty;
            string Metodo = "FoliosFiscalesFacturas";
            string Id = string.Empty;

            string Conexion = ConfigurationManager.AppSettings.Get("strConnection");
            string ConexionWS = ConfigurationManager.AppSettings.Get("strConnectionWS");

            try
            {
                CN_Consulta clsConsulta = new CN_Consulta();
                string Resultado = clsConsulta.InsertBitacoraWS(ConexionWS, Metodo, idBD);

                string[] Result = Resultado.Split(',');
                BD = Result[0].ToString();
                Id = Result[1].ToString();

                Conexion = Conexion.Replace("BD", BD);
                XML = clsConsulta.FoliosFiscalesFacturas(Conexion, d_ini, d_fin);

                clsConsulta.UpdateBitacoraWS(ConexionWS, Id, idBD);
            }
            catch (Exception ex)
            {
            }
            return XML;
        }

        [WebMethod]
        public string FacturasSinFechaRevision(string idBD,string d_fin)
        {

            string XML = string.Empty;
            string BD = string.Empty;
            string Metodo = "Facturas";
            string Id = string.Empty;

            string Conexion = ConfigurationManager.AppSettings.Get("strConnection");
            string ConexionWS = ConfigurationManager.AppSettings.Get("strConnectionWS");

            try
            {
                CN_Consulta clsConsulta = new CN_Consulta();
                string Resultado = clsConsulta.InsertBitacoraWS(ConexionWS, Metodo, idBD);

                string[] Result = Resultado.Split(',');
                BD = Result[0].ToString();
                Id = Result[1].ToString();

                Conexion = Conexion.Replace("BD", BD);
                XML = clsConsulta.FacturasSinFechaRevision(Conexion, d_fin);

                clsConsulta.UpdateBitacoraWS(ConexionWS, Id, idBD);
            }
            catch (Exception ex)
            {
            }
            return XML;
        }

        [WebMethod]
        public string InventarioProductosAsignados(string idBD)
        {
            string XML = string.Empty;
            string BD = string.Empty;
            string Metodo = "InventarioProductosAsignados";
            string Id = string.Empty;

            string Conexion = ConfigurationManager.AppSettings.Get("strConnection");
            string ConexionWS = ConfigurationManager.AppSettings.Get("strConnectionWS");

            try
            {
                CN_Consulta clsConsulta = new CN_Consulta();
                string Resultado = clsConsulta.InsertBitacoraWS(ConexionWS, Metodo, idBD);

                string[] Result = Resultado.Split(',');
                BD = Result[0].ToString();
                Id = Result[1].ToString();

                Conexion = Conexion.Replace("BD", BD);
                XML = clsConsulta.InventarioProductosAsignados(Conexion);

                clsConsulta.UpdateBitacoraWS(ConexionWS, Id, idBD);
            }
            catch (Exception ex)
            {
            }
            return XML;
        }

        [WebMethod]
        public string IgualaPorTerritorio(string idBD, string Id_Terr, string Prd_FechaFin, string Id_Tm, string d_ini, string d_fin)
        {
            string XML = string.Empty;
            string BD = string.Empty;
            string Metodo = "InventarioProductosAsignados";
            string Id = string.Empty;

            string Conexion = ConfigurationManager.AppSettings.Get("strConnection");
            string ConexionWS = ConfigurationManager.AppSettings.Get("strConnectionWS");

            try
            {
                CN_Consulta clsConsulta = new CN_Consulta();
                string Resultado = clsConsulta.InsertBitacoraWS(ConexionWS, Metodo, idBD);

                string[] Result = Resultado.Split(',');
                BD = Result[0].ToString();
                Id = Result[1].ToString();

                Conexion = Conexion.Replace("BD", BD);
                XML = clsConsulta.IgualaPorTerritorio(Conexion, Id_Terr, Prd_FechaFin, Id_Tm, d_ini, d_fin);

                clsConsulta.UpdateBitacoraWS(ConexionWS, Id, idBD);
            }
            catch (Exception ex)
            {
            }
            return XML;
        }

        [WebMethod]
        public string Territorios(string idBD)
        {
            string XML = string.Empty;
            string BD = string.Empty;
            string Metodo = "Territorios";
            string Id = string.Empty;

            string Conexion = ConfigurationManager.AppSettings.Get("strConnection");
            string ConexionWS = ConfigurationManager.AppSettings.Get("strConnectionWS");

            try
            {
                CN_Consulta clsConsulta = new CN_Consulta();
                string Resultado = clsConsulta.InsertBitacoraWS(ConexionWS, Metodo, idBD);

                string[] Result = Resultado.Split(',');
                BD = Result[0].ToString();
                Id = Result[1].ToString();

                Conexion = Conexion.Replace("BD", BD);
                XML = clsConsulta.Territorios(Conexion);

                clsConsulta.UpdateBitacoraWS(ConexionWS, Id, idBD);
            }
            catch (Exception ex)
            {
            }
            return XML;
        }

        [WebMethod]
        public string NotasDeCredito(string idBD, string d_ini, string d_fin)
        {
            string XML = string.Empty;
            string BD = string.Empty;
            string Metodo = "NotasDeCredito";
            string Id = string.Empty;

            string Conexion = ConfigurationManager.AppSettings.Get("strConnection");
            string ConexionWS = ConfigurationManager.AppSettings.Get("strConnectionWS");

            try
            {
                CN_Consulta clsConsulta = new CN_Consulta();
                string Resultado = clsConsulta.InsertBitacoraWS(ConexionWS, Metodo, idBD);

                string[] Result = Resultado.Split(',');
                BD = Result[0].ToString();
                Id = Result[1].ToString();

                Conexion = Conexion.Replace("BD", BD);
                XML = clsConsulta.NotasDeCredito(Conexion, d_ini, d_fin);

                clsConsulta.UpdateBitacoraWS(ConexionWS, Id, idBD);
            }
            catch (Exception ex)
            {
            }
            return XML;
        }

        [WebMethod]
        public string IgualaPorClientes(string idBD, string idCliente, string Id_Tm, string Prd_FechaFin, string d_ini, string d_fin)
        {
            string XML = string.Empty;
            string BD = string.Empty;
            string Metodo = "IgualaPorClientes";
            string Id = string.Empty;

            string Conexion = ConfigurationManager.AppSettings.Get("strConnection");
            string ConexionWS = ConfigurationManager.AppSettings.Get("strConnectionWS");

            try
            {
                CN_Consulta clsConsulta = new CN_Consulta();
                string Resultado = clsConsulta.InsertBitacoraWS(ConexionWS, Metodo, idBD);

                string[] Result = Resultado.Split(',');
                BD = Result[0].ToString();
                Id = Result[1].ToString();

                Conexion = Conexion.Replace("BD", BD);
                XML = clsConsulta.IgualaPorClientes(Conexion, idCliente, Id_Tm, Prd_FechaFin, d_ini, d_fin);

                clsConsulta.UpdateBitacoraWS(ConexionWS, Id, idBD);
            }
            catch (Exception ex)
            {
            }
            return XML;

        }

        [WebMethod]
        public string Productos(string idBD, string d_Fecha)
        {
            string XML = string.Empty;
            string BD = string.Empty;
            string Metodo = "Productos";
            string Id = string.Empty;

            string Conexion = ConfigurationManager.AppSettings.Get("strConnection");
            string ConexionWS = ConfigurationManager.AppSettings.Get("strConnectionWS");

            try
            {
                CN_Consulta clsConsulta = new CN_Consulta();
                string Resultado = clsConsulta.InsertBitacoraWS(ConexionWS, Metodo, idBD);

                string[] Result = Resultado.Split(',');
                BD = Result[0].ToString();
                Id = Result[1].ToString();

                Conexion = Conexion.Replace("BD", BD);
                XML = clsConsulta.Productos(Conexion, d_Fecha);

                clsConsulta.UpdateBitacoraWS(ConexionWS, Id, idBD);
            }
            catch (Exception ex)
            {
            }
            return XML;
        }

        [WebMethod]
        public string Pedidos(string idBD, string d_ini, string d_fin)
        {
            string XML = string.Empty;
            string BD = string.Empty;
            string Metodo = "Pedidos";
            string Id = string.Empty;

            string Conexion = ConfigurationManager.AppSettings.Get("strConnection");
            string ConexionWS = ConfigurationManager.AppSettings.Get("strConnectionWS");

            try
            {
                CN_Consulta clsConsulta = new CN_Consulta();
                string Resultado = clsConsulta.InsertBitacoraWS(ConexionWS, Metodo, idBD);

                string[] Result = Resultado.Split(',');
                BD = Result[0].ToString();
                Id = Result[1].ToString();

                Conexion = Conexion.Replace("BD", BD);
                XML = clsConsulta.Pedidos(Conexion, d_ini, d_fin);

                clsConsulta.UpdateBitacoraWS(ConexionWS, Id, idBD);
            }
            catch (Exception ex)
            {
            }
            return XML;
        }

        [WebMethod]
        public string Proveedores(string idBD)
        {
            string XML = string.Empty;
            string BD = string.Empty;
            string Metodo = "Proveedores";
            string Id = string.Empty;

            string Conexion = ConfigurationManager.AppSettings.Get("strConnection");
            string ConexionWS = ConfigurationManager.AppSettings.Get("strConnectionWS");

            try
            {
                CN_Consulta clsConsulta = new CN_Consulta();
                string Resultado = clsConsulta.InsertBitacoraWS(ConexionWS, Metodo, idBD);

                string[] Result = Resultado.Split(',');
                BD = Result[0].ToString();
                Id = Result[1].ToString();

                Conexion = Conexion.Replace("BD", BD);
                XML = clsConsulta.Proveedores(Conexion);

                clsConsulta.UpdateBitacoraWS(ConexionWS, Id, idBD);
            }
            catch (Exception ex)
            {
            }
            return XML;
        }

        [WebMethod]
        public string Remisiones(string idBD, string d_ini, string d_fin)
        {
            string XML = string.Empty;
            string BD = string.Empty;
            string Metodo = "Remisiones";
            string Id = string.Empty;

            string Conexion = ConfigurationManager.AppSettings.Get("strConnection");
            string ConexionWS = ConfigurationManager.AppSettings.Get("strConnectionWS");

            try
            {
                CN_Consulta clsConsulta = new CN_Consulta();
                string Resultado = clsConsulta.InsertBitacoraWS(ConexionWS, Metodo, idBD);

                string[] Result = Resultado.Split(',');
                BD = Result[0].ToString();
                Id = Result[1].ToString();

                Conexion = Conexion.Replace("BD", BD);
                XML = clsConsulta.Remisiones(Conexion, d_ini, d_fin);

                clsConsulta.UpdateBitacoraWS(ConexionWS, Id, idBD);
            }
            catch (Exception ex)
            {
            }
            return XML;
        }

        [WebMethod]
        public string Rutas(string idBD, string d_ini, string d_fin)
        {
            string XML = string.Empty;
            string BD = string.Empty;
            string Metodo = "Rutas";
            string Id = string.Empty;

            string Conexion = ConfigurationManager.AppSettings.Get("strConnection");
            string ConexionWS = ConfigurationManager.AppSettings.Get("strConnectionWS");

            try
            {
                CN_Consulta clsConsulta = new CN_Consulta();
                string Resultado = clsConsulta.InsertBitacoraWS(ConexionWS, Metodo, idBD);

                string[] Result = Resultado.Split(',');
                BD = Result[0].ToString();
                Id = Result[1].ToString();

                Conexion = Conexion.Replace("BD", BD);
                XML = clsConsulta.Rutas(Conexion, d_ini, d_fin);

                clsConsulta.UpdateBitacoraWS(ConexionWS, Id, idBD);
            }
            catch (Exception ex)
            {
            }
            return XML;
        }
        #endregion

        
    }
}
