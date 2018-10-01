using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class GastoViaje
    {
        private int _id_Emp;

        public int Id_Emp
        {
            get { return _id_Emp; }
            set { _id_Emp = value; }
        }
        private int _id_Cd;

        public int Id_Cd
        {
            get { return _id_Cd; }
            set { _id_Cd = value; }
        }
        private int _id_GV;

        public int Id_GV
        {
            get { return _id_GV; }
            set { _id_GV = value; }
        }
        private int _id_GVEst;

        public int Id_GVEst
        {
            get { return _id_GVEst; }
            set { _id_GVEst = value; }
        }

        private string _gVEst_Descripcion;

        public string GVEst_Descripcion
        {
            get { return _gVEst_Descripcion; }
            set { _gVEst_Descripcion = value; }
        }
        private string _gV_Solicitante;

        public string GV_Solicitante
        {
            get { return _gV_Solicitante; }
            set { _gV_Solicitante = value; }
        }
        private string _gV_Motivo;

        public string GV_Motivo
        {
            get { return _gV_Motivo; }
            set { _gV_Motivo = value; }
        }
        private DateTime? _gV_FechaSalida;

        public DateTime? GV_FechaSalida
        {
            get { return _gV_FechaSalida; }
            set { _gV_FechaSalida = value; }
        }

        private DateTime? _gV_FechaRegreso;

        public DateTime? GV_FechaRegreso
        {
            get { return _gV_FechaRegreso; }
            set { _gV_FechaRegreso = value; }
        }

        private decimal _gV_Importe;

        public decimal GV_Importe
        {
            get { return _gV_Importe; }
            set { _gV_Importe = value; }
        }
        private int _id_PagElec;

        public int Id_PagElec
        {
            get { return _id_PagElec; }
            set { _id_PagElec = value; }
        }

        private int _id_PagElecTipo;

        public int Id_PagElecTipo
        {
            get { return _id_PagElecTipo; }
            set { _id_PagElecTipo = value; }
        }

        //JFCV 22 dic 2015 guardar datos de detalle de los gastos de viaje 

        private DateTime? _GV_FechaElaboracion;

        public DateTime? GV_FechaElaboracion
        {
            get { return _GV_FechaElaboracion; }
            set { _GV_FechaElaboracion = value; }
        }

        private int _gV_TipoTransporte;

        public int GV_TipoTransporte
        {
            get { return _gV_TipoTransporte; }
            set { _gV_TipoTransporte = value; }
        }


        private int _gV_DiasHospedaje;

        public int GV_DiasHospedaje
        {
            get { return _gV_DiasHospedaje; }
            set { _gV_DiasHospedaje = value; }
        }


        private int _gV_CantidadDesayunos;

        public int GV_CantidadDesayunos
        {
            get { return _gV_CantidadDesayunos; }
            set { _gV_CantidadDesayunos = value; }
        }


        private int _gV_CantidadComidas;

        public int GV_CantidadComidas
        {
            get { return _gV_CantidadComidas; }
            set { _gV_CantidadComidas = value; }
        }

        private int _gV_CantidadCenas;

        public int GV_CantidadCenas
        {
            get { return _gV_CantidadCenas; }
            set { _gV_CantidadCenas = value; }
        }

        private int _gV_CantidadOtros;

        public int GV_CantidadOtros
        {
            get { return _gV_CantidadOtros; }
            set { _gV_CantidadOtros = value; }
        }

        private decimal _gV_ImporteOtros;

        public decimal GV_ImporteOtros
        {
            get { return _gV_ImporteOtros; }
            set { _gV_ImporteOtros = value; }
        }

        private DateTime? _fechaUltimaMod;

        public DateTime? FechaUltimaMod
        {
            get { return _fechaUltimaMod; }
            set { _fechaUltimaMod = value; }
        }

        private int _usuarioMod;

        public int UsuarioMod
        {
            get { return _usuarioMod; }
            set { _usuarioMod = value; }
        }

        private decimal _gV_TransporteCuota;

        public decimal GV_TransporteCuota
        {
            get { return _gV_TransporteCuota; }
            set { _gV_TransporteCuota = value; }
        }


        private string _gV_PagElec_Destino;

        public string GV_PagElec_Destino
        {
            get { return _gV_PagElec_Destino; }
            set { _gV_PagElec_Destino = value; }
        }



        private decimal _gV_Saldo_Comprobar;

        public decimal GV_Saldo_Comprobar
        {
            get { return _gV_Saldo_Comprobar; }
            set { _gV_Saldo_Comprobar = value; }
        }

        private string _gV_MotivoRechazo;

        public string GV_MotivoRechazo
        {
            get { return _gV_MotivoRechazo; }
            set { _gV_MotivoRechazo = value; }
        }

        private string _acr_NumeroGenerado;

        public string Acr_NumeroGenerado
        {
            get { return _acr_NumeroGenerado; }
            set { _acr_NumeroGenerado = value; }
        }

        private int _gv_TipoGasto;

        public int GV_TipoGasto
        {
            get { return _gv_TipoGasto; }
            set { _gv_TipoGasto = value; }
        }

        //JFCV 29 enero , se agregan los campos de nombre del acreedor y observaciones 
        private string _acr_Nombre;

        public string GV_Acr_Nombre
        {
            get { return _acr_Nombre; }
            set { _acr_Nombre = value; }
        }

        private string _gv_PagElec_Observaciones;

        public string GV_PagElec_Observaciones
        {
            get { return _gv_PagElec_Observaciones; }
            set { _gv_PagElec_Observaciones = value; }
        }
        private string _pagElecTipo_Descripcion;
        public string PagElecTipo_Descripcion
        {
            get { return _pagElecTipo_Descripcion; }
            set { _pagElecTipo_Descripcion = value; }
        }
        private string _gV_Fecha_Comprobacion;
        public string GV_Fecha_Comprobacion
        {
            get { return _gV_Fecha_Comprobacion; }
            set { _gV_Fecha_Comprobacion = value; }
        }

        //JFCV 23 Jun 2017 
        private string _pagElec_Cc;
        public string PagElec_Cc
        {
            get { return _pagElec_Cc; }
            set { _pagElec_Cc = value; }
        }

        private string _pagElec_SubCuenta;
        public string PagElec_SubCuenta
        {
            get { return _pagElec_SubCuenta; }
            set { _pagElec_SubCuenta = value; }
        }
        private string _pagElec_SubSubCuenta;
        public string PagElec_SubSubCuenta
        {
            get { return _pagElec_SubSubCuenta; }
            set { _pagElec_SubSubCuenta = value; }
        }


    }
}
