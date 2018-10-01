using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
  public  class Solucion
  {
      private int _Id_Emp;

      public int Id_Emp
      {
          get { return _Id_Emp; }
          set { _Id_Emp = value; }
      }
      private int _Id_Sol;

      public int Id_Sol
      {
          get { return _Id_Sol; }
          set { _Id_Sol = value; }
      }
      private int _Id_Area;

      public int Id_Area
      {
          get { return _Id_Area; }
          set { _Id_Area = value; }
      }
      private string _Sol_Descripcion;

      public string Sol_Descripcion
      {
          get { return _Sol_Descripcion; }
          set { _Sol_Descripcion = value; }
      }
      private int _Id_Seg;

      public int Id_Seg
      {
          get { return _Id_Seg; }
          set { _Id_Seg = value; }
      }
      private int _Id_Uen;

      public int Id_Uen
      {
          get { return _Id_Uen; }
          set { _Id_Uen = value; }
      }
      private double _Sol_Potencial;

      public double Sol_Potencial
      {
          get { return _Sol_Potencial; }
          set { _Sol_Potencial = value; }
      }
      private bool _Estatus;

      public bool Estatus
      {
          get { return _Estatus; }
          set { _Estatus = value; }
      }
      private string _EstatusStr;

      public string EstatusStr
      {
          get { return _EstatusStr; }
          set { _EstatusStr = value; }
      }
    }
}
