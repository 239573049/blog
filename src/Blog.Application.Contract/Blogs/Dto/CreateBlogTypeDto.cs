using System.ComponentModel.DataAnnotations;

namespace Blog.Application.Contract.Blogs.Dto;

public class CreateBlogTypeDto
{
    /// <summary>
    /// 博客类型
    /// </summary>
    [Required(ErrorMessage = "类型名称不能为空")]
    [MinLength(2, ErrorMessage = "博客类型长度不能小于两位")]
    public string Name { get; set; } = null!;
}
