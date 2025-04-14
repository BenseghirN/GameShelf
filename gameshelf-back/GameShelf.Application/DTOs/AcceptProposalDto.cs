namespace GameShelf.Application.DTOs
{
    public class AcceptProposalDto
    {
        public string Description { get; set; } = string.Empty;
        public List<TagDto> Tags { get; set; } = new();
    }
}