using LojaAPI.Loja.Domain.Entities;
using LojaAPI.Loja.Domain.Interfaces;

namespace LojaAPI.Loja.Application.Services
{
           //aqui e uma injencao de dependencia, no caso ele nao cria o repositorio mas sim utiliza (Preciso estudar e rever sobre)
        public class ProdutoService
        {
            private readonly IProdutoRepository _produtoRepository;

            //construtor para receber do ProdutoService e guardar no _produtoRepository
            public ProdutoService(IProdutoRepository produtoRepository)
            {
                _produtoRepository = produtoRepository;
            }

            //aqui to pedindo para listar os produtos e retornar os produtos listado
            public async Task<IEnumerable<Produto>> ListarProdutosAsync()
            {
                return await _produtoRepository.GetAllAsync();
            }

            //aqui o sistema vai buscar o produto pelo ID, caso o produto nao for encontrado vai retornar nulo
            public async Task<Produto?> BuscarPorId(int id)
            {
                return await _produtoRepository.GetByIdAsync(id);
            }

            //Aqui to falando para adicionar um produto, mas se o preco do produto for menor ou igual a 0, 
            //vai retornar uma excecao
            //caso o preco for maior que 0, vai adicionar o produto
            public async Task AdicionarProduto(Produto produto)
            {
                if (produto.Preco <= 0)
                {
                    throw new ArgumentException("O preço do produto deve ser maior que zero");
                }

                await _produtoRepository.AddAsync(produto);
            }

            //aqui vai atualizar o o nome preco e quantidade do produto existente, buscando pelo ID, 
            //caso o produto for igual a null, vai retornar produto n encontrado
            public async Task AtualizarProduto(Produto produto)
            {
                var existente = await _produtoRepository.GetByIdAsync(produto.Id);
                if (existente == null)
                {
                    throw new Exception("Produto não encontrado");

                    existente.Nome = produto.Nome;
                    existente.Preco = produto.Preco;
                    existente.Quantidade = produto.Quantidade;
                }

                await _produtoRepository.UpdateAsync(existente);
            }

            // Deleta um produto pelo ID, mas apenas se ele existir.
            // Lança exceção se o produto não for encontrado.
            public async Task DeletarProduto(int id)
            {
                var produto = await _produtoRepository.GetByIdAsync(id);
                if (produto == null)
                {
                    throw new Exception("Produto não encontrado");
                }

                await _produtoRepository.DeleteAsync(id);
            }
        }


    }
