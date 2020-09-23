<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/CLFNested.master" CodeBehind="NOTCoPermittee.aspx.vb" Inherits="NoticeOfIntent.NOTCoPermittee" %>
<%@ Register src="../UserControls/ucNameAddressInfo.ascx" tagname="ucNameAddressInfo" tagprefix="uc1" %>
<%@ Register src="../UserControls/ucNameAddressInfoReadOnly.ascx" tagname="ucNameAddressInfoReadOnly" tagprefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="NestedContentPlaceHolderHeader" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NestedContentPlaceHolderPageTitle" runat="server">
    <div class="page-header well text-center font-bold">
        <h1>Notice of Termination (NOT) <small>of Shared Operational Control
            <br />
            for Storm Water Discharges Associated With<br />
            CONSTRUCTION ACTIVITY Under a NPDES General Permit</small></h1>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="NestedContentPlaceHolderBody" runat="server">
<%--    <div>
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="alert alert-danger" ShowModelStateErrors="true" EnableClientScript="true" DisplayMode="BulletList" HeaderText="<div class='validationheader'>&nbsp;Please correct the following error:</div>" />
    </div>--%>
    <div>
        <div class="panel panel-default">
            <div class="panel-heading">
                <h2 class="panel-title font-bold">Permit Information</h2>
            </div>
            <div class="panel-body input-group-sm">
                <div class="row">
                    <div class="col-md-6 input-group-sm">
                        <asp:Label ID="lblNOIID" AssociatedControlID="lblNOIIDDisplay" runat="server" Text="NOI ID#:" CssClass="control-label "></asp:Label>
                        <asp:Label ID="lblNOIIDDisplay" runat="server" Text="" CssClass="form-control-static  well-sm"></asp:Label>
                        <asp:HiddenField ID="hfNOISubmissionID" Value="" runat="server" />
                    </div>
                    <div class="col-md-6 input-group-sm">
                        <asp:Label ID="lblNOIDateReceived" AssociatedControlID="lblNOIDateReceivedDisplay" runat="server" Text="Date Received:" CssClass="control-label"></asp:Label>
                        <asp:Label ID="lblNOIDateReceivedDisplay" runat="server" Text="" CssClass="form-control-static well-sm"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <uc2:ucNameAddressInfoReadOnly ID="ucnaProjectDetails" runat="server" CompanyNameLabel="Project Name:" companytypevisible="false" personnamevisible="false" address1visible="false" address2visible="false" CityZipStateVisible="false" phonevisible="false" emailvisible="false" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6 input-group-sm">
                        <asp:Label ID="lblCoPermitteeAppRecdDate" AssociatedControlID="lblCoPermitteeAppRecDateDisplay" runat="server" Text="Co-Permittee Application Date Received:" CssClass="control-label"></asp:Label>
                        <asp:Label ID="lblCoPermitteeAppRecDateDisplay" runat="server" Text="" CssClass="form-control-static well-sm"></asp:Label>
                    </div>
                </div>
            </div>
        </div>
        <div class="panel panel-default">
            <div class="panel-heading">
                <h2 class="panel-title font-bold">Co-Permittee Information</h2>
            </div>
            <div class="panel-body input-group-sm">
                <uc2:ucNameAddressInfoReadOnly ID="ucnaCoPermitteeInfo" runat="server" companytypevisible="false" countymunicipalityvisible="false" />
            </div>
        </div>
        <div class="panel panel-default">
            <div class="panel-heading">
                <h2 class="panel-title font-bold">Termination of Coverage Information</h2>
            </div>
            <div class="panel-body">
                <div class="form-inline">
                    <div class="form-group input-group-sm">
                        <label class="control-label">Co-Permittee Construction Completion Date:</label>
                        <asp:TextBox ID="txtCompletionDate" runat="server" CssClass="form-control datepicker" MaxLength="10"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvCompletionDate" ControlToValidate="txtCompletionDate" runat="server" Display="Dynamic" EnableClientScript="true" CssClass="alert-text" ToolTip="Completion Date is required." ErrorMessage="Completion Date is required." ValidationGroup="ValidateNOI">*</asp:RequiredFieldValidator>
                    </div>
                </div>
                <br />
                <div class="form-group input-group-sm">
                    <label class="control-label">Have all items and conditions of the Plan been satisfied in accordance with the Delaware Sediment and Stormwater Regulations?</label>
                    <div>
                        <asp:RadioButtonList ID="rblStatisfiedYesNo" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" CssClass="radio">
                            <asp:ListItem Value="Y" Text="Yes"></asp:ListItem>
                            <asp:ListItem Value="N" Text="No"></asp:ListItem>
                        </asp:RadioButtonList>
                        <asp:RequiredFieldValidator ID="rfvSatisfiedYesNo" ControlToValidate="rblStatisfiedYesNo" runat="server" Display="Dynamic" EnableClientScript="true" CssClass="alert-text" ToolTip="Please select an option for is plan been satisfied." ErrorMessage="Please select an option for is plan been satisfied." ValidationGroup="ValidateNOI">*</asp:RequiredFieldValidator>
                        <asp:CustomValidator ID="cvSatisfiedYesNo" runat="server" ControlToValidate="rblStatisfiedYesNo" EnableClientScript="true" Display="Dynamic" CssClass="alert-text" ErrorMessage="This item needs to be satisfied before termination can be completed." ClientValidationFunction="cvSatisfiedYesNo_ClientValidation" ValidationGroup="ValidateNOI"></asp:CustomValidator>
                    </div>
                </div>
                <div class="form-group input-group-sm">
                    <label class="control-label">Has as-built documentation verified that the permanent stormwater management measures have been constructed in accordance with the approved Plan and the Delaware Sediment and Stormwater?</label>
                    <div>
                        <asp:RadioButtonList ID="rblVerifiedYesNO" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" CssClass="radio">
                            <asp:ListItem Value="Y" Text="Yes"></asp:ListItem>
                            <asp:ListItem Value="N" Text="No"></asp:ListItem>
                        </asp:RadioButtonList>
                        <asp:RequiredFieldValidator ID="rfvVerifiedYesNo" ControlToValidate="rblVerifiedYesNO" runat="server" Display="Dynamic" EnableClientScript="true" CssClass="alert-text" ToolTip="Please select an option is verified with approved plan." ErrorMessage="Please select an option is verified with approved plan." ValidationGroup="ValidateNOI">*</asp:RequiredFieldValidator>
                        <asp:CustomValidator ID="cvVerifiedYesNO" runat="server" ControlToValidate="rblVerifiedYesNO" EnableClientScript="true" Display="Dynamic" CssClass="alert-text" ErrorMessage="This item needs to be satisfied before termination can be completed." ClientValidationFunction="cvVerifiedYesNO_ClientValidation" ValidationGroup="ValidateNOI"></asp:CustomValidator>
                    </div>
                </div>
                <div class="form-group input-group-sm">
                    <label class="control-label">Has final stabilzation of the site been achieved?</label>
                    <div>
                        <asp:RadioButtonList ID="rblAchievedYesNo" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" CssClass="radio">
                            <asp:ListItem Value="Y" Text="Yes"></asp:ListItem>
                            <asp:ListItem Value="N" Text="No"></asp:ListItem>
                        </asp:RadioButtonList>
                        <asp:RequiredFieldValidator ID="rfvAchievedYesNo" ControlToValidate="rblAchievedYesNo" runat="server" Display="Dynamic" EnableClientScript="true" CssClass="alert-text" ToolTip="Please select an option is final stabilzation is achieved." ErrorMessage="Please select an option is final stabilzation is achieved." ValidationGroup="ValidateNOI">*</asp:RequiredFieldValidator>
                        <asp:CustomValidator ID="cvAchievedYesNo" runat="server" ControlToValidate="rblAchievedYesNo" EnableClientScript="true" Display="Dynamic" CssClass="alert-text" ErrorMessage="This item needs to be satisfied before termination can be completed." ClientValidationFunction="cvAchievedYesNo_ClientValidation" ValidationGroup="ValidateNOI"></asp:CustomValidator>
                    </div>
                </div>
            </div>
        </div>
        <div class="form-group">
            <asp:Label ID="lblComments" AssociatedControlID="txtComments" runat="server" Text="Comments:" CssClass="control-label"></asp:Label>
            <asp:TextBox ID="txtComments" runat="server" TextMode="MultiLine" Rows="5" MaxLength="2000" CssClass="form-control input-group-sm"></asp:TextBox>
        </div>
        <div class="well clearfix">
            <div class="col-md-4 clearfix pull-right">
                <table>
                    <tr>
                        <td>
                            <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-default btn-block" ValidationGroup="ValidateNOI" />
                        </td>
                        <td>
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-default btn-block" CausesValidation="false"  />           
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>

<%--    <td>
                            <asp:Button ID="btnSubmit" runat="server" Text="Save & Submit" CssClass="btn btn-default btn-lg btn-block" />
                        </td>--%>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="NestedContentPlaceHolderJS" runat="server">
    <script>

        function cvSatisfiedYesNo_ClientValidation(source, arguments) {
            if (arguments.Value == "N") {
                arguments.IsValid = false;
            }
            else {
                arguments.IsValid = true;
                return;
            }

        }

        function cvVerifiedYesNO_ClientValidation(source, arguments) {
            if (arguments.Value == "N") {
                arguments.IsValid = false;
            }
            else {
                arguments.IsValid = true;
                return;
            }

        }

        function cvAchievedYesNo_ClientValidation(source, arguments) {
            if (arguments.Value == "N") {
                arguments.IsValid = false;
            }
            else {
                arguments.IsValid = true;
                return;
            }

        }
    </script>
</asp:Content>
