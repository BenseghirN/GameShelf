using GameShelf.Application.Interfaces;

namespace GameShelf.Application.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly GameShelfDbContext _context;
        public IUserRepository UserRepository { get; }
        // public IGameRepository GameRepository { get; }

        public UnitOfWork(GameShelfDbContext context, IUserRepository userRepository)
        {
            _context = context;
            UserRepository = userRepository;
            // GameRepository = gameRepository;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}