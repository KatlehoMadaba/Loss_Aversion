<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="WebForm5.aspx.cs" Inherits="Loss_Aversion.WebForm5" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <section id="hero">
        <div class="hero-container">
            <div class="card" style="width: 60%;">
                <div class="card-header">
                    <p class="fw-bold fs-2">There are rumors circulating about a potential merger involving a company you've invested in.</p>
                </div>
                <div class="card-body">
                    <asp:Button ID="btnAlosses" runat="server" CssClass="btn btn-success btn-Alosses" Text="Avoid Losses" Width="212px" />
                    <asp:Button ID="btnGains" runat="server" CssClass="btn btn-success btn-Gains" Text="Gains" Width="212px" />
                </div>
            </div>
        </div>
    </section>

</asp:Content>
