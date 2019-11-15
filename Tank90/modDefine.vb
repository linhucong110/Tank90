Module modDefine

    Public CParams As New clsParams

    Public Function RandInt(ByVal x As Integer, ByVal y As Integer) As Integer
        Randomize()

        Return CInt(Int((y - x + 1) * Rnd()) + x)
    End Function

    Public Function RandFloat() As Double
        Randomize()
        Return RandInt(0, 32737) / 32768
    End Function

    Public Function RandBool() As Boolean
        If (RandInt(0, 1)) = 1 Then
            Return True
        Else
            Return False
        End If

    End Function

    Public Function RandomClamped() As Double
        Return RandFloat() - RandFloat()
    End Function

    ''' <summary>
    ''' 根据驾驶力在给定的左右范围进行左右转向
    ''' </summary>
    ''' <param name="arg">驾驶力</param>
    ''' <param name="min">最小范围</param>
    ''' <param name="max">最大范围</param>
    ''' <remarks></remarks>
    Public Sub Clamp(ByRef arg As Double, ByVal min As Double, ByVal max As Double)
        If arg < min Then
            arg = min
        End If

        If arg > max Then
            arg = max
        End If
    End Sub

    Public Sub Clamp(ByRef arg As Integer, ByVal min As Integer, ByVal max As Integer)
        If arg < min Then
            arg = min
        End If

        If arg > max Then
            arg = max
        End If
    End Sub

    Public Function CompareArray(ByVal arr1 As List(Of Double), ByVal arr2 As List(Of Double)) As Boolean
        Dim i As Integer

        For i = 0 To arr1.Count - 1
            If arr1(i) <> arr2(i) Then
                Return False
            End If
        Next
        Return True
    End Function
End Module
