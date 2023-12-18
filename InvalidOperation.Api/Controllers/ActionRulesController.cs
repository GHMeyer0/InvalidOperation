using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InvalidOperation.Api.Data;
using InvalidOperation.Api.Models;
using SystemTextJsonPatch;

namespace InvalidOperation.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActionRulesController : ControllerBase
    {
        private readonly InvalidOperationApiContext _context;

        public ActionRulesController(InvalidOperationApiContext context)
        {
            _context = context;
        }

        // GET: api/ActionRules
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ActionRule>>> GetActionRule()
        {
            return await _context.ActionRule.ToListAsync();
        }


        // PATCH: api/ActionRules/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchActionRule(Guid id, JsonPatchDocument<ActionRule> actionRulePatch)
        {

            var actionRule = await _context.ActionRule
                .Where(x => x.Id == id)
                .AsNoTracking()
                .FirstOrDefaultAsync();


            actionRulePatch.ApplyTo(actionRule);
            _context.Update(actionRule);


            await _context.SaveChangesAsync();



            return NoContent();
        }

        // POST: api/ActionRules
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ActionRule>> PostActionRule(ActionRule actionRule)
        {
            _context.ActionRule.Add(actionRule);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetActionRule", new { id = actionRule.Id }, actionRule);
        }

    }
}
