namespace WebApplication1.Classi
{
    public class Articolo
    {
        public Int32 ArticoloID { get; set; }
        // Nome? potrebbe essere null
        public String? Nome { get; set; }
        public String? Descrizione { get; set; }
        public Single PrezzoUnitarioVendita { get; set; }
        public Int32 Giacenza { get; set; }
        public Int32 AliquotaIVA { get; set; }

        public Articolo()
        {
            
        }
    }
}
