using System.ComponentModel.DataAnnotations;

public class SummarizeRequest{
    
    [Required]
    public string? VideoUrl { get; set; }
}