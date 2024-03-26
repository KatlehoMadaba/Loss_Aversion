<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="Loss_Aversion.WebForm1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <body>
        <!-- ======= Hero Section ======= -->
        <section id="hero">
            <div class="hero-container">
                <a href="index.html" class="hero-logo" data-aos="zoom-in">
                    <img src="assets/img/hero-logo.png" alt=""></a>
                <h1 data-aos="zoom-in" class="boldHeader">Welcome To </h1>
                <h1 data-aos="zoom-in" class="boldHeader"> Aversion Game</h1>
                <h2 data-aos="fade-up" class="glowHeading">Get for the Loss Aversion Game!</h2>
                <div class="display-box game-display">
                    <h2 id="typed-title"></h2>
                    <p id="typed-text">
                    </p>
                </div>
                <asp:Button ID="btnNext" runat="server" OnClick="btnNext_Click1" data-aos="fade-up" data-aos-delay="200" CssClass="btn-get-started scrollto" Text="Get Started" />
            </div>
            <a href="#" class="back-to-top d-flex align-items-center justify-content-center"><i class="bi bi-arrow-up-short"></i></a>
        </section>
        <!-- Vendor JS Files -->
    </body>
</asp:Content>
