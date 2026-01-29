<%@ Page Title="Login" Language="C#" MasterPageFile="~/Public.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ToDoApp.Web._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <div class="row justify-content-center align-items-center min-vh-100">
            <div class="col-md-6 col-lg-4">
                <asp:Label ID="lblError" 
                    runat="server" 
                    CssClass="alert alert-danger d-block mb-3"
                    Visible="false"
                    role="alert"
                />

                <div class="card shadow-sm">
                    <div class="card-header bg-primary text-white text-center">
                        <h4 class="mb-0">
                            <i class="bi bi-box-arrow-in-right"></i>Login
                        </h4>
                    </div>
                    <div class="card-body p-4">
                        <div class="mb-3">
                            <label for="<%= txtUser.ClientID %>" class="form-label">Usuário</label>
                            <div class="input-group">
                                <span class="input-group-text">
                                    <i class="bi bi-person"></i>
                                </span>
                                <asp:TextBox ID="txtUser" runat="server" CssClass="form-control" placeholder="Digite seu usuário" />
                            </div>
                        </div>

                        <div class="mb-3">
                            <label for="<%= txtPassword.ClientID %>" class="form-label">Senha</label>
                            <div class="input-group">
                                <span class="input-group-text">
                                    <i class="bi bi-lock"></i>
                                </span>
                                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control" placeholder="Digite sua senha" />
                            </div>
                        </div>

                        <div class="d-grid gap-2">
                            <asp:Button ID="btnLogin" runat="server" Text="Entrar" CssClass="btn btn-primary" OnClick="btnLogin_Click" />
                            <button type="button" class="btn btn-outline-secondary btn-novo-usuario" data-bs-toggle="modal" data-bs-target="#userModal">
                                <i class="bi bi-person-plus"></i>Criar Nova Conta
                            </button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade" id="userModal" tabindex="-1" aria-labelledby="userModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header bg-primary text-white">
                    <h5 class="modal-title" id="userModalLabel">
                        <i class="bi bi-person"></i>Criar Novo Usuário
                    </h5>
                    <button type="button" class="btn btn-close btn-close-white" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <div class="mb-3">
                        <label for="<%= txtUserName.ClientID %>" class="form-label">Usuário</label>
                        <div class="input-group">
                            <span class="input-group-text">
                                <i class="bi bi-person"></i>
                            </span>
                            <asp:TextBox ID="txtUserName" runat="server" CssClass="form-control" placeholder="Digite seu usuário" />
                        </div>
                    </div>

                    <div class="mb-3">
                        <label for="<%= txtUserPassword.ClientID %>" class="form-label">Senha</label>
                        <div class="input-group">
                            <span class="input-group-text">
                                <i class="bi bi-lock"></i>
                            </span>
                            <asp:TextBox ID="txtUserPassword" runat="server" TextMode="Password" CssClass="form-control" placeholder="Digite sua senha" />
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">
                        <i class="bi bi-x-circle"></i>Cancelar
                    </button>
                    <asp:Button ID="btnSalvar" runat="server" CssClass="btn btn-primary" Text="Salvar" OnClick="btnSalvar_Click"/>
                </div>
            </div>
        </div>
    </div>
    <script>
        $('.btn-novo-usuario').click(function (e) {
            e.preventDefault();
            console.log("modal")
            $('#userModal').modal('show');
        });
    </script>

</asp:Content>