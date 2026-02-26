namespace StudentAPI.Models
{
    public class Grade
    {
        public int Id { get; set; }
        public string Subject { get; set; } // e.g., "Math"
        public int Score { get; set; }      // e.g., 95
        
        // Relationship: This connects the grade to a specific student
        public int StudentId { get; set; } 
    }
}