using Microsoft.EntityFrameworkCore.Query;
using PhoneBook.Lib.Domain;

namespace PhoneBook.Lib.Infra;

public interface IPersonRepository : IRepository<Person>
{
    Task<Person?> GetRandom(Func<IQueryable<Person>, IIncludableQueryable<Person, object>>? include = null);
}