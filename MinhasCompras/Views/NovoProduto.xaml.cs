using MinhasCompras.Models;
using System.Threading.Tasks;

namespace MinhasCompras.Views;

public partial class NovoProduto : ContentPage
{
	public NovoProduto()
	{
		InitializeComponent();
	}

    private async void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        try
		{
          Produto p = new Produto
            {
				descricao = txt_descricao.Text,
				quantidade = Convert.ToDouble(txt_quantidade.Text),
				preco = Convert.ToDouble(txt_preco.Text)
			};

			await App.Db.Insert(p);
			await DisplayAlert("Sucesso!", "Registro Inserido", "OK");

		} catch (Exception ex)
        {
            await DisplayAlert("ERRO", ex.Message, "OK");
		}
    }
}