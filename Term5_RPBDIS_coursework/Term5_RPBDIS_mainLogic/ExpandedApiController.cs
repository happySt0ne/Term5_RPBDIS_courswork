using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Term5_RPBDIS_library;
using Term5_RPBDIS_sql_library;

namespace Term5_RPBDIS_Infrastructure {
    [ApiController]
    [Route("[controller]")]
    public class ExpandedApiController<T> 
                : ControllerBase 
                where T: class, ISqlTable {
        private ValuatingSystemContext _context;

        public ExpandedApiController(ValuatingSystemContext context) => 
            _context = context;

        [HttpGet]
        public IEnumerable<T> Get() => _context.Set<T>().ToList();


    }
}
