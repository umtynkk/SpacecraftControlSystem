using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpacecraftControlSystem.Models
{
    public class ResponseBaseEmpty
    {
        public ResponseBaseEmpty()
        {

        }

        public ResponseBaseEmpty(bool success)
        {
            Meta = new ResponseBaseEmptyMetaModel
            {
                Success = success
            };
        }
        public ResponseBaseEmpty(bool success, string? message)
        {
            Meta = new ResponseBaseEmptyMetaModel
            {
                Success = success,
                Message = message
            };
        }

        public ResponseBaseEmptyMetaModel Meta { get; set; }
    }

    public class ResponseBaseEmptyMetaModel
    {
        public string? Message { get; set; }
        public bool Success { get; set; }
    }
}
