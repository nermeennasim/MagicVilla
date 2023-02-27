namespace MagicVilla_VillaAPI.Models
{
    public class Villa
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        //forexample in real db, we have one more field of record creation
        //we dont want to expose it to api

        public DateTime? CreatedDate { get; set; }
    }
}
