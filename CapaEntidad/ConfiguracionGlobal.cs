using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class ConfiguracionGlobal
    {
        private Int32 _Id_Cd = 0;
        private bool _Solicitud_Prospecto = false;
        private string _Hora_Zona = "";
        private bool _Hora_Verano = false;
        private string _Mail_Servidor = "";
        private string _Mail_Usuario = "";
        private string _Mail_Contraseña = "";
        private string _Mail_Puerto = "";
        private string _Mail_Remitente = "";
        private string _Login_Intentos = "";
        private string _Login_Tiempo_Bloqueo = "";
        private string _Contraseña_Tiempo_Vida = "";
        private string _Contraseña_Long_Min = "";
        private string _Mail_CompLocal = "";
        private string _Mail_PrecioEsp;
        private string _Mail_BaseInstalada;
        private string _Mail_Acys;
        private string _Mail_EVirtual;
        private Int32 _Id_Conf;
        private int _Id_Emp;
        private string _Mail_Valuacion;
        private double _Mail_MinValuacion;
        private string _Mail_OrdenCompra;
        private bool _Remisiones_Especiales = false;
        private string _Mail_TransferenciasCedis;
        private string _Mail_OrdenCompra_sisprop;
        private string _CorreAprobadoresCambiosTerriotrio;
        //RBM Se agrega elemnto para autorizar baja de factura
        private string _Mail_AutorizaBajaFactura;

        public string Mail_AutorizaBajaFactura
        {
            get { return _Mail_AutorizaBajaFactura; }
            set { _Mail_AutorizaBajaFactura = value; }
        }
        //RBM

        public string Mail_OrdenCompra_sisprop
        {
            get { return _Mail_OrdenCompra_sisprop; }
            set { _Mail_OrdenCompra_sisprop = value; }
        }

        public string Mail_TransferenciasCedis
        {
            get { return _Mail_TransferenciasCedis; }
            set { _Mail_TransferenciasCedis = value; }
        }
        private string _Mail_OrdenesCompra;

        public string Mail_OrdenesCompra
        {
            get { return _Mail_OrdenesCompra; }
            set { _Mail_OrdenesCompra = value; }
        }

        public int Id_Emp
        {
            get { return _Id_Emp; }
            set { _Id_Emp = value; }
        }

        public Int32 Id_Cd
        {
            get { return _Id_Cd; }
            set { _Id_Cd = value; }
        }
        public bool Solicitud_Prospecto
        {
            get { return _Solicitud_Prospecto; }
            set { _Solicitud_Prospecto = value; }
        }
        public string Hora_Zona
        {
            get { return _Hora_Zona.Trim(); }
            set { _Hora_Zona = value.Trim(); }
        }
        public bool Hora_Verano
        {
            get { return _Hora_Verano; }
            set { _Hora_Verano = value; }
        }
        public string Mail_Servidor
        {
            get { return _Mail_Servidor.Trim(); }
            set { _Mail_Servidor = value.Trim(); }
        }
        public string Mail_Usuario
        {
            get { return _Mail_Usuario.Trim(); }
            set { _Mail_Usuario = value.Trim(); }
        }
        public string Mail_Contraseña
        {
            get { return _Mail_Contraseña.Trim(); }
            set { _Mail_Contraseña = value.Trim(); }
        }
        public string Mail_Puerto
        {
            get { return _Mail_Puerto.Trim(); }
            set { _Mail_Puerto = value.Trim(); }
        }
        public string Mail_Remitente
        {
            get { return _Mail_Remitente.Trim(); }
            set { _Mail_Remitente = value.Trim(); }
        }
        public string Login_Intentos
        {
            get { return _Login_Intentos.Trim(); }
            set { _Login_Intentos = value.Trim(); }
        }
        public string Login_Tiempo_Bloqueo
        {
            get { return _Login_Tiempo_Bloqueo.Trim(); }
            set { _Login_Tiempo_Bloqueo = value.Trim(); }
        }
        public string Contraseña_Tiempo_Vida
        {
            get { return _Contraseña_Tiempo_Vida.Trim(); }
            set { _Contraseña_Tiempo_Vida = value.Trim(); }
        }
        public string Contraseña_Long_Min
        {
            get { return _Contraseña_Long_Min.Trim(); }
            set { _Contraseña_Long_Min = value.Trim(); }
        }
        public Int32 Id_Conf
        {
            get { return _Id_Conf; }
            set { _Id_Conf = value; }
        }
        public string Mail_CompLocal
        {
            get { return _Mail_CompLocal; }
            set { _Mail_CompLocal = value; }
        }
        public string Mail_PrecioEsp
        {
            get { return _Mail_PrecioEsp; }
            set { _Mail_PrecioEsp = value; }
        }


        public string Mail_BaseInstalada
        {
            get { return _Mail_BaseInstalada; }
            set { _Mail_BaseInstalada = value; }
        }

        public string Mail_Acys
        {
            get { return _Mail_Acys; }
            set { _Mail_Acys = value; }
        }



        public string Mail_EVirtual
        {
            get { return _Mail_EVirtual; }
            set { _Mail_EVirtual = value; }
        }


        public string Mail_Valuacion
        {
            get { return _Mail_Valuacion; }
            set { _Mail_Valuacion = value; }
        }

        public double Mail_MinValuacion
        {
            get { return _Mail_MinValuacion; }
            set { _Mail_MinValuacion = value; }
        }

        //jfcv 26oct2016 agregar configuración para remisiones especiales
        public bool Remisiones_Especiales
        {
            get { return _Remisiones_Especiales; }
            set { _Remisiones_Especiales = value; }
        }

        public string Mail_OrdenCompra
        {
            get { return _Mail_OrdenCompra; }
            set { _Mail_OrdenCompra = value; }
        }



        public string CorreAprobadoresCambiosTerriotrio
        {
            get { return _CorreAprobadoresCambiosTerriotrio; }
            set { _CorreAprobadoresCambiosTerriotrio = value; }
        }

        public string Mail_GastosProveedores { get; set; }
        public string Mail_GastosAcreedores { get; set; }
        public string Mail_GastosComprasLocales { get; set; }
        public string Mail_GastosFletes { get; set; }
        public string Mail_GastosNoInventariables { get; set; }
        public string Mail_GastosPagoServicios { get; set; }
        public string Mail_GastosOtrosPagos { get; set; }
        public string Mail_GastosReposicionCaja { get; set; }
        public string Mail_GastosCuentaGastos { get; set; }
        public string Mail_GastosComprobacion { get; set; }
        public string Mail_GastosAvisoGerente { get; set; }
        public string Mail_GastosAvisoUsuario { get; set; }
        //jfcv 12 sep 2016
        public string Mail_GastosHonorarios { get; set; }
        public string Mail_GastosArrendamientos { get; set; }
        //jfcv 14 oct 2016
        public string Mail_AutorizaReFacturas { get; set; }
        public string Mail_ResponsableReFacturas { get; set; }
        //jfcv 22Jun2017 agregar configuración para comprobación de compras 
        public string Mail_GastosComprobacionCompras { get; set; }
        public string Cuenta_GastosComprobacion { get; set; }
        public string Cuenta_GastosComprobacionCompras { get; set; }

        //RBM se agrega configuracion para modulo de quejas

        private string mail_PlaneacioyCompras;

        public string Mail_PlaneacioyCompras
        {
            get { return mail_PlaneacioyCompras; }
            set { mail_PlaneacioyCompras = value; }
        }
        private string mail_AbastoyEntregas;

        public string Mail_AbastoyEntregas
        {
            get { return mail_AbastoyEntregas; }
            set { mail_AbastoyEntregas = value; }
        }
        private string mail_OperacionesCEDIS;

        public string Mail_OperacionesCEDIS
        {
            get { return mail_OperacionesCEDIS; }
            set { mail_OperacionesCEDIS = value; }
        }
        private string mail_ServicioCliente;

        public string Mail_ServicioCliente
        {
            get { return mail_ServicioCliente; }
            set { mail_ServicioCliente = value; }
        }

        private string mail_CXCFranquicias;

        public string Mail_CXCFranquicias
        {
            get { return mail_CXCFranquicias; }
            set { mail_CXCFranquicias = value; }
        }

        private string mail_ValidarCorreos;

        public string Mail_ValidarCorreos
        {
            get { return mail_ValidarCorreos; }
            set { mail_ValidarCorreos = value; }
        }

        private string mail_Autorizaterritorios;

        public string Mail_Autorizaterritorios
        {
            get { return mail_Autorizaterritorios; }
            set { mail_Autorizaterritorios = value; }
        }

        //RBM


    }
}
