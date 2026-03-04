namespace StudentAPI.DTOs
{
    public class GradeCreateDto
    {
        public string Subject { get; set; } = string.Empty;
        public int Score { get; set; }
    }
}