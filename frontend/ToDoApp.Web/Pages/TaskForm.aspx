<%@ Page Title="Criar Tarefa" Language="C#" AutoEventWireup="true" EnableSessionState="True" CodeBehind="TaskForm.aspx.cs" MasterPageFile="~/Site.Master" Inherits="ToDoApp.Web.Pages.TaskForm" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-4">
        <div class="col-md-8 col-lg-6">
            <asp:Label ID="lblError" runat="server" CssClass="alert alert-danger d-none" role="alert"/>
            <asp:Label ID="lblSuccess" runat="server" CssClass="alert alert-success d-none" role="alert"/>

            <div class="card shadow-sm">
                <div class="card-header bg-primary text-white">
                    <h4 class="mb-0">
                        <i class="bi bi-clipboard-check"></i>
                        <asp:Literal ID="ltTitle" runat="server" Text="Nova Tarefa" />
                    </h4>
                </div>
                <div class="card-body p-4">
                    <div class="mb-3">
                        <label for="<%= txtTitle.ClientID %>" class="form-label">
                            Título
                        </label>
                        <asp:TextBox ID="txtTitle" runat="server" CssClass="form-control" placeholder="Digite o título da tarefa" MaxLength="100" />
                    </div>

                    <div class="mb-3">
                        <label for="<%= txtDescription.ClientID %>" class="form-label">
                            Descrisção
                        </label>
                        <asp:TextBox ID="txtDescription" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="4" placeholder="Digite a descrição da tarefa" MaxLength="500" />
                        <div class="form-text">Máximo 500 caracteres</div>
                    </div>

                    <div class="mb-3">
                        <label for="<%= txtCategory.ClientID %>" class="form-label">
                            Categoria <span class="text-danger">*</span>
                        </label>
                        <asp:TextBox ID="txtCategory" runat="server" CssClass="form-control" placeholder="Digite a categoria da tarefa" MaxLength="50" />
                    </div>
                </div>
                <div class="card-footer">
                    <asp:LinkButton ID="btnCancel" CssClass="btn btn-secondary" runat="server" OnClick="btnCancel_Click">
                        <i class="bi bi-x-circle"></i> Cancelar
                    </asp:LinkButton>
                    <asp:Button ID="btnSalvar" runat="server" Text="Entrar" CssClass="btn btn-primary" OnClick="btnSalvar_Click" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
