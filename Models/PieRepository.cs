using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace PieShop.Models
{
    public class PieRepository : IPieRepository
    {
        private readonly AppDbContext appDbContext;

        public PieRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public IEnumerable<Pie> AllPies => appDbContext.Pies.Include(_ => _.Category);

        public IEnumerable<Pie> PiesOfTheWeek => appDbContext.Pies.Include(_ => _.Category).Where(_ => _.IsPieOfTheWeek);

        public Pie GetPieById(int pieId)
        {
            return appDbContext.Pies.Find(pieId);
        }
    }
}
