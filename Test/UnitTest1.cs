
using Domain.Entities;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Test
{
    public class UnitTest1
    {

        [Fact]
        public async Task DeleteAsync_BookDeletedSuccessfully()
        {
            // Arrange
            var books = new List<Book>(); // Mock list of books
            var bookIdToDelete = 1; // Id of the book to delete

            var mockDbSet = new Mock<DbSet<Book>>(); // Mock DbSet<Book>

            // Populate mock DbSet with some books
            var mockBooks = new List<Book>
            {
                new Book { Id = 1, /* Add other properties */ },
                new Book { Id = 2, /* Add other properties */ },
                new Book { Id = 3, /* Add other properties */ }
            }.AsQueryable();
            mockDbSet.As<IQueryable<Book>>().Setup(m => m.Provider).Returns(mockBooks.Provider);
            mockDbSet.As<IQueryable<Book>>().Setup(m => m.Expression).Returns(mockBooks.Expression);
            mockDbSet.As<IQueryable<Book>>().Setup(m => m.ElementType).Returns(mockBooks.ElementType);
            mockDbSet.As<IQueryable<Book>>().Setup(m => m.GetEnumerator()).Returns(mockBooks.GetEnumerator());

            // Configure mock DbSet behavior for Find method
            mockDbSet.Setup(m => m.FindAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => books.FirstOrDefault(b => b.Id == id));

            var mockDbContext = new Mock<ApplicationDbContext>(); // Mock ApplicationDbContext
            mockDbContext.Setup(c => c.Books).Returns(mockDbSet.Object); // Set up context to return mock DbSet

            var repository = new BookRepository(mockDbContext.Object); // Create repository instance

            // Act
            await repository.DeleteAsync(bookIdToDelete);

            // Assert
            // Verify that FindAsync was called with the correct bookIdToDelete
            mockDbSet.Verify(m => m.FindAsync(bookIdToDelete), Times.Once());

            // Verify that Remove was called with the correct book to delete
            mockDbSet.Verify(m => m.Remove(It.IsAny<Book>()), Times.Once());

            // Verify that SaveChangesAsync was called
            mockDbContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once());
        }
    }
}
