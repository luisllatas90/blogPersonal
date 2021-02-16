Class SurroundingClass
    Private Shared OnCacheRemove As CacheItemRemovedCallback = Nothing

    Protected Sub Application_Start(ByVal sender As Object, ByVal e As EventArgs)
        AddTask("DoStuff", 60)
    End Sub

    Private Sub AddTask(ByVal name As String, ByVal seconds As Integer)
        OnCacheRemove = New CacheItemRemovedCallback(AddressOf CacheItemRemoved)
        HttpRuntime.Cache.Insert(name, seconds, Nothing, DateTime.Now.AddSeconds(seconds), Cache.NoSlidingExpiration, CacheItemPriority.NotRemovable, OnCacheRemove)
    End Sub

    Public Sub CacheItemRemoved(ByVal k As String, ByVal v As Object, ByVal r As CacheItemRemovedReason)
        AddTask(k, Convert.ToInt32(v))
    End Sub
End Class