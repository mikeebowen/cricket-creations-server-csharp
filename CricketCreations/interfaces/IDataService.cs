using CricketCreations.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CricketCreations.Interfaces
{
    public interface IDataService<T>
    {
        public abstract int? Id { get; set; }
        public abstract DateTime Created { get; set; }
        public abstract DateTime LastUpdated { get; set; }
        public abstract Task<T> GetById(int id, bool? myBool);
        public abstract Task<List<T>> GetAll(int? id);
        public abstract Task<int> GetCount();
        public abstract Task<List<T>> GetRange(int page, int count, int? id);
        public abstract Task<T> Create(T t, int userId);
        public abstract Task<T> Update(T t);
        public abstract Task<bool> Delete(int id);
    }
}
