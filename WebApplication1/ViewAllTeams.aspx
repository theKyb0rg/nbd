<%@ Page Title="" Language="C#" MasterPageFile="~/NBDCostTrackingSystem.Master" AutoEventWireup="true" CodeBehind="ViewAllTeams.aspx.cs" Inherits="NBDCostTrackingSystem.ViewAllTeams" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">NBD - View All Teams</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">
    <%--CONTENT--%>
    <div class="container">
        <div class="row">
            <%--PROJECTS--%>
            <div class="col-xs-12">
                <div class="panel panel-default">
                    <div class="panel-heading">
                         <div class="btn-group btn-group-xs pull-right" role="group">
                             <button type="button" class="btn btn-default" data-toggle="modal" data-target="#mdlCreateTeam">
                                 <span data-toggle="tooltip" data-container="body" class="glyphicon glyphicon-file" title="Create"></span>
                             </button>
                         </div>
                        <h4>Teams</h4>
                    </div>
                    <div class="panel-body">
                        <asp:Table ID="tblTeams" CssClass="table table-condensed" runat="server"></asp:Table>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
