using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SOS_Natureza.Domain.Entities;
using SOS_Natureza.Infrastructure.DTOs;
using SOS_natureza.Infrastructure.Repositories;
using Swashbuckle.AspNetCore.Annotations;

namespace SOS_natureza.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepository _repository;
        private readonly IMapper _mapper;

        public UsuarioController(IUsuarioRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet("getAll")]
        [SwaggerOperation(Summary = "Listar todos os usuários", Description = "Retorna uma lista com todos os usuários cadastrados.")]
        [SwaggerResponse(200, "Lista de usuários retornada com sucesso", typeof(IEnumerable<UsuarioDTO>))]
        [SwaggerResponse(500, "Erro interno no servidor")]
        public async Task<IActionResult> GetAll()
        {
            var usuarios = await _repository.GetAllAsync();
            var usuariosDto = _mapper.Map<IEnumerable<UsuarioDTO>>(usuarios);
            return Ok(usuariosDto);
        }

        [HttpGet("getById/{id}")]
        [SwaggerOperation(Summary = "Buscar usuário por ID", Description = "Retorna um usuário específico pelo ID.")]
        [SwaggerResponse(200, "Usuário encontrado", typeof(UsuarioDTO))]
        [SwaggerResponse(404, "Usuário não encontrado")]
        [SwaggerResponse(500, "Erro interno no servidor")]
        public async Task<IActionResult> GetById(int id)
        {
            var usuario = await _repository.GetByIdAsync(id);
            if (usuario == null) return NotFound();

            var usuarioDto = _mapper.Map<UsuarioDTO>(usuario);
            return Ok(usuarioDto);
        }

        [HttpGet("buscaByName")]
        [SwaggerOperation(Summary = "Buscar usuários por nome", Description = "Retorna usuários cujo nome contenha o texto informado.")]
        [SwaggerResponse(200, "Usuários encontrados", typeof(IEnumerable<UsuarioDTO>))]
        [SwaggerResponse(500, "Erro interno no servidor")]
        public async Task<IActionResult> SearchByName([FromQuery] string name)
        {
            var usuarios = await _repository.FindByNameAsync(name);
            var usuariosDto = _mapper.Map<IEnumerable<UsuarioDTO>>(usuarios);
            return Ok(usuariosDto);
        }

        [HttpPost("create")]
        [SwaggerOperation(Summary = "Criar novo usuário", Description = "Cria um novo usuário com os dados fornecidos.")]
        [SwaggerResponse(201, "Usuário criado com sucesso", typeof(UsuarioDTO))]
        [SwaggerResponse(400, "Dados inválidos")]
        [SwaggerResponse(500, "Erro interno no servidor")]
        public async Task<IActionResult> Create([FromBody] CreateUsuarioDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    Message = "Dados inválidos fornecidos.",
                    Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)
                });
            }

            try
            {
                var usuario = new Usuario(dto.Name, dto.Email, dto.Password);
                await _repository.AddAsync(usuario);
                await _repository.SaveChangesAsync();

                var usuarioDto = _mapper.Map<UsuarioDTO>(usuario);
                return CreatedAtAction(nameof(GetById), new { id = usuario.Id }, usuarioDto);
            }
            catch (Exception ex)
            {
                return StatusCode(400, new
                {
                    Message = "Dados inválidos para a criação do usuario.",
                    Details = ex.Message 
                });
            }
        }

        [HttpPut("update/{id}")]
        [SwaggerOperation(Summary = "Atualizar usuário", Description = "Atualiza os dados de um usuário existente.")]
        [SwaggerResponse(204, "Usuário atualizado com sucesso")]
        [SwaggerResponse(400, "Dados inválidos")]
        [SwaggerResponse(404, "Usuário não encontrado")]
        [SwaggerResponse(500, "Erro interno no servidor")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateUsuarioDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var usuario = await _repository.GetByIdAsync(id);
            if (usuario == null) return NotFound();

            usuario.SetNome(dto.Name);
            usuario.SetEmail(dto.Email);
            if (!string.IsNullOrWhiteSpace(dto.Password))
                usuario.SetSenha(dto.Password);

            _repository.Update(usuario);
            await _repository.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("delete/{id}")]
        [SwaggerOperation(Summary = "Remover usuário", Description = "Remove um usuário com base no ID fornecido.")]
        [SwaggerResponse(204, "Usuário removido com sucesso")]
        [SwaggerResponse(404, "Usuário não encontrado")]
        [SwaggerResponse(500, "Erro interno no servidor")]
        public async Task<IActionResult> Delete(int id)
        {
            var usuario = await _repository.GetByIdAsync(id);
            if (usuario == null) return NotFound();

            _repository.Remove(usuario);
            await _repository.SaveChangesAsync();

            return NoContent();
        }
    }
}
