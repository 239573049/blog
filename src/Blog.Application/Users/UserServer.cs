using AutoMapper;
using Blog.Application.Contract.User;
using Blog.Application.Contract.User.Dto;
using Blog.EntityFrameworkCore;
using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Blog.Application.Users;

public class UserServer : IUserServer
{
    private readonly BlogDbContext BlogDbContext;
    private readonly IMapper _mapper;
    private readonly CurrentService _currentService;
    public UserServer(BlogDbContext blogDbContext, IMapper mapper, CurrentService currentService)
    {
        BlogDbContext = blogDbContext;
        this._mapper = mapper;
        this._currentService = currentService;
    }

    /// <inheritdoc/>
    public async Task CreateUserAsync(CreateUserDto input)
    {
        if(await BlogDbContext.Users.AnyAsync(x => x.Username == input.Username))
        {
            throw new BusinessExceptions("用户名已存在");
        }

        // Mapper 映射
        var data = _mapper.Map<Module.Users>(input);
        data.CreationTime = DateTime.Now;

        // 添加到数据库
        await BlogDbContext.AddAsync(data);

        // 保存操作
        await BlogDbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var user = await BlogDbContext.Users.FirstOrDefaultAsync(x => x.Id == id);

        if(user != null)
        {
            // 删除用户
            BlogDbContext.Users.Remove(user);

            // 保存操作
            await BlogDbContext.SaveChangesAsync();
        }
    }

    public async Task<UserDto> GetAsync()
    {
        var userId = _currentService.GetUserId();

        var data = await BlogDbContext.Users.FirstOrDefaultAsync(x=>x.Id==userId);

        var dto = _mapper.Map<UserDto>(data);

        return dto;
    }

    /// <inheritdoc/>
    public async Task<UserDto> LoginAsync(LoginDto input)
    {
        // 更具用户名和密码查询用户
        var data = await BlogDbContext.Users.FirstOrDefaultAsync(x => x.Username == input.Username && x.Password == input.Password);

        if(data == null)
        {
            throw new BusinessExceptions("账号或密码错误");
        }

        var dto = _mapper.Map<UserDto>(data);

        return dto;
    }

    /// <inheritdoc/>
    public async Task UpdateAsync(UpdateUserDto user)
    {
        var userId = _currentService.GetUserId();

        var result = await BlogDbContext.Users.FirstOrDefaultAsync(x => x.Id == userId);

        if(result == null)
        {
            return;
        }

        _mapper.Map(user, result);

        BlogDbContext.Users.Update(result);

        await BlogDbContext.SaveChangesAsync();
    }
}
