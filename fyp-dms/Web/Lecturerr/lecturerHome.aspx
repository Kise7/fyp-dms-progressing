<%@ Page Title="" Language="C#" MasterPageFile="~/Web/Lecturerr/lecturerLayout.Master" AutoEventWireup="true" CodeBehind="lecturerHome.aspx.cs" Inherits="fyp_dms.Web.Lecturerr.lecturerHome" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>HoldDoc - Staff Dashboard</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main>
        <div class="container-fluid">
            <h1 class="mt-4">Dashboard</h1>
            <ol class="breadcrumb mb-4">
                <li class="breadcrumb-item active">Dashboard</li>
            </ol>

            <div class="row">
                <div class="col-xl-3 col-md-6">
                    <div class="card mb-4" style="font-weight: bold;">
                        <div class="card-body">Notification</div>
                        <div class="card-footer d-flex align-items-center justify-content-between">
                            <asp:HyperLink ID="HyperLink1" runat="server" class="small stretched-link" style="color:black;" NavigateUrl="~/Web/Lecturerr/lecturerNotification.aspx">View Details</asp:HyperLink>
                            <div class="small"><i class="fas fa-angle-right"></i></div>
                        </div>
                    </div>
                </div>
                <div class="col-xl-3 col-md-6">
                    <div class="card mb-4" style="font-weight: bold;">
                        <div class="card-body">Bookmark</div>
                        <div class="card-footer d-flex align-items-center justify-content-between">
                            <asp:HyperLink ID="HyperLink2" runat="server" class="small stretched-link" style="color:black;" NavigateUrl="~/Web/Lecturerr/lecturerBookmark.aspx">View Details</asp:HyperLink>
                            <div class="small text-white"><i class="fas fa-angle-right"></i></div>
                        </div>
                    </div>
                </div>
                <div class="col-xl-3 col-md-6">
                    <div class="card mb-4"  style="font-weight: bold;">
                        <div class="card-body">Document</div>
                        <div class="card-footer d-flex align-items-center justify-content-between">
                            <asp:HyperLink ID="HyperLink3" runat="server" class="small stretched-link" style="color:black;" NavigateUrl="~/Web/Lecturerr/lecturerDocumentMain.aspx">View Details</asp:HyperLink>
                            <div class="small"><i class="fas fa-angle-right"></i></div>
                        </div>
                    </div>
                </div>

                <div class="col-xl-3 col-md-6">
                    <div class="card mb-4" style="font-weight: bold;">
                        <div class="card-body">Claim Main Page</div>
                        <div class="card-footer d-flex align-items-center justify-content-between">
                            <asp:HyperLink ID="HyperLink6" runat="server" class="small stretched-link" style="color:black;" NavigateUrl="~/Web/Lecturerr/workflowMain.aspx">View Details</asp:HyperLink>
                            <div class="small"><i class="fas fa-angle-right"></i></div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </main>
</asp:Content>
