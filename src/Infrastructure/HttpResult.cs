namespace Infrastructure;

public class HttpResult
{
    public HttpResult(int code)
    {
        Code = code;
    }

    /// <summary>
    /// 状态码
    /// </summary>
    public int Code { get; set; }

    /// <summary>
    /// 异常信息
    /// </summary>

    public string? Message { get; set; }

    /// <summary>
    /// 数据
    /// </summary>
    public object? Data { get; set; }


    public HttpResult(int code, string? message)
    {
        this.Code = code;
        this.Message = message;
    }

    public HttpResult(int code, object? data)
    {
        this.Code = code;
        this.Data = data;
    }

    public HttpResult(int code, string? message, object? data)
    {
        this.Code = code;
        this.Message = message;
        this.Data = data;
    }
}
