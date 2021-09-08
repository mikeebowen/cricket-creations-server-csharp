using CricketCreations.Interfaces;
using CricketCreations.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;

namespace CricketCreations.Services
{
    public class ControllerService<T> : IControllerService<T> where T : IDataService<T>
    {
        public async Task<ActionResult<ResponseBody<T>>> GetById(int id, bool? include)
        {
            try
            {
                Type type = typeof(T);
                MethodInfo getById = type.GetMethod("GetById");
                var instance = (T)Activator.CreateInstance(type);
                var element = await (Task<T>)getById.Invoke(instance, new object[] { id, include });
                if (element != null)
                {
                    ResponseBody<T> response = new ResponseBody<T>(element, type.Name.ToString(), null);
                    return response;
                }
                else
                {
                    return new NotFoundResult();
                }
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(500);
            }
        }
        public async Task<ActionResult<ResponseBody<List<T>>>> Get(string page, string count, string userId)
        {
            try
            {
                Type type = typeof(T);
                MethodInfo getCount = type.GetMethod("GetCount");
                MethodInfo getRange = type.GetMethod("GetRange");
                MethodInfo getAll = type.GetMethod("GetAll");
                var instance = (T)Activator.CreateInstance(type);
                ResponseBody<List<T>> response;
                List<T> tees;
                bool validId = int.TryParse(userId, out int id);
                bool validPage = int.TryParse(page, out int pg);
                bool validCount = int.TryParse(count, out int cnt);
                int tCount = await (Task<int>)getCount.Invoke(instance, null);
                bool inRange = tCount - (pg * cnt) >= ((cnt * -1) + 1);

                if (tCount > 0 && !inRange)
                {
                    return new StatusCodeResult(416);
                }

                if (validPage && validCount)
                {
                    if (validId)
                    {
                        tees = await (Task<List<T>>)getRange.Invoke(instance, new object[] { pg, cnt, id });
                    }
                    else
                    {
                        tees = await (Task<List<T>>)getRange.Invoke(instance, new object[] { pg, cnt, null });
                    }
                }
                else
                {
                    if (validId)
                    {
                        tees = await (Task<List<T>>)getAll.Invoke(instance, new object[] { id });
                    }
                    else
                    {
                        tees = await (Task<List<T>>)getAll.Invoke(instance, new object[] { null });
                    }

                }
                response = new ResponseBody<List<T>>(tees, type.Name.ToString(), tCount);
                return response;
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(500);
            }
        }
        public async Task<ActionResult<ResponseBody<T>>> Post(JsonElement json, int userId)
        {
            try
            {
                string jsonString = json.ToString();
                NJsonSchema.JsonSchema jsonSchema = NJsonSchema.JsonSchema.FromType<T>();
                ICollection<NJsonSchema.Validation.ValidationError> errors = jsonSchema.Validate(jsonString);

                if (errors.Count == 0)
                {
                    Type type = typeof(T);
                    MethodInfo create = type.GetMethod("Create");
                    var instance = (T)Activator.CreateInstance(type);
                    T t = JsonConvert.DeserializeObject<T>(jsonString);
                    T newT = await (Task<T>)create.Invoke(instance, new object[] { t, userId });
                    ResponseBody<T> response = new ResponseBody<T>(newT, type.Name.ToString(), null);
                    string path = $"api/{type.Name.ToLower().ToString()}/{newT.Id}";
                    Uri uri = new Uri(path, UriKind.Relative);
                    return new CreatedResult(uri, response);
                }
                else
                {
                    List<ErrorObject> errs = new List<ErrorObject>();
                    errors.ToList().ForEach(e =>
                    {
                        errs.Add(new ErrorObject() { Message = e.Kind.ToString(), Property = e.Property });
                    });
                    return new BadRequestObjectResult(errs);
                }
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(500);
            }
        }
        public async Task<ActionResult<ResponseBody<T>>> Patch(string jsonString)
        {
            try
            {
                NJsonSchema.JsonSchema jsonSchema = NJsonSchema.JsonSchema.FromType<T>();
                ICollection<NJsonSchema.Validation.ValidationError> errors = jsonSchema.Validate(jsonString);

                if (errors.Count == 0)
                {
                    Type type = typeof(T);
                    MethodInfo update = type.GetMethod("Update");
                    var instance = (T)Activator.CreateInstance(type);
                    T t = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(jsonString);
                    T newT = await (Task<T>)update.Invoke(instance, new object[] { t });
                    if (newT != null)
                    {
                        ResponseBody<T> response = new ResponseBody<T>(newT, type.Name.ToString(), null);
                        return response;
                    }
                    else
                    {
                        return new NotFoundResult();
                    }
                }
                else
                {
                    List<ErrorObject> errs = new List<ErrorObject>();
                    errors.ToList().ForEach(e =>
                    {
                        errs.Add(new ErrorObject() { Message = e.Kind.ToString(), Property = e.Property });
                    });
                    return new BadRequestObjectResult(errs);
                }
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(500);
            }
        }
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                Type type = typeof(T);
                MethodInfo delete = type.GetMethod("Delete");
                var instance = (T)Activator.CreateInstance(type);
                bool deleted = await (Task<bool>)delete.Invoke(instance, new object[] { id });
                if (deleted)
                {
                    return new StatusCodeResult(204);
                }
                else
                {
                    return new NotFoundResult();
                }
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(500);
            }
        }
    }
}
