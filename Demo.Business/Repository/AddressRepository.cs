using Demo.Business.Data;
using Demo.Business.Implementation;
using Demo.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Business.Repository
{
    public class AddressRepository : BaseRepository<Address>, IAddress
    {
        public AddressRepository(DataContext dbContext) : base(dbContext)
        {

        }

        public Address GetObject()
        {
            return new Address { Id = 1, Name = "My Address" };
        }
    }
}
