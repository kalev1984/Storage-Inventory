using App.Contracts.DAL;
using App.Domain;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class ImageRepository : BaseEntityRepository<Image, AppDbContext>, IImageRepository
{
    public ImageRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
}