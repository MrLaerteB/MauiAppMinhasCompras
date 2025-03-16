using MauiAppMinhasCompras.Models; // Importa a classe de produto
using System.Collections.ObjectModel; // Importa a ObservableCollection

namespace MauiAppMinhasCompras.Views // Namespace da página
{
    public partial class NovoProduto : ContentPage // Define a classe NovoProduto (tela de cadastro)
    {
        // ObservableCollection para armazenar os produtos
        public ObservableCollection<Produto> Produtos { get; set; }

        public NovoProduto()
        {
            InitializeComponent(); // Inicialização dos componentes
            Produtos = new ObservableCollection<Produto>(); // Inicializa a coleção de produtos
            produtoListView.ItemsSource = Produtos; // Atribui a lista ao ListView
        }

        private async void ToolbarItem_Clicked(object sender, EventArgs e) // Método assíncrono quando o usuário clicar
        {
            try
            {
                Produto p = new Produto
                {
                    Descricao = txt_descricao.Text, // Captura a descrição
                    Quantidade = Convert.ToDouble(txt_quantidade.Text), // Captura e converte a quantidade
                    Preco = Convert.ToDouble(txt_preco.Text) // Captura e converte o preço
                };

                await App.Db.Insert(p); // Insere o produto no BD
                Produtos.Add(p); // Adiciona o produto à ObservableCollection para atualização da UI
                await DisplayAlert("Sucesso!", "Registro Inserido", "OK"); // Alerta de sucesso
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ops", ex.Message, "OK"); // Se não for sucesso, exibe mensagem de erro.
            }
        }

        // Método para filtrar os produtos conforme o texto da SearchBar
        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            var searchText = e.NewTextValue?.ToLower(); // Obtém o texto digitado na SearchBar

            // Filtra os produtos que contêm o texto da busca na descrição
            var filteredProducts = Produtos.Where(p => p.Descricao.ToLower().Contains(searchText)).ToList();

            // Atualiza a lista para mostrar os produtos filtrados
            produtoListView.ItemsSource = new ObservableCollection<Produto>(filteredProducts);
        }
    }
}
