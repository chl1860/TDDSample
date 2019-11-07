using ConsoleApp1.repository.interf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDDSample.model;
using TDDSample.repository.interf;

namespace ConsoleApp1.repository.imp
{
    public class Record : IRecord
    {
  
        public bool IsStudentRecord(Student student)
        {
            if (student == null || student.Name == null)
            {
                throw new NullReferenceException(nameof(Student));
            }
            if(student.Score >= 600)
            {
                return true;
            }
            return false;
        }

    }
}
