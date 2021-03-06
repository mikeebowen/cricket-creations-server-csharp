﻿using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace CricketCreations.Models
{
    public class ResponseBody<T>
    {
        T t;
        string type;
        int? count;
        public ResponseBody(T typ, string s, int? cnt)
        {
            t = typ;
            type = s;
            count = cnt;
        }
        public string Id
        {
            get
            {
                Type tp = t.GetType();
                PropertyInfo prop = tp.GetProperty("Id");
                string val = prop != null && prop.GetValue(t)  != null ? prop.GetValue(t).ToString() : null;
                return val;
            }
        }
        public string Type
        {
            get
            {
                return type ?? typeof(T).ToString();
            }
        }
        public Object Meta
        {
            get
            {
                return new
                {
                    total = count
                };
            }
        }
        public T Data
        {
            get
            {
                return t;
            }
        }
        public T Errors { get; set; }
        public string GetJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented, new JsonSerializerSettings { 
                NullValueHandling = NullValueHandling.Ignore, 
                ContractResolver = new CamelCasePropertyNamesContractResolver() 
            });
        }
    }
}
