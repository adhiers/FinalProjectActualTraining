<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ConsultationPage.aspx.cs" Inherits="FinalProject.WebForm.ConsultationPage" Async="true"%>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        <h3>Consultation Table</h3>
        <asp:Button ID="btnShowInsertForm" runat="server" Text="Add Consultation" CssClass="btn btn-success mb-3" OnClick="btnShowInsertForm_Click" />
        <asp:Panel ID="pnlInsertConsultation" runat="server" Visible="false" CssClass="mb-4">
            <h4>Insert Consultation</h4>
            <asp:TextBox ID="txtConsultId" runat="server" placeholder="Consultation ID" CssClass="form-control mb-2" />
            <asp:TextBox ID="txtScheduleId" runat="server" placeholder="Schedule ID" CssClass="form-control mb-2" />
            <asp:TextBox ID="txtSpid" runat="server" placeholder="Salesperson ID" CssClass="form-control mb-2" />
            <asp:TextBox ID="txtCustomerBudget" runat="server" placeholder="Customer Budget" CssClass="form-control mb-2" />
            <asp:TextBox ID="txtConsultDate" runat="server" placeholder="Consult Date (yyyy-MM-dd)" CssClass="form-control mb-2" />
            <asp:TextBox ID="txtNote" runat="server" placeholder="Note" CssClass="form-control mb-2" TextMode="MultiLine" Rows="3" />
            <%--<textarea id="txtNote" placeholder="Note" runat="server" cols="20" rows="3"></textarea>--%>
            <asp:Button ID="btnInsertConsultation" runat="server" Text="Insert" CssClass="btn btn-primary" OnClick="btnInsertConsultation_Click" />
            <asp:Button ID="btnUpdateConsultation" runat="server" Text="Update" CssClass="btn btn-warning" OnClick="btnUpdateConsultation_Click" Visible="false" />
            <asp:Button ID="btnCancelInsert" runat="server" Text="Cancel" CssClass="btn btn-secondary" OnClick="btnCancelInsert_Click" />
        </asp:Panel>
        <asp:GridView ID="gvConsultations" runat="server" AutoGenerateColumns="False" CssClass="table table-striped" OnRowCommand="gvConsultations_RowCommand">
            <Columns>
                
                <asp:BoundField DataField="ConsultId" HeaderText="Consultation ID" />
                <asp:BoundField DataField="ScheduleId" HeaderText="Schedule ID" />
                <asp:BoundField DataField="Spid" HeaderText="Salesperson ID" />
                <asp:BoundField DataField="CustomerBudget" HeaderText="Customer Budget" />
                <asp:BoundField DataField="ConsultDate" HeaderText="Consult Date" DataFormatString="{0:yyyy-MM-dd}" />
                <asp:BoundField DataField="Note" HeaderText="Note" />
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Button ID="btnUpdate" runat="server" Text="Update" CommandName="UpdateConsultation" CommandArgument='<%# Eval("ConsultId") %>' CssClass="btn btn-warning btn-sm mr-2" />
                        <asp:Button ID="btnDelete" runat="server" Text="Delete" CommandName="DeleteConsultation" CommandArgument='<%# Eval("ConsultId") %>' CssClass="btn btn-danger btn-sm" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </main>
</asp:Content>
