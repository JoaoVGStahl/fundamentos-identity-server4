namespace SkyCommerce.CrossCutting.Services
{
    public class UserLocalizationData
    {
        public string Ip { get; }
        public string HostName { get; }
        public string Type { get; }
        public string ContinentCode { get; }
        public string ContinentName { get; }
        public string CountryCode { get; }
        public string CountryName { get; }
        public string RegionCode { get; }
        public string RegionName { get; }
        public string City { get; }
        public string Zip { get; }
        public double Latitude { get; }
        public double Longitude { get; }
    }
}