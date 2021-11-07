using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CommandService.Models
{
    // Use model name as PlatformModel as this conflict with Serilog's Azure EventHub skins. 
    public class PlatformModel
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int ExternlId { get; set; }

        public ICollection<Command>  Commands { get; set; } = new List<Command>();
    }
}