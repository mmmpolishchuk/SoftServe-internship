using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDo.Models;
using ToDo.Models.Repository;

namespace ToDo.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoController : ControllerBase
    {
        private IToDoRepository ToDoItemRepository;

        public ToDoController(IToDoRepository toDoItemRepository)
        {
            ToDoItemRepository = toDoItemRepository;
        }

        /// <summary>
        /// Gets all ToDoItems.
        /// </summary>
        [HttpGet]
        public async Task<IEnumerable<ToDoItemDTO>> GetTodoItems()
        {
            return await ToDoItemRepository.GetAllItems();
        }

        /// <summary>
        /// Gets a specific ToDoItem.
        /// </summary>
        /// <param name="id"></param>>
        [HttpGet("{id}")]
        public async Task<ToDoItemDTO> GetTodoItem(int id)
        {
            return await ToDoItemRepository.GetItemById(id);
        }

        /// <summary>
        /// Updates a specific ToDoItem.
        /// </summary>
        /// <param name="id"></param>>
        /// <param name="toDoItemDto"></param>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTodoItem(int id, ToDoItemDTO toDoItemDto)
        {
            if (id != toDoItemDto.Id)
            {
                return NotFound();
            }

            await ToDoItemRepository.UpdateItem(id, toDoItemDto);

            return Ok(toDoItemDto);
        }

        /// <summary>
        /// Creates a TodoItem.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Todo
        ///     {
        ///        "id": 1,
        ///        "name": "Item1",
        ///        "isComplete": true
        ///     }
        ///
        /// </remarks>
        /// <param name="todoItemDTO"></param>
        /// <returns>A newly created TodoItem</returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response>            
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ToDoItemDTO>> CreateTodoItem([FromBody] ToDoItemDTO todoItemDTO)
        {
            if (todoItemDTO == null)
            {
                throw new ArgumentNullException("To Do item must not be null");
            }

            return await ToDoItemRepository.CreateItem(todoItemDTO);
        }

        /// <summary>
        /// Deletes a specific ToDoItem.
        /// </summary>
        /// <param name="id"></param>>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(int id)
        {
            await ToDoItemRepository.DeleteItem(id);

            return NoContent();
        }
    }
}