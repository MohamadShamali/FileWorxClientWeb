using Microsoft.AspNetCore.Components;
using Web_Fileworx_Client.Models;

namespace Web_Fileworx_Client.Pages.MainPages
{
    public partial class LogIn
    {
        private string? userName;
        private string? password;
        private bool showInvalidInfo = false;
        User? loggedInUser;

        protected override void OnInitialized()
        {
            userServices.RefreshUsers();
            base.OnInitialized();
        }

        private void checkLogInRequest()
        {
            User? tryToLogInUser = userServices.CheckLogInInfo(userName, password);

            if (tryToLogInUser != null)
            {
                loggedInUser = tryToLogInUser;
                userServices.loggedInUser = tryToLogInUser;
                navManager.NavigateTo("./f");
            }

            else
            {
                clearBoxes();
                showInvalidInfo = true;
            }
        }

        private void clearBoxes()
        {
            password = string.Empty;
            showInvalidInfo = false;
        }
    }
}
