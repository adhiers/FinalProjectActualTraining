<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="FinalProject.WebForm._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <main>
        <section class="text-center mb-5" aria-labelledby="aspnetTitle">
            <h1 id="aspnetTitle">Welcome Dealer</h1>
            <p class="lead">What would like to check or manage today?</p>
            <%--<p><a href="http://www.asp.net" class="btn btn-primary btn-md">Learn more &raquo;</a></p>--%>
        </section>

        <div class="row">
            <section class="col-md-3" aria-labelledby="ttlGuest">
                <h2 id="ttlGuest">Guest</h2>
                <p>
                    lorem15
                </p>
                <p>
                    <a class="btn btn-primary btn-sm" href="GuestPage.aspx">Table &raquo;</a>
                </p>
            </section>
            <section class="col-md-3" aria-labelledby="ttlSchedule">
                <h2 id="ttlSchedule">Schedule</h2>
                <%--<p>
                    NuGet is a free Visual Studio extension that makes it easy to add, remove, and update libraries and tools in Visual Studio projects.
                </p>--%>
                <p>
                    <a class="btn btn-primary btn-sm" href="https://go.microsoft.com/fwlink/?LinkId=301949">Table &raquo;</a>
                </p>
            </section>
            <section class="col-md-3" aria-labelledby="ttlConsult">
                <h2 id="ttlConsult">Consultation</h2>
                <%--<p>
                    You can easily find a web hosting company that offers the right mix of features and price for your applications.
                </p>--%>
                <p>
                    <a class="btn btn-primary btn-sm" href="https://go.microsoft.com/fwlink/?LinkId=301950">Table &raquo;</a>
                </p>
            </section>
            <section class="col-md-3" aria-labelledby="ttlTestDrive">
                <h2 id="ttlTestDrive">Test Drive</h2>
                <%--<p>s
                    You can easily find a web hosting company that offers the right mix of features and price for your applications.
                </p>--%>
                <p>
                    <a class="btn btn-primary btn-sm" href="https://go.microsoft.com/fwlink/?LinkId=301950">Table &raquo;</a>
                </p>
            </section>
        </div>
    </main>

</asp:Content>
