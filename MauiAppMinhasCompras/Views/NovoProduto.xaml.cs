using MauiAppMinhasCompras.Models; // Importa a classe de produto
using System.Collections.ObjectModel; // Importa a ObservableCollection

namespace MauiAppMinhasCompras.Views // Namespace da p�gina
{
    public partial class NovoProduto : ContentPage // Define a classe NovoProduto (tela de cadastro)
    {
        // ObservableCollection para armazenar os produtos
        public ObservableCollection<Produto> Produtos { get; set; }

        public NovoProduto()
        {
            InitializeComponent(); // Inicializa��o dos componentes
            Produtos = new ObservableCollection<Produto>(); // Inicializa a cole��o de produtos
            produtoListView.ItemsSource = Produtos; // Atribui a lista ao ListView
        }

        private async void ToolbarItem_Clicked(object sender, EventArgs e) // M�todo ass�ncrono quando o usu�rio clicar
        {
            try
            {
                Produto p = new Produto
                {
                    Descricao = txt_descricao.Text, // Captura a descri��o
                    Quantidade = Convert.ToDouble(txt_quantidade.Text), // Captura e converte a quantidade
                    Preco = Convert.ToDouble(txt_preco.Text) // Captura e converte o pre�o
                };

                await App.Db.Insert(p); // Insere o produto no BD
                Produtos.Add(p); // Adiciona o produto � ObservableCollection para atualiza��o da UI
                await DisplayAlert("Sucesso!", "Registro Inserido", "OK"); // Alerta de sucesso
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ops", ex.Message, "OK"); // Se n�o for sucesso, exibe mensagem de erro.
            }
        }

        // M�todo para filtrar os produtos conforme o texto da SearchBar
        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            var searchText = e.NewTextValue?.ToLower(); // Obt�m o texto digitado na SearchBar

            // Filtra os produtos que cont�m o texto da busca na descri��o
            var filteredProducts = Produtos.Where(p => p.Descricao.ToLower().Contains(searchText)).ToList();

            // Atualiza a lista para mostrar os produtos filtrados
            produtoListView.ItemsSource = new ObservableCollection<Produto>(filteredProducts);
        }
    }
}
