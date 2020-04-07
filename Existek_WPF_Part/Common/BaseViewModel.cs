using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Existek_WPF_Part.Common
{
    public class BaseViewModel : BasePropertyChangedNotifier
    {
        public virtual bool SetValue<T>(ref T field, T newValue)
        {
            if (Equals(field, newValue)) return false;

            field = newValue;
            return true;
        }
    }
}
