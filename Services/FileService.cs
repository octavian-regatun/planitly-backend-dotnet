using Amazon.S3;

namespace Planitly.Backend.Services
{
    public interface IFileService
    {
        public Task UploadImage();
    }

    public class FileService : IFileService
    {
        IConfiguration? _config;
        AmazonS3Client _s3Client;
        public FileService(IConfiguration? config)
        {
            this._config = config;
            var accessKey = _config.GetValue<string?>("ScaleWayAccessKey");
            var secretKey = _config.GetValue<string?>("ScaleWayAccessKey");

            var s3Config = new AmazonS3Config() { ServiceURL = "https://planitly.s3.fr-par.scw.cloud" };

            this._s3Client = new AmazonS3Client(
                accessKey, secretKey, s3Config
            );
        }

        public async Task UploadImage()
        {
            var x = await this._s3Client.ListBucketsAsync();
            var y = x.Buckets;
            int i;
        }
    }
}