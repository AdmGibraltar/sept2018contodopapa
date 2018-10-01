using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class Informe3
    {
   
        private decimal _A;
        private decimal _P;
        private decimal _X;

        private string _ZonaID;
        public string ZonaID
        {
            get { return _ZonaID; }
            set { _ZonaID = value; }
        }

        private string _UsuarioId;
        public string UsuarioId
        {
            get { return _UsuarioId; }
            set { _UsuarioId = value; }
        }

        private decimal _Monto;
        public decimal Monto
        {
            get { return _Monto; }
            set { _Monto = value; }
        }

        public decimal A
        {
            get { return _A; }
            set { _A= value; }
        }
        public decimal P
        {
            get { return _P; }
            set { _P = value; }
        }

        private decimal _N;
        public decimal N
        {
            get { return _N; }
            set { _N = value; }
        }

        private decimal _C;
        public decimal C
        {
            get { return _C; }
            set { _C = value; }
        }
        public decimal X
        {
            get { return _X; }
            set { _X = value; }
        }

        private string _Representante;
        public string Representante
        {
            get { return _Representante; }
            set { _Representante = value; }
        }

        private string _Zona;
        public string Zona
        {
            get { return _Zona; }
            set { _Zona = value; }
        }        
    }

    public class Informe2
    {
        private string _Fecha;
        private string _Zona;
        private string _ZonaID;
        private string _UsuarioID;
        private string _Representante;

        public string Fecha
        {
            get { return _Fecha; }
            set { _Fecha = value; }
        }
        public string ZonaID
        {
            get { return _ZonaID; }
            set { _ZonaID = value; }
        }
        public string Zona
        {
            get { return _Zona; }
            set { _Zona = value; }
        }
        public string UsuarioID
        {
            get { return _UsuarioID; }
            set { _UsuarioID = value; }
        }

        public string Representante
        {
            get { return _Representante; }
            set { _Representante= value; }
        }
    }

    public class Informe1
    {

        private string _Proyecto;
        private string _Cliente;
        private string _Area;
        private string _Solucion;
        private string _Aplicacion;
        private string _Productos;
        private decimal _VTeorico;
        private string _Analisis;
        private string _Presentacion;
        private string _Negociacion;
        private string _Cierre;
        private string _Cancelacion;
        private decimal _MontoProyecto;
        private string _Comentarios;
        private string _FechaModificacion;
        private string _Estatus;
        private string _ClienteSIANID;
        private string _OportunidadID;
        private string _Rik;
        private string _Nombre;
        private string _Causa;

        public string Proyecto
        {
            get { return _Proyecto; }
            set { _Proyecto = value; }
        }
        public string Cliente
        {
            get { return _Cliente; }
            set { _Cliente = value; }
        }
        public string Area
        {
            get { return _Area; }
            set { _Area = value; }
        }
        public string Solucion
        {
            get { return _Solucion; }
            set { _Solucion = value; }
        }
        public string Aplicacion
        {
            get { return _Aplicacion; }
            set { _Aplicacion = value; }
        }
        public string Productos
        {
            get { return _Productos; }
            set { _Productos = value; }
        }

        public decimal VTeorico
        {
            get { return _VTeorico; }
            set { _VTeorico = value; }
        }

        private decimal _VPO;
        public decimal VPO
        {
            get { return _VPO; }
            set { _VPO = value; }
        }
        
        public string Analisis
        {
            get { return _Analisis; }
            set { _Analisis = value; }
        }
        public string Presentacion
        {
            get { return _Presentacion; }
            set { _Presentacion = value; }
        }
        public string Negociacion
        {
            get { return _Negociacion; }
            set { _Negociacion = value; }
        }
        public string Cierre
        {
            get { return _Cierre; }
            set { _Cierre = value; }
        }
        public string Cancelacion
        {
            get { return _Cancelacion; }
            set { _Cancelacion = value; }
        }
        public decimal MontoProyecto
        {
            get { return _MontoProyecto; }
            set { _MontoProyecto = value; }
        }
        public string Comentarios
        {
            get { return _Comentarios; }
            set { _Comentarios = value; }
        }
        public string FechaModificacion
        {
            get { return _FechaModificacion; }
            set { _FechaModificacion = value; }
        }
        public string Estatus
        {
            get { return _Estatus; }
            set { _Estatus = value; }
        }        
        public string ClienteSIANID
        {
            get { return _ClienteSIANID; }
            set { _ClienteSIANID = value; }
        }
        public string OportunidadID
        {
            get { return _OportunidadID; }
            set { _OportunidadID = value; }
        }
        public string Rik
        {
            get { return _Rik; }
            set { _Rik = value; }
        }
        public string Nombre
        {
            get { return _Nombre; }
            set { _Nombre = value; }
        }
        public string Causa
        {
            get { return _Causa; }
            set { _Causa = value; }
        }

    }

}
