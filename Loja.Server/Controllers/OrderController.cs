using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Loja.Domain.Entities;
using Loja.Domain.Interfaces.Services;
using Loja.Server.Dtos;

namespace Loja.Server.Controllers
{
    ///<summary>Controlador referente à entidade Order(Pedido)</summary>
    [ApiController]
    [Route("[controller]")]
    [Route("Pedidos")]
    public class OrderController : ControllerBase
    {
        private readonly ILogger<OrderController> _logger;
        private readonly IMapper _mapper;
        private readonly IOrderService _orderService;

        public OrderController(
            ILogger<OrderController> logger,
            IMapper mapper,
            IOrderService orderService)
        {
            _logger = logger;
            _mapper = mapper;
            _orderService = orderService;
        }

        ///<summary>Retorna a lista de todos os pedidos</summary>
        ///<remarks>
        /// 
        ///     GET /order OU /pedidos
        ///     
        ///</remarks>
        ///<returns>Lista com todos os pedidos</returns>
        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<OrderDto>>> Get()
        {
            var result = await _orderService.Get();

            var dtos = _mapper.Map<IEnumerable<OrderDto>>(result);

            return Ok(dtos);
        }

        ///<summary>Retorna os detalhes de um pedido específico, incluindo os itens do pedido</summary>
        ///<remarks>
        /// 
        ///     GET /order/{id} OU /pedidos/{id}
        ///     
        ///</remarks>
        ///<param name="id">Id do pedido</param>
        ///<returns>Dados do pedido</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<OrderDto>> Get(int id)
        {
            var result = await _orderService.Get(id);

            var dto = _mapper.Map<OrderDto>(result);

            return Ok(dto);
        }

        ///<summary>Adiciona um novo pedido</summary>
        ///<remarks>
        /// 
        ///     POST /order OU /pedidos
        ///     
        ///</remarks>
        ///<param name="dto">Dados do novo pedido</param>
        ///<returns>Id do pedido criado</returns>
        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<object>> Post([FromBody] OrderDto dto)
        {
            var model = _mapper.Map<Order>(dto);

            var result = await _orderService.Add(model);

            return Ok(new { result.Id });
        }

        ///<summary>Atualiza um pedido existente</summary>
        ///<remarks>
        /// 
        ///     PUT /order OU /pedidos
        ///     
        ///</remarks>
        ///<param name="id">Id do pedido para atualizar</param>
        ///<param name="dto">Dados para atualização do pedido</param>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> Put(int id, [FromBody] OrderDto dto)
        {
            dto.Id = id;
            var model = _mapper.Map<Order>(dto);

            await _orderService.Edit(model);

            return NoContent();
        }

        ///<summary>Remove um pedido</summary>
        ///<remarks>
        /// 
        ///     DELETE /order OU /pedidos
        ///     
        ///</remarks>
        ///<param name="dto">Id do pedido a ser excluído</param>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> Delete(int id)
        {
            await _orderService.Delete(id);

            return NoContent();
        }
    }
}
