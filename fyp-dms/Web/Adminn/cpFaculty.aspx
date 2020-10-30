<%@ Page Title="" Language="C#" MasterPageFile="~/Web/Adminn/adminLayout.Master" AutoEventWireup="true" CodeBehind="cpFaculty.aspx.cs" Inherits="fyp_dms.Web.Adminn.cpFaculty" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>HoldDoc - Manage School Faculty</title>

    <script>      
        var object = { status: false, element: null };

        //delete school faculty 
        function confirmDeleteFaculty(event) {
            if (object.status) { return true; };

            swal({
                title: "Are you sure?",
                text: "You will not be able to recover this faculty record later!",
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
            <h1 class="mt-4">Manage School Faculty</h1>

            <ol class="breadcrumb mb-4">
                <li class="breadcrumb-item"><asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Web/Adminn/adminHome.aspx">Dashboard</asp:HyperLink></li>
                <li class="breadcrumb-item active">Manage Faculty</li>
            </ol>

            <%-- Faculty Table here --%>
            <div class="card mb-4">
                <div class="card-header">
                    <i class="fas fa-table mr-1"></i>
                    Faculty Table
                </div>

                <div class="card-body">
                    <div class="table-responsive">
                        <asp:datagrid id="dgFaculty" runat="server" CssClass="table table-bordered" AutoGenerateColumns="false" Width="100%" ShowFooter="true"
		                 OnItemDataBound="dgFaculty_ItemDataBound" OnItemCommand="dgFaculty_ItemCommand" OnEditCommand="dgEdit_Faculty" 
		                 OnCancelCommand="dgCancel_Faculty" OnUpdateCommand="dgUpdate_Faculty" PageSize="10" AllowPaging="True" OnPageIndexChanged="dgFaculty_PageIndexChanged" OnSelectedIndexChanged="dgFaculty_SelectedIndexChanged">
			 	            <AlternatingItemStyle CssClass=""></AlternatingItemStyle>
                            <ItemStyle CssClass="GridItem"></ItemStyle>
				            <HeaderStyle CssClass="GridItem"></HeaderStyle>
                            <FooterStyle CssClass="GridItem"></FooterStyle>
				            <Columns>
					            <asp:TemplateColumn HeaderText="Faculty Name">
                                <ItemTemplate>
                                <asp:Label ID="lblID" visible="false" Runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "facultyID") %>'></asp:Label>
                                <asp:Label ID="lblFacultyNAME" Runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "facultyName") %>'></asp:Label>
                    
                                </ItemTemplate>
                                <EditItemTemplate>
                                <asp:Label ID="txtID" visible="false" Runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "facultyID") %>'></asp:Label>
                                <asp:TextBox ID="txtFacultyName" Runat="server" CssClass="form-control" Text='<%# DataBinder.Eval(Container.DataItem, "facultyName") %>'></asp:TextBox>                        
					            </EditItemTemplate>
                                <FooterTemplate>	
                                     <asp:TextBox ID="txtFacultyNew" MaxLength="30" runat="server" CssClass="form-control"></asp:TextBox>
					            </FooterTemplate>
                                    <ItemStyle Width="20%" />
                                    <HeaderStyle Font-Bold="True" />
                                </asp:TemplateColumn>  	           
                    
			                    <asp:EditCommandColumn ItemStyle-Width="10%" HeaderText="Update" ButtonType="PushButton" UpdateText="Update" CancelText="Cancel" EditText="Update" HeaderStyle-Font-Bold="True"></asp:EditCommandColumn>
                    
 					            <asp:TemplateColumn HeaderText="Delete">
						            <HeaderStyle Font-Bold="True"></HeaderStyle>
						            <ItemTemplate>
							            <asp:Button CSSclass="btn btn-danger" ID="btnDeleteFaculty" CommandName="DeleteFaculty" Text="Delete" Runat="server"></asp:Button>
						            </ItemTemplate>
						            <FooterTemplate>
							            <asp:Button CSSclass="btn btn-success" ID="btnAddFaculty" CommandName="AddFaculty" Runat="server" Text="Add"></asp:Button>
						            </FooterTemplate>
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
