using Hiroshima.Maas.DL.Entities.ProductModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Hiroshima.Maas.DL.Entities.CategoryModel
{
    public class Category : BaseEntity
    {
        [Required]
        public string Name { get; set; }
        public virtual IList<CategoryInfos> CategoryInfos { get; set; }
        public virtual IList<CategoryProductMapper> ProductMapper { get; set; }
    }
}
