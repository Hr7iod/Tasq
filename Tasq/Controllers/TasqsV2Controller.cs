using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts;
using Entities.RequestFeatures;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace Tasq.Controllers
{
    [Route("api/tasqs")]
    [ApiController]
    public class TasqsV2Controller : ControllerBase
    {
        private readonly IRepositoryManager _repository;

        public TasqsV2Controller(IRepositoryManager repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetTasqs([FromQuery]TasqParameters tasqParameters)
        {
            var tasqs = await _repository.Tasq.GetAllTasqsAsync(tasqParameters, trackChanges: false);

            return Ok(tasqs);
        }
    }
}