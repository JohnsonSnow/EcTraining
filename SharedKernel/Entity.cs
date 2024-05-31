namespace SharedKernel;

public abstract class Entity
{
    private readonly List<IDomainEvent> _domainEvents = new();

    protected Entity(Guid id) 
    {
        Id = id;
    }

    protected Entity() { }

    public Guid Id { get; init; }

    public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.ToList();

    protected void Raise(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    public object GetDomainEvents()
    {
        throw new NotImplementedException();
    }
}
