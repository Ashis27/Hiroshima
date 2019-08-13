using Hiroshima.Maas.DL.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using Hiroshima.Maas.DL.Entities.CategoryModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hiroshima.Maas.DL.Entities.ProductModel
{
    public class Product:BaseEntity
    {
        [Required]
        public string Name { get; set; }
        public string QuantityInPackage { get; set; }
        public string UnitOfMeasurement { get; set; }
        public virtual IList<CategoryProductMapper> ProductMapper { get; set; }
    }
}
