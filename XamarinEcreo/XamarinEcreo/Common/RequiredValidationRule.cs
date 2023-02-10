using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinEcreo.Common
{
    public class RequiredValidationRule : IValidationRule<string>
    {
        public string ValidationMessage { get; set; } = "This field is required, and must be a valid email";

        public bool Validate(string value)
        {
            if (value.Length>5||value.Contains(".")&& !value.Contains("@")||value.Contains(" "))
            {
                if (value.Contains("@"))
                {
                    return !string.IsNullOrEmpty(value);
                }
                return false;
            }
            return true;
        }
    }
}
