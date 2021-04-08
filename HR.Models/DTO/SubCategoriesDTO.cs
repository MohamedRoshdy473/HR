using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HR.DTO
{
    public class SubCategoriesDTO
    {
        public int ID { get; set; }
        public string SubCategoryName { get; set; }
        public string CategoryName { get; set; }
        public int CategoryID { get; set; }
    }
}
