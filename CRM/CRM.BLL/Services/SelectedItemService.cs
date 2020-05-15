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
    public class SelectedItemService : ISelectedItemService
    {
        ICompanyService companyServ;
        IMailFindService mailFindServ;
        ApiContext db;
        public SelectedItemService(ICompanyService companyService, IMailFindService mailFindService, ApiContext context)
        {
            companyServ = companyService;
            mailFindServ = mailFindService;
            db = context;
        }
        public IEnumerable<CompanyDTO> NewCompanies { get; set; }
        public IEnumerable<CompanyDTO> QualifiedCompanies { get; set; }
        public IEnumerable<CompanyDTO> NotQualifiedCompanies { get; set; }
        public IEnumerable<CompanyDTO> AllCompanies { get; set; }
        public IEnumerable<ContactDTO> Contacts { get; set; }
        public IEnumerable<CompanyContactLink> CompanyContactLinks { get; set; }
        public IEnumerable<Linkedin> Linkedins { get; set; }

        public int Id { get; set; }
        public void SetId (int Id)
        {
            this.Id = Id;
        }
        public int GetId()
        {
            return Id;
        }
        public async Task UpdateCompanies()
        {
            NewCompanies = await companyServ.GetNewCompanies();
            QualifiedCompanies = await companyServ.GetQualifiedCompanies();
            NotQualifiedCompanies = await companyServ.GetNotQualifiedCompanies();
            AllCompanies = await companyServ.GetCompanies();
            Contacts = await mailFindServ.GetAllContacts();
            CompanyContactLinks = await mailFindServ.GetCompanyContactLinks();
            Linkedins = await db.Linkedins.ToListAsync();
        }
        public async Task<IEnumerable<ContactDTO>> GetCompanyContacts(int CompanyId)
        {
            IEnumerable<CompanyContactLink> _companyContactLinks = CompanyContactLinks.Where(p=>p.CompanyId==CompanyId);
            List<ContactDTO> contacts = new List<ContactDTO>();
            foreach (var companyContact in _companyContactLinks)
            {
                ContactDTO contactDTO = await mailFindServ.Map(companyContact.Contact);
                contacts.Add(contactDTO);
            }
            return contacts;
        }

    }
}
