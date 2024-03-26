<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="Loss_Aversion.WebForm1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <body>
        <!-- ======= Hero Section ======= -->
        <section id="hero">
            <div class="hero-container">
                <a href="index.html" class="hero-logo" data-aos="zoom-in">
                    <img src="assets/img/hero-logo.png" alt="herePng"></a>
                <h1 data-aos="zoom-in" class="boldHeader">Welcome To </h1>
                <h1 data-aos="zoom-in" class="boldHeader"> Loss Aversion Game</h1>
                <h2 data-aos="fade-up" class="glowHeading">Get for the Loss Aversion Game!</h2>
                 <div class="card" style="width: 60%;">
                <div class="card-header" style="box-shadow: 0 0 20px #00FF00; /* Glowing effect */">
                    <div class="fs-3 fw-bold">
                    <h2 id="typed-title"></h2>
                    <p id="typed-text"></p>
                    </div>
                 <%--   <p class="fw-bold fs-2" >There are rumors circulating about a potential merger involving a company you've invested in.</p>--%>
                </div>
            </div>
                <asp:Button ID="btnNext" runat="server" OnClick="btnNext_Click1" data-aos="fade-up" data-aos-delay="200"  CssClass="btn btn-success btn-Alosses" Width="212px" Text="Get Started" style="margin-top:25px" />
            </div>
            <a href="#" class="back-to-top d-flex align-items-center justify-content-center"><i class="bi bi-arrow-up-short"></i></a>
        </section>
        <!-- Vendor JS Files -->
    </body>
</asp:Content>
