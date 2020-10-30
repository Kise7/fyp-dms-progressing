<%@ Page Title="" Language="C#" MasterPageFile="~/Web/Studentt/studentLayout.Master" AutoEventWireup="true" CodeBehind="studentFolder.aspx.cs" Inherits="fyp_dms.Web.Studentt.studentFolder" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>HoldDoc - Student Folder</title>
</asp:Content>


<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main>
        <div class="container-fluid">
            <h1 class="mt-4">Student Folder</h1>

            <ol class="breadcrumb mb-4">
                <li class="breadcrumb-item"><asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Web/Studentt/studentHome.aspx">Dashboard</asp:HyperLink></li>
                <li class="breadcrumb-item"><asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/Web/Studentt/studentDocumentMain.aspx">Manage Document</asp:HyperLink></li>
                <li class="breadcrumb-item active">Student Folder</li>
            </ol>

            <%-- Student Folder here --%>
            <div class="card mb-4">
                <div class="card-header">
                    <i class="fas fa-table mr-1"></i>
                    Student Folder
                </div>

                <div class="card-body">
                    <div class="table-responsive">
                       <asp:datagrid id="dgStudentFolder" CssClass="table table-bordered" runat="server" AutoGenerateColumns="false" Width="100%" ShowFooter="true"
		               OnItemDataBound="dgStudentFolder_ItemDataBound" OnItemCommand="dgStudentFolder_ItemCommand" OnEditCommand="dgEdit_StudentFolder" 
		               OnCancelCommand="dgCancel_StudentFolder" OnUpdateCommand="dgUpdate_StudentFolder" PageSize="10" AllowPaging="True" OnPageIndexChanged="dgStudentFolder_PageIndexChanged" OnSelectedIndexChanged="dgStudentFolder_SelectedIndexChanged">
			       	  <AlternatingItemStyle CssClass=""></AlternatingItemStyle>
                      <ItemStyle CssClass="GridItem"></ItemStyle>
			          <HeaderStyle CssClass="GridItem"></HeaderStyle>
                      <FooterStyle CssClass="GridItem bottom"></FooterStyle>

				       <Columns>
					      <asp:TemplateColumn HeaderText="No">
                          <ItemTemplate>
                              <asp:Label ID="No" Runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "No") %>'></asp:Label>
                              <asp:Label ID="lblID" Visible="False" Runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ID") %>'></asp:Label>
                          </ItemTemplate> 
                              <ItemStyle Width="10%" />
                              <HeaderStyle Font-Bold="True" />
                          </asp:TemplateColumn>  	           
                    
                          <asp:TemplateColumn HeaderText="FolderName">
					     <ItemTemplate>
                             <asp:Label ID="lblFolderName" Runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "FolderName") %>'></asp:Label>
                        </ItemTemplate>
					         <ItemStyle Width="20%" />
                             <HeaderStyle Font-Bold="True" />
					    </asp:TemplateColumn>

                        <asp:TemplateColumn HeaderText="Created By">
					    <ItemTemplate>
                             <asp:Label ID="lblLecturerName" Runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "name") %>'></asp:Label>
                        </ItemTemplate>
					         <ItemStyle Width="20%" />
                             <HeaderStyle Font-Bold="True" />
				     	</asp:TemplateColumn>

                        <asp:TemplateColumn HeaderText="Privelege">
					    <ItemTemplate>
                             <asp:Label ID="lblPrivilege" visible="False" runat="server" Text='<%# (DataBinder.Eval(Container.DataItem, "Privilege")) %>'></asp:Label>
                             <asp:Label ID="lblshow" runat="server" visible="true" Text=""></asp:Label>
                        </ItemTemplate>
					    <ItemStyle Width="20%" />
                             <HeaderStyle Font-Bold="True" />
				     	</asp:TemplateColumn>

                        <asp:TemplateColumn HeaderText="Open">
					    <ItemTemplate>
                             <asp:LinkButton Cssclass="btn btn-primary" ID="btnManage" CommandName="Manage" Runat="server" Text="<i class='fa fa-folder'></i>Access"></asp:LinkButton>
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
