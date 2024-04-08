<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Question1.aspx.cs" Inherits="Loss_Aversion.Question1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <section id="hero">
        <div class="hero-container">
         <h1 data-aos="zoom-in" class="boldHeader">Loss Aversion Game</h1>
            <h2 data-aos="fade-up" class="glowHeading fs-4">Balance: R<asp:Label ID="lblBettedAmount" runat="server"/></h2>
            <h2 data-aos="fade-up" class="glowHeading fs-4"> Potential Loss: R<asp:Label ID="lblPotentialLoss" runat="server" /></h2>
             <h2 data-aos="fade-up" class="glowHeading fs-4"> Potential Win: R<asp:Label ID="lblPotentialGain" runat="server" /></h2>
            <div class="card" style="width: 60%; box-shadow: 0 0 20px #00FF00" data-aos="fade-up" >

                <div class="card-header">
                    <asp:Label ID="lblQuestions" runat="server" CssClass="fw-bold fs-2" ></asp:Label>
                </div>
                <div class="card-body" style="display: flex;
    flex-direction: column;
    align-items: center;" data-aos="fade-up">

                    <asp:Button ID="btnAlosses" runat="server" style="margin-top:1rem;" CssClass="btn btn-success btn-Alosses" Text="Avoid Losses" Width="212px" OnClick="btnAlosses_Click" />
                    <asp:Button ID="btnGains" runat="server" CssClass="btn btn-success" style="margin-top:1rem;" Text="Gamble" Width="212px" OnClick="btnGains_Click"/>
                </div>
            </div>
        </div>
    </section>

</asp:Content>

