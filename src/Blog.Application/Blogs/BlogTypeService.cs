using AutoMapper;
using Blog.Application.Contract.Blogs;
using Blog.Application.Contract.Blogs.Dto;
using Blog.EntityFrameworkCore;
using Blog.Module;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Net.WebSockets;

namespace Blog.Application.Blogs;

/// <summary>
/// 博客类型
/// </summary>
public class BlogTypeService : IBlogTypeService
{
    private readonly BlogDbContext _blogDbContext;
    private readonly IMapper _mapper;
    public BlogTypeService(BlogDbContext blogDbContext, IMapper mapper)
    {
        this._blogDbContext = blogDbContext;
        this._mapper = mapper;
    }

    /// <inheritdoc/>
    public async Task CreateAsync(CreateBlogTypeDto input)
    {
        if(await _blogDbContext.BlogTypes.AnyAsync(x => x.Name == input.Name))
        {
            throw new BusinessExceptions("已经存在相同博客类型");
        }

        var data = _mapper.Map<BlogTypes>(input);
        data.CreationTime = DateTime.Now;

        // 添加到数据库
        await _blogDbContext.BlogTypes.AddAsync(data);

        // 保存操作
        await _blogDbContext.SaveChangesAsync();

    }

    /// <inheritdoc/>
    public async Task DeleteAsync(Guid id)
    {
        var type = await _blogDbContext.BlogTypes.FirstOrDefaultAsync(x => x.Id == id);

        if(type != null)
        {
            _blogDbContext.BlogTypes.Remove(type);

            await _blogDbContext.SaveChangesAsync();
        }
    }


    /// <inheritdoc/>
    public async Task<List<BlogTypeDto>> GetListAsync()
    {
        var data = await _blogDbContext.BlogTypes.ToListAsync();

        var dto = _mapper.Map<List<BlogTypeDto>>(data);

        return dto;
    }

    /// <inheritdoc/>
    public async Task UpdateAsync(UpdateBlogTypeDto input)
    {
        var data = await _blogDbContext.BlogTypes.FirstOrDefaultAsync(x => x.Id == input.Id);

        if(data == null)
        {
            throw new BusinessExceptions("博客类型不存在");
        }

        data.Name = input.Name;

        _blogDbContext.BlogTypes.Update(data);

        await _blogDbContext.SaveChangesAsync();
    }
}
