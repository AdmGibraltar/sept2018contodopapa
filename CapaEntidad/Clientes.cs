
using System;

namespace CapaEntidad
{
    public class Clientes
    {

        int _Id_Emp;
        int _Id_Cd;
        int? _Id_Cte;
        int _Id_Corp;
        int _Id_Rik;
        int _Id_Cfe;
        int _Id_Ade;
        string _Cte_NomComercial;
        string _Cte_NomCorto;
        string _Cte_FacCalle;
        string _Cte_FacNumero;
        string _Cte_FacNumeroInterior;
        string _Cte_FacCp;
        string _Cte_FacColonia;
        string _Cte_FacMunicipio;
        string _Cte_FacTel;
        string _Cte_FacRfc;
        string _Cte_UsoCFDI;
        string _Cte_MetodoPago;
        string _Cte_FacEstado;
        string _Cte_Calle;
        string _Cte_Numero;
        string _Cte_NumeroInterior;
        string _Cte_Cp;
        string _Cte_Colonia;
        string _Cte_Municipio;
        string _Cte_Estado;
        string _Cte_Telefono;
        string _Cte_Fax;
        string _Cte_Contacto;
        int _Cte_Tipo;
        string _Cte_Email;
        bool _Cte_Credito;
        bool _Cte_Contado;
        bool _Cte_EsSucursal;
        bool _Cte_Facturacion;
        string _Cte_DRfc;
        int _Id_Mon;
        double _Cte_LimCobr;
        private string _Cte_RHoraam1;
        private string _Cte_RHoraam2;
        private string _Cte_RHorapm1;
        private string _Cte_RHorapm2;

        bool _Cte_RLunes;
        bool _Cte_RMartes;
        bool _Cte_RMiercoles;
        bool _Cte_RJueves;
        bool _Cte_RViernes;
        bool _Cte_RSabado;
        bool _Cte_RDomingo;
        int _Cte_CondPago;
        bool _Cte_CPLunes;
        bool _Cte_CPMartes;
        bool _Cte_CPMiercoles;
        bool _Cte_CPJueves;
        bool _Cte_CPViernes;
        bool _Cte_CPSabado;
        bool _Cte_CPDomingo;
        bool _Cte_Comisiones;
        bool _Cte_DesgIva;
        bool _Cte_RetIva;
        int _Cte_SerieCre;
        int _Cte_SerieCa;
        int _Cte_AsignacionPed;
        bool _Estatus;
        string _EstatusStr;
        private string _Cte_Documentos;
        private bool _Cte_ReqOrdenCompra;
        private bool _Cte_Transferencia;
        private bool _Cte_Cheque;
        private bool _Cte_Factoraje;
        private bool _Cte_Efectivo;
        private bool _Cte_RecDomingo;
        private bool _Cte_RecSabado;
        private bool _Cte_RecViernes;
        private bool _Cte_RecJueves;
        private bool _Cte_RecMiercoles;
        private bool _Cte_RecMartes;
        private bool _Cte_RecLunes;
        private int _Cte_SemRec;
        private int _Cte_SemRev;
        private int _Cte_SemCob;
        private bool _Cte_CreditoSuspendido;
        private string _Cte_PHoraam1;
        private string _Cte_PHoraam2;
        private string _Cte_PHorapm1;
        private string _Cte_PHorapm2;
        private int _GenInt;
        private bool _GenBool;
        private string _FacSerie;
        private int? _Id_Uen;
        private int? _Id_Seg;
        private int? _Id_Terr;
        private string _Factor;
        private string _Dimension;
        private string _UnidadDimension;
        private double _VPTeorico;
        private double _VPObservado;
        public string Seg_Descripcion;
        public string Uen_Descripcion;
        public string Seg_Unidades;
        public double Seg_ValUniDim;
        public double Cte_Dimension;
        public int Id_Apl;
        public string Ade_Nombre;
        private bool _Ignora_Inactivo;
        private string _Id;
        private string _Cte_TelCobranza1;
        private double? _PorcientoNotaCredito;
        private double? _PorcientoRetencion;
        private int? _PorcientoIVA;
        private bool _BPorcNotaCredito;
        private bool _BPorcientoIVA;       
        private string _Cte_TelCobranza2;
        private string _Descripcion;
        private int _RemisionElectronica;
        private int? _GenInt2;
        private System.Collections.Generic.List<FormaPago> _FormasPago;
        private string _Cte_UDigitos;
        private string _Cte_Referencia;
        public int Id_U;
        public int Cte_DiasVencidos;
        private string _Cte_MotCreditoSuspendido;
        public string Db;
        public string Db_Cobranza;
        public int Id_UCd;
        public int? Id_UPlazo;
        public int? Id_CdPlazo;
        public string Cte_UPlazo;
        public object Cte_AutorizaPlazo_IdU;
        public object Cte_AutorizaPlazo_IdCd;
        public string UPlazo;
        private int _Cte_NumCuentaContNacional;


