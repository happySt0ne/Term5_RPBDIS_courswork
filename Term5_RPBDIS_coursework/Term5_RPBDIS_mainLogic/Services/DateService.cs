using Microsoft.Extensions.Caching.Memory;
using Term5_RPBDIS_library;
using Term5_RPBDIS_library.models.tables;

namespace Term5_RPBDIS_mainLogic.Services {
    public class DateService : CacheService<Date> {
        public DateService(ValuatingSystemContext context, IMemoryCache cache, IServiceProvider serviceProvider)
            : base(context, cache, serviceProvider) {
        }
    }
}
