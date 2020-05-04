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
using Tasq.ModelBinders;

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

        [HttpGet("{id}", Name = "TasqById")]
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

        [HttpGet("{tasqId}/children/{childId}", Name = "GetChildTasq")]
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

        [HttpPost]
        public IActionResult CreateTasq([FromBody]TasqForCreationDto tasq)
        {
            if (tasq == null)
            {
                _logger.LogError("TasqForCreationDto object sent from client is null.");
                return BadRequest("TasqFroCrationDto object is null");
            }

            var tasqEntity = _mapper.Map<Entities.Models.Tasq>(tasq);

            _repository.Tasq.CreateTasq(tasqEntity);
            _repository.Save();

            var tasqToReturn = _mapper.Map<TasqDto>(tasqEntity);

            return CreatedAtRoute("TasqById", new { id = tasqToReturn.Id }, tasqToReturn);
        }

        [HttpPost("{tasqId}/children")]
        public IActionResult CreateChildTasq(Guid tasqId, [FromBody]TasqForCreationDto childTasq)
        {
            if(childTasq == null)
            {
                _logger.LogError("TasqForCreationDto object sent from client is null.");
                return BadRequest("TasqForCreationDto object is null");
            }

            var parentTasq = _repository.Tasq.GetTasq(tasqId, trackChanges: false);
            if(parentTasq == null)
            {
                _logger.LogInfo($"Parent tasq with id {tasqId} doesn't exist in the database.");
                return NotFound();
            }

            var tasqEntity = _mapper.Map<Entities.Models.Tasq>(childTasq);

            _repository.Tasq.CreateChildTasq(tasqId, tasqEntity);
            _repository.Save();

            var tasqToReturn = _mapper.Map<TasqDto>(tasqEntity);

            return CreatedAtRoute("GetChildTasq", new { tasqId, childId = tasqToReturn.Id }, tasqToReturn);
        }

        [HttpGet("collection/({ids})", Name = "TasqCollection")]
        public IActionResult GetTasqCollection([ModelBinder(BinderType = typeof(ArrayModelBinder))]IEnumerable<Guid> ids)
        {
            if(ids == null)
            {
                _logger.LogError("Parameter ids is null");
                return BadRequest("Parameter ids is null");
            }

            var tasqEntities = _repository.Tasq.GetByIds(ids, trackChanges: false);

            if(ids.Count() != tasqEntities.Count())
            {
                _logger.LogError("Some ids are not valid in a collection");
                return NotFound();
            }

            var tasqToReturn = _mapper.Map<IEnumerable<TasqDto>>(tasqEntities);
            return Ok(tasqToReturn);
        }

        [HttpPost("collection")]
        public IActionResult CreateTasqCollection([FromBody]IEnumerable<TasqForCreationDto> tasqCollection)
        {
            if(tasqCollection == null)
            {
                _logger.LogError("Tasq collection sent from client is null.");
                return BadRequest("Tasq collection is null");
            }

            var tasqEntities = _mapper.Map<IEnumerable<Entities.Models.Tasq>>(tasqCollection);
            foreach (var tasq in tasqEntities)
            {
                _repository.Tasq.CreateTasq(tasq);
            }

            _repository.Save();

            var tasqCollectionToReturn = _mapper.Map<IEnumerable<TasqDto>>(tasqEntities);
            var ids = string.Join(",", tasqCollectionToReturn.Select(t => t.Id));

            return CreatedAtRoute("TasqCollection", new { ids }, tasqCollectionToReturn);
        }

        

    }
}