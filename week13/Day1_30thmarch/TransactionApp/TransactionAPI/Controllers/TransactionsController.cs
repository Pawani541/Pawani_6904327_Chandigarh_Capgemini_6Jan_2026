using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TransactionAPI.Data;
using TransactionAPI.DTOs;
using TransactionAPI.Models;

namespace TransactionAPI.Controllers
{
    [ApiController]
    [Route("api/transactions")]
    [Authorize]
    public class TransactionsController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;

        public TransactionsController(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        private int GetUserId()
        {
            return int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        }

        private List<TransactionDTO> FetchAll(int userId)
        {
            var list = _db.Transactions
                .Where(t => t.UserId == userId)
                .OrderByDescending(t => t.Date)
                .ToList();
            return _mapper.Map<List<TransactionDTO>>(list);
        }

        [HttpGet]
        public IActionResult GetTransactions()
        {
            return Ok(FetchAll(GetUserId()));
        }

        [HttpPost]
        public IActionResult AddTransaction([FromBody] TransactionInputDTO input)
        {
            var userId = GetUserId();

            var t = new Transaction
            {
                Amount = input.Amount,
                Type = input.Type,
                Date = DateTime.Now,
                UserId = userId,
                User = null
            };

            _db.Transactions.Add(t);
            _db.SaveChanges();

            Console.WriteLine(">>> Saved transaction Id=" + t.Id + " UserId=" + t.UserId);

            return Ok(FetchAll(userId));
        }

        [HttpPut("{id}")]
        public IActionResult UpdateTransaction(int id, [FromBody] TransactionInputDTO input)
        {
            var userId = GetUserId();
            var t = _db.Transactions.FirstOrDefault(t => t.Id == id && t.UserId == userId);
            if (t == null) return NotFound();
            t.Amount = input.Amount;
            t.Type = input.Type;
            t.Date = DateTime.Now;
            _db.SaveChanges();
            return Ok(FetchAll(userId));
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTransaction(int id)
        {
            var userId = GetUserId();
            var t = _db.Transactions.FirstOrDefault(t => t.Id == id && t.UserId == userId);
            if (t == null) return NotFound();
            _db.Transactions.Remove(t);
            _db.SaveChanges();
            return Ok(FetchAll(userId));
        }
    }
}
