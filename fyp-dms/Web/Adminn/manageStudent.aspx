<%@ Page Title="" Language="C#" MasterPageFile="~/Web/Adminn/adminLayout.Master" AutoEventWireup="true" CodeBehind="manageStudent.aspx.cs" Inherits="fyp_dms.Web.Adminn.manageStudent" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>HoldDoc - Manage Student</title>

    <script>      
        var object = { status: false, element: null };

        //delete student
        function confirmDeleteStudent(event) {
            if (object.status) { return true; };

            swal({
                title: "Are you sure?",
                text: "You will not be able to recover this student record later!",
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
            <h1 class="mt-4">Manage Student</h1>

            <ol class="breadcrumb mb-4">
                <li class="breadcrumb-item"><asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Web/Adminn/adminHome.aspx">Dashboard</asp:HyperLink></li>
                <li class="breadcrumb-item active">Manage Student</li>
            </ol>

            <%-- Student Table here --%>
            <div class="card mb-4">
                <div class="card-header">
                    <i class="fas fa-table mr-1"></i>
                    Student Table
                </div>

                <div class="card-body">
                    <div class="table-responsive">
                        <asp:datagrid id="dgStudent" CssClass="table table-bordered" runat="server" AutoGenerateColumns="false" Width="100%" ShowFooter="true"
		                 OnItemDataBound="dgStudent_ItemDataBound" OnItemCommand="dgStudent_ItemCommand" OnEditCommand="dgEdit_Student" 
		                 OnCancelCommand="dgCancel_Student" OnUpdateCommand="dgUpdate_Student" PageSize="10" AllowPaging="True" OnPageIndexChanged="dgStudent_PageIndexChanged" OnSelectedIndexChanged="dgStudent_SelectedIndexChanged">
			 	            <AlternatingItemStyle CssClass=""></AlternatingItemStyle>
                            <ItemStyle CssClass="GridItem"></ItemStyle>
				            <HeaderStyle CssClass="GridItem"></HeaderStyle>
                            <FooterStyle CssClass="GridItem"></FooterStyle>
				            <Columns>
					            <asp:TemplateColumn HeaderText="Student ID">
                                <ItemTemplate>   
                                <asp:Label ID="lblStudentID" Runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "StudentID") %>'></asp:Label>
                                </ItemTemplate>
            <%--                    <EditItemTemplate>
                                <asp:Label ID="txtStudentID" Runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "StudentID") %>'></asp:Label>
					            </EditItemTemplate>--%>
                                <FooterTemplate>	
                                    <asp:TextBox ID="txtStudentIDNew" MaxLength="8" runat="server" CssClass="form-control"></asp:TextBox>
					            </FooterTemplate>
                                    <HeaderStyle Font-Bold="True" />
                                </asp:TemplateColumn>  	           
                    
                                <asp:TemplateColumn HeaderText="Year">
                                <ItemTemplate>   
                                    <asp:Label ID="lblYear" Runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "YearValue") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:Label ID="lblYearEdit" visible="false" Runat="server" CssClass="form-control" Text='<%# DataBinder.Eval(Container.DataItem, "YearValue") %>'></asp:Label>
                                    <asp:DropDownList ID="ddlYear" runat="server" CssClass="form-control"></asp:DropDownList>
					            </EditItemTemplate>
                                <FooterTemplate>	
                                    <asp:DropDownList ID="ddlYearNew" runat="server" CssClass="form-control"></asp:DropDownList>
					            </FooterTemplate>
                                    <HeaderStyle Font-Bold="True" />
                                </asp:TemplateColumn>

                                <asp:TemplateColumn HeaderText="Intake">
                                <ItemTemplate>   
                                    <asp:Label ID="lblIntake" Runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "IntakeMonth") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:Label ID="lblIntakeEdit" visible="false" Runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "IntakeMonth") %>'></asp:Label>
                                    <asp:DropDownList ID="ddlIntake" runat="server" CssClass="form-control"></asp:DropDownList>
					            </EditItemTemplate>
                                <FooterTemplate>	
                                    <asp:DropDownList ID="ddlIntakeNew" runat="server" CssClass="form-control"></asp:DropDownList>
					            </FooterTemplate>
                                    <HeaderStyle Font-Bold="True" />
                                </asp:TemplateColumn>

                                <asp:TemplateColumn HeaderText="Programme">
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
                                    <HeaderStyle Font-Bold="True" />
                                </asp:TemplateColumn>

                                <asp:TemplateColumn HeaderText="Student Name">
						            <ItemTemplate>
                                        <asp:Label ID="lblName" Runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Name") %>'></asp:Label>
                                    </ItemTemplate>
						            <EditItemTemplate>     
							            <asp:TextBox ID="txtName" runat="server" CssClass="form-control" Text='<%# DataBinder.Eval(Container.DataItem, "Name") %>'></asp:TextBox>
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
						            <asp:TextBox ID="txtPhone" runat="server" CssClass="form-control" Text='<%# DataBinder.Eval(Container.DataItem, "PhoneNo") %>'></asp:TextBox>
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
                                    <asp:Label ID="lblAdmin" Runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "adminName") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="20%" />
                                <HeaderStyle Font-Bold="True" />
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
						            <HeaderStyle Font-Bold="True"></HeaderStyle>
						            <ItemTemplate>
							            <asp:Button CSSclass="btn btn-danger" ID="btnDeleteStudent" CommandName="DeleteStudent" Text="Delete" Runat="server"></asp:Button>
						            </ItemTemplate>
						            <FooterTemplate>
							            <asp:Button CSSclass="btn btn-success" ID="btnAddStudent" CommandName="AddStudent" Runat="server" Text="Add"></asp:Button>
						            </FooterTemplate>
					            </asp:TemplateColumn>
				            </Columns>
			            <PagerStyle Mode="NumericPages" CssClass="pages" />
 
			            </asp:datagrid>

                        <asp:Label ID="Label1" runat="server" Text="The student with <u>new password</u> and <u>reset password</u> will be <b> holdDoc </b>"></asp:Label>

                    </div>
                </div>
            </div>
        </div>
    </main>
</asp:Content>
