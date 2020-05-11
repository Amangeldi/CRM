using AutoMapper;
using CRM.BLL.DTO;
using CRM.BLL.Interfaces;
using CRM.RAZOR.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.RAZOR.Mappings
{
    public class CompanyMap
    {
        readonly ICompanyService companyServ;
        readonly ICountryService countryServ;
        readonly IUserRegistrationService userRegistrationServ;
        readonly IQualificationService qualificationServ;
        private readonly IServiceScopeFactory serviceScopeFactory;
        public CompanyMap(ICompanyService companyService, ICountryService countryService,
            IUserRegistrationService userRegistrationService, IQualificationService qualificationService,
            IServiceScopeFactory serviceScopeFactory)
        {
            userRegistrationServ = userRegistrationService;
            qualificationServ = qualificationService;
            countryServ = countryService;
            companyServ = companyService;
            this.serviceScopeFactory = serviceScopeFactory;
        }
        public IEnumerable<CompanyModel> companyModels;
        public async Task UpdateCompanies()
        {

            List<CompanyModel> companies = new List<CompanyModel>();
            await Task.Run(async () =>
            {
                foreach (var company in await companyServ.GetCompanies())
                {
                    var Qualifications = await qualificationServ.GetQualifications();
                    var QualificationName = Qualifications.Where(p => p.Id == company.QualificationId).FirstOrDefault().QualificationName;
                    var lead = await userRegistrationServ.GetUserFullName(company.LeadOwnerId);
                    var country = await countryServ.GetCountry(company.HGBasedInCountryId);
                    CompanyModel companyModel = new CompanyModel
                    {
                        CompanyLegalName = company.CompanyLegalName,
                        HGBasedInCountryName = country.Name,
                        Id = company.Id,
                        LeadOwnerFullName = lead,
                        QualificationName = QualificationName,
                        QualifiedDate = company.QualifiedDate,
                        TradingName = company.TradingName,
                        Website = company.Website
                    };
                    companies.Add(companyModel);
                }
            }
            );
            companyModels = companies;
            /*IEnumerable<CompanyDTO> companies = await companyServ.GetCompanies();
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<CompanyDTO, CompanyModel>()
            .ForMember(p=>p.HGBasedInCountryName, p=>p.MapFrom(s=>countryServ.GetCountry(s.HGBasedInCountryId).Result.Name))
            .ForMember(p => p.LeadOwnerFullName, p => p.MapFrom(s =>  userRegistrationServ.GetUserFullName(s.LeadOwnerId)))
            .ForMember(p => p.QualificationName, p => p.MapFrom(s => qualificationServ.GetQualifications().Result.Where(p=>p.Id==s.QualificationId).FirstOrDefault().QualificationName))
            ).CreateMapper();
            companyModels  = mapper.Map<IEnumerable<CompanyDTO>, IEnumerable<CompanyModel>>(companies);*/
        }
    }
}
