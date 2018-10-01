using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CapaEntidad;
using CapaDatos;
using System.Data; 

namespace CapaNegocios
{
    public class CN_Rep_VenEstadisticaVentas
    {

        public void ConsultaVentaSem_Territorio(VentaSemanal semanal, string Conexion, ref List<VentaSemanal> List,int Id_Emp, int Id_Cd,  
                                                string Fecha, string Territorio, string Cliente, string Producto,
                                                int Tipo, int NivelCliente, int NivelProducto, int Mov_80)
        {
            try 
            {
                CD_Rep_VenEstadisticaVentas claseCapaDatos = new CD_Rep_VenEstadisticaVentas();
                claseCapaDatos.ConsultaVentaSem_Territorio(semanal, Conexion, ref List, Id_Emp, Id_Cd, Fecha, Territorio, Cliente, Producto
                                                           , Tipo, NivelCliente, NivelProducto, Mov_80);     
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
