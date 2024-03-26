<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="WebForm4.aspx.cs" Inherits="Loss_Aversion.WebForm4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <section id="hero">
        <div class="hero-container">
            <div class="card" style="width: 60%;">
                <div class="card-header">
                    <p class="fw-bold fs-2">One of the companies in your portfolio is about to release its earnings report, and there are mixed predictions.</p>
                </div>
                <div class="card-body">
                    <asp:Button ID="btnAlosses" runat="server" CssClass="btn btn-success btn-Alosses" Text="Avoid Losses" Width="212px" />
                    <asp:Button ID="btnGains" runat="server" CssClass="btn btn-success btn-Gains" Text="Gains" Width="212px" />
                </div>
            </div>
        </div>
    </section>

</asp:Content>
