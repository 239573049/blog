using AutoMapper;
using Blog.Application.Contract.Base;
using Blog.Application.Contract.Blogs;
using Blog.Application.Contract.Blogs.Dto;
using Blog.EntityFrameworkCore;
using Blog.Module;
using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Blog.Application.Blogs;

/// <summary>
/// 博客
/// </summary>
public class BlogServer : IBlogServer
{
    private readonly BlogDbContext _blogDbContext;
    private readonly IMapper _mapper;
    private readonly CurrentService _currentService;

    public BlogServer(BlogDbContext blogDbContext, IMapper mapper, CurrentService currentService)
    {
        _blogDbContext = blogDbContext;
        this._mapper = mapper;
        this._currentService = currentService;
    }

    /// <inheritdoc/>
    public async Task AddPageViewAsync(Guid blogId)
    {
        var data = await _blogDbContext.Blogs.FirstOrDefaultAsync(x => x.Id == blogId);
        if(data != null)
        {
            data.PageView++;

            _blogDbContext.Blogs.Update(data);

            await _blogDbContext.SaveChangesAsync();
        }
    }

    /// <inheritdoc/>
    public async Task CreateAsync(CreateBlogsDto input)
    {
        var userId = _currentService.GetUserId();

        // 查询数据库是否存在指定博客文章类型
        if(!await _blogDbContext.BlogTypes.AnyAsync(x => x.Id == input.TypeId))
        {
            throw new BusinessExceptions("博客文章类型不存在");
        }

        var data = _mapper.Map<Module.Blogs>(input);

        // 赋值作者id
        data.AuthorId = userId;

        // 添加到数据库
        await _blogDbContext.AddAsync(data);

        // 保存操作
        await _blogDbContext.SaveChangesAsync();
    }

    /// <inheritdoc/>
    public async Task CreateCommentAsync(CreateCommentDto input)
    {
        if(!await _blogDbContext.Blogs.AnyAsync(x => x.Id == input.BlogId))
        {
            throw new BusinessExceptions("博客不存在");
        }

        // 获取当前用户id
        var userId = _currentService.GetUserId();

        var data = _mapper.Map<BlogComments>(input);
        data.UserId = userId; // 设置评论人
        data.CreationTime = DateTime.Now; // 评论时间

        // 添加博客评论
        await _blogDbContext.BlogComments.AddAsync(data);

        await _blogDbContext.SaveChangesAsync();
    }

    /// <inheritdoc/>
    public async Task DeleteAsync(Guid id)
    {
        var userId = _currentService.GetUserId();

        // 获取博客 并且只能删除自己的博客
        var data = await _blogDbContext.Blogs.FirstOrDefaultAsync(x => x.Id == id && x.AuthorId == userId);

        if(data != null)
        {
            // 删除博客
            _blogDbContext.Blogs.Remove(data);

            // 保持操作
            await _blogDbContext.SaveChangesAsync();
        }
    }

    /// <inheritdoc/>
    public async Task DeleteCommentAsync(Guid id)
    {
        var userId = _currentService.GetUserId();

        // 获取本人发送的评论
        var comment = await _blogDbContext.BlogComments.FirstOrDefaultAsync(x => x.Id == id && x.UserId == userId);

        if(comment == null)
        {
            return;
        }

        // 删除评论
        _blogDbContext.BlogComments.Remove(comment);

        await _blogDbContext.SaveChangesAsync();
    }

    /// <inheritdoc/>
    public async Task<BlogDto> GetAsync(Guid id)
    {
        var data = await _blogDbContext.Blogs.Where(x => x.Id == id)
        .AsNoTracking()  // 禁用跟踪查询
        .AsSplitQuery()
        .Include(x => x.Type)  // 导航属性查询类型信息
        .Include(x => x.Author) // 导航属性查询用户信息
        .FirstOrDefaultAsync();

        if(data == null)
        {
            throw new BusinessExceptions("博客不存在", 404);
        }

        var dto = _mapper.Map<BlogDto>(data);

        var comment = await _blogDbContext.BlogComments.Where(x => x.BlogId == id).OrderByDescending(x => x.CreationTime).ToListAsync();

        dto.BlogComments = _mapper.Map<List<BlogCommentsDto>>(comment);

        // 获取博客点赞数量
        dto.Like = await _blogDbContext.BlogLikes.LongCountAsync(x => x.BlogId == id);

        return dto;
    }

    /// <inheritdoc/>
    public async Task<PageResponseDto<PageBlogDto>> GetBlogListAsync(BlogInput input)
    {
        var data = _blogDbContext.Blogs.AsQueryable()
        ;

        if(input.TypeId.HasValue)
        {
            data = data.Where(x => x.TypeId == input.TypeId);
        }

        if(!string.IsNullOrEmpty(input.Keyword))
        {
            data = data.Where(x => x.Title.Contains(input.Keyword) || x.Content.Contains(input.Keyword));
        }

        data = data
        .Include(x => x.Type)
        .Include(x => x.Author)
        .OrderBy(x => x.PageView)
        .OrderByDescending(x => x.CreationTime);

        var result = await data.Skip(input.SkipCount).Take(input.MaxResultCount).ToListAsync();

        var count = await data.CountAsync();

        var likes = await _blogDbContext.BlogLikes.Where(x => result.Select(s => s.Id).Contains(x.BlogId)).Select(x => x.BlogId).ToListAsync();

        var dto = _mapper.Map<List<PageBlogDto>>(data);

        foreach(var d in dto)
        {
            d.Like = likes.Count(x => x == d.Id);
        }

        return new PageResponseDto<PageBlogDto>(count, dto);

    }

    /// <inheritdoc/>
    public async Task LikeAsync(Guid id)
    {
        var userId = _currentService.GetUserId();

        var data = await _blogDbContext.BlogLikes.FirstOrDefaultAsync(x => x.BlogId == id && x.UserId == userId);

        // 是否存在点赞 存在就取消点赞不存在就新增点赞
        if(data == null)
        {
            data = new BlogLikes()
            {
                UserId = userId,
                BlogId = id,
                CreationTime = DateTime.Now,
            };

            await _blogDbContext.BlogLikes.AddAsync(data);

        }
        else
        {
            _blogDbContext.BlogLikes.Remove(data);
        }

        await _blogDbContext.SaveChangesAsync();
    }

    /// <inheritdoc/>
    public async Task UpdateAsync(UpdateBlogDto input)
    {

        var userId = _currentService.GetUserId();

        var data = await _blogDbContext.Blogs.FirstOrDefaultAsync(x => x.AuthorId == userId && x.Id == input.Id);

        if(data == null)
        {
            throw new BusinessExceptions("博客不存在");
        }

        // 将Input的数据映射到data
        _mapper.Map(input, data);

        // 更新data
        _blogDbContext.Blogs.Update(data);

        // 保存到数据库
        await _blogDbContext.SaveChangesAsync();
    }
}
