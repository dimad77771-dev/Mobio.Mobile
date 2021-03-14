using System.Threading.Tasks;
using Xamarin.Forms;

namespace OneBuilder.Mobile.Services
{
    public interface INavigationService
    {
		Task NavigateToAsync<TViewModel>(TViewModel viewModel);
		Task NavigateToRootAsync();
        Task NavigateBackAsync();
    }
}