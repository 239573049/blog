namespace Blog.Components.Module.Base;

/// <summary>
/// 分页模型
/// </summary>
/// <typeparam name="T"></typeparam>
public class PageResponseDto<T>
{
    /// <summary>
    /// 总数
    /// </summary>
    public int Total { get; set; }

    public List<T> Items { get; set; }

    /// <inheritdoc/>
    public PageResponseDto(int total, List<T> items)
    {
        Total = total;
        Items = items;
    }

    public PageResponseDto()
    {
        Items= new List<T>();
    }
}
