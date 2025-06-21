using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Mvc;

namespace CLDV6211PART_1_App.Controllers
{
    
    public class FileController : Controller
    {
        private readonly string connectionString = "DefaultEndpointsProtocol=https;AccountName=saheel;AccountKey=TM7EoJgqH99ItV6J4aBV61TMskVM0jsK/IQQHpWzYvbSadJP9mfzGniZcNrYq5TptGr4cAULeAto+AStxr0Vxw==;EndpointSuffix=core.windows.net";
        private readonly string containerName = "images";
        //Get: File (display upload form and list of images)
        
        public async Task<IActionResult> Index()
        {
            var imageUrls = await FetchImageUrlsAsync();
            return View(imageUrls);
        }
        //Post: File/Upload (handle file upload to azure blob storage)
        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile uploadedFile) 
        {
            if (uploadedFile != null && uploadedFile.Length > 0)
            {
                //upload the file to blob storage
                await UploadFileToBlobStorageAsync(uploadedFile);
            } 
            //redirect back to the index view to refresh the file list
            return RedirectToAction("Index");
        }
        //Get: File/ViewFile (display a single file embedded on the webpage)
        public IActionResult ViewFile(string fileUrl)
        {
            if (string.IsNullOrEmpty(fileUrl))
            {
                return NotFound("File not found"); //return 404 if file url not provided
            }
            ViewBag.FileUrl = fileUrl; //pass the file url to the view
            return View();
        }

        private async Task<List<string>> FetchImageUrlsAsync()
        {
            var imageUrls = new List<string>();
            var containerClient = new BlobContainerClient(connectionString, containerName);

            await foreach (BlobItem blobItem in containerClient.GetBlobsAsync()) 
            {
                var blobClient = containerClient.GetBlobClient(blobItem.Name);
                imageUrls.Add(blobClient.Uri.ToString());
            }

            return imageUrls;
        }
        private async Task UploadFileToBlobStorageAsync(IFormFile uploadedFile)
        {
            var containerClient = new BlobContainerClient(connectionString, containerName);
            await containerClient.CreateIfNotExistsAsync(PublicAccessType.Blob); //Ensure contianer exists

            //create a BlobClient for the uploaded file
            var blobClient = containerClient.GetBlobClient(uploadedFile.FileName);

            //upload the file stream asnchronously
            using (var stream = uploadedFile.OpenReadStream())
            {
                await blobClient.UploadAsync(stream, true);
            }
        }
    }
}
