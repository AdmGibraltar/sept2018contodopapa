using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace CapaEntidad
{
    public class Pedido
    {
        private int _Id_Emp;
        private int _Id_Cd;
        int _Id_Ped;
        DateTime _Ped_Fecha;
        int _Id_Cte;
        string _Cte_NomComercial;
        int _Id_Ter;
        int _Id_Rik;
        int? _Id_Fac;
        string _Pedido_del;
        string _Requisicion;
        string _Ped_Solicito;
        string _Ped_Flete;
        string _Ped_OrdenEntrega;
        int _Ped_CondEntrega;
        //bool _Ped_ABC;
        DateTime _Ped_FechaEntrega;
        string _Ped_Observaciones;
        double _Ped_DescPorcen1;
        string _Ped_Desc1;
        double _Ped_DescPorcen2;
        string _Ped_Desc2;
        string _Ped_Comentarios;
        double _Ped_Importe;
        double _Ped_Subtotal;
        double _Ped_Iva;
        double _Ped_Total;
        string _Estatus;
        int _Id_U;
        string _U_Nombre;
        int _Ped_Tipo;
        int cant_Facturada;
        private string _Ped_TipoStr;
        private bool _Facturacion;
        private string _EstatusStr;
        private bool _Credito;
        private string _CreditoStr;
        private DateTime _ped_FechaAut;
        public string Filtro_Nombre;
        private string _Filtro_CteIni;
        private int? _Id_TG;

        public string Territorios { get; set; }
        public string Clientes { get; set; }
        public string Productos { get; set; }
        public DateTime? FechaIni { get; set; }
        public DateTime? FechaFin { get; set; }
        public string Pedidos { get; set; }


        public int? Id_TG
        {
            get
            {
                return _Id_TG;
            }
            set
            {
                _Id_TG = value;
            }
        }

        public string U_Nombre
        {
            get { return _U_Nombre; }
            set { _U_Nombre = value; }
        }       

        public string Filtro_CteIni
        {
            get { return _Filtro_CteIni; }
            set { _Filtro_CteIni = value; }
        }
        private string _Filtro_CteFin;

        public string Filtro_CteFin
        {
            get { return _Filtro_CteFin; }
            set { _Filtro_CteFin = value; }
        }
        private DateTime? _Filtro_FecIni;

        public DateTime? Filtro_FecIni
        {
            get { return _Filtro_FecIni; }
            set { _Filtro_FecIni = value; }
        }
        private DateTime? _Filtro_FecFin;

        public DateTime? Filtro_FecFin
        {
            get { return _Filtro_FecFin; }
            set { _Filtro_FecFin = value; }
        }
        private double? _Filtro_PedIni;

        public double? Filtro_PedIni
        {
            get { return _Filtro_PedIni; }
            set { _Filtro_PedIni = value; }
        }
        private double? _Filtro_PedFin;
        private DateTime _FechaAsignacion;
        private string _Filtro_usuario;
        private string _Filtro_Tipo;

        public string Filtro_Tipo
        {
            get { return _Filtro_Tipo; }
            set { _Filtro_Tipo = value; }
        }
        private string _Filtro_Estatus;
        public string Rik_Nombre;
        public string Ter_Nombre;
        public string Ped_SolicitoTel;
        public string Ped_SolicitoEmail;
        public string Ped_SolicitoPuesto;
        public string Ped_ConsignadoCalle;
        public string Ped_ConsignadoNo;
        public string Ped_ConsignadoCp;
        public string Ped_ConsignadoMunicipio;
        public string Ped_ConsignadoEstado;
        public string Ped_ConsignadoColonia;
        public bool Ped_ReqOrden;
        public string Ped_OrdenCompra;
        public int Ped_AcysSemana;
        public int Ped_AcysAnio;
        public int Id_Acs;

        private bool _Ped_Captacion = false;
        public string Filtro_Doc;

        public bool Ped_Captacion
        {
            get { return _Ped_Captacion; }
            set { _Ped_Captacion = value; }
        }

        public string Filtro_Estatus
        {
            get { return _Filtro_Estatus; }
            set { _Filtro_Estatus = value; }
        }


        public string Filtro_usuario
        {
            get { return _Filtro_usuario; }
            set { _Filtro_usuario = value; }
        }

        public DateTime FechaAsignacion
        {
            get { return _FechaAsignacion; }
            set { _FechaAsignacion = value; }
        }

        public double? Filtro_PedFin
        {
            get { return _Filtro_PedFin; }
            set { _Filtro_PedFin = value; }
        }


        public string EstatusStr
        {
            get { return _EstatusStr; }
            set { _EstatusStr = value; }
        }
        public bool Facturacion
        {
            get { return _Facturacion; }
            set { _Facturacion = value; }
        }
        public string Ped_TipoStr
        {
            get { return _Ped_TipoStr; }
            set { _Ped_TipoStr = value; }
        }
        public int Ped_Tipo
        {
            get { return _Ped_Tipo; }
            set { _Ped_Tipo = value; }
        }

        public int Cant_Facturada
        {
            get { return cant_Facturada; }
            set { cant_Facturada = value; }
        }
        public int Id_U
        {
            get { return _Id_U; }
            set { _Id_U = value; }
        }
        public int Id_Ped
        {
            get { return _Id_Ped; }
            set { _Id_Ped = value; }
        }
        public DateTime Ped_Fecha
        {
            get { return _Ped_Fecha; }
            set { _Ped_Fecha = value; }
        }
        public int Id_Cte
        {
            get { return _Id_Cte; }
            set { _Id_Cte = value; }
        }
        public string Cte_NomComercial
        {
            get { return _Cte_NomComercial; }
            set { _Cte_NomComercial = value; }
        }
        public int Id_Ter
        {
            get { return _Id_Ter; }
            set { _Id_Ter = value; }
        }
        public int Id_Rik
        {
            get { return _Id_Rik; }
            set { _Id_Rik = value; }
        }
        public int? Id_Fac
        {
            get { return _Id_Fac; }
            set { _Id_Fac = value; }
        }
        public string Pedido_del
        {
            get { return _Pedido_del; }
            set { _Pedido_del = value; }
        }
        public string Requisicion
        {
            get { return _Requisicion; }
            set { _Requisicion = value; }
        }
        public string Ped_Solicito
        {
            get { return _Ped_Solicito; }
            set { _Ped_Solicito = value; }
        }
        public string Ped_Flete
        {
            get { return _Ped_Flete; }
            set { _Ped_Flete = value; }
        }
        public string Ped_OrdenEntrega
        {
            get { return _Ped_OrdenEntrega; }
            set { _Ped_OrdenEntrega = value; }
        }
        public int Ped_CondEntrega
        {
            get { return _Ped_CondEntrega; }
            set { _Ped_CondEntrega = value; }
        }
        public DateTime Ped_FechaEntrega
        {
            get { return _Ped_FechaEntrega; }
            set { _Ped_FechaEntrega = value; }
        }
        public string Ped_Observaciones
        {
            get { return _Ped_Observaciones; }
            set { _Ped_Observaciones = value; }
        }
        public double Ped_DescPorcen1
        {
            get { return _Ped_DescPorcen1; }
            set { _Ped_DescPorcen1 = value; }
        }
        public string Ped_Desc1
        {
            get { return _Ped_Desc1; }
            set { _Ped_Desc1 = value; }
        }
        public double Ped_DescPorcen2
        {
            get { return _Ped_DescPorcen2; }
            set { _Ped_DescPorcen2 = value; }
        }
        public string Ped_Desc2
        {
            get { return _Ped_Desc2; }
            set { _Ped_Desc2 = value; }
        }
        public string Ped_Comentarios
        {
            get { return _Ped_Comentarios; }
            set { _Ped_Comentarios = value; }
        }
        public double Ped_Importe
        {
            get { return _Ped_Importe; }
            set { _Ped_Importe = value; }
        }
        public double Ped_Subtotal
        {
            get { return _Ped_Subtotal; }
            set { _Ped_Subtotal = value; }
        }
        public double Ped_Iva
        {
            get { return _Ped_Iva; }
            set { _Ped_Iva = value; }
        }
        public double Ped_Total
        {
            get { return _Ped_Total; }
            set { _Ped_Total = value; }
        }
        public int Id_Emp
        {
            get { return _Id_Emp; }
            set { _Id_Emp = value; }
        }
        public int Id_Cd
        {
            get { return _Id_Cd; }
            set { _Id_Cd = value; }
        }
        public string Estatus
        {
            get { return _Estatus; }
            set { _Estatus = value; }
        }
        public bool Credito
        {
            get { return _Credito; }
            set { _Credito = value; }
        }
        public string CreditoStr
        {
            get { return _CreditoStr; }
            set { _CreditoStr = value; }
        }

        public DateTime Ped_FechaAut
        {
            get { return _ped_FechaAut; }
            set { _ped_FechaAut = value; }
        }

        private int _ped_Cantidad;
        private int _ped_CantidadDisponible;
        private int _ped_PorcentajeCantidadDisponible;
        private int _ped_ImporteOrdenado;
        private int _ped_ImporteDisponible;
        private int _ped_PorcentajeImporteDisponible;
        private int _ped_Asignado;
        private int _ped_PorcentajeAsignado;
        private int _ped_ImporteAsignado;
        private int _ped_PorcentajeImporteAsignado;
        private string _ruta;
        private string _sector;
        private int _secuencia;
        private DateTime _FechaFacAcys;
        private string _PedAcys;
        private string _ReqAcys;
        private string _OcAcys;

        public string Ruta
        {
            get { return _ruta; }
            set { _ruta = value; }
        }

        public string PedAcys
        {
            get { return _PedAcys; }
            set { _PedAcys = value; }
        }

        public string ReqAcys
        {
            get { return _ReqAcys; }
            set { _ReqAcys = value; }
        }

        public string OcAcys
        {
            get { return _OcAcys; }
            set { _OcAcys = value; }
        }


        public DateTime FechaFacAcys
        {
            get { return _FechaFacAcys; }
            set { _FechaFacAcys = value; }
        }


        public string Sector
        {
            get { return _sector; }
            set { _sector = value; }
        }

        public int Secuencia
        {
            get { return _secuencia; }
            set { _secuencia = value; }
        }

        public int Ped_Cantidad
        {
            get { return _ped_Cantidad; }
            set { _ped_Cantidad = value; }
        }
        public int Ped_CantidadDisponible
        {
            get { return _ped_CantidadDisponible; }
            set { _ped_CantidadDisponible = value; }
        }
        public int Ped_PorcentajeCantidadDisponible
        {
            get { return _ped_PorcentajeCantidadDisponible; }
            set { _ped_PorcentajeCantidadDisponible = value; }
        }
        public int Ped_ImporteOrdenado
        {
            get { return _ped_ImporteOrdenado; }
            set { _ped_ImporteOrdenado = value; }
        }
        public int Ped_ImporteDisponible
        {
            get { return _ped_ImporteDisponible; }
            set { _ped_ImporteDisponible = value; }
        }
        public int Ped_PorcentajeImporteDisponible
        {
            get { return _ped_PorcentajeImporteDisponible; }
            set { _ped_PorcentajeImporteDisponible = value; }
        }
        public int Ped_Asignado
        {
            get { return _ped_Asignado; }
            set { _ped_Asignado = value; }
        }
        public int Ped_PorcentajeAsignado
        {
            get { return _ped_PorcentajeAsignado; }
            set { _ped_PorcentajeAsignado = value; }
        }
        public int Ped_ImporteAsignado
        {
            get { return _ped_ImporteAsignado; }
            set { _ped_ImporteAsignado = value; }
        }
        public int Ped_PorcentajeImporteAsignado
        {
            get { return _ped_PorcentajeImporteAsignado; }
            set { _ped_PorcentajeImporteAsignado = value; }
        }

        //Filtros agregados

        private double? _filtroRutaInicial;
        private double? _filtroRutaFinal;
        private double? _filtroSectorInicial;
        private double? _filtroSectorFinal;

        public double? Filtro_RutaInicial
        {
            get { return _filtroRutaInicial; }
            set { _filtroRutaInicial = value; }
        }

        public double? Filtro_RutaFinal
        {
            get { return _filtroRutaFinal; }
            set { _filtroRutaFinal = value; }
        }

        public double? Filtro_SectorInicial
        {
            get { return _filtroSectorInicial; }
            set { _filtroSectorInicial = value; }
        }

        public double? Filtro_SectorFinal
        {
            get { return _filtroSectorFinal; }
            set { _filtroSectorFinal = value; }
        }

        private bool _filtro_Credito;

        public bool Filtro_Credito
        {
            get { return _filtro_Credito; }
            set { _filtro_Credito = value; }
        }
    } 
}
