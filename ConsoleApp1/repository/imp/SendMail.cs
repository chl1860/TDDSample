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
    public class SendMail : ISendMail
    {
        private readonly IRecord _iRecord;
        public SendMail(IRecord iRecord)
        {
            _iRecord = iRecord;
        }
        public string GenderateTemplate(Student student)
        {
            var isReord = _iRecord.IsStudentRecord(student);
            if(isReord)
            {
                return "Success";
            }
            else
            {
                return "Failed";
            }
        }

        
        public void SendMailTo(Student student)
        {
            try
            {
                if (student == null || string.IsNullOrEmpty(student.Name) || string.IsNullOrEmpty(student.EmailAddr))
                {
                    throw new NullReferenceException(nameof(SendMail));
                }
                var body = GenderateTemplate(student);
                Console.Write(body);
            }catch(NullReferenceException nullEx)
            {
                throw nullEx;
            }
        }
    }
}
