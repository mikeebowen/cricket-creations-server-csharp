using CricketCreations.Models;
using CricketCreationsRepository.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace CricketCreations.Interfaces
{
    public interface IApiService<T>
    {
        public abstract Task<T> Read(int id);
        public abstract Task<List<T>> Read(int page, int count);
        public abstract Task<List<T>> Read(int page, int count, int userId);
        public abstract Task<T> Update(T t);
        public abstract Task<T> Create(T t, int userId);
        public abstract Task<bool> Delete(int id);
        public abstract Task<int> GetCount();
        public abstract Task<int> GetCount(int id);
    }
}
