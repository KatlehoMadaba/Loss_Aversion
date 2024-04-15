<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="TestpAGE.aspx.cs" Inherits="Loss_Aversion.TestpAGE" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script> <!-- Reference SweetAlert2 from CDN -->
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form>
    </form>
   <asp:Button ID="btnTriggerAlert" runat="server" Text="Click Me" OnClientClick="showAlert(); return false;" />
    <script>
        function showAlert() {
            Swal.fire({
                title: 'Hello!',
                text: 'This is a SweetAlert2 alert!',
                icon: 'success',
                confirmButtonText: 'OK'
            });
        }
    </script>
</asp:Content>
