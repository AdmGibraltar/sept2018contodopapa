using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class PronCierre
    {
        private int _Id_Emp;
        public int Id_Emp
        {
            get { return _Id_Emp; }
            set { _Id_Emp = value; }
        }
        private bool _Autorizado;
        public bool Autorizado
        {
            get { return _Autorizado; }
            set { _Autorizado = value; }
        }
        private int _Id_Cd;
        public int Id_Cd
        {
            get { return _Id_Cd; }
            set { _Id_Cd = value; }
        }        
        private int _Id_ProCierre;
        public int Id_ProCierre
        {
            get { return _Id_ProCierre; }
            set { _Id_ProCierre = value; }
        }
        private int _Id_Cte;
        public int Id_Cte
        {
            get { return _Id_Cte; }
            set { _Id_Cte = value; }
        }
        private string _Cte_NomComercial;
        public string Cte_NomComercial
        {
            get { return _Cte_NomComercial; }
            set { _Cte_NomComercial = value; }
        }
        private int _Id_Ter;
        public int Id_Ter
        {
            get { return _Id_Ter; }
            set { _Id_Ter = value; }
        }
        private string _Ter_Nombre;
        public string Ter_Nombre
        {
            get { return _Ter_Nombre; }
            set { _Ter_Nombre = value; }
        }
        private int _Id_Rik;
        public int Id_Rik
        {
            get { return _Id_Rik; }
            set { _Id_Rik = value; }
        }
        private double _Pron_Anterior;
        public double Pron_Anterior
        {
            get { return _Pron_Anterior; }
            set { _Pron_Anterior = value; }
        }
        private double _Pron_Actual;
        public double Pron_Actual
        {
            get { return _Pron_Actual; }
            set { _Pron_Actual = value; }
        }      
    }
}
