using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace DemoApp.Core.Attribute
{
    public class CheckGuidAttribute : ValidationAttribute
    {
        public string Property { get; set; }
        protected override ValidationResult IsValid(object id, ValidationContext validationContext)
        {

            if (!Guid.TryParse(id as string, out Guid guidId))
            {
                return new ValidationResult($"Invalid {Property}");
            }

            return ValidationResult.Success;
        }
    }

    public class CheckDateAttribute : ValidationAttribute
    {
        public string Property { get; set; }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                try
                {
                    DateTime.ParseExact(value as string, "yyyyMMdd", CultureInfo.InvariantCulture);
                }
                catch (Exception)
                {
                    return new ValidationResult($"Invalid Date (Valid: yyyyMMdd)");
                }

            }
            return ValidationResult.Success;
        }
    }
    public class CheckGuidOrNullAttribute : ValidationAttribute
    {
        public string Property { get; set; }
        protected override ValidationResult IsValid(object id, ValidationContext validationContext)
        {
            if (id != null)
            {
                if (!Guid.TryParse(id as string, out Guid guidId))
                {
                    return new ValidationResult($"Invalid {Property}");
                }
            }
            return ValidationResult.Success;
        }
    }
    public class CheckUrlAttribute : ValidationAttribute
    {
        public string Property { get; set; }
        protected override ValidationResult IsValid(object uriName, ValidationContext validationContext)
        {
            if (!string.IsNullOrEmpty(uriName as string))
                return Uri.TryCreate(uriName as string, UriKind.Absolute, out Uri uriResult)
                     && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps) == true
                     ? ValidationResult.Success
                     : new ValidationResult($"Invalid URL");
            return ValidationResult.Success;
        }
    }
    public class CheckNameAttribute : ValidationAttribute
    {
        public string Property { get; set; }
        protected override ValidationResult IsValid(object inputText, ValidationContext validationContext)
        {
            //@"^(\\b[A-Za-z]*\\b\\s+\\b[A-Za-z]*\\b+\\.[A-Za-z])$",
            Regex regex = new Regex(
                                       "^[a-zA-Z_ÀÁÂÃÈÉÊÌÍÒÓÔÕÙÚĂĐĨŨƠàáâãèéêìíòóôõùúăđĩũơƯĂẠẢẤẦẨẪẬẮẰẲẴẶẸẺẼỀỀỂưăạảấầẩẫậắằẳẵặẹẻẽềềểỄỆỈỊỌỎỐỒỔỖỘỚỜỞỠỢỤỦỨỪễệỉịọỏốồổỗộớờởỡợụủứừỬỮỰỲỴÝỶỸửữựỳỵỷỹ\\s]+$",
                                   RegexOptions.IgnoreCase
                                   | RegexOptions.CultureInvariant
                                   | RegexOptions.IgnorePatternWhitespace
                                   | RegexOptions.Compiled
                                   );


            if (regex.IsMatch(inputText as string))
            {
                return ValidationResult.Success;
            }
            return new ValidationResult($"{Property} can't have number and special character");
        }
    }
}
