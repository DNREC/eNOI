<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ucTaxParcelKent.ascx.vb" Inherits="NoticeOfIntent.ucTaxParcelKent" %>
<div class="col-md-9 gutter-5">
    <div class="col-md-3 gutter-5">
        <asp:DropDownList ID="ddlTaxKentHundred" runat="server" CssClass="form-control input-xs" ItemType="NoticeOfIntent.TaxKentHundred" DataTextField="Text" DataValueField="Value" SelectMethod="GetTaxKentHundred">
        </asp:DropDownList>
    </div>
    <div class="col-md-3 gutter-5">
        <asp:DropDownList ID="ddlKentCommunityCode" runat="server" CssClass="form-control input-xs" ItemType="NoticeOfIntent.TaxKentTown" DataTextField="Text" DataValueField="Value" SelectMethod="GetTaxKentTowns">
        </asp:DropDownList>
    </div>
    <div class="col-md-1 gutter-5">
        <asp:TextBox ID="txtKentTP3" CssClass="form-control input-xs text-center" MaxLength="3" runat="server"></asp:TextBox>
    </div>
    <div class="col-md-1 gutter-5">
        <asp:TextBox ID="txtKentTP4" CssClass="form-control input-xs text-center" MaxLength="2" runat="server"></asp:TextBox>
    </div>
    <div class="col-md-1 gutter-5">
        <asp:TextBox ID="txtKentTP5" CssClass="form-control input-xs text-center" MaxLength="2" runat="server"></asp:TextBox>
    </div>
    <div class="col-md-1 gutter-5">
        <asp:TextBox ID="txtKentTP6" CssClass="form-control input-xs text-center" MaxLength="2" runat="server"></asp:TextBox>
    </div>
    <div class="col-md-1 gutter-5">
        <asp:TextBox ID="txtKentTP7" CssClass="form-control input-xs text-center" MaxLength="2" runat="server"></asp:TextBox>
    </div>
    <div class="col-md-1 gutter-5">
        <asp:TextBox ID="txtKentTP8" CssClass="form-control input-xs text-center" MaxLength="3" runat="server"></asp:TextBox>
    </div>
</div>
