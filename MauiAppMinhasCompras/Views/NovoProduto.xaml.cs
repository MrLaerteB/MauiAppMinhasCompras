using MauiAppMinhasCompras.Models; //Importa a classe de produto

namespace MauiAppMinhasCompras.Views; //Namespace da página

public partial class NovoProduto : ContentPage // Define a classe NovoProduto (tela de cadastro)
{
	public NovoProduto()
	{
		InitializeComponent(); //Inicialização dos componentes
	}

    private async void ToolbarItem_Clicked(object sender, EventArgs e) //Método assíncrono quando o usuário clicar
    {
		try
		{
			Produto p = new Produto
			{
				Descricao = txt_descricao.Text, //Captura a descrição
				Quantidade = Convert.ToDouble(txt_quantidade.Text), //Captura e converte a quantidade
				Preco = Convert.ToDouble(txt_preco.Text) //Captura e converte o preço
			};

			await App.Db.Insert(p); //Insere o produto no BD
			await DisplayAlert("Sucesso!", "Registro Inserido", "OK"); //Alerta de sucesso

		}
		catch (Exception ex)
		{

			DisplayAlert("Ops", ex.Message, "OK"); //Se não for sucesso, exibe mensagem de erro.
		}
    }
}