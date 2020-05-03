using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.BLL.DTO;
using CRM.BLL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRM.WEB.Controllers.Api
{
    /// <summary>
    /// Контроллер для Регионов
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class RegionController : ControllerBase
    {
        readonly IRegionService serv;
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="service"></param>
        public RegionController(IRegionService service)
        {
            serv = service;
        }
        /// <summary>
        /// Возвращает имя Региона по ID
        /// </summary>
        /// <param name="RegionId"></param>
        /// <returns></returns>
        [HttpGet("GetRegionName/{RegionId}")]
        public async Task<ActionResult<string>> GetRegionName(int RegionId)
        {
            return Ok(await serv.GetRegionName(RegionId));
        }
        /// <summary>
        /// Получить все Регионы
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetRegions")]
        public async Task<ActionResult<IEnumerable<RegionDTO>>> GetRegions()
        {
            return Ok(await serv.GetRegions());
        }
    }
}