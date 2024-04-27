using System.ComponentModel.DataAnnotations.Schema;

namespace CloudHRMS.Models.DataModels
{
    [Table("Position")]
    public class PositionEntity:BaseEntity
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
    }
}
