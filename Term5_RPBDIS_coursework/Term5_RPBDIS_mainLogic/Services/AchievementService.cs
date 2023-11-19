using Microsoft.Extensions.Caching.Memory;
using Term5_RPBDIS_library;
using Term5_RPBDIS_library.models.tables;

namespace Term5_RPBDIS_mainLogic.Services {
    public class AchievementService : CacheService<Achievement> {
        public AchievementService(ValuatingSystemContext context, IMemoryCache memoryCache, IServiceProvider service)
            : base(context, memoryCache, service) { }
    }
}
