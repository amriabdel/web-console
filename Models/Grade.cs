using System.Text.Json.Serialization;

namespace StudentAPI.Models
{
    public class Grade
    {
        public int Id { get; set; }

        public string Subject { get; set; } = string.Empty;
        public int Score { get; set; }

        public int StudentId { get; set; }

        [JsonIgnore]
        public Student? Student { get; set; }
    }
}