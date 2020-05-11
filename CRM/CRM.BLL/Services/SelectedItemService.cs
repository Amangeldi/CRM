using CRM.BLL.DTO;
using CRM.BLL.Interfaces;
using CRM.DAL.EF;
using CRM.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CRM.BLL.Services
{
    public class SelectedItemService : ISelectedItemService
    {
        ICompanyService companyServ;
        public SelectedItemService(ICompanyService companyService)
        {
            companyServ = companyService;
        }
        public IEnumerable<CompanyDTO> NewCompanies { get; set; }
        public IEnumerable<CompanyDTO> QualifiedCompanies { get; set; }
        public IEnumerable<CompanyDTO> NotQualifiedCompanies { get; set; }
        public IEnumerable<CompanyDTO> AllCompanies { get; set; }
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
        }
    }
}
