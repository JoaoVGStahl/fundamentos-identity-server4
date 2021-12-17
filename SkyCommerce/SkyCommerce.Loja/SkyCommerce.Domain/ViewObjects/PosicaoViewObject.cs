using SkyCommerce.Models;

namespace SkyCommerce.ViewObjects
{
    public class PosicaoViewObject
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public static PosicaoViewObject FromGeoCoordinate(GeoCoordinate pos)
        {
            return new PosicaoViewObject() { Latitude = pos.Latitude, Longitude = pos.Longitude };
        }
    }
}