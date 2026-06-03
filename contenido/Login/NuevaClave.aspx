<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NuevaClave.aspx.cs" Inherits="contenido_Login_NuevaClave" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css" integrity="sha384-Gn5384xqQ1aoWXA+058RXPxPg6fy4IWvTNh0E263XmFcJlSAwiGgFAW/dAiS6JXm" crossorigin="anonymous">
    <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.3.1/css/all.css" integrity="sha384-mzrmE5qonljUremFsqc01SB46JvROS7bZs3IO2EmfFsd15uHvIt+Y8vEf7N7fWAU" crossorigin="anonymous">
    <style type="text/css">
        /* Made with love by Mutiullah Samim*/

@import url('https://fonts.googleapis.com/css?family=Numans');

html,body{
background-image: url('../../imagenes/Frontis-HCSBA.jpg');
background-size: cover;
background-repeat: no-repeat;
height: 100%;
font-family: 'Numans', sans-serif;
}

.container{
height: 100%;
align-content: center;
}

.card{
height: 370px;
margin-top: auto;
margin-bottom: auto;
width: 400px;
background-color: rgba(0,0,0,0.5) !important;
}

.social_icon span{
font-size: 60px;
margin-left: 10px;
color: #FFC312;
}

.social_icon span:hover{
color: white;
cursor: pointer;
}

.card-header h3{
color: white;
}

.social_icon{
position: absolute;
right: 20px;
top: -45px;
}

.input-group-prepend span{
width: 50px;
background-color: #FFC312;
color: black;
border:0 !important;
}

input:focus{
outline: 0 0 0 0  !important;
box-shadow: 0 0 0 0 !important;

}

        .remember {
            color: white;
        }

.remember input
{
width: 20px;
height: 20px;
margin-left: 15px;
margin-right: 5px;
}

.login_btn{
color: black;
background-color: #FFC312;
width: 100px;
}

.login_btn:hover{
color: black;
background-color: white;
}

.links{
color: white;
}

.links a{
margin-left: 4px;
}
    </style>
    <script language="JavaScript">
        //javascript:window.history.forward(1); //Esto es para cuando le pulse al botón de Atrás
        //javascript:window.history.back(1); //Esto para cuando le pulse al botón de Adelante
</script>
</head>
<body>
    <div class="container">
        <div class="d-flex justify-content-center h-100">
            <div class="card">
                <div class="card-header">
				    <h3>Cambio de Clave</h3>
                    <asp:Label ID="lblTit" runat="server" CssClass="cssTitSys" Style="z-index: 100; left: 246px; position: absolute; top: 40px;" Width="614px"></asp:Label>
                </div>
                <div class="card-body">
				<form id="form1" runat="server">
					<div class="input-group form-group" style="box-sizing:border-box;">
						<div class="input-group-prepend">
							<span class="input-group-text"><i class="fas fa-key"></i></span>
						</div>
                        <asp:TextBox ID="Told" runat="server" TextMode="Password" class="form-control" placeholder="clave antigua"></asp:TextBox>
					</div>
					<div class="input-group form-group" style="box-sizing:border-box;">
						<div class="input-group-prepend">
							<span class="input-group-text"><i class="fas fa-key"></i></span>
						</div>
                        <asp:TextBox ID="TNew" runat="server" TextMode="Password" class="form-control" placeholder="clave nueva"></asp:TextBox>
					</div>
                    <div class="input-group form-group" style="box-sizing:border-box;">
						<div class="input-group-prepend">
							<span class="input-group-text"><i class="fas fa-key"></i></span>
						</div>
                        <asp:TextBox ID="TRNew" runat="server" TextMode="Password" class="form-control" placeholder="repita clave nueva"></asp:TextBox>
					</div>
					<div class="form-group">
                        <asp:Button ID="Ingresar" runat="server" Text="Cambiar" class="btn float-right login_btn" OnClick="Ingresar_Click" />
					</div>
				</form>
			</div>
            </div>
        </div>
    </div>

<%--    <script src="../js/jquery/jquery-3.4.1.min.js" integrity="sha384-J6qa4849blE2+poT4WnyKhv5vZF5SrPo0iEjwBvKU7imGFAV0wwj1yYfoRSJoZ+n" crossorigin="anonymous"></script>
    <script src="../js/popper/popper.min.js" integrity="sha384-Q6E9RHvbIyZFJoft+2mJbHaEWldlvI9IOYy5n3zV9zzTtmI3UksdQRVvoxMfooAo" crossorigin="anonymous"></script>
    <script src="../js/bootstrap/bootstrap.min.js" integrity="sha384-wfSDF2E50Y2D1uUdj0O3uMBJnjuUD4Ih7YwaYd1iqfktj0Uod8GCExl3Og8ifwB6" crossorigin="anonymous"></script>--%>

    </body>
</html>