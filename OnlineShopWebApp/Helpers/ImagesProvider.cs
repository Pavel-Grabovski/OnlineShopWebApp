namespace OnlineShopWebApp.Helpers
{
    public class ImagesProvider
    {
        private readonly IWebHostEnvironment appEnvironment;

        public ImagesProvider(IWebHostEnvironment appEnvironment)
        {
            this.appEnvironment = appEnvironment;
        }

        public IEnumerable<string> SafeFiles(string name, IFormFile[] files, ImageFolders folder)
        {
            var imagePaths = new List<string>();
            if(files != null && files.Length > 0)
            {
                foreach (var file in files)
                {
                    var imagePath = SafeFiles(name, file, folder);
                    imagePaths.Add(imagePath);
                }
            }
            return imagePaths;
        }
        public string SafeFiles(string name, IFormFile file, ImageFolders folder)
        {
            if(file != null)
            {
                var folderPath = Path.Combine(appEnvironment.WebRootPath + "/images/" + folder);
                if(!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                var fileName = name + "_" + Guid.NewGuid() + "." + file.FileName.Split('.').Last();
                string path = Path.Combine(folderPath, fileName);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
                return "/images/" + folder + "/" + fileName;
            }
            return null;
        }
    }
}
