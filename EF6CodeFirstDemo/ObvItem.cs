namespace EF6CodeFirstDemo
{
    public class ObvItem
    {
        public string Typ { get; set; }
        public int Rok { get; set; }
        public int Cislo { get; set; }
        public int Poradi { get; set; }

        public string Nazev { get; set; }

        public virtual Obv Obv { get; set; }
    }
}
