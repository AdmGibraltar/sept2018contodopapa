using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class ValuacionProyecto
    {
        public int _Id_Emp;
        public int Id_Emp
        {
            get { return _Id_Emp; }
            set { _Id_Emp = value; }
        }

        public int _Id_Cd;
        public int Id_Cd
        {
            get { return _Id_Cd; }
            set { _Id_Cd = value; }
        }

        public string _Cd_Nombre;
        public string Cd_Nombre
        {
            get { return _Cd_Nombre; }
            set { _Cd_Nombre = value; }
        }

        public int _Id_Vap;
        public int Id_Vap
        {
            get { return _Id_Vap; }
            set { _Id_Vap = value; }
        }

        public DateTime _Vap_Fecha;
        public DateTime Vap_Fecha
        {
            get { return _Vap_Fecha; }
            set { _Vap_Fecha = value; }
        }

        public string _Vap_FechaStr;
        public string Vap_FechaStr
        {
            get { return _Vap_FechaStr; }
            set { _Vap_FechaStr = value; }
        }

        public int _Id_Cte;
        public int Id_Cte
        {
            get { return _Id_Cte; }
            set { _Id_Cte = value; }
        }

        public string _Cte_NomComercial;
        public string Cte_NomComercial
        {
            get { return _Cte_NomComercial; }
            set { _Cte_NomComercial = value; }
        }

        public int _Id_U;
        public int Id_U
        {
            get { return _Id_U; }
            set { _Id_U = value; }
        }

        public string _Vap_Nota;
        public string Vap_Nota
        {
            get { return _Vap_Nota; }
            set { _Vap_Nota = value; }
        }

        public string _Vap_Estatus;
        public string Vap_Estatus
        {
            get { return _Vap_Estatus; }
            set { _Vap_Estatus = value; }
        }

        public string _Vap_EstatusStr;
        public string Vap_EstatusStr
        {
            get { return _Vap_EstatusStr; }
            set { _Vap_EstatusStr = value; }
        }

        private int _Vap_Sustituida;
        public int Vap_Sustituida
        {
            get { return _Vap_Sustituida; }
            set { _Vap_Sustituida = value; }
        }

        private List<ValuacionProyectoDetalle> _ListaProductosValuacionProyecto;
        private DateTime _Vap_FechaAut;
        private string _Vap_FechaAutStr;
        private string _Vap_Usuario;

        public string Vap_Usuario
        {
            get { return _Vap_Usuario; }
            set { _Vap_Usuario = value; }
        }

        public string Vap_FechaAutStr
        {
            get { return _Vap_FechaAutStr; }
            set { _Vap_FechaAutStr = value; }
        }

        public DateTime Vap_FechaAut
        {
            get { return _Vap_FechaAut; }
            set { _Vap_FechaAut = value; }
        }
        public List<ValuacionProyectoDetalle> ListaProductosValuacionProyecto
        {
            get { return _ListaProductosValuacionProyecto; }
            set { _ListaProductosValuacionProyecto = value; }
        }

        /// <summary>
        /// Propiedad utilizada para acceder al valor calculado de la utilidad remanante en la generación de la valuacion
        /// </summary>
        public Nullable<decimal> Vap_UtilidadRemanente { get; set; }
        /// <summary>
        /// Propiedad utilizada para acceder al valor calculado del valor presente neto en la generación de la valuacion
        /// </summary>
        public Nullable<decimal> Vap_ValorPresenteNeto { get; set; }

        /// <summary>
        /// Campo utilizado para representar el estado asociado a la aceptación de la valuación: valuación en espera de ser aceptada, valuación aceptada, valuación en espera de aprovación, valuación rechazada
        /// </summary>
        /// 
        //public int? Estatus2 // RFH
        public int Estatus2
        {
            get;
            set;
        }

        /// <summary>
        /// Identificador del RIK que generó la valuación
        /// </summary>
        public int? Id_Rik
        {
            get;
            set;
        }

        public int? Id_Ter
        {
            get;
            set;
        }

        public string MotivoParaAutorizacion
        {
            get;
            set;
        }

        //Id_Cd	Cd_Nombre	Id_U	U_Nombre	Id_Vap	Vap_Fecha	           Vap_Nota	Id_Emp	Vap_Estatus	Vap_Sustituida
        //    2	Sucursal1	  2	  Oscar G. Casillas	9	2011-07-14 00:00:00.000	prueba	   1	   C	       9
    }
}
