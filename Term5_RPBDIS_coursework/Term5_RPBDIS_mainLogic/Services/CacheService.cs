using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Term5_RPBDIS_library;
using Term5_RPBDIS_library.models.tables;
using Term5_RPBDIS_sql_library;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Term5_RPBDIS_mainLogic.Services {
    public abstract class CacheService<T> where T : class, ISqlTable {
        protected const int ROWS_NUMBER = 20;
        protected ValuatingSystemContext _valuatingSystemContext;
        protected IMemoryCache _cache;
        protected readonly IServiceProvider _serviceProvider;
        
        public CacheService(ValuatingSystemContext context, IMemoryCache cache, IServiceProvider serviceProvider) {
            _valuatingSystemContext = context;
            _cache = cache;
            _serviceProvider = serviceProvider;
        }

        public IEnumerable<T>? Get(string cacheKey) {
            if (_cache.TryGetValue(cacheKey, out IEnumerable<T> result)) {

                Console.WriteLine("Взято из кеша");
                return result;
            }

            result = _valuatingSystemContext
                .Set<T>()
                .AsEnumerable()
                .OrderBy(x => x.ID)
                .Take(ROWS_NUMBER)
                .ToList();

            if (result is not null) {

                _cache.Set(cacheKey, result, _serviceProvider.GetService<MemoryCacheEntryOptions>());
            }
            Console.WriteLine("Взято из бд");
            return result;
        }
    }
}
