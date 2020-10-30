<%@ Page Title="" Language="C#" MasterPageFile="~/Web/Studentt/studentLayout.Master" AutoEventWireup="true" CodeBehind="studentDocumentMain.aspx.cs" Inherits="fyp_dms.Web.Studentt.studentDocumentMain" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>HoldDoc - Manage Document</title>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main>
        <div class="container-fluid">
            <h1 class="mt-4">Manage Document</h1>

            <ol class="breadcrumb mb-4">
                <li class="breadcrumb-item"><asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Web/Studentt/studentHome.aspx">Dashboard</asp:HyperLink></li>
                <li class="breadcrumb-item active">Manage Document</li>
            </ol>

            <%-- Manage Document here --%>
            <div class="card mb-4">
                <div class="card-header">
                    <i class="fas fa-table mr-1"></i>
                    Manage Document
                </div>

                <div class="card-body">
                    <div class="table-responsive">
                         <asp:datagrid id="dgStudentCourseRegistered" CssClass="table table-bordered" runat="server" AutoGenerateColumns="false" Width="100%" ShowFooter="true"
		                 OnItemDataBound="dgStudentCourseRegistered_ItemDataBound" OnItemCommand="dgStudentCourseRegistered_ItemCommand" OnEditCommand="dgEdit_StudentCourseRegistered" 
		                 OnCancelCommand="dgCancel_StudentCourseRegistered" OnUpdateCommand="dgUpdate_StudentCourseRegistered" PageSize="10" AllowPaging="True" OnPageIndexChanged="dgStudentCourseRegistered_PageIndexChanged" OnSelectedIndexChanged="dgStudentCourseRegistered_SelectedIndexChanged">
			 	         <AlternatingItemStyle CssClass="GridItem"></AlternatingItemStyle>
                         <ItemStyle CssClass="GridItem"></ItemStyle>
				         <HeaderStyle CssClass="GridItem"></HeaderStyle>
                         <FooterStyle CssClass=""></FooterStyle>

			               <Columns>
				        	 <asp:TemplateColumn HeaderText="No">
                             <ItemTemplate>
                                <asp:Label ID="lblID" Runat="server" visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "studentCourseSectionID") %>'></asp:Label>
                                <asp:Label ID="lblCourseSectionID" Runat="server" visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "CourseSectionID") %>'></asp:Label>
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

                             <asp:TemplateColumn HeaderText="View">
				             <ItemTemplate>
                                 <asp:LinkButton CssClass="btn btn-success" ID="btnView" CommandName="View" Runat="server" Text="<i class='fa fa-eye'></i> View"></asp:LinkButton>
                             </ItemTemplate>
                    
					         <ItemStyle Width="20%" />
                                 <HeaderStyle Font-Bold="True" />
					         </asp:TemplateColumn>

				           </Columns>
			               <PagerStyle Mode="NumericPages" />
 
		            	</asp:datagrid>
                        <asp:Label ID="lblAttention" runat="server" CssClass="Note"></asp:Label>
 
                    </div>
                </div>
            </div>
        </div>
    </main>
</asp:Content>