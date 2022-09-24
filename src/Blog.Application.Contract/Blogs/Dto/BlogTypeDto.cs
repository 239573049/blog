using Blog.Application.Contract.Base;

namespace Blog.Application.Contract.Blogs.Dto;

public class BlogTypeDto : EntityDto
{
    public string Name { get; set; } = null!;
}
