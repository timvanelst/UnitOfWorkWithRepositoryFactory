using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitOfWorkWithRepositoryFactory
{
    class Program
    {
        static void Main(string[] args)
        {
            var uow = new UnitOfWork();
            var repo = uow.GetRepo<BulkDroppointRepository>();
            var repo1 = repo as BulkDroppointRepository;
            IEnumerable<BulkDroppoint> x = repo1.GetSpecificBulkDroppointStuff();

            var repo2 = uow.GetRepo<BulkTransportDtoRepository>() as BulkTransportDtoRepository;
            IEnumerable<BulkTransportDto> y = repo2.GetAll();
        }

        class UnitOfWork
        {
            public IRepository<T> GetRepo<T>() where T : class
            {
                IRepository<T>  result = null;
                Type x = typeof(T);
                switch (x.Name.ToString())
                {
                    case "BulkDroppointRepository":
                        BulkDroppointRepository bulkDroppointRepository = new BulkDroppointRepository();
                        result = (IRepository<T>)bulkDroppointRepository;
                        break;
                    case "BulkTransportDtoRepository":
                        BulkTransportDtoRepository bulkTransportDtoRepository = new BulkTransportDtoRepository();
                        result = (IRepository<T>)bulkTransportDtoRepository;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException($"{typeof(T)}");
                }

                return result;
            }
        }

        interface IRepository<T> where T : class
        {

        }

        class BulkDroppointRepository : IRepository<BulkDroppoint>
        {
            public IEnumerable<BulkDroppoint> GetSpecificBulkDroppointStuff()
            {
                return Enumerable.Empty<BulkDroppoint>();
            }
        }

        class BulkTransportDtoRepository : IRepository<BulkTransportDto>
        {
            public IEnumerable<BulkTransportDto> GetAll()
            {
                return Enumerable.Empty<BulkTransportDto>();
            }

        }

        class BulkDroppoint
        {
        }

        class BulkTransportDto
        {

        }
    }

}
