<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="Loss_Aversion.WebForm1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <body>
        <!-- ======= Hero Section ======= -->
        <section id="hero">
            <div id="hero-container">
                <a href="index.html" class="hero-logo" data-aos="zoom-in">
                    <img src="assets/img/hero-logo.png" alt="herePng"></a>
                <h1 data-aos="zoom-in" class="boldHeader">Welcome To </h1>
                <h1 data-aos="zoom-in" class="boldHeader"> Loss Aversion Game</h1>
                <h2 data-aos="fade-up" class="glowHeading fs-4">Get ready for the Loss Aversion Game!</h2>
                <div class="card">
                    <div class="card-header">
                        <div class="fs-3 fw-bold">
                            <h2 id="typed-title"></h2>
                            <p style="font-size: 15px"  id="typed-text"></p>
                        </div>
                    </div>
                </div>
                <asp:Button ID="btnNext" runat="server" OnClick="btnNext_Click1" data-aos="fade-up" data-aos-delay="200" CssClass="btn btn-success btn-Alosses" Width="212px" Text="Get Started" style="margin-top:25px" />
            </div>
            <a href="#" class="back-to-top d-flex align-items-center justify-content-center"><i class="bi bi-arrow-up-short"></i></a>
        </section>
        <!-- Vendor JS Files -->

        <!-- Additional styles/scripts for scroll-to-top button, if any -->
    </body>
</asp:Content>
