using Firebase.Auth;
using Firebase.Storage;

namespace Planitly.Backend.Services
{
    public interface IFileService
    {
        public Task<string> UploadEventPicture(Stream stream);
        public Stream ImageBase64ToStream(string base64);
    }

    public class FileService : IFileService
    {
        public string? firebaseToken;
        IConfiguration _configuration;
        public FileService(IConfiguration configuration)
        {
            _configuration = configuration;

            var firebaseApiKey = _configuration.GetValue<string?>("FirebaseApiKey");
            var authProvider = new FirebaseAuthProvider(new FirebaseConfig(firebaseApiKey));
            this.firebaseToken = authProvider.SignInAnonymouslyAsync().Result.FirebaseToken;
        }

        public async Task<string> UploadEventPicture(Stream stream)
        {
            var url = await new FirebaseStorage("planitly.appspot.com", new FirebaseStorageOptions
            {
                AuthTokenAsyncFactory = () => Task.FromResult(this.firebaseToken),
                ThrowOnCancel = true,
            }).Child("eventPictures").Child(Nanoid.Nanoid.Generate("1234567890abcdef", 10)).PutAsync(stream);

            return url;
        }

        public Stream ImageBase64ToStream(string base64)
        {
            var metadata = base64.Split(',')[0];
            var data = base64.Split(',')[1];
            
            return new MemoryStream(Convert.FromBase64String(data));
        }
    }
}