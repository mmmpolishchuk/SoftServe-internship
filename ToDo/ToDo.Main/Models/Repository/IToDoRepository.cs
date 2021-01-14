using System.Collections.Generic;
using System.Threading.Tasks;

namespace ToDo.Models.Repository
{
    public interface IToDoRepository
    {
        Task<IEnumerable<ToDoItemDTO>> GetAllItems();
        Task<ToDoItemDTO> GetItemById(int id);
        Task UpdateItem(int id, ToDoItemDTO toDoItemDto);
        Task<ToDoItemDTO> CreateItem(ToDoItemDTO toDoItemDto);
        Task DeleteItem(int id);
    }
}