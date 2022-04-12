using AutoMapper;
using Joinler.Data;
using Joinler.Infrastructure.Extensions;
using Joinler.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AutoMapper.QueryableExtensions;

namespace Joinler.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext data;
        private readonly AutoMapper.IConfigurationProvider mapper;

        public UserController(
            ApplicationDbContext data, 
            IMapper mapper)
        {
            this.data = data;
            this.mapper = mapper.ConfigurationProvider;
        }

        [Authorize]
        public IActionResult Mine()
        {
            var userId = this.User.Id();

            var user = this.data
                .UserProfiles
                .Where(u=>u.Id==userId)
                .ProjectTo<UserViewModel>(this.mapper)
                .FirstOrDefault();

            return View(user);
        }

    }
}
