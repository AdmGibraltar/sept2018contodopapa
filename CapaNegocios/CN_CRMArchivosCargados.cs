using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaModelo;
using CapaDatos;
using CapaEntidad;

namespace CapaNegocios
{
    public class CN_CRMArchivosCargados
    {
        public int Crear(Sesion sesion, int IdDocumento, int IdDocTipo, string NombreArchivo, string Hash, int Id_U)
        {
            int Result = 0;
            
            CD_CRMArchivosCargados cd = new CD_CRMArchivosCargados();
            Result = cd.CRMArchivosCargados_Insert(sesion.Id_Emp, sesion.Id_Cd, NombreArchivo, Hash, IdDocumento, 
                IdDocTipo, Id_U, sesion.Emp_Cnx);

            return Result;            
        }

        private static Random random = new Random();

        public string CrearCadenaHash()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, 64)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        //
    }
}
