using Autofac;
using Digests.Data.Abstract;
using Digests.Data.EfCore.Uow;

namespace WebApi.AutofacModules
{
    public class UnitOfWorkAutofacModule : Module
    {
        private readonly string _connectionString;



        #region ctor

        public UnitOfWorkAutofacModule(string connectionString)
        {
            _connectionString = connectionString;
        }

        #endregion



        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => EfUowDigests.UowDigestsFactory(_connectionString)).As<IUnitOfWorkDigests>()
                   .InstancePerLifetimeScope();
                
        }
    }
}