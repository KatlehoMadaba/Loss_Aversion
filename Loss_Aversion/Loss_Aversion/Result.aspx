<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Result.aspx.cs" Inherits="Loss_Aversion.assets.WebForm9" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <section id="hero">
        <div class="hero-container">
            <h1 data-aos="zoom-in" class="boldHeader">Game Over !</h1>
         <h1 data-aos="zoom-in" class="boldHeader"> Losss Aversion Game</h1>
            <div class="card" style="width: 60%;  box-shadow: 0 0 20px #00FF00" data-aos="fade-up">
                <div class="card-header">
                    <p class="fw-bold fs-2">Your Results:</p>
                    <asp:Label ID="lblResults" class="fw-bold fs-2" runat="server"></asp:Label>
                </div>
            </div>
            <div style="margin-top:50px;">
                <asp:Button ID="btnTryAgain" runat="server" CssClass="btn btn-success btn-Gains" Text="Try Again" Width="212px" Style="margin-left: 20px" OnClick="btnTryAgain_Click" />
            </div>
        </div>

    </section>
</asp:Content>
