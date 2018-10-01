using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace CapaEntidad
{
    public class Usuario
    {
        //Datos de la empresa------------------------------------
        private int _Id_Emp;
        //Datos de la oficina------------------------------------
        private int _Id_Cd;
        private string _Ofi_Descripcion;
        //Datos de la cuenta-------------------------------------
        private string _Cu_user;
        private string _Cu_Pass;
        private bool _Cu_Estatus;
        private bool _Cu_Caducada;
        private DateTime _Cu_FBloq;
        //Datos de configuración---------------------------------
        private int _Id_Skin;
        private string _Skin_Descripcion;
        private bool _U_VerTodo;
        private bool _U_MultiCentro;
        private ArrayList _Id_Centros;
        //Datos del usuario--------------------------------------
        private int _Id_U;
        private int _Id_TU;
        private string _Tu_Descripcion;
        private string _U_Nombre;
        private string _U_NombreCorto;
        private string _U_Correo;
        private DateTime _U_FNac;

        //SAUL GUERRA 20150513 BEGIN
        private string _U_Telefono;
        private bool _U_ACYS;
        //SAUL GUERRA 20150513 END

        private bool _U_Activo;
        private bool _U_AutorizaPrecio;

      
        private string _U_ActivoStr;
        private int _Id_Id_U;
        //private int _U_ConfigFiltro;
        //Datos de la configuración de cuenta de correo----------
        private int _Id_CC;
        private string _Cc_Correo;
        private string _Cc_Pass;
        private string _Cc_ServEntr;
        private string _Cc_ServSal;
        private int _Cc_PuertoEnt;
        private bool _Cu_Activo;
        private bool _cc_Propia;
        public bool ProcSvtasAlm { get; set; }
        public bool ProcEmbAlm { get; set; }
        public bool ProcEntAlm { get; set; }
        public bool ProcAlmCob { get; set; }
        public bool ProcRevCob { get; set; }


        private string _U_NumNomina;
        public string U_NumNomina
        {
            get { return _U_NumNomina; }
            set { _U_NumNomina = value; }
        }

        public bool cc_Propia
        {
            get { return _cc_Propia; }
            set { _cc_Propia = value; }
        }
        public bool Cu_Activo
        {
            get { return _Cu_Activo; }
            set { _Cu_Activo = value; }
        }
        private int _Cc_PuertoSal;

        public bool U_AutorizaPrecio
        {
            get { return _U_AutorizaPrecio; }
            set { _U_AutorizaPrecio = value; }
        }

        //Datos de la empresa------------------------------------
        public int Id_Emp
        {
            get { return _Id_Emp; }
            set { _Id_Emp = value; }
        }
        private DateTime _CalendarioIni;
        private DateTime _CalendarioFin;
        private int _Id_Rik;
       

        public int Id_Rik
        {
            get { return _Id_Rik; }
            set { _Id_Rik = value; }
        }
        
        //Datos de la oficina------------------------------------
        public Int32 Id_Cd
        {
            get { return _Id_Cd; }
            set { _Id_Cd = value; }
        }
        public string Ofi_Descripcion
        {
            get { return _Ofi_Descripcion.Trim(); }
            set { _Ofi_Descripcion = value.Trim(); }
        }

        //Datos de la cuenta-------------------------------------
        public string Cu_User
        {
            get { return _Cu_user; }
            set { _Cu_user = value; }
        }
        public string Cu_pass
        {
            get { return _Cu_Pass; }
            set { _Cu_Pass = value; }
        }
        public bool Cu_Estatus
        {
            get { return _Cu_Estatus; }
            set { _Cu_Estatus = value; }
        }
        public bool Cu_Caducada
        {
            get { return _Cu_Caducada; }
            set { _Cu_Caducada = value; }
        }
        public DateTime Cu_FBloq
        {
            get { return _Cu_FBloq; }
            set { _Cu_FBloq = value; }
        }
        //Datos de configuración---------------------------------
        public int Id_Skin
        {
            get { return _Id_Skin; }
            set { _Id_Skin = value; }
        }
        public string Skin_Descripcion
        {
            get { return _Skin_Descripcion.Trim(); }
            set { _Skin_Descripcion = value.Trim(); }
        }
        public bool U_VerTodo
        {
            get { return _U_VerTodo; }
            set { _U_VerTodo = value; }
        }
        public bool U_MultiCentro
        {
            get { return _U_MultiCentro; }
            set { _U_MultiCentro = value; }
        }
        public ArrayList Id_Centros
        {
            get { return _Id_Centros; }
            set { _Id_Centros = value; }
        }
        //Datos del usuario--------------------------------------
        public int Id_U
        {
            get { return _Id_U; }
            set { _Id_U = value; }
        }
        public Int32 Id_TU
        {
            get { return _Id_TU; }
            set { _Id_TU = value; }
        }
        public string Tu_Descripcion
        {
            get { return _Tu_Descripcion; }
            set { _Tu_Descripcion = value; }
        }
        public string U_Nombre
        {
            get { return _U_Nombre; }
            set { _U_Nombre = value; }
        }
        public string U_NombreCorto
        {
            get { return _U_NombreCorto; }
            set { _U_NombreCorto = value; }
        }
        public string U_Correo
        {
            get { return _U_Correo; }
            set { _U_Correo = value; }
        }
        public DateTime U_FNac
        {
            get { return _U_FNac; }
            set { _U_FNac = value; }
        }

        //SAUL GUERRA 20150513 BEGIN
        public string U_Telefono {
            get { return _U_Telefono; }
            set { _U_Telefono = value; }
        }
        public bool U_ACYS {
            get { return _U_ACYS; }
            set { _U_ACYS = value; }
        }
        //SAUL GUERRA 20150513 END

        public bool U_Activo
        {
            get { return _U_Activo; }
            set { _U_Activo = value; }
        }
        public string U_ActivoStr
        {
            get { return _U_ActivoStr; }
            set { _U_ActivoStr = value; }
        }
        public int Id_Id_U
        {
            get { return _Id_Id_U; }
            set { _Id_Id_U = value; }
        }
        //public int U_ConfigFiltro
        //{
        //    get { return _U_ConfigFiltro; }
        //    set { _U_ConfigFiltro = value; }
        //}
        //Datos de Configuracion de correo-----------------------
        public int Id_CC
        {
            get { return _Id_CC; }
            set { _Id_CC = value; }
        }
        public string Cc_Corrreo
        {
            get { return _Cc_Correo; }
            set { _Cc_Correo = value; }
        }
        public string Cc_Pass
        {
            get { return _Cc_Pass; }
            set { _Cc_Pass = value; }
        }
        public string Cc_ServEntr
        {
            get { return _Cc_ServEntr; }
            set { _Cc_ServEntr = value; }
        }
        public string Cc_ServSal
        {
            get { return _Cc_ServSal; }
            set { _Cc_ServSal = value; }
        }
        public int Cc_PuertoEnt
        {
            get { return _Cc_PuertoEnt; }
            set { _Cc_PuertoEnt = value; }
        }
        public int Cc_PuertoSal
        {
            get { return _Cc_PuertoSal; }
            set { _Cc_PuertoSal = value; }
        }
        public DateTime CalendarioIni
        {
            get { return _CalendarioIni; }
            set { _CalendarioIni = value; }
        }


        public DateTime CalendarioFin
        {
            get { return _CalendarioFin; }
            set { _CalendarioFin = value; }
        }


         //CREDITO

        private bool _U_SusCredito;

        public bool U_SusCredito
        {
            get { return _U_SusCredito; }
            set { _U_SusCredito = value; }
        }
        private double? _U_DiasVencimiento;
        public double? U_DiasVencimiento
        {
            get { return _U_DiasVencimiento; }
            set { _U_DiasVencimiento = value; }
        }

        public string DbName;
        public int Id_Cd_Ver;
        public int Id_TCd;
    }
}
