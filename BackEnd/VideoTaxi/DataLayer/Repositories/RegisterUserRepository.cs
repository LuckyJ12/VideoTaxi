using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repositories
{
    public class RegisterUserRepository:IRegisterUserRepositoryInterface
    {
        private readonly DataContext _context;

        public RegisterUserRepository(DataContext context)
        {
            _context = context;
        }

        public async Task SaveToDb()
        {
            await _context.SaveChangesAsync();
        }
    }
}
