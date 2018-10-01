using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaModelo;
using CapaDatos;
using CapaEntidad;
using System.IO;

namespace CapaNegocios
{
    public class CN_CatRecursoArchivo
    {
        /// <summary>
        /// Crea un flujo de datos hacia el recurso de archivo físico.
        /// </summary>
        /// <param name="sesion">Sesión del usuario en operación</param>
        /// <param name="idRecurso">Identificador del recurso</param>
        /// <param name="ibt">Transacción de capa de negocio</param>
        /// <returns>FileStream</returns>
        public InfoArchivoAbierto AbrirArchivo(Sesion sesion, int idRecurso, IBusinessTransaction ibt)
        {
            CD_CatRecursoArchivo cdCatRecursoArchivo = new CD_CatRecursoArchivo();
            var catRecursoArchivo = cdCatRecursoArchivo.ConsultarPorIdentificador(sesion.Id_Emp, sesion.Id_Cd, idRecurso, ibt.DataContext);
            FileStream fs = File.OpenRead(catRecursoArchivo.RecArc_Nombre);//new FileStream(catRecursoArchivo.RecArc_Nombre, FileMode.Open, FileAccess.Read);
            return new InfoArchivoAbierto() { Fs=fs, CatRecursoArchivo=catRecursoArchivo };
        }

        public class InfoArchivoAbierto 
            : IDisposable
        {
            public FileStream Fs
            {
                get;
                set;
            }

            public CatRecursoArchivo CatRecursoArchivo
            {
                get;
                set;
            }

            public void Dispose()
            {
                if (Fs != null)
                {
                    Fs.Dispose();
                }
            }
        }
    }
}
