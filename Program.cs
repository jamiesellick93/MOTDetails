using System;
using System.Text.RegularExpressions;

namespace MOTDetails
{
    class MainClass
    {
        private static GovUk.ApiClient apiClient;

        public static void Main(string[] args)
        {
            apiClient = new GovUk.ApiClient();
            bool carryOn;

            do
            {
                Console.WriteLine("Please enter a registration number to retrieve MOT details, then hit ENTER...");
                var registrationNumber = GetRegistrationNumber();
                GetAndDisplayVehicle(registrationNumber);
                Console.WriteLine("Would you like to run another search (Y/N)?");
                carryOn = ShouldContinue();
            } while (carryOn);

        }

        static bool ShouldContinue()
        {
            var rawInput = Console.ReadLine();
            var sanitisedInput = Regex.Replace(rawInput, "[^a-z]", string.Empty, RegexOptions.CultureInvariant).ToUpper();

            if (string.IsNullOrWhiteSpace(sanitisedInput))
            {
                Console.WriteLine($"Invalid input ({rawInput}) received, please enter Y or N, then hit ENTER...");
                return ShouldContinue();
            }

            var firstChar = sanitisedInput[0];
            switch (firstChar)
            {
                case 'Y': return true;
                case 'N': return false;
                default:
                    Console.WriteLine($"Invalid input ({rawInput}) received, please enter Y or N, then hit ENTER...");
                    return ShouldContinue();
            }
        }

        static string GetRegistrationNumber()
        {
            var rawInput = Console.ReadLine();
            var sanitisedInput = Regex.Replace(rawInput, "[^a-z\\d]", string.Empty, RegexOptions.CultureInvariant).ToUpper();

            if (sanitisedInput.Length < 3)
            {
                Console.WriteLine($"Invalid input ({rawInput}) received, please enter a registration number, then hit ENTER...");
                return GetRegistrationNumber();
            }

            return sanitisedInput;
        }

        static void GetAndDisplayVehicle(string registrationNumber)
        {
            try
            {
                var vehicleRecord = apiClient.GetVehicleRecord(registrationNumber);
                Console.WriteLine(vehicleRecord == null
                    ? $"Vehicle with registration {registrationNumber} not found."
                    : VehicleSummary.fromGovUkRecord(vehicleRecord).ToString());
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to fetch vehicle details, please contact support.", ex);
            }
        }
    }
}
