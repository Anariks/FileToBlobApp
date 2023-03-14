using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services;

namespace FileBlob.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class FileController : ControllerBase
{
    private readonly ILogger<FileController> _logger;
    private readonly FileService _service;

    public FileController(ILogger<FileController> logger, FileService fileService)
    {
        _service = fileService;
        _logger = logger;
    }

    [HttpPost(nameof(UploadFile))]
    public async Task<IActionResult> UploadFile([FromForm] FormData formData)
    {
        await _service.UploadToStorage(formData);
        return Ok("File Uploaded Successfully");
    }
}
