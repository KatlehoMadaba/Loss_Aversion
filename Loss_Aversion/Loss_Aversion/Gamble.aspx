<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Gamble.aspx.cs" Inherits="Loss_Aversion.WebForm2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
    <style>
        /* Customize the SweetAlert popup */
        .swal2-popup {
         /*    font-family: 'Press Start 2P', cursive; */
          /* font-family: "Rubik Dirt" , system-ui; */ 
            font-family:Arial; /*I really cant decide on a font :(  */
            font-weight:bold;
            background-color: black; /* Background color */
            border: 2px solid #4CAF50; /* Border color */
            border-radius: 10px;
            box-shadow: 0 0 10px #4CAF50;    /* Green glow effect , maybe this one is better idunno*/ 
          /*  box-shadow: 0 0 20px #00FF00;  Glowing effect */
            color: white; /* Text color */
        }

        /* Customize the title */
        .swal2-title {
         /*   color: #4CAF50;  Title color */
            color: white;
      /*font-family: "Rubik Dirt" , system-ui;  */  
        font-family:Arial;
            font-weight:bold;
           /*   font-family: "Press Start 2P"; */
        }

        /* Customize the text */
        .swal2-content {
            color: white; /* Text color */
        }

        /* Customize the buttons */
        .swal2-actions button {
            background-color: #4CAF50; /* Button background color */
            color: white; /* Button text color */
            border: none;
            border-radius: 5px;
            padding: 10px 20px;
            margin: 5px;
            cursor: pointer;
        }

        /* Hover effect for buttons */
        .swal2-actions button:hover {
            background-color: #45a049;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server" Visible="false">
    
    <section id="hero" >
        <div id="divStart" class="hero-container" runat="server">
            <div class="hero-container">
         <h1 data-aos="zoom-in" class="boldHeader">Press Start to begin</h1>
            <asp:Button ID="btnstart" runat="server" CssClass="btn btn-success btn-Gains" Text="Start" Width="212px" style="margin-left:20px" OnClick="btnstart_Click" />
        </div>
        </div>
        <div class="hero-container" id="divGame" runat="server">
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
                    <asp:Button ID="btnAlosses" runat="server" style="margin-top:1rem;" CssClass="btn btn-success btn-Alosses" Text="Avoid Losses" Width="212px" OnClick="btnAlosses_Click" OnClientClick="showLpopup() return false;"/>
                    <asp:Button ID="btnGains" runat="server" CssClass="btn btn-success" style="margin-top:1rem;" Text="Gamble" Width="212px" OnClick="btnGains_Click" OnClientClick="showGpopup() return false;"/>
                </div>
            </div>
        </div>
    </section>
     <script>
         function avoidLossPopup(won,amount) {
             let title = "";
             if (won) {
                 title = "You could have won:R" + amount;
             }
             else {
                 title = "Great choice you avoided a loss of:R" + amount;
             }
             Swal.fire({
                 title: title,
                 imageUrl: "assets/img/hero-logo.png",
                 imageWidth: 100,
                 imageHeight: 100,
                 imageAlt: "LV logo",
                 timer: 2000,
                 width: 600,
                 padding: "3em",
                 color: "#ffffff",
                 background: "#000000 url(assets/img/hero-bg.png)",
                 backdrop: 
                    `rgba(2,48,32,0.8)
                    url("/images/nyan-cat.gif")
                    left top
                    no-repeat`,
                 timerProgressBar: true,
                 didOpen: () => {
                     Swal.showLoading();
                     const timer = Swal.getPopup().querySelector("b");
                     timerInterval = setInterval(() => {
                         timer.textContent = `${Swal.getTimerLeft()}`;
                     }, 100);
                 },
                 willClose: () => {
                     clearInterval(timerInterval);
                 }

             }).then((result) => {
                 if (result.dismiss === Swal.DismissReason.timer && result.isConfirmed) {
                     window.location.href = 'Result.aspx'; // Redirect to the next page
                 }
             });
         }
         function GainsPopup(won, amount) {
             let title = "";
             if (won) {
                 title = "Opps you missed out on :R" + amount;
             }
             else {
                 title = "Great choice you almost lost :R" + amount;
             }
             Swal.fire({
                 title: title,
                 imageUrl: "assets/img/hero-logo.png",
                 imageWidth: 100,
                 imageHeight: 100,
                 imageAlt: "LV logo",
                 timer: 2000,
                 width: 600, 
                 padding: "3em",
                 color: "#ffffff",
                 background: "#000000 url(assets/img/hero-bg.png)",
                 backdrop: `
                   rgba(2,48,32,0.8)
                    url("/images/nyan-cat.gif")
                    left top
                    no-repeat
                     `,
                 timerProgressBar: true,
                 didOpen: () => {
                     Swal.showLoading();
                     const timer = Swal.getPopup().querySelector("b");
                     timerInterval = setInterval(() => {
                         timer.textContent = `${Swal.getTimerLeft()}`;
                     }, 100);
                 },
                 willClose: () => {
                     clearInterval(timerInterval);
                 }

             }).then((result) => {
                 if (result.dismiss === Swal.DismissReason.timer && result.isConfirmed) {                     
                     window.location.href = 'Result.aspx'; // Redirect to the next page
                 }
             }); 
         }
     </script>
</asp:Content>
