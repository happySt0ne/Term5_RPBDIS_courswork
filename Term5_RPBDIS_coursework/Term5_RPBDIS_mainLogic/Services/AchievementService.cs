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
    public class AchievementService : CacheService<Achievement> {
        public AchievementService(ValuatingSystemContext context, IMemoryCache memoryCache, IServiceProvider service) 
            : base(context, memoryCache, service) { }
    }
}
