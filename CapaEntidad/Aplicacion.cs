using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class Aplicacion
    {
        private object _Id_Emp;

        public object Id_Emp
        {
            get { return _Id_Emp; }
            set { _Id_Emp = value; }
        }
        private int _Id_Sol;

        public int Id_Sol
        {
            get { return _Id_Sol; }
            set { _Id_Sol = value; }
        }
        private int _Id_Area;

        public int Id_Area
        {
            get { return _Id_Area; }
            set { _Id_Area = value; }
        }
        private int _Id_Seg;

        public int Id_Seg
        {
            get { return _Id_Seg; }
            set { _Id_Seg = value; }
        }
        private int _Id_Uen;

        public int Id_Uen
        {
            get { return _Id_Uen; }
            set { _Id_Uen = value; }
        }
        private bool _Estatus;

        public bool Estatus
        {
            get { return _Estatus; }
            set { _Estatus = value; }
        }
        private string _EstatusStr;

        public string EstatusStr
        {
            get { return _EstatusStr; }
            set { _EstatusStr = value; }
        }
        private int _Id_Apl;

        public int Id_Apl
        {
            get { return _Id_Apl; }
            set { _Id_Apl = value; }
        }
        private string _Apl_Descripcion;

        public string Apl_Descripcion
        {
            get { return _Apl_Descripcion; }
            set { _Apl_Descripcion = value; }
        }
        private double _Apl_Potencial;

        public double Apl_Potencial
        {
            get { return _Apl_Potencial; }
            set { _Apl_Potencial = value; }
        }
        private bool _Apl_Limpieza;
        public int Id_Cd;

        public bool Apl_Limpieza
        {
            get { return _Apl_Limpieza; }
            set { _Apl_Limpieza = value; }
        }
    }
}
