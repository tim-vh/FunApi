IniRead, ServerAdress, %A_ScriptDir%\FunApiClientConfig.ini, Server, ServerAdress
IniRead, ApiKey, %A_ScriptDir%\FunApiClientConfig.ini, Server, ApiKey

IniRead, MediaFile1, %A_ScriptDir%\FunApiClientConfig.ini, Media, MediaFile1
IniRead, MediaFile2, %A_ScriptDir%\FunApiClientConfig.ini, Media, MediaFile2
IniRead, MediaFile3, %A_ScriptDir%\FunApiClientConfig.ini, Media, MediaFile3
IniRead, MediaFile4, %A_ScriptDir%\FunApiClientConfig.ini, Media, MediaFile4
IniRead, MediaFile5, %A_ScriptDir%\FunApiClientConfig.ini, Media, MediaFile5
IniRead, MediaFile6, %A_ScriptDir%\FunApiClientConfig.ini, Media, MediaFile6
IniRead, MediaFile7, %A_ScriptDir%\FunApiClientConfig.ini, Media, MediaFile7
IniRead, MediaFile8, %A_ScriptDir%\FunApiClientConfig.ini, Media, MediaFile8
IniRead, MediaFile9, %A_ScriptDir%\FunApiClientConfig.ini, Media, MediaFile9

^!Numpad1::
    PlayMedia(MediaFile1)
return

^!Numpad2::
    PlayMedia(MediaFile2)
return

^!Numpad3::
    PlayMedia(MediaFile3)
return

^!Numpad4::
    PlayMedia(MediaFile4)
return

^!Numpad5::
    PlayMedia(MediaFile5)
return

^!Numpad6::
    PlayMedia(MediaFile6)
return

^!Numpad7::
    PlayMedia(MediaFile7)
return

^!Numpad8::
    PlayMedia(MediaFile8)
return

^!Numpad9::
    PlayMedia(MediaFile9)
return

^!Numpad0::
	StopMedia()
return

PlayMedia(file) {
    global ServerAdress
    HttpGet(ServerAdress "/api/media/play/" file)
}
StopMedia() {
    global ServerAdress
    HttpGet(ServerAdress "/api/media/stop")
}

HttpGet(url) {
    global ApiKey
	static req := ComObjCreate("Msxml2.XMLHTTP")
	req.open("GET", url, false)
	req.SetRequestHeader("If-Modified-Since", "Sat, 1 Jan 2000 00:00:00 GMT")
    req.SetRequestHeader("X-API-KEY", ApiKey)
	req.send()
}