<%@ Page Title="" Language="C#" MasterPageFile="~/Web/Adminn/adminLayout.Master" AutoEventWireup="true" CodeBehind="manageLecturer.aspx.cs" Inherits="fyp_dms.Web.Adminn.manageLecturer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>HoldDoc - Manage Lecturer</title>

    <script>      
        var object = { status: false, element: null };

        //delete lecturer
        function confirmDeleteLecturer(event) {
            if (object.status) { return true; };

            swal({
                title: "Are you sure?",
                text: "You will not be able to recover this lecturer record later!",
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
            <h1 class="mt-4">Manage Lecturer</h1>

            <ol class="breadcrumb mb-4">
                <li class="breadcrumb-item"><asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Web/Adminn/adminHome.aspx">Dashboard</asp:HyperLink></li>
                <li class="breadcrumb-item active">Manage Lecturer</li>
            </ol>

            <%-- Lecturer Table here --%>
            <div class="card mb-4">
                <div class="card-header">
                    <i class="fas fa-table mr-1"></i>
                    Lecturer Table
                </div>

                <div class="card-body">
                    <div class="table-responsive">
                        <asp:datagrid id="dgLecturer" CssClass="table table-bordered" runat="server" AutoGenerateColumns="false" Width="100%" ShowFooter="true"
		                 OnItemDataBound="dgLecturer_ItemDataBound" OnItemCommand="dgLecturer_ItemCommand" OnEditCommand="dgEdit_Lecturer" 
		                 OnCancelCommand="dgCancel_Lecturer" OnUpdateCommand="dgUpdate_Lecturer" PageSize="10" AllowPaging="True" OnPageIndexChanged="dgLecturer_PageIndexChanged" OnSelectedIndexChanged="dgLecturer_SelectedIndexChanged">
			 	            <AlternatingItemStyle CssClass=""></AlternatingItemStyle>
                            <ItemStyle CssClass="GridItem"></ItemStyle>
				            <HeaderStyle CssClass="GridItem"></HeaderStyle>
                            <FooterStyle CssClass="GridItem"></FooterStyle>
				            <Columns>
					            <asp:TemplateColumn HeaderText="Lecturer ID">
                                <ItemTemplate>
                                <asp:Label ID="lblLecturerID" Runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "LecturerID") %>'></asp:Label>
                  
                                </ItemTemplate>
<%--                                <EditItemTemplate> 
                                    <asp:TextBox ID="txtLecturerID" runat="server" CssClass="form-control" Text='<%# DataBinder.Eval(Container.DataItem, "LecturerID") %>'></asp:TextBox>
					            </EditItemTemplate>--%>
                                <FooterTemplate>
							
                                    <asp:TextBox ID="txtLecturerIDNew" MaxLength="8" runat="server" CssClass="form-control"></asp:TextBox>
					            </FooterTemplate>
                                    <HeaderStyle Font-Bold="True" />
                                    <ItemStyle Width="20%" />
                                </asp:TemplateColumn>  	           
                    

                                <asp:TemplateColumn HeaderText="Lecturer Name">
						            <ItemTemplate>
                                <asp:Label ID="lblName" Runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Name") %>'></asp:Label>
                   
                                    </ItemTemplate>
						            <EditItemTemplate>
                            
							            <asp:TextBox ID="txtName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Name") %>' CssClass="form-control"></asp:TextBox>
						

						            </EditItemTemplate>
						            <FooterTemplate>
							            <asp:TextBox ID="txtNameNew" MaxLength="60" runat="server" CssClass="form-control"></asp:TextBox>
                            
						            </FooterTemplate>
                                   <ItemStyle Width="30%" />
                                    <HeaderStyle Font-Bold="True" />
					            </asp:TemplateColumn>

                                <asp:TemplateColumn HeaderText="Phone No">
                                <ItemTemplate>
                                    <asp:Label ID="lblPhone" Runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PhoneNo") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>     
						            <asp:TextBox ID="txtPhone" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PhoneNo") %>' CssClass="form-control"></asp:TextBox>
					            </EditItemTemplate>
                                <FooterTemplate>
						            <asp:TextBox ID="txtPhoneNew" MaxLength="11" runat="server" CssClass="form-control"></asp:TextBox>
					            </FooterTemplate>
                                <ItemStyle Width="20%" />
                                <HeaderStyle Font-Bold="True" />
                                </asp:TemplateColumn>

                                <asp:TemplateColumn HeaderText="Email">
                                <ItemTemplate>
                                    <asp:Label ID="lblEmail" Runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Email") %>'></asp:Label>
                                </ItemTemplate>

                                <ItemStyle Width="30%" />
                                <HeaderStyle Font-Bold="True" />
                                </asp:TemplateColumn>

                                <asp:TemplateColumn HeaderText="Modified By">
                                <ItemTemplate>
                                    <asp:Label ID="lblAdmin" Runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "AdminName") %>'></asp:Label>
                                </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" />
                                    <ItemStyle Width="20%" />
                                </asp:TemplateColumn>

			                    <asp:EditCommandColumn HeaderText="Update" ButtonType="PushButton" UpdateText="Update" CancelText="Cancel"
			                     EditText="Update" HeaderStyle-Font-Bold="True"></asp:EditCommandColumn>

                                <asp:TemplateColumn HeaderText="Reset">
						            <ItemTemplate>
							            <asp:Button ID="btnResetPassword" CssClass="btn btn-info" CommandName="ResetPassword" Text="Reset" Runat="server"></asp:Button>
						            </ItemTemplate>
                                     <HeaderStyle Font-Bold="True" />
                                    <ItemStyle Width="10%" />
					            </asp:TemplateColumn>

 					            <asp:TemplateColumn HeaderText="Delete">
						            <ItemTemplate>
							            <asp:Button ID="btnDeleteLecturer" CssClass="btn btn-danger" CommandName="DeleteLecturer" Text="Delete" Runat="server"></asp:Button>
						            </ItemTemplate>
						            <FooterTemplate>
							            <asp:Button ID="btnAddLecturer" CssClass="btn btn-success" CommandName="AddLecturer" Runat="server" Text="Add"></asp:Button>
						            </FooterTemplate>
                                     <HeaderStyle Font-Bold="True" />
                                    <ItemStyle Width="20%" />
					            </asp:TemplateColumn>
				            </Columns>
			            <PagerStyle Mode="NumericPages" />
 
			            </asp:datagrid>

                        <asp:Label ID="Label1" runat="server" Text="The lecturer with <u>new password</u> and <u>reset password</u> will be <b> holdDoc1 </b>"></asp:Label>

                    </div>
                </div>
            </div>
        </div>
    </main>
</asp:Content>
