using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace StockScan
{
	internal class Repository
	{


		[JsonPropertyName("results")]
		public List<Result> Results { get; set; }
		
		/// <summary>
		/// status (string) The status of this request's response.
		/// </summary>
		[JsonPropertyName("status")]
		public string Status { get; set; }

		/// <summary>
		/// requestId (string) A request id assigned by the server.
		/// </summary>
		[JsonPropertyName("request_id")]
		public string RequestId { get; set; }

		/// <summary>
		/// resultsCount (integer) The total number of results for this request.
		/// </summary>
		[JsonPropertyName("count")]
		public int Count { get; set; }

		

		

		public override string ToString()
		{
			string toReturn = "Request Id:\t" + RequestId + "\nCount:\t" + Count + "\nStatus:\t" + Status + "\nResults:\t";
			if (Results != null)
			{
				foreach (var result in Results)
				{
					toReturn += "\n" + result.ToString();
				}
			}
			

			return toReturn;
		}

		public void ClearBadResults()
		{
			DeleteNotInRange();
			DeleteNotMove();
			DeleteUnderDistanse(FindMidDistanse());
			DeleteUnderVolume(FindMidVolume());
		}

		private void DeleteNotInRange()
		{
			if (Results != null)
			{
				List<Result> tempResults = new();
				//int ctr = 0;
				foreach (var result in Results)
				{
					if (result.InRange())
					{
						tempResults.Add(result);
					}
					else
					{
						//ctr++;
					}
				}
				//Console.WriteLine(ctr);
				Results = tempResults;
			}
		}

		private void DeleteNotMove()
		{
			if (Results != null)
			{
				List<Result> tempResults = new();
				//int ctr = 0;
				foreach (var result in Results)
				{
					if (result.High != result.Low)
					{
						tempResults.Add(result);
					}
					else
					{
						//ctr++;
					}
				}
				Results = tempResults;
				//Console.WriteLine(ctr);
			}
		}


		private double FindMidDistanse()
		{
			if (Results != null)
			{
				List<double> dis = new();
				foreach (var result in Results)
				{
					dis.Add(result.High - result.Low);
				}
				dis.Sort();

				return dis[(Info.CENTURY - Info.DIS_PART_OF_100) * dis.Count / Info.CENTURY];	
			}
			return -1;
		}

		private double FindMidVolume()
		{
			if (Results != null)
			{
				List<double> volume = new();
				foreach (var result in Results)
				{
					volume.Add(result.Volume);
				}
				volume.Sort();
				return volume[(Info.CENTURY - Info.VOL_PART_OF_100) * volume.Count / Info.CENTURY];
			}
			return -1;
		}

		private void DeleteUnderDistanse(double distanse)
		{
			if (Results != null)
			{
				List<Result> tempResults = new();
				foreach (var result in Results)
				{
					if (result.High - result.Low > distanse)
					{
						tempResults.Add(result);
					}
				}
				Results = tempResults;
			}
		}

		private void DeleteUnderVolume(double volume)
		{
			if (Results != null)
			{
				List<Result> tempResults = new();
				foreach (var result in Results)
				{
					if (result.Volume > volume)
					{
						tempResults.Add(result);
					}
				}
				Results = tempResults;
			}
		}

	}
}
