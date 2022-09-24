using Blog.Components.Module.Base;

namespace Blog.Components.Module;

public class BlogTypeDto
{
    public Guid? Id { get; set; }
    public string Name { get; set; } = null!;
}
