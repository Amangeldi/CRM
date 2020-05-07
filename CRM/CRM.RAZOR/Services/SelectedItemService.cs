using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.RAZOR.Services
{
    public class SelectedItemService
    {
        private int Id { get; set; }

        public event Action OnChange;

        public void SetId(int Id)
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
