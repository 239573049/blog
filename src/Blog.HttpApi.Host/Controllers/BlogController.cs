using Blog.Application.Contract.Base;
using Blog.Application.Contract.Blogs;
using Blog.Application.Contract.Blogs.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.HttpApi.Host.Controllers;

/// <summary>
/// 博客
/// </summary>
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class BlogController : ControllerBase
{
    private readonly IBlogServer _blogServer;

    public BlogController(IBlogServer blogServer)
    {
        this._blogServer = blogServer;
    }

    /// <summary>
    /// 新增博客
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task CreateAsync(CreateBlogsDto input)
    {
        await _blogServer.CreateAsync(input);
    }

    /// <summary>
    /// 更新浏览量
    /// </summary>
    /// <param name="blogId"></param>
    /// <returns></returns>
    [HttpGet("page-view/{blogId}")]
    [AllowAnonymous]
    public async Task AddPageViewAsync(Guid blogId)
    {
        await _blogServer.AddPageViewAsync(blogId);
    }

    /// <summary>
    /// 更新博客
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPut]
    public async Task UpdateAsync(UpdateBlogDto input)
    {
        await _blogServer.UpdateAsync(input);
    }

    /// <summary>
    /// 删除博客
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task DeleteAsync(Guid id)
    {
        await _blogServer.DeleteAsync(id);
    }

    /// <summary>
    /// 获取博客详情
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    [AllowAnonymous] // 标记无权限
    public async Task<BlogDto> GetAsync(Guid id)
    {
        return await _blogServer.GetAsync(id);
    }

    /// <summary>
    /// 点赞博客
    /// </summary>
    /// <param name="id">博客Id</param>
    /// <returns></returns>
    [HttpGet("like/{id}")]
    public async Task LikeAsync(Guid id)
    {
        await _blogServer.LikeAsync(id);
    }

    /// <summary>
    /// 添加评论
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost("comment")]
    public async Task CreateCommentAsync(CreateCommentDto input)
    {
        await _blogServer.CreateCommentAsync(input);
    }

    /// <summary>
    /// 删除评论
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("comment/{id}")]
    public async Task DeleteCommentAsync(Guid id)
    {
        await _blogServer.DeleteCommentAsync(id);
    }

    /// <summary>
    /// 获取博客推荐类
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpGet("list")]
    [AllowAnonymous] // 标记无权限
    public async Task<PageResponseDto<PageBlogDto>> GetListAsync([FromQuery]BlogInput input)
    {
        return await _blogServer.GetBlogListAsync(input);
    }
}
