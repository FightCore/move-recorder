using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoveRecorder.Data
{
	public class CsvLoader
	{
		public List<MoveData> Load(string location)
		{
			using var reader = new StreamReader(location);
			using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
			return csv.GetRecords<MoveData>().ToList();
		}
	}
}
