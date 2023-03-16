using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;
using PhoneBook.Lib.Domain;

namespace PhoneBook.Lib.Infra;

public class CompanyRepository : GenericRepository<Company>, ICompanyRepository
{
    public CompanyRepository(PhoneBookDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<IEnumerable<CompanyWithTotalPeople>> GetWithTotalPeopleAsync(
        Expression<Func<Company, bool>>? filter = null,
        Func<IQueryable<Company>, IOrderedQueryable<Company>>? orderBy = null)
    {
        IQueryable<Company> query = DbSet;

        if (filter != null)
        {
            query = query.Where(filter);
        }

        if (orderBy != null)
        {
            return await orderBy(query).Select(x => new CompanyWithTotalPeople(x.Id, x.Name, x.RegistrationDate, x.People.Count)).ToListAsync();
        }

        return await query.Select(x => new CompanyWithTotalPeople(x.Id, x.Name, x.RegistrationDate, x.People.Count)).ToListAsync();
    }

    public async Task<CompanyWithTotalPeople?> GetByIdWithTotalPeopleAsync(int id,
        Func<IQueryable<Company>, IIncludableQueryable<Company, object>>? include = null)
    {
        IQueryable<Company> query = DbSet;

        if (include != null)
        {
            query = include(query);
        }

        return await query
            .Where(x => x.Id == id)
            .Select(x => new CompanyWithTotalPeople(x.Id, x.Name, x.RegistrationDate, x.People.Count))
            .SingleOrDefaultAsync();
    }
}