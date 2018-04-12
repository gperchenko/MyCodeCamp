using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyCodeCamp.Data;
using MyCodeCamp.Data.Entities;
using MyCodeCamp.Filters;
using MyCodeCamp.Models;

namespace MyCodeCamp.Controllers
{
    [Route(("api/[controller]"))]
    [ValidateModel]
    [ExceptionHandler]
    public class CampsController : BaseController<CampsController>
    {
        private readonly ICampRepository _campRepository;
        private readonly IMapper _mapper;

        public CampsController(
            ICampRepository campRepository,
            IMapper mapper)
        {
            _campRepository = campRepository;
            _mapper = mapper;
        }

        [HttpGet("")]
        public IActionResult Get()
        {
            //Logger.LogInformation("All camps request");

            var camps = _campRepository.GetAllCamps();
            return Ok(_mapper.Map<IEnumerable<CampModel>>(camps));
        }

        [HttpGet("{id}", Name = "CampGet")]
        public IActionResult Get(int id, bool includeSpeakers = false)
        {
            var camp = includeSpeakers ?
                _campRepository.GetCampWithSpeakers(id) :
                _campRepository.GetCamp(id);

            if (camp == null) return NotFound($"Camp {id} was not found");

            return Ok(_mapper.Map<CampModel>(camp));
        }

        [HttpPost("")]
        public async Task<IActionResult> Post([FromBody] CampModel model)
        {

            var camp = _mapper.Map<Camp>(model);

            _campRepository.Add(camp);
            await _campRepository.SaveAllAsync();

            var newUri = Url.Link("CampGet", new { id = camp.Id });
            return Created(newUri, _mapper.Map<CampModel>(camp));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] CampModel model)
        {
            var oldCamp = _campRepository.GetCamp(id);
            if (oldCamp == null) return NotFound($"Camp with id {id} not found");

            _mapper.Map(model, oldCamp);

            await _campRepository.SaveAllAsync();

            return Ok(_mapper.Map<CampModel>(oldCamp));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var oldCamp = _campRepository.GetCamp(id);
            if (oldCamp == null) return NotFound($"Camp with id {id} not found");

            // business rule validation here 

            _campRepository.Delete(oldCamp);

            await _campRepository.SaveAllAsync();

            return Ok();
        }
    }
}