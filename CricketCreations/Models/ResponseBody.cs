using System;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace CricketCreations.Models
{
    public class ResponseBody<T>
    {
        private readonly T _t;
        private readonly string _type;
        private readonly int? _count;

        public ResponseBody(T typ, string s, int? cnt)
        {
            _t = typ;
            _type = s;
            _count = cnt;
        }

        public string Id
        {
            get
            {
                Type tp = _t.GetType();
                PropertyInfo prop = tp.GetProperty("Id");
                string val = prop != null && prop.GetValue(_t) != null ? prop.GetValue(_t).ToString() : null;
                return val;
            }
        }

        public string Type
        {
            get
            {
                return _type ?? typeof(T).ToString();
            }
        }

        public object Meta
        {
            get
            {
                return new
                {
                    total = _count,
                };
            }
        }

        public T Data
        {
            get
            {
                return _t;
            }
        }

        public T Errors { get; set; }

        public string GetJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
            });
        }
    }
}
