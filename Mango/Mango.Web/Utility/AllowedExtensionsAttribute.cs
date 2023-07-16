using System.ComponentModel.DataAnnotations;

namespace Mango.Web.Utility
{
    public class AllowedExtensionsAttribute : ValidationAttribute
    {
        private readonly string[] _extensions;
        public AllowedExtensionsAttribute(string[] extenstions) 
        {
            _extensions = extenstions;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var file = value as IFormFile;
            if (file != null)
            {
                var extention = Path.GetExtension(file.FileName);
                if (!_extensions.Contains(extention.ToLower()))
                {
                    return new ValidationResult("This file extention is not allowed.");
                }
            }
            return ValidationResult.Success;
        }

    }
}
