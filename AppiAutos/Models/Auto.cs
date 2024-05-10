namespace AppiAutos.Models
{
    public class Auto
    {
        public int IdAuto { get; set; }
        public string? Marca { get; set; }
        public decimal? Cilindraje { get; set; }

        public string? Color { get; set;}

        public string? Propietario { get; set;}
        public byte?  Estado { get; set;}


    }
}
