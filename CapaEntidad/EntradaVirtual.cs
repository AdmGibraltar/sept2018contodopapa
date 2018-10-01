using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class EntradaVirtual
    {

        int _Id_Emp;
        int _Id_Cd;
        int _Id_Env;
        int _Id_Es;
        int _Id_Cte;
        int _Ev_Pro;
        string _Env_CteNomComercial;
        DateTime _Env_Fecha;
        string _Env_Estatus; 
        int _Id_USolicita;
        int _Env_Credito;
        decimal _Env_Rentabilidad;
        decimal _Env_ImporteFacturar;
        string _Env_ComentariosSolicitante;
        string _Env_ComentariosAutoriza;
        DateTime _Env_FechaAutorizo;
        int _Id_UAutorizo;
        string _Env_Unique;
        string _U_Nombre;
        string _Cd_Nombre;
        int _Env_Solicitar;
        string _Accion;
        string _Env_DocAutorizar;
        string _Env_Contrato;
        string _Env_OrdCompra;
        string _EnvComunicadoCliente;
        int _Id_Pvd;
        
        public string Env_DocAutorizar
        {
            get { return _Env_DocAutorizar; }
            set { _Env_DocAutorizar = value; }
        }

        public int Id_Pvd
        {
            get { return _Id_Pvd; }
            set { _Id_Pvd = value; }
        }

        public int Id_Es
        {
            get { return _Id_Es; }
            set { _Id_Es = value; }
        }


        public string Env_Contrato
        {
            get { return _Env_Contrato; }
            set { _Env_Contrato = value; }
        }

        public string Env_OrdCompra
        {
            get { return _Env_OrdCompra; }
            set { _Env_OrdCompra = value; }
        }

        public string EnvComunicadoCliente
        {
            get { return _EnvComunicadoCliente; }
            set { _EnvComunicadoCliente = value; }
        }
        
        

        public List<EntradaVirtualDet> _VentanaEntradaVirtualDet;

        public List<EntradaVirtualArchivo> _VentanaEntradaVirtualArchivo;

        public int Id_Emp
        {
            get { return _Id_Emp; }
            set { _Id_Emp = value; }
        }

        public string Accion
        {
            get { return _Accion; }
            set { _Accion = value; }
        }

        public int Env_Solicitar
        {
            get { return _Env_Solicitar; }
            set { _Env_Solicitar = value; }
        }


        

        public string U_Nombre
        {
            get { return _U_Nombre; }
            set { _U_Nombre = value; }
        }


        public DateTime Env_Fecha
        {
            get { return _Env_Fecha; }
            set { _Env_Fecha = value; }
        }


        public string Env_Estatus
        {
            get { return _Env_Estatus; }
            set { _Env_Estatus = value; }
        }


        public string Cd_Nombre
        {
            get { return _Cd_Nombre; }
            set { _Cd_Nombre = value; }
        }

        public int Ev_Pro
        {
            get { return _Ev_Pro; }
            set { _Ev_Pro = value; }
        }

        public string Env_Unique
        {
            get { return _Env_Unique; }
            set { _Env_Unique = value; }
        }

        public int Id_Cd
        {
            get { return _Id_Cd; }
            set { _Id_Cd = value; }
        }

        public int Id_Env
        {
            get { return _Id_Env; }
            set { _Id_Env = value; }
        }

        public int Id_Cte
        {
            get { return _Id_Cte; }
            set { _Id_Cte = value; }
        }

        public string Env_CteNomComercial
        {
            get { return _Env_CteNomComercial; }
            set { _Env_CteNomComercial = value; }
        }

        public int Id_USolicita
        {
            get { return _Id_USolicita; }
            set { _Id_USolicita = value; }
        }

        public int Env_Credito
        {
            get { return _Env_Credito; }
            set { _Env_Credito = value; }
        }

        public decimal Env_Rentabilidad
        {
            get { return _Env_Rentabilidad; }
            set { _Env_Rentabilidad = value; }
        }

        public decimal Env_ImporteFacturar
        {
            get { return _Env_ImporteFacturar; }
            set { _Env_ImporteFacturar = value; }
        }
               

        public string Env_ComentariosSolicitante
        {
            get { return _Env_ComentariosSolicitante; }
            set { _Env_ComentariosSolicitante = value; }
        }


        public string Env_ComentariosAutoriza
        {
            get { return _Env_ComentariosAutoriza; }
            set { _Env_ComentariosAutoriza = value; }
        }

        public DateTime Env_FechaAutorizo
        {
            get { return _Env_FechaAutorizo; }
            set { _Env_FechaAutorizo = value; }
        }


        public int Id_UAutorizo
        {
            get { return _Id_UAutorizo; }
            set { _Id_UAutorizo = value; }
        }

        public List<EntradaVirtualDet> ListVentanaEntradaVirtualDet
        {
            get { return _VentanaEntradaVirtualDet; }
            set { _VentanaEntradaVirtualDet = value; }
        }


        public List<EntradaVirtualArchivo> ListVentanaEntradaVirtualArchivo
        {
            get { return _VentanaEntradaVirtualArchivo; }
            set { _VentanaEntradaVirtualArchivo = value; }
        }

    }
}
