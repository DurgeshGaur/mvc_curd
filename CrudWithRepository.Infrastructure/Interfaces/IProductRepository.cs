﻿using CrudWithRepository.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudWithRepository.Infrastructure.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetaAll();
        Task<Product> GetById(int id);
        Task Add(Product model);
        Task Update(Product model);
        Task Delete(int id);
    }
}
