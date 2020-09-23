<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ucNameAddressInfoReadOnly.ascx.vb" Inherits="NoticeOfIntent.ucNameAddressInfoReadOnly" %>
<div class="row" id="divcompanytype" runat="server">
    <div class="col-md-6 input-group-sm">
        <asp:Label ID="lblCompanyType" AssociatedControlID="lblCompanyTypeDisp" runat="server" Text="Company Type:" CssClass="control-label"></asp:Label>
        <asp:Label ID="lblCompanyTypeDisp" runat="server" Text="" CssClass="form-control-static  well-sm"></asp:Label>
        <asp:HiddenField ID="hfCompanyType" runat="server" Value="" />
    </div>
</div>
<div id="divcompanyname" class="row" runat="server">
    <div class="col-md-11 input-group-sm">
        <asp:Label ID="lblCompanyName" AssociatedControlID="lblCompanyNameDisp" runat="server" Text="Company Name:" CssClass="control-label"></asp:Label>
        <asp:Label ID="lblCompanyNameDisp" runat="server" Text="" CssClass="form-control-static  well-sm"></asp:Label>
    </div>
</div>
<div class="row" id="divpersonname" runat="server">
    <div class="col-md-6 input-group-sm">
		<asp:Label ID="lblLastName" AssociatedControlID="lblLastNameDisp" runat="server" Text="Last Name:"  CssClass="control-label"></asp:Label>
        <asp:Label ID="lblLastNameDisp" runat="server" Text="" CssClass="form-control-static  well-sm" ></asp:Label>
    </div>
    <div class="col-md-6 input-group-sm">
        <asp:Label ID="lblFirstName" AssociatedControlID="lblFirstNameDisp" runat="server" Text="First Name:" CssClass="control-label"></asp:Label>
        <asp:Label ID="lblFirstNameDisp" runat="server" Text="" CssClass="form-control-static  well-sm"></asp:Label>
    </div>
</div>
<div id="divaddress1" class="row" runat="server">
    <div class="col-md-11 input-group-sm">
	    <asp:Label ID="lblAddress1" AssociatedControlID="lblAddress1Disp" runat="server" Text="Address1:" CssClass="control-label"></asp:Label>
        <asp:Label ID="lblAddress1Disp" runat="server" Text="" CssClass="form-control-static  well-sm"></asp:Label>
    </div>
</div>
<div id="divaddress2" class="row" runat="server">
    <div class="col-md-11 input-group-sm">
        <asp:Label ID="lblAddress2" AssociatedControlID="lblAddress2Disp" runat="server" Text="Address2:" CssClass="control-label"></asp:Label>
        <asp:Label ID="lblAddress2Disp" runat="server" Text="" CssClass="form-control-static  well-sm"></asp:Label>
    </div>
</div>
<div id="divcityzipstate" runat="server" class="row">
    <div class="col-md-4 input-group-sm">
        <asp:Label ID="lblZip" AssociatedControlID="lblZipDisp" runat="server" Text="Zip:" CssClass="control-label"></asp:Label>
        <asp:Label ID="lblZipDisp" runat="server" Text="" CssClass="form-control-static  well-sm"></asp:Label>
    </div>
    <div class="col-md-4 input-group-sm">
        <asp:Label ID="lblCity" AssociatedControlID="lblCityDisp" runat="server" Text="City:" CssClass="control-label"></asp:Label>
        <asp:Label ID="lblCityDisp" runat="server" Text="" CssClass="form-control-static  well-sm"></asp:Label>
    </div>
    <div class="col-md-4 input-group-sm">
        <asp:Label ID="lblState" AssociatedControlID="lblStateDisp" runat="server" Text="State:" CssClass="control-label"></asp:Label>
        <asp:HiddenField ID="hfStateAbv" runat="server" value="" />
        <asp:Label ID="lblStateDisp" runat="server" Text="" CssClass="form-control-static  well-sm"></asp:Label>
    </div>
</div>
<div class="row" id="divphone" runat="server">
    <div class="col-md-4 input-group-sm">
        <asp:Label ID="lblPhone" AssociatedControlID="lblPhoneDisp" runat="server" Text="Phone:" CssClass="control-label"></asp:Label>
        <asp:Label ID="lblPhoneDisp" runat="server" Text="" CssClass="form-control-static  well-sm"></asp:Label>
    </div>
    <div class="col-md-4 input-group-sm">
        <asp:Label ID="lblExt" AssociatedControlID="lblExtDisp" runat="server" Text="Ext:" CssClass="control-label"></asp:Label>
        <asp:Label ID="lblExtDisp" runat="server" Text="" CssClass="form-control-static  well-sm"></asp:Label>
    </div>
    <div class="col-md-4 input-group-sm">
        <asp:Label ID="lblMobile" AssociatedControlID="lblMobileDisp" runat="server" Text="Mobile:" CssClass="control-label"></asp:Label>
        <asp:Label ID="lblMobileDisp" runat="server" Text="" CssClass="form-control-static  well-sm"></asp:Label>
    </div>
</div>
<div id="divemail" class="input-group-sm" runat="server">
    <asp:Label ID="lblEmail" AssociatedControlID="lblEmailDisp" runat="server" Text="Email:" CssClass="control-label"></asp:Label>
    <asp:Label ID="lblEmailDisp" runat="server" Text="" CssClass="form-control-static  well-sm"></asp:Label>
</div>
<div class="row" id="countymunicipality" runat="server">
    <div class="col-md-6 input-group-sm">
        <asp:Label ID="lblCounty" AssociatedControlID="lblCountyDisp" runat="server" Text="County:" CssClass="control-label"></asp:Label>
        <asp:Label ID="lblCountyDisp" runat="server" Text="" CssClass="form-control-static  well-sm"></asp:Label>
    </div>
    <div class="col-md-6 input-group-sm">
        <asp:Label ID="lblMunicipality" AssociatedControlID="lblMunicipalityDisp" runat="server" Text="Municipality:" CssClass="control-label"></asp:Label>
        <asp:Label ID="lblMunicipalityDisp" runat="server" Text="" CssClass="form-control-static  well-sm"></asp:Label>
    </div>
</div>    
