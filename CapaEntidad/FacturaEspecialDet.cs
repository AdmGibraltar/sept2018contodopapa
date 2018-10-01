using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class FacturaEspecialDet
    {
        int _Id_Prd;

        public int Id_Prd
        {
            get { return _Id_Prd; }
            set { _Id_Prd = value; }
        }
        string _FacEsp_Descripcion;

        public string FacEsp_Descripcion
        {
            get { return _FacEsp_Descripcion; }
            set { _FacEsp_Descripcion = value; }
        }
        string _FacEsp_Presentacion;

        public string FacEsp_Presentacion
        {
            get { return _FacEsp_Presentacion; }
            set { _FacEsp_Presentacion = value; }
        }
        string _FacEsp_Unidades;

        public string FacEsp_Unidades
        {
            get { return _FacEsp_Unidades; }
            set { _FacEsp_Unidades = value; }
        }
        string _FacEsp_Release;

        public string FacEsp_Release
        {
            get { return _FacEsp_Release; }
            set { _FacEsp_Release = value; }
        }
        int _Fac_Cant;

        public int Fac_Cant
        {
            get { return _Fac_Cant; }
            set { _Fac_Cant = value; }
        }
        float _Fac_Precio;

        public float Fac_Precio
        {
            get { return _Fac_Precio; }
            set { _Fac_Precio = value; }
        }
    }
}
