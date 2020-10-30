<%@ Page Title="" Language="C#" MasterPageFile="~/Web/Lecturerr/lecturerLayout.Master" AutoEventWireup="true" CodeBehind="lecturerProfile.aspx.cs" Inherits="fyp_dms.Web.Lecturerr.lecturerProfile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>HoldDoc - Lecturer Profile</title>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main>
        <div class="container-fluid">
            <h1 class="mt-4">Lecturer Profile</h1>

            <ol class="breadcrumb mb-4">
                <li class="breadcrumb-item">
                    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Web/Lecturerr/lecturerHome.aspx">Dashboard</asp:HyperLink></li>
                <li class="breadcrumb-item active">Lecturer Profile</li>
            </ol>

            <%-- Lecturer Profile here --%>
            <div class="card mb-4">
                <div class="card-header">
                    <i class="fas fa-table mr-1"></i>
                    Lecturer Profile
               
                </div>

                <div class="card-body">
                    <div class="table-responsive">
                        <asp:datagrid id="dgLecturerProfile"  CssClass="Grid table" runat="server"  AutoGenerateColumns="false" ShowFooter="true" BorderColor="Transparent"
		                 OnItemDataBound="dgLecturerProfile_ItemDataBound" OnItemCommand="dgLecturerProfile_ItemCommand" OnEditCommand="dgEdit_LecturerProfile" 
		                 OnCancelCommand="dgCancel_LecturerProfile" OnUpdateCommand="dgUpdate_LecturerProfile" PageSize="10" AllowPaging="True" OnPageIndexChanged="dgLecturerProfile_PageIndexChanged" OnSelectedIndexChanged="dgLecturerProfile_SelectedIndexChanged">
			 	        <AlternatingItemStyle CssClass=""></AlternatingItemStyle>
                        <ItemStyle CssClass="GridItem"></ItemStyle>
				        <HeaderStyle CssClass="GridItem"></HeaderStyle>
                
				          <Columns>
					        <asp:TemplateColumn HeaderText="Profile">
                            <ItemTemplate>
                             LecturerID:<b><asp:Label ID="lblID" Runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "lecturerID") %>'></asp:Label></b><br/>
                             Name:<b><asp:Label ID="lblName" Runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "name") %>'></asp:Label></b><br/>
                             Email:<b><asp:Label ID="lblEmail" Runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "email") %>'></asp:Label></b><br/>
                             PhoneNo:<b><asp:Label ID="lblPhone" Runat="server" Maxlength="11" Text='<%# DataBinder.Eval(Container.DataItem, "phoneno") %>'></asp:Label></b><br/>
                             Password:<b>*********</b>
                             </ItemTemplate>

                             <EditItemTemplate>
                             LecturerID:<b><asp:Label ID="txtID" Runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "lecturerID") %>'></asp:Label></b><br/>
                             Name:<b><asp:Label ID="txtName" Runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "name") %>'></asp:Label></b><br/>
                             Email:<b><asp:Label ID="txtEmail" Runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "email") %>'></asp:Label></b><br/>
                             PhoneNo:<asp:TextBox ID="txtPhone" Runat="server" Maxlength="11" Text='<%# DataBinder.Eval(Container.DataItem, "phoneno") %>'></asp:TextBox><br/>
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
