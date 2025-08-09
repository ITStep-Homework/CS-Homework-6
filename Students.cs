using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace homework6
{
  public partial class Student
  {
    private static long next_id = 1;
    public long   id { get; }
    public string FIO { get; set; }
    public string Group { get; set; }
    
    private double avrg_mark;
    public double AvrgMark
    {
      get { return avrg_mark; }
      set {
        if (value >= 0 && value <= 12) { avrg_mark = value; }
        else {
          MessageBox.Show("Неверный ср. балл!",
            "Ошибка ввода",
            MessageBoxButtons.OK,
            MessageBoxIcon.Error);
        }
      }
    }

    public Student()
    {
      id = Student.next_id++;
    }

    public Student( string fio, string group,
      double aver_mark ) : this()  // id = Student.next_id++;
    {
      FIO = fio;
      Group = group;
      AvrgMark = aver_mark;
    }

    public override string ToString()
    {
      return $"{id}: {FIO}; {Group}; {AvrgMark:N2}";
    }
  } // class Student;
}

