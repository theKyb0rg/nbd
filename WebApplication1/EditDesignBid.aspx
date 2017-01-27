<%@ Page Title="" Language="C#" MasterPageFile="~/NBDCostTrackingSystem.Master" AutoEventWireup="true" CodeBehind="EditDesignBid.aspx.cs" Inherits="NBDCostTrackingSystem.EditDesignBid1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">NBD - Edit Design Bid</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">
    <div class="container">
        <asp:Panel ID="results" runat="server" Visible="true">
            <div class="row">
                <div class="col-xs-12">
                    <h1 class="text-center">Edit a Design Bid</h1>
                </div>
            </div>
        </asp:Panel>
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
                    <div class="col-xs-12 text-center">
                        <asp:Button ID="btnSaveBid" runat="server" CssClass="btn btn-default" Text="Save Project Information" OnClick="btnSaveBid_Click" ValidationGroup="projInfo" />
                    </div>
                    <div class="col-xs-12">
                        <h4>Materials Summary</h4>
                    </div>
                    <div class="col-xs-12">
                        <asp:Table ID="tblMaterialsSummary" CssClass="table table-condensed" runat="server"></asp:Table>
                        <br />
                    </div>
                    <div class="col-xs-12">
                        <h4>Add New Material<asp:EntityDataSource ID="materialsDataSource" runat="server" ConnectionString="name=EntitiesNBD" DefaultContainerName="EntitiesNBD" EnableFlattening="False" EntitySetName="MATERIALs" EntityTypeFilter="MATERIAL" Select="it.[matDesc], it.[ID]">
                            </asp:EntityDataSource>
                        </h4>
                    </div>
                    <div class="col-xs-12 col-md-5">
                        <div class="form-group">
                            <label for="ddlMaterialDescription">Item:</label>
                            <asp:DropDownList ID="ddlMaterialDescription" runat="server" CssClass="form-control" DataSourceID="materialsDataSource" DataTextField="matDesc" DataValueField="ID"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-xs-12 col-md-5">
                        <div class="form-group">
                            <label for="txtMaterialQuantity">Quantity:</label><asp:RequiredFieldValidator ID="valMatQtyReq" CssClass="text-danger" ControlToValidate="txtMaterialQuantity" runat="server" Display="Dynamic" ValidationGroup="newMat" ErrorMessage=" Please enter a quantity!"></asp:RequiredFieldValidator><asp:RangeValidator ID="valMatQty" CssClass="text-danger" Display="Dynamic" runat="server" ErrorMessage=" Please enter more than 0!" MinimumValue="1" MaximumValue="999" ValidationGroup="newMat" ControlToValidate="txtMaterialQuantity"></asp:RangeValidator>
                            <asp:TextBox ID="txtMaterialQuantity" runat="server" min="1" TextMode="Number" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-xs-12 col-md-2">
                        <br />
                        <asp:Button ID="btnMaterialAdd" runat="server" CssClass="btn btn-default" Text="Add New" OnClick="btnMaterialAdd_Click" ValidationGroup="newMat" />
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
                        <h4>Add New Labor Requirement</h4>
                        <asp:EntityDataSource ID="edsLaborType" runat="server" ConnectionString="name=EntitiesNBD" DefaultContainerName="EntitiesNBD" EnableFlattening="False" EntitySetName="WORKER_TYPE" Select="it.[ID], it.[wrkTypeDesc]"></asp:EntityDataSource>
                        <asp:EntityDataSource ID="edsLaborTask" runat="server" ConnectionString="name=EntitiesNBD" DefaultContainerName="EntitiesNBD" EnableFlattening="False" EntitySetName="TASKs" Select="it.[ID], it.[taskDesc]"></asp:EntityDataSource>
                    </div>
                    <div class="col-xs-12 col-md-3">
                        <div class="form-group">
                            <label for="ddlLaborType">Type:</label>
                            <asp:DropDownList ID="ddlLaborType" runat="server" CssClass="form-control" DataSourceID="edsLaborType" DataTextField="wrkTypeDesc" DataValueField="ID"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-xs-12 col-md-3">
                        <div class="form-group">
                            <label for="ddlLaborTask">Task:</label>
                            <asp:DropDownList ID="ddlLaborTask" runat="server" CssClass="form-control" DataSourceID="edsLaborTask" DataTextField="taskDesc" DataValueField="ID"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-xs-12 col-md-4">
                        <div class="form-group">
                            <label for="txtLaborHours">Hours:</label><asp:RequiredFieldValidator ID="valLabHoursReq" CssClass="text-danger" ControlToValidate="txtLaborHours" runat="server" Display="Dynamic" ValidationGroup="newLab" ErrorMessage=" Please enter hours!"></asp:RequiredFieldValidator><asp:RangeValidator ID="valLabHours" CssClass="text-danger" Display="Dynamic" runat="server" ErrorMessage=" Please enter more than 0 hours!" MinimumValue="1" MaximumValue="999" ValidationGroup="newLab" ControlToValidate="txtLaborHours"></asp:RangeValidator>
                            <asp:TextBox ID="txtLaborHours" runat="server" TextMode="Number" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-xs-12 col-md-2">
                        <br />
                        <asp:Button ID="btnAddLabor" runat="server" CssClass="btn btn-default" Text="Add New" OnClick="btnAddLabor_Click" ValidationGroup="newLab" />
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
