using Microsoft.Extensions.Caching.Memory;
using Term5_RPBDIS_library;
using Term5_RPBDIS_library.models.tables;

namespace Term5_RPBDIS_mainLogic.Services {
    public class DivisionService : CacheService<Division> {
        public DivisionService(ValuatingSystemContext context, IMemoryCache cache, IServiceProvider serviceProvider)
            : base(context, cache, serviceProvider) {
        }
    }
}
