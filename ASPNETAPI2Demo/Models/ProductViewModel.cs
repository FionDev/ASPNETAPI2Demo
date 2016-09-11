using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASPNETAPI2Demo.Models
{
    public class ProductViewModel
    {
        public ProductViewModel()
        {
        }
        public Nullable<decimal> Price { get; set; }

        public Nullable<decimal> Stock { get; set; }

    }
}