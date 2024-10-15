namespace ControledeSalaFEMASS.Domain.Dtos;
public class ErrorDto
{
    public IList<string> Errors { get; set; }
    public bool TokenIsExpired { get; set; }

    public ErrorDto(IList<string> errors) => Errors = errors;

    public ErrorDto(string error)
    {
        Errors = new List<string>
        {
            error
        };
    }
}