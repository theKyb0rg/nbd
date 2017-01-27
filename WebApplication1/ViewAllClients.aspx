<%@ Page Title="" Language="C#" MasterPageFile="~/NBDCostTrackingSystem.Master" AutoEventWireup="true" CodeBehind="ViewAllClients.aspx.cs" Inherits="NBDCostTrackingSystem.AllClients" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">NBD - View All Clients</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">
    <%--CONTENT--%>
    <div class="container">
        <div class="row">
            <%--PROJECTS--%>
            <div class="col-xs-12">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        Clients
                         <div class="btn-group btn-group-xs pull-right" role="group">
                             <button type="button" class="btn btn-default" data-toggle="modal" data-target="#mdlCreateClient">
                                 <span data-toggle="tooltip" data-container="body" class="glyphicon glyphicon-file" title="Create"></span>
                             </button>
                         </div>
                    </div>
                    <div class="panel-body">
                        <asp:Table ID="tblClient" CssClass="table table-condensed" runat="server"></asp:Table>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
