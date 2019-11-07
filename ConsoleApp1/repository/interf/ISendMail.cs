using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDDSample.model;

namespace ConsoleApp1.repository.interf
{
    /// <summary>
    /// 提供发送邮件的方法
    /// </summary>
    public interface ISendMail
    {
        string GenderateTemplate(Student student);
        void SendMailTo(Student student);
    }
}
