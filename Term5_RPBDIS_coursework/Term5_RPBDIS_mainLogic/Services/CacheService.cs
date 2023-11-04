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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Term5_RPBDIS_mainLogic.Services {
    public abstract class CacheService<T> {
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

            result = GetValues();
           
            if (result is not null) {

                _cache.Set(cacheKey, result, _serviceProvider.GetService<MemoryCacheEntryOptions>());
            }
            Console.WriteLine("Взято из бд");
            return result;
        }

        private IEnumerable<T> GetValues() {
            return typeof(T) switch {
                Type type when type == typeof(Achievement) =>
                    _valuatingSystemContext.Achievements.OrderBy(x => x.AchievementId).Take(ROWS_NUMBER).Cast<T>().ToList(),

                Type type when type == typeof(Date) =>
                    _valuatingSystemContext.Dates.OrderBy(x => x.DateId).Take(ROWS_NUMBER).Cast<T>().ToList(),

                Type type when type == typeof(Division) =>
                    _valuatingSystemContext.Divisions.OrderBy(x => x.DivisionId).Take(ROWS_NUMBER).Cast<T>().ToList(),

                Type type when type == typeof(Employee) =>
                    _valuatingSystemContext.Employees.OrderBy(x => x.EmployeeId).Take(ROWS_NUMBER).Cast<T>().ToList(),

                Type type when type == typeof(Mark) =>
                    _valuatingSystemContext.Marks.OrderBy(x => x.MarkId).Take(ROWS_NUMBER).Cast<T>().ToList(),

                Type type when type == typeof(PlannedEfficiency) =>
                    _valuatingSystemContext.PlannedEfficiencies.OrderBy(x => x.PlannedEfficiencyId).Take(ROWS_NUMBER).Cast<T>().ToList(),

                Type type when type == typeof(RealEfficiency) =>
                    _valuatingSystemContext.RealEfficiencies.OrderBy(x => x.RealEfficiencyId).Take(ROWS_NUMBER).Cast<T>().ToList(),

                _ => throw new Exception() //TODO: По хорошему нужно тут нормальное исключение поставить.
            };
        }
    }
}
