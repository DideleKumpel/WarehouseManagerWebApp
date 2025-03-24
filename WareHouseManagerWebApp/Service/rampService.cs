using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using WareHouseManagerWebApp.Data;
using WareHouseManagerWebApp.Model;

namespace WareHouseManagerWebApp.Service
{
    internal class rampService
    {
        private readonly AppDbContext _context;

        public rampService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<rampModel>> LoadRampsAsync()
        {
            return await _context.Ramps.ToListAsync();
        }

    }
}
