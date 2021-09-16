using ACMEPAY_LTD.AppDatabaseContext;
using ACMEPAY_LTD.Models;
using ACMEPAY_LTD.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACMEPAY_LTD.Controllers
{
	[Route("api/")]
	[ApiController]
	public class AuthorizeController : ControllerBase
	{
		private readonly AppDbContext context;
		private GenericUtility genericUtility;
		public AuthorizeController(AppDbContext context)
		{
			this.context = context;
			genericUtility = new GenericUtility();
		}

		[HttpPost("authorize")]
		public async Task<IActionResult> Authorize(TransactionDetail TransactionDetail)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);
			await context.TransactionDetails.AddAsync(TransactionDetail);
			int res = await context.SaveChangesAsync();
			if (res > 0)
				return Ok(new { Id = TransactionDetail.PaymentId, Status = TransactionDetail.Status.ToString() });
			else
				return StatusCode(StatusCodes.Status500InternalServerError);
		}

		[HttpPost("authorize/{Id}/voids")]
		public async Task<IActionResult> Voids([FromRoute] Guid Id, [FromBody] string OrderReference)
		{
			var transaction = context.TransactionDetails.Where(w => w.PaymentId == Id && w.OrderReference == OrderReference).FirstOrDefault();
			if(transaction != null)
			{
				transaction.Status = Enums.Status.Voided;
				var res = context.TransactionDetails.Update(transaction);
				await context.SaveChangesAsync();
				return Ok(new { Id = transaction.PaymentId, Status = transaction.Status.ToString() });
			}
			return NotFound();
		}

		[HttpPost("authorize/{Id}/capture")]
		public async Task<IActionResult> Capture([FromRoute] Guid Id, [FromBody] string OrderReference)
		{
			var transaction = context.TransactionDetails.Where(w => w.PaymentId == Id && w.OrderReference == OrderReference).FirstOrDefault();
			if (transaction != null)
			{
				transaction.Status = Enums.Status.Captured;
				var res = context.TransactionDetails.Update(transaction);
				await context.SaveChangesAsync();
				return Ok(new { Id = transaction.PaymentId, Status = transaction.Status.ToString() });
			}
			return NotFound();
		}

		[HttpGet("transactions")]
		public async Task<IActionResult> GetAllTransactions()
		{
			var transactions = await context.TransactionDetails.ToListAsync();

			return Ok(transactions.Select(sel => new {
				sel.PaymentId,
				sel.Amount,
				sel.Currency,
				CardholderNumber = genericUtility.GetCardNoWithAsteriks(sel.CardholderNumber),
				sel.HolderName,
				sel.OrderReference,
				Status = sel.Status.ToString()
			}));
		}

		[HttpGet("transactions/{CurrentPage}")]
		public async Task<IActionResult> GetAllTransactions([FromRoute] int CurrentPage)
		{
			int maxRows = 10;
			var transactions = await context.TransactionDetails
								.OrderBy(o => o.Id)
								.Skip((CurrentPage - 1) * maxRows)
								.Take(maxRows).ToListAsync();

			return Ok(transactions.Select(sel => new {
				sel.PaymentId,
				sel.Amount,
				sel.Currency,
				CardholderNumber = genericUtility.GetCardNoWithAsteriks(sel.CardholderNumber),
				sel.HolderName,
				sel.OrderReference,
				Status = sel.Status.ToString()
			}));
		}
	}
}
