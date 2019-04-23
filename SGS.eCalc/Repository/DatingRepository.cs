using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SGS.eCalc.Data;
using SGS.eCalc.Helpers;
using SGS.eCalc.Models;

namespace SGS.eCalc.Repository
{
    public class DatingRepository : IDatingRepository
    {
        private readonly DataContext _context;
        public DatingRepository(DataContext context)
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

        public async Task<Photo> GetPhoto(int id)
        {
            var photo = await _context.Photos.FirstOrDefaultAsync(p => p.Id == id);
            return photo;
        }

        public async Task<User> GetUser(int id)
        {
            return await _context.Users.Include(p=>p.Photos).FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<PagedList<User>> GetUsers(UserParams userParams)
        {
            var users =  _context.Users.Include(p=>p.Photos).AsQueryable();
            users = users.Where(u=> u.Id != userParams.UserId);

            users = users.Where(u=> u.Gender == userParams.Gender);
            if (userParams.MinAge != 18 || userParams.MaxAge != 99){
                var minDob = DateTime.Now.AddYears(-userParams.MaxAge -1);
                var maxDob = DateTime.Now.AddYears(-userParams.MinAge -1);

                users = users.Where(u => u.DateOfBirth >= minDob && u.DateOfBirth <= maxDob);
            }
            if(!string.IsNullOrEmpty(userParams.OrderBy)) {
                switch (userParams.OrderBy) {
                    case "created":
                        users = users.OrderByDescending(u => u.Created);
                        break;
                    default:
                        users = users.OrderByDescending(u => u.LastActive);
                        break;

                }
            }

            return await PagedList<User>.CreateAsync( users
                                                , userParams.PageNumber ,userParams.PageSize);
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}