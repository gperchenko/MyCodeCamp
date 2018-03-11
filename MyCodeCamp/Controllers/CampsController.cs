using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyCodeCamp.Data;
using MyCodeCamp.Data.Entities;
using MyCodeCamp.Models;

namespace MyCodeCamp.Controllers
{
	[Route(("api/[controller]"))]
	public class CampsController : BaseController
	{
	    private ILogger<CampsController> _logger;
		private ICampRepository _repo;
	    private IMapper _mapper;

		public CampsController(
		    ICampRepository repo, 
		    ILogger<CampsController> logger,
		    IMapper mapper)
		{
			_repo = repo;
		    _logger = logger;
		    _mapper = mapper;
		}

		[HttpGet("")]
		public IActionResult Get()
		{
		    _logger.LogInformation("All camps request");

            var camps = _repo.GetAllCamps();
			return Ok(_mapper.Map<IEquatable<CampModel>>(camps));
		}

		[HttpGet("{id}", Name ="CampGet" )]
		public IActionResult Get(int id, bool includeSpeakers = false)
		{
			try
			{
				var camp = includeSpeakers ? _repo.GetCampWithSpeakers(id) : _repo.GetCamp(id);

				if (camp == null) return NotFound($"Camp {id} was not found");

				return Ok(_mapper.Map<CampModel>(camp));
			}
			catch (Exception e)
			{	
			}

			return BadRequest();
		}

        [HttpPost("")]
        public async Task<IActionResult> Post([FromBody] CampModel model)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);

                var camp = _mapper.Map<Camp>(model);

                _repo.Add(camp);
                if (await _repo.SaveAllAsync())
                {
                    var newUri = Url.Link("CampGet", new { id = camp.Id });
                    return Created(newUri, _mapper.Map<CampModel>(camp));
                }
            }
            catch (Exception e)
            {
            }

            return BadRequest();
        }

	    [HttpPut("{id}")]
	    public async Task<IActionResult> Put(int id, [FromBody] CampModel model)
	    {
	        try
	        {
	            if (!ModelState.IsValid) return BadRequest(ModelState);

                var oldCamp = _repo.GetCamp(id);
	            if (oldCamp == null) return NotFound($"Camp with id {id} not found");

	            _mapper.Map(model, oldCamp);

	            if (await _repo.SaveAllAsync())
	            {
	                return Ok(_mapper.Map<CampModel>(oldCamp));
	            }
	        }
	        catch (Exception e)
	        {
	        }

	        return BadRequest();
	    }

	    [HttpDelete("{id}")]
	    public async Task<IActionResult> Delete(int id)
	    {
	        try
	        {
	            var oldCamp = _repo.GetCamp(id);
	            if (oldCamp == null) return NotFound($"Camp with id {id} not found");

	            // business rule validation here 

                _repo.Delete(oldCamp);

	            if (await _repo.SaveAllAsync())
	            {
	                return Ok();
	            }
	        }
	        catch (Exception e)
	        {
	        }

	        return BadRequest();
	    }
    }
}