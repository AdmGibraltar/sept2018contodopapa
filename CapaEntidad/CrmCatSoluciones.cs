using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class CrmCatSoluciones
    {
        #region Valores
        int clave;
        string solucion;
        double porcentaje;
        int id;
        string descripcion;
        int uEN;
        int segmento;
        int area;
        string solucion1;
        double porcentaje1;
        #endregion

        #region refactorizacion
        public int Clave
        {
            get { return clave; }
            set { clave = value; }
        }
        public string Solucion
        {
            get { return solucion; }
            set { solucion = value; }
        }
        public double Porcentaje
        {
            get { return porcentaje; }
            set { porcentaje = value; }
        }
        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        public string Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }
        public int UEN
        {
            get { return uEN; }
            set { uEN = value; }
        }
        public int Segmento
        {
            get { return segmento; }
            set { segmento = value; }
        }
        public int Area
        {
            get { return area; }
            set { area = value; }
        }
        public string Solucion1
        {
            get { return solucion1; }
            set { solucion1 = value; }
        }
        public double Porcentaje1
        {
            get { return porcentaje1; }
            set { porcentaje1 = value; }
        }
        #endregion
    }
}
