<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ucNameAddressInfo.ascx.vb" Inherits="NoticeOfIntent.ucNameAddressInfo" %>
<div class="row" id="divcompanytype" runat="server">
    <div class="col-md-6 input-group-sm">
        <asp:Label ID="lblCompanyType" AssociatedControlID="ddlCompanyType" runat="server" Text="Company Type" CssClass="control-label"></asp:Label>
        <asp:DropDownList ID="ddlCompanyType" runat="server" CssClass="form-control font-bold" DataValueField="PersonOrgTypeCode" DataTextField="PersonOrgType" ItemType="NoticeOfIntent.CompanyTypelst" SelectMethod="GetCompanyType">
         </asp:DropDownList>
    </div>
</div>
<div id="divcompanyname" class="input-group-sm" runat="server">
    <asp:Label ID="lblCompanyName" AssociatedControlID="txtCompanyName" runat="server" Text="Company Name *" CssClass="control-label"></asp:Label>
    <asp:TextBox ID="txtCompanyName" runat="server" CssClass="form-control font-bold" MaxLength="80" ></asp:TextBox>
    <asp:RequiredFieldValidator ID="rfvCompanyName" runat="server" ControlToValidate="txtCompanyName" Display="Dynamic" EnableClientScript="true" CssClass="alert-text" ToolTip='<%# "Company Name is required for " + ControlName()%>'  ErrorMessage='<%# "Company Name is required for " + ControlName()%>'>*</asp:RequiredFieldValidator>
    <asp:CustomValidator ID="cvCheckProjectName" runat="server" ControlToValidate="txtCompanyName" Display="Dynamic" EnableClientScript="true" CssClass="alert-text" ErrorMessage="Project Name already exists. Please change the name to be unique." ToolTip="Project Name already exists. Please change the name to be unique."></asp:CustomValidator>
</div>
<div class="row" id="divpersonname" runat="server">
    <div class="col-md-6 input-group-sm">
		<asp:Label ID="lblLastName" AssociatedControlID="txtLastName" runat="server" Text="Last Name *"  CssClass="control-label"></asp:Label>
		<asp:TextBox ID="txtLastName" runat="server" CssClass="form-control font-bold" MaxLength="30" placeholder="Last Name"></asp:TextBox>
        <asp:RequiredFieldValidator ID="rfvLastName" runat="server" ControlToValidate="txtLastName" Display="Dynamic" EnableClientScript="true" CssClass="alert-text" ToolTip='<%# "Last Name is required for " + ControlName()%>' ErrorMessage='<%# "Last Name is required for " + ControlName()%>'>*</asp:RequiredFieldValidator>
    </div>
    <div class="col-md-6 input-group-sm">
        <asp:Label ID="lblFirstName" AssociatedControlID="txtFirstName" runat="server" Text="First Name *" CssClass="control-label"></asp:Label>
		<asp:TextBox ID="txtFirstName" runat="server" CssClass="form-control font-bold" MaxLength="20" placeholder="First Name"></asp:TextBox>
        <asp:RequiredFieldValidator ID="rfvFirstName" runat="server" ControlToValidate="txtFirstName" Display="Dynamic" EnableClientScript="true" CssClass="alert-text" ToolTip='<%# "First Name is required for " + ControlName()%>' ErrorMessage='<%# "First Name is required for " + ControlName()%>'>*</asp:RequiredFieldValidator>
    </div>
</div>
<div id="divaddress1" class="input-group-sm" runat="server">
	<asp:Label ID="lblAddress1" AssociatedControlID="txtAddress1" runat="server" Text="Address1 *" CssClass="control-label"></asp:Label>
	<asp:TextBox ID="txtAddress1" runat="server" CssClass="form-control font-bold" MaxLength="50" placeholder="Address1"></asp:TextBox>
    <asp:RequiredFieldValidator ID="rfvAddress1" runat="server" ControlToValidate="txtAddress1" Display="Dynamic" EnableClientScript="true" CssClass="alert-text" ToolTip='<%# "Address1 is required for " + ControlName()%>' ErrorMessage='<%# "Address1 is required for " + ControlName()%>'>*</asp:RequiredFieldValidator>
</div>
<div id="divaddress2" class="input-group-sm" runat="server">
	<asp:Label ID="lblAddress2" AssociatedControlID="txtAddress2" runat="server" Text="Address2" CssClass="control-label"></asp:Label>
	<asp:TextBox ID="txtAddress2" runat="server" CssClass="form-control font-bold" MaxLength="50" placeholder="Address2"></asp:TextBox>
