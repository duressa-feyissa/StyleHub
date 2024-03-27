namespace Domain.Entities
{
    public class Material
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public required string Name { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}