using System;

namespace CapaEntidad
{
    [Serializable]
    public class Acys
    {
        private int _Id_Emp;
        private int _Id_Cd;
        private int _Id_Ter;
        private int _Id_Cte;
        private int _Id_Rik;
        private int _Id_Acs;
        private int _Id_AcsVersion;

        private string _Acs_Estatus;
        private string _Cte_Nombre;
        private DateTime _Acs_Fecha;
        private DateTime _Acs_FechaInicioDocumento;
        private DateTime _Acs_FechaFinDocumento;
        private string _Acs_EstatusStr;
        private DateTime? _Filtro_FecIni;
        private DateTime? _Filtro_FecFin;
        private string _Filtro_Estatus;
        private string _Filtro_FolIni;
        private string _Filtro_FolFin;
        private string _Filtro_usuario;
        private int _id_Rik;
        private string _Acs_Contacto;
        private string _Acs_Puesto;
        private int _Acs_Telefono;
        private string _Acs_Correo;
        private string _Acs_Contacto2;
        private int _Acs_Telefono2;
        private string _Acs_Correo2;
        private string _Acs_Contacto3;
        private int _Acs_Telefono3;
        private string _Acs_Correo3;
        private string _Acs_Contacto4;
        private int _Acs_Telefono4;
        private string _Acs_Correo4;
        private string _Acs_Contacto5;
        private string _ClienteDireccion;
        private string _ClienteColonia;
        private string _ClienteMunicipio;
        private string _ClienteEstado;
        private string _ClienteRFC;
        private string _ClienteCodPost;
        private bool _CuentaCorporativa;
        private bool _AddendaSI;
        private string _FechaInicioVersion;
        private string _FechaFinVersion;
        private int _Acs_Version;
        private string _DireccionEntrega;
        private string _ClienteColoniaE;
        private string _ClienteMunicipioE;
        private string _ClienteEstadoE;
        private string _ClienteCPE;
        private string _Acs_Territorio;

        private string _Acs_PedidoEncargadoEnviar;
        private string _Acs_PedidoPuesto;
        private string _Acs_PedidoTelefono;
        private string _Acs_PedidoEmail;


        private bool _Acs_RecDocReposicion;
        private bool _Acs_RecDocFolio;
        private string _Acs_RecDocOtro;

        private bool _Cte_Contado;
        private bool Cte_TarjetaDebito;
        private bool CteTarjetaCredito;
        private bool CteDeposito;

        private string _Acs_VisitaOtro;
        private bool _Acs_ReqServAsesoria;
        private bool _Acs_ReqServTecnicoRelleno;
        private bool _Acs_ReqServMantenimiento;

        private string _Acs_Notas;

        private int _Acs_ContactoRepVenta;
        private string _Acs_ContactoRepVentaTel;
        private string _Acs_ContactoRepVentaEmail;

        private int _Acs_ContactoRepServ;
        private string _Acs_ContactoRepServTel;
        private string _Acs_ContactoRepServEmail;

        private int _Acs_ContactoJefServ;
        private string _Acs_ContactoJefServTel;
        private string _Acs_ContactoJefServEmail;

        private int _Acs_ContactoAseServ;
        private string _Acs_ContactoAseServTel;
        private string _Acs_ContactoAseServEmail;


        private int _Acs_ContactoJefOper;
        private string _Acs_ContactoJefOperTel;
        private string _Acs_ContactoJefOperEmail;

        private int _Acs_ContactoCAlmRep;
        private string _Acs_ContactoCAlmRepTel;
        private string _Acs_ContactoCAlmRepEmail;

        private int _Acs_ContactoCServTec;
        private string _Acs_ContactoCServTecTel;
        private string _Acs_ContactoCServTecEmail;

        private int _Acs_ContactoCCreCob;
        private string _Acs_ContactoCCreCobTel;
        private string _Acs_ContactoCCreCobEmail;

        private string _Acs_Unique;
        private int _Acs_Solicitar;
        private int _Acs_Sustituye;
        private string _Acs_Vencido;
        private string _Acs_Modalidad;

        private string _Acs_Sucursal;

        private int _Acs_ParcialidadesSi;
        private int _Acs_ParcialidadesNo;
        private int _Acs_ConfirmacionPedidosSI;
        private int _Acs_ConfirmacionPedidosnO;
        private int _Acs_chkRecRevLunes;
        private int _Acs_RecRevMartes;
        private int _Acs_RecRevMiercoles;
        private int _Acs_RecRevJueves;
        private int _Acs_RecRevViernes;
        private int _Acs_RecRevSabado;
        private string _Acs_TimePicker1;
        private string _Acs_TimePicker2;
        private string _Acs_TimePicker3;
        private string _Acs_TimePicker4;
        private string _Acs_RecPersonaRecibe;
        private string _Acs_RecPuesto;
        private int _Acs_RecCitaMismoDia;
        private int _Acs_RecCitaSinCita;
        private int _Acs_RecCitaPrevia;
        private string _Acs_RecCitaContacto;
        private string _Acs_RecCitaTelefono;
        private int _Acs_RecCitaDiasdeAnticipacion;
        private int _Acs_RecAreaPropia;
        private int _Acs_RecAreaPlaza;
        private int _Acs_RecAreaCalle;
        private int _Acs_RecAreaAvTransitada;
        private int _Acs_RecEstCortesia;
        private int _Acs_RecEstCosto;
        private int _Acs_RecEstMonto;
        private int _Acs_RecDocFactFranquiciaEnt;
        private int _Acs_RecDocFactFranquiciaEntCop;
        private int _Acs_RecDocFactFranquiciaRec;
        private int _Acs_RecDocFactFranquiciaRecCop;
        private int _Acs_RecDocFactKeyEnt;
        private int _Acs_RecDocFactKeyEntCop;
        private int _Acs_RecDocFactKeyRec;
        private int _Acs_RecDocFactKeyRecCop;
        private int _Acs_RecDocOrdCompraEnt;
        private int _Acs_RecDocOrdCompraEntCop;
        private int _Acs_RecDocOrdCompraRec;
        private int _Acs_RecDocOrdCompraRecCop;
        private int _Acs_RecDocOrdReposEnt;
        private int _Acs_RecDocOrdReposEntCop;
        private int _Acs_RecDocOrdReposRec;
        private int _Acs_RecDocOrdReposRecCop;
        private int _Acs_RecDocCopPedidoEnt;
        private int _Acs_RecDocCopPedidoEntCop;
        private int _Acs_RecDocCopPedidoRec;
        private int _Acs_RecDocCopPedidoRecCop;
        private int _ACS_RecDocRemisionEnt;
        private int _ACS_RecDocRemisionEntCop;
        private int _ACS_RecDocRemisionRec;
        private int _ACS_RecDocRemisionRecCop;
        private int _ACS_RecDocFolioEnt;
        private int _ACS_RecDocFolioEntCop;
        private int _ACS_RecDocFolioRec;
        private int _ACS_RecDocFolioRecCop;
        private int _ACS_RecDocContraRecEnt;
        private int _ACS_RecDocContraRecEntCop;
        private int _ACS_RecDocContraRecRec;
        private int _ACS_RecDocContraRecRecCop;
        private int _ACS_RecDocEntAlmacenEnt;
        private int _ACS_RecDocEntAlmacenEntCop;
        private int _ACS_RecDocEntAlmacenRec;
        private int _ACS_RecDocEntAlmacenRecCop;
        private int _ACS_RecDocSopServicioEnt;
        private int _ACS_RecDocSopServicioEntCop;
        private int _ACS_RecDocSopServicioRec;
        private int _ACS_RecDocSopServicioRecCop;
        private int _ACS_RecDocNomFirmaEnt;
        private int _ACS_RecDocNomFirmaEntCop;
        private int _ACS_RecDocNomFirmaoRec;
        private int _ACS_RecDocNomFirmaRecCop;
        private int _ACS_RecCitaEnt;
        private int _ACS_RecCitaEntCop;
        private int _ACS_RecCitaRec;
        private int _ACS_RecCitaRecCop;
        private string _ACS_RecOtroRec;




