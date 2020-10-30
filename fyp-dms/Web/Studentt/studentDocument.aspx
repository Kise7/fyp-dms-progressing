<%@ Page Title="" Language="C#" MasterPageFile="~/Web/Studentt/studentLayout.Master" AutoEventWireup="true" CodeBehind="studentDocument.aspx.cs" Inherits="fyp_dms.Web.Studentt.studentDocument" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>HoldDoc - Student Document</title>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main>
        <div class="container-fluid">
            <h1 class="mt-4">Student Document</h1>

            <ol class="breadcrumb mb-4">
                <li class="breadcrumb-item"><asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Web/Studentt/studentHome.aspx">Dashboard</asp:HyperLink></li>
                <li class="breadcrumb-item"><asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/Web/Studentt/studentDocumentMain.aspx">Manage Document</asp:HyperLink></li>
                <li class="breadcrumb-item"><asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="~/Web/Studentt/studentFolder.aspx">Student Folder</asp:HyperLink></li>
                <li class="breadcrumb-item active">Student Document</li>
            </ol>

            <%-- Student Document here --%>
            <div class="card mb-4">
                <div class="card-header">
                    <i class="fas fa-table mr-1"></i>
                    Student Document
                </div>

                 <div>
                      <asp:TextBox ID="txtSearch" MaxLength="60" runat="server" CssClass="txtsearch"></asp:TextBox>
                      <asp:LinkButton ID="LinkButtonSearch" runat="server" Text='<i class="fas fa-search"></i>' onclick="BtnSearch_Click"></asp:LinkButton>
                      <asp:Label ID="lblSearchResult" Text="" runat="server"></asp:Label>
                 </div>

                 <div class="card-body">
                    <div class="table-responsive">
                          <asp:datagrid id="dgStudentDocument" CssClass="Grid table" runat="server" BackColor="White" BorderStyle="Solid" 
		                  BorderColor="Transparent" AutoGenerateColumns="false" Width="100%" ShowFooter="true"
		                  OnItemDataBound="dgStudentDocument_ItemDataBound" OnItemCommand="dgStudentDocument_ItemCommand" OnEditCommand="dgEdit_StudentDocument" 
		                  OnCancelCommand="dgCancel_StudentDocument" OnUpdateCommand="dgUpdate_StudentDocument" PageSize="10" AllowPaging="True" OnPageIndexChanged="dgStudentDocument_PageIndexChanged" OnSelectedIndexChanged="dgStudentDocument_SelectedIndexChanged">
			 	          <AlternatingItemStyle CssClass="GridItem"></AlternatingItemStyle>
                         <ItemStyle CssClass="GridItem"></ItemStyle>
				         <HeaderStyle CssClass="GridItem"></HeaderStyle>
                         <FooterStyle CssClass=""></FooterStyle>

				            <Columns>
			                  <asp:TemplateColumn HeaderText="Title">
					          <ItemTemplate>
                                 <asp:Label ID="lblID" Visible="False" Runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ID") %>'></asp:Label>
                                 <asp:Label ID="lblTitle" Runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Title") %>'></asp:Label>
                              </ItemTemplate>
					          <ItemStyle Width="20%" />
                                  <HeaderStyle Font-Bold="True" />
				              </asp:TemplateColumn>

                              <asp:TemplateColumn HeaderText="Document">
					          <ItemTemplate>
                                 <asp:Label ID="lblFile" Visible="false" Runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "File") %>'></asp:Label>
                                 <asp:Label ID="lblDisplayTitle" Visible="true" Runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "DisplayName") %>'></asp:Label>
                             </ItemTemplate>
					         <ItemStyle Width="20%" />
                                 <HeaderStyle Font-Bold="True" />
					         </asp:TemplateColumn>
			        
                             <asp:TemplateColumn HeaderText="View">
					         <ItemTemplate>
                                 <asp:LinkButton CssClass="btn btn-info" ID="btnView" CommandName="View" Runat="server" Text="<i class='fa fa-eye'></i> View"></asp:LinkButton>
                             </ItemTemplate>
                                 <ItemStyle Width="10%" />
                             <HeaderStyle Font-Bold="True" />
					         </asp:TemplateColumn>

                             <asp:TemplateColumn HeaderText="Download">
					         <ItemTemplate>
                                 <asp:LinkButton CssClass="btn btn-success" ID="btnDownload" CommandName="Download" Runat="server" Text="<i class='fa fa-folder'></i> Download"></asp:LinkButton>
                             </ItemTemplate>
                             <ItemStyle Width="10%" />
                             <HeaderStyle Font-Bold="True" />
					         </asp:TemplateColumn>

                             <asp:TemplateColumn HeaderText="Bookmark">
					         <ItemTemplate>
                                 <asp:Label ID="lblBookmark" Visible="False" Runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Bookmark") %>'></asp:Label>
                                 <asp:LinkButton CssClass="btn btn-danger" ID="btnAddBookmark" CommandName="AddBookmark" Runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Bookmark") %>'></asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle Width="10%" />
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
