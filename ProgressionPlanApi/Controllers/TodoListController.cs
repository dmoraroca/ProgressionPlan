using Microsoft.AspNetCore.Mvc;
using ProgressPlanDDD.Application.Interfaces;

namespace ProgressPlan.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoListController : ControllerBase
    {
        private readonly ITodoList _TodoList;
        private readonly ITodoListRepository _TodoListRepository;

        public TodoListController(ITodoList todoList, ITodoListRepository todoListRepository) 
        {
            _TodoList = todoList;
            _TodoListRepository = todoListRepository;
        }

        [HttpPost("addItem")]
        public IActionResult AddItem([FromQuery] string title, [FromQuery] string description, [FromQuery] string category)
        {
            try
            {
                _TodoList.AddItem(_TodoListRepository.GetNextId(),  title, description, category);
                return Ok("Todo item created.");
            } 
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("update/{id}")]
        public IActionResult Update(int id, [FromQuery] string description)
        {
            try
            {
                _TodoList.UpdateItem(id, description);
                return Ok("Todo item updated.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _TodoList.RemoveItem(id);
                return Ok("Todo item deleted.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("progress/{id}")]
        public IActionResult AddProgression(int id, [FromQuery] DateTime date, [FromQuery] decimal percent)
        {
            try
            {
                _TodoList.RegisterProgression(id, date, percent);
                return Ok("Progression registered.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("print")]
        public IActionResult Print()
        {
            try
            {
                _TodoList.PrintItems(); // Esto imprime en consola, puedes hacer que devuelva string
                return Ok("Printed to console.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("items")]
        public IActionResult GetAll()
        {
            try
            {
                var items = _TodoList.GetAll();
                return Ok(items.Values.ToArray());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("clearitems")]
        public IActionResult Clear()
        {
            try
            {
                _TodoList.ClearItems();
                return Ok("Items borrado");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
