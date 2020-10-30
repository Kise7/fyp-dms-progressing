<%@ Page Title="" Language="C#" MasterPageFile="~/Web/Adminn/adminLayout.Master" AutoEventWireup="true" CodeBehind="manageAdmin.aspx.cs" Inherits="fyp_dms.Web.Adminn.manageAdmin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>HoldDoc - Manage Admin</title>

    <script>      
        var object = { status: false, element: null };

        //delete admin 
        function confirmDeleteAdmin(event) {
            if (object.status) { return true; };

            swal({
                title: "Are you sure?",
                text: "You will not be able to recover this admin record later!",
                type: "warning",
                showCancelButton: true,
                confirmButtonClass: "btn-danger",
                confirmButtonText: "Yes!",
                closeOnConfirm: true
            },
                function () {
                    object.status = true;
                    object.element = event;
                    object.element.click();
                });

            return false;
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main>
        <div class="container-fluid">
            <h1 class="mt-4">Manage Admin</h1>

            <ol class="breadcrumb mb-4">
                <li class="breadcrumb-item"><asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Web/Adminn/adminHome.aspx">Dashboard</asp:HyperLink></li>
                <li class="breadcrumb-item active">Manage Admin</li>
            </ol>

            <%-- Admin Table here --%>
            <div class="card mb-4">
                <div class="card-header">
                    <i class="fas fa-table mr-1"></i>
                    Admin Table
                </div>

                <div class="card-body">
                    <div class="table-responsive">
                        <asp:datagrid id="dgAdmin" CssClass="table table-bordered" runat="server"  AutoGenerateColumns="false" ShowFooter="true"
		                 OnItemDataBound="dgAdmin_ItemDataBound" OnItemCommand="dgAdmin_ItemCommand" OnEditCommand="dgEdit_Admin" 
		                 OnCancelCommand="dgCancel_Admin" OnUpdateCommand="dgUpdate_Admin" PageSize="10" AllowPaging="True" OnPageIndexChanged="dgAdmin_PageIndexChanged" OnSelectedIndexChanged="dgAdmin_SelectedIndexChanged">
			 	            <AlternatingItemStyle CssClass=""></AlternatingItemStyle>
                            <ItemStyle CssClass="GridItem"></ItemStyle>
				            <HeaderStyle CssClass="GridItem"></HeaderStyle>
                            <FooterStyle CssClass="GridItem"></FooterStyle>
				            <Columns>
					            <asp:TemplateColumn HeaderText="Admin ID">
                                <ItemTemplate>
                                <asp:Label ID="lblAdminID" Runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "AdminID") %>'></asp:Label>
                    
                                </ItemTemplate>
                                <EditItemTemplate>
                                <asp:Label ID="txtAdminID" Runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "AdminID") %>'></asp:Label>                        
					            </EditItemTemplate>
                                <FooterTemplate>	
                                     <asp:TextBox ID="txtAdminIDNew" MaxLength="8" runat="server" CssClass="form-control"></asp:TextBox>
					            </FooterTemplate>
                                    <ItemStyle Width="20%" />
                                    <HeaderStyle Font-Bold="True" />
                                </asp:TemplateColumn>  	           
                    
                                <asp:TemplateColumn HeaderText="Admin Name">
					            <ItemTemplate>
                                    <asp:Label ID="lblName" Runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "AdminName") %>'></asp:Label>
                                </ItemTemplate>
					            <EditItemTemplate>
                                    <asp:TextBox ID="txtName" runat="server" CssClass="form-control" Text='<%# DataBinder.Eval(Container.DataItem, "AdminName") %>'></asp:TextBox>
					            </EditItemTemplate>
					            <FooterTemplate>
						            <asp:TextBox ID="txtNameNew" MaxLength="60" runat="server" CssClass="form-control"></asp:TextBox>
                                </FooterTemplate>
                                    <ItemStyle Width="20%" />
                                    <HeaderStyle Font-Bold="True" />
					            </asp:TemplateColumn>
                  
			                    <asp:EditCommandColumn ItemStyle-Width="10%" HeaderText="Update" ButtonType="PushButton" UpdateText="Update" CancelText="Cancel" EditText="Update" HeaderStyle-Font-Bold="True"></asp:EditCommandColumn>

 					            <asp:TemplateColumn HeaderText="Delete">
						            <HeaderStyle Font-Bold="True"></HeaderStyle>
						            <ItemTemplate>
							            <asp:Button CSSclass="btn btn-danger" ID="btnDeleteAdmin" CommandName="DeleteAdmin" Text="Delete" Runat="server"></asp:Button>
						            </ItemTemplate>
						            <FooterTemplate>
							            <asp:Button CSSclass="btn btn-success" ID="btnAddAdmin" CommandName="AddAdmin" Runat="server" Text="Add"></asp:Button>
						            </FooterTemplate>
                                     <ItemStyle Width="10%" />
                                    <HeaderStyle Font-Bold="True" />
					            </asp:TemplateColumn>
				            </Columns>
			            <PagerStyle Mode="NumericPages" CssClass="pages" />
 
			            </asp:datagrid>

                        <br />

                        <asp:Label ID="Label1" runat="server" Text="The admin with <u>new password</u> and <u>reset password</u> will be <b> holdDoc123 </b>"></asp:Label>

                    </div>
                </div>
            </div>
        </div>
    </main> 
</asp:Content>
