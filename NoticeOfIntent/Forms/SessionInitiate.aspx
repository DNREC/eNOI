<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/CLFNested.master" CodeBehind="SessionInitiate.aspx.vb" Inherits="NoticeOfIntent.SessionInitiate" %>
<asp:Content ID="Content1" ContentPlaceHolderID="NestedContentPlaceHolderHeader" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NestedContentPlaceHolderPageTitle" runat="server">

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="NestedContentPlaceHolderBody" runat="server">
    <div class="panel panel-default">
        <div class="panel-heading">
            <h1 class="panel-title font-bold">Billing Information</h1>
        </div>
        <div class="panel-body input-group-sm">
            <div class="row input-group-sm">
                <label Class="control-label col-md-2">First Name:*</label>
                <div class="col-md-10">
				    <asp:textbox id="txtFirstName" runat="server" MaxLength="60"  CssClass="form-control"></asp:textbox>
                    <asp:RequiredFieldValidator ID="rfvFirstName" runat="server" ControlToValidate="txtFirstName" EnableClientScript="false" ErrorMessage="Enter First Name"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="row input-group-sm">
                <label Class="control-label col-md-2">Last Name:*</label>
                <div class="col-md-10">
				    <asp:textbox id="txtLastName" runat="server" MaxLength="60" CssClass="form-control"></asp:textbox>
                    <asp:RequiredFieldValidator ID="rfvLastName" runat="server" ControlToValidate="txtLastName" EnableClientScript="false" ErrorMessage="Enter Last Name"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="row input-group-sm">
                <label Class="control-label col-md-2">Address 1:*</label>
                <div class="col-md-10">
				    <asp:textbox id="txtAddress1" runat="server" MaxLength="100"  CssClass="form-control"></asp:textbox>
                    <asp:RequiredFieldValidator ID="rfvAddress1" runat="server" ControlToValidate="txtAddress1" EnableClientScript="false" ErrorMessage="Enter Address"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="row input-group-sm">
                <label Class="control-label col-md-2">City:*</label>
                <div class="col-md-10">
				    <asp:TextBox id="txtCity" runat="server" maxlength="30"  CssClass="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvCity" runat="server" ControlToValidate="txtCity" EnableClientScript="false" ErrorMessage="Enter City"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="row input-group-sm">
                <label Class="control-label col-md-2">State:*</label>
                <div class="col-md-10">
	                <asp:DropDownList id="ddlStateAbv" runat="server" CssClass="form-control" DataValueField="StateAbv" DataTextField="State" ItemType="NoticeOfIntent.StateAbvlst" SelectMethod="GetStates">
	                </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvState" runat="server" ControlToValidate="ddlStateAbv" EnableClientScript="false" InitialValue="0" ErrorMessage="Select State"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="row input-group-sm">
                <label Class="control-label col-md-2">Zip:*</label>
                <div class="col-md-10">
	                <asp:TextBox id="txtZip" MaxLength="5" runat="server" Columns="5" CssClass="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvZip" runat="server" ControlToValidate="txtZip" EnableClientScript="false" ErrorMessage="Enter Zip Code"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="row input-group-sm">
                <label Class="control-label col-md-2">Email:*</label>
                <div class="col-md-10">
                    <asp:TextBox ID="txtEmail" MaxLength="60" Columns="50" runat="server" CssClass="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail" EnableClientScript="false" ErrorMessage="Enter Email Address"></asp:RequiredFieldValidator>

                </div>
            </div>
            <div class="row input-group-sm">
                <label Class="control-label col-md-2">Required Payment:</label>
                <div class="col-md-10">
                    <asp:Label ID="lblRequiredPayment" runat="server"></asp:Label>
                    <asp:TextBox ID="txtRequiredPayment" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
        </div>
        <div class="panel-footer input-group-sm">
            <asp:Button ID="btnBack" runat="server" Text="Cancel" CssClass="btn btn-default" CausesValidation="false" />
            <asp:Button ID="btnContinue" runat="server" Text="Continue" CssClass="btn btn-default" />
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="NestedContentPlaceHolderJS" runat="server">
</asp:Content>
