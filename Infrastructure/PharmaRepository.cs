using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PharmaBackend.Helpers;
using PharmaBackend.Models;

namespace PharmaBackend.Infrastructure
{
    public class PharmaRepository : IPharmaRepository
    {
        private readonly DataContext _context;
        public PharmaRepository(DataContext context)
        {
            _context = context;
        }

        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<Medicine> GetMedicine(int id)
        {
            return await _context.Medicine.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<PagedList<Medicine>> GetMedicines(SearchParams searchParams = null)
        {
            var medicines = _context.Medicine.AsQueryable();
            if (!string.IsNullOrEmpty(searchParams.FullName))
            {
                medicines = medicines.Where(m => m.FullName == searchParams.FullName);
            }
            
            return await PagedList<Medicine>.CreateAsync(medicines, searchParams.PageNumber, searchParams.PageSize);
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}