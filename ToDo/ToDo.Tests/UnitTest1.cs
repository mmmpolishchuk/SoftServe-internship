using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using ToDo.Controllers;
using ToDo.Models;
using ToDo.Models.Repository;

namespace ToDo.Tests
{
    [TestFixture]
    public class Tests
    {
        private IToDoRepository _toDoRepository;

        private List<ToDoItemDTO> toDos;

        [Test]
        public async Task getToDoItems_returnAllItems_ifExists()
        {
            var mockRepo = new Mock<IToDoRepository>();
            toDos = GetToDos().ToList();

            mockRepo.Setup(repo => repo.GetAllItems())
                .ReturnsAsync(toDos);
            var controller = new ToDoController(mockRepo.Object);

            var result = await controller.GetTodoItems();

            Assert.AreEqual(toDos.Count, result.Count());

            Enumerable
                .Range(0, toDos.Count)
                .ToList()
                .ForEach(i => { Assert.AreEqual(toDos, result); });
        }


        [Test]
        public async Task getToDoItem_returnSingleItemById_ifExists()
        {
            var toDoItem = new ToDoItemDTO();
            var mockRepo = new Mock<IToDoRepository>();
            toDos = GetToDos().ToList();
            var toDoItemId = 3;

            mockRepo.Setup(x => x.GetItemById(It.IsAny<int>()))
                .ReturnsAsync((int i) => toDos.SingleOrDefault(bo => bo.Id == i));

            var controller = new ToDoController(mockRepo.Object);
            var result = await controller.GetTodoItem(toDoItemId);

            // Assert.AreEqual(toDos[toDoItemId], result);
            var itemThatExists = await controller.GetTodoItem(3);
            Assert.IsNotNull(itemThatExists);
            Assert.AreEqual(itemThatExists.Id, 3);
            Assert.AreEqual(itemThatExists.Name, "To drink 2 L of water within a day");
        }

        static List<ToDoItemDTO> GetToDos()
        {
            var toDos = new List<ToDoItemDTO>
            {
                new ToDoItemDTO() {Id = 1, Name = "To exercise in the morning", IsComplete = false},
                new ToDoItemDTO() {Id = 2, Name = "To read 25 pages of the book", IsComplete = false},
                new ToDoItemDTO() {Id = 3, Name = "To drink 2 L of water within a day", IsComplete = false},
                new ToDoItemDTO() {Id = 4, Name = "To work on a new project", IsComplete = false}
            };
            return toDos;
        }
    }
}