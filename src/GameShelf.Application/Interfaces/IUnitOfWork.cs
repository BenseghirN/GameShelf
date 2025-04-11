namespace GameShelf.Application.Interfaces
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository { get; }
        // IGameRepository GameRepository { get; }
        Task SaveChangesAsync();
    }
}