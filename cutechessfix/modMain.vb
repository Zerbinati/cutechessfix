Module modMain
    Public tabOutput(1000000) As String
    Public indexOutput As Long
    Public goodStart As Boolean

    Sub Main()
        Dim arguments As String, indexActual As Long, i As Long, procCUTECHESS As System.Diagnostics.Process

        'récupérer les arguments
        arguments = Command()

cutechess:
        'lancer cutechess + arguments
        procCUTECHESS = New System.Diagnostics.Process

        Array.Clear(tabOutput, 0, tabOutput.Length)
        indexOutput = 0
        indexActual = 0
        goodStart = False

        procCUTECHESS.StartInfo.UseShellExecute = False

        procCUTECHESS.StartInfo.RedirectStandardOutput = True
        AddHandler procCUTECHESS.OutputDataReceived, AddressOf evenement

        procCUTECHESS.StartInfo.WorkingDirectory = My.Application.Info.DirectoryPath
        procCUTECHESS.StartInfo.FileName = "cutechess-cli.exe"
        procCUTECHESS.StartInfo.Arguments = arguments

        Console.WriteLine("Start...")
        Try
            procCUTECHESS.Start()
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try

        procCUTECHESS.PriorityClass = ProcessPriorityClass.Idle

        Console.WriteLine("BeginOutputReadLine...")
        Try
            procCUTECHESS.BeginOutputReadLine()
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try

        Do
            If indexOutput > indexActual Then
                For i = indexActual + 1 To indexOutput
                    Console.WriteLine(tabOutput(i))
                    If InStr(tabOutput(i), "Started game 1 of", CompareMethod.Text) > 0 Then
                        goodStart = True
                    End If

                    If Not goodStart And InStr(tabOutput(i), "Terminating process", CompareMethod.Text) > 0 Then

                        Console.WriteLine("CancelOutputRead...")
                        Try
                            procCUTECHESS.CancelOutputRead()
                        Catch ex As Exception
                            Console.WriteLine(ex.Message)
                        End Try
                        Threading.Thread.Sleep(1000)

                        Console.WriteLine("Close...")
                        Try
                            procCUTECHESS.Close()
                        Catch ex As Exception
                            Console.WriteLine(ex.Message)
                        End Try
                        Threading.Thread.Sleep(1000)

                        procCUTECHESS = Nothing

                        Console.WriteLine("retrying in 30s...")
                        Threading.Thread.Sleep(30000)

                        GoTo cutechess
                    End If

                    If InStr(tabOutput(i), "Finished match", CompareMethod.Text) > 0 Then
                        indexOutput = i
                        Exit For
                    End If
                Next
                indexActual = indexOutput
            End If
            Threading.Thread.Sleep(1000)
        Loop Until InStr(tabOutput(indexOutput), "Finished match", CompareMethod.Text) > 0

        If Not goodStart Then

            Console.WriteLine("CancelOutputRead...")
            Try
                procCUTECHESS.CancelOutputRead()
            Catch ex As Exception
                Console.WriteLine(ex.Message)
            End Try
            Threading.Thread.Sleep(1000)

            Console.WriteLine("Close...")
            Try
                procCUTECHESS.Close()
            Catch ex As Exception
                Console.WriteLine(ex.Message)
            End Try
            Threading.Thread.Sleep(1000)

            procCUTECHESS = Nothing

            Console.WriteLine("retrying in 30s...")
            Threading.Thread.Sleep(30000)

            GoTo cutechess
        End If

    End Sub

    Private Sub evenement(sendingProcess As Object, donnees As DataReceivedEventArgs)
        indexOutput = indexOutput + 1
        tabOutput(indexOutput) = donnees.Data
    End Sub

End Module
