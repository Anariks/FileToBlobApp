using System.Text.Json;

namespace Models;

public enum ErrorCode
{
    BadRequest = 400,
    Unauthorized = 401,
    NotFound = 404,
    InternalServerError = 500,
}

public class ErrorResponse
{
    public ErrorCode ErrorCode { get; set; }
    public string? Message { get; set; }
}
