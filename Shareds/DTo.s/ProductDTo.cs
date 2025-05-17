using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shareds.DTo.s
{
    public class ProductDTo
    {
        public int Id {  get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string PicturUrl { get; set; } = null!;
        public decimal Price { get; set; }
        public string BrandName { get; set; }
       
        public string TypeName { get; set; }
       
    }
}
