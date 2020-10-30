<%@ Page Title="" Language="C#" MasterPageFile="~/Web/Adminn/adminLayout.Master" AutoEventWireup="true" CodeBehind="cpCourseSection.aspx.cs" Inherits="fyp_dms.Web.Adminn.cpCourseSection" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>HoldDoc - Manage Course Section</title>

    <script>      
        var object = { status: false, element: null };

        //delete course 
        function confirmDeleteCourseSection(event) {
            if (object.status) { return true; };

            swal({
                title: "Are you sure?",
                text: "You will not be able to recover this course section record later!",
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
            <h1 class="mt-4">Manage Course Section</h1>

            <ol class="breadcrumb mb-4">
                <li class="breadcrumb-item"><asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Web/Adminn/adminHome.aspx">Dashboard</asp:HyperLink></li>
                <li class="breadcrumb-item active">Manage Course Section</li>
            </ol>

            <%-- Course Section Table here --%>
            <div class="card mb-4">
                <div class="card-header">
                    <i class="fas fa-table mr-1"></i>
                    Course Section Table
                </div>

                <div class="card-body">
                    <div class="table-responsive">
                        <asp:datagrid id="dgCourseSection" runat="server" CssClass="table table-bordered" AutoGenerateColumns="false" Width="100%" ShowFooter="true"
		                 OnItemDataBound="dgCourseSection_ItemDataBound" OnItemCommand="dgCourseSection_ItemCommand" OnEditCommand="dgEdit_CourseSection" 
		                 OnCancelCommand="dgCancel_CourseSection" OnUpdateCommand="dgUpdate_CourseSection" PageSize="10" AllowPaging="True" OnPageIndexChanged="dgCourseSection_PageIndexChanged" OnSelectedIndexChanged="dgCourseSection_SelectedIndexChanged">
			 	            <AlternatingItemStyle CssClass=""></AlternatingItemStyle>
                            <ItemStyle CssClass="GridItem"></ItemStyle>
				            <HeaderStyle CssClass="GridItem"></HeaderStyle>
                            <FooterStyle CssClass="GridItem"></FooterStyle>
				            <Columns>
					            <asp:TemplateColumn HeaderText="Course Code">
                                <ItemTemplate>
                                <asp:Label ID="lblID" visible="false" Runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CourseSectionID") %>'></asp:Label>
                                <asp:Label ID="lblCourseCode" Runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CourseCode") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:Label ID="txtID" visible="false" Runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CourseSectionID") %>'></asp:Label>
                                    <asp:Label ID="lblCourseCodeEdit" visible="false" Runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CourseCode") %>'></asp:Label>  
                                    <asp:DropDownList ID="ddlCourseCode" runat="server" CssClass="form-control"></asp:DropDownList> 
					            </EditItemTemplate>
                                <FooterTemplate>	
                                    <asp:DropDownList ID="ddlCourseCodeNew" runat="server" CssClass="form-control"></asp:DropDownList>
					            </FooterTemplate>
                                    <ItemStyle Width="20%" />
                                    <HeaderStyle Font-Bold="True" />
                                </asp:TemplateColumn>  	           
                    
                                <asp:TemplateColumn HeaderText="Section No">
                                <ItemTemplate>
                                <asp:Label ID="lblSectionNo" Runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "SectionNo") %>'></asp:Label>               
                                </ItemTemplate>
                                <EditItemTemplate>
                                <asp:TextBox ID="txtSectionNo" Runat="server" CssClass="form-control" Text='<%# DataBinder.Eval(Container.DataItem, "SectionNo") %>'></asp:TextBox>                        
					            </EditItemTemplate>
                                <FooterTemplate>	
                                     <asp:TextBox ID="txtSectionNoNew" MaxLength="5" runat="server" CssClass="form-control"></asp:TextBox>
					            </FooterTemplate>
                                    <ItemStyle Width="20%" />
                                    <HeaderStyle Font-Bold="True" />
                                </asp:TemplateColumn>  	           

			                    <asp:EditCommandColumn ItemStyle-Width="10%" HeaderText="Update" ButtonType="PushButton" UpdateText="Update" CancelText="Cancel" EditText="Update" HeaderStyle-Font-Bold="True"></asp:EditCommandColumn>
                    
 					            <asp:TemplateColumn HeaderText="Delete">
						            <HeaderStyle Font-Bold="True"></HeaderStyle>
						            <ItemTemplate>
							            <asp:Button CSSclass="btn btn-danger" ID="btnDeleteCourseSection" CommandName="DeleteCourseSection" Text="Delete" Runat="server"></asp:Button>
						            </ItemTemplate>
						            <FooterTemplate>
							            <asp:Button CSSclass="btn btn-success" ID="btnAddCourseSection" CommandName="AddCourseSection" Runat="server" Text="Add"></asp:Button>
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
