using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.BLL.DTO;
using CRM.BLL.Interfaces;
using CRM.DAL.Entities;
using CRM.WEB.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRM.WEB.Controllers.Api
{
    /// <summary>
    /// Контроллер для компаний
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService companyServ;
        private readonly IQualificationService qualificationServ;
        private readonly ICountryService countryServ;
        private readonly IRegionService regionServ;
        private readonly IUserRegistrationService userRegistrationServ;
        /// <summary>
        /// Конструктор
        /// </summary>
        public CompanyController(ICompanyService companyService, IQualificationService qualificationService, 
            ICountryService countryService, IRegionService regionService, IUserRegistrationService userRegistrationService)
        {
            companyServ = companyService;
            qualificationServ = qualificationService;
            countryServ = countryService;
            regionServ = regionService;
            userRegistrationServ = userRegistrationService;
        }
        /// <summary>
        /// Возвращает все виды кваливикаций для компаний
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetQualifications")]
        public async Task<IEnumerable<CompanyQualification>> GetQualifications()
        {
            return await qualificationServ.GetQualifications();
        }
        /// <summary>
        /// Запрос на назначение менеджера для компании
        /// </summary>
        /// <param name="AppointCompanyLeadDTO"></param>
        /// <returns></returns>
        [HttpPost("AppointLead")]
        public async Task<ActionResult<AppointCompanyLeadDTO>> AppointLead(AppointCompanyLeadDTO AppointCompanyLeadDTO)
        {
            await companyServ.AppointLead(AppointCompanyLeadDTO);
            return Ok(AppointCompanyLeadDTO);
        }
        /// <summary>
        /// Запрос на создание компании
        /// </summary>
        /// <param name="CompanyRegistrationDTO"></param>
        /// <returns></returns>
        [HttpPost("CreateCompany")]
        public async Task<ActionResult<CompanyRegistrationDTO>> CreateCompany(CompanyRegistrationDTO CompanyRegistrationDTO)
        {
            await companyServ.CreateCompany(CompanyRegistrationDTO);
            return Ok(CompanyRegistrationDTO);
        }
        /// <summary>
        /// Удаляет компанию по Id
        /// </summary>
        /// <param name="CompanyId"></param>
        /// <returns></returns>
        [HttpDelete("DeleteCompany/{CompanyId}")]
        public async Task<ActionResult> DeleteCompany(int CompanyId)
        {
            if (await companyServ.DeleteCompany(CompanyId))
            {
                return Ok("Компания " + CompanyId + " удалена");
            }
            var err = new { error = "Компания " + CompanyId + " не существует" };
            return Ok(err);
        }
        /// <summary>
        /// Редактирует компанию
        /// </summary>
        /// <param name="CompanyRegistrationDTO"></param>
        /// <returns></returns>
        [HttpPut("EditCompany")]
        public async Task<ActionResult> EditCompany(CompanyRegistrationDTO CompanyRegistrationDTO)
        {
            await companyServ.EditCompany(CompanyRegistrationDTO);
            return Ok(CompanyRegistrationDTO);
        }
        /// <summary>
        /// Возвращает все компании
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetCompanies")]
        public async Task<IEnumerable<CompanyDTO>> GetCompanies()
        {
            return await companyServ.GetCompanies();
        }
        /// <summary>
        /// Возвращает компанию по ID
        /// </summary>
        /// <param name="CompanyId"></param>
        /// <returns></returns>
        [HttpGet("GetCompany/{CompanyId}")]
        public async Task<ActionResult<CompanyViewModel>> GetCompany(int CompanyId)
        {
            CompanyDTO company = await companyServ.GetCompany(CompanyId);
            CountryDTO country = await countryServ.GetCountry(company.HGBasedInCountryId);
            CompanyQualification companyQualification = qualificationServ.GetQualifications().Result.Where(p => p.Id == company.QualificationId).FirstOrDefault();
            string userName = "";
            if (company.LeadOwnerId!=null)
            {
                userName = await userRegistrationServ.GetUserFullName(company.LeadOwnerId);
            }
            string region = await regionServ.GetRegionName(country.RegionId);
            CompanyViewModel companyView = new CompanyViewModel
            {
                CompanyLegalName = company.CompanyLegalName,
                CompanyRegion = region,
                HGBasedInCountryName = country.Name,
                Id = company.Id,
                LeadOwnerFullName = userName,
                QualificationName = companyQualification.QualificationName,
                QualifiedDate = company.QualifiedDate,
                TradingName = company.TradingName,
                Website = company.Website
            };
            return Ok(companyView);
        }
        /// <summary>
        /// Возвращает все компании для конкретной страны
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetCountryCompanies/{CountryId}")]
        public async Task<IEnumerable<CompanyDTO>> GetCountryCompanies(int CountryId)
        {
            return await companyServ.GetCountryCompanies(CountryId);
        }
        /// <summary>
        /// Возвращает все компании для конкретного региона
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetRegionCompanies/{RegionId}")]
        public async Task<IEnumerable<CompanyDTO>> GetRegionCompanies(int RegionId)
        {
            return await companyServ.GetRegionCompanies(RegionId);
        }
        /// <summary>
        /// Запрос на изменение кваливикации компании
        /// </summary>
        /// <param name="QualifyCompanyDTO"></param>
        /// <returns></returns>
        [HttpPost("QualifyCompany")]
        public async Task<ActionResult<CompanyDTO>> QualifyCompany(QualifyCompanyDTO QualifyCompanyDTO)
        {
            await companyServ.QualifyCompany(QualifyCompanyDTO);
            return Ok(QualifyCompanyDTO);
        }
    }
}