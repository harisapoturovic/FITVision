namespace FitVision.Modul2.ViewModels
{
    public class OpremaGetVM
    {
        public int id { get; set; }
        public string naziv { get; set; }
        public int broj { get; set; }
        public string slika { get; set; }
        public string opis { get; set; }
        public string tipOpreme { get; set; }
        public int tip_opreme_id { get; set; }//zbog ovoga se nije učitavalo tipi opreme prilikom azuriranja
    }
}
