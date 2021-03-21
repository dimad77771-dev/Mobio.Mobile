using System.Threading.Tasks;
using Xamarin.Forms;

namespace OneBuilder.Mobile.Services
{
    public interface INavigationService
    {
		Task NavigateToAsync<TViewModel>(TViewModel viewModel, bool? animated = null);
		Task NavigateToRootAsync(bool? animated = null);
        Task NavigateBackAsync(bool? animated = null);
    }
}