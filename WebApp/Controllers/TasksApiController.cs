using System.Threading.Tasks;
using System.Web.Http;
using AutoFixture;
using DAL.Interfaces;
using DAL.Wrappers;

namespace WebApp.Controllers
{
    public class TasksApiController : ApiController
    {
        private const string _tfsUri = "http://tfssrv.systtech.ru:8080/tfs/defaultcollection";
        private const string _project = "STDrive";

        private Fixture _randomizer = new Fixture();
        private ITfsWrapper _wrapper;

        public TasksApiController()
        {
            _wrapper = new TfsWrapper(_tfsUri, _project);
        }

        // GET: api/TasksApi
        [HttpGet]
        [Route("api/tasks/currentsprint")]
        public async Task<IHttpActionResult> GetCurrentSprint()
        {
            var tasks = await _wrapper.GetCurrentSprintAsync();
            return Json(tasks, Configuration.Formatters.JsonFormatter.SerializerSettings);
        }

        // GET: api/TasksApi/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/TasksApi
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/TasksApi/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/TasksApi/5
        public void Delete(int id)
        {
        }
    }
}