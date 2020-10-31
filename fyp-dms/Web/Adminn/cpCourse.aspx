<%@ Page Title="" Language="C#" MasterPageFile="~/Web/Adminn/adminLayout.Master" AutoEventWireup="true" CodeBehind="cpCourse.aspx.cs" Inherits="fyp_dms.Web.Adminn.cpCourse" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>HoldDoc - Manage School Course</title>

    <script>      
        var object = { status: false, element: null };

        //delete course 
        function confirmDeleteCourse(event) {
            if (object.status) { return true; };

            swal({
                title: "Are you sure?",
                text: "You will not be able to recover this course record later!",
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
            <h1 class="mt-4">Manage Course</h1>

            <ol class="breadcrumb mb-4">
                <li class="breadcrumb-item"><asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Web/Adminn/adminHome.aspx">Dashboard</asp:HyperLink></li>
                <li class="breadcrumb-item active">Manage Course</li>
            </ol>

            <%-- Course Table here --%>
            <div class="card mb-4">
                <div class="card-header">
                    <i class="fas fa-table mr-1"></i>
                    Course Table
                </div>

                <div class="card-body">
                    <div class="table-responsive">
                        <asp:datagrid id="dgCourse" CssClass="table table-bordered" runat="server" AutoGenerateColumns="false" Width="100%" ShowFooter="true"
		                 OnItemDataBound="dgCourse_ItemDataBound" OnItemCommand="dgCourse_ItemCommand" OnEditCommand="dgEdit_Course" 
		                 OnCancelCommand="dgCancel_Course" OnUpdateCommand="dgUpdate_Course" PageSize="10" AllowPaging="True" OnPageIndexChanged="dgCourse_PageIndexChanged" OnSelectedIndexChanged="dgCourse_SelectedIndexChanged">
			 	            <AlternatingItemStyle CssClass=""></AlternatingItemStyle>
                            <ItemStyle CssClass="GridItem"></ItemStyle>
				            <HeaderStyle CssClass="GridItem"></HeaderStyle>
                            <FooterStyle CssClass="GridItem"></FooterStyle>
				            <Columns>
					            <asp:TemplateColumn HeaderText="Course Code">
                                <ItemTemplate>
                                <asp:Label ID="lblID" visible="false" Runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CourseID") %>'></asp:Label>
                                <asp:Label ID="lblCourseCode" MaxLength="8" Runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CourseCode") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                <asp:Label ID="txtID" visible="false" Runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CourseID") %>'></asp:Label>
                                <asp:TextBox ID="txtCourseCode" Maxlength="8" Runat="server" CssClass="form-control" Text='<%# DataBinder.Eval(Container.DataItem, "CourseCode") %>'></asp:TextBox>                        
					            </EditItemTemplate>
                                <FooterTemplate>	
                                     <asp:TextBox ID="txtCourseCodeNew" MaxLength="8" runat="server" CssClass="form-control"></asp:TextBox>
					            </FooterTemplate>
                                    <ItemStyle Width="20%" />
                                    <HeaderStyle Font-Bold="True" />
                                </asp:TemplateColumn>  	           
                    
                    
                                <asp:TemplateColumn HeaderText="Programme ID">
                                <ItemTemplate>
                                <asp:Label ID="lblProgrammeID" Runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ProgrammeName") %>'></asp:Label>               
                                </ItemTemplate>
                                <EditItemTemplate>
                                <asp:Label ID="lblProgrammeIDEdit" visible="false" Runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ProgrammeName") %>'></asp:Label>  
                                <asp:DropDownList ID="ddlProgrammeID" runat="server" CssClass="form-control"></asp:DropDownList>
					            </EditItemTemplate>
                                <FooterTemplate>	
                                <asp:DropDownList ID="ddlProgrammeIDNew" runat="server" CssClass="form-control"></asp:DropDownList>
					            </FooterTemplate>
                                    <ItemStyle Width="20%" />
                                    <HeaderStyle Font-Bold="True" />
                                </asp:TemplateColumn>  	           


                                <asp:TemplateColumn HeaderText="Course Name">
                                <ItemTemplate>
                                <asp:Label ID="lblCourseName" Runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CourseName") %>'></asp:Label>               
                                </ItemTemplate>
                                <EditItemTemplate>
                                <asp:TextBox ID="txtCourseName" Runat="server" CssClass="form-control" Text='<%# DataBinder.Eval(Container.DataItem, "CourseName") %>'></asp:TextBox>                        
					            </EditItemTemplate>
                                <FooterTemplate>	
                                     <asp:TextBox ID="txtCourseNameNew" MaxLength="30" runat="server" CssClass="form-control"></asp:TextBox>
					            </FooterTemplate>
                                    <ItemStyle Width="20%" />
                                    <HeaderStyle Font-Bold="True" />
                                </asp:TemplateColumn>  	           

			                    <asp:EditCommandColumn ItemStyle-Width="10%" HeaderText="Update" ButtonType="PushButton" UpdateText="Update" CancelText="Cancel" EditText="Update" HeaderStyle-Font-Bold="True"></asp:EditCommandColumn>
                    
 					            <asp:TemplateColumn HeaderText="Delete">
						            <HeaderStyle Font-Bold="True"></HeaderStyle>
						            <ItemTemplate>
							            <asp:Button CSSclass="btn btn-danger" ID="btnDeleteCourse" CommandName="DeleteCourse" Text="Delete" Runat="server"></asp:Button>
						            </ItemTemplate>
						            <FooterTemplate>
							            <asp:Button CSSclass="btn btn-success" ID="btnAddCourse" CommandName="AddCourse" Runat="server" Text="Add"></asp:Button>
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
