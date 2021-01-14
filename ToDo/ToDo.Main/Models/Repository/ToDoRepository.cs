using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ToDo.Models.Repository
{
    public class ToDoRepository : IToDoRepository
    {
        private ToDoContext Context { get; set; }

        public ToDoRepository(ToDoContext context)
        {
            Context = context;
        }

        public async Task<IEnumerable<ToDoItemDTO>> GetAllItems()
        {
            var toDoItems = await Context.ToDoItems
                .Select(x => ItemToDTO(x))
                .ToListAsync();

            return toDoItems;
        }

        public async Task<ToDoItemDTO> GetItemById(int id)
        {
            var toDoItem = await Context.ToDoItems.FirstOrDefaultAsync(x => x.Id == id);
            return ItemToDTO(toDoItem);
        }

        public async Task UpdateItem(int id, ToDoItemDTO toDoItemDto)
        {
            var todoItem = await Context.ToDoItems.FindAsync(id);

            if (todoItem == null)
            {
                throw new NullReferenceException("There is no To Do item with such Id");
            }

            todoItem.Name = toDoItemDto.Name;
            todoItem.IsComplete = toDoItemDto.IsComplete;

            await Context.SaveChangesAsync();
        }

        public async Task<ToDoItemDTO> CreateItem(ToDoItemDTO toDoItemDto)
        {
            var toDoItem = new ToDoItem
            {
                IsComplete = toDoItemDto.IsComplete,
                Name = toDoItemDto.Name
            };

            try
            {
                await Context.ToDoItems.AddAsync(toDoItem);
                await Context.SaveChangesAsync();

                return toDoItemDto;
            }
            catch (Exception ex)
            {
                throw new Exception($"To Do item could not be saved: {ex.Message}");
            }
        }

        public async Task DeleteItem(int id)
        {
            var todoItem = await Context.ToDoItems.FindAsync(id);

            if (todoItem == null)
            {
                throw new NullReferenceException("There is no To Do item with such Id");
            }

            Context.ToDoItems.Remove(todoItem);

            await Context.SaveChangesAsync();
        }


        private bool TodoItemExists(long id) =>
            Context.ToDoItems.Any(e => e.Id == id);

        private static ToDoItemDTO ItemToDTO(ToDoItem toDoItem) =>
            new ToDoItemDTO
            {
                Id = toDoItem.Id,
                Name = toDoItem.Name,
                IsComplete = toDoItem.IsComplete
            };
    }
}