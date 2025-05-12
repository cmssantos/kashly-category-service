namespace Kashly.Category.Communication.Responses;

public class ResponseErrorJson
{
    public List<string> ErrorMessages { get; set; }
    public ResponseErrorJson(string errorMessage) => this.ErrorMessages = [errorMessage];
    public ResponseErrorJson(List<string> errorMessages) => this.ErrorMessages = errorMessages;
}
