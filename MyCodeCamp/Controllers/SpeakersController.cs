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
    [Route("api/camps/{campId}/speakers")]
    [ValidateModel]
    public class SpeakersController : BaseController<SpeakersController>
    {
        private readonly ICampRepository _campRepository;
        private readonly IMapper _mapper;

        public SpeakersController(
            ICampRepository campRepository,
            IMapper mapper)
        {
            _campRepository = campRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get(int campId)
        {
            var speakers = _campRepository.GetSpeakers(campId);

            return Ok(_mapper.Map<IEnumerable<SpeakerModel>>(speakers));
        }

        [HttpGet("{speakerId}", Name = "SpeakerGet")]
        public IActionResult Get(int campId, int speakerId)
        {
            var speaker = _campRepository.GetSpeaker(speakerId);
            if (speaker == null) return NotFound();
            if (speaker.Camp.Id != campId) return BadRequest();

            return Ok(_mapper.Map<SpeakerModel>(speaker));
        }

        [HttpPost]
        public async Task<IActionResult> Post(int campId, [FromBody] SpeakerModel model)
        {
            var camp = _campRepository.GetCamp(campId);
            if (camp == null) return BadRequest("No camp found");

            var speaker = _mapper.Map<Speaker>(model);
            speaker.Camp = camp;

            _campRepository.Add(speaker);
            await _campRepository.SaveAllAsync();

            var url = Url.Link("SpeakerGet", new { campId = camp.Id, speakerId = speaker.Id });
            return Created(url, _mapper.Map<SpeakerModel>(speaker));
        }
    }
}