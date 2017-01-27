<%@ Page Title="" Language="C#" MasterPageFile="~/NBDCostTrackingSystem.Master" AutoEventWireup="true" CodeBehind="ViewAllProductionPlans.aspx.cs" Inherits="NBDCostTrackingSystem.ViewAllProductionPlans" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">NBD - View All Prodution Plans</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">
    <div class="container">
        <asp:Panel ID="results" runat="server" Visible="true">
            <div class="row">
                <div class="col-xs-12">
                    <h1 class="text-center">View a Production Plan</h1>
                    <div class="col-xs-12 col-md-6">
                        <div class="panel panel-default">
                            <div class="panel-heading text-center">
                                <h4>Clients</h4>
                            </div>
                            <div class="panel-body">
                                <asp:ListBox ID="lbClientResults" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="lbClientResults_SelectedIndexChanged"></asp:ListBox>
                            </div>
                        </div>
                    </div>
                    <div class="col-xs-12 col-md-6">
                        <div class="panel panel-default">
                            <div class="panel-heading text-center">
                                <h4>Projects</h4>
                            </div>
                            <div class="panel-body">
                                <asp:ListBox ID="lbProjectResults" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="lbProjectResults_SelectedIndexChanged"></asp:ListBox>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </asp:Panel>
        <!--Display Design Bid Modal-->
        <div class="panel panel-default" id="showBid" runat="server" visible="false">
            <div class="panel-heading">
                <h4 class="text-center">Project Details</h4>
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="col-xs-12 col-md-6">
                        <div class="form-group">
                            <label for="mdlTxtClientName">Client Name:</label>
                            <asp:TextBox runat="server" ID="mdlTxtClientName" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-xs-12 col-md-6">
                        <div class="form-group">
                            <label for="txtClientName">Client Address:</label>
                            <asp:TextBox ID="mdlTxtClientAddress" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-xs-12 col-md-6">
                        <div class="form-group">
                            <label for="mdlTxtClientContact">Contact:</label>
                            <asp:TextBox ID="mdlTxtClientContact" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-xs-12 col-md-6">
                        <div class="form-group">
                            <label for="mdlTxtClientPhone">Phone:</label>
                            <asp:TextBox ID="mdlTxtClientPhone" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-xs-12 col-md-6">
                        <div class="form-group">
                            <label for="mdlTxtProjectName">Project Name:</label>
                            <asp:TextBox ID="mdlTxtProjectName" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-xs-12 col-md-6">
                        <div class="form-group">
                            <label for="mdlTxtProjectBeginDate">Estimated Begin Date:</label>
                            <asp:TextBox ID="mdlTxtProjectBeginDate" runat="server" CssClass="form-control" TextMode="Date" ReadOnly="true"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-xs-12 col-md-6">
                        <div class="form-group">
                            <label for="mdlTxtProjectSite">Project Site:</label>
                            <asp:TextBox ID="mdlTxtProjectSite" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-xs-12 col-md-6">
                        <div class="form-group">
                            <label for="mdlTxtProjectEndDate">Estimated End Date:</label>
                            <asp:TextBox ID="mdlTxtProjectEndDate" runat="server" CssClass="form-control" TextMode="Date" ReadOnly="true"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-xs-12 col-md-6">
                        <div class="form-group">
                            <label for="mdlTxtNBDSales">Sales Associate:</label>
                            <asp:TextBox ID="mdlTxtNBDSales" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-xs-12 col-md-6">
                        <div class="form-group">
                            <label for="txtNBDDesigner">Designer:</label>
                            <asp:TextBox ID="mdlTxtNBDDesigner" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-xs-12 text-center">
                        <h4>Materials Summary</h4>
                    </div>
                    <div class="col-xs-12">
                        <asp:Table ID="tblMaterialsSummary" CssClass="table table-condensed" runat="server"></asp:Table>
                        <div class="col-xs-12" id="editMatSummary" runat="server" visible="false"></div>
                    </div>
                    <div class="col-xs-12 text-center">
                        <h4>Labour Summary</h4>
                    </div>
                    <div class="col-xs-12">
                        <asp:Table ID="tblLabourSummary" CssClass="table table-condensed" runat="server"></asp:Table>
                        <div class="col-xs-12" id="editLabSummary" runat="server" visible="false"></div>
                    </div>
                    <div class="col-xs-12">
                        <hr />
                        <div class="col-xs-12 col-md-4">
                            <div class="form-group text-center">
                                <h2>Materials Total</h2>
                                <h3 id="headerMaterialTotal" runat="server"></h3>
                            </div>
                        </div>
                        <div class="col-xs-12 col-md-4">
                            <div class="form-group text-center">
                                <h1>Bid Total</h1>
                                <h2 id="headerBidTotal" runat="server"></h2>
                            </div>
                        </div>
                        <div class="col-xs-12 col-md-4">
                            <div class="form-group text-center">
                                <h2>Labour Total</h2>
                                <h3 id="headerLabourlTotal" runat="server"></h3>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="panel-footer text-center">
                <asp:Button ID="btnSaveBid" runat="server" CssClass="btn btn-default" Visible="false" Text="Save" OnClick="btnSaveBid_Click" />
                <asp:Button ID="btnEditBid" runat="server" CssClass="btn btn-default" Text="Edit" OnClick="btnEditBid_Click" />
            </div>
        </div>
    </div>
</asp:Content>
