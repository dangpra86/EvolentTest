using System;
using System.Collections.Generic;
using System.Text;

namespace ContactAPI.Infrastructure.Model
{
    public class Contact
    {
        public int contactId { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string email { get; set; }
        public bool activeStatus { get; set; }
    }
   
}
