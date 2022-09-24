namespace Blog.Components.Module.Base;

public class HttpResult<T>
{

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
    public T? Data { get; set; }

}


public class HttpResult
{

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

}
