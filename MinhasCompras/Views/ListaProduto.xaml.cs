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
			lista.Clear();

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

            lst_produto.IsRefreshing = true;

            lista.Clear();

			List<Produto> tmp = await App.Db.Search(q);

			tmp.ForEach(i => lista.Add(i));
		}
		catch (Exception ex)
		{
			await DisplayAlert("ERRO", ex.Message, "OK");
		} finally
		{
			lst_produto.IsRefreshing = false;
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
				DisplayAlert("Atenção", $"Remover {p.descricao}?", "SIM", "NÃO");

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

    // Evento do ItemSelected para editar o produto //
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

    private async void lst_produto_Refreshing(object sender, EventArgs e)
    {
        try
        {
            lista.Clear();

            List<Produto> tmp = await App.Db.GetAll();

            tmp.ForEach(i => lista.Add(i));
        }
        catch (Exception ex)
        {
            await DisplayAlert("ERRO", ex.Message, "OK");

        }finally
		{ lst_produto.IsRefreshing = false; }
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        DateTime inicio = dtInicio.Date;
        DateTime fim = dtFim.Date;

        var filtrados = lista
            .Where(p => p.datacompra.Date >= inicio && p.datacompra.Date <= fim)
            .ToList();

        lst_produto.ItemsSource = filtrados;
    }
}