        private bool _Cte_TarjetaDebito;
        private bool _Cte_TarjetaCredito;
        private bool _Cte_Deposito;
        public int Cte_SemRev2 { get; set; }

        private string _Cte_Portal;
        private string _Cte_ReferenciaTecleada;
        private string _Cte_NumeroCuenta;
        private int _Id_TCte;
        private int _Id_Ban;
        private int _Id_UMod;
        public DateTime Cte_Modfecha { get; set; }
        public string U_Nombre { get; set; }

        public int Id_Ban 
        {
            get { return _Id_Ban; }
            set { _Id_Ban = value; }
        }



        public int Id_UMod
        {
            get { return _Id_UMod; }
            set { _Id_UMod = value; }
        }

        public int Id_TCte
        {
            get { return _Id_TCte; }
            set { _Id_TCte = value; }
        }

        public string Cte_Portal
        {
            get { return _Cte_Portal; }
            set { _Cte_Portal = value; }
        }

        public string Cte_ReferenciaTecleada
        {
            get { return _Cte_ReferenciaTecleada; }
            set { _Cte_ReferenciaTecleada = value; }
        }

        public string Cte_NumeroCuenta
        {
            get { return _Cte_NumeroCuenta; }
            set { _Cte_NumeroCuenta = value; }
        }

        public bool Cte_Deposito
        {
            get { return _Cte_Deposito; }
            set { _Cte_Deposito = value; }
        }

        public bool Cte_EsSucursal
        {
            get { return _Cte_Deposito; }
            set { _Cte_Deposito = value; }
        }

        public bool Cte_TarjetaCredito
        {
            get { return _Cte_TarjetaCredito; }
            set { _Cte_TarjetaCredito = value; }
        }

        public bool Cte_TarjetaDebito
        {
            get { return _Cte_TarjetaDebito; }
            set { _Cte_TarjetaDebito = value; }
        }


        public string Cte_MotCreditoSuspendido
        {
            get { return _Cte_MotCreditoSuspendido; }
            set { _Cte_MotCreditoSuspendido = value; }
        }

        public string Cte_Referencia
        {
            get { return _Cte_Referencia; }
            set { _Cte_Referencia = value; }
        }

        public string Cte_UDigitos
        {
            get { return _Cte_UDigitos; }
            set { _Cte_UDigitos = value; }
        }
        

        public System.Collections.Generic.List<FormaPago> FormasPago
        {
            get { return _FormasPago; }
            set { _FormasPago = value; }
        }

        public int Cte_NumCuentaContNacional
        {
            get { return _Cte_NumCuentaContNacional; }
            set { _Cte_NumCuentaContNacional = value; }
        }

        public int? GenInt2
        {
            get { return _GenInt2; }
            set { _GenInt2 = value; }
        }

        public bool BPorcNotaCredito
        {
            get { return _BPorcNotaCredito; }
            set { _BPorcNotaCredito = value; }
        }

        public double? PorcientoNotaCredito
        {
            get { return _PorcientoNotaCredito; }
            set { _PorcientoNotaCredito = value; }
        }

        public double? PorcientoRetencion
        {
            get { return _PorcientoRetencion; }
            set { _PorcientoRetencion = value; }
        }

        public bool BPorcientoIVA
        {
            get { return _BPorcientoIVA; }
            set { _BPorcientoIVA = value; }
        }

        public int? PorcientoIVA
        {
            get { return _PorcientoIVA; }
            set { _PorcientoIVA = value; }
        }

        public string Cte_TelCobranza1
        {
            get { return _Cte_TelCobranza1; }
            set { _Cte_TelCobranza1 = value; }
        }

        public string Cte_TelCobranza2
        {
            get { return _Cte_TelCobranza2; }
            set { _Cte_TelCobranza2 = value; }
        }

        public string Id
        {
            get { return _Id; }
            set { _Id = value; }
        }

        public string Descripcion
        {
            get { return _Descripcion; }
            set { _Descripcion = value; }
        }

