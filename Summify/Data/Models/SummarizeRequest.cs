using System.ComponentModel.DataAnnotations;

public class SummarizeRequest{
    
    [Required]
    public string? VideoUrl { get; set; }

    public int? NumberOfSentences { get; set;}

    public List<string>? Language { get; set; }

}