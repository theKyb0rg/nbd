<%@ Page Title="" Language="C#" MasterPageFile="~/NBDCostTrackingSystem.Master" AutoEventWireup="true" CodeBehind="Search.aspx.cs" Inherits="NBDCostTrackingSystem.Search" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    NBD - Search
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">
    <div class="container">
        <div class="row">
            <div class="col-xs-12">
                <h1 class="text-center">Search Client Records</h1>
                <div class="col-xs-12">
                    <div class="panel panel-default">
                        <div class="panel-heading text-center">
                            <h4>Search Criteria</h4>
                        </div>
                        <div class="panel-body">
                            <!-- Start Search Fields -->
                            <div class="col-xs-12 col-md-3">
                                <div class="form-group">
                                    <label for="txtSearchFName">Search by Name: </label>
                                    <asp:TextBox ID="txtSearchName" CssClass="form-control" runat="server" placeholder="Enter a Client Name to Search"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-xs-12 col-md-3">
                                <div class="form-group">
                                    <label for="txtSearchPhone">Search by Phone Number: </label>
                                    <asp:TextBox ID="txtSearchPhone" CssClass="form-control" runat="server" placeholder="Enter A Phone Number to Search"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-xs-12 col-md-3">
                                <div class="form-group">
                                    <label for="txtSearchPostal">Search by Postal Code: </label>
                                    <asp:TextBox ID="txtSearchPostal" CssClass="form-control" runat="server" placeholder="Enter A Postal Code to Search"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-xs-12 col-md-3">
                                <div class="form-group">
                                    <label for="ddlSearchProvince">Search by Province: </label>
                                    <asp:DropDownList ID="ddlSearchProvince" CssClass="form-control" runat="server">
                                        <asp:ListItem Value="-1" Selected="True" disabled="disabled">Select A Province..</asp:ListItem>
                                        <asp:ListItem Value="Ontario">Ontario</asp:ListItem>
                                        <asp:ListItem Value="Quebec">Quebec</asp:ListItem>
                                        <asp:ListItem Value="Nova Scotia">Nova Scotia</asp:ListItem>
                                        <asp:ListItem Value="New Brunswick">New Brunswick</asp:ListItem>
                                        <asp:ListItem Value="Manitoba">Manitoba</asp:ListItem>
                                        <asp:ListItem Value="British Columbia">British Columbia</asp:ListItem>
                                        <asp:ListItem Value="Prince Edward Island">Prince Edward Island</asp:ListItem>
                                        <asp:ListItem Value="Saskatchewan">Saskatchewan</asp:ListItem>
                                        <asp:ListItem Value="Newfoundland & Labrador">Newfoundland & Labrador</asp:ListItem>
                                        <asp:ListItem Value="Northwest Territories">Northwest Territories</asp:ListItem>
                                        <asp:ListItem Value="Yukon">Yukon</asp:ListItem>
                                        <asp:ListItem Value="Nunavut">Nunavut</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-xs-12 col-md-4"></div>
                            <div class="col-xs-12 col-md-4 text-center">
                                <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-default" Text="Search" OnClick="btnSearch_Click" />
                            </div>
                            <div class="col-xs-12 col-md-4"></div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <asp:Panel ID="results" runat="server" Visible="false">
            <div class="row">
                <div class="col-xs-12">
                    <h1 class="text-center">Results</h1>
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
    </div>
    <!--Display Design Bid Modal-->
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div id="mdlShowDetails" class="modal fade" role="dialog">
                <div class="modal-dialog  modal-lg">
                    <!-- Modal content-->
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title">Project Details</h4>
                        </div>
                        <div class="modal-body">
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
                                </div>
                                <div class="col-xs-12 text-center">
                                    <h4>Labour Summary</h4>
                                </div>
                                <div class="col-xs-12">
                                    <asp:Table ID="tblLabourSummary" CssClass="table table-condensed" runat="server"></asp:Table>
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
                        <div class="modal-footer text-center">
                            <button type="button" data-toggle="tooltip" data-container="body" title="Print"  class="btn btn-default"><span class="glyphicon glyphicon-print" ></span></button>
                            <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="lbProjectResults" EventName="SelectedIndexChanged" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
