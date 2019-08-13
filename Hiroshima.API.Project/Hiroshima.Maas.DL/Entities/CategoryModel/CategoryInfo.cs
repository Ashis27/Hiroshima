using System;
using System.Collections.Generic;
using System.Text;

namespace Hiroshima.Maas.DL.Entities.CategoryModel
{
   public class CategoryInfos:BaseEntity
    {
        public int CategoryId { get; set; }
        public string Description { get; set; }
        public int Language { get; set; }
        public virtual Category Category { get; set; }
    }
}
