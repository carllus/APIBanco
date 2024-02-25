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
    public class EmprestimoController : ControllerBase
    {

        private readonly APIDbContext _dbContext;
        private readonly IEmprestimoService _emprestimoService;
        private readonly IClienteService _clienteService;
        private readonly ITipoEmprestimoService _tipoEmprestimoService;
        private readonly IMapper _mapper;

        public EmprestimoController(APIDbContext dbContext
            ,IEmprestimoService emprestimoService
            ,IClienteService clienteService
            ,ITipoEmprestimoService tipoEmprestimoService
            )
        {
            _dbContext = dbContext;
            _emprestimoService = emprestimoService;
            _tipoEmprestimoService = tipoEmprestimoService;
            _clienteService = clienteService;
            _mapper = AutoMapperConfig.Configure();
        }


        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<EmprestimoDto>>> GetAll()
        {
            return Ok(_mapper.Map<List<EmprestimoDto>>(await _emprestimoService.GetAll()));
        }

        [HttpPost("Add")]
        public async Task<ActionResult<Result>> Create(EmprestimoDto emprestimoDto)
        {
            try
            {
                emprestimoDto.Id = 0;
                return Ok(await _emprestimoService.Add(_mapper.Map<Emprestimo>(emprestimoDto)));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocorreu um erro ao processar a solicitação: " + ex.InnerException.Message);
            }
        }

        [HttpGet("Get/{id}")]
        public async Task<ActionResult<EmprestimoDto>> Get(int id)
        {
            var retorno = _mapper.Map<EmprestimoDto>(await _emprestimoService.Get(id));
            return retorno == null ? NotFound() : Ok(retorno);
        }

        [HttpPut("Update")]
        public async Task<ActionResult<bool>> Update(EmprestimoDto emprestimoDto) => Ok(await _emprestimoService.Update(_mapper.Map<Emprestimo>(emprestimoDto)));

        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult<bool>> Delete(int id) => Ok(await _emprestimoService.Delete(id));

    }

}
