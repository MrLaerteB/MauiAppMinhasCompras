using MauiAppMinhasCompras.Models;

namespace MauiAppMinhasCompras.Views;

public partial class EditarProduto : ContentPage
{
	public EditarProduto()
	{
		InitializeComponent();
	}

    private async void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        try
        {
            Produto produto_anexado = BindingContext as Produto;

            Produto p = new Produto
            {
                Id = produto_anexado.Id,
                Descricao = txt_descricao.Text, // Captura a descrição
                Quantidade = Convert.ToDouble(txt_quantidade.Text), // Captura e converte a quantidade
                Preco = Convert.ToDouble(txt_preco.Text) // Captura e converte o preço
            };

            await App.Db.Update(p); // ATUALIZA O PRODUTO NO BD
            await DisplayAlert("Sucesso!", "Registro atualizado", "OK"); // Alerta de sucesso
            await Navigation.PopAsync();
        }
        catch (Exception ex)
        {
            await DisplayAlert("Ops", ex.Message, "OK"); // Se não for sucesso, exibe mensagem de erro.
        }
    }
}