using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using ConsoleApp1.repository.interf;
using TDDSample.model;
using ConsoleApp1.repository.imp;
using TDDSample.repository.interf;

namespace UnitTest
{
    [TestClass]
    public class Unit_ISendMail
    {
        [TestMethod]
        public void Test_GenerateMailBody()
        {
            //Arrange
            var mock = new Mock<ISendMail>();
            
            mock.Setup(x => x.GenderateTemplate(It.Is<Student>(s => s.Score > 600))).Returns("Success");
            mock.Setup(x => x.GenderateTemplate(It.Is<Student>(s => s.Score <= 600))).Returns("Rejected");

            var studentMike = new Student
            {
                Name = "Mick",
                EmailAddr = "aaa@163.com",
                Score = 601
            };

            var studentHarry = new Student
            {
                Name = "Harry",
                EmailAddr = "aaa@163.com",
                Score = 599
            };

            //Act
            var resultSuccess = mock.Object.GenderateTemplate(studentMike);
            var resultRejectd = mock.Object.GenderateTemplate(studentHarry);

            //Assert
            Assert.AreEqual("Success", resultSuccess);
            Assert.AreEqual("Rejected", resultRejectd);
        }

        [TestMethod]
        public void Test_ISendMail()
        {
            //Arrange
            var mock = new Mock<ISendMail>();

            mock.Setup(m => m.SendMailTo(null)).Throws(new NullReferenceException("Student could not be null"));
            mock.Setup(m => m.SendMailTo(It.Is<Student>(s => string.IsNullOrEmpty(s.Name) || string.IsNullOrEmpty(s.EmailAddr))))
                .Throws(new NullReferenceException());

            var studentMike = new Student
            {
                Name = "Mick",
                EmailAddr = "aaa@163.com",
                Score = 601
            };

            var studentNameNull = new Student
            {
                Name = null,
                EmailAddr = "aaa@163.com",
                Score = 601
            };

            var studentEmailNull = new Student
            {
                Name = "Ok",
                EmailAddr = null,
                Score = 601
            };

            //Act
            mock.Object.SendMailTo(studentMike);
            
            //Assert
            mock.Verify(m => m.SendMailTo(It.Is<Student>(s => s.Name != null && s.EmailAddr != null)));
            Assert.ThrowsException<NullReferenceException>(() => mock.Object.SendMailTo(null));
            Assert.ThrowsException<NullReferenceException>(() => mock.Object.SendMailTo(studentNameNull));
            Assert.ThrowsException<NullReferenceException>(() => mock.Object.SendMailTo(studentEmailNull));
        }

        [TestMethod]
        public void Test_SendMail_Imp()
        {
            //Arrange
            var mock = new Mock<IRecord>();
            var sendMail = new SendMail(mock.Object);

            var studentMike = new Student
            {
                Name = "Mick",
                EmailAddr = "aaa@163.com",
                Score = 601
            };

            var studentNameNull = new Student
            {
                Name = null,
                EmailAddr = "aaa@163.com",
                Score = 601
            };

            var studentEmailNull = new Student
            {
                Name = "Ok",
                EmailAddr = null,
                Score = 601
            };

            //Act
            sendMail.SendMailTo(studentMike);

            //Assert
            mock.Verify(x => x.IsStudentRecord(It.IsAny<Student>()));
            Assert.ThrowsException<NullReferenceException>(() => sendMail.SendMailTo(studentNameNull));
            Assert.ThrowsException<NullReferenceException>(() => sendMail.SendMailTo(studentEmailNull));
        }

        [TestMethod]
        public void Test_Generate_Success_Tempalate()
        {
            //Arrange
            var mock = new Mock<IRecord>();
            var smock = new Mock<ISendMail>();

            smock.Setup(s => s.GenderateTemplate(It.Is<Student>(st => st.Score >= 600))).Returns("Success");

            //Arange
            var studentMike = new Student
            {
                Name = "Mick",
                EmailAddr = "aaa@163.com",
                Score = 601
            };

            //Assert

            Assert.AreEqual("Success", smock.Object.GenderateTemplate(studentMike));

        }

        [TestMethod]
        public void Test_Generate_Reject_Tempalate()
        {
            //Arrange
            var mock = new Mock<IRecord>();
            var smock = new Mock<ISendMail>();

            smock.Setup(s => s.GenderateTemplate(It.Is<Student>(st => st.Score < 600))).Returns("Failed");

            //Arange
            var studentMike = new Student
            {
                Name = "Mick",
                EmailAddr = "aaa@163.com",
                Score = 599
            };

            //Assert
            Assert.AreEqual("Failed", smock.Object.GenderateTemplate(studentMike));

        }


        [TestMethod]
        public void Test_Generate_Reject_Tempalate_With_Record_Flag()
        {
            //Arrange
            var mock = new Mock<IRecord>();
            mock.Setup(m => m.IsStudentRecord(It.Is<Student>(s => s.Score < 600)))
             .Returns(false);

            var studentMike = new Student
            {
                Name = "Mick",
                EmailAddr = "aaa@163.com",
                Score = 599
            };

            var smock = new SendMail(mock.Object);

            //Action
            var content = smock.GenderateTemplate(studentMike);

            //Assert
            mock.Verify(o => o.IsStudentRecord(studentMike));
            Assert.AreEqual("Failed", content);
        }


        [TestMethod]
        public void Test_Generate_Success_Tempalate_With_Record_Flag()
        {
            //Arrange
            var mock = new Mock<IRecord>();
            mock.Setup(m => m.IsStudentRecord(It.Is<Student>(s => s.Score >= 600)))
                .Returns(true);

            var studentMike = new Student
            {
                Name = "Mick",
                EmailAddr = "aaa@163.com",
                Score = 600
            };

            var smock = new SendMail(mock.Object);

            //Action
            var content = smock.GenderateTemplate(studentMike);

            //Assert
            mock.Verify(o => o.IsStudentRecord(studentMike));
            Assert.AreEqual("Success", content);
        }
    }
}
