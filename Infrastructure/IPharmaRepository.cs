using System.Threading.Tasks;
using PharmaBackend.Helpers;
using PharmaBackend.Models;

namespace PharmaBackend.Infrastructure
{
    public interface IPharmaRepository
    {
        void Add<T>(T entity) where T: class;
         void Delete<T>(T entity) where T: class;
         Task<bool> SaveAll();
         Task<PagedList<Medicine>> GetMedicines(SearchParams searchParams = null);
         Task<Medicine> GetMedicine(int id);
         //Task<Medicine> GetMedicineByName(string name);
    }
}