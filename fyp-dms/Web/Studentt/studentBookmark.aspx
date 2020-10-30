<%@ Page Title="" Language="C#" MasterPageFile="~/Web/Studentt/studentLayout.Master" AutoEventWireup="true" CodeBehind="studentBookmark.aspx.cs" Inherits="fyp_dms.Web.Studentt.studentBookmark" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>HoldDoc - Bookmark</title>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main>
        <div class="container-fluid">
            <h1 class="mt-4">Bookmark</h1>

            <ol class="breadcrumb mb-4">
                <li class="breadcrumb-item"><asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Web/Studentt/studentHome.aspx">Dashboard</asp:HyperLink></li>
                <li class="breadcrumb-item active">Bookmark</li>
            </ol>

            <%-- Bookmark here --%>
            <div class="card mb-4">
                <div class="card-header">
                    <i class="fas fa-table mr-1"></i>
                    Bookmark
                </div>

                <div class="card-body">
                    <div class="table-responsive">
                         <asp:datagrid id="dgStudentMain" CssClass="Grid table" BorderColor="Transparent" runat="server"  AutoGenerateColumns="false" ShowFooter="true"
		                  OnItemDataBound="dgStudentMain_ItemDataBound" OnItemCommand="dgStudentMain_ItemCommand" OnEditCommand="dgEdit_StudentMain" 
		                  OnCancelCommand="dgCancel_StudentMain" OnUpdateCommand="dgUpdate_StudentMain" PageSize="10" AllowPaging="True" OnPageIndexChanged="dgStudentMain_PageIndexChanged" OnSelectedIndexChanged="dgStudentMain_SelectedIndexChanged">
			 	         <AlternatingItemStyle CssClass="GridItem"></AlternatingItemStyle>
                         <ItemStyle CssClass="GridItem"></ItemStyle>
				         <HeaderStyle CssClass="GridItem"></HeaderStyle>
                         <FooterStyle CssClass=""></FooterStyle>

				           <Columns>
					        <asp:TemplateColumn HeaderText="No">
                            <ItemTemplate>
                               <asp:Label ID="lblID" visible="False" Runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ID") %>'></asp:Label>
                               <asp:Label ID="lblNo" Runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "No") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="5%" />
                            <HeaderStyle Font-Bold="True" />
                            </asp:TemplateColumn>  	           
                    
                            <asp:TemplateColumn HeaderText="Title">
                            <HeaderTemplate>
                                <asp:Label ID="OrderTitle" Runat="server" Text='Title'></asp:Label>
                                <asp:LinkButton ID="LinkButtonTitleASC" runat="server" Text='<i class="fas fa-angle-up"></i>' CommandName="Sort" CommandArgument="TitleASC">
                                </asp:LinkButton>
                                <asp:LinkButton ID="LinkButtonTitleDESC" runat="server" Text='<i class="fas fa-angle-down"></i>' CommandName="Sort" CommandArgument="TitleDESC">
                                </asp:LinkButton>
                             </HeaderTemplate>

					         <ItemTemplate>
                                <asp:Label ID="lblTitle" Runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Title") %>'></asp:Label>
                             </ItemTemplate>

                             <ItemStyle Width="20%" />
                                <HeaderStyle Font-Bold="True" />
					         </asp:TemplateColumn>

                             <asp:TemplateColumn HeaderText="Folder">
                             <HeaderTemplate>
                                 <asp:Label ID="OrderFolder" Runat="server" Text='Folder'></asp:Label>
                                 <asp:LinkButton ID="LinkButtonFolderNameASC" runat="server" Text='<i class="fas fa-angle-up"></i>' CommandName="Sort" CommandArgument="FolderASC">
                                 </asp:LinkButton>
                                 <asp:LinkButton ID="LinkButtonFolderNameDESC" runat="server" Text='<i class="fas fa-angle-down"></i>' CommandName="Sort" CommandArgument="FolderDESC">
                                 </asp:LinkButton>
                             </HeaderTemplate>

					         <ItemTemplate>
                                 <asp:Label ID="lblFile" Visible="false" Runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "FILE") %>'></asp:Label>
                                 <asp:Label ID="lblFolderName" Visible="true" Runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "FolderName") %>'></asp:Label>
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
                            <ItemStyle Width="14%" />
                                 <HeaderStyle Font-Bold="True" />
					        </asp:TemplateColumn>

                            <asp:TemplateColumn HeaderText="Tag">
					        <ItemTemplate>
                                 <asp:Label ID="lblTag" Runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Tag") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtTag" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Tag") %>'></asp:TextBox>
                                <asp:Label ID="txtID" visible="False" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ID") %>'></asp:Label>
					        </EditItemTemplate>
                            <ItemStyle Width="20%" />
                            <HeaderStyle Font-Bold="True" />
				        	</asp:TemplateColumn>

			                <asp:EditCommandColumn ItemStyle-Width="10%" HeaderText="Update" ButtonType="PushButton" UpdateText="Update" CancelText="Cancel" EditText="Update" HeaderStyle-Font-Bold="True"></asp:EditCommandColumn>

                            <asp:TemplateColumn HeaderText="Delete">
						    <HeaderStyle Font-Bold="True"></HeaderStyle>
						   <ItemTemplate>
							   <asp:LinkButton CSSclass="btn btn-danger" ID="btnDeleteLec" CommandName="DeleteLec" Text="<i class='fa fa-trash'></i>" Runat="server"></asp:LinkButton>
						   </ItemTemplate>
                           <ItemStyle Width="5%" />
                          <HeaderStyle Font-Bold="True" />
					      </asp:TemplateColumn>
				         </Columns>
			             <PagerStyle Mode="NumericPages" CssClass="pages" />
 
			           </asp:datagrid>
                       <asp:Label ID="lblAttention" runat="server" CssClass="Note"></asp:Label>
                    </div>
                </div>
            </div>
        </div>
    </main>
</asp:Content>