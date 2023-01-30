using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TosunlarYapiMarket.Data.Abstract;
using TosunlarYapiMarket.Data.Concrete.EntityFramework.Context;
using TosunlarYapiMarket.Entity.Concrete;

namespace TosunlarYapiMarket.Data.Concrete.EntityFramework.Repositories
{
    public class EfNoteRepository:BaseRepository<Note>,INoteRepository
    {
        public EfNoteRepository(TosunlarYapiMarketDbContext context) : base(context)
        {
        }
    }
}
