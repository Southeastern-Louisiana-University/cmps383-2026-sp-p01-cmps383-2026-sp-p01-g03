namespace Selu383.SP26.Api.Entities
{
    public class Locations
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public int TableCount { get; set; }
    }

    public class LocationGetDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public int TableCount { get; set; }
    }

    public class LocationCreateDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public int TableCount { get; set; }
    }

    public class LocationUpdateDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public int TableCount { get; set; }
    }
}