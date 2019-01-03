using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MTest.Data.Entities;
using MTest.Infra;
using MTest.Models;
using MTest.Services;
using MTest.Services.Contracts;
using MTest.Validators;

namespace MTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        //Assumption: This controller is small enough that I can do few injections via constructor
        //If it is designed for a controller that can potentially have a lot of dependencies(try to imagine 20 of ixxxxxService at constructor), then I will go for a different option such as dispatch request from one dispatcher and handle them
        //at different handler(tools like MediatR can help that). So that the dependency injection will be much simpler and may just have to only inject a dispatcher. However, it will 
        //increase code complexity
        private readonly ICustomerService _customerService;
        private readonly IMapper _mapper;
        public CustomersController(ICustomerService customerService, IMapper mapper)
        {
            _customerService = customerService;
            _mapper = mapper;
        }

        // GET api/customers
        [HttpGet]
        public async Task<ActionResult<PagedResult<Customer>>> Get([FromQuery] GetCustomersRequest request)
        {
            var validator = new GetCustomersRequestValidator();
            var validationResult = validator.Validate(request);
            if (!validationResult.IsValid)
            {
                return BadRequest(new { ErrorMessage = validationResult.ErrorMessage });
            }

            var filter = _mapper.Map<GetCustomersFilter>(request);
            var result = await _customerService.GetCustomersAsync(filter);
            var customers = _mapper.Map<IEnumerable<CustomerEntity>, IEnumerable<Customer>>(result.Result.Items);
            //A pagination result will give frontend flexibility to display data in pages.
            return Ok(new PagedResult<Customer>() { Page = result.Result.Page, TotalCount = result.Result.TotalCount, RecordsPerPage = result.Result.RecordsPerPage, Items = customers });
        }

        // GET api/customers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> Get(int id)
        {
            var validator = new GetCustomerRequestValidator();
            var validationResult = validator.Validate(id);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.ErrorMessage);
            }

            var result = await _customerService.GetCustomerAsync(id);

            if (!result.IsSuccessful)
            {
                return BadRequest(new { ErrorMessage = result.ErrorMessage });
            }

            var customer = _mapper.Map<Customer>(result.Result);
            return Ok(customer);
        }



        // POST api/customers
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] AddCustomerRequest request)
        {
            /*
            Assumption: Errors that I return from here are not really in a proper structure, in order to handle more complex case, I will need to define a proper json for Error return
            For example: 
             {
              "errors": [
                {
                  "code": "001",
                  "message": "some error here"
                },
                {
                  "code": "002",
                  "message": "some error here"
                }
              ]            
              }
              */
            var validator = new AddCustomerRequestValidator();
            var validationResult = validator.Validate(request);
            if (!validationResult.IsValid)
            {
                return BadRequest(new { ErrorMessage = validationResult.ErrorMessage });

            }

            var customerEntity = _mapper.Map<CustomerEntity>(request);

            var result = await _customerService.AddCustomerAsync(customerEntity);
            if (!result.IsSuccessful)
            {
                return BadRequest(new { ErrorMessage = result.ErrorMessage });
            }

            var customer = _mapper.Map<Customer>(result.Result);
            return Created($"{Request.Path}/{result.Result.Id}", customer);
        }
    }
}
