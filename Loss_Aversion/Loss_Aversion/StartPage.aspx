<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="StartPage.aspx.cs" Inherits="Loss_Aversion.StartPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section id="hero" >
        <div id="divStart" class="hero-container" runat="server">
            <div class="hero-container">
         <h1 data-aos="zoom-in" class="boldHeader">Press Start to begin</h1>
                <asp:Button ID="btnstart" runat="server" CssClass="btn btn-success btn-Gains" Text="Start" Width="212px" style="margin-left:20px" OnClick="btnstart_Click" />
        </div>
            </div>
</section>
</asp:Content>
