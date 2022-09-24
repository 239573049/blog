using Blog.Application.Contract.Blogs.Dto;

namespace Blog.Application.Contract.Blogs;

public interface IBlogTypeService
{
    /// <summary>
    /// 添加博客类型
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task CreateAsync(CreateBlogTypeDto input);

    /// <summary>
    /// 删除博客类型
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task DeleteAsync(Guid id);

    /// <summary>
    /// 更新博客
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task UpdateAsync(UpdateBlogTypeDto input);

    /// <summary>
    /// 博客分类
    /// </summary>
    /// <returns></returns>
    Task<List<BlogTypeDto>> GetListAsync();
}
