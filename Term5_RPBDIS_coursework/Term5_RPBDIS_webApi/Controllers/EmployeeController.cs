using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Term5_RPBDIS_Infrastructure;
using Term5_RPBDIS_library;
using Term5_RPBDIS_library.models.tables;

namespace Term5_RPBDIS_Web.Controllers {
    public class EmployeeController : ExpandedApiController<Employee> {
        public EmployeeController(ValuatingSystemContext context) 
            : base(context) {}
    }
}
