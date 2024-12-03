using AutoMapper;

using InternetBanking.Application.Dto.Product;
using InternetBanking.Application.Extras.ResultObject;
using InternetBanking.Application.Helpers;
using InternetBanking.Application.Interfaces.Repositories;
using InternetBanking.Application.Interfaces.Services;
using InternetBanking.Application.Interfaces.ViewModels;
using InternetBanking.Domain.Entities;

namespace InternetBanking.Application.Services
{
    public class ProductService<TSaveViewModel, TViewModel, TProduct>
    : GenericService<TSaveViewModel, TViewModel, TProduct>,
      IProductService<TSaveViewModel, TViewModel, TProduct>
    where TSaveViewModel : class, IProductSaveViewModel
    where TViewModel : class
    where TProduct : BasicProduct
    {
        private readonly IProductRepository<TProduct> _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository<TProduct> productRepository, IMapper mapper)
        : base(productRepository, mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public override async Task<TSaveViewModel> Add(TSaveViewModel vm)
        {
            string[] samplingSeqs;
            string[] validSeqs;
            do
            {
                samplingSeqs = new string[10];
                for (int i = 0; i < 10; i++)
                {
                    samplingSeqs[i] = RNG.SequenceOfLength(9);
                }
                validSeqs = await _productRepository.FilterWithExistingIdentifiers(samplingSeqs.ToArray()); // Copy array
            } while (validSeqs.Length == 0);

            vm.IdentifierAccount = validSeqs[0];

            return await base.Add(vm);
        }
        public async Task<TViewModel?> FindByNumberAccount(string accountNumber)
        {
            var product  = await _productRepository.FindByNumberAccount(accountNumber);
            return _mapper.Map<TViewModel>(product);
        }

        public async Task RemoveByAccountNumber(string identifierAccount)
        {
            await _productRepository.RemoveByAccountNumber(identifierAccount);
        }

    }

    public class ProductService
    : IProductService
    {
        private readonly IProductRepository<BasicProduct> _productRepository;

        public ProductService(IProductRepository<BasicProduct> productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ProductsStatisticsDTO> CalculateStatistics()
        {
            return new()
            {
                TotalProducts = await _productRepository.CountProducts()
            };
        }
    }
}