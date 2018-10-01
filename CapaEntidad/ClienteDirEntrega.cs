using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class ClienteDirEntrega
    {
        private int _id_Emp;
        private int _id_Cd;
        private int _id_Cte;
        private int _id_CteDirEntrega;
        private string _cte_Calle;
        private string _cte_Numero;
        private string _cte_Cp;
        private string _cte_Colonia;
        private string _cte_Municipio;
        private string _cte_Estado;
        private string _cte_Sector;
        private string _cte_Telefono;
        private string _cte_Fax;
        private string _cte_HoraAm1;
        private string _cte_HoraAm2;
        private string _cte_HoraPm1;
        private string _cte_HoraPm2;
        private string _Direccion;

        public int Id_Emp 
        {
            get { return _id_Emp; }
            set { _id_Emp = value; }
        }
        public int Id_Cd
        {
            get { return _id_Cd; }
            set { _id_Cd = value; }
        }
        public int Id_Cte
        {
            get { return _id_Cte; }
            set { _id_Cte = value; }
        }
        public int Id_CteDirEntrega
        {
            get { return _id_CteDirEntrega; }
            set { _id_CteDirEntrega = value; }
        }
        public string Cte_Calle
        {
            get { return _cte_Calle; }
            set { _cte_Calle = value; }
        }
        public string Cte_Numero
        {
            get { return _cte_Numero; }
            set { _cte_Numero = value; }
        }
        public string Cte_Cp
        {
            get { return _cte_Cp; }
            set { _cte_Cp = value; }
        }
        public string Cte_Colonia
        {
            get { return _cte_Colonia; }
            set { _cte_Colonia = value; }
        }
        public string Cte_Municipio
        {
            get { return _cte_Municipio; }
            set { _cte_Municipio = value; }
        }
        public string Cte_Estado
        {
            get { return _cte_Estado; }
            set { _cte_Estado = value; }
        }
        public string Cte_Sector
        {
            get { return _cte_Sector; }
            set { _cte_Sector = value; }
        }
        public string Cte_Telefono
        {
            get { return _cte_Telefono; }
            set { _cte_Telefono = value; }
        }
        public string Cte_Fax
        {
            get { return _cte_Fax; }
            set { _cte_Fax = value; }
        }
        public string Cte_HoraAm1
        {
            get { return _cte_HoraAm1; }
            set { _cte_HoraAm1 = value; }
        }
        public string Cte_HoraAm2
        {
            get { return _cte_HoraAm2; }
            set { _cte_HoraAm2 = value; }
        }
        public string Cte_HoraPm1
        {
            get { return _cte_HoraPm1; }
            set { _cte_HoraPm1 = value; }
        }
        public string Cte_HoraPm2
        {
            get { return _cte_HoraPm2; }
            set { _cte_HoraPm2 = value; }
        }

        public string Direccion
        {
            get { return _Direccion; }
            set { _Direccion = value; }
        }
        
    }
}
