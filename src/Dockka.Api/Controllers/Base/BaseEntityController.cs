using System.Linq;
using System.Threading.Tasks;
using Dockka.Data.Context;
using Dockka.Data.Models.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Dockka.Api.Controllers.Base
{
    public abstract class BaseEntityController<T> : Controller where T : class, ISqlEntity
    {
        protected DbSet<T> Set { get; }
        protected DockkaContext Context { get; }

        public BaseEntityController(DockkaContext context)
        {
            Context = context;
            Set = context.Set<T>();
        }

        [Route("{id}")]
        [HttpGet]
        public async Task<IActionResult> Get(int id)
        {
            var result = await Set.FindAsync(id);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [Route("")]
        [HttpGet]
        public async Task<IActionResult> Query()
        {
            return Ok(Set.AsQueryable());
        }

        [Route("")]
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] T item)
        {
            await Set.AddAsync(item);
            return Ok(await Context.SaveChangesAsync());
        }

        [Route("{id}")]
        [HttpPut]
        public async Task<IActionResult> Update(int id, [FromBody] T item)
        {
            var dbItem = await Set.FindAsync(id);
            if (dbItem == null)
                return NotFound();
            Context.Entry(dbItem).CurrentValues.SetValues(item);
            dbItem.Id = id;
            Set.Update(dbItem);
            return Ok(await Context.SaveChangesAsync());
        }

        [Route("{id}")]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var dbItem = await Set.FindAsync(id);
            if (dbItem == null)
                return NotFound();
            Set.Remove(dbItem);
            return Ok(await Context.SaveChangesAsync());
        }
    }
}