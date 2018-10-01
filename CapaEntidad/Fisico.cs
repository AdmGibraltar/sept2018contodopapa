using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class Fisico
    {
        int _Id_Emp;
        int _Id_Cd;
        int _Id_Fis;
        int _Id_Prd;
        DateTime _Fis_Fecha;
        int _Fis_Fisico;
        private List<FisicoConsignado> _ListFisicoConsignado;

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
        public int Id_Prd
        {
            get { return _Id_Prd; }
            set { _Id_Prd = value; }
        }
        public DateTime Fis_Fecha
        {
            get { return _Fis_Fecha; }
            set { _Fis_Fecha = value; }
        }
        public int Fis_Fisico
        {
            get { return _Fis_Fisico; }
            set { _Fis_Fisico = value; }
        }

        public List<FisicoConsignado> ListFisicoConsignado
        {
            get { return _ListFisicoConsignado; }
            set { _ListFisicoConsignado = value; }
        }

        private bool _Reiniciar;


        public bool Reiniciar
        {
            get { return _Reiniciar; }
            set { _Reiniciar = value; }
        }

        private int _Auto;

        public int Auto
        {
            get { return _Auto; }
            set { _Auto = value; }
        }
    }
}
