using CRM.BLL.DTO;
using CRM.BLL.Interfaces;
using CRM.DAL.EF;
using CRM.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.BLL.Services
{
    public class MailFindService : IMailFindService
    {
        ApiContext db;
        public MailFindService(ApiContext context)
        {
            db = context;
        }
        public async Task<IEnumerable<ContactDTO>> FindContactsForCompany(int CompanyId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ContactDTO>> FindContactsForId(params int[] ContactId)
        {
            List<ContactDTO> contacts = new List<ContactDTO>();
            foreach(int id in ContactId)
            {
                Contact contact = await db.Contacts.FindAsync(id);
                ContactDTO contactDTO = new ContactDTO
                {
                    Id = contact.Id,
                    Email = contact.Email
                };
                contacts.Add(contactDTO);
            }
            return contacts;
        }

        public async Task<IEnumerable<ContactDTO>> GetCompanyContacts(int CompanyId)
        {
            IEnumerable<CompanyContactLink> companyContactLinks = await db.CompanyContactLinks.Where(p => p.CompanyId == CompanyId)
                .Include(p=>p.Contact).ThenInclude(p=>p.Linkedin).ToListAsync();
            List<ContactDTO> contacts = new List<ContactDTO>();
            foreach(var companyContact in companyContactLinks)
            {
                ContactDTO contactDTO = new ContactDTO
                {
                    Id = companyContact.ContactId,
                    Email = companyContact.Contact.Email
                };
                contacts.Add(contactDTO);
            }
            return contacts;
        }

        public async Task<IEnumerable<Email>> SendGroupMailMessage(params int[] ContactId)
        {
            throw new NotImplementedException();
        }
    }
}
