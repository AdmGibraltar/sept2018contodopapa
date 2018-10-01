using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CapaEntidad;
using CapaDatos;
using System.Data;
namespace CapaNegocios
{


   public class CN_GestionRentabilidadSeguimiento
    {
        
        public void ConsultaGestionRentabilidadSeguimiento_Buscar(GestionRentabilidadSeguimiento gestionRentabilidadseguimiento, string Conexion, ref List<GestionRentabilidadSeguimiento> List		    
            , int Id_Emp
            , int Id_Cd
            , int Id_Cte
            , int Id_Ter
            )
        {  
            try
            {
                CD_GestionRentabilidadSeguimiento claseCapaDatos = new CD_GestionRentabilidadSeguimiento();
                claseCapaDatos.ConsultaGestionRentabilidadSeguimiento_Buscar(gestionRentabilidadseguimiento, Conexion, ref List           
            , Id_Emp
            , Id_Cd
            , Id_Cte
            , Id_Ter
            );
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
