using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;

namespace FileUploadService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
    public class FileUploadController
    {
        [HttpPost]
        public Task? Upload(string FileName)
        {
            var connectionString = "DefaultEndpointsProtocol=https;" +
                "AccountName=rocketimagestorage;" +
                "AccountKey=Z0uGf4hOLuqddOfRIip9Rx5j/Nb86yOP6ihMzuhlbiSuI9qSsPjur8yxbukGIYyUL6zYK6xIovK8+AStRT3hPg==;" +
                "EndpointSuffix=core.windows.net";

            // intialize BobClient 
            Azure.Storage.Blobs.BlobClient blobClient = new Azure.Storage.Blobs.BlobClient(
                connectionString: connectionString,
                blobContainerName: "rocketimagestorage",
                blobName: FileName);

            // Create the file, or overwrite if the file exists.
            blobClient.Upload(File.Create("Files/" + FileName + ".txt"));

            // string extension = Path.GetExtension(fs.Name);
            // SendMessage _sendMessage = new();
            // _sendMessage.Send(fs.Name + ", " + extension + ", " + DateTime.Now) ;
            return null;
        }
    }
}