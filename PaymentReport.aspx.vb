Imports System.Data.SqlClient

Public Class PaymentReport
    Inherits System.Web.UI.Page


    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Me.IsPostBack Then
            'Populating a DataTable from database.
            Dim dt As DataTable = Me.GetData()

            'Building an HTML string.
            Dim html As New StringBuilder()
            btnClear.Enabled = False

            If dt.Rows.Count > 0 Then
                btnClear.Enabled = True

                'Table start.
                html.Append("<table border = '1'>")

                'Building the Header row.
                html.Append("<tr>")
                For Each column As DataColumn In dt.Columns
                    html.Append("<th>")
                    html.Append(column.ColumnName)
                    html.Append("</th>")
                Next
                html.Append("</tr>")

                'Building the Data rows.
                Dim sDateonly As String = ""
                For Each row As DataRow In dt.Rows
                    html.Append("<tr>")
                    For Each column As DataColumn In dt.Columns
                        html.Append("<td>")
                        sDateonly = row(column.ColumnName).ToString()
                        If sDateonly.Length > 15 Then
                            sDateonly = sDateonly.Substring(0, sDateonly.Length - 11)
                        End If
                        html.Append(sDateonly)
                        html.Append("</td>")
                    Next
                    html.Append("</tr>")
                Next

                'Table end.
                html.Append("</table>")


            Else
                html.Append("<br/> <br/><br>Currently there is no data for the Report </br>")

            End If
            'Append the HTML string to Placeholder.
            PlaceHolder1.Controls.Add(New Literal() With {
      .Text = html.ToString()
    })
        End If
    End Sub

    Private Function GetData() As DataTable

        Dim constr As String = "Data Source=tcp:arogyaportal20230506112748dbserver.database.windows.net,1433;Initial Catalog=ArogyaPortal20230506112748_db;User Id=Harshil@arogyaportal20230506112748dbserver;Password=Test_12345678"
        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand("Select * from HCollection")
                Using sda As New SqlDataAdapter()
                    cmd.Connection = con
                    sda.SelectCommand = cmd
                    Using dt As New DataTable()
                        sda.Fill(dt)
                        Return dt
                    End Using
                End Using
            End Using
        End Using
    End Function
    Private Sub ClearData()

        Dim constr As String = "Data Source=tcp:arogyaportal20230506112748dbserver.database.windows.net,1433;Initial Catalog=ArogyaPortal20230506112748_db;User Id=Harshil@arogyaportal20230506112748dbserver;Password=Test_12345678"

        Using conN As New SqlConnection(constr)
            Try
                Using cmd As New SqlCommand("Select * from HCollection")
                    Using sda As New SqlDataAdapter()
                        cmd.Connection = conN
                        sda.SelectCommand = cmd
                        Using dt As New DataTable()
                            sda.Fill(dt)
                            If dt.Rows.Count > 10 Then
                                Using cmdN As New SqlCommand("Delete from HCollection where Amount in (SELECT TOP 50 PERCENT Amount FROM HCollection)")
                                    conN.Open()

                                    With cmdN
                                        .Connection = conN

                                        Dim Result As Integer = 0
                                        Result = Convert.ToInt32(.ExecuteNonQuery())
                                    End With

                                End Using
                            End If

                        End Using
                    End Using
                End Using
            Finally
                conN.Close()
            End Try

        End Using
    End Sub

    Protected Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        ClearData()
    End Sub
End Class