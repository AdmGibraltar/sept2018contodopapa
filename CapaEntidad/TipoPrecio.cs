using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class TipoPrecio
    {
        private int _Id_Emp;
        public int Id_Emp
        {
            get { return _Id_Emp; }
            set { _Id_Emp = value; }
        }
        private int _Id_Pre;
        public int Id_Pre
        {
            get { return _Id_Pre; }
            set { _Id_Pre = value; }
        }
        private string _Pre_Descripcion;
        public string Pre_Descripcion
        {
            get { return _Pre_Descripcion; }
            set { _Pre_Descripcion = value; }
        }

        private Int16 _Pre_Tipo;
        public Int16 Pre_Tipo
        {
            get { return _Pre_Tipo; }
            set { _Pre_Tipo = value; }
        }

        private string _Pre_TipoStr;
        public string Pre_TipoStr
        {
            get { return _Pre_TipoStr; }
            set { _Pre_TipoStr = value; }
        }

        private bool _Pre_Activo;
        public bool Pre_Activo
        {
            get { return _Pre_Activo; }
            set { _Pre_Activo = value; }
        }

        private string _Pre_ActivoStr;
        public string Pre_ActivoStr
        {
            get { return _Pre_ActivoStr; }
            set { _Pre_ActivoStr = value; }
        }
    }
}
