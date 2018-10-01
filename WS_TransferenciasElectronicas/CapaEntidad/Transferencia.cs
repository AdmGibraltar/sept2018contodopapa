using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class  Transferencia
    {
	
        private int _Id_Emp;
        private int _Id_Cd;
        private int _Id_Trans;
        private string _TransNota;
        private int _Id_CdOrigen;
        private string _Id_CdOrigenStr;
        private int _Id_RemOrigen;
        private int _Id_U;
        private DateTime _Trans_Fecha;
        private DateTime _Trans_FechaHr;
        private double _Trans_SubTotal;
        private double _Trans_Iva;
        private double _Trans_Total;
        private string _Trans_Estatus;
        private string _Trans_EstatusStr;
 
        private string _Filtro_Estatus;
 
        private string _Filtro_TransIni;
        private string _Filtro_TransFin;      
        public DateTime? Filtro_FecIni;
        public DateTime? Filtro_FecFin;
        private int _Id_UOrigen;
        private string _U_NombreOrigen;
        

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

        public int Id_Trans
        {
            get { return _Id_Trans; }
            set { _Id_Trans = value; }
        }


        public string TransNota
        {
            get { return _TransNota; }
            set { _TransNota = value; }
        }
        public int Id_CdOrigen
        {
            get { return _Id_CdOrigen; }
            set { _Id_CdOrigen = value; }
        }
        public string Id_CdOrigenStr
        {
            get { return _Id_CdOrigenStr; }
            set { _Id_CdOrigenStr = value; }
        }
        public int Id_RemOrigen
        {
            get { return _Id_RemOrigen; }
            set { _Id_RemOrigen = value; }
        }
        public int Id_U
        {
            get { return _Id_U; }
            set { _Id_U = value; }
        }
        public DateTime Trans_Fecha
        {
            get { return _Trans_Fecha; }
            set { _Trans_Fecha = value; }
        }

        public DateTime Trans_FechaHr
        {
            get { return _Trans_FechaHr; }
            set { _Trans_FechaHr = value; }
        }
       
        
        
        public double Trans_SubTotal
        {
            get { return _Trans_SubTotal; }
            set { _Trans_SubTotal = value; }
        }
        public double Trans_Iva
        {
            get { return _Trans_Iva; }
            set { _Trans_Iva = value; }
        }
        public double Trans_Total
        {
            get { return _Trans_Total; }
            set { _Trans_Total = value; }
        }
       
        public string Trans_Estatus
        {
            get { return _Trans_Estatus; }
            set { _Trans_Estatus = value; }
        }
        public string Trans_EstatusStr
        {
            get { return _Trans_EstatusStr; }
            set { _Trans_EstatusStr = value; }
        }
      
        public string Filtro_Estatus
        {
            get { return _Filtro_Estatus; }
            set { _Filtro_Estatus = value; }
        }
        
        public string Filtro_TransIni
        {
            get { return _Filtro_TransIni; }
            set { _Filtro_TransIni = value; }
        }
        public string Filtro_TransFin
        {
            get { return _Filtro_TransFin; }
            set { _Filtro_TransFin = value; }
        }
        public int Id_UOrigen
        {
            get { return _Id_UOrigen; }
            set { _Id_UOrigen = value; }
        }


        public string U_NombreOrigen
        {
            get { return _U_NombreOrigen; }
            set { _U_NombreOrigen = value; }
        }
       

      
    }
}
