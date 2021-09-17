﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CricketCreationsRepository.Interfaces
{
    public interface IRepository<T>
    {
        public abstract Task<T> Create(T t, int userId);
        public abstract Task<List<T>> Read();
        public abstract Task<List<T>> Read(int page, int count);
        public abstract Task<List<T>> Read(int page, int count, int id);
        public abstract Task<T> Read(int id);
        public abstract Task<T> Update(T t);
        public abstract Task<bool> Delete(int id);
        public abstract Task<int> GetCount();
    }
}
