namespace DomainModels
{
    public class VehicleDto
    {
        public int VehicleId { get; set; }
        public string VehicleNumber { get; set; }
        public VehicleType VehicleType { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
    }
    public enum VehicleType
    {
        SUV=1,
        Sedan=2,
        Truck=3,
        EV=4
    }
}