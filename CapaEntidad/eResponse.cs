using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class eResponse<T>
    {            
            /// <summary>
            /// Indica el estatus con el que responde la aplicacion.
            /// -1 - Error
            ///  0 - No se cumple alguna regla
            ///  1 - Todo OK
            ///  2 - Otro motivo por el cual no se puede llevar a cabo el proceso.
            /// </summary>
            public int Estado { get; set; }

            /// <summary>
            /// Mensaje de error para el usuario. Solo se llena cuando el Estado es diferente a 1.
            /// </summary>
            public string Mensaje { get; set; }

            /// <summary>
            /// Mensaje de error mas tecnico para el desarrollador o el equipo de soporte.
            /// Solo se llena cuando el Estado es diferente a 1.
            /// </summary>
            public string MensajeTecnico { get; set; }

            /// <summary>
            /// Datos que se devuelven, si todo sale bien.
            /// </summary>
            public T Datos { get; set; }
    }
}
