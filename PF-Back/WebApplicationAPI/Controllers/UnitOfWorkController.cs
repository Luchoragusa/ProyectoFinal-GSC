using Microsoft.AspNetCore.Mvc;
using WebApplicationAPI.DataAccess;

namespace WebApplicationAPI.Controllers
{
    public class UnitOfWorkController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;

        public UnitOfWorkController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
    }
}
