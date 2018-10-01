using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class Area
    {
        private int _Id_Emp;

        public int Id_Emp
        {
            get { return _Id_Emp; }
            set { _Id_Emp = value; }
        }
        private int _Id_Cd;

        public int Id_Cd
        {
            get { return _Id_Cd; }
            set { _Id_Cd = value; }
        }
        private int _Id_Area;

        public int Id_Area
        {
            get { return _Id_Area; }
            set { _Id_Area = value; }
        }
        private string _Area_Descripcion;

        public string Area_Descripcion
        {
            get { return _Area_Descripcion; }
            set { _Area_Descripcion = value; }
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
        private double _Area_Potencial;

        public double Area_Potencial
        {
            get { return _Area_Potencial; }
            set { _Area_Potencial = value; }
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
    }
}
