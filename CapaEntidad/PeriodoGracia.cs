using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
   public class PeriodoGracia
   {
       private string _GUID;

       public string GUID
       {
           get { return _GUID; }
           set { _GUID = value; }
       }
       private int _Reg_Condicion;

       public int Reg_Condicion
       {
           get { return _Reg_Condicion; }
           set { _Reg_Condicion = value; }
       }
       private int _Reg_Periodo;

       public int Reg_Periodo
       {
           get { return _Reg_Periodo; }
           set { _Reg_Periodo = value; }
       }

        
   }
}
