using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace CricketCreations.Models
{
    public class APIModel<T>
    {
        public async Task<ActionResult<ResponseBody<T>>> GetById(int id)
        {
            try
            {
                Type type = typeof(T);
                MethodInfo getById = type.GetMethod("GetById", BindingFlags.Static | BindingFlags.Public);
                var element = await (Task<T>)getById.Invoke(null, new object[] { id });
                if (element != null)
                {
                    ResponseBody<T> response = new ResponseBody<T>(element, type.ToString(), null);
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
    }
}
