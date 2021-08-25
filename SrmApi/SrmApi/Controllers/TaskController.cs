using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SrmApi.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SrmApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private TaskLogic TaskLogic;

        public TaskController(IServiceProvider provider)
        {
            TaskLogic = new TaskLogic(provider);
        }

        [HttpGet]
        public async Task<IEnumerable<SrmApi.Models.TaskEntity>> Get()
        {
            return await TaskLogic.GetEntitiesAsync();
        }

        [HttpGet("{id}")]
        public SrmApi.Models.TaskEntity Get(int id)
        {
            return TaskLogic.GetEntity(id);
        }

        [HttpPost]
        public bool Post(SrmApi.Models.TaskEntity entity)
        {
            var result = TaskLogic.EditEntity(entity);

            return result;
        }

        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            var result = TaskLogic.DeleteEntity(id);

            return result;
        }
    }
}
