using System.Collections.Generic;
using System.Threading.Tasks;
using SGS.eCalc.Helpers;
using SGS.eCalc.Models;

namespace SGS.eCalc.Repository
{
    public interface IDatingRepository
    {
         void Add<T>(T entity) where T: class;
         void Delete<T>(T entity) where T: class;

         Task<bool> SaveAll();
         Task<PagedList<User>> GetUsers(UserParams userParams);
         Task<User> GetUser(int id);
         Task<Photo> GetPhoto(int id);

         Task<Like> GetLike(int userId, int recipientId);
    }
}