        public bool Ignora_Inactivo
        {
            get { return _Ignora_Inactivo; }
            set { _Ignora_Inactivo = value; }
        }

        public int? Id_Uen
        {
            get { return _Id_Uen; }
            set { _Id_Uen = value; }
        }
        public int? Id_Seg
        {
            get { return _Id_Seg; }
            set { _Id_Seg = value; }
        }
        public int? Id_Terr
        {
            get { return _Id_Terr; }
            set { _Id_Terr = value; }
        }
        public string Factor
        {
            get { return _Factor; }
            set { _Factor = value; }
        }
        public string Dimension
        {
            get { return _Dimension; }
            set { _Dimension = value; }
        }
        public int Id_Corp
        {
            get { return _Id_Corp; }
            set { _Id_Corp = value; }
        }
        public string Cte_NomCorto
        {
            get { return _Cte_NomCorto; }
            set { _Cte_NomCorto = value; }
        }

        public double VPTeorico
        {
            get { return _VPTeorico; }
            set { _VPTeorico = value; }
        }

        public double VPObservado
        {
            get { return _VPObservado; }
            set { _VPObservado = value; }
        }

        public string UnidadDimension
        {
            get { return _UnidadDimension; }
            set { _UnidadDimension = value; }
        }
        public string FacSerie
        {
            get { return _FacSerie; }
            set { _FacSerie = value; }
        }

        public bool GenBool
        {
            get { return _GenBool; }
            set { _GenBool = value; }
        }

        public int GenInt
        {
            get { return _GenInt; }
            set { _GenInt = value; }
        }

        public string Cte_PHoraam1
        {
            get { return _Cte_PHoraam1; }
            set { _Cte_PHoraam1 = value; }
        }

        public string Cte_PHoraam2
        {
            get { return _Cte_PHoraam2; }
            set { _Cte_PHoraam2 = value; }
        }

        public string Cte_PHorapm1
        {
            get { return _Cte_PHorapm1; }
            set { _Cte_PHorapm1 = value; }
        }

        public string Cte_PHorapm2
        {
            get { return _Cte_PHorapm2; }
            set { _Cte_PHorapm2 = value; }
        }

