using TDDSample.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDDSample.repository.interf
{
    /// <summary>
    /// 描述 Student 录取情况
    /// </summary>
    public interface IRecord
    {
        bool IsStudentRecord(Student student);
    }
}
