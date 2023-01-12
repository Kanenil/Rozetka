using BAL.DTO.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Interfaces
{
    public interface ICategoryService
    {
        IEnumerable<CategoryEntityDTO> GetCategories();
    }
}