        private int _ACS_chk62Lunes;
        private int _ACS_chk62Martes;
        private int _ACS_chk62Miercoles;
        private int _ACS_chk62Jueves;
        private int _ACS_chk62Viernes;
        private int _ACS_chk62Sabado;
        private string _ACS_RadTimePicker162;
        private string _ACS_RadTimePicker262;
        private string _ACS_RadTimePicker362;
        private string _ACS_RadTimePicker462;
        private string _ACS_txtRecPersonaRecibe62;
        private string _ACS_txtRecPuesto62;
        private int _ACS_Chk62Mismodia;
        private int _ACS_Chk62Sincita;
        private int _ACS_Chk62Previa;
        private string _ACS_txt62CitaContacto;
        private string _ACS_txt62CitaTelefono;
        private int _ACS_txt62CitaDiasdeAnticipacion;
        private int _ACS_chk62AreaPropia;
        private int _ACS_chk62AreaPlaza;
        private int _ACS_chk62AreaCalle;
        private int _ACS_chk62AreaAvTransitada;
        private int _ACS_chk62EstCortesia;
        private int _ACS_chk62EstCosto;
        private int _ACS_txt62EstMonto;
        private string _ACS_txt62ClienteDireccion;
        private string _ACS_txt62ClienteColonia;
        private string _ACS_txt62ClienteMunicipio;
        private string _ACS_txt62ClienteEstado;
        private string _ACS_txt62ClienteCodPost;
        private int _ACS_chk62DocFactFranquiciaEnt;
        private int _ACS_txt62DocFactFranquiciaEntCop;
        private int _ACS_chk62DocFactFranquiciaRec;
        private int _ACS_txt62DocFactFranquiciaRecCop;
        private int _ACS_chk62DocFactKeyEnt;
        private int _ACS_txt62DocFactKeyEntCop;
        private int _ACS_chk62DocFactKeyRec;
        private int _ACS_txt62DocFactKeyRecCop;
        private int _ACS_chk62DocOrdCompraEnt;
        private int _ACS_txt62DocOrdCompraEntCop;
        private int _ACS_chk62DocOrdCompraRec;
        private int _ACS_txt62DocOrdCompraRecCop;
        private int _ACS_chk62DocOrdReposEnt;
        private int _ACS_txt62DocOrdReposEntCop;
        private int _ACS_chk62DocOrdReposRec;
        private int _ACS_txt62DocOrdReposRecCop;
        private int _ACS_chk62DocCopPedidoEnt;
        private int _ACS_txt62DocCopPedidoEntCop;
        private int _ACS_chk62DocCopPedidoRec;
        private int _ACS_txt62DocCopPedidoRecCop;
        private int _ACS_chk62DocRemisionEnt;
        private int _ACS_txt62DocRemisionEntCop;
        private int _ACS_chk62DocRemisionRec;
        private int _ACS_txt62DocRemisionRecCop;
        private int _ACS_chk62DocFolioEnt;
        private int _ACS_txt62DocFolioEntCop;
        private int _ACS_chk62DocFolioRec;
        private int _ACS_txt62DocFolioRecCop;
        private int _ACS_chk62DocContraRecEnt;
        private int _ACS_txt62DocContraRecEntCop;
        private int _ACS_chk62DocContraRecRec;
        private int _ACS_txt62DocContraRecRecCop;
        private int _ACS_chk62DocEntAlmacenEnt;
        private int _ACS_txt62DocEntAlmacenEntCop;
        private int _ACS_chk62DocEntAlmacenRec;
        private int _ACS_txt62DocEntAlmacenRecCop;
        private int _ACS_chk62DocSopServicioEnt;
        private int _ACS_txt62DocSopServicioEntCop;
        private int _ACS_chk62DocSopServicioRec;
        private int _ACS_txt62DocSopServicioRecCop;
        private int _ACS_chk62DocNomFirmaEnt;
        private int _ACS_txt62DocNomFirmaEntCop;
        private int _ACS_chk62DocNomFirmaoRec;
        private int _ACS_txt62DocNomFirmaRecCop;
        private int _ACS_chk62CitaEnt;
        private int _ACS_txt62CitaEntCop;
        private int _ACS_chk62CitaRec;
        private int _ACS_txt62CitaRecCop;
        private int _ACS_chk63Lunes;
        private int _ACS_chk63Martes;
        private int _ACS_chk63Miercoles;
        private int _ACS_chk63Jueves;
        private int _ACS_chk63Viernes;
        private int _ACS_chk63Sabado;
        private string _ACS_Rad63TimePicker163;
        private string _ACS_Rad63TimePicker263;
        private string _ACS_Rad63TimePicker363;
        private string _ACS_Rad63TimePicker463;
        private string _ACS_txtRecPersonaRecibe63;
        private string _ACS_txtRecPuesto63;
        private int _ACS_Chk63Mismodia;
        private int _ACS_Chk63Sincita;
        private int _ACS_Chk63Previa;
        private string _ACS_txt63CitaContacto;
        private string _ACS_txt63CitaTelefono;
        private int _ACS_txt63CitaDiasdeAnticipacion;
        private int _ACS_chk63AreaPropia;
        private int _ACS_chk63AreaPlaza;
        private int _ACS_chk63AreaCalle;
        private int _ACS_chk63AreaAvTransitada;
        private int _ACS_chk63EstCortesia;
        private int _ACS_chk63EstCosto;
        private int _ACS_txt63EstMonto;
        private string _ACS_txt63ClienteDireccion;
        private string _ACS_txt63ClienteColonia;
        private string _ACS_txt63ClienteMunicipio;
        private string _ACS_txt63ClienteEstado;
        private string _ACS_txt63ClienteCodPost;
        private int _ACS_chk63DocFactFranquiciaEnt;
        private int _ACS_txt63DocFactFranquiciaEntCop;
        private int _ACS_chk63DocFactFranquiciaRec;
        private int _ACS_txt63DocFactFranquiciaRecCop;
        private int _ACS_chk63DocFactKeyEnt;
        private int _ACS_txt63DocFactKeyEntCop;
        private int _ACS_chk63DocFactKeyRec;
        private int _ACS_txt63DocFactKeyRecCop;
        private int _ACS_chk63DocOrdCompraEnt;
        private int _ACS_txt63DocOrdCompraEntCop;
        private int _ACS_chk63DocOrdCompraRec;
        private int _ACS_txt63DocOrdCompraRecCop;
        private int _ACS_chk63DocOrdReposEnt;
        private int _ACS_txt63DocOrdReposEntCop;
        private int _ACS_chk63DocOrdReposRec;
        private int _ACS_txt63DocOrdReposRecCop;
        private int _ACS_chk63DocCopPedidoEnt;
        private int _ACS_txt63DocCopPedidoEntCop;
        private int _ACS_chk63DocCopPedidoRec;
        private int _ACS_txt63DocCopPedidoRecCop;
        private int _ACS_chk63DocRemisionEnt;
        private int _ACS_txt63DocRemisionEntCop;
        private int _ACS_chk63DocRemisionRec;
        private int _ACS_txt63DocRemisionRecCop;
        private int _ACS_chk63DocFolioEnt;
        private int _ACS_txt63DocFolioEntCop;
        private int _ACS_chk63DocFolioRec;
        private int _ACS_txt63DocFolioRecCop;
        private int _ACS_chk63DocContraRecEnt;
        private int _ACS_txt63DocContraRecEntCop;
        private int _ACS_chk63DocContraRecRec;
        private int _ACS_txt63DocContraRecRecCop;
        private int _ACS_chk63DocEntAlmacenEnt;
        private int _ACS_txt63DocEntAlmacenEntCop;
        private int _ACS_chk63DocEntAlmacenRec;
        private int _ACS_txt63DocEntAlmacenRecCop;
        private int _ACS_chk63DocSopServicioEnt;
        private int _ACS_txt63DocSopServicioEntCop;
        private int _ACS_chk63DocSopServicioRec;
        private int _ACS_txt63DocSopServicioRecCop;
        private int _ACS_chk63DocNomFirmaEnt;
        private int _ACS_txt63DocNomFirmaEntCop;
        private int _ACS_chk63DocNomFirmaoRec;
        private int _ACS_txt63DocNomFirmaRecCop;
        private int _ACS_chk63CitaEnt;
        private int _ACS_txt63CitaEntCop;
        private int _ACS_chk63CitaRec;
        private int _ACS_txt63CitaRecCop;



        private int _IdCte_DirEntrega;

        public int Id_Modalidad { get; set; }
        public string Acs_Modalidad
        {
            get { return _Acs_Modalidad; }
            set { _Acs_Modalidad = value; }
        }


        public string Acs_Sucursal
        {
            get { return _Acs_Sucursal; }
            set { _Acs_Sucursal = value; }
        }



        public int IdCte_DirEntrega
        {
            get { return _IdCte_DirEntrega; }
            set { _IdCte_DirEntrega = value; }
        }

        public int Id_AcsVersion
        {
            get { return _Id_AcsVersion; }
            set { _Id_AcsVersion = value; }
        }

        public string FechaInicioVersion
        {
            get { return _FechaInicioVersion; }
            set { _FechaInicioVersion = value; }
        }


        public string FechaFinVersion
        {
            get { return _FechaFinVersion; }
            set { _FechaFinVersion = value; }
        }



        public int Acs_Version
        {
            get { return _Acs_Version; }
            set { _Acs_Version = value; }
        }

        public string Acs_Unique
        {
            get { return _Acs_Modalidad; }
            set { _Acs_Modalidad = value; }
        }

        public string Acs_Vencido
        {
            get { return _Acs_Vencido; }
            set { _Acs_Vencido = value; }
        }

        public int Acs_Solicitar
        {
            get { return _Acs_Solicitar; }
            set { _Acs_Solicitar = value; }
        }

        public int Acs_Sustituye
        {
            get { return _Acs_Sustituye; }
            set { _Acs_Sustituye = value; }
        }


        public string Acs_PedidoEncargadoEnviar
        {
            get { return _Acs_PedidoEncargadoEnviar; }
            set { _Acs_PedidoEncargadoEnviar = value; }
        }

        public string Acs_Notas
        {
            get { return _Acs_Notas; }
            set { _Acs_Notas = value; }
        }


        public string Acs_PedidoPuesto
        {
            get { return _Acs_PedidoPuesto; }
            set { _Acs_PedidoPuesto = value; }
        }


        public string Acs_PedidoEmail
        {
            get { return _Acs_PedidoEmail; }
            set { _Acs_PedidoEmail = value; }
        }



        public string Acs_PedidoTelefono
        {
            get { return _Acs_PedidoTelefono; }
            set { _Acs_PedidoTelefono = value; }
        }


