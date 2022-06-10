using AntesQueVenca.Domain.Entities;
using AntesQueVenca.Domain.Exceptions;
using AntesQueVenca.Mobile.Services.Services;
using AntesQueVenca.Views;
using System.Windows.Input;
using Xamarin.Forms;

namespace AntesQueVenca.ViewModels
{
    public class UserViewModel : ViewModelBase<User>
    {
        private UserServices userServices;
        public string PasswordConfirmation { get; private set; }

        public UserViewModel()
        {
            Entity.Person = new PhysicalPerson();
        }

        public ICommand RegisterCommand 
        {
            get 
            {
                return new Command(async () => 
                {
                    try
                    {
                        var loggedUser = await new UserServices().Add(Entity);
                        if (loggedUser != null)
                            await Navigation.PushAsync(new MainView());
                        else
                            throw new ValidationException("Erro ao cadastrar usuário!");
                    }
                    catch (ValidationException vex)
                    {
                        await Message.DisplayAlert("Aviso", vex.Message, "OK");
                    }
                    catch (System.Exception ex)
                    {
                        await Message.DisplayAlert("Aviso", ex.Message, "OK");
                    }
                });
            }
        }
    }
}
