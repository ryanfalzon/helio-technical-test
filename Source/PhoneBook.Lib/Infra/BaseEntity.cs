namespace PhoneBook.Lib.Infra;

public abstract class BaseEntity
{
    public virtual int Id { get; protected set; }
    
    internal void __setId(int id) => Id = id;
}