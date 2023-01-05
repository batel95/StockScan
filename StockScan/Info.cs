using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockScan
{
	public static class Info
	{
		public static double MIN_RANGE = 150;
		public static double MAX_RANGE = 250;

		private static string API_KEY = "My polygon api key";

		public static string BASE_URL = @"https://api.polygon.io/v2/aggs/grouped/locale/us/market/stocks/{0}?adjusted=true&apiKey=" + API_KEY;

		public static int DIS_PART_OF_100 = 20; 
		public static int VOL_PART_OF_100 = 20;

		public static int CENTURY = 100;

		public static int DAYS_TO_DOWNLOAD_DATA = 7;
	}
}
