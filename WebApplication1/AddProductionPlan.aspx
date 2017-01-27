<%@ Page Title="" Language="C#" MasterPageFile="~/NBDCostTrackingSystem.Master" AutoEventWireup="true" CodeBehind="AddProductionPlan.aspx.cs" Inherits="NBDCostTrackingSystem.AddProductionPlan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">NBD - Production Plan</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">
    <div class="container-fluid">
        <div class="row">
            <div class="col-xs-12">
                <h1 class="text-center">Production Plan</h1>
                <%--CLIENT INFORMATION--%>
                <div class="col-md-4">
                    <div class="panel panel-default">
                        <div class="panel-heading text-center">
                            <h4>Client Information</h4>
                        </div>
                        <div class="panel-body">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label for="ddlClientName">Client Name:</label><asp:CompareValidator ID="valClientName" runat="server" ErrorMessage=" Select Client" ControlToValidate="ddlClientName" ValueToCompare="-1" Operator="NotEqual" SetFocusOnError="True"></asp:CompareValidator>
                                    <asp:DropDownList ID="ddlClientName" runat="server" CssClass="form-control" AutoPostBack="True" DataSourceID="edsClients" DataTextField="cliName" DataValueField="ID" OnSelectedIndexChanged="ddlClientName_SelectedIndexChanged" AppendDataBoundItems="True">
                                        <asp:ListItem Value="-1">Select a Client...</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group">
                                    <label for="txtClientName">Client Address:</label>
                                    <asp:TextBox ID="txtClientAddress" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label for="txtClientContact">Contact:</label>
                                    <asp:TextBox ID="txtClientContact" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <label for="txtClientPhone">Phone:</label>
                                    <asp:TextBox ID="txtClientPhone" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <%--PROJECT--%>
                <div class="col-xs-12 col-md-4">
                    <div class="panel panel-default">
                        <div class="panel-heading text-center">
                            <h4>Project</h4>
                        </div>
                        <div class="panel-body">
                            <div class="col-xs-12">
                                <div class="col-xs-12 col-md-6">
                                    <div class="form-group">
                                        <label for="ddlProject">Project Name:</label>
                                        <asp:DropDownList ID="ddlProject" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlProject_SelectedIndexChanged" AutoPostBack="True">
                                            <asp:ListItem Value="-1">Please Select a Client...</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-xs-12 col-md-6">
                                    <div class="form-group">
                                        <label for="txtProjectBeginDate">Estimated Begin Date:</label><asp:RequiredFieldValidator ID="valBeginDate" CssClass="error" runat="server" ErrorMessage=" Choose Date" ControlToValidate="txtProjectBeginDate" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                        <asp:TextBox ID="txtProjectBeginDate" ReadOnly="true" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="col-xs-12">
                                <div class="col-xs-12 col-md-6">
                                    <div class="form-group">
                                        <label for="txtProjectSite">Project Site:</label><asp:RequiredFieldValidator ID="valProjectSite" CssClass="error" runat="server" ErrorMessage=" Enter a Project Site" ControlToValidate="txtProjectSite" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                        <asp:TextBox ID="txtProjectSite" ReadOnly="true" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-xs-12 col-md-6">
                                    <div class="form-group">
                                        <label for="txtProjectEndDate">Estimated End Date:</label><asp:RequiredFieldValidator ID="valEndDate" CssClass="error" runat="server" ErrorMessage=" Choose Date" ControlToValidate="txtProjectEndDate" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                        <asp:TextBox ID="txtProjectEndDate" ReadOnly="true" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <%--NBD STAFF--%>
                <div class="col-xs-12 col-md-4">
                    <div class="panel panel-default">
                        <div class="panel-heading text-center">
                            <h4>NBD Staff</h4>
                        </div>
                        <div class="panel-body">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label for="txtTeamSalesAssociate">Sales Associate:</label>
                                    <asp:TextBox ID="txtTeamSalesAssociate" ReadOnly="true" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <label for="txtTeamDesigner">Designer:</label>
                                    <asp:TextBox ID="txtTeamDesigner" ReadOnly="true" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label for="ddlTeamProduction">Team:</label>
                                    <asp:DropDownList ID="ddlTeamProduction" runat="server" CssClass="form-control">
                                        <asp:ListItem>Select a Team...</asp:ListItem>
                                        <asp:ListItem>Team Awesome</asp:ListItem>
                                        <asp:ListItem>The Best Team</asp:ListItem>
                                        <asp:ListItem>Wow Much Team Very Good</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-xs-12">
                    <div class="panel panel-default" id="pnlMatSum" runat="server" visible="false">
                        <div class="panel-heading">
                            <h4 class="text-center">Materials Summary</h4>
                        </div>
                        <div class="panel-body">
                            <div class="col-xs-12">
                                <asp:Table ID="tblMaterialsSummary" CssClass="table table-condensed" runat="server"></asp:Table>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-xs-12">
                    <div class="panel panel-default" id="pnlLabSum" runat="server" visible="false">
                        <div class="panel-heading">
                            <h4 class="text-center">Labour Summary</h4>
                        </div>
                        <div class="panel-body">
                            <div class="col-xs-12">
                                <asp:Table ID="tblLabourSummary" CssClass="table table-condensed" runat="server"></asp:Table>
                            </div>
                        </div>
                        <div class="panel-footer">
                            <div class="text-center">
                                <asp:Button ID="btnSave" runat="server" CssClass="btn btn-default" Text="Save" OnClick="btnSave_onClick" />
                                <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-default" Text="Cancel" OnClick="btnSave_onClick" />
                            </div>
                        </div>
                    </div>
                </div>
                <asp:Label ID="lblFocusHAX" runat="server" Text="HAX FOR POSTBACK BRA" Visible="false"></asp:Label>
                <asp:EntityDataSource ID="edsClients" runat="server" ConnectionString="name=EntitiesNBD" DefaultContainerName="EntitiesNBD" EnableFlattening="False" EntitySetName="CLIENTs" Select="it.[ID], it.[cliName]"></asp:EntityDataSource>
                <asp:EntityDataSource ID="edsMaterialTypes" runat="server" ConnectionString="name=EntitiesNBD" DefaultContainerName="EntitiesNBD" EnableFlattening="False" EntitySetName="MATERIALs" Select="it.[matType], it.[ID]"></asp:EntityDataSource>
            </div>
        </div>
    </div>
</asp:Content>
