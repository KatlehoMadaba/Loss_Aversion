<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="Loss_Aversion.WebForm8" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <section id="hero">
        <div class="hero-container">
         <h1 data-aos="zoom-in" class="boldHeader">Lets Begin</h1>
            <div class="card" style="width: 60%;  box-shadow: 0 0 20px #00FF00" data-aos="fade-up">
                <div class="card-header">
                    <p class="fw-bold fs-2">Please Enter Your Name: </p>
                     <asp:TextBox ID="txtname" runat="server"  CssClass="line-input"></asp:TextBox>
                </div>
                <div class="card-body" data-aos="fade-up">
                    <asp:Button ID="btnNext" runat="server" CssClass="btn btn-success btn-Gains" Text="Next" Width="212px" style="margin-left:20px" Onclick="btnNext_Click"/>
                </div>
            </div>
        </div>
    </section>
</asp:Content>
