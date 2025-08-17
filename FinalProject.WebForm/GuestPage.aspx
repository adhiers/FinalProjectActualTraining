<%@ Page Title="Guest Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GuestPage.aspx.cs" Inherits="FinalProject.WebForm.GuestPage" Async="true"%>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        <section class="row" aria-labelledby="aspnetTitle">
            <h3 id="aspnetTitle">List Of Guests</h3>
        </section>
        <br />
        <div class="col-md-6">
            <asp:DataGrid ID="gvGuests" runat="server" CssClass="table table-striped" AutoGenerateColumns="false">
                <Columns>
                    <asp:BoundColumn DataField="GuestId" HeaderText="Guest ID" />
                    <asp:BoundColumn DataField="GuestName" HeaderText="Name" />
                    <asp:BoundColumn DataField="Email" HeaderText="Email" />
                    <asp:BoundColumn DataField="PhoneNumber" HeaderText="Phone Number"/>
                    <%--<asp:TemplateColumn>
                        <ItemTemplate>
                            <a class="btn btn-primary btn-sm" runat="server" href='<%# "GuestPage.aspx?CarId=" + Eval("GuestId") %>'>select</a>
                        </ItemTemplate>
                    </asp:TemplateColumn>--%>
                </Columns>
                <PagerStyle CssClass="pagination justify-content-center" />
            </asp:DataGrid>
        </div>
    </main>
</asp:Content>
