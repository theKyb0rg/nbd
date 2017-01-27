<%@ Page Title="" Language="C#" MasterPageFile="~/NBDCostTrackingSystem.Master" AutoEventWireup="true" CodeBehind="ViewDesignBid.aspx.cs" Inherits="NBDCostTrackingSystem.ViewDesignBid" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">NBD - View Design Bid</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">
    <div class="container">
        <asp:Panel ID="results" runat="server" Visible="true">
            <div class="row">
                <div class="col-xs-12">
                    <h1 class="text-center">View a Design Bid</h1>
                </div>
            </div>
        </asp:Panel>
        <!--Display Design Bid Modal-->
        <div class="panel panel-default" id="showBid" runat="server">
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
                            <label for="mdlTxtProjectName">Project Name:</label><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="text-danger" ErrorMessage=" Please enter a project name!" ControlToValidate="mdlTxtProjectName" ValidationGroup="projInfo"></asp:RequiredFieldValidator>
                            <asp:TextBox ID="mdlTxtProjectName" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-xs-12 col-md-6">

                        <div class="form-group">
                            <label for="mdlTxtProjectBeginDate">Estimated Begin Date:</label><asp:RequiredFieldValidator ID="valBeginDate" runat="server" CssClass="text-danger" ErrorMessage=" Please select a valid date!" ControlToValidate="mdlTxtProjectBeginDate" ValidationGroup="projInfo"></asp:RequiredFieldValidator>
                            <asp:TextBox ID="mdlTxtProjectBeginDate" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-xs-12 col-md-6">
                        <div class="form-group">
                            <label for="mdlTxtProjectSite">Project Site:</label><asp:RequiredFieldValidator ID="valProjSite" runat="server" CssClass="text-danger" ErrorMessage=" Please enter a site name!" ControlToValidate="mdlTxtProjectSite" ValidationGroup="projInfo"></asp:RequiredFieldValidator>
                            <asp:TextBox ID="mdlTxtProjectSite" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-xs-12 col-md-6">
                        <div class="form-group">
                            <label for="mdlTxtProjectEndDate">Estimated End Date:</label><asp:RequiredFieldValidator ID="valEndDate" runat="server" CssClass="text-danger" ErrorMessage=" Please select a valid date!" ControlToValidate="mdlTxtProjectEndDate" ValidationGroup="projInfo"></asp:RequiredFieldValidator>
                            <asp:TextBox ID="mdlTxtProjectEndDate" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-xs-12 col-md-6">
                        <div class="form-group">
                            <label for="ddlSales">Sales Associate:</label>
                            <asp:DropDownList ID="ddlSales" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-xs-12 col-md-6">
                        <div class="form-group">
                            <label for="ddlDesigner">Designer:</label>
                            <asp:DropDownList ID="ddlDesigner" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-xs-12">
                        <h4>Materials Summary</h4>
                    </div>
                    <div class="col-xs-12">
                        <asp:Table ID="tblMaterialsSummary" CssClass="table table-condensed" runat="server"></asp:Table>
                        <br />
                    </div>
                    <div class="col-xs-12">
                        <br />
                        <h4>Labour Summary</h4>
                    </div>
                    <div class="col-xs-12">
                        <asp:Table ID="tblLabourSummary" CssClass="table table-condensed" runat="server"></asp:Table>
                        <br />
                    </div>
                    <div class="col-xs-12">
                        <h1 class="text-center">Project Summary</h1>
                        <hr />
                        <div class="col-xs-4" id="pieCode1" runat="server">
                            <h3 class="colourBox1 text-center">Materials</h3>
                            <h2 class="text-center" id="materialPercentage" runat="server"></h2>
                        </div>
                        <div class="col-xs-4 text-center">
                            <div class="pie" id="pie" runat="server"></div>
                        </div>
                        <div class="col-xs-4" id="pieCode2" runat="server">
                            <h3 class="colourBox2 text-center">Labour</h3>
                            <h2 class="text-center" id="labourPercentage" runat="server"></h2>
                        </div>
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
        </div>
    </div>
</asp:Content>
