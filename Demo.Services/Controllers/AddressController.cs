using Demo.Business.Data;
using Demo.Business.Implementation;
using Demo.Model.Entity;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Services.Controllers
{
    [Produces("application/json")]
    [Route("api/address")]
    public class AddressController: AbstractController<Address>
    {
        private readonly IAddress addressRepository;
        public AddressController(DataContext context, IAddress _addressRepository)
        {
            this.addressRepository = _addressRepository;
            this.context = context;
        }

    }
}
