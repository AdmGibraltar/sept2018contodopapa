using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class AplicacionCampana
    {
        private int _Id_Emp;
        public int Id_Emp
        {
            get { return _Id_Emp; }
            set { _Id_Emp = value; }
        }

        private int _Id_Cam;
        public int Id_Cam
        {
            get { return _Id_Cam; }
            set { _Id_Cam = value; }
        }

        private int _Id_Apl;
        public int Id_Apl
        {
            get { return _Id_Apl; }
            set { _Id_Apl = value; }
        }

        private string _Apl_Descripcion;
        public string Apl_Descripcion
        {
            get { return _Apl_Descripcion; }
            set { _Apl_Descripcion = value; }
        }


        private int _Id_Uen;
        public int Id_Uen
        {
            get { return _Id_Uen; }
            set { _Id_Uen = value; }
        }

        private string _Uen_Descripcion;
        public string Uen_Descripcion
        {
            get { return _Uen_Descripcion; }
            set { _Uen_Descripcion = value; }
        }

        private int _Id_Seg;
        public int Id_Seg
        {
            get { return _Id_Seg; }
            set { _Id_Seg = value; }
        }

        private string _Seg_Descripcion;
        public string Seg_Descripcion
        {
            get { return _Seg_Descripcion; }
            set { _Seg_Descripcion = value; }
        }


        private int _CamApl_Estatus;
        public int CamApl_Estatus
        {
            get { return _CamApl_Estatus; }
            set { _CamApl_Estatus = value; }
        }


    }
}
