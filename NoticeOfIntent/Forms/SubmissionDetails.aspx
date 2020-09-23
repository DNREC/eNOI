<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/CLFNested.master" CodeBehind="SubmissionDetails.aspx.vb" Inherits="NoticeOfIntent.SubmissionDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="NestedContentPlaceHolderHeader" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NestedContentPlaceHolderPageTitle" runat="server">
    <div class="well">
        <h1 class="text-center font-bold">Submission Summary</h1>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="NestedContentPlaceHolderBody" runat="server">
    <div class="panel panel-default">
        <div class="panel-heading">
            <h1 class="panel-title font-bold">Submission Information</h1>
        </div>
        <div id="SubmissionInfo">
            <div class="panel-body input-group-sm">
                <div class="row input-group-sm">
                    <asp:Label ID="lblOwnerName" AssociatedControlID="txtOwnerName" runat="server" Text="Owner Name" CssClass="control-label col-md-3"></asp:Label>
                    <div class="col-md-9">
                        <asp:TextBox ID="txtOwnerName" runat="server" CssClass="form-control font-bold" ReadOnly="true" ></asp:TextBox>
                    </div>
                </div>
                <div class="row input-group-sm">
                    <asp:Label ID="lblOwnerAddress" AssociatedControlID="txtOwnerAddress" runat="server" Text="Owner Address" CssClass="control-label col-md-3"></asp:Label>
                    <div class="col-md-9">
                        <asp:TextBox ID="txtOwnerAddress" runat="server" CssClass="form-control font-bold" ReadOnly="true" ></asp:TextBox>
                    </div>
                </div>
                <div class="row input-group-sm">
                    <asp:Label ID="Label1" AssociatedControlID="txtPermitNumber" runat="server" Text="Permit Number" CssClass="control-label col-md-3"></asp:Label>
                    <div class="col-md-9">
                        <asp:TextBox ID="txtPermitNumber" runat="server" CssClass="form-control font-bold" ReadOnly="true" ></asp:TextBox>
                    </div>
                </div>
                <div class="row input-group-sm">
                    <asp:Label ID="lblProjectName" AssociatedControlID="txtProjectName" runat="server" Text="Project Name" CssClass="control-label col-md-3"></asp:Label>
                    <div class="col-md-9">
                        <asp:TextBox ID="txtProjectName" runat="server" CssClass="form-control font-bold" ReadOnly="true" ></asp:TextBox>
                    </div>
                </div>
                <div class="row input-group-sm">
                    <asp:Label ID="lblProjectAddress" AssociatedControlID="txtProjectAddress" runat="server" Text="Project Address" CssClass="control-label col-md-3"></asp:Label>
                    <div class="col-md-9">
                        <asp:TextBox ID="txtProjectAddress" runat="server" CssClass="form-control font-bold" ReadOnly="true" ></asp:TextBox>
                    </div>
                </div>
                <div class="row input-group-sm">
                    <asp:Label ID="lblSubmissionType" AssociatedControlID="txtSubmissionType" runat="server" Text="Submission Type" CssClass="control-label col-md-3"></asp:Label>
                    <div class="col-md-9">
                        <asp:TextBox ID="txtSubmissionType" runat="server" CssClass="form-control font-bold" ReadOnly="true" ></asp:TextBox>
                    </div>
                </div>
                <div class="row input-group-sm">
                    <asp:Label ID="Label2" AssociatedControlID="txtPreparedBy" runat="server" Text="Prepared By" CssClass="control-label col-md-3"></asp:Label>
                    <div class="col-md-9">
                        <asp:TextBox ID="txtPreparedBy" runat="server" CssClass="form-control font-bold" ReadOnly="true" ></asp:TextBox>
                    </div>
                </div>
                <div class="row input-group-sm">
                    <asp:Label ID="Label3" AssociatedControlID="txtPreparedCompany" runat="server" Text="Prepared Company" CssClass="control-label col-md-3"></asp:Label>
                    <div class="col-md-9">
                        <asp:TextBox ID="txtPreparedCompany" runat="server" CssClass="form-control font-bold" ReadOnly="true" ></asp:TextBox>
                    </div>
                </div>
                <div class="row input-group-sm" id="RefNo">
                    <asp:Label ID="lblSubmissionID" AssociatedControlID="txtSubmissionID" runat="server" Text="Ref. No." CssClass="control-label col-md-3"></asp:Label>
                    <div class="col-md-9">
                        <asp:TextBox ID="txtSubmissionID" runat="server" CssClass="form-control font-bold" ReadOnly="true" ></asp:TextBox>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="panel panel-default">
        <div class="panel-heading">
            <h1 class="panel-title font-bold">Submission Status</h1>
        </div>
        <div id="SubmissionStatus">
            <div class="panel-body input-group-sm">
                <asp:Repeater ID="rptSubmissionStatus" runat="server" SelectMethod="rptSubmissionStatus_GetData" EnableViewState="false" ViewStateMode="Disabled">
                    <HeaderTemplate>
                        <table class="table table-bordered table-hover table-condensed">
                            <thead>
                                <tr>
                                    <th style="background-color:#006DCC;color:white;">Status Date</th>
                                    <th style="background-color:#006DCC;color:white;">Status</th>
                                    <th style="background-color:#006DCC;color:white;">Comment</th>
                                </tr>
                            </thead>
                            <tbody>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td><%# Eval("SubmissionStatusDate", "{0:MM/dd/yyyy}")%></td>
                            <td><%# Eval("SubmissionStatus")%></td>
                            <td><%# Eval("SubmissionStatusComment")%></td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                            </tbody>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
            </div>
        </div>
    </div>
    <div class="panel panel-default">
        <div class="panel-heading">
            <h1 class="panel-title font-bold">Payment Information</h1>
        </div>
        <div id="SubmissionPayment">
            <div class="panel-body input-group-sm">
                <div class="input-group-sm">
                    <label for="txtApplicationFee" class="control-label col-md-2">Application Fee</label>
                    <div class="col-md-10">
                        <asp:TextBox ID="txtApplicationFee" runat="server" CssClass="form-control font-bold" ReadOnly="true" ></asp:TextBox>
                    </div>
                    
                </div>
                <div class="input-group-sm">
                    <label for="txtExemptionCode" class="control-label col-md-2">Discount Code</label>
                    <div class="col-md-10">
                        <asp:TextBox ID="txtExemptionCode" runat="server" CssClass="form-control font-bold"></asp:TextBox>
                        <asp:CustomValidator ID="cvtxtExemptionCode" runat="server" Display="Dynamic" ValidateEmptyText="false" ControlToValidate="txtExemptionCode" ErrorMessage="Please enter a valid discount code." ToolTip="Please enter a valid discount code." CssClass="alert-text" ValidationGroup="grpFiled"></asp:CustomValidator>
                    </div>
                </div>
                <div class="input-group-sm">
                    <label for="txtAmountPaid" class="control-label col-md-2">Amount Paid</label>
                    <div class="col-md-10">
                        <asp:TextBox ID="txtAmountPaid" runat="server" CssClass="form-control font-bold" ReadOnly="true" ClientIDMode="Static"></asp:TextBox>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="SubmissionAction" class="panel">
        <div class="pull-right" style="float:none;">
            <asp:Button ID="btnUploadDoc" runat="server" Text="Upload Documents" CssClass="btn btn-default"  CausesValidation="false"/>
            <asp:Button ID="btnCopyOfRecord" runat="server" CssClass="btn btn-default" Text="Copy of Record" Visible="false" CausesValidation="false" />
            <asp:Button ID="btnDelete" runat="server" CssClass="btn btn-default" Text="Delete" Visible="false" />
            <asp:Button ID="btnView" runat="server" CssClass="btn btn-default" Text="View" Visible="false" />
            <asp:Button ID="btnPreview" runat="server" CssClass="btn btn-default" Text="Preview" Visible="true" ValidationGroup="grpFiled" />
            <input id="btnNewPayment" runat="server" type="button" value="New Payment"  Class="btn btn-default" Visible="false" data-toggle="modal" data-target="#pnlNewPayment" data-backdrop="static" />
            <asp:Button ID="btnFile" runat="server" CssClass="btn btn-default" Text="Submit" Visible="false" ValidationGroup="grpFiled" />
            <input id="btnAccept" runat="server" type="button" value="Accept"  Class="btn btn-default" Visible="false" data-toggle="modal" data-target="#pnlAcceptComments" data-backdrop="static" />
            <input id="btnReject" runat="server" type="button" value="Reject"  Class="btn btn-default" Visible="false" data-toggle="modal" data-target="#pnlRejectComments" data-backdrop="static" />
            <input id="btnReturn" runat="server" type="button" value="Return"  Class="btn btn-default" Visible="false" data-toggle="modal" data-target="#pnlReturnComments" data-backdrop="static" />
            <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-default" Text="Cancel" CausesValidation="false"/>
            <asp:Button ID="btnApprovedEmail" runat="server" Text="Send Approved Email" CssClass="btn btn-default" visible="false"/>
        </div>
    </div>
    <br />
    <div class="modal fade" id="pnlNewPayment" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header bg-primary">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" id="myModalLabel">NEW PAYMENT</h4>
                </div>
                <div class="modal-body">
                    <div class="form-group input-group-sm">
                        <label for="txtNewPayment" class="control-label col-md-4">New Amount Paid:</label>
                        <div class="col-md-8">
                            <asp:TextBox ID="txtNewPayment" runat="server" MaxLength="10" ClientIDMode="Static"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="btnAcceptNewPayment" runat="server" CssClass="btn btn-default" Text="Accept" />
                    <input id="btnCancelNewPayment" type="button" Class="btn btn-default" value="Cancel" data-dismiss="modal" />
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade" id="pnlAcceptComments" tabindex="-1" role="dialog" aria-labelledby="myModalStatusAccept">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header bg-primary">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" id="myModalStatusAccept">Accepted Status Comments</h4>
                </div>
                <div class="modal-body clearfix">
                    <div class="form-group input-group-sm">
                        <label for="lblStatusComment" class="control-label col-md-3">Comments:</label>
                        <div class="col-md-10">
                            <asp:TextBox ID="txtAcceptComments" runat="server" TextMode="MultiLine" Rows="10" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="btnSubmitAccept" runat="server" CssClass="btn btn-default" Text="Submit" />
                    <input id="btnCancelAcceptComment" type="button" Class="btn btn-default" value="Cancel" data-dismiss="modal" />
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade" id="pnlReturnComments" tabindex="-1" role="dialog" aria-labelledby="myModalStatusReturn">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header bg-primary">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" id="myModalStatusReturn">Returned For ReSubmission Status Comments</h4>
                </div>
                <div class="modal-body clearfix">
                    <div class="form-group input-group-sm">
                        <label for="lblStatusComment1" class="control-label col-md-3">Comments:</label>
                        <div class="col-md-10">
                            <asp:TextBox ID="txtReturnComments" runat="server" TextMode="MultiLine" Rows="10" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="btnSubmitReturn" runat="server" CssClass="btn btn-default" Text="Submit" />
                    <input id="btnCancelReturnComment" type="button" Class="btn btn-default" value="Cancel" data-dismiss="modal" />
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade" id="pnlRejectComments" tabindex="-1" role="dialog" aria-labelledby="myModalStatusReject">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header bg-primary">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" id="myModalStatusReject">Rejected Status Comments</h4>
                </div>
                <div class="modal-body clearfix">
                    <div class="form-group input-group-sm">
                        <label for="lblStatusComment1" class="control-label col-md-3">Comments:</label>
                        <div class="col-md-10">
                            <asp:TextBox ID="txtRejectComments" runat="server" TextMode="MultiLine" Rows="10" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="btnSubmitReject" runat="server" CssClass="btn btn-default" Text="Submit" />
                    <input id="btnCancelRejectComment" type="button" Class="btn btn-default" value="Cancel" data-dismiss="modal" />
                </div>
            </div>
        </div>
    </div>



</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="NestedContentPlaceHolderJS" runat="server">
    <script>
        $(document).ready(function () {
            $("[id*='btnNewPayment']").on('click', function () {
                $('#txtAmountPaid').val($('#txtNewPayment').val());
            })
            
        })
    </script>
</asp:Content>