        public string Cte_RHoraam1
        {
            get { return _Cte_RHoraam1; }
            set { _Cte_RHoraam1 = value; }
        }
        public string Cte_RHoraam2
        {
            get { return _Cte_RHoraam2; }
            set { _Cte_RHoraam2 = value; }
        }
        public string Cte_RHorapm1
        {
            get { return _Cte_RHorapm1; }
            set { _Cte_RHorapm1 = value; }
        }
        public string Cte_RHorapm2
        {
            get { return _Cte_RHorapm2; }
            set { _Cte_RHorapm2 = value; }
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

        public int? Id_Cte
        {
            get { return _Id_Cte; }
            set { _Id_Cte = value; }
        }
        public int Id_Rik
        {
            get { return _Id_Rik; }
            set { _Id_Rik = value; }
        }
        public string Cte_NomComercial
        {
            get { return _Cte_NomComercial; }
            set { _Cte_NomComercial = value; }
        }
        public string Cte_FacCalle
        {
            get { return _Cte_FacCalle; }
            set { _Cte_FacCalle = value; }
        }
        public string Cte_FacNumero
        {
            get { return _Cte_FacNumero; }
            set { _Cte_FacNumero = value; }
        }

        public string Cte_FacNumeroInterior
        {
            get { return _Cte_FacNumeroInterior; }
            set { _Cte_FacNumeroInterior = value; }
        }

        public string Cte_FacCp
        {
            get { return _Cte_FacCp; }
            set { _Cte_FacCp = value; }
        }
        public string Cte_FacColonia
        {
            get { return _Cte_FacColonia; }
            set { _Cte_FacColonia = value; }
        }
        public string Cte_FacMunicipio
        {
            get { return _Cte_FacMunicipio; }
            set { _Cte_FacMunicipio = value; }
        }
        public string Cte_FacTel
        {
            get { return _Cte_FacTel; }
            set { _Cte_FacTel = value; }
        } 
        
        public string Cte_FacRfc
        {
            get { return _Cte_FacRfc; }
            set { _Cte_FacRfc = value; }
        }
        public string Cte_UsoCFDI
        {
            get { return _Cte_UsoCFDI; }
            set { _Cte_UsoCFDI = value; }
        }
        public string Cte_MetodoPago
        {
            get { return _Cte_MetodoPago; }
            set { _Cte_MetodoPago = value; }
        }
        public string Cte_FacEstado
        {
            get { return _Cte_FacEstado; }
            set { _Cte_FacEstado = value; }
        }
        public string Cte_Calle
        {
            get { return _Cte_Calle; }
            set { _Cte_Calle = value; }
        }
        public string Cte_Numero
        {
            get { return _Cte_Numero; }
            set { _Cte_Numero = value; }
        }

        public string Cte_NumeroInterior
        {
            get { return _Cte_NumeroInterior; }
            set { _Cte_NumeroInterior = value; }
        }

        public string Cte_Cp
        {
            get { return _Cte_Cp; }
            set { _Cte_Cp = value; }
        }
        public string Cte_Colonia
        {
            get { return _Cte_Colonia; }
            set { _Cte_Colonia = value; }
        }
        public string Cte_Municipio
        {
            get { return _Cte_Municipio; }
            set { _Cte_Municipio = value; }
        }
        public string Cte_Estado
        {
            get { return _Cte_Estado; }
            set { _Cte_Estado = value; }
        }
        public string Cte_Telefono
        {
            get { return _Cte_Telefono; }
            set { _Cte_Telefono = value; }
        }
        public string Cte_Fax
        {
            get { return _Cte_Fax; }
            set { _Cte_Fax = value; }
        }
        public string Cte_Contacto
        {
            get { return _Cte_Contacto; }
            set { _Cte_Contacto = value; }
        }
        public int Cte_Tipo
        {
            get { return _Cte_Tipo; }
            set { _Cte_Tipo = value; }
        }
        public string Cte_Email
        {
            get { return _Cte_Email; }
            set { _Cte_Email = value; }
        }
        public bool Cte_Credito
        {
            get { return _Cte_Credito; }
            set { _Cte_Credito = value; }
        }

        public bool Cte_Contado
        {
            get { return _Cte_Contado; }
            set { _Cte_Contado = value; }
        }

        public bool Cte_Facturacion
        {
            get { return _Cte_Facturacion; }
            set { _Cte_Facturacion = value; }
        }
        public int Id_Mon
        {
            get { return _Id_Mon; }
            set { _Id_Mon = value; }
        }
        public double Cte_LimCobr
        {
            get { return _Cte_LimCobr; }
            set { _Cte_LimCobr = value; }
        }
        public bool Cte_RLunes
        {
            get { return _Cte_RLunes; }
            set { _Cte_RLunes = value; }
        }
        public bool Cte_RMartes
        {
            get { return _Cte_RMartes; }
            set { _Cte_RMartes = value; }
        }
        public bool Cte_RMiercoles
        {
            get { return _Cte_RMiercoles; }
            set { _Cte_RMiercoles = value; }
        }
        public bool Cte_RJueves
        {
            get { return _Cte_RJueves; }
            set { _Cte_RJueves = value; }
        }
        public bool Cte_RViernes
        {
            get { return _Cte_RViernes; }
            set { _Cte_RViernes = value; }
        }
        public bool Cte_RSabado
        {
            get { return _Cte_RSabado; }
            set { _Cte_RSabado = value; }
        }
        public bool Cte_RDomingo
        {
            get { return _Cte_RDomingo; }
            set { _Cte_RDomingo = value; }
        }
        public int Cte_CondPago
        {
            get { return _Cte_CondPago; }
            set { _Cte_CondPago = value; }
        }
        public bool Cte_CPLunes
        {
            get { return _Cte_CPLunes; }
            set { _Cte_CPLunes = value; }
        }
        public bool Cte_CPMartes
        {
            get { return _Cte_CPMartes; }
            set { _Cte_CPMartes = value; }
        }
        public bool Cte_CPMiercoles
        {
            get { return _Cte_CPMiercoles; }
            set { _Cte_CPMiercoles = value; }
        }
        public bool Cte_CPJueves
        {
            get { return _Cte_CPJueves; }
            set { _Cte_CPJueves = value; }
        }
        public bool Cte_CPViernes
        {
            get { return _Cte_CPViernes; }
            set { _Cte_CPViernes = value; }
        }
        public bool Cte_CPSabado
        {
            get { return _Cte_CPSabado; }
            set { _Cte_CPSabado = value; }
        }
        public bool Cte_CPDomingo
        {
            get { return _Cte_CPDomingo; }
            set { _Cte_CPDomingo = value; }
        }
        public bool Cte_Comisiones
        {
            get { return _Cte_Comisiones; }
            set { _Cte_Comisiones = value; }
        }
        public bool Cte_DesgIva
        {
            get { return _Cte_DesgIva; }
            set { _Cte_DesgIva = value; }
        }
        public bool Cte_RetIva
        {
            get { return _Cte_RetIva; }
            set { _Cte_RetIva = value; }
        }
        public int Id_Cfe
        {
            get { return _Id_Cfe; }
            set { _Id_Cfe = value; }
        }
        public int Cte_AsignacionPed
        {
            get { return _Cte_AsignacionPed; }
            set { _Cte_AsignacionPed = value; }
        }
        public int Id_Ade
        {
            get { return _Id_Ade; }
            set { _Id_Ade = value; }
        }
        public bool Estatus
        {
            get { return _Estatus; }
            set { _Estatus = value; }
        }
        public string EstatusStr
        {
            get { return _EstatusStr; }
            set { _EstatusStr = value; }
        }
        public int Cte_SerieNCre
        {
            get { return _Cte_SerieCre; }
            set { _Cte_SerieCre = value; }
        }
        public int Cte_SerieNCa
        {
            get { return _Cte_SerieCa; }
            set { _Cte_SerieCa = value; }
        }
        public string Cte_DRfc
        {
            get { return _Cte_DRfc; }
            set { _Cte_DRfc = value; }
        }
        public string Cte_Documentos
        {
            get { return _Cte_Documentos; }
            set { _Cte_Documentos = value; }
        }
        public bool Cte_ReqOrdenCompra
        {
            get { return _Cte_ReqOrdenCompra; }
            set { _Cte_ReqOrdenCompra = value; }
        }
        public bool Cte_Transferencia
        {
            get { return _Cte_Transferencia; }
            set { _Cte_Transferencia = value; }
        }
        public bool Cte_Cheque
        {
            get { return _Cte_Cheque; }
            set { _Cte_Cheque = value; }
        }
        public bool Cte_Factoraje
        {
            get { return _Cte_Factoraje; }
            set { _Cte_Factoraje = value; }
        }
        public bool Cte_Efectivo
        {
            get { return _Cte_Efectivo; }
            set { _Cte_Efectivo = value; }
        }
        public bool Cte_RecDomingo
        {
            get { return _Cte_RecDomingo; }
            set { _Cte_RecDomingo = value; }
        }
        public bool Cte_RecSabado
        {
            get { return _Cte_RecSabado; }
            set { _Cte_RecSabado = value; }
        }
        public bool Cte_RecViernes
        {
            get { return _Cte_RecViernes; }
            set { _Cte_RecViernes = value; }
        }
        public bool Cte_RecJueves
        {
            get { return _Cte_RecJueves; }
            set { _Cte_RecJueves = value; }
        }
        public bool Cte_RecMiercoles
        {
            get { return _Cte_RecMiercoles; }
            set { _Cte_RecMiercoles = value; }
        }
        public bool Cte_RecMartes
        {
            get { return _Cte_RecMartes; }
            set { _Cte_RecMartes = value; }
        }
        public bool Cte_RecLunes
        {
            get { return _Cte_RecLunes; }
            set { _Cte_RecLunes = value; }
        }
        public int Cte_SemRec
        {
            get { return _Cte_SemRec; }
            set { _Cte_SemRec = value; }
        }

        public int Cte_SemRev
        {
            get { return _Cte_SemRev; }
            set { _Cte_SemRev = value; }
        }

        public int Cte_SemCob
        {
            get { return _Cte_SemCob; }
            set { _Cte_SemCob = value; }
        }

        public bool Cte_CreditoSuspendido
        {
            get { return _Cte_CreditoSuspendido; }
            set { _Cte_CreditoSuspendido = value; }
        }
        public string Ter_Nombre { get; set; }                
        public int Cte_RemisionElectronica
        {
            get { return _RemisionElectronica; }
            set { _RemisionElectronica = value; }
        }



        public string Cte_CorreoEdoCuenta1 { get; set; }

        public string Cte_CorreoEdoCuenta2 { get; set; }

        public string Cte_CorreoEdoCuenta3 { get; set; }

        public string ClienteSIAN { get; set; }

        private string _Cte_PagoUsoCFDI;
        private string _Cte_PagoMetodoPago;

        public string Cte_PagoMetodoPago
        {
            get { return _Cte_PagoMetodoPago; }
            set { _Cte_PagoMetodoPago = value; }
        }

        public string Cte_PagoUsoCFDI
        {
            get { return _Cte_PagoUsoCFDI; }
            set { _Cte_PagoUsoCFDI = value; }
        }
    }
}
