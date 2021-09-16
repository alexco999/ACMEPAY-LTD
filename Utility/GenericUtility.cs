using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACMEPAY_LTD.Utility
{
	public class GenericUtility
	{
		public string GetCardNoWithAsteriks(string CardNo)
		{
			int asterikLength = CardNo.Length - 10;
			string asteriks = "";
			for (int i = 0; i < asterikLength; i++)
				asteriks += "*";
			return CardNo.Remove(6, asterikLength).Insert(6, asteriks);
		}
	}
}
