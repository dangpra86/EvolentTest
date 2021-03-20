using ContactAPI.Controllers;
using ContactAPI.Infrastructure.Interface;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace ContactUnitTest
{
    public class Tests
    {
        Mock<IContactinfoService> mock;
        Mock<ILogger<ContactController>> logger;

        [SetUp]
        public void Setup()
        {
             mock = new Mock<IContactinfoService>();
             logger = new Mock<ILogger<ContactController>>();
        }

        [Test]
        public void Add_Contact_Test()
        {
            //Define         

            var contactlist = new List<ContactAPI.Infrastructure.Model.Contact>() {
                new ContactAPI.Infrastructure.Model.Contact { activeStatus = true, contactId = 1, email = "ABC@XYZ.COM", firstname = "ABC", lastname = "XYZ" }
            };

            //Setup            
            mock.Setup(p => p.AddContactList(contactlist)).Returns(1);
            ContactController contact = new ContactController(mock.Object, logger.Object);
            var result = contact.PostContactInfo(contactlist);

            //Assert
            Assert.AreNotEqual(null, result);
        }

        [Test]
        public void Delete_Contact_Test()
        {
           
            //Setup            
            mock.Setup(p => p.Remove(1)).Returns(1);
            ContactController contact = new ContactController(mock.Object, logger.Object);
            var result = contact.Delete(1);

            //Assert
            Assert.AreNotEqual(null, result);
        }

        [Test]
        public void Update_Contact_Test()
        {
       
            //Setup
            var contactlist = new List<ContactAPI.Infrastructure.Model.Contact>() {
                new ContactAPI.Infrastructure.Model.Contact { activeStatus = true, contactId = 1, email = "ABC@XYZ.COM", firstname = "ABC", lastname = "XYZ" }
            };
            mock.Setup(p => p.Update(contactlist)).Returns(1);
            ContactController contact = new ContactController(mock.Object, logger.Object);
            var result = contact.PutContactInfo(1, contactlist);

            //Assert
            Assert.AreNotEqual(null, result);
        }

        [Test]
        public void Get_Contact_Test()
        {        

            //Setup            
            mock.Setup(p => p.GetContactByID(1)).Returns(new ContactAPI.Infrastructure.Model.Contact {activeStatus = true,contactId =1,email ="ABC@XYZ.COM",firstname ="ABC",lastname="XYZ" });
            ContactController contact = new ContactController(mock.Object,logger.Object);
            var result = contact.GetContactInfoById(1);

            //Assert
            Assert.AreNotEqual(null, result);

        }

    }
}