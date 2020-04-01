using System;
using System.Collections.Generic;
using System.Text;
using DO = Gestor.Core.Domain.Entities;

namespace Gestor.Core.Infra.Repository.Mappings
{
    public class ProductMap : BaseModelMap<DO.Product>
    {
        public ProductMap() : base("product")
        {
            Map(x => x.Name).Column("name").Not.Nullable();
            Map(x => x.Description).Column("description").Nullable();
        }
    }
}
