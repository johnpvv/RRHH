<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="contenido_Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css" integrity="sha384-Gn5384xqQ1aoWXA+058RXPxPg6fy4IWvTNh0E263XmFcJlSAwiGgFAW/dAiS6JXm" crossorigin="anonymous">
    <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.3.1/css/all.css" integrity="sha384-mzrmE5qonljUremFsqc01SB46JvROS7bZs3IO2EmfFsd15uHvIt+Y8vEf7N7fWAU" crossorigin="anonymous">
    <style type="text/css">
        /* Made with love by Mutiullah Samim*/

        @import url('https://fonts.googleapis.com/css?family=Numans');

        html, body {
            background-image: url('../imagenes/Frontis-HCSBA.jpg');
            background-size: cover;
            background-repeat: no-repeat;
            height: 100%;
            font-family: 'Numans', sans-serif;
        }

        .container {
            height: 100%;
            align-content: center;
        }

        .card {
            height: 380px;
            margin-top: auto;
            margin-bottom: auto;
            width: 450px;
            background-color: rgba(0,0,0,0.5) !important;
        }

        .social_icon span {
            font-size: 60px;
            margin-left: 10px;
            color: #FFC312;
        }

            .social_icon span:hover {
                color: white;
                cursor: pointer;
            }

        .card-header h3 {
            color: white;
        }

        .social_icon {
            position: absolute;
            right: 20px;
            top: -45px;
        }

        .input-group-prepend span {
            width: 50px;
            background-color: #FFC312;
            color: black;
            border: 0 !important;
        }

        input:focus {
            outline: 0 0 0 0 !important;
            box-shadow: 0 0 0 0 !important;
        }

        .remember {
            color: white;
        }

            .remember input {
                width: 20px;
                height: 20px;
                margin-left: 15px;
                margin-right: 5px;
            }

        .login_btn {
            color: black;
            background-color: #FFC312;
            width: 100px;
        }

            .login_btn:hover {
                color: black;
                background-color: white;
            }

        .links {
            color: white;
        }

            .links a {
                margin-left: 4px;
            }
    </style>
</head>
<body>
    <div class="container">
        <div class="d-flex justify-content-center h-100">
            <div class="card">
                <div class="card-header">                    
                    <h3><asp:Image ID="caps" ImageUrl="~/imagenes/user_white.png" runat="server" Width="30px" Height="24px" />Login RR.HH.</h3>
                </div>
                <div class="card-body">
                    <form id="form1" runat="server">
                        <div class="input-group form-group" style="box-sizing: border-box;">
                            <div class="input-group-prepend">
                                <span class="input-group-text"><i class="fas fa-user"></i></span>
                            </div>
                            <asp:TextBox ID="txtUser" runat="server" class="form-control" placeholder="usuario" autocomplete="off" MaxLength="8"></asp:TextBox>

                        </div>
                        <div class="input-group form-group" style="box-sizing: border-box;">
                            <div class="input-group-prepend">
                                <span class="input-group-text"><i class="fas fa-key"></i></span>
                            </div>
                            <asp:TextBox ID="txtClave" runat="server" TextMode="Password" class="form-control" placeholder="clave"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <asp:Button ID="Ingresar" runat="server" Text="Ingresar" class="btn float-right login_btn" OnClick="Ingresar_Click" />
                        </div>
                        <br />
                        <div class="form-group" style="color: white; font-size: 15px">
                        </div>
                    </form>
                </div>
                <div style="color: white">En caso de bloqueo de claves, favor informar al correo: rrHH@gmail.com</div>
            </div>
        </div>
    </div>

</body>
</html>
