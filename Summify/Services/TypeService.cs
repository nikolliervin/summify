public class TypeService : ITypeService
{
    public List<string> Types(){
        return new List<string>(){"youtube", "pdf"};
    }
}