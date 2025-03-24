using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using WareHouseManagerWebApp.Data;
using WareHouseManagerWebApp.Model;


namespace WareHouseManagerWebApp.Service
{
    internal class locationsServise
    {
        private readonly AppDbContext _context;

        public locationsServise(AppDbContext context)
        {
            _context = context;
        }

        public async Task<int> GetEmptySpaceId()
        {
            locationModel location = await _context.Locations.FirstOrDefaultAsync(l => l.ItemBarcode == null);
            if (location == null)
            {
                return -1;
            }
            else
            {
                return location.Id;
            }
        }
        public async Task<int> GetSpaceIdWithProduct(string productBarcode)
        {
            locationModel location = await _context.Locations.FirstOrDefaultAsync(l => l.ItemBarcode == productBarcode);
            if (location == null)
            {
                return -1;
            }
            else
            {
                return location.Id;
            }
        }
        
    }
}
