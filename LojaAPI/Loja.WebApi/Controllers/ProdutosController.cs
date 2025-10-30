using LojaAPI.Loja.Domain.Entities;
using LojaAPI.Loja.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LojaAPI.loja.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutosController : ControllerBase
    {
        private readonly IProdutoRepository _produtoRepository;

        public ProdutosController(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }


        //Retorna todos os produtos cadastrados
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Produto>>> GetProdutos()
        {
            var produtos = await _produtoRepository.GetAllAsync();
            return Ok(produtos);
        }

        //Retorna um produto específico pelo ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Produto>> GetProduto(int id)
        {
            var produto = await _produtoRepository.GetByIdAsync(id);

            if (produto == null)
            {
                return NotFound($"Produto com ID {id} não encontrado.");
            }

            return Ok(produto);
        }

        //Cadastra um novo produto
        [HttpPost]
        public async Task<ActionResult<Produto>> PostProduto(Produto produto)
        {
            if (produto.Preco <= 0)
            {
                return BadRequest("O preço do produto deve ser maior que zero.");
            }

            await _produtoRepository.AddAsync(produto);

            return CreatedAtAction(
                nameof(GetProduto),
                new { id = produto.Id },
                produto
            );
        }


        //Atualiza um produto existente
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduto(int id, Produto produto)
        {
            if (id != produto.Id)
            {
                return BadRequest("O ID do produto não corresponde ao ID da URL.");
            }

            try
            {
                await _produtoRepository.UpdateAsync(produto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound($"Produto com ID {id} não encontrado. Erro: {ex.Message}");
            }
        }

        // Remove um produto pelo ID
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduto(int id)
        {
            var produto = await _produtoRepository.GetByIdAsync(id);
            if (produto == null)
            {
                return NotFound($"Produto com ID {id} não encontrado.");
            }

            await _produtoRepository.DeleteAsync(id);

            return NoContent();
        }
    }
}