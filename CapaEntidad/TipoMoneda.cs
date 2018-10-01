using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class TipoMoneda
    {
        private int _Id_Emp;
        public int Id_Emp
        {
            get { return _Id_Emp; }
            set { _Id_Emp = value; }
        }

        private int _Id_Mon;
        public int Id_Mon
        {
            get { return _Id_Mon; }
            set { _Id_Mon = value; }
        }

        private string _Mon_Descripcion;
        public string Mon_Descripcion
        {
            get { return _Mon_Descripcion; }
            set { _Mon_Descripcion = value; }
        }

        private string _Mon_Abrev;
        public string Mon_Abrev
        {
            get { return _Mon_Abrev; }
            set { _Mon_Abrev = value; }
        }

        private double _Mon_TipCambio;
        public double Mon_TipCambio
        {
            get { return _Mon_TipCambio; }
            set { _Mon_TipCambio = value; }
        }

        private bool _Mon_Activo;
        public bool Mon_Activo
        {
            get { return _Mon_Activo; }
            set { _Mon_Activo = value; }
        }

        private string _Mon_ActivoStr;
        public string Mon_ActivoStr
        {
            get { return _Mon_ActivoStr; }
            set { _Mon_ActivoStr = value; }
        }
    }
}
