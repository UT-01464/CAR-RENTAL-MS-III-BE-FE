using CAR_RENTAL_MS_III.Entities;

namespace CAR_RENTAL_MS_III.Models.Car
{
    public class ModelDto
    {
        public int ModelId { get; set; }
        public string Name { get; set; }
        public int BrandId { get; set; }
        

        // Constructor to map Model to ModelDto
        public ModelDto(int modelId, string name, int brandId)
        {
            ModelId = modelId;
            Name = name;
            BrandId = brandId;
        }

        public ModelDto()
        {
        }
    }
}
