using DemoApp.Core.Data.Enity;
using DemoApp.Core.ViewModel;
using DemoApp.Core.ViewModel.ViewPage;

namespace DemoApp.Core.Services
{
    public interface IAccountService
    {
        BaseViewModel<TokenViewModel> Register(RegisterViewModel user);
        BaseViewModel<TokenViewModel> Login(LoginViewModel user);
        BaseViewModel<AccountViewPage> getInfo();
    }
}
