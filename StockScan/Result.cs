using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace StockScan
{
	internal class Result
	{
		/// <summary>
		/// T (string) is the symbol that the item is trade under
		/// </summary>
		[JsonPropertyName("T")]
		public string Ticker { get; set; }

		/// <summary>
		/// c (double) is the close price for the symbol
		/// </summary>
		[JsonPropertyName("c")]
		public double Close { get; set; }

		/// <summary>
		/// h (double) is the highest price for the symbol
		/// </summary>
		[JsonPropertyName("h")]
		public double High { get; set; }

		/// <summary>
		/// l (double) is the lowest price for the symbol
		/// </summary>
		[JsonPropertyName("l")]
		public double Low { get; set; }

		/// <summary>
		/// o (double) is the open price for the symbol
		/// </summary>
		[JsonPropertyName("o")]
		public double Open { get; set; }

		/// <summary>
		/// t (integer) is the Unix Msec timestamp for the start of the aggregate window
		/// </summary>
		[JsonPropertyName("t")]
		public long Time { get; set; }

		/// <summary>
		/// v (double) is thetrading volume in the given time period
		/// </summary>
		[JsonPropertyName("v")]
		public double Volume { get; set; }

		/// <summary>
		/// vw (double) is the volume weighted average price
		/// </summary>
		[JsonPropertyName("vw")]
		public double VolumeWeighted { get; set; }

		public override string ToString()
		{
			return "\n\tTicker:\t" + Ticker + "\n\tOpen:\t" + Open + "\n\tHigh:\t" + High + "\n\tLow:\t" + Low + "\n\tClose:\t" + Close + "\n\tTime:\t" + Time + "\n\tVolume:\t" + Volume;
		}

		public bool InRange()
		{
			double average = (High + Low) / 2;
			return average <= Info.MAX_RANGE && average >= Info.MIN_RANGE;
		}
	}
}
