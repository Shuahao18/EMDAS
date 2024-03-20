Imports System.Text.RegularExpressions

Public Class Form1
    Private calculationPerformed As Boolean = False

    Private Sub display(num As String)
        outputScreen.Text &= num
    End Sub

    Private Sub calculate()
        Try
            Dim expression As String = outputScreen.Text.Replace("x", "*")
            expression = expression.Replace("%", "/100") ' Replace % with /100 for modulo operation
            Dim result As Double = EvaluateExpression(expression)
            outputScreen.Text = result.ToString()
            UpdateHistory(expression, result.ToString())
            ' Set the flag to true after calculation
            calculationPerformed = True
        Catch ex As Exception
            MessageBox.Show("Invalid Expression!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Private Sub Clear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        outputScreen.Text = ""
    End Sub

    Private Sub del()
        If outputScreen.Text.Length > 0 Then
            outputScreen.Text = outputScreen.Text.Substring(0, outputScreen.Text.Length - 1)
        End If
    End Sub

    Private Function EvaluateExpression(expression As String) As Double
        ' Use regular expressions to find and replace parentheses with calculated values
        Dim pattern As String = "\(([^()]+)\)"
        Dim regex As New Regex(pattern)

        While regex.IsMatch(expression)
            expression = regex.Replace(expression, Function(match) EvaluateWithoutParentheses(match.Groups(1).Value).ToString())
        End While

        ' Evaluate the modified expression without parentheses
        Return EvaluateWithoutParentheses(expression)
    End Function

    Private Function EvaluateWithoutParentheses(expression As String) As Double
        Dim dt As New DataTable()
        Dim result As Object = dt.Compute(expression, "")
        Return Convert.ToDouble(result)
    End Function

    Private Sub btnNumber_Click(sender As Object, e As EventArgs) Handles btnZero.Click, btnOne.Click, btnTwo.Click, btnThree.Click,
                                                                             btnFour.Click, btnFive.Click, btnSix.Click, btnSeven.Click,
                                                                             btnEight.Click, btnNine.Click, btnDot.Click
        Dim button As Button = CType(sender, Button)
        ' Clear output screen if calculation has been performed
        If calculationPerformed Then
            outputScreen.Text = ""
            calculationPerformed = False ' Reset the flag
        End If
        display(button.Text)
    End Sub

    Private Sub btnOperator_Click(sender As Object, e As EventArgs) Handles btnPlus.Click, btnMinus.Click, btnMultiply.Click, btnDivide.Click, btnModulo.Click
        Dim button As Button = CType(sender, Button)
        display($" {button.Text} ")
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        del()
    End Sub

    Private Sub btnEquals_Click(sender As Object, e As EventArgs) Handles btnEquals.Click
        calculate()
    End Sub

    ' Clear button handler
    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        outputScreen.Text = ""
    End Sub

    ' Update the history label
    Private Sub UpdateHistory(expression As String, result As String)
        historyLabel.Text &= $"{expression} = {result}{Environment.NewLine}"
    End Sub

    Private Sub btnDeleteHistory_Click(sender As Object, e As EventArgs) Handles btnDeleteHistory.Click
        ' Clear the text of the history label
        historyLabel.Text = ""
    End Sub

End Class
