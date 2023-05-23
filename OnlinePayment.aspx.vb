Imports System.Data.SqlClient
Imports System.Net.Http

Public Class OnlinePayment
    Inherits System.Web.UI.Page
    Dim vDate As Date = Today().Date
    Dim vPatientCode As String = ""
    Dim vAmount As Integer = 0

    Dim sError As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        vDate = Today()
        vPatientCode = ""
        vAmount = 0
        sError = ""

        'ClearData()
    End Sub

    Protected Sub Pay_Click(sender As Object, e As EventArgs) Handles Pay.Click
        Dim myConn As SqlConnection
        Dim cmd As SqlCommand
        Dim sqlstring, DNAME, DEXP As String
        'Dim ddate As Date = Today.Date.Date()
        Dim ddate As String
        Dim vEx As Boolean = False
        Dim bPCError As Boolean = False
        Dim bCalError As Boolean = False
        Dim bPAError As Boolean = False

        If TextBox1.Text IsNot "" Then
            DNAME = TextBox1.Text
        Else
            bPCError = True
        End If
        If TextBox2.Text IsNot "" Then
            DEXP = TextBox2.Text
        Else
            bPAError = True
        End If
        ddate = Calendar1.SelectedDate.ToString()

        If (ddate = "01-01-0001 00:00:00") Or ddate Like "1/1/0001" Then
            bCalError = True
        End If
        If bPCError = True Or bCalError = True Or bPAError = True Then
            Dim responseMsg = New HttpResponseMessage
            responseMsg.Content = New StringContent("Custom error message")
            sError = "Please fill in all values"
            Response.Redirect("PaymentFailed.html", False)
            Exit Sub
            'Throw New Exception(sError)
        End If




        Try


            myConn = New SqlConnection("Data Source=tcp:arogyaportal20230506112748dbserver.database.windows.net,1433;Initial Catalog=ArogyaPortal20230506112748_db;User Id=Harshil@arogyaportal20230506112748dbserver;Password=Test_12345678")


            myConn.Open()

            sqlstring = "INSERT INTO HCollection (PatientCode,Amount,PayDate) VALUES ('" + DNAME + "','" + DEXP + "', '" + ddate + "')"

            cmd = New SqlCommand(sqlstring, myConn)

            cmd.ExecuteNonQuery()


            myConn.Close()

        Catch ex As Exception

            If ex.Message.Contains("Conversion failed when converting date and/or time from character string") Then
                vEx = False
            Else
                vEx = True
            End If
            myConn.Close()

        Finally

            If vEx = False Then
                Response.Redirect("PaymentSuccess.html", True)
            Else
                Response.Redirect("PaymentFailed.html", False)
            End If
        End Try





    End Sub

End Class