        public bool Acs_RecDocReposicion
        {
            get { return _Acs_RecDocReposicion; }
            set { _Acs_RecDocReposicion = value; }
        }


        public bool Acs_RecDocFolio
        {
            get { return _Acs_RecDocFolio; }
            set { _Acs_RecDocFolio = value; }
        }


        public string Acs_RecDocOtro
        {
            get { return _Acs_RecDocOtro; }
            set { _Acs_RecDocOtro = value; }
        }

        private string _Acs_NomComercial;
        public string Acs_NomComercial
        {
            get { return _Acs_NomComercial; }
            set { _Acs_NomComercial = value; }
        }

        public string Acs_VisitaOtro
        {
            get { return _Acs_VisitaOtro; }
            set { _Acs_VisitaOtro = value; }
        }

        public bool Acs_ReqServAsesoria
        {
            get { return _Acs_ReqServAsesoria; }
            set { _Acs_ReqServAsesoria = value; }
        }

        public bool Acs_ReqServTecnicoRelleno
        {
            get { return _Acs_ReqServTecnicoRelleno; }
            set { _Acs_ReqServTecnicoRelleno = value; }
        }

        public bool Acs_ReqServMantenimiento
        {
            get { return _Acs_ReqServMantenimiento; }
            set { _Acs_ReqServMantenimiento = value; }
        }


        public int Acs_ContactoRepVenta
        {
            get { return _Acs_ContactoRepVenta; }
            set { _Acs_ContactoRepVenta = value; }
        }

        public string Acs_ContactoRepVentaTel
        {
            get { return _Acs_ContactoRepVentaTel; }
            set { _Acs_ContactoRepVentaTel = value; }
        }

        public string Acs_ContactoRepVentaEmail
        {
            get { return _Acs_ContactoRepVentaEmail; }
            set { _Acs_ContactoRepVentaEmail = value; }
        }

        public int Acs_ContactoRepServ
        {
            get { return _Acs_ContactoRepServ; }
            set { _Acs_ContactoRepServ = value; }
        }

        public string Acs_ContactoRepServTel
        {
            get { return _Acs_ContactoRepServTel; }
            set { _Acs_ContactoRepServTel = value; }
        }

        public string Acs_ContactoRepServEmail
        {
            get { return _Acs_ContactoRepServEmail; }
            set { _Acs_ContactoRepServEmail = value; }
        }


        public int Acs_ContactoJefServ
        {
            get { return _Acs_ContactoJefServ; }
            set { _Acs_ContactoJefServ = value; }
        }

        public string Acs_ContactoJefServTel
        {
            get { return _Acs_ContactoJefServTel; }
            set { _Acs_ContactoJefServTel = value; }
        }

        public string Acs_ContactoJefServEmail
        {
            get { return _Acs_ContactoJefServEmail; }
            set { _Acs_ContactoJefServEmail = value; }
        }

        public int Acs_ContactoAseServ
        {
            get { return _Acs_ContactoAseServ; }
            set { _Acs_ContactoAseServ = value; }
        }

        public string Acs_ContactoAseServTel
        {
            get { return _Acs_ContactoAseServTel; }
            set { _Acs_ContactoAseServTel = value; }
        }

        public string Acs_ContactoAseServEmail
        {
            get { return _Acs_ContactoAseServEmail; }
            set { _Acs_ContactoAseServEmail = value; }
        }

        public int Acs_ContactoJefOper
        {
            get { return _Acs_ContactoJefOper; }
            set { _Acs_ContactoJefOper = value; }
        }

        public string Acs_ContactoJefOperTel
        {
            get { return _Acs_ContactoJefOperTel; }
            set { _Acs_ContactoJefOperTel = value; }
        }

        public string Acs_ContactoJefOperEmail
        {
            get { return _Acs_ContactoJefOperEmail; }
            set { _Acs_ContactoJefOperEmail = value; }
        }

        public int Acs_ContactoCAlmRep
        {
            get { return _Acs_ContactoCAlmRep; }
            set { _Acs_ContactoCAlmRep = value; }
        }

        public string Acs_ContactoCAlmRepTel
        {
            get { return _Acs_ContactoCAlmRepTel; }
            set { _Acs_ContactoCAlmRepTel = value; }
        }

        public string Acs_ContactoCAlmRepEmail
        {
            get { return _Acs_ContactoCAlmRepEmail; }
            set { _Acs_ContactoCAlmRepEmail = value; }
        }


        public int Acs_ContactoCServTec
        {
            get { return _Acs_ContactoCServTec; }
            set { _Acs_ContactoCServTec = value; }
        }

        public string Acs_ContactoCServTecTel
        {
            get { return _Acs_ContactoCServTecTel; }
            set { _Acs_ContactoCServTecTel = value; }
        }

        public string Acs_ContactoCServTecEmail
        {
            get { return _Acs_ContactoCServTecEmail; }
            set { _Acs_ContactoCServTecEmail = value; }
        }

        public int Acs_ContactoCCreCob
        {
            get { return _Acs_ContactoCCreCob; }
            set { _Acs_ContactoCCreCob = value; }
        }

        public string Acs_ContactoCCreCobTel
        {
            get { return _Acs_ContactoCCreCobTel; }
            set { _Acs_ContactoCCreCobTel = value; }
        }

        public string Acs_ContactoCCreCobEmail
        {
            get { return _Acs_ContactoCCreCobEmail; }
            set { _Acs_ContactoCCreCobEmail = value; }
        }

        public string DireccionEntrega
        {
            get { return _DireccionEntrega; }
            set { _DireccionEntrega = value; }
        }

        public string ClienteColoniaE
        {
            get { return _ClienteColoniaE; }
            set { _ClienteColoniaE = value; }
        }

        public string ClienteMunicipioE
        {
            get { return _ClienteMunicipioE; }
            set { _ClienteMunicipioE = value; }
        }

        public string ClienteEstadoE
        {
            get { return _ClienteEstadoE; }
            set { _ClienteEstadoE = value; }
        }


        public string ClienteCPE
        {
            get { return _ClienteCPE; }
            set { _ClienteCPE = value; }
        }




        public string ClienteDireccion
        {
            get { return _ClienteDireccion; }
            set { _ClienteDireccion = value; }
        }

        public string ClienteColonia
        {
            get { return _ClienteColonia; }
            set { _ClienteColonia = value; }
        }

        public string ClienteMunicipio
        {
            get { return _ClienteMunicipio; }
            set { _ClienteMunicipio = value; }
        }

        public string ClienteEstado
        {
            get { return _ClienteEstado; }
            set { _ClienteEstado = value; }
        }

        public string ClienteRFC
        {
            get { return _ClienteRFC; }
            set { _ClienteRFC = value; }
        }

        public string ClienteCodPost
        {
            get { return _ClienteCodPost; }
            set { _ClienteCodPost = value; }
        }

        public bool CuentaCorporativa
        {
            get { return _CuentaCorporativa; }
            set { _CuentaCorporativa = value; }
        }

        public bool AddendaSI
        {
            get { return _AddendaSI; }
            set { _AddendaSI = value; }
        }

        public string Acs_Contacto5
        {
            get { return _Acs_Contacto5; }
            set { _Acs_Contacto5 = value; }
        }

        private int _Acs_Telefono5;
        private string _Acs_Correo5;
        private string _Acs_Contacto6;
        private int _Acs_Telefono6;

        public int Acs_Telefono6
        {
            get { return _Acs_Telefono6; }
            set { _Acs_Telefono6 = value; }
        }
        private string _Acs_Correo6;
        private string _Acs_Proveedor;
        private int _Acs_RutaEntrega;
        private int _Acs_RutaServicio;
        private bool _Acs_ReqOrdenCompra;
        private DateTime? _Acs_VigenciaIni;
        private int _Acs_Semana;
        private bool _Acs_ReqConfirmacion;
        private bool _Acs_RecPedCorreo;
        private bool _Acs_RecPedFax;
        private bool _Acs_RecPedTel;
        private bool _Acs_RecPedRep;
        private bool _Acs_RecPedOtro;
        private string _Acs_RecPedOtroStr;
        private int _Id_U;


