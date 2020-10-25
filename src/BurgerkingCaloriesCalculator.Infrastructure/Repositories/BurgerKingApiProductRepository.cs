using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using BurgerkingCaloriesCalculator.Domain.Models;
using BurgerkingCaloriesCalculator.Domain.Repositories;
using BurgerkingCaloriesCalculator.Infrastructure.Repositories.Dto;
using BurgerkingCaloriesCalculator.Options;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace BurgerkingCaloriesCalculator.Infrastructure.Repositories
{
    /// <summary>
    /// Access to the burgerking rest api
    /// </summary>
    public class BurgerKingApiProductRepository : IProductRepository
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<BurgerKingApiProductRepository> _logger;
        private readonly BurgerkingApiOptions _apiOptions;

        public BurgerKingApiProductRepository(HttpClient httpClient, IOptions<BurgerkingApiOptions> apiOptions, ILogger<BurgerKingApiProductRepository> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
            _apiOptions = apiOptions.Value;
        }
        
        /// <inheritdoc />
        public async Task<IEnumerable<Product>> FindAll()
        {
            var response = await _httpClient.GetStringAsync(_apiOptions.ProductEndpoint);

            try
            {
                var data = JsonConvert.DeserializeObject<IEnumerable<ProductResponse>>(response);
                return data.Select(product =>
                {
                    if (double.TryParse(
                        product.NutritionFacts.FirstOrDefault(fact => fact.Key == "energykcal")?.Value ??
                        string.Empty, out var kCal))
                    {
                        return Product.Create(product.Id, ProductName.Create(product.Name),
                            EnergyValue.CreateFromKCal(kCal));
                    }

                    return null;
                }).Where(product => product != null);
            }
            catch (JsonException)
            {
                _logger.LogWarning("Unable to get valid response from burgerking api");
                return Enumerable.Empty<Product>();
            }
        }

        /// <inheritdoc />
        public async Task<Maybe<Product>> FindById(string id)
        {
            var product = (from p in await FindAll() where p.Id == id select p).FirstOrDefault();
            return product != null ? Maybe<Product>.From(product) : Maybe<Product>.None;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<Product>> FindAllById(IEnumerable<string> ids)
        {
            return from product in await FindAll() where ids.Contains(product.Id) select product;
        }
    }
}