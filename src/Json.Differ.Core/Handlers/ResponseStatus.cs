using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Json.Differ.Core.Handlers
{
    public enum ResponseStatus
    {
        OK,

        Created,
        Accepted,
        NoContent,

        BadRequest,
        Unauthorized,
        NotFound,
    }
}