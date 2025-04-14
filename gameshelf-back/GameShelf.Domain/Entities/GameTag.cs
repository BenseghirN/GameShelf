namespace GameShelf.Domain.Entities
{
    public class GameTag
    {
        public Guid GameId { get; set; }
        public Game Game { get; set; } = default!;

        public Guid TagId { get; set; }
        public Tag Tag { get; set; } = default!;
    }
}