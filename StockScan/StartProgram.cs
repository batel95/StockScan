using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace StockScan
{
	public class StartProgram
	{
		private static readonly HttpClient client = new HttpClient();
		private static readonly List<Repository> repositories = new List<Repository>();
		private static readonly List<string> Tickers = new();

		public static async Task<int> Start()
		{
			DateTime Date = DateTime.Today;
			for (int i = 1; i <= Info.DAYS_TO_DOWNLOAD_DATA; i++)
			{
				DateTime date = Date.AddDays(- i);
				if (date.DayOfWeek != DayOfWeek.Sunday && date.DayOfWeek != DayOfWeek.Saturday)
				{
					var r = await ProcessRepositories(date);
					if (r != null && r.Results != null)
					{
						repositories.Add(r);
						repositories[^1].ClearBadResults();
					}
				}
			}

			FilterTickers();
			foreach (var ticker in Tickers)
			{
				Console.WriteLine(ticker);
			}


			return 0;
		}

		private static void FilterTickers()
		{
			foreach (var result in repositories[0].Results)
			{
				var isTicker = true;
				for(int i = 1; i < repositories.Count; i++)
				{
					isTicker = isTicker && IsTickerInResult(result.Ticker, repositories[i].Results);
				}

				if (isTicker)
				{
					Tickers.Add(result.Ticker);
				}
			}
		}

		private static bool IsTickerInResult(string ticker, List<Result> results)
		{
			foreach (Result result in results)
			{
				if (result.Ticker == ticker)
					return true;
			}
			return false;
		}

		private static async Task<Repository> ProcessRepositories(DateTime Date)
		{
		
			client.DefaultRequestHeaders.Accept.Clear();
			
			var streamTask = client.GetStreamAsync(string.Format(Info.BASE_URL, Date.ToString("yyyy-MM-dd")));
			//var repositories = await JsonSerializer.DeserializeAsync<Repository>(await streamTask);
			//Console.WriteLine(streamTask);
			var repository = await JsonSerializer.DeserializeAsync<Repository>(await streamTask);
			if (repository == null)
				return new Repository();
			return repository;
		}


	}
}
