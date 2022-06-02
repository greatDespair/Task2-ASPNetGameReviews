using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameReviewsAPI.Domain.Models
{
    public class Game
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Genre { get; set; }
    }
}
