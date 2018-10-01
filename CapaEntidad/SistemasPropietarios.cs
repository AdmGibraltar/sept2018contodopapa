using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class SistemasPropietarios
    {
        int _Id;
        int _Id_Anterior;
        int _Id_Emp;
        string _Descripcion;
        double _Factor;
        bool _Clase;
        bool _Estatus;
        string _EstatusStr;

        public int Id
        {
            get { return _Id; }
            set { _Id = value; }
        }
        public int Id_Emp
        {
            get { return _Id_Emp; }
            set { _Id_Emp = value; }
        }
        public int Id_Anterior
        {
            get { return _Id_Anterior; }
            set { _Id_Anterior = value; }
        }
        public string Descripcion
        {
            get { return _Descripcion; }
            set { _Descripcion = value; }
        }
        public double Factor
        {
            get { return _Factor; }
            set { _Factor = value; }
        }
        public bool Clase
        {
            get { return _Clase; }
            set { _Clase = value; }
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
