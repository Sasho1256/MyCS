using CsvHelper;
using Database;
using System.Globalization;
using System.IO;

namespace Services
{
    public class SeedService : ISeedService
    {
        public void SeedRecords()
        {
            using var reader = new StreamReader("..\\Services\\Data\\data.csv");
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            var records = csv.GetRecords<Account>();
        }
    }
}
