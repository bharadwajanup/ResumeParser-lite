<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <div class="form-group">
        
        <asp:TextBox TextMode="MultiLine" Rows="10" ID="resume" runat="server" CssClass="form-control" />

    </div>    
    <div class="form-group">
        
        <asp:Button runat="server" OnClick="Resume_Click"  Text="Parse" Font-Size="Large" CssClass="form-control btn btn-danger" />
        <asp:Label runat="server" ID="message" CssClass="control-label">Output JSON appears here</asp:Label>
    </div>
    </div>
    </form>
</body>
</html>
