using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CRM.DAL.Entities
{
    public class Company
    {
        [Key]
        public int Id { get; set; }
        public string CompanyLegalName { get; set; }
        public string TradingName { get; set; }
        public int HGBasedInCountryId { get; set; }
        public Country HGBasedInCountry { get; set; }
        public string LeadOwnerId { get; set; }
        public User LeadOwner { get; set; }
        public int QualificationId { get; set; }
        public CompanyQualification Qualification { get; set; }
        public string Website { get; set; }
        public DateTime QualifiedDate { get; set; }
        public int? CompanyLinkedinId { get; set; }
        public Linkedin CompanyLinkedin { get; set; }

        public virtual List<CompanyContactLink> CompanyContactLinks { get; set; }
    }
}
