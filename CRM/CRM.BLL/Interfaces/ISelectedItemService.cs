using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.BLL.Interfaces
{
    public interface ISelectedItemService
    {
        void SetId(int Id);
        int GetId();
        void NotifyStateChanged();
        event Action OnChange;
    }
}
