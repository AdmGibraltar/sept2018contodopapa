using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
   public class Grupos
    {
       private Int32 _Id_Cd;
        private Int32 _Id_Gpo;
        private Int32 _Id_SGpo;
        private Int32 _Id_DSGpo;
        private Int32 _Tipo;
        private Int32 _Caso;
        private string _Descripcion;
        private bool _Activo;
        private Int32 _Caract_Necesidad;
        private string _ActivoStr;

        private Int32 _Orden;

        public Int32 Id_Cd
        {
            get { return _Id_Cd; }
            set { _Id_Cd = value; }
        }
        public Int32 Id_Gpo
        {
            get { return _Id_Gpo; }
            set { _Id_Gpo = value; }
        }
        public Int32 Id_SGpo
        {
            get { return _Id_SGpo; }
            set { _Id_SGpo = value; }
        }
        public Int32 Id_DSGpo
        {
            get { return _Id_DSGpo; }
            set { _Id_DSGpo = value; }
        }
        public Int32 Orden
        {
            get { return _Orden; }
            set { _Orden = value; }
        }
        public Int32 Tipo
        {
            get { return _Tipo; }
            set { _Tipo = value; }
        }
        public Int32 Caso
        {
            get { return _Caso; }
            set { _Caso = value; }
        }
        public string Descripcion
        {
            get { return _Descripcion.Trim(); }
            set { _Descripcion = value.Trim(); }
        }
        public bool Activo
        {
            get { return _Activo; }
            set { _Activo = value; }
        }
        public Int32 CaracteristicaNecesidad
        {
            get { return _Caract_Necesidad; }
            set { _Caract_Necesidad = value; }
        }
        public string ActivoStr
        {
            get { return _ActivoStr; }
            set { _ActivoStr = value; }
        }

    }
}
