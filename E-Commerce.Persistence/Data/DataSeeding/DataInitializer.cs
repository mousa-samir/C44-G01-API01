using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities;
using E_Commerce.Domain.Entities.ProductModule;
using E_Commerce.Persistence.Data.DbContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace E_Commerce.Persistence.Data.DataSeed
{
    public class DataInitializer : IDataInitializer
    {
        private readonly StoreDbContext _dbContext;

        public DataInitializer(StoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task InitializeAsync()
        {
            try
            {
                var HasProducts = await _dbContext.products.AnyAsync();
                var HasBrands = await _dbContext.productBrands.AnyAsync();
                var HasTypes = await _dbContext.productTypes.AnyAsync();
                if (!HasBrands)
                   await DataSeedFromJsonAsync< ProductBrand, int>("brands.json", _dbContext.productBrands);                
                if (!HasTypes)
                    await DataSeedFromJsonAsync< ProductType, int>("types.json", _dbContext.productTypes);
                await _dbContext.SaveChangesAsync();
                if (!HasProducts)
                    await DataSeedFromJsonAsync< Product, int>("products.json", _dbContext.products);
                await _dbContext.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                Console.WriteLine($" Data Seeding Failed :{ex}");
            }

        }

        private async Task DataSeedFromJsonAsync<T ,TKey>( string fileName, DbSet<T> dbset) where T : BaseEntity<TKey>
        {
            //E:\Route\C#\Projects\E-CommerceSolution\E-Commerce.Persistence\Data\DataSeed\JSONFiles\

            var FilePath = @"../E-Commerce.Persistence\Data\DataSeed\JSONFiles\" + fileName;
            if (!File.Exists(FilePath)) throw new FileNotFoundException($"File {fileName} is not Exists");

            try
            {
               using var dataStream = File.OpenRead(FilePath);
                var data = await JsonSerializer.DeserializeAsync<List<T>>(dataStream , new JsonSerializerOptions()
                {
                    PropertyNameCaseInsensitive = true
                });
                if(data is not null)
                {
                   await dbset.AddRangeAsync(data);
                }
            }
            catch(Exception Ex)
            {
                Console.WriteLine($" Error While Reading JSON File :{Ex}");

            }
           
        }
    }
}
