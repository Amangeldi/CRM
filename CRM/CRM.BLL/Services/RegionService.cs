using AutoMapper;
using CRM.BLL.DTO;
using CRM.BLL.Interfaces;
using CRM.DAL.EF;
using CRM.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CRM.BLL.Services
{
    public class RegionService : IRegionService
    {
        readonly ApiContext db;
        public RegionService(ApiContext context)
        {
            db = context;
        }
        public async Task<string> GetRegionName(int RegionId)
        {
            var region = await db.Regions.FindAsync(RegionId);
            return region.Name;
        }

        public async Task<IEnumerable<RegionDTO>> GetRegions()
        {
            IEnumerable<Region> regions = await db.Regions.ToListAsync();
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Region, RegionDTO>()).CreateMapper();
            IEnumerable<RegionDTO> RegionDTO = mapper.Map<IEnumerable<RegionDTO>>(regions);
            return RegionDTO;
        }
    }
}