</div>
<div id="divcityzipstate" runat="server" class="row">
    <div class="col-md-3 input-group-sm">
        <asp:Label ID="lblZip" AssociatedControlID="txtZip" runat="server" Text="Zip *" CssClass="control-label"></asp:Label>
        <asp:TextBox ID="txtZip" runat="server" CssClass="form-control font-bold" MaxLength="14" placeholder="Zip"></asp:TextBox>
        <asp:RequiredFieldValidator ID="rfvZip" runat="server" ControlToValidate="txtZip" Display="Dynamic" EnableClientScript="true" CssClass="alert-text" ToolTip='<%# "Zip is required for " + ControlName()%>' ErrorMessage='<%# "Zip is required for " + ControlName()%>'>*</asp:RequiredFieldValidator>
    </div>
    <div class="col-md-6 input-group-sm">
        <asp:Label ID="lblCity" AssociatedControlID="txtCity" runat="server" Text="City *" CssClass="control-label"></asp:Label>
        <asp:TextBox ID="txtCity" runat="server" CssClass="form-control font-bold" MaxLength="30" placeholder="City"></asp:TextBox>
        <asp:RequiredFieldValidator ID="rfvCity" runat="server" ControlToValidate="txtCity" Display="Dynamic" EnableClientScript="true" CssClass="alert-text" ToolTip='<%# "City is required for " + ControlName()%>' ErrorMessage='<%# "City is required for " + ControlName()%>'>*</asp:RequiredFieldValidator>
    </div>
    <div class="col-md-3 input-group-sm">
        <asp:Label ID="lblState" AssociatedControlID="ddlStateAbv" runat="server" Text="State *" CssClass="control-label"></asp:Label>
        <asp:DropDownList ID="ddlStateAbv" runat="server" CssClass="form-control font-bold" DataValueField="StateAbv" DataTextField="State" ItemType="NoticeOfIntent.StateAbvlst" SelectMethod="GetStates">
        </asp:DropDownList>
        <asp:RequiredFieldValidator ID="rfvStateAbv" runat="server" ControlToValidate="ddlStateAbv" Display="Dynamic" EnableClientScript="true" InitialValue="--" CssClass="alert-text" ToolTip='<%# "State is required for " + ControlName()%>' ErrorMessage='<%# "State is required for " + ControlName()%>'>*</asp:RequiredFieldValidator>
    </div>
</div>
<div class="row" id="divphone" runat="server">
    <div class="col-md-4 input-group-sm">
        <asp:Label ID="lblPhone" AssociatedControlID="txtPhone" runat="server" Text="Phone *" CssClass="control-label"></asp:Label>
        <asp:TextBox ID="txtPhone" runat="server" CssClass="form-control font-bold" MaxLength="16" placeholder="Phone"></asp:TextBox>
        <asp:RequiredFieldValidator ID="rfvPhone" runat="server" ControlToValidate="txtPhone" Display="Dynamic" EnableClientScript="true" CssClass="alert-text" ToolTip='<%# "Phone is required for " + ControlName()%>' ErrorMessage='<%# "Phone is required for " + ControlName()%>'>*</asp:RequiredFieldValidator>
    </div>
    <div class="col-md-3 input-group-sm">
        <asp:Label ID="lblExt" AssociatedControlID="txtExt" runat="server" Text="Ext" CssClass="control-label"></asp:Label>
        <asp:TextBox ID="txtExt" runat="server" CssClass="form-control font-bold" MaxLength="50" placeholder="Ext"></asp:TextBox>
    </div>
    <div class="col-md-5 input-group-sm">
        <asp:Label ID="lblMobile" AssociatedControlID="txtMobile" runat="server" Text="Mobile" CssClass="control-label"></asp:Label>
        <asp:TextBox ID="txtMobile" runat="server" CssClass="form-control font-bold" MaxLength="16" placeholder="Mobile" ></asp:TextBox>
    </div>
</div>
<div id="divemail" class="input-group-sm" runat="server">
    <asp:Label ID="lblEmail" AssociatedControlID="txtEmail" runat="server" Text="Email *" CssClass="control-label"></asp:Label>
    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control font-bold" MaxLength="50" placeholder="Email" ></asp:TextBox>
    <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail" Display="Dynamic" EnableClientScript="true" CssClass="alert-text" ToolTip='<%# "Email is required for " + ControlName()%>' ErrorMessage='<%# "Email is required for " + ControlName()%>'>*</asp:RequiredFieldValidator>
</div>
<div class="row" id="countymunicipality" runat="server">
    <div class="col-md-6 input-group-sm">
        <asp:Label ID="lblCounty" AssociatedControlID="ddlCounty" runat="server" Text="County *" CssClass="control-label"></asp:Label>
        <asp:DropDownList ID="ddlCounty" runat="server" CssClass="form-control font-bold">
            <asp:ListItem Text="Select" Value=""></asp:ListItem>
            <asp:ListItem Text="New Castle" Value="New Castle"></asp:ListItem>
            <asp:ListItem Text="Kent" Value="Kent"></asp:ListItem>
            <asp:ListItem Text="Sussex" Value="Sussex"></asp:ListItem>
        </asp:DropDownList>
        <asp:RequiredFieldValidator ID="rfvCounty" runat="server" ControlToValidate="ddlCounty" Display="Dynamic" EnableClientScript="true" InitialValue="" CssClass="alert-text" ToolTip='<%# "County is required for " + ControlName()%>' ErrorMessage='<%# "County is required for " + ControlName()%>'>*</asp:RequiredFieldValidator>
    </div>
    <div class="col-md-6 input-group-sm">
        <asp:Label ID="lblMunicipality" AssociatedControlID="txtMunicipality" runat="server" Text="Municipality * (Enter N/A if unincorporated)" CssClass="control-label"></asp:Label>
        <asp:TextBox ID="txtMunicipality" runat="server" CssClass="form-control font-bold" MaxLength="50" placeholder="Municipality"></asp:TextBox>
        <asp:RequiredFieldValidator ID="rfvMunicipality" runat="server" ControlToValidate="txtMunicipality" Display="Dynamic" EnableClientScript="true" CssClass="alert-text" ToolTip='<%# "Municipality is required for " + ControlName()%>' ErrorMessage='<%# "Municipality is required for " + ControlName()%>'>*</asp:RequiredFieldValidator>
    </div>
</div>   
