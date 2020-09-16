using Microsoft.EntityFrameworkCore;
using PencatatanSuhuPekerjaAPI.Context;
using PencatatanSuhuPekerjaAPI.Models;
using PencatatanSuhuPekerjaAPI.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PencatatanSuhuPekerjaAPI.Repositories.Data
{
    public class DivisionRepository : GeneralRepository<Division, MyContext>
    {
        MyContext _context;
        public DivisionRepository(MyContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<List<Division>> GetAll()
        {
            var data = await _context.Divisions.Include("Department").Where(D => D.IsDelete == false).ToListAsync();
            return data;
        }
    }
}
