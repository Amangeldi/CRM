using CRM.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CRM.BLL.Interfaces
{
    public interface IQualificationService
    {
        Task<IEnumerable<CompanyQualification>> GetQualifications();
    }
}
