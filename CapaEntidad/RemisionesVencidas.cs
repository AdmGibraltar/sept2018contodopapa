using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class RemisionesVencidas
    {
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

        private int _Id_Rem;
        public int Id_Rem
        {
            get { return _Id_Rem; }
            set { _Id_Rem = value; }
        }

        private DateTime? _Rem_Fecha;
        public DateTime? Rem_Fecha
        {
            get { return _Rem_Fecha; }
            set { _Rem_Fecha = value; }
        }

        private int _Id_Prd;
        public int Id_Prd
        {
            get { return _Id_Prd; }
            set { _Id_Prd = value; }
        }

        private string _Prd_Descripcion;
        public string Prd_Descripcion
        {
            get { return _Prd_Descripcion; }
            set { _Prd_Descripcion = value; }
        }

        private int _Rem_Cant;
        public int Rem_Cant
        {
            get { return _Rem_Cant; }
            set { _Rem_Cant = value; }
        }

        private int _vencido;
        public int vencido
        {
            get { return _vencido; }
            set { _vencido = value; }
        }

        private int _Rem_Dev;
        public int Rem_Dev
        {
            get { return _Rem_Dev; }
            set { _Rem_Dev = value; }
        }

        private decimal _Prd_Pesos;
        public decimal Prd_Pesos
        {
            get { return _Prd_Pesos; }
            set { _Prd_Pesos = value; }
        }

        private int _SaldoUnidades;
        public int SaldoUnidades
        {
            get { return _SaldoUnidades; }
            set { _SaldoUnidades = value; }
        }
        
    }
}
