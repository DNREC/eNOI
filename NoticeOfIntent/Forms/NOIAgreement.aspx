<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/CLFNested.master" CodeBehind="NOIAgreement.aspx.vb" Inherits="NoticeOfIntent.NOIAgreement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="NestedContentPlaceHolderHeader" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NestedContentPlaceHolderPageTitle" runat="server">
    <div class="well">
        <h2>The Submission is successfully saved.</h2>
        <!--<p>Agree and then click on the Submit button to send the application for approval. You will not be able to make any changes to the application after submit.</p>-->
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="NestedContentPlaceHolderBody" runat="server">
    <div>
        <div id="divEmailAddressOfParties" runat="server">
            <h3>Email address(es) of the applicant(s) who needs to sign this application.</h3>
            <div id="divPermittee1" runat="server" class="input-group-sm">
                <div class="row">
                    <div class="col-md-4">
                        <label for="txtOrigPermitteeEmail" class="control-label">Permittee 1 Email address:</label>
                    </div>
                    <div class="col-md-6 col-md-offset-2">
                        <asp:CheckBox ID="chkOrigPermitteeEmail" runat="server" CssClass="checkbox" ClientIDMode="Static" Text="Check this box if you are signing this application." />
                    </div>
                </div>
                <asp:TextBox ID="txtOrigPermitteeEmail" runat="server" CssClass="form-control" ClientIDMode="Static"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rvOrigPermitteeEmail" ControlToValidate="txtOrigPermitteeEmail" runat="server" EnableClientScript="true" Display="Dynamic" ErrorMessage="Valid email address is required." ToolTip="Valid email address is required." CssClass="alert-text"></asp:RequiredFieldValidator>
            </div>
            <div id="divPermittee2" runat="server" class="input-group-sm" visible="false">
                <div class="row">
                    <div class="col-md-4">
                        <label for="txtCoPermitteeEmail" class="control-label">Permittee 2 Email address:</label>
                    </div>
                    <div class="col-md-6 col-md-offset-2">
                        <asp:CheckBox ID="chkCoPermittee" runat="server" CssClass="checkbox" ClientIDMode="Static" Text="Check this box if you are signing this application." />
                    </div>
                </div>
                <asp:TextBox ID="txtCoPermitteeEmail" runat="server" CssClass="form-control" ClientIDMode="Static"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rvCoPermitteeEmail" ControlToValidate="txtCoPermitteeEmail" runat="server" EnableClientScript="true" Display="Dynamic" ErrorMessage="Valid email address is required." ToolTip="Valid email address is required." CssClass="alert-text"></asp:RequiredFieldValidator>
                <asp:CompareValidator ID="cvCoPermitteeEmail" runat="server" ControlToValidate="txtCoPermitteeEmail" ControlToCompare="txtOrigPermitteeEmail" Operator="NotEqual" Type="String" Display="Dynamic" ErrorMessage="Email address should be different from the Permitee 1 email address." ToolTip="Email address should be different from the Permitee 1 email address." CssClass="alert-text" ></asp:CompareValidator>
            </div>
            <asp:HiddenField ID="hfCurrentUseremail" runat="server" Value="" ClientIDMode="Static" />
        </div>
        <br />
        <div class="well">
            <div class="page-header">
                <h2>
                    <asp:Label ID="lblCertHeading" runat="server" Text="Permittee Certification"></asp:Label></h2>
            </div>
            <rsweb:ReportViewer ID="ReportViewer1" runat="server" ShowBackButton="False"  ShowCredentialPrompts="False" ShowExportControls="False" ShowFindControls="False" ShowPageNavigationControls="False" ShowParameterPrompts="False" ShowPrintButton="False" ShowRefreshButton="False" ShowToolBar="False" ShowZoomControl="False" Width="100%" Height="300px">
                <LocalReport ReportPath="Report/AgreementCert.rdlc">
                </LocalReport>
            </rsweb:ReportViewer>
            <div class="form-group clearfix">
                <asp:CheckBox ID="chkAgree" runat="server" Text="I Agree" CssClass="checkbox" Checked="false" ClientIDMode="Static" Visible="false"/>
            </div>
            <div>
                <table>
                    <tr>
                        <td>
                            <asp:Button ID="btnSubmit" runat="server" Text="Next" CssClass="btn btn-default btn-lg btn-block" ClientIDMode="Static" />
                        </td>
                        <td>
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-default btn-lg btn-block" CausesValidation="false"/>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="NestedContentPlaceHolderJS" runat="server">
    <script>
        $(document).ready(function () {

            //$('#chkAgree').each(function (e) {
            //    //debugger;

            //    if ($(this).is(':checked'))
            //    {
            //        $('#btnSubmit').prop('disabled', false);
            //    }
            //    else {

            //        $('#btnSubmit').prop('disabled', true);
            //    }

            //    $(this).on('click', function () {
            //        //debugger;
            //        if ($(this).is(':checked')) {
            //            $('#btnSubmit').prop('disabled', false);
            //        }
            //        else {

            //            $('#btnSubmit').prop('disabled', true);
            //        }

            //    })

            //})

        
            $('#chkOrigPermitteeEmail').each(function(e){

                if ($(this).is(':checked'))
                {
                    $('#txtOrigPermitteeEmail').prop('readonly', true);
                }
                else
                {
                    $('#txtOrigPermitteeEmail').prop('readonly', false);
                }

                $(this).on('click', function () {
                //debugger;
                if ($(this).is(':checked'))
                {
                    $('#txtOrigPermitteeEmail').prop('readonly', true);
                    $('#txtOrigPermitteeEmail').val($('#hfCurrentUseremail').val());                   
                }
                else
                {
                    $('#txtOrigPermitteeEmail').prop('readonly', false);
                    $('#txtOrigPermitteeEmail').val('');
                    
                }
            
                })
            
            })

            

            $('#chkCoPermittee').each(function (e) {

                if ($(this).is(':checked')) {
                    $('#txtCoPermitteeEmail').prop('readonly', true);
                }
                else {
                    $('#txtCoPermitteeEmail').prop('readonly', false);
                }


                $(this).on('click', function () {
                    if ($(this).is(':checked')) {
                        $('#txtCoPermitteeEmail').prop('readonly', true);
                        $('#txtCoPermitteeEmail').val($('#hfCurrentUseremail').val());
                    }
                    else {
                        $('#txtCoPermitteeEmail').prop('readonly', false);
                        $('#txtCoPermitteeEmail').val('');
                    }

                })
            })


        })
    </script>
</asp:Content>
