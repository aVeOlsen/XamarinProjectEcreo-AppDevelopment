using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinEcreo.Common
{
    public interface IValidationRule<T>
    {
        string ValidationMessage { get; set; }
        bool Validate(T value);

    }
}
