<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/CLFNested.master" CodeBehind="SessionSuccessfull.aspx.vb" Inherits="NoticeOfIntent.SessionSuccessfull" %>
<asp:Content ID="Content1" ContentPlaceHolderID="NestedContentPlaceHolderHeader" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NestedContentPlaceHolderPageTitle" runat="server">

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="NestedContentPlaceHolderBody" runat="server">
<table width="700px">
            <tr>
                <td class="PageTable">
                    State of Delaware<br />
                    Department of Natural Resources & Environmental Control
                </td>
                <td rowspan="2">
                    <img alt="" src="../Content/images/vectordnreclogo.jpg" width="150px" height="100px" 
                        style="" align="right" />
                </td>
            </tr>
            <tr>
                <td class="detail" align="left" style="height: 36px">
                    <asp:Label ID="lblApplicationDetails" runat="server" />
                </td>
               
            </tr>
        </table>
        <table width="700px" class="ControlTable">
            <tr>
                <td colspan="2" class="greyBarNew" style="height: 10px;" align="center">
                    <label style="font-family: Arial;font-size: 12pt;font-weight: bold;width: 456px;">Receipt</label>
                </td>
            </tr>
            <tr>
                <td style="height: 18px">
                    <label>Date:</label>
                </td>
                <td style="height: 18px">
                    <asp:Label ID="lblDate" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    <label>
                        Description:</label>
                </td>
                <td>
                    <asp:Label ID="lblDescription" runat="server" />
                </td>
            </tr>
            <tr>
                <td >
                    <label>Card Type:</label>
                </td>
                <td>
                    <asp:label ID="lblCardType" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    <label>Card Number:</label>
                </td>
                <td>
                    <asp:label ID="lblCardNumber" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    <label>Amount:</label>
                </td>
                <td>
                    <asp:label ID="lblAmount" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    <label>Receipt No.:</label>
                </td>
                <td>
                    <asp:label ID="lblRemittanceID" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    <label>Payee Name:</label>
                </td>
                <td>
                    <asp:label ID="lblPayeeName" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    <label>Reference No.:</label>
                </td>
                <td>
                    <asp:label ID="lblSubmissionID" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    <label>Application Type:</label>
                </td>
                <td>
                    <asp:label ID="lblAppType" runat="server" />
                </td>
            </tr>            
            <tr>
                <td colspan="2" class="greyBarNew" style="height:10px;">
                   
                </td>
            </tr>
            <tr>
               <td colspan="2">
                   Please <a onclick="print();" type="text/html">print</a> a copy of this receipt for your records.
                </td>
            </tr>
            <tr>
                <td style="height:100px;text-align:center;" colspan="2">
                    <asp:Button ID="btnReturn" runat="server" Text="Continue the Submission" />
                </td>        
            </tr>
        </table>
    <asp:HiddenField ID="hfpaymentsuccessfull" runat="server" Value="0" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="NestedContentPlaceHolderJS" runat="server">
</asp:Content>
