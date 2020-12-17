using System;
using System.Text;
using System.Linq;

using MOTDetails.GovUk;

namespace MOTDetails
{
    public class VehicleSummary
    {
        public string Make { get; }
        public string Model { get; }
        public string Colour { get; }
        public DateTime? MOTExpiry { get; }
        public Tuple<int, string> OdometerReading { get; }
        public int? Mileage
        {
            get
            {
                if (OdometerReading == null) return null;
                return OdometerReading.Item2 == "mi" ? OdometerReading.Item1 : (int)Math.Round((float)OdometerReading.Item1 / 1.609);
            }
        }

        private VehicleSummary(string make, string model, string colour, DateTime? motExpiry, Tuple<int, string> odometerReading)
        {
            Make = make;
            Model = model;
            Colour = colour;
            MOTExpiry = motExpiry;
            OdometerReading = odometerReading;
        }

        public static VehicleSummary fromGovUkRecord(VehicleRecord vehicleRecord)
        {
            MOTResult lastPassedTest = vehicleRecord.motTests
                ?.Where(test => test.testResult == "PASSED")
                ?.OrderByDescending(test => test.completedDate)
                .First();

            return new VehicleSummary(
                vehicleRecord.make,
                vehicleRecord.model,
                vehicleRecord.primaryColour,
                lastPassedTest?.expiryDate,
                lastPassedTest == null ? null : Tuple.Create(lastPassedTest.odometerValue, lastPassedTest.odometerUnit));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.Append($"Make: {Make}\nModel: {Model}\nColour: {Colour}\n");
            if (MOTExpiry.HasValue && Mileage.HasValue)
                builder.Append($"MOT Expiry Date: {MOTExpiry.Value:D}\nMileage at Last MOT: {Mileage.Value:#,##0}");
            else
                builder.Append("No history of passed MOT tests found.\nMileage at Last MOT: N/A");

            return builder.ToString();
        }
    }
}
