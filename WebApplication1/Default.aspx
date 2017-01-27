<%@ Page Title="" Language="C#" MasterPageFile="~/NBDSplash.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="NBDCostTrackingSystem.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">Natural By Design</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">
    <div class="text-center splashMain">
        <img src="Content/Images/NBDSplash2.jpg" class="img-responsive" style="user-select: none;" />
        <h1 class="splashTitle text-center">Natural By Design | <span style="font-weight: 100; font-size: 3vw;">Beautiful. Outdoor. Solutions.</span></h1>
    </div>
    <div id="whatWeDo" class="text-center wwdMain">
        <img src="Content/Images/NBDWhatWeDo.jpg" class="img-responsive" style="user-select: none;" />
        <h1 class="wwdTitle">Test</h1>
    </div>
    <div id="howWeWork" class="text-center hwwMain">
        <img src="Content/Images/NBDHowWeWork.jpg" class="img-responsive" style="user-select: none;" />
    </div>
    <div id="contactUs" class="text-center cuMain">
        <img src="Content/Images/NBDContactUs.jpg" class="img-responsive" style="user-select: none;" />
    </div>
    <a href="#DefaultForm" class="cd-top"></a>
    <%--CREATE LOGIN MODAL WORK REPORT MODAL--%>
    <div id="mdlLogin" class="modal fade" role="dialog">
        <div class="modal-dialog  modal-sm">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Login - Cost Tracking System</h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-xs-12">
                            <div class="form-group">
                                <label for="txtUsername">Username:</label>
                                <asp:RequiredFieldValidator ID="txtUsernameValidator1" CssClass="text-danger" runat="server" ErrorMessage=" Please enter a username" ControlToValidate="txtUsername" ValidationGroup="loginValidate"></asp:RequiredFieldValidator>
                                <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label for="txtPassword">Password:</label>
                                <asp:RequiredFieldValidator ID="txtPasswordValidator1" CssClass="text-danger" runat="server" ErrorMessage=" Please enter a password" ValidationGroup="loginValidate" ControlToValidate="txtPassword"></asp:RequiredFieldValidator>
                                <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <div class="checkbox">
                                    <label for="cbRemember">
                                        <asp:CheckBox ID="cbRemember" runat="server" />Remember Me
                                    </label>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <div class="text-center">
                        <div class="btn-group" role="group">
                            <asp:Button ID="btnLogin" runat="server" CssClass="btn btn-default buttonGrey" Text="Login" OnClick="btnLogin_Click" ValidationGroup="loginValidate" />
                            <button type="button" class="btn btn-primary buttonGrey reset" formnovalidate="formnovalidate">Reset</button>
                            <button type="button" class="btn btn-danger buttonGrey" data-dismiss="modal">Cancel</button>
                        </div>
                        <asp:Button ID="btnForgotPassword" runat="server" CssClass="btn btn-link" Text="Forgot Password?" />
                    </div>
                </div>
            </div>
        </div>
    </div>
    <asp:Label ID="lblDBERROR" runat="server" Text="Label"></asp:Label>
    <%--CREATE REGISTRATION PAGE MODAL--%>
    <div id="mdlRegister" class="modal fade" role="dialog">
        <div class="modal-dialog  modal-sm">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Register - Cost Tracking System</h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-xs-12">
                            <div class="form-group">
                                <label for="txtRegisterUsername">Username:</label>
                                <asp:TextBox ID="txtRegisterUsername" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label for="txtRegisterPassword">Password:</label>
                                <asp:TextBox ID="txtRegisterPassword" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label for="txtRegisterConfirmPassword">Confirm Password:</label>
                                <asp:TextBox ID="txtRegisterConfirmPassword" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <div class="text-center">
                        <div class="btn-group" role="group">
                            <asp:Button ID="btnRegister" runat="server" CssClass="btn btn-default buttonGrey" Text="Register" OnClick="btnRegister_Click" />
                            <button type="button" class="btn btn-primary buttonGrey reset">Reset</button>
                            <button type="button" class="btn btn-danger buttonGrey" data-dismiss="modal">Cancel</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
