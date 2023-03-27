namespace BE_CRUDPets.Models.DTO
{
    public class PetDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Race { get; set; }
        public string Color { get; set; }
        public int Age { get; set; }
        public float Weight { get; set; }
    }
}
