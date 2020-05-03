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
    public class QualificationService : IQualificationService
    {
        readonly ApiContext db;
        public QualificationService(ApiContext context)
        {
            db = context;
        }
        public async Task<IEnumerable<CompanyQualification>> GetQualifications()
        {
            return await db.CompanyQualifications.ToListAsync();
        }
    }
}
