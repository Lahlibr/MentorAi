using NPOI.SS.Formula.Functions;

namespace MentorAi_backd.Models.Entity
{
    public class ApiResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
        public List <String> Errors { get; set; } = new List<string>();
    }
}
