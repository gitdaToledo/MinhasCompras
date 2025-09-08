namespace MinhasCompras.Views;
using MinhasCompras.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

public partial class ListaProduto : ContentPage
{
	ObservableCollection<Produto> lista = new ObservableCollection<Produto>();
	public ListaProduto()
	{
		InitializeComponent();

        lst_produto.ItemsSource = lista;
	}

	protected async override void OnAppearing()
	{
		try
		{
			List<Produto> tmp = await App.Db.GetAll();

			tmp.ForEach(i => lista.Add(i));
		}
		catch (Exception ex)
		{
			await DisplayAlert("ERRO", ex.Message, "OK");

		}
	}


	private void ToolbarItem_Clicked(object sender, EventArgs e)
	{
		try
		{
			Navigation.PushAsync(new Views.NovoProduto());
		}
		catch (Exception ex)
		{
			DisplayAlert("ERRO", ex.Message, "OK");
		}
	}



	private async void txt_search_TextChanged(object sender, TextChangedEventArgs e)
	{
		try
		{
			string q = e.NewTextValue;

			lista.Clear();

			List<Produto> tmp = await App.Db.Search(q);

			tmp.ForEach(i => lista.Add(i));
		}
		catch (Exception ex)
		{
			await DisplayAlert("ERRO", ex.Message, "OK");
		}
	}


	private void ToolbarItem_Clicked_1(object sender, EventArgs e)
	{
		try
		{
			double soma = lista.Sum(i => i.total);

			string msg = $"Total da Compra: {soma:C}";

			DisplayAlert("Total dos produtos", msg, "OK");
		}
		catch (Exception ex)
		{
			DisplayAlert("ERRO", ex.Message, "OK");
		}
	}


    // Evento do MenuItem (Excluir)//
    private async void MenuItem_Clicked(object sender, EventArgs e)
	{
		try
		{
			MenuItem selecionado = sender as MenuItem;

			Produto p = selecionado.BindingContext as Produto;

			bool confirm = await 
				DisplayAlert("Aten��o", $"Remover {p.descricao}?", "SIM", "N�O");

            if (confirm)
            {
              await App.Db.Delete(p.Id);
				lista.Remove(p);
            }
        }
		catch (Exception ex)
		{
			DisplayAlert("ERRO", ex.Message, "OK");
		}
	}

    private void lst_produto_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
		try
		{
			Produto p = e.SelectedItem as Produto;

			Navigation.PushAsync(new Views.EditarProduto
			    { 
				BindingContext = p,
				});

        }
        catch (Exception ex)
		{
			DisplayAlert("ERRO", ex.Message, "OK");
        }
    }
}