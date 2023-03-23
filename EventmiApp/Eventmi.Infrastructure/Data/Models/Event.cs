using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;


namespace Eventmi.Infrastructure.Data.Models
{

    [Comment("Събития")]
    public class Event
    {

        [Key]
        [Comment("Идентификатор")]
        public int Id { get; set; }

        [Required]
        ///<summary>
        /// Име на събитието
        ///</summary>
        [StringLength(50)]
        [Comment("Име на събитието")]
        public string Name { get; set; }

        [Required]
        ///<summary>
        ///"Начални дата и час"
        ///</summary>
        [Comment("Начални дата и час")]
        public DateTime Start { get; set; }
        [Required]
        ///<summary>
        /// Крайни дата и час
        ///</summary>

        [Comment("Крайни дата и час")]
        public DateTime End { get; set; }

        [Required]
        [StringLength(100)]
        public string Place { get; set; } = null!;
    }
}
