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
    /// Контроллер для стран
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly ICountryService countryServ;
        private readonly IRegionService regionServ;
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="service"></param>
        public CountryController(ICountryService service, IRegionService regionService)
        {
            countryServ = service;
            regionServ = regionService;

        }
        /// <summary>
        /// Запрос на создание страны
        /// </summary>
        /// <param name="CountryDTO"></param>
        /// <returns></returns>
        [HttpPost("CreateCountry")]
        public async Task<ActionResult<CountryDTO>> CreateCountry(CountryDTO CountryDTO)
        {
            await countryServ.CreateCountry(CountryDTO);
            return Ok(CountryDTO);
        }
        /// <summary>
        /// Возвращает страну по ID
        /// </summary>
        /// <param name="CountryId"></param>
        /// <returns></returns>
        [HttpGet("GetCountry")]
        public async Task<ActionResult<CountryViewModel>> GetCountry(int CountryId)
        {
            CountryDTO country = await countryServ.GetCountry(CountryId);
            string region = await regionServ.GetRegionName(country.RegionId);
            CountryViewModel countryView = new CountryViewModel
            {
                Capital = country.Capital,
                Id = CountryId,
                Name = country.Name,
                Region = region
            };
            return Ok(countryView);
        }
        /// <summary>
        /// Возвращает все страны
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetCountries")]
        public async Task<IEnumerable<CountryDTO>> GetCountries()
        {
            return await countryServ.GetCountries();
        }
        /// <summary>
        /// Возвращает все страны в регионе regionId
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetRegionCountries/{regionId}")]
        public async Task<IEnumerable<CountryDTO>> GetRegionCountries(int regionId)
        {
            return await countryServ.GetRegionCountries(regionId);
        }
        /// <summary>
        /// Удаляет страну по Id
        /// </summary>
        /// <param name="CountryId"></param>
        /// <returns></returns>
        [HttpDelete("DeleteCountry/{CountryId}")]
        public async Task<ActionResult> DeleteCountry(int CountryId)
        {
            if (await countryServ.DeleteCountry(CountryId))
            {
                return Ok("Страна " + CountryId + " удалена");
            }
            var err = new { error = "Страна " + CountryId + " не существует" };
            return Ok(err);
        }
        /// <summary>
        /// Редактирует страну
        /// </summary>
        /// <param name="CountryDTO"></param>
        /// <returns></returns>
        [HttpPut("EditCountry")]
        public async Task<ActionResult> EditCountry(CountryDTO CountryDTO)
        {
            await countryServ.EditCountry(CountryDTO);
            return Ok(CountryDTO);
        }
    }
}