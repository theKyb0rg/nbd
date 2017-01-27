<%@ Page Title="" Language="C#" MasterPageFile="~/NBDCostTrackingSystem.Master" AutoEventWireup="true" CodeBehind="CreateTeam.aspx.cs" Inherits="NBDCostTrackingSystem.CreateTeamNew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">NBD - Add a Team</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">
    <div class="container-fluid">
        <div class="row">
            <div class="col-xs-12">
                <h1 class="text-center">Create Team</h1>
                <%-- MEMBERS--%>
                <div class="col-xs-6">
                    <div class="panel panel-default">
                        <div class="panel-heading text-center">Add Members</div>
                        <div class="panel-body">
                            <div class="col-xs-12">
                                <div class="form-group">
                                    <label for="ddlDesigners">Designers:</label>
                                    <asp:DropDownList ID="ddlDesigners" runat="server" CssClass="form-control">
                                        <asp:ListItem>Select a Designer...</asp:ListItem>
                                        <asp:ListItem>Connie Nguyen</asp:ListItem>
                                        <asp:ListItem>Cheryl Poy</asp:ListItem>
                                        <asp:ListItem>Tamara Bakken</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <asp:Button ID="btnAddDesigner" runat="server" CssClass="btn btn-default pull-right" Text="Add Designer" />
                            </div>
                            <div class="col-xs-12">
                                <div class="form-group">
                                    <label for="ddlProduction">Production Staff:</label>
                                    <asp:DropDownList ID="ddlProduction" runat="server" CssClass="form-control">
                                        <asp:ListItem>Select a Production Staff Member...</asp:ListItem>
                                        <asp:ListItem>Bert Swenson</asp:ListItem>
                                        <asp:ListItem>Monica Goce</asp:ListItem>
                                        <asp:ListItem>Sue Kaufman</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <asp:Button ID="btnAddProductionStaff" runat="server" CssClass="btn btn-default pull-right" Text="Add Production Staff" />
                            </div>
                            <div class="col-xs-12">
                                <div class="form-group">
                                    <label for="ddlSales">Sales Associates:</label>
                                    <asp:DropDownList ID="ddlSales" runat="server" CssClass="form-control">
                                        <asp:ListItem>Select a Sales Associate...</asp:ListItem>
                                        <asp:ListItem>Bob Reindhardt</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <asp:Button ID="btnAddSalesAssociate" runat="server" CssClass="btn btn-default pull-right" Text="Add Sales Associate" />
                            </div>
                            <div class="col-xs-12">
                                <div class="form-group">
                                    <label for="ddlManagers">Managers:</label>
                                    <asp:DropDownList ID="ddlManagers" runat="server" CssClass="form-control">
                                        <asp:ListItem>Select a Manager...</asp:ListItem>
                                        <asp:ListItem>Keri Yamaguchi</asp:ListItem>
                                        <asp:ListItem>Stan Fenton </asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <asp:Button ID="btnAddManager" runat="server" CssClass="btn btn-default pull-right" Text="Add Manager" />
                            </div>
                        </div>
                    </div>
                </div>
                <%--TEAM INFO--%>
                <div class="col-xs-6">
                    <div class="panel panel-default">
                        <div class="panel-heading text-center">Team Info</div>
                        <div class="panel-body">
                            <div class="col-xs-6">
                                <div class="form-group">
                                    <label for="txtTeamName">Team Name:</label>
                                    <asp:TextBox ID="txtTeamName" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-xs-6">
                                <div class="form-group">
                                    <label for="txtCreatedBy">Created By:</label>
                                    <asp:TextBox ID="txtCreatedBy" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-xs-12">
                                <hr />
                                <h5>Current Team Members:</h5>
                                <div class="col-xs-6">
                                    <asp:Label ID="lblDesigners" runat="server" Text="Designers" Font-Bold="True"></asp:Label>
                                    <p>Connie Nguyen</p>
                                    <br />
                                    <asp:Label ID="lblProductionStaff" runat="server" Text="Production Staff" Font-Bold="True"></asp:Label>
                                    <p>Sue Kaufman</p>
                                </div>
                                <div class="col-xs-6">
                                    <asp:Label ID="lblSalesAssociates" runat="server" Text="Sales Associates" Font-Bold="True"></asp:Label>
                                    <p>Bob Reindhardt</p>
                                    <br />
                                    <asp:Label ID="lblManagers" runat="server" Text="Managers" Font-Bold="True"></asp:Label>
                                    <p>Keri Yamaguchi</p>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-xs-12 text-center">
                    <asp:Button ID="btnProductionPlanSave" runat="server" CssClass="btn btn-default" Text="Save Team" />
                </div>
            </div>
        </div>
        <div class="col-xs-12 footer"></div>
    </div>
</asp:Content>
