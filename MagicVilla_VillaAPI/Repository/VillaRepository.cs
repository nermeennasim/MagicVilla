using MagicVilla_VillaAPI.Data;
using MagicVilla_VillaAPI.Models;
using MagicVilla_VillaAPI.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace MagicVilla_VillaAPI.Repository
{
    public class VillaRepository : IVillaRepository
    {
       private readonly ApplicationDBContext _db;
        public VillaRepository(ApplicationDBContext db)
        {
            _db = db;

        }
        async Task IVillaRepository.CreateAsync(Villa entity)
        {
            await _db.Villas.AddAsync(entity);
            await SaveAsync();
        }

        async Task  IVillaRepository.DeleteAsync(Villa entity)
        {
            _db.Villas.Remove(entity);
             await SaveAsync();
        }

        async Task<Villa> IVillaRepository.GetAsync(Expression<Func<Villa,bool>> filter = null, bool tracked = true)
        {
            IQueryable<Villa> query = _db.Villas;
            if (!tracked)
            {
                query = query.AsNoTracking();
            }
            

            if (filter != null)
            {
                query = query.Where(filter);
            }
            return await query.FirstOrDefaultAsync();
        }

         async Task<List<Villa>> IVillaRepository.GetAllAsync(Expression<Func<Villa,bool>> filter = null)
        {
            IQueryable<Villa> query = _db.Villas;

            if(filter != null)
            {
                query = query.Where(filter);
            }
            return await query.ToListAsync();
        }

       public async Task SaveAsync()
        {
             await _db.SaveChangesAsync();
        }

       async Task IVillaRepository.UpdateAsync(Villa entity)
        {
            _db.Villas.Update(entity);
            await SaveAsync();
        
        }
    }
}
