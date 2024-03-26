<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="WebForm6.aspx.cs" Inherits="Loss_Aversion.WebForm6" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <section id="hero">
        <div class="hero-container">
          <h1 data-aos="zoom-in" class="boldHeader"> Aversion Game</h1>
            <div class="card" style="width: 60%;  box-shadow: 0 0 20px #00FF00" data-aos="fade-up">
                <div class="card-header">
                    <p class="fw-bold fs-2">The global economic situation is uncertain, impacting stock markets worldwide.</p>
                </div>
                <div class="card-body" data-aos="fade-up">
                    <asp:Button ID="btnAlosses" runat="server" CssClass="btn btn-success btn-Alosses" Text="Avoid Losses" Width="212px" OnClick="btnAlosses_Click" />
                    <asp:Button ID="btnGains" runat="server" CssClass="btn btn-success btn-Gains" Text="Gains" Width="212px"  OnClick="btnGains_Click"/>
                </div>
            </div>
        </div>
    </section>

</asp:Content>
