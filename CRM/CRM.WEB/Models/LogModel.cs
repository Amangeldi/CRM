﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.WEB.Models
{
    /// <summary>
    /// Логи
    /// </summary>
    public class LogModel
    {
        public int Id { get; set; }
        public string Action { get; set; }
        public string UserName { get; set; }
    }
}