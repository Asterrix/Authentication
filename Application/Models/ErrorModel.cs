namespace Application.Models;

public class ErrorModel
{
    public string? Type { get; set; }
    public string? Title { get; set; }
    public int Status { get; set; }
    public string? Detail { get; set; }
    public string? Instance { get; set; }
    public List<string> ErrorList { get; set; } = new();
}