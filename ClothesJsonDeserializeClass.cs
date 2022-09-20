namespace ClothesWeather;

public class ClothesJsonDeserializeClass
{
    public class BitCold
    {
        public string shorts { get; set; }
    }

    public class Clothes
    {
        public MustHave must_have { get; set; }
        public Summer summer { get; set; }
        public BitCold bit_cold { get; set; }
        
        public DemiSeason DemiSeason { get; set; }
        public Winter winter { get; set; }
    }

    public class DemiSeason
    {
        public string upper_body { get; set; }
        public string shorts { get; set; }
    }

    public class MustHave
    {
        public string body { get; set; }
    }

    public class Root
    {
        public List<Clothes> Clothes { get; set; }
    }

    public class Summer
    {
        public string shorts { get; set; }
    }

    public class Winter
    {
        public string upper_body { get; set; }
        public string body { get; set; }
        public string shorts { get; set; }
    }
}