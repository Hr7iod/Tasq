using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using LoggerService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Tasq.Controllers
{
    [Route("api/tasqs")]
    [ApiController]
    public class TasqController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public TasqController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetTasqs()
        {

            //throw new Exception("ОШИБКА СТОП НОЛЬНОЛЬНОЛЬ");
            var tasqs = _repository.Tasq.GetAllTasqs(trackChanges: false);

            var tasqsDto = _mapper.Map<IEnumerable<TasqDto>>(tasqs);

            return Ok(tasqsDto);
        }
    }
}