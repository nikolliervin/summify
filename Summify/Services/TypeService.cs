using System.Collections.Generic;
public class TypeService : ITypeService
{
    public Dictionary<string, string> Types(){
        return new Dictionary<string, string>(){
            {"youtube", "Youtube videos"},
            {"pdf", "Pdf Documents"},
            {"article/website" ,"Articles & Websites"},
            {"text", "Plain text"}
        };
    }

    public Dictionary<string, string> Models(){
        return new Dictionary<string, string>(){
            {"llama3", "llama3"},
            {"tinyllama", "tinyllama"},
            {"tinydolphin" ,"tinydolphin"}
        };
        
    }
}