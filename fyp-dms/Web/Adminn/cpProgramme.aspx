<%@ Page Title="" Language="C#" MasterPageFile="~/Web/Adminn/adminLayout.Master" AutoEventWireup="true" CodeBehind="cpProgramme.aspx.cs" Inherits="fyp_dms.Web.Adminn.cpProgramme" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>HoldDoc - Manage School Programme</title>

    <script>      
        var object = { status: false, element: null };

        //delete school programme 
        function confirmDeleteProgramme(event) {
            if (object.status) { return true; };

            swal({
                title: "Are you sure?",
                text: "You will not be able to recover this programme record later!",
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
            <h1 class="mt-4">Manage School Programme</h1>

            <ol class="breadcrumb mb-4">
                <li class="breadcrumb-item"><asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Web/Adminn/adminHome.aspx">Dashboard</asp:HyperLink></li>
                <li class="breadcrumb-item active">Manage Programme</li>
            </ol>

            <%-- Programme Table here --%>
            <div class="card mb-4">
                <div class="card-header">
                    <i class="fas fa-table mr-1"></i>
                    Programme Table
                </div>

                <div class="card-body">
                    <div class="table-responsive">
                        <asp:datagrid id="dgProgramme" CssClass="table table-bordered" runat="server" AutoGenerateColumns="false" Width="100%" ShowFooter="true"
		                 OnItemDataBound="dgProgramme_ItemDataBound" OnItemCommand="dgProgramme_ItemCommand" OnEditCommand="dgEdit_Programme" 
		                 OnCancelCommand="dgCancel_Programme" OnUpdateCommand="dgUpdate_Programme" PageSize="10" AllowPaging="True" OnPageIndexChanged="dgProgramme_PageIndexChanged" OnSelectedIndexChanged="dgProgramme_SelectedIndexChanged">
			 	            <AlternatingItemStyle CssClass=""></AlternatingItemStyle>
                            <ItemStyle CssClass="GridItem"></ItemStyle>
				            <HeaderStyle CssClass="GridItem"></HeaderStyle>
                            <FooterStyle CssClass="GridItem"></FooterStyle>
				            <Columns>
					            <asp:TemplateColumn HeaderText="Programme Code">
                                <ItemTemplate>
                                <asp:Label ID="lblID" visible="false" Runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "programmeID") %>'></asp:Label>
                                <asp:Label ID="lblProgrammeID" Runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "programmeCode") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                <asp:Label ID="txtID" visible="false" Runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "programmeID") %>'></asp:Label>
                                <asp:TextBox ID="txtProgrammeID" Runat="server" CssClass="form-control" Text='<%# DataBinder.Eval(Container.DataItem, "programmeCode") %>'></asp:TextBox>                        
					            </EditItemTemplate>
                                <FooterTemplate>	
                                     <asp:TextBox ID="txtProgrammeNew" MaxLength="30" runat="server" CssClass="form-control"></asp:TextBox>
					            </FooterTemplate>
                                    <ItemStyle Width="20%" />
                                    <HeaderStyle Font-Bold="True" />
                                </asp:TemplateColumn>  	           
                    
                                <asp:TemplateColumn HeaderText="Faculty Name">
                                <ItemTemplate>
                                <asp:Label ID="lblFacultyName" Runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "facultyName") %>'></asp:Label>               
                                </ItemTemplate>
                                <EditItemTemplate>
                                <asp:Label ID="lblFacultyNameEdit" visible="false" Runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "facultyName") %>'></asp:Label>              
                                <asp:DropDownList ID="ddlFacultyName" runat="server" CssClass="dropdown-list"></asp:DropDownList>
					            </EditItemTemplate>
                                <FooterTemplate>	
                                <asp:DropDownList ID="ddlFacultyNameNew" runat="server" CssClass="form-control"></asp:DropDownList>
					            </FooterTemplate>
                                    <ItemStyle Width="20%" />
                                    <HeaderStyle Font-Bold="True" />
                                </asp:TemplateColumn>  	           

                                <asp:TemplateColumn HeaderText="Programme Name">
                                <ItemTemplate>
                                <asp:Label ID="lblProgrammeName" Runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "programmeName") %>'></asp:Label>               
                                </ItemTemplate>
                                <EditItemTemplate>
                                <asp:TextBox ID="txtProgrammeName" Runat="server" CssClass="form-control" Text='<%# DataBinder.Eval(Container.DataItem, "programmeName") %>'></asp:TextBox>                        
					            </EditItemTemplate>
                                <FooterTemplate>	
                                     <asp:TextBox ID="txtProgrammeNameNew" MaxLength="30" runat="server" CssClass="form-control"></asp:TextBox>
					            </FooterTemplate>
                                    <ItemStyle Width="20%" />
                                    <HeaderStyle Font-Bold="True" />
                                </asp:TemplateColumn>

			                    <asp:EditCommandColumn ItemStyle-Width="10%" HeaderText="Update" ButtonType="PushButton" UpdateText="Update" CancelText="Cancel" EditText="Update" HeaderStyle-Font-Bold="True"></asp:EditCommandColumn>

 					            <asp:TemplateColumn HeaderText="Delete">
						            <HeaderStyle Font-Bold="True"></HeaderStyle>
						            <ItemTemplate>
							            <asp:Button CSSclass="btn btn-danger" ID="btnDeleteProgramme" CommandName="DeleteProgramme" Text="Delete" Runat="server"></asp:Button>
						            </ItemTemplate>
						            <FooterTemplate>
							            <asp:Button CSSclass="btn btn-success" ID="btnAddProgramme" CommandName="AddProgramme" Runat="server" Text="Add"></asp:Button>
						            </FooterTemplate>
                                     <ItemStyle Width="10%" />
                                    <HeaderStyle Font-Bold="True" />
					            </asp:TemplateColumn>
				            </Columns>
			            <PagerStyle Mode="NumericPages"/>
			            </asp:datagrid>

                        <asp:Label ID="lblAttention" runat="server" CssClass="Note"></asp:Label>

                    </div>
                </div>
            </div>
        </div>
    </main>
</asp:Content>
