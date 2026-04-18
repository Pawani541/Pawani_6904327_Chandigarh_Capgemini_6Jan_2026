using BookStore.Application.DTOs;
using BookStore.Application.Interfaces;
using BookStore.API.Controllers;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace BookStore.Tests.NUnit
{
    [TestFixture]
    public class BooksControllerTests
    {
        private Mock<IBookService> _mockBookService;
        private Mock<IValidator<BookCreateDto>> _mockCreateValidator;
        private Mock<IValidator<BookUpdateDto>> _mockUpdateValidator;
        private BooksController _controller;

        [SetUp]
        public void Setup()
        {
            _mockBookService = new Mock<IBookService>();
            _mockCreateValidator = new Mock<IValidator<BookCreateDto>>();
            _mockUpdateValidator = new Mock<IValidator<BookUpdateDto>>();

            _controller = new BooksController(
                _mockBookService.Object,
                _mockCreateValidator.Object,
                _mockUpdateValidator.Object);
        }

        [Test]
        public async Task GetAll_ShouldReturn200WithBooks()
        {
            // Arrange
            var books = new List<BookDto>
            {
                new BookDto { BookId = 1, Title = "C# Book", Price = 499 },
                new BookDto { BookId = 2, Title = "ASP.NET Book", Price = 599 }
            };
            _mockBookService.Setup(s => s.GetAllBooksAsync()).ReturnsAsync(books);

            // Act
            var result = await _controller.GetAll();

            // Assert
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
            var okResult = result as OkObjectResult;
            Assert.That(okResult!.StatusCode, Is.EqualTo(200));
        }

        [Test]
        public async Task GetById_WithValidId_ShouldReturn200()
        {
            // Arrange
            var book = new BookDto { BookId = 1, Title = "C# Book", Price = 499 };
            _mockBookService.Setup(s => s.GetBookByIdAsync(1)).ReturnsAsync(book);

            // Act
            var result = await _controller.GetById(1);

            // Assert
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
        }

        [Test]
        public async Task GetById_WithInvalidId_ShouldReturn404()
        {
            // Arrange
            _mockBookService.Setup(s => s.GetBookByIdAsync(999)).ReturnsAsync((BookDto?)null);

            // Act
            var result = await _controller.GetById(999);

            // Assert
            Assert.That(result, Is.InstanceOf<NotFoundObjectResult>());
        }

        [Test]
        public async Task Create_WithValidData_ShouldReturn201()
        {
            // Arrange
            var createDto = new BookCreateDto
            {
                Title = "New Book",
                ISBN = "9781234567890",
                Price = 299,
                Stock = 10,
                CategoryId = 1,
                AuthorId = 1,
                PublisherId = 1
            };
            var bookDto = new BookDto { BookId = 1, Title = "New Book", Price = 299 };

            _mockCreateValidator.Setup(v => v.ValidateAsync(createDto, default))
                .ReturnsAsync(new ValidationResult());
            _mockBookService.Setup(s => s.CreateBookAsync(createDto)).ReturnsAsync(bookDto);

            // Act
            var result = await _controller.Create(createDto);

            // Assert
            Assert.That(result, Is.InstanceOf<CreatedAtActionResult>());
        }

        [Test]
        public async Task Create_WithInvalidData_ShouldReturn400()
        {
            // Arrange
            var createDto = new BookCreateDto { Title = "" };
            var failures = new List<ValidationFailure>
            {
                new ValidationFailure("Title", "Title is required.")
            };

            _mockCreateValidator.Setup(v => v.ValidateAsync(createDto, default))
                .ReturnsAsync(new ValidationResult(failures));

            // Act
            var result = await _controller.Create(createDto);

            // Assert
            Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
        }

        [Test]
        public async Task Delete_WithValidId_ShouldReturn204()
        {
            // Arrange
            _mockBookService.Setup(s => s.DeleteBookAsync(1)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Delete(1);

            // Assert
            Assert.That(result, Is.InstanceOf<NoContentResult>());
        }

        [Test]
        public async Task Search_WithKeyword_ShouldReturn200()
        {
            // Arrange
            var books = new List<BookDto>
            {
                new BookDto { BookId = 1, Title = "C# Programming" }
            };
            _mockBookService.Setup(s => s.SearchBooksAsync("C#")).ReturnsAsync(books);

            // Act
            var result = await _controller.Search("C#");

            // Assert
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
        }

        [Test]
        public async Task Search_WithEmptyKeyword_ShouldReturn400()
        {
            // Act
            var result = await _controller.Search("");

            // Assert
            Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
        }

        [Test]
        public async Task GetByCategory_ShouldReturn200()
        {
            // Arrange
            var books = new List<BookDto>
            {
                new BookDto { BookId = 1, Title = "Tech Book", CategoryName = "Technology" }
            };
            _mockBookService.Setup(s => s.GetBooksByCategoryAsync(1)).ReturnsAsync(books);

            // Act
            var result = await _controller.GetByCategory(1);

            // Assert
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
        }
    }
}
