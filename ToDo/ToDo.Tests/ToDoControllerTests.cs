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
    public class ToDoControllerTests
    {
        private Mock<IToDoRepository> _moqRepo;

        private List<ToDoItemDTO> _toDo;
        private ToDoItemDTO _ToDoItemDto;
        private int _toDoItemDtoId = 3;

        [SetUp]
        public void Setup()
        {
            _moqRepo = new Mock<IToDoRepository>();
            _toDo = GetToDos().ToList();
            _ToDoItemDto = new ToDoItemDTO();
        }

        [Test]
        public async Task getToDoItems_whenItemsExists_returnAll()
        {
            //Arrange
            _moqRepo.Setup(repo => repo.GetAllItems())
                .ReturnsAsync(_toDo);
            var controller = new ToDoController(_moqRepo.Object);

            //Act
            var result = await controller.GetTodoItems();

            //Assert
            Assert.AreEqual(_toDo.Count, result.Count());

            Enumerable
                .Range(0, _toDo.Count)
                .ToList()
                .ForEach(i => { Assert.AreEqual(_toDo, result); });
        }


        [Test]
        public async Task getToDoItemById_ItemWithSelectedIdExists_returnItem()
        {
            //Arrange
            _moqRepo.Setup(x => x.GetItemById(It.IsAny<int>()))
                .ReturnsAsync((int i) => _toDo.SingleOrDefault(bo => bo.Id == i));

            var controller = new ToDoController(_moqRepo.Object);

            //Act
            var itemThatExists = await controller.GetTodoItem(3);

            //Assert
            Assert.IsNotNull(itemThatExists);
            Assert.That(itemThatExists.Id, Is.EqualTo(_toDoItemDtoId));
        }

        //Fake To Do tasks
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