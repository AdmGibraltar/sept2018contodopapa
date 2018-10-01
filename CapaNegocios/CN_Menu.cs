using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace CapaNegocios
{
    public class CN_Menu
    {
        public void LlenarMenu(string conexion, ref DataTable dt, Int32 Id_Cd, Int32 ID_U)
        {
            try
            {
                CapaDatos.CD_Menu claseCapaDatos = new CapaDatos.CD_Menu(conexion);
                claseCapaDatos.LlenarMenu(ref dt, Id_Cd, ID_U);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
