using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaDatos;
using CapaModelo;
using CapaEntidad;

namespace CapaNegocios
{
    public class CN_CapNotasProspecto
    {
        public CapNotasProspecto Crear(Sesion s, int idCte, string texto)
        {
            CapNotasProspecto result = null;
            CN_CatNotas cnCatNotas = new CN_CatNotas();
            var nuevaNota = cnCatNotas.Crear(s, texto);
            CN_CapNotasProspecto cnCapNotasProspecto = new CN_CapNotasProspecto();
            CD_CapNotasProspecto cdCapNotasProspecto = new CD_CapNotasProspecto();
            result = cdCapNotasProspecto.Insertar(s.Id_Emp, s.Id_Cd, s.Id_Rik, idCte, nuevaNota.Id_Nota, s.Emp_Cnx_EF);
            return result;
        }

        public void Eliminar(Sesion s, CapNotasProspecto capNotasProspecto)
        {
            CD_CapNotasProspecto cdCapNotasProspecto = new CD_CapNotasProspecto();
            cdCapNotasProspecto.Eliminar(capNotasProspecto.Id_Emp, capNotasProspecto.Id_Cd, capNotasProspecto.Id_Rik, capNotasProspecto.Id_Cliente, capNotasProspecto.Id_Nota, s.Emp_Cnx_EF);
            CN_CatNotas cnCatNotas = new CN_CatNotas();
            cnCatNotas.Eliminar(s, capNotasProspecto.Id_Nota);
        }

        public void Actualizar(Sesion s, CapNotasProspecto capNotasProspecto)
        {
            CN_CatNotas cnCatNotas = new CN_CatNotas();
            cnCatNotas.Actualizar(s, new CatNota() { 
                Id_Nota = capNotasProspecto.Id_Nota, 
                Texto = capNotasProspecto.CatNota.Texto 
            });
        }

        public IEnumerable<CapNotasProspecto> ObtenerPorProspecto(Sesion s, int idCte)
        {
            CD_CapNotasProspecto cdCapNotasProspecto = new CD_CapNotasProspecto();
            return cdCapNotasProspecto.ConsultarPorProspecto(s.Id_Emp, s.Id_Cd, s.Id_Rik, idCte, s.Emp_Cnx_EF);
        }

    }
}
