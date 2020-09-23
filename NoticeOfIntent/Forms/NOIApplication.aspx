<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/CLFNested.master" CodeBehind="NOIApplication.aspx.vb" MaintainScrollPositionOnPostback="true" Inherits="NoticeOfIntent.NOIApplication" %>

<%@ Register Src="../UserControls/ucNameAddressInfo.ascx" TagName="ucNameAddressInfo" TagPrefix="uc1" %>
<%@ Register Src="../UserControls/ucTaxParcelAdd.ascx" TagName="ucTaxParcelAdd" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="NestedContentPlaceHolderHeader" runat="server">
    <%--<link rel="stylesheet" href="https://js.arcgis.com/3.23/esri/css/esri.css"/>--%>
    <link rel="stylesheet" href="https://js.arcgis.com/4.8/esri/css/main.css"/>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NestedContentPlaceHolderPageTitle" runat="server">
    <div class="well text-center font-bold">
        <asp:Label ID="lblNOIHeading" runat="server" Text="Notice of Intent (NOI) for Storm Water Discharges Associated With CONSTRUCTION ACTIVITY Under a NPDES General Permit" CssClass="h1"></asp:Label>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="NestedContentPlaceHolderBody" runat="server">
    <div>
        <asp:HiddenField ID="hfNOISubmissionID" Value="" runat="server" ClientIDMode="Static" />
        <asp:HiddenField ID="hfIsLocked" Value="N" runat="server" ClientIDMode="Static" />
        <asp:HiddenField ID="hfSiteInfo" runat="server" ClientIDMode="Static" />
        <asp:HiddenField ID="hfTaxParcel" runat="server" ClientIDMode="Static" />
        <asp:HiddenField ID="hfPiTypeID" runat="server" ClientIDMode="Static" />
        <asp:Wizard ID="wzNOI" runat="server" CssClass="table table-bordered" DisplayCancelButton="True" >
            <SideBarStyle CssClass="col-md-3" />
            <NavigationButtonStyle CssClass="btn btn-default" />
            <WizardSteps>
                <asp:WizardStep ID="wsApplicantInfo" runat="server" Title="Applicant Information" StepType="Start">
                    <div class="panel-group" id="appinfoaccordion">
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <div data-toggle="collapse" data-target="#ownerdetails" class="panel-title font-bold cursorpointer" >Owner Information<span class="pull-right glyphicon glyphicon-chevron-up"></span></div>
                            </div>
                            <div id="ownerdetails" class="panel-collapse collapse in">
                                <div class="panel-body input-group-sm">
                                    <uc1:ucNameAddressInfo ID="ucnaOwnerInfo" runat="server" controlname="Owner Information"  countymunicipalityvisible="false" ValidationGroup="ValidateNOI" />
                                </div>
                            </div>
                        </div>
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <div data-toggle="collapse" data-target="#contactdetails" class="panel-title font-bold cursorpointer">
                                    <asp:Label ID="lblContactInfoHeading" runat="server" Text="Contact Information"></asp:Label><span class="pull-right glyphicon glyphicon-chevron-down"></span></div>
                            </div>
                            <div id="contactdetails" class="panel-collapse collapse">
                                <div class="panel-body input-group-sm">
                                    <div class="form-inline pull-right">
                                        <div class="form-group form-group-sm">
                                            <asp:DropDownList ID="ddlCopyContactInfoFrom" runat="server" CssClass="form-control" ClientIDMode="Static">
                                                <asp:ListItem Text="Owner Information" Value="O"></asp:ListItem>
                                                <asp:ListItem Text="Billing Information" Value="B"></asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:Button ID="btnCopyContactInfo" runat="server" Text="Copy" ClientIDMode="Static" CausesValidation="false" />
                                        </div>
                                    </div>
                                    <div class="clearfix"></div>
                                    <uc1:ucNameAddressInfo ID="ucnaContactInfo" runat="server" controlname="Contact Information"  companytypevisible="false" ValidateCompanyName="false" countymunicipalityvisible="false" ValidationGroup="ValidateNOI" CompanyNameLabel="Company Name" />
                                </div>
                            </div>
                        </div>
                        <div id="divBilleeDetails" runat="server" class="panel panel-default">
                            <div class="panel-heading">
                                <div data-toggle="collapse" data-target="#billeedetails"  class="panel-title font-bold cursorpointer">Billing Information<span class="pull-right glyphicon glyphicon-chevron-down"></span></div>
                            </div>
                            <div id="billeedetails" class="panel-collapse collapse">
                                <div class="panel-body input-group-sm">
                                    <div class="form-inline pull-right">
                                        <div class="form-group form-group-sm">
                                            <asp:DropDownList ID="ddlCopyBilleeInfoFrom" runat="server" CssClass="form-control" ClientIDMode="Static">
                                                <asp:ListItem Text="Owner Information" Value="O"></asp:ListItem>
                                                <asp:ListItem Text="Contact Information" Value="C"></asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:Button ID="btnCopyBilleeInfo" runat="server" Text="Copy" ClientIDMode="Static" CausesValidation="false" />
                                        </div>
                                    </div>
                                    <div class="clearfix"></div>
                                    <uc1:ucNameAddressInfo ID="ucnaBilleeInfo" runat="server" controlname="Payee Information" companytypevisible="false" ValidateCompanyName="false" countymunicipalityvisible="false" ValidationGroup="ValidateNOI" CompanyNameLabel="Company Name" />
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:WizardStep>
                <asp:WizardStep ID="wsProjectInfo" runat="server" Title="Project Information" StepType="Step">
                    <div class="panel-group" id="projectinfoaccordion">
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <div data-toggle="collapse" data-target="#siteinfo" class="panel-title font-bold cursorpointer">
                                        <asp:Label ID="lblSiteInfoPnlHeading" runat="server" Text="Site Information"></asp:Label><span class="pull-right glyphicon glyphicon-chevron-up"></span></div>                            </div>
                            <div id="siteinfo" class="panel-collapse collapse in">
                                <div class="panel-body input-group-sm">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <uc1:ucNameAddressInfo ID="ucnaSiteInfo" runat="server" controlname="Site Information" usercontroltype="project" allowDEstateonly="True" companytypevisible="false" emailvisible="false" personnamevisible="false" address2visible="false" phonevisible="false" CompanyNameLabel="Project Name *" Address1Label="Project Location/Address *" ValidationGroup="ValidateNOI" />
                                        </div>
                                    </div>
                                    <div id="divProjectType" runat="server" class="row">
                                        <div class="col-md-6 input-group-sm">
                                            <asp:Label ID="lblProjectType" AssociatedControlID="ddlProjectType" runat="server" Text="Project Type" CssClass="control-label"></asp:Label>
                                            <asp:DropDownList ID="ddlProjectType" runat="server" CssClass="form-control" DataTextField="ProjectType" DataValueField="ProjectTypeID" ItemType="NoticeOfIntent.ProjectTypelst" SelectMethod="GetProjectType" ClientIDMode="Static">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-6 input-group-sm">
                                            <label class="control-label">Other</label>
                                            <asp:TextBox ID="txtProjectTypeOther" runat="server" CssClass="form-control" ClientIDMode="Static"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvProjectTypeOther" runat="server" ControlToValidate="txtProjectTypeOther" Display="Dynamic" EnableClientScript="true" CssClass="alert-text" ErrorMessage="Other Project type is required." ToolTip="Other Project type is required." ValidationGroup="ValidateNOI">*</asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div id="divTaxParcel" runat="server" class="panel panel-default">
                            <div class="panel-heading">
                                <div data-toggle="collapse" data-target="#taxparceldiv" class="panel-title font-bold cursorpointer">Tax Parcel Information<span class="pull-right glyphicon glyphicon-chevron-up"></span></div>
                            </div>
                            <div id="taxparceldiv" class="panel- panel-collapse collapse in">
                                <div class="panel-body input-group-sm table-responsive">
                                    <asp:ListView ID="lvTaxparcel" DataKeyNames="SubmissionTaxParcelID,SubmissionID,TaxParcelNumber" ItemType="NoticeOfIntent.NOISubmissionTaxParcels" SelectMethod="GetTaxParcelLst" runat="server" InsertItemPosition="LastItem" InsertMethod="lvTaxparcel_InsertItem" DeleteMethod="lvTaxparcel_DeleteItem">
                                        <LayoutTemplate>
                                            <table class="table table-bordered table-hover table-condensed">
                                                <thead>
                                                    <tr>
                                                        <th >Tax Parcel Number</th>
                                                        <th >County</th>
                                                        <th >Action</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr runat="server" id="itemPlaceholder" />
                                                </tbody>
                                            </table>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblTaxParcelNumber" runat="server" Text='<%#: Item.TaxParcelNumber%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblcounty" runat="server" Text='<%#: Item.TaxParcelCounty%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:ImageButton ID="ibtnDelete" AlternateText="Delete" runat="server" CssClass="btn-xs" CommandName="Delete" CommandArgument="<%#: Item.SubmissionTaxParcelID %>" ImageUrl="~/Content/images/Delete.png" ToolTip="Delete the taxparcel" OnClientClick="javascript:return confirm('Are you sure to delete?')" CausesValidation="false" />
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                        <AlternatingItemTemplate>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblTaxParcelNumber" runat="server" Text='<%#: Item.TaxParcelNumber%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblcounty" runat="server" Text='<%#: Item.TaxParcelCounty%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:ImageButton ID="ibtnDelete" AlternateText="Delete" runat="server" CssClass="btn-xs" CommandName="Delete" CommandArgument="<%#: Item.SubmissionTaxParcelID %>" ImageUrl="~/Content/images/Delete.png" ToolTip="Delete the taxparcel" OnClientClick="javascript:return confirm('Are you sure to delete?')" CausesValidation="false" />
                                                </td>
                                            </tr>
                                        </AlternatingItemTemplate>
                                        <InsertItemTemplate>
                                            <tr class="insertfooter">
                                                <td colspan="3" class="panel-group">
                                                    <!--<div id="countyselection" class="panel-group">-->
                                                    <div class="panel panel-default">
                                                        <div class="panel panel-heading">
                                                            <a data-toggle="collapse" href="#pnltaxparcelbody">
                                                                <h6 class="panel-title font-bold">Click here to add Tax Parcel<span class="pull-right glyphicon glyphicon-chevron-down"></span></h6>
                                                            </a>
                                                        </div>
                                                        <div id="pnltaxparcelbody" class="panel-collapse collapse">
                                                            <div class="panel-body">
                                                                <uc2:ucTaxParcelAdd ID="ucTaxParcelAdd1" runat="server" SelectedCounty="<%# BindItem.TaxParcelCounty%>" FullTaxParcel="<%# BindItem.TaxParcelNumber %>" />
                                                                <br />
                                                                <div class="row col-md-12 col-md-offset-10">
                                                                    <asp:Button ID="btnInsert" runat="server" CommandName="Insert" CssClass="btn btn-default" CausesValidation="false" Text="Add Tax Parcel" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <!--</div>-->
                                                </td>
                                            </tr>
                                        </InsertItemTemplate>
                                        <EmptyDataTemplate>
                                            <table class="table-bordered">
                                                <thead>
                                                    <tr>
                                                        <th class="col-md-4">Tax Parcel Number</th>
                                                        <th class="col-md-4">County</th>
                                                        <th class="col-md-4">Action</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr>
                                                        <td colspan="3">No Tax Parcel available</td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </EmptyDataTemplate>
                                    </asp:ListView>
                                    <asp:CustomValidator ID="cvTaxparcel" runat="server" Display="Dynamic" EnableClientScript="true" CssClass="alert-text" ClientValidationFunction="cvTaxparcel_ClientValidation" ToolTip="At least one tax parcel needs to be entered." ErrorMessage="At least one tax parcel needs to be entered." ValidationGroup="ValidateNOI">*</asp:CustomValidator>
                                </div>
                            </div>
                        </div>
                        <div id="divLocDet" runat="server" class="panel panel-default">
                            <div class="panel-heading">
                                <div data-toggle="collapse" data-target="#siteloc" class="panel-title font-bold cursorpointer">Site Location<span class="pull-right glyphicon glyphicon-chevron-up"></span></div>
                            </div>
                            <div id="siteloc" class="panel-collapse collapse in">
                                <div class="panel-body input-group-sm">
