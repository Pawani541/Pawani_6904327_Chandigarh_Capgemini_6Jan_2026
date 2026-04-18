using AutoMapper;
using BookStore.Application.DTOs;
using BookStore.Application.Interfaces;
using BookStore.Application.Services;
using BookStore.Domain.Entities;
using Moq;
using Xunit;

namespace BookStore.Tests.XUnit
{
    public class BookServiceTests
    {
        private readonly Mock<IBookRepository> _mockRepo;
        private readonly Mock<IMapper> _mockMapper;
        private readonly BookService _bookService;

        public BookServiceTests()
        {
            _mockRepo = new Mock<IBookRepository>();
            _mockMapper = new Mock<IMapper>();
            _bookService = new BookService(_mockRepo.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task GetAllBooksAsync_ShouldReturnAllBooks()
        {
            // Arrange
            var books = new List<Book>
            {
                new Book { BookId = 1, Title = "C# Programming", Price = 499, Stock = 10 },
                new Book { BookId = 2, Title = "ASP.NET Core", Price = 599, Stock = 5 }
            };
            var bookDtos = new List<BookDto>
            {
                new BookDto { BookId = 1, Title = "C# Programming", Price = 499 },
                new BookDto { BookId = 2, Title = "ASP.NET Core", Price = 599 }
            };

            _mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(books);
            _mockMapper.Setup(m => m.Map<IEnumerable<BookDto>>(books)).Returns(bookDtos);

            // Act
            var result = await _bookService.GetAllBooksAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetBookByIdAsync_WithValidId_ShouldReturnBook()
        {
            // Arrange
            var book = new Book { BookId = 1, Title = "C# Programming", Price = 499 };
            var bookDto = new BookDto { BookId = 1, Title = "C# Programming", Price = 499 };

            _mockRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(book);
            _mockMapper.Setup(m => m.Map<BookDto>(book)).Returns(bookDto);

            // Act
            var result = await _bookService.GetBookByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("C# Programming", result.Title);
            Assert.Equal(499, result.Price);
        }

        [Fact]
        public async Task GetBookByIdAsync_WithInvalidId_ShouldReturnNull()
        {
            // Arrange
            _mockRepo.Setup(r => r.GetByIdAsync(999)).ReturnsAsync((Book?)null);

            // Act
            var result = await _bookService.GetBookByIdAsync(999);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task CreateBookAsync_ShouldCreateAndReturnBook()
        {
            // Arrange
            var createDto = new BookCreateDto
            {
                Title = "New Book",
                ISBN = "9781234567890",
                Price = 299,
                Stock = 20,
                CategoryId = 1,
                AuthorId = 1,
                PublisherId = 1
            };
            var book = new Book { BookId = 1, Title = "New Book", Price = 299 };
            var bookDto = new BookDto { BookId = 1, Title = "New Book", Price = 299 };

            _mockMapper.Setup(m => m.Map<Book>(createDto)).Returns(book);
            _mockRepo.Setup(r => r.AddAsync(book)).Returns(Task.CompletedTask);
            _mockRepo.Setup(r => r.SaveAsync()).Returns(Task.CompletedTask);
            _mockMapper.Setup(m => m.Map<BookDto>(book)).Returns(bookDto);

            // Act
            var result = await _bookService.CreateBookAsync(createDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("New Book", result.Title);
            _mockRepo.Verify(r => r.AddAsync(It.IsAny<Book>()), Times.Once);
            _mockRepo.Verify(r => r.SaveAsync(), Times.Once);
        }

        [Fact]
        public async Task UpdateBookAsync_WithValidId_ShouldUpdateBook()
        {
            // Arrange
            var updateDto = new BookUpdateDto
            {
                BookId = 1,
                Title = "Updated Book",
                Price = 399,
                Stock = 15
            };
            var existingBook = new Book { BookId = 1, Title = "Old Book", Price = 299 };
            var updatedDto = new BookDto { BookId = 1, Title = "Updated Book", Price = 399 };

            _mockRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(existingBook);
            _mockMapper.Setup(m => m.Map(updateDto, existingBook)).Returns(existingBook);
            _mockRepo.Setup(r => r.UpdateAsync(existingBook)).Returns(Task.CompletedTask);
            _mockRepo.Setup(r => r.SaveAsync()).Returns(Task.CompletedTask);
            _mockMapper.Setup(m => m.Map<BookDto>(existingBook)).Returns(updatedDto);

            // Act
            var result = await _bookService.UpdateBookAsync(updateDto);

            // Assert
            Assert.NotNull(result);
            _mockRepo.Verify(r => r.UpdateAsync(It.IsAny<Book>()), Times.Once);
        }

        [Fact]
        public async Task UpdateBookAsync_WithInvalidId_ShouldThrowException()
        {
            // Arrange
            var updateDto = new BookUpdateDto { BookId = 999, Title = "Not Found" };
            _mockRepo.Setup(r => r.GetByIdAsync(999)).ReturnsAsync((Book?)null);

            // Act & Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(
                () => _bookService.UpdateBookAsync(updateDto));
        }

        [Fact]
        public async Task DeleteBookAsync_WithValidId_ShouldDelete()
        {
            // Arrange
            var book = new Book { BookId = 1, Title = "Book to Delete" };
            _mockRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(book);
            _mockRepo.Setup(r => r.DeleteAsync(1)).Returns(Task.CompletedTask);
            _mockRepo.Setup(r => r.SaveAsync()).Returns(Task.CompletedTask);

            // Act
            await _bookService.DeleteBookAsync(1);

            // Assert
            _mockRepo.Verify(r => r.DeleteAsync(1), Times.Once);
            _mockRepo.Verify(r => r.SaveAsync(), Times.Once);
        }

        [Fact]
        public async Task DeleteBookAsync_WithInvalidId_ShouldThrowException()
        {
            // Arrange
            _mockRepo.Setup(r => r.GetByIdAsync(999)).ReturnsAsync((Book?)null);

            // Act & Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(
                () => _bookService.DeleteBookAsync(999));
        }

        [Fact]
        public async Task SearchBooksAsync_ShouldReturnMatchingBooks()
        {
            // Arrange
            var books = new List<Book>
            {
                new Book { BookId = 1, Title = "C# Programming", ISBN = "1234567890" }
            };
            var bookDtos = new List<BookDto>
            {
                new BookDto { BookId = 1, Title = "C# Programming" }
            };

            _mockRepo.Setup(r => r.SearchBooksAsync("C#")).ReturnsAsync(books);
            _mockMapper.Setup(m => m.Map<IEnumerable<BookDto>>(books)).Returns(bookDtos);

            // Act
            var result = await _bookService.SearchBooksAsync("C#");

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
        }

        [Fact]
        public async Task GetBooksInStockAsync_ShouldReturnOnlyInStockBooks()
        {
            // Arrange
            var books = new List<Book>
            {
                new Book { BookId = 1, Title = "In Stock Book", Stock = 5 }
            };
            var bookDtos = new List<BookDto>
            {
                new BookDto { BookId = 1, Title = "In Stock Book", Stock = 5 }
            };

            _mockRepo.Setup(r => r.GetBooksInStockAsync()).ReturnsAsync(books);
            _mockMapper.Setup(m => m.Map<IEnumerable<BookDto>>(books)).Returns(bookDtos);

            // Act
            var result = await _bookService.GetBooksInStockAsync();

            // Assert
            Assert.NotNull(result);
            Assert.All(result, b => Assert.True(b.Stock > 0));
        }
    }
}
