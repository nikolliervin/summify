using System.ComponentModel.DataAnnotations;

public class SummarizeRequest
{
    [Required]
    public string Type { get; set; } = string.Empty; 

    [Required]
    public string Content { get; set; } = string.Empty; 

    public int NumberOfSentences { get; set; }

    public string Model {get; set;} 
}