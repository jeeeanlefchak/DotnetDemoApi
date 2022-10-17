using Demo.Business.Data;
using Demo.Model.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Services.Controllers
{
    public abstract class AbstractController<TEntity> : Controller
            where TEntity : EntityBase, new()
    {
        protected DataContext context;

        //public virtual ActionResult Create()
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }
        //    var entity = new TEntity();
        //    return View(entity);
        //}

        [HttpPost]
        //[Route("api/{Controller}")]
        public async Task<IActionResult> Create([FromBody] TEntity entity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            context.Set<TEntity>().Add(entity);
            await context.SaveChangesAsync();
            return Ok(entity);
        }

        [HttpPut("{id}")]
        // [Route("api/{Controller}")]
        public async Task<IActionResult> Put([FromRoute] long id, [FromBody] TEntity entity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            context.Set<TEntity>().Update(entity);
            await context.SaveChangesAsync();
            return Ok(entity);
        }
        [HttpPut]
        // [Route("api/{Controller}")]
        public async Task<IActionResult> Put([FromBody] TEntity entity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            context.Set<TEntity>().Update(entity);
            await context.SaveChangesAsync();
            return Ok(entity);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] long id)
        {
            try
            {
                var entity = context.Set<TEntity>().FirstOrDefault(c => c.Id == id);
                return Ok(entity);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        //TA FUNCIONANDO CERTINHO !!
        public async Task<IActionResult> Get()
        {
            try
            {
                return Ok(context.Set<TEntity>().ToList());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = context.Set<TEntity>().FirstOrDefault(c => c.Id == id);
            if (entity == null) NotFound();
            context.Set<TEntity>().Remove(entity);
            context.SaveChanges();
            return Ok(true);
        }



    }
}
