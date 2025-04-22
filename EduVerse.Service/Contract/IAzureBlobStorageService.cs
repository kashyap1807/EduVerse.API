using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduVerse.Service.Contract
{
    public interface IAzureBlobStorageService
    {
        Task<string> UploadAsync(byte[] fileData, string fileName, string containerName = "");
    }
}
