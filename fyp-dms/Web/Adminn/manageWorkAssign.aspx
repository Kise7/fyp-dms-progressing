<%@ Page Title="" Language="C#" MasterPageFile="~/Web/Adminn/adminLayout.Master" AutoEventWireup="true" CodeBehind="manageWorkAssign.aspx.cs" Inherits="fyp_dms.Web.Adminn.manageWorkAssign" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>HoldDoc - Manage Work Assign to Lecturer</title>

    <script>      
        var object = { status: false, element: null };

        //delete work assign
        function confirmDeleteWorkAssign(event) {
            if (object.status) { return true; };

            swal({
                title: "Are you sure?",
                text: "You will not be able to recover this work assign record later!",
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
            <h1 class="mt-4">Manage Work Assign</h1>

            <ol class="breadcrumb mb-4">
                <li class="breadcrumb-item"><asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Web/Adminn/adminHome.aspx">Dashboard</asp:HyperLink></li>
                <li class="breadcrumb-item active">Manage Work Assign</li>
            </ol>

            <%-- Work Assign Table here --%>
            <div class="card mb-4">
                <div class="card-header">
                    <i class="fas fa-table mr-1"></i>
                    Work Assign Table
                </div>

                <div class="card-body">
                    <div class="table-responsive">
                        <asp:datagrid id="dgWorkAssign" runat="server" CssClass="table table-bordered" AutoGenerateColumns="false" Width="100%" ShowFooter="true"
		                 OnItemDataBound="dgWorkAssign_ItemDataBound" OnItemCommand="dgWorkAssign_ItemCommand" OnEditCommand="dgEdit_WorkAssign" 
		                 OnCancelCommand="dgCancel_WorkAssign" OnUpdateCommand="dgUpdate_WorkAssign" PageSize="10" AllowPaging="True" OnPageIndexChanged="dgWorkAssign_PageIndexChanged" OnSelectedIndexChanged="dgWorkAssign_SelectedIndexChanged">
			 	            <AlternatingItemStyle CssClass=""></AlternatingItemStyle>
                            <ItemStyle CssClass="GridItem"></ItemStyle>
				            <HeaderStyle CssClass="GridItem"></HeaderStyle>
                            <FooterStyle CssClass="GridItem"></FooterStyle>
				            <Columns>
					            <asp:TemplateColumn HeaderText="Course Code">
                                <ItemTemplate>
                                <asp:Label ID="lblID" visible="false" Runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "WorkAssignID") %>'></asp:Label>
                                <asp:Label ID="lblCourseCode" Runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CourseCode") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:Label ID="txtID" visible="false" Runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "WorkAssignID") %>'></asp:Label>
                                    <asp:Label ID="lblCourseCodeEdit" visible="false" Runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CourseCode") %>'></asp:Label>  
                                    <asp:DropDownList ID="ddlCourseCode" runat="server" CssClass="form-control"></asp:DropDownList> 
					            </EditItemTemplate>
                                <FooterTemplate>	
                                    <asp:DropDownList ID="ddlCourseCodeNew" runat="server" CssClass="form-control"></asp:DropDownList>
					            </FooterTemplate>
                                    <ItemStyle Width="20%" />
                                    <HeaderStyle Font-Bold="True" />
                                </asp:TemplateColumn>  	           
                    
                                <asp:TemplateColumn HeaderText="Lecturer Name">
                                <ItemTemplate>
                                <asp:Label ID="lblLecturerName" Runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "LecturerName") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:Label ID="lblLecturerNameEdit" visible="false" Runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "LecturerName") %>'></asp:Label>  
                                    <asp:DropDownList ID="ddlLecturerName" runat="server" CssClass="form-control"></asp:DropDownList> 
					            </EditItemTemplate>
                                <FooterTemplate>	
                                    <asp:DropDownList ID="ddlLecturerNameNew" runat="server" CssClass="form-control"></asp:DropDownList>
					            </FooterTemplate>
                                    <ItemStyle Width="20%" />
                                    <HeaderStyle Font-Bold="True" />
                                </asp:TemplateColumn>  	

                                <asp:TemplateColumn HeaderText="Position">
                                <ItemTemplate>
                                    <asp:Label ID="lblPosotion" Runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Position") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtPosition" MaxLength="60" runat="server" CssClass="form-control" Text='<%# DataBinder.Eval(Container.DataItem, "Position") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <FooterTemplate>
							        <asp:TextBox ID="txtPositionNew" MaxLength="60" runat="server" CssClass="form-control"></asp:TextBox>
					            </FooterTemplate>
                                    <ItemStyle Width="20%" />
                                    <HeaderStyle Font-Bold="True" />
                                </asp:TemplateColumn>  	

                                <asp:TemplateColumn HeaderText="Status">
					            <ItemTemplate>
                                    <asp:Label ID="lblStatus" runat="server" visible="true" Text='<%# (DataBinder.Eval(Container.DataItem, "Status")) %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:Label ID="txtStatus" runat="server" visible="false" Text='<%# (DataBinder.Eval(Container.DataItem, "Status")) %>'></asp:Label>
                                    <asp:DropDownList ID="ddlStatusEdit" runat="server" CssClass="form-control">
                                        <asp:ListItem>Please Choose</asp:ListItem>
                                        <asp:ListItem>Active</asp:ListItem>
                                        <asp:ListItem>Not Active</asp:ListItem>
                                    </asp:DropDownList>
					            </EditItemTemplate>
                                <FooterTemplate>
							        <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control">
                                        <asp:ListItem>Please Choose</asp:ListItem>
                                        <asp:ListItem>Active</asp:ListItem>
                                        <asp:ListItem>Not Active</asp:ListItem>
                                    </asp:DropDownList>
					            </FooterTemplate>
					            <ItemStyle Width="20%" />
                                    <HeaderStyle Font-Bold="True" />
					            </asp:TemplateColumn>

			                    <asp:EditCommandColumn ItemStyle-Width="10%" HeaderText="Update" ButtonType="PushButton" UpdateText="Update" CancelText="Cancel" EditText="Update" HeaderStyle-Font-Bold="True"></asp:EditCommandColumn>
                    
 					            <asp:TemplateColumn HeaderText="Delete">
						            <HeaderStyle Font-Bold="True"></HeaderStyle>
						            <ItemTemplate>
							            <asp:Button CSSclass="btn btn-danger" ID="btnDeleteWorkAssign" CommandName="DeleteWorkAssign" Text="Delete" Runat="server"></asp:Button>
						            </ItemTemplate>
						            <FooterTemplate>
							            <asp:Button CSSclass="btn btn-success" ID="btnAddWorkAssign" CommandName="AddWorkAssign" Runat="server" Text="Add"></asp:Button>
						            </FooterTemplate>
                                     <ItemStyle Width="10%" />
                                    <HeaderStyle Font-Bold="True" />
					            </asp:TemplateColumn>
				            </Columns>
			            <PagerStyle Mode="NumericPages" />
 
			            </asp:datagrid>
                    </div>
                </div>
            </div>
        </div>
    </main>
</asp:Content>
