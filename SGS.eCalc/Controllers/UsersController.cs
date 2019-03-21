using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SGS.eCalc.DTO;
using SGS.eCalc.Repository;

namespace SGS.eCalc.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IDatingRepository _datingRepository;
        private readonly IMapper _mapper;
        public UsersController(IDatingRepository datingRepository, IMapper mapper)
        {
            _mapper = mapper;
            _datingRepository = datingRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            // Does not affect performance using async 
            return Ok(_mapper.Map<IEnumerable<UserForListDTO>>(await _datingRepository.GetUsers()));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            return Ok( _mapper.Map<UserForDetailedDto>(await _datingRepository.GetUser(id)));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, UserForUpdateDto userForUpdateDto)
        {
            if(id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();
            
            var userFromRepo = await _datingRepository.GetUser(id);
            _mapper.Map(userForUpdateDto, userFromRepo);
            if(await _datingRepository.SaveAll())
                return NoContent();
            throw new Exception($"Updating user {id} failed on save");

        }

        // POST api/values
        // [HttpPost]
        // public void Post([FromBody] string value)
        // {
        //     await  _datingRepository.(id)
        //     Return Ok();
        // }

        // // PUT api/values/5
        // [HttpPut("{id}")]
        // public void Put(int id, [FromBody] string value)
        // {
        // }
    }
}