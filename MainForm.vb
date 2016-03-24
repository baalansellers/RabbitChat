Imports System.ComponentModel
Imports System.Threading
Imports RabbitMQ.Client
Imports RabbitMQ.Client.Events

Public Class MainForm
    Delegate Sub SetTextCallback(Text As String)

    Private p_factory As ConnectionFactory
    Private p_connection As IConnection
    Private p_channel As IModel
    Private WorkerThread As Thread = Nothing
    Private p_FormWStart As Integer = 0
    Private p_FormHStart As Integer = 0
    Private p_MinChatW As Integer = 0
    Private p_MinChatH As Integer = 0
    Private p_MinMessageW As Integer = 0
    Private p_MinMessageH As Integer = 0
    Private Property p_UserName As String
    Private WithEvents myConsumer As EventingBasicConsumer

    Private Sub Form1_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        Dim wDif As Integer = 0
        Dim hDif As Integer = 0

        'Initialize on app start
        If p_FormWStart = 0 Then
            p_FormWStart = Width
            p_FormHStart = Height

            p_MinChatW = tbChat.Width
            p_MinChatH = tbChat.Height

            p_MinMessageW = tbMessage.Width
            p_MinMessageH = tbMessage.Height
        End If

        'Calculate pixal differences
        wDif = Width - p_FormWStart
        hDif = Height - p_FormHStart

        'Width - tbConsole.Location.X
        'Transform controls
        If Width > p_FormWStart Then
            tbChat.Width = p_MinChatW + wDif
            tbMessage.Width = p_MinMessageW + wDif
        Else
            tbChat.Width = p_MinChatW
            tbMessage.Width = p_MinMessageW
        End If
        If Height > p_FormHStart Then
            tbChat.Height = p_MinChatH + hDif
            'tbMessage.Height = p_MinMessageH + hDif
        Else
            tbChat.Height = p_MinChatH
            'tbMessage.Height = p_MinMessageH
        End If

    End Sub

    Private Sub ChatAppend(Text As String)
        If tbChat.InvokeRequired Then
            Dim d As New SetTextCallback(AddressOf ChatAppend)
            Me.Invoke(d, New Object() {Text})
        Else
            tbChat.Text = Text & vbCrLf & tbChat.Text
        End If
    End Sub

    Private Sub ChatAppendFirstLine(Text As String)
        If tbChat.InvokeRequired Then
            Dim d As New SetTextCallback(AddressOf ChatAppendFirstLine)
            Me.Invoke(d, New Object() {Text})
        Else
            Dim myText As String()
            myText = tbChat.Text.Split(vbCrLf)
            myText(0) &= Text
            tbChat.Text = String.Join(vbCrLf, myText)
        End If
    End Sub

    Private Sub ChatClear()
        If tbChat.InvokeRequired Then
            Dim d As New SetTextCallback(AddressOf ChatClear)
            Me.Invoke(d, New Object() {String.Empty})
        Else
            tbChat.Text = String.Empty
        End If
    End Sub

    Private Sub btSend_Click(sender As Object, e As EventArgs) Handles btSend.Click
        If String.IsNullOrEmpty(tbMessage.Text.Trim) Then Return
        Using connection As IConnection = p_factory.CreateConnection
            Using channel As IModel = connection.CreateModel

                channel.ExchangeDeclare("rabbitchat", "fanout")

                Dim props As IBasicProperties = channel.CreateBasicProperties
                props.Persistent = True

                channel.BasicPublish(exchange:="rabbitchat",
                                     routingKey:="",
                                     basicProperties:=props,
                                     body:=System.Text.Encoding.UTF8.GetBytes(p_UserName & ": " & tbMessage.Text.Trim))
            End Using
        End Using
        tbMessage.Text = String.Empty
    End Sub

    Private Sub InitListener()
        p_channel.ExchangeDeclare("rabbitchat", "fanout")

        Dim qName As String = p_channel.QueueDeclare.QueueName
        p_channel.QueueBind(queue:=qName,
                            exchange:="rabbitchat",
                            routingKey:="")

        myConsumer = New EventingBasicConsumer(p_channel)

        p_channel.BasicConsume(queue:=qName,
                               noAck:=True,
                               consumer:=myConsumer)
    End Sub

    Private Sub myConsumer_Received(sender As Object, e As BasicDeliverEventArgs) Handles myConsumer.Received
        Me.ChatAppend(System.Text.Encoding.UTF8.GetString(e.Body))
    End Sub

    Private Sub Form1_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        InitListener()
    End Sub

    Private Sub AskForName()
        Dim nameDialog As New NameForm()
        If nameDialog.ShowDialog(Me) = DialogResult.OK Then
            p_UserName = nameDialog.tbName.Text
        End If
        nameDialog.Dispose()
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles Me.Load
        AskForName()
        p_factory = New ConnectionFactory() With {.HostName = "10.200.1.182", .UserName = "alans", .Password = "bigal12345%$"}
        p_connection = p_factory.CreateConnection
        p_channel = p_connection.CreateModel
    End Sub

    Private Sub MainForm_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        p_channel.Dispose()
        p_connection.Dispose()
    End Sub
End Class
