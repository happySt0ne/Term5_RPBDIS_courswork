using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Term5_RPBDIS_library;
using Term5_RPBDIS_library.models.tables;
using Term5_RPBDIS_sql_library;

namespace Term5_RPBDIS_Infrastructure {
    [ApiController]
    [Route("[controller]")]
    public class ExpandedApiController<T>
                : ControllerBase
                where T : class, ISqlTable {
        private ValuatingSystemContext _context;

        protected ExpandedApiController(ValuatingSystemContext context) =>
            _context = context;

        /// <summary>
        /// Выводит список объектов. 
        /// </summary>
        [HttpGet]
        public Task<List<T>> Get() => _context.Set<T>().ToListAsync();

        /// <summary>
        /// Создаёт новый объект. Передаваемый объект не должен содержать ID.
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<T>> CreateItem(T item) {
            _context.Set<T>().Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = item.ID }, item);
        }

        /// <summary>
        /// Удаляет объект по id.
        /// </summary>
        [HttpDelete("{id}")] 
        public async Task<IActionResult> Delete(int id) {
            var item = await _context.Set<T>().FindAsync(id);
            if (item is null) return NotFound();
            
            _context.Set<T>().Remove(item);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Обновляет объект в базе данных.
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateItem(int id, T item) {
            if (id != item.ID) return BadRequest();

            _context.Entry(item).State = EntityState.Modified;

            try {
                await _context.SaveChangesAsync();
            } catch (DbUpdateConcurrencyException) {
                if (!ItemExists(id)) {
                    return NotFound();
                } else {
                    throw;
                }
            }

            return NoContent();
        }

        private bool ItemExists(int id) => _context.Set<T>().Any(e => e.ID == id);

    }
}
