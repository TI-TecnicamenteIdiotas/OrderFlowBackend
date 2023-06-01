using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Net.Http.Headers;
using NimbleFlow.Api.Helpers;
using NimbleFlow.Api.Services;
using NimbleFlow.Contracts.Enums;

namespace NimbleFlow.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class UploadController : ControllerBase
{
    // 1mb in binary bytes
    private const int FileSizeLimit = 1048576;
    private readonly UploadService _uploadService;

    private readonly Dictionary<FileTypesEnum, byte[]> _validFileHeaders = new()
    {
        { FileTypesEnum.Jpeg, new byte[] { 0xFF, 0xD8 } },
        { FileTypesEnum.Png, new byte[] { 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A } }
    };

    public UploadController(UploadService uploadService)
    {
        _uploadService = uploadService;
    }

    /// <summary>Sends a image file to storage</summary>
    /// <param name="file"></param>
    /// <response code="400">Bad Request</response>
    /// <response code="415">Unsupported Media Type</response>
    [HttpPost]
    [Consumes("multipart/form-data")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    public async Task<IActionResult> UploadImage(IFormFile file)
    {
        if (file.Length > FileSizeLimit)
            return BadRequest();

        await using var memoryStream = new MemoryStream();
        await file.CopyToAsync(memoryStream);

        if (memoryStream.Length > FileSizeLimit)
            return BadRequest();

        var fileBytes = memoryStream.ToArray();
        var fileType = fileBytes.GetByteArrayFileType(_validFileHeaders);
        if (fileType is FileTypesEnum.Invalid)
            return new UnsupportedMediaTypeResult();
        var (contentType, fileExtension) = fileType switch
        {
            FileTypesEnum.Jpeg => ("image/jpeg", ".jpeg"),
            FileTypesEnum.Png => ("image/png", ".png"),
            _ => (string.Empty, string.Empty)
        };

        var (responseStatus, response) = await _uploadService.UploadFileAsync(memoryStream, contentType, fileExtension);
        return responseStatus switch
        {
            HttpStatusCode.Created => Created(string.Empty, response),
            _ => Problem()
        };
    }

    /// <summary>Sends a image file to storage</summary>
    /// <response code="400">Bad Request</response>
    /// <response code="415">Unsupported Media Type</response>
    [HttpPost("binary")]
    [Consumes("image/jpeg", "image/png")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    public async Task<IActionResult> UploadBinaryImage()
    {
        Request.Headers.TryGetValue(HeaderNames.ContentType, out var contentType);
        await using var fileBuffer = new FileBufferingReadStream(
            Request.Body,
            FileSizeLimit,
            FileSizeLimit,
            string.Empty
        );
        await using var memoryStream = new MemoryStream();
        await fileBuffer.CopyToAsync(memoryStream);

        if (memoryStream.Length > FileSizeLimit)
            return BadRequest();

        var fileBytes = memoryStream.ToArray();
        var fileType = fileBytes.GetByteArrayFileType(_validFileHeaders);
        if (fileType is FileTypesEnum.Invalid)
            return new UnsupportedMediaTypeResult();
        var fileExtension = fileType switch
        {
            FileTypesEnum.Jpeg => ".jpeg",
            FileTypesEnum.Png => ".png",
            _ => string.Empty
        };

        var (responseStatus, response) = await _uploadService.UploadFileAsync(memoryStream, contentType, fileExtension);
        return responseStatus switch
        {
            HttpStatusCode.Created => Created(string.Empty, response),
            _ => Problem()
        };
    }
}