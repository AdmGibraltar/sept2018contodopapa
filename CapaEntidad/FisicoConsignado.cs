using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class FisicoConsignado
    {
        int _Id_Emp;
        int _Id_Cd;
        int _Id_Fis;
        int _Id_FisCons;
        int _Id_Cte;
        int _Id_Ter;
        int _Fis_Consignados;
        private string _Cte_Nombre;

        public string Cte_Nombre
        {
            get { return _Cte_Nombre; }
            set { _Cte_Nombre = value; }
        }
        private string _Ter_Nombre;

        public string Ter_Nombre
        {
            get { return _Ter_Nombre; }
            set { _Ter_Nombre = value; }
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
        public int Id_Fis
        {
            get { return _Id_Fis; }
            set { _Id_Fis = value; }
        }
        public int Id_FisCons
        {
            get { return _Id_FisCons; }
            set { _Id_FisCons = value; }
        }
        public int Id_Cte
        {
            get { return _Id_Cte; }
            set { _Id_Cte = value; }
        }
        public int Id_Ter
        {
            get { return _Id_Ter; }
            set { _Id_Ter = value; }
        }
        public int Fis_Consignados
        {
            get { return _Fis_Consignados; }
            set { _Fis_Consignados = value; }
        }
    }
}
