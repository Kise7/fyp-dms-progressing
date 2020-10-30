<%@ Page Title="" Language="C#" MasterPageFile="~/Web/Studentt/studentLayout.Master" AutoEventWireup="true" CodeBehind="studentProfile.aspx.cs" Inherits="fyp_dms.Web.Studentt.studentProfile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>HoldDoc - Student Profile</title>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main>
        <div class="container-fluid">
            <h1 class="mt-4">Student Profile</h1>

            <ol class="breadcrumb mb-4">
                <li class="breadcrumb-item">
                    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Web/Studentt/studentHome.aspx">Dashboard</asp:HyperLink></li>
                <li class="breadcrumb-item active">Student Profile</li>
            </ol>

            <%-- Student Profile here --%>
            <div class="card mb-4">
                <div class="card-header">
                    <i class="fas fa-table mr-1"></i>
                    Student Profile
                </div>

                <div class="card-body">
                    <div class="table-responsive">
                            <asp:DataGrid ID="dgStudentProfile" CssClass="Grid table" runat="server" BorderColor="Transparent" AutoGenerateColumns="false" ShowFooter="true"
                            OnItemDataBound="dgStudentProfile_ItemDataBound" OnItemCommand="dgStudentProfile_ItemCommand" OnEditCommand="dgEdit_StudentProfile"
                            OnCancelCommand="dgCancel_StudentProfile" OnUpdateCommand="dgUpdate_StudentProfile" PageSize="10" AllowPaging="True" OnPageIndexChanged="dgStudentProfile_PageIndexChanged" OnSelectedIndexChanged="dgStudentProfile_SelectedIndexChanged">
                            <AlternatingItemStyle CssClass=""></AlternatingItemStyle>
                            <ItemStyle CssClass="GridItem"></ItemStyle>
                            <HeaderStyle CssClass="GridItem"></HeaderStyle>

                            <Columns>
                                <asp:TemplateColumn HeaderText="Profile">
                                <ItemTemplate>
                                StudentID:<b><asp:Label ID="lblID" Runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "studentID") %>'></asp:Label></b><br/>
                                Name:<b><asp:Label ID="lblName" Runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "name") %>'></asp:Label></b><br/>
                                Email:<b><asp:Label ID="lblEmail" Runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "email") %>'></asp:Label></b><br/>
                                PhoneNo:<b><asp:Label ID="lblPhone" Runat="server" MaxLength="11" Text='<%# DataBinder.Eval(Container.DataItem, "phoneno") %>'></asp:Label></b><br/>
                                Programme:<b><asp:Label ID="lblProgrammeName" Runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ProgrammeName") %>'></asp:Label></b><br/>
                                Year:<b><asp:Label ID="lblYear" Runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "yearValue") %>'></asp:Label></b><br/>
                                Batch:<b><asp:Label ID="lblBatch" Runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "intakeMonth") %>'></asp:Label></b><br/
                                Password:<b>*********</b>
                                </ItemTemplate>

                                <EditItemTemplate>
                                StudentID:<b><asp:Label ID="txtID" Runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "studentID") %>'></asp:Label></b><br/>
                                Name:<b><asp:Label ID="txtName" Runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "name") %>'></asp:Label></b><br/>
                                Email:<b><asp:Label ID="txtEmail" Runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "email") %>'></asp:Label></b><br/>
                                PhoneNo:<asp:TextBox ID="txtPhone" Runat="server" MaxLength="11" Text='<%# DataBinder.Eval(Container.DataItem, "phoneno") %>'></asp:TextBox><br/>
                                Programme:<b><asp:Label ID="txtProgrammeName" Runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ProgrammeName") %>'></asp:Label></b><br/>
                                Year:<b><asp:Label ID="txtYear" Runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "yearValue") %>'></asp:Label></b><br/>
                                Batch:<b><asp:Label ID="txtBatch" Runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "intakeMonth") %>'></asp:Label></b><br/>
                                Old Password:<asp:TextBox TextMode="Password" ID="txtOldPassword" runat="server" Text=""></asp:TextBox><br />
                                New Password:<asp:TextBox TextMode="Password" ID="txtNewPassword" runat="server" Text=""></asp:TextBox><br />
                                Confirm Password:<asp:TextBox TextMode="Password" ID="txtConfirmPassword" runat="server" Text=""></asp:TextBox><br />
                                </EditItemTemplate>
                                <ItemStyle Width="20%" />
                                <HeaderStyle Font-Bold="True" />
                                </asp:TemplateColumn>

                                <asp:EditCommandColumn ItemStyle-Width="10%" HeaderText="Update" ButtonType="PushButton" UpdateText="Update" CancelText="Cancel" EditText="Update Profile" HeaderStyle-Font-Bold="True"></asp:EditCommandColumn>
                            </Columns>
                            <PagerStyle Visible="False" Mode="NumericPages" CssClass="pages" />
                            </asp:DataGrid>
                    </div>
                </div>
            </div>
        </div>
    </main>
</asp:Content>
