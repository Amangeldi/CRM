using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.DAL.Entities
{
    public class Region
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Country> Countries { get; set; }
    }
}
