using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    [Serializable]
    public class CampanasMetas
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

        private int _Id_Rik;
        public int Id_Rik
        {
            get { return _Id_Rik; }
            set { _Id_Rik = value; }
        }

        private int _Id_Cd;
        public int Id_Cd
        {
            get { return _Id_Cd; }
            set { _Id_Cd = value; }
        }

        private string _Rik_Nombre;
        public string Rik_Nombre
        {
            get { return _Rik_Nombre; }
            set { _Rik_Nombre = value; }
        }

        private int _MetCam_Cantidad;
        public int MetCam_Cantidad
        {
            get { return _MetCam_Cantidad; }
            set { _MetCam_Cantidad = value; }
        }

        private double _MetCam_Monto;
        public double MetCam_Monto
        {
            get { return _MetCam_Monto; }
            set { _MetCam_Monto = value; }
        }

        private bool _MetCam_Estatus;
        public bool MetCam_Estatus
        {
            get { return _MetCam_Estatus; }
            set { _MetCam_Estatus = value; }
        }
    }
}
