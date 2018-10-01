using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class Uen
    {
        int _Id_Emp;
        int _Id;
        string _Descripcion;
        bool _Estatus;
        string _EstatusStr;
        private int? _Id_U;

        public int? Id_U
        {
            get { return _Id_U; }
            set { _Id_U = value; }
        }

        public int Id_Emp
        {
            get { return _Id_Emp; }
            set { _Id_Emp = value; }
        }
        public int Id
        {
            get { return _Id; }
            set { _Id = value; }
        }
        public string Descripcion
        {
            get { return _Descripcion; }
            set { _Descripcion = value; }
        }
        public bool Estatus
        {
            get { return _Estatus; }
            set { _Estatus = value; }
        }
        public string EstatusStr
        {
            get { return _EstatusStr; }
            set { _EstatusStr = value; }
        }
    }
}
