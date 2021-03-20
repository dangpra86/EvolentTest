using ContactAPI.Infrastructure.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContactAPI.Infrastructure.Interface
{
    public interface IContactinfoService
    {
        int AddContactList(List<Contact> contact);
        List<Contact> GetAllContacts();
        Contact GetContactByID(int id);
        int Remove(int id);
        int Update(List<Contact> contact);
    }
}
