namespace Api.Dto;

public class AuthenticationResponseDto
{
    public string Username { get; set; }
    public string AccessToken { get; set; }

    public AuthenticationResponseDto() { }

    public AuthenticationResponseDto(string username, string accessToken)
    {
        Username = username;
        AccessToken = accessToken;
    }
}