        public bool Acs_RecPedRep
        {
            get { return _Acs_RecPedRep; }
            set { _Acs_RecPedRep = value; }
        }
        public int Id_U
        {
            get { return _Id_U; }
            set { _Id_U = value; }
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
        public int Id_Ter
        {
            get { return _Id_Ter; }
            set { _Id_Ter = value; }
        }
        public int Id_Cte
        {
            get { return _Id_Cte; }
            set { _Id_Cte = value; }
        }
        public int Id_Rik
        {
            get { return _Id_Rik; }
            set { _Id_Rik = value; }
        }
        public int Id_Acs
        {
            get { return _Id_Acs; }
            set { _Id_Acs = value; }
        }
        public string Acs_Estatus
        {
            get { return _Acs_Estatus; }
            set { _Acs_Estatus = value; }
        }
        public string Cte_Nombre
        {
            get { return _Cte_Nombre; }
            set { _Cte_Nombre = value; }
        }
        public DateTime Acs_Fecha
        {
            get { return _Acs_Fecha; }
            set { _Acs_Fecha = value; }
        }

        public DateTime Acs_FechaInicioDocumento
        {
            get { return _Acs_FechaInicioDocumento; }
            set { _Acs_FechaInicioDocumento = value; }
        }

        public DateTime Acs_FechaFinDocumento
        {
            get { return _Acs_FechaFinDocumento; }
            set { _Acs_FechaFinDocumento = value; }
        }

        public string Acs_Territorio
        {
            get { return _Acs_Territorio; }
            set { _Acs_Territorio = value; }
        }

        public string Acs_EstatusStr
        {
            get { return _Acs_EstatusStr; }
            set { _Acs_EstatusStr = value; }
        }
        public DateTime? Filtro_FecIni
        {
            get { return _Filtro_FecIni; }
            set { _Filtro_FecIni = value; }
        }
        public DateTime? Filtro_FecFin
        {
            get { return _Filtro_FecFin; }
            set { _Filtro_FecFin = value; }
        }
        public string Filtro_Estatus
        {
            get { return _Filtro_Estatus; }
            set { _Filtro_Estatus = value; }
        }
        public string Filtro_FolIni
        {
            get { return _Filtro_FolIni; }
            set { _Filtro_FolIni = value; }
        }
        public string Filtro_FolFin
        {
            get { return _Filtro_FolFin; }
            set { _Filtro_FolFin = value; }
        }
        public string Filtro_usuario
        {
            get { return _Filtro_usuario; }
            set { _Filtro_usuario = value; }
        }
        public int Id_Rik1
        {
            get { return _id_Rik; }
            set { _id_Rik = value; }
        }
        public string Acs_Contacto
        {
            get { return _Acs_Contacto; }
            set { _Acs_Contacto = value; }
        }
        public string Acs_Puesto
        {
            get { return _Acs_Puesto; }
            set { _Acs_Puesto = value; }
        }
        public int Acs_Telefono
        {
            get { return _Acs_Telefono; }
            set { _Acs_Telefono = value; }
        }
        public string Acs_Correo
        {
            get { return _Acs_Correo; }
            set { _Acs_Correo = value; }
        }
        public string Acs_Contacto2
        {
            get { return _Acs_Contacto2; }
            set { _Acs_Contacto2 = value; }
        }
        public int Acs_Telefono2
        {
            get { return _Acs_Telefono2; }
            set { _Acs_Telefono2 = value; }
        }
        public string Acs_Correo2
        {
            get { return _Acs_Correo2; }
            set { _Acs_Correo2 = value; }
        }
        public string Acs_Contacto3
        {
            get { return _Acs_Contacto3; }
            set { _Acs_Contacto3 = value; }
        }
        public int Acs_Telefono3
        {
            get { return _Acs_Telefono3; }
            set { _Acs_Telefono3 = value; }
        }
        public string Acs_Correo31
        {
            get { return _Acs_Correo3; }
            set { _Acs_Correo3 = value; }
        }
        public string Acs_Correo3
        {
            get { return Acs_Correo31; }
            set { Acs_Correo31 = value; }
        }
        public string Acs_Contacto4
        {
            get { return _Acs_Contacto4; }
            set { _Acs_Contacto4 = value; }
        }
        public int Acs_Telefono4
        {
            get { return _Acs_Telefono4; }
            set { _Acs_Telefono4 = value; }
        }
        public string Acs_Correo4
        {
            get { return _Acs_Correo4; }
            set { _Acs_Correo4 = value; }
        }
        public int Acs_Telefono5
        {
            get { return _Acs_Telefono5; }
            set { _Acs_Telefono5 = value; }
        }
        public string Acs_Correo5
        {
            get { return _Acs_Correo5; }
            set { _Acs_Correo5 = value; }
        }
        public string Acs_Contacto6
        {
            get { return _Acs_Contacto6; }
            set { _Acs_Contacto6 = value; }
        }
        public string Acs_Correo6
        {
            get { return _Acs_Correo6; }
            set { _Acs_Correo6 = value; }
        }
        public string Acs_Proveedor
        {
            get { return _Acs_Proveedor; }
            set { _Acs_Proveedor = value; }
        }
        public int Acs_RutaEntrega
        {
            get { return _Acs_RutaEntrega; }
            set { _Acs_RutaEntrega = value; }
        }
        public int Acs_RutaServicio
        {
            get { return _Acs_RutaServicio; }
            set { _Acs_RutaServicio = value; }
        }
        public bool Acs_ReqOrdenCompra
        {
            get { return _Acs_ReqOrdenCompra; }
            set { _Acs_ReqOrdenCompra = value; }
        }
        public DateTime? Acs_VigenciaIni
        {
            get { return _Acs_VigenciaIni; }
            set { _Acs_VigenciaIni = value; }
        }
        public int Acs_Semana
        {
            get { return _Acs_Semana; }
            set { _Acs_Semana = value; }
        }
        public bool Acs_ReqConfirmacion
        {
            get { return _Acs_ReqConfirmacion; }
            set { _Acs_ReqConfirmacion = value; }
        }
        public bool Acs_RecPedCorreo
        {
            get { return _Acs_RecPedCorreo; }
            set { _Acs_RecPedCorreo = value; }
        }
        public bool Acs_RecPedFax
        {
            get { return _Acs_RecPedFax; }
            set { _Acs_RecPedFax = value; }
        }
        public bool Acs_RecPedTel
        {
            get { return _Acs_RecPedTel; }
            set { _Acs_RecPedTel = value; }
        }
        public bool Acs_RecPedOtro
        {
            get { return _Acs_RecPedOtro; }
            set { _Acs_RecPedOtro = value; }
        }
        public string Acs_RecPedOtroStr
        {
            get { return _Acs_RecPedOtroStr; }
            set { _Acs_RecPedOtroStr = value; }
        }

        private string _Acs_RikNombre;
        public string Acs_RikNombre
        {
            get { return _Acs_RikNombre; }
            set { _Acs_RikNombre = value; }
        }

        private string _Acs_RscNombre;
        public string Acs_RscNombre
        {
            get { return _Acs_RscNombre; }
            set { _Acs_RscNombre = value; }
        }

        private int _Acs_RscIdTerr;
        public int Acs_RscIdTerr
        {
            get { return _Acs_RscIdTerr; }
            set { _Acs_RscIdTerr = value; }
        }

        private string _Acs_RscTerritorio;
        public string Acs_RscTerritorio
        {
            get { return _Acs_RscTerritorio; }
            set { _Acs_RscTerritorio = value; }
        }


        //VISITA
        private double? _Vis_Frecuencia;

        public double? Vis_Frecuencia
        {
            get { return _Vis_Frecuencia; }
            set { _Vis_Frecuencia = value; }
        }
        private bool _Vis_Lunes;

        public bool Vis_Lunes
        {
            get { return _Vis_Lunes; }
            set { _Vis_Lunes = value; }
        }
        private bool _Vis_Martes;

        public bool Vis_Martes
        {
            get { return _Vis_Martes; }
            set { _Vis_Martes = value; }
        }
        private bool _Vis_Miercoles;

        public bool Vis_Miercoles
        {
            get { return _Vis_Miercoles; }
            set { _Vis_Miercoles = value; }
        }
        private bool _Vis_Jueves;

        public bool Vis_Jueves
        {
            get { return _Vis_Jueves; }
            set { _Vis_Jueves = value; }
        }
        private bool _Vis_Viernes;

        public bool Vis_Viernes
        {
            get { return _Vis_Viernes; }
            set { _Vis_Viernes = value; }
        }
        private bool _Vis_Sabado;

        public bool Vis_Sabado
        {
            get { return _Vis_Sabado; }
            set { _Vis_Sabado = value; }
        }
        private string _Vis_HrAm1;

        public string Vis_HrAm1
        {
            get { return _Vis_HrAm1; }
            set { _Vis_HrAm1 = value; }
        }
        private string _Vis_HrAm2;

        public string Vis_HrAm2
        {
            get { return _Vis_HrAm2; }
            set { _Vis_HrAm2 = value; }
        }
        private string _Vis_HrPm1;

        public string Vis_HrPm1
        {
            get { return _Vis_HrPm1; }
            set { _Vis_HrPm1 = value; }
        }
        private string _Vis_HrPm2;
        public string Rec_Semanas;
        public bool Rec_Lunes;
        public bool Rec_Martes;
        public bool Rec_Miercoles;
        public bool Rec_Jueves;
        public bool Rec_Viernes;
        public bool Rec_Sabado;
        public bool Rec_Confirmacion;
        public bool Rec_Correo;
        public bool Rec_Fax;
        public bool Rec_Telefono;
        public bool Rec_Representante;
        public bool Rec_Otro;
        public string Rec_OtroStr;


        public string Vis_HrPm2
        {
            get { return _Vis_HrPm2; }
            set { _Vis_HrPm2 = value; }
        }

        private double _VentaMes;

        public double VentaMes
        {
            get { return _VentaMes; }
            set { _VentaMes = value; }
        }

        private double _VentaInst;

        public double VentaInst
        {
            get { return _VentaInst; }
            set { _VentaInst = value; }
        }

        private double _VentaProm;

        public double VentaProm
        {
            get { return _VentaProm; }
            set { _VentaProm = value; }
        }


        public int Acs_ParcialidadesSi { get { return _Acs_ParcialidadesSi; } set { _Acs_ParcialidadesSi = value; } }
        public int Acs_ParcialidadesNo { get { return _Acs_ParcialidadesNo; } set { _Acs_ParcialidadesNo = value; } }
        public int Acs_ConfirmacionPedidosSI { get { return _Acs_ConfirmacionPedidosSI; } set { _Acs_ConfirmacionPedidosSI = value; } }
        public int Acs_ConfirmacionPedidosnO { get { return _Acs_ConfirmacionPedidosnO; } set { _Acs_ConfirmacionPedidosnO = value; } }
        public int Acs_chkRecRevLunes { get { return _Acs_chkRecRevLunes; } set { _Acs_chkRecRevLunes = value; } }
        public int Acs_RecRevMartes { get { return _Acs_RecRevMartes; } set { _Acs_RecRevMartes = value; } }
        public int Acs_RecRevMiercoles { get { return _Acs_RecRevMiercoles; } set { _Acs_RecRevMiercoles = value; } }
        public int Acs_RecRevJueves { get { return _Acs_RecRevJueves; } set { _Acs_RecRevJueves = value; } }
        public int Acs_RecRevViernes { get { return _Acs_RecRevViernes; } set { _Acs_RecRevViernes = value; } }
        public int Acs_RecRevSabado { get { return _Acs_RecRevSabado; } set { _Acs_RecRevSabado = value; } }
        public string Acs_TimePicker1 { get { return _Acs_TimePicker1; } set { _Acs_TimePicker1 = value; } }
        public string Acs_TimePicker2 { get { return _Acs_TimePicker2; } set { _Acs_TimePicker2 = value; } }
        public string Acs_TimePicker3 { get { return _Acs_TimePicker3; } set { _Acs_TimePicker3 = value; } }
        public string Acs_TimePicker4 { get { return _Acs_TimePicker4; } set { _Acs_TimePicker4 = value; } }
        public string Acs_RecPersonaRecibe { get { return _Acs_RecPersonaRecibe; } set { _Acs_RecPersonaRecibe = value; } }
        public string Acs_RecPuesto { get { return _Acs_RecPuesto; } set { _Acs_RecPuesto = value; } }
        public int Acs_RecCitaMismoDia { get { return _Acs_RecCitaMismoDia; } set { _Acs_RecCitaMismoDia = value; } }
        public int Acs_RecCitaSinCita { get { return _Acs_RecCitaSinCita; } set { _Acs_RecCitaSinCita = value; } }
        public int Acs_RecCitaPrevia { get { return _Acs_RecCitaPrevia; } set { _Acs_RecCitaPrevia = value; } }
        public string Acs_RecCitaContacto { get { return _Acs_RecCitaContacto; } set { _Acs_RecCitaContacto = value; } }
        public string Acs_RecCitaTelefono { get { return _Acs_RecCitaTelefono; } set { _Acs_RecCitaTelefono = value; } }
        public int Acs_RecCitaDiasdeAnticipacion { get { return _Acs_RecCitaDiasdeAnticipacion; } set { _Acs_RecCitaDiasdeAnticipacion = value; } }
        public int Acs_RecAreaPropia { get { return _Acs_RecAreaPropia; } set { _Acs_RecAreaPropia = value; } }
        public int Acs_RecAreaPlaza { get { return _Acs_RecAreaPlaza; } set { _Acs_RecAreaPlaza = value; } }
        public int Acs_RecAreaCalle { get { return _Acs_RecAreaCalle; } set { _Acs_RecAreaCalle = value; } }
        public int Acs_RecAreaAvTransitada { get { return _Acs_RecAreaAvTransitada; } set { _Acs_RecAreaAvTransitada = value; } }
        public int Acs_RecEstCortesia { get { return _Acs_RecEstCortesia; } set { _Acs_RecEstCortesia = value; } }
        public int Acs_RecEstCosto { get { return _Acs_RecEstCosto; } set { _Acs_RecEstCosto = value; } }
        public int Acs_RecEstMonto { get { return _Acs_RecEstMonto; } set { _Acs_RecEstMonto = value; } }
        public int Acs_RecDocFactFranquiciaEnt { get { return _Acs_RecDocFactFranquiciaEnt; } set { _Acs_RecDocFactFranquiciaEnt = value; } }
        public int Acs_RecDocFactFranquiciaEntCop { get { return _Acs_RecDocFactFranquiciaEntCop; } set { _Acs_RecDocFactFranquiciaEntCop = value; } }
        public int Acs_RecDocFactFranquiciaRec { get { return _Acs_RecDocFactFranquiciaRec; } set { _Acs_RecDocFactFranquiciaRec = value; } }
        public int Acs_RecDocFactFranquiciaRecCop { get { return _Acs_RecDocFactFranquiciaRecCop; } set { _Acs_RecDocFactFranquiciaRecCop = value; } }
        public int Acs_RecDocFactKeyEnt { get { return _Acs_RecDocFactKeyEnt; } set { _Acs_RecDocFactKeyEnt = value; } }
        public int Acs_RecDocFactKeyEntCop { get { return _Acs_RecDocFactKeyEntCop; } set { _Acs_RecDocFactKeyEntCop = value; } }
        public int Acs_RecDocFactKeyRec { get { return _Acs_RecDocFactKeyRec; } set { _Acs_RecDocFactKeyRec = value; } }
        public int Acs_RecDocFactKeyRecCop { get { return _Acs_RecDocFactKeyRecCop; } set { _Acs_RecDocFactKeyRecCop = value; } }
        public int Acs_RecDocOrdCompraEnt { get { return _Acs_RecDocOrdCompraEnt; } set { _Acs_RecDocOrdCompraEnt = value; } }
        public int Acs_RecDocOrdCompraEntCop { get { return _Acs_RecDocOrdCompraEntCop; } set { _Acs_RecDocOrdCompraEntCop = value; } }
        public int Acs_RecDocOrdCompraRec { get { return _Acs_RecDocOrdCompraRec; } set { _Acs_RecDocOrdCompraRec = value; } }
        public int Acs_RecDocOrdCompraRecCop { get { return _Acs_RecDocOrdCompraRecCop; } set { _Acs_RecDocOrdCompraRecCop = value; } }
        public int Acs_RecDocOrdReposEnt { get { return _Acs_RecDocOrdReposEnt; } set { _Acs_RecDocOrdReposEnt = value; } }
        public int Acs_RecDocOrdReposEntCop { get { return _Acs_RecDocOrdReposEntCop; } set { _Acs_RecDocOrdReposEntCop = value; } }
        public int Acs_RecDocOrdReposRec { get { return _Acs_RecDocOrdReposRec; } set { _Acs_RecDocOrdReposRec = value; } }
        public int Acs_RecDocOrdReposRecCop { get { return _Acs_RecDocOrdReposRecCop; } set { _Acs_RecDocOrdReposRecCop = value; } }
        public int Acs_RecDocCopPedidoEnt { get { return _Acs_RecDocCopPedidoEnt; } set { _Acs_RecDocCopPedidoEnt = value; } }
        public int Acs_RecDocCopPedidoEntCop { get { return _Acs_RecDocCopPedidoEntCop; } set { _Acs_RecDocCopPedidoEntCop = value; } }
        public int Acs_RecDocCopPedidoRec { get { return _Acs_RecDocCopPedidoRec; } set { _Acs_RecDocCopPedidoRec = value; } }
        public int Acs_RecDocCopPedidoRecCop { get { return _Acs_RecDocCopPedidoRecCop; } set { _Acs_RecDocCopPedidoRecCop = value; } }
        public int ACS_RecDocRemisionEnt { get { return _ACS_RecDocRemisionEnt; } set { _ACS_RecDocRemisionEnt = value; } }
        public int ACS_RecDocRemisionEntCop { get { return _ACS_RecDocRemisionEntCop; } set { _ACS_RecDocRemisionEntCop = value; } }
        public int ACS_RecDocRemisionRec { get { return _ACS_RecDocRemisionRec; } set { _ACS_RecDocRemisionRec = value; } }
        public int ACS_RecDocRemisionRecCop { get { return _ACS_RecDocRemisionRecCop; } set { _ACS_RecDocRemisionRecCop = value; } }
        public int ACS_RecDocFolioEnt { get { return _ACS_RecDocFolioEnt; } set { _ACS_RecDocFolioEnt = value; } }
        public int ACS_RecDocFolioEntCop { get { return _ACS_RecDocFolioEntCop; } set { _ACS_RecDocFolioEntCop = value; } }
        public int ACS_RecDocFolioRec { get { return _ACS_RecDocFolioRec; } set { _ACS_RecDocFolioRec = value; } }
        public int ACS_RecDocFolioRecCop { get { return _ACS_RecDocFolioRecCop; } set { _ACS_RecDocFolioRecCop = value; } }
        public int ACS_RecDocContraRecEnt { get { return _ACS_RecDocContraRecEnt; } set { _ACS_RecDocContraRecEnt = value; } }
        public int ACS_RecDocContraRecEntCop { get { return _ACS_RecDocContraRecEntCop; } set { _ACS_RecDocContraRecEntCop = value; } }
        public int ACS_RecDocContraRecRec { get { return _ACS_RecDocContraRecRec; } set { _ACS_RecDocContraRecRec = value; } }
        public int ACS_RecDocContraRecRecCop { get { return _ACS_RecDocContraRecRecCop; } set { _ACS_RecDocContraRecRecCop = value; } }
        public int ACS_RecDocEntAlmacenEnt { get { return _ACS_RecDocEntAlmacenEnt; } set { _ACS_RecDocEntAlmacenEnt = value; } }
        public int ACS_RecDocEntAlmacenEntCop { get { return _ACS_RecDocEntAlmacenEntCop; } set { _ACS_RecDocEntAlmacenEntCop = value; } }
        public int ACS_RecDocEntAlmacenRec { get { return _ACS_RecDocEntAlmacenRec; } set { _ACS_RecDocEntAlmacenRec = value; } }
        public int ACS_RecDocEntAlmacenRecCop { get { return _ACS_RecDocEntAlmacenRecCop; } set { _ACS_RecDocEntAlmacenRecCop = value; } }
        public int ACS_RecDocSopServicioEnt { get { return _ACS_RecDocSopServicioEnt; } set { _ACS_RecDocSopServicioEnt = value; } }
        public int ACS_RecDocSopServicioEntCop { get { return _ACS_RecDocSopServicioEntCop; } set { _ACS_RecDocSopServicioEntCop = value; } }
        public int ACS_RecDocSopServicioRec { get { return _ACS_RecDocSopServicioRec; } set { _ACS_RecDocSopServicioRec = value; } }
        public int ACS_RecDocSopServicioRecCop { get { return _ACS_RecDocSopServicioRecCop; } set { _ACS_RecDocSopServicioRecCop = value; } }
        public int ACS_RecDocNomFirmaEnt { get { return _ACS_RecDocNomFirmaEnt; } set { _ACS_RecDocNomFirmaEnt = value; } }
        public int ACS_RecDocNomFirmaEntCop { get { return _ACS_RecDocNomFirmaEntCop; } set { _ACS_RecDocNomFirmaEntCop = value; } }
        public int ACS_RecDocNomFirmaoRec { get { return _ACS_RecDocNomFirmaoRec; } set { _ACS_RecDocNomFirmaoRec = value; } }
        public int ACS_RecDocNomFirmaRecCop { get { return _ACS_RecDocNomFirmaRecCop; } set { _ACS_RecDocNomFirmaRecCop = value; } }
        public int ACS_RecCitaEnt { get { return _ACS_RecCitaEnt; } set { _ACS_RecCitaEnt = value; } }
        public int ACS_RecCitaEntCop { get { return _ACS_RecCitaEntCop; } set { _ACS_RecCitaEntCop = value; } }
        public int ACS_RecCitaRec { get { return _ACS_RecCitaRec; } set { _ACS_RecCitaRec = value; } }
        public int ACS_RecCitaRecCop { get { return _ACS_RecCitaRecCop; } set { _ACS_RecCitaRecCop = value; } }
        public string ACS_RecOtroRec { get { return _ACS_RecOtroRec; } set { _ACS_RecOtroRec = value; } }


        public int ACS_chk62Lunes { get { return _ACS_chk62Lunes; } set { _ACS_chk62Lunes = value; } }
        public int ACS_chk62Martes { get { return _ACS_chk62Martes; } set { _ACS_chk62Martes = value; } }
        public int ACS_chk62Miercoles { get { return _ACS_chk62Miercoles; } set { _ACS_chk62Miercoles = value; } }
        public int ACS_chk62Jueves { get { return _ACS_chk62Jueves; } set { _ACS_chk62Jueves = value; } }
        public int ACS_chk62Viernes { get { return _ACS_chk62Viernes; } set { _ACS_chk62Viernes = value; } }
        public int ACS_chk62Sabado { get { return _ACS_chk62Sabado; } set { _ACS_chk62Sabado = value; } }
        public string ACS_RadTimePicker162 { get { return _ACS_RadTimePicker162; } set { _ACS_RadTimePicker162 = value; } }
        public string ACS_RadTimePicker262 { get { return _ACS_RadTimePicker262; } set { _ACS_RadTimePicker262 = value; } }
        public string ACS_RadTimePicker362 { get { return _ACS_RadTimePicker362; } set { _ACS_RadTimePicker362 = value; } }
        public string ACS_RadTimePicker462 { get { return _ACS_RadTimePicker462; } set { _ACS_RadTimePicker462 = value; } }
        public string ACS_txtRecPersonaRecibe62 { get { return _ACS_txtRecPersonaRecibe62; } set { _ACS_txtRecPersonaRecibe62 = value; } }
        public string ACS_txtRecPuesto62 { get { return _ACS_txtRecPuesto62; } set { _ACS_txtRecPuesto62 = value; } }
        public int ACS_Chk62Mismodia { get { return _ACS_Chk62Mismodia; } set { _ACS_Chk62Mismodia = value; } }
        public int ACS_Chk62Sincita { get { return _ACS_Chk62Sincita; } set { _ACS_Chk62Sincita = value; } }
        public int ACS_Chk62Previa { get { return _ACS_Chk62Previa; } set { _ACS_Chk62Previa = value; } }
        public string ACS_txt62CitaContacto { get { return _ACS_txt62CitaContacto; } set { _ACS_txt62CitaContacto = value; } }
        public string ACS_txt62CitaTelefono { get { return _ACS_txt62CitaTelefono; } set { _ACS_txt62CitaTelefono = value; } }
        public int ACS_txt62CitaDiasdeAnticipacion { get { return _ACS_txt62CitaDiasdeAnticipacion; } set { _ACS_txt62CitaDiasdeAnticipacion = value; } }
        public int ACS_chk62AreaPropia { get { return _ACS_chk62AreaPropia; } set { _ACS_chk62AreaPropia = value; } }
        public int ACS_chk62AreaPlaza { get { return _ACS_chk62AreaPlaza; } set { _ACS_chk62AreaPlaza = value; } }
        public int ACS_chk62AreaCalle { get { return _ACS_chk62AreaCalle; } set { _ACS_chk62AreaCalle = value; } }
        public int ACS_chk62AreaAvTransitada { get { return _ACS_chk62AreaAvTransitada; } set { _ACS_chk62AreaAvTransitada = value; } }
        public int ACS_chk62EstCortesia { get { return _ACS_chk62EstCortesia; } set { _ACS_chk62EstCortesia = value; } }
        public int ACS_chk62EstCosto { get { return _ACS_chk62EstCosto; } set { _ACS_chk62EstCosto = value; } }
        public int ACS_txt62EstMonto { get { return _ACS_txt62EstMonto; } set { _ACS_txt62EstMonto = value; } }
        public string ACS_txt62ClienteDireccion { get { return _ACS_txt62ClienteDireccion; } set { _ACS_txt62ClienteDireccion = value; } }
        public string ACS_txt62ClienteColonia { get { return _ACS_txt62ClienteColonia; } set { _ACS_txt62ClienteColonia = value; } }
        public string ACS_txt62ClienteMunicipio { get { return _ACS_txt62ClienteMunicipio; } set { _ACS_txt62ClienteMunicipio = value; } }
        public string ACS_txt62ClienteEstado { get { return _ACS_txt62ClienteEstado; } set { _ACS_txt62ClienteEstado = value; } }
        public string ACS_txt62ClienteCodPost { get { return _ACS_txt62ClienteCodPost; } set { _ACS_txt62ClienteCodPost = value; } }
        public int ACS_chk62DocFactFranquiciaEnt { get { return _ACS_chk62DocFactFranquiciaEnt; } set { _ACS_chk62DocFactFranquiciaEnt = value; } }
        public int ACS_txt62DocFactFranquiciaEntCop { get { return _ACS_txt62DocFactFranquiciaEntCop; } set { _ACS_txt62DocFactFranquiciaEntCop = value; } }
        public int ACS_chk62DocFactFranquiciaRec { get { return _ACS_chk62DocFactFranquiciaRec; } set { _ACS_chk62DocFactFranquiciaRec = value; } }
        public int ACS_txt62DocFactFranquiciaRecCop { get { return _ACS_txt62DocFactFranquiciaRecCop; } set { _ACS_txt62DocFactFranquiciaRecCop = value; } }
        public int ACS_chk62DocFactKeyEnt { get { return _ACS_chk62DocFactKeyEnt; } set { _ACS_chk62DocFactKeyEnt = value; } }
        public int ACS_txt62DocFactKeyEntCop { get { return _ACS_txt62DocFactKeyEntCop; } set { _ACS_txt62DocFactKeyEntCop = value; } }
        public int ACS_chk62DocFactKeyRec { get { return _ACS_chk62DocFactKeyRec; } set { _ACS_chk62DocFactKeyRec = value; } }
        public int ACS_txt62DocFactKeyRecCop { get { return _ACS_txt62DocFactKeyRecCop; } set { _ACS_txt62DocFactKeyRecCop = value; } }
        public int ACS_chk62DocOrdCompraEnt { get { return _ACS_chk62DocOrdCompraEnt; } set { _ACS_chk62DocOrdCompraEnt = value; } }
        public int ACS_txt62DocOrdCompraEntCop { get { return _ACS_txt62DocOrdCompraEntCop; } set { _ACS_txt62DocOrdCompraEntCop = value; } }
        public int ACS_chk62DocOrdCompraRec { get { return _ACS_chk62DocOrdCompraRec; } set { _ACS_chk62DocOrdCompraRec = value; } }
        public int ACS_txt62DocOrdCompraRecCop { get { return _ACS_txt62DocOrdCompraRecCop; } set { _ACS_txt62DocOrdCompraRecCop = value; } }
        public int ACS_chk62DocOrdReposEnt { get { return _ACS_chk62DocOrdReposEnt; } set { _ACS_chk62DocOrdReposEnt = value; } }
        public int ACS_txt62DocOrdReposEntCop { get { return _ACS_txt62DocOrdReposEntCop; } set { _ACS_txt62DocOrdReposEntCop = value; } }
        public int ACS_chk62DocOrdReposRec { get { return _ACS_chk62DocOrdReposRec; } set { _ACS_chk62DocOrdReposRec = value; } }
        public int ACS_txt62DocOrdReposRecCop { get { return _ACS_txt62DocOrdReposRecCop; } set { _ACS_txt62DocOrdReposRecCop = value; } }
        public int ACS_chk62DocCopPedidoEnt { get { return _ACS_chk62DocCopPedidoEnt; } set { _ACS_chk62DocCopPedidoEnt = value; } }
        public int ACS_txt62DocCopPedidoEntCop { get { return _ACS_txt62DocCopPedidoEntCop; } set { _ACS_txt62DocCopPedidoEntCop = value; } }
        public int ACS_chk62DocCopPedidoRec { get { return _ACS_chk62DocCopPedidoRec; } set { _ACS_chk62DocCopPedidoRec = value; } }
        public int ACS_txt62DocCopPedidoRecCop { get { return _ACS_txt62DocCopPedidoRecCop; } set { _ACS_txt62DocCopPedidoRecCop = value; } }
        public int ACS_chk62DocRemisionEnt { get { return _ACS_chk62DocRemisionEnt; } set { _ACS_chk62DocRemisionEnt = value; } }
        public int ACS_txt62DocRemisionEntCop { get { return _ACS_txt62DocRemisionEntCop; } set { _ACS_txt62DocRemisionEntCop = value; } }
        public int ACS_chk62DocRemisionRec { get { return _ACS_chk62DocRemisionRec; } set { _ACS_chk62DocRemisionRec = value; } }
        public int ACS_txt62DocRemisionRecCop { get { return _ACS_txt62DocRemisionRecCop; } set { _ACS_txt62DocRemisionRecCop = value; } }
        public int ACS_chk62DocFolioEnt { get { return _ACS_chk62DocFolioEnt; } set { _ACS_chk62DocFolioEnt = value; } }
        public int ACS_txt62DocFolioEntCop { get { return _ACS_txt62DocFolioEntCop; } set { _ACS_txt62DocFolioEntCop = value; } }
        public int ACS_chk62DocFolioRec { get { return _ACS_chk62DocFolioRec; } set { _ACS_chk62DocFolioRec = value; } }
        public int ACS_txt62DocFolioRecCop { get { return _ACS_txt62DocFolioRecCop; } set { _ACS_txt62DocFolioRecCop = value; } }
        public int ACS_chk62DocContraRecEnt { get { return _ACS_chk62DocContraRecEnt; } set { _ACS_chk62DocContraRecEnt = value; } }
        public int ACS_txt62DocContraRecEntCop { get { return _ACS_txt62DocContraRecEntCop; } set { _ACS_txt62DocContraRecEntCop = value; } }
        public int ACS_chk62DocContraRecRec { get { return _ACS_chk62DocContraRecRec; } set { _ACS_chk62DocContraRecRec = value; } }
        public int ACS_txt62DocContraRecRecCop { get { return _ACS_txt62DocContraRecRecCop; } set { _ACS_txt62DocContraRecRecCop = value; } }
        public int ACS_chk62DocEntAlmacenEnt { get { return _ACS_chk62DocEntAlmacenEnt; } set { _ACS_chk62DocEntAlmacenEnt = value; } }
        public int ACS_txt62DocEntAlmacenEntCop { get { return _ACS_txt62DocEntAlmacenEntCop; } set { _ACS_txt62DocEntAlmacenEntCop = value; } }
        public int ACS_chk62DocEntAlmacenRec { get { return _ACS_chk62DocEntAlmacenRec; } set { _ACS_chk62DocEntAlmacenRec = value; } }
        public int ACS_txt62DocEntAlmacenRecCop { get { return _ACS_txt62DocEntAlmacenRecCop; } set { _ACS_txt62DocEntAlmacenRecCop = value; } }
        public int ACS_chk62DocSopServicioEnt { get { return _ACS_chk62DocSopServicioEnt; } set { _ACS_chk62DocSopServicioEnt = value; } }
        public int ACS_txt62DocSopServicioEntCop { get { return _ACS_txt62DocSopServicioEntCop; } set { _ACS_txt62DocSopServicioEntCop = value; } }
        public int ACS_chk62DocSopServicioRec { get { return _ACS_chk62DocSopServicioRec; } set { _ACS_chk62DocSopServicioRec = value; } }
        public int ACS_txt62DocSopServicioRecCop { get { return _ACS_txt62DocSopServicioRecCop; } set { _ACS_txt62DocSopServicioRecCop = value; } }
        public int ACS_chk62DocNomFirmaEnt { get { return _ACS_chk62DocNomFirmaEnt; } set { _ACS_chk62DocNomFirmaEnt = value; } }
        public int ACS_txt62DocNomFirmaEntCop { get { return _ACS_txt62DocNomFirmaEntCop; } set { _ACS_txt62DocNomFirmaEntCop = value; } }
        public int ACS_chk62DocNomFirmaoRec { get { return _ACS_chk62DocNomFirmaoRec; } set { _ACS_chk62DocNomFirmaoRec = value; } }
        public int ACS_txt62DocNomFirmaRecCop { get { return _ACS_txt62DocNomFirmaRecCop; } set { _ACS_txt62DocNomFirmaRecCop = value; } }
        public int ACS_chk62CitaEnt { get { return _ACS_chk62CitaEnt; } set { _ACS_chk62CitaEnt = value; } }
        public int ACS_txt62CitaEntCop { get { return _ACS_txt62CitaEntCop; } set { _ACS_txt62CitaEntCop = value; } }
        public int ACS_chk62CitaRec { get { return _ACS_chk62CitaRec; } set { _ACS_chk62CitaRec = value; } }
        public int ACS_txt62CitaRecCop { get { return _ACS_txt62CitaRecCop; } set { _ACS_txt62CitaRecCop = value; } }
        public int ACS_chk63Lunes { get { return _ACS_chk63Lunes; } set { _ACS_chk63Lunes = value; } }
        public int ACS_chk63Martes { get { return _ACS_chk63Martes; } set { _ACS_chk63Martes = value; } }
        public int ACS_chk63Miercoles { get { return _ACS_chk63Miercoles; } set { _ACS_chk63Miercoles = value; } }
        public int ACS_chk63Jueves { get { return _ACS_chk63Jueves; } set { _ACS_chk63Jueves = value; } }
        public int ACS_chk63Viernes { get { return _ACS_chk63Viernes; } set { _ACS_chk63Viernes = value; } }
        public int ACS_chk63Sabado { get { return _ACS_chk63Sabado; } set { _ACS_chk63Sabado = value; } }
        public string ACS_Rad63TimePicker163 { get { return _ACS_Rad63TimePicker163; } set { _ACS_Rad63TimePicker163 = value; } }
        public string ACS_Rad63TimePicker263 { get { return _ACS_Rad63TimePicker263; } set { _ACS_Rad63TimePicker263 = value; } }
        public string ACS_Rad63TimePicker363 { get { return _ACS_Rad63TimePicker363; } set { _ACS_Rad63TimePicker363 = value; } }
        public string ACS_Rad63TimePicker463 { get { return _ACS_Rad63TimePicker463; } set { _ACS_Rad63TimePicker463 = value; } }
        public string ACS_txtRecPersonaRecibe63 { get { return _ACS_txtRecPersonaRecibe63; } set { _ACS_txtRecPersonaRecibe63 = value; } }
        public string ACS_txtRecPuesto63 { get { return _ACS_txtRecPuesto63; } set { _ACS_txtRecPuesto63 = value; } }
        public int ACS_Chk63Mismodia { get { return _ACS_Chk63Mismodia; } set { _ACS_Chk63Mismodia = value; } }
        public int ACS_Chk63Sincita { get { return _ACS_Chk63Sincita; } set { _ACS_Chk63Sincita = value; } }
        public int ACS_Chk63Previa { get { return _ACS_Chk63Previa; } set { _ACS_Chk63Previa = value; } }
        public string ACS_txt63CitaContacto { get { return _ACS_txt63CitaContacto; } set { _ACS_txt63CitaContacto = value; } }
        public string ACS_txt63CitaTelefono { get { return _ACS_txt63CitaTelefono; } set { _ACS_txt63CitaTelefono = value; } }
        public int ACS_txt63CitaDiasdeAnticipacion { get { return _ACS_txt63CitaDiasdeAnticipacion; } set { _ACS_txt63CitaDiasdeAnticipacion = value; } }
        public int ACS_chk63AreaPropia { get { return _ACS_chk63AreaPropia; } set { _ACS_chk63AreaPropia = value; } }
        public int ACS_chk63AreaPlaza { get { return _ACS_chk63AreaPlaza; } set { _ACS_chk63AreaPlaza = value; } }
        public int ACS_chk63AreaCalle { get { return _ACS_chk63AreaCalle; } set { _ACS_chk63AreaCalle = value; } }
        public int ACS_chk63AreaAvTransitada { get { return _ACS_chk63AreaAvTransitada; } set { _ACS_chk63AreaAvTransitada = value; } }
        public int ACS_chk63EstCortesia { get { return _ACS_chk63EstCortesia; } set { _ACS_chk63EstCortesia = value; } }
        public int ACS_chk63EstCosto { get { return _ACS_chk63EstCosto; } set { _ACS_chk63EstCosto = value; } }
        public int ACS_txt63EstMonto { get { return _ACS_txt63EstMonto; } set { _ACS_txt63EstMonto = value; } }
        public string ACS_txt63ClienteDireccion { get { return _ACS_txt63ClienteDireccion; } set { _ACS_txt63ClienteDireccion = value; } }
        public string ACS_txt63ClienteColonia { get { return _ACS_txt63ClienteColonia; } set { _ACS_txt63ClienteColonia = value; } }
        public string ACS_txt63ClienteMunicipio { get { return _ACS_txt63ClienteMunicipio; } set { _ACS_txt63ClienteMunicipio = value; } }
        public string ACS_txt63ClienteEstado { get { return _ACS_txt63ClienteEstado; } set { _ACS_txt63ClienteEstado = value; } }
        public string ACS_txt63ClienteCodPost { get { return _ACS_txt63ClienteCodPost; } set { _ACS_txt63ClienteCodPost = value; } }
        public int ACS_chk63DocFactFranquiciaEnt { get { return _ACS_chk63DocFactFranquiciaEnt; } set { _ACS_chk63DocFactFranquiciaEnt = value; } }
        public int ACS_txt63DocFactFranquiciaEntCop { get { return _ACS_txt63DocFactFranquiciaEntCop; } set { _ACS_txt63DocFactFranquiciaEntCop = value; } }
        public int ACS_chk63DocFactFranquiciaRec { get { return _ACS_chk63DocFactFranquiciaRec; } set { _ACS_chk63DocFactFranquiciaRec = value; } }
        public int ACS_txt63DocFactFranquiciaRecCop { get { return _ACS_txt63DocFactFranquiciaRecCop; } set { _ACS_txt63DocFactFranquiciaRecCop = value; } }
        public int ACS_chk63DocFactKeyEnt { get { return _ACS_chk63DocFactKeyEnt; } set { _ACS_chk63DocFactKeyEnt = value; } }
        public int ACS_txt63DocFactKeyEntCop { get { return _ACS_txt63DocFactKeyEntCop; } set { _ACS_txt63DocFactKeyEntCop = value; } }
        public int ACS_chk63DocFactKeyRec { get { return _ACS_chk63DocFactKeyRec; } set { _ACS_chk63DocFactKeyRec = value; } }
        public int ACS_txt63DocFactKeyRecCop { get { return _ACS_txt63DocFactKeyRecCop; } set { _ACS_txt63DocFactKeyRecCop = value; } }
        public int ACS_chk63DocOrdCompraEnt { get { return _ACS_chk63DocOrdCompraEnt; } set { _ACS_chk63DocOrdCompraEnt = value; } }
        public int ACS_txt63DocOrdCompraEntCop { get { return _ACS_txt63DocOrdCompraEntCop; } set { _ACS_txt63DocOrdCompraEntCop = value; } }
        public int ACS_chk63DocOrdCompraRec { get { return _ACS_chk63DocOrdCompraRec; } set { _ACS_chk63DocOrdCompraRec = value; } }
        public int ACS_txt63DocOrdCompraRecCop { get { return _ACS_txt63DocOrdCompraRecCop; } set { _ACS_txt63DocOrdCompraRecCop = value; } }
        public int ACS_chk63DocOrdReposEnt { get { return _ACS_chk63DocOrdReposEnt; } set { _ACS_chk63DocOrdReposEnt = value; } }
        public int ACS_txt63DocOrdReposEntCop { get { return _ACS_txt63DocOrdReposEntCop; } set { _ACS_txt63DocOrdReposEntCop = value; } }
        public int ACS_chk63DocOrdReposRec { get { return _ACS_chk63DocOrdReposRec; } set { _ACS_chk63DocOrdReposRec = value; } }
        public int ACS_txt63DocOrdReposRecCop { get { return _ACS_txt63DocOrdReposRecCop; } set { _ACS_txt63DocOrdReposRecCop = value; } }
        public int ACS_chk63DocCopPedidoEnt { get { return _ACS_chk63DocCopPedidoEnt; } set { _ACS_chk63DocCopPedidoEnt = value; } }
        public int ACS_txt63DocCopPedidoEntCop { get { return _ACS_txt63DocCopPedidoEntCop; } set { _ACS_txt63DocCopPedidoEntCop = value; } }
        public int ACS_chk63DocCopPedidoRec { get { return _ACS_chk63DocCopPedidoRec; } set { _ACS_chk63DocCopPedidoRec = value; } }
        public int ACS_txt63DocCopPedidoRecCop { get { return _ACS_txt63DocCopPedidoRecCop; } set { _ACS_txt63DocCopPedidoRecCop = value; } }
        public int ACS_chk63DocRemisionEnt { get { return _ACS_chk63DocRemisionEnt; } set { _ACS_chk63DocRemisionEnt = value; } }
        public int ACS_txt63DocRemisionEntCop { get { return _ACS_txt63DocRemisionEntCop; } set { _ACS_txt63DocRemisionEntCop = value; } }
        public int ACS_chk63DocRemisionRec { get { return _ACS_chk63DocRemisionRec; } set { _ACS_chk63DocRemisionRec = value; } }
        public int ACS_txt63DocRemisionRecCop { get { return _ACS_txt63DocRemisionRecCop; } set { _ACS_txt63DocRemisionRecCop = value; } }
        public int ACS_chk63DocFolioEnt { get { return _ACS_chk63DocFolioEnt; } set { _ACS_chk63DocFolioEnt = value; } }
        public int ACS_txt63DocFolioEntCop { get { return _ACS_txt63DocFolioEntCop; } set { _ACS_txt63DocFolioEntCop = value; } }
        public int ACS_chk63DocFolioRec { get { return _ACS_chk63DocFolioRec; } set { _ACS_chk63DocFolioRec = value; } }
        public int ACS_txt63DocFolioRecCop { get { return _ACS_txt63DocFolioRecCop; } set { _ACS_txt63DocFolioRecCop = value; } }
        public int ACS_chk63DocContraRecEnt { get { return _ACS_chk63DocContraRecEnt; } set { _ACS_chk63DocContraRecEnt = value; } }
        public int ACS_txt63DocContraRecEntCop { get { return _ACS_txt63DocContraRecEntCop; } set { _ACS_txt63DocContraRecEntCop = value; } }
        public int ACS_chk63DocContraRecRec { get { return _ACS_chk63DocContraRecRec; } set { _ACS_chk63DocContraRecRec = value; } }
        public int ACS_txt63DocContraRecRecCop { get { return _ACS_txt63DocContraRecRecCop; } set { _ACS_txt63DocContraRecRecCop = value; } }
        public int ACS_chk63DocEntAlmacenEnt { get { return _ACS_chk63DocEntAlmacenEnt; } set { _ACS_chk63DocEntAlmacenEnt = value; } }
        public int ACS_txt63DocEntAlmacenEntCop { get { return _ACS_txt63DocEntAlmacenEntCop; } set { _ACS_txt63DocEntAlmacenEntCop = value; } }
        public int ACS_chk63DocEntAlmacenRec { get { return _ACS_chk63DocEntAlmacenRec; } set { _ACS_chk63DocEntAlmacenRec = value; } }
        public int ACS_txt63DocEntAlmacenRecCop { get { return _ACS_txt63DocEntAlmacenRecCop; } set { _ACS_txt63DocEntAlmacenRecCop = value; } }
        public int ACS_chk63DocSopServicioEnt { get { return _ACS_chk63DocSopServicioEnt; } set { _ACS_chk63DocSopServicioEnt = value; } }
        public int ACS_txt63DocSopServicioEntCop { get { return _ACS_txt63DocSopServicioEntCop; } set { _ACS_txt63DocSopServicioEntCop = value; } }
        public int ACS_chk63DocSopServicioRec { get { return _ACS_chk63DocSopServicioRec; } set { _ACS_chk63DocSopServicioRec = value; } }
        public int ACS_txt63DocSopServicioRecCop { get { return _ACS_txt63DocSopServicioRecCop; } set { _ACS_txt63DocSopServicioRecCop = value; } }
        public int ACS_chk63DocNomFirmaEnt { get { return _ACS_chk63DocNomFirmaEnt; } set { _ACS_chk63DocNomFirmaEnt = value; } }
        public int ACS_txt63DocNomFirmaEntCop { get { return _ACS_txt63DocNomFirmaEntCop; } set { _ACS_txt63DocNomFirmaEntCop = value; } }
        public int ACS_chk63DocNomFirmaoRec { get { return _ACS_chk63DocNomFirmaoRec; } set { _ACS_chk63DocNomFirmaoRec = value; } }
        public int ACS_txt63DocNomFirmaRecCop { get { return _ACS_txt63DocNomFirmaRecCop; } set { _ACS_txt63DocNomFirmaRecCop = value; } }
        public int ACS_chk63CitaEnt { get { return _ACS_chk63CitaEnt; } set { _ACS_chk63CitaEnt = value; } }
        public int ACS_txt63CitaEntCop { get { return _ACS_txt63CitaEntCop; } set { _ACS_txt63CitaEntCop = value; } }
        public int ACS_chk63CitaRec { get { return _ACS_chk63CitaRec; } set { _ACS_chk63CitaRec = value; } }
        public int ACS_txt63CitaRecCop { get { return _ACS_txt63CitaRecCop; } set { _ACS_txt63CitaRecCop = value; } }



    }
}
