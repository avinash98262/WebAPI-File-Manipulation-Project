using Data;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class FileRepository :IFileRepository
    {

        private readonly FileContext _Context;

        public FileRepository(FileContext context)
        {
            _Context = context;
        }

        public async Task<List<string>> GetAllFiles()
        {
            var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "Upload", "Files");
            if (!Directory.Exists(directoryPath))
            {
                return new List<string>(); // Return an empty list if directory doesn't exist
            }

            var files = Directory.GetFiles(directoryPath);
            var fileNames = files.Select(filePath => Path.GetFileName(filePath)).ToList();
            return fileNames;
        }




 
        public string GetUniqueFilePath(string fileName)
        {
            var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "Upload", "Files");
            var filePath = Path.Combine(directoryPath, fileName);

            // Check if the file already exists
            if (System.IO.File.Exists(filePath))
            {
                // Get the file extension
                var extension = Path.GetExtension(fileName);

                // Remove the extension from the file name
                var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);

                // Attempt to find a unique file name by appending numbers
                var uniqueFileName = fileNameWithoutExtension;
                var count = 1;
                while (System.IO.File.Exists(filePath))
                {
                    uniqueFileName = $"{fileNameWithoutExtension}_{count}{extension}";
                    filePath = Path.Combine(directoryPath, uniqueFileName);
                    count++;
                }

                // Return the unique file path
                return filePath;
            }

            // If the file does not exist, return the original file path
            return filePath;
        }




        public bool DeleteFile( string fileName)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "Upload", "Files");
            var filePath = Path.Combine(path, fileName);


            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
                return true; 
            }
            return false; 
          
        }


    }
}
