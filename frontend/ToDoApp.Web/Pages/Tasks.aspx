<%@ Page Title="Minhas Tarefas" Language="C#" AutoEventWireup="true" EnableSessionState="True"  MasterPageFile="~/Site.Master" CodeBehind="Tasks.aspx.cs" Inherits="ToDoApp.Web.Pages.Tasks" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h4> Minhas Tarefas</h4>
    <div class="d-flex justify-content-between align-items-center mb-3">
        <div class="btn-group" role="group">
            <asp:LinkButton ID="btnViewTable" runat="server" CssClass="btn btn-primary" OnClick="btnViewTable_Click">
                <span class="bi bi-table"></span>
            </asp:LinkButton>
            <asp:LinkButton ID="btnViewCards" runat="server" CssClass="btn btn-outline-primary" OnClick="btnViewCards_Click">
                <span class="bi bi-grid-3x3-gap"></span>
            </asp:LinkButton>
        </div>
        <asp:LinkButton ID="btnNewTask" runat="server" CssClass="btn btn-primary" OnClick="btnNewTask_Click">
            <span class="bi bi-plus-circle"></span>Nova Tarefa
        </asp:LinkButton>
    </div>

    <asp:Panel ID="pnlTable" runat="server" Visible="true">
        <div class="table-responsive">
            <asp:GridView ID="gvTasks" runat="server" 
                CssClass="table table-striped table-hover"
                AutoGenerateColumns="false"
                OnRowCommand="gvTasks_RowCommand"
                DataKeyNames="id"
                >
                <Columns>
                    <asp:BoundField DataField="title" HeaderText="Título" />
                    <asp:BoundField DataField="description" HeaderText="Descrição" />
                    <asp:BoundField DataField="category" HeaderText="Categoria" />
                    <asp:TemplateField HeaderText="Status">
                        <ItemTemplate>
                            <span class='badge <%# (bool)Eval("isCompleted") ? "bg-success" : "bg-warning text-dark" %>'>
                                <%# (bool)Eval("isCompleted") ? "Finalizada" : "Pendente" %>
                            </span>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Ações">
                        <ItemTemplate>
                            <div class="btn-group btn-group-sm" role="group">
                                <asp:LinkButton ID="btnComplete" runat="server"
                                    CommandName="CompleteTask"
                                    CommandArgument='<%# Eval("id")%>'
                                    CssClass="btn btn-success"
                                    Visible='<%# !(bool)Eval("isCompleted")%>'
                                    Tooltip="Completar"
                                >
                                    <i class="bi bi-check-circle"></i>
                                </asp:LinkButton>
                                <asp:HyperLink ID="lnkEditGV" runat="server"
                                    NavigateUrl='<%# "~/Pages/TaskForm.aspx?id="+ Eval("id") %>'
                                    CssClass="btn btn-warning"
                                    Tooltip="Editar"
                                >
                                    <i class="bi bi-pencil"></i>
                                </asp:HyperLink>
                                <asp:LinkButton ID="btnDelete" runat="server"
                                    CommandName="DeleteTask"
                                    CommandArgument='<%# Eval("id")%>'
                                    CssClass="btn btn-danger"
                                    Tooltip="Excluir"
                                >
                                    <i class="bi bi-trash"></i>
                                </asp:LinkButton>
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </asp:Panel>

    <asp:Panel ID="pnlCards" runat="server" Visible="false">
        <div class="row">
            <asp:Repeater ID="rptTasks" runat="server" OnItemCommand="rptTasks_ItemCommand">
                <ItemTemplate>
                    <div class="col-12 col-sm-6 col-md-4 mb-3">
                        <div class='card h-100 border-5 %# (bool)Eval("isCompleted") ? "border-success" : "border-warning" %> task-card'>
                            <div class="card-header d-flex justify-content-between align-items-center">
                                <h5 class="card-title mb-0">
                                    <%# Eval("title") %>
                                </h5>
                                 <span class='badge <%# (bool)Eval("isCompleted") ? "bg-success" : "bg-warning text-dark" %>'>
                                    <%# (bool)Eval("isCompleted") ? "Finalizada" : "Pendente" %>
                                </span>
                            </div>
                            <div class="card-body">
                                <p class="card-text text-muted"><%# Eval("description") %></p>
                                <p class="mb-0">
                                    <span class="badge bg-primary">
                                        <i class="bi bi-tag"></i><%# Eval("category") %>
                                    </span>
                                </p>
                            </div>
                            <div class="card-footer">
                                <div class="d-grid gap-2">
                                    <div class="btn-group" role="group">
                                        <asp:LinkButton ID="btnComplete" runat="server"
                                            CommandName="CompleteTask"
                                            CommandArgument='<%# Eval("id")%>'
                                            CssClass="btn btn-success"
                                            Visible='<%# !(bool)Eval("isCompleted")%>'
                                            Tooltip="Completar"
                                        >
                                            <i class="bi bi-check-circle"></i>
                                        </asp:LinkButton>
                                        <asp:HyperLink ID="lnkEditGV" runat="server"
                                            NavigateUrl='<%# "~/Pages/TaskForm.aspx?id="+ Eval("id") %>'
                                            CssClass="btn btn-warning"
                                            Tooltip="Editar"
                                        >
                                            <i class="bi bi-pencil"></i>
                                        </asp:HyperLink>
                                        <asp:LinkButton ID="btnDelete" runat="server"
                                            CommandName="DeleteTask"
                                            CommandArgument='<%# Eval("id")%>'
                                            CssClass="btn btn-danger"
                                            Tooltip="Excluir"
                                        >
                                            <i class="bi bi-trash"></i>
                                        </asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </asp:Panel>
</asp:Content>

