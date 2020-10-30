<%@ Page Title="" Language="C#" MasterPageFile="~/Web/Adminn/adminLayout.Master" AutoEventWireup="true" CodeBehind="cpIntake.aspx.cs" Inherits="fyp_dms.Web.Adminn.cpIntake" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>HoldDoc - Manage Intake</title>

    <script>      
        var object = { status: false, element: null };

        //delete study intake 
        function confirmDeleteIntake(event) {
            if (object.status) { return true; };

            swal({
                title: "Are you sure?",
                text: "You will not be able to recover this intake record later!",
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
            <h1 class="mt-4">Manage Intake</h1>

            <ol class="breadcrumb mb-4">
                <li class="breadcrumb-item"><asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Web/Adminn/adminHome.aspx">Dashboard</asp:HyperLink></li>
                <li class="breadcrumb-item active">Manage Intake</li>
            </ol>

            <%-- Intake Table here --%>
            <div class="card mb-4">
                <div class="card-header">
                    <i class="fas fa-table mr-1"></i>
                    Intake Table
                </div>

                <div class="card-body">
                    <div class="table-responsive">
                        <asp:datagrid id="dgIntake" runat="server" CssClass="table table-bordered" AutoGenerateColumns="false" Width="100%" ShowFooter="true"
		                 OnItemDataBound="dgIntake_ItemDataBound" OnItemCommand="dgIntake_ItemCommand" OnEditCommand="dgEdit_Intake" 
		                 OnCancelCommand="dgCancel_Intake" OnUpdateCommand="dgUpdate_Intake" PageSize="10" AllowPaging="True" OnPageIndexChanged="dgIntake_PageIndexChanged" OnSelectedIndexChanged="dgIntake_SelectedIndexChanged">
			 	            <AlternatingItemStyle CssClass=""></AlternatingItemStyle>
                            <ItemStyle CssClass="GridItem"></ItemStyle>
				            <HeaderStyle CssClass="GridItem"></HeaderStyle>
                            <FooterStyle CssClass="GridItem"></FooterStyle>
				            <Columns>
					            <asp:TemplateColumn HeaderText="Intake Month">
                                <ItemTemplate>
                                <asp:Label ID="lblID" Runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "intakeID") %>'></asp:Label>
                                <asp:Label ID="lblMonth" Runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "intakeMonth") %>'></asp:Label>
                    
                                </ItemTemplate>
                                <EditItemTemplate>
                                <asp:Label ID="txtID" Runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "intakeID") %>'></asp:Label>

                                <asp:TextBox ID="txtMonth" Runat="server" CssClass="form-control" Text='<%# DataBinder.Eval(Container.DataItem, "intakeMonth") %>'></asp:TextBox>                        
					            </EditItemTemplate>
                                <FooterTemplate>	
                                     <asp:TextBox ID="txtMonthNew" MaxLength="30" runat="server" CssClass="form-control"></asp:TextBox>
					            </FooterTemplate>
                                    <ItemStyle Width="20%" />
                                    <HeaderStyle Font-Bold="True" />
                                </asp:TemplateColumn>  	           
                    
			                    <asp:EditCommandColumn ItemStyle-Width="10%" HeaderText="Update" ButtonType="PushButton" UpdateText="Update" CancelText="Cancel" EditText="Update" HeaderStyle-Font-Bold="True"></asp:EditCommandColumn>
                    
 					            <asp:TemplateColumn HeaderText="Delete">
						            <HeaderStyle Font-Bold="True"></HeaderStyle>
						            <ItemTemplate>
							            <asp:Button CSSclass="btn btn-danger" ID="btnDeleteIntake" CommandName="DeleteIntake" Text="Delete" Runat="server"></asp:Button>
						            </ItemTemplate>
						            <FooterTemplate>
							            <asp:Button CSSclass="btn btn-success" ID="btnAddIntake" CommandName="AddIntake" Runat="server" Text="Add"></asp:Button>
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
