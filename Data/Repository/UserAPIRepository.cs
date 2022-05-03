using Core.Domain;
using Data.Context;
using Manager.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class UserAPIRepository : IUserAPIRepository
    {

        private readonly DataContext _context;

        public UserAPIRepository(DataContext context)
        {
            this._context = context;
        }

        public async Task<IEnumerable<UserAPI>> GetAsync()
        {
            return await _context.UsersAPI.Include(p => p.userProfiles).ToListAsync();
        }

        public async Task<UserAPI> GetAsync(int id)
        {
            return await _context.UsersAPI
                .Include(p => p.userProfiles)
                .SingleOrDefaultAsync(p => p.Id == id);
        }

        public async Task<UserAPI> GetAsync(string login)
        {
            return await _context.UsersAPI
                .Include(p => p.userProfiles)
                .SingleOrDefaultAsync(p => p.Login == login);
        }

        public async Task<UserAPI> InsertAsync(UserAPI userAPI)
        {
            await InsertUserAPIProfileAsync(userAPI);
            await _context.UsersAPI.AddAsync(userAPI);
            await _context.SaveChangesAsync();
            return userAPI;
        }

        private async Task InsertUserAPIProfileAsync(UserAPI userAPI)
        {
            var profileSearch = new List<UserProfile>();
            foreach (var profile in userAPI.userProfiles)
            {
                var profileFound = await _context.UserProfiles.FindAsync(profile.Id);
                profileSearch.Add(profileFound);
            }
            userAPI.userProfiles = profileSearch;
        }

        public async Task<UserAPI> UpdateAsync(UserAPI userAPI)
        {
            var usuarioSearch = await _context.UsersAPI.FindAsync(userAPI.Login);
            if (usuarioSearch == null)
            {
                return null;
            }
            _context.Entry(usuarioSearch).CurrentValues.SetValues(userAPI);
            await _context.SaveChangesAsync();
            return usuarioSearch;
        }
    }
}
