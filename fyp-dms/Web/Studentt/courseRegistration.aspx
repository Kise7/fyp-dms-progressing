<%@ Page Title="" Language="C#" MasterPageFile="~/Web/Studentt/studentLayout.Master" AutoEventWireup="true" CodeBehind="courseRegistration.aspx.cs" Inherits="fyp_dms.Web.Studentt.courseRegistration" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>HoldDoc - Course Registration</title>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main>
        <div class="container-fluid">
            <h1 class="mt-4">Course Registration</h1>

            <ol class="breadcrumb mb-4">
                <li class="breadcrumb-item"><asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Web/Studentt/studentHome.aspx">Dashboard</asp:HyperLink></li>
                <li class="breadcrumb-item active">Course Registration</li>
            </ol>

            <%-- Course Registration here --%>
            <div class="card mb-4">
                <div class="card-header">
                    <i class="fas fa-table mr-1"></i>
                    Course Registration
                </div>

                <div class="card-body">
                    <div class="table-responsive">
                       <asp:datagrid id="dgStudentRegisterCourse" CssClass="table table-bordered" runat="server" AutoGenerateColumns="false" Width="100%" ShowFooter="true"
		               OnItemDataBound="dgStudentRegisterCourse_ItemDataBound" OnItemCommand="dgStudentRegisterCourse_ItemCommand" OnEditCommand="dgEdit_StudentRegisterCourse" 
		               OnCancelCommand="dgCancel_StudentRegisterCourse" OnUpdateCommand="dgUpdate_StudentRegisterCourse" PageSize="10" AllowPaging="True" OnPageIndexChanged="dgStudentRegisterCourse_PageIndexChanged" OnSelectedIndexChanged="dgStudentRegisterCourse_SelectedIndexChanged">
			 	       <AlternatingItemStyle CssClass="GridItem"></AlternatingItemStyle>
                       <ItemStyle CssClass="GridItem"></ItemStyle>
				       <HeaderStyle CssClass="GridItem"></HeaderStyle>
                       <FooterStyle CssClass=""></FooterStyle>

				          <Columns>
					        <asp:TemplateColumn HeaderText="No">
                            <ItemTemplate>
                               <asp:Label ID="lblID" Runat="server" visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "courseSectionID") %>'></asp:Label>
                               <asp:Label ID="lblNo" Runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "No") %>'></asp:Label>
                            </ItemTemplate> 
                            <ItemStyle Width="10%" />
                            <HeaderStyle Font-Bold="True" />
                            </asp:TemplateColumn>  	           
                    
                            <asp:TemplateColumn HeaderText="CourseCode">
					        <ItemTemplate>
                              <asp:Label ID="lblCourseCode" Runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CourseCode") %>'></asp:Label>
                            </ItemTemplate>
					        <ItemStyle Width="20%" />
                            <HeaderStyle Font-Bold="True" />
					        </asp:TemplateColumn>

                            <asp:TemplateColumn HeaderText="Course Name">
					        <ItemTemplate>
                            <asp:Label ID="lblName" Runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CourseName") %>'></asp:Label>
                            </ItemTemplate>
                
					        <ItemStyle Width="20%" />
                            <HeaderStyle Font-Bold="True" />
				            </asp:TemplateColumn>

                            <asp:TemplateColumn HeaderText="Section No">
					        <ItemTemplate>
                               <asp:Label ID="lblSectionNo" Runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "SectionNo") %>'></asp:Label>
                            </ItemTemplate>
					        <ItemStyle Width="20%" />
                                <HeaderStyle Font-Bold="True" />
				            </asp:TemplateColumn>

                            <asp:TemplateColumn HeaderText="Register">
					        <ItemTemplate>
                               <asp:Button CSSclass="btn btn-success" ID="btnRegister" CommandName="Register" Runat="server" Text="Register"></asp:Button>
                            </ItemTemplate>
                    
					       <ItemStyle Width="20%" />
                              <HeaderStyle Font-Bold="True" />
				   	       </asp:TemplateColumn>
			        
				         </Columns>
			         <PagerStyle Mode="NumericPages" />
 
			         </asp:datagrid>
                   </div>
                </div>
            </div>
        </div>
    </main>
</asp:Content>

