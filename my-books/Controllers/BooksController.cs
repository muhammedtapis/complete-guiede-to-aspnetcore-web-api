﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using my_books.Data.Services;
using my_books.Data.ViewModels;

namespace my_books.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        //startup.cs dosyasında service ekledikten sonra servisi buraya inject edebiliriz.
        public BooksService _booksService;

        public BooksController(BooksService booksService)
        {
            _booksService = booksService;
        }

        //ilk endpointi oluşturacağız 
        //HTTP POST API endpoint olacak çünkü database veri gönderiyoruz.
        [HttpPost("add-book")]
        public IActionResult AddBook([FromBody] BookVM book)
        {
            _booksService.AddBook(book);
            return Ok();
        }


        [HttpGet("get-all-books")]

        public IActionResult GetAllBooks()
        {
            var allBooks = _booksService.GetAllBooks(); //allBooks parametresini _booksService sınıfındaki getallbooks methoduna atadık bu method liste dönüyor.

            return Ok(allBooks);
        }

        [HttpGet("get-book-by-id/{bookId}")] //method parametresindeki bookId nin httpget requestinden gelmesi lazım o
                                             // o yüzden /{} ekleyip içine bookId yazdık.
        public IActionResult GetBookById(int bookId)
        {
            var book = _booksService.GetBookById(bookId); //allBooks parametresini _booksService sınıfındaki getallbooks methoduna atadık bu method liste dönüyor.

            return Ok(book);
        }

        [HttpPut("update-book-by-id/{bookId}")]

        public IActionResult UpdateBookById(int bookId, [FromBody] BookVM book) // BookVM de frombody eklememiz gerekiyor çünkü API de body kısmından değişikliği yapıyoruz ordan gelecek bu bilgi.
        {
            var updatedBook = _booksService.UpdateBookById(bookId, book);

            return Ok(updatedBook);
        }

        [HttpDelete("delete-book-by-id/{bookId}")]

        public IActionResult DeleteBookById(int bookId) { 
        
            _booksService.DeleteBookById(bookId); //method bir şey dönmediği için direkt servisteki methodu çağırdık herhangi bir değere atamadık.
            return Ok();
        }
    }
}