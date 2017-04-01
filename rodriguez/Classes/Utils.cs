﻿using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace rodriguez
{
	public static class Utils
	{

		public static int WordsCount(string s)
		{
			MatchCollection collection = Regex.Matches(s, @"[\S]+");
			return collection.Count;
		}

		public static bool IsValidPhone(string telefonoDestino)
		{
			string pattern = @"1?-?8{1}(09|29|49)+-?\d{3}-?\d{4}";
			return Regex.Match(telefonoDestino, pattern).Success;
		}

		public static bool IsValidCedula(object value)
		{
			var str = value as String;
			if (String.IsNullOrEmpty(str) || ValidExceptions.Contains(str))
			{
				return true;
			}

			var regex = new Regex(@"^[0-9]{3}-?[0-9]{7}-?[0-9]{1}$");
			if (!regex.IsMatch(str))
			{
				return false;
			}

			str = Regex.Replace(str, @"[^\d]", String.Empty);

			// Do check digit.
			return CheckDigit(str);
		}

		private static bool CheckDigit(string str)
		{
			int sum = 0;
			for (int i = 0; i < 10; ++i)
			{
				int n = ((i + 1) % 2 != 0 ? 1 : 2) * (int)Char.GetNumericValue(str[i]);
				sum += (n <= 9 ? n : n % 10 + 1);
			}
			int dig = ((10 - sum % 10) % 10);

			return dig == (int)Char.GetNumericValue(str[10]);
		}

		#region Cédulas that do not fill the criteria of the algorithm
		private readonly static string[] ValidExceptions =
		{
			"03121982479",
			"40200401324",
			"40200700675",
			"01400074875",
			"01400000282",
			"01200004166",
			"00800106971",
			"00500146023",
			"03600005006",
			"00200123640",
			"00200066461",
			"00111685651",
			"00109802472",
			"00114532330",
			"00414198021",
			"02500017580",
			"02800004558",
			"03200066940",
			"03103749672",
			"90001200901",
			"03200033116",
			"03300222958",
			"09700061422",
			"03800032522",
			"03900192284",
			"00301200901",
			"04700160012",
			"04400030228",
			"03401548588",
			"04600176999",
			"04700382339",
			"04700502946",
			"04900026260",
			"05300049899",
			"05900072869",
			"07100144181",
			"07600106353",
			"07700009346",
			"00100759932",
			"00103098181",
			"00211870608",
			"10000063683",
			"00200409772",
			"00108436337",
			"00105278289",
			"00105606543",
			"00103022479",
			"00114272360",
			"12345678912",
			"00121581800",
			"00119161853",
			"00121581750",
			"10621581792",
			"09421581768",
			"22321581834",
			"22721581818"
		};


		#endregion
	}
}
