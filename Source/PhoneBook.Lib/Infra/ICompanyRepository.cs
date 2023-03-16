using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;
using PhoneBook.Lib.Domain;

namespace PhoneBook.Lib.Infra;

public interface ICompanyRepository : IRepository<Company>
{
    Task<IEnumerable<CompanyWithTotalPeople>> GetWithTotalPeopleAsync(
        Expression<Func<Company, bool>>? filter = null,
        Func<IQueryable<Company>, IOrderedQueryable<Company>>? orderBy = null);

    Task<CompanyWithTotalPeople?> GetByIdWithTotalPeopleAsync(int id,
        Func<IQueryable<Company>, IIncludableQueryable<Company, object>>? include = null);
}