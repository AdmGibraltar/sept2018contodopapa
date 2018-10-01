using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaModelo;
using CapaModelo.SIANCentral;

namespace CapaDatos
{
    public class CD_FabricaContexto
    {
        public static ICD_Contexto CrearDefault(string cadenaDeConexion)
        {
            CD_ContextoDefault cd = new CD_ContextoDefault();
            cd.Contexto = new sianwebmty_gEntities(cadenaDeConexion);
            return cd;
        }

        public static ICD_Contexto CrearParaSIANCentral(string cadenaDeConexion)
        {
            CD_ContextoSIANCentral cd = new CD_ContextoSIANCentral();
            cd.Contexto = new SIANCentralEntities1(cadenaDeConexion);
            return cd;
        }
    }
}
