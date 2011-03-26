<%@ Page Language="C#" Inherits="WebUI.Default" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
<html>
<head runat="server">
	<title>Default</title>
</head>
<body>
	<form id="form1" runat="server">
		<fieldset>
			<legend>JIRA Login</legend>
			<ul>
				<li>
					<asp:Label AssociatedControlID="">User Name</asp:Label>
					<asp:TextBox ID="tbUserName" runat="server" />
				</li>
				<li>
					<asp:Label AssociatedControlID="">Password</asp:Label>
					<asp:TextBox TextMode="Password" ID="tbPassword" runat="server" />
				</li>
				<li>
					<asp:Label AssociatedControlID="">Project Code (3-6 characters)</asp:Label>
					<asp:TextBox ID="tbProjectCode" MaxLength="6" MinLength="3" runat="server" />
				</li>								
			</ul>
		</fieldset>
	
		<asp:Button ID="button1" runat="server" Text="Create Test Issue" OnClick="button1_Clicked" />
	</form>
</body>
</html>
