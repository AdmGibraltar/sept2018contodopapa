using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class PermisoControl
    {
        int _Id_Emp;
        int _Id_Tu;
        int _Sm_Cve;
        string _Id_Ctrl;
        string _Descripcion;
        bool _Ctrl_Deshabilitado;
        bool _Ctrl_Oculto;
        string _Tipo;
        string _Ctrl_Label;

        public string Ctrl_Label
        {
            get { return _Ctrl_Label; }
            set { _Ctrl_Label = value; }
        }

        public string Tipo
        {
            get { return _Tipo; }
            set { _Tipo = value; }
        }
        public int Id_Emp
        {
            get { return _Id_Emp; }
            set { _Id_Emp = value; }
        }
        public int Id_Tu
        {
            get { return _Id_Tu; }
            set { _Id_Tu = value; }
        }
        public int Sm_Cve
        {
            get { return _Sm_Cve; }
            set { _Sm_Cve = value; }
        }
        public string Id_Ctrl
        {
            get { return _Id_Ctrl; }
            set { _Id_Ctrl = value; }
        }
        public string Descripcion
        {
            get { return _Descripcion; }
            set { _Descripcion = value; }
        }
        public bool Ctrl_Deshabilitado
        {
            get { return _Ctrl_Deshabilitado; }
            set { _Ctrl_Deshabilitado = value; }
        }
        public bool Ctrl_Oculto
        {
            get { return _Ctrl_Oculto; }
            set { _Ctrl_Oculto = value; }
        }
        
    }
}
