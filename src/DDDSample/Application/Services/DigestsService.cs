using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationMediator.Dto._4Digests;
using ApplicationMediator.Mapper;
using AutoMapper;
using CSharpFunctionalExtensions;
using Digests.Core.Model._4Company;
using Digests.Data.Abstract;

namespace ApplicationMediator.Services
{
    public class DigestsService
    {
        private readonly IUnitOfWorkDigests _uowDigests;
        private readonly IMapper _mapper;



        #region ctor

        public DigestsService(IUnitOfWorkDigests uowDigests)
        {
            _uowDigests = uowDigests;
            _mapper = ApplicationAutoMapperConfig.Mapper;
        }


        static DigestsService()
        {
            ApplicationAutoMapperConfig.Register();
            try
            {
                ApplicationAutoMapperConfig.AssertConfigurationIsValid();//Если настройки маппинга не валидны будет выбрашенно исключение
            }
            catch (Exception e)
            {
                throw;
            }
        }

        #endregion



        #region Methods

        public async Task<List<CompanyDto>> GetCompanys()
        {
            var companys = (await _uowDigests.CompanyRepository.ListAsync()).ToList();
            var companysDto = _mapper.Map<List<CompanyDto>>(companys);
            return companysDto;
        }


        public async Task<Result> AddNewCompany(CompanyDto companyDto)
        {
           var companyDetailResult = CompanyDetails.Create(companyDto.CompanyDetails.DetailInfo);
           if (companyDetailResult.IsFailure)
           {
               return Result.Fail(companyDetailResult.Error);
           }
           var companyResult= Company.Create(companyDto.Name, companyDetailResult.Value);
           if(companyResult.IsFailure)
           {
               return Result.Fail(companyDetailResult.Error);
           }

           await _uowDigests.CompanyRepository.AddAsync(companyResult.Value);
           await _uowDigests.SaveChangesAsync();
           return Result.Ok();
        }

        #endregion
    }
}