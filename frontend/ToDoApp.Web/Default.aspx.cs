using System;
using System.Web.UI;
using ToDoApp.Web.Helpers;
using ToDoApp.Web.Models.Auth;
using ToDoApp.Web.Models.Register;
using ToDoApp.Web.Services;

namespace ToDoApp.Web
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                var authService = new AuthService();

                var request = new LoginRequest()
                {
                    UserName = txtUser.Text,
                    Password = txtPassword.Text
                };

                var token = authService.LoginAsync(request).Result;

                if (!string.IsNullOrEmpty(token))
                {
                    AuthSession.SetToken(token);

                    Response.Redirect("~/Pages/Tasks.aspx");
                }
                else
                {
                    lblError.Text = "Usuario ou senha invalidos";
                    lblError.Visible = true;
                }
            }
            catch(Exception)
            {
                ShowError("Erro interno da API");
            }
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                var authService = new AuthService();

                var request = new RegisterRequest()
                {
                    UserName = txtUserName.Text,
                    Password = txtUserPassword.Text
                };

                var isSuccess = authService.CreateAsync(request).Result;

                if(isSuccess)
                    Response.Redirect("~/");
                else
                {
                    lblError.Text = "Não foi possivel criar o usuario";
                    lblError.Visible = true;
                }
            }
            catch
            {
                lblError.Text = "Houve um problema na hora de criar";
                lblError.Visible = true;
            }
        }

        private void ShowError(string message)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "error", $"alert('{message}');", true);
        }
    }
}