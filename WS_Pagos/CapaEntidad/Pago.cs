using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class  Pago
    {
        private int _Id_Emp;
        private int _Id_Cd;
        private int _Id_Pag;
        private string _Pag_TipoStr;
        private int _Id_U;
        private DateTime _Pag_Fecha;
        private double _Pag_Total;
        private string _Pag_Estatus;
        private string _Pag_EstatusStr;
 
        private string _Filtro_Estatus;
 
        private string _Filtro_PagIni;
        private string _Filtro_PagFin;
        private string _Filtro_usuario;
        private string _Cte_Nombre;
        private int _Tipo;
        private int _Id_Tmov;
        private double _Pag_Importe;
        public DateTime? Filtro_FecIni;
        public DateTime? Filtro_FecFin;
        private string _Pag_ExtemporaneoStr;
        private bool _Pag_Extemporaneo;
        private int _Filtro_Extemporaneo;

        public int Filtro_Extemporaneo
        {
            get { return _Filtro_Extemporaneo; }
            set { _Filtro_Extemporaneo = value; }
        }

        public bool Pag_Extemporaneo
        {
            get { return _Pag_Extemporaneo; }
            set { _Pag_Extemporaneo = value; }
        }

        public string Pag_ExtemporaneoStr
        {
            get { return _Pag_ExtemporaneoStr; }
            set { _Pag_ExtemporaneoStr = value; }
        }
        

     

        public double Pag_Importe
        {
            get { return _Pag_Importe; }
            set { _Pag_Importe = value; }
        }
        public string Cte_Nombre
        {
            get { return _Cte_Nombre; }
            set { _Cte_Nombre = value; }
        }
        public int Tipo
        {
            get { return _Tipo; }
            set { _Tipo = value; }
        }
        public int Id_Tmov
        {
            get { return _Id_Tmov; }
            set { _Id_Tmov = value; }
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
        public int Id_Pag
        {
            get { return _Id_Pag; }
            set { _Id_Pag = value; }
        }
        public string Pag_TipoStr
        {
            get { return _Pag_TipoStr; }
            set { _Pag_TipoStr = value; }
        }
        public int Id_U
        {
            get { return _Id_U; }
            set { _Id_U = value; }
        }
        public DateTime Pag_Fecha
        {
            get { return _Pag_Fecha; }
            set { _Pag_Fecha = value; }
        }
        public double Pag_Total
        {
            get { return _Pag_Total; }
            set { _Pag_Total = value; }
        }
        public string Pag_Estatus
        {
            get { return _Pag_Estatus; }
            set { _Pag_Estatus = value; }
        }
        public string Pag_EstatusStr
        {
            get { return _Pag_EstatusStr; }
            set { _Pag_EstatusStr = value; }
        }
      
        public string Filtro_Estatus
        {
            get { return _Filtro_Estatus; }
            set { _Filtro_Estatus = value; }
        }
        
        public string Filtro_PagIni
        {
            get { return _Filtro_PagIni; }
            set { _Filtro_PagIni = value; }
        }
        public string Filtro_PagFin
        {
            get { return _Filtro_PagFin; }
            set { _Filtro_PagFin = value; }
        }
        public string Filtro_usuario
        {
            get { return _Filtro_usuario; }
            set { _Filtro_usuario = value; }
        }

        public string Id_CdOrigenStr { get; set; }

        public int Id_PagExt { get; set; }
        public int Id_CdOrigen { get; set; }
        public int Id_UOrigen { get; set; }
        public int Id_PagOrigen { get; set; }
        public string U_Nombre { get; set; }
    }
}
