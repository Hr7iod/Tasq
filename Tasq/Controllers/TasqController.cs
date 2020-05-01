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

        [HttpGet("{id}")]
        public IActionResult GetTasq(Guid id)
        {
            var tasq = _repository.Tasq.GetTasq(id, trackChanges: false);
            if (tasq == null)
            {
                _logger.LogInfo($"Tasq with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            else
            {
                var tasqDto = _mapper.Map<TasqDto>(tasq);
                return Ok(tasqDto);
            }
        }

        [HttpGet("{tasqId}/children")]
        public IActionResult GetChildrenForTasq(Guid tasqId)
        {
            var tasq = _repository.Tasq.GetTasq(tasqId, trackChanges: false);
            if (tasq == null)
            {
                _logger.LogInfo($"Tasq with id: {tasqId} doesn't exist in the database.");
                return NotFound();
            }

            var childrenFromDb = _repository.Tasq.GetChildren(tasqId, trachChanges: false);
            var childrenFromDbDto = _mapper.Map<IEnumerable<TasqDto>>(childrenFromDb);
            return Ok(childrenFromDbDto);
        }

        [HttpGet("{tasqId}/children/{childId}")]
        public IActionResult GetChildForTasq(Guid tasqId, Guid childId)
        {
            var tasq = _repository.Tasq.GetTasq(tasqId, trackChanges: false);
            if (tasq == null)
            {
                _logger.LogInfo($"Tasq with id: {tasqId} doesn't exist in the database.");
                return NotFound();
            }

            var childDb = _repository.Tasq.GetChild(tasqId, childId, trackChanges: false);
            if (childDb == null)
            {
                _logger.LogInfo($"Tasq with id: {childId} doesn't exist in the database.");
                return NotFound();
            }

            var child = _mapper.Map<TasqDto>(childDb);
            return Ok(child);
        }
    }
}