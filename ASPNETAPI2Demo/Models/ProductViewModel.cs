using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ASPNETAPI2Demo.Models
{
    public class ProductViewModel: IValidatableObject
    {
        public ProductViewModel()
        {
        }
        public Nullable<decimal> Price { get; set; }

        public Nullable<decimal> Stock { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }

    }
}