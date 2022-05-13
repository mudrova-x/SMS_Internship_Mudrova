using Notes.Application.Interfaces;
using System;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Notes.WebApi.Services
{
    public class CurrentUserService: ICurrentUserService
        {
            private readonly IHttpContextAccessor _httpContextAccessor;

            public CurrentUserService(IHttpContextAccessor httpContextAccessor) =>
                _httpContextAccessor = httpContextAccessor;

            public Guid UserId
            {
                get
                {
                // httpContextAccessor предоставляет доступ к текущему http контексту, откуда можно достать инф о пользователе
                var id = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
                   
                return string.IsNullOrEmpty(id) ? Guid.Empty : Guid.Parse(id);
                }
            }
        
    }
}
