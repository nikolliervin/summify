using System.ComponentModel.DataAnnotations;
public class PdfSummarizeRequest
{
    [Required]
    public string Base64Pdf { get; set; } = string.Empty; 

    [Required]
    public int NumberOfSentences { get; set; }

    public List<string>? Language { get; set; }
}
