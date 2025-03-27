using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WareHouseManagerWebApp.Data;
using WareHouseManagerWebApp.Model;
using static Google.Protobuf.Reflection.SourceCodeInfo.Types;

namespace WareHouseManagerWebApp.Service
{
    internal class productService
    {
        private readonly AppDbContext _context;

        public productService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<productModel>> LoadProductsAsync()
        {
            return await _context.Products.ToListAsync();
        }
        public async Task AddProductAsync(productModel product)
        {
            if(product == null)
            {
                throw new Exception("Product is null");
            }
            if ( await ProductAlreadyExistAsync(product))
            {
                throw new Exception("Product already exist");
            }
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
        }
        public async Task<bool> ProductBarcodeExistAsync(string Barcode)
        {
            productModel exist = _context.Products.FirstOrDefault(p => p.Barcode == Barcode);
            if (exist != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task<bool> ProductAlreadyExistAsync(productModel product)
        {
            productModel exist = _context.Products.FirstOrDefault(p => p.Barcode == product.Barcode && p.Name == product.Name && p.Weight == product.Weight && p.Category == product.Category && p.Description == product.Description);
            if(exist != null)
            {
                return true;
            }else
            {
                return false;
            }    

        }
        //public async Task<bool> ProductInsertAsync(string Barcode, string Name, string Category, string Description, double Weight)
        //{

        //}
        //public async Task<bool> DeleteProductsAsync(string Barcode)
        //{

        //}
        //public async Task<bool> UpdateProductAsync(string Barcode, string Name, string Category, string Description, double Weight)
        //{
        //}
    }
}
