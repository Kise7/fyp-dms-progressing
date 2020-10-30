<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="fyp_dms.Web.login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />
    <meta name="description" content="" />
    <meta name="author" content="" />
    <link href="css/styles.css" rel="stylesheet" />

    <title>HoldDoc - Login</title>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.13.0/js/all.min.js"></script>

        <%-- Sweet Alert --%>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/sweetalert/1.1.3/sweetalert.css" />
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/sweetalert/1.1.3/sweetalert.min.js"></script>

    <script type="text/javascript">
        //admin success login alert message
        function successAdminAlert() {
            swal({
                title: "Success!",
                text: "Welcome!",
                type: "success",
                confirmButtonText: "OK"
            },
            function (isConfirm) {
                if (isConfirm) {
                    window.location.href = "Adminn/adminHome.aspx";
                }
            });
        }

        //lecturer success login alert message
        function successLecturerAlert() {
            swal({
                title: "Success!",
                text: "Welcome!",
                type: "success",
                confirmButtonText: "OK"
            },
                function (isConfirm) {
                    if (isConfirm) {
                        window.location.href = "Lecturerr/lecturerHome.aspx";
                    }
                });
        }

        //student success login alert message
        function successStudentAlert() {
            swal({
                title: "Success!",
                text: "Welcome!",
                type: "success",
                confirmButtonText: "OK"
            },
                function (isConfirm) {
                    if (isConfirm) {
                        window.location.href = "Studentt/studentHome.aspx";
                    }
                });
        }

        //incorrect admin id or password
        function errorAdminAlert() {
            swal({
                title: "Error!",
                text: "Incorrect Admin ID or Password!",
                type: "error",
                confirmButtonText: "OK"
            },
                function (isConfirm) {
                    if (isConfirm) {
                        window.location.href = "login.aspx";
                    }
                });
        }

        //incorrect lecturer id or password
        function errorLecturerAlert() {
            swal({
                title: "Error!",
                text: "Incorrect Lecturer ID or Password!",
                type: "error",
                confirmButtonText: "OK"
            },
                function (isConfirm) {
                    if (isConfirm) {
                        window.location.href = "login.aspx";
                    }
                });
        }

        //incorrect student id or password
        function errorStudentAlert() {
            swal({
                title: "Error!",
                text: "Incorrect Student ID or Password!",
                type: "error",
                confirmButtonText: "OK"
            },
                function (isConfirm) {
                    if (isConfirm) {
                        window.location.href = "login.aspx";
                    }
                });
        }

        //no enter id and password
        function errorNoIDPasswordAlert() {
            swal({
                title: "Warning!",
                text: "Please fill in School ID or Password!",
                type: "error",
                confirmButtonText: "OK"
            },
                function (isConfirm) {
                    if (isConfirm) {
                        window.location.href = "login.aspx";
                    }
                });
        }

        //no choose any radio box
        function errorNoRadioBox() {
            swal({
                title: "Warning!",
                text: "Please select Admin or Lecturer or Student to login!",
                type: "error",
                confirmButtonText: "OK"
            },
                function (isConfirm) {
                    if (isConfirm) {
                        window.location.href = "login.aspx";
                    }
                });
        }
    </script>
</head>

<body class="bg-primary">
    <div id="layoutAuthentication">
        <div id="layoutAuthentication_content">
            <form id="form1" runat="server">
                <div>
                    <div class="container">
                        <div class="row justify-content-center">
                            <div class="col-lg-5">
                                <div class="card shadow-lg border-0 rounded-lg mt-5">
                                    <div class="card-header"><h3 class="text-center font-weight-light my-4">Login</h3></div>                                        <div class="card-body">
                                            <div class="form-group">
                                                <label class="small mb-1" for="inputSchoolID">School ID</label>
                                                <asp:TextBox ID="TxtID" runat="server" class="form-control py-4" placeholder="Enter school ID"></asp:TextBox>
                                            </div>
                                            <div class="form-group">
                                                <label class="small mb-1" for="inputPassword">Password</label>
                                                <asp:TextBox ID="TxtPassword" runat="server" class="form-control py-4" TextMode="Password" placeholder="Enter password"></asp:TextBox>
                                            </div>

                                            <div class="form-group">
                                                <asp:Table ID="Table1" runat="server" Width="100%">
                                                    <asp:TableRow>
                                                        <asp:TableCell>
                                                            <asp:RadioButton class="radio" ID="RadioButton1" runat="server" Text="Admin" GroupName="identity" /> 
                                                        </asp:TableCell>

                                                        <asp:TableCell>
                                                            <asp:RadioButton class="radio" ID="RadioButton2" runat="server" Text="Lecturer" GroupName="identity" />
                                                        </asp:TableCell>

                                                        <asp:TableCell>
                                                            <asp:RadioButton class="radio" ID="RadioButton3" runat="server" Text="Student" GroupName="identity" />
                                                        </asp:TableCell>

                                                    </asp:TableRow>
                                                </asp:Table>
                                            </div>

                                            <div class="form-group d-flex align-items-center justify-content-between mt-4 mb-0">
                                                <asp:CheckBox class="radio" ID="checkbox" runat="server" Text="Remember Me" /><br/>
                                                <asp:Button class="btn btn-primary" ID="Button1" runat="server" Text="Log In" onclick="Button1_Click" style="margin-right:-100px"/><br /><br />
                                            </div>

                                            <div class="form-group">
                                                <asp:Label ID="lblAttention" runat="server" CssClass="Note" ForeColor="Red" Font-Bold="True"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                </div>
            </form>
        </div>
        <%-- Footer --%>
        <div id="layoutAuthentication_footer">
            <footer class="py-4 bg-light mt-auto">
                <div class="container-fluid">
                    <div class="d-flex align-items-center justify-content-between small">
                        <div class="text-muted">Copyright &copy; HoldDoc 2020</div>
                        <div>
                            <a href="#">Privacy Policy</a>
                            &middot;
                            <a href="#">Terms &amp; Conditions</a>
                        </div>
                    </div>
                </div>
            </footer>
        </div>
    </div>

    <script src="https://code.jquery.com/jquery-3.5.1.min.js"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.5.0/js/bootstrap.bundle.min.js"></script>
    <script src="js/scripts.js"></script>
</body>
</html>
