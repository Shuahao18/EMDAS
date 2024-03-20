Public Class history
    Private Sub historyLabel_TextChanged(sender As Object, e As EventArgs) Handles historyLabel.TextChanged

    End Sub
    Public Property HistoryData As String
    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        historyLabel.Text = HistoryData
    End Sub

    Private Sub btnHistory_Click(sender As Object, e As EventArgs) Handles btnHistory.Click
        historyLabel.Clear()
        If (historyLabel.Text = "") Then
        End If
        btnHistory.Visible = True
        historyLabel.ScrollBars = 0
    End Sub
End Class