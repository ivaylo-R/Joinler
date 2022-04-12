using AutoMapper;
using Joinler.Data.Models;
using Joinler.Models;

namespace Joinler.Infrastructure
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            this.CreateMap<UserProfile, UserViewModel>();
        }
    }
}
