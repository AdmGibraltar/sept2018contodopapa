using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class Estatus
    {
        private int _Id_Estatus;

        public int Id_Estatus
        {
            get { return _Id_Estatus; }
            set { _Id_Estatus = value; }
        }
        private string _Es_Descripcion;

        public string Es_Descripcion
        {
            get { return _Es_Descripcion; }
            set { _Es_Descripcion = value; }
        }
        private int _Id_Emp;

        public int Id_Emp
        {
            get { return _Id_Emp; }
            set { _Id_Emp = value; }
        }
        private bool _Es_Estatus;

        public bool Es_Estatus
        {
            get { return _Es_Estatus; }
            set { _Es_Estatus = value; }
        }
        private string _Es_EstatusStr;

        public string Es_EstatusStr
        {
            get { return _Es_EstatusStr; }
            set { _Es_EstatusStr = value; }
        }
        
    }
}
