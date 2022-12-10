using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Json.Differ.Application.Files.UploadFileToCompare
{
    public class FileUploadedDto
    {
        public FileUploadedDto()
        {

        }

        public Guid ExternalId { get; set; }
        public DateTimeOffset UploadedOn { get; set; }
    }
}
