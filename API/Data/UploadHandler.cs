namespace API.Data
{
    public class UploadHandler
    {

        public string Uploud(IFormFile file)
        {
            string extention = Path.GetExtension(file.FileName);

            long size = file.Length;

            string fileName = Guid.NewGuid().ToString() + extention;

            string path = Path.Combine(Directory.GetCurrentDirectory(), "Uplaods");

             using FileStream stream = new FileStream(Path.Combine(path, fileName), FileMode.Create);

            file.CopyTo(stream);
            // using ==  stream.Dispose(); && stream.Close(); 

            return fileName;

        }
    }
}
