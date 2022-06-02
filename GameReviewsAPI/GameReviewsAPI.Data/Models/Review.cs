using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameReviewsAPI.Data.Models
{
    public class Review
    {
        public long Id { get; set; }
        public Game Game { get; set; }
        public string ReviewText { get; set; }
        public float Rating { get; set; }
    }
}
