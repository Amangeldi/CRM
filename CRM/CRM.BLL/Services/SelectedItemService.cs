using CRM.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.BLL.Services
{
    public class SelectedItemService : ISelectedItemService
    {
        private int Id { get; set; }

        public event Action OnChange;

        public void SetId (int Id)
        {
            this.Id = Id;
        }
        public int GetId()
        {
            return Id;
        }

        public void NotifyStateChanged()
        {
            OnChange?.Invoke();
        }
    }
}
