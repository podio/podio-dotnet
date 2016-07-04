namespace PodioAPI.Models
{
    public class FileResponse
    {
        public byte[] FileContents { get; set; }
        public string ContentType { get; set; }
        public long ContentLength { get; set; }
    }
}