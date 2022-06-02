using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameReviewsAPI.Data.Dto
{
    public class ReviewInfoDto
    {
        public long GameId { get; set; }
        public string ReviewText { get; set; }
        public float Rating { get; set; }
    }
}
