using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Digests.Core.Model._4Company;
using Digests.Core.Model._4House;
using Digests.Data.Abstract;
using Digests.Data.EfCore.DbContext;
using Digests.Data.EfCore.Entities._4Company;
using Digests.Data.EfCore.Entities._4House;
using Digests.Data.EfCore.Mapper;
using Microsoft.EntityFrameworkCore;
using Shared.Database.EFCore;

namespace Digests.Data.EfCore.Repositories
{
    public class EfCompanyRepository : EfBaseRepository<EfCompany, Company>, ICompanyRepository
    {
        #region fields

        private readonly Context _context;

        #endregion



        #region ctor

        public EfCompanyRepository(Context context) : base(context, AutoMapperConfig.Mapper)
        {
            _context = context;
        }

        #endregion



        #region Special Methods
        public async Task<Company> GetCompanyByNameAsync(string companyName)
        {
            var efCompany = await _context.Companys.AsNoTracking().Include(c => c.Houses).FirstOrDefaultAsync(c => c.Name == companyName);
            var company = Mapper.Map<Company>(efCompany);
            return company;
        }
    }

    #endregion


}