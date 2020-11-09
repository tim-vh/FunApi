Get-ChildItem "." | 
Foreach-Object {    
    ffmpeg -itsoffset -1 -i $_.Name -vcodec mjpeg -vframes 1 -an -f rawvideo -s 320x180 -y (".\thumbnails\" + $_.Name + ".jpeg")
}