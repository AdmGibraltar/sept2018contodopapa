using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class Asesoria
    {
        private int _Id_Emp;
        public int Id_Emp
        {
            get { return _Id_Emp; }
            set { _Id_Emp = value; }
        }

        private int _Id_Ase;
        public int Id_Ase
        {
            get { return _Id_Ase; }
            set { _Id_Ase = value; }
        }

        private string _Ase_Descripcion;
        public string Ase_Descripcion
        {
            get { return _Ase_Descripcion; }
            set { _Ase_Descripcion = value; }
        }

        private int _Ase_Revision;
        public int Ase_Revision
        {
            get { return _Ase_Revision; }
            set { _Ase_Revision = value; }
        }

        private Double _Ase_Costo;
        public Double Ase_Costo
        {
            get { return _Ase_Costo; }
            set { _Ase_Costo = value; }
        }

        private bool _Ase_Activo;
        public bool Ase_Activo
        {
            get { return _Ase_Activo; }
            set { _Ase_Activo = value; }
        }

        private string _Ase_ActivoStr;
        
        public string Ase_ActivoStr
        {
            get { return _Ase_ActivoStr; }
            set { _Ase_ActivoStr = value; }
        }

        private int _Ase_Frecuencia;
        public int Ase_Frecuencia
        {
            get { return _Ase_Frecuencia; }
            set { _Ase_Frecuencia = value; }
        }


        private bool _Ase_ServAsesoriaMensual;
        public bool Ase_ServAsesoriaMensual
        {
            get { return _Ase_ServAsesoriaMensual; }
            set { _Ase_ServAsesoriaMensual = value; }
        }


        private DateTime? _Ase_ServAsesoriaMensualfechaIni;
        public DateTime? Ase_ServAsesoriaMensualfechaIni
        {
            get { return _Ase_ServAsesoriaMensualfechaIni; }
            set { _Ase_ServAsesoriaMensualfechaIni = value; }
        }


        private bool _Ase_ServAsesoriaBimestral;
        public bool Ase_ServAsesoriaBimestral
        {
            get { return _Ase_ServAsesoriaBimestral; }
            set { _Ase_ServAsesoriaBimestral = value; }
        }

        private DateTime? _Ase_ServAsesoriaBimestralfechaIni;
        public DateTime? Ase_ServAsesoriaBimestralfechaIni
        {
            get { return _Ase_ServAsesoriaBimestralfechaIni; }
            set { _Ase_ServAsesoriaBimestralfechaIni = value; }
        }

        private bool _Ase_ServAsesoriaTrimestral;
        public bool Ase_ServAsesoriaTrimestral
        {
            get { return _Ase_ServAsesoriaTrimestral; }
            set { _Ase_ServAsesoriaTrimestral = value; }
        }

        private DateTime? _Ase_ServAsesoriaTrimestralfechaIni;
        public DateTime? Ase_ServAsesoriaTrimestralfechaIni
        {
            get { return _Ase_ServAsesoriaTrimestralfechaIni; }
            set { _Ase_ServAsesoriaTrimestralfechaIni = value; }
        }
    }
}
