namespace ApiSDM.Models.ViewsModel
{
    public class GeoCoordinate
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }

    }
    public class DistaciaModel
    {
        public Repartidor repartidor { get; set; }
        public double distancia { get; set; }
        public bool rango { get; set; }
    }
}
