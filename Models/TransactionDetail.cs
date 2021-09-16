using ACMEPAY_LTD.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ACMEPAY_LTD.Models
{
	public class TransactionDetail
	{
		[Key]
		[JsonIgnore]
		public int Id { get; set; }

		[Required]
		[JsonIgnore]
		public Guid PaymentId { get; set; }

		[Column(TypeName = "decimal(18, 2)")]
		[Required(ErrorMessage = "Amount is required.")]
		public decimal Amount { get; set; }

		[Required(ErrorMessage = "Currency is required.")]
		[MaxLength(3, ErrorMessage = "Please enter valid 3 digit currency code.")]
		[MinLength(3, ErrorMessage = "Please enter valid 3 digit currency code.")]
		public string Currency { get; set; }

		[Required(ErrorMessage = "Cardholder Number is required.")]
		[RegularExpression("[0-9]{16,16}", ErrorMessage = "Cardholder Number is not valid.")]
		public string CardholderNumber { get; set; }

		[Required(ErrorMessage = "Holder Name is required.")]
		public string HolderName { get; set; }

		[Required(ErrorMessage = "Expiration Month is required.")]
		[RegularExpression("[0-9]{2,2}", ErrorMessage = "Month can only be in this 'MM' format.")]
		public int ExpirationMonth { get; set; }

		[Required(ErrorMessage = "Expiration Year is required.")]
		[RegularExpression("[0-9]{4,4}", ErrorMessage = "Year can only be in this 'YYYY' format.")]
		public int ExpirationYear { get; set; }

		[Required(ErrorMessage = "CVV is required.")]
		public int CVV { get; set; }

		[Required(ErrorMessage = "Order Reference is required.")]
		[MaxLength(50, ErrorMessage = "Order reference can have maximum length of 50.")]
		public string OrderReference { get; set; }

		[Required(ErrorMessage = "Status is required.")]
		[JsonIgnore]
		public Status Status { get; set; }

		public TransactionDetail()
		{
			PaymentId = Guid.NewGuid();
		}
	}
}
