namespace SK.Visit.Application.Dtos
{
    public class NoteDto
    {
        public string CustomerId { get; set; }

        public Guid DestinationId { get; set; } = Guid.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public string Descreption { get; set; }
    }
}
