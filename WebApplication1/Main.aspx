<%@ Page Title="" Language="C#" MasterPageFile="~/NBDCostTrackingSystem.Master" AutoEventWireup="true" CodeBehind="Main.aspx.cs" Inherits="NBDCostTrackingSystem.Manager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">NBD - Cost Tracking System</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">
    <asp:EntityDataSource ID="edsProjectList" runat="server" ConnectionString="name=EntitiesNBD" DefaultContainerName="EntitiesNBD" EnableFlattening="False" EntitySetName="PROJECTs" Select="it.[ID], it.[projName]">
    </asp:EntityDataSource>
    <%--CLIENTS--%>
    <div class="container">
        <div class="row">
            <div class="col-xs-12">
                <div class="panel panel-default">
                    <div class="panel-heading text-center">
                        <h4>Project Administration Reports</h4>
                    </div>
                    <div class="panel-body">
                        <asp:Table ID="tblProjects" CssClass="table table-condensed" runat="server" ViewStateMode="Disabled"></asp:Table>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <%--EMPLOYEES--%>
            <div class="col-xs-12 col-md-6">
                <div class="panel panel-default">
                    <div class="panel-heading text-center panelHeaderFix">
                        <h4>Daily Work Report</h4>
                    </div>
                    <div class="panel-body panelBodyFix">
                        <div class="col-xs-6">
                            <div class="form-group">
                                <label for="txtCreateDailyWorkReportDateInPanel">Date:</label>
                                <asp:TextBox ID="txtCreateDailyWorkReportDateInPanel" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-xs-6">
                            <div class="form-group">
                                <label for="ddlDailyWorkReportProjectInPanel">Project:</label>
                                <asp:DropDownList ID="ddlDailyWorkReportProjectInPanel" runat="server" CssClass="form-control" AppendDataBoundItems="True" OnSelectedIndexChanged="ddlDailyWorkReportProjectInPanel_SelectedIndexChanged" AutoPostBack="True" DataSourceID="edsProjectList" DataTextField="projName" DataValueField="ID">
                                    <asp:ListItem Value="-1">Select a Project...</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-xs-6">
                            <div class="form-group">
                                <label for="ddlDailyWorkReportTaskstInPanel">Task:</label>
                                <asp:DropDownList ID="ddlDailyWorkReportTaskstInPanel" runat="server" CssClass="form-control" AppendDataBoundItems="True">
                                    <asp:ListItem Value="-1">Select a Task...</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-xs-6">
                            <div class="form-group">
                                <label for="txtHoursWorked">Hours Worked:</label>
                                <asp:TextBox ID="txtHoursWorked" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-xs-12 text-center">
                            <hr />
                            <asp:LinkButton ID="btnDailyWorkReportAddNewTask" CssClass="btn btn-default" runat="server" OnClick="btnDailyWorkReportAddNewTask_Click"><span class="glyphicon glyphicon-plus"></span>&nbsp;Add</asp:LinkButton></td>
                            <button type="button" id="Body_DailyWorkReportClear" class="btn btn-default"><span class="glyphicon glyphicon-minus"></span>&nbsp;Clear</button>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-xs-12 col-md-6">
                <div class="panel panel-default">
                    <div class="panel-heading text-center panelHeaderFix">
                        <h4>Daily Work Summary</h4>
                    </div>
                    <div class="panel-body text-center panelBodyFix">
                        <asp:Panel ID="pnlDailyWorkReportDeleteControls" runat="server" Visible="false">
                            <div class="col-xs-12 text-center">
                                <div class="form-inline">
                                    <label for="ddlDeleteDailyWorkReportFromTable">Delete Row:</label>
                                    &nbsp;
                                    <asp:DropDownList ID="ddlDeleteDailyWorkReportFromTable" runat="server" CssClass="form-control" AppendDataBoundItems="True">
                                        <asp:ListItem Value="-1">Delete All Rows</asp:ListItem>
                                    </asp:DropDownList>
                                    &nbsp;
                                    <asp:LinkButton ID="btnDailyWorkReportDeleteRow" CssClass="btn btn-default" runat="server" OnClick="btnDailyWorkReportDeleteRow_Click"><span class="glyphicon glyphicon-minus"></span>&nbsp;Remove</asp:LinkButton>
                                </div>
                            </div>
                        </asp:Panel>
                        <div class="col-xs-12 text-center">
                            <br />
                            <asp:GridView ID="gvDailyWorkReportSummary" CssClass="table table-condensed" runat="server" GridLines="Horizontal" BorderStyle="None" HorizontalAlign="Center" RowStyle-HorizontalAlign="Left" OnRowDataBound="gvDailyWorkReportSummary_RowDataBound"></asp:GridView>
                            <asp:Label ID="lblDailyWorkReportSummary" runat="server" Text="No items currently added."></asp:Label>
                            <asp:LinkButton ID="btnSaveDailyWorkReport" CssClass="btn btn-default" runat="server" OnClick="btnSaveDailyWorkReport_Click" Visible="False">&nbsp;Save</asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-xs-12 col-md-6">
                <div class="panel panel-default">
                    <div class="panel-heading text-center">
                        <div class="btn-group btn-group-xs pull-right" role="group">
                            <button type="button" class="btn btn-default" data-toggle="modal" data-target="#mdlCreateClient">
                                <span data-toggle="tooltip" data-container="body" title="Create" class="glyphicon glyphicon-file"></span>
                            </button>
                            <asp:LinkButton ID="btnViewAllClients" class="btn btn-default" CausesValidation="false" data-toggle="tooltip" data-container="body" title="View All" runat="server" PostBackUrl="~/ViewAllClients.aspx"><span class="glyphicon glyphicon-eye-open"></span></asp:LinkButton>
                        </div>
                        <h4>Clients</h4>
                    </div>
                    <div class="panel-body">
                        <div class="col-xs-9">
                            <div class="form-group">
                                <asp:DropDownList ID="ddlClientList" runat="server" CssClass="form-control" AppendDataBoundItems="True" OnSelectedIndexChanged="ddlClientList_SelectedIndexChanged" AutoPostBack="True">
                                    <asp:ListItem Value="-1">Select a Client...</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-xs-3">
                            <div class="btn-group btn-group-xs" role="group">
                                <asp:LinkButton ID="btnClientView" class="btn btn-default" CausesValidation="false" data-toggle="tooltip" data-container="body" title="View" runat="server" OnClick="btnClientView_Click"><span class="glyphicon glyphicon-eye-open"></span></asp:LinkButton>
                                <asp:LinkButton ID="btnClientEdit" class="btn btn-default" CausesValidation="false" data-toggle="tooltip" data-container="body" title="Edit" runat="server" OnClick="btnClientEdit_Click"><span class="glyphicon glyphicon-pencil"></span></asp:LinkButton>
                                <asp:LinkButton ID="btnClientRemove" class="btn btn-default" CausesValidation="false" data-toggle="tooltip" data-container="body" title="Remove" runat="server" OnClick="btnClientRemove_Click" OnClientClick='return confirm("Are you sure?");'><span class="glyphicon glyphicon-remove"></span></asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <%--EMPLOYEES--%>
            <div class="col-xs-12 col-md-6">
                <div class="panel panel-default">
                    <div class="panel-heading text-center">
                        <div class="btn-group btn-group-xs pull-right" role="group">
                            <button type="button" class="btn btn-default" data-toggle="modal" data-target="#mdlCreateEmployee">
                                <span data-toggle="tooltip" data-container="body" class="glyphicon glyphicon-file" title="Create"></span>
                            </button>
                            <asp:LinkButton ID="btnViewAllEmployees" class="btn btn-default" CausesValidation="false" data-toggle="tooltip" data-container="body" title="View All" runat="server" PostBackUrl="~/ViewAllEmployees.aspx"><span class="glyphicon glyphicon-eye-open"></span></asp:LinkButton>
                        </div>
                        <h4>Employees</h4>
                    </div>
                    <div class="panel-body">
                        <div class="col-xs-9">
                            <div class="form-group">
                                <asp:DropDownList ID="ddlEmployeeList" runat="server" CssClass="form-control" AppendDataBoundItems="True" AutoPostBack="True" OnSelectedIndexChanged="ddlEmployeeList_SelectedIndexChanged">
                                    <asp:ListItem Value="-1">Select an Employee...</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-xs-3">
                            <div class="btn-group btn-group-xs" role="group">
                                <asp:LinkButton ID="btnEmployeeView" class="btn btn-default" CausesValidation="false" data-toggle="tooltip" data-container="body" title="View" runat="server" OnClick="btnEmployeeView_Click"><span class="glyphicon glyphicon-eye-open"></span></asp:LinkButton>
                                <asp:LinkButton ID="btnEmployeeEdit" class="btn btn-default" CausesValidation="false" data-toggle="tooltip" data-container="body" title="Edit" runat="server" OnClick="btnEmployeeEdit_Click"><span class="glyphicon glyphicon-pencil"></span></asp:LinkButton>
                                <asp:LinkButton ID="btnEmployeeRemove" class="btn btn-default" CausesValidation="false" data-toggle="tooltip" data-container="body" title="Remove" runat="server" OnClick="btnEmployeeRemove_Click"><span class="glyphicon glyphicon-remove"></span></asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <asp:Label ID="lblFocusHAX" runat="server" Text="HAX FOR POSTBACK BRA" Visible="false"></asp:Label>
        <%--CREATE CLIENT MODAL--%>
        <div id="mdlViewClient" class="modal fade" role="dialog">
            <div class="modal-dialog  modal-lg">
                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title">View Client Information</h4>
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-xs-12">
                                <div class="col-xs-6">
                                    <div class="form-group">
                                        <label for="txtClientName">Name: </label>
                                        <asp:TextBox ID="txtClientName" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-xs-6">
                                    <div class="form-group">
                                        <label for="txtClientAddress">Address: </label>
                                        <asp:TextBox ID="txtClientAddress" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="col-xs-12">
                                <div class="col-xs-6">
                                    <div class="form-group">
                                        <label for="txtClientCity">City: </label>
                                        <asp:DropDownList ID="ddlClientCity" runat="server" Enabled="false" CssClass="form-control" DataSourceID="edsClientCity" DataTextField="city1" DataValueField="ID" AppendDataBoundItems="True"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-xs-6">
                                    <div class="form-group">
                                        <label for="ddlClientProv">Province: </label>
                                        <asp:DropDownList ID="ddlClientProv" runat="server" Enabled="false" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="col-xs-12">
                                <div class="col-xs-6">
                                    <div class="form-group">
                                        <label for="txtClientPCode">Postal Code: </label>
                                        <asp:TextBox ID="txtClientPCode" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-xs-6">
                                    <div class="form-group">
                                        <label for="txtClientPhone">Phone Number: </label>
                                        <asp:TextBox ID="txtClientPhone" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="col-xs-12">
                                <div class="col-xs-4">
                                    <div class="form-group">
                                        <label for="txtClientConFName">Contact First Name: </label>
                                        <asp:TextBox ID="txtClientConFName" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-xs-4">
                                    <div class="form-group">
                                        <label for="txtClientConLName">Contact Last Name: </label>
                                        <asp:TextBox ID="txtClientConLName" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-xs-4">
                                    <div class="form-group">
                                        <label for="txtClientConPosition">Contact Position: </label>
                                        <asp:TextBox ID="txtClientConPosition" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">Cancel</button>
                    </div>
                </div>
            </div>
        </div>
</asp:Content>
