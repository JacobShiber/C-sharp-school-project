using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradeConsoleApp
{
    class Teacher
    {
        string teacherName;
        public string studentName;
        public int studentId;
        public List<int> grades;

        public Teacher(string _teacherName, string _studentName, int _studentId, List<int> _grades)
        {
            this.teacherName = _teacherName;
            this.studentName = _studentName;
            this.studentId = _studentId;
            this.grades = _grades;
        }
    }
}
