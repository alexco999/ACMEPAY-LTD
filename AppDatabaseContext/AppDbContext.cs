using ACMEPAY_LTD.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACMEPAY_LTD.AppDatabaseContext
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions options) : base(options)
		{}

		public DbSet<TransactionDetail> TransactionDetails { get; set; }
	}
}
