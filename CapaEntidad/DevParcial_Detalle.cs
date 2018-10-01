using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class DevParcial_Detalle
    {
        int numero;
        int tipoDev;
        int factura;
        int tipoMovimiento;
        int Cliente;
        int territorio;
        int representante;
        double descuento;
        double descuento2;
        string desc;
        string desc2;
        string notas;
        string fac_Total2;
        int idProd;
        int dev_Cant;
        double dev_Precio;
        double dev_Importe;
        int nota;
        DateTime fecha_dev;
        DateTime _Fecha_devHr;
        DateTime fecha_Fac;
        double importe;
        double subtotal;
        double iva;
        double total;
        bool devuelto;
        int cantDevuelta;
        double totalImporte;
        int id_u;


        public double TotalImporte
        {
            get { return totalImporte; }
            set { totalImporte = value; }
        }


        public string Fac_Total2
        {
            get { return fac_Total2; }
            set { fac_Total2 = value; }
        }

        public int CantDevuelta
        {
            get { return cantDevuelta; }
            set { cantDevuelta = value; }
        }

        public int Id_U
        {
            get { return id_u; }
            set { id_u = value; }
        }

        public bool Devuelto
        {
            get { return devuelto; }
            set { devuelto = value; }
        }

        public int Numero
        {
            get { return numero; }
            set { numero = value; }
        }        

        public int TipoDev
        {
            get { return tipoDev; }
            set { tipoDev = value; }
        }        

        public int Factura
        {
            get { return factura; }
            set { factura = value; }
        }        

        public int TipoMovimiento
        {
            get { return tipoMovimiento; }
            set { tipoMovimiento = value; }
        }        

        public int Cliente1
        {
            get { return Cliente; }
            set { Cliente = value; }
        }        

        public int Territorio
        {
            get { return territorio; }
            set { territorio = value; }
        }        

        public int Representante
        {
            get { return representante; }
            set { representante = value; }
        }

        public double Descuento
        {
            get { return descuento; }
            set { descuento = value; }
        }

        public double Descuento2
        {
            get { return descuento2; }
            set { descuento2 = value; }
        }

        public string Desc
        {
            get { return desc; }
            set { desc = value; }
        }

        public string Desc2
        {
            get { return desc2; }
            set { desc2 = value; }
        }        

        public string Notas
        {
            get { return notas; }
            set { notas = value; }
        }

        public int IdProd
        {
            get { return idProd; }
            set { idProd = value; }
        }
        
        public int Dev_Cant
        {
            get { return dev_Cant; }
            set { dev_Cant = value; }
        }

        public double Dev_Precio
        {
            get { return dev_Precio; }
            set { dev_Precio = value; }
        }

        public double Dev_Importe
        {
            get { return dev_Importe; }
            set { dev_Importe = value; }
        }

        public int Nota
        {
            get { return nota; }
            set { nota = value; }
        }        

        public DateTime Fecha_dev
        {
            get { return fecha_dev; }
            set { fecha_dev = value; }
        }

        public DateTime Fecha_devHr
        {
            get { return _Fecha_devHr; }
            set { _Fecha_devHr = value; }
        }   

        public DateTime Fecha_Fac
        {
            get { return fecha_Fac; }
            set { fecha_Fac = value; }
        }        

        public double Importe
        {
            get { return importe; }
            set { importe = value; }
        }
        
        public double Subtotal
        {
            get { return subtotal; }
            set { subtotal = value; }
        }
        
        public double Iva
        {
            get { return iva; }
            set { iva = value; }
        }
        
        public double Total
        {
            get { return total; }
            set { total = value; }
        }


        private DateTime? _Dev_FecCan;
        public DateTime? Dev_FecCan
        {
            get { return _Dev_FecCan; }
            set { _Dev_FecCan = value; }
        }
    }
}
