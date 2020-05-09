using CRM.DAL.EF;
using CRM.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CRM.DAL.Data
{
    public class SampleData
    {
        public static async Task Initialize(UserManager<User> userManager, ApiContext context, RoleManager<IdentityRole> roleManager)
        {
            string adminEmail = "admin@gmail.com";
            string password = "_Aa123456";
            if (await userManager.FindByNameAsync(adminEmail) == null)
            {
                await roleManager.CreateAsync(new IdentityRole("admin"));
                await roleManager.CreateAsync(new IdentityRole("lead"));
                await roleManager.CreateAsync(new IdentityRole("client"));
                User admin = new User { Email = adminEmail, UserName = adminEmail };
                await userManager.CreateAsync(admin, password);
                await userManager.AddToRoleAsync(admin, "admin");
                User lead = new User { Email = "dustin.carroll@example.com", UserName = "dustin.carroll@example.com", FirstName = "Dustin", LastName = "Carroll" };
                await userManager.CreateAsync(lead, password);
                await userManager.AddToRoleAsync(lead, "lead");
                User client = new User { Email = "john.hale@example.com", UserName = "john.hale@example.com", FirstName = "John", LastName = "Hale" };
                await userManager.CreateAsync(client, password);
                await userManager.AddToRoleAsync(client, "client");
                User admin1 = new User { Email = "douglas.henry@example.com", UserName = "douglas.henry@example.com", FirstName = "Douglas", LastName = "Henry" };
                await userManager.CreateAsync(admin1, password);
                await userManager.AddToRoleAsync(admin1, "admin");
                User client1 = new User { Email = "dale.hopkins@example.com", UserName = "dale.hopkins@example.com", FirstName = "Dale", LastName = "Hopkins" };
                await userManager.CreateAsync(client1, password);
                await userManager.AddToRoleAsync(client1, "client");
                Region Africa = new Region { Name = "Africa" };
                Region Antarctica = new Region { Name = "Antarctica" };
                Region AsiaPacific = new Region { Name = "Asia/Pacifig" };
                Region Europe = new Region { Name = "Europe" };
                Region NorthAmerica = new Region { Name = "North America" };
                Region SouthAmerica = new Region { Name = "South America" };
                await context.Regions.AddRangeAsync(Africa, Antarctica, AsiaPacific, Europe, NorthAmerica, SouthAmerica);
                Country Australia = new Country { Capital = "Canberra", Name = "Australia", Region = AsiaPacific };
                Country Germany = new Country { Capital = "Berlin", Name = "Germany", Region = Europe };
                Country Slovakia = new Country { Capital = "Bratislava", Name = "Slovakia", Region = Europe };
                Country USA = new Country { Capital = "Washington", Name = "Unated States", Region = NorthAmerica };
                Country TM = new Country { Capital = "Ashgabat", Name = "Turkmenistan", Region = AsiaPacific };
                await context.Countries.AddRangeAsync(Australia, Germany, Slovakia, USA);
                CompanyQualification Qualified = new CompanyQualification { QualificationName = "Qualified" };
                CompanyQualification NotQualified = new CompanyQualification { QualificationName = "NotQualified" };
                CompanyQualification NewCompany = new CompanyQualification { QualificationName = "NewCompany" };
                await context.CompanyQualifications.AddRangeAsync(Qualified, NotQualified, NewCompany);
                Company ACN = new Company 
                { 
                    CompanyLegalName = "ATM ATM Pty Ltd", 
                    HGBasedInCountry = Australia, 
                    Qualification = NewCompany, 
                    TradingName = "A.C.N." ,
                    Website = "turkmen-tranzit.com"
                };
                Company ATM = new Company
                {
                    CompanyLegalName = "A.C.N. 605 479 678 Pty Ltd",
                    HGBasedInCountry = Australia,
                    Qualification = Qualified,
                    TradingName = "ATM",
                    LeadOwner = lead,
                    QualifiedDate = DateTime.Now,
                    Website = "ttweb.org"
                };
                Company Pay = new Company
                {
                    CompanyLegalName = "24-pay s.r.o",
                    HGBasedInCountry = Australia,
                    Qualification = NotQualified,
                    TradingName = "24 Pay SRO",
                    LeadOwner = lead,
                    Website = "24-pay.sk"
                };
                Company Ttz = new Company
                {
                    CompanyLegalName = "HO ''Turkmen-Tranzit''",
                    HGBasedInCountry = TM,
                    Qualification = NewCompany,
                    TradingName = "Turkmen-Tranzit",
                    Website = "Turkmen-Tranzit.com"
                };
                Company Ttw = new Company
                {
                    CompanyLegalName = "HO ''Turkmen-Tranzit'' -> TTWeb",
                    HGBasedInCountry = TM,
                    Qualification = NewCompany,
                    TradingName = "TtWeb",
                    Website = "Ttweb.org"
                };
                Company google = new Company
                {
                    CompanyLegalName = "google",
                    HGBasedInCountry = USA,
                    Qualification = NewCompany,
                    TradingName = "google",
                    Website = "google.com"
                };
                Company microsoft = new Company
                {
                    CompanyLegalName = "microsoft",
                    HGBasedInCountry = USA,
                    Qualification = NewCompany,
                    TradingName = "microsoft",
                    Website = "microsoft.com"
                };
                await context.Companies.AddRangeAsync(ACN, ATM, Pay, Ttz, Ttw, google, microsoft);

                await context.SaveChangesAsync();
            }
        }
    }
}
