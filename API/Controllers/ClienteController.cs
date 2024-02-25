using AutoMapper;
using API.AutoMapper.Dto;
using API.Business.Interfaces;
using API.Business.Model;
using API.Service.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.AutoMapper.Profiles;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    //[Authorize]
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        
        private readonly APIDbContext _dbContext;
        private readonly IClienteService _clienteService;
        private readonly IMapper _mapper;

        public ClienteController(APIDbContext dbContext
            ,IClienteService clienteService
            //IMapper mapper
            )
        {
            _dbContext = dbContext;
            _clienteService = clienteService;
            _mapper = AutoMapperConfig.Configure();
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<ClienteDto>>> GetAll()
        {
            return Ok(_mapper.Map<List<ClienteDto>>(await _clienteService.GetAll()));
        }

        [HttpPost("Add")]
        public async Task<ActionResult<bool>> Create(ClienteDto clienteDto)
        {
            try
            {
                clienteDto.Id = 0;
                return Ok(await _clienteService.Add(_mapper.Map<Cliente>(clienteDto)));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocorreu um erro ao processar a solicitação: " + ex.Message);
            }
        }
        
        [HttpGet("Get/{id}")]
        public async Task<ActionResult<ClienteDto>> Get(int id)
        {
            var retorno = _mapper.Map<ClienteDto>(await _clienteService.Get(id));
            return retorno == null? NotFound() : Ok(retorno);
        }

        [HttpPut("Update")]
        public async Task<ActionResult<bool>> Update(ClienteDto clienteDto) => Ok(await _clienteService.Update(_mapper.Map<Cliente>(clienteDto)));

        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult<bool>> Delete(int id) => Ok(await _clienteService.Delete(id));
        
    }
}
