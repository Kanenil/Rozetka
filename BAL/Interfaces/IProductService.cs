﻿using BAL.DTO.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Interfaces
{
    public interface IProductService
    {
        Task CreateProduct(ProductEntityDTO entity);
    }
}
