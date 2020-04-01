using Gestor.Core.Domain.Contracts.Repository;
using Gestor.Tools.Contracts.Repository;
using Gestor.Core.Infra.Providers;
using DO = Gestor.Core.Domain.Entities;

namespace Gestor.Core.Infra.Repository
{
    public class ProductRepository : NHBaseRepository<DO.Product>, IProductRepository
    {
        public ProductRepository(IUnitOfWork uow) : base(uow) { }
    }
}
