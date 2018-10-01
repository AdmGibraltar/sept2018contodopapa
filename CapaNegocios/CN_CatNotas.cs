using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaDatos;
using CapaEntidad;
using CapaModelo;

namespace CapaNegocios
{
    public class CN_CatNotas
    {
        public CatNota Crear(Sesion s, string texto)
        {
            CatNota resultado = null;
            CD_CatNotas cdCatNotas = new CD_CatNotas();
            resultado = cdCatNotas.Insertar(texto, s.Emp_Cnx_EF);
            return resultado;
        }

        public void Eliminar(Sesion s, int idNota)
        {
            CD_CatNotas cdCatNotas = new CD_CatNotas();
            cdCatNotas.Eliminar(idNota, s.Emp_Cnx_EF);
        }

        public void Actualizar(Sesion s, CatNota nota)
        {
            CD_CatNotas cdCatNotas = new CD_CatNotas();
            cdCatNotas.Actualizar(nota.Id_Nota, nota.Texto, s.Emp_Cnx_EF);
        }
    }
}
