using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.DAL.Entities
{
    public class Log
    {
        public int Id { get; set; }
        public string Record { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
    }
}
