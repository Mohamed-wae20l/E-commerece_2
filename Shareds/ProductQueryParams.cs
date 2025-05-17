using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shareds
{
   public class ProductQueryParams
    {
        private const int DefaultPageSize = 5;
        private const int MaxPageSize = 10;
        public int? brandID {  get; set; }
        public int? typeID { get; set; }
        public ProductSortingOptions sortingOptions { get; set; }
        public string? SearchValue { get; set; }
        public int PageIdnex { get; set; } = 1;
        public int PageSize=DefaultPageSize;
        //Property
        public int pageSize
        {
            get { return PageSize; }
            set { PageSize = value > MaxPageSize ? MaxPageSize : value; }
        }
    }
}
