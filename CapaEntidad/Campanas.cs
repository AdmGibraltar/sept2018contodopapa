using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class Campanas
    {
        private int _Id_Emp;
        public int Id_Emp
        {
            get { return _Id_Emp; }
            set { _Id_Emp = value; }
        }

        private int _Id_Prd;
        public int Id_Prd
        {
            get { return _Id_Prd; }
            set { _Id_Prd = value; }
        }

        private string _Prd_Descripcion;
        public string Prd_Descripcion
        {
            get { return _Prd_Descripcion; }
            set { _Prd_Descripcion = value; }
        }

        private int _Cam_PrdCant;
        public int Cam_PrdCant
        {
            get { return _Cam_PrdCant; }
            set { _Cam_PrdCant = value; }
        }


        private int _Id_Cd;
        public int Id_Cd
        {
            get { return _Id_Cd; }
            set { _Id_Cd = value; }
        }

        private int _Id_Cam;
        public int Id_Cam
        {
            get { return _Id_Cam; }
            set { _Id_Cam = value; }
        }

        private int? _Id_Uen;
        public int? Id_Uen
        {
            get { return _Id_Uen; }
            set { _Id_Uen = value; }
        }

        private string _Uen;
        public string Uen
        {
            get { return _Uen; }
            set { _Uen = value; }
        }

        private int? _Id_Seg;
        public int? Id_Seg
        {
            get { return _Id_Seg; }
            set { _Id_Seg = value; }
        }

        private string _Segmento;
        public string Segmento
        {
            get { return _Segmento; }
            set { _Segmento = value; }
        }


        private int? _Id_Area;
        public int? Id_Area
        {
            get { return _Id_Area; }
            set { _Id_Area = value; }
        }

        private string _Area;
        public string Area
        {
            get { return _Area; }
            set { _Area = value; }
        }

        private int? _Id_Sol;
        public int? Id_Sol
        {
            get { return _Id_Sol; }
            set { _Id_Sol = value; }
        }

        private string _Solucion;
        public string Solucion
        {
            get { return _Solucion; }
            set { _Solucion = value; }
        }

        private int? _Id_Aplicacion;
        public int? Id_Aplicacion
        {
            get { return _Id_Aplicacion; }
            set { _Id_Aplicacion = value; }
        }

        private string _Aplicacion;
        public string Aplicacion
        {
            get { return _Aplicacion; }
            set { _Aplicacion = value; }
        }

        private string _EstatusStr;
        public string EstatusStr
        {
            get { return _EstatusStr; }
            set { _EstatusStr = value; }
        }
        
        private DateTime _Cam_FechaInicio;
        public DateTime Cam_FechaInicio
        {
            get { return _Cam_FechaInicio; }
            set { _Cam_FechaInicio = value; }
        }


        private DateTime _Cam_FechaFin;
        public DateTime Cam_FechaFin
        {
            get { return _Cam_FechaFin; }
            set { _Cam_FechaFin = value; }
        }

        private bool? _Cam_Activo;
        public bool? Cam_Activo
        {
            get { return _Cam_Activo; }
            set { _Cam_Activo = value; }
        }


        private string _Cam_Aplicacion;
        public string Cam_Aplicacion
        {
            get { return _Cam_Aplicacion; }
            set { _Cam_Aplicacion = value; }
        }

        private string _Cam_Nombre;
        public string Cam_Nombre
        {
            get { return _Cam_Nombre; }
            set { _Cam_Nombre = value; }
        }

        private bool? _Cam_Jabon;
        public bool? Cam_Jabon
        {
            get { return _Cam_Jabon; }
            set { _Cam_Jabon = value; }
        }

        private bool? _Cam_Toalla;
        public bool? Cam_Toalla
        {
            get { return _Cam_Toalla; }
            set { _Cam_Toalla = value; }
        }

        private bool? _Cam_Olores;
        public bool? Cam_Olores
        {
            get { return _Cam_Olores; }
            set { _Cam_Olores = value; }
        }

        private bool? _Cam_Quimicos;
        public bool? Cam_Quimicos
        {
            get { return _Cam_Quimicos; }
            set { _Cam_Quimicos = value; }
        }

        private bool? _Cam_Tratamiento;
        public bool? Cam_Tratamiento
        {
            get { return _Cam_Tratamiento; }
            set { _Cam_Tratamiento = value; }
        }

        private bool? _Cam_Bolsa;
        public bool? Cam_Bolsa
        {
            get { return _Cam_Bolsa; }
            set { _Cam_Bolsa = value; }
        }

        private bool? _Cam_Wipers;
        public bool? Cam_Wipers
        {
            get { return _Cam_Wipers; }
            set { _Cam_Wipers = value; }
        }

        private bool? _Cam_Suplementos;
        public bool? Cam_Suplementos
        {
            get { return _Cam_Suplementos; }
            set { _Cam_Suplementos = value; }
        }

        private List<AplicacionCampana> _listaAplicacionCampana;
        public List<AplicacionCampana> ListaAplicacionCampana
        {
            get { return _listaAplicacionCampana; }
            set { _listaAplicacionCampana = value; }
        }
    }
}
