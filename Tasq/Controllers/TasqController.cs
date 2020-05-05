﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using LoggerService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Tasq.ActionFilters;
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
        public async Task<IActionResult> GetTasqs()
        {

            //throw new Exception("ОШИБКА СТОП НОЛЬНОЛЬНОЛЬ");
            var tasqs = await _repository.Tasq.GetAllTasqsAsync(trackChanges: false);

            var tasqsDto = _mapper.Map<IEnumerable<TasqDto>>(tasqs);

            return Ok(tasqsDto);
        }

        [HttpGet("{id}", Name = "TasqById")]
        public async Task<IActionResult> GetTasq(Guid id)
        {
            var tasq = await _repository.Tasq.GetTasqAsync(id, trackChanges: false);
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
        public async Task<IActionResult> GetChildrenForTasq(Guid tasqId)
        {
            var tasq = await _repository.Tasq.GetTasqAsync(tasqId, trackChanges: false);
            if (tasq == null)
            {
                _logger.LogInfo($"Tasq with id: {tasqId} doesn't exist in the database.");
                return NotFound();
            }

            var childrenFromDb = await _repository.Tasq.GetChildrenAsync(tasqId, trachChanges: false);
            var childrenFromDbDto = _mapper.Map<IEnumerable<TasqDto>>(childrenFromDb);
            return Ok(childrenFromDbDto);
        }

        [HttpGet("{tasqId}/children/{childId}", Name = "GetChildTasq")]
        public async Task<IActionResult> GetChildForTasq(Guid tasqId, Guid childId)
        {
            var tasq = await _repository.Tasq.GetTasqAsync(tasqId, trackChanges: false);
            if (tasq == null)
            {
                _logger.LogInfo($"Tasq with id: {tasqId} doesn't exist in the database.");
                return NotFound();
            }

            var childDb = await _repository.Tasq.GetChildAsync(tasqId, childId, trackChanges: false);
            if (childDb == null)
            {
                _logger.LogInfo($"Tasq with id: {childId} doesn't exist in the database.");
                return NotFound();
            }

            var child = _mapper.Map<TasqDto>(childDb);
            return Ok(child);
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateTasq([FromBody]TasqForCreationDto tasq)
        {
            var tasqEntity = _mapper.Map<Entities.Models.Tasq>(tasq);

            _repository.Tasq.CreateTasq(tasqEntity);
            await _repository.SaveAsync();

            var tasqToReturn = _mapper.Map<TasqDto>(tasqEntity);

            return CreatedAtRoute("TasqById", new { id = tasqToReturn.Id }, tasqToReturn);
        }

        [HttpPost("{tasqId}/children")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateChildTasq(Guid tasqId, [FromBody]TasqForCreationDto childTasq)
        {
            var parentTasq = await _repository.Tasq.GetTasqAsync(tasqId, trackChanges: false);
            var tasqEntity = _mapper.Map<Entities.Models.Tasq>(childTasq);

            _repository.Tasq.CreateChildTasq(tasqId, tasqEntity);
            await _repository.SaveAsync();

            var tasqToReturn = _mapper.Map<TasqDto>(tasqEntity);

            return CreatedAtRoute("GetChildTasq", new { tasqId, childId = tasqToReturn.Id }, tasqToReturn);
        }

        [HttpGet("collection/({ids})", Name = "TasqCollection")]
        public async Task<IActionResult> GetTasqCollection([ModelBinder(BinderType = typeof(ArrayModelBinder))]IEnumerable<Guid> ids)
        {
            if (ids == null)
            {
                _logger.LogError("Parameter ids is null");
                return BadRequest("Parameter ids is null");
            }

            var tasqEntities = await _repository.Tasq.GetByIdsAsync(ids, trackChanges: false);

            if (ids.Count() != tasqEntities.Count())
            {
                _logger.LogError("Some ids are not valid in a collection");
                return NotFound();
            }

            var tasqToReturn = _mapper.Map<IEnumerable<TasqDto>>(tasqEntities);
            return Ok(tasqToReturn);
        }

        [HttpPost("collection")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateTasqCollection([FromBody]IEnumerable<TasqForCreationDto> tasqCollection)
        {
            var tasqEntities = _mapper.Map<IEnumerable<Entities.Models.Tasq>>(tasqCollection);
            foreach (var tasq in tasqEntities)
            {
                _repository.Tasq.CreateTasq(tasq);
            }

            await _repository.SaveAsync();

            var tasqCollectionToReturn = _mapper.Map<IEnumerable<TasqDto>>(tasqEntities);
            var ids = string.Join(",", tasqCollectionToReturn.Select(t => t.Id));

            return CreatedAtRoute("TasqCollection", new { ids }, tasqCollectionToReturn);
        }

        [HttpDelete("{tasqId}/children/{childId}")]
        [ServiceFilter(typeof(ValidateChildTasqExistsAttribute))]
        public async Task<IActionResult> DeleteChildTasq(Guid tasqId, Guid childId)
        {
            var tasqChild = HttpContext.Items["child"] as Entities.Models.Tasq;

            _repository.Tasq.DeleteTasq(tasqChild);
            await _repository.SaveAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ServiceFilter(typeof(ValidateTasqExistsAttribute))]
        public async Task<IActionResult> DeleteTasq(Guid id)
        {
            var tasq = HttpContext.Items["tasq"] as Entities.Models.Tasq;

            var tasqChildren = await _repository.Tasq.GetChildrenAsync(id, trachChanges: false);
            foreach(var tasqChild in tasqChildren)
            {
                _repository.Tasq.DeleteTasq(tasqChild);
            }

            _repository.Tasq.DeleteTasq(tasq);
            await _repository.SaveAsync();

            return NoContent();
        }

        [HttpPut("{tasqId}/children/{childId}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(ValidateChildTasqExistsAttribute))]
        public async Task<IActionResult> UpdateChildTasq(Guid tasqId, Guid childId, [FromBody]TasqForUpdateDto tasq)
        {
            var tasqEntity = HttpContext.Items["child"] as Entities.Models.Tasq;

            _mapper.Map(tasq, tasqEntity);
            await _repository.SaveAsync();

            return NoContent();
        }

        [HttpPut("{id}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(ValidateTasqExistsAttribute))]
        public async Task<IActionResult> UpdateTasq(Guid id, [FromBody]TasqForUpdateDto tasq)
        {
            var tasqEntity = HttpContext.Items["tasq"] as Entities.Models.Tasq;

            _mapper.Map(tasq, tasqEntity);
            await _repository.SaveAsync();

            return NoContent();
        }

        [HttpPatch("{tasqId}/children/{childId}")]
        [ServiceFilter(typeof(ValidateChildTasqExistsAttribute))]
        public async Task<IActionResult> PartiallyUpdateChildTasq(Guid tasqId, Guid childId, [FromBody]JsonPatchDocument<TasqForUpdateDto> patchDoc)
        {
            var tasqEntity = HttpContext.Items["child"] as Entities.Models.Tasq;

            var tasqToPatch = _mapper.Map<TasqForUpdateDto>(tasqEntity);
            patchDoc.ApplyTo(tasqToPatch, ModelState);

            TryValidateModel(tasqToPatch);

            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state for the TasqForUpdateDto object");
                return UnprocessableEntity(ModelState);
            }

            _mapper.Map(tasqToPatch, tasqEntity);
            await _repository.SaveAsync();

            return NoContent();
        }

        [HttpPatch("{id}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(ValidateTasqExistsAttribute))]
        public async Task<IActionResult> PatriallyUpdateTasq(Guid id, [FromBody]JsonPatchDocument<TasqForUpdateDto> patchDoc)
        {
            var tasqEntity = HttpContext.Items["tasq"] as Entities.Models.Tasq;

            var tasqToPatch = _mapper.Map<TasqForUpdateDto>(tasqEntity);
            patchDoc.ApplyTo(tasqToPatch, ModelState);

            TryValidateModel(tasqToPatch);

            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state for the TasqForUpdateDto object");
                return UnprocessableEntity(ModelState);
            }

            _mapper.Map(tasqToPatch, tasqEntity);
            await _repository.SaveAsync();

            return NoContent();
        }
    }
}