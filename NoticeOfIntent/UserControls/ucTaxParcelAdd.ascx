<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ucTaxParcelAdd.ascx.vb" Inherits="NoticeOfIntent.ucTaxParcelAdd" %>
<div class="row">
    <asp:Label ID="lblSelectCounty" AssociatedControlID="rbNewCastle" runat="server" Text="Select County:" CssClass="col-md-3 control-label"></asp:Label>
    <div class="col-md-9">
        <asp:RadioButton ID="rbNewCastle" runat="server" GroupName="grpCountySelection" ClientIDMode="Static" Text="New Castle" Checked="true" CssClass="radio-inline countyselect" data-parent="#pnltaxparcelbody" data-target="#divNewCastle"   />
        <asp:RadioButton ID="rbKent" runat="server" GroupName="grpCountySelection" ClientIDMode="Static" Text="Kent" CssClass="radio-inline countyselect" data-target="#divKent" data-parent="#pnltaxparcelbody"   />
        <asp:RadioButton ID="rbSussex" runat="server" GroupName="grpCountySelection" ClientIDMode="Static" Text="Sussex" CssClass="radio-inline countyselect" data-target="#divSussex" data-parent="#pnltaxparcelbody"   />
    </div>
</div>
<div id="divNewCastle" class="panel-collapse collapse">
    <div class="row">
        <label class="col-md-3 control-label">Tax Parcel Sample:</label>
        <div class="col-md-9">
            <label>06-234.56-789</label>
        </div>
    </div>
    <div class="row">
        <div class="col-md-9 col-md-offset-3 gutter-5">
            <label class="form-control-static col-md-3 gutter-5 text-center">06</label>
            <label class="form-control-static col-md-3 gutter-5 text-center">234</label>
            <label class="form-control-static col-md-3 gutter-5 text-center">56</label>
            <label class="form-control-static col-md-3 gutter-5 text-center">789</label>
        </div>
    </div>
    <div class="row">
        <asp:Label ID="lblTPNCNumber" AssociatedControlID="txtTPNC1" CssClass="control-label col-md-3" runat="server" Text="Tax Parcel Number:"></asp:Label>
        <div class="col-md-9 gutter-5">
            <div class="col-md-3 gutter-5">
                <asp:TextBox ID="txtTPNC1" CssClass="form-control input-xs text-center" MaxLength="2" runat="server"></asp:TextBox>
            </div>
            <div class="col-md-3 gutter-5">
                <asp:TextBox ID="txtTPNC2" CssClass="form-control input-xs text-center" MaxLength="3" runat="server"></asp:TextBox>
            </div>
            <div class="col-md-3 gutter-5">
                <asp:TextBox ID="txtTPNC3" CssClass="form-control input-xs text-center" MaxLength="2" runat="server"></asp:TextBox>
            </div>
            <div class="col-md-3 gutter-5">
                <asp:TextBox ID="txtTPNC4" CssClass="form-control input-xs text-center" MaxLength="3" runat="server"></asp:TextBox>
            </div>
        </div>
    </div>
</div>
<div id="divKent" class="panel-collapse collapse">
    <div class="row">
        <label class="col-md-3 control-label">Tax Parcel Sample:</label>
        <div class="col-md-9">
            <label>MN-00-123.01-02-34.00.000</label>
        </div>
    </div>
    <div class="row">
        <div class="col-md-9 col-md-offset-3 gutter-5">
            <label class="form-control-static col-md-3 gutter-5 text-center">MN</label>
            <label class="form-control-static col-md-3 gutter-5 text-center">00</label>
            <label class="form-control-static col-md-1 gutter-5 text-center">123</label>
            <label class="form-control-static col-md-1 gutter-5 text-center">01</label>
            <label class="form-control-static col-md-1 gutter-5 text-center">02</label>
            <label class="form-control-static col-md-1 gutter-5 text-center">34</label>
            <label class="form-control-static col-md-1 gutter-5 text-center">00</label>
            <label class="form-control-static col-md-1 gutter-5 text-center">000</label>
        </div>
    </div>
    <div class="row input-group-sm">
        <asp:Label ID="lblTPKentNumber" AssociatedControlID="ddlTaxKentHundred" CssClass="control-label col-md-3" runat="server" Text="Tax Parcel Number:"></asp:Label>
        <div class="col-md-9 gutter-5">
            <div class="col-md-3 gutter-5">
                <asp:DropDownList ID="ddlTaxKentHundred" runat="server" CssClass="form-control input-xs" ItemType="NoticeOfIntent.TaxKentHundred" DataTextField="Text1" DataValueField="Value" SelectMethod="GetTaxKentHundred">
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
    </div>
</div>
<div id="divSussex" class="panel-collapse collapse gutter-5">
    <div class="row">
        <label class="col-md-3 control-label">Tax Parcel Sample:</label>
        <div class="col-md-9">
            <label>1-30-09.00-0046.00</label>
        </div>
    </div>
    <div class="row">
        <div class="col-md-9 col-md-offset-3  gutter-5">
            <label class="form-control-static col-md-2 gutter-5 text-center">1</label>
            <label class="form-control-static col-md-2 gutter-5 text-center">30</label>
            <label class="form-control-static col-md-2 gutter-5 text-center">09</label>
            <label class="form-control-static col-md-2 gutter-5 text-center">00</label>
            <label class="form-control-static col-md-2 gutter-5 text-center">0046</label>
            <label class="form-control-static col-md-2 gutter-5 text-center">00</label>
        </div>
    </div>
    <div class="row input-group-sm">
        <asp:Label ID="lblTPSussexNumber" AssociatedControlID="txtTPNC1" CssClass="control-label col-md-3" runat="server" Text="Tax Parcel Number:"></asp:Label>
        <div class="col-md-9 gutter-5">
            <div class="col-md-2 gutter-5">
                <asp:TextBox ID="txtTPSussex1" CssClass="form-control input-xs text-center" MaxLength="1" runat="server"></asp:TextBox>
            </div>
            <div class="col-md-2 gutter-5">
                <asp:TextBox ID="txtTPSussex2" CssClass="form-control input-xs text-center" MaxLength="2" runat="server"></asp:TextBox>
            </div>
            <div class="col-md-2 gutter-5">
                <asp:TextBox ID="txtTPSussex3" CssClass="form-control input-xs text-center" MaxLength="2" runat="server"></asp:TextBox>
            </div>
            <div class="col-md-2 gutter-5">
                <asp:TextBox ID="txtTPSussex4" CssClass="form-control input-xs text-center" MaxLength="2" runat="server"></asp:TextBox>
            </div>
            <div class="col-md-2 gutter-5">
                <asp:TextBox ID="txtTPSussex5" CssClass="form-control input-xs text-center" MaxLength="4" runat="server"></asp:TextBox>
            </div>
            <div class="col-md-2 gutter-5">
                <asp:TextBox ID="txtTPSussex6" CssClass="form-control input-xs text-center" MaxLength="2" runat="server"></asp:TextBox>
            </div>
        </div>
    </div>
</div>
