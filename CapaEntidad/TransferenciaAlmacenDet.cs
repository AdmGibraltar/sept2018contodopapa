using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class TransferenciaAlmacenDet
    {
    	
        private int _Id_Emp;
        private int _Id_Cd;
        private int _Id_Trans;
        private int _Id_TransDet;
        private int _Id_Prd ;        
        private int _Trans_Cant;


        public int Id_Emp
        {
            get { return _Id_Emp; }
            set { _Id_Emp = value; }
        }
        public int Id_Cd
        {
            get { return _Id_Cd; }
            set { _Id_Cd = value; }
        }
        public int Id_Trans
        {
            get { return _Id_Trans; }
            set { _Id_Trans = value; }
        }
        public int Id_TransDet
        {
            get { return _Id_TransDet; }
            set { _Id_TransDet = value; }
        }

        public int Id_Prd
        {
            get { return _Id_Prd; }
            set { _Id_Prd = value; }
        }

        public int Trans_Cant
        {
            get { return _Trans_Cant; }
            set { _Trans_Cant = value; }
        }

        private Producto _producto;
        public Producto Producto
        {
            get { return _producto; }
            set { _producto = value; }
        }

        private ProductoPrecios _productoPrecio;
        public ProductoPrecios ProductoPrecio
        {
            get { return _productoPrecio; }
            set { _productoPrecio = value; }
        }
        
       
      
    }
}
