using Gestor.Core.Domain.Contracts.Repository;
using Gestor.Tools.Utils.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DO = Gestor.Core.Domain.Entities;
using DTO = Gestor.Core.Domain.DTO;

namespace Gestor.Core.Application
{
    public class ProductApplication : BaseApplication, IDisposable
    {
        private readonly IProductRepository productRepository;

        public ProductApplication(IProductRepository productRepository) : base()
            => this.productRepository = productRepository;

        public async Task<DTO.Product> Save(DTO.Product product)
        {
            var result = await productRepository.Save(new DO.Product(product));

            product = new DTO.Product(result);

            return product;
        }

        public async Task<DTO.Product> Get(Guid id)
        {
            var result = await productRepository.Get(id);

            if (result.IsNull())
                throw new ArgumentException($"Produto {id} não encontrado.");

            return new DTO.Product(result);
        }

        public async Task Delete(Guid id)
        {
            await Get(id);
            await productRepository.Delete(id);
        }
    }
}
