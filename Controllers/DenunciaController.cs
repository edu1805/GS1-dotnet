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
    public class DenunciaController : ControllerBase
    {
        private readonly IDenunciaRepository _repository;
        private readonly IMapper _mapper;

        public DenunciaController(IDenunciaRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet("getAll")]
        [SwaggerOperation(Summary = "Listar todas as denúncias", Description = "Retorna todas as denúncias cadastradas.")]
        [SwaggerResponse(200, "Lista de denúncias retornada com sucesso", typeof(IEnumerable<DenunciaDTO>))]
        [SwaggerResponse(500, "Erro interno no servidor")]
        public async Task<IActionResult> GetAll()
        {
            var denuncias = await _repository.GetAllAsync();
            var dto = _mapper.Map<IEnumerable<DenunciaDTO>>(denuncias);
            return Ok(dto);
        }

        [HttpGet("getById/{id}")]
        [SwaggerOperation(Summary = "Buscar denúncia por ID", Description = "Retorna uma denúncia específica pelo ID.")]
        [SwaggerResponse(200, "Denúncia encontrada", typeof(DenunciaDTO))]
        [SwaggerResponse(404, "Denúncia não encontrada")]
        [SwaggerResponse(500, "Erro interno no servidor")]
        public async Task<IActionResult> GetById(int id)
        {
            var denuncia = await _repository.GetByIdAsync(id);
            if (denuncia == null) return NotFound();

            var dto = _mapper.Map<DenunciaDTO>(denuncia);
            return Ok(dto);
        }

        [HttpGet("usuario/{usuarioId}")]
        [SwaggerOperation(Summary = "Buscar denúncias por usuário", Description = "Retorna todas as denúncias feitas por um usuário específico.")]
        [SwaggerResponse(200, "Denúncias encontradas", typeof(IEnumerable<DenunciaDTO>))]
        [SwaggerResponse(404, "Nenhuma denúncia encontrada para o usuário informado")]
        [SwaggerResponse(500, "Erro interno no servidor")]
        public async Task<IActionResult> GetByUsuarioId(int usuarioId)
        {
            var denuncias = await _repository.GetByUsuarioIdAsync(usuarioId);

            if (denuncias == null || !denuncias.Any())
                return NotFound("Nenhuma denúncia encontrada para o usuário informado.");

            var denunciasDto = _mapper.Map<IEnumerable<DenunciaDTO>>(denuncias);
            return Ok(denunciasDto);
        }

        [HttpPost("create")]
        [SwaggerOperation(Summary = "Criar uma nova denúncia", Description = "Cria uma nova denúncia com os dados informados.")]
        [SwaggerResponse(201, "Denúncia criada com sucesso", typeof(DenunciaDTO))]
        [SwaggerResponse(400, "Dados inválidos")]
        [SwaggerResponse(500, "Erro interno no servidor")]
        public async Task<IActionResult> Create([FromBody] CreateDenunciaDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var denuncia = new Denuncia(dto.UsuarioId, dto.Categoria, dto.Descricao);
            await _repository.AddAsync(denuncia);
            await _repository.SaveChangesAsync();

            var result = _mapper.Map<DenunciaDTO>(denuncia);
            return CreatedAtAction(nameof(GetById), new { id = denuncia.Id }, result);
        }

        [HttpPut("update/{id}")]
        [SwaggerOperation(Summary = "Atualizar denúncia", Description = "Atualiza os dados de uma denúncia existente.")]
        [SwaggerResponse(204, "Denúncia atualizada com sucesso")]
        [SwaggerResponse(400, "Dados inválidos")]
        [SwaggerResponse(404, "Denúncia não encontrada")]
        [SwaggerResponse(500, "Erro interno no servidor")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateDenunciaDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var denuncia = await _repository.GetByIdAsync(id);
            if (denuncia == null) return NotFound();

            if (!string.IsNullOrWhiteSpace(dto.Categoria))
                denuncia.AtualizarCategoria(dto.Categoria);

            if (!string.IsNullOrWhiteSpace(dto.Descricao))
                denuncia.AtualizarDescricao(dto.Descricao);

            _repository.Update(denuncia);
            await _repository.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("delete/{id}")]
        [SwaggerOperation(Summary = "Excluir denúncia", Description = "Remove uma denúncia do sistema com base no ID fornecido.")]
        [SwaggerResponse(204, "Denúncia removida com sucesso")]
        [SwaggerResponse(404, "Denúncia não encontrada")]
        [SwaggerResponse(500, "Erro interno no servidor")]
        public async Task<IActionResult> Delete(int id)
        {
            var denuncia = await _repository.GetByIdAsync(id);
            if (denuncia == null) return NotFound();

            _repository.Remove(denuncia);
            await _repository.SaveChangesAsync();

            return NoContent();
        }
    }
}