<%--                                    <div class="form-inline">
                                        <div class="form-group">
                                            <label class="control-label">Select the Discharge Point:</label>
                                            <input id="btnMapPoint" runat="server" type="button" value="Map Point" />
                                            &nbsp;&nbsp;
                                        </div>
                                    </div>--%>
                                    <div class="row" style="height:100%; width: 100%;">
                                        <div id="mapDiv" style="height:400px; width: 100%;">
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-6 input-group-sm">
                                            <label class="control-label">Latitude: *</label>
                                            <asp:TextBox ID="txtLatitude" runat="server" CssClass="form-control" ClientIDMode="Static"></asp:TextBox>
                                            <asp:CompareValidator ID="CompareValidator1" ControlToValidate="txtLatitude" Type="Double" Operator="DataTypeCheck" Display="Dynamic" runat="server" ErrorMessage="Enter only numeric values for Latitude." ToolTip="Enter only numeric values for Latitude." ValidationGroup="ValidateNOI" CssClass="alert-text">*</asp:CompareValidator>
                                            <asp:RequiredFieldValidator ID="rfvLatitude" ControlToValidate="txtLatitude" runat="server" Display="Dynamic" CssClass="alert-text" ToolTip="Latitude is required" ErrorMessage="Latitude is required. Click Map to select a point." ValidationGroup="ValidateNOI">*</asp:RequiredFieldValidator>
                                            <asp:HiddenField ID="hfX" runat="server" ClientIDMode="Static" />
                                        </div>
                                        <div class="col-md-6 input-group-sm">
                                            <label class="control-label">Longitude: *</label>
                                            <asp:TextBox ID="txtLongitude" runat="server" CssClass="form-control" ClientIDMode="Static"></asp:TextBox>
                                            <asp:CompareValidator ID="CompareValidator2" ControlToValidate="txtLongitude" Type="Double" Operator="DataTypeCheck" Display="Dynamic" runat="server" ErrorMessage="Enter only numeric values for Longitude." ToolTip="Enter only numeric values for Longitude." ValidationGroup="ValidateNOI" CssClass="alert-text">*</asp:CompareValidator>
                                            <asp:RequiredFieldValidator ID="rfvLongitude" ControlToValidate="txtLongitude" runat="server" Display="Dynamic"  CssClass="alert-text" ToolTip="Longitude is required" ErrorMessage="Longitude is required. Click Map to select a point." ValidationGroup="ValidateNOI">*</asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="revLongitude" ControlToValidate="txtLongitude" runat="server" Display="Dynamic" CssClass="alert-text" ValidationExpression="^-\d+(\.\d{1,20})?$" ErrorMessage="Longitude has to be a negative value." ToolTip="Longitude has to be a negative value." ValidationGroup="ValidateNOI" >*</asp:RegularExpressionValidator>
                                            <asp:HiddenField ID="hfY" runat="server" ClientIDMode="Static" />
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-6 input-group-sm">
                                            <label class="control-label">Watershed: *</label>
                                            <asp:TextBox ID="txtWatershed" runat="server" CssClass="form-control  NOIReadonly" ClientIDMode="Static"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvWatershed" ControlToValidate="txtWatershed" runat="server" Display="Dynamic"  CssClass="alert-text" ToolTip="Watershed is required" ErrorMessage="Watershed is required. Click Map to select a point." ValidationGroup="ValidateNOI">*</asp:RequiredFieldValidator>
                                            <asp:HiddenField ID="hfWaterShedCode" runat="server" ClientIDMode="Static" />                                            
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div id="divbmp" runat="server" class="panel panel-default">
                            <div class="panel-heading">
                                <div data-toggle="collapse" data-target="#SWBMPdiv" class="panel-title font-bold">Stormwater Management Practices Proposed<span class="pull-right glyphicon glyphicon-chevron-up"></span></div>
                            </div>
                            <div id="SWBMPdiv" class="panel- panel-collapse collapse in">
                                <div class="panel-body input-group-sm table-responsive">
                                    <asp:ListView ID="lvSWBMP" DataKeyNames="SubmissionSWBMPID,SubmissionID,SWBMPID" ItemType="NoticeOfIntent.NOISubmissionSWBMP" SelectMethod="GetProjectSWBMPLst" runat="server" InsertItemPosition="LastItem" InsertMethod="lvSWBMP_InsertItem" DeleteMethod="lvSWBMP_DeleteItem">
                                        <LayoutTemplate>
                                            <table class="table table-bordered table-hover table-condensed">
                                                <thead>
                                                    <tr>
                                                        <th>BMP Name</th>
                                                        <th>BMP Other Name</th>
                                                        <th>Quantity</th>
                                                        <th>Action</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr runat="server" id="itemPlaceholder" />
                                                </tbody>
                                            </table>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblBMPName" runat="server" Text='<%#: Item.SWBMPlst.SWBMP%>'></asp:Label>
                                                    <asp:HiddenField ID="hfBMPID" runat="server" Value='<%#: Item.SWBMPID %>' />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblBMPOtherName" runat="server" Text='<%#: Item.SWBMPOtherName%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblQty" runat="server" Text='<%#: Item.SWBMPQty%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:ImageButton ID="ibtnDelete" AlternateText="Delete" runat="server" CssClass="btn-xs" CommandName="Delete" CommandArgument="<%#: Item.SubmissionSWBMPID %>" ImageUrl="~/Content/images/Delete.png" ToolTip="Delete the BMP" OnClientClick="javascript:return confirm('Are you sure to delete?')" CausesValidation="false" />
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                        <AlternatingItemTemplate>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblBMPName" runat="server" Text='<%#: Item.SWBMPlst.SWBMP%>'></asp:Label>
                                                    <asp:HiddenField ID="hfBMPID" runat="server" Value='<%#: Item.SWBMPID %>' />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblBMPOtherName" runat="server" Text='<%#: Item.SWBMPOtherName%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblQty" runat="server" Text='<%#: Item.SWBMPQty%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:ImageButton ID="ibtnDelete" AlternateText="Delete" runat="server" CssClass="btn-xs" CommandName="Delete" CommandArgument="<%#: Item.SubmissionSWBMPID %>" ImageUrl="~/Content/images/Delete.png" ToolTip="Delete the BMP" OnClientClick="javascript:return confirm('Are you sure to delete?')" CausesValidation="false" />
                                                </td>
                                            </tr>
                                        </AlternatingItemTemplate>
                                        <InsertItemTemplate>
                                            <tr class="insertfooter">
                                                <td>
                                                    <asp:DropDownList ID="ddlSWBMP" runat="server" CssClass="form-control" ItemType="NoticeOfIntent.SWBMPlst" SelectMethod="GetSWBMPLst" DataTextField="SWBMP" DataValueField="SWBMPID" ClientIDMode="Static" SelectedValue="<%#: BindItem.SWBMPID %>"></asp:DropDownList>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtBMPOtherName" runat="server" CssClass="form-control" Text="<%#: BindItem.SWBMPOtherName %>" ClientIDMode="Static"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvBMPOther" runat="server" ControlToValidate="txtBMPOtherName" Display="Dynamic" EnableClientScript="true" CssClass="alert-text" ErrorMessage="Other BMP Name is required." ToolTip="Other BMP Name is required." ValidationGroup="ValidateBMP">*</asp:RequiredFieldValidator>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtQty" runat="server" CssClass="form-control" Text="<%#: BindItem.SWBMPQty %>" ClientIDMode="Static"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rvQty" runat="server" ControlToValidate="txtQty" Display="Dynamic" EnableClientScript="true" CssClass="alert-text" ErrorMessage="BMP Quantity is required" ToolTip="BMP Quantity is required" ValidationGroup="ValidateBMP">*</asp:RequiredFieldValidator>
                                                </td>
                                                <td>
                                                    <asp:Button ID="btnInsertBMP" runat="server" CommandName="Insert" CssClass="btn btn-default" Text="Add BMP" ClientIDMode="Static" ValidationGroup="ValidateBMP" />
                                                </td>
                                            </tr>
                                        </InsertItemTemplate>
                                        <EmptyDataTemplate>
                                            <table class="table-bordered">
                                                <thead>
                                                    <tr>
                                                        <th>BMP Name</th>
                                                        <th>BMP Other Name</th>
                                                        <th>Quantity</th>
                                                        <th>Action</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr>
                                                        <td colspan="3">No BMP available</td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </EmptyDataTemplate>
                                    </asp:ListView>
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:WizardStep>
                <asp:WizardStep ID="wsProjectInfo1" runat="server" Title="Project Information (continued)" StepType="Finish" >
                    <div class="panel-group" id="projectinfo1accordion">
                        <div id="divOtherInfo" runat="server" class="panel panel-default">
                            <div class="panel-heading">
                                <div data-toggle="collapse" data-target="#otherinfo" class="panel-title font-bold">Other Information<span class="pull-right glyphicon glyphicon-chevron-up"></span></div>
                            </div>
                            <div id="otherinfo" class="panel-collapse collapse in">
                                <div class="panel-body input-group-sm">
                                    <div class="form-group input-group-sm">
                                        <label class="control-label">Has the Sediment & Stormwater / Storm Water Pollution Prevention Plan (SWPPP) been prepared? *</label>
                                        <asp:RadioButtonList ID="rblSWPPPYesNo" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" CssClass="radio" ClientIDMode="Static">
                                            <asp:ListItem Value="Yes" Text="Yes"></asp:ListItem>
                                            <asp:ListItem Value="No" Text="No"></asp:ListItem>
                                        </asp:RadioButtonList>
                                        <asp:RequiredFieldValidator ID="rfvSWPPPYesNo" ControlToValidate="rblSWPPPYesNo" runat="server" Display="Dynamic" EnableClientScript="true" CssClass="alert-text" ToolTip="Please select an option." ErrorMessage="Please select an option." ValidationGroup="ValidateNOI">*</asp:RequiredFieldValidator>
                                        <asp:CustomValidator ID="cvSWPPPYesNo" runat="server" ControlToValidate="rblSWPPPYesNo" EnableClientScript="true" Display="Dynamic" CssClass="alert-text" ErrorMessage="The Sediment and Stormwater Plan will contain the standard and specifications for pollution prevention practices which constitutes a SWPPP." ClientValidationFunction="cvSWPPPYesNo_ClientValidation" ValidationGroup="ValidateNOI"></asp:CustomValidator>
                                    </div>
                                    <div class="form-group input-group-sm">
                                        <asp:Label ID="lblPlanApprovalAgency" AssociatedControlID="ddlPlanApprovalAgency" runat="server" Text="Plan Approval Agency: *" CssClass="control-label"></asp:Label>
                                        <asp:DropDownList ID="ddlPlanApprovalAgency" runat="server" CssClass="form-control" DataTextField="DisplayName" DataValueField="PersonOrgID" ItemType="NoticeOfIntent.PlanApprovalAgency" SelectMethod="GetPlanApprovalAgency">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvPlanApprovalAgency" ControlToValidate="ddlPlanApprovalAgency" InitialValue="0" runat="server" Display="Dynamic" EnableClientScript="true" CssClass="alert-text" ToolTip="Plan Approval Agency is required." ErrorMessage="Plan Approval Agency is required." ValidationGroup="ValidateNOI">*</asp:RequiredFieldValidator>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-6 input-group-sm">
                                            <asp:Label ID="lblAreaOfSite" AssociatedControlID="txtTotalAreaOfSite" runat="server" Text="Total Land Area of Site (tenths of acres): *" CssClass="control-label"></asp:Label>
                                            <asp:TextBox ID="txtTotalAreaOfSite" runat="server" MaxLength="18" CssClass="form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvTotalAreaOfSite" ControlToValidate="txtTotalAreaOfSite" runat="server" Display="Dynamic" EnableClientScript="true" CssClass="alert-text" ToolTip="Total Land Area of Site is required" ErrorMessage="Total Land Area of Site is required" ValidationGroup="ValidateNOI">*</asp:RequiredFieldValidator>
                                            <asp:CompareValidator ID="covtxtTotalAreaOfSite" runat="server" ControlToValidate="txtTotalAreaOfSite" Type="Double" Operator="DataTypeCheck" Display="Dynamic" EnableClientScript="true" CssClass="alert-text" ErrorMessage="Only numbers are allowed" ToolTip="Only numbers are allowed" ValidationGroup="ValidateNOI">*</asp:CompareValidator>
                                        </div>
                                        <div class="col-md-6 input-group-sm">
                                            <asp:Label ID="lblAreaOfDisturbed" AssociatedControlID="txtAreaOfDisturbed" runat="server" Text="Est. Area to be Disturbed (tenths of acres): *" CssClass="control-label"></asp:Label>
                                            <asp:TextBox ID="txtAreaOfDisturbed" runat="server" MaxLength="18" CssClass="form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvAreaOfDisturbed" ControlToValidate="txtAreaOfDisturbed" runat="server" Display="Dynamic" EnableClientScript="true" CssClass="alert-text" ToolTip="Estimated Area to be Disturbed is required" ErrorMessage="Estimated Area to be Disturbed is required" ValidationGroup="ValidateNOI">*</asp:RequiredFieldValidator>
                                            <asp:CompareValidator ID="covtxtAreaofDisturbed" runat="server" ControlToValidate="txtAreaOfDisturbed" Type="Double" Operator="DataTypeCheck" Display="Dynamic" EnableClientScript="true" CssClass="alert-text" ErrorMessage="Only numbers are allowed" ToolTip="Only numbers are allowed" ValidationGroup="ValidateNOI">*</asp:CompareValidator>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-6 input-group-sm">
                                            <asp:Label ID="lblConstructStartDate" AssociatedControlID="txtConstructStartDate" runat="server" Text="Est. Construction Start Date: *" CssClass="control-label"></asp:Label>
                                            <asp:TextBox ID="txtConstructStartDate" runat="server" CssClass="form-control datepicker" MaxLength="10"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvConstructStartDate" ControlToValidate="txtConstructStartDate" runat="server" Display="Dynamic" EnableClientScript="true" CssClass="alert-text" ToolTip="Construction Start Date is required" ErrorMessage="Construction Start Date is required" ValidationGroup="ValidateNOI">*</asp:RequiredFieldValidator>
                                        </div>
                                        <div class="col-md-6 input-group-sm">
                                            <asp:Label ID="lblConstructCompleteDate" AssociatedControlID="txtConstructCompleteDate" runat="server" Text="Est. Construction Completion Date: *" CssClass="control-label"></asp:Label>
                                            <asp:TextBox ID="txtConstructCompleteDate" runat="server" CssClass="form-control datepicker" MaxLength="10"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvConstructCompleteDate" ControlToValidate="txtConstructCompleteDate" runat="server" Display="Dynamic" EnableClientScript="true" CssClass="alert-text" ToolTip="Construction Completion Date is required" ErrorMessage="Construction Completion Date is required" ValidationGroup="ValidateNOI">*</asp:RequiredFieldValidator>
                                            <asp:CompareValidator ID="cvConstructCompleteDate" ControlToValidate="txtConstructCompleteDate" Type="Date" ControlToCompare="txtConstructStartDate" Display="Dynamic" runat="server" EnableClientScript="true" CssClass="alert-text" Operator="GreaterThan" ErrorMessage="Entered date should be greater than Construction Start Date." ToolTip="Entered date should be greater than Construction Start Date." ValidationGroup="ValidateNOI">*</asp:CompareValidator>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div id="divSicCodes" runat="server" class="panel panel-default">
                            <div class="panel-heading">
                                <div data-toggle="collapse" data-target="#SICdiv" class="panel-title font-bold cursorpointer">SIC Codes<span class="pull-right glyphicon glyphicon-chevron-up"></span></div>
                            </div>
                            <div id="SICdiv" class="panel- panel-collapse collapse in">
                                <div class="panel-body input-group-sm table-responsive">
                                    <asp:ListView ID="lvSIC" DataKeyNames="SubmissionSICID,SubmissionID,SICCode" ItemType="NoticeOfIntent.NOISubmissionSIC" SelectMethod="GetSubmissionSICCodes" runat="server" InsertItemPosition="LastItem" InsertMethod="lvSIC_InsertItem" DeleteMethod="lvSIC_DeleteItem">
                                        <LayoutTemplate>
                                            <table class="table table-bordered table-hover table-condensed">
                                                <thead>
                                                    <tr>
                                                        <th>SIC Code</th>
                                                        <th>Rank</th>
                                                        <th>Action</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr runat="server" id="itemPlaceholder" />
                                                </tbody>
                                            </table>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblSICCode" runat="server" Text='<%#: Item.SICCode%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblRank" runat="server" Text='<%#: Item.RankCode%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:ImageButton ID="ibtnDelete" AlternateText="Delete" runat="server" CssClass="btn-xs" CommandName="Delete"  ImageUrl="~/Content/images/Delete.png" ToolTip="Delete the BMP" OnClientClick="javascript:return confirm('Are you sure to delete?')" CausesValidation="false" />
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                        <AlternatingItemTemplate>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblSICCode" runat="server" Text='<%#: Item.SICCode%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblRank" runat="server" Text='<%#: Item.RankCode%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:ImageButton ID="ibtnDelete" AlternateText="Delete" runat="server" CssClass="btn-xs" CommandName="Delete"  ImageUrl="~/Content/images/Delete.png" ToolTip="Delete the BMP" OnClientClick="javascript:return confirm('Are you sure to delete?')" CausesValidation="false" />
                                                </td>
                                            </tr>
                                        </AlternatingItemTemplate>
                                        <InsertItemTemplate>
                                            <tr class="insertfooter">
                                                <td>
                                                    <asp:TextBox ID="txtSICCode" runat="server" CssClass="form-control" Text="<%#: BindItem.SICCode %>" ClientIDMode="Static"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvSICCode" runat="server" ControlToValidate="txtSICCode" Display="Dynamic" EnableClientScript="true" CssClass="alert-text" ErrorMessage="SIC Code is required." ToolTip="SIC Code is required." ValidationGroup="ValidateSIC">*</asp:RequiredFieldValidator>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlSICCodeRank" runat="server" CssClass="form-control" ClientIDMode="Static" SelectedValue="<%#: BindItem.RankCode %>">
                                                        <asp:ListItem Text="--select--" Value="0"></asp:ListItem>
                                                        <asp:ListItem Text="Primary" Value="P"></asp:ListItem>
                                                        <asp:ListItem Text="Secondary" Value="O"></asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvSICCodeRank" runat="server" ControlToValidate="ddlSICCodeRank" Display="Dynamic" EnableClientScript="true" CssClass="alert-text" InitialValue="0" ErrorMessage="Rank Code is required" ToolTip="Rank Code is required" ValidationGroup="ValidateSIC">*</asp:RequiredFieldValidator>
                                                </td>
                                                <td>
                                                    <asp:Button ID="btnInsertSIC" runat="server" CommandName="Insert" CssClass="btn btn-default" Text="Add SIC Code" ClientIDMode="Static" ValidationGroup="ValidateSIC" />
                                                </td>
                                            </tr>
                                        </InsertItemTemplate>
                                        <EmptyDataTemplate>
                                            <table class="table-bordered">
                                                <thead>
                                                    <tr>
                                                        <th>SIC Code</th>
                                                        <th>Rank</th>
                                                        <th>Action</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr>
                                                        <td colspan="3">No SIC Code available</td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </EmptyDataTemplate>
                                    </asp:ListView>
                                </div>
                            </div>
                        </div>
                        <div id="divNAICSCodes" runat="server" class="panel panel-default">
                            <div class="panel-heading">
                                <div data-toggle="collapse" data-target="#NAICSdiv" class="panel-title font-bold cursorpointer">NAICS Codes<span class="pull-right glyphicon glyphicon-chevron-up"></span></div>
                            </div>
                            <div id="NAICSdiv" class="panel- panel-collapse collapse in">
                                <div class="panel-body input-group-sm table-responsive">
                                    <asp:ListView ID="lvNAICS" DataKeyNames="SubmissionNAICSID,SubmissionID,NAICSCode" ItemType="NoticeOfIntent.NOISubmissionNAICS" SelectMethod="GetSubmissionNAICSCodes" runat="server" InsertItemPosition="LastItem" InsertMethod="lvNAICS_InsertItem" DeleteMethod="lvNAICS_DeleteItem">
                                        <LayoutTemplate>
                                            <table class="table table-bordered table-hover table-condensed">
                                                <thead>
                                                    <tr>
                                                        <th>NAICS Code</th>
                                                        <th>Rank</th>
                                                        <th>Action</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr runat="server" id="itemPlaceholder" />
                                                </tbody>
                                            </table>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblNAICSCode" runat="server" Text='<%#: Item.NAICSCode%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblRank" runat="server" Text='<%#: Item.RankCode%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:ImageButton ID="ibtnDelete" AlternateText="Delete" runat="server" CssClass="btn-xs" CommandName="Delete"  ImageUrl="~/Content/images/Delete.png" ToolTip="Delete the BMP" OnClientClick="javascript:return confirm('Are you sure to delete?')" CausesValidation="false" />
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                        <AlternatingItemTemplate>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblNAICSCode" runat="server" Text='<%#: Item.NAICSCode%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblRank" runat="server" Text='<%#: Item.RankCode%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:ImageButton ID="ibtnDelete" AlternateText="Delete" runat="server" CssClass="btn-xs" CommandName="Delete"  ImageUrl="~/Content/images/Delete.png" ToolTip="Delete the BMP" OnClientClick="javascript:return confirm('Are you sure to delete?')" CausesValidation="false" />
                                                </td>
                                            </tr>
                                        </AlternatingItemTemplate>
                                        <InsertItemTemplate>
                                            <tr class="insertfooter">
                                                <td>
                                                    <asp:TextBox ID="txtNAICSCode" runat="server" CssClass="form-control" Text="<%#: BindItem.NAICSCode %>" ClientIDMode="Static"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvNAICSCode" runat="server" ControlToValidate="txtNAICSCode" Display="Dynamic" EnableClientScript="true" CssClass="alert-text" ErrorMessage="NAICS Code is required." ToolTip="NAICS Code is required." ValidationGroup="ValidateNAICS">*</asp:RequiredFieldValidator>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlNAICSCodeRank" runat="server" CssClass="form-control" ClientIDMode="Static" SelectedValue="<%#: BindItem.RankCode %>">
                                                        <asp:ListItem Text="--select--" Value="0"></asp:ListItem>
                                                        <asp:ListItem Text="Primary" Value="P"></asp:ListItem>
                                                        <asp:ListItem Text="Secondary" Value="O"></asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvNAICSCodeRank" runat="server" ControlToValidate="ddlNAICSCodeRank" Display="Dynamic" EnableClientScript="true" CssClass="alert-text" InitialValue="0" ErrorMessage="Rank Code is required" ToolTip="Rank Code is required" ValidationGroup="ValidateNAICS">*</asp:RequiredFieldValidator>
                                                </td>
                                                <td>
                                                    <asp:Button ID="btnInsertNAICS" runat="server" CommandName="Insert" CssClass="btn btn-default" Text="Add NAICS Code" ClientIDMode="Static" ValidationGroup="ValidateNAICS" />
                                                </td>
                                            </tr>
                                        </InsertItemTemplate>
                                        <EmptyDataTemplate>
                                            <table class="table-bordered">
                                                <thead>
                                                    <tr>
                                                        <th>NAICS Code</th>
                                                        <th>Rank</th>
                                                        <th>Action</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr>
                                                        <td colspan="3">No NAICS Code available</td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </EmptyDataTemplate>
                                    </asp:ListView>
                                </div>
                            </div>
                        </div>
                        <div id="divNoExposure" runat="server" class="panel panel-default">
                            <div class="panel-heading">
                                <div data-toggle="collapse" data-target="#NEdiv" class="panel-title font-bold cursorpointer">Conditional "No Exposure" Exclusion Checklist<span class="pull-right glyphicon glyphicon-chevron-up"></span></div>
                            </div>
                            <div id="NEdiv" class="panel- panel-collapse collapse in">
                                <div class="panel-body input-group-sm table-responsive">
                                    <div>
                                        <asp:CheckBox ID="chkDisableNoExposure" runat="server" AutoPostBack="true" Text="Disable No Exposure" CssClass="checkbox" ClientIDMode="Static" />
                                    </div>
                                    <div>
                                        <asp:ListView ID="lvNoExposure" DataKeyNames="SubmissionNEID,SubmissionID,NOExposureCLID" runat="server" SelectMethod="lvNoExposure_GetData">
                                            <LayoutTemplate>
                                                <table class="table table-bordered">
                                                    <thead>
                                                        <tr>
                                                            <th colspan="2" rowspan="2">Are any of the following materials or activities exposed to precipitation, now or in the foreseeable future? If you answer "Yes" to any of these questions, you are not eligible for the "No Exposure" exclusion.</th>
                                                            <th>Yes/No</th>
                                                        </tr>
                                                        <tr>
                                                            <th style="color:black;">
                                                                <asp:DropDownList ID="ddlResultAll" runat="server" CssClass="dropdown" AutoPostBack="true" OnSelectedIndexChanged="ddlResultAll_SelectedIndexChanged">
                                                                    <asp:ListItem Text="NA" Value=""></asp:ListItem>
                                                                    <asp:ListItem Text="Yes" Value="Y"></asp:ListItem>
                                                                    <asp:ListItem Text="No" Value="N"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <tr runat="server" id="itemPlaceholder" />
                                                    </tbody>
                                                </table>
                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblNOExposureCLID" runat="server" Text='<%# Eval("NOExposureCLID") %>'></asp:Label>
                                                        <asp:HiddenField ID="hfSubmissionNEID" runat="server" Value='<%# Eval("SubmissionNEID") %>' />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblQuestion" runat="server" Text='<%# Eval("Question") %>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlResult" runat="server" CssClass="dropdown noexposurecheck" SelectedValue='<%#Eval("Answer") %>'>
                                                            <asp:ListItem Text="NA" Value=""></asp:ListItem>
                                                            <asp:ListItem Text="Yes" Value="Y"></asp:ListItem>
                                                            <asp:ListItem Text="No" Value="N"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>
                                        <asp:CustomValidator ID="cvNoExposure" runat="server" Display="Dynamic"  EnableClientScript="true" CssClass="alert-text" ClientValidationFunction="cvNoExposure_ClientValidation" ToolTip="Not eligible for the No Exposure." ErrorMessage="Not eligible for the No Exposure." ValidationGroup="ValidateNOI" ClientIDMode="Static" ></asp:CustomValidator>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div id="divOutfall" runat="server" class="panel panel-default">
                            <div class="panel-heading">
                                <div data-toggle="collapse" data-target="#PermittedFeaturediv" class="panel-title font-bold cursorpointer">Outfall Collection<span class="pull-right glyphicon glyphicon-chevron-up"></span></div>
                            </div>
                            <div id="PermittedFeaturediv" class="panel panel-collapse collapse in">
                                <div class="panel-body input-group-sm table-responsive">
                                    <asp:ListView ID="lvOutfall" DataKeyNames="SubmissionUnitID,SubmissionID,UnitName" runat="server" ItemType="NoticeOfIntent.NOISubmissionUnit" SelectMethod="lvOutfall_GetData" InsertMethod="lvOutfall_InsertItem" DeleteMethod="lvOutfall_DeleteItem" InsertItemPosition="LastItem">
                                        <LayoutTemplate>
                                            <table class="table table-bordered table-hover table-condensed">
                                                <thead>
                                                    <tr>
                                                        <th colspan="2">Outfall Name</th>
                                                        <th>Latitude</th>
                                                        <th>Longitude</th>
                                                        <th>Watershed</th>
                                                        <th>Action</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr runat="server" id="itemPlaceholder" />
                                                </tbody>
                                            </table>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td colspan="2"><%#: Item.UnitName  %></td>
                                                <td><%#: Item.NOILoc.Latitude %></td>
                                                <td><%#: Item.NOILoc.Longitude %></td>
                                                <td><%#: Item.NOILoc.Watershed %></td>
                                                <td>
                                                    <asp:ImageButton ID="ibtnDelete" AlternateText="Delete" runat="server" CssClass="btn-xs" CommandName="Delete" ImageUrl="~/Content/images/Delete.png"  ToolTip="Delete the outfall" OnClientClick="javascript:return confirm('Are you sure to delete?')" CausesValidation="false" /></td>
                                            </tr>
                                        </ItemTemplate>
                                        <AlternatingItemTemplate>
                                            <tr>
                                                <td colspan="2"><%#: Item.UnitName  %></td>
                                                <td><%#: Item.NOILoc.Latitude %></td>
                                                <td><%#: Item.NOILoc.Longitude %></td>
                                                <td><%#: Item.NOILoc.Watershed %></td>
                                                <td>
                                                    <asp:ImageButton ID="ibtnDelete" AlternateText="Delete" runat="server" CssClass="btn-xs" CommandName="Delete" ImageUrl="~/Content/images/Delete.png"  ToolTip="Delete the outfall" OnClientClick="javascript:return confirm('Are you sure to delete?')" CausesValidation="false" /></td>
                                            </tr>
                                        </AlternatingItemTemplate>
                                        <InsertItemTemplate>
                                            <tr class="insertfooter">
                                                <td>
                                                    <asp:TextBox ID="txtOutfallName" runat="server" ClientIDMode="Static" CssClass="form-control" Text="<%#: BindItem.UnitName %>"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvOutfallName" ControlToValidate="txtOutfallName" runat="server" ErrorMessage="Outfall name is required" Display="Dynamic" CssClass="alert-text" ToolTip="Outfall name is required" ValidationGroup="OutfallValidationGrp">*</asp:RequiredFieldValidator>
                                                </td>
                                                <td>
                                                    <input id="btnMapOutfallPoint" runat="server" type="button" value="Map Point" causesvalidation="false" />
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtoutfallLatitude" runat="server" ClientIDMode="Static" CssClass="form-control" Text="<%#: BindItem.NOILoc.Latitude %>"></asp:TextBox>
                                                    <asp:HiddenField ID="hfoutfallX" runat="server" ClientIDMode="Static" Value="<%#: BindItem.NOILoc.X %>" />
                                                    <asp:RequiredFieldValidator ID="rfvoutfallLatitude" ControlToValidate="txtoutfallLatitude" runat="server" CssClass="alert-text" ErrorMessage="Latitude is required. Click Map Point button to select a point." Display="Dynamic" ToolTip="Latitude is required" ValidationGroup="OutfallValidationGrp">*</asp:RequiredFieldValidator>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtoutfallLongitude" runat="server" ClientIDMode="Static"  CssClass="form-control" Text="<%#: BindItem.NOILoc.Longitude %>"></asp:TextBox>
                                                    <asp:HiddenField ID="hfoutfallY" runat="server" ClientIDMode="Static" Value="<%#: BindItem.NOILoc.Y %>" />
                                                    <asp:RequiredFieldValidator ID="rfvoutfallLongitude" runat="server" ControlToValidate="txtoutfallLongitude" CssClass="alert-text" ErrorMessage="Longitude is required. Click Map Point button to select a point." Display="Dynamic" ToolTip="Longitude is required" ValidationGroup="OutfallValidationGrp">*</asp:RequiredFieldValidator>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtoutfallWatershed" runat="server" ClientIDMode="Static"  CssClass="form-control" Text="<%#: BindItem.NOILoc.Watershed %>"></asp:TextBox>
                                                    <asp:HiddenField ID="hfoutfallWaterShedCode" runat="server" ClientIDMode="Static" Value="<%#: BindItem.NOILoc.HUC_12_Code %>" />
                                                    <asp:RequiredFieldValidator ID="rfvoutfallWatershedCode" runat="server" ControlToValidate="txtoutfallWatershed" CssClass="alert-text" ErrorMessage="Watershed is required. Click Map Point button to select a point." Display="Dynamic" ToolTip="Watershed is required" ValidationGroup="OutfallValidationGrp">*</asp:RequiredFieldValidator>
                                                </td>
                                                <td>
                                                    <asp:Button ID="btnInsertOutfall" runat="server" CommandName="Insert" CssClass="btn btn-default" Text="Add Outfall" ClientIDMode="Static" ValidationGroup="OutfallValidationGrp" />
                                                </td>
                                            </tr>
                                        </InsertItemTemplate>
                                    </asp:ListView>
                                    <asp:HiddenField ID="hfOutfalllist" runat="server" ClientIDMode="Static" Value="" />
                                    <!-- Modal -->
                                    <div class="modal fade" id="MapModal" tabindex="-1" role="dialog">
                                        <div class="modal-dialog" role="document">
                                            <div class="modal-content" style="width: 800px; height: 600px">

                                                <div class="modal-header  bg-primary" >
                                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                                        <span aria-hidden="true">&times;</span>
                                                    </button>
                                                    <h5 class="modal-title" id="noiModalLabel">Outfalls</h5>
                                                </div>


                                                <div class="modal-body" id="mapOne" style="height: 100%; width: 100%;">
                                                </div>
                                                <div class="modal-footer">
                                                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div id="divPesticidesEntity" runat="server" class="panel panel-default">
                            <div class="panel-heading">
                                <div data-toggle="collapse" data-target="#Entityinfo" class="panel-title font-bold cursorpointer">Type Of Entity<span class="pull-right glyphicon glyphicon-chevron-up"></span></div>
                            </div>
                            <div id="Entityinfo" class="panel-collapse collapse in">
                                <div class="panel-body input-group-sm">
                                    <div class="col-md-12">
                                        <div class="radio">
                                            <asp:RadioButtonList ID="rblEntityType" runat="server" ItemType="NoticeOfIntent.NOIEntityType" DataTextField="EntityTypeDesc" DataValueField="EntityTypeID" SelectMethod="rblEntityType_GetData" ClientIDMode="Static"></asp:RadioButtonList>
                                            <asp:RequiredFieldValidator ID="rfvrblEntityType" ControlToValidate="rblEntityType" Display="Dynamic" ValidationGroup="ValidateNOI" runat="server" ErrorMessage="Type of Entity is required" ToolTip="Type of Entity is required" CssClass="alert-text" ClientIDMode="Static">*</asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <asp:Label ID="lblCommercialApplicatorID" AssociatedControlID="txtCommercialApplicatorID" runat="server" Text="Commercial Applicator ID:" CssClass="control-label col-md-3"></asp:Label>
                                        <div class="col-md-9">
                                            <asp:TextBox ID="txtCommercialApplicatorID" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div id="divPesticidesThreshold" runat="server" class="panel panel-default">
                            <div class="panel-heading">
                                    <div data-toggle="collapse" data-target="#Thresholdinfo" class="panel-title font-bold cursorpointer">Does this entity exceed the Annual Treatment Threshold identified in §9.8.2 of the Regulations Governing Discharges from the Application of Pesticides to Waters of the State? 
                                                                                    *The Annual Treatment Thresholds identified in §9.8.2 are as follows*<span class="pull-right glyphicon glyphicon-chevron-up"></span></div>
                                    <h6>(Check all that apply)</h6>
                            </div>
                            <div id="Thresholdinfo" class="panel-collapse collapse in">                            
                                <div class="panel-body">
                                     <div class="form-group-sm">
                                    <div class="checkbox">
                                        <label>
                                            <asp:CheckBox ID="chkInsectPestControl" runat="server" CssClass="anntreatmentthreshold" Text="Mosquitoes and Other Flying Insect Pest Control (larvaecide and adulticide) on 6400 acres of treatment area cumulative" TextAlign="Right" ClientIDMode="Static" AutoPostBack="true" />
                                        </label>
                                    </div>
                                    <div class="checkbox">
                                        <label>
                                            <asp:CheckBox ID="chkWeedPestControl" runat="server" CssClass="anntreatmentthreshold" Text="Weed and Algae Pest Control to 20 linear miles or 80 acres of water (i.e. surface area)" TextAlign="Right" ClientIDMode="Static" AutoPostBack="true" />
                                        </label>
                                    </div>
                                    <div class="checkbox">
                                        <label>
                                            <asp:CheckBox ID="chkAnimalPestControl" runat="server" CssClass="anntreatmentthreshold" Text="Animal Pest Control on 20 linear miles or 80 acres of water (i.e. surface area)" TextAlign="Right" ClientIDMode="Static" AutoPostBack="true" />
                                        </label>
                                    </div>
                                    <div class="checkbox">
                                        <label>
                                            <asp:CheckBox ID="chkForestCanopyPestControl" runat="server" CssClass="anntreatmentthreshold" Text="Forest Canopy Pest Control 6400 acres of treatment area cumulative" TextAlign="Right" ClientIDMode="Static" AutoPostBack="true" />
                                        </label>
                                    </div>
                                    <div class="radio">
                                        <label>
                                            <asp:RadioButton ID="rbtnThresholdNotExceeded" runat="server" CssClass="anntreatmentthreshold" Text="This entity DOES NOT exceed the Annual Treatment Threshold" TextAlign="Right" ClientIDMode="Static" AutoPostBack="true" />
                                        </label>
                                    </div>
                                    <div>
                                        <asp:CustomValidator ID="cvAnnlTreatmentThreshold" runat="server" Display="Dynamic" EnableClientScript="true" CssClass="alert-text" ClientValidationFunction="cvAnnlTreatmentThreshold_ClientValidation" ToolTip="At least one Annual Treatment Threshold needs to be selected." ErrorMessage="At least one Annual Treatment Threshold needs to be selected." ValidationGroup="ValidateNOI" ClientIDMode="Static"></asp:CustomValidator>
                                    </div>
                                    <div class="alert">
                                        <a data-toggle="collapse" href="#divcalculation">Click here to show how to calculate annual treatment areas.</a>
                                        <div id="divcalculation" class="alert alert-info panel-collapse collapse">
                                            <p>
                                                For calculating annual treatment areas for Mosquitoes and Other Flying Insect Pest Control and Forest Canopy Pest for comparing with any 
                                                                threshold listed above, count each pesticide application activity to a treatment area (i.e., that area where a pesticide application is 
                                                                intended to provide pesticidal benefits within the pest management area) as a separate area treated. For example, applying pesticides three 
                                                                times a year to the same 3,000 acre site should be counted as 9,000 acres of treatment area for purposes of determining if such an application 
                                                                exceeds an annual treatment area threshold. The treatment area for these two pesticide use patterns is additive over the calendar year.
                                            </p>
                                            <p>
                                                For calculating annual treatment areas for Weed and Algae Control and Animal Pest Control for comparing with any threshold listed above, 
                                                                calculations should include either the linear extent of or the surface area of waters for applications made to Waters of the State or 
                                                                at water’s edge adjacent to Waters of the State. For calculating the annual treatment area, count each treatment area only once, regardless 
                                                                of the number of pesticide application activities performed on that area in a given year. Also, for linear features (e.g., a canal or ditch), 
                                                                use the length of the linear feature whether treating in or adjacent to the feature, regardless of the number of applications made to that feature 
                                                                during the calendar year. For example, whether treating the bank on one side of a ten-mile long ditch, banks on both sides of the ditch, and/or 
                                                                water in that ditch, the total treatment area is ten miles for purposes of determining if such an application exceeds an annual treatment area 
                                                                threshold. Additionally, if the same 10 miles area is treated more than once in a calendar year, the total area treated is still 10 miles for 
                                                                purposes of comparing with any threshold listed above. The treatment area for these two pesticide use patterns is not additive over the calendar year.
                                            </p>
                                        </div>
                                    </div>
                                </div>
                                </div>
                            </div>
                        </div>
                        <div id="divChemicals" runat="server" class="panel panel-default">
                            <div class="panel-heading">
                                <div data-toggle="collapse" data-target="#Chemicalsinfo" class="panel-title font-bold cursorpointer">Chemicals Used in Operation (Note: a change in use of active ingredient or a change in annual average totals that vary by more than 15% as indicated on NOI will require NOI resubmission)<span class="pull-right glyphicon glyphicon-chevron-up"></span></div>
                            </div>
                            <div id="Chemicalsinfo" class="panel-collapse collapse in">
                                <div class="panel-body input-group-sm table-responsive">
                                    <asp:ListView ID="lvAPChemicals" DataKeyNames="APChemicalID,SubmissionID,Ingredient" ItemType="NoticeOfIntent.NOISubmissionAPChemicals" SelectMethod="lvAPChemicals_GetData" runat="server" InsertItemPosition="LastItem" InsertMethod="lvAPChemicals_InsertItem" DeleteMethod="lvAPChemicals_DeleteItem" UpdateMethod="lvAPChemicals_UpdateItem" >
                                        <LayoutTemplate>
                                            <table class="table table-bordered table-hover table-condensed">
                                                <thead>
                                                    <tr>
                                                        <th class="col-sm-3">Ingredient (% Active Ingredient) (not specific product name)</th>
                                                        <th class="col-sm-2">Pattern</th>
                                                        <th class="col-sm-2" title="Application Rate">Appl. Rate</th>
                                                        <th class="col-sm-2" title="Annual average amount used">Annl. avg. amt. used</th>
                                                        <th class="col-sm-2" title="Annual average area">Annl. avg. area</th>
                                                        <th class="col-sm-1">Action</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr runat="server" id="itemPlaceholder" />
                                                </tbody>
                                            </table>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <%#: Item.Ingredient %>
                                                </td>
                                                <td>
                                                    <%#: Item.NOIPesticidePattern.PesticidePattern %>
                                                </td>
                                                <td>
                                                    <label><%#: Item.ApplicationRate %></label><label><%#: Item.ApplicationRateUnit %></label>
                                                </td>
                                                <td>
                                                    <label><%#: Item.AnnlAvgQty %></label><label><%#: Item.AnnlAvgQtyUnit %></label>
                                                </td>
                                                <td>
                                                    <label><%#: Item.AnnlAvgArea %></label><label><%#: Item.AnnlAvgAreaUnit %></label>
                                                </td>
                                                <td>
                                                    <asp:ImageButton ID="ibtnEdit" AlternateText="Edit" runat="server" CssClass="btn-xs" CommandName="Edit" ImageUrl="~/Content/images/Edit.png" ToolTip="Edit the Chemical"/>
                                                    <asp:ImageButton ID="ibtnDelete" AlternateText="Delete" runat="server" CssClass="btn-xs" CommandName="Delete"  ImageUrl="~/Content/images/Delete.png" ToolTip="Delete the Chemical" OnClientClick="javascript:return confirm('Are you sure to delete?')" CausesValidation="false" />
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                        <AlternatingItemTemplate>
                                            <tr>
                                                <td>
                                                    <%#: Item.Ingredient %>
                                                </td>
                                                <td>
                                                    <%#: Item.NOIPesticidePattern.PesticidePattern %>
                                                </td>
                                                <td>
                                                    <label><%#: Item.ApplicationRate %></label><label><%#: Item.ApplicationRateUnit %></label>
                                                </td>
                                                <td>
                                                    <label><%#: Item.AnnlAvgQty %></label><label><%#: Item.AnnlAvgQtyUnit %></label>
                                                </td>
                                                <td>
                                                    <label><%#: Item.AnnlAvgArea %></label><label><%#: Item.AnnlAvgAreaUnit %></label>
                                                </td>
                                                <td>
                                                    <asp:ImageButton ID="ibtnEdit" AlternateText="Edit" runat="server" CssClass="btn-xs" CommandName="Edit" ImageUrl="~/Content/images/Edit.png" ToolTip="Edit the Chemical"/>
                                                    <asp:ImageButton ID="ibtnDelete" AlternateText="Delete" runat="server" CssClass="btn-xs" CommandName="Delete"  ImageUrl="~/Content/images/Delete.png" ToolTip="Delete the Chemical" OnClientClick="javascript:return confirm('Are you sure to delete?')" CausesValidation="false" />
                                                </td>
                                            </tr>
                                        </AlternatingItemTemplate>
                                        <EditItemTemplate>
                                            <tr>
                                                <td colspan="6" class="panel panel-default">
                                                    <div class="panel-body">
                                                        <div class="form-group form-group-sm">
                                                            <div class="row">
                                                                <div class="col-sm-8">
                                                                    <asp:Label ID="Label1" AssociatedControlID="txtIngredientEdit" runat="server" Text="Ingredient: " CssClass="control-label"></asp:Label>
                                                                    <asp:TextBox ID="txtIngredientEdit" runat="server" CssClass="form-control" Text='<%#: Item.Ingredient %>' ClientIDMode="Static" ReadOnly="true"></asp:TextBox>
                                                                </div>
                                                                <div class="col-sm-4">
                                                                    <asp:Label ID="Label2" runat="server" AssociatedControlID="ddlPesticidePatternEdit" Text="Pattern: " CssClass="control-label"></asp:Label>
                                                                    <asp:DropDownList ID="ddlPesticidePatternEdit" runat="server" CssClass="form-control" ItemType="NoticeOfIntent.NOIPesticidePattern" SelectMethod="GetPesticidePatterns" DataTextField="PesticidePattern" DataValueField="PesticidePatternID" ClientIDMode="Static" SelectedValue="<%#: BindItem.PesticidePatternID %>" AppendDataBoundItems="true">
                                                                        <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="rfvPesticidePatternEdit" runat="server" ControlToValidate="ddlPesticidePatternEdit" Display="Dynamic" EnableClientScript="true" CssClass="alert-text" InitialValue="0" ErrorMessage="Pesticide Pattern is required" ToolTip="Pesticide Pattern is required" ValidationGroup="ValidateEditChemical">*</asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-4">
                                                                    <label class="control-label">Application Rate:</label>
                                                                    <div class="form-inline">
                                                                        <asp:TextBox ID="txtApplicationRateEdit" runat="server" CssClass="form-control" Text="<%#: BindItem.ApplicationRate  %>" ClientIDMode="Static"></asp:TextBox>
                                                                        <asp:DropDownList ID="ddlApplicationRateUnitEdit" runat="server" CssClass="dropdown" ClientIDMode="Static" SelectedValue="<%#: BindItem.ApplicationRateUnit %>">
                                                                            <asp:ListItem Text="gal/acre" Value="gal/acre"></asp:ListItem>
                                                                            <asp:ListItem Text="lbs/acre" Value="lbs/acre"></asp:ListItem>
                                                                            <asp:ListItem Text="gal/mile" Value="gal/mile"></asp:ListItem>
                                                                            <asp:ListItem Text="oz/acre" Value="oz/acre"></asp:ListItem>
                                                                            <asp:ListItem Text="oz/gal" Value="oz/gal"></asp:ListItem>
                                                                            <asp:ListItem Text="pdu/acre" Value="pdu/acre"></asp:ListItem>
                                                                            <asp:ListItem Text="pints/acre" Value="pints/acre"></asp:ListItem>
                                                                            <asp:ListItem Text="ppb" Value="ppb"></asp:ListItem>
                                                                            <asp:ListItem Text="ppm" Value="ppm"></asp:ListItem>
                                                                            <asp:ListItem Text="qts/acre" Value="qts/acre"></asp:ListItem>
                                                                            <asp:ListItem Text="tablet/100 sqft" Value="tablet/100 sqft"></asp:ListItem>
                                                                        </asp:DropDownList>
                                                                        <asp:RequiredFieldValidator ID="rfvApplicationRateEdit" runat="server" ControlToValidate="txtApplicationRateEdit" Display="Dynamic" EnableClientScript="true" CssClass="alert-text" ErrorMessage="Enter the Application Rate." ToolTip="Enter the Application Rate." ValidationGroup="ValidateEditChemical">*</asp:RequiredFieldValidator>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-4">
                                                                    <label class="control-label">Annl. average amount used:</label>
                                                                    <div class="form-inline">                                                           
                                                                        <asp:TextBox ID="txtAnnlAvgQtyEdit" runat="server" CssClass="form-control" Text="<%#: BindItem.AnnlAvgQty  %>" ClientIDMode="Static"></asp:TextBox>
                                                                        <asp:DropDownList ID="ddlAnnlAvgQtyUnitEdit" runat="server" CssClass="dropdown" ClientIDMode="Static" SelectedValue="<%#: BindItem.AnnlAvgQtyUnit %>">
                                                                            <asp:ListItem Text="gal" Value="gal"></asp:ListItem>
                                                                            <asp:ListItem Text="lbs" Value="lbs"></asp:ListItem>
                                                                            <asp:ListItem Text="oz" Value ="oz"></asp:ListItem>
                                                                            <asp:ListItem Text="Tablets" Value="Tablets"></asp:ListItem>
                                                                        </asp:DropDownList>
                                                                        <asp:RequiredFieldValidator ID="rfvAnnlAvgQtyEdit" runat="server" ControlToValidate="txtAnnlAvgQtyEdit" Display="Dynamic" EnableClientScript="true" CssClass="alert-text" ErrorMessage="Enter the Annual average amount." ToolTip="Enter the Annual average amount." ValidationGroup="ValidateEditChemical">*</asp:RequiredFieldValidator>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-4">                                                                 
                                                                    <label class="control-label">Annual average area:</label>
                                                                    <div class="form-inline">
                                                                        <asp:TextBox ID="txtAnnlAvgAreaEdit" runat="server" CssClass="form-control" Text="<%#: BindItem.AnnlAvgArea  %>" ClientIDMode="Static"></asp:TextBox>
                                                                        <asp:DropDownList ID="ddlAnnlAvgAreaUnitEdit" runat="server" CssClass="dropdown" ClientIDMode="Static" SelectedValue="<%#: BindItem.AnnlAvgAreaUnit %>">
                                                                            <asp:ListItem Text="acres" Value="acres"></asp:ListItem>
                                                                            <asp:ListItem Text="miles" Value="miles"></asp:ListItem>
                                                                        </asp:DropDownList>
                                                                        <asp:RequiredFieldValidator ID="rfvAnnlAvgAreaEdit" runat="server" ControlToValidate="txtAnnlAvgAreaEdit" Display="Dynamic" EnableClientScript="true" CssClass="alert-text" ErrorMessage="Enter the Annual average area." ToolTip="Enter the Annual average area." ValidationGroup="ValidateEditChemical">*</asp:RequiredFieldValidator>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="pull-right">
                                                                    <asp:Button ID="btnUpdateChemical" runat="server" CommandName="Update" CssClass="btn btn-default" Text="Update" ClientIDMode="Static" ValidationGroup="ValidateEditChemical" />
                                                                    <asp:Button ID="btnCancel" runat="server" CommandName="Cancel" CssClass="btn btn-default" Text="Cancel" ClientIDMode="Static" CausesValidation="false" ValidationGroup="ValidateEditChemical" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </td>
                                            </tr>
                                        </EditItemTemplate>                                        
                                        <InsertItemTemplate>
                                            <tr class="insertfooter">
                                                <td colspan="6" class="panel-group">
                                                    <div class="panel panel-default">
                                                        <div class="panel panel-heading">
                                                            <a data-toggle="collapse" href="#pnlChemical">
                                                                <h6 class="panel-title font-bold">Click here to add Chemicals<span class="pull-right glyphicon glyphicon-chevron-down"></span></h6>
                                                            </a>
                                                        </div>
                                                        <div id="pnlChemical" class="panel-collapse collapse">
                                                            <div class="panel-body">
                                                                <div class="form-group form-group-sm">
                                                                    <div class="row">
                                                                        <div class="col-sm-8">
                                                                            <asp:Label ID="Label1" AssociatedControlID="txtIngredient" runat="server" Text="Ingredient: " CssClass="control-label"></asp:Label>

                                                                            <asp:TextBox ID="txtIngredient" runat="server" CssClass="form-control" Text='<%#: BindItem.Ingredient %>' ClientIDMode="Static"></asp:TextBox>
                                                                            <asp:RequiredFieldValidator ID="rfvIngredient" runat="server" ControlToValidate="txtIngredient" Display="Dynamic" EnableClientScript="true" CssClass="alert-text" ErrorMessage="Active Ingredient is required." ToolTip="Active Ingredient is required." ValidationGroup="ValidateChemical">*</asp:RequiredFieldValidator>

                                                                        </div>
                                                                        <div class="col-sm-4">
                                                                            <asp:Label ID="Label2" runat="server" AssociatedControlID="ddlPesticidePattern" Text="Pattern: " CssClass="control-label"></asp:Label>
                                                                            <asp:DropDownList ID="ddlPesticidePattern" runat="server" CssClass="form-control" ItemType="NoticeOfIntent.NOIPesticidePattern" SelectMethod="GetPesticidePatterns" DataTextField="PesticidePattern" DataValueField="PesticidePatternID" ClientIDMode="Static" SelectedValue="<%#: BindItem.PesticidePatternID %>" AppendDataBoundItems="true">
                                                                                <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                            <asp:RequiredFieldValidator ID="rfvPesticidePattern" runat="server" ControlToValidate="ddlPesticidePattern" Display="Dynamic" EnableClientScript="true" CssClass="alert-text" InitialValue="0" ErrorMessage="Pesticide Pattern is required" ToolTip="Pesticide Pattern is required" ValidationGroup="ValidateChemical">*</asp:RequiredFieldValidator>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row">
                                                                        <div class="col-sm-4">
                                                                             <label class="control-label">Application Rate:</label>
                                                                            <div class="form-inline">
                                                                                 <asp:TextBox ID="txtApplicationRate" runat="server" CssClass="form-control" Text="<%#: BindItem.ApplicationRate  %>" ClientIDMode="Static"></asp:TextBox>
                                                                                <asp:DropDownList ID="ddlApplicationRateUnit" runat="server" CssClass="dropdown" ClientIDMode="Static" SelectedValue="<%#: BindItem.ApplicationRateUnit %>">
                                                                                    <asp:ListItem Text="gal/acre" Value="gal/acre"></asp:ListItem>
                                                                                    <asp:ListItem Text="lbs/acre" Value="lbs/acre"></asp:ListItem>
                                                                                    <asp:ListItem Text="gal/mile" Value="gal/mile"></asp:ListItem>
                                                                                    <asp:ListItem Text="oz/acre" Value="oz/acre"></asp:ListItem>
                                                                                    <asp:ListItem Text="oz/gal" Value="oz/gal"></asp:ListItem>
                                                                                    <asp:ListItem Text="pdu/acre" Value="pdu/acre"></asp:ListItem>
                                                                                    <asp:ListItem Text="pints/acre" Value="pints/acre"></asp:ListItem>
                                                                                    <asp:ListItem Text="ppb" Value="ppb"></asp:ListItem>
                                                                                    <asp:ListItem Text="ppm" Value="ppm"></asp:ListItem>
                                                                                    <asp:ListItem Text="qts/acre" Value="qts/acre"></asp:ListItem>
                                                                                    <asp:ListItem Text="tablet/100 sqft" Value="tablet/100 sqft"></asp:ListItem>

                                                                                </asp:DropDownList>
                                                                                <asp:RequiredFieldValidator ID="rfvApplicationRate" runat="server" ControlToValidate="txtApplicationRate" Display="Dynamic" EnableClientScript="true" CssClass="alert-text" ErrorMessage="Enter the Application Rate." ToolTip="Enter the Application Rate." ValidationGroup="ValidateChemical">*</asp:RequiredFieldValidator>
                                                                            </div>
                                                                        </div>
                                                                         <div class="col-sm-4">
                                                                            <label class="control-label">Annl. average amount used:</label>
                                                                            <div class="form-inline">                                                           
                                                                                <asp:TextBox ID="txtAnnlAvgQty" runat="server" CssClass="form-control" Text="<%#: BindItem.AnnlAvgQty  %>" ClientIDMode="Static"></asp:TextBox>
                                                                                <asp:DropDownList ID="ddlAnnlAvgQtyUnit" runat="server" CssClass="dropdown"  ClientIDMode="Static" SelectedValue="<%#: BindItem.AnnlAvgQtyUnit %>" >
                                                                                    <asp:ListItem Text="gal" Value="gal"></asp:ListItem>
                                                                                    <asp:ListItem Text="lbs" Value="lbs"></asp:ListItem>
                                                                                    <asp:ListItem Text="oz" Value ="oz"></asp:ListItem>
                                                                                    <asp:ListItem Text="Tablets" Value="Tablets"></asp:ListItem>
                                                                                </asp:DropDownList>
                                                                                <asp:RequiredFieldValidator ID="rfvAnnlAvgQty" runat="server" ControlToValidate="txtAnnlAvgQty" Display="Dynamic" EnableClientScript="true" CssClass="alert-text" ErrorMessage="Enter the Annual average amount." ToolTip="Enter the Annual average amount." ValidationGroup="ValidateChemical">*</asp:RequiredFieldValidator>
                                                                            </div>
                                                                        </div>
                                                                         <div class="col-sm-4">                                                                 
                                                                            <label class="control-label">Annual average area:</label>
                                                                            <div class="form-inline">
                                                                                <asp:TextBox ID="txtAnnlAvgArea" runat="server" CssClass="form-control" Text="<%#: BindItem.AnnlAvgArea  %>" ClientIDMode="Static"></asp:TextBox>
                                                                                <asp:RequiredFieldValidator ID="rfvAnnlAvgArea" runat="server" ControlToValidate="txtAnnlAvgArea" Display="Dynamic" EnableClientScript="true" CssClass="alert-text" ErrorMessage="Enter the Annual average area." ToolTip="Enter the Annual average area." ValidationGroup="ValidateChemical">*</asp:RequiredFieldValidator>
                                                                                <asp:DropDownList ID="ddlAnnlAvgAreaUnit" runat="server" CssClass="dropdown"  ClientIDMode="Static" SelectedValue="<%#: BindItem.AnnlAvgAreaUnit %>" >
                                                                                    <asp:ListItem Text="acres" Value="acres"></asp:ListItem>
                                                                                    <asp:ListItem Text="miles" Value="miles"></asp:ListItem>
                                                                                </asp:DropDownList>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row">
                                                                        <div class="pull-right">
                                                                            <asp:Button ID="btnInsertChemical" runat="server" CommandName="Insert" CssClass="btn btn-default" Text="Add Chemical" ClientIDMode="Static" ValidationGroup="ValidateChemical" />
                                                                        </div>
                                                                    </div>
                                                                </div>

                                                            </div>
                                                        </div>
                                                    </div>
                                                </td>
                                            </tr>
                                        </InsertItemTemplate>
                                    </asp:ListView>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <asp:Label ID="lblComments" AssociatedControlID="txtComments" runat="server" Text="Comments:" CssClass="control-label"></asp:Label>
                            <asp:TextBox ID="txtComments" runat="server" TextMode="MultiLine" Rows="5" MaxLength="2000" CssClass="form-control input-group-sm"></asp:TextBox>
                        </div>
                    </div>
                </asp:WizardStep>
            </WizardSteps>
            <StartNavigationTemplate>
                <asp:Button ID="btnStartNext" runat="server" CommandName="MoveNext" Text="Next" CssClass="btn btn-default" ClientIDMode="Static" ValidationGroup="ValidateNOI"  />
                <asp:Button ID="btnStartCancel" runat="server" Text="Cancel" CausesValidation="false" CommandName="Cancel" CssClass="btn btn-default" ClientIDMode="Static" />
            </StartNavigationTemplate>
            <StepNavigationTemplate>
                <asp:Button ID="btnStepPrevious" runat="server" Text="Previous" CausesValidation="false" CssClass="btn btn-default" CommandName="MovePrevious" ClientIDMode="Static" />
                <asp:Button ID="btnStepNext" runat="server" CommandName="MoveNext" Text="Next" CssClass="btn btn-default" ClientIDMode="Static" ValidationGroup="ValidateNOI"  />
                <asp:Button ID="btnStepCancel" runat="server" Text="Cancel" CausesValidation="false" CommandName="Cancel" CssClass="btn btn-default" ClientIDMode="Static" />
            </StepNavigationTemplate>
            <FinishNavigationTemplate>
                <table>
                    <tr>
                        <td>
                            <asp:Button ID="btnPrevious" runat="server" Text="Previous" CausesValidation="false" CssClass="btn btn-default" CommandName="MovePrevious" ClientIDMode="Static" />
                        </td>
                        <td>
                            <asp:Button ID="btnFinishSubmit" runat="server" Text="Save" CommandName="MoveComplete" CssClass="btn btn-default" ClientIDMode="Static" ValidationGroup="ValidateNOI" />
                        </td>
                        <td>
                            <asp:Button ID="btnFinishCancel" runat="server" Text="Cancel" CausesValidation="false" CommandName="Cancel" CssClass="btn btn-default" ClientIDMode="Static" />
                        </td>
                    </tr>
                </table>
            </FinishNavigationTemplate>
            <SideBarTemplate>
                <asp:ListView ID="sideBarList" runat="server" OnItemDataBound="sideBarList_ItemDataBound" >
                    <LayoutTemplate>
                        <table>
                            <tr runat="server" id="ItemPlaceHolder">

                            </tr>
                        </table>
                    </LayoutTemplate>

                    <ItemTemplate>
                        <tr>
                            <td>
                              <asp:LinkButton ID="sideBarButton" runat="server" 
                                  Text="Button" ValidationGroup="ValidateNOI" />
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:ListView>
            </SideBarTemplate>
        </asp:Wizard>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="NestedContentPlaceHolderJS" runat="server">
    <script>
        $(document).ready(function () {
            //alert("page ready");
            //debugger;


            //Modernizr.load({
            //    test: Modernizr.inputtypes.date,
            //    nope: "Scripts/jquery-ui-1.11.4.min.js",
            //    callback: function () {
            //        $("input[type=date]").datepicker({ dateFormat: "mm-dd-yy" });
            //    }
            //});

            //$('.datepicker').mask("99-99-9999", {placeholder:"mm-dd-yyyy"});
            // $('.datepicker').datepicker({ dateFormat: "mm-dd-yy" });

            dojoConfig = { isDebug: true };


            $("#ContentPlaceHolderBody_NestedContentPlaceHolderBody_wzNOI_lvOutfall_btnMapOutfallPoint").click(function () {
                $("#MapModal").modal({ backdrop: "static" });
                $("#MapModal").modal("show");
                createMap('mapOne');
            });

            $('#chkDisableNoExposure').each(function (e) {

                if ($(this)[0].checked == true) {
                    ConfigureValidators($('#cvNoExposure')[0], false);
                }
                else {
                    ConfigureValidators($('#cvNoExposure')[0], true);
                }

                $(this).on('change', function (e) {

                    if ($(this)[0].checked == true) {
                        ConfigureValidators($('#cvNoExposure')[0], false);
                    }
                    else {
                        ConfigureValidators($('#cvNoExposure')[0], true);
                    }
                })
            })
                
                
                


            $('#rblSWPPPYesNo input').change(function (e) {
                //debugger;
                if ($(this).is(":checked")) {
                    if ($(this)[0].value == "No") {
                        $('#btnFinishSubmit').attr("disabled", true)
                    }
                    if ($(this)[0].value == "Yes") {
                        $('#btnFinishSubmit').attr("disabled", false)
                    }
                }

            })

            $('.panel-collapse').on('hide.bs.collapse', function (e) {
                //debugger;
                if ($(this).is(e.target)) {
                    var $ctl = $(this).parent().find('.panel-title>span').first()
                    if ($ctl.hasClass('glyphicon glyphicon-chevron-up')) {
                        $ctl.removeClass('glyphicon glyphicon-chevron-up');
                        $ctl.addClass('glyphicon glyphicon-chevron-down');
                    }
                }

            });

            $('.panel-collapse').on('show.bs.collapse', function (e) {
                //debugger;
                if ($(this).is(e.target)) {
                    var $ctl = $(this).parent().find('.panel-title>span').first()
                    if ($ctl.hasClass('glyphicon glyphicon-chevron-down')) {
                        $ctl.removeClass('glyphicon glyphicon-chevron-down');
                        $ctl.addClass('glyphicon glyphicon-chevron-up');
                    }
                }
            });




            $('.countyselect').each(function (index) {
                //debugger;
                if ($(this).find('input').is(":checked")) {
                    var parent = $(this).data('parent')
                    var $parent = parent && $(parent)
                    var target = $(this).data('target')
                    var $target = $(target)

                    //$parent.find('.panel-collapse').collapse('hide');
                    $target.collapse('show');

                    var focusables = $parent.find('input:text');
                    focusables.keyup(function (e) {
                        var maxchar = false;
                        if ($(this).attr("maxlength")) {
                            if ($(this).val().length >= $(this).attr("maxlength"))
                                maxchar = true;
                        }
                        if (e.keyCode == 13 || maxchar) {
                            var current = focusables.index(this),
                                next = focusables.eq(current + 1).length ? focusables.eq(current + 1) : focusables.eq(0);
                            next.focus();
                        }
                    });
                }


                $(this).on('change', function () {
                    //debugger;
                    if ($(this).find('input').is(":checked")) {
                        var parent = $(this).data('parent')
                        var $parent = parent && $(parent)
                        var target = $(this).data('target')
                        var $target = $(target)

                        $parent.find('.in').collapse('hide');
                        $target.collapse('show');
                    }


                });
            });


            $('#ContentPlaceHolderBody_NestedContentPlaceHolderBody_wzNOI_ucnaOwnerInfo_txtZip').on("change", function (e) {
                e.preventDefault();
                $.getJSON('../api/Zipcodes/' + $(this).val())
                .done(function (data) {
                    //debugger;
                    if (data != null) {
                        $('#ContentPlaceHolderBody_NestedContentPlaceHolderBody_wzNOI_ucnaOwnerInfo_txtCity').val(data.PO_Name)
                        $('#ContentPlaceHolderBody_NestedContentPlaceHolderBody_wzNOI_ucnaOwnerInfo_ddlStateAbv').val(data.StateAbv)
                    }
                    else {
                        $('#ContentPlaceHolderBody_NestedContentPlaceHolderBody_wzNOI_ucnaOwnerInfo_txtCity').val("")
                        $('#ContentPlaceHolderBody_NestedContentPlaceHolderBody_wzNOI_ucnaOwnerInfo_ddlStateAbv').val("--")
                        alert("no matching records available.");
                        $('#ContentPlaceHolderBody_NestedContentPlaceHolderBody_wzNOI_ucnaOwnerInfo_txtZip').focus()
                    }
                })
                .fail(function (jqxhr, textStatus, error) {
                    alert(error);
                })

            });


            $('#ContentPlaceHolderBody_NestedContentPlaceHolderBody_wzNOI_ucnaContactInfo_txtZip').on("change", function (e) {
                e.preventDefault();
                $.getJSON('../api/Zipcodes/' + $(this).val())
                .done(function (data) {
                    if (data != null) {
                        $('#ContentPlaceHolderBody_NestedContentPlaceHolderBody_wzNOI_ucnaContactInfo_txtCity').val(data.PO_Name)
                        $('#ContentPlaceHolderBody_NestedContentPlaceHolderBody_wzNOI_ucnaContactInfo_ddlStateAbv').val(data.StateAbv)
                    }
                    else {
                        $('#ContentPlaceHolderBody_NestedContentPlaceHolderBody_wzNOI_ucnaContactInfo_txtCity').val("")
                        $('#ContentPlaceHolderBody_NestedContentPlaceHolderBody_wzNOI_ucnaContactInfo_ddlStateAbv').val("--")
                        alert("no matching records available.");
                        $('#ContentPlaceHolderBody_NestedContentPlaceHolderBody_wzNOI_ucnaContactInfo_txtZip').focus()
                    }
                })
                .fail(function (jqxhr, textStatus, error) {
                    alert(error);
                })

            });


            $('#ContentPlaceHolderBody_NestedContentPlaceHolderBody_wzNOI_ucnaBilleeInfo_txtZip').on("change", function (e) {
                e.preventDefault();
                $.getJSON('../api/Zipcodes/' + $(this).val())
                .done(function (data) {
                    if (data != null) {
                        $('#ContentPlaceHolderBody_NestedContentPlaceHolderBody_wzNOI_ucnaBilleeInfo_txtCity').val(data.PO_Name)
                        $('#ContentPlaceHolderBody_NestedContentPlaceHolderBody_wzNOI_ucnaBilleeInfo_ddlStateAbv').val(data.StateAbv)
                    }
                    else {
                        $('#ContentPlaceHolderBody_NestedContentPlaceHolderBody_wzNOI_ucnaBilleeInfo_txtCity').val("")
                        $('#ContentPlaceHolderBody_NestedContentPlaceHolderBody_wzNOI_ucnaBilleeInfo_ddlStateAbv').val("--")
                        alert("no matching records available.");
                        $('#ContentPlaceHolderBody_NestedContentPlaceHolderBody_wzNOI_ucnaBilleeInfo_txtZip').focus()
                    }
                })
                .fail(function (jqxhr, textStatus, error) {
                    alert(error);
                })

            });


            $('#ContentPlaceHolderBody_NestedContentPlaceHolderBody_wzNOI_ucnaSiteInfo_txtZip').on({
                change: function (e) {
                    e.preventDefault();
                    $.getJSON('../api/ZipcodesDE/' + $(this).val())
                    .done(function (data) {
                        if (data != null) {
                            $('#ContentPlaceHolderBody_NestedContentPlaceHolderBody_wzNOI_ucnaSiteInfo_txtCity').val(data.PO_Name)
                            $('#ContentPlaceHolderBody_NestedContentPlaceHolderBody_wzNOI_ucnaSiteInfo_ddlCounty').val(data.County)
                        }
                        else {
                            $('#ContentPlaceHolderBody_NestedContentPlaceHolderBody_wzNOI_ucnaSiteInfo_txtCity').val("")
                            $('#ContentPlaceHolderBody_NestedContentPlaceHolderBody_wzNOI_ucnaSiteInfo_ddlCounty').val("")
                            alert("no matching records available.");
                            $('#ContentPlaceHolderBody_NestedContentPlaceHolderBody_wzNOI_ucnaSiteInfo_txtZip').focus()
                        }


                    })
                    .fail(function (jqxhr, textStatus, error) {
                        alert(error);
                    })

                    var street = $('#ContentPlaceHolderBody_NestedContentPlaceHolderBody_wzNOI_ucnaSiteInfo_txtAddress1').val() + ","
                                        + $('#ContentPlaceHolderBody_NestedContentPlaceHolderBody_wzNOI_ucnaSiteInfo_txtCity').val() + ","
                                        + $('#ContentPlaceHolderBody_NestedContentPlaceHolderBody_wzNOI_ucnaSiteInfo_ddlStateAbv').val() + " "
                                        + $(this).val()

                    $("#hfSiteInfo").val(street)

                },
                blur: function (e) {
                    if ($('#hfSiteInfo').val().length > 0) {
                        locateAddress($("#hfSiteInfo").val());
                    }
                }


            });

   



            $('#ContentPlaceHolderBody_NestedContentPlaceHolderBody_wzNOI_ucnaOwnerInfo_ddlCompanyType').each(function (e) {
                //debugger;
                if ($(this).val() == 'P') {
                    $('#ContentPlaceHolderBody_NestedContentPlaceHolderBody_wzNOI_ucnaOwnerInfo_divcompanyname').hide();
                    ConfigureValidators($("#ContentPlaceHolderBody_NestedContentPlaceHolderBody_wzNOI_ucnaOwnerInfo_txtCompanyName")[0], false);
                    //$('#ContentPlaceHolderBody_NestedContentPlaceHolderBody_wzNOI_ucnaOwnerInfo_divpersonname').show();
                    //ConfigureValidators($("#ContentPlaceHolderBody_NestedContentPlaceHolderBody_wzNOI_ucnaOwnerInfo_txtLastName")[0], true);
                    //ConfigureValidators($("#ContentPlaceHolderBody_NestedContentPlaceHolderBody_wzNOI_ucnaOwnerInfo_txtFirstName")[0], true);
                }
                else {
                    $('#ContentPlaceHolderBody_NestedContentPlaceHolderBody_wzNOI_ucnaOwnerInfo_divcompanyname').show();
                    ConfigureValidators($("#ContentPlaceHolderBody_NestedContentPlaceHolderBody_wzNOI_ucnaOwnerInfo_txtCompanyName")[0], true);
                    //ConfigureValidators($("#ContentPlaceHolderBody_NestedContentPlaceHolderBody_wzNOI_ucnaOwnerInfo_txtLastName")[0], false);
                    //ConfigureValidators($("#ContentPlaceHolderBody_NestedContentPlaceHolderBody_wzNOI_ucnaOwnerInfo_txtFirstName")[0], false);
                    //$('#ContentPlaceHolderBody_NestedContentPlaceHolderBody_wzNOI_ucnaOwnerInfo_divpersonname').hide();
                }

                $(this).on("change", function (e) {
                    //debugger;

                    if ($(this).val() == 'P') {
                        $('#ContentPlaceHolderBody_NestedContentPlaceHolderBody_wzNOI_ucnaOwnerInfo_divcompanyname').hide();
                        ConfigureValidators($("#ContentPlaceHolderBody_NestedContentPlaceHolderBody_wzNOI_ucnaOwnerInfo_txtCompanyName")[0], false);
                        //$('#ContentPlaceHolderBody_NestedContentPlaceHolderBody_wzNOI_ucnaOwnerInfo_divpersonname').show();
                        //ConfigureValidators($("#ContentPlaceHolderBody_NestedContentPlaceHolderBody_wzNOI_ucnaOwnerInfo_txtLastName")[0], true);
                        //ConfigureValidators($("#ContentPlaceHolderBody_NestedContentPlaceHolderBody_wzNOI_ucnaOwnerInfo_txtFirstName")[0], true);
                    }
                    else {
                        $('#ContentPlaceHolderBody_NestedContentPlaceHolderBody_wzNOI_ucnaOwnerInfo_divcompanyname').show();
                        ConfigureValidators($("#ContentPlaceHolderBody_NestedContentPlaceHolderBody_wzNOI_ucnaOwnerInfo_txtCompanyName")[0], true);
                        //ConfigureValidators($("#ContentPlaceHolderBody_NestedContentPlaceHolderBody_wzNOI_ucnaOwnerInfo_txtLastName")[0], false);
                        //ConfigureValidators($("#ContentPlaceHolderBody_NestedContentPlaceHolderBody_wzNOI_ucnaOwnerInfo_txtFirstName")[0], false);
                        //$('#ContentPlaceHolderBody_NestedContentPlaceHolderBody_wzNOI_ucnaOwnerInfo_divpersonname').hide();
                    }
                });

            });


            $('#btnCopyContactInfo').on("click", function (e) {

                if ($('#ddlCopyContactInfoFrom option:selected').val() == 'O') {

                    $('#ContentPlaceHolderBody_NestedContentPlaceHolderBody_wzNOI_ucnaContactInfo_txtCompanyName').val($('#ContentPlaceHolderBody_NestedContentPlaceHolderBody_wzNOI_ucnaOwnerInfo_txtCompanyName').val())
                    $('#ContentPlaceHolderBody_NestedContentPlaceHolderBody_wzNOI_ucnaContactInfo_txtLastName').val($('#ContentPlaceHolderBody_NestedContentPlaceHolderBody_wzNOI_ucnaOwnerInfo_txtLastName').val())
                    $('#ContentPlaceHolderBody_NestedContentPlaceHolderBody_wzNOI_ucnaContactInfo_txtFirstName').val($('#ContentPlaceHolderBody_NestedContentPlaceHolderBody_wzNOI_ucnaOwnerInfo_txtFirstName').val())
                    $('#ContentPlaceHolderBody_NestedContentPlaceHolderBody_wzNOI_ucnaContactInfo_txtAddress1').val($('#ContentPlaceHolderBody_NestedContentPlaceHolderBody_wzNOI_ucnaOwnerInfo_txtAddress1').val())
                    $('#ContentPlaceHolderBody_NestedContentPlaceHolderBody_wzNOI_ucnaContactInfo_txtAddress2').val($('#ContentPlaceHolderBody_NestedContentPlaceHolderBody_wzNOI_ucnaOwnerInfo_txtAddress2').val())
                    $('#ContentPlaceHolderBody_NestedContentPlaceHolderBody_wzNOI_ucnaContactInfo_txtZip').val($('#ContentPlaceHolderBody_NestedContentPlaceHolderBody_wzNOI_ucnaOwnerInfo_txtZip').val())
                    $('#ContentPlaceHolderBody_NestedContentPlaceHolderBody_wzNOI_ucnaContactInfo_txtCity').val($('#ContentPlaceHolderBody_NestedContentPlaceHolderBody_wzNOI_ucnaOwnerInfo_txtCity').val())
                    $('#ContentPlaceHolderBody_NestedContentPlaceHolderBody_wzNOI_ucnaContactInfo_ddlStateAbv').val($('#ContentPlaceHolderBody_NestedContentPlaceHolderBody_wzNOI_ucnaOwnerInfo_ddlStateAbv').val())
                    $('#ContentPlaceHolderBody_NestedContentPlaceHolderBody_wzNOI_ucnaContactInfo_txtPhone').val($('#ContentPlaceHolderBody_NestedContentPlaceHolderBody_wzNOI_ucnaOwnerInfo_txtPhone').val())
                    $('#ContentPlaceHolderBody_NestedContentPlaceHolderBody_wzNOI_ucnaContactInfo_txtExt').val($('#ContentPlaceHolderBody_NestedContentPlaceHolderBody_wzNOI_ucnaOwnerInfo_txtExt').val())
                    $('#ContentPlaceHolderBody_NestedContentPlaceHolderBody_wzNOI_ucnaContactInfo_txtMobile').val($('#ContentPlaceHolderBody_NestedContentPlaceHolderBody_wzNOI_ucnaOwnerInfo_txtMobile').val())
                    $('#ContentPlaceHolderBody_NestedContentPlaceHolderBody_wzNOI_ucnaContactInfo_txtEmail').val($('#ContentPlaceHolderBody_NestedContentPlaceHolderBody_wzNOI_ucnaOwnerInfo_txtEmail').val())

                }
                else if ($('#ddlCopyContactInfoFrom option:selected').val() == 'B') {

                    $('#ContentPlaceHolderBody_NestedContentPlaceHolderBody_wzNOI_ucnaContactInfo_txtCompanyName').val($('#ContentPlaceHolderBody_NestedContentPlaceHolderBody_wzNOI_ucnaBilleeInfo_txtCompanyName').val())
                    $('#ContentPlaceHolderBody_NestedContentPlaceHolderBody_wzNOI_ucnaContactInfo_txtLastName').val($('#ContentPlaceHolderBody_NestedContentPlaceHolderBody_wzNOI_ucnaBilleeInfo_txtLastName').val())
                    $('#ContentPlaceHolderBody_NestedContentPlaceHolderBody_wzNOI_ucnaContactInfo_txtFirstName').val($('#ContentPlaceHolderBody_NestedContentPlaceHolderBody_wzNOI_ucnaBilleeInfo_txtFirstName').val())
                    $('#ContentPlaceHolderBody_NestedContentPlaceHolderBody_wzNOI_ucnaContactInfo_txtAddress1').val($('#ContentPlaceHolderBody_NestedContentPlaceHolderBody_wzNOI_ucnaBilleeInfo_txtAddress1').val())
                    $('#ContentPlaceHolderBody_NestedContentPlaceHolderBody_wzNOI_ucnaContactInfo_txtAddress2').val($('#ContentPlaceHolderBody_NestedContentPlaceHolderBody_wzNOI_ucnaBilleeInfo_txtAddress2').val())
                    $('#ContentPlaceHolderBody_NestedContentPlaceHolderBody_wzNOI_ucnaContactInfo_txtZip').val($('#ContentPlaceHolderBody_NestedContentPlaceHolderBody_wzNOI_ucnaBilleeInfo_txtZip').val())
                    $('#ContentPlaceHolderBody_NestedContentPlaceHolderBody_wzNOI_ucnaContactInfo_txtCity').val($('#ContentPlaceHolderBody_NestedContentPlaceHolderBody_wzNOI_ucnaBilleeInfo_txtCity').val())
                    $('#ContentPlaceHolderBody_NestedContentPlaceHolderBody_wzNOI_ucnaContactInfo_ddlStateAbv').val($('#ContentPlaceHolderBody_NestedContentPlaceHolderBody_wzNOI_ucnaBilleeInfo_ddlStateAbv').val())
                    $('#ContentPlaceHolderBody_NestedContentPlaceHolderBody_wzNOI_ucnaContactInfo_txtPhone').val($('#ContentPlaceHolderBody_NestedContentPlaceHolderBody_wzNOI_ucnaBilleeInfo_txtPhone').val())
                    $('#ContentPlaceHolderBody_NestedContentPlaceHolderBody_wzNOI_ucnaContactInfo_txtExt').val($('#ContentPlaceHolderBody_NestedContentPlaceHolderBody_wzNOI_ucnaBilleeInfo_txtExt').val())
                    $('#ContentPlaceHolderBody_NestedContentPlaceHolderBody_wzNOI_ucnaContactInfo_txtMobile').val($('#ContentPlaceHolderBody_NestedContentPlaceHolderBody_wzNOI_ucnaBilleeInfo_txtMobile').val())
                    $('#ContentPlaceHolderBody_NestedContentPlaceHolderBody_wzNOI_ucnaContactInfo_txtEmail').val($('#ContentPlaceHolderBody_NestedContentPlaceHolderBody_wzNOI_ucnaBilleeInfo_txtEmail').val())

                }

                e.preventDefault();
            });

            $('#btnCopyBilleeInfo').on("click", function (e) {

                if ($('#ddlCopyBilleeInfoFrom option:selected').val() == 'O') {

                    $('#ContentPlaceHolderBody_NestedContentPlaceHolderBody_wzNOI_ucnaBilleeInfo_txtCompanyName').val($('#ContentPlaceHolderBody_NestedContentPlaceHolderBody_wzNOI_ucnaOwnerInfo_txtCompanyName').val())
                    $('#ContentPlaceHolderBody_NestedContentPlaceHolderBody_wzNOI_ucnaBilleeInfo_txtLastName').val($('#ContentPlaceHolderBody_NestedContentPlaceHolderBody_wzNOI_ucnaOwnerInfo_txtLastName').val())
                    $('#ContentPlaceHolderBody_NestedContentPlaceHolderBody_wzNOI_ucnaBilleeInfo_txtFirstName').val($('#ContentPlaceHolderBody_NestedContentPlaceHolderBody_wzNOI_ucnaOwnerInfo_txtFirstName').val())
                    $('#ContentPlaceHolderBody_NestedContentPlaceHolderBody_wzNOI_ucnaBilleeInfo_txtAddress1').val($('#ContentPlaceHolderBody_NestedContentPlaceHolderBody_wzNOI_ucnaOwnerInfo_txtAddress1').val())
                    $('#ContentPlaceHolderBody_NestedContentPlaceHolderBody_wzNOI_ucnaBilleeInfo_txtAddress2').val($('#ContentPlaceHolderBody_NestedContentPlaceHolderBody_wzNOI_ucnaOwnerInfo_txtAddress2').val())
                    $('#ContentPlaceHolderBody_NestedContentPlaceHolderBody_wzNOI_ucnaBilleeInfo_txtZip').val($('#ContentPlaceHolderBody_NestedContentPlaceHolderBody_wzNOI_ucnaOwnerInfo_txtZip').val())
                    $('#ContentPlaceHolderBody_NestedContentPlaceHolderBody_wzNOI_ucnaBilleeInfo_txtCity').val($('#ContentPlaceHolderBody_NestedContentPlaceHolderBody_wzNOI_ucnaOwnerInfo_txtCity').val())
                    $('#ContentPlaceHolderBody_NestedContentPlaceHolderBody_wzNOI_ucnaBilleeInfo_ddlStateAbv').val($('#ContentPlaceHolderBody_NestedContentPlaceHolderBody_wzNOI_ucnaOwnerInfo_ddlStateAbv').val())
                    $('#ContentPlaceHolderBody_NestedContentPlaceHolderBody_wzNOI_ucnaBilleeInfo_txtPhone').val($('#ContentPlaceHolderBody_NestedContentPlaceHolderBody_wzNOI_ucnaOwnerInfo_txtPhone').val())
                    $('#ContentPlaceHolderBody_NestedContentPlaceHolderBody_wzNOI_ucnaBilleeInfo_txtExt').val($('#ContentPlaceHolderBody_NestedContentPlaceHolderBody_wzNOI_ucnaOwnerInfo_txtExt').val())
                    $('#ContentPlaceHolderBody_NestedContentPlaceHolderBody_wzNOI_ucnaBilleeInfo_txtMobile').val($('#ContentPlaceHolderBody_NestedContentPlaceHolderBody_wzNOI_ucnaOwnerInfo_txtMobile').val())
                    $('#ContentPlaceHolderBody_NestedContentPlaceHolderBody_wzNOI_ucnaBilleeInfo_txtEmail').val($('#ContentPlaceHolderBody_NestedContentPlaceHolderBody_wzNOI_ucnaOwnerInfo_txtEmail').val())

                }
                else if ($('#ddlCopyBilleeInfoFrom option:selected').val() == 'C') {

                    $('#ContentPlaceHolderBody_NestedContentPlaceHolderBody_wzNOI_ucnaBilleeInfo_txtCompanyName').val($('#ContentPlaceHolderBody_NestedContentPlaceHolderBody_wzNOI_ucnaContactInfo_txtCompanyName').val())
                    $('#ContentPlaceHolderBody_NestedContentPlaceHolderBody_wzNOI_ucnaBilleeInfo_txtLastName').val($('#ContentPlaceHolderBody_NestedContentPlaceHolderBody_wzNOI_ucnaContactInfo_txtLastName').val())
                    $('#ContentPlaceHolderBody_NestedContentPlaceHolderBody_wzNOI_ucnaBilleeInfo_txtFirstName').val($('#ContentPlaceHolderBody_NestedContentPlaceHolderBody_wzNOI_ucnaContactInfo_txtFirstName').val())
                    $('#ContentPlaceHolderBody_NestedContentPlaceHolderBody_wzNOI_ucnaBilleeInfo_txtAddress1').val($('#ContentPlaceHolderBody_NestedContentPlaceHolderBody_wzNOI_ucnaContactInfo_txtAddress1').val())
                    $('#ContentPlaceHolderBody_NestedContentPlaceHolderBody_wzNOI_ucnaBilleeInfo_txtAddress2').val($('#ContentPlaceHolderBody_NestedContentPlaceHolderBody_wzNOI_ucnaContactInfo_txtAddress2').val())
                    $('#ContentPlaceHolderBody_NestedContentPlaceHolderBody_wzNOI_ucnaBilleeInfo_txtZip').val($('#ContentPlaceHolderBody_NestedContentPlaceHolderBody_wzNOI_ucnaContactInfo_txtZip').val())
                    $('#ContentPlaceHolderBody_NestedContentPlaceHolderBody_wzNOI_ucnaBilleeInfo_txtCity').val($('#ContentPlaceHolderBody_NestedContentPlaceHolderBody_wzNOI_ucnaContactInfo_txtCity').val())
                    $('#ContentPlaceHolderBody_NestedContentPlaceHolderBody_wzNOI_ucnaBilleeInfo_ddlStateAbv').val($('#ContentPlaceHolderBody_NestedContentPlaceHolderBody_wzNOI_ucnaContactInfo_ddlStateAbv').val())
                    $('#ContentPlaceHolderBody_NestedContentPlaceHolderBody_wzNOI_ucnaBilleeInfo_txtPhone').val($('#ContentPlaceHolderBody_NestedContentPlaceHolderBody_wzNOI_ucnaContactInfo_txtPhone').val())
                    $('#ContentPlaceHolderBody_NestedContentPlaceHolderBody_wzNOI_ucnaBilleeInfo_txtExt').val($('#ContentPlaceHolderBody_NestedContentPlaceHolderBody_wzNOI_ucnaContactInfo_txtExt').val())
                    $('#ContentPlaceHolderBody_NestedContentPlaceHolderBody_wzNOI_ucnaBilleeInfo_txtMobile').val($('#ContentPlaceHolderBody_NestedContentPlaceHolderBody_wzNOI_ucnaContactInfo_txtMobile').val())
                    $('#ContentPlaceHolderBody_NestedContentPlaceHolderBody_wzNOI_ucnaBilleeInfo_txtEmail').val($('#ContentPlaceHolderBody_NestedContentPlaceHolderBody_wzNOI_ucnaContactInfo_txtEmail').val())

                }

                e.preventDefault();
            });

            $('#ddlProjectType').each(function (e) {
                //debugger;
                if ($(this).val() == '10') {  // if the projecttype is 'Other'
                    $('#txtProjectTypeOther').prop('disabled', false)
                    ConfigureValidators($("#txtProjectTypeOther")[0], true)
                }
                else {
                    $('#txtProjectTypeOther').val('');
                    $('#txtProjectTypeOther').prop('disabled', true);
                    ConfigureValidators($("#txtProjectTypeOther")[0], false)
                }

                $(this).on("change", function (e) {

                    if ($(this).val() == '10') {  // if the projecttype is 'Other' //'#ddlProjectType option:selected'
                        $('#txtProjectTypeOther').prop('disabled', false)
                        ConfigureValidators($("#txtProjectTypeOther")[0], true)
                    }
                    else {
                        $('#txtProjectTypeOther').val('');
                        $('#txtProjectTypeOther').prop('disabled', true);
                        ConfigureValidators($("#txtProjectTypeOther")[0], false)
                    }
                });

            });



            $('#ddlSWBMP').each(function (e) {

                if ($(this).val() == '8') {  //if the SWBMP is otherBMP

                    $('#txtBMPOtherName').prop('disabled', false)
                    ConfigureValidators($("#txtBMPOtherName")[0], true)
                }
                else {
                    //debugger;
                    $('#txtBMPOtherName').val('');
                    $('#txtBMPOtherName').prop('disabled', true)
                    ConfigureValidators($("#txtBMPOtherName")[0], false)
                }

                $(this).on("change", function (e) {
                    if ($(this).val() == '8') {    //if the SWBMP is otherBMP

                        $('#txtBMPOtherName').prop('disabled', false)
                        ConfigureValidators($("#txtBMPOtherName")[0], true)
                    }
                    else {
                        //debugger;
                        $('#txtBMPOtherName').val('');
                        $('#txtBMPOtherName').prop('disabled', true)
                        ConfigureValidators($("#txtBMPOtherName")[0], false)
                    }
                });
            })


            //$('#txtLatitude').on("change", function (e) {
               // $('#hfX').val($(this).val())
         //   })
//
           // $('#txtLongitude').on("change", function (e) {
            //    $('#hfY').val($(this).val())
            //  })

            $("#MapModal").draggable({
                handle: ".modal-header"
            });

            $("#Wizard1_lvOutfall_btnMapOutfallPoint").click(function () {
                $("#MapModal").modal({ backdrop: "static" });
                $("#MapModal").modal("show");
                createMap('mapOne');
            });


            $('#txtLatitude').on("focusout", function (e) {
                if ($(this).val().length > 0 && $('#txtLongitude').val().length>0)
                {
                    WaterShedSearchByLatLong($(this).val(), $('#txtLongitude').val());
                }
                else {
                    $('#hfX').val('')
                    $('#hfY').val('')
                    document.getElementById("txtWatershed").value = ''
                    document.getElementById("hfWaterShedCode").value = ''

                }
            })
            
            $('#txtLongitude').on("focusout", function (e) {
                if ($(this).val().length > 0 && $('#txtLatitude').val().length > 0) {
                    WaterShedSearchByLatLong($('#txtLatitude').val(), $(this).val());
                }
                else {
                    $('#hfX').val('')
                    $('#hfY').val('')
                    document.getElementById("txtWatershed").value = ''
                    document.getElementById("hfWaterShedCode").value = ''
                }
              })


            //useful function to disable the .net validators client side.
            function ConfigureValidators(control, enabled) {
                //debugger;
                //enabled = enabled || false;
                //control.style.visibility=!enabled
                if (typeof Page_Validators != 'undefined') {
                    for (i = 0; i <= Page_Validators.length; i++) {
                        if (Page_Validators[i] != null) {
                            //var visible = $('#' + Page_Validators[i].controltovalidate).parent().is(':visible');
                            if (Page_Validators[i].controltovalidate == control.id || Page_Validators[i].id == control.id) {
                                //validator.enabled = enabled;
                                //Page_Validators[i].enabled = enabled;
                                ValidatorEnable(Page_Validators[i], enabled);

                            }

                        }
                    }
                };

            }
        })


        function cvTaxparcel_ClientValidation(source, arguments) {
            if ($('#hfTaxParcel').val() == '') {
                //arguments.IsValid = false;
            }
            else {
                //arguments.IsValid = true;
            }
        }

        function cvSWPPPYesNo_ClientValidation(source, arguments) {
            if (arguments.Value == "No") {
                arguments.IsValid = false;
            }
            else {
                arguments.IsValid = true;
            }

        }

        function cvCheckProjectName_ClientValidation(source, arguments) {
            var bool = true;
            //$.getJSON('../api/ProjectExists/' + arguments.Value)
            //    .done(function (data,source,arguments) {
            //       // debugger;
            //        if (data == null) {
            //            arguments.IsValid = true;
            //        }
            //        else {
            //            arguments.IsValid = false;
            //        }
            //    });
           // debugger;
           // if ($('#hfNOISubmissionID').val() == '') {

                $.ajax({
                    url: "../api/ProjectExists",
                    data: { pname: arguments.Value, pitypeid: $('#hfPiTypeID').val() },       //encodeURIComponent(arguments.Value)},
                    datatype: "json",
                    type: "GET",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    success: function (msg) {
                        if (msg.length == 0) {
                            bool = true;
                        }
                        else {
                            bool = false
                        }
                    }
                });
            //}

            arguments.IsValid = bool;
        }

        function cvNoExposure_ClientValidation(source, arguments) {
            var bool = true;
            $('.noexposurecheck').each(function (index) {
                if ($(this).val() == "")
                {
                    source.errormessage = "Please answer to all the questions."
                    source.innerHTML = "Please answer to all the questions."
                    source.title = "Please answer to all the questions."
                    bool = false;
                    return false;
                }
            });

            if (bool == true)
            {
                $('.noexposurecheck').each(function (index) {
                    if ($(this).val() == 'Y') {
                        source.errormessage = "Not eligible for the No Exposure."
                        source.innerHTML = "Not eligible for the No Exposure."
                        source.title = "Not eligible for the No Exposure."
                        bool = false;
                        return false;
                    }
                });
            }
            arguments.IsValid = bool;
        }

        function cvAnnlTreatmentThreshold_ClientValidation(source, arguments) {
            var bool = false;
            $('.anntreatmentthreshold').find('input:checkbox, input:radio').each(function (index) {
                if ($(this).is(':checked')) {
                    bool = true;
                }
            })
            arguments.IsValid = bool;

        }

    </script>
    <script src="https://js.arcgis.com/4.8/"></script>
    <script src="<%=ResolveUrl("~/MapJS/initmap4.js")%>" type="text/javascript"></script>
    <script src="<%=ResolveUrl("~/MapJS/initmapoutfall.js")%>" type="text/javascript"></script>
</asp:Content>
