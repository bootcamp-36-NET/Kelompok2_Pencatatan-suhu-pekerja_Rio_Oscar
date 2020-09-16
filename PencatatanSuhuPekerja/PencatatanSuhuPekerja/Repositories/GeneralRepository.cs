using Microsoft.EntityFrameworkCore;
using PencatatanSuhuPekerjaAPI.Base;
using PencatatanSuhuPekerjaAPI.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PencatatanSuhuPekerjaAPI.Repositories.Interfaces
{
    public class GeneralRepository<TEntity, TContext> : IRepository<TEntity>
        where TEntity : class, BaseModel
        where TContext : MyContext
    {
        MyContext _context;
        public GeneralRepository(MyContext myContex)
        {
            _context = myContex;
        }
        public async Task<int> Create(TEntity entity)
        {
            entity.CreatedAt = DateTimeOffset.Now;
            entity.IsDelete = false;
            await _context.Set<TEntity>().AddAsync(entity);
            var createItem = await _context.SaveChangesAsync();
            return createItem;
        }

        public async Task<int> Delete(string id)
        {
            var data = await GetId(id);
            if (data == null)
            {
                return 0;
            }
            else
            {
                data.DeletedAt = DateTimeOffset.Now;
                data.IsDelete = true;
                _context.Entry(data).State = EntityState.Modified;
                return await _context.SaveChangesAsync();
            }
        }

        public virtual async Task<List<TEntity>> GetAll()
        {
            var data = await _context.Set<TEntity>().Where(x => x.IsDelete == false).ToListAsync();
            if (!data.Count.Equals(0))
            {
                return data;
            }
            else
            {
                return null;
            }
        }

        public async Task<TEntity> GetId(string id)
        {
            var data = await _context.Set<TEntity>().SingleOrDefaultAsync(x => x.Id == id && x.IsDelete == false);
            if (!data.Equals(0))
            {
                return data;
            }
            else
            {
                return null;
            }
        }

        public async Task<int> Update(TEntity entity)
        {
            entity.UpdatedAt = DateTimeOffset.Now;
            _context.Entry(entity).State = EntityState.Modified;
            return await _context.SaveChangesAsync();
        }
    }
}