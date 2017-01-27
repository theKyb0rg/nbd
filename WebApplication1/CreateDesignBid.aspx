<%@ Page Title="" Language="C#" MasterPageFile="~/NBDCostTrackingSystem.Master" AutoEventWireup="true" MaintainScrollPositionOnPostback="true" CodeBehind="CreateDesignBid.aspx.cs" Inherits="NBDCostTrackingSystem.AddDesignBid" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">NBD - Add a Design Bid</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">
    <%--CONTENT--%>
    <div class="container-fluid">
        <div class="row">
            <div class="col-xs-12">
                <h1 class="text-center">Design Bid</h1>
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
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label for="txtProjectName">Project Name:</label><asp:RequiredFieldValidator ID="valProjectName" CssClass="error" runat="server" ErrorMessage=" Enter a Project Name" ControlToValidate="txtProjectName" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                    <asp:TextBox ID="txtProjectName" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <label for="txtProjectBeginDate">Estimated Begin Date:</label><asp:RequiredFieldValidator ID="valBeginDate" CssClass="error" runat="server" ErrorMessage=" Choose Date" ControlToValidate="txtProjectBeginDate" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                    <asp:TextBox ID="txtProjectBeginDate" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label for="txtProjectSite">Project Site:</label><asp:RequiredFieldValidator ID="valProjectSite" CssClass="error" runat="server" ErrorMessage=" Enter a Project Site" ControlToValidate="txtProjectSite" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                    <asp:TextBox ID="txtProjectSite" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <label for="txtProjectEndDate">Estimated End Date:</label><asp:RequiredFieldValidator ID="valEndDate" CssClass="error" runat="server" ErrorMessage=" Choose Date" ControlToValidate="txtProjectEndDate" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                    <asp:TextBox ID="txtProjectEndDate" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
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
                                    <label for="txtNBDSales">Sales Associate:</label><asp:CompareValidator ID="valNBDSales" runat="server" ErrorMessage=" Select Sales Assc." ControlToValidate="ddlNBDSales" ValueToCompare="-1" Operator="NotEqual" SetFocusOnError="True"></asp:CompareValidator>
                                    <asp:DropDownList ID="ddlNBDSales" runat="server" CssClass="form-control" AppendDataBoundItems="True">
                                        <asp:ListItem Value="-1">Select a Sales Associate...</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group">
                                    <label for="txtNBDDesigner">Designer:</label><asp:CompareValidator ID="valNBDDesigner" runat="server" ErrorMessage=" Select Designer" ControlToValidate="ddlNBDDesigner" ValueToCompare="-1" Operator="NotEqual" SetFocusOnError="True"></asp:CompareValidator>
                                    <asp:DropDownList ID="ddlNBDDesigner" runat="server" CssClass="form-control" AppendDataBoundItems="True">
                                        <asp:ListItem Value="-1">Select a Designer...</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label for="txtNBDSalesPhone">Phone:</label>
                                    <asp:TextBox ID="txtNBDSalesPhone" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <label for="txtNBDDesignerPhone">Phone:</label>
                                    <asp:TextBox ID="txtNBDDesignerPhone" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <h2 class="text-center">Material Requirements</h2>
                <%--MATERIAL REQUIREMENTS--%>
                <div class="col-xs-12 col-md-6">
                    <div class="panel panel-default">
                        <div class="panel-heading text-center">
                            <h4>Add Materials</h4>
                        </div>
                        <div class="panel-body">
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label for="ddlMaterialType">Type:</label><asp:CompareValidator ID="valMatType" runat="server" ErrorMessage=" Select Type" ControlToValidate="ddlMaterialType" ValueToCompare="-1" Operator="NotEqual" SetFocusOnError="True" ValidationGroup="materialsGroup"></asp:CompareValidator>
                                    <asp:DropDownList ID="ddlMaterialType" runat="server" CssClass="form-control" AppendDataBoundItems="True" AutoPostBack="True" OnSelectedIndexChanged="ddlMaterialType_SelectedIndexChanged">
                                        <asp:ListItem Value="-1">Select a Material Type...</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label for="txtMaterialUnitPrice">Unit Price:</label>
                                    <div class="input-group">
                                        <span class="input-group-addon"><span class="glyphicon glyphicon-usd"></span></span>
                                        <asp:UpdatePanel ID="upPrice" runat="server">
                                            <ContentTemplate>
                                                <asp:TextBox ID="txtMaterialUnitPrice" runat="server" CssClass="form-control" Text="0.00"></asp:TextBox>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="ddlMaterialDescription" EventName="SelectedIndexChanged" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label for="txtMaterialExtendedPrice">Extended Price:</label>
                                    <div class="input-group">
                                        <span class="input-group-addon"><span class="glyphicon glyphicon-usd"></span></span>
                                        <asp:UpdatePanel ID="upMaterialExtendedPrice" runat="server">
                                            <ContentTemplate>
                                                <asp:TextBox ID="txtMaterialExtendedPrice" runat="server" CssClass="form-control" Text="0.00"></asp:TextBox>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="txtMaterialQuantity" EventName="TextChanged" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <asp:UpdatePanel ID="upMaterialDescription" runat="server">
                                        <ContentTemplate>
                                            <label for="ddlMaterialDescription">Item:</label><asp:CompareValidator ID="valItem" runat="server" ErrorMessage=" Select Item" ControlToValidate="ddlMaterialDescription" ValueToCompare="-1" Operator="NotEqual" SetFocusOnError="True" ValidationGroup="materialsGroup"></asp:CompareValidator>
                                            <asp:DropDownList ID="ddlMaterialDescription" runat="server" CssClass="form-control" AutoPostBack="True" AppendDataBoundItems="True" OnSelectedIndexChanged="ddlMaterialDescription_SelectedIndexChanged">
                                                <asp:ListItem Value="-1">Material type not selected.</asp:ListItem>
                                            </asp:DropDownList>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="ddlMaterialType" EventName="SelectedIndexChanged" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label for="txtMaterialQuantity">Quantity:</label><asp:RangeValidator ID="valQuantity" runat="server" CssClass="error" ErrorMessage=" Please Enter More than 0" ControlToValidate="txtMaterialQuantity" MinimumValue="1" MaximumValue="999" ValidationGroup="materialsGroup" SetFocusOnError="True"></asp:RangeValidator>
                                    <asp:TextBox ID="txtMaterialQuantity" runat="server" CssClass="form-control" min="0" TextMode="Number" Text="0" OnTextChanged="txtMaterialQuantity_TextChanged" AutoPostBack="True"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label for="ddlMaterialSize">Size:</label>
                                    <asp:UpdatePanel ID="upMaterialySize" runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="ddlMaterialSize" runat="server" CssClass="form-control">
                                                <asp:ListItem Value="-1">Item not selected.</asp:ListItem>
                                            </asp:DropDownList>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="ddlMaterialDescription" EventName="SelectedIndexChanged" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                            <div class="col-xs-12 text-center">
                                <hr />
                                <asp:LinkButton ID="btnMaterialAddNew" CssClass="btn btn-default" runat="server" OnClick="btnMaterialAddNew_Click" ValidationGroup="materialsGroup"><span class="glyphicon glyphicon-plus"></span>&nbsp;Add</asp:LinkButton>
                                <asp:LinkButton ID="btnMaterialClear" CssClass="btn btn-default" runat="server" OnClick="btnMaterialClear_Click"><span class="glyphicon glyphicon-minus"></span>&nbsp;Clear</asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-xs-12 col-md-6">
                    <div class="panel panel-default">
                        <div class="panel-heading text-center panelHeaderFix">
                            <h4>Materials Summary</h4>
                        </div>
                        <div class="panel-body panelBodyFix">
                            <asp:UpdatePanel ID="upMaterialsSummary" runat="server">
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnMaterialAddNew" EventName="Click"></asp:AsyncPostBackTrigger>
                                </Triggers>
                                <ContentTemplate>
                                    <asp:Panel ID="pnlDeleteMaterialRowControls" runat="server" Visible="false">
                                        <div class="col-xs-12 text-center">
                                            <div class="form-inline">
                                                <label for="ddlDeleteMaterialFromTable">Delete Row:</label>
                                                &nbsp;
                                                <asp:DropDownList ID="ddlDeleteMaterialFromTable" runat="server" CssClass="form-control" AppendDataBoundItems="True">
                                                    <asp:ListItem Value="-1">Delete All Rows</asp:ListItem>
                                                </asp:DropDownList>
                                                &nbsp;
                                            <asp:LinkButton ID="btnMaterialRemove" CssClass="btn btn-default" runat="server" OnClick="btnMaterialRemove_Click"><span class="glyphicon glyphicon-minus"></span>&nbsp;Remove</asp:LinkButton>
                                            </div>
                                        </div>
                                    </asp:Panel>
                                    <div class="col-xs-12 text-center">
                                        <br />
                                        <asp:GridView ID="gvMaterialsSummary" CssClass="table table-condensed" runat="server" GridLines="Horizontal" BorderStyle="None" RowStyle-HorizontalAlign="Left" OnRowDataBound="gvMaterialsSummary_RowDataBound">
                                            <RowStyle HorizontalAlign="Left"></RowStyle>
                                        </asp:GridView>
                                        <asp:Label ID="lblMaterialsSummary" runat="server" Text="No materials currently added."></asp:Label>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
                <%--LABOR REQUIREMENTS--%>
                <h2 class="text-center">Labor Requirements</h2>
                <div class="col-xs-12 col-md-6">
                    <div class="panel panel-default">
                        <div class="panel-heading text-center">
                            <h4>Add Labor</h4>
                        </div>
                        <div class="panel-body">
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label for="ddlLaborType">Type:</label><asp:CompareValidator ID="valLaborType" runat="server" ErrorMessage=" Select Type" ControlToValidate="ddlLaborType" ValueToCompare="-1" Operator="NotEqual" SetFocusOnError="True" ValidationGroup="labourGroup"></asp:CompareValidator>
                                    <asp:DropDownList ID="ddlLaborType" runat="server" CssClass="form-control" AppendDataBoundItems="True" OnSelectedIndexChanged="ddlLaborType_SelectedIndexChanged" AutoPostBack="True">
                                        <asp:ListItem Value="-1">Select a Worker Type...</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label for="txtLaborUnitPrice">Unit Price:</label>
                                    <div class="input-group">
                                        <span class="input-group-addon"><span class="glyphicon glyphicon-usd"></span></span>
                                        <asp:UpdatePanel ID="upLaborUnitPrice" runat="server">
                                            <ContentTemplate>
                                                <asp:TextBox ID="txtLaborUnitPrice" runat="server" CssClass="form-control" TextMode="Number" Text="0.00"></asp:TextBox>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="ddlLaborType" EventName="SelectedIndexChanged" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label for="txtLaborExtendedPrice">Extended Price:</label>
                                    <div class="input-group">
                                        <span class="input-group-addon"><span class="glyphicon glyphicon-usd"></span></span>
                                        <asp:UpdatePanel ID="upLaborExtendedPrice" runat="server">
                                            <ContentTemplate>
                                                <asp:TextBox ID="txtLaborExtendedPrice" runat="server" CssClass="form-control" TextMode="Number" Text="0.00"></asp:TextBox>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="txtLaborHours" EventName="TextChanged" />
                                            </Triggers>
                                        </asp:UpdatePanel>

                                    </div>
                                </div>
                            </div>
                            <div class="col-md-8">
                                <div class="form-group">
                                    <asp:UpdatePanel ID="upLaborTask" runat="server">
                                        <ContentTemplate>
                                            <label for="ddlLaborTask">Task:</label><asp:CompareValidator ID="valLaborTask" runat="server" ErrorMessage=" Select Task" ControlToValidate="ddlLaborTask" ValueToCompare="-1" Operator="NotEqual" SetFocusOnError="True" ValidationGroup="labourGroup"></asp:CompareValidator>
                                            <asp:DropDownList ID="ddlLaborTask" runat="server" CssClass="form-control" AppendDataBoundItems="True">
                                                <asp:ListItem Value="-1">Select a Task...</asp:ListItem>
                                            </asp:DropDownList>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="ddlLaborType" EventName="SelectedIndexChanged" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label for="txtLaborHours">Hours:</label><asp:RangeValidator ID="valHours" runat="server" CssClass="error" ErrorMessage=" Please Enter More Hours Than 0" ControlToValidate="txtLaborHours" MinimumValue="1" MaximumValue="99" ValidationGroup="labourGroup" Font-Bold="True" SetFocusOnError="True"></asp:RangeValidator>
                                    <asp:TextBox ID="txtLaborHours" runat="server" CssClass="form-control" TextMode="Number" Text="0" OnTextChanged="txtLaborHours_TextChanged" AutoPostBack="true"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-xs-12 text-center">
                                <hr />
                                <asp:LinkButton ID="btnLaborAddNew" CssClass="btn btn-default" runat="server" OnClick="btnLaborAddNew_Click" ValidationGroup="labourGroup"><span class="glyphicon glyphicon-plus"></span>&nbsp;Add</asp:LinkButton>
                                <asp:LinkButton ID="btnLaborClear" CssClass="btn btn-default" runat="server" OnClick="btnLaborClear_Click"><span class="glyphicon glyphicon-minus"></span>&nbsp;Clear</asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-xs-12 col-md-6">
                    <div class="panel panel-default">
                        <div class="panel-heading text-center panelHeaderFix">
                            <h4>Labor Summary</h4>
                        </div>
                        <div class="panel-body text-center panelBodyFix">
                            <asp:UpdatePanel ID="upPanelLaborSummary" runat="server">
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnLaborAddNew" EventName="Click" />
                                </Triggers>
                                <ContentTemplate>
                                    <asp:Panel ID="pnlLaborDeleteControls" runat="server" Visible="false">
                                        <div class="col-xs-12 text-center">
                                            <div class="form-inline">
                                                <label for="ddlDeleteLaborFromTable">Delete Row:</label>
                                                &nbsp;
                                                <asp:DropDownList ID="ddlDeleteLaborFromTable" runat="server" CssClass="form-control" AppendDataBoundItems="True">
                                                    <asp:ListItem Value="-1">Delete All Rows</asp:ListItem>
                                                </asp:DropDownList>
                                                &nbsp;
                                            <asp:LinkButton ID="btnLaborDeleteRow" CssClass="btn btn-default" runat="server" OnClick="btnLaborDeleteRow_Click"><span class="glyphicon glyphicon-minus"></span>&nbsp;Remove</asp:LinkButton>
                                            </div>
                                        </div>
                                    </asp:Panel>
                                    <div class="col-xs-12 text-center">
                                        <br />
                                        <asp:GridView ID="gvLaborSummary" CssClass="table table-condensed" runat="server" GridLines="Horizontal" BorderStyle="None" RowStyle-HorizontalAlign="Left" OnRowDataBound="gvLaborSummary_RowDataBound"></asp:GridView>
                                        <asp:Label ID="lblLaborSummary" runat="server" Text="No labor information currently added."></asp:Label>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
                <div class="col-xs-12 col-md-4"></div>
                <div class="col-xs-12 col-md-4">
                    <div class="panel panel-default">
                        <div class="panel-heading text-center">
                            <h4>Bid Amount</h4>
                        </div>
                        <div class="panel-body">
                            <div class="form-group">
                                <label for="txtBidAmount">Bid Amount:</label>
                                <div class="input-group">
                                    <span class="input-group-addon"><span class="glyphicon glyphicon-usd"></span></span>
                                    <asp:UpdatePanel ID="upBidAmount" runat="server">
                                        <ContentTemplate>
                                            <asp:TextBox ID="txtBidAmount" runat="server" CssClass="form-control" Text="0.00"></asp:TextBox>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-xs-12 col-md-4"></div>
                <div class="col-xs-12 text-center">
                    <asp:Button ID="btnDesignBidCreate" runat="server" CssClass="btn btn-default" Text="Create" OnClick="btnDesignBidCreate_Click" />
                    <asp:Button ID="btnDesignBidCancel" runat="server" CssClass="btn btn-default" Text="Cancel" OnClick="btnDesignBidCancel_Click" CausesValidation="False" />
                </div>
            </div>
            <div class="col-xs-12 footer"></div>
        </div>
    </div>
    <asp:Label ID="lblFocusHAX" runat="server" Text="HAX FOR POSTBACK BRA" Visible="false"></asp:Label>
    <asp:EntityDataSource ID="edsClients" runat="server" ConnectionString="name=EntitiesNBD" DefaultContainerName="EntitiesNBD" EnableFlattening="False" EntitySetName="CLIENTs" Select="it.[ID], it.[cliName]"></asp:EntityDataSource>
    <asp:EntityDataSource ID="edsMaterialTypes" runat="server" ConnectionString="name=EntitiesNBD" DefaultContainerName="EntitiesNBD" EnableFlattening="False" EntitySetName="MATERIALs" Select="it.[matType], it.[ID]"></asp:EntityDataSource>
</asp:Content>
