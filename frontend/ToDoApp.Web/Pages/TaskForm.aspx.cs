using System;
using System.Web.UI;
using ToDoApp.Web.Helpers;
using ToDoApp.Web.Models.Tasks;

namespace ToDoApp.Web.Pages
{
    public partial class TaskForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!AuthSession.IsAuthenticated())
            {
                Response.Redirect("~/");
                return;
            }

            if (!IsPostBack)
            {
                var id = Request.QueryString["id"];

                if (!string.IsNullOrEmpty(id))
                {
                    ltTitle.Text = "Editar Tarefar";
                    LoadTask(Guid.Parse(id));
                }
                else
                {
                    ltTitle.Text = "Criar Tarefa";
                }
            }
        }

        private void LoadTask(Guid id)
        {
            var taskService = new TaskService();

            var task = taskService.GetTasksByIdAsync(id).Result;

            if(task == null)
            {
                ShowError("Tarefa não encontrada");
                return;
            }

            txtTitle.Text = task.Title;
            txtDescription.Text = task.Description;
            txtCategory.Text = task.Category;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/Tasks.aspx");
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            var taskService = new TaskService();

            if (!Page.IsValid)
                return;

            try
            {
                var id = Request.QueryString["id"];

                if (string.IsNullOrEmpty(id))
                {
                    var created = new TaskRequest()
                    {
                        Title = txtTitle.Text.Trim(),
                        Category = txtCategory.Text.Trim(),
                        Description = txtDescription.Text.Trim()
                    };
                    taskService.CreateTaskAsync(created).ConfigureAwait(false);

                    ShowSuccess("Tarefa criada com sucesso");
                }
                else
                {
                    var updated = new TaskRequest()
                    {
                        Title = txtTitle.Text.Trim(),
                        Category = txtCategory.Text.Trim(),
                        Description = txtDescription.Text.Trim()
                    };
                    taskService.UpdateTaskAsync(Guid.Parse(id), updated).ConfigureAwait(false);

                    ShowSuccess("Tarefa atualizada com sucesso");
                }

                Response.Redirect("~/Pages/Tasks.aspx");
            }
            catch (Exception ex)
            {
                ShowError($"Erro interno da API: {ex.Message}");
            }
            
        }

        private void ShowSuccess(string message)
        {
            lblSuccess.Text = message;
            lblSuccess.CssClass = "alert alert-success";
        }

        private void ShowError(string message)
        {
            lblError.Text = message;
            lblError.CssClass = "alert alert-danger";
        }
    }
}