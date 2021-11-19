using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Dionysus.DBModels
{
    [Table("rating")]
    public partial class Rating
    {
        [Key]
        [Column("rating_id")]
        public int RatingId { get; set; }
        [Required]
        [Column("username")]
        [StringLength(30)]
        public string Username { get; set; }
        [Column("batch_id")]
        public int BatchId { get; set; }
        [Column("sweatness_rating")]
        public byte SweatnessRating { get; set; }
        [Column("acidity_rating")]
        public byte AcidityRating { get; set; }
        [Column("bitterness_rating")]
        public byte BitternessRating { get; set; }
        [Column("overall_rating")]
        public byte OverallRating { get; set; }
        [Required]
        [Column("feedback", TypeName = "text")]
        public string Feedback { get; set; }
        [Column("rated_on", TypeName = "datetime")]
        public DateTime RatedOn { get; set; }

        [ForeignKey(nameof(BatchId))]
        [InverseProperty("Ratings")]
        public virtual Batch Batch { get; set; }
        [ForeignKey(nameof(Username))]
        [InverseProperty(nameof(User.Ratings))]
        public virtual User UsernameNavigation { get; set; }
    }
}
