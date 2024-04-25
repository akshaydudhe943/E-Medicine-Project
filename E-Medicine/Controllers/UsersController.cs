using E_Medicine_Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
namespace E_Medicine_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public UsersController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        [HttpPost]
        [Route("registration")]
        public Response register(Users user)
        {
            Response response = new Response();
            DAL dal = new DAL();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("EMedCS").ToString());
            response = dal.register(user, connection);
            return response;
        }

        [HttpPost]
        [Route("login")]
        public Response login(Users user)
        {
            DAL dal = new DAL();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("EMedCS").ToString());
            Response response = dal.login(user, connection);
            return response;
        }

        [HttpPost]
        [Route("viewUser")]
        public Response viewUser(Users user)
        {
            DAL dal = new DAL();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("EMedCS").ToString());
            Response response = dal.viewUser(user, connection);
            return response;
        }

        [HttpPost]
        [Route("updateProfile")]
        public Response updateProfile(Users user)
        {
            DAL dal = new DAL();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("EMedCS").ToString());
            Response response = dal.updateProfile(user, connection);
            return response;
        }

    }


}
