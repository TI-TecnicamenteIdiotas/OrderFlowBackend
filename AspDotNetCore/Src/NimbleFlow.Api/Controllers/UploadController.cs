using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Net.Http.Headers;
using NimbleFlow.Api.Helpers;
using NimbleFlow.Api.Services;
using NimbleFlow.Contracts.Constants;
using NimbleFlow.Contracts.Enums;

namespace NimbleFlow.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class UploadController : ControllerBase
{
    // 1mb in binary bytes
    private const int FileSizeLimit = 1048576;

    private readonly Dictionary<FileTypeEnum, byte[]> _acceptedFileSignatures = new()
    {
        { FileTypeEnum.Jpeg, FileSignatures.Jpeg },
        { FileTypeEnum.Png, FileSignatures.Png }
    };

    private readonly UploadService _uploadService;

    public UploadController(UploadService uploadService)
    {
        _uploadService = uploadService;
    }

    /// <summary>Sends a image file to storage, consumes a binary file</summary>
    /// <response code="400">Bad Request</response>
    /// <response code="415">Unsupported Media Type</response>
    [HttpPost("image")]
    [Consumes("image/jpeg", "image/png")]
    [ProducesResponseType(typeof(string), StatusCodes.Status201Created)]
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
        var fileSignatureType = fileBytes.GetFileTypeBySignature(_acceptedFileSignatures);
        if (fileSignatureType is FileTypeEnum.Unknown)
            return new UnsupportedMediaTypeResult();
        var fileExtension = fileSignatureType switch
        {
            FileTypeEnum.Jpeg => ".jpeg",
            FileTypeEnum.Png => ".png",
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