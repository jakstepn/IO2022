using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiGateway_ApiClasses.Requests
{
    public class PaginatedRequest
    {
        int page { get; set; } = 1;
        int pageSize { get; set; } = 10;
    }
}
