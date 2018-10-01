using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class InvExceso
    {
        #region Variables
        int _Indicador;
        int _id_Cd;
        int _Proveedor;
        int _Sucursal;
        int _Dias;
        int _Periodos;
        int _TipoProductos;
        #endregion

        #region refactorizacion
        public int Indicador
        {
            get { return _Indicador; }
            set { _Indicador = value; }
        }      
        public int Id_Cd
        {
            get { return _id_Cd; }
            set { _id_Cd = value; }
        }
        public int Proveedor
        {
            get { return _Proveedor; }
            set { _Proveedor = value; }
        }
        public int Sucursal
        {
            get { return _Sucursal; }
            set { _Sucursal = value; }
        }
        public int Dias
        {
            get { return _Dias; }
            set { _Dias = value; }
        }
        public int Periodos
        {
            get { return _Periodos; }
            set { _Periodos = value; }
        }
        public int TipoProductos
        {
            get { return _TipoProductos; }
            set { _TipoProductos = value; }
        }      
        #endregion
    }
}
