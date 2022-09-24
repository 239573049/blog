using Blog.HttpApi.Host.Options;
using Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Blog.HttpApi.Host.Controllers;

/// <summary>
/// 上传服务
/// </summary>
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class FileController : ControllerBase
{
    private readonly BlogFileOptions _blogFileOptions;

    public FileController(IOptions<BlogFileOptions> blogFileOptions)
    {
        this._blogFileOptions = blogFileOptions.Value;
    }

    /// <summary>
    /// 上传文件
    /// </summary>
    /// <param name="file"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<string> UploadingAsync(IFormFile file)
    {
        // 限制文件大小
        if(file.Length > ((_blogFileOptions.MaxSize * 1024) * 1024))
        {
            throw new BusinessExceptions("文件超过限制的大小");
        }

        var name = Guid.NewGuid().ToString("N") + file.FileName;

        // 是否存在文件夹
        if(!Directory.Exists("./wwwroot/file"))
        {
            Directory.CreateDirectory("./wwwroot/file");
        }

        var fileStream = System.IO.File.Create(Path.Combine("./wwwroot/file", name));

        await file.OpenReadStream().CopyToAsync(fileStream);

        // 释放文件流
        await file.OpenReadStream().DisposeAsync();

        fileStream.Close();

        return "file/" + name;
    }
}
