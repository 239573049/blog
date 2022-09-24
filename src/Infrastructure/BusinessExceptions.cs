namespace Infrastructure;

/// <summary>
/// 业务异常类
/// </summary>
public class BusinessExceptions : Exception
{
    public int Code { get; }

    public BusinessExceptions(string? message, int code = 400) : base(message)
    {
        Code = code;
    }
}
