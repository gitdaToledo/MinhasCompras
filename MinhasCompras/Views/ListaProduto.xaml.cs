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
		List<Produto> tmp = await App.Db.GetAll();

		tmp.ForEach(i => lista.Add(i));
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
		string q = e.NewTextValue;

		lista.Clear();

        List<Produto> tmp = await App.Db.Search(q);

		tmp.ForEach(i => lista.Add(i));
    }

    private void ToolbarItem_Clicked_1(object sender, EventArgs e)
    {
		double soma = lista.Sum(i => i.total);

		string msg = $"Total da Compra: {soma:C}";

		DisplayAlert("Total dos produtos", msg, "OK");
    }

    private void MenuItem_Clicked(object sender, EventArgs e)
    {

    }
}