using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Term5_RPBDIS_library;
using Term5_RPBDIS_library.models.tables;

namespace Term5_RPBDIS_mainLogic.Services {
    public class AchievementService {
        private ValuatingSystemContext _valuatingSystemContext;
        private IMemoryCache _cache;
        private const int ROWS_NUMBER = 20;
        private readonly IServiceProvider _serviceProvider;

        public AchievementService(ValuatingSystemContext context, IMemoryCache memoryCache, IServiceProvider service) {
            _valuatingSystemContext = context;
            _cache = memoryCache;
            _serviceProvider = service;
        }

        public IEnumerable<Achievement>? GetAchievements(string cacheKey) {
            if (_cache.TryGetValue(cacheKey, out IEnumerable<Achievement> achievements)) {

                return achievements;
            }

            achievements = _valuatingSystemContext.Achievements.OrderBy(x => x.AchievementId).Take(ROWS_NUMBER).ToList();

            if (achievements is not null) {

                _cache.Set(cacheKey, achievements, _serviceProvider.GetService<MemoryCacheEntryOptions>());
            }

            return achievements;
        }

    }
}
