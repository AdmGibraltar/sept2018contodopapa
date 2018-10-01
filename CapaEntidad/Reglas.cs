using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class Reglas
    {
        private double? _Plazo;
        public object Plazo
        {
            get
            {
                return _Plazo;
            }
            set
            {
                _Plazo = (double?)value;
            }
        }

        private int _Id_Tu1;
        public object Id_Tu1
        {
            get { return _Id_Tu1; }
            set
            {
                if (value == null || value == DBNull.Value)
                {
                    _Id_Tu1 = -1;
                }
                else
                {
                    _Id_Tu1 = Convert.ToInt32(value);
                }
            }
        }

        private int _Id_Tu2;
        public object Id_Tu2
        {
            get { return _Id_Tu2; }
            set
            {
                if (value == null || value == DBNull.Value)
                {
                    _Id_Tu2 = -1;
                }
                else
                {
                    _Id_Tu2 = Convert.ToInt32(value);
                }
            }
        }

        private int _Id_Tu3;
        public object Id_Tu3
        {
            get { return _Id_Tu3; }
            set
            {
                if (value == null || value == DBNull.Value)
                {
                    _Id_Tu3 = -1;
                }
                else
                {
                    _Id_Tu3 = Convert.ToInt32(value);
                }
            }
        }

        private double? _Val1;
        public object Val1
        {
            get { return _Val1; }
            set
            {
                if (value == DBNull.Value)
                {
                    _Val1 = null;
                }
                else
                {
                    _Val1 = (double?)value;
                }
            }
        }

        private double? _Val2;
        public object Val2
        {
            get { return _Val2; }
            set
            {
                if (value == DBNull.Value)
                {
                    _Val2 = null;
                }
                else
                {
                    _Val2 = (double?)value;
                }
            }
        }

        private double? _Val3;
        public object Val3
        {
            get { return _Val3; }
            set
            {
                if (value == DBNull.Value)
                {
                    _Val3 = null;
                }
                else
                {
                    _Val3 = (double?)value;
                }
            }
        }

        private double? _Val4;
        public object Val4
        {
            get { return _Val4; }
            set
            {
                if (value == DBNull.Value)
                {
                    _Val4 = null;
                }
                else
                {
                    _Val4 = (double?)value;
                }
            }
        }

        private double? _Val5;
        public object Val5
        {
            get { return _Val5; }
            set
            {
                if (value == DBNull.Value)
                {
                    _Val5 = null;
                }
                else
                {
                    _Val5 = (double?)value;
                }
            }
        }

        private double? _Val6;
        public object Val6
        {
            get { return _Val6; }
            set
            {
                if (value == DBNull.Value)
                {
                    _Val6 = null;
                }
                else
                {
                    _Val6 = (double?)value;
                }
            }
        }

        private List<PeriodoGracia> _list_gracia;

        public List<PeriodoGracia> List_gracia
        {
            get { return _list_gracia; }
            set { _list_gracia = value; }
        }
    }
}
