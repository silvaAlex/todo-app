using System;
using System.Collections.Generic;
using System.Web.UI;
using ToDoApp.Web.Helpers;
using ToDoApp.Web.Models.Tasks;

namespace ToDoApp.Web.Pages
{
    public partial class Tasks : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!AuthSession.IsAuthenticated())
            {
                Response.Redirect("~/");
                return;
            }
            if(!IsPostBack)
                LoadTasks();
        }

        private void LoadTasks()
        {
            try
            {
                var service = new TaskService();

                var tasks = service.GetAllTasksAsync().Result;

                gvTasks.DataSource = tasks;
                gvTasks.DataBind();
                rptTasks.DataSource = tasks;
                rptTasks.DataBind();
            }
            catch(Exception ex)
            {
                ShowError($"Erro ao carregar tarefas: {ex.Message}");
            }
        }

        protected void btnViewTable_Click(object sender, EventArgs e)
        {
            pnlTable.Visible = true;
            pnlCards.Visible = false;

            btnViewTable.CssClass = "btn btn-primary";
            btnViewCards.CssClass = "btn btn-outline-primary";
        }

        protected void btnViewCards_Click(object sender, EventArgs e)
        {
            pnlTable.Visible = false;
            pnlCards.Visible = true;

            btnViewTable.CssClass = "btn btn-outline-primary";
            btnViewCards.CssClass = "btn btn-primary";
        }

        protected void btnNewTask_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/TaskForm.aspx");
        }

        protected void gvTasks_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            var taskId = Guid.Parse(e.CommandArgument.ToString());
            var taskService = new TaskService();

            bool result = false;

            if (e.CommandName == "CompleteTask")
            {
                result = taskService.CompleteTaskAsync(taskId).Result;
            }
            else if (e.CommandName == "DeleteTask")
            {
                result = taskService.DeleteTaskAsync(taskId).Result;
            }

            if (result)
                LoadTasks();
            else
            {
                ShowError("Erro ao processar a tarefa");
            }
        }

        protected void rptTasks_ItemCommand(object source, System.Web.UI.WebControls.RepeaterCommandEventArgs e)
        {
            var taskId = Guid.Parse(e.CommandArgument.ToString());
            var taskService = new TaskService();

            bool result = false;

            if (e.CommandName == "CompleteTask")
            {
                result = taskService.CompleteTaskAsync(taskId).Result;
            }
            else if (e.CommandName == "DeleteTask")
            {
                result = taskService.DeleteTaskAsync(taskId).Result;
            }

            if (result)
                LoadTasks();
            else
            {
                ShowError("Erro ao processar a tarefa");
            }
        }

        private void ShowError(string message)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "error", $"alert('{message}');", true);
        }

        protected void gvTasks_RowDeleting(object sender, System.Web.UI.WebControls.GridViewDeleteEventArgs e)
        {
            e.Cancel = true;
        }

        protected void gvTasks_RowEditing(object sender, System.Web.UI.WebControls.GridViewEditEventArgs e)
        {
            e.Cancel = true;
        }
    }
}