using SrmApi.Context;
using SrmApi.Interfaces;
using SrmApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SrmApi.Enums;

namespace SrmApi.Logic
{
    public class TaskLogic : IWorkEntity<SrmApi.Models.TaskEntity>
    {
        private readonly IServiceProvider _provider;

        public TaskLogic(IServiceProvider provider)
        {
            _provider = provider;
        }

        public bool DeleteEntity(int entityId)
        {
            using (var context = _provider.GetService<DbConnectContext>())
            {
                var task = context.Tasks.FirstOrDefault(x => x.Id == entityId);

                if (task != null)
                {
                    try
                    {
                        task.Status = (int)Status.Removed;
                        context.SaveChanges();

                        return true;
                    }
                    catch (Exception ex)
                    {
                        var error = ex.Message;
                        return false;
                    }

                }
                else
                {
                    return false;
                }
            }
        }

        public bool EditEntity(TaskEntity entity)
        {
            using (var context = _provider.GetService<DbConnectContext>())
            {
                try
                {
                    SrmApi.Models.TaskEntity task;
                    if (entity.Id > 0)
                    {
                        task = context.Tasks.FirstOrDefault(x => x.Id == entity.Id);
                    }
                    else
                    {
                        task = new SrmApi.Models.TaskEntity();
                        task.DateCreate = DateTime.Now;
                        context.Tasks.Add(task);
                    }

                    task.Name = entity.Name;
                    task.Status = entity.Status;
                    task.DateUpdate = DateTime.Now;
                    task.Description = entity.Description;

                    context.SaveChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    var error = ex.Message;
                    return false;
                }
            }
        }

        public async Task<IEnumerable<TaskEntity>> GetEntitiesAsync()
        {
            using (var context = _provider.GetService<DbConnectContext>())
            {
                var result = await context.Tasks.Where(x => x.Status != (int)Status.Removed).ToListAsync();

                return result;
            }
        }

        public TaskEntity GetEntity(int entityId)
        {
            using (var context=_provider.GetService<DbConnectContext>())
            {
                var result = context.Tasks.FirstOrDefault(x=>x.Id==entityId);

                return result;
            }
        }
    }
}
