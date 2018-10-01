using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class Comun
    {
        private int _Id;
        private string _IdStr;
        private string _Descripcion;
        private string _Relacion;
        private double _valorDoble;
        private bool _ValorBool;
        private int _ValorInt;
        private double _ValorDoble2;

        public double ValorDoble2
        {
            get { return _ValorDoble2; }
            set { _ValorDoble2 = value; }
        }
        private double _ValorDoble3;

        public double ValorDoble3
        {
            get { return _ValorDoble3; }
            set { _ValorDoble3 = value; }
        }
        private int _ValorInt2;

        public int ValorInt2
        {
            get { return _ValorInt2; }
            set { _ValorInt2 = value; }
        }
        private int _ValorInt3;

        public int ValorInt31
        {
            get { return _ValorInt3; }
            set { _ValorInt3 = value; }
        }
        public string _IdStr2;
        private string _ValorStr;

        public string ValorStr
        {
            get { return _ValorStr; }
            set { _ValorStr = value; }
        }
        private string _ValorStr2;

        public string ValorStr2
        {
            get { return _ValorStr2; }
            set { _ValorStr2 = value; }
        }
        private string _ValorStr3;
        public DateTime ValorDateTime;
      
        public string IdStr2
        {
            get { return _IdStr2; }
            set { _IdStr2 = value; }
        }

        public string ValorStr3
        {
            get { return _ValorStr3; }
            set { _ValorStr3 = value; }
        }

        public int ValorInt3
        {
            get { return ValorInt31; }
            set { ValorInt31 = value; }
        }

        public int ValorInt
        {
            get { return _ValorInt; }
            set { _ValorInt = value; }
        }

        public bool ValorBool
        {
            get { return _ValorBool; }
            set { _ValorBool = value; }
        }


        public int Id
        {
            get { return _Id; }
            set { _Id = value; }
        }
        public string IdStr
        {
            get { return _IdStr; }
            set { _IdStr = value; }
        }
        public string Descripcion
        {
            get { return _Descripcion.Trim(); }
            set { _Descripcion = value.Trim(); }
        }
        public string Relacion
        {
            get { return _Relacion.Trim(); }
            set { _Relacion = value.Trim(); }
        }
        public double ValorDoble
        {
            get { return _valorDoble; }
            set { _valorDoble = value; }
        }

    }
}
