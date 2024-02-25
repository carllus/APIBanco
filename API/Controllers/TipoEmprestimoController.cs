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
    public class TipoEmprestimoController : ControllerBase
    {
        
        private readonly APIDbContext _dbContext;
        private readonly ITipoEmprestimoService _tipoEmprestimoService;
        private readonly IMapper _mapper;

        public TipoEmprestimoController(APIDbContext dbContext
            , ITipoEmprestimoService tipoEmprestimoService
            //IMapper mapper
            )
        {
            _dbContext = dbContext;
            _tipoEmprestimoService = tipoEmprestimoService;
            _mapper = AutoMapperConfig.Configure();
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<TipoEmprestimoDto>>> GetAll()
        {
            return Ok(_mapper.Map<List<TipoEmprestimoDto>>(await _tipoEmprestimoService.GetAll()));
        }

        [HttpPost("Add")]
        public async Task<ActionResult<bool>> Create(TipoEmprestimoDto tipoEmprestimoDto)
        {
            try
            {
                tipoEmprestimoDto.Id = 0;
                return Ok(await _tipoEmprestimoService.Add(_mapper.Map<TipoEmprestimo>(tipoEmprestimoDto)));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocorreu um erro ao processar a solicitação: " + ex.Message);
            }
        }
        
        [HttpGet("Get/{id}")]
        public async Task<ActionResult<TipoEmprestimoDto>> Get(int id)
        {
            var retorno = _mapper.Map<TipoEmprestimoDto>(await _tipoEmprestimoService.Get(id));
            return retorno == null? NotFound() : Ok(retorno);
        }

        [HttpPut("Update")]
        public async Task<ActionResult<bool>> Update(TipoEmprestimoDto tipoEmprestimoDto) => Ok(await _tipoEmprestimoService.Update(_mapper.Map<TipoEmprestimo>(tipoEmprestimoDto)));

        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult<bool>> Delete(int id) => Ok(await _tipoEmprestimoService.Delete(id));
        
    }
}
