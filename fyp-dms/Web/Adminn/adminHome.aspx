<%@ Page Title="" Language="C#" MasterPageFile="~/Web/Adminn/adminLayout.Master" AutoEventWireup="true" CodeBehind="adminHome.aspx.cs" Inherits="fyp_dms.Web.Adminn.adminHome" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>HoldDoc - Admin Dashboard</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main>
        <div class="container-fluid">
            <h1 class="mt-4">Dashboard</h1>
            <ol class="breadcrumb mb-4">
                <li class="breadcrumb-item active">Dashboard</li>
            </ol>

            <h3 class="mt-4">Staff</h3>
            <div class="row">
                <div class="col-xl-3 col-md-6">
                    <div class="card mb-4" style="font-weight: bold;">
                        <div class="card-body">Admin</div>
                        <div class="card-footer d-flex align-items-center justify-content-between">
                            <asp:HyperLink ID="HyperLink1" runat="server" class="small stretched-link" Style="color: black;" NavigateUrl="~/Web/Adminn/manageAdmin.aspx">View Details</asp:HyperLink>
                            <div class="small"><i class="fas fa-angle-right"></i></div>
                        </div>
                    </div>
                </div>

                <div class="col-xl-3 col-md-6">
                    <div class="card mb-4" style="font-weight: bold;">
                        <div class="card-body">Lecturer</div>
                        <div class="card-footer d-flex align-items-center justify-content-between">
                            <asp:HyperLink ID="HyperLink2" runat="server" class="small stretched-link" Style="color: black;" NavigateUrl="~/Web/Adminn/manageLecturer.aspx">View Details</asp:HyperLink>
                            <div class="small text-white"><i class="fas fa-angle-right"></i></div>
                        </div>
                    </div>
                </div>

                <div class="col-xl-3 col-md-6">
                    <div class="card mb-4" style="font-weight: bold;">
                        <div class="card-body">Work Assign</div>
                        <div class="card-footer d-flex align-items-center justify-content-between">
                            <asp:HyperLink ID="HyperLink3" runat="server" class="small stretched-link" Style="color: black;" NavigateUrl="~/Web/Adminn/manageWorkAssign.aspx">View Details</asp:HyperLink>
                            <div class="small text-white"><i class="fas fa-angle-right"></i></div>
                        </div>
                    </div>
                </div>

                <div class="col-xl-3 col-md-6">
                    <div class="card mb-4" style="font-weight: bold;">
                        <div class="card-body">Workflow</div>
                        <div class="card-footer d-flex align-items-center justify-content-between">
                            <asp:HyperLink ID="HyperLink4" runat="server" class="small stretched-link" Style="color: black;" NavigateUrl="~/Web/Adminn/manageWorkflow.aspx">View Details</asp:HyperLink>
                            <div class="small text-white"><i class="fas fa-angle-right"></i></div>
                        </div>
                    </div>
                </div>
            </div>
            <br />
            <h3 class="mt-4">Student</h3><br />

            <div class="row">
                <div class="col-xl-3 col-md-6">
                    <div class="card mb-4" style="font-weight: bold;">
                        <div class="card-body">Student</div>
                        <div class="card-footer d-flex align-items-center justify-content-between">
                            <asp:HyperLink ID="HyperLink5" runat="server" class="small stretched-link" Style="color: black;" NavigateUrl="~/Web/Adminn/manageStudent.aspx">View Details</asp:HyperLink>
                            <div class="small text-white"><i class="fas fa-angle-right"></i></div>
                        </div>
                    </div>
                </div>

                <div class="col-xl-3 col-md-6">
                    <div class="card mb-4" style="font-weight: bold;">
                        <div class="card-body">Year</div>
                        <div class="card-footer d-flex align-items-center justify-content-between">
                            <asp:HyperLink ID="HyperLink6" runat="server" class="small stretched-link" Style="color: black;" NavigateUrl="~/Web/Adminn/cpYear.aspx">View Details</asp:HyperLink>
                            <div class="small text-white"><i class="fas fa-angle-right"></i></div>
                        </div>
                    </div>
                </div>

                <div class="col-xl-3 col-md-6">
                    <div class="card mb-4" style="font-weight: bold;">
                        <div class="card-body">Intake</div>
                        <div class="card-footer d-flex align-items-center justify-content-between">
                            <asp:HyperLink ID="HyperLink7" runat="server" class="small stretched-link" Style="color: black;" NavigateUrl="~/Web/Adminn/cpIntake.aspx">View Details</asp:HyperLink>
                            <div class="small text-white"><i class="fas fa-angle-right"></i></div>
                        </div>
                    </div>
                </div>

                <div class="col-xl-3 col-md-6">
                    <div class="card mb-4" style="font-weight: bold;">
                        <div class="card-body">Faculty</div>
                        <div class="card-footer d-flex align-items-center justify-content-between">
                            <asp:HyperLink ID="HyperLink8" runat="server" class="small stretched-link" Style="color: black;" NavigateUrl="~/Web/Adminn/cpFaculty.aspx">View Details</asp:HyperLink>
                            <div class="small text-white"><i class="fas fa-angle-right"></i></div>
                        </div>
                    </div>
                </div>

                <div class="col-xl-3 col-md-6">
                    <div class="card mb-4" style="font-weight: bold;">
                        <div class="card-body">Programme</div>
                        <div class="card-footer d-flex align-items-center justify-content-between">
                            <asp:HyperLink ID="HyperLink9" runat="server" class="small stretched-link" Style="color: black;" NavigateUrl="~/Web/Adminn/cpProgramme.aspx">View Details</asp:HyperLink>
                            <div class="small text-white"><i class="fas fa-angle-right"></i></div>
                        </div>
                    </div>
                </div>

                <div class="col-xl-3 col-md-6">
                    <div class="card mb-4" style="font-weight: bold;">
                        <div class="card-body">Course</div>
                        <div class="card-footer d-flex align-items-center justify-content-between">
                            <asp:HyperLink ID="HyperLink10" runat="server" class="small stretched-link" Style="color: black;" NavigateUrl="~/Web/Adminn/cpCourse.aspx">View Details</asp:HyperLink>
                            <div class="small text-white"><i class="fas fa-angle-right"></i></div>
                        </div>
                    </div>
                </div>

                <div class="col-xl-3 col-md-6">
                    <div class="card mb-4" style="font-weight: bold;">
                        <div class="card-body">Course Section</div>
                        <div class="card-footer d-flex align-items-center justify-content-between">
                            <asp:HyperLink ID="HyperLink11" runat="server" class="small stretched-link" Style="color: black;" NavigateUrl="~/Web/Adminn/cpCourseSection.aspx">View Details</asp:HyperLink>
                            <div class="small text-white"><i class="fas fa-angle-right"></i></div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </main>
</asp:Content>
