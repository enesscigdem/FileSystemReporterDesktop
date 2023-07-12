using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FluentValidation;
namespace FileOrbis___File_System_Reporter.FluentValidation
{
  
    public class Validation : AbstractValidator<FormValidate>
    {
        public Validation()
        {
            RuleFor(x => x.Thread).NotEmpty().WithMessage("Thread alanı boş olamaz.");
            RuleFor(x => x.Thread).Must(BeNumeric).WithMessage("Thread alanına sadece sayı girişi yapılmalıdır.");
            RuleFor(x => x.Path).NotEmpty().WithMessage("Path alanı boş olamaz.");
            RuleFor(x => x.Path).Must(BeValidPath).WithMessage("Böyle bir yol bulunamadı.");
        }
        private bool BeValidPath(string path)
        {
            return !string.IsNullOrEmpty(path) && Directory.Exists(path);
        }

        private bool BeNumeric(string value)
        {
            return int.TryParse(value, out _);
        }
    }
}
