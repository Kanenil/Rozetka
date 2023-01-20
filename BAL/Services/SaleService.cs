using AutoMapper;
using BAL.DTO.Models;
using BAL.Interfaces;
using BAL.Mapper;
using BAL.Utilities;
using DAL.Constants;
using DAL.Data;
using DAL.Data.Entities;
using DAL.Interfaces;
using DAL.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services
{
    public class SaleService
    {
        private readonly SaleRepository saleRepository;
        private readonly IMapper _mapper;
        public SaleService()
        {
            EFAppContext context = new EFAppContext();
            saleRepository = new SaleRepository(context);

            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            _mapper = configuration.CreateMapper();
        }

        public IEnumerable<SaleEntityDTO> GetAllSales()
        {
            return _mapper.Map<IEnumerable<SaleEntity>, IEnumerable<SaleEntityDTO>>(saleRepository.GetAllSales());
        }
    }
}
