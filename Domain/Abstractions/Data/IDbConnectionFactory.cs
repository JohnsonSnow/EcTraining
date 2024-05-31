namespace Domain.Abstractions.Data;

public interface IDbConnectionFactory
{
    IDbConnectionFactory CreateOpenConnection();
}
