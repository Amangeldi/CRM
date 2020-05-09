using CRM.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.BLL.Services
{
    public class SelectedItemService : ISelectedItemService
    {
        public int Id { get; set; }
        public void SetId (int Id)
        {
            this.Id = Id;
        }
        public int GetId()
        {
            return Id;
        }
    }
}
