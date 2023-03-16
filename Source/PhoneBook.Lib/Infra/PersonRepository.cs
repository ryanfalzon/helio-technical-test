using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using PhoneBook.Lib.Domain;

namespace PhoneBook.Lib.Infra;

public class PersonRepository : GenericRepository<Person>, IPersonRepository
{
    public PersonRepository(PhoneBookDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<Person?> GetRandom(Func<IQueryable<Person>, IIncludableQueryable<Person, object>>? include = null)
    {
        IQueryable<Person> query = DbSet;

        var total = await query.CountAsync();
        var randomIndex = new Random().Next(0, total);
        
        if (include != null)
        {
            query = include(query);
        }
        
        return await query.Skip(randomIndex).FirstOrDefaultAsync();
    }
}