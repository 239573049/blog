using Blog.Application.Contract.Blogs;
using Blog.Application.Contract.Blogs.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.HttpApi.Host.Controllers;

/// <summary>
/// 博客类型
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class BlogTypeController : ControllerBase
{
    private readonly IBlogTypeService _blogTypeService;

    public BlogTypeController(IBlogTypeService blogTypeService)
    {
        this._blogTypeService = blogTypeService;
    }

    /// <summary>
    /// 创建博客类型
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task CreateAsync(CreateBlogTypeDto input)
    {
        await _blogTypeService.CreateAsync(input);
    }

    /// <summary>
    /// 删除博客类型
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task DeleteAsync(Guid id) =>
        await _blogTypeService.DeleteAsync(id);

    /// <summary>
    /// 更新博客类型
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPut]
    [Authorize(Roles ="Admin")]
    public async Task UpdateAsync(UpdateBlogTypeDto input)
    {
        await _blogTypeService.UpdateAsync(input);
    }

    /// <summary>
    /// 获取博客所有类型
    /// </summary>
    /// <returns></returns>
    [HttpGet("list")]
    public async Task<List<BlogTypeDto>> GetListAsync()
    {
        return await _blogTypeService.GetListAsync();
    }
}
