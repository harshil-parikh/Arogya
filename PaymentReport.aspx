<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PaymentReport.aspx.vb" Inherits="ArogyaPortal.PaymentReport" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>PaymentReport</title>
</head>
<body>
    <form id="form1" runat="server">
      <div>
        
        <h1 style="border:orange; border-width:5px; border-style:solid;">
         
            <br />
        
            <a href="ArogyaHomePage.html" class="visually-hidden focusable">
        Back to Home Page
            </a>
            <br />
       </h1> 
            <asp:Button ID="btnClear" runat="server" Text="Clear Data" />
          <br />
          <br />
            <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
       
            <br />
            <br />
            <asp:SqlDataSource ID="PaymentData" runat="server" ></asp:SqlDataSource>
            <br />
            
            <br />
       
             
        </div>
    </form>
</body>
</html>
