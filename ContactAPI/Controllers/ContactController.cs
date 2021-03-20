using ContactAPI.Infrastructure.Interface;
using ContactAPI.Infrastructure.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContactinfoService _contactinfoservice;
        private readonly ILogger<ContactController> _logger;
        public ContactController(IContactinfoService contactinfoservice, ILogger<ContactController> logger)
        {
            _contactinfoservice = contactinfoservice;
            _logger = logger;
        }
        
        [HttpGet]
        [Route("GetAllContacts")]
        public IEnumerable<Contact> GetContactInfos() => _contactinfoservice.GetAllContacts();


        [HttpGet]
        [Route("GetContactsByid")]
        public IActionResult GetContactInfoById(int id)
        {            
            Contact contactinfo = _contactinfoservice.GetContactByID(id);
            if (contactinfo == null)
            {
                return NotFound();
            }
            else
            {
                return new ObjectResult(contactinfo);
            }           
        }

        [HttpPost]
        [Route("AddContact")]
        public IActionResult PostContactInfo([FromBody] List<Contact> contactinfo)
        {
            if (contactinfo == null) return BadRequest();
            int retVal = _contactinfoservice.AddContactList(contactinfo);
            if (retVal > 0) return Ok(); else return NotFound();
        }

        [HttpPut]
        [Route("UpdateContact")]
        public IActionResult PutContactInfo(int id, [FromBody] List<Contact> contactinfo)
        {
            if (contactinfo == null || id != contactinfo[0].contactId) return BadRequest();
            if (_contactinfoservice.GetContactByID(id) == null) return NotFound();
            int retVal = _contactinfoservice.Update(contactinfo);
            if (retVal > 0) return Ok(); else return NotFound();
        }

        [HttpDelete]
        [Route("DeleteContact")]
        public IActionResult Delete(int id)
        {
            int retVal = _contactinfoservice.Remove(id);
            if (retVal > 0) return Ok(); else return NotFound();
        }
    }






}

