using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.DAL.Entities
{
    public class Contact
    {
        public int Id { get; set; }
        public string Email { get; set; }

        public List<Email> Emails { get; set; }
        public List<CompanyContactLink> CompanyContactLinks { get; set; }
    }
}
