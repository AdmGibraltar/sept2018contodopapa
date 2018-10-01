using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
   public class TipoUsuario
    {
        private Int32 _Id_Cd;
        private Int32 _Id_TU;
        private string _TU_Descripcion;
        private Int32 _TU_Id_TU;
        private bool _TU_Activo;
        private string _TU_ActivoStr;
        private Int32 _PerteneceId;
        private Int32 _Id_Emp;
        private string _Pertence;
        private bool _Tu_Propia;

        public bool Tu_Propia
        {
            get { return _Tu_Propia; }
            set { _Tu_Propia = value; }
        }
        
        public string Id_Cd
        {
            get { return _Id_Cd.ToString(); }
            set { _Id_Cd = Convert.ToInt32(value); }
        }
        public int Id_TU
        {
            get { return _Id_TU; }
            set { _Id_TU = value; }
        }
        public string TU_Descripcion
        {
            get { return _TU_Descripcion.Trim(); }
            set { _TU_Descripcion = value; }
        }
        public int TU_Id_TU
        {
            get { return _TU_Id_TU; }
            set { _TU_Id_TU = value; }
        }
        public bool TU_Activo
        {
            get { return _TU_Activo; }
            set { _TU_Activo = value; }
        }
         
        public string TU_ActivoStr
        {
            get { return _TU_ActivoStr; }
            set { _TU_ActivoStr = value; }
        }
        public int PerteneceId
        {
            get { return _PerteneceId; }
            set { _PerteneceId = value; }
        }
        public string Pertence
        {
            get { return _Pertence.Trim(); }
            set { _Pertence = value; }
        }
        public int Id_Emp
        {
            get { return _Id_Emp; }
            set { _Id_Emp = value; }
        }
    }
}
