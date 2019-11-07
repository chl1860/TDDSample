using System;
using System.Collections.Generic;
using ConsoleApp1.repository.imp;
using ConsoleApp1.repository.interf;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TDDSample.model;
using TDDSample.repository;
using TDDSample.repository.interf;

namespace UnitTest
{
    [TestClass]
    public class Uniti_Record
    {
        [TestMethod]
        public void Test_IRecord()
        {
            //Arrange
            var recordMock = new Mock<IRecord>();
            var sendMailMock = new Mock<ISendMail>();
            var list = new Mock<List<Student>>();

            var studentMike = new Student
            {
                Name = "Mick",
                EmailAddr = "aaa@163.com",
                Score = 601
            };

            var studentHarry = new Student
            {
                Name = null,
                EmailAddr = "aaa@163.com",
                Score = 601
            };


            recordMock.Setup(r => r.IsStudentRecord(It.Is<Student>(s=>s.Score >= 600)))
                .Returns(true);

            recordMock.Setup(r => r.IsStudentRecord(It.Is<Student>(s=>s.Score < 600)))
                .Returns(false);

            recordMock.Setup(r => r.IsStudentRecord(null)).Throws<NullReferenceException>();
            recordMock.Setup(r => r.IsStudentRecord(It.Is<Student>(s => s.Name == null)))
                .Throws(new NullReferenceException("Student Name could not be null"));

            //Act
            var result = recordMock.Object.IsStudentRecord(studentMike);
            
            //Assert
            Assert.AreEqual(true, result);
            Assert.ThrowsException<NullReferenceException>(() => recordMock.Object.IsStudentRecord(null));
            Assert.ThrowsException<NullReferenceException>(() => recordMock.Object.IsStudentRecord(studentHarry));
        }



    }
}
