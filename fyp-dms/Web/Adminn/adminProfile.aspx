<%@ Page Title="" Language="C#" MasterPageFile="~/Web/Adminn/adminLayout.Master" AutoEventWireup="true" CodeBehind="adminProfile.aspx.cs" Inherits="fyp_dms.Web.Adminn.adminProfile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>HoldDoc - Admin Profile</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main>
        <div class="container-fluid">
            <h1 class="mt-4">Admin Profile</h1>

            <ol class="breadcrumb mb-4">
                <li class="breadcrumb-item"><asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Web/Adminn/adminHome.aspx">Dashboard</asp:HyperLink></li>
                <li class="breadcrumb-item active">Admin Profile</li>
            </ol>

            <%-- Admin Profile here --%>
            <div class="card mb-4">
                <div class="card-header">
                    <i class="fas fa-table mr-1"></i>
                    Admin Profile
                </div>

                <div class="card-body">
                    <div class="table-responsive">
                       <asp:datagrid id="dgAdminProfile" CssClass="table table-bordered" runat="server" AutoGenerateColumns="false" Width="100%" ShowFooter="true"
		               OnItemDataBound="dgAdminProfile_ItemDataBound" OnItemCommand="dgAdminProfile_ItemCommand" OnEditCommand="dgEdit_AdminProfile" 
		               OnCancelCommand="dgCancel_AdminProfile" OnUpdateCommand="dgUpdate_AdminProfile" PageSize="10" AllowPaging="True" OnPageIndexChanged="dgAdminProfile_PageIndexChanged" OnSelectedIndexChanged="dgAdminProfile_SelectedIndexChanged">
			 	        <AlternatingItemStyle CssClass=""></AlternatingItemStyle>
                        <ItemStyle CssClass="GridItem"></ItemStyle>
				        <HeaderStyle CssClass="GridItem"></HeaderStyle>
                
				          <Columns>
					        <asp:TemplateColumn HeaderText="Profile">
                            <ItemTemplate>
                            AdminID:<b><asp:Label ID="lblID" Runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "adminID") %>'></asp:Label></b><br/>
                            Name:<b><asp:Label ID="lblName" Runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "AdminName") %>'></asp:Label></b><br/>
                            Password:<b>*********</b>
                            </ItemTemplate>

                            <EditItemTemplate>
                            AdminID:<b><asp:Label ID="txtID" Runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "adminID") %>'></asp:Label></b><br/>
                            Name:<b><asp:TextBox ID="txtName" Runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "AdminName") %>'></asp:TextBox></b><br/>                    
                            Old Password:<asp:TextBox TextMode="Password" ID="txtOldPassword" Runat="server" Text=""></asp:TextBox><br/>
                            New Password:<asp:TextBox TextMode="Password" ID="txtNewPassword" Runat="server" Text=""></asp:TextBox><br/>
                            Confirm Password:<asp:TextBox TextMode="Password" ID="txtConfirmPassword" Runat="server" Text=""></asp:TextBox><br/>
					        </EditItemTemplate>

                            <ItemStyle Width="20%" />
                            <HeaderStyle Font-Bold="True" />
                            </asp:TemplateColumn>  	           
                    
                            <asp:EditCommandColumn ItemStyle-Width="10%" HeaderText="Update" ButtonType="PushButton" UpdateText="Update" CancelText="Cancel" EditText="Update Profile" HeaderStyle-Font-Bold="True"></asp:EditCommandColumn>
				          </Columns>
			            <PagerStyle visible="False" Mode="NumericPages" CssClass="pages" />
 
			            </asp:datagrid>

                     </div>
                </div>
            </div>
        </div>
    </main>
</asp:Content>