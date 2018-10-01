using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CapaEntidad;
using CapaDatos;
using System.Data;
namespace CapaNegocios
{
    public class CN_PolizaContable
    {

        public void PolizaAmortizacionSistemasPropietarios (
                int Id_Cd, 
                int Orden, 
                int NivelRik,
                int NivelTer,
                int NivelCte, 
                int Mes, 
                int Anio, 
                String Conexion, 
                ref String NombreArchivo)
        {

            try
            {
                CD_PolizaContable claseCapaDatos = new CD_PolizaContable();

                claseCapaDatos.PolizaAmortizacionSistemasPropietarios(
                Id_Cd, 
                Orden, 
                NivelRik,
                NivelTer,
                NivelCte,
                Mes, 
                Anio, 
                Conexion, 
                ref NombreArchivo
            );
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }


    }
}
