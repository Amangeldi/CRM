﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.DAL.Entities
{
    public class Company
    {
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

        public List<CompanyContactLink> CompanyContactLinks { get; set; }
    }
}
