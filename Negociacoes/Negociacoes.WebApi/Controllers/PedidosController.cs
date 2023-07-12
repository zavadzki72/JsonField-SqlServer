using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Negociacoes.WebApi.Enumeradores;
using Negociacoes.WebApi.Infra;
using Negociacoes.WebApi.Models.Dtos;
using Negociacoes.WebApi.Models.Entities;

namespace Negociacoes.WebApi.Controllers
{
    [ApiController]
    public class PedidosController : ControllerBase
    {
        private readonly ApplicationContext _applicationContext;

        public PedidosController(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        [HttpGet("orders/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var data = await _applicationContext.Set<Pedido>().FirstOrDefaultAsync(x => x.Id == id);

            return Ok(data);
        }

        [HttpGet("orders")]
        public async Task<IActionResult> GetAll()
        {
            var data = await _applicationContext.Set<Pedido>().ToListAsync();

            return Ok(data);
        }

        [HttpPost("orders")]
        public async Task<IActionResult> Post([FromBody] RegisterPedidoDto registerPedidoDto)
        {
            if(string.IsNullOrWhiteSpace(registerPedidoDto.Item) || registerPedidoDto.Quantidade < 1)
            {
                return BadRequest("Dados invalidos");
            }

            var newPedido = new Pedido
            {
                DataEntrega = registerPedidoDto.DataEntrega,
                Item = registerPedidoDto.Item,
                Quantidade = registerPedidoDto.Quantidade,
                Status = StatusPedido.CRIADO
            };

            var pedidoBase = await _applicationContext.AddAsync(newPedido);
            await _applicationContext.SaveChangesAsync();

            return Ok(pedidoBase.Entity.Id);
        }

        [HttpPatch("orders/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] RegisterPedidoDto registerPedidoDto)
        {
            if(string.IsNullOrWhiteSpace(registerPedidoDto.Item) || registerPedidoDto.Quantidade < 1)
            {
                return BadRequest("Dados invalidos");
            }

            var data = await _applicationContext.Set<Pedido>().FirstOrDefaultAsync(x => x.Id == id);

            if(data == null)
            {
                return BadRequest($"Pedido com o Id: {id} nao encontrado na base");
            }

            data.DataEntrega = registerPedidoDto.DataEntrega;
            data.Item = registerPedidoDto.Item;
            data.Quantidade = registerPedidoDto.Quantidade;

            _applicationContext.Update(data);
            await _applicationContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
