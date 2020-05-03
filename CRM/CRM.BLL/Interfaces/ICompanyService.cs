using CRM.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CRM.BLL.Interfaces
{
    public interface ICompanyService
    {
        Task<CompanyRegistrationDTO> CreateCompany(CompanyRegistrationDTO CompanyRegistrationDTO);
        Task<CompanyDTO> GetCompany(int CompanyId);
        Task<IEnumerable<CompanyDTO>> GetCompanies();
        Task<bool> DeleteCompany(int CompanyId);
        Task<CompanyRegistrationDTO> EditCompany(CompanyRegistrationDTO CompanyDTO);
        Task<IEnumerable<CompanyDTO>> GetRegionCompanies(int RegionId);
        Task<IEnumerable<CompanyDTO>> GetCountryCompanies(int CountryId);
        Task<CompanyDTO> AppointLead(AppointCompanyLeadDTO companyLeadDTO);
        Task<CompanyDTO> QualifyCompany(QualifyCompanyDTO QualifyCompanyDTO);
    }
}
