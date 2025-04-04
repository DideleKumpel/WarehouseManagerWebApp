using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
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
            if (await ProductBarcodeExistAsync(product.Barcode))
            {
                throw new Exception("Product barcode already exist");
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
        
        public async Task DeleteProductAsync(string barcode)
        {
            if (barcode != null)
            {
                try
                {
                    productModel product = await _context.Products.FirstOrDefaultAsync(p => p.Barcode == barcode);
                    if (product != null)
                    {
                        _context.Products.Remove(product);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        throw new Exception("Product not found");
                    }
                }
                catch (Exception e)
                {
                    throw new Exception("Error deleting product: " + e.Message);

                }
            }
        }
    }
}
