using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CapaEntidad;
using CapaDatos;
using System.Data.SqlClient;
using System.Xml.Serialization;
using System.IO;

namespace CapaNegocios
{
    public class CN_Consulta
    {
        #region Listo
        public string Clientes(string Conexion)
        {
            string XMLstring = string.Empty;
            CD_Consultas claseCapaDatos = new CD_Consultas();
            claseCapaDatos.Cientes(Conexion, ref XMLstring);
            return XMLstring;
        }
        public string ClientesBloqueados(string Conexion)
        {
            string XMLstring = string.Empty;
            CD_Consultas claseCapaDatos = new CD_Consultas();
            claseCapaDatos.CientesBloqueados(Conexion, ref XMLstring);
            return XMLstring;
        }
        public string ClientesProducto(string Conexion)
        {
            string XMLstring = string.Empty;
            CD_Consultas claseCapaDatos = new CD_Consultas();
            claseCapaDatos.CientesProducto(Conexion, ref XMLstring);
            return XMLstring;
        }
        public string DetalleCientes(string Conexion)
        {
            string XMLstring = string.Empty;
            CD_Consultas claseCapaDatos = new CD_Consultas();
            claseCapaDatos.DetalleCientes(Conexion, ref XMLstring);
            return XMLstring;
        }
        public string EstadisticaVentasEnUnidades(string Conexion, int Id_Emp, int Id_Cd, int Anio, string Territorio, string Cliente, string Producto, int Tipo, int NivelCliente, int NivelProducto, int Reporte, string Id_U)
        {
            string XMLstring = string.Empty;
            CD_Consultas claseCapaDatos = new CD_Consultas();
            claseCapaDatos.EstadisticaVentasEnUnidades(Conexion,Id_Emp,Id_Cd,Anio,Territorio,Cliente,Producto,Tipo,NivelCliente,NivelProducto,Reporte,Id_U,ref XMLstring);
            return XMLstring;
        }
        public string EstadisticaVentasEnPesos(string Conexion, int Id_Emp, int Id_Cd, int Anio, string Territorio, string Cliente, string Producto, int Tipo, int NivelCliente, int NivelProducto, int Reporte, string Id_U)
        {
            string XMLstring = string.Empty;
            CD_Consultas claseCapaDatos = new CD_Consultas();
            claseCapaDatos.EstadisticaVentasEnPesos(Conexion, Id_Emp, Id_Cd, Anio, Territorio, Cliente, Producto, Tipo, NivelCliente, NivelProducto, Reporte, Id_U, ref XMLstring);
            return XMLstring;
        }
        public string FacturasEnRevision(string Conexion,string d_ini,string d_fin)
        {
            string XMLstring = string.Empty;
            CD_Consultas claseCapaDatos = new CD_Consultas();
            claseCapaDatos.FacturasEnRevision(Conexion,d_ini,d_fin,ref XMLstring);
            return XMLstring;
        }
        public string CostosVigentes(string Conexion,string Prd_FechaFin)
        {
            string XMLstring = string.Empty;
            CD_Consultas claseCapaDatos = new CD_Consultas();
            claseCapaDatos.CostosVigentes(Conexion,Prd_FechaFin,ref XMLstring);
            return XMLstring;
        }
        public string DetallePedidos(string Conexion, string d_ini, string d_fin)
        {
            string XMLstring = string.Empty;
            CD_Consultas claseCapaDatos = new CD_Consultas();
            claseCapaDatos.DetallePedidos(Conexion,d_ini,d_fin,ref XMLstring);
            return XMLstring;
        }
        public string FacturasMenores(string Conexion,string d_ini,string d_fin)
        {
            string XMLstring = string.Empty;
            CD_Consultas claseCapaDatos = new CD_Consultas();
            claseCapaDatos.FacturasMenores(Conexion,d_ini,d_fin,ref XMLstring);
            return XMLstring;
        }
        public string Facturas(string Conexion, string d_ini, string d_fin)
        {
            string XMLstring = string.Empty;
            CD_Consultas claseCapaDatos = new CD_Consultas();
            claseCapaDatos.Facturas(Conexion,d_ini,d_fin,ref XMLstring);
            return XMLstring;
        }
        public string FoliosFiscalesFacturas(string Conexion, string d_ini, string d_fin)
        {
            string XMLstring = string.Empty;
            CD_Consultas claseCapaDatos = new CD_Consultas();
            claseCapaDatos.FoliosFiscalesFacturas(Conexion,d_ini,d_fin,ref XMLstring);
            return XMLstring;
        }
        public string FacturasSinFechaRevision(string Conexion, string d_fin)
        {
            string XMLstring = string.Empty;
            CD_Consultas claseCapaDatos = new CD_Consultas();
            claseCapaDatos.FacturasSinFechaRevision(Conexion,d_fin,ref XMLstring);
            return XMLstring;
        }
        public string InventarioProductosAsignados(string Conexion)
        {
            string XMLstring = string.Empty;
            CD_Consultas claseCapaDatos = new CD_Consultas();
            claseCapaDatos.InventarioProductosAsignados(Conexion, ref XMLstring);
            return XMLstring;
        }
        public string IgualaPorTerritorio(string Conexion, string Id_Terr, string Prd_FechaFin, string Id_Tm, string d_ini, string d_fin)
        {
            string XMLstring = string.Empty;
            CD_Consultas claseCapaDatos = new CD_Consultas();
            claseCapaDatos.IgualaPorTerritorio(Conexion, Id_Terr, Prd_FechaFin, Id_Tm, d_ini, d_fin, ref XMLstring);
            return XMLstring;
        }
        public string Territorios(string Conexion)
        {
            string XMLstring = string.Empty;
            CD_Consultas claseCapaDatos = new CD_Consultas();
            claseCapaDatos.Territorios(Conexion, ref XMLstring);
            return XMLstring;
        }
        public string NotasDeCredito(string Conexion, string d_ini, string d_fin)
        {
            string XMLstring = string.Empty;
            CD_Consultas claseCapaDatos = new CD_Consultas();
            claseCapaDatos.NotasDeCredito(Conexion, d_ini, d_fin, ref XMLstring);
            return XMLstring;
        }
        public string IgualaPorClientes(string Conexion, string idCliente, string Id_Tm, string Prd_FechaFin, string d_ini, string d_fin)
        {
            string XMLstring = string.Empty;
            CD_Consultas claseCapaDatos = new CD_Consultas();
            claseCapaDatos.IgualaPorClientes(Conexion, idCliente, Id_Tm, Prd_FechaFin, d_ini, d_fin, ref XMLstring);
            return XMLstring;
        }
        public string Productos(string Conexion, string d_fecha)
        {
            string XMLstring = string.Empty;
            CD_Consultas claseCapaDatos = new CD_Consultas();
            claseCapaDatos.Productos(Conexion, d_fecha, ref XMLstring);
            return XMLstring;
        }
        public string Pedidos(string Conexion, string d_ini, string d_fin)
        {
            string XMLstring = string.Empty;
            CD_Consultas claseCapaDatos = new CD_Consultas();
            claseCapaDatos.Pedidos(Conexion, d_ini, d_fin, ref XMLstring);
            return XMLstring;
        }
        public string Proveedores(string Conexion)
        {
            string XMLstring = string.Empty;
            CD_Consultas claseCapaDatos = new CD_Consultas();
            claseCapaDatos.Proveedores(Conexion, ref XMLstring);
            return XMLstring;
        }
        public string Remisiones(string Conexion, string d_ini, string d_fin)
        {
            string XMLstring = string.Empty;
            CD_Consultas claseCapaDatos = new CD_Consultas();
            claseCapaDatos.Remisiones(Conexion, d_ini, d_fin, ref XMLstring);
            return XMLstring;
        }
        public string Rutas(string Conexion, string d_ini, string d_fin)
        {
            string XMLstring = string.Empty;
            CD_Consultas claseCapaDatos = new CD_Consultas();
            claseCapaDatos.Rutas(Conexion, d_ini, d_fin, ref XMLstring);
            return XMLstring;
        } 
        #endregion

        public string InsertBitacoraWS(string Conexion,string Metodo,string idBD)
        {
            CD_Consultas claseCapaDatos = new CD_Consultas();
            return claseCapaDatos.InsertBitacoraWS(Conexion,Metodo,idBD);
        }
        public string UpdateBitacoraWS(string Conexion, string id, string idBD)
        {
            CD_Consultas claseCapaDatos = new CD_Consultas();
            return claseCapaDatos.UpdateBitacoraWS(Conexion, id, idBD);
        }
        private ClienteBloqueado Deserialize(string xml)
        {
            var xs = new XmlSerializer(typeof(ClienteBloqueado));
            return (ClienteBloqueado)xs.Deserialize(new StringReader(xml));
        }
        private T Deserialize<T>(string xml)
        {
            var xs = new XmlSerializer(typeof(T));
            return (T)xs.Deserialize(new StringReader(xml));
        }

    }
}
