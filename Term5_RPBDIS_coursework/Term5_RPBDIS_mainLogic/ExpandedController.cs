using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;
using Term5_RPBDIS_library;
using Term5_RPBDIS_library.models.tables;
using Term5_RPBDIS_sql_library;
namespace Term5_RPBDIS_Web.Controllers {
    public abstract class ExpandedController<T> : Controller where T : class, ISqlTable {
        private const int CacheDuration = 264;
        private const int pageSize = 20;
        protected ValuatingSystemContext _context;

        protected ExpandedController([FromServices] ValuatingSystemContext context) {
            _context = context;
        }

        public abstract IActionResult Create();
        public abstract IActionResult Update();

        [ResponseCache(Duration = CacheDuration, VaryByQueryKeys = new[] { "pageNumber" })]
        public async Task<IActionResult> ShowTable(int pageNumber = 1) {
            var query = _context.Set<T>().AsQueryable();

            int total = await query.CountAsync();
            int totalPages = (int)Math.Ceiling((double)total / pageSize);

            var data = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            ViewBag.data = data;
            ViewBag.pageNumber = pageNumber;
            ViewBag.totalPages = totalPages;

            return View();
        }

        public IActionResult Delete() {
            if (!TryGetFromQuery("Id", out int? id)) {

                return View();
            }

            DeleteFromDb(id);

            return View();
        }
        protected void DeleteFromDb(int? id) {
            T? entity = _context.Set<T>().Find(id);

            if (entity is null) return;

            DeleteFromDb(entity);
        }
        protected void DeleteFromDb(T recordToDelete) {
            _context.Set<T>().Remove(recordToDelete);
            _context.SaveChanges();
        }

        /// <param name="key"> Ключ из запроса. </param>
        /// <param name="number"> Переменная, в которую может быть присвоено значение. </param>
        /// <returns>true, если <paramref name="number"/> было успешно присвоено значение.</returns>
        protected bool TryGetFromQuery(string key, out int? number) {
            if (!HttpContext.Request.Query.ContainsKey(key) ||
                string.IsNullOrEmpty(HttpContext.Request.Query[key])) {

                number = null;
                return false;
            }

            number = int.Parse(HttpContext.Request.Query[key]);
            return true;
        }
        /// <param name="key"> Ключ из запроса. </param>
        /// <param name="str"> Переменная, в которую может быть присвоено значение. </param>
        /// <returns>true, если <paramref name="str"/> было успешно присвоено значение.</returns>
        protected bool TryGetFromQuery(string key, out string? str) {
            if (!HttpContext.Request.Query.ContainsKey(key) ||
                string.IsNullOrEmpty(HttpContext.Request.Query[key])) {

                str = null;
                return false;
            }

            str = HttpContext.Request.Query[key];
            return true;
        }
        /// <param name="key"> Ключ из запроса. </param>
        /// <param name="dateTime"> Переменная, в которую может быть присвоено значение. </param>
        /// <returns>true, если <paramref name="dateTime"/> было успешно присвоено значение.</returns>
        protected bool TryGetFromQuery(string key, out DateTime? dateTime) {
            if (!HttpContext.Request.Query.ContainsKey(key) || 
                string.IsNullOrEmpty(HttpContext.Request.Query[key])) {

                dateTime = null;
                return false;
            }

            dateTime = DateTime.Parse(HttpContext.Request.Query[key]);
            return true;
        }

        protected Employee GetAdd(string? name, DateTime? hireDate, string? achievementText, int? markValue,
                                  int? divisionMarkValue, string? divisionName, DateTime? startDate, DateTime? endDate, 
                                  int? planned, int? real) {
            Division division = GetAdd(divisionName, divisionMarkValue, startDate, endDate, planned, real);
            Mark mark = GetAdd(markValue);
            Achievement achievement = GetAdd(achievementText);

            if(!IsRecordExist(e => e.Name == name && 
                                    e.HireDate == hireDate && 
                                    e.Division == division && 
                                    e.Mark == mark && 
                                    e.Achievement == achievement , out Employee employee)) {

                employee = new() {
                    Name = name,
                    Achievement = achievement,
                    Mark = mark,
                    Division = division,
                };

                AddToDb(employee);
            }

            return employee;
        }
        protected Division GetAdd(string? name, int? markValue, DateTime? startDate, 
                                  DateTime? endDate, int? plannedEfficiency, int? realEfficiency) {
            Mark mark = GetAdd(markValue);
            PlannedEfficiency planned = GetAddPlanned(plannedEfficiency, startDate, endDate);
            RealEfficiency real = GetAddReal(realEfficiency, startDate, endDate);

            if (!IsRecordExist(d => d.PlannedEfficiency == planned && 
                                    d.RealEfficiency == real && 
                                    d.Mark == mark && 
                                    d.Name == name, out Division division)) {
                division = new() {
                    Name = name,
                    Mark = mark,
                    PlannedEfficiency = planned,
                    RealEfficiency = real,
                };

                AddToDb(division);
            }

            return division;
        }
        protected PlannedEfficiency GetAddPlanned(int? efficiency, DateTime? startDate, DateTime? endDate) {
            var date = GetAdd(startDate, endDate);

            if (!IsRecordExist(x => x.Date == date && x.Efficiecy == efficiency, out PlannedEfficiency? planned)) {

                planned = new() {
                    Date = date,
                    Efficiecy = efficiency,
                };

                AddToDb(planned);
            }

            return planned;
        }
        protected RealEfficiency GetAddReal(int? efficiency, DateTime? startDate, DateTime? endDate) {
            var date = GetAdd(startDate, endDate);

            if (!IsRecordExist(x => x.Date == date && x.Efficiecy == efficiency, out RealEfficiency real)) {

                real = new() {
                    Date = date,
                    Efficiecy = efficiency,
                };

                AddToDb(real);
            }

            return real;
        }
        protected Date GetAdd(DateTime? startDate, DateTime? endDate) {
            if (startDate is null || endDate is null) throw new NullReferenceException("getAdd получил null");
            
            if (!IsRecordExist(x => x.EndDate == endDate && x.StartDate == startDate, out Date? date)) {
                date = new() {
                    StartDate = startDate,
                    EndDate = endDate
                };

                AddToDb(date);
            }

            return date;
        }
        protected Mark GetAdd(int? value) {
            if (!IsRecordExist(x => x.Value == value, out Mark? mark)) {

                mark = new() {
                    Value = value
                };

                AddToDb(mark);
            }

            return mark;
        }
        protected Achievement GetAdd(string? text) {
            if (!IsRecordExist(x => x.Text == text, out Achievement achievement)) {

                achievement = new() {
                    Text = text
                };

                AddToDb(achievement);
            }

            return achievement;
        }

        protected void AddToDb<TAdd>(TAdd recordToAdd) where TAdd : class, ISqlTable {
            _context.Set<TAdd>().Add(recordToAdd);
            _context.SaveChanges();
        }

        /// <param name="record">null, если в базе данных нет искомой записи, Id этой записи в ином случае</param>
        /// <returns>true, если в базе данных существует запись, соответсвующая условию <paramref name="predicate"/>. </returns>
        protected bool IsRecordExist<TFind>(Func<TFind, bool> predicate, out TFind? record) where TFind : class, ISqlTable {
            record = _context.Set<TFind>().FirstOrDefault(predicate);

            return record is not null;
        }
    }
}
