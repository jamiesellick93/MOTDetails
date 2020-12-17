namespace MOTDetails.GovUk
{
    public class VehicleRecord
    {
        public string make { get; set; }
        public string model { get; set; }
        public string primaryColour { get; set; }
        public MOTResult[] motTests { get; set; }

        public VehicleRecord()
        {
        }
    }
}
