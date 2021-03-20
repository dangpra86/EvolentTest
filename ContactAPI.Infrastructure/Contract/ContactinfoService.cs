using ContactAPI.Infrastructure.Common;
using ContactAPI.Infrastructure.Interface;
using ContactAPI.Infrastructure.Model;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace ContactAPI.Infrastructure.Contract
{
    public class ContactinfoService : IContactinfoService
    {
        private readonly ILogger<ContactinfoService> _logger;
        public ContactinfoService(ILogger<ContactinfoService> logger)
        {
            _logger = logger;
        }       

        public Contact GetContactByID(int id)
        {
            _logger.LogInformation(">> GetContactByID ");
            Contact contactInfo = null;
            try
            {                
                SqlParameter[] parameters = new SqlParameter[] {new SqlParameter() {
                    ParameterName = "@ID",
                    SqlDbType = SqlDbType.Int,
                    Value = id
            } };
                DataSet dtContactInfo = Utils.ExecuteDataset("dbo.GetContactsByID", parameters);
                if (dtContactInfo != null)
                {
                    DataRow dr = dtContactInfo.Tables[0].Rows[0];
                    contactInfo = GetContactInfoByRow(dr);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error while Getting contact by ID" + ex.ToString());
            }
            _logger.LogInformation("<< GetContactByID ");
            return contactInfo;
        }

        public List<Contact> GetAllContacts()
        {
            _logger.LogInformation(">> GetAllContacts ");

            List<Contact> contactInfos = null;
            try
            {
               
                SqlParameter[] parameters = new SqlParameter[] {new SqlParameter() {
                    ParameterName = "@ID",
                    SqlDbType = SqlDbType.Int,
                    Value = 0
            } };
                DataSet dtContactInfo = Utils.ExecuteDataset("dbo.GetAllContacts", parameters);
                if (dtContactInfo != null)
                {
                    contactInfos = new List<Contact>();
                    foreach (DataRow dr in dtContactInfo.Tables[0].Rows)
                        contactInfos.Add(GetContactInfoByRow(dr));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error while Getting All Contacts" + ex.ToString());
            }
            _logger.LogInformation("<< GetAllContacts ");
            return contactInfos;
        }

        public int Remove(int id)
        {
            _logger.LogInformation(">> Remove ");

            int result = 0;
            try
            {
                SqlParameter[] parameters = new SqlParameter[] {new SqlParameter() {
                    ParameterName = "@ID",
                    SqlDbType = SqlDbType.Structured,
                    Value = id
            } };

                 result = Utils.ExecuteNonQuery("dbo.DeleteContact", parameters);
            }
            catch (Exception ex)
            {

                _logger.LogError("Error while Deleting Contacts" + ex.ToString());
            }

            _logger.LogInformation("<< Remove ");

            return result;
        }

        public int Update(List<Contact> contact)
        {
            _logger.LogInformation(">> Update ");

            int result = 0;
            try
            {
                DataTable ContactTable = Utils.GetContactTable(contact);
                SqlParameter[] parameters = new SqlParameter[] {new SqlParameter() {
                    ParameterName = "@TBLContact",
                    SqlDbType = SqlDbType.Structured,
                    Value = ContactTable
            } };

                 result = Utils.ExecuteNonQuery("dbo.UpdateContactList", parameters);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error while Updating Contacts" + ex.ToString());
            }

            _logger.LogInformation("<< Update ");
            return result;
        }
        // code detail is below          

        public int AddContactList(List<Contact> contact)
        {
            _logger.LogInformation(">> AddContactList ");
            int result = 0;
            try
            {
                DataTable ContactTable = Utils.GetContactTable(contact);
                SqlParameter[] parameters = new SqlParameter[] {new SqlParameter() {
                    ParameterName = "@TBLContact",
                    SqlDbType = SqlDbType.Structured,
                    Value = ContactTable
            } };

                 result = Utils.ExecuteNonQuery("dbo.AddContactList", parameters);
            }
            catch (Exception ex)
            {

                _logger.LogError("Error while Creating new Contacts" + ex.ToString());
            }

            _logger.LogInformation("<< AddContactList ");
            return result;
        }        

        private Contact GetContactInfoByRow(DataRow dr)
        {
            Contact contactInfo = new Contact();
            contactInfo.firstname = dr["firstname"].ToString();
            contactInfo.lastname = dr["lastname"].ToString();
            contactInfo.email = dr["email"].ToString();
            contactInfo.activeStatus = Convert.ToBoolean(dr["activeStatus"].ToString());
            contactInfo.contactId = Convert.ToInt32(dr["Id"]);
            return contactInfo;
        }
           
    }

}

