using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class CentroDistribucion
    {

        #region Campos de datos generales y Valuacion de proyectos

        //campos para datos generales de C. de Dist.
        private int _Id_Emp;
        private int _Id_Cd;
        private int _Id_Reg;
        private string _Cd_Pais;
        private string _Cd_Estado;
        private string _Cd_Ciudad;
        private string _Cd_Municipio;
        private string _Cd_Colonia;
        private string _Cd_Descripcion;
        private string _Cd_CalleNo;
        private string _Cd_Calle;
        private string _Cd_Numero;
        private string _Cd_CP;
        private string _Cd_Tel;
        private string _Cd_Rfc;
        private int _Id_TipoCD;
        private int _Cd_Formato;

        //Campos para los datos de valuacion de proyectos de centro de distribución
        private double? _Cd_TasaCetes;
        private int _Cd_DiasCuentasPorCobrar;
        private int? _Cd_Dias;
        private int? _Cd_DiasInv;
        private double? _Cd_FactorInvComodato;
        private double? _Cd_FactorConvActFijo;
        private int _Cd_DiasFinanciaProv;
        private double? _Cd_TasaIncCostoCapital;
        private double? _Cd_Iva;
        private double? _Cd_Flete;
        private double? _Cd_ComisionRik;
        private double? _Cd_OtrosGastosVar;
        private double? _Cd_ContribucionGastosFijosOtros;
        private double? _Cd_ContribucionGastosFijosPapel;
        private double? _Cd_ISRyPTU;
        private double? _Cd_CargoUCS;
        private bool _Cd_Activo;


        #endregion


        #region Propiedades de datos generales y Valuacion de proyectos

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
        public int Id_Reg
        {
            get { return _Id_Reg; }
            set { _Id_Reg = value; }
        }
        public string Cd_Pais
        {
            get { return _Cd_Pais; }
            set { _Cd_Pais = value; }
        }
        public string Cd_Estado
        {
            get { return _Cd_Estado; }
            set { _Cd_Estado = value; }
        }
        public string Cd_Ciudad
        {
            get { return _Cd_Ciudad; }
            set { _Cd_Ciudad = value; }
        }
        public string Cd_Municipio
        {
            get { return _Cd_Municipio; }
            set { _Cd_Municipio = value; }
        }
        public string Cd_Colonia
        {
            get { return _Cd_Colonia; }
            set { _Cd_Colonia = value; }
        }
        public string Cd_Descripcion
        {
            get { return _Cd_Descripcion.Trim(); }
            set { _Cd_Descripcion = value.Trim(); }
        }
        public string Cd_CalleNo
        {
            get { return _Cd_CalleNo; }
            set { _Cd_CalleNo = value; }
        }
        public string Cd_Calle
        {
            get { return _Cd_Calle; }
            set { _Cd_Calle = value; }
        }
        public string Cd_Numero
        {
            get { return _Cd_Numero; }
            set { _Cd_Numero = value; }
        }
        public string Cd_CP
        {
            get { return _Cd_CP.Trim(); }
            set { _Cd_CP = value.Trim(); }
        }
        public string Cd_Tel
        {
            get { return _Cd_Tel.Trim(); }
            set { _Cd_Tel = value.Trim(); }
        }
        public string Cd_Rfc
        {
            get { return _Cd_Rfc; }
            set { _Cd_Rfc = value; }
        }
        public int Id_TipoCD
        {
            get { return _Id_TipoCD; }
            set { _Id_TipoCD = value; }
        }
        public int Cd_Formato
        {
            get { return _Cd_Formato; }
            set { _Cd_Formato = value; }
        }
        public bool Cd_Activo
        {
            get { return _Cd_Activo; }
            set { _Cd_Activo = value; }
        }
        public string Cd_ActivoStr
        {
            get { return this._Cd_Activo ? "Si" : "No"; }
        }


        public double? Cd_TasaCetes
        {
            get { return _Cd_TasaCetes; }
            set { _Cd_TasaCetes = value; }
        }
        public int Cd_DiasCuentasPorCobrar
        {
            get { return _Cd_DiasCuentasPorCobrar; }
            set { _Cd_DiasCuentasPorCobrar = value; }
        }
        public int? Cd_Dias
        {
            get { return _Cd_Dias; }
            set { _Cd_Dias = value; }
        }
        public int? Cd_DiasInv
        {
            get { return _Cd_DiasInv; }
            set { _Cd_DiasInv = value; }
        }
        public double? Cd_FactorInvComodato
        {
            get { return _Cd_FactorInvComodato; }
            set { _Cd_FactorInvComodato = value; }
        }
        public double? Cd_FactorConvActFijo
        {
            get { return _Cd_FactorConvActFijo; }
            set { _Cd_FactorConvActFijo = value; }
        }
        public int Cd_DiasFinanciaProv
        {
            get { return _Cd_DiasFinanciaProv; }
            set { _Cd_DiasFinanciaProv = value; }
        }
        public double? Cd_TasaIncCostoCapital
        {
            get { return _Cd_TasaIncCostoCapital; }
            set { _Cd_TasaIncCostoCapital = value; }
        }
        public double? Cd_Iva
        {
            get { return _Cd_Iva; }
            set { _Cd_Iva = value; }
        }
        public double? Cd_Flete
        {
            get { return _Cd_Flete; }
            set { _Cd_Flete = value; }
        }
        public double? Cd_ComisionRik
        {
            get { return _Cd_ComisionRik; }
            set { _Cd_ComisionRik = value; }
        }
        public double? Cd_OtrosGastosVar
        {
            get { return _Cd_OtrosGastosVar; }
            set { _Cd_OtrosGastosVar = value; }
        }
        public double? Cd_ContribucionGastosFijosOtros
        {
            get { return _Cd_ContribucionGastosFijosOtros; }
            set { _Cd_ContribucionGastosFijosOtros = value; }
        }
        public double? Cd_ContribucionGastosFijosPapel
        {
            get { return _Cd_ContribucionGastosFijosPapel; }
            set { _Cd_ContribucionGastosFijosPapel = value; }
        }
        public double? Cd_ISRyPTU
        {
            get { return _Cd_ISRyPTU; }
            set { _Cd_ISRyPTU = value; }
        }
        public double? Cd_CargoUCS
        {
            get { return _Cd_CargoUCS; }
            set { _Cd_CargoUCS = value; }
        }

        #endregion


        #region Propiedades de informacion pedidos y facturacion

        private int _Cd_FacturasRangoInicio;
        public int Cd_FacturasRangoInicio
        {
            get { return _Cd_FacturasRangoInicio; }
            set { _Cd_FacturasRangoInicio = value; }
        }
        private int _Cd_PartidaFacturas;
        public int Cd_PartidaFacturas
        {
            get { return _Cd_PartidaFacturas; }
            set { _Cd_PartidaFacturas = value; }
        }
        private int _Cd_FacturasRangoFin;
        public int Cd_FacturasRangoFin
        {
            get { return _Cd_FacturasRangoFin; }
            set { _Cd_FacturasRangoFin = value; }
        }
        private int _Cd_ContadorPedidos;
        public int Cd_ContadorPedidos
        {
            get { return _Cd_ContadorPedidos; }
            set { _Cd_ContadorPedidos = value; }
        }
        private int _Cd_PartidaPedidos;
        public int Cd_PartidaPedidos
        {
            get { return _Cd_PartidaPedidos; }
            set { _Cd_PartidaPedidos = value; }
        }
        private int _Cd_IvaPedidosFacturacion;
        public int Cd_IvaPedidosFacturacion
        {
            get { return _Cd_IvaPedidosFacturacion; }
            set { _Cd_IvaPedidosFacturacion = value; }
        }
        private int _Cd_ClientesRangoInicio;
        public int Cd_ClientesRangoInicio
        {
            get { return _Cd_ClientesRangoInicio; }
            set { _Cd_ClientesRangoInicio = value; }
        }
        private int _Cd_ClientesRangoFin;
        public int Cd_ClientesRangoFin
        {
            get { return _Cd_ClientesRangoFin; }
            set { _Cd_ClientesRangoFin = value; }
        }

        private int _Cd_AjusteFromatoReng;
        public int Cd_AjusteFromatoReng
        {
            get { return _Cd_AjusteFromatoReng; }
            set { _Cd_AjusteFromatoReng = value; }
        }
        private int _Cd_MaximoTerritoriosSegmentos;
        public int Cd_MaximoTerritoriosSegmentos
        {
            get { return _Cd_MaximoTerritoriosSegmentos; }
            set { _Cd_MaximoTerritoriosSegmentos = value; }
        }
        private bool _Cd_FormatoFacturaRetIva;
        public bool Cd_FormatoFacturaRetIva
        {
            get { return _Cd_FormatoFacturaRetIva; }
            set { _Cd_FormatoFacturaRetIva = value; }
        }
        private bool _Cd_DeshabilitarReglaCons;
        public bool Cd_DeshabilitarReglaCons
        {
            get { return _Cd_DeshabilitarReglaCons; }
            set { _Cd_DeshabilitarReglaCons = value; }
        }
        private bool _Cd_ActivaCapPedRep;
        public bool Cd_ActivaCapPedRep
        {
            get { return _Cd_ActivaCapPedRep; }
            set { _Cd_ActivaCapPedRep = value; }
        }

        #endregion


        #region Propiedades de informacion de Inventarios


        private int _Cd_PartidaRemisiones;
        public int Cd_PartidaRemisiones
        {
            get { return _Cd_PartidaRemisiones; }
            set { _Cd_PartidaRemisiones = value; }
        }
        private int _Cd_PartidaEntradas;
        public int Cd_PartidaEntradas
        {
            get { return _Cd_PartidaEntradas; }
            set { _Cd_PartidaEntradas = value; }
        }
        private int _Cd_AjusteFromatoRengInventario;
        public int Cd_AjusteFromatoRengInventario
        {
            get { return _Cd_AjusteFromatoRengInventario; }
            set { _Cd_AjusteFromatoRengInventario = value; }
        }


        #endregion


        #region Propiedades de informacion de Cobranza

        private int _Cd_InteresMoratorio;
        public int Cd_InteresMoratorio
        {
            get { return _Cd_InteresMoratorio; }
            set { _Cd_InteresMoratorio = value; }
        }
        private decimal _Cd_ContribucionBruta;
        public decimal Cd_ContribucionBruta
        {
            get { return _Cd_ContribucionBruta; }
            set { _Cd_ContribucionBruta = value; }
        }
        private decimal _Cd_Amortizacion;
        public decimal Cd_Amortizacion
        {
            get { return _Cd_Amortizacion; }
            set { _Cd_Amortizacion = value; }
        }
        private decimal _Cd_SaldosMenores;
        public decimal Cd_SaldosMenores
        {
            get { return _Cd_SaldosMenores; }
            set { _Cd_SaldosMenores = value; }
        }
        private string _Cd_CobranzaPersonaFormula;
        public string Cd_CobranzaPersonaFormula
        {
            get { return _Cd_CobranzaPersonaFormula; }
            set { _Cd_CobranzaPersonaFormula = value; }
        }
        private string _Cd_CobranzaPersonaAutoriza;
        public string Cd_CobranzaPersonaAutoriza
        {
            get { return _Cd_CobranzaPersonaAutoriza; }
            set { _Cd_CobranzaPersonaAutoriza = value; }
        }
        private int _Cd_PartidaNotaCargo;
        public int Cd_PartidaNotaCargo
        {
            get { return _Cd_PartidaNotaCargo; }
            set { _Cd_PartidaNotaCargo = value; }
        }
        private int _Cd_PartidaNotaCredito;
        public int Cd_PartidaNotaCredito
        {
            get { return _Cd_PartidaNotaCredito; }
            set { _Cd_PartidaNotaCredito = value; }
        }
        private int _Cd_RelacionCobranza;
        public int Cd_RelacionCobranza
        {
            get { return _Cd_RelacionCobranza; }
            set { _Cd_RelacionCobranza = value; }
        }

        #endregion


        #region Propiedades de administración de inventarios

        private int _Cd_TiempoEntrega;
        public int Cd_TiempoEntrega
        {
            get { return _Cd_TiempoEntrega; }
            set { _Cd_TiempoEntrega = value; }
        }
        private int _Cd_TiempoTransportacion;
        public int Cd_TiempoTransportacion
        {
            get { return _Cd_TiempoTransportacion; }
            set { _Cd_TiempoTransportacion = value; }
        }

        private int? _Cd_NumMacola;
        public int? Cd_NumMacola
        {
            get { return _Cd_NumMacola; }
            set { _Cd_NumMacola = value; }
        }



        #endregion


        #region Propiedades de compras locales

        private bool _Cd_ActualizaEntradaAuto;
        public bool Cd_ActualizaEntradaAuto
        {
            get { return _Cd_ActualizaEntradaAuto; }
            set { _Cd_ActualizaEntradaAuto = value; }
        }
        private int _Cd_FactorCosto;
        public int Cd_FactorCosto
        {
            get { return _Cd_FactorCosto; }
            set { _Cd_FactorCosto = value; }
        }

        #endregion


        #region Propiedades de Datos calculados

        private int _consecutivoValProyCD;
        public int ConsecutivoValProyCD
        {
            get { return _consecutivoValProyCD; }
            set { _consecutivoValProyCD = value; }
        }
        private int _cantidadPedidosCD;
        public int CantidadPedidosCD
        {
            get { return _cantidadPedidosCD; }
            set { _cantidadPedidosCD = value; }
        }
        private int _cantidadRemisionesCD;
        public int CantidadRemisionesCD
        {
            get { return _cantidadRemisionesCD; }
            set { _cantidadRemisionesCD = value; }
        }
        private int _cantidadEntradasCD;
        public int CantidadEntradasCD
        {
            get { return _cantidadEntradasCD; }
            set { _cantidadEntradasCD = value; }
        }
        private int _cantidadSalidasCD;
        public int CantidadSalidasCD
        {
            get { return _cantidadSalidasCD; }
            set { _cantidadSalidasCD = value; }
        }
        private int _cantidadDevolucionesCD;
        public int CantidadDevolucionesCD
        {
            get { return _cantidadDevolucionesCD; }
            set { _cantidadDevolucionesCD = value; }
        }
        private int _cantidadContratosComCD;
        public int CantidadContratosComCD
        {
            get { return _cantidadContratosComCD; }
            set { _cantidadContratosComCD = value; }
        }
        private int _cantidadEmbarquesCD;
        public int CantidadEmbarquesCD
        {
            get { return _cantidadEmbarquesCD; }
            set { _cantidadEmbarquesCD = value; }
        }
        private int _cantidadNotaCargoCD;
        public int CantidadNotaCargoCD
        {
            get { return _cantidadNotaCargoCD; }
            set { _cantidadNotaCargoCD = value; }
        }
        private int _cantidadNotaCreditoCD;
        public int CantidadNotaCreditoCD
        {
            get { return _cantidadNotaCreditoCD; }
            set { _cantidadNotaCreditoCD = value; }
        }
        private int _cantidadPagosCD;
        public int CantidadPagosCD
        {
            get { return _cantidadPagosCD; }
            set { _cantidadPagosCD = value; }
        }
        private int _cantidadOrdenesCompraCD;
        public int CantidadOrdenesCompraCD
        {
            get { return _cantidadOrdenesCompraCD; }
            set { _cantidadOrdenesCompraCD = value; }
        }
        private int _CantidadReclamacionesCD;
        public int CantidadReclamacionesCD
        {
            get { return _CantidadReclamacionesCD; }
            set { _CantidadReclamacionesCD = value; }
        }

        #endregion


        #region Propiedades de Tabla Cobranza
        private int _ID_Emp;
        public int ID_Emp
        {
            get { return _ID_Emp; }
            set { _ID_Emp = value; }
        }
        private int _ID_Cd;
        public int ID_Cd
        {
            get { return _ID_Cd; }
            set { _ID_Cd = value; }
        }
        private int _Id_Cob;
        public int Id_Cob
        {
            get { return _Id_Cob; }
            set { _Id_Cob = value; }
        }
        private int _Cob_DiaInicio;
        public int Cob_DiaInicio
        {
            get { return _Cob_DiaInicio; }
            set { _Cob_DiaInicio = value; }
        }
        private int _Cob_DiaLimite;
        public int Cob_DiaLimite
        {
            get { return _Cob_DiaLimite; }
            set { _Cob_DiaLimite = value; }
        }
        private double _Cob_Multiplicador;
        public double Cob_Multiplicador
        {
            get { return _Cob_Multiplicador; }
            set { _Cob_Multiplicador = value; }
        }
        #endregion

        #region Propiedades de Tabla Rentabilidad
        private int _ID_Emp1;
        public int ID_Emp1
        {
            get { return _ID_Emp1; }
            set { _ID_Emp1 = value; }
        }
        private int _ID_Cd1;
        public int ID_Cd1
        {
            get { return _ID_Cd1; }
            set { _ID_Cd1 = value; }
        }
        private int _Id_Rent;
        public int Id_Rent
        {
            get { return _Id_Rent; }
            set { _Id_Rent = value; }
        }
        private double _Rent_LInferior;
        public double Rent_LInferior
        {
            get { return _Rent_LInferior; }
            set { _Rent_LInferior = value; }
        }
        private double _Rent_LSuperior;
        public double Rent_LSuperior
        {
            get { return _Rent_LSuperior; }
            set { _Rent_LSuperior = value; }
        }
        private double _Rent_Multiplicador;
        private int? _Id_U;
        private double? _Cd_CreditoKey;

        public double? Cd_CreditoKey
        {
            get { return _Cd_CreditoKey; }
            set { _Cd_CreditoKey = value; }
        }
        private double? _Cd_CreditoPapel;
        public bool Generico;


        public double? Cd_CreditoPapel
        {
            get { return _Cd_CreditoPapel; }
            set { _Cd_CreditoPapel = value; }
        }

        public int? Id_U
        {
            get { return _Id_U; }
            set { _Id_U = value; }
        }
        public double Rent_Multiplicador
        {
            get { return _Rent_Multiplicador; }
            set { _Rent_Multiplicador = value; }
        }


        #endregion

        #region Propiedades Comisiones

        public bool CD_NuevoEsquemaCom { get; set; }
        public Double? CD_Gasto { get; set; }

        #endregion


        public double Cd_FactorCostoFinanciero;
        public double Cd_MargenDiferenciaDocs { get; set; }

        //Edsg 28062017
        public Boolean CD_PermiteTerrMismaUEN { get; set; }
    }
}
